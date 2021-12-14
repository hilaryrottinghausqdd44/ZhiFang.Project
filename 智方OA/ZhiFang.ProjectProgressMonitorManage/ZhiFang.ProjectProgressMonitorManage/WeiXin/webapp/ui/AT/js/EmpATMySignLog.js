$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        limit: 5,
        save_btn_click: function () {
            //alert('123');
            //alert($('#leaveeventtype').val());
            //alert($("#leaveeventtype").find("option:selected").text());
            var data = {
                EvenLength: $('#evenlength').val(),
                Memo: $('#overtimeeventmemo').val(),
                ApproveID: $('#approveid').val(),
                ApproveName: $('#approve').val()
            };
            ShellComponent.mask.save();
            Shell.util.Server.ajax({
                type: "post",
                data: Shell.util.JSON.encode({ entity: data, StartDateTime: $('#datetimepickstartinput').val(), EndDateTime: $('#datetimepickendinput').val() }),
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventOvertimeevent"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    ShellComponent.messagebox.msg("新增成功！");
                    history.go(-1);
                    //shell_win.LoadSignInfo();
                    //shell_win.patient.change_member_list(info, data.value);
                    //shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
                    //shell_win.patient.to_page();
                } else {
                    shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                }
            });
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
            //alert(shell_win.tmpdate);
            //alert(Shell.util.Date.toString(Shell.util.Date.getNextDate(shell_win.tmpdate, -1), true));
            var ed = Shell.util.Date.toString(Shell.util.Date.getNextDate(shell_win.tmpdate, -1), true);
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATEmpSignLogByLimit?ed=" + ed + "&limit=" + shell_win.limit
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {

                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        li = document.createElement('li');
                        var datecode = jsona[i].WeekInfo;
                        var workday = jsona[i].IsWorkDay ? "<span class='label label-primary' style='float:right'>工作日</span>" : "<span class='label label-success' style='float:right'>节假日</span>";
                        tmphtml = " <div class='panel panel-default' style='margin:1px'><div class='panel-heading'><h3 class='panel-title'>日期：" + datecode + workday + "</h3></div><div class='panel-body' style='padding:2px'>";

                        var signin = jsona[i].SignInId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].SignInTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";
                        var signinpostionname='';
                        var signinpostionstyle='display:none';
                        if (jsona[i].SigninATEventLogPostionName) {
                            signinpostionname = jsona[i].SigninATEventLogPostionName;
                            signinpostionstyle = 'display:block';
                        }

                        //var signintime = jsona[i].SignInTime ? "<div style='float :right;margin-right:150px;vertical-align:middle;' class='text-success'>"+jsona[i].SignInTime+"</div>" : "";
                        //alert(jsona[i].SignOutId);

                        var signout = jsona[i].SignOutId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].SignOutTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";
                        //var signouttime = jsona[i].SignOutTime ? "<div style='float :right;margin-right:50px;vertical-align:middle;' class='text-success'>"+jsona[i].SignOutTime+"</div>" : "";

                        var signoutpostionname = '';
                        var signoutpostionstyle = 'display:none';
                        if (jsona[i].SignoutATEventLogPostionName) {
                            signoutpostionname = jsona[i].SignoutATEventLogPostionName;
                            signoutpostionstyle = 'display:block';
                        }

                        var late = jsona[i].SignInType ? "<button type='button' class='btn btn-danger  btn-xs'>迟到</button>" : "&nbsp;";
                        var latememo = jsona[i].SignInType ? "<button type='button' class='btn btn-warning  btn-xs' onclick=\"AddMemo('" + jsona[i] + "')\"  >填写说明</button>" : "&nbsp;";

                        var leaveearly = jsona[i].SignOutType ? "<button type='button' class='btn btn-danger  btn-xs'>早退</button>" : "&nbsp;";
                        var leaveearlymemo = jsona[i].SignOutType ? "<button type='button' class='btn btn-warning  btn-xs' onclick=\"AddMemo('" + jsona[i] + "')\"  >填写说明</button>" : "&nbsp;";

                        var other = jsona[i].OtherInfo ? "<p class='text-warning' style='padding:1px'>" + jsona[i].OtherInfo + "</p>" : "&nbsp;";

                        //li.innerHTML
                        tmphtml += "<table style='width:100%;' border='0'><tr style='margin:2px;height:25px'><td width='150px' ><div style='float :left;margin-left:5px;'>签到：</div>" + signin + "</td><td >" + late + "</td><td align='left' >" + latememo + "</td></tr><tr style='margin:2px;height:25px;'><td colspan=3 ><div style='float :left;margin-left:5px;'>签到地址：" + signinpostionname + "</div></td></tr><tr style='margin:2px;height:25px'><td width='150px' ><div style='float :left;margin-left:5px;'>签退：</div>" + signout + "</td><td>" + leaveearly + "</td><td>" + leaveearlymemo + "</td></tr><tr style='margin:2px;height:25px'><td colspan=3><div style='float :left;margin-left:5px;'>签退地址：</div>" + signoutpostionname + "</td></tr><tr style='margin:2px;height:25px'><td colspan='4' >" + other + "</td></tr></table></div></div>";
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.tmpdate = jsona[jsona.length - 1].ATEventDateCode;
                    wrapper.refresh();
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ATCommon.info.error(data.msg);
                }
            });
        },
        /*列表初始化*/
        LoadSignLog: function () {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATEmpSignLogByLimit?limit=" + shell_win.limit
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        li = document.createElement('li');
                        var datecode = jsona[i].WeekInfo;
                        var workday = jsona[i].IsWorkDay ? "<span class='label label-primary' style='float:right'>工作日</span>" : "<span class='label label-success' style='float:right'>节假日</span>";
                        tmphtml = " <div class='panel panel-default' style='margin:1px'><div class='panel-heading'><h3 class='panel-title'>日期：" + datecode + workday + "</h3></div><div class='panel-body' style='padding:2px'>";

                        var signin = jsona[i].SignInId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].SignInTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";

                        var signinpostionname = '';
                        var signinpostionstyle = 'display:none';
                        if (jsona[i].SigninATEventLogPostionName) {
                            signinpostionname = jsona[i].SigninATEventLogPostionName;
                            signinpostionstyle = 'display:block';
                        }
                        //var signintime = jsona[i].SignInTime ? "<div style='float :right;margin-right:150px;vertical-align:middle;' class='text-success'>"+jsona[i].SignInTime+"</div>" : "";
                        //alert(jsona[i].SignOutId);

                        var signout = jsona[i].SignOutId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].SignOutTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";
                        //var signouttime = jsona[i].SignOutTime ? "<div style='float :right;margin-right:50px;vertical-align:middle;' class='text-success'>"+jsona[i].SignOutTime+"</div>" : "";
                        var signoutpostionname = '';
                        var signoutpostionstyle = 'display:none';
                        if (jsona[i].SignoutATEventLogPostionName) {
                            signoutpostionname = jsona[i].SignoutATEventLogPostionName;
                            signoutpostionstyle = 'display:block';
                        }

                        var late = jsona[i].SignInType ? "<button type='button' class='btn btn-danger  btn-xs'>迟到</button>" : "&nbsp;";

                        var latememo = "&nbsp;";
                        if (jsona[i].SignInType) {
                            if (jsona[i].SignInMemo) {
                                latememo = "已填写说明";
                            }
                            else {
                                latememo = "<button type='button' class='btn btn-warning  btn-xs' onclick=\"AddMemo('" + jsona[i].SignInId + "')\"  >填写说明</button>";
                            }
                        }

                        var leaveearly = jsona[i].SignOutType ? "<button type='button' class='btn btn-danger  btn-xs'>早退</button>" : "&nbsp;";
                        var leaveearlymemo = "&nbsp;";
                        if (jsona[i].SignOutType) {
                            if (jsona[i].SignOutMemo) {
                                leaveearlymemo = "已填写说明";
                            }
                            else {
                                leaveearlymemo = "<button type='button' class='btn btn-warning  btn-xs' onclick=\"AddMemo('" + jsona[i].SignOutId + "')\"  >填写说明</button>"
                            }
                        }

                        var other = jsona[i].OtherInfo ? "<p class='text-warning' style='padding:1px'>" + jsona[i].OtherInfo + "</p>" : "&nbsp;";

                        tmphtml += "<table style='width:100%;' border='0'><tr style='margin:2px;height:25px'><td width='150px' ><div style='float :left;margin-left:5px;'>签到：</div>" + signin + "</td><td >" + late + "</td><td align='left' >" + latememo + "</td></tr><tr style='margin:2px;height:25px;'><td colspan=3 ><div style='float :left;margin-left:5px;'>签到地址：" + signinpostionname + "</div></td></tr><tr style='margin:2px;height:25px'><td width='150px' ><div style='float :left;margin-left:5px;'>签退：</div>" + signout + "</td><td>" + leaveearly + "</td><td>" + leaveearlymemo + "</td></tr><tr style='margin:2px;height:25px'><td colspan=3><div style='float :left;margin-left:5px;'>签退地址：</div>" + signoutpostionname + "</td></tr><tr style='margin:2px;height:25px'><td colspan='4' >" + other + "</td></tr></table></div></div>";
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.tmpdate = jsona[jsona.length - 1].ATEventDateCode;
                    wrapper.refresh();
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ATCommon.info.error(data.msg);
                }
            });
        },
        InputMemo_action_save_btn: function (singid) {
            if ($("#memo").val() == "") {
                ATCommon.info.error("说明信息未保存！错误原因：未输入说明信息！");
                return;
            }
            if ($("#signid").val() == "")
            {
                ATCommon.info.error("说明信息未保存！错误原因：考勤记录错误！");
                return;
            }
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "post",
                data: Shell.util.JSON.encode({ entity: { Memo: $("#memo").val(), Id: $("#signid").val() }, fields: "Memo,Id" }),
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_UpdateATEmpAttendanceEventLogByField"
            }, function (data) {
                ShellComponent.mask.hide();
                $("#InputMemo_div").modal('hide');
                if (data.success) {
                    location.href = location.href;
                } else {
                    ATCommon.info.error("说明信息未保存！错误原因：" + data.ErrorInfo);
                }
            });
            $("#signid").val("");
            $("#memo").val("");
        },
        InputMemo_action_cancel_btn: function (singid) {
            $("#signid").val("");
            $("#memo").val("");
        },
        /**初始化*/
        init: function () {
            //alert('123123123');
            //ATCommon.info.show('aaaaaaaaa');
            //$('#aaa').val('aaa');
            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            $('#InputMemo_action_save_btn').on('click', shell_win.InputMemo_action_save_btn);
            $('#InputMemo_action_cancel_btn').on('click', shell_win.InputMemo_action_cancel_btn);
            shell_win.LoadSignLog();
        }
    };
    shell_win.init();
});
function AddMemo(signid) {
    //alert(signid);
    if (signid) {
        $("#signid").val(signid);
        $("#InputMemo_div").modal('show');
    }
}