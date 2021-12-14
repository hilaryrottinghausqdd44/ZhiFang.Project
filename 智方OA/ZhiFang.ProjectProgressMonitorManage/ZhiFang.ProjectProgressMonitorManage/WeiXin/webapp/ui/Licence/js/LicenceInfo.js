$(function () {
    //页面所有功能对象
    var shell_win = {
        ServerLicenceid: "",
        ExportType: "",
        ActionType: "",
        fields: "AHServerLicence_PClientName,AHServerLicence_LRNo,AHServerLicence_LRNo1,AHServerLicence_LRNo2,AHServerLicence_ApplyName,AHServerLicence_OneAuditName,AHServerLicence_TwoAuditName",
        /*初始化*/
        Load: function () {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicenceById?id=" + shell_win.ServerLicenceid + "&fields=" + shell_win.fields
            }, function (data) {
                //alert('A');
                ShellComponent.mask.hide();
                if (data.success) {
                    alert(data.ResultDataValue);
                    var jsona = $.parseJSON(data.ResultDataValue);
                    var CName = jsona.PClientName ? jsona.PClientName : "";
                    $('#PClientName').text(CName);
                    var LRNo = jsona.LRNo ? jsona.LRNo : "";
                    $('#LRNo').val(LRNo);
                    var LRNo1 = jsona.LRNo1 ? jsona.LRNo1 : "";
                    $('#LRNo1').val(LRNo1);
                    var LRNo2 = jsona.LRNo2 ? jsona.LRNo2 : "";
                    $('#LRNo2').val(LRNo2);
                    var ApplyName = jsona.ApplyName ? jsona.ApplyName : "";
                    $('#ApplyName').val(ApplyName);
                    var OneAuditName = jsona.OneAuditName ? jsona.OneAuditName : "";
                    $('#OneAuditName').val(OneAuditName);
                    var TwoAuditName = jsona.TwoAuditName ? jsona.TwoAuditName : "";
                    $('#TwoAuditName').val(TwoAuditName);

                    var htmlstr = "<table width=\"100%\">";
                    //for (var j = 0; j < jsonobj.count; j++) {
                        htmlstr += "<tr><td><a href=\"" + Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_DownLoadAHServerLicenceFile?operateType=0&id=" + shell_win.ServerLicenceid + "\">授权文件下载   </a></td></tr>";
                    //}
                    htmlstr += "</table>";
                    $('#LiceseFile').html(htmlstr);
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }
            });
        },
        /**初始化*/
        init: function () {
            alert('aaa');
            //ATCommon.info.show('aaaaaaaaa');
            //$('#aaa').val('aaa');
            var p = Shell.util.getRequestParams();
            //alert("id=" + p["id"] + ";ExportType=" + p["ExportType"]);
            if (p["id"]) {
                shell_win.ServerLicenceid = p["id"];
            }
            shell_win.Load();
        }
    };
    shell_win.init();
});