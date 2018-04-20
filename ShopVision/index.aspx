<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Protected.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ShopVision.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Static/CSS/buttons.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>LSKYSD ShopVision</h1>
    <div class="tile_container">
        <div class="tile">
            <h2>Screens</h2>
            <ul>
                <li><a class="orange" href="/Screens/BusGarage/">Bus Garage</a></li>
            </ul>
        </div>
        <div class="tile">
            <h2>Shop Messages</h2>            
            <ul>
                <li><a class="green" href="/Manage/ShopMessages.aspx">Manage shop messages</a></li>                
            </ul>            
            <h2>SGI Inspections</h2>
            <ul>
                <li><a class="green" href="/Manage/Vehicles.aspx">Vehicles</a></li>                
                <li><a class="red" href="/Manage/QuickSGIInspection.aspx">Quick add an SGI bus inspection</a></li>
            </ul>
            <h3>Reports</h3>
            <ul>
                <li><a class="green" href="/Reports/BusInspectionDueDates.aspx">SGI Inspections: Next due dates</a></li>
                <li><a class="green" href="/Reports/BusInspectionLogs.aspx">SGI Inspections: Full inspection log</a></li>
            </ul>                
        </div>
        <asp:Literal ID="litAdminControls" runat="server" Visible="false">
            <div class="tile">
                <h1>Admin functions</h1>
                <h2>JSON</h2> 
                <h3>General</h3>
                <ul>
                    <li><a class="blue" href="/JSON/ShopMessages.aspx">Shop Messages</a></li>
                    <li><a class="blue" href="/JSON/JSONTime.aspx">Time</a></li>
                </ul>
                <h3>FleetVision</h3>
                <ul>
                    <li><a class="blue" href="/JSON/FleetVision/NewestWorkOrders.aspx">Newest work orders</a></li>
                    <li><a class="blue" href="/JSON/FleetVision/WorkOrderCounts.aspx">Work order counts</a></li>
                </ul>
                <h3>Versatrans</h3>
                <ul>        
                    <li><a class="blue" href="/JSON/Versatrans/UpcomingBusInspections.aspx">Upcoming bus inspections</a></li>
                </ul>
            </div>
        </asp:Literal>
        
    </div>
</asp:Content>
