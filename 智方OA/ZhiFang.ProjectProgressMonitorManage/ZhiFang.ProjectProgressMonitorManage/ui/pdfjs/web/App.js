var params = JcallShell.Page.getParams(true);
//隐藏文件打开按钮
var openFile = document.getElementById("openFile");
    openFile.style.display = "none";

//隐藏打印按钮
if(params.PRINT=='0'){
	var print = document.getElementById("print");
    print.style.display = "none";
}
//隐藏下载按钮
if(params.DOWNLOAD=='0'){
	var download = document.getElementById("download");
    download.style.display = "none";
}