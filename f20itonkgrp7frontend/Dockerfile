#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
copy dist /app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=base /app/out .
ENTRYPOINT ["dotnet", "f20itonkgrp7frontend.dll"]