<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="debug.aspx.cs" Inherits="GenPres.debug" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd"> 
<meta http-equiv="X-UA-Compatible" content="IE=100" >
<html>
<head id="Head1" runat="server">
    
    <title>GenPres Debug</title>
    
    <link rel="stylesheet" type="text/css" href="Client/Library/extjs/resources/css/ext-all.css" /> 
    <link rel="stylesheet" type="text/css" href="Client/GenPres/style/css/GenPres.css" /> 
    
    <script type="text/javascript">
        Ext = {};
        Ext.app = {};
    </script>    
    
    <script type="text/javascript" src="Direct/Api"></script>
    
    <script type="text/javascript">
        var remoteApi = Ext.app.REMOTING_API;
    </script>    
    
    <script type="text/javascript" src="Client/Library/extjs/bootstrap.js"></script>

    
    <script type="text/javascript">

        Ext.require('Ext.direct.*');

        Ext.Loader.setConfig({
            enabled: true,
            disableCaching: true
        });

        Ext.onReady(function () {
            Ext.direct.Manager.addProvider(remoteApi);
        });
        
    </script>
    
    <script type="text/javascript" src="Client/GenPres/app/Application.js"></script>
    
</head>

<body style="margin: 0px 0px 0px 0px;">
    <div style="z-index:2222222;" id="topzindexdiv"></div>
    <div class="page">

        </div>

        <div id="main">
            <div id="footer">
            </div>
        </div>
    </div>
</body>
</html>
