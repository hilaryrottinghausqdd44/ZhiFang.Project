var TaskCommon = TaskCommon || {
    TaskStatusType: {
        TmpApply: "5588205522535239744",
        Applyed: "4649883000533627142",

        OneAuditing: "5097561283722347016",
        OneAudited: "5682026874588216259",
        OneAuditBack: "5150044114524428979",

        TwoAuditing: "5221836557183432811",
        TwoAudited: "5434763342795916000",
        TwoAuditBack: "5324460690574315774",

        Publishing: "5181783138154880332",
        Published: "4967258396876635439",
        PublishBack: "5741016721777107562",

        Executing: "4621621720762238176",
        Executed: "5391772382920326538",
        ExecutStop: "5001555516032423353",

        Checking: "4890928177429564879",
        Checked: "5518558271903118484",
        CheckBack: "5490921315541028028",

        Stoped: "5484216973649314900"
    },
    TaskStatusName: {
        TmpApply: "暂存",
        Applyed: "申请",

        OneAuditing: "一审中",
        OneAudited: "一审通过",
        OneAuditBack: "一审退回",

        TwoAuditing: "二审中",
        TwoAudited: "二审通过",
        TwoAuditBack: "二审退回",

        Publishing: "分配中",
        Published: "分配完成",
        PublishBack: "分配退回",

        Executing: "执行中",
        Executed: "执行完成",
        ExecutStop: "不执行",

        Checking: "验收中",
        Checked: "已验收",
        CheckBack: "验收退回",

        Stoped: "已终止"
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
    },
    ApplyAction: function (id, btnid, callback,message) {
        alert('请登录PC端进行任务申请！');
        //ShellComponent.mask.submit();
        //Shell.util.Server.ajax({
        //    type: "Get",
        //    url: Shell.util.Path.rootPath + "/RBACService.svc/RBAC_UDTO_SearchHREmployeeById?isPlanish=true&id=" + Shell.util.Cookie.getCookie("EmployeeID")
        //}, function (data) {
        //    if (data.success) {
        //        var jsona = $.parseJSON(data.ResultDataValue);
        //        alert(jsona.HREmployee_ManagerID);
        //        alert(jsona.HREmployee_ManagerName);
        //        var data = {
        //                Id: id,
        //                Status: { Id: TaskCommon.TaskStatusType.Applyed, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
        //                StatusName:"申请",
        //                OneAuditID: jsona.HREmployee_ManagerID,
        //                OneAuditName: jsona.HREmployee_ManagerName
        //            };
        //        Shell.util.Server.ajax({
        //                type: "Post",
        //                data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,OneAuditID,OneAuditName,Id" }),
        //                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        //            }, function (data) {
        //                if (data.success) {
        //                    var data = {
        //                        PTaskID: id,
        //                        PTaskOperTypeID: TaskCommon.TaskStatusType.Applyed,
        //                        OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
        //                        OperaterName: Shell.util.Cookie.getCookie("EmployeeName")
        //                    };
        //                    Shell.util.Server.ajax({
        //                        type: "Post",
        //                        data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,OneAuditID,OneAuditName,Id" }),
        //                        url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
        //                    }, function (data) {
        //                        if (data.success) {
        //                            ShellComponent.mask.hide();
        //                            alert('申请成功！');
        //                            $("#BtnApply" + id).hide();
        //                        }
        //                        else {
        //                            alert(data.ErrorInfo);
        //                        }
        //                    });
        //                }
        //            });
        //    }
        //});
    },
    OneAuditAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.OneAuditing, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "一审中"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.OneAuditing,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo:message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('一审受理！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    OneAuditedAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.OneAudited, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "一审通过"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.OneAudited,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('一审通过！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    OneAuditBackAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.OneAuditBack, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "一审退回"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.OneAuditBack,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('一审退回！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    TwoAuditAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();

        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.TwoAuditing, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "二审中",
            TwoAuditID: Shell.util.Cookie.getCookie("EmployeeID"),
            TwoAuditName: Shell.util.Cookie.getCookie("EmployeeName")
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,TwoAuditID,TwoAuditName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.TwoAuditing,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('二审受理！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    TwoAuditedAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.TwoAudited, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "二审通过",
            TwoAuditID: Shell.util.Cookie.getCookie("EmployeeID"),
            TwoAuditName: Shell.util.Cookie.getCookie("EmployeeName")
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,TwoAuditID,TwoAuditName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.TwoAudited,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('二审通过！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    TwoAuditBackAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.TwoAuditBack, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "二审退回",
            TwoAuditID: Shell.util.Cookie.getCookie("EmployeeID"),
            TwoAuditName: Shell.util.Cookie.getCookie("EmployeeName")
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,TwoAuditID,TwoAuditName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.TwoAuditBack,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('二审退回！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    PublishAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.Publishing, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "分配中",
            PublisherID: Shell.util.Cookie.getCookie("EmployeeID"),
            PublisherName: Shell.util.Cookie.getCookie("EmployeeName")
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,PublisherID,PublisherName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.Publishing,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('开始分配！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    PublishedAction: function (id, btnid, callback, message,empid,empname) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.Published, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "分配完成",
            PublisherID: Shell.util.Cookie.getCookie("EmployeeID"),
            PublisherName: Shell.util.Cookie.getCookie("EmployeeName"),
            ExecutorID:empid,
            ExecutorName: empname
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,PublisherID,PublisherName,ExecutorID,ExecutorName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.Published,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('分配完成！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    PublishBackAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.PublishBack, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "分配退回",
            PublisherID: Shell.util.Cookie.getCookie("EmployeeID"),
            PublisherName: Shell.util.Cookie.getCookie("EmployeeName")
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,PublisherID,PublisherName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.PublishBack,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('分配退回！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    ExecutAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.Executing, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "执行中"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.Executing,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('确认执行！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    ExecutedAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.Executed, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "执行完成"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.Executed,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('执行完成！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    ExecutStopAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();
        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.ExecutStop, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "不执行"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.ExecutStop,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('不执行！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    CheckAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();

        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.Checking, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "验收中"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.Checking,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('开始检查！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    CheckedAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();

        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.Checked, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "已验收"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.Checked,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('已验收！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    },
    CheckBackAction: function (id, btnid, callback, message) {
        ShellComponent.mask.submit();

        var data = {
            Id: id,
            Status: { Id: TaskCommon.TaskStatusType.CheckBack, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
            StatusName: "验收退回"
        };
        Shell.util.Server.ajax({
            type: "Post",
            data: Shell.util.JSON.encode({ entity: data, fields: "Status_Id,StatusName,Id" }),
            url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField"
        }, function (data) {
            if (data.success) {
                var data = {
                    PTaskID: id,
                    PTaskOperTypeID: TaskCommon.TaskStatusType.CheckBack,
                    OperaterID: Shell.util.Cookie.getCookie("EmployeeID"),
                    OperaterName: Shell.util.Cookie.getCookie("EmployeeName"),
                    OperateMemo: message
                };
                Shell.util.Server.ajax({
                    type: "Post",
                    data: Shell.util.JSON.encode({ entity: data }),
                    url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog"
                }, function (data) {
                    if (data.success) {
                        ShellComponent.mask.hide();
                        alert('验收退回！');
                        if (btnid) {
                            $("#" + btnid).hide();
                        }
                        if (callback) {
                            callback();
                        }
                    }
                    else {
                        alert(data.ErrorInfo);
                    }
                });
            }
            else {
                alert(data.ErrorInfo);
            }
        });
    }
};