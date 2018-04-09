﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision.Login
{
    public partial class index : System.Web.UI.Page
    {
        private void createCookie(string sessionID)
        {
            HttpCookie newCookie = new HttpCookie(Settings.CookieName);
            newCookie.Value = sessionID;
            newCookie.Expires = DateTime.Now.AddHours(8);
            newCookie.Domain = Settings.GetServerName(Request);
            newCookie.Secure = true;
            Response.Cookies.Add(newCookie);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                tblLoginform.Visible = false;
                displayError("<p>This login form will only work over an SSL encrypted connection.</p><p>Your web server should be configured to only serve this site over SSL.</p>");
            }

            // Check to see if a user is already logged in and display an appropriate message
            string userSessionID = Authentication.GetSessionIDFromCookies(Settings.CookieName, Request);
            LoginSessionRepository loginSessionRepo = new LoginSessionRepository();
            LoginSession currentUser = loginSessionRepo.LoadIfValid(userSessionID, Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);

            if (currentUser != null)
            {
                tblAlreadyLoggedIn.Visible = true;
                tblLoginform.Visible = false;
                lblUsername.Text = currentUser.Username;
            }

            Page.SetFocus(txtUsername);
        }
        protected void displayError(string errorMessage)
        {
            tblErrorMessage.Visible = true;
            lblErrorMessage.Text = errorMessage;
        }
        public void redirectToIndex()
        {
            string IndexURL = Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + Settings.IndexURL;
            Response.Clear();
            Response.Write("<html>");
            Response.Write("<meta http-equiv=\"refresh\" content=\"0; url=" + IndexURL + "\">");
            Response.Write("<div style=\"padding: 5px; text-align: center; font-size: 10pt; font-family: sans-serif;\">Login successful - Redirecting to site... <a href=\"" + IndexURL + "\">Click here if you are not redirected automatically</a></div>");
            Response.Write("</html>");
            Response.End();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {// Do a sanity check on the username and password 
            string username = Authentication.ParseUsername(txtUsername.Text);
            string password = txtPassword.Text;

            if (
                (username.Length > 3) &&
                (password.Length > 3)
                )
            {
                // Validate username and password
                if (Authentication.ValidateADCredentials(Settings.Domain, username, password))
                {
                    // Check the user's permissions
                    UserPermissionResponse permissions = Authentication.GetUserPermissions(Settings.Domain, username);

                    // Check if the user is a member of a required group
                    if (permissions.CanUserUseSystem)
                    {
                        // Attempt to create a session for the user
                        LoginSessionRepository loginSessionRepo = new LoginSessionRepository();
                        string newSessionID = loginSessionRepo.CreateSession(username, Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);

                        if (newSessionID != string.Empty)
                        {
                            // Create a cookie with the user's shiny new session ID
                            createCookie(newSessionID);

                            // Wait a few seconds
                            System.Threading.Thread.Sleep(1000 * 3);

                            // Redirect to the front page
                            tblAlreadyLoggedIn.Visible = true;
                            tblLoginform.Visible = false;
                            lblUsername.Text = username;
                            redirectToIndex();
                        }
                        else
                        {
                            displayError(
                                "<b style=\"color: red\">Access denied:</b> There was an error creating your login session.<br><br> Please create a ticket in our <a href=\"https://helpdesk.lskysd.ca\">Help Desk system</a>.");
                        }
                    }
                    else
                    {
                        displayError(
                            "<b style=\"color: red\">Access denied:</b> Your account is not authorized for access to this site.<br><br> To request access to this site, please create a ticket in our <a href=\"https://helpdesk.lskysd.ca\">Help Desk system</a>.");
                    }
                }
                else
                {
                    displayError("<b style=\"color: red\">Access denied:</b> Invalid username or password entered");
                }
            }
            else
            {
                displayError("<b style=\"color: red\">Access denied:</b> Invalid username or password entered");
            }
        }
    }
}