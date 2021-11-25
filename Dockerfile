FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Tic-Tac-Toe.csproj", "./"]
RUN dotnet restore "Tic-Tac-Toe.csproj"
COPY . .
RUN dotnet publish "Tic-Tac-Toe.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Tic-Tac-Toe.dll"]

