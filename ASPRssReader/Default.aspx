<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPRssReader._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Content from the web site:</h3>
    <div style="max-height:350px; overflow:auto">
        <asp:GridView ID="gvRss" runat="server" AutoGenerateColumns="false" ShowHeader="false" Width="90%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="5">
                            <tr>
                                <td>
                                    <h3 style="color:#0094ff"><%#Eval("Title") %>></h3>
                                </td>
                                <td width="200px">
                                    <%#Eval("PublishedDate")  %>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                    <%#Eval("Description")  %>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td align="right">
                                    <a href='<%#Eval("Link") %>' target="_blank">Read More....</a>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
