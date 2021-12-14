$(function () {
    //页面所有功能对象
    var shell_win = { 
        save_btn_click: function () {
            //alert('123');
            //alert($('#leaveeventtype').val());
            //alert($("#leaveeventtype").find("option:selected").text());

            var data = {
                EvenLength: $('#evenlength').val(),
                Memo: $('#tripeventmemo').val(),
                EventStatPostion: $('#eventstatpostion_input').val(),
                EventDestinationPostion: $('#eventdestinationpostion_input').val(),
                ATTransportation: { Id: $('#transportationtype').val(), DataTimeStamp:[0,0,0,0,0,0,0,0]},
                TransportationName: $('#transportationtype').find("option:selected").text(),
                ApproveID: $('#approveid').val(),
                ApproveName: $('#approve').val()
            };
            //alert(data.TransportationID);
            ShellComponent.mask.save();
            Shell.util.Server.ajax({
                type: "post",
                data: JSON.stringify({ entity: data, StartDateTime: $('#datetimepickstartinput').val(), EndDateTime: $('#datetimepickendinput').val() }),
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent"
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
        loadtransportationtype:function()
        {
            //alert('123');
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_SearchATTransportationByHQL?page=1&limit=100&fields=ATTransportation_Id,ATTransportation_Name&where=IsUse=1 "
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

                        $("#transportationtype").empty();
                        $("#transportationtype").append(tempAjax);
                        //更新内容刷新到相应的位置
                        $('#transportationtype').selectpicker('render');
                        $('#transportationtype').selectpicker('refresh');
                    }
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ATCommon.info.error(data.ErrorInfo);
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
                    url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetATEmpAttendanceEventDayCount?sd=" + sd + "&ed=" + ed
                }, function (data) {
                    ShellComponent.mask.hide();
                    if (data.success) {
                        //alert(data.ResultDataValue);
                        var jsona = $.parseJSON(data.ResultDataValue);
                        $('#evenlength').val(data.ResultDataValue);
                    } else {
                        //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                        ATCommon.info.error(data.ErrorInfo);
                    }
                });
            }
        },
        /**初始化*/
        init: function () {
            //ATCommon.info.show('aaaaaaaaa');
            $('#datetimepickstart').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: true,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
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
                minView: 2,
                forceParse: 0,
                showMeridian: 1,
                language: 'zh-CN'
            });
            ///*加载审批人*/
            ATCommon.loadapprove();          
            
            shell_win.loadtransportationtype();
            //提交按钮监听
            $("#save_btn").on('click', shell_win.save_btn_click);

            $('#datetimepickstart').datetimepicker().on('changeDate', shell_win.vailddate);

            $('#datetimepickend').datetimepicker().on('changeDate', shell_win.vailddate);

        }
    };
    shell_win.init();
});