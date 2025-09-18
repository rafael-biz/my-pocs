package main

import (
	"database/sql"
	"fmt"

	_ "modernc.org/sqlite"
)

func main() {
	db, err := sql.Open("sqlite", "books.db")
	if err != nil {
		panic(err)
	}
	defer db.Close()

	createTableSQL := `CREATE TABLE IF NOT EXISTS books (
		id INTEGER PRIMARY KEY AUTOINCREMENT,
		title TEXT NOT NULL,
		author TEXT NOT NULL,
		year INTEGER
	);`

	_, err = db.Exec(createTableSQL)
	if err != nil {
		panic(fmt.Sprintf("Failed to create table: %v", err))
	}

	fmt.Println("Table 'books' created successfully.")

	// Insert 3 books
	insertSQL := `INSERT INTO books (title, author, year) VALUES (?, ?, ?)`
	books := []struct {
		title  string
		author string
		year   int
	}{
		{"The Go Programming Language", "Alan A. A. Donovan", 2015},
		{"Clean Code", "Robert C. Martin", 2008},
		{"Introduction to Algorithms", "Thomas H. Cormen", 2009},
	}

	for _, book := range books {
		_, err := db.Exec(insertSQL, book.title, book.author, book.year)
		if err != nil {
			panic(fmt.Sprintf("Failed to insert book: %v", err))
		}
	}

	fmt.Println("Inserted 3 books.")

	// Query and print all books
	rows, err := db.Query("SELECT id, title, author, year FROM books")
	if err != nil {
		panic(fmt.Sprintf("Failed to query books: %v", err))
	}
	defer rows.Close()

	fmt.Println("Books in database:")
	for rows.Next() {
		var id int
		var title, author string
		var year int
		err := rows.Scan(&id, &title, &author, &year)
		if err != nil {
			panic(fmt.Sprintf("Failed to scan row: %v", err))
		}
		fmt.Printf("%d: '%s' by %s (%d)\n", id, title, author, year)
	}
	if err := rows.Err(); err != nil {
		panic(fmt.Sprintf("Row error: %v", err))
	}
}
