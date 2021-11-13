using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CactusDAL.Models
{
    public class BaseEntity : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
    }
}
