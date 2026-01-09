import { getFeed, createPost, toggleLike, retweetPost } from "./api.js";

const feedUl = document.getElementById("feed");

// Logout
document.getElementById("logout-btn").addEventListener("click", () => {
    localStorage.removeItem("jwt");
    window.location.href = "login.html";
});

// Create post
document.getElementById("create-post-btn").addEventListener("click", async () => {
    const content = document.getElementById("new-post-content").value;
    if (!content) return;

    await createPost(content);
    document.getElementById("new-post-content").value = "";
    loadFeed();
});

// Load feed
async function loadFeed() {
    feedUl.innerHTML = "";
    const posts = await getFeed();

    posts.forEach(p => {
        const li = document.createElement("li");
        li.innerHTML = `
            <strong>${p.username}</strong>: ${p.content}<br>
            Likes: ${p.likesCount}
            <button class="like" data-id="${p.id}">Like</button>
            <button class="retweet" data-id="${p.id}">Retweet</button>
        `;
        feedUl.appendChild(li);
    });

    document.querySelectorAll(".like").forEach(btn =>
        btn.onclick = async () => {
            await toggleLike(btn.dataset.id);
            loadFeed();
        }
    );

    document.querySelectorAll(".retweet").forEach(btn =>
        btn.onclick = async () => {
            await retweetPost(btn.dataset.id);
            loadFeed();
        }
    );
}

loadFeed();
