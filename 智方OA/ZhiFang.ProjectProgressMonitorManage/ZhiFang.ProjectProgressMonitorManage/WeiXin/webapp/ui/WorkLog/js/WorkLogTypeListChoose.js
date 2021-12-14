$(function () {
    //页面所有功能对象
    var shell_win = {
        pageindex: 1,
        limit: 30,
        ExportType: 4,
        TaskStatusType: TaskCommon.TaskStatusType.Executing,
        Load: function () {
        },
        Backpage: function () {
            if (localStorage.getItem("Backpage")) {
                location.href = localStorage.getItem("Backpage");
            }
            else {
                location.href = "Index.html";
            }
        },
        inittasklist: function () {
            var html = "";//"<table width=\"100%\"><tr style=\"height:50px;border-bottom:solid 1px;border-bottom-color:gainsboro\"><td align=\"left\"><img src=\"../img/icon/week.png\" style=\"width:32px;height:32px;margin:10px\" />周报</td><td align=\"right\"><img src=\"../img/icon/rightarrow.png\" style=\"width:32px;height:32px\" /></td></tr><tr style=\"height:50px;border-bottom:solid 1px;border-bottom-color:gainsboro\"><td align=\"left\"><img src=\"../img/icon/week.png\" style=\"width:32px;height:32px;margin:10px\" />周报</td><td align=\"right\"><img src=\"../img/icon/rightarrow.png\" style=\"width:32px;height:32px\" /></td></tr><tr style=\"height:50px;border-bottom:solid 1px;border-bottom-color:gainsboro\"><td align=\"left\"><img src=\"../img/icon/week.png\" style=\"width:32px;height:32px;margin:10px\" />周报</td><td align=\"right\"><img src=\"../img/icon/rightarrow.png\" style=\"width:32px;height:32px\" /></td></tr></table>";
            var data={
                ExportType: shell_win.ExportType,
                Status: shell_win.TaskStatusType,
                Page: shell_win.pageindex,
                Limit: shell_win.limit,
                CopyForEmpNameList: []
            };

            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "Post",
                data: Shell.util.JSON.encode({ entity: data }),
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPTask"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    tmphtml = "";
                    //alert(data.ResultDataValue);
                    var jsona = $.parseJSON(data.ResultDataValue);
                    //alert(jsona.count);
                    //alert(jsona.list.length);
                    for (var i = 0; i < jsona.list.length; i++) {
                        //alert(jsona[i].Id);
                        tmphtml = "<tr style=\"height:50px;border-bottom:solid 1px;border-bottom-color:gainsboro\" onclick=\"WorkTaskLogAdd('" + jsona.list[i].IdString + "');\"><td align=\"left\"><img src=\"../img/icon/iconfont-task-list.png\" style=\"width:32px;height:32px;margin:10px\" />" + jsona.list[i].CName + "</td><td align=\"right\">" + jsona.list[i].DataAddTime + "</td><td align=\"right\" width='50px'><img src=\"../img/icon/rightarrow.png\" style=\"width:32px;height:32px\" /></td></tr>";
                        html += tmphtml;
                    }
                } else {
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }           
                $("#tasklisttd").html("<table width=\"100%\" border=0>" + html + "</table>");
            });
        },
        test:function (id)
        {
            alert(id);
        },
        /**初始化*/
        init: function () {
            $("#daytr").on(Shell.util.Event.touch, function () {
                //alert($(this).attr('data-url'));
                location.href = "WorkLogDayAdd.html?WorkLogType=day";
            });
            $("#weektr").on(Shell.util.Event.touch, function () {
                //alert($(this).attr('data-url'));
                location.href = "WorkLogDayAdd.html?WorkLogType=week";
            });
            $("#monthtr").on(Shell.util.Event.touch, function () {
                //alert($(this).attr('data-url'));
                location.href = "WorkLogDayAdd.html?WorkLogType=month";
            });
            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Backpage);
            localStorage.setItem("IsRefers", "true");
            shell_win.inittasklist();
            shell_win.Load();
        }
    };
    shell_win.init();
});
function WorkTaskLogAdd(id) {
    location.href = "TaskLog.html?id=" + id;
};