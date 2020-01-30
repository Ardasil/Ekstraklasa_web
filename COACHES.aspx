<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="COACHES.aspx.cs" Inherits="Ekstra_web.COACHES" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TRENERZY</title>
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

                <li>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <asp:Button ID="btnLogout" runat="server" Text="Wyloguj" OnClick="btnLogout_Click" /></li>
            </ul>
        </div>
        <div id="main">
            <asp:GridView ID="ekTrenerzy" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="Id_tren"
                ShowHeaderWhenEmpty="true"
                OnRowCommand="ekTrenerzy_RowCommand" OnRowEditing="ekTrenerzy_RowEditing" OnRowCancelingEdit="ekTrenerzy_RowCancelingEdit"
                OnRowUpdating="ekTrenerzy_RowUpdating" OnRowDeleting="ekTrenerzy_RowDeleting"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                <%-- Theme Properties --%>
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
                <Columns>
                    <asp:TemplateField HeaderText="ID drużyny">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Id_druz") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtIDDruz" Text='<%# Eval("Id_druz") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIDDruzFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Imię">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Imie_tren") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtImie" Text='<%# Eval("Imie_tren") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtImieFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nazwisko">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Nazwisko_tren") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNazwisko" Text='<%# Eval("Nazwisko_tren") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNazwiskoFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Drużyna">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Nazwa_druz_tren") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDruz" Text='<%# Eval("Nazwa_druz_tren") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDruzFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wiek">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Wiek_tren") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWiek" Text='<%# Eval("Wiek_tren") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtWiekFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Save" ToolTip="Save" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ImageUrl="~/Images/add.png" runat="server" CommandName="Add" ToolTip="Add" Width="20px" Height="20px" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblSucces" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblError" Text="" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
