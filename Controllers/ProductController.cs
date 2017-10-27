using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FritoLay.Models.Repositories;
using FritoLay.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FritoLay.Controllers
{

    public class ProductController : Controller
    {
        private IProductRepository productRepo { get; }
        private readonly UserManager<User> _userManager;

        public ProductController(UserManager<User> userManager,IProductRepository thisRepo = null)
        {
            _userManager = userManager;

            if (thisRepo == null)
            {
                this.productRepo = EFProductRepository.Instance;
            }
            else
            {
                this.productRepo = thisRepo;
            }
        }
        public IActionResult Index()
        {
            return View(this.productRepo.Products.ToList());
        }

        public IActionResult Edit(int id)
        {

            var product = this.productRepo.Products.FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }

        public IActionResult CreateReview(int id)
        {
            var product = this.productRepo.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                throw new Exception($"Product {id} does not exist");
            }

            var review = new Review();
            review.ProductId = id;
            review.Rating = 5;
            return View(review);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            this.productRepo.Edit(product);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var product = this.productRepo.Products.Include(x => x.Reviews).FirstOrDefault(p => p.ProductId == id);
            //Console.WriteLine($"{product.Reviews.Count}");
            return View(product);
        }

        public IActionResult Create()
        {
            ViewBag.ProductId = new SelectList(this.productRepo.Products, "ProductId", "ProductName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            this.productRepo.Save(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateReview(Review review)
        {
            this.productRepo.Save(review);
            return View("Details", this.productRepo.Products.First(x => x.ProductId == review.ProductId));
        }
        public ActionResult Delete(int id)
        {
            var thisProduct = this.productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisProduct = this.productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            this.productRepo.Remove(thisProduct);
            return RedirectToAction("Index");
        }
    }
}
