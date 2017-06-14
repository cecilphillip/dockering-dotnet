FROM microsoft/aspnetcore:1.1.2
LABEL Name=docker-mvc Version=0.0.1
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
VOLUME /data/sqlite
COPY  bin/Release/netcoreapp1.1/publish .
ENTRYPOINT dotnet docker-volume.dll
