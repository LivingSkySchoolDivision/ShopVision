using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision.JSON
{
    public partial class ShopMessages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShopMessageRepository msgRepo = new ShopMessageRepository();

            List<ShopMessage> activeMessages = msgRepo.GetActive();
            List<ShopMessage> NormalPriority = activeMessages.Where(x => x.IsImportant == false).ToList();
            List<ShopMessage> HighPriority = activeMessages.Where(x => x.IsImportant == true).ToList();

            Response.Clear();
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/json; charset=utf-8";

            Response.Write("{");

            Response.Write(" \"Normal\": ");

            Response.Write("[\n");

            int counter = 0;

            foreach (ShopMessage msg in NormalPriority)
            {
                Response.Write("{");
                Response.Write("\"ID\":\"" + msg.ID + "\",\n");
                Response.Write("\"Sender\":\"" + Helpers.SanitizeForJSON(msg.Sender) + "\",\n");
                Response.Write("\"Content\":\"" + Helpers.SanitizeForJSON(msg.Content) + "\",\n");
                Response.Write("\"StartTime\":\"" + msg.Start + "\",\n");
                Response.Write("\"EndTime\":\"" + msg.End + "\",\n");
                Response.Write("\"IsImportant\":" + msg.IsImportant.ToString().ToLower() + "\n");
                Response.Write("}");

                counter++;
                if (counter < NormalPriority.Count)
                {
                    Response.Write(",");
                }
            }

            Response.Write("],\n");
            
            Response.Write(" \"High\": ");
            Response.Write("[\n");

            counter = 0;
            foreach (ShopMessage msg in HighPriority)
            {
                Response.Write("{");
                Response.Write("\"ID\":\"" + msg.ID + "\",\n");
                Response.Write("\"Sender\":\"" + Helpers.SanitizeForJSON(msg.Sender) + "\",\n");
                Response.Write("\"Content\":\"" + Helpers.SanitizeForJSON(msg.Content) + "\",\n");
                Response.Write("\"StartTime\":\"" + msg.Start + "\",\n");
                Response.Write("\"EndTime\":\"" + msg.End + "\",\n");
                Response.Write("\"IsImportant\":" + msg.IsImportant.ToString().ToLower() + "\n");
                Response.Write("}");

                counter++;
                if (counter < HighPriority.Count)
                {
                    Response.Write(",");
                }
            }

            Response.Write("]\n");

            Response.Write("}");
            Response.End();
            
        }
    }
}