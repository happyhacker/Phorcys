<%@ Page Title="User Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Phorcys.Core.User>" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>User Details</h1>

    <ul>
		<li>
			<label for="User_LoginId">LoginId:</label>
            <span id="User_LoginId"><%= Server.HtmlEncode(ViewData.Model.LoginId.ToString()) %></span>
		</li>
		<li>
			<label for="User_Password">Password:</label>
            <span id="User_Password"><%= Server.HtmlEncode(ViewData.Model.Password.ToString()) %></span>
		</li>
		<li>
			<label for="User_LoginCount">LoginCount:</label>
            <span id="User_LoginCount"><%= Server.HtmlEncode(ViewData.Model.LoginCount.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<UsersController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
