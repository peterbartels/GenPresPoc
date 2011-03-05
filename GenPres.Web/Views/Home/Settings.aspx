<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Settings.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <!-- ExtJS base -->
    <script language="JavaScript" type="text/javascript" src="<%= Url.Content("~/Client/adapter/ext/ext-base.js") %>"></script>
    <!-- ExtJS base -->

    <!-- ExtJS library -->
    <script language="JavaScript" type="text/javascript" src="<%= Url.Content("~/Client/ext-all.js") %>"></script>
    <!-- ExtJS library -->
    
    <!-- ############## STYLES(THEME) ############## -->
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Client/resources/css/ext-all.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Client/resources/css/xtheme-gray.css") %>" />
    <!-- ############## STYLES(THEME) ############## -->
    
    <script type="text/javascript" src="<%= Url.Content("~/Direct/Api") %>"></script>
    <script>        Ext.Direct.addProvider(Ext.app.REMOTING_API);</script>
    
    <script>

        //this.patientInfo.GetPanel()
        var computerName = new Ext.form.TextField({
            fieldLabel: 'Computernaam',
            width: 200
        });
        var GenPresDB = new Ext.form.TextField({
            fieldLabel: 'GenPres Connectionstring',
            width: 400
        });
        var patientDB = new Ext.form.TextField({
            fieldLabel: 'PatientenDatabase (of PDMS) Connectionstring',
            width:400
        })
        var GenFormWebService = new Ext.form.TextField({
            fieldLabel: 'GenForm Webservice',
            width: 400
        })

        var form = new Ext.FormPanel({
            layout: 'form',
            width: 750,
            labelWidth: 300,
            buttons: [{
                text: 'Submit',
                handler: function() {
                    Settings.SetSetting(computerName.getValue(), "GenPresDBConnectionString", GenPresDB.getValue());
                    Settings.SetSetting(computerName.getValue(), "PatientDBConnectionString", patientDB.getValue());
                    Settings.SetSetting(computerName.getValue(), "GenFormWebService", GenFormWebService.getValue());
                    Ext.Msg.alert('Opgeslagen', "Instellingen zijn opgeslagen!")
                    win.close();
                }
}],
                items: [
                computerName,
                GenPresDB,
                patientDB,
                GenFormWebService   
            ]
            });
        var win = new Ext.Window({
            layout: '',
            width: 800,
            height:500,
            items: form
        });
        win.show();
    </script>
</asp:Content>
