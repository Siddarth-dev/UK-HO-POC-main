namespace Domain.Entities
{
    public class ReadGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual Acl Acl { get; set; }
    }
}