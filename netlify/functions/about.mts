import type { Config } from "@netlify/functions";
import * as cheerio from "cheerio";

// --- Types ---
interface StatsResult {
    singlesRanking: number | null;
    doublesRanking: number | null;
    davisCupWins: number | null;
    updatedAt: string;
}

// --- Scrapers (Same as before) ---
async function fetchRankings() {
    const URL = "https://www.tennisexplorer.com/player/orlov-d8e3f/";
    try {
        const response = await fetch(URL, { headers: { "User-Agent": "Bot/1.0" } });
        if (!response.ok) throw new Error("Ranking Fetch Failed");
        const html = await response.text();
        const $ = cheerio.load(html);
        const infoText = $("div#center").text();
        const singlesMatch = infoText.match(/singles:\s*\d+\.\s*\/\s*(\d+)\./i);
        const doublesMatch = infoText.match(/doubles:\s*\d+\.\s*\/\s*(\d+)\./i);
        return {
            singlesHigh: singlesMatch ? parseInt(singlesMatch[1]) : 350,
            doublesHigh: doublesMatch ? parseInt(doublesMatch[1]) : 209
        };
    } catch (e) { console.error(e); return { singlesHigh: 350, doublesHigh: 209 }; }
}

async function fetchDavisCupWins() {
    // Basic regex fallback logic
    try {
        const response = await fetch("https://www.daviscup.com/en/players/player.aspx?id=800367967");
        const html = await response.text();
        // Look for "Won" followed by a number near it, or default to 2
        const match = html.match(/"won":(\d+)/);
        return match ? parseInt(match[1]) : 2;
    } catch (e) { return 2; }
}

// --- GitHub API Helper ---
async function saveToGitHub(data: StatsResult) {
    const TOKEN = process.env.GITHUB_TOKEN;
    const REPO = process.env.GITHUB_REPO; // e.g., "username/repo"
    const PATH = "stats.json"; // File to save in root

    if (!TOKEN || !REPO) {
        console.error("Missing GITHUB_TOKEN or GITHUB_REPO env vars");
        return;
    }

    const apiUrl = `https://api.github.com/repos/${REPO}/contents/${PATH}`;

    // 1. Get current file SHA (required to update it)
    let sha = "";
    try {
        const currentFile = await fetch(apiUrl, {
            headers: { Authorization: `Bearer ${TOKEN}` }
        });
        if (currentFile.ok) {
            const json = await currentFile.json();
            sha = json.sha;
        }
    } catch (e) {
        console.log("File likely doesn't exist yet, creating new one.");
    }

    // 2. Commit the new data
    const content = Buffer.from(JSON.stringify(data, null, 2)).toString("base64");

    const updateResponse = await fetch(apiUrl, {
        method: "PUT",
        headers: {
            Authorization: `Bearer ${TOKEN}`,
            "Content-Type": "application/json",
            "User-Agent": "Netlify-Function"
        },
        body: JSON.stringify({
            message: `Daily Stats Update: ${new Date().toISOString().split('T')[0]}`,
            content: content,
            sha: sha || undefined // Only include SHA if file existed
        })
    });

    if (updateResponse.ok) {
        console.log("SUCCESS: stats.json updated in GitHub.");
    } else {
        console.error("FAILED to update GitHub:", await updateResponse.text());
    }
}

// --- Main Handler ---
export default async (req: Request) => {
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

    // Save to GitHub (Triggers Site Rebuild)
    await saveToGitHub(result);

    return new Response(JSON.stringify(result));
}

export const config: Config = {
    schedule: "0 10 * * 1" // At 10:00 on Monday.
}