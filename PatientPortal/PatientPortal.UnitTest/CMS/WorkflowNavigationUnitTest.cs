﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientPortal.DataAccess.CMS;
using PatientPortal.DataAccess.Repo.CMS;
using PatientPortal.Domain.Models.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.UnitTest.CMS
{
    [TestClass]
    public class WorkflowNavigationUnitTest
    {
        public IWorkflowNavigation _workflowNavigationImlp;
        private IWorkflowState _workflowStateImpl;
        private IWorkflow _workflowImpl;

        [TestInitialize]
        public void TestInitialize()
        {
            //_workflowNavigationImlp = new WorkflowNavigationImpl();
            //_workflowStateImpl = new WorkflowStateImpl();
            //_workflowImpl = new WorkflowImpl();
        }

        [TestMethod]
        public void WorkflowNavigationGetAll()
        {
            //var count = _workflowNavigationImlp.GetAll().Count();
            //Assert.IsTrue(count >= 0);
        }

        //private WorkflowState WorkflowStateInsert()
        //{
        //    //var listworkflow = _workflowImpl.GetAll();
        //    //var wf = new Workflow();
        //    //if (listworkflow.Count == 0)
        //    //{
        //    //    wf = new Workflow { Name = "Workflow" };
        //    //    listworkflow.Add(wf);
        //    //    _workflowImpl.Transaction(listworkflow, 'I');
        //    //};
        //    //wf = _workflowImpl.GetAll().LastOrDefault();

        //    //var workflowStateList = new List<WorkflowStateEdit>() {
        //    //    new WorkflowStateEdit
        //    //    {
        //    //        Id = 0,
        //    //        Name = "WFSt",
        //    //        IsActive = true,
        //    //        WorkflowId = wf.Id
        //    //    }
        //    //};
        //    //_workflowStateImpl.Transaction(workflowStateList, 'I');
        //    //return _workflowStateImpl.GetAll().LastOrDefault();
        //}

        [TestMethod]
        public void WorkflowNavigationInsert()
        {
            //var workflowStateList = _workflowStateImpl.GetAll();
            //var list = new List<WorkflowNavigationEdit>();

            //var wfst = new WorkflowNavigationEdit
            //{
            //    WorkflowStateId = WorkflowStateInsert().Id,
            //    NextWorkflowStateId = WorkflowStateInsert().Id
            //};
            //list.Add(wfst);
            //var flag = _workflowNavigationImlp.Transaction(list, 'I');
            //Assert.IsTrue(flag)
;        }
    }
}