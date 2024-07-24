# Note-API Application

This API app is for creating notes and reminders with flags. Built with .NET 7.

## Note: This project is incomplete

## Project Overview
	Built with Clean Architecture, ASP.NET Core 7 Web API, and CQRS.

# Usage
 I used the (.) in the connection string. If you have a DB server locally or on a WAN, you need to edit the connection string.
This app is built to use with SQL Server. If you have another DB, you need to change some configurations in the ConfigureServices class in the Infrastructure project.
## To run this app, you should migrate first, then update the DB:
    add-migration "example"
    update-database
 ![Alt text](images/example.png)   

Don’t forget to set the Note.Api as the startup project!!!.