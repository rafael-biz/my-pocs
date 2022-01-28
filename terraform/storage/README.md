# Azure Blob Storage Demo

Some of the features and functionalities covered in this demo include:
 * Create an Azure Storage Account
 * Create an Azure Storage Queue
 * Create an Azure Storage Table
 * Create an Azure Storage Container
 * Create a Shared Access Signature
 * Upload file using Terraform
 * Download the file

# Inspect

Get the Shared Access Signature token from the output and download the file.

```
curl "https://rbbblobdemo.blob.core.windows.net/rbbblobdemo/helloworld.txt<SAS_TOKEN>"
```