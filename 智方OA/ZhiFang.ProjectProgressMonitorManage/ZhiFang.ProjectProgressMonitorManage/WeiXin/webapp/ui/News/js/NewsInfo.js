$(function () {
    //页面所有功能对象
    var shell_win = {
        WorkLogType: '',
        WorkLogTypeUrl: '',
        ffileid: '',
        Closepage: function () {
            Shell.util.weixin.closeWindow();
        },
        AddInteraction: function () {
            location.href = "NewsInteractionList.html?ffileid=" + shell_win.ffileid;
        },
        InitHtml: function () {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/QMSService.svc/QMS_UDTO_SearchFFileById?isPlanish=true&isAddFFileReadingLog=1&isAddFFileOperation=0&id=" + shell_win.ffileid + "&fields=FFile_Title,FFile_Memo,FFile_BeginTime,FFile_EndTime,FFile_DataAddTime,FFile_PublisherDateTime"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var jsona = $.parseJSON(data.ResultDataValue);
                    if (jsona.FFile_Title) {
                        $('#newtitle').text(jsona.FFile_Title);
                    }
                    if (jsona.FFile_Memo) {
                        $('#newsmemo').html(jsona.FFile_Memo);
                    }
                    if (jsona.FFile_PublisherDateTime) {
                        $('#newspublisherdateTime').text(jsona.FFile_PublisherDateTime);
                    }
                    var begintime = "";
                    var endtime = "永久";
                    if (jsona.FFile_BeginTime) {
                        begintime = jsona.FFile_BeginTime;
                    }
                    if (jsona.FFile_EndTime) {
                        endtime = jsona.FFile_EndTime;
                    }
                    $('#newsvaliddateTime').text(begintime + "-" + endtime);
                    Shell.util.Server.ajax({
                        type: "get",
                        url: Shell.util.Path.rootPath + "/QMSService.svc/QMS_UDTO_SearchFFileAttachmentByHQL?isPlanish=false&fields=FFileAttachment_Id,FFileAttachment_FileName,FFileAttachment_FileSize,FFileAttachment_CreatorName,FFileAttachment_DataAddTime&where=ffileattachment.IsUse=1 and ffileattachment.FFile.Id=" + shell_win.ffileid
                    }, function (data) {
                        ShellComponent.mask.hide();
                        if (data.success) {
                            //alert(data.ResultDataValue);
                            var jsonobj = $.parseJSON(data.ResultDataValue);
                            //alert(jsonobj.count);
                            var htmlstr = "<table width=\"100%\">";
                            for (var j = 0; j < jsonobj.count; j++)
                            {
                                htmlstr += "<tr><td><a href=\"" + Shell.util.Path.rootPath + "/QMSService.svc/QMS_UDTO_FFileAttachmentDownLoadFiles?operateType=1&id=" + jsonobj.list[j].Id + "\">" + jsonobj.list[j].FileName + "   (" + Shell.util.Bytes.toSize(jsonobj.list[j].FileSize) + ")</a></td><td width=\"25%\">" + jsonobj.list[j].CreatorName + "</td></tr>";
                            }
                            htmlstr += "</table>";
                            $('#wlAttachment').html(htmlstr);
                        }
                    });

                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }
            });
        },
        /**初始化*/
        init: function () {
            //alert('aaa');
            var p = Shell.util.getRequestParams();
            //alert('aaa');
            if (p["ffileid"]) {
                shell_win.ffileid = p["ffileid"];
            }
            //Shell.util.weixin.init();
            shell_win.InitHtml();
            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Closepage);
            $("#AddInteraction").on(Shell.util.Event.touch, shell_win.AddInteraction);
        }
    };
    shell_win.init();
});
function ShowAttachment(id)
{

}