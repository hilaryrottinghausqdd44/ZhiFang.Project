$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        pageindex: 1,
        limit: 50,
        approvestatus: null,
        apsid: "",
        ApprovalAllLogUnList: new Array(),
        ApprovalBatch: function () {
            $("#ApprovalBatch_div").modal('show');
            //alert(shell_win.ApprovalAllLogUnList);
        },
        ApprovalBatch_action_pass_btn: function () {
            // alert($("#ApprovalBatch_memo").val());
            // alert(shell_win.ApprovalAllLogUnList);
            //var data = {
            //    ATEventTypeName: ateventtypename,
            //    Memo: memo,
            //    ATEventLogPostion: latitude + ',' + longitude
            //};
            //Shell.util.Server.ajax({
            //    type: "post",
            //    data: Shell.util.JSON.encode({ entity: data, StartDateTime: $('#datetimepickstartinput').val(), EndDateTime: $('#datetimepickendinput').val() }),
            //    url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventTripevent"
            //}
            if ($('#tmpid').val() && $('#tmpid').val() != "") {
                Approval([$('#tmpid').val()], 1);
            }
            else {
                Approval(shell_win.ApprovalAllLogUnList, 1);
            }
        },
        ApprovalBatch_action_rebut_btn: function () {
            if ($('#tmpid').val() && $('#tmpid').val() != "") {
                Approval([$('#tmpid').val()], 2);
            }
            else {
                Approval(shell_win.ApprovalAllLogUnList, 2);
            }
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
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATMyApprovalAllLogByEmpId?limit=" + shell_win.limit + "&pageindex=" + shell_win.pageindex + "&apsid=" + shell_win.apsid
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {

                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        shell_win.ApprovalAllLogUnList.push(jsona[i].ATEmpAttendanceEventLogId);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona[i].ApplyEmp.HeadImgUrl ? jsona[i].ApplyEmp.HeadImgUrl : "../img/icon/user.png";

                        var applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>&nbsp;</div>";
                        var eventinfo = jsona[i].EvenLength + jsona[i].EvenLengthUnit;
                        if (jsona[i].ATEventTypeID == '10301') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>请假" + eventinfo + "(" + jsona[i].ATEventSubTypeName + ")</div>";
                        }
                        if (jsona[i].ATEventTypeID == '10401') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-info'>外出" + eventinfo + "</div>";
                        }
                        if (jsona[i].ATEventTypeID == '10501') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-primary'>出差" + eventinfo + "</div>";
                        }
                        if (jsona[i].ATEventTypeID == '10601') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-warning'>加班" + eventinfo + "</div>";
                        }
                        var empname = jsona[i].ApplyEmp.EmpName ? jsona[i].ApplyEmp.EmpName : "未知";
                        var startdate = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].StartDateTime + "</div>";
                        var enddate = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].EndDateTime + "</div>";
                        var memo = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>&nbsp;</div>";
                        if (jsona[i].Memo) {
                            memo = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].Memo + "</div>";
                        }
                        var applydate = jsona[i].DataAddTime;
                        tmphtml += "<table style='width:100%;' border='0'><tr style='margin:2px;height:29px'><td rowspan='3' width='70' align='center' valign='middle'><img width='60' src='" + picpath + "'></td><td width='105' valign='middle' colspan='3'><div style='float :left;margin-left:5px;'>事项：</div>" + applyhtml + "</td></tr><tr style='margin:2px;height:29px'><td width='130' valign='middle'><div style='float :left;margin-left:5px;'>时间：</div>" + startdate + "</td><td width='15px' style='padding:0px'>至</td><td align='left'>" + enddate + "</td></tr><tr style='margin:2px;height:29px'><td valign='middle' colspan='3'>" + memo + "</td></tr><tr style='margin:2px;height:20px'><td width='70' align='center' valign='middle'>" + empname + "</td><td colspan='3' valign='middle'><p class='text-warning' style='float :left;padding:5px;'>" + applydate + "申请</p><button id=\"Approval_" + jsona[i].ATEmpAttendanceEventLogId + "\" type=\"button\" class=\"btn btn-success\" style='float :right;margin-right:5px;' onclick=\"ApprovalById('" + jsona[i].ATEmpAttendanceEventLogId + "')\">审批</button></td></tr></table></div></div>";
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.pageindex = shell_win.pageindex + 1;
                    wrapper.refresh();
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ATCommon.info.error(data.msg);
                }
            });
        },
        /*列表初始化*/
        LoadApprovalAllLog: function () {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATMyApprovalAllLogByEmpId?limit=" + shell_win.limit + "&pageindex=" + shell_win.pageindex + "&apsid=" + shell_win.apsid
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        shell_win.ApprovalAllLogUnList.push(jsona[i].ATEmpAttendanceEventLogId);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var picpath = jsona[i].ApplyEmp.HeadImgUrl ? jsona[i].ApplyEmp.HeadImgUrl : "../img/icon/user.png";

                        var applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>&nbsp;</div>";
                        var eventinfo = jsona[i].EvenLength + jsona[i].EvenLengthUnit;
                        if (jsona[i].ATEventTypeID == '10301') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>请假" + eventinfo + "(" + jsona[i].ATEventSubTypeName + ")</div>";
                        }
                        if (jsona[i].ATEventTypeID == '10401') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-info'>外出" + eventinfo + "</div>";
                        }
                        if (jsona[i].ATEventTypeID == '10501') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-primary'>出差" + eventinfo + "</div>";
                        }
                        if (jsona[i].ATEventTypeID == '10601') {
                            applyhtml = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-warning'>加班" + eventinfo + "</div>";
                        }
                        var empname = jsona[i].ApplyEmp.EmpName ? jsona[i].ApplyEmp.EmpName : "未知";
                        var startdate = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].StartDateTime + "</div>";
                        var enddate = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].EndDateTime + "</div>";
                        var memo = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>&nbsp;</div>";
                        if (jsona[i].Memo) {
                            memo = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].Memo + "</div>";
                        }
                        var applydate = jsona[i].DataAddTime;
                        tmphtml += "<table style='width:100%;' border='0'><tr style='margin:2px;height:29px'><td rowspan='3' width='70' align='center' valign='middle'><img width='60' src='" + picpath + "'></td><td width='105' valign='middle' colspan='3'><div style='float :left;margin-left:5px;'>事项：</div>" + applyhtml + "</td></tr><tr style='margin:2px;height:29px'><td width='130' valign='middle'><div style='float :left;margin-left:5px;'>时间：</div>" + startdate + "</td><td width='15px' style='padding:0px'>至</td><td align='left'>" + enddate + "</td></tr><tr style='margin:2px;height:29px'><td valign='middle' colspan='3'>" + memo + "</td></tr><tr style='margin:2px;height:20px'><td width='70' align='center' valign='middle'>" + empname + "</td><td colspan='3' valign='middle'><p class='text-warning' style='float :left;padding:5px;'>" + applydate + "申请</p><button id=\"Approval_" + jsona[i].ATEmpAttendanceEventLogId + "\" type=\"button\" class=\"btn btn-success\" style='float :right;margin-right:5px;' onclick=\"ApprovalById('" + jsona[i].ATEmpAttendanceEventLogId + "')\">审批</button></td></tr></table></div></div>";
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.pageindex = shell_win.pageindex + 1;
                    wrapper.refresh();
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ATCommon.info.error(data.msg);
                }
            });
        },
        /**初始化*/
        init: function () {
            //alert('aaa');
            //ATCommon.info.show('aaaaaaaaa');
            //$('#aaa').val('aaa');
            var p = Shell.util.getRequestParams();
            //alert("Un@" + p["ATEventType"]);
            if (p["ATEventType"] == "leave") {
                shell_win.apsid = "10301";
            }
            if (p["ATEventType"] == "egress") {
                shell_win.apsid = "10401";
            }
            if (p["ATEventType"] == "trip") {
                shell_win.apsid = "10501";
            }
            if (p["ATEventType"] == "overtime") {
                shell_win.apsid = "10601";
            }

            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            $('#ApprovalBatch').on('click', shell_win.ApprovalBatch);
            $('#ApprovalBatch_action_pass_btn').on('click', shell_win.ApprovalBatch_action_pass_btn);
            $('#ApprovalBatch_action_rebut_btn').on('click', shell_win.ApprovalBatch_action_rebut_btn);
            shell_win.LoadApprovalAllLog();
        }
    };
    shell_win.init();
});
function ApprovalById(id) {
    
    $("#tmpid").val(id);
    $("#ApprovalBatch_div").modal('show');
}
function Approval(ids, status) {

    ShellComponent.mask.loading();
    Shell.util.Server.ajax({
        type: "post",
        data: Shell.util.JSON.encode({ memo: $("#ApprovalBatch_memo").val(), eventlogids: ids, type: status }),
        url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ApprovalATApplyEventLog"
    }, function (data) {
        //alert('a');
        ShellComponent.mask.hide();
        //alert('b');
        $("#ApprovalBatch_div").modal('hide');
        if (data.success) {
            //alert('c');
            location.href = location.href;
        } else {
            $("#tmpid").val("");
            shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
        }
    });
}