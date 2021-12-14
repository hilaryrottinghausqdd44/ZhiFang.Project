$(function() {
	var params = Shell.util.Path.getRequestParams();
	var Account = Shell.util.Cookie.getCookie("ZhiFangUser");
	//初始化客户表
	$('#customer').datagrid({
		loadMsg: '数据加载中...',
		pageSize: 20,
		method: 'GET',
		loadMsg: '数据加载...',
		rownumbers: true,
		pagination: true,
		fitColumns: true,
		singleSelect: true,
		striped: true,
		fit: true,
		idField: 'ClIENTNO',
		toolbar: '#rightBars',
		columns: csmColumns(),

		method: 'GET',
		url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetLogisticsCustomerByDeliveryIDAndType?selectedflag=1&account=' + Shell.util.Cookie.getCookie("ZhiFangUser"),
		onBeforeLoad: function(param) {
			if(param.page == 0) return false;
		},
		loadFilter: function(data) {
			if(data.success) {
				var result = {};
				data = eval('(' + data.ResultDataValue + ')'); //eval()方法把字符串转换成JSON格式
				var length = data.rows ? data.rows.length : 0;
				if(length == 0) {
					data.rows = [];
				}
				for(var i = 0; i < length; i++) {
					data.rows[i].ClientNo_CName = '(' + data.rows[i].ClIENTNO + ')' + data.rows[i].CNAME;
					var jsonEntity = getAESDecryptByLabId(data.rows[i].ClIENTNO);
					data.rows[i].AESDecryptJSON = jsonEntity;
				}
				result.total = data.total || 0;
				result.rows = data.rows ? data.rows : [];
				return result;
			}
		}
	});
});
//刷新客户列表
function refreshCsm() {
	$('#customer').datagrid('load', {
		itemkey: null
	});
	$('#txtCsm').searchbox('setValue', null);
}
//查询客户列表
function searchCsm(value) {
	$('#customer').datagrid('load', {
		itemkey: value
	});
}
//设置列宽
function fixWidth(percent) {
	return document.body.clientWidth * percent;
}
//创建数据列
function csmColumns() {
	var columns = [
		[{
			field: 'ClIENTNO',
			//hidden: true,
			title: '客户编码',
			width: fixWidth(0.3)
		}, {
			field: 'CNAME',
			title: '客户名称',
			width: fixWidth(0.5)
		}, {
			field: 'Account',
			hidden: true,
			title: '帐号',
			width: fixWidth(0.4)
		}, {
			field: 'ClientNo_CName',
			hidden: true,
			title: '客户名称',
			width: fixWidth(0.4)
		}, {
			field: 'opt',
			title: '操作',
			align: 'right',
			width: 85,
			align: 'center',
			formatter: function(value, row, index) {
				if(row.Account == Shell.util.Cookie.getCookie("ZhiFangUser")) {
					var edit = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick="onOpenAes(' + index + ')">生成密钥</a>';
					var downLoad = "";
					if(row.AESDecryptJSON) {
						downLoad = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick="onDwonloadAes(' + index + ')">下载</a>';
					}
					return edit + downLoad;
				} else {
					return "";
				}
			}
		}]
	];
	return columns;
}
//打开生成密钥文件
function onOpenAes(index) {
	var curData = $('#customer').datagrid('getData'),
		curRow = curData.rows[index];
	$('#dlg').dialog({
		modal: true
	});
	var editEntity = curRow.AESDecryptJSON;
	if(!editEntity) editEntity = getAESDecryptByLabId(curRow.ClIENTNO);

	var validity = "";
	if(editEntity) {
		var entity = eval('(' + editEntity + ')');
		if(entity) validity = entity.Validity;
	}

	$('#dlg').dialog('open').dialog('setTitle', '生成密钥文件');
	$('#dlg').window('center');
	$('#frm').form('clear');

	$('#ClientNO').textbox('setValue', curRow.ClIENTNO);
	$('#ClientCName').textbox('setValue', curRow.CNAME);
	$('#Validity').datebox('setValue', validity);
}
//下载密钥文件
function onDwonloadAes(index) {
	var curData = $('#customer').datagrid('getData'),
		curRow = curData.rows[index];
	var clientNo = curRow.ClIENTNO;
	if(!clientNo) {
		$.messager.alert('提示', '获取下载密钥文件的labCode为空', 'warning');
	} else {
		var url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DownLoadAESEncryptFileByClientNo?clientNo=' + clientNo;
		window.open(url);
	}
}
//还原已生成密钥文件
function getAESDecryptByLabId(clientNo) {
	var jsonEntity = null;
	$.ajax({
		type: 'get',
		async: false,
		contentType: 'application/json',
		url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAESDecryptFileByClientNo?clientNo=' + clientNo,
		dataType: 'json',
		success: function(data) {
			jsonEntity = data.ResultDataValue;
		}
	});
	return jsonEntity;
}
//生成密钥文件并保存
function saveAESEncrypt() {
	var errors = 0;
	var ClientNO = $('#ClientNO').val();
	if(ClientNO == "") errors += 1;
	var ClientCName = $('#ClientCName').val();
	if(ClientCName == "") errors += 1;
	var Validity = $('#Validity').datebox('getValue');
	if(Validity == "")errors += 1;
	
	if(errors > 0) {
		$.messager.alert('提示', '请检查输入值的完整性', 'warning');
	} else {
		var r = '{"jsonentity":{"ClientNO":"' + ClientNO + '","ClientCName":"' + ClientCName + '","Validity":"' + Validity + '"}}';
		$.ajax({
			type: 'post',
			contentType: 'application/json',
			url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CreatAESEncryptFile',
			data: r,
			dataType: 'json',
			success: function(data) {
				if(data.success == true) {
					$('#dlg').dialog('close');
					refreshCsm()
					$.messager.alert('提示', '生成密钥文件成功！');
				} else {
					$.messager.alert('提示', '生成密钥文件失败！失败信息：' + data.msg);
				}
			}
		});
	}
}
//取消
function onclose() {
	$('#dlg').dialog('close');
}