var ATCommon = ATCommon || {
    AttendanceEventTypeID: {
        SignIn: "10101",
        SignInLate: "10102",
        SignOut: "10201",
        SignOutLeaveearly: "10202",
        Leave: "10301",
        LeaveSick: "10302",
        LeaveSupplementaryCard: "10303",
        LeaveMaternity: "10304",
        LeaveOff: "10305",
        LeaveNursing: "10306",
        LeaveMarriage: "10307",
        LeaveAnnual: "10308",
        LeaveFuneral: "10309",
        LeaveAffair : "10310",
        Egress: "10401",
        Trip: "10501",
        Overtime: "10601"
    },
    AttendanceEventTypeName: {
        SignIn: "签到",
        SignInLate: "迟到",
        SignOut: "签退",
        SignOutLeaveearly: "早退",
        Leave: "请假",
        LeaveSick: "病假",
        LeaveSupplementaryCard: "补签卡",
        LeaveMaternity: "产假",
        LeaveOff: "调休",
        LeaveNursing: "护理假",
        LeaveMarriage: "婚假",
        LeaveAnnual: "年休假",
        LeaveFuneral: "丧假",
        LeaveAffair : "事假",
        Egress: "外出",
        Trip: "出差",
        Overtime: "加班"
    },
    info: {
        /**显示正确信息*/
        show: function (value) {
            ShellComponent.messagebox.msg(value);
        },
        /**显示错误信息*/
        error: function (value) {
            var msg = '<b style="color:red;">' + value + '</b>';
            ShellComponent.messagebox.msg(msg);
        }
    },
    /*加载审批人*/
    loadapprove: function () {
        //alert('loadleaveeventapprove');
        //alert(Shell.util.Cookie.getCookie('HRDeptID'));
        ShellComponent.mask.loading();
        Shell.util.Server.ajax({
            type: "get",
            //url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetATEmpAttendanceEventApproveByDeptId?DeptId=" + Shell.util.Cookie.getCookie('HRDeptID')
            url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetATEmpAttendanceEventApprove?EmpId=" + Shell.util.Cookie.getCookie('EmployeeID')
        }, function (data) {
            ShellComponent.mask.hide();
            if (data.success) {
                //alert(data.ResultDataValue);
                var jsona = $.parseJSON(data.ResultDataValue);
                $('#approve').val(jsona.CName);
                $('#approveid').val(jsona.Id);
            } else {
                //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                ATCommon.info.error(data.msg);
            }
        });
    }
};