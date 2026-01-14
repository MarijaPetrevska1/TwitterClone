const API_BASE = "https://localhost:7211/api";

// ---------- AUTH ----------
export async function loginUser(user) {
  const res = await fetch(`${API_BASE}/Users/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user),
  });

  if (!res.ok) throw new Error(await res.text());
  const data = await res.json(); 
  localStorage.setItem("jwt", data.token);
  return data;
}

export async function registerUser(user) {
  const res = await fetch(`${API_BASE}/Users/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user),
  });

  if (!res.ok) throw new Error(await res.text());
  return res.text();
}

// ---------- POSTS ----------
function authHeader() {
  const token = localStorage.getItem("jwt");
  return token ? { Authorization: `Bearer ${token}` } : {};
}

export async function getFeed() {
  const res = await fetch(`${API_BASE}/posts/feed`, {
    headers: authHeader(),
  });
  if (!res.ok) throw new Error(await res.text());
  return res.json();
}

export async function createPost(content) {
  const res = await fetch(`${API_BASE}/posts`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      ...authHeader(),
    },
    body: JSON.stringify({ content }),
  });
  if (!res.ok) throw new Error(await res.text());
  return res.json();
}

export async function toggleLike(postId) {
  const res = await fetch(`${API_BASE}/posts/${postId}/like`, {
    method: "POST",
    headers: authHeader(),
  });
  if (!res.ok) throw new Error(await res.text());
  return res.json();
}

export async function retweetPost(postId) {
  const res = await fetch(`${API_BASE}/posts/${postId}/retweet`, {
    method: "POST",
    headers: authHeader(),
  });
  if (!res.ok) throw new Error(await res.text());
  return res.json();
}

// ---------- COMMENTS ----------

// Get all comments for a post
export async function getComments(postId) {
  const res = await fetch(`${API_BASE}/posts/${postId}/comments`, {
    headers: authHeader(),
  });
  if (!res.ok) throw new Error(await res.text());
  return res.json(); 
}

// Add a comment
export async function addComment(postId, content) {
  const res = await fetch(`${API_BASE}/posts/${postId}/comment`, { 
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      ...authHeader(),
    },
    body: JSON.stringify({ content }), 
  });
  if (!res.ok) throw new Error(await res.text());
  return res.json();
}