﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.DataAccess.CORE;
using System.Collections.Generic;
using System.Linq;
using PatientPortal.DataAccess.Repo.CORE;

namespace PatientPortal.UnitTest.Core
{
    [TestClass]
    public class RoleUnitTest
    {
        public IRole _roleImpl;
        [TestInitialize]
        public void TestInitialize()
        {
            //_roleImpl = new RoleImpl();
        }

        //[TestMethod]
        //public void RoleGetAll()
        //{
        //    var count = _roleImpl.Query().Count;
        //    Assert.IsTrue(count >= 0);
        //}

        //[TestMethod]
        //public void RoleInsert()
        //{
        //    var list = new List<Role>()
        //    {
        //        new Role
        //        {
        //            Name = "role 1"
        //        }
        //    };
        //    var flag = _roleImpl.Transaction(list, 'I');
        //    Assert.IsTrue(flag);
        //}

        //[TestMethod]
        //public void RoleUpdate()
        //{
        //    var listRole = _roleImpl.Query();
        //    var list = new List<Role>();
        //    if (listRole.Count > 0)
        //    {
        //        list.Add(listRole.LastOrDefault());
        //    }
        //    else
        //    {
        //        var listInsert = new List<Role>()
        //        {
        //            new Role
        //            {
        //                Name = "role 1"
        //            }
        //        };
        //        _roleImpl.Transaction(listInsert, 'I');
        //        listRole = _roleImpl.Query();
        //        list.Add(listRole.LastOrDefault());
        //    }
        //    var flag = _roleImpl.Transaction(list, 'U');
        //    Assert.IsTrue(flag);
        //}

        //[TestMethod]
        //public void RoleDelete()
        //{
        //    var listRole = _roleImpl.Query();
        //    var list = new List<Role>();
        //    if (listRole.Count > 0)
        //    {
        //        list.Add(listRole.LastOrDefault());
        //    }
        //    else
        //    {
        //        var listInsert = new List<Role>()
        //        {
        //            new Role
        //            {
        //                Name = "role 1"
        //            }
        //        };
        //        _roleImpl.Transaction(listInsert, 'I');
        //        listRole = _roleImpl.Query();
        //        list.Add(listRole.LastOrDefault());
        //    }
        //    var flag = _roleImpl.Transaction(list, 'D');
        //    Assert.IsTrue(flag);
        //}
    }
}
