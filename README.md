# Note-API Application

This API app is for creating notes and reminders with flags. Built with .NET 7.

## Note: This project is incomplete

## Project Overview
	Built with Clean Architecture, ASP.NET Core 7 Web API, and CQRS.

# Usage
 I used the (.) in the connection string. If you have a DB server locally or on a WAN, you need to edit the connection string.
This app is built to use with SQL Server. If you have another DB, you need to change some configurations in ConfigureServices class in the Infrastructure project.
## To run this app, you should migrate first, then update the DB:
    add-migration "example"
    update-database
 ![add-migration "example"](https://github.com/dragonblue327/Note-API/blob/master/contant/migration-example.jpg?raw=true)   

Don’t forget to set the Note.Api as the startup project!!!.


## Create 
	all create methods can create new objects and create relationship between objects. 
## Delete 
	all delete methods can delete object but will not delete related objects.
## GetAll
	all GetAll methods can return objects and related objects.
## GetById
	all GetById methods can return object and related objects without tracking.
## Update
	all update methods can update objects and create relationship between objects 
	if you will try to create relation to object not found it will create the object 
	and can update related objects.

