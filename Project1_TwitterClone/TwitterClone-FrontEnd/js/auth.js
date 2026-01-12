import { loginUser, registerUser } from "./api.js";

// ===== LOGIN =====
document.getElementById("login-btn")?.addEventListener("click", async () => {
    try {
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;

        if (!username || !password) return alert("Enter username and password");

        await loginUser({ username, password });

        window.location.href = "home.html"; // после login
    } catch (err) {
        alert("Login failed: " + err.message);
        console.error(err);
    }
});

// ===== REGISTER =====
document.getElementById("register-btn")?.addEventListener("click", async () => {
    try {
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;
        const confirm = document.getElementById("confirm").value;

        if (!username || !password || !confirm) return alert("Fill all fields");
        if (password !== confirm) return alert("Passwords do not match");

        await registerUser({ username, password, confirmPassword: confirm });

        alert("Registered successfully");
        window.location.href = "login.html";
    } catch (err) {
        alert("Register failed: " + err.message);
        console.error(err);
    }
});
