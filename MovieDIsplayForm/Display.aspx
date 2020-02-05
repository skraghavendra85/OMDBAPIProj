<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="MovieDIsplayForm.Display" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tbody>
                    <tr>
                        <td> Search Keyword</td>
                        <td>
                            <asp:TextBox ID="txtSearchKey" runat="server"></asp:TextBox>
                            

                        </td>
                        <td>
                            Year
                        </td>
                        <td>
                            <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
                            
                        </td>
                        <td>
                            Plot
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlot" runat="server"></asp:TextBox>
                            
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" ValidationGroup="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RegularExpressionValidator runat="server" ID="rgvYear" ControlToValidate="txtYear" ValidationExpression="^(19[2-9]\d|20[0-4]\d|2050)|\s$" ValidationGroup="Search" ForeColor="Red" ErrorMessage="Year should be in the range from 1920-2019"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RegularExpressionValidator runat="server" ID="rgvPlot" ControlToValidate="txtPlot" ValidationExpression="^(short|full)|\s$" ValidationGroup="Search" ForeColor="Red" ErrorMessage="Can have values short or full"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator ID="rqvSearch" runat="server" ControlToValidate="txtSearchKey" ValidationGroup="Search" ForeColor="Red" ErrorMessage="Search KeyWord is Required!!"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <asp:GridView ID="grdMovieData" runat="server" AllowSorting="true">

            </asp:GridView>
        </div>
    </form>
</body>
</html>
