﻿using Inventory_System.EF_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System.DataBaseLayers
{
    public class SupplierLayer : Base
    {
       
        public IQueryable<Supplier> GetAllSupplier()
        {
            var sup = context.Suppliers;
            if (sup.ToList().Count == 0)
                return null;
            return sup;
        }
    }
}
