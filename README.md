**This project is made using dotnet core MVC and Web api as backend.**
---------------------------------------------------------------------------
Make sure you have all necessary packages install in your visual studio to run a dotnet core MVC ptoject.


Step By Step Appoarch to run the Application on your system
---------------------------------------------------------------------

Step 1
----------------
Download the Project from the drive link.

Step 2
----------------
Open the project in your Visual Studio.

Step 3
----------------
This Project is made using Database First Approach. 
So, create a database and add the tables in SQL from the database script that is provided 
in the drive link.

Step 4
----------------
Change the Data Source and Intial Catalog name to your Database Details in the appsettings.json
in the main project.

Step 5
----------------
run the scaffold command to generate the necessary models in the main project.

-----Scaffold-DbContext "Server=(localdb)\ProjectModels;Database=OnlineMovieBookingApplication;
		Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
-----Here the Server and Database name should match to your Sql server name and database name.

Use -force command at the end to overwrite any existing models in the main project.

Move the DbContext file in the Models folder to EntityFramework folder in the DAL services and 
replace with the existing file.

Step 6
----------------
Run the Application.
