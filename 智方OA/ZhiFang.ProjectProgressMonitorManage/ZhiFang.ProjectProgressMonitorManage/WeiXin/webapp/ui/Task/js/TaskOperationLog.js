$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        pageindex: 1,
        limit: 20,
        taskid: "",
        Refresh: function () {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.querySelector("#wrapper ul");
                //这里写你的刷新代码
                document.getElementById("wrapper").querySelector(".pullDownIcon").style.display = "none";
                document.getElementById("wrapper").querySelector(".pullDownLabel").innerHTML = "<img style='width:32px' src='../img/accept.png'/>刷新成功";
                setTimeout(function () {
                    wrapper.refresh();
                    document.getElementById("wrapper").querySelector(".pullDownLabel").innerHTML = "";
                }, 1000);//模拟qq下拉刷新显示成功效果
                /****remember to refresh after  action completed！ ---yourId.refresh(); ----| ****/
            }, 1000);
        },
        Load: function () {
            var where = "pinteraction.IsUse=1 and pinteraction.PTask.Id=" + shell_win.taskid;
            var fields = "PTaskOperLog_Id,PTaskOperLog_PTaskOperTypeID,PTaskOperLog_OperaterID,PTaskOperLog_OperaterName,PTaskOperLog_OperateMemo,PTaskOperLog_DataAddTime";
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInteractionByHQL?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&where=" + where + "&fields=" + fields + "&sort=[{\"property\":\"PInteraction_DataAddTime\",\"direction\":\"DESC\"}]"
            }, function (data) {
                //alert('A');
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.list.length; i++) {
                        //alert(jsona[i].Id);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona.list[i].HeadImgUrl ? jsona.list[i].HeadImgUrl : "../img/icon/user.png";
                        var adddatetime = jsona.list[i].DataAddTime ? jsona.list[i].DataAddTime : "xxxx-xx-xx";
                        var sendername = jsona.list[i].OperaterName ? jsona.list[i].OperaterName : "未知";
                        var content = "";
                        if (jsona.list[i].OperaterName) {
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TmpApply) {
                                content = TaskCommon.TaskStatusName.TmpApply;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Applyed) {
                                content = TaskCommon.TaskStatusName.Applyed;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.OneAuditing) {
                                content = TaskCommon.TaskStatusName.OneAuditing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.OneAudited) {
                                content = TaskCommon.TaskStatusName.OneAudited;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.OneAuditBack) {
                                content = TaskCommon.TaskStatusName.OneAuditBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TwoAuditing) {
                                content = TaskCommon.TaskStatusName.TwoAuditing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TwoAudited) {
                                content = TaskCommon.TaskStatusName.TwoAudited;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TwoAuditBack) {
                                content = TaskCommon.TaskStatusName.TwoAuditBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Publishing) {
                                content = TaskCommon.TaskStatusName.Publishing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Published) {
                                content = TaskCommon.TaskStatusName.Published;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.PublishBack) {
                                content = TaskCommon.TaskStatusName.PublishBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Executing) {
                                content = TaskCommon.TaskStatusName.Executing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Executed) {
                                content = TaskCommon.TaskStatusName.Executed;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.ExecutStop) {
                                content = TaskCommon.TaskStatusName.ExecutStop;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Checking) {
                                content = TaskCommon.TaskStatusName.Checking;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Checked) {
                                content = TaskCommon.TaskStatusName.Checked;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.CheckBack) {
                                content = TaskCommon.TaskStatusName.CheckBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Stoped) {
                                content = TaskCommon.TaskStatusName.Stoped;
                            }
                        }
                        if (jsona.list[i].OperateMemo) {
                            content += ":" + jsona.list[i].OperateMemo
                        }

                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan=\"1\" width=\"80\" align=\"center\" valign=\"middle\"><img width=\"50\" src=\"" + picpath + "\"></td><td width='105' valign='middle' colspan='3'><div style=\"margin-left:5px;font-weight:bold\">" + content + "</div></td></tr><tr style='margin:2px;height:29px'><td align=\"center\">" + sendername + "</td><td width=\"125\" valign='middle' colspan=\"4\"><div style='float :right;margin-right:5px;vertical-align:middle;font-weight:bold' class='text-primary'>时间：" + adddatetime + "</div></td></tr></table></div></div>";
                        //alert(tmphtml)
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.pageindex = shell_win.pageindex + 1;
                    wrapper.refresh();
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }
            });
        },
        /*列表初始化*/
        LoadWorkLog: function () {
            var where = "ptaskoperlog.PTaskID=" + shell_win.taskid;
            var fields = "PTaskOperLog_Id,PTaskOperLog_PTaskOperTypeID,PTaskOperLog_OperaterID,PTaskOperLog_OperaterName,PTaskOperLog_OperateMemo,PTaskOperLog_DataAddTime";
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskOperLogByHQL?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&where=" + where + "&fields=" + fields + "&sort=[{\"property\":\"PTaskOperLog_DataAddTime\",\"direction\":\"ASC\"}]"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.list.length; i++) {
                        //alert(jsona[i].Id);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona.list[i].HeadImgUrl ? jsona.list[i].HeadImgUrl : "../img/icon/user.png";
                        var adddatetime = jsona.list[i].DataAddTime ? jsona.list[i].DataAddTime : "xxxx-xx-xx";
                        var sendername = jsona.list[i].OperaterName ? jsona.list[i].OperaterName : "未知";
                        var content = "";
                        if (jsona.list[i].OperaterName)
                        {
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TmpApply)
                            {
                                content = TaskCommon.TaskStatusName.TmpApply;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Applyed) {
                                content = TaskCommon.TaskStatusName.Applyed;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.OneAuditing) {
                                content = TaskCommon.TaskStatusName.OneAuditing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.OneAudited) {
                                content = TaskCommon.TaskStatusName.OneAudited;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.OneAuditBack) {
                                content = TaskCommon.TaskStatusName.OneAuditBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TwoAuditing) {
                                content = TaskCommon.TaskStatusName.TwoAuditing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TwoAudited) {
                                content = TaskCommon.TaskStatusName.TwoAudited;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.TwoAuditBack) {
                                content = TaskCommon.TaskStatusName.TwoAuditBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Publishing) {
                                content = TaskCommon.TaskStatusName.Publishing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Published) {
                                content = TaskCommon.TaskStatusName.Published;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.PublishBack) {
                                content = TaskCommon.TaskStatusName.PublishBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Executing) {
                                content = TaskCommon.TaskStatusName.Executing;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Executed) {
                                content = TaskCommon.TaskStatusName.Executed;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.ExecutStop) {
                                content = TaskCommon.TaskStatusName.ExecutStop;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Checking) {
                                content = TaskCommon.TaskStatusName.Checking;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Checked) {
                                content = TaskCommon.TaskStatusName.Checked;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.CheckBack) {
                                content = TaskCommon.TaskStatusName.CheckBack;
                            }
                            if (jsona.list[i].PTaskOperTypeID == TaskCommon.TaskStatusType.Stoped) {
                                content = TaskCommon.TaskStatusName.Stoped;
                            }
                        }
                        if (jsona.list[i].OperateMemo)
                        {
                            content += ":" + jsona.list[i].OperateMemo
                        }

                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan=\"1\" width=\"80\" align=\"center\" valign=\"middle\"><img width=\"50\" src=\"" + picpath + "\"></td><td width='105' valign='middle' colspan='3'><div style=\"margin-left:5px;font-weight:bold\">" + content + "</div></td></tr><tr style='margin:2px;height:29px'><td align=\"center\">" + sendername + "</td><td width=\"125\" valign='middle' colspan=\"4\"><div style='float :right;margin-right:5px;vertical-align:middle;font-weight:bold' class='text-primary'>时间：" + adddatetime + "</div></td></tr></table></div></div>";
                        //alert(tmphtml)
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.pageindex = shell_win.pageindex + 1;
                    wrapper.refresh();
                } else {

                    //var msg = '<b style="color:red;">' + Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType?limit=" + shell_win.limit + "&pageindex=" + shell_win.pageindex + "&sendtype=" + shell_win.sendtype + "&worklogtype=" + shell_win.worklogtype + '</b>';
                    //alert(Shell.util.Path.rootPath);
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }
            });
        },
        /**初始化*/
        init: function () {
            //alert('aaa');
            //ATCommon.info.show('aaaaaaaaa');
            //$('#aaa').val('aaa');
            var p = Shell.util.getRequestParams();
            if (p["id"]) {
                shell_win.taskid = p["id"];
            }
            else {
                alert("参数错误id！");
                history.go(-1);
                return;
            }
            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            shell_win.LoadWorkLog();
        }
    };
    shell_win.init();
});