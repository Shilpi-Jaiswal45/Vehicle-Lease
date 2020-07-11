using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaseVehicle.Models
{
    public class VMLead
    {
        public int id { get; set; }
        public string VImage { get; set; }
        public string VName { get; set; }
        public double Price { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<long> DownPayment { get; set; }
        public Nullable<long> TotalAmount { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }

        public bool IsDelete { get; set; }
        [Required]
        public string FirstName { get; internal set; }
        [Required]

       public string LastName { get; internal set; }
        [Required]

        public string Email { get; internal set; }
        [Required]

        public string Address { get; internal set; }
        [Required]

        public string Contact { get; internal set; }

        //public static implicit operator VMLead(vMLead v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
