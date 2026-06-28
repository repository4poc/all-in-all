FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY DocumentManager.Api.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish DocumentManager.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5010

EXPOSE 5010

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DocumentManager.Api.dll"]