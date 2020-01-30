<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DASHBOARD.aspx.cs" Inherits="Ekstra_web.DASHBOARD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EKSTRAKLASA</title>
    <link href="StyleSheet1.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="menu">
            <ul>
                <li><a href="GOALS.aspx">BRAMKI</a></li>
                <li><a href="TEAMS.aspx">DRUŻYNY</a></li>
                <li><a href="CARDS.aspx">KARTKI</a></li>
                <li><a href="PLAYERS.aspx">PIŁKARZE</a></li>
                <li><a href="REFEREES.aspx">SĘDZIOWIE</a></li>
                <li><a href="MATCHES.aspx">SPOTKANIA</a></li>
                <li><a href="COACHES.aspx">TRENERZY</a></li>

                <li><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:Button ID="btnLogout" runat="server" Text="Wyloguj" OnClick="btnLogout_Click" /></li>
            </ul>
        </div>
          <br /><br /><br /><br /><br /><br /><br /><br />
        <div id="welcome">
                <p>
                    Witaj w Ekstraklasie.
                </p>
        </div>
    </form>
</body>
</html>
