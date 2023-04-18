using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UYGS203.ViewModel
{
    public class EstateModel
    {
        public string EstateId { get; set; }
        public string EstateName { get; set; }
        public string EstatePrice { get; set; }
        public string DiscountPrice { get; set; }
        public string Clicks { get; set; }
        public string IsActive { get; set; }
        public string IsDiscount { get; set; }
        public string EstateAdress { get; set; }
        public string EstateDescription { get; set; }
        public string EstateLocationId { get; set; }
        public string EstateRegDate { get; set; }
        public string EstateEditDate { get; set; }
    }
}