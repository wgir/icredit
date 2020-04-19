<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="R01Tipificaciones.aspx.cs" Inherits="CrediAdmin.Reports._01Tipificaciones" %><%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="568px">
            <ServerReport ReportPath="/CrediAdmin/Reports/01Tipificaciones" ReportServerUrl="http://192.168.2.69/ReportServer" />
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
