# Azure Storage Emulator In Docker Test Project

A small project to demonstrate running a console app in a Docker container which inserts a message into a queue in the Azure Storage Emulator running in yet another Docker container.

## Known Issues

- You will need to install .Net Core 2.1 Preview1 Final to run the console application
- You must be running Windows 10 Fall Creators Update to run the Windows Server container images used as the base images in this project.
- I am able to put a message into the queue and subsequently "peek" it, but am unable to actually dequeue it.
    - The console application is more or less based off of the code shown at Microsoft's MSDN page demonstrating using Azure Storage Queues.

- I was unable to use a "friendly name" for the Azure Storage Emulator container (e.g. `storage`) and use said name from the console application container to communicate with ASE.
    - This may be something environmental with my current setup, as I was attempting to checkout minikube and Kubernetes on Windows, so I may need to reset my Docker engine.
    - Or, this is something with the Windows NAT driver network?
    - **Don't forget to change the IP address in the connection string in `appsettings.json` to match your ASE container's IP address.**

- I am not using the Microsoft Azure Storage Emulator image available from the Docker Hub/Store. It's a custom image based off of [this writeup](http://agilesnowball.com/azure/docker/2017/05/15/azure-storage-emulator-in-docker.html) with a few modifications to actually make it work correctly.
- You will need to download the latest `MicrosoftAzureStorageEmulator.msi` installer and the SqlLocalDB 2012 `SqlLocalDB.msi` in order to build the containers.
- There's no Docker compose file--so you'll need to manually spin up the containers. This was just a quick 'n dirty demonstration project.
