Weblog Project

This project is a Weblog (Blog) application developed under the guidance of Murat Yücedağ. The full development process can be followed in the accompanying YouTube video. It is built using ASP.NET Core 8.0 and follows a clean architecture approach with Repository Pattern and N-Tier Architecture using the Code-First approach.

🛠️ Technologies Used

ASP.NET Core 8.0 — Backend framework

Entity Framework Core — ORM for database operations

Code-First Migration — Database schema management

Repository Pattern — Layered data access structure

N-Tier Architecture — Clear separation of concerns

✨ Core Features

User Authentication & Authorization

Blog Post Management (Create, Read, Update, Delete)

Category Management

Comment System with moderation

Admin Panel for full content management

Responsive & user-friendly interface

⚙️ Installation

git clone <repository-url>
cd BlogProject
dotnet restore
dotnet ef database update
dotnet run

👁️ Usage Overview

👤 User Registration & Authentication

Users can register with username, email, and password.

Secure login and password recovery/reset options.

📄 Blog Post Management

Create, edit, update, and delete blog posts.

Posts include title, content, and optional featured image.

Support for draft or immediate publishing.

📅 Category Management

Blogs are categorized.

Filter/search functionality by category.

💬 Comment System

Users can comment and reply to blogs.

Admins can moderate, approve, or delete comments.

📆 Admin Panel

Manage users, posts, comments, and messages.

Grant or restrict permissions.

👤 Profile Management

Users can update profile info, password, and profile picture.

📢 Notifications & Engagement

Users get notified about new comments.

Optional email notifications for updates or new posts.

🌟 My Contributions (Beyond the Tutorial)

↑ Category Sorting by Blog Count

Categories are sorted based on how many blogs they contain.

⭐ Blog Rating System

Users can rate blogs during commenting.

Average rating is displayed per blog.

⏰ Recent & Most Engaged Posts

Display "Recent Posts" and "Most Commented Posts" sections.

📷 Enhanced Author Panel

Authors can upload up to 5 images for blogs or profiles.

📢 Improved Notification Display

Better structured and more visible notifications for authors.

📆 Admin Panel Enhancements

About Us section: editable content + image upload.

Message Panel: Shows read/unread status.

Contact Section: Users can message admins.

Comment Moderation: Flag/delete inappropriate comments.

Notification System: Admins can create and push notifications to authors.

https://private-user-images.githubusercontent.com/95929697/411545191-b1b560c3-feb1-4fd8-b209-579ff9efc203.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NDc2NjA2MjQsIm5iZiI6MTc0NzY2MDMyNCwicGF0aCI6Ii85NTkyOTY5Ny80MTE1NDUxOTEtYjFiNTYwYzMtZmViMS00ZmQ4LWIyMDktNTc5ZmY5ZWZjMjAzLnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNTA1MTklMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjUwNTE5VDEzMTIwNFomWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTE4ZmY0YzM0ODI1OWNkYmJhYjAxYmZmMDQ4YWVjMzdjYjVkZmZkM2I0NDY5ZGE0Nzc1YzRjNWUzOTI4MGI2YmMmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0In0.A7jWTEG5WlzJpddiC-qTTknBixiJeACRbGMaOScn5_E
