/**
 * Created by ganwh on 2015/6/15.
 * 统计查询
 */

$(function () {
    //医生
    $('#cmbDoctor').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetCenterDoctorAllList',
        method: 'GET',
        valueField: 'DoctorNo',
        textField: 'CName',
        onLoadSuccess: function () {
            $(this).combobox('select', '');//默认为空
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options'),
                shortCode = row['ShortCode'] || "",
                CName = row[opts.textField] || "";

            if (CName.indexOf(q) > -1) {
                return true;
            }
            q = q.toUpperCase();
            if (shortCode.indexOf(q) > -1) {
                return true;
            }
            return false;
        }
    });

    $('#OperDateStart').datebox('setValue', setDate(new Date()));
    $('#OperDateEnd').datebox('setValue', setDate(new Date()));
    //$('#CollectDateStart').datebox('setValue', setDate(new Date()));
    //$('#CollectDateEnd').datebox('setValue', setDate(new Date()));
    //日期转换
    function setDate(date) {
        var day = date.getDate() > 9 ? date.getDate() : '0' + date.getDate(),
            a = date.getMonth(),
            month = (date.getMonth() + 1) > 9 ? date.getMonth() + 1 : '0' + (date.getMonth() + 1);
        return date.getFullYear() + '-' + month + '-' + day;
    }
    //送检单位
    $('#cmbClient').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=CLIENTELE&fields=ClIENTNO,CNAME',
        method: 'GET',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            data = eval('(' + data.ResultDataValue + ')').rows || [];//eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData');
            if (data.length > 0) {
                $(this).combobox('select', data[0].ClIENTNO);//默认第一项的值
            }
            $('#tbNRequestFormList').datagrid('options').url=Shell.util.Path.rootPath+'/ServiceWCF/NRequestFromService.svc/GetNRequestFromStatisticsList';
            $('#tbNRequestFormList').datagrid('load', { jsonentity:getSearchParams()});
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options'),
                shortCode = row['SHORTCODE'] || "",
                CName = row[opts.textField] || "";

            if (CName.indexOf(q) > -1) {
                return true;
            }
            q = q.toUpperCase();
            if (shortCode.indexOf(q) > -1) {
                return true;
            }
            return false;
        }
    });

    //查询
    $('#ltnSearch').bind('click', function () {
        $('#tbNRequestFormList').datagrid('load', { jsonentity:getSearchParams()});
    });
    //获取查询条件
    function getSearchParams(){
        var ClientNo=$('#cmbClient').combobox('getValue'),
            DoctorName=$('#cmbDoctor').combobox('getText'),
            CName=$('#txtCName').textbox('getValue'),
            PatNo=$('#txtPatNo').textbox('getValue'),
            NRequestFormNo=$('#txtNRequestFormNo').textbox('getValue'),
            BarCode=$('#txtBarCode').textbox('getValue'),
            WeblisFlag=$('#WeblisFlag').combobox('getValue'),
            OperDateStart=$('#OperDateStart').datebox('getValue'),
            OperDateEnd=$('#OperDateEnd').datebox('getValue'),
            //CollectDateStart=$('#CollectDateStart').datebox('getValue'),
            //CollectDateEnd=$('#CollectDateEnd').datebox('getValue'),
            ParamEntity={},
            ParamString="";

        if(ClientNo){
            ParamEntity.ClientNo=ClientNo;
            ParamString+='ClientNo:'+ClientNo;
        }
        if(DoctorName){
            ParamEntity.DoctorName=DoctorName;
            ParamString+=',DoctorName:"'+DoctorName+'"';
        }
        if(CName){
            ParamEntity.CName=CName;
            ParamString+=',CName:"'+CName+'"';
        }
        if(PatNo){
            ParamEntity.PatNo=PatNo;
            ParamString+=',PatNo:'+PatNo;
        }
        if(NRequestFormNo){
            ParamEntity.NRequestFormNo=NRequestFormNo;
            ParamString+=',NRequestFormNo:'+NRequestFormNo;
        }
        if(BarCode){
            ParamEntity.BarCode=BarCode;
            ParamString+=',BarCode:"'+BarCode+'"';
        }
        if(WeblisFlag>-1){
            ParamEntity.WeblisFlag=WeblisFlag;
            ParamString+=',WeblisFlag:'+WeblisFlag;
        }
        if(OperDateStart){
            ParamEntity.OperDateStart=OperDateStart;
            ParamString+=',OperDateStart:"'+OperDateStart+'"';
        }
        if(OperDateEnd){
            ParamEntity.OperDateEnd=OperDateEnd;
            ParamString+=',OperDateEnd:"'+OperDateEnd+'"';
        }
       /* if(CollectDateStart){
            ParamEntity.CollectDateStart=CollectDateStart;
            ParamString+=',CollectDateStart:"'+CollectDateStart+'"';
        }
        if(CollectDateEnd){
            ParamEntity.CollectDateEnd=CollectDateEnd;
            ParamString+=',CollectDateEnd:"'+CollectDateEnd+'"';
        }*/
        ParamString='{'+ParamString+'}';
        //ParamString=JSON.stringify(ParamEntity);
       // ParamString=encodeURI(ParamString);
        return ParamString;
    };

    //申请单列表
    $('#tbNRequestFormList').datagrid({
        method: 'GET',
        loadMsg: '数据加载...',
        fit: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        fitColumns: true,
        checkOnSelect: false,
        striped: true,
        border: false,
        pageSize: 20,
        columns: [[
            {field: 'NRequestFormNo', title: '申请单号', width: fixWidth(0.3)},
            {field: 'BarcodeList', title: '条码号', width: fixWidth(0.2)},
            {field: 'CName', title: '姓名', width: fixWidth(0.1)},
            {field: 'GenderName', title: '性别', width: fixWidth(0.1), align: 'center'},
            {field: 'Age', title: '年龄(岁)', width: fixWidth(0.12)},
            {field: 'SampleTypeName', title: '样本', width: fixWidth(0.1)},
            {field: 'ItemList', title: '项目', width: fixWidth(0.4), align: 'center'}, 
            {field: 'DoctorName', title: '医生', width: fixWidth(0.1)},
            {field: 'WebLisFlag', title: '状态', width: fixWidth(0.1),formatter:function(value,row,index){
                var FlagName='';
                if(value==0){
                    FlagName='未知';
                }else if(value==5){
                    FlagName='已签收';
                }else if(value==10){
                    FlagName='已报告';
                }
                return FlagName;
            }},
            {field: 'OperDate', title: '开单时间', width: fixWidth(0.25), align: 'center'},
            {field: 'CollectDate', title:   '采样时间', width: fixWidth(0.3), align: 'center',hidden:true},
            {field: 'WebLisSourceOrgName', title: '送检单位', width: fixWidth(0.2), align: 'center'}
        ]],
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        },
        loadFilter: function (data) {
           return data;
        }
    });
    //设置列宽
    function fixWidth(percent) {
        return document.body.clientWidth * percent;
    }
});