<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MFP4._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center">
    <h1 class="display-4">Funktionslösung</h1>
    <p>Geben Sie hier den Therm ein:</p>
    <p>
        <asp:TextBox ID="thermTb" runat="server" Rows="1" Columns="255" Text="12+14"></asp:TextBox>
    </p>
    <asp:Button ID="wurzelBtn" runat="server" OnClick="wurzelBtn_Click" Text="√■" />
    <asp:Button ID="pot2Btn" runat="server" OnClick="pot2Btn_Click" Text="■²" />
    <asp:Button ID="pot3Btn" runat="server" OnClick="pot3Btn_Click" Text="■³" />
    <asp:Button ID="potxBtn" runat="server" OnClick="potxBtn_Click" Text="■ˣ" />
    <asp:Button ID="piBtn" runat="server" OnClick="piBtn_Click" Text="π" />
    <asp:Button ID="parseBtn" runat="server" OnClick="parseBtn_Click" Text="=" />
    <p>Die Lösung lautet:</p>
    <p>
        <asp:Label ID="ergebnisLbl" runat="server" Text="ERGEBNIS..."></asp:Label>

    </p>
    <p>
        Ergebnis wurde in <asp:Label ID="timeLbl" runat="server" Text="..."></asp:Label> Millisekunden erzeugt.
    </p>
</div>

</asp:Content>
