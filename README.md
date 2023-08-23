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
## Create local self-signed developer certificate
If you don't already have self signed developer certificate create one (dotnet dev-certs --check):\
dotnet dev-certs https --clean\
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\Favourites.API.pfx -p "password"\
dotnet dev-certs https --trust\
dotnet user-secrets -p .\Favourites.API\Favourites.API.csproj set "Kestrel:Certificates:Development:Password" "password"\
If you already have one, copy it and set the password in the secret storage:\
cp cert.pfx $env:USERPROFILE\.aspnet\https\Favourites.API.pfx\
dotnet user-secrets -p .\Favourites.API\Favourites.API.csproj set "Kestrel:Certificates:Development:Password" "password"
## Build image
docker build -t favouritesapi:0.1 .
## Run 
docker run --rm -it -p 5208:80 -p 7213:443 -v favourites-data:/app/data -v $env:APPDATA//microsoft//UserSecrets//:/root/.microsoft/usersecrets -v $env:USERPROFILE//.aspnet//https//:/root/.aspnet/https/ --name favourites favouritesapi:0.1
## Local address
https://localhost:7213/api/bookmarks
## Delete container
docker rm -f favourites
## Shell
docker exec -it favourites sh
## Azure Container Registry
az login\
$resourceGroup = "xxx"\
$registryName = "xxx"\
az acr login -n $registryName\
az acr create -g $resourceGroup -n $registryName --sku Basic --admin-enabled true\
$loginServer = az acr show -n $registryName --query loginServer --output tsv\
docker push $loginServer/favouritesapi:0.1
# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
## TODOs
### Frontend
* Fix tags and tag dropdown list
* ~~User should be able to delete tags while in bookmark creation modal~~
* Tags should be displayed as chips/badges with delete icon
* Add icons
* ~~Modal closes after creating a bookmark~~
* bookmark list automatically refreshes after creating or deleting
### API
## Further Reading
If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
