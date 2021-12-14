var btnType;
var errors = 0;
var delCount = 0;
$(function() {
	$("#dg").datagrid({
		toolbar: "#toolbar",
		singleSelect: false,
		fit: true,
		border: false,
		pagination: true,
		rownumbers: true,
		collapsible: false,
		idField: 'Id',
		url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=' + "BPhysicalExamType",
		//url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllBPhysicalExamType',
		method: 'get',
		striped: true,
		columns: [
			[{
					field: 'cb',
					checkbox: 'true'
				},
				{
					field: 'Id',
					title: '编码',
					width: '28%'
				},
				{
					field: 'CName',
					title: '中文名称',
					width: '28%'
				},
				{
					field: 'ShortCode',
					title: '简码',
					width: '20%'
				},
				{
					field: 'DispOrder',
					title: '次序',
					width: '10%'
				},
				{
					field: 'Operation',
					title: '操作',
					width: '10%',
					formatter: function(value, row, index) {
						var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>";
						return edit;
					}
				}
			]
		],
		loadFilter: function(data) {
			var result = eval("(" + data.ResultDataValue + ")");
			if(result) {
				return {
					total: result.total || 0,
					rows: result.rows || []
				};
			} else {
				return {
					total: 0,
					rows: []
				};
			}
		}

	})
	var p = $('#dg').datagrid('getPager');
	$(p).pagination({

		pageSize: 10, //每页显示的记录条数，默认为10           
		pageList: [10, 50, 100, 200, 500], //可以设置每页记录条数的列表           
		beforePageText: '第', //页数文本框前显示的汉字           
		afterPageText: '页    共 {pages} 页',
		displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
	});
	$('#txtId').numberbox({
		validType: 'length[0,10]',
		onChange: function(newValue, oldValue) {
			var Id = $('#txtId').numberbox('getValue');
			if(btnType == 'add') {
				if(Id.length > 10) {
					$.ajax({
						success: function() {
							$('#txtId').numberbox('clear');
						}
					})
				} else {
					$.ajax({
						type: 'get',
						contentType: 'application/json',
						url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
						data: {
							filerValue: newValue.trim(),
							tablename: "B_PhysicalExamType",
							precisequery: "Id"
						},
						dataType: "json",
						success: function(data) {
							if(data.success) {
								var data = eval('(' + data.ResultDataValue + ')'),
									total = data.total || 0;
								if(total) {
									$('#txtId').numberbox('clear');

									$.messager.alert('提示', '数据库已存在此编号！不能重复插入', 'info');
								}

							}

						}
					});
				}
			}
		}
	});

})

function del() {
	var rows = $('#dg').datagrid('getSelections');
	if(rows.length == 0) {
		$.messager.alert("提示", "请勾选需要删除的数据!")
		return;
	} else {
		$.messager.confirm('提示', '你确定要删除么?', function(r) {
			if(r) {
				for(var i = 0; i < rows.length; i++) {
					$.ajax({
						type: 'get',
						contentType: 'application/json',
						url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteBPhysicalExamTypeByID?Id=' + rows[i].Id,
						dataType: 'json',
						async: false,
						success: function(data) {
							if(data.success == true) {
								delCount++;
							} else {
								$.messager.alert('提示', '删除数据失败！失败信息：' + data.msg);
							}
						}
					})
				}
				if(delCount > 0) {
					//alert('成功删除' + delCount + '条记录');
					delCount = 0;
					$('#dg').datagrid('clearSelections');
					$('#dg').datagrid('reload'); //因为getSelections会记忆选过的记录，所以要清空一下
				}
			}
		});
	}
}

function add() {
	$('#txtId').textbox('enable');
	btnType = 'add';
	$('#dlg').dialog('open').dialog('setTitle', '新增');
	$('#fm').form('clear');
	$("#ddlVisible").combobox('select', '是');
}

function edit(index, value) {
	btnType = 'edit';
	var rowData = $("#dg").datagrid('getRows')[index];
	$('#fm').form('load', rowData)

	$('#dlg').dialog('open').dialog('setTitle', '修改');
	// $('#txtId').textbox("readonly", true);
	$('#txtId').textbox('disable');

}

function update() {
	$('#dg').datagrid('load');
	$('#fm').form('clear')
	$('#txtSearchKey').searchbox("clear");
}

function doSearch() {
	var SH;
	var SearchKey = $("#txtSearchKey").val();
	if(SH == 0) {
		$('#txtSearchKey').searchbox('disable');
	} else {
		$('#txtSearchKey').searchbox('enable');
		$('#dg').datagrid({
			url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
			queryParams: {
				filerValue: SearchKey,
				tableName: 'BPhysicalExamType'
			},
			loadFilter: function(data) {
				var result = eval("(" + data.ResultDataValue + ")");
				return {
					total: result.total || 0,
					rows: result.rows || []
				};
			},
			onBeforeLoad: function() {
				SH = 0;
			},
			onLoadSuccess: function(data) {
				SH = 1;
				$('#txtSearchKey').searchbox("clear");
			}
		});
	}
}

function save() {
	if(btnType == 'edit') {
		$("#save").one('click', function(event) {
			event.preventDefault();

			$(this).prop('disabled', true);
		});
		var r = '';
		r += '{"jsonentity":{';
		var Id = $("#txtId").val();
		if(Id == "") {
			errors += 1;
		}
		if(Id) {
			r += '"Id":' + Id + ',';
		}
		var CName = $("#txtCName").val();
		if(CName == "") {
			errors += 1;
		}
		if(CName) {
			r += '"CName":"' + CName + '",';
		}
		var ShortCode = $("#txtShortCode").val();
		if(ShortCode == "") {
			errors += 1;
		}
		if(ShortCode) {
			r += '"ShortCode":"' + ShortCode + '",';
		}
		var Visible = $("#ddlVisible").combobox('getValue');
		if(Visible == "是") {
			Visible = 1;
		} else if(Visible == "否") {
			Visible = 0;
		}
		if(Visible) {
			r += '"Visible":' + Visible + ',';
		}
		var DispOrder = $("#txtDispOrder").val();
		if(DispOrder) {
			r += '"DispOrder":' + DispOrder + '';
		}
		r += '}}'
		if(errors > 0) {
			$.messager.alert('提示', '请检查输入值的完整性', 'warning');
			errors = 0;
		} else {
			$.ajax({
				type: 'post',
				contentType: 'application/json',
				url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateBPhysicalExamTypeByID',
				data: r,
				dataType: 'json',
				success: function(data) {
					if(data.success == true) {

						$('#dg').datagrid('load');
						$('#dlg').dialog('close');
						$('#dg').datagrid('unselectAll');
					} else {
						$.messager.alert('提示', '修改数据失败！失败信息：' + data.ErrorInfo);
					}
				}
			});
		}
	} else if(btnType == "add") {
		$("#save").one('click', function(event) {
			event.preventDefault();

			$(this).prop('disabled', true);
		});
		var r = '';
		r += '{"jsonentity":{';
		var Id = $("#txtId").val();
		if(Id == "") {
			errors += 1;
		}
		if(Id) {
			r += '"Id":' + Id + ',';
		}
		var CName = $("#txtCName").val();
		if(CName == "") {
			errors += 1;
		}
		if(CName) {
			r += '"CName":"' + CName + '",';
		}
		var ShortCode = $("#txtShortCode").val();
		if(ShortCode == "") {
			errors += 1;
		}
		if(ShortCode) {
			r += '"ShortCode":"' + ShortCode + '",';
		}
		var Visible = $("#ddlVisible").combobox('getValue');
		if(Visible == "是") {
			Visible = 1;
		} else if(Visible == "否") {
			Visible = 0;
		}
		if(Visible) {
			r += '"Visible":' + Visible + ',';
		}

		var DispOrder = $("#txtDispOrder").val();
		if(DispOrder) {
			r += '"DispOrder":' + DispOrder + '';
		}
		r += '}}'
		if(errors > 0) {
			$.messager.alert('提示', '请检查输入值的完整性', 'warning');
			errors = 0;
		} else {
			$.ajax({
				type: 'post',
				contentType: 'application/json',
				url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddBPhysicalExamType',
				data: r,
				dataType: "json",
				success: function(data) {
					if(data.success == true) {
						// $.messager.alert('提示', '插入数据成功！');
						$('#dg').datagrid('load');
						$('#dlg').dialog('close');
						$('#dg').datagrid('unselectAll');
					} else {
						$.messager.alert('提示', '插入数据失败！失败信息：' + data.ErrorInfo);
					}
				}
			});
		}
	}
}