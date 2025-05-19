# Weblog Project

This project is a Weblog (Blog) application developed under the guidance of Murat Yücedağ. The full development process can be followed in the accompanying [YouTube video](https://www.youtube.com/watch?v=u2q8rYBx9WA). It is built using **ASP.NET Core 5.0** and follows a clean architecture approach with **Repository Pattern** and **N-Tier Architecture** using the **Code-First** approach.

## 🛠️ Technologies Used

* **ASP.NET Core 5.0** — Backend framework
* **Entity Framework Core** — ORM for database operations
* **Code-First Migration** — Database schema management
* **Repository Pattern** — Layered data access structure
* **N-Tier Architecture** — Clear separation of concerns

## ✨ Core Features

* **User Authentication & Authorization**
* **Blog Post Management** (Create, Read, Update, Delete)
* **Category Management**
* **Comment System** with moderation
* **Admin Panel** for full content management
* **Responsive & user-friendly interface**

## ⚙️ Installation

```bash
git clone <repository-url>
cd BlogProject
dotnet restore
dotnet ef database update
dotnet run
```

## 👁️ Usage Overview

### 👤 User Registration & Authentication

* Users can register with username, email, and password.
* Secure login and password recovery/reset options.

### 📄 Blog Post Management

* Create, edit, update, and delete blog posts.
* Posts include title, content, and optional featured image.
* Support for draft or immediate publishing.

### 📅 Category Management

* Blogs are categorized.
* Filter/search functionality by category.

### 💬 Comment System

* Users can comment and reply to blogs.
* Admins can moderate, approve, or delete comments.

### 📆 Admin Panel

* Manage users, posts, comments, and messages.
* Grant or restrict permissions.

### 👤 Profile Management

* Users can update profile info, password, and profile picture.

### 📢 Notifications & Engagement

* Users get notified about new comments.
* Optional email notifications for updates or new posts.

## 🌟 My Contributions (Beyond the Tutorial)

### ↑ Category Sorting by Blog Count

* Categories are sorted based on how many blogs they contain.

### ⭐ Blog Rating System

* Users can rate blogs during commenting.
* Average rating is displayed per blog.

### ⏰ Recent & Most Engaged Posts

* Display "Recent Posts" and "Most Commented Posts" sections.

### 📷 Enhanced Author Panel

* Authors can upload up to 5 images for blogs or profiles.

### 📢 Improved Notification Display

* Better structured and more visible notifications for authors.

### 📆 Admin Panel Enhancements

* **About Us** section: editable content + image upload.
* **Message Panel**: Shows read/unread status.
* **Contact Section**: Users can message admins.
* **Comment Moderation**: Flag/delete inappropriate comments.
* **Notification System**: Admins can create and push notifications to authors.

[![Admin Panel Example](sandbox:/mnt/data/94039720-c806-46e3-ad94-77dadc2713fd.png)
](https://github.com/ilhan2651/BlogProject/issues/1#issue-2842211605)
## 📄 License

This project is developed for educational purposes and is open for improvement and extension.
