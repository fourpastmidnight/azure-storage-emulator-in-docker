# Find the docker container's external IP address
$ip = (get-netipaddress | ? { $_.AddressFamily -eq "IPv4" -and $_.AddressState -eq "Preferred" -and $_.PrefixOrigin -ne "WellKnown" }[0]).IPAddress

# Rewrite AzureStorageEmulator.exe.config to use it
$config = "C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\AzureStorageEmulator.exe.config"
(get-content $config) -replace "127.0.0.1",$ip | out-file $config

# Start the 'azure' LocalDb instance
& 'C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SqlLocalDB.exe' start azure

# Launch the emulator
& "C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\AzureStorageEmulator.exe" start -inprocess
