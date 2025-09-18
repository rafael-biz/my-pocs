# 17.oapi-codegen

This project demonstrates the use of [oapi-codegen](https://github.com/deepmap/oapi-codegen) in a Go project.

## Structure

- `go.mod`, `go.sum`: Go module files
- `main.go`: Entry point for the application
- `api/`: Contains OpenAPI specs and generated code
- `tools/`: Tooling for code generation

## Usage

1. **Install oapi-codegen** (if not already installed):

```sh
go install github.com/deepmap/oapi-codegen/v2/cmd/oapi-codegen@latest
```

2. **Generate code**:

```sh
go run api/generate.go
```

3. **Run the application**:

```sh
go run main.go
```
