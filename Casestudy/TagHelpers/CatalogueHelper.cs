﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Http;
using System.Text;
using Casestudy.Models;
using Newtonsoft.Json;
using Casestudy.Utils;
using Microsoft.Extensions.Primitives;

namespace Casestudy.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("products", Attributes = BrandIdAttribute)]
    public class CatalogueHelper : TagHelper
    {
        private const string BrandIdAttribute = "brand";
        [HtmlAttributeName(BrandIdAttribute)]
        public string BrandId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CatalogueHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_session.Get<ProductViewModel[]>(SessionVars.Products) != null && Convert.ToInt32(BrandId) > 0)
            {
                var innerHtml = new StringBuilder();
                ProductViewModel[] product = _session.Get<ProductViewModel[]>(SessionVars.Products);
                innerHtml.Append("<div class=\"col-xs-12\" style=\"font-size:x-large;\"><span>Catalogue</span></div>");
                foreach (ProductViewModel item in product)
                {
                    if (item.BrandId == Convert.ToInt32(BrandId))
                    {
                        // remove double apostrophe
                        item.Description = item.Description.Contains("''") ? item.Description.Replace("''", "") : item.Description;
                        item.JsonData = JsonConvert.SerializeObject(item);
                        innerHtml.Append("<div  class=\"col-sm-3 col-xs-12 text-center\" \">");
                        innerHtml.Append("<span style=\"padding-left: 10px;class=\"col-xs-12\"><img src=" + item.GraphicName+" height=\"120\" width=\"\"/></span>");
                        innerHtml.Append("<p><span style=\"font-size:large;\">" + item.Description.Substring(0, 10) + "...</span></p><div>");
                        innerHtml.Append("<span>For Price Info.<br />Click Details</span></div>");
                        innerHtml.Append("<div style=\"padding-bottom: 10px; color:cadetblue;\"><a href=\"#details_popup\" data-toggle=\"modal\" class=\"btn btn-default\"");
                        innerHtml.Append(" id=\"modalbtn" + item.Id + "\" data-id=\"" + item.Id + "\" data-details='" + item.JsonData + "'>Details</a></div></div>");
                    }
                }
                output.Content.SetHtmlContent(innerHtml.ToString());
            }
        
        }
    }
}
