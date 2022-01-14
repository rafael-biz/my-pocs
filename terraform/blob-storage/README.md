# Azure Blob Storage Demo

Some of the features and functionalities covered in this demo include:
 * Create a Azure Storage Account
 * Create a Azure Storage Container
 * Create a Shared Access Signature
 * Upload file using Terraform
 * Download the file

# Sign in

```
az login
```

# Run Terraform init

```
terraform init
```

# Run Terraform apply

```
terraform apply
```

# Get token

Get the Shared Access Signature token from the output and download the file.

```
curl "https://rbbblobdemo.blob.core.windows.net/rbbblobdemo/helloworld.txt<SAS_TOKEN>"
```

# Cleanup Demo

```
terraform destroy
```
