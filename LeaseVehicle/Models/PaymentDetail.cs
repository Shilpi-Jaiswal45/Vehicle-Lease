//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaseVehicle.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentDetail
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public string TransactionId { get; set; }
        public int Amount { get; set; }
        public string PaymentMode { get; set; }
        public int StatusId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string StatusResponse { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
    
        public virtual Quote Quote { get; set; }
        public virtual Status Status { get; set; }
    }
}