using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopVision.Manage
{
    public partial class EditVehicle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            }
        }
        public void RedirectToViewPage(int id)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            //Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + Settings.LoginURL);
            Response.Redirect("VehicleInfo.aspx?vid=" + id);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }

        private void updateVehicleDisplay(Vehicle vehicle)
        {
            txtVehicleID.Value = vehicle.ID.ToString();
            txtVehicleName.Text = vehicle.Name;
            lblVehicleName.Text = vehicle.Name;
            if (vehicle.IsInService)
            {
                chkActive.Checked = true;
            } else
            {
                chkActive.Checked = false;
            }

            txtVehiclePlate.Text = vehicle.Plate;
            txtVehicleVIN.Text = vehicle.VIN;
            
            txtDescription.Text = vehicle.Description;            
        }

        protected void lnkToggleInactive_Click(object sender, EventArgs e)
        {
            int vehicleID = Parsers.ParseInt(txtVehicleID.Value);

            VehicleRepository vr = new VehicleRepository();
            Vehicle v = vr.Get(vehicleID);

            if (v != null)
            {
                if (v.IsInService)
                {
                    vr.MakeVehicleInactive(v.ID);
                    v.IsInService = false;
                }
                else
                {
                    vr.MakeVehicleActive(v.ID);
                    v.IsInService = true;
                }
                updateVehicleDisplay(v);
            }

        }

        

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            int givenID = Parsers.ParseInt(txtVehicleID.Value);
            if (givenID > 0)
            {
                VehicleRepository vr = new VehicleRepository();
                Vehicle vehicle = vr.Get(givenID);
                if (vehicle != null)
                {
                    // Update the vehicle object
                    vehicle.Name = txtVehicleName.Text.Trim();
                    vehicle.Description = txtDescription.Text.Trim();
                    vehicle.IsInService = chkActive.Checked;
                    vehicle.Plate = txtVehiclePlate.Text.Trim();
                    vehicle.VIN = txtVehicleVIN.Text.Trim();

                    // Send the updated vehicle object back to the database
                    vr.Update(vehicle);

                    // Redirect
                    RedirectToViewPage(vehicle.ID);
                }
            }            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int givenID = Parsers.ParseInt(txtVehicleID.Value);
            if (givenID > 0)
            {
                VehicleRepository vr = new VehicleRepository();
                Vehicle vehicle = vr.Get(givenID);
                if (vehicle != null)
                {
                    RedirectToViewPage(vehicle.ID);
                }
            }            
        }
    }
}