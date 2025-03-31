Weblog Project

This project is a Weblog (Blog) application developed under the guidance of Murat Yücedağ.
Video: https://www.youtube.com/watch?v=u2q8rYBx9WA
The project is built using ASP.NET Core 5.0 and follows a clean architecture approach with Repository Pattern and N-Tier Architecture using the Code-First Approach.

Technologies Used: ASP.NET Core 5.0 – Backend framework, Entity Framework Core – ORM for database operations, Code-First Migration – Database management, Repository Pattern – Layered data access structure, N-Tier Architecture – Separation of concerns.

Features: User authentication and authorization, Blog post management (CRUD operations), Category management, Comment system, Admin panel for content management, Responsive and user-friendly interface.

Installation: Clone the repository with git clone <repository-url>. Navigate to the project folder with cd BlogProject. Restore dependencies using dotnet restore. Apply migrations with dotnet ef database update. Run the project using dotnet run.

Usage:
User Registration & Authentication: Users can register by providing their username, email, and password. Secure authentication and password recovery/reset options are available.
Blog Post Management: Users can create, edit, update, and delete blog posts. Blogs can include title, content, and an optional featured image. Posts can be drafted before publishing or made public immediately.
Category Management: Blogs can be categorized under different categories. Users can filter and search for blogs based on categories.
Comment System: Users can leave comments on blog posts. Admins can moderate, approve, or delete comments. Users can reply to existing comments, creating discussion threads.
Admin Panel: Admin users have a dashboard to manage users, blog posts, and comments. Admins can restrict or grant user permissions.
Profile Management: Users can update their profile information. Users can change their password and profile picture.
Notifications & User Engagement: Users get notified when someone comments on their blog post. Email notifications for new posts or updates.

My Contributions (Beyond the Tutorial):

Category Sorting by Blog Count: Categories in the blog panel are now sorted based on the number of blogs they contain.

Blog Rating System: Users can rate blogs while commenting. The blog panel displays the average rating for each blog.

Recent & Most Engaged Posts: Added "Recent Posts" and "Most Engaged Posts" sections based on comment count.

Enhanced Author Panel: Authors can upload up to 5 images from their local device while adding/editing a blog post or updating their profile.

Improved Notification Display in Author Panel: Enhanced visibility and structure of notifications for a better user experience.

Admin Panel Enhancements: Added an "About Us" section where admins can edit content and upload images. Improved the message panel to show read/unread status. Created a "Contact" section allowing users to send messages directly to the admin panel. Implemented a comment moderation system where admins can review and delete spam/inappropriate comments. Added a system for listing notifications in the admin panel, allowing admins to create new notifications. Ensured that notifications created in the admin panel are visible to all authors in their dashboards.

License: This project is licensed under the MIT License.
