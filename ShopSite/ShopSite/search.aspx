<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="ShopSite.search" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 1140px;
            height: 148px;
            text-align: center;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            width: 161px;
            height: 194px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div style="float: left; width: 101%; text-align: center;">
        <img class="auto-style1" src="assets/banner.PNG" /></div>
    <p style="text-align: center">
        </p>
        <p style="text-align: center">
             <asp:Label ID="errLbl" runat="server" ForeColor="Red"></asp:Label>
        </p>
        <asp:Panel ID="Panel1" runat="server" Style="left: 50%; margin-left: 432px; text-align: center;" Width="579px" BorderStyle="Solid">
            <asp:CheckBox ID="CheckBox1" runat="server"  TextAlign="Left" Text="Please generate a Purchase Order (P.O)" />
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="custPnl" runat="server" BorderStyle="Solid">
            Customer<br />
            <br />
            custID
            <asp:TextBox ID="custIDTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; firstName
            <asp:TextBox ID="firstNameTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; lastName
            <asp:TextBox ID="lastNameTxt" runat="server"></asp:TextBox>
            &nbsp; phoneNumber
            <asp:TextBox ID="phoneTxt" runat="server"></asp:TextBox>
            &nbsp;xxx-xxx-xxxx<br />
        </asp:Panel>
        <p style="text-align: center">
            </p>
        <asp:Panel ID="prodPnl" runat="server" BorderStyle="Solid">
            Product<br />
            <br />
            prodID
            <asp:TextBox ID="prodIDTxt" runat="server"></asp:TextBox>
            &nbsp; prodName
            <asp:TextBox ID="prodNameTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; price
            <asp:TextBox ID="priceTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; prodWeight
            <asp:TextBox ID="prodWeightTxt" runat="server"></asp:TextBox>
            &nbsp;kg.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="soldOutChk" runat="server" Text="Sold Out" TextAlign="Left" />
        </asp:Panel>
        <p style="text-align: center">
            </p>
        <asp:Panel ID="orderPnl" runat="server" BorderStyle="Solid">
            Order<br />
            <br />
            orderID
            <asp:TextBox ID="orderIDTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; custID
            <asp:TextBox ID="ordCustIDTxt" runat="server"></asp:TextBox>
            &nbsp; poNumber
            <asp:TextBox ID="poNumberTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; orderDate
            <asp:TextBox ID="orderDateTxt" runat="server"></asp:TextBox>
            &nbsp;MM-DD-YY<br />
        </asp:Panel>
        <p class="auto-style2" style="text-align: center">
        </p>
        <asp:Panel ID="cartPnl" runat="server" BorderStyle="Solid">
            Cart<br />
            <br />
            orderID
            <asp:TextBox ID="cartOrderIDTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; prodID
            <asp:TextBox ID="cartProdIDTxt" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; quantity
            <asp:TextBox ID="quantityTxt" runat="server"></asp:TextBox>
            &nbsp;
            <br />
        </asp:Panel>
        <p class="auto-style2">
            &nbsp;</p>
        <p class="auto-style2">
            &nbsp;</p>
        <p class="auto-style2">
            &nbsp;</p>
        <p class="auto-style2">
            <asp:Button ID="backBtn" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" style="text-align: center" Text="Go Back" Width="122px" OnClick="backBtn_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="excecuteBtn" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" style="text-align: center" Text="Execute" Width="122px" OnClick="excecuteBtn_Click"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button15" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" style="text-align: center" Text="Get me outta here !" Width="122px" OnClick="Button15_Click" />
        </p>
            <div style="float: left; width: 101%; text-align: right;">
                <img class="auto-style3" src="assets/girl.PNG" />
        <p class="auto-style2" style="text-align: center">
            &nbsp;</p>
    </div>
    </form>
    </body>
</html>

