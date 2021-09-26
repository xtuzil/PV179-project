using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Review : BaseEntity
    {
        public DateTime Date { get; set; }
        public string ReviewText { get; set; }
        public double ReviewScore { get; set; }
        public User SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public virtual User Seller { get; set; }
        public User AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }



    }
}
