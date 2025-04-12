using PersonManager.Common.Enums;

namespace PersonManager.UI.Models
{
    public class PersonInfoPost
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public ContactType ContactType { get; set; }

        public string Content { get; set; }
    }
}
