Weblog Project

This project is a Weblog (Blog) application developed under the guidance of Murat Yücedağ.
Video: https://www.youtube.com/watch?v=u2q8rYBx9WA
The project is built using ASP.NET Core 5.0 and follows a clean architecture approach with Repository Pattern and N-Tier Architecture using the Code-First Approach.

Technologies Used
ASP.NET Core 5.0 – Backend framework
Entity Framework Core – ORM for database operations
Code-First Migration – Database management
Repository Pattern – Layered data access structure
N-Tier Architecture – Separation of concerns
Features
User authentication and authorization
Blog post management (CRUD operations)
Category management
Comment system
Admin panel for content management
Responsive and user-friendly interface
Installation
Clone the repository:
sh
Kopyala
Düzenle
git clone <repository-url>
Navigate to the project folder:
sh
Kopyala
Düzenle
cd BlogProject
Restore dependencies:
sh
Kopyala
Düzenle
dotnet restore
Apply migrations:
sh
Kopyala
Düzenle
dotnet ef database update
Run the project:
sh
Kopyala
Düzenle
dotnet run
Usage
User Registration & Authentication:
Users can register by providing their username, email, and password.
Secure authentication and password recovery/reset options are available.
Blog Post Management:
Users can create, edit, update, and delete blog posts.
Blogs can include title, content, and an optional featured image.
Posts can be drafted before publishing or made public immediately.
Category Management:
Blogs can be categorized under different categories.
Users can filter and search for blogs based on categories.
Comment System:
Users can leave comments on blog posts.
Admins can moderate, approve, or delete comments.
Users can reply to existing comments, creating discussion threads.
Admin Panel:
Admin users have a dashboard to manage users, blog posts, and comments.
Admins can restrict or grant user permissions.
Profile Management:
Users can update their profile information.
Users can change their password and profile picture.
Notifications & User Engagement:
Users get notified when someone comments on their blog post.
Email notifications for new posts or updates.
My Contributions (Beyond the Tutorial)
Category Sorting by Blog Count:
Categories in the blog panel are now sorted based on the number of blogs they contain.
Blog Rating System:
Users can rate blogs while commenting.
The blog panel displays the average rating for each blog.
Recent & Most Engaged Posts:
Added "Recent Posts" and "Most Engaged Posts" sections based on comment count.
Enhanced Author Panel:
Authors can upload up to 5 images from their local device while adding/editing a blog post or updating their profile.
Improved Notification Display in Author Panel:
Enhanced visibility and structure of notifications for a better user experience.
Admin Panel Enhancements:
Added an "About Us" section where admins can edit content and upload images.
Improved the message panel to show read/unread status.
Created a "Contact" section allowing users to send messages directly to the admin panel.
Implemented a comment moderation system where admins can review and delete spam/inappropriate comments.
Added a system for listing notifications in the admin panel, allowing admins to create new notifications.
Ensured that notifications created in the admin panel are visible to all authors in their dashboards.
License
This project is licensed under the MIT License.

![Image](https://github.com/user-attachments/assets/b1b560c3-feb1-4fd8-b209-579ff9efc203)

![Image](https://github.com/user-attachments/assets/8c614c12-ab9d-484d-ac65-f49fec02bfd4)
![Image](https://github.com/user-attachments/assets/31e6aca9-b164-41f6-8e0e-81cd7e38f332)
![Image](https://github.com/user-attachments/assets/c07450a0-8cd1-442a-8765-a2a401b0df41)
![Image](https://github.com/user-attachments/assets/93495449-2f5b-4c87-a899-1f2dcc0116ca)
![Image](https://github.com/user-attachments/assets/0f7f3b91-c8ab-42b0-8dde-8f8c5688ddba)
![Image](https://github.com/user-attachments/assets/32f43923-2c13-45a1-9b2c-60c515d9d199)
![Image](https://github.com/user-attachments/assets/440e5026-456a-49da-9040-c2bc0bcad6cb)
![Image](https://github.com/user-attachments/assets/263c853e-ed30-49df-b583-ff0066f32a54)
![Image](https://github.com/user-attachments/assets/303263ca-ffdd-4c0e-8dfd-ea57d2bb0d30)
![Image](https://github.com/user-attachments/assets/522bdfd0-bb74-459f-a8a8-b5af4603ae37)
![Image](https://github.com/user-attachments/assets/ae7cdf63-8e16-4d6d-9da5-2f0bce99b335)
![Image](https://github.com/user-attachments/assets/3df6b101-a7c8-481a-b423-2d4364a543a8)
![Image](https://github.com/user-attachments/assets/f5b9f423-5e13-4035-af67-8ea0841cc6db)
![Image](https://github.com/user-attachments/assets/d45e39a5-cefe-4373-b0fa-4827dd2cbab2)
![Image](https://github.com/user-attachments/assets/6dcdcf6e-c09a-4209-8e4d-edcd18602cf6)
![Image](https://github.com/user-attachments/assets/e300865d-8a6b-49b6-bea1-79ef3d6af3ac)
![Image](https://github.com/user-attachments/assets/f445de50-6ab3-446d-9cf4-dd05a727fd1c)


