# RetroGameStore - Student Setup Guide
Follow these step-by-step instructions to set up, configure, run, and push the **RetroGameStore** ASP.NET Core MVC application to your personal GitHub account.

---

### Step 1: Download and Extract the Project
1. Navigate to the public GitHub repository link provided by your instructor.
2. Click the green **Code** button and select **Download ZIP**.
3. Locate the downloaded ZIP file in your `Downloads` folder.
4. **Extract/Unzip** the contents to your working directory (e.g., `Documents` or a dedicated `Projects` folder).
5. Open the extracted folder and double-click the **`RetroGameStore.slnx`** file to open the solution in **Visual Studio**.

---

### Step 2: Create Your Local Database in SSMS
1. Launch **SQL Server Management Studio (SSMS)**.
2. Connect to your local SQL Server instance (typically `(localdb)\mssqllocaldb`).
3. In the **Object Explorer**, right-click the **Databases** folder and select **New Database...**.
4. Set the **Database name** to exactly: `retrogamestore`
5. Click **OK** to create the database.

---

### Step 3: Configure `appsettings.json`
1. In Visual Studio, open the **`appsettings.json`** file from the Solution Explorer.
2. Locate the **`ConnectionStrings`** section and verify that your `DefaultConnection` matches the following string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=retrogamestore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```
3. Save the file (`Ctrl + S`).

---

### Step 4: Apply Database Migrations
1. Open the Package Manager Console in Visual Studio by going to:
   > **Tools** > **NuGet Package Manager** > **Package Manager Console**
2. Run the following command to create the database schema and tables:
   ```powershell
   Update-Database
   ```
3. Switch back to **SSMS**, expand the `retrogamestore` database, and refresh the **Tables** folder to verify that all required tables have been generated.

---

### Step 5: Build and Run the Application
1. In Visual Studio, press **F5** (or click the **Start / IIS Express** button in the top toolbar).
2. The project will build and launch the web application on `localhost` in your browser.

---

### Step 6: Create Your Own Repository & Push to GitHub
1. Make sure you are signed into your GitHub account inside Visual Studio (**File** > **Account Settings...**).
2. Go to the top menu and click **Git** > **Create Git Repository...** (or click **Add to Source Control** in the bottom-right status bar).
3. In the creation window:
   * Select **GitHub** under *Push to a new remote*.
   * Verify your GitHub account is selected.
   * Enter your preferred **Repository Name** (e.g., `RetroGameStore-Assignment`).
   * Select **Public** or **Private**.
4. Click **Create and Push**. 

Visual Studio will initialize your local Git repository, create the remote repository under your GitHub account, and push all project files automatically.
