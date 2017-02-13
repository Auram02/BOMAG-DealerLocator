<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Login.aspx.cs" Inherits="Dealer_Locator.admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        body 
        {
            font-family: "Trebuchet MS", Arial;
            font-size: 11px;
            
        }
        
        .loginButtonStyle
        {
        
        margin:4px;
        
        }
    </style>
    <title>Dealer Locator Admin - Login</title>
</head>
<body bgcolor="#3D51AA">
    <form id="form1" runat="server">
        <center>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <table bgcolor="white" style="border: solid 1px black;">
                <tr>
                    <td>
                        <img src="/admin/Images/login_world.jpg" />
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <img src="/admin/Images/login_bomag.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" DisplayRememberMe="False"
                                        LoginButtonText="Login" LoginButtonStyle-CssClass="loginButtonStyle" TitleText="" PasswordLabelText="Password  " UserNameLabelText="User Name">
                                        <LoginButtonStyle CssClass="loginButtonStyle" />
                                    </asp:Login>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right:8px;">
                                Copyright © 2007 BOMAG AMERICA
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
