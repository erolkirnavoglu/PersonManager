using PersonManager.Common.Enums;

namespace PersonManager.UI.Models
{
    public class PersonInfoModel
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public ContactType ContactType { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
