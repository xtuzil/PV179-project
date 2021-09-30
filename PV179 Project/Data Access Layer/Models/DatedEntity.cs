using System;
using System.ComponentModel;

namespace Data_Access_Layer.Models
{
    public class DatedEntity : BaseEntity
    {
        // TODO: set it automatically to current time at creation
        [DefaultValue("getutcdate()")]
        public DateTime CreationDate { get; set; }
    }
}
