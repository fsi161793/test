using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.LCW.Domain.Abstract;
using SportsStore.LCW.WebUI.Models;

namespace SportsStore.LCW.WebUI.Controllers
{
    public class ProductController : Controller {
        //
        // GET: /Product/
        private IProductsRepository repository;
        public int pageSize = 4;

        public ProductController(IProductsRepository productRepository) {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1) {

            ProductsListViewModel mode = new ProductsListViewModel {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo  = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems =
                    category==null
                    ? repository.Products.Count()
                    : repository.Products.Count(e => e.Category == category)

                },
                CurrentCategory = category
            };

            return View(mode);
        }
    }
}
