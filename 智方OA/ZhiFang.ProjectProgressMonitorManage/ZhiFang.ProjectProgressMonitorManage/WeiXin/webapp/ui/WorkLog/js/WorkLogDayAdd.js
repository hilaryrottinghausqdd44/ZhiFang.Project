$(function () {
    //页面所有功能对象
    var shell_win = {
        WorkLogType: null,
        GetPara: [],
        ImgServerIds: new Array(5),
        ImgIds: new Array(5),
        ImgphIndex: 0,
        Load: function () {
            if (shell_win.getpara["WorkLogId"]) {
                localStorage.clear();
                shell_win.LoadWorkLogInfoById(shell_win.getpara["WorkLogId"]);                
            }
            else {
                $('#todaycontentmemo').val(localStorage.getItem("todaycontentmemo"));
                $('#exportlevel').val(localStorage.getItem("exportlevel"));
                shell_win.ImgIds = localStorage.getItem("ImgIds").split(',');
                shell_win.ImgServerIds = localStorage.getItem("ImgServerIds").split(',');
                if (localStorage.getItem("CopyForEmpIdList")) {
                    shell_win.SetCopyForList(localStorage.getItem("CopyForEmpIdList"), localStorage.getItem("CopyForEmpNameList"));
                }
            }
        },
        LoadWorkLogInfoById: function (WorkLogId)
        {

        },        
        SaveWorkLog:function()
        {
            if (shell_win.WorkLogType == "day") {
                var data = {
                    ToDayContent: $('#todaycontentmemo').val(),
                    WorkLogExportLevel: $('#exportlevel').val(),
                    CopyForEmpIdList: [],
                    CopyForEmpNameList: []
                };
                if ($("#ids_div").text()) {
                    data.CopyForEmpIdList = $("#ids_div").text().split(',');
                    data.CopyForEmpNameList = $("#names_div").text().split(',');
                }
                ShellComponent.mask.save();
                //$.ajax({
                //    type: 'post',
                //    contentType: 'application/json',
                //    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkDayLogByWeiXin",
                //    data: JSON.stringify({ entity: data, AttachmentUrlList: null }),
                //    //filerValue:newValue.trim(),tablename:"TestItem"
                //    dataType: "json",
                //    success: function (data) {
                //        ShellComponent.mask.hide();
                //        if (data.success) {
                //            ShellComponent.messagebox.msg("新增成功！");
                //        }

                //    }
                //});


                Shell.util.Server.ajax({
                    type: "post",
                    data: JSON.stringify({ entity: data, AttachmentUrlList: shell_win.ImgServerIds }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkDayLogByWeiXin"
                }, function (data) {
                    ShellComponent.mask.hide();
                    if (data.success) {
                        ShellComponent.messagebox.msg("新增成功！");
                        shell_win.backpage();
                        //shell_win.LoadSignInfo();
                        //shell_win.patient.change_member_list(info, data.value);
                        //shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
                        //shell_win.patient.to_page();
                    } else {
                        ShellComponent.messagebox.error("content_config_patient_info_msg", data.ErrorInfo);
                    }
                });
            }
            if (shell_win.WorkLogType == "week") {
                var data = {
                    ToDayContent: $('#todaycontentmemo').val(),
                    WorkLogExportLevel: $('#exportlevel').val(),
                    CopyForEmpIdList: [],
                    CopyForEmpNameList: []
                };
                if ($("#ids_div").text()) {
                    data.CopyForEmpIdList = $("#ids_div").text().split(',');
                    data.CopyForEmpNameList = $("#names_div").text().split(',');
                }
                ShellComponent.mask.save();
                Shell.util.Server.ajax({
                    type: "post",
                    data: Shell.util.JSON.encode({ entity: data, AttachmentUrlList: shell_win.ImgServerIds }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkWeekLogByWeiXin"
                }, function (data) {
                    ShellComponent.mask.hide();
                    if (data.success) {
                        ShellComponent.messagebox.msg("新增成功！");
                        shell_win.backpage();
                        //shell_win.LoadSignInfo();
                        //shell_win.patient.change_member_list(info, data.value);
                        //shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
                        //shell_win.patient.to_page();
                    } else {
                        ShellComponent.messagebox.error("content_config_patient_info_msg", data.msg);
                    }
                });
            }
            if (shell_win.WorkLogType == "month") {
                var data = {
                    ToDayContent: $('#todaycontentmemo').val(),
                    WorkLogExportLevel: $('#exportlevel').val(),
                    CopyForEmpIdList: [],
                    CopyForEmpNameList: []
                };
                if ($("#ids_div").text()) {
                    data.CopyForEmpIdList = $("#ids_div").text().split(',');
                    data.CopyForEmpNameList = $("#names_div").text().split(',');
                }
                ShellComponent.mask.save();
                Shell.util.Server.ajax({
                    type: "post",
                    data: Shell.util.JSON.encode({ entity: data, AttachmentUrlList: shell_win.ImgServerIds }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkMonthLogByWeiXin"
                }, function (data) {

                    ShellComponent.mask.hide();
                    if (data.success) {
                        ShellComponent.messagebox.msg("新增成功！");
                        shell_win.backpage();
                        //shell_win.LoadSignInfo();
                        //shell_win.patient.change_member_list(info, data.value);
                        //shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
                        //shell_win.patient.to_page();
                    } else {
                        ShellComponent.messagebox.error("content_config_patient_info_msg", data.msg);
                    }
                });
            }
        },
        getchooseImagecallback: function (localIds) {          
            //alert(shell_win.ImgphIndex);
            var tmphtml = "";
            if (localIds.length > 0)
            {
                shell_win.ImgIds[shell_win.ImgphIndex-1] = localIds[0];
                Shell.util.weixin.uploadImage(localIds[0], shell_win.ImgphIndex,shell_win.uploadImagecallback);
            }
        },
        uploadImagecallback: function (serverId, index) {
            alert("serverId=" + serverId + "@@@index" + index);
            shell_win.ImgServerIds[index - 1] = serverId;
            $("#imgpg" + shell_win.ImgphIndex).css("display", "none");
            $("#imgshow" + shell_win.ImgphIndex).attr('src', shell_win.ImgIds[index - 1]);
            $("#imgshow" + shell_win.ImgphIndex).css("display", "block");
        },
        ChooseCopyForuserShow: function () {
            localStorage.setItem("WorkLogTypeListChoosebackpageurl", location.href);
            localStorage.setItem("todaycontentmemo", $('#todaycontentmemo').val());
            localStorage.setItem("exportlevel", $('#exportlevel').val());
            localStorage.setItem("ImgIds", shell_win.ImgIds);
            localStorage.setItem("ImgServerIds", shell_win.ImgServerIds);
            //if ($("#ids_div").text()) {
            //    localStorage.setItem("CopyForEmpIdList", $("#ids_div").text().split(','));
            //    localStorage.setItem("CopyForEmpNameList", $("#names_div").text().split(','));
            //}
            //else {
            //    localStorage.setItem("CopyForEmpIdList", []);
            //    localStorage.setItem("CopyForEmpNameList", []);
            //}
            var url = "../Common/ChooseEmp.aspx?";
            location.href = url;
        },
        Backpage: function ()
        {
            shell_win.RemoveLocalStorage();
            location.href = "WorkLogTypeListChoose.html";
        },
        SetCopyForList: function (ids, names) {
            $("#ids_div").text(ids);
            $("#names_div").text(names);
        },
        initImgpg: function () {
            $("#photographtd").on(Shell.util.Event.touch, function () {
                Shell.util.weixin.getchooseImage(shell_win.getchooseImagecallback);
            });

            $("#imgpg1").on(Shell.util.Event.touch, function () {
                shell_win.ImgphIndex = 1;
                //alert('photographtd');
                Shell.util.weixin.getchooseImage(shell_win.getchooseImagecallback);
            });
            $("#imgpg2").on(Shell.util.Event.touch, function () {
                shell_win.ImgphIndex = 2;
                //alert('photographtd');
                Shell.util.weixin.getchooseImage(shell_win.getchooseImagecallback);
            });
            $("#imgpg3").on(Shell.util.Event.touch, function () {
                shell_win.ImgphIndex = 3;
                //alert('photographtd');
                Shell.util.weixin.getchooseImage(shell_win.getchooseImagecallback);
            });
            $("#imgpg4").on(Shell.util.Event.touch, function () {
                shell_win.ImgphIndex = 4;
                //alert('photographtd');
                Shell.util.weixin.getchooseImage(shell_win.getchooseImagecallback);
            });
            $("#imgpg5").on(Shell.util.Event.touch, function () {
                shell_win.ImgphIndex = 5;
                //alert('photographtd');
                Shell.util.weixin.getchooseImage(shell_win.getchooseImagecallback);
            });

            $("#imgshow1").on('click', function () {
                Shell.util.weixin.previewImage($("#imgshow1").attr("src"), [$("#imgshow1").attr("src")]);
                //shell_win.imgshow($("#imgshow1").attr("src"));
            });
            $("#imgshow2").on('click', function () {
                Shell.util.weixin.previewImage($("#imgshow2").attr("src"), [$("#imgshow2").attr("src")]);
            });
            $("#imgshow3").on('click', function () {
                Shell.util.weixin.previewImage($("#imgshow3").attr("src"), [$("#imgshow3").attr("src")]);
            });
            $("#imgshow4").on('click', function () {
                Shell.util.weixin.previewImage($("#imgshow4").attr("src"), [$("#imgshow4").attr("src")]);
            });
            $("#imgshow5").on('click', function () {
                Shell.util.weixin.previewImage($("#imgshow5").attr("src"), [$("#imgshow5").attr("src")]);
            });
        },
        RemoveLocalStorage:function()
        {
            localStorage.removeItem("WorkLogTypeListChoosebackpageurl");
            localStorage.removeItem("todaycontentmemo");
            localStorage.removeItem("exportlevel");
            localStorage.removeItem("CopyForEmpIdList");
            localStorage.removeItem("CopyForEmpNameList");
            localStorage.removeItem("ImgIds");
            localStorage.removeItem("ImgServerIds");
        },
        /**初始化*/
        init: function () {
            //Shell.util.weixin.init();
            shell_win.initImgpg();
            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Backpage);
            $("#submitspan").on(Shell.util.Event.touch, shell_win.SaveWorkLog);

            $("#choosecopyforuserbtn").on('click', shell_win.ChooseCopyForuserShow);

            shell_win.getpara = Shell.util.getRequestParams();
            //alert(shell_win.getpara["WorkLogType"]);
            if(shell_win.getpara["WorkLogType"])
            {
                shell_win.WorkLogType = shell_win.getpara["WorkLogType"]
                //alert(shell_win.getpara["WorkLogType"]);
                if (shell_win.getpara["WorkLogType"] == "day") {
                    $("#panel-head").text("新增日报");
                    $("title").text("新增日报");
                }
                if (shell_win.getpara["WorkLogType"] == "week") {
                    $("#panel-head").text("新增周报");
                    $("title").text("新增周报");
                }
                if (shell_win.getpara["WorkLogType"] == "month") {
                    $("#panel-head").text("新增月报");
                    $("title").text("新增月报");
                }
            }
            if (localStorage.getItem("IsRefers") && localStorage.getItem("IsRefers") == "true") {
                shell_win.RemoveLocalStorage();
                localStorage.removeItem("IsRefers");
            }            
            shell_win.Load();
        }
    };
    shell_win.init();
});