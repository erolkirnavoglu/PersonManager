using PersonManager.Domain.Base;

namespace PersonManager.Domain
{
    public class Person : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<PersonInfo> PersonInfos { get; set; }
        
    }
}
