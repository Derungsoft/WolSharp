@echo off
set /P username=Enter username:
set /P password=Enter password:

docker login -u %username% -p %password%
docker push derungsoft/wolsharp:debian-arm32