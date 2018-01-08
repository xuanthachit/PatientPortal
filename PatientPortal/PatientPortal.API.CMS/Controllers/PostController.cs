﻿using AutoMapper;
using PatientPortal.API.CMS.Models;
using PatientPortal.API.CMS.ViewModels;
using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.CMS.Report;
using PatientPortal.Domain.Utilities;
using PatientPortal.IRepository.CMS;
using PatientPortal.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PatientPortal.API.CMS.Controllers
{
    [AuthorizeRoles]
    public class PostController : ApiController
    {
        /// <summary>
        /// Declare variable
        /// </summary>
        /// 
        IPostRepo _postRepo;

        /// <summary>
        /// Constructor PostController
        /// </summary>
        /// <param name="postRepo"></param>
        public PostController(IPostRepo postRepo)
        {
            this._postRepo = postRepo;
        }

        #region API function

        [Route("api/Post/Counter")]
        [HttpGet]
        // GET: api/Post/GetAll
        public async Task<IEnumerable<DashboardCounter>> Counter()
        {
            var data = await _postRepo.Counter(null);
            return data;
        }

        [Route("api/Post/GetAll")]
        [HttpGet]
        // GET: api/Post/GetAll
        public async Task<IEnumerable<PostListViewModel>> GetAll([FromUri] PostFilterViewModel param)
        {
            param.Id = 0;

            var para = APIProvider.APIDefaultParameter<PostFilterViewModel>(param);
            var data = await _postRepo.Query(para);

            var results = Mapper.Map<List<PostListViewModel>>(data);

            return results;
        }

        [Route("api/Post/GetAccess")]
        [HttpGet]
        // GET: api/Post/GetAccess
        public async Task<IEnumerable<AccessPostViewModel>> GetAccess(int id)
        {
            List<string> list = new List<string> { "Id"};
            var para = APIProvider.APIDefaultParameter(list, id);

            var data = await _postRepo.QueryAccess(para);


            var results = Mapper.Map<List<AccessPostViewModel>>(data);

            return results;
        }

        // GET: api/Post/5
        [HttpGet]
        public async Task<PostViewModel> GetById(int id)
        {
            List<string> list = new List<string> {"Id", "LanguageCode", "WorkflowStateId" };
            var para = APIProvider.APIDefaultParameter(list, id, string.Empty, 0);

            var data = await _postRepo.SingleQuery(para);
            var result = Mapper.Map<PostViewModel>(data);

            return result;
        }

        // POST: api/Post
        [HttpPost]
        [Route("api/Post/PostTrans")]
        public async Task<int> Transaction(PostTransViewModel data, char action)
        {
            try
            {
                var obj = Mapper.Map<Domain.Models.CMS.PostEdit>(data);
                var result = await _postRepo.Transaction(obj, action);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        // POST: api/Approve
        [HttpPost]
        [Route("api/Post/Approve")]
        public async Task<bool> Approve(PostStateHistoryViewModel data, char action)
        {
            try
            {
                var obj = Mapper.Map<Domain.Models.CMS.PostStateHistory>(data);
                var result = await _postRepo.Transaction(obj, action);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        // api/Publish
        [HttpPost]
        [Route("api/Post/Publish")]
        public async Task<bool> PublishPost(PostStateHistoryViewModel data, char action)
        {
            try
            {
                var obj = Mapper.Map<Domain.Models.CMS.PostStateHistory>(data);
                var result = await _postRepo.Transaction(obj, action);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        #region Post trans
        [HttpGet]
        [Route("api/Post/PostTranBy")]
        public async Task<PostTranViewModel> PostTranBy(int postId, byte languageId)
        {
            try
            {
                var result = new PostTranViewModel();
                List<string> list = new List<string> { "PostId", "LanguageId" };
                var para = APIProvider.APIDefaultParameter(list, postId, languageId);

                var data = await _postRepo.SingleQueryPostTran(para);
                if(data != null)
                {
                    result = Mapper.Map<PostTranViewModel>(data);
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("api/Post/PostTransUpdate")]
        public async Task<bool> PostTransUpdate(PostTranViewModel data, char action)
        {
            try
            {
                var obj = Mapper.Map<Domain.Models.CMS.PostTran>(data);
                var result = await _postRepo.Transaction(obj, action);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }
        #endregion


        #endregion
    }
}