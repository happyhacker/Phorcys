<%@ Page Title="Login" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Phorcys.Web.Controllers.UsersController.UserFormViewModel>" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Login</h1>

  <form action="/Users/Login/" method="post" id="form1">
  <fieldset>
      User Id: <input name="LoginId" id="LoginId" size="30" maxlength="30" /><br />
      <br />
      Password: <input type="password" name="password" id="password" size="30" maxlength="30" /><br />
      <br />
      <input type="submit" value="Submit" id="submit" />
    </fieldset>
  </form>
  <br />
  <p>Please enter your ID and password or <%= Html.ActionLink<UsersController>(c => c.Register(), "Register") %> if you don't have an account.</p>
</asp:Content>
