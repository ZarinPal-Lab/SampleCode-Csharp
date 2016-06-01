<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Request.aspx.cs" Inherits="Request" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        مبلغ<asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
        <br />
        توضیحات<asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnRequest" runat="server" onclick="btnRequest_Click" 
            Text="Request" />
    </div>
    </form>
</body>
</html>
