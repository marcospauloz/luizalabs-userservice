# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy csproj and restore dependencies.
COPY ["luizalabs.UserService.API/luizalabs.UserService.API.csproj", "luizalabs.UserService.API/"]
COPY ["luizalabs.UserService.Application/luizalabs.UserService.Application.csproj", "luizalabs.UserService.Application/"]
COPY ["luizalabs.UserService.Domain/luizalabs.UserService.Domain.csproj", "luizalabs.UserService.Domain/"]
COPY ["luizalabs.UserService.Infrastructure/luizalabs.UserService.Infrastructure.csproj", "luizalabs.UserService.Infrastructure/"]

RUN dotnet restore "luizalabs.UserService.API/luizalabs.UserService.API.csproj"

# Copy the rest of the working directory contents into the container at /app.
COPY . ./

# Build the app.
RUN dotnet publish -c Release -o out

# Build runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 8080 for the application.
EXPOSE 8080

# Define the command to run the app.
ENTRYPOINT ["dotnet", "luizalabs.UserService.API.dll"]