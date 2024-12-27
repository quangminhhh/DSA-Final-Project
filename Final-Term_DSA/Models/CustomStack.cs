using System;
using System.Collections.Generic;

namespace CircularLinkedListApp.Models
{
    public class CustomStack<T> where T : IComparable<T>
    {
        private StackNode<T> top;

        public CustomStack()
        {
            top = null;
        }

        // Kiem tra stack rong
        public bool IsEmpty()
        {
            return top == null;
        }

        // Push mot ptu vao stack
        public bool Push(T data)
        {
            if (Contains(data))
                return false; // Trung gia tri

            StackNode<T> newNode = new StackNode<T>(data);
            newNode.Next = top;
            top = newNode;
            return true;
        }

        // pop ptu dau stack
        public bool Pop(out T data)
        {
            if (IsEmpty())
            {
                data = default(T);
                return false;
            }

            data = top.Data;
            top = top.Next;
            return true;
        }

        // Peek ptu stack (K loai bo)
        public bool Peek(out T data)
        {
            if (IsEmpty())
            {
                data = default(T);
                return false;
            }

            data = top.Data;
            return true;
        }

        // tim kiem
        public bool Search(T target)
        {
            if (IsEmpty()) return false;

            StackNode<T> current = top;
            while (current != null)
            {
                if (current.Data.CompareTo(target) == 0)
                    return true;
                current = current.Next;
            }

            return false;
        }

        // In stack thành danh sách
        public List<T> PrintStack()
        {
            List<T> stackList = new List<T>();
            StackNode<T> current = top;
            while (current != null)
            {
                stackList.Add(current.Data);
                current = current.Next;
            }
            return stackList;
        }

        // Kiểm tra sự tồn tại của một giá trị
        public bool Contains(T data)
        {
            StackNode<T> current = top;
            while (current != null)
            {
                if (current.Data.CompareTo(data) == 0)
                    return true;
                current = current.Next;
            }
            return false;
        }

        // Selection Sort
        public void SelectionSort()
        {
            if (IsEmpty())
                return;

            StackNode<T> current = top;
            while (current != null)
            {
                StackNode<T> min = current;
                StackNode<T> r = current.Next;

                while (r != null)
                {
                    if (r.Data.CompareTo(min.Data) < 0)
                        min = r;
                    r = r.Next;
                }

                // Swap dữ liệu nếu cần
                if (min != current)
                {
                    T temp = current.Data;
                    current.Data = min.Data;
                    min.Data = temp;
                }

                current = current.Next;
            }
        }
        // Quick Sort
        public void QuickSort()
        {
            List<T> dataList = PrintStack();
            QuickSortHelper(dataList, 0, dataList.Count - 1);

            // Cập nhật lại stack
            StackNode<T> current = top;
            int index = 0;
            foreach (var item in dataList)
            {
                if (current != null)
                {
                    current.Data = item;
                    current = current.Next;
                }
                else
                {
                    break;
                }
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

        // 1. Gộp hai stack
        public void Merge(CustomStack<T> stack2)
        {
            if (stack2.IsEmpty())
                return;

            List<T> tempList = stack2.PrintStack();
            // Đảo ngược để giữ thứ tự push
            tempList.Reverse();
            foreach (var item in tempList)
            {
                Push(item);
            }
        }

        // 2. Đảo ngược stack
        public void Reverse()
        {
            if (IsEmpty())
                return;

            StackNode<T> prev = null;
            StackNode<T> current = top;
            StackNode<T> next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            top = prev;
        }

        // 3. Loại bỏ tất cả các phần tử thỏa mãn điều kiện
        public void RemoveAll(Func<T, bool> condition)
        {
            while (!IsEmpty() && condition(top.Data))
            {
                Pop(out _);
            }

            StackNode<T> current = top;
            while (current != null && current.Next != null)
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
    }
}
