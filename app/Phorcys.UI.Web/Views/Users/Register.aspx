<%@ Page Title="Register" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Phorcys.Web.Controllers.UsersController.UserFormViewModel>" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Register</h1>

	<%// Html.RenderPartial("UserForm", ViewData); %>

 <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% Html.EnableClientValidation(); %>
<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <br />
    <%= Html.Hidden("User.Id", (ViewData.Model.User != null) ? ViewData.Model.User.Id : 0)%>
      
      <label>Login ID</label>
			<div><%= Html.TextBoxFor(m => m.User.LoginId) %></div>
			<%= Html.ValidationMessageFor(m => m.User.LoginId) %>
      <br />

			<%= Html.LabelFor(m => m.User.Password) %>
			<div><%= Html.PasswordFor(m => m.User.Password) %></div>
			<%= Html.ValidationMessageFor(m => m.User.Password) %>
     
      <br />
      <%= Html.SubmitButton("btnSave", "OK") %>
      <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
		    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<UsersController>(c => c.Index()) + "';") %>
      
<% } %>


</asp:Content>
