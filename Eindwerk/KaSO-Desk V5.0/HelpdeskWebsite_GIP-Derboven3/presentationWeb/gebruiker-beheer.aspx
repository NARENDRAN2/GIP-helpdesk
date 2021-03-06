﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="gebruiker-beheer.aspx.cs" Inherits="presentationWeb.gebruiker_beheer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main_content_wrapper" runat="server">
    <link rel="stylesheet" href="/CSS/gridview-opmaak.css" />
    <link rel="stylesheet" href="/Plugins/fontawesome.min.css">

    <script type="text/javascript" src="/Plugins/jquery.min.js"></script>
    <script type="text/javascript" src="/Plugins/jquery.quicksearch.js"></script>
    <script type="text/javascript">
    $(function () {
        $('.search_textbox').each(function (i) {
            $(this).quicksearch("[id*=gvGebruikers] tr:not(:has(th))", {
                'testQuery': function (query, txt, row) {
                    return $(row).children(":eq(" + (i+2) + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                }
            });
        });
    });
    </script>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12">
                <ol class="breadcrumb" id="ol_40ac_0">
                    <li>
                        <i class="fa fa-chevron-right"></i>&nbsp Gebruikers </li>
                    <li>
                        <i class="fa fa-book"></i>&nbsp Beheer
                    </li>
                </ol>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12 col-xs-12">
                <h4>Beheer gebruikers</h4>
                <div class="col-sm-10 col-xs-12" id="div_40ac_0">
                    <p>Verander de status of instellingen van gebruikers hieronder.</p>
                </div>
            </div>
        </div>
                        <!-- Error Alert -->
        <div runat="server" id="errordiv" visible="false" class="alert alert-danger">
            <p class="style-Alert"><asp:Label ID="lblError" runat="server" Text=""></asp:Label></p>
        </div>

        <div class="row">
            <div class="col-lg-12">
            </div>
        </div>

                <div class="row">
            <div class="col-lg-12 col-xs-12">
                 <div class="panel "> <!--  panel-kasodesk -->
                <div class="panel-body">
                    <div class="input-group">
                    Filters: &nbsp
                    <asp:TextBox  style="padding: 2px;" class="Form-control search_textbox" placeholder='ID' onfocus="this.style.background='#FFFFFF'" onblur="this.style.background='#FFFFFF'" value="" ID="txtID" runat="server" ></asp:TextBox>&nbsp&nbsp
                        <asp:TextBox style="padding: 2px;" class="Form-control search_textbox" placeholder='Gebruikersnaam' onfocus="this.style.background='#FFFFFF'" onblur="this.style.background='#FFFFFF'" value="" ID="txtNaam" runat="server"></asp:TextBox>&nbsp&nbsp
                        <asp:TextBox style="padding: 2px;" class="Form-control search_textbox" placeholder='Email' onfocus="this.style.background='#FFFFFF'" onblur="this.style.background='#FFFFFF'" value="" ID="txtEmail" runat="server"></asp:TextBox>
                        </div>
                    </div>
                     </div>
                </div>
                    </div>

        <div class="table-responsive table-curved">

            <asp:GridView CssClass="gridviewcss" ID="gvGebruikers" runat="server"
                DataKeyNames="ID"
                AutoGenerateColumns="False" Width="100%"
                OnDataBound="gvGebruikers_DataBound"
                OnRowCancelingEdit="gvGebruikers_RowCancelingEdit"
                OnRowDeleting="gvGebruikers_RowDeleting"
                OnRowEditing="gvGebruikers_RowEditing"
                OnRowUpdating="gvGebruikers_RowUpdating"
                EmptyDataText="Geen gebruikers."
                OnRowDataBound="gvGebruikers_RowDataBound"
                OnSelectedIndexChanged="gvGebruikers_SelectedIndexChanged"
                OnRowCommand="gvGebruikers_RowCommand">
                <AlternatingRowStyle CssClass="AltRow" />
                <Columns>
                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="0.7%">
                        <ItemTemplate>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="" Style="color: red;" class="btn btn-mini">
                                <i class="fa fa-user-times fa-lg"></i>
                            </asp:LinkButton>
                        </ItemTemplate>

<ItemStyle Width="0.7%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="5.5%">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="" Style="color: darkgreen;" class="btn btn-mini">
                                    <i class="fa fa-edit fa-lg">&nbsp</i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="" Style="color: darkgreen;">
                                &nbsp&nbsp&nbsp&nbsp<i class="fa fa-check-circle fa-lg">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" Text=" " Style="color: darkred;">
                                <i class="fa fa-ban fa-lg"></i>
                            </asp:LinkButton>
                        </EditItemTemplate>

<ItemStyle Width="5.5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="id #" InsertVisible="False" ReadOnly="True" Visible="true" ItemStyle-Width="5%" >
<ItemStyle Width="5%"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Naam" ItemStyle-Width="12%">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditNaam" runat="server" Text='<%# Bind("naam") %>'></asp:TextBox>
                            <asp:Label ID="lblnaam" runat="server" Visible="false" Text='<%# Bind("naam") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblnaam" runat="server" Text='<%# Bind("naam") %>'></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="12%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E-mail" ItemStyle-Width="17%">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEmail" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                            <asp:Label ID="lblEditEmail" runat="server" Visible="false" Text='<%# Bind("email") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="17%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role" ItemStyle-Width="7%">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditGroep" runat="server"
                                DataTextField="groep"
                                DataValueField="Id"
                                AppendDataBoundItems="true"
                                SelectMethod="ddlEditGroepen_Data"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblGroep" runat="server" Text='<%# Bind("groep") %>'></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="7%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actief" ItemStyle-Width="5%">
                        <EditItemTemplate>
                            <asp:CheckBox ID="cbEditActief" AutoPostBack="True" runat="server" Checked='<%# Bind("actief") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("actief") %>' Enabled="false" />
                        </ItemTemplate>

<ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registratiedatum" ItemStyle-Width="15%">
                        <EditItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("dtmGeregistreerd") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("dtmgeregistreerd") %>'></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" InsertVisible="false">
                        <ItemTemplate>
                            <asp:Button ID="Button1"  runat="server" CausesValidation="false" CommandName="rp" Enabled="false" Text="Reset Passwoord" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="Button1"  runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ResetPasswoord" Text="Reset Passwoord" />
                        </EditItemTemplate>
                        <ItemStyle Width="7%" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>