$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        pageindex: 1,
        limit: 5,
        ExportType: 1,
        TaskStatusType: "",
        TaskStatusTypeNo: "",
        Role:"",
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
            var data = {
                ExportType: shell_win.ExportType,
                Status: shell_win.TaskStatusType,
                NoStatus: shell_win.TaskStatusTypeNo,
                Page: shell_win.pageindex,
                Limit: shell_win.limit,
                CopyForEmpNameList: []
            };
            if (shell_win.Role == "all") {
                data.ExportType = -1;
            }
            if (shell_win.Role == "apply") {
                data.ApplyID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "oneaudit") {
                data.OneAuditID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "twoaudit") {
                data.TwoAuditID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "publish") {
                data.PublisherID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "execut") {
                data.ExecutorID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "check") {
                data.CheckerID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "Post",
                data: Shell.util.JSON.encode({ entity: data }),
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPTask"
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

                        var UrgencyNamehtml = jsona.list[i].UrgencyName ? jsona.list[i].UrgencyName : "";
                        var PaceNamehtml = jsona.list[i].PaceName ? jsona.list[i].PaceName : "0%";
                        var ReqEndTimehtml = jsona.list[i].ReqEndTime ? Shell.util.Date.toString(jsona.list[i].ReqEndTime, true) : "&nbsp;";



                        var Btnhtml = "&nbsp;";
                        if (shell_win.ExportType == 0 && (shell_win.TaskStatusType == TaskCommon.TaskStatusType.TmpApply || shell_win.TaskStatusType == TaskCommon.TaskStatusType.OneAuditBack)) {
                            Btnhtml = "<button id=\"BtnApply" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.ApplyAction('" + jsona.list[i].IdString + "','BtnApply" + jsona.list[i].IdString + "')\">提交</button>";
                        }
                        if (shell_win.ExportType == 1) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Applyed) {
                                Btnhtml = "<button id=\"BtnOneAudit" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.OneAuditAction('" + jsona.list[i].IdString + "','BtnOneAudit" + jsona.list[i].IdString + "')\">一审受理</button>";
                            }
                        }
                        if (shell_win.ExportType == 2) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.OneAudited) {
                                Btnhtml = "<button id=\"BtnTwoAudit" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.TwoAuditAction('" + jsona.list[i].IdString + "','BtnTwoAudit" + jsona.list[i].IdString + "')\">二审受理</button>";
                            }
                        }
                        if (shell_win.ExportType == 3) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.TwoAudited) {
                                Btnhtml = "<button id=\"BtnPublish" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.PublishAction('" + jsona.list[i].IdString + "','BtnPublish" + jsona.list[i].IdString + "')\">开始分配</button>";
                            }
                        }
                        if (shell_win.ExportType == 4) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Published) {
                                Btnhtml = "<button id=\"BtnExecut" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.ExecutAction('" + jsona.list[i].IdString + "','BtnExecut" + jsona.list[i].IdString + "')\">确认执行</button>";
                            }
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Executing) {
                                Btnhtml = "<button id=\"AddWorkLog" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"AddWorkTaskLog('" + jsona.list[i].IdString + "')\">填写日志</button>";
                            }
                        }
                        if (shell_win.ExportType == 5) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Executed) {
                                Btnhtml = "<button id=\"BtnCheck" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.CheckAction('" + jsona.list[i].IdString + ",'BtnCheck" + jsona.list[i].IdString + "'')\">开始检查</button>";
                            }
                        }


                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0' ><tr style='margin:2px;height:29px' onclick=\"GetTaskInfo('" + jsona.list[i].IdString + "','" + jsona.list[i].CName + "')\" ><td colspan=\"2\"><div style=\"float:left\">" + jsona.list[i].CName + "</div><div style=\"float:right\" class=\"label label-primary\">" + jsona.list[i].StatusName + "</div></td><td align=\"right\" style=\"font-size:small\">" + Shell.util.Date.toString(jsona.list[i].DataAddTime, true) + "</td></tr><tr style='margin:2px;height:29px' onclick=\"GetTaskInfo('" + jsona.list[i].IdString + "','" + jsona.list[i].CName + "')\"><td>申请者：" + jsona.list[i].ApplyName + "</td><td align=\"right\" style=\"width:100px\"><span class=\"text-danger\" style=\"padding-right:10px\">" + UrgencyNamehtml + "</span></td><td align=\"right\" style=\"width:100px\" >完成度：" + PaceNamehtml + "</td></tr><tr style='margin:2px;height:29px'><td colspan=\"2\" align=\"left\"  onclick=\"GetTaskInfo('" + jsona.list[i].IdString + "','" + jsona.list[i].CName + "')\"><span class=\"label label-warning\"  style=\"float:left\">截至日期：" + ReqEndTimehtml + "</span></td><td  align=\"right\">" + Btnhtml + "</td></tr></table></div></div>";
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
        /*列表初始化*/
        LoadTask: function () {
            var data = {
                ExportType: shell_win.ExportType,
                Status: shell_win.TaskStatusType,
                NoStatus:shell_win.TaskStatusTypeNo,
                Page: shell_win.pageindex,
                Limit: shell_win.limit,
                CopyForEmpNameList: []
            };
            if (shell_win.Role == "all")
            {
                data.ExportType = -1;
            }
            if (shell_win.Role == "apply") {
                data.ApplyID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "oneaudit") {
                data.OneAuditID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "twoaudit") {
                data.TwoAuditID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "publish") {
                data.PublisherID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "execut") {
                data.ExecutorID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            if (shell_win.Role == "check") {
                data.CheckerID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
            }
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "Post",
                data: Shell.util.JSON.encode({ entity: data }),
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPTask"
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

                        var UrgencyNamehtml = jsona.list[i].UrgencyName ? jsona.list[i].UrgencyName : "";
                        var PaceNamehtml = jsona.list[i].PaceName ? jsona.list[i].PaceName : "0%";
                        var ReqEndTimehtml = jsona.list[i].ReqEndTime ? Shell.util.Date.toString(jsona.list[i].ReqEndTime, true) : "&nbsp;";

                        

                        var Btnhtml = "&nbsp;";
                        if (shell_win.ExportType == 0 && (shell_win.TaskStatusType == TaskCommon.TaskStatusType.TmpApply || shell_win.TaskStatusType == TaskCommon.TaskStatusType.OneAuditBack)) {
                            Btnhtml = "<button id=\"BtnApply" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.ApplyAction('" + jsona.list[i].IdString + "','BtnApply" + jsona.list[i].IdString + "')\">提交</button>";
                        }
                        if (shell_win.ExportType == 1 )
                        {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Applyed ) {
                                Btnhtml = "<button id=\"BtnOneAudit" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.OneAuditAction('" + jsona.list[i].IdString + "','BtnOneAudit" + jsona.list[i].IdString + "')\">一审受理</button>";
                            }
                        }
                        if (shell_win.ExportType == 2) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.OneAudited) {
                                Btnhtml = "<button id=\"BtnTwoAudit" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.TwoAuditAction('" + jsona.list[i].IdString + "','BtnTwoAudit" + jsona.list[i].IdString + "')\">二审受理</button>";
                            }
                        }
                        if (shell_win.ExportType == 3) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.TwoAudited) {
                                Btnhtml = "<button id=\"BtnPublish" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.PublishAction('" + jsona.list[i].IdString + "','BtnPublish" + jsona.list[i].IdString + "')\">开始分配</button>";
                            }
                        }
                        if (shell_win.ExportType == 4) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Published) {
                                Btnhtml = "<button id=\"BtnExecut" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.ExecutAction('" + jsona.list[i].IdString + "','BtnExecut" + jsona.list[i].IdString + "')\">确认执行</button>";
                            }
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Executing) {
                                Btnhtml = "<button id=\"AddWorkLog" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"AddWorkTaskLog('" + jsona.list[i].IdString + "')\">填写日志</button>";
                            }
                        }
                        if (shell_win.ExportType == 5) {
                            if (shell_win.TaskStatusType == TaskCommon.TaskStatusType.Executed) {
                                Btnhtml = "<button id=\"BtnCheck" + jsona.list[i].IdString + "\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"TaskCommon.CheckAction('" + jsona.list[i].IdString + ",'BtnCheck" + jsona.list[i].IdString + "'')\">开始检查</button>";
                            }
                        }


                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0' ><tr style='margin:2px;height:29px' onclick=\"GetTaskInfo('" + jsona.list[i].IdString + "','" + jsona.list[i].CName + "')\" ><td colspan=\"2\"><div style=\"float:left\">" + jsona.list[i].CName + "</div><div style=\"float:right\"  class=\"label label-primary\">" + jsona.list[i].StatusName + "</div></td><td align=\"right\" style=\"font-size:small\">" + Shell.util.Date.toString(jsona.list[i].DataAddTime, true) + "</td></tr><tr style='margin:2px;height:29px' onclick=\"GetTaskInfo('" + jsona.list[i].IdString + "','" + jsona.list[i].CName + "')\"><td>申请者：" + jsona.list[i].ApplyName + "</td><td align=\"right\" style=\"width:100px\"><span class=\"text-danger\" style=\"padding-right:10px\">" + UrgencyNamehtml + "</span></td><td align=\"right\" style=\"width:100px\" >完成度：" + PaceNamehtml + "</td></tr><tr style='margin:2px;height:29px'><td colspan=\"2\" align=\"left\"  onclick=\"GetTaskInfo('" + jsona.list[i].IdString + "','" + jsona.list[i].CName + "')\"><span class=\"label label-warning\"  style=\"float:left\">截至日期：" + ReqEndTimehtml + "</span></td><td  align=\"right\">" + Btnhtml + "</td></tr></table></div></div>";
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
            //alert("ExportType=" + p["ExportType"] + "@TaskStatusType=" + p["TaskStatusType"]);
            if (p["ExportType"]) {
                shell_win.ExportType = p["ExportType"];
            }
            if (p["TaskStatusType"]) {
                shell_win.TaskStatusType = p["TaskStatusType"];
            }
            if (p["TaskStatusTypeNo"]) {
                shell_win.TaskStatusTypeNo = p["TaskStatusTypeNo"];
            }
            if (p["Role"]) {
                shell_win.Role = p["Role"];
            }

            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            shell_win.LoadTask();
        }
    };
    shell_win.init();
});
function GetTaskInfo(id,name) {
    //alert(1);
    var p = Shell.util.getRequestParams();
    parent.parent.location.href = "TaskInfo.html?id=" + id + "&name=" + name + "&ExportType=" + p["ExportType"] + "&TaskStatusType=" + p["TaskStatusType"] + "&TaskStatusTypeNo=" + p["TaskStatusTypeNo"] + "&Role=" + p["Role"];
}
function AddWorkTaskLog(id) {
    //alert(2);
    parent.parent.location.href = "../Worklog/TaskLog.html?id=" + id;
}

