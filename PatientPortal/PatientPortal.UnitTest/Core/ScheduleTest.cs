﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientPortal.DataAccess.CORE;
using PatientPortal.DataAccess.Repo.CORE;
using PatientPortal.DataAccess.Repo.Wrapper;
using PatientPortal.DataAccess.Wrapper;
using PatientPortal.Domain.Models.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.UnitTest.Core
{
    [TestClass]
    public class ScheduleTest
    {
        private ISchedule _schedule;
        private IGroup _group;
        private IAdapterPattern _adapterPattern;

        [TestInitialize]
        public void TestInitialize()
        {
            _adapterPattern = new AdapterPattern();
            _schedule = new ScheduleImpl(_adapterPattern);
            _group = new GroupImpl(_adapterPattern);
        }

        [TestMethod]
        public void InsertSchedule()
        {
            var model = new Schedule()
            {
                Title = "Họp chuyên khoa",
                Priority = 1,
                Detail = "Họp chuyên khoa các giáo sư đầu ngành",
                IsAlarm = true,
                Start = DateTime.Now,
                End = DateTime.Now,
                Color = "GREEN",
                IsExamine = true,
                UserId = "DT001"
            };
            _schedule.Transaction(model, 'I');
            //Assert.IsInstanceOfType(flag, Type.);
        }

        [TestMethod]
        public void InsertGroup()
        {
            var model = new Group()
            {
                Name = "Group1",
                IsReadOnly = true
            };
            _group.Transaction(model, 'I');
        }
    }
}