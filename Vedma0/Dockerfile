FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Vedma0/Vedma0.csproj", "Vedma0/"]
RUN dotnet restore "Vedma0/Vedma0.csproj"
COPY . .
WORKDIR "/src/Vedma0"
RUN dotnet build "Vedma0.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Vedma0.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Vedma0.dll"]