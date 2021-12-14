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
        Load: function () {
        },
        /**初始化*/
        init: function () {
            //alert('123123123');
            //ATCommon.info.show('aaaaaaaaa');
            var p = Shell.util.getRequestParams();
            //alert("all@"+p["ATEventType"]);
            $('#iframe_content').attr("src", "WorkLogDay.html?SendType=" + p["SendType"] + "&WorkLogType=WorkLogDay");
            //$('#worklogall').on('click', function () {
            //    $(".active").removeClass('active');
            //    $('#iframe_content').attr("src", "WorkLogAll.html?SendType=" + p["SendType"] + "&WorkLogType=all");
            //    $('#unfinishl').addClass('active');
            //});
            $('#worklogday').on('click', function () {
                $(".active").removeClass('active');
                $('#iframe_content').attr("src", "WorkLogDay.html?SendType=" + p["SendType"] + "&WorkLogType=WorkLogDay");
                $('#panel-head').text('日报');
                $('#worklogdayl').addClass('active');
            });
            $('#worklogweek').on('click', function () {
                $(".active").removeClass('active');
                $('#iframe_content').attr("src", "WorkLogWeek.html?SendType=" + p["SendType"] + "&WorkLogType=WorkLogWeek");
                $('#panel-head').text('周报');
                $('#worklogweekl').addClass('active');
            });
            $('#worklogmonth').on('click', function () {
                $(".active").removeClass('active');
                $('#iframe_content').attr("src", "WorkLogMonth.html?SendType=" + p["SendType"] + "&WorkLogType=WorkLogMonth");
                $('#panel-head').text('月报');
                $('#worklogmonthl').addClass('active');
            });
            $("#applog").on(Shell.util.Event.touch, function () {
                //alert($(this).attr('data-url'));
                location.href = $(this).attr('data-url');
            });
            $("#worklogaddi").on(Shell.util.Event.touch, function () {
                //alert($(this).attr('data-url'));
                location.href = $(this).attr('data-url');
            });
            shell_win.Load();
        }
    };
    shell_win.init();
});