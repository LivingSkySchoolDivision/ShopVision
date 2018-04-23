<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Protected.master" AutoEventWireup="true" CodeBehind="EditVehicle.aspx.cs" Inherits="ShopVision.Manage.EditVehicle" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"><asp:HiddenField ID="txtVehicleID" runat="server" />
    <div style="width: 1000px; margin-left: auto; margin-right: auto;">
        <a href="Vehicles.aspx">Back to vehicle list</a><br />
        <h1>Editing Vehicle: <asp:Label ID="lblVehicleName" runat="server" Text="Label"></asp:Label></h1>

        <table>
            <tr>
                <td><b>Vehicle Name:</b></td>
                <td><asp:TextBox ID="txtVehicleName" runat="server" Width="500" Text="Vehicle Name"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>Vehicle Description: </b></td>
                <td><asp:TextBox ID="txtDescription" runat="server" Height="50" Width="500" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>VIN</b>:</td>
                <td><asp:TextBox ID="txtVehicleVIN" runat="server" Width="500" Text="################"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>Plate</b>: </td>
                <td><asp:TextBox ID="txtVehiclePlate" runat="server" Width="500" Text="### ###"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>Active</b>:</td>
                <td><asp:CheckBox ID="chkActive" runat="server" /></td>
            </tr>
            <tr>                
                <td colspan="2" style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
