-- Create a new database
CREATE DATABASE BookCatalogDB;
GO

-- Create 'authors' table
CREATE TABLE authors (
    author_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,

);
GO

-- Create 'books' table
CREATE TABLE books (
    book_id INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(100) NOT NULL,
    publicationYear INT,
    author_id INT FOREIGN KEY REFERENCES authors(author_id)
);
GO

-- Insert sample authors
INSERT INTO authors (first_name, last_name ) VALUES ('Ilia', 'Chavchavadze');
INSERT INTO authors (first_name, last_name) VALUES ('Shota', 'Rustaveli');
INSERT INTO authors (first_name, last_name) VALUES ('Galaktion', 'Tabidze');
GO

-- Insert sample books
INSERT INTO books (title,  publicationYear, author_id) VALUES ('Kacia adamiani?',  1900, 1);
INSERT INTO books (title, publicationYear, author_id) VALUES ('Vefkhistkaosani',  1230, 2);
INSERT INTO books (title,publicationYear, author_id) VALUES ('Mtawmindis mtvare',  1937, 3);
GO

 
-- Update a book's publish year
UPDATE books SET publicationYear = 1231 WHERE book_id = 2;
GO

-- Delete a book by ID
DELETE FROM books WHERE book_id = 2;
GO

-- Delete an author by ID
DELETE FROM authors WHERE author_id = 3;
GO

-- Select all authors
SELECT * FROM authors;
GO

-- Select all books
SELECT * FROM books;
GO
 
SELECT b.title, a.first_name AS author_name
FROM books b
JOIN authors a ON b.author_id = a.author_id
WHERE b.publicationYear > 2010;