$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        pageindex: 1,
        limit: 5,
        apsid: 10601,
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
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATMyApplyAllLogByLimit?limit=" + shell_win.limit + "&pageindex=" + shell_win.pageindex
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {

                    //var str = "{\"id\":\"123123123\"}";
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.length; i++) {
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px'><div class='panel-body' style='padding:2px'>";
                        var applyhtml = "<a class='btn btn-success btn-circle btn-xl'>请假</a>";
                        if (jsona[i].ATEventTypeID == '10301') {
                            applyhtml = "<a class='btn btn-success btn-circle btn-xl'>请假</a>";
                        }
                        if (jsona[i].ATEventTypeID == '10401') {
                            applyhtml = "<a class='btn btn-info btn-circle btn-xl'>外出</a>";
                        }
                        if (jsona[i].ATEventTypeID == '10501') {
                            applyhtml = "<a class='btn btn-primary btn-circle btn-xl'>出差</a>";
                        }
                        if (jsona[i].ATEventTypeID == '10601') {
                            applyhtml = "<a class='btn btn-warning btn-circle btn-xl'>加班</a>";
                        }

                        var startdate = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].StartDateTime + "</div>";
                        var enddate = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].EndDateTime + "</div>";
                        var memo = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].Memo + "</div>";
                        var day = jsona[i].EvenLength + jsona[i].EvenLengthUnit;
                        var applydate = jsona[i].DataAddTime;
                        var approvestatus = "<button type='button' style='float:right' class='btn btn-primary'>未批准</button>";
                        if (jsona[i].ApproveStatusID) {
                            if (jsona[i].ApproveStatusID == 1) {
                                approvestatus = "<button type='button' style='float:right' class='btn btn-success'>已批准</button>";
                            }
                            if (jsona[i].ApproveStatusID == 2) {
                                approvestatus = "<button type='button' style='float:right' class='btn btn-danger'>已打回</button>";
                            }
                        }
                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan='2' width='70' align='center' valign='middle'>" + applyhtml + "</td><td width='150' valign='middle'><div style='float :left;margin-left:5px;'>时间：</div>" + startdate + "</td><td width='15px' style='padding:0px'>至</td><td align='left'>" + enddate + "</td></tr><tr style='margin:2px;height:29px'><td colspan='3'><div style='float :left;margin-left:5px;'>事由：</div>" + memo + "</td></tr><tr style='margin:2px;height:20px' valign='middle'><td width='70' align='center' valign='middle'>" + day + "</td><td colspan='3' valign='middle'>申请时间：" + applydate + "</td><td>" + approvestatus + "</td></tr></table></div></div>";
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
        LoadSignLog: function () {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/SearchATMyApplyAllLogByLimit?limit=" + shell_win.limit + "&pageindex=" + shell_win.pageindex + "&apsid=" + shell_win.apsid
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
                        tmphtml = " <div class='panel panel-default' style='margin:1px'><div class='panel-body' style='padding:2px'>";
                        var applyhtml = "<a class='btn btn-success btn-circle btn-xl'>请假</a>";
                        if (jsona[i].ATEventTypeID == '10301') {
                            applyhtml = "<a class='btn btn-success btn-circle btn-xl'>请假</a>";
                        }
                        if (jsona[i].ATEventTypeID == '10401') {
                            applyhtml = "<a class='btn btn-info btn-circle btn-xl'>外出</a>";
                        }
                        if (jsona[i].ATEventTypeID == '10501') {
                            applyhtml = "<a class='btn btn-primary btn-circle btn-xl'>出差</a>";
                        }
                        if (jsona[i].ATEventTypeID == '10601') {
                            applyhtml = "<a class='btn btn-warning btn-circle btn-xl'>加班</a>";
                        }

                        var startdate = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].StartDateTime + "</div>";
                        var enddate = "<div style='float :left;margin-left:5px;vertical-align:middle;' class='text-success'>" + jsona[i].EndDateTime + "</div>";
                        var memo = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>&nbsp;</div>";
                        if (jsona[i].Memo) {
                            memo = "<div style='float :left;margin-left:0px;vertical-align:middle;' class='text-success'>" + jsona[i].Memo + "</div>";
                        }
                        var day = jsona[i].EvenLength + jsona[i].EvenLengthUnit;
                        var applydate = jsona[i].DataAddTime;
                        var approvestatus = "<button type='button' style='float:right' class='btn btn-primary'>未批准</button>";
                        if (jsona[i].ApproveStatusID) {
                            if (jsona[i].ApproveStatusID == 1) {
                                approvestatus = "<button type='button' style='float:right' class='btn btn-success'>已批准</button>";
                            }
                            if (jsona[i].ApproveStatusID == 2) {
                                approvestatus = "<button type='button' style='float:right' class='btn btn-danger'>已打回</button>";
                            }
                        }
                        tmphtml += "<table style='width:100%;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td rowspan='2' width='70' align='center' valign='middle'>" + applyhtml + "</td><td width='150' valign='middle'><div style='float :left;margin-left:5px;'>时间：</div>" + startdate + "</td><td width='15px' style='padding:0px'>至</td><td align='left'>" + enddate + "</td></tr><tr style='margin:2px;height:29px'><td colspan='3'><div style='float :left;margin-left:5px;'>事由：</div>" + memo + "</td></tr><tr style='margin:2px;height:20px' valign='middle'><td width='70' align='center' valign='middle'>" + day + "</td><td colspan='3' valign='middle'>申请时间：" + applydate + "</td><td>" + approvestatus + "</td></tr></table></div></div>";
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
            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            shell_win.LoadSignLog();
        }
    };
    shell_win.init();
});