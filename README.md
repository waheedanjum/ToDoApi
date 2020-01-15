# ToDoApi
This first commit of swagger REST API

## Framework and Tools
This application was written using ASP.NET Core 3.0 and Visual Studio 2019
## Database
No database is required. The API uses  Microsoft.EntityFrameworkCore.InMemory.
Once the project is started, Make an [HttpPost] call to insert the database and then, [GET], [PUT] [DELETE] etc.
If the project is stopped and started again, all the Memory data will be lost. Make [HttpPost] calls again to insert the data.

## Testing
The API can be tested with Postman or directly from Swagger UI on https://localhost:5001/swagger

