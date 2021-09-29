using System;

namespace Data_Access_Layer.Models
{
    public class DatedEntity : BaseEntity
    {
        // TODO: set it automatically to current time at creation
        public DateTime CreationDate { get; set; }
    }
}
