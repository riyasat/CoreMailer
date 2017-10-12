using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreMailer.Interfaces
{
    public interface ITemplateRenderer
    {
        Task<string> RenderViewAsync<TModel>(string name, TModel model);
        string RenderView<TModel>(string name, TModel model);
    }
}
