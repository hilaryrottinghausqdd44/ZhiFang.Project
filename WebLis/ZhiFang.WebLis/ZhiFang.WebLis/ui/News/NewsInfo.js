var newsurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsById';
$(function () {
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
                    $('#NewsContent').html(content);
                    $('#NewsTitle').html(title);
                    $('#NewsPublishDate').html(publishdate.replace("T","  "));
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