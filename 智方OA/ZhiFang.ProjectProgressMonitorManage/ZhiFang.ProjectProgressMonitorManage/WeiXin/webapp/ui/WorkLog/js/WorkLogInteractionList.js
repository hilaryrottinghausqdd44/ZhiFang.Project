$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        pageindex: 1,
        limit: 20,
        worklogid: "",
        WorkLogType: "",
        WorkLogList: new Array(),
        AddInteraction: function () {
            $("#AddInteraction_div").modal('show');
            //alert(shell_win.ApprovalAllLogUnList);
        },
        save_btn_click: function () {
            //alert('123');
            //alert($('#Contents').val());
            //alert($("#leaveeventtype").find("option:selected").text());
            var data = {
                Contents: $('#Contents').val(),
                IsUse: true,
                SenderID: Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID),
                SenderName: Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeName)
            };
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogDay") {
                data.WorkDayLogID = shell_win.worklogid;
            }
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogWeek") {
                data.WorkWeekLogID = shell_win.worklogid;
            }
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogMonth") {
                data.WorkMonthLogID = shell_win.worklogid;
            }
           
            ShellComponent.mask.save();
            Shell.util.Server.ajax({
                type: "post",
                data: Shell.util.JSON.encode({ entity: data }),
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkLogInteraction"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    ShellComponent.messagebox.msg("新增成功！");
                    $("#AddInteraction_div").modal('hide');
                    location.href = location.href;
                } else {
                    shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                }
            });
        },
        cancel_btn_click: function () {
            $("#AddInteraction_div").modal('hide');
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
            var where = "";
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogDay") {
                where = "pworkloginteraction.IsUse=1 and pworkloginteraction.WorkDayLogID=" + shell_win.worklogid;
            }
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogWeek") {
                where = "pworkloginteraction.IsUse=1 and pworkloginteraction.WorkWeekLogID=" + shell_win.worklogid;
            }
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogMonth") {
                where = "pworkloginteraction.IsUse=1 and pworkloginteraction.WorkMonthLogID=" + shell_win.worklogid;
            }
            var fields = "PWorkLogInteraction_DataAddTime,PWorkLogInteraction_SenderName,PWorkLogInteraction_Id,PWorkLogInteraction_SenderID,PWorkLogInteraction_Contents";
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogInteractionByHQL?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&where=" + where + "&fields=" + fields + "&sort=[{\"property\":\"PWorkLogInteraction_DataAddTime\",\"direction\":\"DESC\"}]"
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
                        shell_win.WorkLogList.push(jsona.list[i].Id);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona.list[i].HeadImgUrl ? jsona.list[i].HeadImgUrl : "../img/icon/user.png";
                        var adddatetime = jsona.list[i].DataAddTime ? jsona.list[i].DataAddTime : "xxxx-xx-xx";
                        var sendername = jsona.list[i].SenderName ? jsona.list[i].SenderName : "未知";
                        var content = jsona.list[i].Contents ? jsona.list[i].Contents : "";

                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan=\"1\" width=\"80\" align=\"center\" valign=\"middle\"><img width=\"50\" src=\"" + picpath + "\"></td><td width='105' valign='middle' colspan='3'><div style=\"margin-left:5px;font-weight:bold\">" + content + "</div></td></tr><tr style='margin:2px;height:29px'><td align=\"center\">" + sendername + "</td><td width=\"125\" valign='middle' colspan=\"4\"><div style='float :right;margin-right:5px;vertical-align:middle;font-weight:bold' class='text-primary'>填报时间：" + adddatetime + "</div></td></tr></table></div></div>";
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
            var where = "";
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogDay") {
                where = "pworkloginteraction.IsUse=1 and pworkloginteraction.WorkDayLogID=" + shell_win.worklogid;
            }
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogWeek") {
                where = "pworkloginteraction.IsUse=1 and pworkloginteraction.WorkWeekLogID=" + shell_win.worklogid;
            }
            if (shell_win.WorkLogType && shell_win.WorkLogType == "WorkLogMonth") {
                where = "pworkloginteraction.IsUse=1 and pworkloginteraction.WorkMonthLogID=" + shell_win.worklogid;
            }
            var fields = "PWorkLogInteraction_DataAddTime,PWorkLogInteraction_SenderName,PWorkLogInteraction_Id,PWorkLogInteraction_SenderID,PWorkLogInteraction_Contents";
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogInteractionByHQL?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&where=" + where + "&fields=" + fields + "&sort=[{\"property\":\"PWorkLogInteraction_DataAddTime\",\"direction\":\"DESC\"}]"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.list.length; i++) {
                        //alert(jsona[i].Id);
                        shell_win.WorkLogList.push(jsona.list[i].Id);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona.list[i].HeadImgUrl ? jsona.list[i].HeadImgUrl : "../img/icon/user.png";
                        var adddatetime = jsona.list[i].DataAddTime ? jsona.list[i].DataAddTime : "xxxx-xx-xx";
                        var sendername = jsona.list[i].SenderName ? jsona.list[i].SenderName : "未知";
                        var content = jsona.list[i].Contents ? jsona.list[i].Contents : "";

                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan=\"1\" width=\"80\" align=\"center\" valign=\"middle\"><img width=\"50\" src=\"" + picpath + "\"></td><td width='105' valign='middle' colspan='3'><div style=\"margin-left:5px;font-weight:bold\">" + content + "</div></td></tr><tr style='margin:2px;height:29px'><td align=\"center\">" + sendername + "</td><td width=\"125\" valign='middle' colspan=\"4\"><div style='float :right;margin-right:5px;vertical-align:middle;font-weight:bold' class='text-primary'>填报时间：" + adddatetime + "</div></td></tr></table></div></div>";
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
            if (p["worklogid"]) {
                shell_win.worklogid = p["worklogid"];
            }
            else {
                alert("参数错误id！");
                history.go(-1);
                return;
            }
            if (p["WorkLogType"]) {
                shell_win.WorkLogType = p["WorkLogType"];
            }
            else {
                alert("参数错误type！");
                history.go(-1);
                return;
            }
            //alert(shell_win.worklogid + "&" + shell_win.WorkLogType);
            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            //$('#ApprovalBatch').on('click', shell_win.ApprovalBatch);
            //$('#ApprovalBatch_action_pass_btn').on('click', shell_win.ApprovalBatch_action_pass_btn);
            //$('#ApprovalBatch_action_rebut_btn').on('click', shell_win.ApprovalBatch_action_rebut_btn);

            $("#AddInteraction").on('click', shell_win.AddInteraction);
            $('#AddInteraction_action_pass_btn').on(Shell.util.Event.touch, shell_win.save_btn_click);
            $('#AddInteraction_action_rebut_btn').on(Shell.util.Event.touch, shell_win.cancel_btn_click);
            shell_win.LoadWorkLog();
        }
    };
    shell_win.init();
});