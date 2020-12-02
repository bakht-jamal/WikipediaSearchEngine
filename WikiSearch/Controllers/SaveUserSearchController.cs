using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WikiSearch.BL;
using WikiSearch.Models;

namespace WikiSearch.Controllers
{
    public class SaveUserSearchController : ApiController
    {
        private readonly UserSearchService userSearchService;
        public SaveUserSearchController()
        {
            userSearchService = new UserSearchService();
        }

        [HttpPost]
        [Route("api/SaveUserSearchData")]
        public async Task<IHttpActionResult> SaveUserSearchData(SaveUserSearchRequest saveUserSearchRequest)
        {
            var saveUserData = await userSearchService.SaveUserSearch(saveUserSearchRequest);

            if (saveUserData != null && saveUserData.Status.ToLower().Equals("success"))
            {
                return Ok(saveUserData);
            }
            else
            {
                return BadRequest(saveUserData?.Message.ToString());
            }
        }
    }
}
