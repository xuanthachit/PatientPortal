﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using PatientPortal.API.Core.Models;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.Domain.Models.CORE.Report;
using PatientPortal.IRepository.CORE;
using PatientPortal.Provider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace PatientPortal.API.Core.Controllers
{
    [AuthorizeRoles]
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentRepo _appointment;

        #region Constructor
        public AppointmentController(IAppointmentRepo appointment)
        {
            this._appointment = appointment;
        }
        #endregion

        #region API function

        /// <summary>
        /// Get all appointment
        /// </summary>
        /// <returns>list of appointment</returns>
        [HttpGet]
        [Route("GetAppointmentConfirm")]
        public async Task<List<RPAppointment>> Get()
        {
            var principalIdentity = RequestContext.Principal.Identity;
            var userId = principalIdentity.GetUserId();
            //var source = new List<RPAppointment>();

            if (userId.Length > 0)
            {
                Dictionary<string, dynamic> para = new Dictionary<string, dynamic>();
                para.Add("UserId", userId);

                return await _appointment.GetRPAppointment(para);
            }
            
            return null;
        }

        
        #endregion
    }
}
