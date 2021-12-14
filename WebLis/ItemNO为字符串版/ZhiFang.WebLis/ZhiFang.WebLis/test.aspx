<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ZhiFang.WebLis.test" %>
<%Response.Write(Request.QueryString["callback"].ToString() + "(" + json + ")");%>