FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src

COPY ./src ./
WORKDIR /src/BestPost.WebApi

RUN dotnet restore
RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS serve
WORKDIR /app
COPY --from=build /src/BestPost.WebApi/output .

EXPOSE 8080
EXPOSE 443

ENTRYPOINT [ "dotnet", "BestPost.WebApi.dll" ]