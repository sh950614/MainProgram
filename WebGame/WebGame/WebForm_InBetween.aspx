<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm_InBetween.aspx.cs" Inherits="WebGame.WebForm_InBetween" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="Label_Hello" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/poker/bicycle_backs.jpg" Height="240px" Width="180px" />
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Resources/poker/bicycle_backs.jpg" Height="240px" Width="180px" />
    <asp:Image ID="Image2" runat="server" ImageUrl="~/Resources/poker/bicycle_backs.jpg" Height="240px" Width="180px" /><br />
    
    <!--
    <input type="image" src="Resources/poker/bicycle_backs.jpg" width="180" height="240" name="image1">
    <input type="image" src="Resources/poker/bicycle_backs.jpg" width="180" height="240" name="image3">
    <input type="image" src="Resources/poker/bicycle_backs.jpg" width="180" height="240" name="image2"><br />
    -->

    <asp:Button ID="Button_Start" runat="server" OnClick="Button_Start_Click" Text="Start" />
    <asp:Button ID="Button_Bet" runat="server" OnClick="Button_Bet_Click" Text="Bet" />    
    <asp:Button ID="Button_Pass" runat="server" OnClick="Button_Pass_Click" Text="Pass" /> 
    <br />
    <asp:Label ID="Label3" runat="server" Text="NowCoin: "></asp:Label>
    <asp:Label ID="Label_NowCoin" runat="server"  Text=50000></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="BetCoin: "></asp:Label>
    <asp:TextBox ID="TextBox_BetCoin" runat="server" Text=1000></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="MaxCoin: "></asp:Label>
    <asp:Label ID="Label_MaxCoinRecord" runat="server" Text=50000></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label_Count" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label_Result" runat="server"></asp:Label>   

   

    
</asp:Content>
