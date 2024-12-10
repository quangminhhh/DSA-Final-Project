using System;

namespace CircularLinkedListApp.Models
{
    public class Node<T>
    {
        // maybe la nhu nay
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            this.Data = data;
            this.Next = null;
        }
    }
}