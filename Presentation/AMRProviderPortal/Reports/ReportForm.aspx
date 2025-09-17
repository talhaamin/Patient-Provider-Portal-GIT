<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportForm.aspx.cs" Inherits="AMRPatientPortal.Views.Report.ReportForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <script>

         function HideLoader() {

             window.parent.document.getElementById('page_loader_Report').style.display = 'none';
         }
         function ShowLoader() {
             parent.document.getElementById('page_loader_Report').style.display = 'block';
         }


    </script>
</head>
<body  onload="HideLoader();">
    <form id="reportForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <%--<rsweb:ReportViewer ID="mainReportViewer" runat="server" Width="700px" Height="600px">
        </rsweb:ReportViewer>--%>
        <rsweb:ReportViewer ID="mainReportViewer" runat="server" Height="505px" Width="891px"></rsweb:ReportViewer>

        <div></div>
    </div>
    </form>
</body>
</html>
