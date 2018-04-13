FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /app

COPY *.sln .
COPY Derungsoft.WolSharp.WebApi/*.csproj ./Derungsoft.WolSharp.WebApi/
RUN dotnet restore

COPY . .
WORKDIR /app/Derungsoft.WolSharp.WebApi
RUN dotnet publish -o out /p:PublishWithAspNetCoreTargetManifest="false"

FROM microsoft/dotnet:2.0-runtime-stretch-arm32v7 AS runtime
ENV ASPNETCORE_URLS http://+:80
WORKDIR /app
COPY --from=build /app/Derungsoft.WolSharp.WebApi/out ./
ENTRYPOINT ["dotnet", "Derungsoft.WolSharp.WebApi.dll"]
