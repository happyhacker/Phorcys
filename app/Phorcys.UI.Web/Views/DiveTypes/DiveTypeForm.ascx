<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Phorcys.Web.Controllers.DiveTypesController.DiveTypeFormViewModel>" %>
<%@ Import Namespace="Phorcys.Core" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% Html.EnableClientValidation(); %>
<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("DiveType.Id", (ViewData.Model.DiveType != null) ? ViewData.Model.DiveType.Id : 0)%>

    <ul>
		<li>
			<%= Html.LabelFor(m => m.DiveType.Title) %>
			<div><%= Html.TextBoxFor(m => m.DiveType.Title) %></div>
			<%= Html.ValidationMessageFor(m => m.DiveType.Title) %>
		</li>
		<li>
			<%= Html.LabelFor(m => m.DiveType.Notes) %>
			<div><%= Html.TextBoxFor(m => m.DiveType.Notes) %></div>
			<%= Html.ValidationMessageFor(m => m.DiveType.Notes) %>
		</li>
    <li>
            <%= Html.SubmitButton("btnSave", "Save DiveType") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<DiveTypesController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
