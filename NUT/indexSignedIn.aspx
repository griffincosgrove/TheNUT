<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indexSignedIn.aspx.cs" Inherits="indexSignedIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Signed In! "></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Menu At Bistro 1908 in SSC"></asp:Label>
            <br />
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" >
            <Columns>
                <asp:BoundField DataField="menuItem" HeaderText="menuItem" SortExpression="menuItem" />
                <asp:BoundField DataField="calories" HeaderText="calories" SortExpression="calories" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [menuItem], [calories] FROM [foodItem] ORDER BY [calories]"></asp:SqlDataSource>
    </form>
</body>
</html>
