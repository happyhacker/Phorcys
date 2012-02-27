<%@ Page Title="DiveTypes" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Phorcys.Core.DiveType>>" %>
<%@ Import Namespace="Phorcys.Core" %>
<%@ Import Namespace="Phorcys.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>Dive Types</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>Title</th>
			    <th>Notes</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (DiveType diveType in ViewData.Model) { %>
			<tr>
				<td><%= diveType.Title %></td>
				<td><%= diveType.Notes %></td>
				<td><%=Html.ActionLink<DiveTypesController>( c => c.Show( diveType.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<DiveTypesController>( c => c.Edit( diveType.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<DiveTypesController>(c => c.Delete(diveType.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<DiveTypesController>(c => c.Create(), "Create New DiveType") %></p>
</asp:Content>
