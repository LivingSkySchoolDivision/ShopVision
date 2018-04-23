using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision.Manage
{
    public partial class VehicleInfo : System.Web.UI.Page
    {
        TableHeaderRow addHeaderRow()
        {
            TableHeaderRow row = new TableHeaderRow();

            row.Cells.Add(new TableHeaderCell() { Text = "Inspection description" });
            row.Cells.Add(new TableHeaderCell() { Text = "Inspection date" });
            row.Cells.Add(new TableHeaderCell() { Text = "Next inspection" });

            return row;
        }

        TableRow addRow(VehicleInspection i)
        {
            TableRow row = new TableRow();

            row.Cells.Add(new TableCell() { Text = i.Description });
            row.Cells.Add(new TableCell() { Text = i.EffectiveDate.ToLongDateString() });
            row.Cells.Add(new TableCell() { Text = i.ExpiryDate.ToLongDateString() });

            return row;
        }

        private void updateVehicleDisplay(Vehicle vehicle)
        {
            txtVehicleID.Value = vehicle.ID.ToString();
            lblVehicleName.Text = vehicle.Name;
            lblVehicleActive.Text = vehicle.IsInService ? "<div class=\"yes\">Yes</div>" : "<div class=\"no\">No</div>";
            lblVehiclePlate.Text = vehicle.Plate;
            lblVehicleVIN.Text = vehicle.VIN;
            lblInspectionDue.Text = vehicle.ExpiryDateInEnglish;
            lblDescription.Text = vehicle.Description;
            lblEditLink.Text = "<a href=\"EditVehicle.aspx?vid=" + vehicle.ID + "\">EDIT THIS VEHICLE</a>";
            
            // Clear all except the top two rows of the inspection table
            if (tblInspections.Rows.Count > 2)
            {
                for (int x = 2; x <= tblInspections.Rows.Count; x ++)
                {
                    tblInspections.Rows.RemoveAt(x);
                }
            }


            foreach (VehicleInspection i in vehicle.Inspections)
            {
                tblInspections.Rows.Add(addRow(i));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the vehicle ID from the querystring
            if (Request.QueryString["vid"] != null)
            {
                int givenID = Parsers.ParseInt(Request.QueryString["vid"].ToString().Trim());
                if (givenID > 0)
                {
                    VehicleRepository vr = new VehicleRepository();
                    Vehicle vehicle = vr.Get(givenID);
                    if (vehicle != null)
                    {
                        updateVehicleDisplay(vehicle);                                             
                    }                    
                }
            }

            // Populate date picker fields
            if (!IsPostBack)
            {
                txtEffectiveDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtExpiryDate.Text = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");
            }
        }

        protected void btnAddInspection_Click(object sender, EventArgs e)
        {
            // Parse the dates
            int ID = Parsers.ParseInt(txtVehicleID.Value);
            string description = txtNewInspectionDescription.Text;

            DateTime inspectionEffectiveDate = DateTime.ParseExact(txtEffectiveDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime inspectionExpires = DateTime.ParseExact(txtExpiryDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            VehicleInspectionRepository vir = new VehicleInspectionRepository();
            VehicleInspection i = vir.Add(ID, description, inspectionEffectiveDate, inspectionExpires);
            if (i != null)
            {
                tblInspections.Rows.Add(addRow(i));
            }

        }

        
    }
}