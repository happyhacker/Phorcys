<%@ Page Title="DiveType Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Phorcys.Core.DiveType>" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>DiveType Details</h1>

    <ul>
		<li>
			<label for="DiveType_Title">Title:</label>
            <span id="DiveType_Title"><%= Server.HtmlEncode(ViewData.Model.Title.ToString()) %></span>
		</li>
		<li>
			<label for="DiveType_Notes">Notes:</label>
            <span id="DiveType_Notes"><%= Server.HtmlEncode(ViewData.Model.Notes.ToString()) %></span>
		</li>
		<li>
			<label for="DiveType_Created">Created:</label>
            <span id="DiveType_Created"><%= Server.HtmlEncode(ViewData.Model.Created.ToString()) %></span>
		</li>
		<li>
			<label for="DiveType_LastModified">LastModified:</label>
            <span id="DiveType_LastModified"><%= Server.HtmlEncode(ViewData.Model.LastModified.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<DiveTypesController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
