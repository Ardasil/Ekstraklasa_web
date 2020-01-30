<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="Ekstra_web.LOGIN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="login">
            <table style="margin:auto;border:2px solid white">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Login"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox></td>
                </tr>
                                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtPass" TextMode="Password" runat="server"></asp:TextBox></td>
                </tr>
                                <tr>
                    <td>
                        </td>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" Text="Zaloguj" OnClick="btnLogin_Click" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lblError" runat="server" Text="Podano błędne dane logowania!" forecolor="red"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
