# ChatHub
Tech Stack: ASP.NET Core, C#, Raw Websockets, SQLite, Postman, REST APIs, GraphQL Subscriptions

# About
* Implemented real-time messaging and chat communication using WebSockets with an ASP.NET Core backend written in C#
* Supports multiple users, allowing them to join the chat and send/receive messages in real time
* Created endpoints for both GraphQL Subscription Websockets and REST API Websockets, more below
* Stores chat history in a SQLite database, ensuring both global and user-specific message histories are available for future access
* Designed to scale, supporting as many users as required for seamless communication
* Utilized `IServerProvider` and ASP.NET best practices for effective C# server lifecycle scope management
* Written using both Controllers and Minimal API endpoints for clean and modularized files
* File directory sorted according to best practices - Controllers, Data, Model, Services files with according classes

# API Endpoints
* Since both REST API and GraphQL are implemented, more details are listed below
* REST API follows the standard C# and ASP.NET API routing with Controllers and input/output sockets
* Uses a websocket Postman Request to get and receive messages, with the query and received both being just plaintext
* GraphQL follows the GraphQL standard, utilizing HotChocolate and GraphQL Queries, Mutations, and Subscriptions 
* Uses HotChocolate and GraphQL endpoint to send and receive GraphQL JSON objects, typically formatted specially before sending
* Much more structured, as GraphQL is, but allows for implmented missed messages caching using Enumerators and yield returns

# Deployment
* Run `dotnet run` to deploy the server to a localhost
* To send/receive messages, connect to the localhost link, `http://localhost:5168` plus add /api/user to join anonymously, or /api/user/{username} to join under a certain username
* To access chat history, run a GET request to `http://localhost:5168/api/history` to retrieve from the database
