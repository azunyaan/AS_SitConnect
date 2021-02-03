<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="AS_Practical_1.Homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>Homepage</legend>

                <br />


            &nbsp;
                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                <br />
                <br />
                <br />
                &nbsp;
                <asp:Button ID="btn_updatepwd" runat="server" BorderStyle="Solid" OnClick="btn_updatepwd_Click" Text="Update Password" Width="131px" />
                <br />
&nbsp;<br />
&nbsp;
                <asp:Button ID="btn_logout" runat="server" Text="Logout" Onclick="Logmeout" Width="91px" BorderStyle="Solid" />
                <br />
                <br />


            </fieldset>
        </div>
    </form>
</body>
</html>
