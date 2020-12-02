using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiSearch.Models
{
    public class SaveUserSearchRequest
    {
        public string SearchTitle { get; set; }
        public string SearchResponse { get; set; }
        public string IpAddress { get; set; }
    }
}