<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Protected.master" AutoEventWireup="true" CodeBehind="Vehicles.aspx.cs" Inherits="ShopVision.Manage.Vehicles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .toprightcorner {
            float: right;
            padding: 5px;
            display: block;
            font-weight: bold;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <div style="width: 1000px; margin-left: auto; margin-right: auto;">  
    <div class="toprightcorner">
        <asp:Button ID="btnAddVehicle" runat="server" Text="Add Vehicle" OnClick="btnAddVehicle_Click" />
    </div>
    <h2>Active vehicles</h2>
    <asp:Table ID="tblVehicles" runat="server"></asp:Table>   
    <h2>Inactive vehicles</h2>
    <asp:Table ID="tblInactiveVehicles" runat="server"></asp:Table>
    </div>
</asp:Content>
