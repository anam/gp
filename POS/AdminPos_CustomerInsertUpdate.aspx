<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="AdminPos_CustomerInsertUpdate.aspx.cs" Inherits="AdminPos_CustomerInsertUpdate"
    Title="Pos_Customer Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableCss
        {
            text-align: left;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function checkFileExtension(elem) {
            var filePath = elem.value;

            if (filePath.indexOf('.') == -1)
                return false;

            var validExtensions = new Array();
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
            //Add valid extentions in this array
            validExtensions[0] = 'jpg';
            validExtensions[1] = 'png';
            validExtensions[2] = 'gif';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i])
                    return true;
            }

            alert('The file extension ' + ext.toUpperCase() + ' is not allowed!. Only allow (.jpg,.png,.gif) extension images.');
            elem.value = "";
            return false;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tableCss">
        <asp:HiddenField ID="hfLoginID" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblBranchNo" runat="server" Text="Branch"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name: ">
                    </asp:Label>
                    <asp:RequiredFieldValidator ID="CustomerNameValidator" runat="server" ControlToValidate="txtCustomerName"
                        ValidationGroup="customerInfo" SetFocusOnError="true" ForeColor="red" Display="Dynamic"
                        ErrorMessage="Please enter Customer name." Text="*" />
                </td>
                <td>
                    <asp:TextBox ID="txtCustomerName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddress" runat="server" Text="Address: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" Text="" Width="300px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCompanyName" runat="server" Text="Company Name: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOfficeAddress" runat="server" Text="Office Address: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOfficeAddress" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContactNo" runat="server" Text="Mobile No: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContactNo" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone" runat="server" Text="Phone Number: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDesignation" runat="server" Text="Designation: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdesignation" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOccupation" runat="server" Text="Occupation: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOccupation" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDateofBirth" runat="server" Text="Date Of Birth: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDateofBirth" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server"
                            TargetControlID="txtDateofBirth">
                        </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblApplicationDate" runat="server" Text="Application Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtApplicationDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server"
                            TargetControlID="txtApplicationDate">
                        </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCardNo" runat="server" Text="Card No: ">
                    </asp:Label>
                    <asp:RequiredFieldValidator ID="CardValidator" runat="server" ControlToValidate="txtCardNo"
                        ValidationGroup="customerInfo" SetFocusOnError="true" ForeColor="red" Display="Dynamic"
                        ErrorMessage="Please enter Card No" Text="*" />
                </td>
                <td>
                    <asp:TextBox ID="txtCardNo" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCardIssueDate" runat="server" Text="Card Issue Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCardIssueDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender3" runat="server"
                            TargetControlID="txtCardIssueDate">
                        </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExpireDate" runat="server" Text="Expire Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExpireDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender4" runat="server"
                            TargetControlID="txtExpireDate">
                        </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCardType" runat="server" Text="Card Type: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCardType" runat="server">
                    <asp:ListItem Value="Gold">Gold</asp:ListItem>
                    <asp:ListItem Value="Golden Plus 10%">Golden Plus 10%</asp:ListItem>
                    <asp:ListItem Value="Silver">Golden Plus 20%</asp:ListItem>
                    <asp:ListItem Value="Silver">Silver</asp:ListItem>
                    <asp:ListItem Value="Silver">Silver</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDiscountPersent" runat="server" Text="Discount: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDiscountPersent" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSignature" runat="server" Text="Signature Upload: ">
                    </asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtSignature" runat="server" Text="">
                    </asp:TextBox>--%>
                    <asp:FileUpload ID="uplFile" runat="server" Width="90%" onchange="checkFileExtension(this);" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtApprovedBy" runat="server" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRemark" runat="server" Text="Remark: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" Text="" TextMode="MultiLine" Rows="5">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReferenceID" runat="server" Text="Reference: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReference" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="customerInfo"
                        CausesValidation="true" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
