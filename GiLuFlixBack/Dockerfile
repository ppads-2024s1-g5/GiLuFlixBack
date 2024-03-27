FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ENV IP 192.168.1.100

WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .


WORKDIR /App
ENTRYPOINT [dotnet","GiLuFlixBack.dll"]
EXPOSE 8080
