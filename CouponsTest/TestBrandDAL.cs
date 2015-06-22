using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coupons.DAL;
using Coupons.Models;
using System.Collections.Generic;

namespace CouponsTest
{
    [TestClass]
    public class TestBrandDAL
    {
        [TestMethod]
        public void TestGetAllbrands()
        {
            BrandDAL target = new BrandDAL();

            List<Brand> expectedOutput = new List<Brand>();
            Brand aroma = new Brand(1, "Aroma");
            Brand nike = new Brand(2, "Nike");
            expectedOutput.Add(aroma);
            expectedOutput.Add(nike);

            List<Brand> actualOutput = target.GetAllbrands();

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);

            BrandComparer brandComparer = new BrandComparer();
            expectedOutput.Sort(brandComparer);
            actualOutput.Sort(brandComparer);

            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i], actualOutput[i]);
            }
        }

        private class BrandComparer : IComparer<Brand>
        {
            public int Compare(Brand a, Brand b)
            {
                return a.ID.CompareTo(b.ID);

            }
        }
    }
}
