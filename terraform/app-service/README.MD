# Azure App Service Demo

Some of the features and functionalities covered in this demo include:
 * Create a Azure App Service with Terraform
 * Deploy a nodejs app with Azure Cli

# Sign in

```
az login
```

# Init terraform

```
terraform init
```

# Run Terraform apply

```
terraform apply
```

# Deploy

Deploy from a zip file or directly from a git repository.

## Zip File

Deploy from a zip file:

```
git clone https://github.com/Azure-Samples/nodejs-docs-hello-world.git
cd cd .\nodejs-docs-hello-world\
powershell Compress-Archive * hello-world.zip
az webapp deployment source config-zip --name rbbnodejshelloworld --resource-group terraform-webapp-demo --src hello-world.zip
```

## Git Repository

Deploy from a git repository:

```
az webapp deployment source config --name rbbnodejshelloworld --resource-group terraform-webapp-demo --branch master --manual-integration --repo-url https://github.com/Azure-Samples/nodejs-docs-hello-world.git
```

# Inspect Web App

```
explorer https://rbbnodejshelloworld.azurewebsites.net
```

# Cleanup Demo

```
terraform destroy
```