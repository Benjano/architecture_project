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


<u> OPTION 2: </u>
Change App.config to 
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <configSections>
    </configSections>
    <connectionStrings>
        <add name="Coupons.Properties.Settings.CouponsDBConnectionString"
            connectionString="Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\CouponsDB.mdf;Integrated Security=True"
            providerName="System.Data.SqlClient" />
    </connectionStrings>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
</configuration>

