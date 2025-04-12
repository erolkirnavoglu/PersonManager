using PersonManager.Common.Enums;

namespace PersonManager.Application.Abstractions.PersonInfo.Contracts
{
    public class PersonInfoRequestDto
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public ContactType ContactType { get; set; }

        public string Content { get; set; }
    }
}
