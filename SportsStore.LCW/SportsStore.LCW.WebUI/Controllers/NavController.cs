using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.LCW.Domain.Abstract;

namespace SportsStore.LCW.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/
        private IProductsRepository repository;

        public NavController(IProductsRepository repo) {

            repository = repo;
        }

        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult Menu(string category) {
            ViewBag.SelectedCategory = category;
            return PartialView(repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x=>x)
                );
        }
    }

    
}
