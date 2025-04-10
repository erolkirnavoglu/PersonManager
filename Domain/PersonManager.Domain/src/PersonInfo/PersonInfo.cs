using PersonManager.Common.Enums;
using PersonManager.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManager.Domain
{
    public class PersonInfo:IEntity
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public ContactType ContactType { get; set; }

        public string Content { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}
