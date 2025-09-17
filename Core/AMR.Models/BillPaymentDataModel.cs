using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
   public  class BillPaymentDataModel
    {
        public bool Valid;
        public string ErrorMessage;
        public Int64 BillPaymentId;
        public Int64 PatientId;
        public int BillRateId;
        public string TransactionDate;
        public string Response;
        public string CustId;
        public string RecurrId;
        public char PaymentType;
        public string PaymentFrequency;
        public string PaymentId;
        public long NoOfPayments;
        public DateTime StartDate;
        public string AccountHolderName;
        public long Amount;
        public Int64 UserId_Created;
        public string DateCreated;
        public Int64 UserId_Modified;
        public string DateModified;
        public bool status = false;
    }
}
