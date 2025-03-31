# ChatHub
Tech Stack: ASP.NET Core, C#, Raw Websockets, SQLite  

# About
* Implemented real-time messaging and chat communication using WebSockets with an ASP.NET Core backend written in C#
* Supports multiple users, allowing them to join the chat and send/receive messages in real time
* Stores chat history in a SQLite database, ensuring both global and user-specific message histories are available for future access
* Designed to scale, supporting as many users as required for seamless communication
* Utilized `IServerProvider` and ASP.NET best practices for effective C# server lifecycle scope management
* Written using both Controllers and Minimal API endpoints for clean and modularized files
* File directory sorted according to best practices - Controllers, Data, Model, Services files with according classes

# Deployment
* Run `dotnet run` to deploy the server to a localhost
* To send/receive messages, connect to the localhost link, `http://localhost:5168` plus add /api/user to join anonymously, or /api/user/{username} to join under a certain username
* To access chat history, run a GET request to `http://localhost:5168/api/history` to retrieve from the database
