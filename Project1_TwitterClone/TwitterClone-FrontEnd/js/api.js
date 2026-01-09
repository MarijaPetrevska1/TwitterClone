const API_BASE = "https://localhost:7211/api";

// ---------- AUTH ----------
export async function loginUser(user) {
    const res = await fetch(`${API_BASE}/Users/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(user)
    });

    if (!res.ok) throw await res.text();
    return res.json(); // { Token: "..." }
}

export async function registerUser(user) {
    const res = await fetch(`${API_BASE}/Users/register`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(user)
    });

    if (!res.ok) throw await res.text();
    return res.text();
}

// ---------- POSTS ----------
function authHeader() {
    return {
        Authorization: `Bearer ${localStorage.getItem("jwt")}`
    };
}

export async function getFeed() {
    const res = await fetch(`${API_BASE}/posts/feed`, {
        headers: authHeader()
    });
    return res.json();
}

export async function createPost(content) {
    const res = await fetch(`${API_BASE}/posts`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            ...authHeader()
        },
        body: JSON.stringify({ content })
    });
    return res.json();
}

export async function toggleLike(id) {
    await fetch(`${API_BASE}/posts/${id}/like`, {
        method: "POST",
        headers: authHeader()
    });
}

export async function retweetPost(id) {
    await fetch(`${API_BASE}/posts/${id}/retweet`, {
        method: "POST",
        headers: authHeader()
    });
}
