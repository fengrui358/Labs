using System.Collections.Generic;

namespace ContextLab.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public virtual List<RecordOfSale> SaleHistory { get; set; }
    }
}
