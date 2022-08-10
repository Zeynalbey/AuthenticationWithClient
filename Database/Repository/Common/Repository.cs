using AuthenticationWithClie.Database.Common;
using AuthenticationWithClie.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Repository
{
    public class Repository <TEntity, TId>
        where TEntity : Entity<TId>
    {
        protected static List<TEntity> DbContext { get; set; } = new List<TEntity>();

        public static TEntity Add(TEntity entry)
        {
            DbContext.Add(entry);
            return entry;
        }

  
    }
}
