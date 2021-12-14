<%@ Page codePage="936" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Xml.XPath" %>
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<title>XPath Analyzer （XML 浏览器）</title>
		<script language="VB" runat="server">

		Dim intIndent as Integer = 0
		Dim xDoc as XPathDocument
		Dim xNav as XPathNavigator

		Sub btnAnalyze_Click(obj as Object, e as EventArgs)
			Dim xNodeIterator as XPathNodeIterator
			Dim objTable as DataTable
			Dim objRow as DataRow
			Dim strAttribute as String
			Dim intTotalNode as Integer = 0
			Dim strNodeType as String

			Try
				'Instantiate XPathDocument and load XML document
				xDoc = New XPathDocument(Server.MapPath(txtXML.Text))

				'Instantiate XPathNavigator
				xNav = xDoc.CreateNavigator()

				'Create temporary table to keep query result
				objTable = New DataTable("QueryResult")
				objTable.Columns.Add("node", Type.GetType("System.String"))
				objTable.Columns.Add("value", Type.GetType("System.String"))
				objTable.Columns.Add("attribute", Type.GetType("System.String"))

				'Execute XPath query
				if txtXPath.Text.Trim()="" then
					txtXPath.Text="*"
				end if
				
				xNodeIterator = xNav.Select(txtXPath.Text)

				'Iterate through the resultant node set
				While xNodeIterator.MoveNext()

					'Count the number of nodes in the node set
					intTotalNode += 1

					'Get the current node as XPathNavigator object
					xNav = xNodeIterator.Current

					'Create a new row in the temporary table
					objRow = objTable.NewRow()

					'Fill the first column of new row
					Select Case xNav.NodeType
						Case XPathNodeType.Element
							strNodeType = "&lt;&gt;"
						Case XPathNodeType.Attribute
							strNodeType = "="
						Case XPathNodeType.Text
							strNodeType = "Abc"
						Case XPathNodeType.Comment
							strNodeType = "&lt;!&gt;"
						Case Else
							strNodeType = ""
					End Select
					objRow("node") = "<font color=""red""><b>" & strNodeType & "</b></font> " & xNav.Name

					'Fill the second column of new row
					If xNav.MoveToFirstChild() Then
						objRow("value") = RenderTree()
						xNav.MoveToParent()
					Else
						objRow("value") = xNav.Value
					End If

					'Fill the third column of new row
					strAttribute = ""
					If xNav.NodeType = XPathNodeType.Attribute Then
						objRow("attribute") = "n/a"
					Else
						'Iterate through attributes if any
						If xNav.MoveToFirstAttribute() Then
							Do
								strAttribute += xNav.Name & ": " & xNav.Value & "<br>"
							Loop While xNav.MoveToNextAttribute()
							xNav.MoveToParent()
							objRow("attribute") = strAttribute
						End If
					End If

					'Effectively add the new row to the temporary table
					objTable.Rows.Add(objRow)
				End While

				lblMessage.Text = "Total nodes found: " & intTotalNode.ToString()

				'Bind data grid
				objRepeater.DataSource = objTable.DefaultView
				objRepeater.DataBind()

			Catch ex as Exception
				lblMessage.Text = "<font class=""error"">" & ex.Message & "</font>"
			End Try
		End Sub

		Function RenderTree() As String
			Dim strRenderTree As String

			'Iterate through all siblings
			Do
				Select Case xNav.NodeType
					Case xPathNodeType.Text
						strRenderTree += xNav.Value
					Case xPathNodeType.Comment
						strRenderTree += "&lt;!-- " & xNav.Value & "--&gt;<br>"
					Case Else
						'Render indent
						strRenderTree += RenderHTMLSpace(intIndent)

						'Render opening tag
						strRenderTree += "&lt;" & xNav.Name

						'Render attributes if any
						If xNav.MoveToFirstAttribute() Then
							Do
								strRenderTree += " " & xNav.Name & "= """ & xNav.Value & """"
							Loop While xNav.MoveToNextAttribute()
							xNav.MoveToParent()
						End If
						strRenderTree += "&gt;"

						'Render child nodes if any
						If xNav.MoveToFirstChild() Then
							If xNav.NodeType <> xPathNodeType.Text Then
								strRenderTree += "<br>"
							End If

							intIndent += 2

							'Invoke this function recursively
							strRenderTree += RenderTree()

							intIndent -= 2

							'Move back to the parent node
							xNav.MoveToParent()
						End If

						'Render closing tag
						strRenderTree += "&lt;/" & xNav.Name & "&gt;" & "<br>"
				End Select
			Loop While (xNav.MoveToNext())
			RenderTree = strRenderTree
		End Function

		Function RenderHTMLSpace(byVal intTotalSpace as Integer) as String
			Dim intLoop as Integer
			Dim strHTMLSpace as String
			For intLoop = 1 to intTotalSpace
				strHTMLSpace += "&nbsp;"
			Next
			RenderHTMLSpace = strHTMLSpace
		End Function

		</script>
		<style>BODY { FONT-SIZE: 10pt; COLOR: #000000; FONT-FAMILY: verdana,helvetica,arial,sans-serif; BACKGROUND-COLOR: #eeeedd }
	FONT.title { FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: #000000; FONT-FAMILY: verdana,helvetica,arial,sans-serif }
	TD.heading { FONT-SIZE: 10pt; COLOR: #ffffff; FONT-FAMILY: verdana,helvetica,arial,sans-serif; BACKGROUND-COLOR: #900b08 }
	TD.normal { FONT-SIZE: 8pt; COLOR: #000000; FONT-FAMILY: verdana,helvetica,arial,sans-serif; BACKGROUND-COLOR: #ffffff }
	TD.alternating { FONT-SIZE: 8pt; COLOR: #000000; FONT-FAMILY: verdana,helvetica,arial,sans-serif; BACKGROUND-COLOR: #e0e0e0 }
	.textbox { FONT-SIZE: 8pt; FONT-FAMILY: verdana,helvetica,arial,sans-serif }
	.button { BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; BACKGROUND-COLOR: #ffffff }
	.error { FONT-SIZE: 10pt; COLOR: #ff0000; FONT-FAMILY: verdana,helvetica,arial,sans-serif }
	</style>
		<script id="clientEventHandlersJS" language="javascript">
	<!--

	function buttSelectXmlFile_onclick() {
		var r;
		r=window.open('ContentPane.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
		if (r != '' && typeof(r) != 'undefined'&&typeof(r)!='object')
			document.all['txtXML'].value=r;
	}
	//-->
		</script>
	</HEAD>
	<body>
		<form runat="server" id="form1">
			<table height="111">
				<tr>
					<td colSpan="2"><font class="title">XPath Analyzer&nbsp; (XML XPATH 分析器)</font></td>
				</tr>
				<tr>
					<td class="heading">
						xml 文件相对路径:</td>
					<td><asp:textbox class="textbox" id="txtXML" runat="server" columns="30" CssClass="textbox"></asp:textbox><INPUT id="buttSelectXmlFile" type="button" value="目录查找" onclick="return buttSelectXmlFile_onclick()"></td>
				</tr>
				<tr>
					<td class="heading">
						XPath 字符串:</td>
					<td><asp:textbox class="textbox" id="txtXPath" runat="server" columns="75"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:button class="button" id="btnAnalyze" onclick="btnAnalyze_Click" runat="server" text="开始分析"></asp:button></td>
				</tr>
			</table>
			<br>
			<asp:label id="lblMessage" runat="server" EnableViewState="false"></asp:label><asp:repeater id="objRepeater" runat="server" EnableViewState="false">
				<HeaderTemplate>
					<table border="1" cellpadding="2" cellspacing="0" width="100%" style="border-collapse: collapse"
						bordercolor="#000000">
						<tr>
							<td class="heading">节点名称</td>
							<td class="heading">值/子节点全部串</td>
							<td class="heading">全部属性</td>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td class="normal" valign="top"><%# Container.DataItem("node") %></td>
						<td class="normal" valign="top"><%# Container.DataItem("value") %></td>
						<td class="normal" valign="top"><%# Container.DataItem("attribute") %></td>
					</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<tr>
						<td class="alternating" valign="top"><%# Container.DataItem("node") %></td>
						<td class="alternating" valign="top"><%# Container.DataItem("value") %></td>
						<td class="alternating" valign="top"><%# Container.DataItem("attribute") %></td>
					</tr>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</table>
				</FooterTemplate>
			</asp:repeater></form>
	</body>
</HTML>
