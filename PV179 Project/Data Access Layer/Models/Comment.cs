using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    class Comment : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public User AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }
    }
}
