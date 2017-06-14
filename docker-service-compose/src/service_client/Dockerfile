FROM microsoft/dotnet:1.1.2-runtime
WORKDIR /app
COPY  bin/Release/netcoreapp1.1/publish .
ENTRYPOINT ["dotnet", "service_client.dll"]