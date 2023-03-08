FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

WORKDIR /build
ADD Store ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime

WORKDIR /StoreProject
RUN mkdir -p /StoreProject/Images

WORKDIR /StoreProject/Images
ADD Images ./

WORKDIR /StoreProject/Store

COPY --from=build-env /build/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Store.dll", "--urls=http://0.0.0.0:5000"]
ENV DOTNET_EnableDiagnostics=0