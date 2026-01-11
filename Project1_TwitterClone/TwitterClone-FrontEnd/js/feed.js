import { getFeed, createPost, toggleLike, retweetPost } from './api.js';

const feedUl = document.getElementById('feed');
const createBtn = document.getElementById('create-post-btn');
const logoutBtn = document.getElementById('logout-btn');

// Logout
logoutBtn.addEventListener('click', () => {
    localStorage.removeItem('jwt');
    window.location.href = "login.html";
});

// Create post
createBtn.addEventListener('click', async () => {
    const content = document.getElementById('new-post-content').value;
    if (!content) return;
    await createPost(content);
    document.getElementById('new-post-content').value = "";
    loadFeed();
});

// Load feed
export async function loadFeed() {
    feedUl.innerHTML = "";
    const posts = await getFeed();

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

    // Add events
    document.querySelectorAll(".like-btn").forEach(btn => {
        btn.addEventListener("click", async () => {
            await toggleLike(btn.dataset.id);
            loadFeed();
        });
    });

    document.querySelectorAll(".retweet-btn").forEach(btn => {
        btn.addEventListener("click", async () => {
            await retweetPost(btn.dataset.id);
            loadFeed();
        });
    });
}

// Initial load
loadFeed();
