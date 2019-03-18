FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["KubernetesPlayground.PersistentApp/KubernetesPlayground.PersistentApp.csproj", "KubernetesPlayground.PersistentApp/"]
RUN dotnet restore "KubernetesPlayground.PersistentApp/KubernetesPlayground.PersistentApp.csproj"
COPY . .
WORKDIR "/src/KubernetesPlayground.PersistentApp"
RUN dotnet build "KubernetesPlayground.PersistentApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KubernetesPlayground.PersistentApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KubernetesPlayground.PersistentApp.dll"]