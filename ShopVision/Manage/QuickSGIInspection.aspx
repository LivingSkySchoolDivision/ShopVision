<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Protected.master" AutoEventWireup="true" CodeBehind="QuickSGIInspection.aspx.cs" Inherits="ShopVision.Manage.QuickSGIInspection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 800px; margin-left: auto; margin-right: auto;">
        <h1>Quick-Add an inspection</h1>
        <asp:Table ID="tblControls" runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell>Vehicle</asp:TableCell>            
                <asp:TableCell><asp:DropDownList ID="drpVehicle" runat="server"></asp:DropDownList></asp:TableCell>            
            </asp:TableRow>        
            <asp:TableRow>
                <asp:TableCell>Description</asp:TableCell>            
                <asp:TableCell><asp:TextBox ID="txtDescription" runat="server" Text="SGI Inspection"></asp:TextBox></asp:TableCell>            
            </asp:TableRow>
                
            <asp:TableRow>
                <asp:TableCell>Effective date</asp:TableCell>            
                <asp:TableCell>
                    <asp:DropDownList ID="drpStartYear" runat="server"></asp:DropDownList>&nbsp;
                    <asp:DropDownList ID="drpStartMonth" runat="server"></asp:DropDownList>&nbsp;
                    <asp:TextBox ID="txtStartDay" runat="server"  Text="1"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
                
            <asp:TableRow>
                <asp:TableCell>Expiry date</asp:TableCell>            
                <asp:TableCell>Expiry date automatically set to 1 year from effective date</asp:TableCell>
            </asp:TableRow>

        
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>            
                <asp:TableCell HorizontalAlign="Right"><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
