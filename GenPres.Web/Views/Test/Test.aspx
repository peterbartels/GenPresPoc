<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

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
   
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Client/Library/extjs/resources/css/ext-all.css") %>" /> 
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Client/Tests/lib/jasmine-1.0.2/jasmine.css") %>" />
    
    <script type="text/javascript" src="<%=Url.Content("~/Client/Tests/lib/jasmine-1.0.2/jasmine.js") %>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Client/Tests/lib/jasmine-1.0.2/jasmine-html.js") %>"></script>

    <script type="text/javascript">
        Ext = {}; 
        Ext.app = {};
    </script>    
    
    <script type="text/javascript" src="<%= Url.Content("~/Direct/Api")  %>"></script>
    
    <script type="text/javascript">
        var remoteApi = Ext.app.REMOTING_API;
    </script>    
    
    <script type="text/javascript" src="<%= Url.Content("~/Client/Library/extjs/bootstrap.js")  %>"></script>

    
    <script type="text/javascript">
        
        Ext.require('Ext.direct.*');
        
        Ext.Loader.setConfig({
            enabled: true,
            disableCaching: true
        });

        Ext.onReady(function() {
            Ext.direct.Manager.addProvider(remoteApi);
            var runTests = function() {
                jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
                jasmine.getEnv().execute();
            }
            Ext.Function.defer(runTests, 2000);
        });
        
    </script>
    
    <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/app/Application.js")  %>"></script>
    
    <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/test/ApplicationTest.js")  %>"></script>
    
</head>

<body>
<div id="TestControls"></div>
</body>
</html>
