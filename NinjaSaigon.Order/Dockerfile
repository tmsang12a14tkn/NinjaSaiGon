FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 50228
EXPOSE 44386

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY NinjaSaigon.Order/NinjaSaigon.Order.csproj NinjaSaigon.Order/
COPY NinjaSaiGon.Data/NinjaSaiGon.Data.csproj NinjaSaiGon.Data/
RUN dotnet restore NinjaSaigon.Order/NinjaSaigon.Order.csproj
COPY . .
WORKDIR /src/NinjaSaigon.Order
RUN dotnet build NinjaSaigon.Order.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NinjaSaigon.Order.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NinjaSaiGon.Order.dll"]
