var newstypeurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsTypeByHQL';
var newsAreaurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsAreaByHQL';
var newsApprovaurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/GetApprovaList';
var newsaddurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_AddNNews';
var Content="";
$(function () {
    $('#txtKeyword').textbox({
        prompt: '关键字之间以逗号分隔'
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

});
function save() {
    {
        var errors = 0;
        var entity = {};
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
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
        if (StartDate && StartDate!="") {
            entity.StartDateTime = StartDate;
        }
        var EndDate = $("#EndDate").datebox('getText');
        if (EndDate && EndDate != "") {
            entity.EndDateTime = EndDate;
        }
        getContent();
        if (Content)
        {
            entity.Content = Content;
        }
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: newsaddurl,
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