$(function () {
    //页面所有功能对象
    var shell_win = {
        tmpdate: "",
        pageindex: 1,
        limit: 5,
        ExportType: 1,
        CustomerServiceStatusType: "",
        CustomerServiceStatusTypeNo: "",
        Role: "",
        Refresh: function () {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.querySelector("#wrapper ul");
                //这里写你的刷新代码
                document.getElementById("wrapper").querySelector(".pullDownIcon").style.display = "none";
                document.getElementById("wrapper").querySelector(".pullDownLabel").innerHTML = "<img style='width:32px' src='../img/accept.png'/>刷新成功";
                setTimeout(function () {
                    wrapper.refresh();
                    document.getElementById("wrapper").querySelector(".pullDownLabel").innerHTML = "";
                }, 1000);//模拟qq下拉刷新显示成功效果
                /****remember to refresh after  action completed！ ---yourId.refresh(); ----| ****/
            }, 1000);
        },
        Load: function () {
            var where = " pcustomerservice.IsUse=1 ";
            var fields = "PCustomerService_Id,PCustomerService_ClientName,PCustomerService_ProvinceName,PCustomerService_IsProxy,PCustomerService_RequestMan,PCustomerService_RequestManPhone,PCustomerService_ProblemMemo,PCustomerService_ServiceAcceptanceMan,PCustomerService_ServiceAcceptanceDateString";
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceByHQL?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&where=" + where + "&fields=" + fields + "&sort=[{\"property\":\"PCustomerService_DataAddTime\",\"direction\":\"DESC\"}]"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    for (var i = 0; i < jsona.list.length; i++) {
                        //alert(jsona.list[i].ProblemMemo);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var ClientNamehtml = jsona.list[i].ClientName ? jsona.list[i].ClientName : "";
                        var ProvinceNamehtml = jsona.list[i].ProvinceName ? jsona.list[i].ProvinceName : "";
                        var IsProxyhtml = jsona.list[i].IsProxy ? "是" : "否";
                        var RequestManhtml = jsona.list[i].RequestMan ? jsona.list[i].RequestMan : "";
                        var RequestManPhonehtml = jsona.list[i].RequestManPhone ? jsona.list[i].RequestManPhone : "";
                        var ProblemMemohtml = jsona.list[i].ProblemMemo ? jsona.list[i].ProblemMemo : "";
                        var ServiceAcceptanceManhtml = jsona.list[i].ServiceAcceptanceMan ? jsona.list[i].ServiceAcceptanceMan : "";
                        var ServiceAcceptanceDatehtml = jsona.list[i].ServiceAcceptanceDateString ? jsona.list[i].ServiceAcceptanceDateString : "";

                        tmphtml += "<table id=\"CustomerServiceInfoTable\" style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td width=\"80\">用户：</td><td>" + ClientNamehtml + "</td></tr><tr style='margin:2px;height:29px'><td colspan=\"2\"><span class=\"text-danger\" style=\"padding-left:0px;float:left\">省份：" + ProvinceNamehtml + "</span><span class=\"text-danger\" style=\"padding-right:10px;float:right\"> 代理：" + IsProxyhtml + "</span></td></tr><tr style='margin:2px;height:29px'><td >请求人：</td><td><span class=\"text-primary\" style=\"padding-left:5px;float:left\">" + RequestManhtml + "</span></td></tr><tr style='margin:2px;height:29px'><td >联系电话：</td><td><span class=\"text-danger\" style=\"padding-left:5px;float:left\">" + RequestManPhonehtml + "</span></td></tr><tr style='margin:2px;height:29px'><td>问题描述：</td><td><span class=\"text-danger\" style=\"padding-left:5px;float:left\">" + ProblemMemohtml + "</span></td></tr><tr style='margin:2px;height:29px'><td>受理人：</td><td align=\"left\"><span style=\"padding-left:5px;float:left;\">" + ServiceAcceptanceManhtml + "</span><span class=\"label-danger btn-large\" style=\"float:right;color:#ffffff\">受理时间：" + ServiceAcceptanceDatehtml + "</span></td></tr><tr style='margin:2px;height:29px'><td align=\"left\" colspan=\"2\"><button id=\"AddWorkLog\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"AddWorkTaskLog('123123123')\">查看</button></td></tr></table></div></div>";
                        //alert(tmphtml)
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                        //alert(tmphtml)
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.pageindex = shell_win.pageindex + 1;
                    wrapper.refresh();
                } else {

                    //var msg = '<b style="color:red;">' + Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType?limit=" + shell_win.limit + "&pageindex=" + shell_win.pageindex + "&sendtype=" + shell_win.sendtype + "&worklogtype=" + shell_win.worklogtype + '</b>';
                    //alert(Shell.util.Path.rootPath);
                    ShellComponent.messagebox.msg(data.ErrorInfo);
                }
            });
        },
        /*列表初始化*/
        LoadCustomerService: function () {
            var where = " pcustomerservice.IsUse=1 " ;
            var fields = "PCustomerService_Id,PCustomerService_ClientName,PCustomerService_ProvinceName,PCustomerService_IsProxy,PCustomerService_RequestMan,PCustomerService_RequestManPhone,PCustomerService_ProblemMemo,PCustomerService_ServiceAcceptanceMan,PCustomerService_ServiceAcceptanceDateString";
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceByHQL?limit=" + shell_win.limit + "&page=" + shell_win.pageindex + "&where=" + where + "&fields=" + fields + "&sort=[{\"property\":\"PCustomerService_DataAddTime\",\"direction\":\"DESC\"}]"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    var el, li, i;
                    el = document.querySelector("#wrapper ul");
                    var jsona = $.parseJSON(data.ResultDataValue);
                    //alert(jsona.list.length);
                    for (var i = 0; i < jsona.list.length; i++) {
                        //alert(jsona.list[i].ProblemMemo);
                        li = document.createElement('li');
                        tmphtml = " <div class='panel panel-default' style='margin:1px;'><div class='panel-body' style='padding:2px;'>";

                        var ClientNamehtml = jsona.list[i].ClientName ? jsona.list[i].ClientName : "";
                        var ProvinceNamehtml = jsona.list[i].ProvinceName ? jsona.list[i].ProvinceName : "";
                        var IsProxyhtml = jsona.list[i].IsProxy ? "是" : "否";
                        var RequestManhtml = jsona.list[i].RequestMan ? jsona.list[i].RequestMan : "";
                        var RequestManPhonehtml = jsona.list[i].RequestManPhone ? jsona.list[i].RequestManPhone : "";
                        var ProblemMemohtml = jsona.list[i].ProblemMemo ? jsona.list[i].ProblemMemo : "";
                        var ServiceAcceptanceManhtml = jsona.list[i].ServiceAcceptanceMan ? jsona.list[i].ServiceAcceptanceMan : "";
                        var ServiceAcceptanceDatehtml = jsona.list[i].ServiceAcceptanceDateString ? jsona.list[i].ServiceAcceptanceDateString : "";

                        tmphtml += "<table id=\"CustomerServiceInfoTable\" style='width:100%;border-bottom-style:solid; border-bottom-width:1px; border-color:darkgrey' border='0'><tr style='margin:2px;height:29px'><td width=\"80\">用户：</td><td>" + ClientNamehtml + "</td></tr><tr style='margin:2px;height:29px'><td colspan=\"2\"><span class=\"text-danger\" style=\"padding-left:0px;float:left\">省份：" + ProvinceNamehtml + "</span><span class=\"text-danger\" style=\"padding-right:10px;float:right\"> 代理：" + IsProxyhtml + "</span></td></tr><tr style='margin:2px;height:29px'><td >请求人：</td><td><span class=\"text-primary\" style=\"padding-left:5px;float:left\">" + RequestManhtml + "</span></td></tr><tr style='margin:2px;height:29px'><td >联系电话：</td><td><span class=\"text-danger\" style=\"padding-left:5px;float:left\">" + RequestManPhonehtml + "</span></td></tr><tr style='margin:2px;height:29px'><td>问题描述：</td><td><span class=\"text-danger\" style=\"padding-left:5px;float:left\">" + ProblemMemohtml + "</span></td></tr><tr style='margin:2px;height:29px'><td>受理人：</td><td align=\"left\"><span style=\"padding-left:5px;float:left;\">" + ServiceAcceptanceManhtml + "</span><span class=\"label-danger btn-large\" style=\"float:right;color:#ffffff\">受理时间：" + ServiceAcceptanceDatehtml + "</span></td></tr><tr style='margin:2px;height:29px'><td align=\"left\" colspan=\"2\"><button id=\"AddWorkLog\" type=\"button\" class=\"btn btn-primary\" style=\"float:right\" onclick=\"AddWorkTaskLog('123123123')\">查看</button></td></tr></table></div></div>";
                        //alert(tmphtml)
                        li.innerHTML = tmphtml;
                        el.appendChild(li, el.childNodes[0]);
                    }
                    shell_win.pageindex = shell_win.pageindex + 1;
                    wrapper.refresh();
                } else {

                    //var msg = '<b style="color:red;">' + Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType?limit=" + shell_win.limit + "&pageindex=" + shell_win.pageindex + "&sendtype=" + shell_win.sendtype + "&worklogtype=" + shell_win.worklogtype + '</b>';
                    //alert(Shell.util.Path.rootPath);
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
            //alert("ExportType=" + p["ExportType"] + "@CustomerServiceStatusType=" + p["CustomerServiceStatusType"]);
            if (p["ExportType"]) {
                shell_win.ExportType = p["ExportType"];
            }
            if (p["CustomerServiceStatusType"]) {
                shell_win.CustomerServiceStatusType = p["CustomerServiceStatusType"];
            }
            if (p["CustomerServiceStatusTypeNo"]) {
                shell_win.CustomerServiceStatusTypeNo = p["CustomerServiceStatusTypeNo"];
            }
            if (p["Role"]) {
                shell_win.Role = p["Role"];
            }

            refresher.init({
                id: "wrapper",
                pullDownAction: shell_win.Refresh,
                pullUpAction: shell_win.Load
            });
            shell_win.LoadCustomerService();
        }
    };
    shell_win.init();
});
function ShowCustomerServiceInfo
//function GetTaskInfo(id, name) {
//    //alert(1);
//    var p = Shell.util.getRequestParams();
//    parent.parent.location.href = "TaskInfo.html?id=" + id + "&name=" + name + "&ExportType=" + p["ExportType"] + "&CustomerServiceStatusType=" + p["CustomerServiceStatusType"] + "&CustomerServiceStatusTypeNo=" + p["CustomerServiceStatusTypeNo"] + "&Role=" + p["Role"];
//}
//function AddWorkTaskLog(id) {
//    //alert(2);
//    parent.parent.location.href = "../Worklog/TaskLog.html?id=" + id;
//}

