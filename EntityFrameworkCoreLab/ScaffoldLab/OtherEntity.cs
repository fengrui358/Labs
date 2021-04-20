using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class OtherEntity
    {
        public Guid Id { get; set; }
        public string ChangeNewColumnName { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastUpdatedDateTimeOffset { get; set; }
    }
}
