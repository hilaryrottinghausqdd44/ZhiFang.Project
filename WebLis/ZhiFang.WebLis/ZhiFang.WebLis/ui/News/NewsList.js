var searchurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsList';
var newstypeurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsTypeByHQL';
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'FileID',
        //url: searchurl,
        method: 'get',
        striped: true,
        columns: [[
             { field: 'FileID', title: 'ID', hidden: true },
              {
                  field: 'ReadCount', title: '阅读状态', width: '4%', align: 'center',
                  formatter: function (value, row, index) {
                      if (row.ReadCount > 0) {
                          //return "已读";
                          return "<span >已读</span>";
                      }
                      else {
                          return "<span >未读</span>";
                      }
                  },
                  styler: function (value, row, index) {
                      if (row.ReadCount > 0) {
                      }
                      else {
                          return 'background-color:red;font-weight:bold;color:#ffffff';
                      }
                      return "";
                  }
                  //,
                  //styler: function (value, row, index) {
                  //    if (value > 0) {
                  //        return 'background-color:blue;';
                  //    }
                  //    else {
                  //        return 'background-color:red;';
                  //    }
                  //}
              },
             { field: 'Title', title: '标题', width: '50%' },
             { field: 'No', title: '编号', width: '5%' },
             { field: 'ContentType', title: 'TypeID', hidden: true },
             { field: 'ContentTypeName', title: '类型', width: '5%' },
             { field: 'Status', title: 'StatusID', hidden: true },
             { field: 'StatusName', title: '状态', width: '3%' },
             //{ field: 'DispOrder', title: '序号', width: '5%' },
             { field: 'DrafterCName', title: '作者', width: '3%' },
             { field: 'PublisherName', title: '发布人', width: '5%' },
             {
                 field: 'PublisherDateTime', title: '发布时间', width: '9%',
                 formatter: function (value, row, index) {
                     if (row.PublisherDateTime) {
                         return String(row.PublisherDateTime).replace("T", " ");
                     }
                     else {
                         return "";
                     }
                 }
             },             
             {
                 field: 'Operation', title: '查看',
                 formatter: function (value, row, index) {
                     var a = '<a href="javascript: parent.OpenWindowFucCallback(\'新增内容\',' + Math.floor(window.screen.width * 0.9) + ',' + Math.floor(window.screen.height * 0.7) + ',\'../ui/News/NewsInfo.html?NewsID=' + row.FileID + '\',\'\',dgreload)" class="ope-save" >查看</a> ';
                     //var a = "查看";
                     return a;
                 },
                 styler: function (value, row, index) {
                     if (value == '0') {
                         return 'background-color:red;';
                     }
                     if (value == '1') {
                         return 'background-color:#FFF2CC;';
                     }
                     return "";
                 }
             }

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            var totalcount = 0;
            if (result.total) {
                totalcount = result.total;
            }
            return { total: totalcount, rows: result.rows || [] };
        },
        onClickRow: function (rowIndex, rowData) {

        },
        onDblClickRow: function (rowIndex, rowData) {
            //parent.OpenWindowFucCallback('新增内容', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/News/NewsInfo.html?NewsID=' + rowData.FileID, "", function () { $('#dg').datagrid('reload'); });
            parent.OpenWindowFucCallback('新闻内容', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/News/NewsInfo.html?NewsID=' + rowData.FileID, "", dgreload);
        }
        //,onClickCell: function (rowIndex, field, value)
        //{
        //    if (field == "Operation")
        //    {
        //        var rows=$('#dg').datagrid('getRows');

        //        parent.OpenWindowFucCallback('新闻内容', Math.floor(window.screen.width * 0.9), +Math.floor(window.screen.height * 0.7), '../ui/News/NewsInfo.html?NewsID=' + rows[rowIndex].FileID, "", dgreload);
        //    }
        //}
    })
    var p = $('#dg').datagrid('getPager');
    $(p).pagination({

        pageSize: 10, //每页显示的记录条数，默认为10           
        pageList: [10, 50, 100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
    });
    $('#txtType').combobox({
        url: newstypeurl + "?page=1&rows=1000&where=IsUse=1 ",
        method: 'GET',
        valueField: 'TypeID',
        textField: 'CName',
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                var tmp = result.rows || [];
                return tmp;
            }
            else {
                $.messager.alert('提示', '新闻类型加载失败！', 'warning');
                return null;
            }
        }
    });
    var datetimenow = new Date();
    var startdatetimestr = datetimenow.getFullYear() + "-" + (datetimenow.getMonth()) + "-" + datetimenow.getDate();
    var enddatetimestr = datetimenow.getFullYear() + "-" + (datetimenow.getMonth() + 1) + "-" + datetimenow.getDate();
    $('#StartDateTime').datebox('setValue', startdatetimestr);
    $('#EndDateTime').datebox('setValue', enddatetimestr);
    doSearch();
});
function doSearch() {
    var SH;
    var wherestr = " 1=1 ";
    var SearchKey = $("#txtSearchKey").val();


    if (SearchKey && SearchKey != "") {
        wherestr = " Title like '%" + SearchKey + "%' and IsUse=1 ";
    }
    var typeId = $('#txtType').combobox('getValue');
    if (typeId) {
        wherestr = "(" + wherestr + " and ContentType=" + typeId + ")";
    }
    else {
        wherestr = "(" + wherestr + ")";
    }
    var startdatetime = "";
    startdatetime = $('#StartDateTime').datebox('getText');
    var enddatetime = "";
    enddatetime = $('#EndDateTime').datebox('getText');
    var url = searchurl + "?where=" + wherestr + "&startdatetime=" + startdatetime + "&enddatetime=" + enddatetime;
    $('#dg').datagrid({
        url: url,
        //queryParams: {
        //    where: wherestr,
        //    startdatetime: wherestr,
        //    enddatetime: wherestr
        //},
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onBeforeLoad: function () {
        },
        onLoadSuccess: function (data) {
        }
    });
}

function update() {
    $('#dg').datagrid('load');
    $('#txtSearchKey').searchbox("clear");
}
function dgreload() {
    $('#dg').datagrid('reload');
}