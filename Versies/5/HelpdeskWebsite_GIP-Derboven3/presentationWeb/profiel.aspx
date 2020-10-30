﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="profiel.aspx.cs" Inherits="presentationWeb.profiel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main_content_wrapper" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12">
                <ol class="breadcrumb" id="ol_f868_0">
                    <li>
                        <i class="fa fa-home"></i>&nbsp Account </li>
                    <li>
                        <i class="fa fa-user"></i>&nbsp; Profiel
                    </li>
                </ol>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12 col-xs-12" id="div_979c_0">
                <h4>Profiel</h4>
                <div class="col-sm-10 col-xs-12" id="div_f868_0">
                    <p>Hier kun je je persoonlijke gegevens aanpassen.</p>
                    <a href="reset-passwoord.aspx" class="btn-link">Verander je passwoord</a>
                </div>
                <div class="col-lg-2 col-xs-12 text-center" id="div_f868_1">
                    <asp:Button runat="server" ID="opslaan" type="submit" class="btn btn-success btn-block" Text="Opslaan" OnClick="opslaan_Click"></asp:Button>
                    <p></p>
                </div>
            </div>
        </div>
        <!-- Error Alert -->
        <div runat="server" id="errordiv" visible="false" class="alert alert-danger">
            <p class="style-Alert">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </p>
        </div>

        <div class="row">
            <div class="col-lg-12 col-xs-12">
                <div class="panel">
                    <!--  panel-kasodesk -->
                    <div class="panel-body">
                        <input type="hidden" name="userid" value="x">
                        <div class="row p-bottom-20">
                            <div class="col-lg-5 col-xs-12">
                                <div class="form-group">
                                    <label for="email">E-mail</label>
                                    <asp:TextBox runat="server" ID="txtemail" class="form-control" value=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2 col-xs-12">
                                <div class="form-group">
                                    <label for="language">Taal</label>
                                    <select name="language" class="form-control" disabled>
                                        <option value="nl" selected>Nederlands</option>
                                        <option value="en">English</option>
                                        <option value="fr">Francais</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row p-bottom-20">
                            <div class="col-lg-5 col-xs-12">
                                <div class="form-group">
                                    <label for="">Gebruikersnaam</label>
                                    <asp:TextBox runat="server" type="text" ID="txtgebruikersnaam" class="form-control" value=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-5 col-xs-12">
                                <div class="form-group">
                                    <label for="registerdate">Registratiedatum</label>
                                    <asp:TextBox runat="server" type="text" ID="txtregisterdate" class="form-control" value="" ReadOnly></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row p-bottom-20">
                            <div class="col-lg-8 col-xs-12">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="meldingen" checked disabled>
                                        Nieuwe meldingen gemaild krijgen
                                    </label>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
