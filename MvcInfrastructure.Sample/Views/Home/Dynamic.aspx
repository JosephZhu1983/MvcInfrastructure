<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dynamic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Dynamic</h2>
 <form action="<%= ResolveUrl("~/Home.mvc/API") %>" method="post">
            <input type="button" id="postButton" value="post" />
        </form>
        
        
        <script language"javascript" type="text/javascript">
            $().ready(function() {
                $("#postButton").bind("click", function(e) {                                        
                    $.post("<%=ResolveUrl("~/Home.mvc/API") %>", function(data) {
                        alert(data.Name + ' ' + data.Age);
                    });
                });
            });
        </script>
</asp:Content>
