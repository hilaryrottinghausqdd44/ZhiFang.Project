$(function () {
    //页面所有功能对象
    var shell_win = {
        WorkLogType: '',
        WorkLogTypeUrl: '',
        WorkLogId: '',
        Backpage: function () {
            if (shell_win.backpageurl) {
                location.href = shell_win.backpageurl;
            }
            else {
                history.go(-1);
            }
        },
        AddLikesCount:function()
        {
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType?Id=" + shell_win.WorkLogId + "&worklogtype=" + shell_win.WorkLogType
            }, function (data) {
                if (data.success) {
                    this.location.href = this.location.href;
                }
            });
        },
        InitHtml:function()
        {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchWorkDayLogByIdAndWorkLogType?Id=" + shell_win.WorkLogId + "&worklogtype=" + shell_win.WorkLogType
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var jsona = $.parseJSON(data.ResultDataValue);
                    if (jsona.ToDayContent) {
                        $('#wlcontent').val(jsona.ToDayContent);
                    }
                    if (jsona.WorkLogExportLevel == 0) {
                        $('#wlsendtype').text('仅自己和直接主管可见');
                    }
                    if (jsona.WorkLogExportLevel == 1) {
                        $('#wlsendtype').text('所属部门可见');
                    }
                    if (jsona.WorkLogExportLevel == 2) {
                        $('#wlsendtype').text('全公司可见');
                    }
                    if (jsona.CopyForEmpNameList && jsona.CopyForEmpNameList.length > 0) {
                        var copyforemp = "@" + jsona.CopyForEmpNameList.join(";@");
                        $('#wlcopyfor').text(copyforemp);
                    }
                    if (jsona.Image1) {
                        $('#wlimg1').attr('style', 'display:block');
                        $('#wlimg1').attr('src', Shell.util.Path.rootPath + "/" + jsona.Image1);
                    }
                    if (jsona.Image2) {
                        $('#wlimg2').attr('style', 'display:block');
                        $('#wlimg2').attr('src', Shell.util.Path.rootPath + "/" + jsona.Image2);
                    }
                    if (jsona.Image3) {
                        $('#wlimg3').attr('style', 'display:block');
                        $('#wlimg3').attr('src', Shell.util.Path.rootPath + "/" + jsona.Image3);
                    }
                    if (jsona.Image4) {
                        $('#wlimg4').attr('style', 'display:block');
                        $('#wlimg4').attr('src', Shell.util.Path.rootPath + "/" + jsona.Image4);
                    }
                    if (jsona.Image5) {
                        $('#wlimg5').attr('style', 'display:block');
                        $('#wlimg5').attr('src', Shell.util.Path.rootPath + "/" + jsona.Image5);
                    }
                    $("#AddLikesCount").text('点赞(' + jsona.LikeCount + ')');
                } else {
                    alert('aaa');
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }
            });
        },
        /**初始化*/
        init: function () {
            //alert('aaa');
            var p = Shell.util.getRequestParams();
            //alert('aaa');
            if (p["worklogid"])
            {
                shell_win.WorkLogId = p["worklogid"];
            }
            if (p["WorkLogType"]) {
                shell_win.WorkLogType = p["WorkLogType"];
                if (shell_win.WorkLogType == "WorkLogDay") {
                    $('#wltype').removeClass();
                    $('#wltype').addClass('btn-info');
                    $('#wltype').text('日报');
                }
                if (shell_win.WorkLogType == "WorkLogWeek") {
                    $('#wltype').removeClass();
                    $('#wltype').addClass('btn-warning');
                    $('#wltype').text('周报');
                }
                if (shell_win.WorkLogType == "WorkLogMonth") {
                    $('#wltype').removeClass();
                    $('#wltype').addClass('btn-success');
                    $('#wltype').text('月报');
                }
            }
            shell_win.InitHtml();
            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Backpage);
            $("#AddLikesCount").on(Shell.util.Event.touch, shell_win.AddLikesCount);
        }
    };
    shell_win.init();
});
function GetTaskInfo(id, name) {
    //alert(1);
    parent.parent.location.href = "Task.html?id=" + id + "&name=" + name;
}
function AddWorkTaskLog(id) {
    //alert(2);
    parent.parent.location.href = "../Worklog/TaskLog.html?id=" + id;
}