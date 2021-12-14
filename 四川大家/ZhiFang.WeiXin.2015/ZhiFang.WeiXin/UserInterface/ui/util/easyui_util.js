/**
 * 系统公用功能类-easyui功能通用
 * @type 
 */
var Shell = Shell || {};

Shell.easyuiUtil = {};

/**easyui-datagrid辅助方法*/
Shell.easyuiUtil.DataGrid = {
	/**后台ResultDataValue数据格式转化为前台数据格式{tolal,rows}*/
	loadFilter:function(data){
		if(data.success){
			return Shell.util.JSON.decode(data.ResultDataValue);
		}else{
			$.messager.alert("错误信息",data.ErrorInfo,"error");
			return {"tolal":0,"rows":[]};
		}
    }
};

$.ajaxSetup ({
   cache:false //关闭AJAX相应的缓存
});
