
$(function () {
    $('#dg').datagrid({
        loadMsg: '数据加载中...',
        //fit: true,
        toolbar: "#toolbar",
        singleSelect: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: true,
        idField: 'id',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetBill',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'id', title: '序号', width: setWidth(0.075) },
                { field: 'monthname', title: '对账月', width: setWidth(0.125) },
                { field: 'clientname', title: '客户名称', width: setWidth(0.175) },
                { field: 'status', title: '确认状态', width: setWidth(0.1) },
                { field: 'createdate', title: '生成日期', width: setWidth(0.125) },
                { field: 'remark', title: '备注', hidden: true },
                //{ field: 'fild', title: '虚拟目录', hidden: true },
                { field: 'filepath', title: '项目虚拟目录', hidden: true },
                {
                    field: 'url', title: '下载', width: setWidth(0.05), formatter: function (value, row, index) {                       
                        var d = "<a href='#' onclick=Url(" + row.id + ")>下载</a>";
                        return d;
                    }
                },
                {
                    field: 'urlitem', title: '项目对账', width: setWidth(0.075), formatter: function (value, row, index) {
                        var p = "<a href='#' onclick=Url2("+row.id+")>项目对账</a>";
                        return p;
                    }
                },
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total, rows: result.rows };
        },
        onSelect: function (rowIndex, rowData) {
            $('#fm').form('load', rowData)


        },
    });
   
})
//下载
function Url(id) {
    var iframe = document.getElementById("downloadiframe");
    iframe.src = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DownLoadExcel?type=0&id=' + id;
}
//项目对账
function Url2(id) {
    var iframe = document.getElementById("downloadiframe");
    iframe.src = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DownLoadExcel?type=1&id=' + id;
}
//设置列宽
function setWidth(percent) {
    return document.body.clientWidth * percent;
}
//更新页面
function update() {
    var currentTime = new Date().getTime();
    $('#dg').datagrid('load', {monthname:null,clientname:null,status:null,time: currentTime });
    $('#fm').form('clear')
    reset2();
}
//查询按钮
function query() {   
    $('#dg').datagrid({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetBill',
        queryParams: {
            monthname : $("#txtmonthname").val(),
            clientname : $("#txtclientname").val(),
            status : $("#cbstatus").combobox('getValue')
        }
    });
}
//保存按钮
function save() {
    var errors = 0;
    var id = $("#txtNo").val();
    var monthname = $("#txtMonthname").val();
    var clientname = $("#txtClientname").val();
    var createdate = $("#txtCreatedate").val();
    var remark = $("#txtRemark").val();
    if (remark=="") {
        errors += 1;
    }
    var status = $("#cbStatus").combobox('getValue');
    var r = '{"jsonentity":{"id":' + id + ',"createdate":"' + createdate + '","monthname":"' + monthname + '","clientname":"' + clientname + '","remark":"' + remark + '","status":"' + status + '"}}';
    if (errors>0) {
        $.messager.alert('提示', '请检查输入值的完整性', 'warning');
    } else {
        $.ajax({
            type: 'post',
            contentType: 'application/json',
            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateBill',
            data: r,
            dataType: 'json',
            success: function (data) {
                if (data.success == true) {
                    // $.messager.alert('提示', '修改数据成功！');
                    var currentTime = new Date().getTime();
                    $('#dg').datagrid('load', { time: currentTime });

                } else {
                    $.messager.alert('提示', '修改数据失败！失败信息：' + data.msg);
                }
            }
        });
    }
}
//重置按钮
function reset1() {
    $("#txtRemark").val("");
    $("#cbStatus").combobox('select', '未确认');
}
function reset2() {
    $("#txtmonthname").textbox("clear");
    $("#txtclientname").textbox("clear");
    $("#cbstatus").combobox('select', '未确认');
}

