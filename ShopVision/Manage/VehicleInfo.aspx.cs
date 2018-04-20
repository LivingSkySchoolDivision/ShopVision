using System;
using System.Collections.Generic;
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
                        lblVehicleName.Text = vehicle.Name;
                        lblVehicleActive.Text = vehicle.IsInService ? "<div class=\"yes\">Yes</div>" : "<div class=\"no\">No</div>";
                        lblVehiclePlate.Text = vehicle.Plate;
                        lblVehicleVIN.Text = vehicle.VIN;

                        tblInspections.Rows.Clear();
                        tblInspections.Rows.Add(addHeaderRow());
                        foreach (VehicleInspection i in vehicle.Inspections)
                        {
                            tblInspections.Rows.Add(addRow(i));
                        }
                    }

                    
                }
            }
        }
    }
}