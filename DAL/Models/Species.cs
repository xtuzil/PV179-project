﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Species : DatedEntity
    {
        public string Name { get; set; }
        public string LatinName { get; set; }

        public int GenusId { get; set; }

        [ForeignKey(nameof(GenusId))]
        public virtual Genus Genus { get; set; }


        // TODO: characteristics of the species
        // e.g. description, origin, shape, size, spike length,
        // type of flower (if it has flower at all), use (edible, healing…)


        public IEnumerable<Cactus> Instances { get; set; }

        public DateTime ConfirmationDate { get; set; }

        // @ optional feature
        public IEnumerable<User> WishlistedBy { get; set; }
        public bool Approved { get; set; }
    }
}
