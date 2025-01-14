<!-- omit in toc -->
# AzFunctionReferenceSolution

- [Prerequisites](#prerequisites)
- [Running the Emulators](#running-the-emulators)
  - [Visual Studio: All In Docker](#visual-studio-all-in-docker)
  - [Visual Studio Code: Emulators in Docker, Azure Function Locally](#visual-studio-code-emulators-in-docker-azure-function-locally)
  - [Testing](#testing)
- [References](#references)

## Prerequisites

- Azure CLI
- Azure Functions Core Tools
- Docker

## Running the Emulators

### Visual Studio: All In Docker

Add a `local.settings.json` file to the `AzFunctionReferenceSolution` directory. Visual Studio will inject these automatically when you debug.

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "primaryEventHub": "APP_TARGET_EVENT_HUB",
    "APP_FIRST_QUEUE": "queue.1",
    "APP_TARGET_EVENT_HUB": "Endpoint=sb://ehemulator;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;",
    "APP_TARGET_SERVICE_BUS_QUEUE": "Endpoint=sb://sbemulator;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;"
  }
}
```

When you start debugging in Visual Studio, it will automatically run `docker compose` based on your `launch.json` settings.

### Visual Studio Code: Emulators in Docker, Azure Function Locally

Update your `local.settings.json` file in the `AzFunctionReferenceSolution` directory.

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "primaryEventHub": "APP_TARGET_EVENT_HUB",
        "APP_FIRST_QUEUE": "queue.1",
        "APP_TARGET_EVENT_HUB": "Endpoint=sb://localhost:5673;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;",
        "APP_TARGET_SERVICE_BUS_QUEUE": "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;"
    }
}
```

Navigate to the `docker` directory and run `docker compose up -d`.  After all the containers are up, you can stop the `azfunctionreferencesolution` container.

Navigate to the `AzFunctionReferenceSolution` and start the Azure functions with `func start`.

### Testing

You can simulate the EventGrid triggered function by using the following endpoint:

```http
POST http://localhost:7071/runtime/webhooks/EventGrid?functionName=DefenderResultHandler
Content-Type: application/json
aeg-event-type: Notification

{
    "topic": "Test",
    "subject": "Local/RestClient",
    "id": "00000000-0000-0000-0000-000000000001",
    "eventType": "Test",
    "eventTime": "2024-01-01T00:00:00.000Z",
    "data":{
        "message": "Hello Test!"
    },
    "dataVersion": "",
    "metadataVersion": "1"
}
```

## References

Check [https://github.com/Azure/azure-functions-extension-bundles/releases](https://github.com/Azure/azure-functions-extension-bundles/releases)

- [https://github.com/Azure/azure-event-hubs-emulator-installer](https://github.com/Azure/azure-event-hubs-emulator-installer)
- [https://learn.microsoft.com/en-us/azure/service-bus-messaging/overview-emulator](https://learn.microsoft.com/en-us/azure/service-bus-messaging/overview-emulator)
- [https://github.com/Azure/azure-service-bus-emulator-installer](https://github.com/Azure/azure-service-bus-emulator-installer)
