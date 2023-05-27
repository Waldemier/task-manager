FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskManager.Presentation/TaskManager.Presentation.csproj", "TaskManager.Presentation/"]
RUN dotnet restore "TaskManager.Presentation/TaskManager.Presentation.csproj"
COPY . .
WORKDIR "/src/TaskManager.Presentation"
RUN dotnet build "TaskManager.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManager.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.Presentation.dll"]
