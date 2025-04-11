namespace PersonManager.UI.Models
{
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
