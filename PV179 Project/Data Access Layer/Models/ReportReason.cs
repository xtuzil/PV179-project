using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class ReportReason : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Report> Reports { get; set; }
    }
}