<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Test.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Client/GenPres/style/css/VisibleTests.css") %>" /> 

    <script>
        window.dontLoadApplication = true;
    </script>

    <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/genpres-all.js")  %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/app/Application.js")  %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/test/MainScreen.js")  %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Client/Tests/RunJasmine.js")  %>"></script>
    
</asp:Content>