var r = "";
var n = 0;
var no = 0;
var param = null;
var OperdateBegin = "";
var OperdateEnd = "";
var ItemName = "";
var ClientName = "";
var BarCode = "";
var PatNo = "";

////页面加载  
//$(function () {
//    //设置时间  
//    var curr_time = new Date();
//    $("#txtoperdatebegin").datebox("setValue", formatter(curr_time));
//    $("#txtoperdateend").datebox("setValue", formatter(curr_time));
//    //获取时间  
//    var date_s = $("#txtoperdatebegin").datebox("getValue");
//    var date_e = $("#txtoperdateend").datebox("getValue");
//});

//时间验证
function CheckTime() {
    n = 0;
    OperdateBegin = "";
    OperdateEnd = "";
    r = "";

    OperdateBegin = $("#txtoperdatebegin").datebox("getValue");
    OperdateEnd = $("#txtoperdateend").datebox("getValue");
    if (OperdateBegin != null && OperdateBegin != "") {
        r += '"OperdateBegin":"' + OperdateBegin + '"';
        if (OperdateEnd != null && OperdateEnd != "") {
            r += ',"OperdateEnd":"' + OperdateEnd + '"';
            if (OperdateBegin > OperdateEnd) {
                return n = 1;
            } else {
                n = 4;
            }
        } else {
            return n = 2;
        }
    } else {
        return n = 3;
    }
}

//查询按钮
function DoSearch() {
    ItemName = "";
    ClientName = "";
    BarCode = "";
    PatNo = "";
    ItemName = $("#txt_ItemeName").combobox("getValue");
    if (ItemName != null && ItemName != "") {
        r += ',"ItemName":"' + ItemName + '"';
    }

    ClientName = $("#txt_ClientName").combobox("getValue");
    if (ClientName != null && ClientName != "") {
        r += ',"ClientName":"' + ClientName + '"';
    }

    BarCode = $("#txt_BarCode").val();
    if (BarCode != null && BarCode != "") {
        r += ',"BarCode":"' + BarCode + '"';
    }

    PatNo = $("#txt_PatNo").val();
    if (PatNo != null && PatNo != "") {
        r += ',"PatNo":"' + PatNo + '"';
    }
}

////获取文本值
//function MakeSearch() {
//    r = "";
//    OperdateBegin = $("#txtoperdatebegin").datebox("getValue");
//    OperdateEnd = $("#txtoperdateend").datebox("getValue");
//    ItemName = $("#txt_ItemeName").combobox("getValue");
//    ClientName = $("#txt_ClientName").combobox("getValue");
//    BarCode = $("#txt_BarCode").combobox("getValue");
//    PatNo = $("#txt_PatNo").combobox("getValue");
//    r += '"OperdateBegin":"' + OperdateBegin + '","OperdateEnd":"' + OperdateEnd + '"';

//    if (ItemName != null && ItemName != "") {
//        r += ',"ItemName":"' + ItemName + '"';
//    }
//    if (ClientName != null && ClientName != "") {
//        r += ',"ClientName":"' + ClientName + '"';
//    }
//    if (BarCode != null && BarCode != "") {
//        r += ',"BarCode":"' + BarCode + '"';
//    }
//    if (PatNo != null && PatNo != "") {
//        r += ',"PatNo":"' + PatNo + '"';
//    }
//}

//默认时间
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
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}

$(function () {
    //TimeLoad();
    //时间提示
    $('#txtoperdatebegin').datebox({
        required: true,
        missingMessage: "请输入开始时间！"
    });

    $('#txtoperdateend').datebox({
        required: true,
        missingMessage: "请输入结束时间！"
    });

    //设置时间
    var curr_time = new Date();
    $("#txtoperdatebegin").datebox("setValue", formatter(curr_time));
    $("#txtoperdateend").datebox("setValue", formatter(curr_time));
    $("#gd").panel('open');
    $("#xz").panel('close');

    //下拉列表
    //项目名称
    $('#txt_ClientName').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=CLIENTELE&fields=CLIENTNO,CNAME',
        method: 'GET',
        value: "",
        valueField: 'CLIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            data = eval('(' + data.ResultDataValue + ')').rows || []; //eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData');
            //            if (data.length > 0) {
            //                $(this).combobox('select', data[0].CLIENTNO); //默认第一项的值
            //            }
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options'),
                            shortCode = row['SHORTCODE'] || "",
                            CNAME = row[opts.textField] || ""

            if (CNAME.indexOf(q) > -1) {
                return true;
            }
            q = q.toUpperCase();
            if (shortCode.indexOf(q) > -1) {
                return true;
            }
            return false;
        }
    });

    //姓名
    $('#txt_ItemeName').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=TESTITEM&fields=ITEMNO,CNAME',
        method: 'GET',
        value: "",
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
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options'),
                    shortCode = row['SHORTCODE'] || "",
                    CNAME = row[opts.textField] || "";

            if (CNAME.indexOf(q) > -1) {
                return true;
            }
            q = q.toUpperCase();
            if (shortCode.indexOf(q) > -1) {
                return true;
            }
            return false;
        }
    });

    //datagrid页面加载显示分页
    $("#dg").datagrid({
        fit: true,
        toolbar: "#toolbar",
        loadMsg: '数据加载...',
        fit: true,
        border: false,
        pagination: true,
        checkOnSelect: false,
        selectOnCheck: true,
        rownumbers: true,
        collapsible: false,
        singleSelect: true,
        idField: 'ClientNo',
        url: Shell.util.Path.rootPath + '',
        method: 'get',
        striped: true,
        columns: [[
            { field: 'ClientNo', title: '编号', width: '9%' },
            { field: 'ClientName', title: '姓名', width: '9%' },
            { field: 'GenderName', title: '性别', width: '9%' },
            { field: 'Age', title: '年龄', width: '9%' },
            { field: 'BarCode', title: '条码号', width: '9%' },
            { field: 'ItemName', title: '项目名称', width: '9%' },
            { field: 'Price', title: '单价', width: '9%' },
            { field: 'SampleNum', title: '标本数量', width: '9%' },
            { field: 'ItemNo', title: '项目编号', width: '9%' },
            { field: 'PatNo', title: '病历号', width: '9%' },
            { field: 'OperDate', title: '日期', width: '9%' }
        ]],
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        },
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')');
                sr = {};
                sr.total = list.total || 0;
                sr.rows = list.rows || [];
                $('#btn_excel').show();
                $('#btn_print').show();
                return sr;
            }
        }
    });
});


$(function () {
    $("#btn_select").bind('click', function () {
        var sr = "";
        CheckTime();
        $("#gd").panel('open');
        $("#xz").panel('close');
        if (n == 3) {
            alert("开始时间不能为空！");
        } else {
            if (n == 2) {
                alert("结束时间不能为空！");
            } else {
                if (n == 1) {
                    alert("开始时间不能大于结束时间！");
                } else {
                    if (n == 4) {
                        n = 5;
                        DoSearch();
                        sr = 'jsonentity={' + r + '}';
                        //显示数据
                        $("#dg").datagrid({
                            fit: true,
                            toolbar: "#toolbar",
                            loadMsg: '数据加载...',
                            fit: true,
                            border: false,
                            pagination: true,
                            checkOnSelect: false,
                            selectOnCheck: true,
                            rownumbers: true,
                            collapsible: false,
                            singleSelect: true,
                            idField: 'ClientNo',
                            url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetStaticPersonTestItemPriceList?' + sr,
                            method: 'get',
                            striped: true,
                            columns: [[
                                { field: 'ClientNo', title: '编号', width: '9%' },
                                { field: 'ClientName', title: '姓名', width: '9%' },
                                { field: 'GenderName', title: '性别', width: '9%' },
                                { field: 'Age', title: '年龄', width: '9%' },
                                { field: 'BarCode', title: '条码号', width: '9%' },
                                { field: 'ItemName', title: '项目名称', width: '9%' },
                                { field: 'Price', title: '单价', width: '9%' },
                                { field: 'SampleNum', title: '标本数量', width: '9%' },
                                { field: 'ItemNo', title: '项目编号', width: '9%' },
                                { field: 'PatNo', title: '病历号', width: '9%' },
                                { field: 'OperDate', title: '日期', width: '9%' }
                            ]],
                            onBeforeLoad: function (param) {
                                if (param.page == 0) return false;
                            },
                            loadFilter: function (data) {
                                if (data.success) {
                                    var list = eval('(' + data.ResultDataValue + ')');
                                    sr = {};
                                    sr.total = list.total || 0;
                                    sr.rows = list.rows || [];
                                    $('#btn_excel').show();
                                    $('#btn_print').show();
                                    return sr;
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
                    }
                }
            }
        }
    });

    $("#btn_excel").bind('click', function () {
        var jr = '{"jsonentity":';
        $("#gd").panel('close');
        $("#xz").panel('open');
        var pager = $("#dg").datagrid('getPager');
        var page = pager.page;
        var rows = pager.rows;
        var opt = $("#dg").datagrid('options');
        var pl = opt.pageNumber;
        var rl = opt.pageSize;

        if (n == 5) {
            jr += '{' + r + ',"page":"' + pl + '","rows":"' + rl + '"}}';
            //            MakeSearch();
            no = 0;
            //            jr += r += '}';
            $.ajax({
                type: "post",
                contentType: "application/json",
                url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFromService.svc/GetStaticPersonTestItemPriceItem',
                dataType: "json",
                //data: { page: pl, rows: rl, jsonentity: jr },
                data: jr,
                success: function (data) {
                    if (data.success == true) {
                        var str = data.ResultDataValue;
                        var splits = str.split("#");
                        var startname = splits[0];
                        var lastname = splits[1];
                        $("#btn_down").show();
                        $("#query").attr("src", Shell.util.Path.rootPath + startname);
                        no = Shell.util.Path.rootPath + lastname;
                    } else {
                        $.messager.alert('提示', '提取数据失败！失败信息：' + data.msg);
                    }
                }
            });
        } else {
            alert("请先进行查询！");
        }
    });
});

//Excel
function btnexport() {
    $("#preview").attr("src", no);
}