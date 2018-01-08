﻿using AutoMapper;
using PatientPortal.API.Core.Models;
using PatientPortal.Domain.Common;
using PatientPortal.IRepository.CORE;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.Provider.Models;

namespace PatientPortal.API.Core.Controllers
{
    [AuthorizeRoles]
    public class ScheduleController : ApiController
    {
        private readonly IScheduleRepo _scheduleRepo;

        public ScheduleController(IScheduleRepo scheduleRepo)
        {
            this._scheduleRepo = scheduleRepo;
        }

        [HttpGet]
        public async Task<List<ScheduleViewModel>> GetSchedule([FromUri]ScheduleFilter param)
        {
            IList<string> list = new List<string> { "id", "userId", "start", "end" };
            var para = APIProvider.APIDefaultParameter(list, 0, param.UserId, param.Start, param.End);

            var source = await _scheduleRepo.Query(para);
            var dest = Mapper.Map<List<ScheduleViewModel>>(source);

            return dest;
        }

        [HttpPost]
        public async Task<int> Transaction(ScheduleDetailViewModel data, char action)
        {
            var param = Mapper.Map<Schedule>(data);
            var result = await _scheduleRepo.Transaction(param, action);
            return result;
        }

        [HttpGet]
        public async Task<ScheduleDetailViewModel> GetScheduleDetail(int id)
        {
            IList<string> list = new List<string> { "id" };
            var para = APIProvider.APIDefaultParameter(list, id);

            var source = await _scheduleRepo.SingleQuery(para);
            var dest = Mapper.Map<ScheduleDetailViewModel>(source);

            return dest;
        }
    }
}
