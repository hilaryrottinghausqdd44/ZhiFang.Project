$(function () {
    //页面所有功能对象
    var shell_win = {
        taskid: "",
        ExportType: "",
        ActionType: "",
        fields: "PTask_CName,PTask_TypeName,PTask_Contents,PTask_PTaskCName,PTask_StatusName,PTask_Status_Id,PTask_PaceName,PTask_ReqEndTime,PTask_UrgencyName,PTask_TypeName,PTask_Memo,PTask_InteractionCount,PTask_OperLogCount,PTask_WorkLogCount,PTask_ApplyName,PTask_OneAuditName,PTask_TwoAuditName,PTask_PublisherName,PTask_ExecutorID,PTask_ExecutorName,PTask_CheckerName",
        AddOperLog: function () {
            $("#AddOperLog_div").modal('show');
        },
        AddOperLogAction: function () {
            var memo = $('#OperateMemo').val();
            //alert(shell_win.ActionType);
            if (shell_win.ActionType == 'OneAuditing')
                TaskCommon.OneAuditAction(shell_win.taskid, 'OneAuditing', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'OneAudited')
                TaskCommon.OneAuditedAction(shell_win.taskid, 'OneAudited', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'OneAuditBack')
                TaskCommon.OneAuditBackAction(shell_win.taskid, 'OneAuditBack', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'TwoAuditing')
                TaskCommon.TwoAuditAction(shell_win.taskid, 'TwoAuditing', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'TwoAudited')
                TaskCommon.TwoAuditedAction(shell_win.taskid, 'TwoAudited', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'TwoAuditBack')
                TaskCommon.TwoAuditBackAction(shell_win.taskid, 'TwoAuditBack', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'Publishing')
                TaskCommon.PublishAction(shell_win.taskid, 'Publishing', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'Publish') {
                if (Shell.util.Cookie.getCookie("ChooseEmpIdList") && Shell.util.Cookie.getCookie("ChooseEmpIdList") != "") {
                    TaskCommon.PublishedAction(shell_win.taskid, 'Publish', function () {
                        Shell.util.Cookie.delCookie("ChooseEmpIdList");
                        Shell.util.Cookie.delCookie("ChooseEmpNameList");
                        location.href = location.href;
                    }, memo, Shell.util.Cookie.getCookie("ChooseEmpIdList"), Shell.util.Cookie.getCookie("ChooseEmpNameList"));
                }
            }
            if (shell_win.ActionType == 'PublishBack')
                TaskCommon.PublishBackAction(shell_win.taskid, 'PublishBack', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'Executing')
                TaskCommon.ExecutAction(shell_win.taskid, 'Executing', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'Executed')
                TaskCommon.ExecutedAction(shell_win.taskid, 'Executed', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'ExecutStop')
                TaskCommon.ExecutStopAction(shell_win.taskid, 'ExecutStop', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'Checking')
                TaskCommon.CheckAction(shell_win.taskid, 'Checking', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'Checked')
                TaskCommon.CheckedAction(shell_win.taskid, 'Checked', function () { location.href = location.href; }, memo);
            if (shell_win.ActionType == 'CheckBack')
                TaskCommon.CheckBackAction(shell_win.taskid, 'CheckBack', function () { location.href = location.href; }, memo);
        },
        Backpage: function () {
            shell_win.RemoveLocalStorage();
            if (shell_win.backpageurl) {
                location.href = shell_win.backpageurl;
            }
            else {
                var p = Shell.util.getRequestParams();
                location.href = "TaskList.html?ExportType=" + p["ExportType"] + "&TaskStatusType=" + p["TaskStatusType"];
            }
        },
        ChooseEmpShow: function () {
            Shell.util.Cookie.setCookie("ChooseEmpbackpageurl", location.href + "&tmpflag=1");
            var url = "../Common/ChooseEmp_Single.aspx?";
            location.href = url;
        },
        RemoveLocalStorage: function () {
            Shell.util.Cookie.delCookie("ChooseEmpbackpageurl");
            Shell.util.Cookie.delCookie("ChooseEmpIdList");
            Shell.util.Cookie.delCookie("ChooseEmpNameList");
        },
        SetChooseEmpIdList: function (ids, names) {
            if (ids && ids != null) {
                $("#ids_div").text(ids);
            }
            if (names && name != null) {
                $("#names_div").text(names);
            }
        },
        /*初始化*/
        Load: function () {
            ShellComponent.mask.loading();
            if (Shell.util.Cookie.getCookie("ChooseEmpIdList")) {
                //alert(Shell.util.Cookie.getCookie("ChooseEmpIdList"));
                shell_win.SetChooseEmpIdList(Shell.util.Cookie.getCookie("ChooseEmpIdList"), Shell.util.Cookie.getCookie("ChooseEmpNameList"));
            }
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskById?id=" + shell_win.taskid + "&fields=" + shell_win.fields
            }, function (data) {
                //alert('A');
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var jsona = $.parseJSON(data.ResultDataValue);
                    var CName = jsona.CName ? jsona.CName : "";
                    $('#panel-head').text(CName);

                    var Contents = jsona.Contents ? jsona.Contents : "";
                    $("#Contents").val(Contents);

                    var PTaskCName = jsona.PTaskCName ? jsona.PTaskCName : "";
                    $("#PTaskCName").val(PTaskCName);

                    $("#PTaskCNametr").attr("style", "display:none")

                    var StatusName = jsona.StatusName ? jsona.StatusName : "";
                    $("#StatusName").text(StatusName);

                    var PaceName = jsona.PaceName ? jsona.PaceName : "0%";
                    $("#PaceName").text(PaceName);

                    var ReqEndTime = jsona.ReqEndTime ? jsona.ReqEndTime : "";
                    $("#ReqEndTime").text(Shell.util.Date.toString(ReqEndTime, true));

                    var UrgencyName = jsona.UrgencyName ? jsona.UrgencyName : "";
                    $("#UrgencyName").text(UrgencyName);

                    var TypeName = jsona.TypeName ? jsona.TypeName : "";
                    $("#TypeName").text(TypeName);

                    var ptemplist = "";
                    if (jsona.ApplyName) {
                        ptemplist += "<div  class='text-info' style='float:left'>申请人：" + jsona.ApplyName + "</div><br>"
                    }
                    if (jsona.OneAuditName) {
                        ptemplist += "<div  class='text-primary' style='float:left'>一审人：" + jsona.OneAuditName + "</div><br>"
                    }
                    if (jsona.TwoAuditName) {
                        ptemplist += "<div  class='text-warning' style='float:left'>二审人：" + jsona.TwoAuditName + "</div><br>"
                    }
                    if (jsona.PublisherName) {
                        ptemplist += "<div  class='text-success' style='float:left'>分配人：" + jsona.PublisherName + "</div><br>"
                    }
                    if (jsona.ExecutorName) {
                        ptemplist += "<div  class='text-info' style='float:left'>执行人：" + jsona.ExecutorName + "</div><br>"
                        //alert(jsona.ExecutorID);
                        if (!Shell.util.Cookie.getCookie("ChooseEmpIdList") || Shell.util.Cookie.getCookie("ChooseEmpIdList") == "") {
                            Shell.util.Cookie.setCookie("ChooseEmpIdList", jsona.ExecutorID);
                            Shell.util.Cookie.setCookie("ChooseEmpNameList", jsona.ExecutorName);
                        }
                        shell_win.SetChooseEmpIdList(Shell.util.Cookie.getCookie("ChooseEmpIdList"), Shell.util.Cookie.getCookie("ChooseEmpNameList"));
                    }
                    if (jsona.CheckerName) {
                        ptemplist += "<div  class='text-danger' style='float:left'>验收人：" + jsona.CheckerName + "</div><br>"
                    }

                    $("#ptEmpList").html(ptemplist);

                    var Memo = jsona.Memo ? jsona.Memo : "";
                    $("#Memo").val(Memo);
                    var InteractionCount = jsona.InteractionCount ? jsona.InteractionCount : "";
                    $("#Interaction").text("(" + $("#Interaction").text() + ":" + InteractionCount + ")");

                    var WorkLogCount = jsona.WorkLogCount ? jsona.WorkLogCount : "";
                    $("#Worklog").text("(" + $("#Worklog").text() + ":" + WorkLogCount + ")");

                    var OperLogCount = jsona.OperLogCount ? jsona.OperLogCount : "";
                    $("#OperationLog").text("(" + $("#OperationLog").text() + ":" + OperLogCount + ")");

                    Shell.util.Server.ajax({
                        type: "get",
                        url: Shell.util.Path.rootPath + "/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=false&fields=HREmployee_Id,HREmployee_CName&where=IsUse=1 order by CName asc&limit=10000&page=1"
                    }, function (data) {
                        if (data.success) {
                            //alert(data.ResultDataValue);
                            var jsonobj = $.parseJSON(data.ResultDataValue);
                            //alert(jsonobj.count);
                            var htmlstr = "";
                            for (var j = 0; j < jsonobj.count; j++) {
                                $('#ExecutorEmpList').append("<option value='" + jsonobj.list[j].Id + "'>" + jsonobj.list[j].CName + "</option>");
                            }
                        }
                    });
                    Shell.util.Server.ajax({
                        type: "get",
                        url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachmentByHQL?isPlanish=false&fields=PProjectAttachment_Id,PProjectAttachment_FileName,PProjectAttachment_NewFileName,PProjectAttachment_FileType,PProjectAttachment_FileExt,PProjectAttachment_CreatorID,PProjectAttachment_CreatorName,PProjectAttachment_DataAddTime,PProjectAttachment_FileSize&where=pprojectattachment.IsUse=1 and pprojectattachment.PTask.Id=" + shell_win.taskid
                    }, function (data) {
                        ShellComponent.mask.hide();
                        if (data.success) {
                            //alert(data.ResultDataValue);
                            var jsonobj = $.parseJSON(data.ResultDataValue);
                            //alert(jsonobj.count);
                            var htmlstr = "<table width=\"100%\">";
                            for (var j = 0; j < jsonobj.count; j++) {
                                htmlstr += "<tr><td><a href=\"" + Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/WM_UDTO_PProjectAttachmentDownLoadFiles?operateType=1&id=" + jsonobj.list[j].Id + "\">" + jsonobj.list[j].FileName + "   (" + Shell.util.Bytes.toSize(jsonobj.list[j].FileSize) + ")</a></td><td width=\"25%\">" + jsonobj.list[j].CreatorName + "</td></tr>";
                            }
                            htmlstr += "</table>";
                            $('#ptAttachment').html(htmlstr);
                        }
                    });
                    if (jsona.Status.Id) {
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.Applyed) {
                            if (shell_win.ExportType == 1) {
                                $("#OneAuditing").show();
                                $("#OneAudited").show();
                                $("#OneAuditBack").show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.OneAuditing || jsona.Status.Id == TaskCommon.TaskStatusType.TwoAuditBack) {
                            if (shell_win.ExportType == 1) {

                                $("#OneAudited").show();
                                $("#OneAuditBack").show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.OneAudited) {
                            if (shell_win.ExportType == 2) {
                                $("#TwoAuditing").show();
                                $("#TwoAudited").show();
                                $("#TwoAuditBack").show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.TwoAuditing || jsona.Status.Id == TaskCommon.TaskStatusType.PublishBack) {
                            if (shell_win.ExportType == 2) {
                                $("#TwoAudited").show();
                                $("#TwoAuditBack").show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.TwoAudited) {
                            if (shell_win.ExportType == 3) {
                                $("#Publishing").show();
                                $("#Publish").show();
                                $("#PublishBack").show();
                                $("#choosecopyforuserbtn").show();
                                $('#ExecutorEmpList').show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.Publishing || jsona.Status.Id == TaskCommon.TaskStatusType.ExecutStop) {
                            if (shell_win.ExportType == 3) {
                                $("#Publish").show();
                                $("#PublishBack").show();
                                $("#choosecopyforuserbtn").show();
                                $('#ExecutorEmpList').show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.Published) {
                            if (shell_win.ExportType == 4) {
                                $("#Executing").show();
                                $("#Executed").show();
                                $("#ExecutStop").show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.Executing || jsona.Status.Id == TaskCommon.TaskStatusType.CheckBack) {
                            if (shell_win.ExportType == 4) {
                                $("#Executed").show();
                                $("#ExecutStop").show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.Executed) {
                            if (shell_win.ExportType == 5) {
                                $("#Checking").show();
                                $("#Checked").show();
                                $("#CheckBack").show();
                            }
                        }
                        if (jsona.Status.Id == TaskCommon.TaskStatusType.Checking) {
                            if (shell_win.ExportType == 5) {
                                $("#Checked").show();
                                $("#CheckBack").show();
                            }
                        }
                    }
                    //alert(jsona.Status.Id);

                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }
            });
        },
        /**初始化*/
        init: function () {
            //alert('aaa');
            //ATCommon.info.show('aaaaaaaaa');
            //$('#aaa').val('aaa');
            var p = Shell.util.getRequestParams();
            //alert("id=" + p["id"] + ";ExportType=" + p["ExportType"]);
            if (p["id"]) {
                shell_win.taskid = p["id"];
            }
            if (p["name"]) {
                shell_win.taskname = p["name"];
                $('#panel-head').text(shell_win.taskname);
            }
            if (p["ExportType"]) {
                shell_win.ExportType = p["ExportType"];
            }
            if (p["IsSingle"]) {
                if (p["IsSingle"] == '1') {
                    $("#cancelspan").hide();
                }
            }
            if (!p["tmpflag"] || p["tmpflag"] != '1') {
                Shell.util.Cookie.delCookie("ChooseEmpIdList");
                Shell.util.Cookie.delCookie("ChooseEmpNameList");
            }

            $("#OneAuditing").hide();
            $("#OneAudited").hide();
            $("#OneAuditBack").hide();
            $("#TwoAuditing").hide();
            $("#TwoAudited").hide();
            $("#TwoAuditBack").hide();
            $("#Publishing").hide();
            $("#Publish").hide();
            $("#PublishBack").hide();
            $("#choosecopyforuserbtn").hide();
            $("#Executing").hide();
            $("#Executed").hide();
            $("#ExecutStop").hide();
            $("#Checking").hide();
            $("#Checked").hide();
            $("#CheckBack").hide();
            $('#ExecutorEmpList').hide();


            $("#OneAuditing").on('click', function () {
                shell_win.ActionType = 'OneAuditing';
                shell_win.AddOperLog();
            });
            $("#OneAudited").on('click', function () {
                shell_win.ActionType = 'OneAudited';
                shell_win.AddOperLog();
            });
            $("#OneAuditBack").on('click', function () {
                shell_win.ActionType = 'OneAuditBack';
                shell_win.AddOperLog();
            });

            $("#TwoAuditing").on('click', function () {
                shell_win.ActionType = 'TwoAuditing';
                shell_win.AddOperLog();
            });
            $("#TwoAudited").on('click', function () {
                shell_win.ActionType = 'TwoAudited';
                shell_win.AddOperLog();
            });
            $("#TwoAuditBack").on('click', function () {
                shell_win.ActionType = 'TwoAuditBack';
                shell_win.AddOperLog();
            });

            $("#Publishing").on('click', function () {
                shell_win.ActionType = 'Publishing';
                shell_win.AddOperLog();
            });
            $("#Publish").on('click', function () {
                shell_win.ActionType = 'Publish';
                shell_win.AddOperLog();
            });
            $("#PublishBack").on('click', function () {
                shell_win.ActionType = 'PublishBack';
                shell_win.AddOperLog();
            });

            $("#Executing").on('click', function () {
                shell_win.ActionType = 'Executing';
                shell_win.AddOperLog();
            });
            $("#Executed").on('click', function () {
                shell_win.ActionType = 'Executed';
                shell_win.AddOperLog()
            });
            $("#ExecutStop").on('click', function () {
                shell_win.ActionType = 'ExecutStop';
                shell_win.AddOperLog()
            });

            $("#Checking").on('click', function () {
                shell_win.ActionType = 'Checking';
                shell_win.AddOperLog()
            });
            $("#Checked").on('click', function () {
                shell_win.ActionType = 'Checked';
                shell_win.AddOperLog()
            });
            $("#CheckBack").on('click', function () {
                shell_win.ActionType = 'CheckBack';
                shell_win.AddOperLog()
            });
            $("#AddOperLog_action_pass_btn").on('click', function () { shell_win.AddOperLogAction(); });
            $("#AddOperLog_action_rebut_btn").on('click', function () { $("#AddOperLog_div").modal('hide'); });
            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Backpage);
            $("#Worklog").on('click', function () { location.href = "TaskWorkLog.html?id=" + p["id"]; });
            $("#OperationLog").on('click', function () { location.href = "TaskOperationLog.html?id=" + p["id"]; });
            $("#Interaction").on('click', function () { location.href = "TaskInteractionList.html?id=" + p["id"]; });
            $("#choosecopyforuserbtn").on('click', shell_win.ChooseEmpShow);

            shell_win.Load();
        }
    };
    shell_win.init();
});