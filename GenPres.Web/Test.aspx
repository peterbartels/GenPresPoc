<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
"http://www.w3.org/TR/html4/loose.dtd">
<html>
<style>
    /*
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
}*/


</style>
<head>
  <title>Test</title>
  
  
  <link rel="stylesheet" type="text/css" href="Client/Library/extjs/resources/css/ext-all.css" /> 
  <link rel="stylesheet" type="text/css" href="Client/GenPres/style/css/GenPres.css" /> 
    
  <!--<link rel="stylesheet" type="text/css" href="Client/Tests/lib/jasmine-1.0.2/jasmine.css" />
    
  <script type="text/javascript" src="Client/Tests/lib/jasmine-1.0.2/jasmine.js"></script>
  <script type="text/javascript" src="Client/Tests/lib/jasmine-1.0.2/jasmine-html.js"></script>
-->
  <!-- include source files here... -->
  <script type="text/javascript" src="Client/Library/ext-all-debug.js"></script>
  <script type="text/javascript" src="Direct/Api"></script>
    
  <script type="text/javascript"> Ext.Direct.addProvider(Ext.app.REMOTING_API);</script>
  <!--
  <script type="text/javascript" src="Client/GenPres/app/model/patient/LogicalUnitModel.js"></script>

  <script type="text/javascript" src="Client/GenPres/app/util/Process.js"></script>
  <script type="text/javascript" src="Client/GenPres/app/util/Spotlight.js"></script>
  
  
  <script type="text/javascript" src="Client/GenPres/app/model/patient/PatientModel.js"></script>
  
  <script type="text/javascript" src="Client/GenPres/app/store/patient/PatientStore.js"></script>
  <script type="text/javascript" src="Client/GenPres/app/store/patient/LogicalUnitStore.js"></script>
  
  
  <script type="text/javascript" src="Client/GenPres/app/view/main/PatientTree.js"></script>
  <script type="text/javascript" src="Client/GenPres/app/view/main/TopToolbar.js"></script>  
  <script type="text/javascript" src="Client/GenPres/app/view/main/MainViewLeft.js"></script>
  <script type="text/javascript" src="Client/GenPres/app/view/main/MainViewCenter.js"></script>
  <script type="text/javascript" src="Client/GenPres/app/view/main/MainView.js"></script>
  
  <script type="text/javascript" src="Client/GenPres/app/view/user/LoginWindow.js"></script>
  
  <script type="text/javascript" src="Client/GenPres/app/controller/user/LoginController.js"></script>
  <script type="text/javascript" src="Client/GenPres/app/controller/patient/PatientController.js"></script>
  
  <script type="text/javascript" src="Client/GenPres/app/Application.js"></script>  

  <script type="text/javascript" src="Client/GenPres/test/ApplicationTest.js"></script>
    -->
    
  <script type="text/javascript" src="Client/GenPres/genpres-all.js"></script>  
  <script type="text/javascript" src="Client/GenPres/app/Application.js"></script>
</head>

<body>

<script type="text/javascript">
/*    Ext.onReady(function() {
        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.getEnv().execute();
    });*/
</script>
<div id="TestControls"></div>
</body>
</html>
