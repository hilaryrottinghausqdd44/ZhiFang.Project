var a = 0;
$(function () {
    $("#cbCLIENTNO").combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO',
        editable: false,
        method: 'get',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            //var result = eval("(" + data.ResultDataValue + ")");
            //return result.rows;
            data = data || [];
            if (data.length > 0) { data[0].selected = true; }
            return data;
        },
        //onLoadSuccess: function () {
        //    var data = $(this).combobox('getData');
        //    if (data.length > 0) {
        //        $(this).combobox('select', data[0].ClIENTNO);//默认第一项的值
        //        labCode = data[0].ClIENTNO;
        //    }
        //},

        //filter: function (q, row) {
        //    var opts = $(this).combobox('options');
        //    return row[opts.textField].indexOf(q) > -1;//返回true,则显示出来
        //},
    })

})
//查询按钮
function doSearch() {
    var errors = 0;
    var r = '';
    r += '{"jsonentity":{';
    var Checkstartdate = $("#starting-time").datebox("getValue");
    if (Checkstartdate == "") {

        errors += 1;
    }
    if (Checkstartdate) {
        r += '"Checkstartdate":"' + Shell.util.Date.toServerDate(Checkstartdate) + '",';
    }
    var Checkenddate = $("#final-time").datebox("getValue");
    if (Checkenddate == "") {

        errors += 1;
    }
    if (Checkenddate) {
        r += '"Checkenddate":"' + Shell.util.Date.toServerDate(Checkenddate) + '",';
    }
    var CNAME = $("#text-CNAME").val();
    if (CNAME) {
        r += '"CNAME":"' + CNAME + '",';
    }
    var SERIALNO = $("#text-SERIALNO").val();
    if (SERIALNO) {
        r += '"SERIALNO":"' + SERIALNO + '",';
    }
    var CLIENTNO = $("#cbCLIENTNO").combobox('getValue');
    if (CLIENTNO) {
        r += '"CLIENTNO":' + CLIENTNO + ',';
    }
    r += '"TYPE":"reporthtmlurl"}}'
    if (errors > 0) {
        $.messager.alert('提示', '请检查输入值的完整性', 'warning');
    } else {
        $.ajax({
            type: 'post',
            contentType: 'application/json',
            url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFromService.svc/GetReportItem',
            // data: r,
            dataType: 'json',
            data: r,
            success: function (data) {
                if (data.success == true) {
                    // var result = eval("(" + data.ResultDataValue + ")");
                    //$('#preview').attr('src', 'result')
                    //var iframe = document.getElementById("preview");
                    // iframe.src = data.ResultDataValue;
                    var str = data.ResultDataValue;
                    var splits = str.split("#");
                    var startname = splits[0];
                    var lastname = splits[1];
                    $('#btnload').attr("enabled",true);
                    $("#query").attr("src", Shell.util.Path.rootPath + startname);
                    //  document.getElementById("btnload").style.display = "block";
                    // $("#btnload").show();
                    a = Shell.util.Path.rootPath + lastname;
                   // $("#preview").attr("src", Shell.util.Path.rootPath + lastname)

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
//导出Excel文档
//function btnexport() {
//    var errors=0;
//    var r = '';
//    r += '{"jsonentity":{';
//    var Checkstartdate = $("#starting-time").datebox("getValue");
//    if (Checkstartdate == "") {

//        errors += 1;
//    }
//    if (Checkstartdate) {
//        r += '"Checkstartdate":"' + Shell.util.Date.toServerDate(Checkstartdate) + '",';
//    }
//    var Checkenddate = $("#final-time").datebox("getValue");
//    if (Checkenddate == "") {

//        errors += 1;
//    }
//    if (Checkenddate) {
//        r += '"Checkenddate":"' + Shell.util.Date.toServerDate(Checkenddate) + '",';
//    }
//    var CNAME = $("#text-CNAME").val();
//    if (CNAME) {
//        r += '"CNAME":"' + CNAME + '",';
//    }
//    var SERIALNO = $("#text-SERIALNO").val();
//    if (SERIALNO) {
//        r += '"SERIALNO":"' + SERIALNO + '",';
//    }
//    var CLIENTNO = $("#cbCLIENTNO").combobox('getValue');
//    if (CLIENTNO) {
//        r += '"CLIENTNO":' + CLIENTNO + ',';
//    }
//    r += '}}'
//    if (errors > 0) {
//        $.messager.alert('提示', '请检查输入值的完整性', 'warning');
//    } else {
//        $.ajax({
//            type: 'post',
//            contentType: 'application/json',
//            url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFromService.svc/DownLoadReportExcel',
//            // data: r,
//            dataType: 'json',
//            data: r,
//            success: function (data) {
//                if (data.success == true) {
//                    //var result = eval("(" + data.ResultDataValue + ")");
//                    //$('#preview').attr('src', 'result')
//                    // var iframe = document.getElementById("preview");
//                    //iframe.src = data.ResultDataValue;
//                    $("#preview").attr("src", Shell.util.Path.rootPath + data.ResultDataValue);
//                } else {
//                    $.messager.alert('提示', '提取数据失败！失败信息：' + data.msg);

//                }
//            }
//        });
//    }
//}
//今天按钮
function Today() {  
    var date = new Date(),
        date_s = Shell.util.Date.toString(date, true),
        date_e = Shell.util.Date.toString(date, true);     
    $("#starting-time").datebox("setValue", date_s)  
    $("#final-time").datebox("setValue", date_e)
}
//三天按钮
function tDay() { 
    var date = new Date(),      
        date_s = Shell.util.Date.toString(Shell.util.Date.getNextDate(date, -2), true),
		date_e = Shell.util.Date.toString(date, true); 
    $("#starting-time").datebox("setValue", date_s)
    $("#final-time").datebox("setValue", date_e)
}
//一周按钮
function sDay() {
    var date = new Date(),
        date_s = Shell.util.Date.toString(Shell.util.Date.getNextDate(date, -6), true),
	    date_e = Shell.util.Date.toString(date, true);
    $("#starting-time").datebox("setValue", date_s)
    $("#final-time").datebox("setValue", date_e)
}