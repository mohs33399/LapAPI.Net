using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Linq.Expressions;

namespace School.Manegers
{
    public class BaseManager <T> where T : class
    {
        private readonly SchoolDbContext context;
        private  DbSet<T> table;

        public BaseManager(SchoolDbContext _schoolDbContext)
        {
            context = _schoolDbContext;
            table =context.Set<T>();
     
;
            
        }

        public IQueryable<T>Get(
            Expression<Func<T,bool>> filter= null,
            int pageSize =4, 
            int pageNumber = 1)
        {


            IQueryable<T> quary = table.AsQueryable();
            
            if(filter!=null) 
            quary = quary.Where(filter);


            if (pageSize < 0)
                pageSize = 4;

            if (pageNumber < 0)
                pageNumber = 1;


            int count = quary.Count();

            if (count < pageSize)
            {
                pageSize = count;
                pageNumber = 1;
            }

            int ToSkip = (pageNumber - 1) * pageSize;

            quary = quary.Skip(ToSkip).Take(pageSize);

            return quary;


        }
    
        public IQueryable<T> GetList(
        Expression<Func<T,bool>>filter= null)
        {
        IQueryable<T> query = table.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query;
    }
        public void Add(T newRow) 
        {
                table.Add(newRow);
                this.context.SaveChanges();

        }
       public void Edit(T newRow)
       {
            table.Update(newRow);
            this.context.SaveChanges();

       }

        public void Delete(T row)
       {
            table.Remove(row);
            this.context.SaveChanges();

       }




    }
}
