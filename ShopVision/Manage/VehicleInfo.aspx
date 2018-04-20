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

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 1000px; margin-left: auto; margin-right: auto;">
        <a href="Vehicles.aspx">Back to vehicle list</a>
        <h1><asp:Label ID="lblVehicleName" runat="server" Text="Vehicle Name"></asp:Label></h1>
        <br />
        <b>VIN</b>: <asp:Label ID="lblVehicleVIN" runat="server" Text="################"></asp:Label><br />
        <b>Plate</b>: <asp:Label ID="lblVehiclePlate" runat="server" Text="### ###"></asp:Label><br />
        <b>Active</b>: <asp:Label ID="lblVehicleActive" runat="server" Text="Unknown"></asp:Label><br />
        <h2>Inspections</h2>
        <asp:Table ID="tblInspections" runat="server" Width="100%"></asp:Table>
    </div>
</asp:Content>
