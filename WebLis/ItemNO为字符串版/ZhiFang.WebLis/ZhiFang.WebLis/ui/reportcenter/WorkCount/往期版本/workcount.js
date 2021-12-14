var LabCode;
var a = 0;
var r = 0; //查询后方可生成Excel
//程序入口
$(function () {
    $('#btn_select').bind('click', function () {
        onSearch();
        if (r == 0)
            return r = 1;

    });
    //时间初值
    function formatter(date) {
        var y = date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
    }

    function parser(s) {
        if (!s) return new Date();
        var ss = (s.split('-'));
        var y = parseInt(ss[0], 10);
        var m = parseInt(ss[1], 10);
        var d = parseInt(ss[2], 10);
        //        if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        //            return new Date();
        //        } else {
        return new Date(y, m, d - 1);
        //        }
    }
    //页面加载  
    $(function () {
        //设置时间  
        var curr_time = new Date();
        $("#txtoperdatebegin").datebox("setValue", formatter(curr_time));
        $("#txtoperdateend").datebox("setValue", formatter(curr_time));
        //获取时间  
        var date_s = $("#txtoperdatebegin").datebox("getValue");
        var date_e = $("#txtoperdateend").datebox("getValue");
    });
    //客户。。。
    $('#ClientNo').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=CLIENTELE&fields=ClIENTNO,CNAME',
        method: 'GET',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            data = eval('(' + data.ResultDataValue + ')').rows || []; //eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData');
            //            if (data.length > 0) {
            //                $(this).combobox('select', data[0].ClIENTNO); //默认第一项的值
            //            }
        }
    });
    //项目。。。
    $('#ItemNo').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=TESTITEM&fields=ITEMNO,CNAME',
        method: 'GET',
        valueField: 'ITEMNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            data = eval('(' + data.ResultDataValue + ')').rows || []; //eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData');
            //            if (data.length > 0) {
            //                $(this).combobox('select', data[0].ITEMNO); //默认第一项的值
            //            }
        }
    });
    $('#jj').panel('close');
    $('#dg').datagrid({
        border: false,
        collapsible: false,
        idField: 'ClientNo',
        url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetStaticRecOrgSamplePrice',
        //url:'data.txt',NRequestFromService
        queryParams: {
            tablename: "StaticRecOrgSamplePrice",
            labcode: LabCode
        },
        method: 'get',
        loadMsg: '数据加载...',
        rownumber: true,
        pagination: true,
        checkOnSelect: false,
        selectOnCheck: true,
        striped: true,
        toolbar: "#toolbar",
        columns: createColumns(),
        singleSelect: true,
        fit: true,
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')'),
                    result = {};
                result.total = list.total || 0;
                result.rows = list.rows || [];
                return result;
            }
        }
    });
    //分页
    $($('#dg').datagrid('getPager')).pagination({

        pageSize: 10, //每页显示的记录条数，默认为10           
        pageList: [10, 50, 100, 200, 500], //可以设置每页记录条数的列表
        beforePageText: '第',
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
    });
    function createColumns() {
        var columns = [
    [
                { field: 'OperDate', title: '开单日期', width: '10%' },
                { field: 'ClientNo', title: '医院编号', width: '10%' },
                { field: 'ClientName', title: '医院名称', width: '10%' },
                { field: 'SampleNum', title: '标本数量', width: '10%' },
                { field: 'ItemNo', title: '项目编号', width: '10%' },
                { field: 'ItemName', title: '项目名称', width: '15%' },
                { field: 'ItemNum', title: '项目数量', width: '10%' },
                { field: 'price', title: '项目单价', width: '10%' },
                { field: 'ItemTotalprice', title: '总计', width: '15%' }
                    ]]
        return columns;
    }
});
/**查询*/
function onSearch() {
    $('#gd').panel('open');
    $('#jj').panel('close');
    var date_s = $("#txtoperdatebegin").datebox("getValue"),
        date_e = $("#txtoperdateend").datebox("getValue"),
        ClientNo = $("#ClientNo").combobox("getValue"),
        ItemNo = $("#ItemNo").combobox("getValue"),
        param = {};


    //开始结束日期必须存在。
    if (!date_s) return null;
    if (!date_e) return null;
    if (date_e < date_s) return null;

    param.StartDate = date_s;
    param.EndDate = date_e;
    if (ClientNo)
        param.labName = ClientNo;
    if (ItemNo)
        param.TestItem = ItemNo;
    if ($('#dg').datagrid("options").url) {
        $('#dg').datagrid('load', param);
    }
    else//初始化列表的情况
    {
        $('#dg').datagrid("options").url = serverUrl.SelectReportListUrl;
        $('#dg').datagrid('load', param);
    }
    return n = 1;
}
//生成Excel
function sExcel() {
    var OperDateBegin = $("#txtoperdatebegin").datebox("getValue"),
        OperDateEnd = $("#txtoperdateend").datebox("getValue"),
        ClientNo = $("#ClientNo").combobox("getValue"),
        ItemNo = $("#ItemNo").combobox("getValue");
    //    if (ClientNo == ""||ClientNo==null)
    //        ClientNo = null;
    //    if (ItemNo == ""||ItemNo==null)
    //        ItemNo = null;
    if (ClientNo == "")
        ClientNo = null;
    if (ItemNo == "")
        ItemNo = null;
    var pager = $('#dg').datagrid('getPager'),
        page = pager.page,
        rows = pager.rows,
        opt = $('#dg').datagrid('options'),
        p1 = opt.pageNumber,
        r1 = opt.pageSize;

    if (r == 1) {
        r = 0;
        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFromService.svc/GetStaticRecOrgSamplePrice',
            dataType: 'json',
            data: { StartDate: OperDateBegin, EndDate: OperDateEnd, rows: r1, page: p1, labName: ClientNo, TestItem: ItemNo },
            success: function (data) {
                if (data.success == true) {
                    $('#gd').panel('close');
                    $('#jj').panel('open');
                    var str = data.ResultDataValue;
                    var splits = str.split("#");
                    var startname = splits[0];
                    var lastname = splits[1];
                    $('#btnload').attr("enabled", true);
                    $("#query").attr("src", Shell.util.Path.rootPath + startname);
                    a = Shell.util.Path.rootPath + lastname;
                } else {
                    $.messager.alert('提示', '提取数据失败！失败信息：' + data.msg);
                }
            }
        });
    }
}
function btnexport() {
    $("#preview").attr("src", a);
}