using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Carhistory
    {
        public int RecordOfSaleId { get; set; }
        public DateTime DateSold { get; set; }
        public decimal Price { get; set; }
        public string CarLicensePlate { get; set; }

        public virtual Car CarLicensePlateNavigation { get; set; }
    }
}
