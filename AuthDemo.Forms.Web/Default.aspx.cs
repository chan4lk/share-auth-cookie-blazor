using System;
using System.Web.Security;
using System.Web.UI;

namespace AuthDemo.Forms.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserNameLabel.Text = User.Identity.Name;
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}