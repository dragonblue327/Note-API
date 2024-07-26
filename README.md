# Note-API Application

This API app is for creating notes and reminders with tags. Built with .NET 7.

## Note: This project is incomplete 
	A friend once said, ‘You can write a calculator in a day, but perfecting it can take a lifetime.’

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
	All create methods can create new objects and create relationship between objects.
	Все методы создания могут создавать новые объекты и устанавливать связи между объектами.
## Delete 
	All delete methods can delete object but will not delete related objects.
	Все методы удаления могут удалять объекты, но не будут удалять связанные объекты.
## GetAll
	All GetAll methods can return objects and related objects.
	Все методы GetAll могут возвращать объекты и связанные объекты
## GetById
	All GetById methods can return object and related objects without tracking.
	Все методы GetById могут возвращать объект и связанные объекты без отслеживания.
## Update
	All update methods can update objects and create relationship between objects 
	if you will try to create relation to object not found it will create the object 
	and can update related objects.
	Все методы обновления могут обновлять объекты и устанавливать связи между объектами.
	Если вы попытаетесь создать связь с объектом, который не найден, он создаст этот объект
	и так же может обновить связанные объекты.