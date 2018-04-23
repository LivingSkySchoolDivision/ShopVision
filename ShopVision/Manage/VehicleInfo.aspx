<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Protected.master" AutoEventWireup="true" CodeBehind="VehicleInfo.aspx.cs" Inherits="ShopVision.Manage.VehicleInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .yes {
            color: forestgreen;
            font-weight: bold;
            display: inline;
        }

        .no {
            color: red;
            font-weight: bold;
            display: inline;
        }

        th {
            text-align: left;
        }

        .toprightcorner {
            float: right;
            padding: 5px;
            display: block;
            font-weight: bold;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="txtVehicleID" runat="server" />
    <div style="width: 1000px; margin-left: auto; margin-right: auto;">        
        <asp:Label ID="lblEditLink" runat="server" CssClass="toprightcorner" Text=""></asp:Label>
        <a href="Vehicles.aspx">Back to vehicle list</a>
        <h1> Vehicle: <asp:Label ID="lblVehicleName" runat="server" Text="Vehicle Name"></asp:Label></h1>
        <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label><br />
        <br />
        <b>VIN</b>: <asp:Label ID="lblVehicleVIN" runat="server" Text="################"></asp:Label><br />
        <b>Plate</b>: <asp:Label ID="lblVehiclePlate" runat="server" Text="### ###"></asp:Label><br />
        <b>Active</b>: <asp:Label ID="lblVehicleActive" runat="server" Text="Unknown"></asp:Label><br />
        <b>Next SGI Inspection due</b>: <asp:Label ID="lblInspectionDue" runat="server" Text="Unknown"></asp:Label><br />
        <h2>Inspections</h2>
        <asp:Table ID="tblInspections" runat="server" Width="100%">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Inspection description</asp:TableHeaderCell>
                <asp:TableHeaderCell>Inspection date</asp:TableHeaderCell>
                <asp:TableHeaderCell>Next inspection due</asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell><asp:TextBox ID="txtNewInspectionDescription" runat="server" Value="SGI Inspection"></asp:TextBox></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtEffectiveDate" runat="server" TextMode="Date"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtExpiryDate" runat="server" TextMode="Date"></asp:TextBox>

                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAddInspection" runat="server" Text="Add inspection" OnClick="btnAddInspection_Click" />

                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
