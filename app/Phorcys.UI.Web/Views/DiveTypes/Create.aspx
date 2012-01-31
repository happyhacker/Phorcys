<%@ Page Title="Create DiveType" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Phorcys.Web.Controllers.DiveTypesController.DiveTypeFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Create DiveType</h1>

	<% Html.RenderPartial("DiveTypeForm", ViewData); %>

</asp:Content>
