using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreMailer.Models;

namespace CoreMailer.Interfaces
{
    public interface ICoreMvcMailer
    {
        void Send(MailerModel model);
        Task SendAsync(MailerModel model);
    }
}
