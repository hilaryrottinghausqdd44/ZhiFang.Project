<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFiles.aspx.cs" Inherits="OA.Includes.Components.UploadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>文件上传</title>
    <base target="_self"> 
    <script type="text/javascript">
    var id = 3;
    function addFile()
    {
        id = id +1;
    	var str = '<INPUT type="file" id="'+id+'"  onchange="judgAddOrNot(this);" size="50" NAME="File">'
    	document.getElementById('MyFile').insertAdjacentHTML("beforeEnd",str)
    }
    function judgAddOrNot(obj)
    {
        var thisID = obj.id;
        if(thisID == id)
        {
            addFile();
        }
    }
    function returnFileInfos(str)
    {
        window.returnValues = str;
        window.close();
    }
    </script>
  </head>
  <body>
    <form id="form2" method="post" runat="server" enctype="multipart/form-data">
      <div style="text-align:center;">
        <h3>文件上传</h3>
        <p id="MyFile">
            <input type="file" id="1" onchange="judgAddOrNot(this);" size="50" name="File" />
            <input type="file" id="2" onchange="judgAddOrNot(this);" size="50" name="File" />
            <input type="file" id="3" onchange="judgAddOrNot(this);" size="50" name="File" />
        </p>
        <p>
          <asp:Button Runat="server" Text="上传" ID="UploadButton" onclick="UploadButton_Click"></asp:Button>
        </p>
        <p>
            <asp:Label id="strStatus" runat="server" Font-Names="宋体" Font-Bold="True" Font-Size="9pt" 
                Width="500px" BorderStyle="None" BorderColor="White"></asp:Label>
        </p>
      </div>
    </form>
  </body>
</html>