$(function() {
	$('#downloadAes').hide();
	//只能选择当前日期之后的日期
	$('#Validity').datebox().datebox('calendar').calendar({
		validator: function(date) {
			var now = new Date();
			var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
			return date >= d1;
		}
	});
});

//登录帐号默认所属单位信息
var DefaultClient = null;

function initData(callback) {
	var Account = Shell.util.Cookie.getCookie("ZhiFangUser");
	DefaultClient = null;
	$('#downloadAes').hide();
	$.ajax({
		type: 'get',
		//async: false,
		contentType: 'application/json',
		url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetLogisticsDeliveryPerson',
		dataType: 'json',
		success: function(data) {
			if(data.success) {
				var result = {};
				data = eval('(' + data.ResultDataValue + ')');
				var length = data.rows ? data.rows.length : 0;
				if(length == 0) DefaultClient = null;
				var rows = [];
				for(var i = 0; i < length; i++) {
					if(data.rows[i].ClientNo && data.rows[i].Account == Account) {
						DefaultClient = data.rows[i];
						DefaultClient.Validity = "";
						var jsonEntity = getAESDecryptByFileName();
						break;
					}
				}
				callback();
			}
		}
	});
}

function getFileName() {
	var fileName = Shell.util.Cookie.getCookie("ZhiFangUserID");
	if(!fileName && DefaultClient) fileName = DefaultClient.ID;
	return fileName;
}
//还原已生成密钥文件
function getAESDecryptByFileName() {
	var fileName = getFileName();
	$.ajax({
		type: 'get',
		async: false,
		contentType: 'application/json',
		url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAESDecryptFileByFileName?fileName=' + fileName,
		dataType: 'json',
		success: function(data) {
			if(data.success) {
				var jsonEntity = eval('(' + data.ResultDataValue + ')');
				DefaultClient.Validity = jsonEntity.Validity;
			}
		}
	});
}
//设置默认值
function setDefaultData() {
	var editEntity = DefaultClient;
	if(editEntity) {
		var validity = "";
		if(editEntity) validity = editEntity.Validity;

		if(!validity) {
			var now = new Date();
			var d1 = new Date(now.getFullYear(), now.getMonth() + 3, now.getDate());
			validity = getCurentDateStr(d1);
		}
		if(!editEntity.Port)editEntity.Port="9999";
		$('#UserCName').textbox('setValue', editEntity.Name);
		$('#LabCode').textbox('setValue', editEntity.ClientNo);
		$('#LabCName').textbox('setValue', editEntity.ClientName);
		$('#Port').textbox('setValue', editEntity.Port);
		$('#Validity').datebox('setValue', validity);
		$('#downloadAes').hide();
	} else {
		var Account = Shell.util.Cookie.getCookie("ZhiFangUser");
		$.messager.alert('提示', '获取登录帐号为' + Account + '的所属单位为空', 'warning');
	}
}
//下载密钥文件
function onDownloadAes() {
	var fileName = getFileName();
	if(!fileName) {
		$.messager.alert('提示', '获取下载密钥文件的fileName为空', 'warning');
	} else {
		var url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DownLoadAESEncryptFile?fileName=' + fileName + "&t=" + new Date().getTime();
		window.open(url);
	}
}
//生成密钥文件并保存
function onSaveAESEncrypt(callback) {
	var errors = 0;
	var LabCode = $('#LabCode').val();
	if(LabCode == "") errors += 1;
	var LabCName = $('#LabCName').val();
	if(LabCName == "") errors += 1;
	var Validity = $('#Validity').datebox('getValue');
	if(Validity == "") errors += 1;	
	var Port = $('#Port').val();
	if(Port == "") errors += 1;
	
	if(errors > 0) {
		$.messager.alert('提示', '请检查输入值的完整性', 'warning');
	} else {
		var editEntity = DefaultClient;
		var entity = {
			'UserID': Shell.util.Cookie.getCookie("ZhiFangUserID"),
			'Account': Shell.util.Cookie.getCookie("ZhiFangUser"),
			'UserCName': Shell.util.Cookie.getCookie("EmployeeName"),
			'UserPwd': Shell.util.Cookie.getCookie("ZhiFangPwd"),
			'LabCode': "" + LabCode + "",
			'LabCName': "" + LabCName + "",
			'Port': "" + Port + "",
			'Validity': "" + Validity + ""
		};
		var jsonData = {
			"jsonentity": entity
		};
		jsonData = Shell.util.JSON.encode(jsonData);
		$.ajax({
			type: 'post',
			contentType: 'application/json',
			url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CreatAESEncryptFile',
			data: jsonData,
			dataType: 'json',
			success: function(data) {
				if(data.success == true) {
					callback();
				} else {
					$.messager.alert('提示', '生成密钥文件失败！失败信息：' + data.msg);
				}
			}
		});
	}
}
//生成密钥文件
function onCreateAes() {
	onSaveAESEncrypt(function() {
		$('#downloadAes').show();
		//onDownloadAes();
		$('#dlg').dialog({
			modal: true
		});
		$('#dlg').dialog('open').dialog('setTitle', '下载密钥文件');
		$('#dlg').window('center');
	});
}
//下载密钥文件
function onDownloadAes2() {
	$('#dlg').dialog('close');
	onDownloadAes();
}

function getCurentDateStr(now) {
	//var now = new Date();
	var year = now.getFullYear(); //年  
	var month = now.getMonth() + 1; //月  
	var day = now.getDate(); //日  
	var clock = year + "-";
	if(month < 10) clock += "0";
	clock += month + "-";
	if(day < 10) clock += "0";
	clock += day;
	return clock;
}
//初始化
initData(function() {
	if(!DefaultClient) {
		var account = Shell.util.Cookie.getCookie("ZhiFangUser");
		//如果所属单位为空,取该客户下的所属客户列表的第一个客户为默认所属单位
		$.ajax({
			type: 'get',
			//async: false,
			contentType: 'application/json',
			url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetLogisticsCustomerByDeliveryIDAndType?selectedflag=1&account=' + account + '&itemkey=&page=1&rows=10',
			dataType: 'json',
			success: function(data) {
				if(data.success) {
					if(data.ResultDataValue) {
						vardataResult = eval('(' + data.ResultDataValue + ')');
						var length = dataResult.rows ? dataResult.rows.length : 0;
						if(length > 0) {
							var row = dataResult.rows[0];
							DefaultClient = {
								"ID": Shell.util.Cookie.getCookie("ZhiFangUserID"),
								"Name": Shell.util.Cookie.getCookie("EmployeeName"),
								"ClientNo": row.ClIENTNO,
								"ClientName": row.CNAME,
								"Port": "9999",
							};
							setDefaultData();
						} else {
							DefaultClient = null;
						}
					}
				}
			}
		});
	} else {
		setDefaultData();
	}

});