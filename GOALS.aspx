<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GOALS.aspx.cs" Inherits="Ekstra_web.GOALS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BRAMKI</title>
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
            <asp:GridView ID="ekBramki" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="Id_strzelec"
                ShowHeaderWhenEmpty="true"
                OnRowCommand="ekBramki_RowCommand" OnRowEditing="ekBramki_RowEditing" OnRowCancelingEdit="ekBramki_RowCancelingEdit"
                OnRowUpdating="ekBramki_RowUpdating" OnRowDeleting="ekBramki_RowDeleting"
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
                            <asp:Label Text='<%# Eval("Id_gospo") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtIDGosp" Text='<%# Eval("Id_gospo") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIDGospFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gość">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Id_gosc") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtIDGosc" Text='<%# Eval("Id_gosc") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIDGoscFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Strzelec">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Id_pilkarze") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtStrzelec" Text='<%# Eval("Id_pilkarze") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtStrzelecFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bramkarz">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("PIL_Id_pilkarze") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBramkarz" Text='<%# Eval("PIL_Id_pilkarze") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtBramkarzFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Czas bramki">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Czas_bramki") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCzas" Text='<%# Eval("Czas_bramki") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCzasFooter" runat="server" />
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
