# Terraform Remote State Demo

Some of the features and functionalities covered in this demo include:
 * Overview of Terraform Local and Remote State
 * Create an Azure Storage Account
 * Create an Azure Storage Container
 * Create an Azure Resource Group using Terraform
 * Configure Terraform to store the state on the created container

# Terraform State

Terraform must store state about your managed infrastructure and configuration. This state is used by Terraform to map real world resources to your configuration, keep track of metadata, and to improve performance for large infrastructures.

This state is stored by default in a local file named "terraform.tfstate", but it can also be stored remotely, which works better in a team environment.

# Create Resource Group

```
az group create -l brazilsouth -n terraform-remote-state-demo
```

# Create storage account

Replace *rbbterraformstatedemo* with a unique name across Azure.

```
az storage account create -g terraform-remote-state-demo -l brazilsouth ^
  --name rbbterraformstatedemo ^
  --sku Standard_LRS ^
  --encryption-services blob
```

# Create Storage Container

List account key and create storage container.

```
az storage account keys list --resource-group terraform-remote-state-demo --account-name rbbterraformstatedemo --query [0].value -o tsv
az storage container create --name terraform-state --account-name rbbterraformstatedemo --account-key REPLACE_WITH_ACCOUNT_KEY
```

# Edit main.tf

Edit the backend section in the main.tf accordinly to your configuration.

# Init terraform

```
terraform init
```

# Run Terraform apply

```
terraform apply
```

# Inspect Terraform state

```
terraform state list
```

# Cleanup Demo

```
terraform destroy
az group delete --name terraform-remote-state-demo
```