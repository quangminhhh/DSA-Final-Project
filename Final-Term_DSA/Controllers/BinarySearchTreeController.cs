using Microsoft.AspNetCore.Mvc;
using CircularLinkedListApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CircularLinkedListApp.Controllers
{
    public class BinarySearchTreeController : Controller
    {
        private static BinarySearchTree<int> bst = new BinarySearchTree<int>();
        
        public IActionResult Index()
        {
            var traversal = bst.InOrderTraversal(); // Bạn có thể thay đổi phương thức traverse theo nhu cầu
            return View(traversal);
        }
        [HttpPost]
        public IActionResult Insert(int data)
        {
            bool result = bst.Insert(data);
            TempData["Message"] = result ? $"Inserted {data} successfully." : $"Insert failed: {data} already exists in the BST.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int data)
        {
            bool exists = bst.Search(data);
            if (exists)
            {
                bst.Delete(data);
                TempData["Message"] = $"Deleted {data} successfully.";
            }
            else
            {
                TempData["Message"] = $"Delete failed: {data} does not exist in the BST.";
            }
            return RedirectToAction("Index");
        }

        // GET: Search
        public IActionResult Search(int target)
        {
            bool found = bst.Search(target);
            if (found)
            {
                ViewBag.SearchResult = $"Value {target} exists in the BST.";
            }
            else
            {
                ViewBag.SearchResult = $"Value {target} does not exist in the BST.";
            }
            var traversal = bst.InOrderTraversal();
            return View("Index", traversal);
        }

        // GET: Traverse
        public IActionResult Traverse(string order)
        {
            List<int> traversal;
            switch (order)
            {
                case "InOrder":
                    traversal = bst.InOrderTraversal();
                    break;
                case "PreOrder":
                    traversal = bst.PreOrderTraversal();
                    break;
                case "PostOrder":
                    traversal = bst.PostOrderTraversal();
                    break;
                default:
                    traversal = bst.InOrderTraversal();
                    break;
            }
            ViewBag.TraverseOrder = order;
            return View("Index", traversal);
        }

        // GET: Visualization
        public IActionResult Visualization()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetTreeData()
        {
            if (bst.Root == null)
            {
                return Json(new { name = "Empty" });
            }

            var treeData = ConvertToTreeData(bst.Root);
            return Json(treeData);
        }

        private object ConvertToTreeData(TreeNode<int> node)
        {
            if (node == null)
                return null;

            return new
            {
                name = node.Data.ToString(),
                children = new List<object>
                {
                    ConvertToTreeData(node.Left),
                    ConvertToTreeData(node.Right)
                }.Where(child => child != null).ToList()
            };
        }
    }
}
    

