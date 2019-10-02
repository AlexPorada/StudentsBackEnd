FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ClassStudent.API/ClassStudent.API.csproj", "ClassStudent.API/"]
COPY ["ClassStudent.DAL/ClassStudent.DAL.csproj", "ClassStudent.DAL/"]
COPY ["ClassStudent.DTO/ClassStudent.DTO.csproj", "ClassStudent.DTO/"]
RUN dotnet restore "ClassStudent.API/ClassStudent.API.csproj"
COPY . .
WORKDIR "/src/ClassStudent.API"
RUN dotnet build "ClassStudent.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ClassStudent.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ClassStudent.API.dll"]