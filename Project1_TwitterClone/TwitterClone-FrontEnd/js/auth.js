import { loginUser, registerUser } from "./api.js";

// LOGIN
document.getElementById("login-btn")?.addEventListener("click", async () => {
    try {
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;

        const res = await loginUser({ username, password });
        localStorage.setItem("jwt", res.Token);

        window.location.href = "home.html";
    } catch (err) {
        alert(err);
    }
});

// REGISTER
document.getElementById("register-btn")?.addEventListener("click", async () => {
    try {
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;
        const confirm = document.getElementById("confirm").value;

        await registerUser({
            username,
            password,
            confirmPassword: confirm,
            firstName: "Test",
            lastName: "User"
        });

        alert("Registered successfully");
        window.location.href = "login.html";
    } catch (err) {
        alert(err);
    }
});
