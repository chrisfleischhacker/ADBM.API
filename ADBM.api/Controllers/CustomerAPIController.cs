using System.Collections.Generic;
using System.Linq;
using ADBM.api.Models;
using System.Web.Http;

namespace ADBM.api.Controllers
{
    public class CustomerAPIController : ApiController
    {
        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            CustomerDBHandler IHandler = new CustomerDBHandler();
            ModelState.Clear();
            return IHandler.GetCustomerList();
        }

        public IHttpActionResult GetCustomer(string id)
        {
            CustomerDBHandler IHandler = new CustomerDBHandler();
            ModelState.Clear();
            var customer = IHandler.GetCustomerList().FirstOrDefault((c) => c.Customer == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
