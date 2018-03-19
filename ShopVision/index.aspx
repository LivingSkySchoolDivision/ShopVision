<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ShopVision.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>LSKYSD ShopVision</h1>
    <h2>Screens</h2>
    <ul>
        <li><a href="/Screens/BusGarage/">Bus Garage</a></li>
    </ul>
    <h2>JSON</h2>    
    <h3>FleetVision</h3>
    <ul>
        <li><a href="/JSON/FleetVision/NewestWorkOrders.aspx">Newest work orders</a></li>
        <li><a href="/JSON/FleetVision/WorkOrderCounts.aspx">Work order counts</a></li>
    </ul>
    <h3>Versatrans</h3>
    <ul>        
        <li><a href="/JSON/Versatrans/UpcomingBusInspections.aspx">Upcoming bus inspections</a></li>
    </ul>
    <h2>Config</h2>
</body>
</html>
