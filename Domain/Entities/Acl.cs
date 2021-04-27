using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Acl
    {
        public Acl()
        {
            ReadGroups = new HashSet<ReadGroup>();
            ReadUsers = new HashSet<ReadUser>();
        }

        public int Id { get; set; }
        public virtual ICollection<ReadUser> ReadUsers { get; set; }
        public virtual ICollection<ReadGroup> ReadGroups { get; set; }
        public Guid BatchId { get; set; }
        public virtual Batch Batch { get; set; }
        public bool IsActive { get; set; }
    }
}