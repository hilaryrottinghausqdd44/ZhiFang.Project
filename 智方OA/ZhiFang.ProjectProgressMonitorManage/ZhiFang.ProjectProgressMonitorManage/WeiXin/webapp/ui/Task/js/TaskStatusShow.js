$(function () {
    //页面所有功能对象
    var shell_win = {
        ExportType: 1,
        TaskStatusType: TaskCommon.TmpApply,//"4621621720762238176,4967258396876635439",
        Backpage: function () {
            if (shell_win.backpageurl) {
                location.href = shell_win.backpageurl;
            }
            else {
                history.go(-1);
            }
        },
        /**初始化*/
        init: function () {
            //alert('aaa');
            var p = Shell.util.getRequestParams();
            //alert("ExportType=" + p["ExportType"] + "@TaskStatusType=" + p["TaskStatusType"]);
            //alert('aaa');
            var TaskStatusTypeNo = TaskCommon.TaskStatusType.Checked + "," + TaskCommon.TaskStatusType.Stoped;
            if (p["ExportType"]) {
                shell_win.ExportType = p["ExportType"];
                if (shell_win.ExportType == 1)
                {                   
                    $("#applytitle").hide();
                    $("#applycells").hide();
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TmpApply;
                    //TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Applyed;
                }
                if (shell_win.ExportType == 2) {
                    $("#applytitle").hide();
                    $("#applycells").hide();
                    $("#oneaudittitle").hide();
                    $("#oneauditcells").hide();
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TmpApply;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Applyed;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditBack;
                }
                if (shell_win.ExportType == 3) {
                    $("#applytitle").hide();
                    $("#applycells").hide();
                    $("#oneaudittitle").hide();
                    $("#oneauditcells").hide();
                    $("#twoaudittitle").hide();
                    $("#twoauditcells").hide();
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TmpApply;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Applyed;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditBack;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAudited;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAuditing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAuditBack;
                }
                if (shell_win.ExportType == 4) {
                    $("#applytitle").hide();
                    $("#applycells").hide();
                    $("#oneaudittitle").hide();
                    $("#oneauditcells").hide();
                    $("#twoaudittitle").hide();
                    $("#twoauditcells").hide();
                    $("#publishtitle").hide();
                    $("#publishcells").hide();
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TmpApply;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Applyed;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditBack;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAudited;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAuditing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAuditBack;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAudited;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Publishing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.PublishBack;
                }
                if (shell_win.ExportType == 5) {
                    $("#applytitle").hide();
                    $("#applycells").hide();
                    $("#oneaudittitle").hide();
                    $("#oneauditcells").hide();
                    $("#twoaudittitle").hide();
                    $("#twoauditcells").hide();
                    $("#publishtitle").hide();
                    $("#publishcells").hide();
                    $("#executtitle").hide();
                    $("#executcells").hide();
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TmpApply;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Applyed;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAuditBack;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.OneAudited;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAuditing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAuditBack;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.TwoAudited;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Publishing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.PublishBack;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Published;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.Executing;
                    TaskStatusTypeNo += "," + TaskCommon.TaskStatusType.ExecutStop;
                }
                if (shell_win.ExportType == 6) //任务终止
                {
                   
                }
            }
            Shell.util.Event.initTouch($("#all_a")[0]);
            $("#all_a").on(Shell.util.Event.touch, function () {
                if ($("#all_a")[0].isClick()) {
                    //alert("TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusTypeNo=" + TaskStatusTypeNo);
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusTypeNo=" + TaskStatusTypeNo;
                }
            });

            Shell.util.Event.initTouch($("#apply_a")[0]);
            $("#apply_a").on(Shell.util.Event.touch, function () {
                if ($("#apply_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.TmpApply + "," + TaskCommon.TaskStatusType.OneAuditBack;
                }
            });

            Shell.util.Event.initTouch($("#oneaudit_a")[0]);
            $("#oneaudit_a").on(Shell.util.Event.touch, function () {
                if ($("#oneaudit_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Applyed + "," + TaskCommon.TaskStatusType.OneAuditing + "," + TaskCommon.TaskStatusType.TwoAuditBack;
                }
            });

            Shell.util.Event.initTouch($("#twoaudit_a")[0]);
            $("#twoaudit_a").on(Shell.util.Event.touch, function () {
                if ($("#twoaudit_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.OneAudited + "," + TaskCommon.TaskStatusType.TwoAuditing + "," + TaskCommon.TaskStatusType.PublishBack;
                }
            });

            Shell.util.Event.initTouch($("#publish_a")[0]);
            $("#publish_a").on(Shell.util.Event.touch, function () {
                if ($("#publish_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.TwoAudited + "," + TaskCommon.TaskStatusType.Publishing + "," + TaskCommon.TaskStatusType.ExecutStop;
                }
            });

            Shell.util.Event.initTouch($("#execut_a")[0]);
            $("#execut_a").on(Shell.util.Event.touch, function () {
                if ($("#execut_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Published + "," + TaskCommon.TaskStatusType.Executing + "," + TaskCommon.TaskStatusType.CheckBack;
                }
            });

            Shell.util.Event.initTouch($("#check_a")[0]);
            $("#check_a").on(Shell.util.Event.touch, function () {
                if ($("#check_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Executed + "," + TaskCommon.TaskStatusType.Checking ;
                }
            });

            Shell.util.Event.initTouch($("#tmpapply_a")[0]);
            $("#tmpapply_a").on(Shell.util.Event.touch, function () {
                if ($("#tmpapply_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.TmpApply;
                }
            });

            Shell.util.Event.initTouch($("#applyed_a")[0]);
            $("#applyed_a").on(Shell.util.Event.touch, function () {
                if ($("#applyed_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Applyed;
                }
            });

            Shell.util.Event.initTouch($("#oneauditback_a")[0]);
            $("#oneauditback_a").on(Shell.util.Event.touch, function () {
                if ($("#oneauditback_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.OneAuditBack;
                }
            });

            Shell.util.Event.initTouch($("#oneauditing_a")[0]);
            $("#oneauditing_a").on(Shell.util.Event.touch, function () {
                if ($("#oneauditing_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.OneAuditing;
                }
            });

            Shell.util.Event.initTouch($("#oneaudited_a")[0]);
            $("#oneaudited_a").on(Shell.util.Event.touch, function () {
                if ($("#oneaudited_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.OneAudited;
                }
            });

            Shell.util.Event.initTouch($("#twoauditback_a")[0]);
            $("#twoauditback_a").on(Shell.util.Event.touch, function () {
                if ($("#twoauditback_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.TwoAuditBack;
                }
            });

            Shell.util.Event.initTouch($("#twoauditing_a")[0]);
            $("#twoauditing_a").on(Shell.util.Event.touch, function () {
                if ($("#twoauditing_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.TwoAuditing;
                }
            });

            Shell.util.Event.initTouch($("#twoaudited_a")[0]);
            $("#twoaudited_a").on(Shell.util.Event.touch, function () {
                if ($("#twoaudited_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.TwoAudited;
                }
            });

            Shell.util.Event.initTouch($("#publishback_a")[0]);
            $("#publishback_a").on(Shell.util.Event.touch, function () {
                if ($("#publishback_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.PublishBack;
                }
            });

            Shell.util.Event.initTouch($("#publishing_a")[0]);
            $("#publishing_a").on(Shell.util.Event.touch, function () {
                if ($("#publishing_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Publishing;
                }
            });

            Shell.util.Event.initTouch($("#published_a")[0]);
            $("#published_a").on(Shell.util.Event.touch, function () {
                if ($("#published_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Published;
                }
            });

            Shell.util.Event.initTouch($("#executstop_a")[0]);
            $("#executstop_a").on(Shell.util.Event.touch, function () {
                if ($("#executstop_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.ExecutStop;
                }
            });

            Shell.util.Event.initTouch($("#executing_a")[0]);
            $("#executing_a").on(Shell.util.Event.touch, function () {
                if ($("#executing_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Executing;
                }
            });

            Shell.util.Event.initTouch($("#executed_a")[0]);
            $("#executed_a").on(Shell.util.Event.touch, function () {
                if ($("#executed_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Executed;
                }
            });

            Shell.util.Event.initTouch($("#checkback_a")[0]);
            $("#checkback_a").on(Shell.util.Event.touch, function () {
                if ($("#checkback_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.CheckBack;
                }
            });

            Shell.util.Event.initTouch($("#checking_a")[0]);
            $("#checking_a").on(Shell.util.Event.touch, function () {
                if ($("#checking_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Checking;
                }
            });

            Shell.util.Event.initTouch($("#checked_a")[0]);
            $("#checked_a").on(Shell.util.Event.touch, function () {
                if ($("#checked_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Checked;
                }
            });

            Shell.util.Event.initTouch($("#stoped_a")[0]);
            $("#stoped_a").on(Shell.util.Event.touch, function () {
                if ($("#stoped_a")[0].isClick()) {
                    location.href = "TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusType=" + TaskCommon.TaskStatusType.Stoped;
                }
            });

            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Backpage);
        }
    };
    shell_win.init();
});
function GetTaskInfo(id, name) {
    //alert(1);
    parent.parent.location.href = "Task.html?id=" + id + "&name=" + name;
}
function AddWorkTaskLog(id) {
    //alert(2);
    parent.parent.location.href = "../Worklog/TaskLog.html?id=" + id;
}