# architecture_project
Project of course architecture in BGU university

This project was written in Visual Studio 2013 and was tested on Visual Studio 2012 (Lab version).

We use local database (.mdf). In order to use it, you might need to update the connection string to match your computer.

Option 1:
- Open server explorer
- Right click on couponsDB
- Properties
- Copy the connectionString
- Open the Properties file under Coupons
- Open Settings file
- Change the connectionString to the one you have copied


Option 2:
Your project created auto App.config with no connection string attached.
Add connectionString:
Name: CouponsDBConnectionString
Type: ConnectionString
Scope: Application
Value: Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\CouponsDB.mdf;Integrated Security=True

Or just open App.config file from Coupons > App.config and copy the file to your project.
