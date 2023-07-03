FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY ./src/API/bin/docker .
ENV DEBIAN_FRONTEND=noninteractive
RUN  apt-get update && apt-get -y upgrade && rm -r /var/lib/apt/lists/*
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5000
ENTRYPOINT dotnet Tickmill.Integrations.Bitly.API.dll
