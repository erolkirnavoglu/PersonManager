namespace PersonManager.Application.Abstractions.Person.Contracts
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }
    }
}
