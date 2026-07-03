FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY Dev-ilTeam.Hera.Platform/*.csproj Dev-ilTeam.Hera.Platform/
RUN dotnet restore ./Dev-ilTeam.Hera.Platform
COPY . .
RUN dotnet publish ./Dev-ilTeam.Hera.Platform -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Dev-ilTeam.Hera.Platform.dll"]
