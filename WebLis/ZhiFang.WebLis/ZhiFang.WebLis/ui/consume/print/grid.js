$(function() {

	var datetimenow = new Date();
	var datetimestr = datetimenow.getFullYear() + "-" + (datetimenow.getMonth() + 1) + "-" + datetimenow.getDate();
	$('#OperateStartDateTime').datebox('setValue', datetimestr);
	$('#OperateEndDateTime').datebox('setValue', datetimestr);

	$("#NRequestFormList").datagrid({
		method: 'get',
		rownumber: true,
		rownumbers: true,
		singleSelect: true,
		pagination: true,
		fitColumns: true,
		checkOnSelect: false,
		striped: true,
		remoteSort: false,
		sortName: 'OperTime',
		sortOrder: 'asc',
		columns: [
			[{
					field: 'ZDYZDY10',
					title: '来源',
					width: 45,
					//hidden:true,
					sortable: true,
					align: 'center',
					styler: function(val, row, index) {
						var value = "" + row.ZDY10;
						if(value != "" && value.length > 0) {
							return "background-color:#FF9900;color:#FFFFFF;";
						} else {
							return "background-color:#337ab7;color:#FFFFFF;"; //a4579d
						}
					},
					formatter: function(val, row, index) {
						var value = "" + row.ZDY10;
						if(value != "" && value.length > 0) {
							return "微信消费";
						} else {
							return "录 入";
						}
					}
				}, {
					field: 'ZDY10',
					title: '消费码',
					width: 120
				},{
					field: 'BarcodeList',
					title: '条码号',
					width: 150
				},
				{
					field: 'CName',
					title: '姓名',
					sortable: true,
					width: 45
				},
				{
					field: 'GenderName',
					title: '性别',
					sortable: true,
					width: 25,
					align: 'center'
				},
				{
					field: 'Age',
					title: '年龄(岁)',
					width: 40,
					align: 'center',
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
					sortable: true,
					align: 'center',
					width: 40
				},
				{
					field: 'ItemList',
					title: '项目',
					sortable: true,
					width: 150,
					align: 'center'
				},
				{
					field: 'DoctorName',
					title: '医生',
					sortable: true,
					align: 'center',
					width: 35
				},
				{
					field: 'OperTime',
					title: '开单时间',
					sortable: true,
					width: 70,
					align: 'center'
				},
				{
					field: 'CollectTime',
					title: '采样时间',
					sortable: true,
					width: 70,
					align: 'center'
				},
				{
					field: 'WebLisSourceOrgName',
					title: '采血站点',
					sortable: true,
					width: 120,
					align: 'center'
				}
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
	$('#btnaddnrequestform').bind('click', function() {
		var SN = Shell.util.Path.getRequestParams()["SN"];
		parent.OpenWindowFuc('新增采样', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/consume/entry/apply.html', SN);

	});

	$('#btnsearch').click(function() {
		SearchGridData();
	});
	$('#btnprint').bind('click', function() {
		PrintNRequestFormList();
	});
	//当按下回车键时查询	
	$('#txtSerialno').textbox('textbox').keydown(function(e) {
		if(e.keyCode == 13) {
			//$('#txtSerialno').textbox('setValue', $(this).val());
			SearchGridData();
		}
	});
	//当按下回车键时查询
	$('#txtZDY10').textbox('textbox').keydown(function(e) {
		if(e.keyCode == 13) {
			SearchGridData();
		}
	});
	//当按下回车键时查询
	$('#txtCName').textbox('textbox').keydown(function(e) {
		if(e.keyCode == 13) {
			SearchGridData();
		}
	});

	function SearchGridData() {
		var ClientNo = $('#txtClientNo').combobox('getValue');
		var Serialno = $('#txtSerialno').textbox('getText');
		var ZDY10 = $('#txtZDY10').textbox('getText');
		var CName = $('#txtCName').textbox('getText');

		var txtStartDate = $('#OperateStartDateTime').datebox('getValue');
		var txtEndDate = $('#OperateEndDateTime').datebox('getValue');
		var txtCollectStartDate = $('#txtCollectStartDate').datebox('getValue');
		var txtCollectEndDate = $('#txtCollectEndDate').datebox('getValue');

		if(!ClientNo) ClientNo = '';
		if(!Serialno) Serialno = '';
		if(!ZDY10) ZDY10 = '';
		if(!CName) CName = '';

		if(!txtStartDate) txtStartDate = '';
		if(!txtEndDate) txtEndDate = '';
		if(!txtCollectStartDate) txtCollectStartDate = '';
		if(!txtCollectEndDate) txtCollectEndDate = '';

		var jsonentity = "{WebLisSourceOrgID:'" + ClientNo +
			"',BarCode:'" + Serialno +
			"',OperDateStart:'" + txtStartDate +
			"',OperDateEnd:'" + txtEndDate +
			"',CollectDateStart:'" + txtCollectStartDate +
			"',CollectDateEnd:'" + txtCollectEndDate +
			"',CName:'" + CName +
			"',ZDY10:'" + ZDY10 +
			"'}";
		$('#NRequestFormList').datagrid({
			url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetNRequestFromListByByDetailsAndRBAC?guid=' + generateMixed(10),
			queryParams: {
				jsonentity: jsonentity
			}
		});
	}

	function ContentReLoad() {
		SearchGridData();
	}

	function PrintNRequestFormList() {
		var ClientNo = $('#txtClientNo').combobox('getValue');
		var Serialno = $('#txtSerialno').textbox('getText');
		var ZDY10 = $('#txtZDY10').textbox('getText');
		var CName = $('#txtCName').textbox('getText');

		var txtStartDate = $('#OperateStartDateTime').datebox('getValue');
		var txtEndDate = $('#OperateEndDateTime').datebox('getValue');
		var txtCollectStartDate = $('#txtCollectStartDate').datebox('getValue');
		var txtCollectEndDate = $('#txtCollectEndDate').datebox('getValue');

		if(!ClientNo) ClientNo = '';
		if(!Serialno) Serialno = '';
		if(!ZDY10) ZDY10 = '';
		if(!CName) CName = '';

		if(!txtStartDate) txtStartDate = '';
		if(!txtEndDate) txtEndDate = '';
		if(!txtCollectStartDate) txtCollectStartDate = '';
		if(!txtCollectEndDate) txtCollectEndDate = '';

		var url = Shell.util.Path.rootPath + "/ui/consume/print/PrintDeliveryList.aspx?ClientNo=" + ClientNo + "&ZDY10=" + ZDY10 + "&PatientName=" + CName + "&BarCode=" + Serialno + "&txtStartDate=" + txtStartDate + "&txtEndDate=" + txtEndDate + "&txtCollectStartDate=" + txtCollectStartDate + "&txtCollectEndDate=" + txtCollectEndDate;
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