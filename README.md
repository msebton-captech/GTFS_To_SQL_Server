
Project Name: GTFS Data Uploader

Description:
This project is a C# application designed to retrieve General Transit Feed Specification (GTFS) data from the Israeli government's official site and upload it to a specific SQL Server database. GTFS data is commonly used by transportation agencies to provide scheduling, geographic, and service information about public transportation systems.

Installation:

Clone or download the repository from GitHub: Link to Repository
Open the solution file (GTFSDataUploader.sln) in Visual Studio.
Build the solution to ensure all dependencies are resolved.

Configuration:

Open the app.config file in the project.
Update the following configuration settings as per your requirements:
GTFSSourceURL: The URL of the GTFS data provided by the Israeli government.
SQLServerConnectionString: Connection string for your SQL Server database.
Any additional settings related to authentication or specific data requirements.

Usage:

Run the application (GTFSDataUploader.exe) either through Visual Studio or by executing the built binary.
The application will retrieve the GTFS data from the specified source URL.
The data will be parsed and uploaded to the specified SQL Server database according to the schema defined.

Contributing:

Contributions are welcome! If you'd like to contribute to this project, feel free to fork the repository, make your changes, and submit a pull request.
License:
This project is licensed under the MIT License.

Credits:

This project is maintained by Malachi Weiss.
Contact:
For any inquiries or support regarding this project, please contact malachiweiss1@gmail.com.
