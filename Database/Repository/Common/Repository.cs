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

        public static List<TEntity> GetAll()
        {
            return DbContext;
        }

        public int GetEntriesCount()
        {
            return DbContext.Count;
        }

        public TEntity GetById(TId id)
        {
            foreach (TEntity entry in DbContext)
            {
                if (Equals(entry.Id, id))
                {
                    return entry;
                }
            }
            return default(TEntity);
        }

        public  void Delete(TEntity entry)
        {
            DbContext.Remove(entry);
        }

        public TEntity Update(TId id, TEntity newEntry)
        {
            TEntity entry = GetById(id);
            newEntry.Id = entry.Id;
            newEntry.CreatedAt = entry.CreatedAt;
            entry = newEntry;
            return entry;
        }








    }
}
