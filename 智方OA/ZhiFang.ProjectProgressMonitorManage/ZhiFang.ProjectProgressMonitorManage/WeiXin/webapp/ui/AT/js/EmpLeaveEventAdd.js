$(function () {
    //页面所有功能对象
    var shell_win = {
        /*加载请假类型字典*/
        loadleaveeventtypelist: function () {
            //alert('123');
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_SearchATAttendanceEventTypeByHQL?page=1&limit=100&fields=ATAttendanceEventType_Id,ATAttendanceEventType_Name&where=ATEventTypeGroupID=103 and IsUse=1 and ATEventTypeID<>10301"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    var jsona = $.parseJSON(data.ResultDataValue);
                    var tempAjax = '';
                    for (var i = 0; i < jsona.list.length; i++) {
                        // alert(jsona.list[i].Name);
                        //var option = $("<option>").val(jsona.list[i].Id).text(jsona.list[i].Name);
                        //$("#leaveeventtype").append(option);
                        tempAjax += "<option value='" + jsona.list[i].Id + "'>" + jsona.list[i].Name + "</option>";

                        $("#leaveeventtype").empty();
                        $("#leaveeventtype").append(tempAjax);
                        //更新内容刷新到相应的位置
                        $('#leaveeventtype').selectpicker('render');
                        $('#leaveeventtype').selectpicker('refresh');
                    }
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ATCommon.info.error(data.msg);
                }
            });
        },       
        leaveevent_save_btn_click: function () {
            //alert('123');
            //alert($('#leaveeventtype').val());
            //alert($("#leaveeventtype").find("option:selected").text());

            var data = {
                ATEventSubTypeID: $('#leaveeventtype').val(),
                ATEventSubTypeName: $("#leaveeventtype").find("option:selected").text(),
                Memo: $('#leaveeventmemo').val(),
                EvenLength: $('#evenlength').val(),
                ApproveID: $('#approveid').val(),
                ApproveName: $('#approve').val()
            };
            ShellComponent.mask.save();
            Shell.util.Server.ajax({
                type: "post",
                data: JSON.stringify({ entity: data, StartDateTime: $('#datetimepickstartinput').val(), EndDateTime: $('#datetimepickendinput').val() }),
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddAndCheckATEmpAttendanceEventleaveevent"
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
                    ATCommon.info.error(data.ErrorInfo);
                }
            });
        },
        vailddate: function (ev) {
            //alert(ev.date.Format("yyyy-MM-dd hh:mm:ss"));
            //alert($('#datetimepickstartinput').val());
            var sd=$('#datetimepickstartinput').val();
            var ed = $('#datetimepickendinput').val()
            if ($('#datetimepickstartinput').val() != "" && $('#datetimepickendinput').val() != "") {
                ShellComponent.mask.loading();
                Shell.util.Server.ajax({
                    type: "get",
                    url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetATEmpAttendanceEventDayCount?sd=" + sd + "&ed=" + ed
                }, function (data) {
                    ShellComponent.mask.hide();
                    if (data.success) {
                        //alert(data.ResultDataValue);
                        var jsona = $.parseJSON(data.ResultDataValue);
                        $('#evenlength').val(data.ResultDataValue);
                    } else {
                        //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                        ATCommon.info.error(data.msg);
                    }
                });
            }
        },
        /**初始化*/
        init: function () {
            //alert('aaa');
            $('#datetimepickstart').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: true,
                todayHighlight: 1,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                language: 'zh-CN'
            });
            $('#datetimepickend').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: true,
                todayHighlight: 1,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                language: 'zh-CN'
            });
            /*加载请假类型字典*/
            shell_win.loadleaveeventtypelist();
            /*加载审批人*/
            ATCommon.loadapprove();
            //提交按钮监听
            $("#leaveevent_save_btn").on('click', shell_win.leaveevent_save_btn_click);

            $('#datetimepickstart').datetimepicker().on('changeDate', shell_win.vailddate);

            $('#datetimepickend').datetimepicker().on('changeDate', shell_win.vailddate);
           
        }
    };
    shell_win.init();
});