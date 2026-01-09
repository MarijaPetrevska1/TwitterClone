# 🚀 TwitterClone
– Product Engineering Assignment

📌 Overview

This project is a simplified Twitter clone built as part of the Product Engineering practical assignment.
The goal of the assignment was to design and implement a functional product-like solution, focusing on architecture, logic, code structure, and problem-solving, rather than just visual polish.
The application supports core social media functionalities such as authentication, posting content, interacting with posts, and viewing a feed, implemented with a clear separation between backend and frontend.

🏗 Architecture Overview

The project is split into two clearly separated parts:

🔹 Backend (ASP.NET Core Web API)

Exposes a RESTful API
Handles authentication, business logic, and data persistence
Stateless authentication using JWT
Designed with services and repositories to ensure clean separation of concerns

🔹 Frontend (HTML, CSS, JavaScript)

Pure client-side web application
Communicates with backend exclusively via HTTP API calls (fetch)
Uses JWT stored in localStorage for authenticated requests
Simple SPA-style navigation using page redirects

🛠 Technologies Used

Backend: ASP.NET Core Web API, Entity Framework Core, JWT Authentication, Swagger / OpenAPI, Dependency Injection, MSSQL (or configured database)

Frontend: HTML5, CSS3, Vanilla JavaScript (ES Modules), Fetch API, LocalStorage for token management

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
