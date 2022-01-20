# Terraform

In these folders, you will find demos for building, changing, and destroying Azure infrastructure using Terraform.

# Prerequisites

 * Terraform version 1.1.3 or higher.

 * Azure Cli Tool version 2.32.0 or higher.

 * An Azure subscription.

# Sign in

In your terminal, use the Azure CLI tool to setup your account permissions locally.

```
az login
```

# Terraform Init

Run the `terraform init` to initialize the Terraform configuration of the demo.

```
terraform init
```

# Terraform Apply

Run the `terraform apply` command to apply the configuration of the demo.

```
terraform apply
```

# Deploy

Check if the demo has additional deployment steps, if any.

# Inspect

Run the `terraform show` command to inspect the state of the resources.

```
terraform show
```

# Cleanup

Run the `terraform destroy` command to apply the configuration of the demo.

```
terraform destroy
```