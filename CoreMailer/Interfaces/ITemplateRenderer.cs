using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace CoreMailer.Interfaces
{
    public interface ITemplateRenderer
    {
        Task<string> RenderViewAsync<TModel>(string name, TModel model);
        string RenderView<TModel>(string name, TModel model);
	    HtmlString RenderViewToHtml<TModel>(string name, TModel model);

    }
}
