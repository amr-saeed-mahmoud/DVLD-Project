# Driving License Management

## Overview  
Driving License Management is a desktop application designed to streamline the entire driving license process. It supports managing administrators and users, processing license applications, scheduling driving tests automatically, issuing and detaining licenses, and tracking detailed records.

## Key Features  
- Secure admin and user management  
- Efficient application handling and license issuance  
- Automated scheduling for driving tests  
- License status monitoring and record keeping  
- Intuitive Windows Forms interface with responsive data operations  

## Technologies  
- C# with .NET Framework (Windows Forms)  
- SQL Server database  
- ADO.NET with Async/Await for data access  
- Three-tier architecture (Presentation, Business Logic, Data Access)

## Run Requirements  
1. Clone the repository
2. Ensure SQL Server is installed and running.  
3. Restore the database from the backup.  
4. Update the connection string in `App.config`:  
   ```xml
   <connectionStrings>
       <add name="DrivingLicenseDb" connectionString="Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;" providerName="System.Data.SqlClient" />
   </connectionStrings>

## Contact  
Amr Saeed  
Email: amrpr6@gmail.com  
Phone: +20 1024378380
WhatsApp: +20 1024378380
