using AuthDemo.Forms.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AuthDemo.Forms.Web
{
    /// <summary>
    /// Summary description for AuthService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AuthService : System.Web.Services.WebService
    {

        [WebMethod]
        public UserModel GetUser(string value)
        {
            var model = new UserModel();

            var user = User.Identity;

            model.Name = user.Name;
            model.IsAuthenticated = User.Identity.IsAuthenticated;            

            return model;
        }
    }
}
