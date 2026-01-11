
import { getFeed, createPost, toggleLike, retweetPost } from "./api.js";

const feedUl = document.getElementById("feed");
const createBtn = document.getElementById("create-post-btn");
const logoutBtn = document.getElementById("logout-btn");

// ===== LOGOUT =====
logoutBtn.addEventListener("click", () => {
    localStorage.removeItem("jwt");
    window.location.href = "login.html";
});

// ===== CREATE POST =====
createBtn.addEventListener("click", async () => {
    const content = document.getElementById("new-post-content").value.trim();
    if (!content) return alert("Write something to tweet!");

    try {
        await createPost(content);
        document.getElementById("new-post-content").value = "";
        loadFeed();
    } catch (err) {
        alert("Failed to create post");
        console.error(err);
    }
});

// ===== LOAD FEED =====
export async function loadFeed() {
    feedUl.innerHTML = "";
    try {
        const posts = await getFeed();

        posts.forEach((p) => {
            const li = document.createElement("li");
            li.innerHTML = `
                <strong>${p.username}</strong>: ${p.content} <br>
                Likes: ${p.likesCount} 
                <button class="like-btn" data-id="${p.id}">Like</button>
                <button class="retweet-btn" data-id="${p.id}">Retweet</button>
            `;
            feedUl.appendChild(li);
        });

        // Add button events
        document.querySelectorAll(".like-btn").forEach((btn) => {
            btn.addEventListener("click", async () => {
                try {
                    await toggleLike(btn.dataset.id);
                    loadFeed();
                } catch (err) {
                    alert("Failed to like post");
                    console.error(err);
                }
            });
        });

        document.querySelectorAll(".retweet-btn").forEach((btn) => {
            btn.addEventListener("click", async () => {
                try {
                    await retweetPost(btn.dataset.id);
                    loadFeed();
                } catch (err) {
                    alert("Failed to retweet");
                    console.error(err);
                }
            });
        });
    } catch (err) {
        console.error("Failed to load feed:", err);
        feedUl.innerHTML = "<li>Failed to load feed. Make sure you are logged in.</li>";
    }
}

// ===== AUTO LOAD FEED =====
if (!localStorage.getItem("jwt")) {
    window.location.href = "login.html";
} else {
    loadFeed();
}
