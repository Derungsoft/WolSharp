FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
WORKDIR /app/
RUN dotnet publish -o out /p:PublishWithAspNetCoreTargetManifest="false"

FROM microsoft/dotnet:2.0-runtime AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Derungsoft.WolSharp.Samples.Server.dll"]
