<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Protected.master" AutoEventWireup="true" CodeBehind="Vehicles.aspx.cs" Inherits="ShopVision.Manage.Vehicles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <h2>Active vehicles</h2>
    <asp:Table ID="tblVehicles" runat="server"></asp:Table>   
    <h2>Inactive vehicles</h2>
    <asp:Table ID="tblInactiveVehicles" runat="server"></asp:Table>
</asp:Content>
