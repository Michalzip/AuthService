#Base image for building the project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

#Set the working directory inside the container
WORKDIR /app

#Copy the project files to the container
COPY . ./

##]Restore NuGet packages
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out


#Runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

#Set the working directory inside the container
WORKDIR /app

#Copy the published output from the build stage
COPY --from=build /app/out ./

#Set the entry point for the container
ENTRYPOINT ["dotnet", "AuthService.Bootstrapper.dll"]