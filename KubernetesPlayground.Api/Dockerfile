FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["KubernetesPlayground.Api/KubernetesPlayground.Api.csproj", "KubernetesPlayground.Api/"]
RUN dotnet restore "KubernetesPlayground.Api/KubernetesPlayground.Api.csproj"
COPY . .
WORKDIR "/src/KubernetesPlayground.Api"
RUN dotnet build "KubernetesPlayground.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KubernetesPlayground.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KubernetesPlayground.Api.dll"]