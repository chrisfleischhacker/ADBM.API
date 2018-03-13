using System.Collections.Generic;
using System.Web.Mvc;
using ADBM.api.Models;

namespace ADBM.api.Controllers
{
    public class CustomerController : Controller
    {
        // 1. *********** Display All Item List in Index Page ***********
        public ActionResult Index()
        {
            ViewBag.ItemList = "Customer List Page";
            CustomerDBHandler IHandler = new CustomerDBHandler();
            ModelState.Clear();
            return View(IHandler.GetCustomerList());
        }

        // 2. *********** Add New Item ***********
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CustomerModel cList)
        {
            // try
            //{
            if (ModelState.IsValid)
            {
                CustomerDBHandler CustomerHandler = new CustomerDBHandler();
                if (CustomerHandler.InsertItem(cList))
                {
                    ViewBag.Message = "Customer Added Successfully";
                    ModelState.Clear();
                }
            }
            return View();
            /* }
             catch { return View();  }*/
        }

        // 3. *********** Update Item Details ***********
        [HttpGet]
        public ActionResult Edit(string id)
        {
            CustomerDBHandler CustomerHandler = new CustomerDBHandler();
            return View(CustomerHandler.GetCustomerList().Find(customermodel => customermodel.Customer == id));
        }
        [HttpPost]
        public ActionResult Edit(string id, CustomerModel cList)
        {
            try
            {
                CustomerDBHandler CustomerHandler = new CustomerDBHandler();
                CustomerHandler.UpdateCustomer(cList);
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        // 4. *********** Delete Item Details ***********
        public ActionResult Delete(string id)
        {
            try
            {
                CustomerDBHandler CustomerHandler = new CustomerDBHandler();
                if (CustomerHandler.DeleteCustomer(id))
                {
                    ViewBag.AlertMsg = "Item Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        public ActionResult Details(string id)
        {
            CustomerDBHandler CustomerHandler = new CustomerDBHandler();
            return View(CustomerHandler.GetCustomerList().Find(customermodel => customermodel.Customer == id));
        }
    }
}