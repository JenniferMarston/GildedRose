# GildedRose

Gilded Rose  API

The Gilded Rose solution is made up of 3 projects built on ASP.Net Core 2.2:
 GildedRoseAPI  
GildedRoseData 
GildedRoseApi.Tests


Gilded Rose API
The Api uses Controllers, Managers, and DTOs to accomplish work.  While Managers and DTO’s are not always necessary, I included them as an example of a best practice, that accommodates complex business logic, and the masking of input and output data.
Identity Framework is used to access and create UserData. 
Jwt Bearer tokens were used for the API Authentication.  
I chose these tools because I am familiar with them and because bearer tokens are commonly used in API authentication. 

GildedRoseData 
The Data project uses Repositories, Entities and Context items to accomplish its work.  

I used EntityFramework Core with an in-memory-database to store data.  The data is seeded in the startup file with 2 users and 2 items.  
 
Entity Framework has been around for a while but is now built into the Core framework.  While I am familiar with other ORMs,  I felt this was the natural tool to use in the Core framework.   The in-memory-database was used because it allows for the easy creation/destruction of the store and does not require additional setup.

Note:  The Item Class has been modified to include an Id and a Quantity.  I did this to facilitate retrieval by ID and keeping track of the quantity available.  
Note:  Normally the Create Purchase would be done via a Post and produce a Purchase Entity/ Record.  However, I have used Put on the Item Entity for this project to keep the models simple.  The Item Quantity is simply debited when a purchase is made.  If there are no Items left in stock, success = false and message = “Insufficient Inventory”  is returned in the response object.


GildedRoseApi.Tests
I used ASPCore MVC Testing and Xunit for the test harness and tests.  I chose this because it allows for the easy injection of fixtures execution of the startup and program files.  I’ve included basic functional tests and unit tests against one of the repositories.




Postman Collection
This is a convenience collection of API calls against the project.  

Note: the API  requires authentication with bearer tokens, so you must first run the Login API call to obtain a bearer token.  Please place that token in the authentication header of subsequent calls.

Swagger
When run, the Api opens to its Swagger page, and this can be used as a model for postman requests.

Example Request/Response:

Log in the Admin User: 

https://localhost:5001/api/Authentication/logins
Body:
 {"username":"admin",
 "password":"Admin123!"}

Response:
{
    "token": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbmlzdHJhdG9yIiwiZXhwIjoxNTU3MTQxMDg0LCJpc3MiOiJodHRwOi8vZ2lsZGVkcm9zZS5jb20iLCJhdWQiOiJodHRwOi8vZ2lsZGVkcm9zZS5jb20ifQ.2bVcbwE4VFi0YdIUptNsmf_QgPTiF7gkqU9jb4FnsNw",
    "expiration": "2019-05-06T11:11:24Z"
}
