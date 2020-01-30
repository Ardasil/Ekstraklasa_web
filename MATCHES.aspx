<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MATCHES.aspx.cs" Inherits="Ekstra_web.MATCHES" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SPOTKANIA</title>
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
            <asp:GridView ID="ekSpotkania" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="Id_gospo,Id_gosc"
                ShowHeaderWhenEmpty="true"
                OnRowCommand="ekSpotkania_RowCommand" OnRowEditing="ekSpotkania_RowEditing" OnRowCancelingEdit="ekSpotkania_RowCancelingEdit"
                OnRowUpdating="ekSpotkania_RowUpdating" OnRowDeleting="ekSpotkania_RowDeleting"
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
                    <asp:TemplateField HeaderText="Gospodarz">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Id_druz") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtIDGosp" Text='<%# Eval("Id_druz") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIDGospFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gość">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("DRU_Id_druz") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtIDGosc" Text='<%# Eval("DRU_Id_druz") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIDGoscFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Data">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Data") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtData" Text='<%# Eval("Data") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDataFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Godzina">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Godzina") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtGodzina" Text='<%# Eval("Godzina") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtGodzinaFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Miejsce">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Miejsce") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMiejsce" Text='<%# Eval("Miejsce") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtMiejsceFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wynik gospodarz">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Wynik_gospo") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWynikGosp" Text='<%# Eval("Wynik_gospo") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtWynikGospFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wynik gość">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Wynik_gosc") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWynikGosc" Text='<%# Eval("Wynik_gosc") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtWynikGoscFooter" runat="server" />
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
