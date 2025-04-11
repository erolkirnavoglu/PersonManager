using System.ComponentModel.DataAnnotations;

namespace PersonManager.UI.Models
{
    public class PersonPost
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Company { get; set; }
    }
}
