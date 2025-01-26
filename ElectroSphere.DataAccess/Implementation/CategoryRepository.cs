using ElectroSphere.Entities.Models;
using ElectroSphere.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSphere.DataAccess.Implementation
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var CategortInDb = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if(CategortInDb != null)
            {
                CategortInDb.Name = category.Name;
                CategortInDb.Description = category.Description;
                CategortInDb.CreatedTime = DateTime.Now;
            }
        }
    }
}
