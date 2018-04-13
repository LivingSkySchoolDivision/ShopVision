using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginSessionRepository loginRepository = new LoginSessionRepository();
            string foundUserSessionID = loginRepository.GetSessionIDFromCookies(Request);
            LoginSession currentUser = null;
            if (!string.IsNullOrEmpty(foundUserSessionID))
            {
                // A cookie exists, lets see if it corresponds to a valid session ID
                currentUser = loginRepository.LoadIfValid(foundUserSessionID,
                    Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);

                if (currentUser != null)
                {
                    if (currentUser.IsAdmin)
                    {
                        litAdminControls.Visible = true;
                    }
                }
            }
        }
    }
}