# Azure Active Directory B2C

Some of the features and functionalities covered in this demo include:
 * Create an Active Directory B2C Tenant with Terraform

Many features of AD B2C are not yet supported by Terraform.

# Active Directory B2C

Azure Active Directory B2C provides business-to-customer identity as a service. Your customers use their preferred social, enterprise, or local account identities to get single sign-on access to your applications and APIs.

Azure Active Directory B2C (Azure AD B2C) is a separate service from Azure Active Directory (Azure AD). It is built on the same technology as Azure AD but for a different purpose. It allows businesses to build customer facing applications, and then allow anyone to sign up into those applications with no restrictions on user account.

Azure AD B2C is a white-label authentication solution. You can customize the entire user experience with your brand so that it blends seamlessly with your web and mobile applications.

# Troubleshooting

**I can't create Azure AD Tenant. What do I do?**

```
The subscription is not registered to use namespace 'Microsoft.DataFactory'.
```

Run:

```
az provider register --namespace Microsoft.DataFactory
```

```
The subscription is not registered to use namespace 'Microsoft.AzureActiveDirectory'.
```

Run:

```
az provider register --namespace Microsoft.AzureActiveDirectory
```

# References

 * [What is Azure Active Directory?](https://docs.microsoft.com/en-us/azure/active-directory/fundamentals/active-directory-whatis)
 * [What is Azure Active Directory B2C?](https://docs.microsoft.com/en-us/azure/active-directory-b2c/overview)
 * [Tutorial: Create an Azure Active Directory B2C tenant](https://docs.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-tenant)