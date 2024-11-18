using Microsoft.AspNetCore.Mvc;
using CircularLinkedListApp.Models; // Đảm bảo sử dụng namespace Models để truy cập CustomStack<T>
using System.Collections.Generic;
using System.Linq;

namespace CircularLinkedListApp.Controllers
{
    public class StackController : Controller
    {

        private static CustomStack<int> stack = new CustomStack<int>();

        // GET: Stack
        public IActionResult Index()
        {
            var data = stack.PrintStack();
            return View(data);
        }

        // POST: Push
        [HttpPost]
        public IActionResult Push(int data)
        {
            bool result = stack.Push(data);
            TempData["Message"] = result ? "Pushed successfully." : "Push failed: Value already exists in the stack.";
            return RedirectToAction("Index");
        }

        // POST: Pop
        [HttpPost]
        public IActionResult Pop()
        {
            bool result = stack.Pop(out int poppedData);
            TempData["Message"] = result ? $"Popped: {poppedData}" : "Pop failed: Stack is empty.";
            return RedirectToAction("Index");
        }

        // GET: Peek
        public IActionResult Peek()
        {
            bool result = stack.Peek(out int topData);
            if (result)
            {
                ViewBag.PeekResult = $"Top of Stack: {topData}";
            }
            else
            {
                ViewBag.PeekResult = "Peek failed: Stack is empty.";
            }
            return View("Index", stack.PrintStack());
        }

        // POST: SortSelection
        [HttpPost]
        public IActionResult SortSelection()
        {
            stack.SelectionSort();
            TempData["Message"] = "Stack sorted using Selection Sort.";
            return RedirectToAction("Index");
        }

        // POST: SortQuick
        [HttpPost]
        public IActionResult SortQuick()
        {
            stack.QuickSort();
            TempData["Message"] = "Stack sorted using Quick Sort.";
            return RedirectToAction("Index");
        }

        // POST: Merge
        [HttpPost]
        public IActionResult Merge(string otherStack)
        {
            if (!string.IsNullOrEmpty(otherStack))
            {
                var numbers = otherStack.Split(',').Select(s =>
                {
                    int.TryParse(s.Trim(), out int num);
                    return num;
                }).ToArray();

                bool anyInserted = false;
                bool anyFailed = false;

                foreach (var item in numbers)
                {
                    if (item != 0) // Giả sử 0 là giá trị không hợp lệ hoặc bỏ qua
                    {
                        bool inserted = stack.Push(item);
                        if (inserted)
                            anyInserted = true;
                        else
                            anyFailed = true;
                    }
                }

                if (anyInserted && anyFailed)
                    TempData["Message"] = "Some values were merged successfully, but some duplicates were skipped.";
                else if (anyInserted)
                    TempData["Message"] = "Stacks merged successfully.";
                else
                    TempData["Message"] = "Merge failed: All values already exist in the stack.";
            }
            else
            {
                TempData["Message"] = "No data provided to merge.";
            }
            return RedirectToAction("Index");
        }

        // POST: Reverse
        [HttpPost]
        public IActionResult Reverse()
        {
            stack.Reverse();
            TempData["Message"] = "Stack reversed successfully.";
            return RedirectToAction("Index");
        }

        // POST: RemoveAllGreaterThan
        [HttpPost]
        public IActionResult RemoveAllGreaterThan(int value)
        {
            stack.RemoveAll(x => x > value);
            TempData["Message"] = $"All stack elements greater than {value} removed.";
            return RedirectToAction("Index");
        }

        // GET: Search
        public IActionResult Search(int target)
        {
            bool found = stack.Search(target);
            if (found)
            {
                ViewBag.SearchResult = $"Value {target} exists in the stack.";
            }
            else
            {
                ViewBag.SearchResult = $"Value {target} does not exist in the stack.";
            }
            return View("Index", stack.PrintStack());
        }
    }
}
