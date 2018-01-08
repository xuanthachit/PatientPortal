﻿using AutoMapper;
using PatientPortal.API.Core.Models;
using PatientPortal.Domain;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.IRepository.CORE;
using PatientPortal.IRepository.Authorize;
using PatientPortal.Provider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using static PatientPortal.Domain.EnumUserConstants;

namespace PatientPortal.API.Core.Controllers
{
    [AuthorizeRoles]
    public class UserController : ApiController
    {
        private readonly IUserRepo _user;

        #region Constructor
        public UserController(IUserRepo user)
        {
            this._user = user;
        }
        #endregion

        [HttpGet]
        public async Task<List<UserViewModel>> Get(string search, UserType type = EnumUserConstants.UserType.ISDOCTOR)
        {
            IList<string> list = new List<string> { "search", "type" };
            var para = APIProvider.APIDefaultParameter(list, search, (byte)type);

            var source = await _user.Query(para);
            List<UserViewModel> dest = Mapper.Map<List<UserViewModel>>(source);

            return dest;
        }

        [HttpGet]
        [Route("api/User/GetUserHaveScheduleExamine")]
        public async Task<List<UserViewModel>> GetUserHaveScheduleExamine(string search)
        {
            IList<string> list = new List<string> { "search"};
            var para = APIProvider.APIDefaultParameter(list, search);

            var source = await _user.GetUserHaveScheduleExamine(para);
            List<UserViewModel> dest = Mapper.Map<List<UserViewModel>>(source);

            return dest;
        }

        [HttpGet]
        public async Task<UserViewModel> Get(string id)
        {
            //Dictionary<string, dynamic> para = new Dictionary<string, dynamic>();
            //para.Add("Id", Id);
            //para.Add("Search", "");
            IList<string> list = new List<string> { "Id", "Search" };
            var para = APIProvider.APIDefaultParameter(list, id, "");
            var source = await _user.SingleQuery(para);
            UserViewModel dest = Mapper.Map<UserViewModel>(source);

            return dest;
        }

        [HttpPost]
        public async Task<int> Transaction(UserViewModel userModel, char action)
        {
            var data = Mapper.Map<User>(userModel);
            return await _user.Transaction(data, action);
        }

        [HttpGet]
        public async Task<string> GetUserId(string email)
        {
            IList<string> list = new List<string> { "email" };
            var para = APIProvider.APIDefaultParameter(list, email);
            var source = await _user.GetUserId(para);
            return source;
        }
    }
}