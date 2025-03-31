# ChatHub
Tech Stack: ASP.NET Core, C#, Raw Websockets, Sqlite  

# About
* Websockets real time messaging chat communication using ASP.NET Core backend written in C#
* Includes multiple user support where users can join and send/receive messages in real time
* Uses Sqlite to setup a database and store the chat history for future access, including both entire chat history and user specific history
* Designed to support as many users as required
* Uses IServerProvider for C# Server Lifecycle Scope Management

# Deployment
* Run `dotnet run` to deploy the server to a localhost
* To send/receive messages, connect to the localhost link, `http://localhost:5168` plus add /api/user to join anonymously, or /api/user/{username} to join under a certain username
* To access chat history, run a GET request to `http://localhost:5168/api/history` to retrieve from the database
