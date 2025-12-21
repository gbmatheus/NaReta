FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY ./src .

WORKDIR /app/NaReta.Api

RUN dotnet restore

RUN dotnet publish -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build-env /app/out .

ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 8080

CMD [ "dotnet", "NaReta.Api.dll" ]
