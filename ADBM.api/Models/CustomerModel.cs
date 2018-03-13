using System.ComponentModel.DataAnnotations;

namespace ADBM.api.Models
{
    public class CustomerModel
    {
        [Display(Name = "Customer")]
        public string Customer { get; set; }  // yes, the adbmsamp database uses a STRING of a Customer for an ID.

        [Display(Name = "Firm Name")]
        public string FirmName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Business Phone")]
        public string BusinessPhone { get; set; }
    }
}