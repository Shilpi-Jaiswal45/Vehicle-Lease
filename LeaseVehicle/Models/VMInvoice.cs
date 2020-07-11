using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseVehicle.Models
{
    public class VMInvoice
    {

        public int QID { get; set; }
        public string VImage { get; set; }
        public string VName { get; set; }
        public double Price { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<long> DownPayment { get; set; }
        public Nullable<long> TotalAmount { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public string FName { get; set; }
        public int vehicleId { get; set; }
        public int userId { get; set; }
        public int statusId { get; set; }

        public Status Status1 { get; set; }
        public ICollection<Document> Documents { get; set; }



        public int PId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public int QuoteId { get; set; }
        public string TransactionId { get; set; }
        public int Amount { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }

        public virtual Quote Quote { get; set; }
        
        public virtual UserDetail UserDetail { get; set; }
        public virtual List<PaymentDetail> paymentDetails { get; set; }
    }
}