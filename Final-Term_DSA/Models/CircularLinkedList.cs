using System;
using System.Collections.Generic;

namespace CircularLinkedListApp.Models
{
    public class CircularLinkedList
    {
        private Node<Book> head;

        public CircularLinkedList()
        {
            head = null;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public Node<Book> CreateNode(Book data)
        {
            return new Node<Book>(data);
        }

        public bool Contains(Book data)
        {
            if (IsEmpty()) return false;

            Node<Book> current = head;
            do
            {
                if (current.Data.CompareTo(data) == 0)
                    return true;
                current = current.Next;
            } while (current != head);

            return false;
        }

        public void InsertAtBeginning(Book data)
        {
            Node<Book> newNode = CreateNode(data);
            if (IsEmpty())
            {
                head = newNode;
                newNode.Next = head;
            }
            else
            {
                Node<Book> temp = head;
                while (temp.Next != head)
                    temp = temp.Next;

                newNode.Next = head;
                temp.Next = newNode;
                head = newNode;
            }
        }

        public void InsertAtEnd(Book data)
        {
            Node<Book> newNode = CreateNode(data);
            if (IsEmpty())
            {
                head = newNode;
                newNode.Next = head;
            }
            else
            {
                Node<Book> temp = head;
                while (temp.Next != head)
                    temp = temp.Next;

                temp.Next = newNode;
                newNode.Next = head;
            }
        }

        public bool InsertAfter(Book target, Book data)
        {
            if (IsEmpty()) return false;

            Node<Book> current = head;
            do
            {
                if (current.Data.CompareTo(target) == 0)
                {
                    Node<Book> newNode = CreateNode(data);
                    newNode.Next = current.Next;
                    current.Next = newNode;
                    return true;
                }
                current = current.Next;
            } while (current != head);

            return false;
        }

        public bool InsertAtBeginningUnique(Book data)
        {
            if (Contains(data))
                return false; 
            InsertAtBeginning(data);
            return true;
        }

        public bool InsertAtEndUnique(Book data)
        {
            if (Contains(data))
                return false; 
            InsertAtEnd(data);
            return true;
        }

        public bool InsertAfterUnique(Book target, Book data)
        {
            if (Contains(data))
                return false; 
            return InsertAfter(target, data);
        }

        public bool RemoveFirst()
        {
            if (IsEmpty()) return false;

            if (head.Next == head)
            {
                head = null;
                return true;
            }

            Node<Book> temp = head;
            while (temp.Next != head)
                temp = temp.Next;

            temp.Next = head.Next;
            head = head.Next;
            return true;
        }

        public bool RemoveLast()
        {
            if (IsEmpty()) return false;

            if (head.Next == head)
            {
                head = null;
                return true;
            }

            Node<Book> current = head;
            Node<Book> previous = null;

            while (current.Next != head)
            {
                previous = current;
                current = current.Next;
            }

            previous.Next = head;
            return true;
        }

        public bool RemoveAfter(Book target)
        {
            if (IsEmpty()) return false;

            Node<Book> current = head;
            do
            {
                if (current.Data.CompareTo(target) == 0)
                {
                    if (current.Next == head)
                    {
                        RemoveFirst();
                    }
                    else
                    {
                        current.Next = current.Next.Next;
                    }
                    return true;
                }
                current = current.Next;
            } while (current != head);

            return false; 
        }

        public Node<Book> Search(Book target)
        {
            if (IsEmpty()) return null;

            Node<Book> current = head;
            do
            {
                if (current.Data.CompareTo(target) == 0)
                    return current;
                current = current.Next;
            } while (current != head);

            return null; 
        }

        public List<Node<Book>> SearchByCondition(Func<Book, bool> condition)
        {
            List<Node<Book>> result = new List<Node<Book>>();
            if (IsEmpty()) return result;

            Node<Book> current = head;
            do
            {
                if (condition(current.Data))
                    result.Add(current);
                current = current.Next;
            } while (current != head);

            return result;
        }

        public List<Book> PrintList()
        {
            List<Book> listData = new List<Book>();
            if (IsEmpty()) return listData;

            Node<Book> current = head;
            do
            {
                listData.Add(current.Data);
                current = current.Next;
            } while (current != head);

            return listData;
        }

        public void SelectionSort()
        {
            if (IsEmpty()) return;

            Node<Book> current = head;
            do
            {
                Node<Book> min = current;
                Node<Book> r = current.Next;

                while (r != head)
                {
                    if (r.Data.CompareTo(min.Data) < 0)
                        min = r;
                    r = r.Next;
                }

                if (min != current)
                {
                    Book temp = current.Data;
                    current.Data = min.Data;
                    min.Data = temp;
                }

                current = current.Next;
            } while (current.Next != head.Next);
        }

        public void QuickSort()
        {
            if (IsEmpty()) return;

            List<Book> dataList = PrintList();
            QuickSortHelper(dataList, 0, dataList.Count - 1);

            Node<Book> current = head;
            int index = 0;
            while (current != null && index < dataList.Count)
            {
                current.Data = dataList[index];
                current = current.Next;
                if (current == head)
                    break;
                index++;
            }
        }

        private void QuickSortHelper(List<Book> data, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(data, low, high);
                QuickSortHelper(data, low, pi - 1);
                QuickSortHelper(data, pi + 1, high);
            }
        }

        private int Partition(List<Book> data, int low, int high)
        {
            Book pivot = data[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (data[j].CompareTo(pivot) < 0)
                {
                    i++;
                    // Swap
                    Book temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                }
            }
            // Swap pivot
            Book temp1 = data[i + 1];
            data[i + 1] = data[high];
            data[high] = temp1;
            return i + 1;
        }

        // Gộp hai danh sách
        public void Merge(CircularLinkedList list2)
        {
            if (list2.IsEmpty()) return;

            if (this.IsEmpty())
            {
                this.head = list2.head;
                return;
            }

            Node<Book> temp1 = this.head;
            while (temp1.Next != this.head)
                temp1 = temp1.Next;

            Node<Book> temp2 = list2.head;
            temp1.Next = temp2;

            while (temp2.Next != list2.head)
                temp2 = temp2.Next;

            temp2.Next = this.head;
        }

        // Loại bỏ tất cả các nút thỏa mãn điều kiện
        public void RemoveAll(Func<Book, bool> condition)
        {
            if (IsEmpty()) return;

            while (head != null && condition(head.Data))
            {
                if (head.Next == head)
                {
                    head = null;
                    return;
                }

                Node<Book> temp = head;
                while (temp.Next != head)
                    temp = temp.Next;

                temp.Next = head.Next;
                head = head.Next;
            }

            if (head == null) return;

            Node<Book> current = head;
            while (current.Next != head)
            {
                if (condition(current.Next.Data))
                {
                    current.Next = current.Next.Next;
                }
                else
                {
                    current = current.Next;
                }
            }
        }

        public void Reverse()
        {
            if (IsEmpty() || head.Next == head) return;

            Node<Book> prev = null;
            Node<Book> current = head;
            Node<Book> next = null;
            do
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            } while (current != head);

            head.Next = prev;
            head = prev;
        }
    }
}
