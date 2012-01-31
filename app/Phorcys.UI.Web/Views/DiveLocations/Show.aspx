<%@ Page Title="DiveLocation Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Phorcys.Core.DiveLocation>" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>DiveLocation Details</h1>

    <ul>
		<li>
			<label for="DiveLocation_Title">Title:</label>
            <span id="DiveLocation_Title"><%= Server.HtmlEncode(ViewData.Model.Title.ToString()) %></span>
		</li>
		<li>
			<label for="DiveLocation_Notes">Notes:</label>
            <span id="DiveLocation_Notes"><%= Server.HtmlEncode(ViewData.Model.Notes.ToString()) %></span>
		</li>
		<li>
			<label for="DiveLocation_UserId">UserId:</label>
            <span id="DiveLocation_UserId"><%= Server.HtmlEncode(ViewData.Model.User.UserId.ToString()) %></span>
		</li>
		<li>
			<label for="DiveLocation_Created">Created:</label>
            <span id="DiveLocation_Created"><%= Server.HtmlEncode(ViewData.Model.Created.ToString()) %></span>
		</li>
		<li>
			<label for="DiveLocation_LastModified">LastModified:</label>
            <span id="DiveLocation_LastModified"><%= Server.HtmlEncode(ViewData.Model.LastModified.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<DiveLocationsController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
