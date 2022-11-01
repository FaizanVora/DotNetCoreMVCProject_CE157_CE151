﻿using Canteen_Management_System.Models;
using Canteen_Management_System.Models.DbModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Canteen_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICustomerRepo _customerepo;
        private readonly AppDbContext _appDbContext;
        private readonly SqlOrderRepo _orderRepo;

        /* private readonly SignInManager<IdentityUser> _signin;*/

        public HomeController(ILogger<HomeController> logger, ICustomerRepo customerRepo,
            AppDbContext appDbContext)
        {
            _logger = logger;
            _customerepo = customerRepo;

            _appDbContext = appDbContext;
            
            /*_signin=signin;*/
        }
        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult menu()
        {
            return View();
        }
        public IActionResult Index()
        {
            /*string str = "name:";*/

            var model = _customerepo.GetallCustomer();
            return View(model);
        }
        public IActionResult Details(int ?id)
        {
            Customer customer = _customerepo.GetCustomerById(id ?? 1);
           /* ViewBag.Customer = customer;  */  

            return View(customer);
            

        }
        public IActionResult Alldetails()
        {

            /* ViewBag.Customer = customer;  */
            IEnumerable<Customer> list=_customerepo.GetallCustomer();
            return View(list);


        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
 


        [HttpPost]
        public IActionResult Signup(Customer customer)
        {
           
          Customer customer1 = _customerepo.AddCustomer(customer);
            
            
            return RedirectToAction("details",new {id=customer1.Id});
            
        }
        [HttpDelete]
        public IActionResult Signup(int id)
        {
            Customer customer1 = _customerepo.DeleteCustomer(id);


            return RedirectToAction("details", new { id = customer1.Id });

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _customerepo.GetCustomerById(id);

            return View(customer);
        }


        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            Customer customer1=_customerepo.GetCustomerById(customer.Id);
            customer1.Email = customer.Email; 
            customer1.Name = customer.Name;
            customer1.Password= customer.Password;
            _customerepo.UpdateCustomer(customer1);
            
            return RedirectToAction("index", new { id = customer1.Id });

        }
        [HttpGet]
        public IActionResult Menu()
        {
            var model = _appDbContext.Orders;
            return View(model);
        }


        /*public async Task<IActionResult> Logout()
        {
            _signin.SignOutAsync();
            return RedirectToAction("index");
        }*/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateOrder() {
            return View();
                
         }
        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {

            Order order1 = new Order
            {
                OrderId = order.OrderId,
                VegOrNonveg=order.VegOrNonveg,
                Ordername = order.Ordername,
                Orderphoto = order.Orderphoto,
                Orderprice = order.Orderprice,
            };

            _appDbContext.Orders.Add(order1);
            _appDbContext.SaveChanges();
            return RedirectToAction("menu");
            /*    Response.WriteAsJsonAsync(order1.Ordername);
                _appDbContext.Orders.Add(order1);
                return RedirectToAction("menu");*/



        }
    }
}