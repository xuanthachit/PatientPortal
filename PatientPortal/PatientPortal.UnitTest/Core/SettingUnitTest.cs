﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.DataAccess.CORE;
using System.Collections.Generic;
using System.Linq;
using PatientPortal.DataAccess.Repo.CORE;

namespace PatientPortal.UnitTest.Core
{
    [TestClass]
    public class SettingUnitTest
    {
        public ISetting _settingImpl;
        [TestInitialize]
        public void TestInitialize()
        {
           // _settingImpl = new SettingImpl();
        }

        //[TestMethod]
        //public void SettingGetAll()
        //{
        //    var count = _settingImpl.Query().Count;
        //    Assert.IsTrue(count > 0);
        //}

        //[TestMethod]
        //public void SettingInsert()
        //{
        //    var list = new List<Setting>() {
        //        new Setting {
        //            Title = "setting",
        //            Description = "",
        //            Keyword = "",
        //            Membership = true,
        //            DefaultRole = 1,
        //            LoginURL = ""
        //        }
        //    };
        //    var flag = _settingImpl.Transaction(list, 'I');
        //    Assert.IsTrue(flag);
        //}

        //[TestMethod]
        //public void SettingUpdate()
        //{
        //    var list = _settingImpl.Query();
        //    var listUpdate = new List<Setting>();
        //    if (list.Count > 0)
        //    {
        //        listUpdate.Add(list.LastOrDefault());
        //    }
        //    else
        //    {
        //        var listInsert = new List<Setting>() {
        //            new Setting {
        //                Title = "setting1",
        //                Description = "",
        //                Keyword = "",
        //                Membership = true,
        //                DefaultRole = 1,
        //                LoginURL = ""
        //            }
        //        };
        //        _settingImpl.Transaction(listInsert, 'I');
        //        list = _settingImpl.Query();
        //        listUpdate.Add(list.LastOrDefault());
        //    }
            
        //    var flag = _settingImpl.Transaction(listUpdate, 'U');
        //    Assert.IsTrue(flag);
        //}

        //[TestMethod]
        //public void SettingDelete()
        //{
        //    var list = _settingImpl.Query();
        //    var listUpdate = new List<Setting>();
        //    if (list.Count > 0)
        //    {
        //        listUpdate.Add(list.LastOrDefault());
        //    }
        //    else
        //    {
        //        var listInsert = new List<Setting>() {
        //            new Setting {
        //                Title = "setting1",
        //                Description = "",
        //                Keyword = "",
        //                Membership = true,
        //                DefaultRole = 1,
        //                LoginURL = ""
        //            }
        //        };
        //        _settingImpl.Transaction(listInsert, 'I');
        //        list = _settingImpl.Query();
        //        listUpdate.Add(list.LastOrDefault());
        //    }

        //    var flag = _settingImpl.Transaction(listUpdate, 'D');
        //    Assert.IsTrue(flag);
        //}
    }
}
