$(function() {

	$("#NRequestFormList").datagrid({
		method: 'get',
		rownumber: true,
		rownumbers: true,
		singleSelect: true,
		pagination: true,
		fitColumns: true,
		checkOnSelect: false,
		striped: true,
		columns: [
			[{
					field: 'BarcodeList',
					title: '条码号',
					width: 150
				},
				{
					field: 'CName',
					title: '姓名',
					width: 50
				},
				{
					field: 'GenderName',
					title: '性别',
					width: 30,
					align: 'center'
				},
				{
					field: 'Age',
					title: '年龄(岁)',
					width: 50,
					formatter: function(value, row, index) {
						if(value == "200" || value == 200) {
							return "成人";
						} else {
							return value;
						}
					}
				},
				{
					field: 'SampleTypeName',
					title: '样本',
					width: 50
				},
				{
					field: 'ItemList',
					title: '项目',
					width: 150,
					align: 'center'
				},
				{
					field: 'DoctorName',
					title: '医生',
					width: 30
				},
				{
					field: 'OperTime',
					title: '开单时间',
					width: 90,
					align: 'center'
				},
				{
					field: 'CollectTime',
					title: '采样时间',
					width: 90,
					align: 'center'
				},
				{
					field: 'WebLisSourceOrgName',
					title: '采血站点',
					width: 80,
					align: 'center'
				},
			]
		]
	});

	$('#txtClientNo').combobox({
		url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?guid=' + generateMixed(10) + '&page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO&where=&sort=',
		method: 'GET',
		valueField: 'ClIENTNO',
		textField: 'CNAME',
		loadFilter: function(data) {
			if(data.length > 0) {
				data[0].selected = true;
			}
			return data;
		},
		onLoadSuccess: function() {
			//$('#btnsearch').click();
		},
		onChange: function(newValue, oldValue) {
			//$('#btnsearch').click();
			SearchGridData();
		}
	});

	$('#btnsearch').bind('click', function() {
		SearchGridData();
	});

	$('#btnprint').bind('click', function() {
		PrintNRequestFormList();
	});
	//当按下回车键时查询
	$('#txtSerialno').textbox('textbox').bind('keydown', function(e) {
		if(e.keyCode == 13) {
			SearchGridData();
		}
	});
	//当按下回车键时查询
	$('#txtPayCode').textbox('textbox').bind('keydown', function(e) {
		if(e.keyCode == 13) {
			SearchGridData();
		}
	});
	//当按下回车键时查询
	$('#txtCName').textbox('textbox').bind('keydown', function(e) {
		if(e.keyCode == 13) {
			SearchGridData();
		}
	});

	function SearchGridData() {
		var ClientNo = $('#txtClientNo').combobox('getValue');
		var Serialno = $('#txtSerialno').textbox('getValue');
		var PayCode = $('#txtPayCode').textbox('getValue');
		var CName = $('#txtCName').textbox('getValue');

		var jsonentity = "{WebLisSourceOrgID:'" + ClientNo +
			"',BarCode:'" + Serialno +
			"',CName:'" + CName +
			"',PayCode:'" + PayCode +
			"'}";
		$('#NRequestFormList').datagrid({
			url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetNRequestFromListByByDetailsAndRBAC?guid=' + generateMixed(10),
			queryParams: {
				jsonentity: jsonentity
			}
		});
	}

	function PrintNRequestFormList() {
		var ClientNo = $('#txtClientNo').combobox('getValue');
		var Serialno = $('#txtSerialno').textbox('getValue');
		var PayCode = $('#txtPayCode').textbox('getValue');
		var CName = $('#txtCName').textbox('getValue');

		var url = Shell.util.Path.rootPath + "/ApplyInput/PrintNRequestFormList.aspx?ClientNo=" + ClientNo + "&PayCode=" + PayCode + "&txtPatientName=" + CName +"&BarCode:" + Serialno;
		window.open(url, "外送清单单预览打印", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
	}

	function generateMixed(n) {
		var res = "";
		var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
		for(var i = 0; i < n; i++) {
			var id = Math.ceil(Math.random() * 35);
			res += chars[id];
		}
		return res;
	}
});