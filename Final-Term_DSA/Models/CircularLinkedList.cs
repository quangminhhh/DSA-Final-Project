using System;
using System.Collections.Generic;

namespace CircularLinkedListApp.Models
{
    // Mot so bien bat buoc dung int de code logic de hon
    public class CircularLinkedList<T> where T : IComparable<T>
    {
        private Node<T> head;

        public CircularLinkedList()
        {
            head = null;
        }


        public bool IsEmpty()
        {
            return head == null;
        }


        public Node<T> CreateNode(T data)
        {
            return new Node<T>(data);
        }
        

        public bool Contains(T data)
        {
            if (IsEmpty()) return false;

            Node<T> current = head;
            do
            {
                if (current.Data.CompareTo(data) == 0)
                    return true;
                current = current.Next;
            } while (current != head);

            return false;
        }



        public void InsertAtBeginning(T data)
        {
            Node<T> newNode = CreateNode(data);
            if (IsEmpty())
            {
                head = newNode;
                newNode.Next = head;
            }
            else
            {
                Node<T> temp = head;
                while (temp.Next != head)
                    temp = temp.Next;

                newNode.Next = head;
                temp.Next = newNode;
                head = newNode;
            }
        }


        public void InsertAtEnd(T data)
        {
            Node<T> newNode = CreateNode(data);
            if (IsEmpty())
            {
                head = newNode;
                newNode.Next = head;
            }
            else
            {
                Node<T> temp = head;
                while (temp.Next != head)
                    temp = temp.Next;

                temp.Next = newNode;
                newNode.Next = head;
            }
        }


        public bool InsertAfter(T target, T data)
        {
            if (IsEmpty()) return false;

            Node<T> current = head;
            do
            {
                if (current.Data.CompareTo(target) == 0)
                {
                    Node<T> newNode = CreateNode(data);
                    newNode.Next = current.Next;
                    current.Next = newNode;
                    return true;
                }
                current = current.Next;
            } while (current != head);

            return false; 
        }
        public bool InsertAtBeginningUnique(T data)
        {
            if (Contains(data))
                return false; 

            InsertAtBeginning(data);
            return true;
        }


        public bool InsertAtEndUnique(T data)
        {
            if (Contains(data))
                return false; 

            InsertAtEnd(data);
            return true;
        }
        
        public bool InsertAfterUnique(T target, T data)
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

            Node<T> temp = head;
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

            Node<T> current = head;
            Node<T> previous = null;

            while (current.Next != head)
            {
                previous = current;
                current = current.Next;
            }

            previous.Next = head;
            return true;
        }
        
        public bool RemoveAfter(T target)
        {
            if (IsEmpty()) return false;

            Node<T> current = head;
            do
            {
                if (current.Data.CompareTo(target) == 0)
                {
                    if (current.Next == head)
                    {
                        // Nếu nút sau là head
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
        
        public Node<T> Search(T target)
        {
            if (IsEmpty()) return null;

            Node<T> current = head;
            do
            {
                if (current.Data.CompareTo(target) == 0)
                    return current;
                current = current.Next;
            } while (current != head);

            return null; 
        }
        
        public List<Node<T>> SearchByCondition(Func<T, bool> condition)
        {
            List<Node<T>> result = new List<Node<T>>();
            if (IsEmpty()) return result;

            Node<T> current = head;
            do
            {
                if (condition(current.Data))
                    result.Add(current);
                current = current.Next;
            } while (current != head);

            return result;
        }
        
        public List<T> PrintList()
        {
            List<T> listData = new List<T>();
            if (IsEmpty()) return listData;

            Node<T> current = head;
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

            Node<T> current = head;
            do
            {
                Node<T> min = current;
                Node<T> r = current.Next;

                while (r != head)
                {
                    if (r.Data.CompareTo(min.Data) < 0)
                        min = r;
                    r = r.Next;
                }
                
                if (min != current)
                {
                    T temp = current.Data;
                    current.Data = min.Data;
                    min.Data = temp;
                }

                current = current.Next;
            } while (current.Next != head.Next);
        }
        
        public void QuickSort()
        {
            if (IsEmpty()) return;
            
            List<T> dataList = PrintList();
            QuickSortHelper(dataList, 0, dataList.Count - 1);
            
            Node<T> current = head;
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

        private void QuickSortHelper(List<T> data, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(data, low, high);
                QuickSortHelper(data, low, pi - 1);
                QuickSortHelper(data, pi + 1, high);
            }
        }

        private int Partition(List<T> data, int low, int high)
        {
            T pivot = data[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (data[j].CompareTo(pivot) < 0)
                {
                    i++;
                    // Swap
                    T temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                }
            }
            // Swap pivot
            T temp1 = data[i + 1];
            data[i + 1] = data[high];
            data[high] = temp1;
            return i + 1;
        }
        

        // 1. Gộp hai danh sách
        public void Merge(CircularLinkedList<T> list2)
        {
            if (list2.IsEmpty()) return;

            if (this.IsEmpty())
            {
                this.head = list2.head;
                return;
            }

            Node<T> temp1 = this.head;
            while (temp1.Next != this.head)
                temp1 = temp1.Next;

            Node<T> temp2 = list2.head;
            temp1.Next = temp2;

            while (temp2.Next != list2.head)
                temp2 = temp2.Next;

            temp2.Next = this.head;
        }

        // 2. Loại bỏ tất cả các nút thỏa mãn điều kiện
        public void RemoveAll(Func<T, bool> condition)
        {
            if (IsEmpty()) return;
            
            while (head != null && condition(head.Data))
            {
                if (head.Next == head)
                {
                    head = null;
                    return;
                }

                Node<T> temp = head;
                while (temp.Next != head)
                    temp = temp.Next;

                temp.Next = head.Next;
                head = head.Next;
            }
            
            if (head == null) return;

            Node<T> current = head;
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

            Node<T> prev = null;
            Node<T> current = head;
            Node<T> next = null;
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
