
# Web API Authentication & Authorization example

This example API shows how to implement Token authentication and authorization with ASP.NET 6, built from scratch.
## Features

- Password hashing;
- Role-based authorization;
- Read, Write, Edit, Delete access 
- Login via access token creation;


## How to test
I added [Swagger](https://swagger.io/) to the API, so we can use it to visualize and test all API routes. You can run the application and navigate to `/swagger` to see the API documentation.

You can also test the API using a tool such as [Postman](https://www.getpostman.com/). I describe how to use Postman to test the API below.

First of all, clone this repository and open it in a terminal. Then restore all the dependencies and run the project.

```sh
$ git clone https://github.com/rahulkapoor007/api-custom-auth.git
$ cd AuthorizationAndAuthentication
$ dotnet restore
$ dotnet run
```

There are already a pre-defined users configured to test the application, with super-admin permissions.

```
{
	"email": "abc@gmail.com",
	"password": "test0008"
}
```
#### Requesting access tokens

To request the access tokens, make a `POST` request to `http://localhost:5154/api/login` sending a JSON object with user credentials. The response will be a JSON object with:

 - An access token which can be used to access protected API endpoints;
 - A long value that represents the expiration date of the token.
 
 Access tokens expire after 30 minutes (you can change this in the `appsetings.json`).

#### Accessing protected data
There is an API endpoints that you can test:

 - `GET:` `http://localhost:5154/api/Dashboard/UserList`: users with access of dashboard management and having read access can access this endpoint if a valid access token is specified;
 - `DELETE:` `http://localhost:5154/api/Dashboard/User`: only users with access of dashboard management and having delete access can access this endpoint if a valid access token is specified;
 
With a valid access token in hands, make a request to any one of the endpoints mentioned above with the following header added to your request:

`Authorization: Bearer your_valid_access_token_here`

If you get a correct token and make a request to the endpoint for all users, you will get a correct response.

But if you try to pass this token to the endpoint that requires permission, you will get a `403 - forbidden` response.
## Contributing

Contributions are always welcome!

This example was created with the intent of helping people who have doubts on how to implement authentication and authorization in APIs to consume these features in different client applications. Tokens are easy to implement and secure.

If you have doubts about the implementation details or if you find a bug, please, open an issue. If you have ideas on how to improve the API or if you want to add a new functionality or fix a bug, please, send a pull request.