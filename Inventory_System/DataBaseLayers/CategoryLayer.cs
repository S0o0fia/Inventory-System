using Inventory_System.EF_Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System.DataBaseLayers
{
    public class CategoryLayer : Base
    {
        public IQueryable<Catogery> GetAllCategories()
        {
            var Categories = context.Catogerys;
            return Categories;
        }

        public List<Item> GetAllItemsinCategory(int cat_id)
        {
            if (cat_id < 0)
                throw new Exception("Invalid ID");
            else
            {
                var query = context.Items.Where(c => c.Cat_Id == cat_id).ToList();
                if (query.Count == 0)
                    throw new Exception("Empty List");
                return query;
            }
        }
    }
}
