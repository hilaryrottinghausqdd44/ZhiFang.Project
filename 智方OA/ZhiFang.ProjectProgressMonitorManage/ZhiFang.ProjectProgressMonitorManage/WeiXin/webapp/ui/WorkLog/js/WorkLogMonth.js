$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        pageindex: 1,
        limit: 3,
        sendtype: "",
        WorkLogType: "",
        WorkLogList: new Array(),
        ApprovalBatch: function () {
            $("#ApprovalBatch_div").modal('show');
            //alert(shell_win.ApprovalAllLogUnList);
        },
        vailddate: function (ev) {
            //alert(ev.date.Format("yyyy-MM-dd hh:mm:ss"));
            //alert($('#datetimepickstartinput').val());
            var sd = $('#datetimepickstartinput').val();
            var ed = $('#datetimepickendinput').val();
            if ($('#datetimepickstartinput').val() != "" && $('#datetimepickendinput').val() != "") {
                ShellComponent.mask.loading();
                Shell.util.Server.ajax({
                    type: "get",
                    url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetATEmpAttendanceEventHourCount?sd=" + sd + "&ed=" + ed
                }, function (data) {
                    ShellComponent.mask.hide();
                    if (data.success) {
                        alert(data.ResultDataValue);
                        //var jsona = $.parseJSON(data.ResultDataValue);
                        $('#evenlength').val(data.ResultDataValue);
                    } else {
                        //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                        ATCommon.info.error(data.msg);
                    }
                });
            }
        },
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

            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&sendtype=" + shell_win.sendtype + "&worklogtype=" + shell_win.WorkLogType
            }, function (data) {
                //alert('A');
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        //alert(jsona[i].Id);
                        shell_win.WorkLogList.push(jsona[i].Id);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona[i].HeadImgUrl ? jsona[i].HeadImgUrl : "../img/icon/user.png";
                        var datecode = jsona[i].DateCode ? jsona[i].DateCode : "xxxx-xx-xx";
                        var adddatetime = jsona[i].DataAddTime ? jsona[i].DataAddTime : "xxxx-xx-xx";
                        var empname = jsona[i].EmpName ? jsona[i].EmpName : "未知";
                        var content = jsona[i].ToDayContent ? jsona[i].ToDayContent : "";
                        if (content.length > 16) {
                            content = content.substring(0, 15) + "...";
                        }
                        var copyforemp = "";
                        var interactioncount = jsona[i].InteractionCount;
                        if (jsona[i].CopyForEmpNameList && jsona[i].CopyForEmpNameList.length > 0) {
                            copyforemp = "@" + jsona[i].CopyForEmpNameList.join(";@");
                        }

                        //alert(jsona[i].WorkLogType);
                        var worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-info'>日报</div>";
                        if (jsona[i].WorkLogType == '0') {
                            worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-info'>日报</div>";
                        }
                        if (jsona[i].WorkLogType == '1') {
                            worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-warning'>周报</div>";
                        }
                        if (jsona[i].WorkLogType == '2') {
                            worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-success'>月报</div>";
                        }

                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan=\"1\" width=\"50\" align=\"center\" valign=\"middle\"><img width=\"50\" src=\"" + picpath + "\"></td><td width='105' valign='middle' colspan=\"3\"><div style=\"margin-left:5px;font-weight:bold\">" + empname + "</div><div style='float :left;margin-left:5px;font-weight:bold'>日期：" + datecode + "</div>" + worklogtype + "</td></tr><tr style='margin:2px;height:5px'><td width='70' align='center' valign='middle'></td></tr><tr style='margin:2px;height:29px'><td width=\"125\" valign='middle' colspan=\"4\"><div style='float :left;margin-left:5px;'>内容：" + content + "</div><br><div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>抄送：" + copyforemp + "</div></td></tr><tr style='margin:2px;height:29px'><td width=\"125\" valign='middle' colspan=\"4\"><button type=\"button\" class=\"btn btn-primary\" onclick=\"GetInfo('" + jsona[i].Id + "')\" style='float :right;margin-right:5px;vertical-align:middle;' >查看详细</button><button type=\"button\" class=\"btn btn-primary\" onclick=\"Interaction('" + jsona[i].Id + "')\" style='float :right;margin-right:5px;vertical-align:middle;' >交流(" + interactioncount + ")</button></td></tr></table></div></div>";
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
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&sendtype=" + shell_win.sendtype + "&worklogtype=" + shell_win.WorkLogType
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        //alert(jsona[i].Id);
                        shell_win.WorkLogList.push(jsona[i].Id);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona[i].HeadImgUrl ? jsona[i].HeadImgUrl : "../img/icon/user.png";
                        var datecode = jsona[i].DateCode ? jsona[i].DateCode : "xxxx-xx-xx";
                        var adddatetime = jsona[i].DataAddTime ? jsona[i].DataAddTime : "xxxx-xx-xx";
                        var empname = jsona[i].EmpName ? jsona[i].EmpName : "未知";
                        var content = jsona[i].ToDayContent ? jsona[i].ToDayContent : "";
                        if (content.length > 16) {
                            content = content.substring(0, 15) + "...";
                        }
                        var copyforemp = "";
                        var interactioncount = jsona[i].InteractionCount;
                        if (jsona[i].CopyForEmpNameList && jsona[i].CopyForEmpNameList.length > 0) {
                            copyforemp = "@" + jsona[i].CopyForEmpNameList.join(";@");
                        }

                        //alert(jsona[i].WorkLogType);
                        var worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-info'>日报</div>";
                        if (jsona[i].WorkLogType == '0') {
                            worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-info'>日报</div>";
                        }
                        if (jsona[i].WorkLogType == '1') {
                            worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-warning'>周报</div>";
                        }
                        if (jsona[i].WorkLogType == '2') {
                            worklogtype = "<div style='float :right;margin-right:5px;vertical-align:middle;' class='btn-success'>月报</div>";
                        }

                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan=\"1\" width=\"50\" align=\"center\" valign=\"middle\"><img width=\"50\" src=\"" + picpath + "\"></td><td width='105' valign='middle' colspan=\"3\"><div style=\"margin-left:5px;font-weight:bold\">" + empname + "</div><div style='float :left;margin-left:5px;font-weight:bold'>日期：" + datecode + "</div>" + worklogtype + "</td></tr><tr style='margin:2px;height:5px'><td width='70' align='center' valign='middle'></td></tr><tr style='margin:2px;height:29px'><td width=\"125\" valign='middle' colspan=\"4\"><div style='float :left;margin-left:5px;'>内容：" + content + "</div><br><div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>抄送：" + copyforemp + "</div></td></tr><tr style='margin:2px;height:29px'><td width=\"125\" valign='middle' colspan=\"4\"><button type=\"button\" class=\"btn btn-primary\" onclick=\"GetInfo('" + jsona[i].Id + "')\" style='float :right;margin-right:5px;vertical-align:middle;' >查看详细</button><button type=\"button\" class=\"btn btn-primary\" onclick=\"Interaction('" + jsona[i].Id + "')\" style='float :right;margin-right:5px;vertical-align:middle;' >交流(" + interactioncount + ")</button></td></tr></table></div></div>";
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
            //alert("sendtype=" + p["SendType"] + "@worklogtype=" + p["WorkLogType"]);
            if (p["SendType"]) {
                shell_win.sendtype = p["SendType"];
            }
            if (p["WorkLogType"]) {
                shell_win.WorkLogType = p["WorkLogType"];
            }

            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            //$('#ApprovalBatch').on('click', shell_win.ApprovalBatch);
            //$('#ApprovalBatch_action_pass_btn').on('click', shell_win.ApprovalBatch_action_pass_btn);
            //$('#ApprovalBatch_action_rebut_btn').on('click', shell_win.ApprovalBatch_action_rebut_btn);
            shell_win.LoadWorkLog();
        }
    };
    shell_win.init();
});
function Interaction(worklogid) {
    var p = Shell.util.getRequestParams();
    location.href = "WorkLogInteractionList.html?worklogid=" + worklogid + "&WorkLogType=" + p["WorkLogType"];
}
function GetInfo(worklogid) {
    var p = Shell.util.getRequestParams();
    location.href = "WorkLogInfo.html?worklogid=" + worklogid + "&WorkLogType=" + p["WorkLogType"];
}