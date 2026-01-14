# 🚀 TwitterClone
– Product Engineering Assignment

📌 Overview

This project is a simplified Twitter clone built as part of the Product Engineering practical assignment.
The goal of the assignment was to design and implement a functional product-like solution, focusing on architecture, clean code, separation of concerns, and interactive features.

The application supports core social media functionalities such as authentication, posting content, interacting with posts, and viewing a feed, implemented with a clear separation between backend and frontend.

---

## 🏗 Architecture Overview

### 🔹 Backend (ASP.NET Core Web API)
- RESTful API handling authentication, posts, likes, retweets, comments and profiles
- Stateless JWT authentication
- Services & repositories to maintain clean separation of concerns
- Data persistence with Entity Framework Core and MSSQL
- Swagger / OpenAPI documentation

### 🔹 Frontend (HTML, CSS, JavaScript)
- Client-side web application
- Communicates with backend via HTTP API (`fetch`)
- JWT stored in `localStorage` for authentication
- SPA-like navigation using page redirects
- Modern UI with dark/light theme, responsive layout, and modular JS code

---

## 🛠 Technologies Used

**Backend:** ASP.NET Core Web API, Entity Framework Core, JWT Authentication, Swagger / OpenAPI, Dependency Injection, MSSQL  

**Frontend:** HTML5, CSS3, Vanilla JavaScript (ES6 Modules), Fetch API, LocalStorage

---

▶ How to Run the Project Locally

*Backend*

Open the backend solution in Visual Studio

Update the database connection string in appsettings.json if needed

Run the project

Swagger will be available at: https://localhost:7211/swagger

*Frontend*

Open the twitter-frontend folder in Visual Studio Code

Install and use Live Server

Start Live Server and open:

http://127.0.0.1:5500/login.html

---

## ✅ Features Implemented

**Core Features (Mandatory)**
- User registration & login
- Create posts (tweets)
- Feed displaying posts 
- Like, retweet, and comment on posts
- User profile page
- Responsive design (works on mobile & desktop)
- Dark/light theme toggle

**Additional Features (Bonus)**
- Display own and other users’ posts
- Comments section for posts
- Prevent duplicate likes
- Protected routes using client-side guards
- Improved UI/UX inspired by Twitter
- Modular JS code for maintainability

---





