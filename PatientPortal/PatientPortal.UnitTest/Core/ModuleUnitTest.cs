﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.DataAccess.CORE;
using System.Collections.Generic;
using PatientPortal.DataAccess.Repo.CORE;

namespace PatientPortal.UnitTest.Core
{
    [TestClass]
    public class ModuleUnitTest
    {
        public IModule _moduleImpl;
        [TestInitialize]
        public void TestInitialize()
        {
            //_moduleImpl = new ModuleImpl();
        }

        //[TestMethod]
        //public void ModuleGetAll()
        //{
        //    var count = _moduleImpl.Query().Count;
        //    Assert.IsTrue(count > 0);
        //}

        //[TestMethod]
        //public void ModuleInsert()
        //{
        //    var list = new List<Module>() {
        //        new Module {
        //            Title = "Module 1",
        //            Handler = "Handler 1",
        //            Sort = 1,
        //            ParentId = 1,
        //            GroupId = 1
        //        }
        //    };

        //    var flag = _moduleImpl.Transaction(list, 'I');
        //    Assert.IsTrue(flag);
        //}
    }
}
