using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaseVehicle.Models
{
    public class VMVehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VMVehicle()
        {
            this.VehicleImages = new HashSet<VehicleImage>();
        }

        public int VehicleId { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public string VehicleNo { get; set; }
        [Required]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int Euronum { get; set; }
        [Required]
        public string Kilometers { get; set; }
        [Required]
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Enter a valid 4 digit Year")]
        public int RegistrationYear { get; set; }
        [Required]
        public double Price { get; set; }
         [Required]
        public int Weight { get; set; }
        [Required]
        public int NoOfSeats { get; set; }
        [Required]
        public bool IsSold { get; set; }
        [Required]
        public System.DateTime SoldDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDelete { get; set; }
   
        public System.DateTime CreatedDate { get; set; }
       
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        [Required]
        public int VehicleImgId { get; set; }

        

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Color Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleImage> VehicleImages { get; set; }


        public string VehicleImgName { get; set; }
       

        public virtual VehicleDetail VehicleDetail { get; set; }
        public HttpPostedFileBase[] VehicleImage1 { get; set; }
    }
}