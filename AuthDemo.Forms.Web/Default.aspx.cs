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

        protected void OpenPopup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.open('http://editor.poc.local/', 'Title', 'left=5,top=200,resizable=yes,toolbar=no,scrollbars=yes,height=350,width=1000')", true);
        }
    }
}