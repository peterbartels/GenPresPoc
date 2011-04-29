<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
"http://www.w3.org/TR/html4/loose.dtd">
<html>
<style>
body div
{
	display:none !important;
}
body .jasmine_reporter
{
	display:block !important;
}
body .jasmine_reporter div
{
	display:block !important;
}


</style>
<head>
  <title>Jasmine Test Runner</title>
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Client/Tests/lib/jasmine-1.0.2/jasmine.css") %>" />
    
  <script type="text/javascript" src="<%=Url.Content("~/Client/Tests/lib/jasmine-1.0.2/jasmine.js") %>"></script>
  <script type="text/javascript" src="<%=Url.Content("~/Client/Tests/lib/jasmine-1.0.2/jasmine-html.js") %>"></script>

  <!-- include source files here... -->
  <script type="text/javascript" src="<%= Url.Content("~/Client/Library/Ext4/ext-all-debug.js") %>"></script>
  <script type="text/javascript" src="<%= Url.Content("~/Direct/Api") %>"></script>
    
  <script type="text/javascript"> Ext.Direct.addProvider(Ext.app.REMOTING_API);</script>
  
  <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/app/util/Process.js")  %>"></script>
  <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/app/util/Spotlight.js")  %>"></script>
  
  <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/app/controller/user/LoginController.js")  %>"></script>
  <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/app/view/user/LoginWindow.js")  %>"></script>
  
  <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/app/Application.js") %>"></script>  

  <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/test/ApplicationTest.js") %>"></script>

</head>

<body>

<script type="text/javascript">
    Ext.onReady(function() {
        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.getEnv().execute();
    });
</script>

</body>
</html>
