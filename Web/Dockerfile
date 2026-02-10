FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Web.sln ./
COPY Web.Api/Web.Api.csproj Web.Api/
COPY Web.DataAccess/Web.DataAccess.csproj Web.DataAccess/
COPY Web.Domain/Web.Domain.csproj Web.Domain/
COPY Web.IoC/Web.IoC.csproj Web.IoC/
COPY Web.Services/Web.Services.csproj Web.Services/

RUN dotnet restore Web.Api/Web.Api.csproj

COPY . .
RUN dotnet publish Web.Api/Web.Api.csproj -c Release -o /app/publish /p:UseAppHost=false
RUN mkdir -p /app/publish/Assets && cp -r /src/Web.Api/Assets/* /app/publish/Assets/

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Web.Api.dll"]
