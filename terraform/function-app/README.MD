# Terraform Azure Functions

Some of the features and functionalities covered in this demo include:
 * Create an Azure Function with Terraform
 * Deploy a JavaScript function with Azure Cli

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

```
cd src
powershell Compress-Archive * src.zip
az functionapp deployment source config-zip --name rbbfunctionappdemo --resource-group rbbfunctionappdemo --src src.zip
```

# Cleanup Demo

```
terraform destroy
```