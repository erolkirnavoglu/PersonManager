namespace PersonManager.Domain.Base
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
