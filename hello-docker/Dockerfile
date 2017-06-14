FROM microsoft/dotnet:1.1.2-runtime
LABEL Name=hello-docker Version=0.0.1
WORKDIR /app
COPY  bin/Release/netcoreapp1.1/publish .
ENTRYPOINT ["dotnet", "hello-docker.dll"]
