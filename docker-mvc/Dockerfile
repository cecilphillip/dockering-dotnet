FROM microsoft/aspnetcore:1.1.2
LABEL Name=docker-mvc Version=0.0.1
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY  bin/Release/netcoreapp1.1/publish .
EXPOSE 80
ENTRYPOINT dotnet docker-mvc.dll
