﻿using System;
using System.ComponentModel;

namespace CactusDAL.Models
{
    public class DatedEntity : BaseEntity
    {
        // TODO: set it automatically to current time at creation
        [DefaultValue("getutcdate()")]
        public DateTime CreationDate { get; set; }
    }
}
