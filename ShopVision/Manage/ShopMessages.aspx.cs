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

            string iconPath = Settings.IconURL + "default.png";
            if (!string.IsNullOrEmpty(msg.Icon))
            {
                iconPath = Settings.IconURL + msg.Icon;
            }

            returnMe.Cells.Add(new TableCell() { Text = "<img src=\"" + iconPath + "\" style=\"width: 100px;background-color: black; border-radius: 5px;\">" });
            returnMe.Cells.Add(new TableCell() { Text = msg.Content });
            returnMe.Cells.Add(new TableCell() { Text = msg.Start.ToString() });
            returnMe.Cells.Add(new TableCell() { Text = msg.Sender });
            returnMe.Cells.Add(new TableCell() { Text = msg.End.ToString() });
            returnMe.Cells.Add(new TableCell() { Text = "<a href=\"?Remove=" + msg.ID + "\">Remove</a>" });

            return returnMe;
        }

        TableHeaderRow addMessageTableHeaderRow()
        {
            TableHeaderRow returnMe = new TableHeaderRow();

            returnMe.Cells.Add(new TableHeaderCell() { Text = "Icon" });
            returnMe.Cells.Add(new TableHeaderCell() { Text = "Content" });
            returnMe.Cells.Add(new TableHeaderCell() { Text = "Created" });
            returnMe.Cells.Add(new TableHeaderCell() { Text = "Author" });
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

            if (!IsPostBack)
            {
                chkIsHighPriority.Checked = true;

                IconRepository iconRepo = new IconRepository();
                drpIcon.Items.Clear();
                drpIcon.Items.Add(new ListItem() { Text = "default.png", Value = "default.png" });
                foreach (string icon in iconRepo.GetAll())
                {
                    drpIcon.Items.Add(new ListItem() { Text = icon, Value = icon });
                }
            }
        }


        protected void btnPostMessage_Click(object sender, EventArgs e)
        {           

            string newMessageContent = txtNewMessageContent.Text.Trim();
            int expireHours = Parsers.ParseInt(drpMessageExpiry.SelectedValue);
            DateTime messageExpires = DateTime.Now.AddHours(expireHours);
            bool isImportant = chkIsHighPriority.Checked;
            string icon = drpIcon.SelectedValue;

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

        protected void drpIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the preview thumbnail to the selected one
            imgThumbnail.ImageUrl = Settings.IconURL + drpIcon.SelectedValue;
        }
    }
}