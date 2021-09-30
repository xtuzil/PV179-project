using System.ComponentModel.DataAnnotations;

namespace CactusDAL.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
