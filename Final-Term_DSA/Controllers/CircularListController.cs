using Microsoft.AspNetCore.Mvc;
using CircularLinkedListApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace CircularLinkedListApp.Controllers
{
    public class CircularListController : Controller
    {
        // Tạo một danh sách liên kết vòng tĩnh để lưu trữ dữ liệu
        private static CircularLinkedList<int> list = new CircularLinkedList<int>();

        // GET: CircularList
        public IActionResult Index()
        {
            var data = list.PrintList();
            return View(data);
        }

        // POST: InsertAtBeginning
        [HttpPost]
        public IActionResult InsertAtBeginning(int data)
        {
            bool result = list.InsertAtBeginningUnique(data);
            TempData["Message"] = result ? "Inserted at the beginning successfully." : "Insert failed: Value already exists in the list.";
            return RedirectToAction("Index");
        }

        // POST: InsertAtEnd
        [HttpPost]
        public IActionResult InsertAtEnd(int data)
        {
            bool result = list.InsertAtEndUnique(data);
            TempData["Message"] = result ? "Inserted at the end successfully." : "Insert failed: Value already exists in the list.";
            return RedirectToAction("Index");
        }

        // POST: InsertAfter
        [HttpPost]
        public IActionResult InsertAfter(int target, int data)
        {
            bool result = list.InsertAfterUnique(target, data);
            TempData["Message"] = result ? "Inserted after target successfully." : "Insert failed: Value already exists or target not found.";
            return RedirectToAction("Index");
        }


        // POST: RemoveFirst
        [HttpPost]
        public IActionResult RemoveFirst()
        {
            bool result = list.RemoveFirst();
            TempData["Message"] = result ? "Removed first node successfully." : "List is empty.";
            return RedirectToAction("Index");
        }

        // POST: RemoveLast
        [HttpPost]
        public IActionResult RemoveLast()
        {
            bool result = list.RemoveLast();
            TempData["Message"] = result ? "Removed last node successfully." : "List is empty.";
            return RedirectToAction("Index");
        }

        // POST: RemoveAfter
        [HttpPost]
        public IActionResult RemoveAfter(int target)
        {
            bool result = list.RemoveAfter(target);
            TempData["Message"] = result ? "Removed node after target successfully." : "Target not found.";
            return RedirectToAction("Index");
        }

        // GET: Search
        public IActionResult Search(int target)
        {
            var node = list.Search(target);
            if (node != null)
            {
                ViewBag.Result = $"Found: {node.Data}";
            }
            else
            {
                ViewBag.Result = "Not found.";
            }
            return View("Index", list.PrintList());
        }

        // POST: SortSelection
        [HttpPost]
        public IActionResult SortSelection()
        {
            list.SelectionSort();
            TempData["Message"] = "List sorted using Selection Sort.";
            return RedirectToAction("Index");
        }

        // POST: SortQuick
        [HttpPost]
        public IActionResult SortQuick()
        {
            list.QuickSort();
            TempData["Message"] = "List sorted using Quick Sort.";
            return RedirectToAction("Index");
        }

        // POST: Merge
        [HttpPost]
        public IActionResult Merge(string otherList)
        {
            if (!string.IsNullOrEmpty(otherList))
            {
                var numbers = otherList.Split(',').Select(s => 
                {
                    int.TryParse(s.Trim(), out int num);
                    return num;
                }).ToArray();

                var newList = new CircularLinkedList<int>();
                foreach (var item in numbers)
                {
                    if (item != 0) // Giả sử 0 là giá trị không hợp lệ hoặc bỏ qua
                        newList.InsertAtEnd(item);
                }
                list.Merge(newList);
                TempData["Message"] = "Lists merged successfully.";
            }
            else
            {
                TempData["Message"] = "No data provided to merge.";
            }
            return RedirectToAction("Index");
        }

        // POST: RemoveAllGreaterThan
        [HttpPost]
        public IActionResult RemoveAllGreaterThan(int value)
        {
            list.RemoveAll(x => x > value);
            TempData["Message"] = $"All nodes with value greater than {value} removed.";
            return RedirectToAction("Index");
        }

        // POST: Reverse
        [HttpPost]
        public IActionResult Reverse()
        {
            list.Reverse();
            TempData["Message"] = "List reversed successfully.";
            return RedirectToAction("Index");
        }
    }
}
