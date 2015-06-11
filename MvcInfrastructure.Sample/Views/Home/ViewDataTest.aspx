<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViewDataTest
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ViewDataTest</h2>
    <%= ViewData.Eval("User.Name") %><br />
    <%= ViewData.Eval("User.address.city") %><br />
    <%= (ViewData.Eval("User.listdata") as IList).Count %><br />
</asp:Content>
