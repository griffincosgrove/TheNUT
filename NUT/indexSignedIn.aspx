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
            &nbsp;<asp:Label ID="lblEmail" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Menu At Bistro 1908 in SSC"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Select the item consumed"></asp:Label>
            <br />
        </div>
        <asp:DropDownList ID="dropDownFood" runat="server" DataSourceID="SqlDataSource1" DataTextField="MenuItem" DataValueField="MenuItem"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [menuItem] FROM [FoodItem]"></asp:SqlDataSource>
        <asp:Button ID="btnAddFoodItem" runat="server" OnClick="btnAddFoodItem_Click" Text="Add Food" />
        <br />
        <br />
        <asp:Button ID="btnViewConsumed" runat="server" OnClick="btnViewConsumed_Click" Text="View Consumed Food" />
        <br />
        <br />
        <asp:GridView ID="GridViewFood" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Visible="False">
            <Columns>
                <asp:BoundField DataField="food" HeaderText="food" SortExpression="food" />
                <asp:BoundField DataField="calories" HeaderText="calories" SortExpression="calories" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [food], [calories] FROM [foodConsumed] WHERE ([email] = @email)">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="error" Name="email" SessionField="Email" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Label ID="lblTotalCal" runat="server" Text="Total Calories"></asp:Label>
        <br />
        <asp:GridView ID="GridViewTotalCalories" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Visible="False">
            <Columns>
                <asp:BoundField DataField="SumTotal" HeaderText="SumTotal" ReadOnly="True" SortExpression="SumTotal" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT top 1 SUM(calories) OVER () AS SumTotal FROM [foodConsumed] WHERE ([email] = @email) group by food, calories">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="error" Name="email" SessionField="Email" />
            </SelectParameters>
        </asp:SqlDataSource>
        </form>
</body>
</html>
