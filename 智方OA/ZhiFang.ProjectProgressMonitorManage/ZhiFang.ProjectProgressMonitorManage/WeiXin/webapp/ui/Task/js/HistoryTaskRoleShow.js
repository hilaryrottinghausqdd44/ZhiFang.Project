$(function () {
    //页面所有功能对象
    var shell_win = {
        ExportType: 1,
        TaskStatusType: TaskCommon.TaskStatusType.Checked + "," + TaskCommon.TaskStatusType.Stoped,//"4621621720762238176,4967258396876635439",
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
            
            Shell.util.Event.initTouch($("#all_a")[0]);
            $("#all_a").on(Shell.util.Event.touch, function () {
                if ($("#all_a")[0].isClick()) {
                    //alert("TaskList.html?ExportType=" + shell_win.ExportType + "&TaskStatusTypeNo=" + TaskStatusTypeNo);
                    location.href = "TaskList.html?Role=all&TaskStatusType=" + shell_win.TaskStatusType;
                }
            });

            Shell.util.Event.initTouch($("#apply_a")[0]);
            $("#apply_a").on(Shell.util.Event.touch, function () {
                if ($("#apply_a")[0].isClick()) {
                    location.href = "TaskList.html?Role=apply&TaskStatusType=" + shell_win.TaskStatusType;
                }
            });

            Shell.util.Event.initTouch($("#oneaudit_a")[0]);
            $("#oneaudit_a").on(Shell.util.Event.touch, function () {
                if ($("#oneaudit_a")[0].isClick()) {
                    location.href = "TaskList.html?Role=oneaudit&TaskStatusType=" + shell_win.TaskStatusType;
                }
            });

            Shell.util.Event.initTouch($("#twoaudit_a")[0]);
            $("#twoaudit_a").on(Shell.util.Event.touch, function () {
                if ($("#twoaudit_a")[0].isClick()) {
                    location.href = "TaskList.html?Role=twoaudit&TaskStatusType=" + shell_win.TaskStatusType;
                }
            });

            Shell.util.Event.initTouch($("#publish_a")[0]);
            $("#publish_a").on(Shell.util.Event.touch, function () {
                if ($("#publish_a")[0].isClick()) {
                    location.href = "TaskList.html?Role=publish&TaskStatusType=" + shell_win.TaskStatusType;
                }
            });

            Shell.util.Event.initTouch($("#execut_a")[0]);
            $("#execut_a").on(Shell.util.Event.touch, function () {
                if ($("#execut_a")[0].isClick()) {
                    location.href = "TaskList.html?Role=execut&TaskStatusType=" + shell_win.TaskStatusType;
                }
            });

            Shell.util.Event.initTouch($("#check_a")[0]);
            $("#check_a").on(Shell.util.Event.touch, function () {
                if ($("#check_a")[0].isClick()) {
                    location.href = "TaskList.html?Role=check&TaskStatusType=" + shell_win.TaskStatusType;
                }
            });

            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Backpage);
        }
    };
    shell_win.init();
});