using System;

namespace CircularLinkedListApp.Models
{
    public class StackNode<T>
    {
        public T Data { get; set; }
        public StackNode<T> Next { get; set; }

        public StackNode(T data)
        {
            this.Data = data;
            this.Next = null;
        }
    }
}