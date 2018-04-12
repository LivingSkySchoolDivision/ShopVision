using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision.Manage
{
    public partial class ShopMessages : System.Web.UI.Page
    {
        TableRow addMessageTableRow(ShopMessage msg)
        {
            TableRow returnMe = new TableRow();

            returnMe.Cells.Add(new TableCell() { Text = msg.Start.ToString() });
            returnMe.Cells.Add(new TableCell() { Text = msg.Sender });
            returnMe.Cells.Add(new TableCell() { Text = msg.IsImportant ? "[High importance] " + msg.Content : msg.Content });
            returnMe.Cells.Add(new TableCell() { Text = msg.End.ToString() });
            returnMe.Cells.Add(new TableCell() { Text = "<a href=\"?Remove=" + msg.ID + "\">Remove</a>" });

            return returnMe;
        }

        TableHeaderRow addMessageTableHeaderRow()
        {
            TableHeaderRow returnMe = new TableHeaderRow();

            returnMe.Cells.Add(new TableHeaderCell() { Text = "Created" });
            returnMe.Cells.Add(new TableHeaderCell() { Text = "Author" });
            returnMe.Cells.Add(new TableHeaderCell() { Text = "Content" });
            returnMe.Cells.Add(new TableHeaderCell() { Text = "Expires" });
            returnMe.Cells.Add(new TableHeaderCell() { Text = " " });

            return returnMe;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check to see if we need to delete any messages
            // Remember to delete them from the ones displayed in the table too, or the user will still see them
            // load active messages in the table and provide button links to delete them
            ShopMessageRepository messageRepo = new ShopMessageRepository();

            if (Request.QueryString["remove"] != null)
            {
                int removeID = Parsers.ParseInt(Request.QueryString["remove"].ToString().Trim());
                if (removeID > 0)
                {
                    messageRepo.Delete(removeID);
                }                
            }


            List<ShopMessage> activeMessages = messageRepo.GetActive();

            tblActiveMessages.Rows.Clear();
            tblActiveMessages.Rows.Add(addMessageTableHeaderRow());
            foreach (ShopMessage msg in activeMessages)
            {
                tblActiveMessages.Rows.Add(addMessageTableRow(msg));
            }
        }


        protected void btnPostMessage_Click(object sender, EventArgs e)
        {           

            string newMessageContent = txtNewMessageContent.Text.Trim();
            int expireHours = Parsers.ParseInt(drpMessageExpiry.SelectedValue);
            DateTime messageExpires = DateTime.Now.AddHours(expireHours);
            bool isImportant = chkIsHighPriority.Checked;
            string icon = "";

            if (icon.Length <= 0)
            {
                icon = "default.png";
            }

            if (newMessageContent.Length > 0)
            {
                // Get the current logged in user
                LoginSessionRepository loginRepository = new LoginSessionRepository();
                string foundUserSessionID = loginRepository.GetSessionIDFromCookies(Request);

                LoginSession currentUser = loginRepository.LoadIfValid(foundUserSessionID, Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);
                if (currentUser != null)
                {
                    ShopMessageRepository messageRepo = new ShopMessageRepository();
                    ShopMessage msg = messageRepo.Add(currentUser.Username, newMessageContent, DateTime.Now, messageExpires, isImportant, icon);
                    tblActiveMessages.Rows.Add(addMessageTableRow(msg));
                }
            }
        }
    }
}