FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY CostManagement.McpServer.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish CostManagement.McpServer.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5080

EXPOSE 5080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CostManagement.McpServer.dll"]