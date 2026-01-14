# 📦 Inventory Management System (Full Stack)

> [!NOTE]
> 🇧🇷 **Português:** Para ler a versão em português deste README, [clique aqui](README.pt-br.md).

A product management and inventory flow control system, developed with a **REST API** backend and a responsive web interface.

![Status](https://img.shields.io/badge/Status-Completed-brightgreen)
![Tech](https://img.shields.io/badge/MySQL-Database-orange)

## 🚀 About the Project

This project was developed to meet a real-world demand for **Baademaq Assistência Técnica**, aiming to solve their inventory control issues.

Unlike simple CRUD applications, this system implements **Business Rules** to ensure inventory integrity (e.g., validation to prevent negative stock balances) and handles static file manipulation.

## 🛠 Technologies Used

**Back-end (API):**
* **C# / .NET Core:** RESTful API development.
* **Entity Framework Core:** ORM for database manipulation.
* **MySQL:** Data persistence.
* **Swagger:** Automatic endpoint documentation.
* **System.IO:** Image manipulation and storage (Uploads).

**Front-end (Client):**
* **HTML5 & CSS3:** Structure and responsive styling.
* **JavaScript:** Asynchronous API consumption (Fetch API) and dynamic DOM manipulation.

## ✨ Key Features

1.  **Full Product CRUD:** Create, Read, Update, and Delete.
2.  **Image Upload:** Management of user-uploaded files (`IFormFile`), saving to a static directory (`wwwroot`) with database referencing.
3.  **Transactional Stock Control:**
    * Specific endpoints for stock `Entry` (Inbound) and `Exit` (Outbound).
    * Business rule validation (prevents exit if stock is insufficient).
4.  **Visual Feedback:** Reactive interface that updates the product list without needing to reload the page.

## ⚙️ How to Run

### Prerequisites
* .NET SDK Installed
* MySQL Server running
* Visual Studio or VS Code

### Step-by-Step

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/brulindner/YOUR-REPO-NAME.git](https://github.com/brulindner/YOUR-REPO-NAME.git)
    ```
    *(Note: Replace `YOUR-REPO-NAME` with the actual repository name)*

2.  **Database Configuration:**
    * In the `appsettings.json` file, configure your connection string to match your MySQL setup.
    * Run Migrations to create the database:
    ```bash
    dotnet ef database update
    ```

3.  **Run the API:**
    * Open the solution in Visual Studio and run it (F5) or use `dotnet run`.
    * Note the port where the API is running (e.g., `localhost:7123`).

4.  **Run the Front-end:**
    * Open the `script.js` file and update the `const apiUrl` variable with the correct port from your running API.
    * Open `index.html` in your browser.

## 👩‍💻 Author

**Bruna Laís Lindner**
* [LinkedIn](https://www.linkedin.com/in/brulindner/)
