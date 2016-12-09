using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Security.Application;
using System.Net.Http;
using System.Web.Mvc;
using System.Web;

namespace HRSS.Utility
{
    /// <summary>
    /// ScurityHelper merupakan kumpulan fungsi Scurity 
    /// </summary>
    public class SecurityHelper
    {
        #region Constructor
        public SecurityHelper()
        {

        }
        #endregion

        #region Method

        public static void InitializeAntiXSS<T>(T entity) where T : class
        {
            var entityProperties = Library.GetFieldAndType(typeof(T), false);            
            foreach (var item in entityProperties)
            {
                entity.GetType().GetProperty(item["fldName"]).SetValue(entity, Sanitizer.GetSafeHtmlFragment(entity.GetType().GetProperty(item["fldName"]).GetValue(entity, null).ToString()));
            }            
        }
        public class InitializeAntiCSRF : FilterAttribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationContext filterContext)
            {
                string clientToken = filterContext.RequestContext.HttpContext.Request.Headers.Get(KEY_NAME);
                if (clientToken == null)
                {
                    //throw new HttpAntiForgeryException(string.Format("Header does not contain {0}", KEY_NAME));
                    throw new HttpException(404, string.Format("Header does not contain {0}", KEY_NAME));
                }

                string serverToken = filterContext.HttpContext.Request.Cookies.Get(KEY_NAME).Value;
                if (serverToken == null)
                {
                    throw new HttpAntiForgeryException(string.Format("Cookies does not contain {0}", KEY_NAME));
                }

                System.Web.Helpers.AntiForgery.Validate(serverToken, clientToken);
            }

            private const string KEY_NAME = "__RequestVerificationToken";
        }

        #endregion
    }
}
