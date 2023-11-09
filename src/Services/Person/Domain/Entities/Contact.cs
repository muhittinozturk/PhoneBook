using Domain.Enums;

namespace Domain.Entities
{
    public class Contact : BaseEntity
    {
        public ContactType Type { get; set; }
        public string? Content { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
