# Nodejs Azure Storage Container

Some of the features and functionalities covered in this demo include:
 * Create an Azure Storage Container with Terraform
 * Create an Azure App Service with Terraform
 * Set a connection string for the App Service with Terraform
 * Deploy a nodejs app that creates a blob on storage container

# Install dependencies

```
npm install
```

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

Deploy from a zip file:

```
powershell Compress-Archive * my-app.zip
az webapp deployment source config-zip --name rbbnodejsblobdemo --resource-group rbb-nodejs-blob-demo --src my-app.zip
```

# Cleanup Demo

```
terraform destroy
```