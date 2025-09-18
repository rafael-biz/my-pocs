# SQLite Example in Go

This is a simple Go console application demonstrating how to use SQLite with the `modernc.org/sqlite` driver.

## Features

- Creates a SQLite database file (`books.db`).
- Creates a `books` table if it does not exist.
- Inserts three sample books into the table (no user input required).
- Lists all books found in the database and prints them to the console.

## How it works

1. The application opens (or creates) a SQLite database file named `books.db`.
2. It creates a table called `books` with columns for `id`, `title`, `author`, and `year`.
3. Three books are inserted into the table using hardcoded values.
4. All books in the table are queried and printed to the console.

## Requirements

- Go 1.21 or newer
- The `modernc.org/sqlite` driver (installed via `go mod tidy`)

## Running

```sh
go run main.go
```

You should see output indicating the table was created, books were inserted, and a list of all books in the database.
