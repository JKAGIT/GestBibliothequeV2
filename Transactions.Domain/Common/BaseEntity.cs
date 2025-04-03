using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    }
}
