<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        测试
    </h2>
    
    <p>
        TestActionFilter：<%= Html.Encode(ViewData["TestActionFilter"])%>
    </p>
    <p>
        TestController：<%= Html.Encode(ViewData["TestController"]) %></p>
    <p>
        TestPerRequestTask：<%=Context.Items["TestPerRequestTask"]%>
    </p>
    <p>
        TestActionInvoker：<%=Html.Encode(ViewData["TestActionInvoker"])%>
    </p>
</asp:Content>
