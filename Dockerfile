FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PaycheckChallenge.Api/PaycheckChallenge.Api.csproj", "PaycheckChallenge.Api/"]
COPY ["src/PaycheckChallenge.Application/PaycheckChallenge.Application.csproj", "PaycheckChallenge.Application/"]
COPY ["src/PaycheckChallenge.CrossCutting/PaycheckChallenge.CrossCutting.csproj", "PaycheckChallenge.CrossCutting/"]
COPY ["src/PaycheckChallenge.Domain/PaycheckChallenge.Domain.csproj", "PaycheckChallenge.Domain/"]
COPY ["src/PaycheckChallenge.Infra/PaycheckChallenge.Infra.csproj", "PaycheckChallenge.Infra/"]
RUN dotnet restore "PaycheckChallenge.Api/PaycheckChallenge.Api.csproj"
COPY ./src/ /src
WORKDIR "/src/PaycheckChallenge.Api"
RUN dotnet build "PaycheckChallenge.Api.csproj" -c Release -o /app/build/

FROM build AS publish
WORKDIR "/src/PaycheckChallenge.Api"
RUN dotnet publish "PaycheckChallenge.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaycheckChallenge.Api.dll"]