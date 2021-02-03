<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Changepwd.aspx.cs" Inherits="AS_Practical_1.Changepwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_newpassword.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById("pwd_checker").innerHTML = "Password Length must be at least 8 characters long";
                document.getElementById("pwd_checker").style.color = "Red";
                return ("too_short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("pwd_checker").innerHTML = "Password requires at least 1 number";
                document.getElementById("pwd_checker").style.color = "Red";
                return ("no_number");
            }
            else if (!/[A-Z]/.test(str)) {
                document.getElementById("pwd_checker").innerHTML = "Password requires at least 1 uppercase";
                document.getElementById("pwd_checker").style.color = "Red";
                return ("no_uppercase");
            }
            else if (!/[a-z]/.test(str)) {
                document.getElementById("pwd_checker").innerHTML = "Password requires at least 1 lowercase";
                document.getElementById("pwd_checker").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("pwd_checker").innerHTML = "Password requires at least 1 special character";
                document.getElementById("pwd_checker").style.color = "Red";
                return ("no_specialcha")
            }
            document.getElementById("pwd_checker").innerHTML = "Perfect!";
            document.getElementById("pwd_checker").style.color = "Green";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>Change Password</legend>
            <div>
                <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UserID :
                <asp:TextBox ID="tb_user" runat="server" BorderStyle="Solid"></asp:TextBox>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="old password :"></asp:Label>
    &nbsp;<asp:TextBox ID="tb_oldpwd" runat="server" Height="16px" BorderStyle="Solid"></asp:TextBox>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                new password : <asp:TextBox ID="tb_newpassword" runat="server" BorderStyle="Solid"></asp:TextBox>
                <br />
                Retype new password :
                <asp:TextBox ID="tb_newpwdretype" runat="server" Height="16px" BorderStyle="Solid"></asp:TextBox>
                <br />
                <asp:Label ID="pwd_checker" runat="server" Text="Password Checker"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
                <br />
                <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btn_newpwd" runat="server" Text="Change Password" Width="280px" OnClick="btn_newpwd_Click" />
            </div>
        </fieldset>
    </form>
</body>
</html>
