namespace Domain.Entities
{
    public class ReadUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public virtual Acl Acl { get; set; }
    }
}