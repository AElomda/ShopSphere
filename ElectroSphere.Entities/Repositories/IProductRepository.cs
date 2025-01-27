using ElectroSphere.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSphere.Entities.Repositories
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        void Update(Product product);
    }
}
