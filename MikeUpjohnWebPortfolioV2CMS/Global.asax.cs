using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MikeUpjohnWebPortfolioV2CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError().GetBaseException();

            string ErrorMessage = ex.Message;
            string StackTrace = ex.StackTrace;
            string ExceptionType = ex.GetType().FullName;

            MailMessage error = new MailMessage();
            error.To.Add(new MailAddress("error@mike-upjohn.com", Code.Settings.SITENAME));
            error.From = new MailAddress("error@mike-upjohn.com", Code.Settings.SITENAME);
            error.Subject = "Error on " + Code.Settings.SITENAME;
            error.Body = "<b>Message: </b>" + ex.Message + "<br/><br/>";
            error.Body += "<b>Inner Exception: </b><br/><br/>";
            while (ex.InnerException != null)
            {
                error.Body += ex.InnerException + "<br/><br/>";
                ex = ex.InnerException.InnerException;
            }

            error.Body += "<b>Stack Trace: </b><br/><br/>";
            error.Body += "<b>Error at: </b>" + DateTime.Now.ToString("dddd d MMMM yyyy HH:mm:ss");
            error.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Send(error);
        }
    }
}
