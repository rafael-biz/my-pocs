# Terraform Azure App Service Plan

Some of the features and functionalities covered in this demo include:
 * Azure App Service Plan overview
 * Create App Service Plans with Terraform

# App Service

In App Service (Web Apps, API Apps, or Mobile Apps), an app always runs in an App Service plan.
In addition, Azure Functions also has the option of running in an App Service plan. An App
Service plan defines a set of compute resources for a web app to run. These compute resources
are analogous to the server farm in conventional web hosting. One or more apps can be configured
to run on the same computing resources (or in the same App Service plan).

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

# Inspect Plans

```
terraform state list
```

# Cleanup Demo

```
terraform destroy
```