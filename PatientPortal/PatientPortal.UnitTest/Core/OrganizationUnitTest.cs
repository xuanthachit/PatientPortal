﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.DataAccess.CORE;
using System.Collections.Generic;
using PatientPortal.DataAccess.Repo.CORE;

namespace PatientPortal.UnitTest.Core
{
    [TestClass]
    public class OrganizationUnitTest
    {
        public IOrganization _organizationImpl;
        [TestInitialize]
        public void TestInitialize()
        {
            //_organizationImpl = new OrganizationImpl();
        }

        //[TestMethod]
        //public void OrganizationGetAll()
        //{
        //    var count = _organizationImpl.Query().Count;
        //    Assert.IsTrue(count >= 0);
        //}

        //[TestMethod]
        //public void OrganizationInsert()
        //{
        //    var list = new List<Organization>()
        //    {
        //        new Organization {
        //            ParentId =  1,
        //            Name = "n",
        //            Phone = "",
        //            Fax="",
        //            Email = "",
        //            Address = ""
        //        }
        //    };
        //    var flag = _organizationImpl.Transaction(list, 'I');
        //    Assert.IsTrue(flag);
        //}
    }
}
