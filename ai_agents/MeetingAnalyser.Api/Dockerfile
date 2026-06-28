FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY MeetingAnalyser.Api.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish MeetingAnalyser.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5005

EXPOSE 5005

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MeetingAnalyser.Api.dll"]