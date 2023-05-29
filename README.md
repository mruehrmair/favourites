# Introduction 
Favourites provides tools to handle web bookmarks. It consists of a web api that stores the bookmarks in a database and clients interact with the api and provide a UI and options to process the data. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Database
## Provider
Microsoft.EntityFrameworkCore.Sqlite
## Creating migrations from CLI
* dotnet ef migrations add BookmarkDbInitial -s ..\Favourites.API\Favourites.API.csproj
* dotnet ef database update -s ..\Favourites.API\Favourites.API.csproj
# Docker
## Build image
docker build -t favouritesapi:0.1 .
## Run 
docker run -d -p 7213:80 --name favourites favouritesapi:0.1
## Delete container
docker rm -f favourites
# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
