FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["KubernetesPlayground/KubernetesPlayground.csproj", "KubernetesPlayground/"]
RUN dotnet restore "KubernetesPlayground/KubernetesPlayground.csproj"
COPY . .
WORKDIR "/src/KubernetesPlayground"
RUN dotnet build "KubernetesPlayground.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KubernetesPlayground.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KubernetesPlayground.dll"]