<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AuthDemo.Forms.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
        <p><asp:Button runat="server" OnClick="Logout_Click" Text="Logout" /></p>
    </div>

    <div class="row">
        <h1>Hi <asp:Label runat="server" ID="UserNameLabel"></asp:Label></h1>
        <p><asp:Button runat="server" OnClick="OpenPopup_Click" Text="View Blazor App" /></p>
    </div>
    
    <script>
        function openWindow() {
            window.open("https://localhost:7160/", "_blank", 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=w, height=h, top=top, left=left');
        }
    </script>
</asp:Content>

