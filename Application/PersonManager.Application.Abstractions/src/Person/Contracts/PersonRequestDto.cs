using System.ComponentModel.DataAnnotations;

namespace PersonManager.Application.Abstractions.Person.Contracts
{
    public class PersonRequestDto
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
