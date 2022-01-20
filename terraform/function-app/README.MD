# Azure Functions Demo

Some of the features and functionalities covered in this demo include:
 * Create an Azure Function with Terraform
 * Deploy a JavaScript function with Azure Cli

# Deploy

Run these commands to deploy the Azure Function.

```
cd src
powershell Compress-Archive * src.zip
az functionapp deployment source config-zip --name rbbfunctionappdemo --resource-group rbbfunctionappdemo --src src.zip
```