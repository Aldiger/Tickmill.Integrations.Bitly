#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd ../src/Tickmill.Integrations.Bitly.API
dotnet run --no-restore