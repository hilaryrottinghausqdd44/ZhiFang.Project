var newstypeurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsTypeByHQL';
var newsAreaurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsAreaByHQL';
var newsApprovaurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/GetApprovaList';
var newsurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsById';
var newsediturl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_UpdateNNews';
var Content = "";
$(function () {
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

    $('#txtApprovalId').combobox({
        url: newsApprovaurl,
        method: 'GET',
        valueField: 'ID',
        textField: 'Name',
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                var tmp = result || [];
                return tmp;
            }
            else {
                $.messager.alert('提示', '审批人列表加载失败！', 'warning');
                return null;
            }
        }
    });

    var para = Shell.getRequestParams();

    if (para["NewsID"] && para["NewsID"] != "") {
        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: newsurl + '?id=' + para["NewsID"],
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.success == true) {
                    var result = eval("(" + data.ResultDataValue + ")");
                    var content = result.Content || "";
                    var publishdate = result.PublisherDateTime || "";
                    var title = result.Title || "";
                    var Keyword = result.Keyword || "";
                    var No = result.No || "";
                    var Summary = result.Summary || "";
                    var StartDate = result.BeginTime || "";
                    var EndDate = result.EndTime || "";


                    $('#txtFileID').val(para["NewsID"]);
                    $('#txtTitle').textbox('setValue', title);
                    $('#txtKeyword').textbox('setValue', Keyword);
                    $('#txtNo').textbox('setValue', No);
                    $('#txtType').combobox('setValue', result.ContentType);
                    $('#txtApprovalId').combobox('setValue', result.ApprovalId);
                    $('#txtSummary').textbox('setValue', Summary);
                    $('#StartDate').datebox('setValue', StartDate);
                    $('#EndDate').datebox('setValue', EndDate);

                    setContent(content);
                } else {
                    $.messager.alert('提示', '禁用数据失败！失败信息：' + data.msg);
                }
            }
        })
    }
    else {
        $.messager.alert('提示', '新闻参数错误！');
    }
});
function setContent(content) {
    var ue = UE.getEditor('container');
    ue.addListener("ready", function () {
        // editor准备好之后才可以使用
        ue.setContent(content);

    });
    //UE.getEditor('container').setContent(content);
}
function save() {
    {
        var errors = 0;
        var entity = {};
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var para = Shell.getRequestParams();
        var NewsID = para["NewsID"];

        entity.FileID = NewsID;

        var Title = $("#txtTitle").val();
        if (!Title || Title == "") {
            errors += 1;
        }
        else {
            entity.Title = Title;
        }

        var Keyword = $("#txtKeyword").val();
        if (Keyword) {
            entity.Keyword = Keyword;
        }

        var No = $("#txtNo").val();
        if (!No || No == "") {
            errors += 1;
        }
        else {
            entity.No = No;
        }

        var ContentType = $("#txtType").combobox('getValue');//int
        if (!ContentType) {
            errors += 1;
        }
        else {
            entity.ContentType = ContentType;
        }

        var ApprovalId = $("#txtApprovalId").combobox('getValue');//int
        if (!ApprovalId) {
            errors += 1;
        }
        else {
            entity.ApprovalId = ApprovalId;
            entity.ApprovalName = $("#txtApprovalId").combobox('getText');
        }
        var Summary = $("#txtSummary").val();
        if (Summary) {
            entity.Summary = Summary;
        }
        var StartDate = $("#StartDate").datebox('getText');
        if (StartDate && StartDate != "") {
            entity.StartDateTime = StartDate;
        }
        var EndDate = $("#EndDate").datebox('getText');
        if (EndDate && EndDate != "") {
            entity.EndDateTime = EndDate;
        }
        getContent();
        if (Content) {
            entity.Content = Content;
        }
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: newsediturl,
                data: Shell.util.JSON.encode({ entity: entity }),
                dataType: "json",
                success: function (data) {
                    if (data.success == true) {
                        $.messager.alert('提示', '新闻保存成功！');
                        parent.CloseWindowFuc();
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息：' + data.msg);
                    }
                }
            });
        }

    }
}
function getContent() {
    Content = UE.getEditor('container').getContent();
    //alert(Content);
}