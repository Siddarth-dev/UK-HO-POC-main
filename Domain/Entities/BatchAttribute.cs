using System;

namespace Domain.Entities
{
    public class BatchAttribute
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid BatchId { get; set; }
        public virtual Batch Batch { get; set; }
        public bool IsActive { get; set; }
    }
}