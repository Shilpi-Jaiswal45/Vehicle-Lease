using LeaseVehicle.Models;

namespace LeaseVehicle.Areas.User.Controllers
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int QuoteId { get; set; }
        public int StatusId { get; set; }

        public virtual Quote Quote { get; set; }
        public virtual Status Status { get; set; }
    }
}