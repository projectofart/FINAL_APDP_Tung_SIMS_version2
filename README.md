🎓 Student Information Management System (SIMS) This is a web-based Student Information Management System developed using ASP.NET Core MVC and Entity Framework Core. It allows admins, teachers, and students to manage academic information efficiently through role-based access.

✨ Features 👤 User Authentication (Admin, Teacher, Student)

📚 Course and Category Management

🏫 Classroom Scheduling and Assignment

🧑‍🏫 Teacher Profiles & Teaching Assignments

🎓 Student Profiles & Grades

📈 Dashboard for Admin Overview

📋 Soft Delete with Audit Trails

🔐 Session-based Role Authorization

🧪 xUnit Tests for Key Controllers & Logic

🛠️ Technologies ASP.NET Core MVC

  - Entity Framework Core (Code First)
  - SQL Server
  - Bootstrap 5
  - xUnit & Moq for Unit Testing

📂 Structure ASM_SIMS/ 
  ├── Controllers/ 
  ├── Models/ 
  ├── Views/ 
  ├── wwwroot/ 
  ├── DB/ (DbContext and EF Configurations) 
  ├── xUnitTest/ (Unit test project)
  
📌 How to Run Clone the repo
  -  Update the appsettings.json with your SQL Server connection string
  -  Run the app using Visual Studio or dotnet run
  -  Access: https://localhost:{port}
