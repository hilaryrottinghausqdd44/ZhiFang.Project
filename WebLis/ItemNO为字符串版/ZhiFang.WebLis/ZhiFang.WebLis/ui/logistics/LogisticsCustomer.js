/**
 * @OverView:物流客户管理
 * @naming:personnel简写：psn,customer简写：csm
 * Created by gwh on 14-12-5.
 */

//人员表对象
var $objPersonnel;
//客户表对象
var $objCustomer;
//物流人员账号
var psnAccount;
//Jquery入口
$(function () {
    //初始化人员表
    $objPersonnel = $('#personnel').datagrid({
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetLogisticsDeliveryPerson',
        //url: 'data.txt',
        method: 'GET',
        loadMsg: '数据加载中...',
        pageSize: 20,
        pagination: true,
        fitColumns: true,
        striped: true,
        fit: true,
        border: false,
        rownumbers: true,
        singleSelect: true,
        idField: 'Account',
        toolbar: '#leftBars',
        columns: psnColumns(),
        loadFilter: function (data) { //过滤数据
			var result = {rows:[],total:0};
			if(data.success) {
				data = eval('(' + data.ResultDataValue + ')'); //eval()方法把字符串转换为JSON格式              
				if(data) {
					var length = data.rows ? data.rows.length : 0;
					for(var i = 0; i < length; i++) {
						if(!data.rows[i].ClientNo || !data.rows[i].ClientName) {
							continue;
						}
						data.rows[i].Client = '(' + data.rows[i].ClientNo + ')' + data.rows[i].ClientName;
					}
					result.rows = data.rows ? data.rows : result.rows;
					result.total = data.total ? data.total : 0;
				}
			}
			return result;
		},
        onSelect: function (index, row) {
            psnAccount = row.Account;
            $objCustomer.datagrid("options").url = Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetLogisticsCustomerByDeliveryIDAndType'+'?selectedflag='+1+'&account='+psnAccount;
            $objCustomer.datagrid("load");
        }
    });

    $('#txtPsn').searchbox('setValue', null);

    //初始化客户表
    $objCustomer = $('#customer').datagrid({
        loadMsg: '数据加载中...',
        pageSize: 20,
        pagination: true,
        fitColumns: true,
        striped: true,
        fit: true,
        border: false,
        rownumbers: true,
        singleSelect: true,
        idField: 'ClientNo_CName',
        toolbar: '#rightBars',
        columns: csmColumns(),

        method: 'GET',
        loadFilter: function (data) {
			var result = {rows:[],total:0};
			if(data.success) {
				data = eval('(' + data.ResultDataValue + ')'); //eval()方法把字符串转换成JSON格式
				if(data) {
					var length = data.rows ? data.rows.length : 0;
					if(length == 0) {
						data.rows = [];
					}
					for(var i = 0; i < length; i++) {
						data.rows[i].ClientNo_CName = '(' + data.rows[i].ClIENTNO + ')' + data.rows[i].CNAME;
					}
					result.rows = data.rows ? data.rows : result.rows;
					result.total = data.total ? data.total : 0;
				}
			}
			return result;
		}
    });

    $('#txtCsm').searchbox('setValue', null);

    //初始化未选客户列表
    $('#unSelect').datagrid({
        title: '未选',
        loadMsg: '数据加载中...',
        striped: true,
        fit: true,
        border: false,
        rownumbers: true,
        singleSelect: false,
        onDblClickRow: function (index, row) {
            $('#selected').datagrid('insertRow', {
                index: 0,
                row: row
            });
            $('#unSelect').datagrid('deleteRow', index);
        },
        columns: dlgColumns()
    });

    //初始化已选客户列表
    $('#selected').datagrid({
        title: '已选',
        loadMsg: '数据加载中...',
        striped: true,
        fit: true,
        border: false,
        rownumbers: true,
        singleSelect: false,
        onDblClickRow: function (index, row) {
            $('#unSelect').datagrid('insertRow', {
                index: 0,
                row: row
            });
            $('#selected').datagrid('deleteRow', index);
        },
        columns: dlgColumns()
    });

});

/*********************人员区域begin*********************/
//创建数据列
function psnColumns() {
    var columns = [
        [
            {field: 'chk', checkbox: true, hidden: true},
            {field: 'Name', title: '物流人员名称', width: fixWidth(0.2)},
            {field: 'Account', title: '物流人员账号', width: fixWidth(0.2)},
            {field: 'Sex', title: '性别', width: fixWidth(0.2)},
            {field: 'Client', title: '所属单位', width: fixWidth(0.2)},
            {field: 'opt', title: '操作', align: 'right', width: fixWidth(0.1), align: 'center',
                formatter: function (value, row, index) {
                    var edit = '<a href="javascript:void(0)" onclick="edit(' + "'" + row.Name + "','" + row.Account + "','" + row.Client + "'" + ')">修改</a>';//'+row+'
                    return edit;
                }}
        ]
    ];
    return columns;
}

//加载数据表
function gridLoad(data) {
    var localData = {};//把返回来的数据格式转换成标准格式{'total':1,'rows':[]}
    if (data) {
        data = columnToChinese(data);//指定列转换成中文显示
        localData.total = data.list.count;//总数
        localData.rows = data.list.list;//数据列
        $obj.datagrid('loadData', localData);
    }
}

//刷新
function refreshPsn() {
    $('#txtPsn').searchbox('setValue', null);
    $('#personnel').datagrid('load',{presonname: null});//查询参数设置为空
}

//查询人员列表
function searchPsn(value) {
    $('#personnel').datagrid('load',{presonname:value});//如果要查询的记录不在第一页呢
}
/*********************人员区域end*********************/

/*********************客户区域begin*********************/
//创建数据列
function csmColumns() {
    var columns = [
        [
            {field: 'ClientNo_CName', title: '客户名称', width: fixWidth(0.4)}
        ]
    ];
    return columns;
}

//初始化下拉框
function initCombobox(account, client) {
    $('#cmbClient').combobox({
        valueField: 'ClIENTNO',
        textField: 'ClientNo_CName',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetLogisticsCustomerByDeliveryIDAndType?selectedflag=0&account=' + account,
        method: 'GET',
        loadFilter: function (data) {
            if (data.success) {
                var result = {};
                data = eval('(' + data.ResultDataValue + ')');//eval()方法把字符串转换为JSON格式
                var length = data.rows ? data.rows.length : 0;
                for (var i = 0; i < length; i++) {
                    data.rows[i].ClientNo_CName = '(' + data.rows[i].ClIENTNO + ')' + data.rows[i].CNAME;
                }
                return data.rows ? data.rows : [];
            }
        },
        onLoadSuccess: function () {
            var opts = $(this).combobox('options'),
                data = $(this).combobox('getData');
            if (data.length > 0) {
                if (client) {
                    var clientNO;
                    for (var i = 0; i < data.length; i++) {
                        if (client == data[i].ClientNo_CName) {
                            clientNO = data[i].ClIENTNO;
                            break;
                        }
                    }
                    $(this).combobox('setValue', clientNO);
                } else {
                    $(this).combobox('select', data[0][opts.valueField]);
                }
            }
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options');
            return row[opts.textField].indexOf(q) > -1;
        }
    });
}

//创建已选或未选的客户数据列
function dlgColumns() {
    var columns = [
        [
            {field: 'ClientNo_CName', title: '客户名称', width: fixWidth(0.23)}
        ]
    ];
    return columns;
}

//加载客户表
function loadCsm(grid, account, flag) {
    $(grid).datagrid({
        //url: 'csmData.txt',
        //flag取值：0全部1已选或2未选
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetLogisticsCustomerByDeliveryIDAndType'+'?selectedflag='+flag+'&account='+account,
        method: 'GET',
        loadFilter: function (data) {
			var result = {rows:[],total:0};
			if(data.success) {
				data = eval('(' + data.ResultDataValue + ')'); //eval()方法把字符串转换成JSON格式
				if(data) {
					var length = data.rows ? data.rows.length : 0;
					for(var i = 0; i < length; i++) {
						data.rows[i].ClientNo_CName = '(' + data.rows[i].ClIENTNO + ')' + data.rows[i].CNAME;
					}
					result.rows = data.rows ? data.rows : result.rows;
					result.total = data.total ? data.total : 0;
				}
			}
			return result;
		},
        onLoadSuccess: function (data) {
            if (grid == '#unSelect') {

                //dragToRight();//拖拽右移
            } else if (grid == '#selected') {
                //dragToLeft();//拖拽左移

            }
        }
    });
}

//刷新客户列表
function refreshCsm() {
    $objCustomer.datagrid('load',{itemkey:null});
    $('#txtCsm').searchbox('setValue', null);
}

//查询客户列表
function searchCsm(value) {
    //askService('searchCustomer', 'selectedflag=1&account=' + psnAccount + "&itemkey=" + value, 'GET', 'GetLogisticsCustomerByDeliveryIDAndType');
    $objCustomer.datagrid('load',{itemkey:value});
}

//客户修改
function edit(name, account, client) {
    $('#dlg').dialog({
        minimizable: true,
        resizable: true,
        //width: fixWidth(0.5),//如果还是被挤压，明天直接用顶宽
        height: fixHeight(0.85),
        width:670
       // height:500
    });
    $('#dlg').dialog('open');
    $('#dlg').window('center');
    $('#txtDlg').searchbox('setValue', null);
    initCombobox(account, client);
    loadCsm('#unSelect', account, 2);//加载未选客户列表
    loadCsm('#selected', account, 1);//加载已选客户列表

    $('#logisticsName').html(name);
    $('#logisticsAccount').html(account);
}

//刷新对话框
function refreshDlg() {
    var account = $('#logisticsAccount').text();
    loadCsm('#unSelect', account, 2);//加载未选客户列表
    $('#txtDlg').searchbox('setValue', null);
}
//对话框查询
function dlgSearch(value) {

    $('#unSelect').datagrid('load',{ itemkey:value});
}

//拖拽左移
function dragToLeft() {
    var rows = $('#selected').datagrid('getRows') || [],
        length = rows.length;
    var opts = $('#selected').datagrid('options');
    var trs = opts.finder.getTr(this, 1);
    $('#selected').draggable({
        revert: true,
        proxy: 'clone'
    });

    $('#unSelect').droppable({
        onDrop: function (e, source) {

        }
    });
}

//全部左移
function leftAllMove() {
    var rows = $('#selected').datagrid('getRows') || [],
        oldRows = $('#unSelect').datagrid('getRows') || [],
        newRows = oldRows ? rows.concat(oldRows) : rows,
        data = {success: true, ResultDataValue: Shell.util.JSON.encode({total: newRows.length, rows: newRows})};

    $('#unSelect').datagrid('loadData', data);
    $('#selected').datagrid('loadData', {success: true, ResultDataValue: Shell.util.JSON.encode({total: 0, rows: []})});

}

//左移
function leftMove() {
    var rows = $('#selected').datagrid('getSelections'),
        length = rows.length,
        index;
    for (var i = 0; i < length; i++) {
        index = $('#selected').datagrid('getRowIndex', rows[i]);
        $('#unSelect').datagrid('insertRow', {
            index: 0,
            row: rows[i]
        });
        $('#selected').datagrid('deleteRow', index);
    }
    $('#selected').datagrid('clearSelections');
}

//拖拽右移
function dragToRight() {
    $('#unSelect').draggable({
        onDrop: function (e, source) {
            var index = $('selected').datagrid('getRowIndex', source);
            $('#unSelect').datagrid('insertRow', {
                index: index,
                row: source
            })
        }
    });
}

//全部右移
function rightAllMove() {
    var rows = $('#unSelect').datagrid('getRows'),
        oldRows = $('#selected').datagrid('getRows'),
        newRows = oldRows ? rows.concat(oldRows) : rows,
        data = {success: true, ResultDataValue: Shell.util.JSON.encode({total: newRows.length, rows: newRows})};

    $('#selected').datagrid('loadData', data);
    $('#unSelect').datagrid('loadData', {success: true, ResultDataValue: Shell.util.JSON.encode({total: 0, rows: []})});

}

//右移
function rightMove() {
    var rows = $('#unSelect').datagrid('getSelections'),
        length = rows.length,
        index;
    for (var i = 0; i < length; i++) {
        index = $('#unSelect').datagrid('getRowIndex', rows[i]);
        $('#selected').datagrid('insertRow', {
            index: 0,
            row: rows[i]
        });
        $('#unSelect').datagrid('deleteRow', index);
    }
    $('#unSelect').datagrid('clearSelections');
}

//下拉框验证
function getClientNo() {
    var text = $('#cmbClient').combobox('getText'),
        value = $('#cmbClient').combobox('getValue'),
        data = $('#cmbClient').combobox('getData'),
        opts = $('#cmbClient').combobox('options'),
        flag = false;
    for (var i = 0; i < data.length; i++) {
        if (value == data[i][opts.valueField]) {
            flag = true;
            break;
        }
    }
    if (!flag) {
        return false;
    }
    return value;
};

//请求服务器
function askService(action, param, type, serviceName) {
    $.ajax({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/' + serviceName,
        data: param,
        dataType: 'json',
        type: type,
        timeout: 10000,
        async:true,
        contentType: 'application/json',
        success: function (data) {
            if (data.success) {
                if (action !== 'add') {
                    switch (action) {
                        case'searchPerson':
                            $objPersonnel.datagrid('loadData', data);
                            break;
                        case'searchCustomer':
                            $('#customer').datagrid('loadData', data);
                            break;
                        case'searchUn':
                            $('#unSelect').datagrid('loadData', data);
                            break;
                    }
                }
            }
        },
        error: function (data) {
            $.messager.alert('提示信息', data.ErrorInfo, 'error');

        }
    });
}

//配置参数
function setParam(ClientNo) {
    var addData = '',
        account = $('#logisticsAccount').text(),
        rows = $('#selected').datagrid('getRows'),
        length = rows ? rows.length : 0;

    if (length >= 0) {
        addData += '{"' + "strentity" + '":{"' + "Account" + '":"' + account + '","' + "ClientNo" + '":' + ClientNo + ',"' + "ClientList" + '":[';
        if (length == 0) {
            addData += 0;
        } else {
            var clientNO = parseInt(rows[0].ClIENTNO);
            addData += clientNO;
        }
        for (var i = 1; i < length; i++) {
            clientNO = parseInt(rows[i].ClIENTNO);
            addData += ',' + clientNO;
        }
        addData += ']}}';

        askService('add', addData, 'POST', 'UpdateLogisticsDeliveryCustomer');
    }
}

//保存
function save() {
    var value = getClientNo();
    if (!value) {
        $.messager.alert('警告', '请检查所属单位的完整性', 'warning');
        return;
    }
    setParam(value);
    $('#dlg').dialog('close');
    $('#personnel').datagrid('load', {presonname: null});//查询参数设置为空
    $objCustomer.datagrid('load',{itemkey:null});
}

//取消
function cancel() {
    $('#dlg').dialog('close');
}

/*********************客户区域end*********************/

//设置列宽
function fixWidth(percent) {
    return document.body.clientWidth * percent;
}

//设置高度
function fixHeight(percent) {
    return document.body.clientHeight * percent;
}
