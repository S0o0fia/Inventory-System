using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventory_System.DataBaseLayers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Windows.Controls;
using Inventory_System;
using Inventory_System.EF_Classes;

namespace Inventory_System_Tests
{

    [TestFixture]
    public class Inventory_System
    {
       using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

using Inventory_System.controller;
using Inventory_System;
using StoreSystem;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
    {

        cat_BI c = new cat_BI();
        Context context = new Context();
        Catogery cat = new Catogery();
        [Test]
        public void txtCat_notEmpty()
        {
           
            string s = "";
            Assert.That(() => c.AddCat(s), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        public void txtCat_DontAceept_Sympols()
        {
            string s = "@#$";
            Assert.That(() => c.AddCat(s), Throws.Exception);
        }
        public void txtCat_Accept_arabic()
        {
            string s = "احمد";
            Assert.That(() => c.AddCat(s),Throws.Exception );
        }
        public void txtCat_DontAccept_numbers()
        {
            string s = "a7med";
            Assert.That(() => c.AddCat(s), Throws.Exception);
        }
       [Test]
        public void NullList_ReturnEmptyIfThereIsNoItems()
        {
            ItemLayer item = new ItemLayer();
            int count = item.GetItems().Count;
            NUnit.Framework.Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void ListhasValue_ReturnListIfHasItems()
        {
            ItemLayer item = new ItemLayer();
            int count = item.GetItems().Count;
            NUnit.Framework.Assert.That(count, Is.GreaterThan(0));
        
       }
       [Test]
        public void IncreaseQuantity_NegativeQuantity_ThrowException()
        {
            ItemLayer iteml = new ItemLayer();
            Item t = new Item();
            Assert.That(() => iteml.IncreaseQuantity(t, -5), Throws.Exception);

        }
        [Test]
        public void IncreaseQuantity_Normal_IncreaseQuantity()
        {
            ItemLayer iteml = new ItemLayer();
            Item t = iteml.GetItem(19);
            int before = t.Quantity;
            iteml.IncreaseQuantity(t,5);
            Assert.That(t.Quantity,Is.EqualTo(before+5));

        }

    }
}



    }

}
