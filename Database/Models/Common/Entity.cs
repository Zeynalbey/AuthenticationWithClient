using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Common
{
    public abstract class Entity <T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
