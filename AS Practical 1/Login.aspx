<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AS_Practical_1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LeKvzkaAAAAAH6aCLSGDgc1GSw1C5fJOLmU_GjZ"></script>
</head>
<body style="background: #76b852; background: -webkit-linear-gradient(right, #76b852, #8DC26F);  background: -moz-linear-gradient(right, #76b852, #8DC26F);
background: -o-linear-gradient(right, #76b852, #8DC26F); background: linear-gradient(to left, #76b852, #8DC26F); font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;">
    <form id="form1" runat="server" style="position: relative; z-index: 1; background: #FFFFFF; max-width: 360px; margin: 0 auto 100px; padding: 45px; text-align: center; box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);">
        <div>
            <fieldset>
                <legend style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;">Login</legend>
                <br />
                Username :
                <asp:TextBox ID="tb_user" runat="server" Height="16px" BorderStyle="Solid"></asp:TextBox>
                <br />
                <br />
                Password :
                <asp:TextBox ID="tb_pwd" runat="server" BorderStyle="Solid" TextMode="Password" Width="126px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Login_btn" runat="server" Text="Login" Onclick="Logmein" Width="128px" BorderStyle="Solid" style="background: #4CAF50; padding: 15px; color: #FFFFFF; font-size: 14px;
-webkit-transition: all 0.3 ease; transition: all 0.3 ease; cursor: pointer; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Button>
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Registeration" Onclick="Register" Width="128px" BorderStyle="Solid" style="background: #4CAF50; padding: 15px; color: #FFFFFF; font-size: 14px;
-webkit-transition: all 0.3 ease; transition: all 0.3 ease; cursor: pointer; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Button>
                <br />
                <br />

                <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>

                <asp:Label ID="lbl_gscore" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="" style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Label>
                <asp:Label ID="Timer" runat="server" Text="" style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Label>
                <br />
            </fieldset>
        </div>
    </form>

    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LeKvzkaAAAAAH6aCLSGDgc1GSw1C5fJOLmU_GjZ', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
