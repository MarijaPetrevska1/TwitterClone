import { loginUser, registerUser } from "./api.js";

// LOGIN
const loginBtn = document.getElementById("login-btn");
if (loginBtn) {
    loginBtn.addEventListener("click", async () => {
        try {
            const username = document.getElementById("username").value;
            const password = document.getElementById("password").value;

            const res = await loginUser({ username, password });

            localStorage.setItem("jwt", res.Token);

            window.location.href = "home.html";
        } catch (err) {
            alert("Login failed");
            console.error(err);
        }
    });
}

// REGISTER
const registerBtn = document.getElementById("register-btn");
if (registerBtn) {
    registerBtn.addEventListener("click", async () => {
        try {
            const username = document.getElementById("username").value;
            const password = document.getElementById("password").value;
            const confirm = document.getElementById("confirm").value;

            if (password !== confirm) {
                alert("Passwords do not match");
                return;
            }

            await registerUser({ username, password, confirmPassword: confirm });
            alert("Registered successfully");
            window.location.href = "login.html";
        } catch (err) {
            alert("Register failed");
            console.error(err);
        }
    });
}


