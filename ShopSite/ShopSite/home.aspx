<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ShopSite.home" %>

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
        </style>
</head>
<body>
    <form id="form1" runat="server">

    <div style="float: left; width: 101%; text-align: center;">
        <img class="auto-style1" src="assets/banner.PNG" /></div>
    <p style="text-align: center">
        Here at Crazy Melvin&#39;s we beleive in selling things cheap !! That&#39;s why our User Interface is cheap !</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p style="text-align: center">
            Use the buttons below to tell me what you&#39;d like to do here at Crazy Melvin&#39;s ??</p>
        <p style="text-align: center">
            &nbsp;</p>
        <p style="text-align: center">
            &nbsp;</p>
        <p class="auto-style2" style="text-align: center">
            <asp:Button ID="searchBtn" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" Text="Search" Width="122px" OnClick="searchBtn_Click"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="insertBtn" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" Text="Insert some Stuff" Width="122px" OnClick="insertBtn_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="updateBtn" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" Text="Update some Stuff" Width="122px" OnClick="updateBtn_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="deleteBtn" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" Text="Delete some Stuff" Width="122px" OnClick="deleteBtn_Click" />
        </p>
        <p class="auto-style2">
            &nbsp;</p>
        <p class="auto-style2">
            &nbsp;</p>
        <p class="auto-style2">
            <asp:Button ID="Button15" runat="server" BackColor="#0000CC" BorderColor="Black" ForeColor="White" Height="42px" style="text-align: center" Text="Get me outta here !" Width="122px" OnClick="Button15_Click" />
        </p>
            <div style="float: left; width: 101%; text-align: right;">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/assets/girl.PNG" OnClick="ImageButton1_Click" />
&nbsp;<p class="auto-style2" style="text-align: center">
            &nbsp;</p>
    </div>
    </form>
    </body>
</html>
