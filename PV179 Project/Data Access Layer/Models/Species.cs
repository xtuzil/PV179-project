using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class Species : BaseEntity
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

        public DateTime SuggestionDate { get; set; }
        public DateTime ConfirmationDate { get; set; }

        public int SuggestedById { get; set; }
        [ForeignKey(nameof(SuggestedById))]
        public User SuggestedBy { get; set; }

        public int ConfirmedById { get; set; }
        [ForeignKey(nameof(ConfirmedById))]
        public User ConfirmedBy { get; set; }
    }
}
