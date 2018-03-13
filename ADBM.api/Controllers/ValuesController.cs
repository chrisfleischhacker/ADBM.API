using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ADBM.api.Models;

namespace ADBM.api.Controllers
{
    public class ValuesController : ApiController
    {
        public IEnumerable<CustomerModel> Get()
        {
            CustomerDBHandler IHandler = new CustomerDBHandler();
            ModelState.Clear();
            return IHandler.GetCustomerList();
        }

        public IHttpActionResult Get(string id)
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

        public IHttpActionResult Post([FromBody]CustomerModel cList)
        {
            if (ModelState.IsValid)
            {
                CustomerDBHandler CustomerHandler = new CustomerDBHandler();
                if (CustomerHandler.InsertItem(cList))
                {
                    ModelState.Clear();
                }
            }
            return this.CreatedAtRoute("CreatedAt", new { controller = "Customer", id = cList.Customer }, cList);
        }

        public IHttpActionResult Put(string id, [FromBody]CustomerModel cList)
        {
            CustomerDBHandler IHandler = new CustomerDBHandler();
            ModelState.Clear();
            var customer = IHandler.GetCustomerList().FirstOrDefault((c) => c.Customer == id);
            if (customer.Customer != id)
            {
                return BadRequest();
            }

            if (IHandler.GetCustomerList().FirstOrDefault((c) => c.Customer == id) == null)
            {
                return this.NotFound();
            }

            IHandler.UpdateCustomer(cList);
            return Ok(cList);
        }

        public IHttpActionResult Delete(string id)
        {
            CustomerDBHandler IHandler = new CustomerDBHandler();
            ModelState.Clear();
            var customer = IHandler.DeleteCustomer(id);
            return Ok("Deleted " + id);
        }
    }
}
