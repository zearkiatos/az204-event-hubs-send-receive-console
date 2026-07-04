# Send and retrieve events from Azure Event Hubs

## Step 1: Create resource group

```sh
$ az group create --name myResourceGroup --location eastus
```

## Step 2: Declare variables

```sh
resourceGroup=myResourceGroup
location=eastus
namespaceName=eventhubsns$RANDOM
```

## Step 3: Create Event Hubs namespace

```sh
$ az eventhubs namespace create --name $namespaceName --resource-group $resourceGroup -l $location
```

## Step 4: Create Event Hubs

```sh
$ az eventhubs eventhub create --name myEventHub --resource-group $resourceGroup \
  --namespace-name $namespaceName
```

## Step 5: Assigning a user

```sh
userPrincipal=$(az rest --method GET --url https://graph.microsoft.com/v1.0/me \
    --headers 'Content-Type=application/json' \
    --query userPrincipalName --output tsv)
```

## Step 6: Set resourceID

```sh
resourceID=$(az eventhubs namespace show --resource-group $resourceGroup \
    --name $namespaceName --query id --output tsv)
```

## Step 7: Create an Assign user

```sh
$ az role assignment create --assignee $userPrincipal \
    --role "Azure Event Hubs Data Owner" \
    --scope $resourceID
```