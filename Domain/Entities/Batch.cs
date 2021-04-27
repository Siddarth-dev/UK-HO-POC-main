using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Batch : AuditableEntity
    {
        public Batch()
        {
            BatchAttributes = new HashSet<BatchAttribute>();
        }

        public Guid Id { get; set; }
        public int BusinessUnitId { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public int BatchStatusId { get; set; }
        public virtual BatchStatus BatchStatus { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BatchPublishedDate { get; set; }
        public virtual ICollection<BatchAttribute> BatchAttributes { get; set; }
        public virtual Acl Acl { get; set; }
        public bool IsActive { get; set; }
    }
}