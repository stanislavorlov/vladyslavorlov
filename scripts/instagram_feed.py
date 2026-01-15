import requests
import xml.etree.ElementTree as ET
import json
import os
import re

RSS_FEED_URL = "https://rss.app/feeds/7CO2DuDg5iozsxY4.xml" # Replace this
SPECIAL_TAG = ""

def download_image(url, save_path):
    try:
        response = requests.get(url, stream=True, timeout=10)
        if response.status_code == 200:
            with open(save_path, 'wb') as f:
                for chunk in response.iter_content(1024):
                    f.write(chunk)
            return True
    except Exception as e:
        print(f"Error downloading {url}: {e}")
    return False

def main():
    print("Fetching RSS feed...")
    resp = requests.get(RSS_FEED_URL)
    if resp.status_code != 200:
        print("Error fetching feed")
        return

    root = ET.fromstring(resp.content)
    posts = []

    namespaces = {
        'media': 'http://search.yahoo.com/mrss/',
        'dc': 'http://purl.org/dc/elements/1.1/'
    }

    # Ensure directories exist
    os.makedirs("data", exist_ok=True)
    os.makedirs("images", exist_ok=True)

    # RSS feeds usually have <item> tags
    for item in root.findall(".//item"):
        title = item.find("title").text if item.find("title") is not None else ""
        description = item.find("description").text if item.find("description") is not None else ""
        link = item.find("link").text
        
        # Combine title/desc to find the tag
        full_text = (title + " " + description)
        
        if SPECIAL_TAG in full_text or not SPECIAL_TAG:
            # Extracting the image URL
            media_content = item.find("media:content", namespaces)
            img_url = media_content.attrib.get("url") if media_content is not None else ""
            
            # If media:content is missing, try to find an img tag in description as fallback
            if not img_url and "<img" in description:
                try:
                    match = re.search(r'<img [^>]*src="([^"]+)"', description)
                    if match:
                        img_url = match.group(1)
                except Exception:
                    pass

            if img_url:
                img_name = f"post_{len(posts)}.jpg"
                img_path = os.path.join("data", img_name)
                print(f"Downloading image for post {len(posts)}...")
                if download_image(img_url, img_path):
                    img_url = img_path

            posts.append({
                "url": link,
                "image": img_url,
                "caption": title.strip()
            })
            
        if len(posts) >= 6:
            break

    # Save to JSON
    with open("data/instagram.json", "w") as f:
        json.dump(posts, f, indent=2)
    print(f"Saved {len(posts)} posts to data/instagram.json")

if __name__ == "__main__":
    main()