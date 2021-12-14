$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        limit: 2,
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
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATOtherSignLogByLimit?ed=" + ed + "&limit=" + shell_win.limit
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        //alert("i=" + jsona[i].ATEventDateCode);
                        li = document.createElement('li');
                        var datecode = jsona[i].WeekInfo;

                        var signloglist = jsona[i].SignLogL;
                        var tmphtml = "";
                        var workday = jsona[i].IsWorkDay ? "<span class='label label-primary' style='float:right'>工作日</span>" : "<span class='label label-success' style='float:right'>节假日</span>";
                        tmphtml = " <div class='panel panel-default' style='margin:1px'><div class='panel-heading'><h3 class='panel-title'>日期：" + datecode + workday + "</h3></div><div class='panel-body' style='padding:2px;'>";

                        for (var j = 0; j < signloglist.length; j++) {
                            //alert("j=" + signloglist[j].EmpName);
                            var empname = signloglist[j].EmpName ? signloglist[j].EmpName : "";
                            var picpath = signloglist[j].HeadImgUrl ? signloglist[j].HeadImgUrl : "../img/icon/user.png";


                            var signin = signloglist[j].SignInId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + signloglist[j].SignInTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";
                            //alert(jsona[i].SignOutId);
                            var signinpostionname = '';
                            var signinpostionstyle = 'display:none';
                            if (signloglist[j].SigninATEventLogPostionName) {
                                signinpostionname = signloglist[j].SigninATEventLogPostionName;
                                signinpostionstyle = 'display:block';
                            }
                            var signout = signloglist[j].SignOutId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + signloglist[j].SignOutTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";
                            var signoutpostionname = '';
                            var signoutpostionstyle = 'display:none';
                            if (signloglist[j].SignoutATEventLogPostionName) {
                                signoutpostionname = signloglist[j].SignoutATEventLogPostionName;
                                signoutpostionstyle = 'display:block';
                            }
                            var late = signloglist[j].SignInType ? "<button type='button' class='btn btn-danger  btn-xs'>迟到</button>" : "&nbsp;";
                            var latememo = signloglist[j].SignInType ? "<button type='button' class='btn btn-warning  btn-xs' onclick='alert(1)'  >填写说明</button>" : "&nbsp;";

                            var leaveearly = signloglist[j].SignOutType ? "<button type='button' class='btn btn-danger  btn-xs'>早退</button>" : "&nbsp;";
                            var leaveearlymemo = signloglist[j].SignOutType ? "<button type='button' class='btn btn-warning  btn-xs' onclick='alert(1)'  >填写说明</button>" : "&nbsp;";

                            var other = signloglist[j].OtherInfo ? "<p class='text-warning' style='padding:1px'>" + jsona[i].OtherInfo + "</p>" : "&nbsp;";
                            var line = j < signloglist.length - 1 ? "<tr style='margin:2px;height:1px;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey;'><td colspan='4' ></td></tr>" : "";

                            tmphtml += "<table style='width:100%;' border='0'><tr style='margin:2px;height:29px'><td rowspan='4' width='70' align='center' valign='middle'><img width='60' src='" + picpath + "'></td><td width='105' valign='middle'><div style='float :left;margin-left:5px;'>签到：</div>" + signin + "</td><td width='75px' style='padding:0px'>" + late + "</td><td align='left'>" + latememo + "</td></tr><tr style='margin:2px;height:25px;'><td colspan=3 ><div style='float :left;margin-left:5px;'>签到地址：" + signinpostionname + "</div></td></tr><tr style='margin:2px;height:29px'><td width='150px' ><div style='float :left;margin-left:5px;'>签退：</div>" + signout + "</td><td>" + leaveearly + "</td><td>" + leaveearlymemo + "</td></tr><tr style='margin:2px;height:25px'><td colspan=3><div style='float :left;margin-left:5px;'>签退地址：</div>" + signoutpostionname + "</td></tr><tr style='margin:2px;height:20px'><td width='70' align='center' valign='middle'>" + empname + "</td><td colspan='3' >" + other + "</td></tr>" + line + "</table>"
                        }
                        li.innerHTML += tmphtml + "</div></div>";
                        //alert(li.innerHTML);
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
        LoadOtherSignLog: function () {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATOtherSignLogByLimit?limit=" + shell_win.limit
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        //alert("i=" + jsona[i].ATEventDateCode);
                        li = document.createElement('li');
                        var datecode = jsona[i].WeekInfo;

                        var signloglist = jsona[i].SignLogL;
                        var tmphtml = "";
                        var workday = jsona[i].IsWorkDay && jsona[i].IsWorkDay ? "<span class='label label-primary' style='float:right'>工作日</span>" : "<span class='label label-success' style='float:right'>节假日</span>";
                        tmphtml = " <div class='panel panel-default' style='margin:1px'><div class='panel-heading'><h3 class='panel-title'>日期：" + datecode + workday + "</h3></div><div class='panel-body' style='padding:2px;'>";

                        for (var j = 0; j < signloglist.length; j++) {
                            //alert("j=" + signloglist[j].EmpName);
                            var empname = signloglist[j].EmpName ? signloglist[j].EmpName : "";
                            var picpath = signloglist[j].HeadImgUrl ? signloglist[j].HeadImgUrl : "../img/icon/user.png";

                            var signin = signloglist[j].SignInId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + signloglist[j].SignInTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";
                            //alert(jsona[i].SignOutId);
                            var signinpostionname = '';
                            var signinpostionstyle = 'display:none';
                            if (signloglist[j].SigninATEventLogPostionName) {
                                signinpostionname = signloglist[j].SigninATEventLogPostionName;
                                signinpostionstyle = 'display:block';
                            }
                            var signout = signloglist[j].SignOutId ? "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + signloglist[j].SignOutTime + "</div>" : "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-error'>未打卡</div>";
                            var signoutpostionname = '';
                            var signoutpostionstyle = 'display:none';
                            if (signloglist[j].SignoutATEventLogPostionName) {
                                signoutpostionname = signloglist[j].SignoutATEventLogPostionName;
                                signoutpostionstyle = 'display:block';
                            }

                            var late = signloglist[j].SignInType ? "<button type='button' class='btn btn-danger  btn-xs'>迟到</button>" : "&nbsp;";
                            var latememo = signloglist[j].SignInType ? "<button type='button' class='btn btn-warning  btn-xs' onclick='alert(1)'  >填写说明</button>" : "&nbsp;";

                            var leaveearly = signloglist[j].SignOutType ? "<button type='button' class='btn btn-danger  btn-xs'>早退</button>" : "&nbsp;";
                            var leaveearlymemo = signloglist[j].SignOutType ? "<button type='button' class='btn btn-warning  btn-xs' onclick='alert(1)'  >填写说明</button>" : "&nbsp;";

                            var other = signloglist[j].OtherInfo ? "<p class='text-warning' style='padding:1px'>" + jsona[i].OtherInfo + "</p>" : "&nbsp;";
                            var line = j<signloglist.length-1?"<tr style='margin:2px;height:1px;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey;'><td colspan='4' ></td></tr>":"";

                            tmphtml += "<table style='width:100%;' border='0'><tr style='margin:2px;height:29px'><td rowspan='4' width='70' align='center' valign='middle'><img width='60' src='" + picpath + "'></td><td width='105' valign='middle'><div style='float :left;margin-left:5px;'>签到：</div>" + signin + "</td><td width='75px' style='padding:0px'>" + late + "</td><td align='left'>" + latememo + "</td></tr><tr style='margin:2px;height:25px;'><td colspan=3 ><div style='float :left;margin-left:5px;'>签到地址：" + signinpostionname + "</div></td></tr><tr style='margin:2px;height:29px'><td width='150px' ><div style='float :left;margin-left:5px;'>签退：</div>" + signout + "</td><td>" + leaveearly + "</td><td>" + leaveearlymemo + "</td></tr><tr style='margin:2px;height:25px'><td colspan=3><div style='float :left;margin-left:5px;'>签退地址：</div>" + signoutpostionname + "</td></tr><tr style='margin:2px;height:20px'><td width='70' align='center' valign='middle'>" + empname + "</td><td colspan='3' >" + other + "</td></tr>" + line + "</table>"
                        }
                        li.innerHTML += tmphtml + "</div></div>";
                        //alert(li.innerHTML);
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
            shell_win.LoadOtherSignLog();
        }
    };
    shell_win.init();
});