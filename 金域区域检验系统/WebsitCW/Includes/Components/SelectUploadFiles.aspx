<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUploadFiles.aspx.cs" Inherits="OA.Includes.Components.SelectUploadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
    function addFile()
    {
        var fileInfos = window.showModalDialog('UploadFiles.aspx',window,'dialogWidth=600px;dialogHeight=400px');
        addf(fileInfos);
        return false;
    }
    function addf(fileInfos)
    {
        var allFileInfos = document.getElementById("allFileInfos");//隐藏控件中存储的文件信息
    
        var curPID = document.getElementById("hiddenPID").value;
        if(curPID != "")
        {
            allFileInfos.value = "";
        }
    
        if(fileInfos == null || fileInfos == undefined || fileInfos=="")
        {
            return;
        }
        
        var splitString = "";
        if(allFileInfos.value != "")
        {
            splitString = "*|*";
        }
        allFileInfos.value = allFileInfos.value+splitString+fileInfos;

        var files;
        files = allFileInfos.value.split("*|*");
        var fileList = document.getElementById("fileList");
        
        for(var i=0;i<files.length;i++)
        {
            var fileName = files[i].split("|_|")[0];
            var newFile = "<label onclick='chooseThis(this);' ondblclick='downFile(this);' value='"+files[i]+"'>"+fileName+"</label>&nbsp;&nbsp;";
            
            fileList.innerHTML += newFile;
        }
        
        //使用AJAX调用后台保存功能
        var curP = document.getElementById("hiddenP");
        var curPID = document.getElementById("hiddenPID");
        var curFileSavePath = document.getElementById("hiddenFileSavePath");
        var hiddenFilePath = document.getElementById("hiddenFilePath");
        
        var hiddenEmployeeID = document.getElementById("hiddenEmployeeID");
        
        OA.Includes.Components.SelectUploadFiles.addFile(curP.value,curPID.value,curFileSavePath.value,document.getElementById("allFileInfos").value,hiddenEmployeeID.value,hiddenFilePath.value);
        return false;//禁止回传
    }
    var fileLink;
    function chooseThis(obj)
    {
        if(fileLink != null && fileLink != undefined)
        {
            fileLink.style.backgroundColor = "";
        }
        obj.style.backgroundColor = "gray";
        fileLink = obj;
        var HiddenCurFileInfo = document.getElementById("HiddenCurFileInfo");
        HiddenCurFileInfo.value = obj.value;
        
        var allFileInfos = document.getElementById("allFileInfos");
        return true;
    }
    function scanFile()
    {
        if(fileLink == null || fileLink == undefined)
        {
            alert("请选择文件！");
            return false;
        }
        return true;
    }
    function delFile()
    {
        var scan = scanFile();
        if(scan)
        {
            var DelEnsure = window.confirm("确定要删除该文件吗？");
            if(!DelEnsure)
            {
                return false;
            }
            fileLink.style.display = "none";
            var HiddenCurFileInfo = document.getElementById("HiddenCurFileInfo");
            var curP = document.getElementById("hiddenP");
            var curPID = document.getElementById("hiddenPID");
            
            OA.Includes.Components.SelectUploadFiles.delSelectedFile(HiddenCurFileInfo.value,curP.value,curPID.value);
            
            fileLink = null;
            HiddenCurFileInfo.value = "";
        }
        return false;
    }
    //外部框架保存文件时调用
    //PID：调用的项目编号，strAllFileInfos：存储全部新文件信息
    function saveFileWithPID(curPID,strAllFileInfos)
    {
        var curP = document.getElementById("hiddenP").value;
        var curFileSavePath = document.getElementById("hiddenFileSavePath").value;
        var curFilePath = document.getElementById("hiddenFilePath").value;
        
        OA.Includes.Components.SelectUploadFiles.saveFileWithPID(curP, curPID, curFileSavePath,curFilePath, strAllFileInfos)
    }
    function saveFileInNewsSysWithPID(curPID)
    {
        var strAllFileInfos = document.getElementById("allFileInfos");
        saveFileWithPID(curPID,strAllFileInfos)
    }
    
    function downFile(obj)
    {
        chooseThis(obj);
        var imgBtnScan = document.getElementById("imgBtnScan");
        imgBtnScan.click();
    }
    function showmsg()
    {
        return "aaa";
    }
    </script>
</head>
<body style="font-size:12px; padding:0; border:0; margin:0;">
    <form id="form1" runat="server">
    <div>
        <fieldset style="height: 98%;">
            <legend>
                <span>附件</span> &nbsp;
                    <asp:ImageButton ImageUrl="../../Images/diary/tj-.gif" ID="imgBtnAdd" runat="server" />
                    <asp:ImageButton ImageUrl="../../Images/diary/sc.gif" ID="imgBtnDel" runat="server" />
                    <asp:ImageButton ImageUrl="../../Images/diary/ck.gif" ID="imgBtnScan" runat="server" onclick="imgBtnScan_Click" />
                </legend>
            <span id="fileList" name="fileList" runat="server"></span>
        </fieldset>
    </div>
        <input type="hidden" id="HiddenOldFileInfos" name="HiddenOldFileInfos" runat="server" /><!-- 存储原有文件信息：文件原名|_|文件全路径名*|*文件原名|_|文件全路径名 -->
        <input type="hidden" id="allFileInfos" name="allFileInfos" runat="server" />            <!-- 存储全部新文件信息：文件原名|_|文件全路径名*|*文件原名|_|文件全路径名 -->
        <input type="hidden" id="HiddenCurFileInfo" name="HiddenCurFileInfo" runat="server" />  <!-- 存储当前文件信息：文件原名|_|文件全路径名 -->
        
        <input type="hidden" id="hiddenP" runat="server" /><!-- P:主调用的系统编号 -->
        <input type="hidden" id="hiddenPID" runat="server" /><!-- PID:主调用的项目编号 -->
        
        <input type="hidden" id="hiddenFileSavePath" runat="server" /><!-- fileSavePath:文件临时保存路径 -->
        <input type="hidden" id="hiddenFilePath" runat="server" /><!-- filePath:文件保存路径 -->
        
        <input type="hidden" id="hiddenPurview" runat="server" /><!-- purview:对本系统的操作权限 -->
        <input type="hidden" id="hiddenEmployeeID" runat="server" /><!-- EmployeeID:登陆用户编号 -->
    </form>
</body>
</html>
