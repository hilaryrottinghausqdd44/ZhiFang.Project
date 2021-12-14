
//程序入口

$(function () {
    $('#btn_select').bind('click', function () {
        onSearch();
    });

    $('#cmbDateType').combobox({
        panelHeight:80,
        width:80,
        valueField: 'value',
        textField: 'text',
        data: [{
            value: 'day',
            text: '年-月-日',
            selected:true
        },{
            value: 'month',
            text: '年-月'
        },{ 
            value: 'year',
            text: '年'
        }],
        onSelect:function(record){
            if(record.value=='year'){
                $('#txtDateBegin').attr('onfocus',"WdatePicker({skin:'whyGreen',dateFmt:'yyyy年'})");
                $('#txtDateEnd').attr('onfocus',"WdatePicker({skin:'whyGreen',dateFmt:'yyyy年'})");
            }else if(record.value=='month'){
                $('#txtDateBegin').attr('onfocus',"WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月'})");
                $('#txtDateEnd').attr('onfocus',"WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月'})");
            }else{
                $('#txtDateBegin').attr('onfocus',"WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月dd日'})");
                $('#txtDateEnd').attr('onfocus',"WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月dd日'})");
            }
        }
    });
    var curr_time = new Date(),
        curr_s=formatter(curr_time,'CN'),
        curr_e=formatter(curr_time,'CN');

    $('#txtDateBegin').val(curr_s);
    $('#txtDateEnd').val(curr_e);


    var date_s = converDate($("#txtDateBegin").val()),
        date_e =converDate( $("#txtDateEnd").val())+' 23:59:5';


    //时间初值
    function formatter(date,type) {
        var y = date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        var result='';
        if(type=='CN'){
            result=y + '年' + (m < 10 ? ('0' + m) : m) + '月' + (d < 10 ? ('0' + d) : d)+'日';
        }else{
            result=y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
        }
        return result;
    }

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
        },
        filter:function(q,row){
            var opts = $(this).combobox('options'),
                shortCode = row['SHORTCODE'] || "",
                CName=row[opts.textField] || "";

            if(CName.indexOf(q)>-1){
                return true;
            }
            q= q.toUpperCase();
            if(shortCode.indexOf(q)>-1){
                return true;
            }
            return false;
        }
    });

    $('#jj').panel('close');   //默认关闭生成的panel。
    $('#tblOperatorCount').datagrid({
        border: false,
        collapsible: false,
        idField: 'ClientNo',
        url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetOpertorWorkCount',
        queryParams: {
            OperDateSart: date_s,
            OperDateEnd: date_e
        },
        method: 'get',
        loadMsg: '数据加载...',
        rownumber: true,
        pagination: true,
        checkOnSelect: false,
        selectOnCheck: true,
        striped: true,
        toolbar: "#toolbar",
        fitColumns:true,
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
        },
        onBeforeLoad:function(param){
            if(param.page==0){
                return false;
            }
        },
        onLoadSuccess: function (data) {
            var barcodes= 0,
                money=0;
            if(data.rows.length){
                for(var i=0;i<data.rows.length;i++){
                    barcodes+=data.rows[i].BarcodeNum;
                    money+=data.rows[i].SumMoney;
                }
                $('#tblOperatorCount').datagrid('appendRow',{
                    OperDate:"汇总",
                    BarcodeNum:barcodes,
                    SumMoney:money
                });
            }
        }
    });

    function createColumns() {
        var columns = [
            [
                {field: 'OperDate', title: '开单日期',  width: fixWidth(0.15) },
                {field: 'ClientNo', title: '医院编号',width:  fixWidth(0.15)},
                {field: 'ClientName', title: '医院名称', width: fixWidth(0.15)},
                {field: 'Operator', title: '操作人员', width:  fixWidth(0.15)},
                {field: 'BarcodeNum', title: '条码数',width:  fixWidth(0.15)},
                {field: 'SumMoney', title: '金额', width:  fixWidth(0.15)}
            ]]
        return columns;
    }
});

//设置列宽
function fixWidth(percent) {
   return document.body.clientWidth * percent;
}

function converDate(date){
    if(date.indexOf('日')>-1){
        date=date.replace('日','');
        date=date.replace('月','-');
        date=date.replace('年','-');
    }else if(date.indexOf('月')>-1){
        date=date.replace('月','');
        date=date.replace('年','-');
    }else {
        date=date.replace('年','');
    }
    return date;
}

//获取查询条件
function getQueryConfig(){
    var date_s = converDate($("#txtDateBegin").val()),
        date_e =converDate( $("#txtDateEnd").val()),
        ClientNo = $("#ClientNo").combobox("getValue"),
        Operator = $("#txtOperator").textbox("getValue"),
        DateType=$('#cmbDateType').combobox('getValue'),
        param = {};

    //开始结束日期必须存在。
    if (!date_s) return null;
    if (!date_e) return null;
    if (date_e < date_s){
        $.messager.alert('提示信息', "结束日期不能小于开始日期", 'info');
        return ;
    }

    if (DateType)
        param.DateType = DateType;
    if(param.DateType=='year'){
        date_s=date_s.split('-')[0];
        var ar=date_e.split('-');
        date_e=ar[0]+'-12-31 23:59:59';
    }
    if(param.DateType=='month'){
        date_s=date_s.split('-')[0]+'-'+date_s.split('-')[1]+'-01';
        var ar=date_e.split('-');
        ar[1]=ar[1]==2?28:ar[1];
        date_e=ar[0]+'-'+ar[1]+'-'+'30 23:59:59';
    }
    if(param.DateType=='day' || param.DateType==null){
        date_e=date_e+' 23:59:59';
    }
    param.OperDateSart = date_s;
    param.OperDateEnd = date_e;
    if (ClientNo)
        param.ClientNo = ClientNo;
    if (Operator)
        param.Operator = Operator;
    if (DateType)
        param.DateType = DateType;

    return param;
}
/**查询*/
function onSearch() {
    $('#gd').panel('open');
    $('#jj').panel('close');

    var param=getQueryConfig();
    if ($('#tblOperatorCount').datagrid("options").url) {
        $('#tblOperatorCount').datagrid('load', param);
    }
    else//初始化列表的情况
    {
        $('#tblOperatorCount').datagrid("options").url = serverUrl.SelectReportListUrl;
        $('#tblOperatorCount').datagrid('load', param);
    }
    return n = 1;
}

//生成Excel
function sExcel() {
    $('#tblOperatorCount').datagrid('options').loadMsg='正在生成Excel...';
    $('#tblOperatorCount').datagrid('loading');
    var param=getQueryConfig();

        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFromService.svc/GetOperatorWorkCountExcel',
            dataType: 'json',
            data:param ,
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
    $('#tblOperatorCount').datagrid('options').loadMsg='正在加载数据...';
}

function btnexport() {
    $("#preview").attr("src", a);
}