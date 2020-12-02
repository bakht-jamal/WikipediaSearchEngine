using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WikiSearch.DAL;
using WikiSearch.Models;

namespace WikiSearch.BL
{
    public class UserSearchService
    {
        private readonly ConnectionLogic connectionLogic;
        public UserSearchService()
        {
            connectionLogic = new ConnectionLogic();
        }

        public async Task<SaveUserSearchResponse> SaveUserSearch(SaveUserSearchRequest saveUserSearch)
        {
            try
            {
                SaveUserSearchResponse saveUserSearchResponse = new SaveUserSearchResponse();
                string spName = @"WIKI_SAVE_SEARCHES";
                Hashtable Param = new Hashtable
                {
                    { "@SearchTitle", string.IsNullOrEmpty(saveUserSearch.SearchTitle) ? "Invalid Search Title" : saveUserSearch.SearchTitle },
                    { "@SearchResponse", string.IsNullOrEmpty(saveUserSearch.SearchResponse) ? "Invalid Search Response" : saveUserSearch.SearchResponse },
                    { "@IpAddress", string.IsNullOrEmpty(saveUserSearch.IpAddress) ? "Invalid IP Address" : saveUserSearch.IpAddress }
                    
                };

                DataTable dtResponse = await connectionLogic.ExecuteQuery(spName, Param);

                if (dtResponse.Rows.Count > 0)
                {
                    saveUserSearchResponse = new SaveUserSearchResponse
                    {
                        Status = dtResponse.Rows[0]["Status"].ToString(),
                        Message = dtResponse.Rows[0]["Message"].ToString()
                    };
                }

                return saveUserSearchResponse;

            }
            catch (Exception ex)
            {
                return new SaveUserSearchResponse
                {
                    Status = "Exception",
                    Message = ex.Message.ToString()
                };
            }
        }
    }
}