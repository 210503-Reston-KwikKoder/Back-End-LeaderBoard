FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app 

COPY *.sln .

COPY BELBRest/*.csproj ./BELBRest/
COPY BELBBL/*.csproj ./BELBBL/
COPY BELBDL/*.csproj ./BELBDL/
COPY BELBModels/*.csproj ./BELBModels/
COPY BELBTests/*.csproj ./BELBTests/

RUN cd BELBRest && dotnet restore

COPY . ./


RUN dotnet publish BELBRest -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app 

COPY --from=build /app/publish ./
CMD ["dotnet", "LeaderboardRest.dll"]