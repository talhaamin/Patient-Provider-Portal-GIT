using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public class BillRateDataModel
    {
        public bool Valid;
        public string ErrorMessage;
        public int BillRateId { get; set; }
        public string PromoCode{get;set;}
        public bool Active{get;set;}
        public bool IsFree{get;set;}
        public decimal Rate{get;set;}
        public decimal Renewal{get;set;}
        public decimal Residual{get;set;}
        public int FreeMonths{get;set;}
        public Int64 UserId_Created { get; set; }
        public string DateCreated { get; set; }
        public Int64 UserId_Modified { get; set; }
        public string DateModified{ get; set; }
    }
}
