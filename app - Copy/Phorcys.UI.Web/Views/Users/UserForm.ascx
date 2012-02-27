<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Phorcys.Web.Controllers.UsersController.UserFormViewModel>" %>
<%@ Import Namespace="Phorcys.Core" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% Html.EnableClientValidation(); %>
<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("User.Id", (ViewData.Model.User != null) ? ViewData.Model.User.Id : 0)%>

    <ul>
		<li>
			<%= Html.LabelFor(m => m.User.LoginId) %>
			<div><%= Html.TextBoxFor(m => m.User.LoginId) %></div>
			<%= Html.ValidationMessageFor(m => m.User.LoginId) %>
		</li>
		<li>
			<%= Html.LabelFor(m => m.User.Password) %>
			<div><%= Html.TextBoxFor(m => m.User.Password) %></div>
			<%= Html.ValidationMessageFor(m => m.User.Password) %>
		</li>
		<li>
			<%= Html.LabelFor(m => m.User.LoginCount) %>
			<div><%= Html.TextBoxFor(m => m.User.LoginCount) %></div>
			<%= Html.ValidationMessageFor(m => m.User.LoginCount) %>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save User") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<UsersController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
