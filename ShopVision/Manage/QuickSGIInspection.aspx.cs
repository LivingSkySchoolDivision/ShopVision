using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision.Manage
{
    public partial class QuickSGIInspection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            // Populate the date picker
            drpStartYear.Items.Clear();
            for (int x = DateTime.Now.Year-5;x <= DateTime.Now.Year+5; x++)
            {
                ListItem i = new ListItem() { Text = x.ToString(), Value = x.ToString() };
                if (x == DateTime.Now.Year)
                {
                    i.Selected = true;
                }
                drpStartYear.Items.Add(i);
            }

            drpStartMonth.Items.Clear();
            for (int x = 1; x <= 12; x++)
            {
                ListItem i = new ListItem() { Text = Helpers.GetMonthName(x), Value = x.ToString() };
                if (x == DateTime.Now.Month)
                {
                    i.Selected = true;
                }
                drpStartMonth.Items.Add(i);
            }

            txtStartDay.Text = DateTime.Now.Day.ToString();

            // Populate the list of vehicles
            VehicleRepository vr = new VehicleRepository();
            drpVehicle.Items.Clear();
            foreach (Vehicle v in vr.GetAll().OrderBy(x => x.Name)) {
                drpVehicle.Items.Add(new ListItem() { Text = v.Name, Value = v.ID.ToString() });
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int vehicleID = Parsers.ParseInt(drpVehicle.SelectedValue);
            int startYear = Parsers.ParseInt(drpStartYear.SelectedValue);
            int startMonth = Parsers.ParseInt(drpStartMonth.SelectedValue);
            int startDay = Parsers.ParseInt(txtStartDay.Text);
            string description = txtDescription.Text.Trim();
            
            DateTime effectiveDate = new DateTime(startYear, startMonth, startDay);
            DateTime expiryDate = effectiveDate.AddYears(1);

            VehicleInspectionRepository vir = new VehicleInspectionRepository();
            vir.Add(vehicleID, description, effectiveDate, expiryDate);


        }
    }
}