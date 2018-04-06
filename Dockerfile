#escape=`

FROM microsoft/windowsservercore

# SQL Server 2012 LocalDB
COPY SqlLocalDB.msi .
RUN powershell -Command Start-Process -FilePath msiexec -ArgumentList /q, /i, SqlLocalDB.msi, IACCEPTSQLLOCALDBLICENSETERMS=YES -Wait;
RUN powershell -Command "& 'C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SqlLocalDB.exe' create azure -s"

# Azure Storage Emulator
COPY MicrosoftAzureStorageEmulator.msi .
RUN powershell -Command Start-PRocess -FilePath msiexec -ArgumentList /q, /i, MicrosoftAzureStorageEmulator.msi -Wait
RUN powershell -Command "& 'C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\AzureStorageEmulator.exe' init /server '(localdb)\azure'"
EXPOSE 1433 10000 10001 10002

# Configure and launch
COPY start.ps1 .
CMD powershell .\start.ps1
