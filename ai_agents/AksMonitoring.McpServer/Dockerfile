FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY AksMonitoring.McpServer.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish AksMonitoring.McpServer.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5070

EXPOSE 5070

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AksMonitoring.McpServer.dll"]