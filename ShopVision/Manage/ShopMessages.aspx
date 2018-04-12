<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Protected.master" AutoEventWireup="true" CodeBehind="ShopMessages.aspx.cs" Inherits="ShopVision.Manage.ShopMessages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top">Message content</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="txtNewMessageContent" runat="server" TextMode="MultiLine" Height="100" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top">Options</asp:TableCell>
                <asp:TableCell>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top"><asp:CheckBox ID="chkIsHighPriority" runat="server" /></asp:TableCell>
                            <asp:TableCell><b>High priority?</b><br /> High priority messages will display on top of all othe content. Normal priority messages are displayed on their own page.</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                     
                </asp:TableCell>                
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top">Display message until</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drpMessageExpiry" runat="server">
                        <asp:ListItem Value="1" Text="1 Hour"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2 Hours"></asp:ListItem>
                        <asp:ListItem Value="3" Text="3 Hours"></asp:ListItem>
                        <asp:ListItem Value="5" Text="5 Hours"></asp:ListItem>
                        <asp:ListItem Value="12" Text="End of day"></asp:ListItem>
                        <asp:ListItem Value="24" Text="24 hours from now"></asp:ListItem>                                                
                        <asp:ListItem Value="168" Text="">A week from now</asp:ListItem>
                    </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top">Icon</asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                       <asp:Button ID="btnPostMessage" runat="server" Text="Create new message" onclick="btnPostMessage_Click" />
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </div>
    <div>
        <h2>Active messages</h2>
        <asp:Table ID="tblActiveMessages" runat="server"></asp:Table>
    </div>    
</asp:Content>
