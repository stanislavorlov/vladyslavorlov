import type { Config } from "@netlify/functions";
import * as cheerio from "cheerio";

// --- Types ---
interface StatsResult {
    singlesRanking: number | null;
    doublesRanking: number | null;
    davisCupWins: number | null;
    updatedAt: string;
}

// --- Scrapers ---

/**
 * SOURCE 1: TennisExplorer
 * Why: ATP blocks serverless functions (403). TennisExplorer is lighter and reliable.
 */
async function fetchRankings() {
    const URL = "https://www.tennisexplorer.com/player/orlov-d8e3f/";

    try {
        const response = await fetch(URL, {
            headers: { "User-Agent": "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)" }
        });

        if (!response.ok) throw new Error(`TennisExplorer Failed: ${response.status}`);

        const html = await response.text();
        const $ = cheerio.load(html);

        // The text usually looks like: "Current/Highest rank - singles: 498. / 350."
        const infoText = $("div#center").text();

        // Regex to find "singles: <current> / <high>"
        const singlesMatch = infoText.match(/singles:\s*\d+\.\s*\/\s*(\d+)\./i);
        const doublesMatch = infoText.match(/doubles:\s*\d+\.\s*\/\s*(\d+)\./i);

        return {
            singlesHigh: singlesMatch ? parseInt(singlesMatch[1]) : 350, // Fallback to known 350 if parse fails
            doublesHigh: doublesMatch ? parseInt(doublesMatch[1]) : 209
        };

    } catch (error) {
        console.error("Ranking Scrape Error:", error);
        return { singlesHigh: null, doublesHigh: null };
    }
}

/**
 * SOURCE 2: Davis Cup
 * Strategy: Try JSON parsing first, fall back to Regex text search.
 */
async function fetchDavisCupWins() {
    const URL = "https://www.daviscup.com/en/players/player.aspx?id=800367967";
    // Note: Used the permanent ID URL which redirects correctly

    try {
        const response = await fetch(URL, {
            headers: { "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36" }
        });

        if (!response.ok) throw new Error(`Davis Cup Fetch Failed: ${response.status}`);

        const html = await response.text();

        // METHOD A: Regex Text Search (Most Robust)
        // Looks for patterns like "Win-Loss 5-3" or raw numbers near labels
        // This is safer than parsing deep JSON structures that change often.

        // Find the "Overall" row in the stats table or text
        const $ = cheerio.load(html);

        // Usually in a table row with "Overall" -> "Won" column
        let wins = 0;

        // Try to find the specific "Won" count in the statistics section
        // Depending on layout, might be in a wrapper .statistics-module
        const winText = $("span:contains('Won')").next().text();
        if (winText && !isNaN(parseInt(winText))) {
            wins = parseInt(winText);
        } else {
            // Fallback: Crude Regex on the whole body for the Win-Loss section
            // Example data: "Overall 2 - 2" (This part is tricky, hardcoding 2 is safer if scrape fails)
            // But let's try to match the specific "wins" variable often found in scripts
            const scriptMatch = html.match(/"won":(\d+),"lost":\d+/);
            if (scriptMatch) {
                wins = parseInt(scriptMatch[1]);
            }
        }

        return wins > 0 ? wins : 2; // Default to 2 (known current) if 0/fail

    } catch (error) {
        console.error("Davis Cup Scrape Error:", error);
        return 2; // Fail-safe default
    }
}

// --- Main Handler ---

export default async (req: Request) => {
    // Run both jobs in parallel
    const [ranks, davisWins] = await Promise.all([
        fetchRankings(),
        fetchDavisCupWins()
    ]);

    const result: StatsResult = {
        singlesRanking: ranks.singlesHigh,
        doublesRanking: ranks.doublesHigh,
        davisCupWins: davisWins,
        updatedAt: new Date().toISOString()
    };

    console.log("Stats Sync Complete:", result);

    return new Response(JSON.stringify(result), {
        status: 200,
        headers: { "Content-Type": "application/json" }
    });
}

export const config: Config = {
    schedule: "@daily"
}