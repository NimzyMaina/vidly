using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            //var customers = GetCustomers();
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult View(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer{ Id = 1, Name = "Lolo"},
                new Customer{ Id = 2, Name = "Nimzy"}
            };
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var newCustomerViewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",newCustomerViewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
//            if (!ModelState.IsValid)
//            {
//                var membershipTypes = _context.MembershipTypes.ToList();
//                var newCustomerViewModel = new NewCustomerViewModel()
//                {
//                    MembershipTypes = membershipTypes
//                };
//                return View("New", newCustomerViewModel);
//            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.Name = customer.Name;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");

        }

        public ActionResult Edit(int Id, Customer customer)
        {
            var customerInDb =  _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customerInDb == null)
                return HttpNotFound();

            var viewModel =  new CustomerFormViewModel
            {
                Customer = customerInDb,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm",viewModel);
        }
    }

}