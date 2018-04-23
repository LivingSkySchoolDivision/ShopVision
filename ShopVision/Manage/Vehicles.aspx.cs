using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision.Manage
{
    public partial class Vehicles : System.Web.UI.Page
    {
        TableRow addVehicleRow(Vehicle vehicle)
        {            
            TableRow row = new TableRow();

            row.Cells.Add(new TableCell() { Text = "<a href=\"VehicleInfo.aspx?vid=" + vehicle.ID + "\">" + vehicle.Name + "</a>" });
            row.Cells.Add(new TableCell() { Text = vehicle.VIN });
            row.Cells.Add(new TableCell() { Text = vehicle.Plate });
            row.Cells.Add(new TableCell() { Text = vehicle.LastInspectionInEnglish });
            row.Cells.Add(new TableCell() { Text = vehicle.ExpiryDateInEnglish });

            return row;
        }

        TableHeaderRow addTitleRow()
        {
            TableHeaderRow row = new TableHeaderRow();

            row.Cells.Add(new TableHeaderCell() { Text = "Vehicle" });
            row.Cells.Add(new TableHeaderCell() { Text = "VIN" });
            row.Cells.Add(new TableHeaderCell() { Text = "Plate" });
            row.Cells.Add(new TableHeaderCell() { Text = "Last inspection" });
            row.Cells.Add(new TableHeaderCell() { Text = "Inspection expires" });

            return row;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VehicleRepository vehicleRepo = new VehicleRepository();

            tblVehicles.Rows.Clear();
            tblVehicles.Rows.Add(addTitleRow());
            foreach (Vehicle v in vehicleRepo.GetAll().Where(v => v.IsInService == true).OrderBy(v => v.Name))
            {
                tblVehicles.Rows.Add(addVehicleRow(v));
            }


            tblInactiveVehicles.Rows.Clear();
            tblInactiveVehicles.Rows.Add(addTitleRow());
            foreach (Vehicle v in vehicleRepo.GetAll().Where(v => v.IsInService == false).OrderBy(v => v.Name))
            {
                tblInactiveVehicles.Rows.Add(addVehicleRow(v));
            }
        }
        public void RedirectToEditPage(int id)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            //Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + Settings.LoginURL);
            Response.Redirect("EditVehicle.aspx?vid=" + id);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }

        protected void btnAddVehicle_Click(object sender, EventArgs e)
        {
            VehicleRepository vr = new VehicleRepository();
            Vehicle v = vr.Add();
            if (v != null)
            {
                RedirectToEditPage(v.ID);
            }
        }
    }
}