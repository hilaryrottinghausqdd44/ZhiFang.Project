$(function () {
    //页面所有功能对象
    var shell_win = {
        WorkLogType: '',
        WorkLogTypeUrl: '',
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
            if (p["WorkLogType"]) {
                shell_win.WorkLogType = p["WorkLogType"];
                if (shell_win.WorkLogType == "WorkLogDay") {
                    shell_win.WorkLogTypeUrl = "WorkLogDay.html";
                    $("#all_p").text("全部日计划/总结");
                    $("#meown_p").text("我的日计划/总结");
                    $("#sendforme_p").text("下属日计划/总结");
                    $("#copyforme_p").text("@我的日计划/总结");
                }
                if (shell_win.WorkLogType == "WorkLogWeek") {
                    shell_win.WorkLogTypeUrl = "WorkLogWeek.html";
                    $("#all_p").text("全部周计划/总结");
                    $("#meown_p").text("我的周计划/总结");
                    $("#sendforme_p").text("下属周计划/总结");
                    $("#copyforme_p").text("@我的周计划/总结");
                }
                if (shell_win.WorkLogType == "WorkLogMonth") {
                    shell_win.WorkLogTypeUrl = "WorkLogMonth.html";
                    $("#all_p").text("全部月计划/总结");
                    $("#meown_p").text("我的月计划/总结");
                    $("#sendforme_p").text("下属月计划/总结");
                    $("#copyforme_p").text("@我的月计划/总结");
                }
            }
            Shell.util.Event.initTouch($("#all_a")[0]);
            $("#all_a").on(Shell.util.Event.touch, function () {
                if ($("#all_a")[0].isClick()) {
                    //alert(shell_win.WorkLogTypeUrl+"?WorkLogType=" + shell_win.WorkLogType + "&SendType=all");
                    location.href = shell_win.WorkLogTypeUrl+"?WorkLogType=" + shell_win.WorkLogType + "&SendType=all";
                }
            });

            Shell.util.Event.initTouch($("#meown_a")[0]);
            $("#meown_a").on(Shell.util.Event.touch, function () {
                if ($("#meown_a")[0].isClick()) {
                    location.href = shell_win.WorkLogTypeUrl + "?WorkLogType=" + shell_win.WorkLogType + "&SendType=meown";
                }
            });            

            Shell.util.Event.initTouch($("#sendforme_a")[0]);
            $("#sendforme_a").on(Shell.util.Event.touch, function () {
                if ($("#sendforme_a")[0].isClick()) {
                    location.href = shell_win.WorkLogTypeUrl + "?WorkLogType=" + shell_win.WorkLogType + "&SendType=sendforme";
                }
            });

            Shell.util.Event.initTouch($("#copyforme_a")[0]);
            $("#copyforme_a").on(Shell.util.Event.touch, function () {
                if ($("#copyforme_a")[0].isClick()) {
                    location.href = shell_win.WorkLogTypeUrl + "?WorkLogType=" + shell_win.WorkLogType + "&SendType=copyforme";
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