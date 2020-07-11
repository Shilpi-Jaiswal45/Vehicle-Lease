using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseVehicle.Models
{
  public class VMQuote
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
    public string FName { get; set; }
    public int vehicleId { get; set; }
    public int userId { get; set; }
    public int statusId { get; set; }
    public int docStatusId { get; set; }
    public string docStatusName { get; set; }
    public  Status Status1 { get; set; }
        public ICollection<Document> Documents { get; set; }

        
    }
}
