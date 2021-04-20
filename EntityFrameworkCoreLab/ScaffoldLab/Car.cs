using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Car
    {
        public Car()
        {
            Carhistories = new HashSet<Carhistory>();
        }

        public int CarId { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public virtual ICollection<Carhistory> Carhistories { get; set; }
    }
}
