#escape=`
FROM microsoft/dotnet:2.1.300-preview1-sdk-nanoserver-1709 AS build-env

WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1.0-preview1-runtime-nanoserver-1709
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT [ "dotnet", "ase-test.dll" ]
