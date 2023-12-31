﻿FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app3

EXPOSE 8081

COPY ./ ./
RUN dotnet restore

RUN dotnet publish -c Release -o outBuildFolder
 
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app4
 
COPY --from=build-env /app3/outBuildFolder/ .
ENTRYPOINT ["dotnet", "UserDataService.dll"]