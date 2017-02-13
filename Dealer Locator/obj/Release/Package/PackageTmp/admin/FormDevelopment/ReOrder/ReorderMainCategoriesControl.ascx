<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReorderMainCategoriesControl.ascx.cs" Inherits="Dealer_Locator.admin.Admin.ReorderMainCategoriesControl" %>
 <%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

       <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        </asp:ScriptManagerProxy>
    <table class="navHeader contentHeader" cellpadding="3">
        <tr>
            <td>
                Reorder Main Categories
            </td>
        </tr>
    </table>
        <br />
        <table   cellpadding="3" cellspacing="0" class="contentContainer">
            <tr>
                <td style="width: 400px; border-right: none;" valign="top" align="center">
                    <center>
                        &nbsp;<br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <div class="reorderListDemo">
                                        <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataSourceID="SqlDataSource1"
                                            PostBackOnReorder="False" SortOrderField="position" CallbackCssStyle="callbackStyle">
                                            <ItemTemplate>
                                                <div class="itemArea">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("categoryName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <DragHandleTemplate>
                                                <div class="dragHandle">
                                                </div>
                                            </DragHandleTemplate>
                                            <ReorderTemplate>
                                                <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
                                            </ReorderTemplate>
                                        </cc1:ReorderList>

                                        <asp:SqlDataSource ID="SqlDataSource1" 
  SelectCommand="SELECT * FROM [DL.MainCategory] ORDER BY position ASC"

  UpdateCommand="UPDATE [DL.MainCategory] SET position = @position WHERE pk_mainCatID = @pk_mainCatID"
  
  ConnectionString="<%$ ConnectionStrings:Production %>"
  RunAt="server"> </asp:SqlDataSource>

                                                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter" UpdateMethod="UpdatePosition_2">
                                                        <InsertParameters>
                                                            <asp:Parameter Name="pk_mainCatID" Type="Int32" />
                                                            <asp:Parameter Name="categoryName" Type="String" />
                                                            <asp:Parameter Name="disable" Type="Boolean" />
                                                            <asp:Parameter Name="position" Type="Int32" />
                                                            <asp:Parameter Name="dockMenuImageUrlLarge" Type="String" />
                                                            <asp:Parameter Name="dockMenuImageUrlSmall" Type="String" />
                                                            <asp:Parameter Name="dockMenuTitle" Type="String" />
                                                        </InsertParameters>
                                                        <UpdateParameters>
                                                            <asp:Parameter Name="position" Type="Int32" />
                                                            <asp:Parameter Name="pk_mainCatID" Type="Int32" />
                                                        </UpdateParameters>
                                                    </asp:ObjectDataSource>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;

                    </center>
                </td>
                <td valign="middle" style="border-left: none;">
                    <br />
                    <br />
                </td>
            </tr>
        </table>
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert"
                                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter"
                                            UpdateMethod="UpdatePosition">
                                            <UpdateParameters>
                                                <asp:Parameter Name="position" Type="Int32" />
                                                <asp:Parameter Name="pk_mainCatID" Type="Int32" />
                                                <asp:Parameter Name="categoryName" Type="String" />
                                                
                                                <asp:Parameter Name="disable" Type="Boolean" />
                                                <asp:Parameter Name="dockMenuImageUrlLarge" Type="String" />
                                                <asp:Parameter Name="dockMenuImageUrlSmall" Type="String" />
                                                <asp:Parameter Name="dockMenuTitle" Type="String" />
                                                                                                
                                            </UpdateParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="pk_mainCatID" Type="Int32" />
                                                <asp:Parameter Name="categoryName" Type="String" />
                                                <asp:Parameter Name="disable" Type="Boolean" />
                                                <asp:Parameter Name="position" Type="Int32" />
                                            </InsertParameters>
                                        </asp:ObjectDataSource>