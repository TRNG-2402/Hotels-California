# Stage 1
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY HotelsCalifornia.API.csproj ./
RUN dotnet restore HotelsCalifornia.API.csproj

COPY . .
RUN dotnet publish HotelsCalifornia.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# Stage 2
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT [ "dotnet", "HotelsCalifornia.API.dll" ]