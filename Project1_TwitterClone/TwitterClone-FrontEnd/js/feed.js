import {
  getFeed,
  createPost,
  toggleLike,
  retweetPost,
  getComments,
  addComment,
} from "./api.js";

const feedUl = document.getElementById("feed");
const createBtn = document.getElementById("create-post-btn");
const logoutBtn = document.getElementById("logout-btn");
const themeBtn = document.getElementById("theme-toggle");

// ===== Theme =====
if (localStorage.getItem("theme") === "dark")
  document.body.classList.add("dark-theme");

themeBtn.addEventListener("click", () => {
  document.body.classList.toggle("dark-theme");
  localStorage.setItem(
    "theme",
    document.body.classList.contains("dark-theme") ? "dark" : "light"
  );
});

// ===== Logout =====
logoutBtn.addEventListener("click", () => {
  localStorage.removeItem("jwt");
  window.location.href = "login.html";
});

// ===== Create Post =====
createBtn.addEventListener("click", async () => {
  const content = document.getElementById("new-post-content").value.trim();
  if (!content) return;

  try {
    await createPost(content);
    document.getElementById("new-post-content").value = "";
    loadFeed();
  } catch (err) {
    console.error("Failed to create post:", err);
    alert("Failed to create post. See console.");
  }
});

// ===== Load Feed =====
export async function loadFeed() {
  if (!localStorage.getItem("jwt"))
    return (window.location.href = "login.html");

  feedUl.innerHTML = "";
  let posts = [];

  try {
    posts = await getFeed();
  } catch (err) {
    console.error("Failed to load feed:", err);
    return;
  }

  posts.forEach((p) => {
    const li = document.createElement("li");

    const userLiked = p.userLiked;

    li.innerHTML = `
      <strong>${p.username}</strong>: ${p.content} <br>
      Likes: <span class="likes-count">${p.likesCount}</span>
      <button class="like-btn ${userLiked ? "liked" : ""}" data-id="${
      p.id
    }">Like</button>
      <button class="retweet-btn" data-id="${p.id}">Retweet</button>
      <button class="comment-btn" data-id="${p.id}">Comment</button>
      <div id="comments-${p.id}" class="comments-container"></div>
    `;

    feedUl.appendChild(li);

    // ===== Like Button =====
    const likeBtn = li.querySelector(".like-btn");
    const likesCountSpan = li.querySelector(".likes-count");

    likeBtn.addEventListener("click", async () => {
      try {
        await toggleLike(p.id);
        likeBtn.classList.toggle("liked");

        let count = parseInt(likesCountSpan.textContent);
        count = likeBtn.classList.contains("liked") ? count + 1 : count - 1;
        likesCountSpan.textContent = count;
      } catch (err) {
        console.error("Failed to toggle like:", err);
      }
    });

    // ===== Retweet Button =====
    const retweetBtn = li.querySelector(".retweet-btn");
    retweetBtn.addEventListener("click", async () => {
      try {
        await retweetPost(p.id);
        loadFeed();
      } catch (err) {
        console.error("Failed to retweet:", err);
      }
    });

    // ===== Comment Button =====
    const commentBtn = li.querySelector(".comment-btn");
    const commentContainer = li.querySelector(`#comments-${p.id}`);

    commentBtn.addEventListener("click", async () => {
      if (commentContainer.innerHTML) {
        commentContainer.innerHTML = ""; 
        return;
      }

      commentContainer.innerHTML = `
        <input class="comment-input" placeholder="Write a comment...">
        <button class="submit-comment">Post</button>
        <div class="comments-list"></div>
      `;

      const list = commentContainer.querySelector(".comments-list");
      const submitBtn = commentContainer.querySelector(".submit-comment");
      const input = commentContainer.querySelector(".comment-input");

      // Load existing comments from backend
      try {
        const comments = await getComments(p.id);
        comments.forEach((c) => {
          const div = document.createElement("div");
          div.textContent = `${c.user.username}: ${c.content}`;
          div.classList.add("comment");
          list.appendChild(div);
        });
      } catch (err) {
        console.error("Failed to load comments:", err);
      }

      // Submit new comment
      submitBtn.addEventListener("click", async () => {
        if (!input.value.trim()) return;

        try {
          await addComment(p.id, input.value.trim());

          const div = document.createElement("div");
          div.textContent = `You: ${input.value.trim()}`;
          div.classList.add("comment");
          list.appendChild(div);

          input.value = "";
        } catch (err) {
          console.error("Failed to add comment:", err);
        }
      });
    });
  });
}

// ===== Auto-load feed =====
loadFeed();
