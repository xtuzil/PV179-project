using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
