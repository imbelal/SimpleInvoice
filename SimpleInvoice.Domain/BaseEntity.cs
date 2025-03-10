namespace SimpleInvoice.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }

        public BaseEntity()
        {

        }

        public BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
