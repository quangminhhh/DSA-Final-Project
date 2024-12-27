using System;

namespace CircularLinkedListApp.Models
{
    // Định nghĩa kiểu dữ liệu Book
    public class Book : IComparable<Book>
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }

        // Constructor
        public Book(int number, string title, string author, string publisher, int year, string isbn)
        {
            Number = number;
            Title = title;
            Author = author;
            Publisher = publisher;
            Year = year;
            ISBN = isbn;
        }
        
        public int CompareTo(Book other)
        {
            if (other == null)
                return 1;
            return this.Number.CompareTo(other.Number);
        }
        
        public override string ToString()
        {
            return $"book{Number}";
        }
    }
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            this.Data = data;
            this.Next = null;
        }
    }
}