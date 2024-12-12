using Microsoft.AspNetCore.Mvc;
using CircularLinkedListApp.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CircularLinkedListApp.Controllers
{
    public class CircularListController : Controller
    {
        // Danh sách chính
        private static CircularLinkedList mainList = new CircularLinkedList();
        // Danh sách phụ
        private static CircularLinkedList secondaryList = new CircularLinkedList();

        // GET: CircularList
        public IActionResult Index()
        {
            var data = mainList.PrintList();                    // Dữ liệu cho mainList
            var secondaryData = secondaryList.PrintList();      // Dữ liệu cho secondaryList
            ViewBag.SecondaryData = secondaryData;
            return View(data);
        }

        // -------------------- Main List Actions --------------------

        [HttpPost]
        public IActionResult InsertAtBeginning(int number, string title, string author, string publisher, int year, string isbn)
        {
            var newBook = new Book(number, title, author, publisher, year, isbn);
            bool result = mainList.InsertAtBeginningUnique(newBook);
            TempData["Message"] = result 
                ? "Inserted at the beginning successfully." 
                : "Insert failed: book with this Number already exists in the list.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult InsertAtEnd(int number, string title, string author, string publisher, int year, string isbn)
        {
            var newBook = new Book(number, title, author, publisher, year, isbn);
            bool result = mainList.InsertAtEndUnique(newBook);
            TempData["Message"] = result 
                ? "Inserted at the end successfully." 
                : "Insert failed: book with this Number already exists in the list.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult InsertAfter(int targetNumber, int dataNumber, string title, string author, string publisher, int year, string isbn)
        {
            var targetBook = new Book(targetNumber, "N/A", "N/A", "N/A", 2000, "N/A");
            var newBook = new Book(dataNumber, title, author, publisher, year, isbn);

            bool result = mainList.InsertAfterUnique(targetBook, newBook);
            TempData["Message"] = result 
                ? "Inserted after target successfully." 
                : "Insert failed: book with this Number already exists or target not found.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFirst()
        {
            bool result = mainList.RemoveFirst();
            TempData["Message"] = result 
                ? "Removed first node successfully." 
                : "List is empty.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveLast()
        {
            bool result = mainList.RemoveLast();
            TempData["Message"] = result 
                ? "Removed last node successfully." 
                : "List is empty.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveAfter(int targetNumber)
        {
            var targetBook = new Book(targetNumber, "N/A", "N/A", "N/A", 2000, "N/A");
            bool result = mainList.RemoveAfter(targetBook);
            TempData["Message"] = result 
                ? "Removed node after target successfully." 
                : "Target not found.";
            return RedirectToAction("Index");
        }

        public IActionResult Search(int targetNumber)
        {
            var targetBook = new Book(targetNumber, "N/A", "N/A", "N/A", 2000, "N/A");
            var node = mainList.Search(targetBook);
            if (node != null)
            {
                ViewBag.Result = $"Found: {node.Data.ToString()}";
            }
            else
            {
                ViewBag.Result = "Not found.";
            }
            return View("Index", mainList.PrintList());
        }

        [HttpPost]
        public IActionResult SortSelection()
        {
            mainList.SelectionSort();
            TempData["Message"] = "List sorted using Selection Sort.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SortQuick()
        {
            mainList.QuickSort();
            TempData["Message"] = "List sorted using Quick Sort.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveAllGreaterThan(int value)
        {
            mainList.RemoveAll(x => x.Number > value);
            TempData["Message"] = $"All nodes with Number greater than {value} removed.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reverse()
        {
            mainList.Reverse();
            TempData["Message"] = "List reversed successfully.";
            return RedirectToAction("Index");
        }

        // -------------------- Secondary List Actions --------------------
        [HttpPost]
        public IActionResult InsertAtEnd_Secondary(int number, string title, string author, string publisher, int year, string isbn)
        {
            var newBook = new Book(number, title, author, publisher, year, isbn);
            bool result = secondaryList.InsertAtEndUnique(newBook);
            TempData["Message"] = result 
                ? "Inserted at the end of secondary list successfully." 
                : "Insert failed: book with this Number already exists in secondary list.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MergeLists()
        {
            if (!secondaryList.IsEmpty())
            {
                mainList.Merge(secondaryList);
                secondaryList = new CircularLinkedList();
                TempData["Message"] = "Lists merged successfully.";
            }
            else
            {
                TempData["Message"] = "Secondary list is empty, nothing to merge.";
            }
            return RedirectToAction("Index");
        }

        // -------------------- Details Action --------------------
        public IActionResult Details(int number)
        {
            var targetBook = new Book(number, "N/A", "N/A", "N/A", 2000, "N/A");
            var node = mainList.Search(targetBook);
            if (node == null)
            {
                TempData["Message"] = "Node not found.";
                return RedirectToAction("Index");
            }

            var book = node.Data; // Book tìm thấy
            return View(book);
        }
    }
}
