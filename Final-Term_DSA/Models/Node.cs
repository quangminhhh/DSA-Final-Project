using System;

namespace CircularLinkedListApp.Models
{
    // Định nghĩa kiểu dữ liệu Book
    public class Book : IComparable<Book>
    {
        // Thuộc tính Number dùng để phân biệt bookX, 
        // bookX là tên hiển thị, không phải Title.
        public int Number { get; set; }  // X (VD: 1 -> book1)
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }

        // Constructor để khởi tạo một Book
        // Người dùng sẽ nhập Number, Title, Author, Publisher, Year, ISBN
        public Book(int number, string title, string author, string publisher, int year, string isbn)
        {
            Number = number;
            Title = title;
            Author = author;
            Publisher = publisher;
            Year = year;
            ISBN = isbn;
        }

        // Implement IComparable<Book> để so sánh theo Number
        public int CompareTo(Book other)
        {
            if (other == null)
                return 1;
            return this.Number.CompareTo(other.Number);
        }

        // Override ToString hiển thị dạng bookX (X là Number)
        // và có thể kèm theo một số thông tin để kiểm tra
        public override string ToString()
        {
            // Chỉ hiển thị đơn giản bookX, 
            // thông tin chi tiết sẽ xem khi click vào node hoặc khi cần thiết
            return $"book{Number}";
        }
    }

    // Định nghĩa Node
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