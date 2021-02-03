<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AS_Practical_1.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SITConnect Registration</title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=TB_pwd.ClientID %>').value;

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
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
</head>
<body style="background: #76b852; background: -webkit-linear-gradient(right, #76b852, #8DC26F);  background: -moz-linear-gradient(right, #76b852, #8DC26F);
background: -o-linear-gradient(right, #76b852, #8DC26F); background: linear-gradient(to left, #76b852, #8DC26F); font-family: "Roboto", sans-serif; -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;">
    <form id="form1" runat="server">
        <div class="form" style="position: relative; z-index: 1; background: #FFFFFF; max-width: 360px; margin: 0 auto 100px; padding: 45px; text-align: center; box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);">
            Registration<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="First Name - " style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Label>
&nbsp;<asp:TextBox ID="TB_Fname" runat="server" BorderStyle="Solid" CssClass="auto-style1" Width="194px" Height="24px"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Last Name - " style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;" ></asp:Label>
&nbsp;<asp:TextBox ID="TB_Lname" runat="server" BorderStyle="Solid"  CssClass="auto-style1" Width="194px" Height="24px"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;<asp:Label ID="Label3" runat="server" Text="Date of Birth - " style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;" ></asp:Label>
            <asp:TextBox ID="TB_DoB" runat="server" BorderStyle="Solid" TextMode="DateTime"  Width="194px" Height="24px"></asp:TextBox>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="CC Number - " style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;" ></asp:Label>
&nbsp;<asp:TextBox ID="TB_cardnum" runat="server" Width="196px" BorderStyle="Solid"  Height="23px"></asp:TextBox>
&nbsp;<br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="Label5" runat="server" Text="Email - " style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Label>
            <asp:TextBox ID="TB_email" runat="server" BorderStyle="Solid" TextMode="Email"  Width="198px" Height="25px"></asp:TextBox>
            &nbsp;<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Text="Password - " style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Label>
&nbsp;<asp:TextBox ID="TB_pwd" runat="server" onkeyup="javascript:validate()" TextMode="Password" BorderStyle="Solid"  Width="192px" Height="21px"></asp:TextBox>
            &nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="pwd_checker" runat="server" Text="Password Checker" style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Label>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="Create" Width="249px" BorderStyle="Solid" style="background: #4CAF50; padding: 15px; color: #FFFFFF; font-size: 14px;
-webkit-transition: all 0.3 ease; transition: all 0.3 ease; cursor: pointer; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"/>
            <br />
            <br />
&nbsp;<asp:Label ID="lbl_message" runat="server" style="font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></asp:Label>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
