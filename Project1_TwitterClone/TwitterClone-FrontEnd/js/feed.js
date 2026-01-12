import { getFeed, createPost, toggleLike, retweetPost } from './api.js';

const feedUl = document.getElementById('feed');
const createBtn = document.getElementById('create-post-btn');
const logoutBtn = document.getElementById('logout-btn');
const themeBtn = document.getElementById('theme-toggle');

// ===== Theme =====
if (localStorage.getItem('theme') === 'dark') document.body.classList.add('dark-theme');

themeBtn.addEventListener('click', () => {
    document.body.classList.toggle('dark-theme');
    localStorage.setItem('theme', document.body.classList.contains('dark-theme') ? 'dark' : 'light');
});

// ===== Logout =====
logoutBtn.addEventListener('click', () => {
    localStorage.removeItem('jwt');
    window.location.href = "login.html";
});

// ===== Create Post =====
createBtn.addEventListener('click', async () => {
    const content = document.getElementById('new-post-content').value.trim();
    if (!content) return;

    try {
        await createPost(content);
        document.getElementById('new-post-content').value = "";
        loadFeed();
    } catch (err) {
        console.error("Failed to create post:", err);
        alert("Failed to create post. See console.");
    }
});

// ===== Load Feed =====
export async function loadFeed() {
    if (!localStorage.getItem("jwt")) return window.location.href = "login.html";

    feedUl.innerHTML = "";
    let posts = [];

    try {
        posts = await getFeed();
    } catch (err) {
        console.error("Failed to load feed:", err);
        return;
    }

    posts.forEach(p => {
        const li = document.createElement('li');
        li.innerHTML = `
            <strong>${p.username}</strong>: ${p.content} <br>
            Likes: ${p.likesCount}
            <button class="like-btn" data-id="${p.id}">Like</button>
            <button class="retweet-btn" data-id="${p.id}">Retweet</button>
        `;
        feedUl.appendChild(li);
    });

    // Add like events
    document.querySelectorAll(".like-btn").forEach(btn => {
        btn.addEventListener("click", async () => {
            try {
                await toggleLike(btn.dataset.id);
                loadFeed();
            } catch (err) {
                console.error("Failed to toggle like:", err);
            }
        });
    });

    // Add retweet events
    document.querySelectorAll(".retweet-btn").forEach(btn => {
        btn.addEventListener("click", async () => {
            try {
                await retweetPost(btn.dataset.id);
                loadFeed();
            } catch (err) {
                console.error("Failed to retweet:", err);
            }
        });
    });
}

// ===== Auto-load feed =====
loadFeed();
