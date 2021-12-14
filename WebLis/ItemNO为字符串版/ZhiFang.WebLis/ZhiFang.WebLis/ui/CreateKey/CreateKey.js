$(function() {
	//只能选择当前日期之后的日期
	$('#Validity').datebox().datebox('calendar').calendar({
		validator: function(date) {
			var now = new Date();
			var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
			return date >= d1;
		}
	});
});

//
var AESDecryptJSON = null;

function initData(callback) {
	var Account = Shell.util.Cookie.getCookie("ZhiFangUser");
	AESDecryptJSON = null;
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
				if(length == 0) {
					AESDecryptJSON = null;
				}
				var rows = [];
				for(var i = 0; i < length; i++) {
					if(data.rows[i].ClientNo && data.rows[i].Account == Account) {
						AESDecryptJSON = data.rows[i];
						AESDecryptJSON.Validity = "";
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
	if(!fileName && AESDecryptJSON) fileName = AESDecryptJSON.ID;
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
				AESDecryptJSON.Validity = jsonEntity.Validity;
			}
		}
	});
}
//设置默认值
function setDefaultData() {
	var editEntity = AESDecryptJSON;
	if(editEntity) {
		var validity = "";
		if(editEntity) validity = editEntity.Validity;

		if(!validity) {
			var now = new Date();
			var d1 = new Date(now.getFullYear(), now.getMonth() + 3, now.getDate());
			validity = getCurentDateStr(d1);
		}
		//$('#frm').form('clear');
		$('#UserCName').textbox('setValue', editEntity.Name);
		$('#LabCode').textbox('setValue', editEntity.ClientNo);
		$('#LabCName').textbox('setValue', editEntity.ClientName);
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

	if(errors > 0) {
		$.messager.alert('提示', '请检查输入值的完整性', 'warning');
	} else {
		var editEntity = AESDecryptJSON;
		var entity = {
			'UserID': Shell.util.Cookie.getCookie("ZhiFangUserID"),
			'Account': Shell.util.Cookie.getCookie("ZhiFangUser"),
			'UserCName': Shell.util.Cookie.getCookie("EmployeeName"),
			'UserPwd': Shell.util.Cookie.getCookie("ZhiFangPwd"),
			'LabCode': "" + LabCode + "",
			'LabCName': "" + LabCName + "",
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
	setDefaultData();
});