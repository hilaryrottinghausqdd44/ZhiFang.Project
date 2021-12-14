var shell_win_all = null;
$(function () {    
    //页面所有功能对象
    var shell_win = {
        limit: 5,
        page: 1,
        count: 0,
        //读取新闻列表
        LoadNews:function()
        {
            ///QMSService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID?isPlanish=true&isSearchChildNode=true&fields=FFile_Title,FFile_PublisherName,FFile_PublisherDateTime,PTask_Id,FFile_IsTop&where=id=5569287534789329919^ffile.Type in(2) and ffile.Status=5 and ffile.IsUse=1 and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='2017-04-10') or (ffile.EndTime>='2017-04-10'))&_dc=1491815241597&page=1&start=0&limit=5&sort=[{"property"%3A"FFile_IsTop"%2C"direction"%3A"DESC"}%2C{"property"%3A"FFile_PublisherDateTime"%2C"direction"%3A"DESC"}%2C{"property"%3A"FFile_BDictTree_Id"%2C"direction"%3A"ASC"}%2C{"property"%3A"FFile_Title"%2C"direction"%3A"ASC"}]
            var ed = Shell.util.Date.toString(new Date().getTime(), true);
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/QMSService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID?isPlanish=false&isSearchChildNode=true&fields=FFile_Id,FFile_Title,FFile_PublisherName,FFile_PublisherDateTime,FFile_IsTop&where=id=5569287534789329919^ffile.Type in(2) and ffile.Status=5 and ffile.IsUse=1 and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + ed + "') or (ffile.EndTime>='" + ed + "'))&page=" + shell_win.page + "&start=0&limit=" + shell_win.limit + "&sort=[{'property':'FFile_IsTop','direction':'DESC'},{'property':'FFile_PublisherDateTime','direction':'DESC'},{'property':'FFile_BDictTree_Id','direction':'ASC'},{'property':'FFile_Title','direction':'ASC'}]"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    var jsona = $.parseJSON(data.ResultDataValue);
                    if(jsona)
                    {
                        shell_win.count=jsona.count;
                    }
                    //alert(data.ResultDataValue);
                    //alert(jsona.list.length);
                    var tmphtml=""
                    for (var i = 0; i < 5; i++) {
                        if (jsona.list[i]) {
                            var tmptitle = jsona.list[i].Title;
                            if (tmptitle.length > 15)
                            {
                                tmptitle = tmptitle.substr(0, 14) + ".";
                            }
                            tmphtml += "<tr onclick=\"ShowNews('" + jsona.list[i].Id + "')\" ><td height='20px'>" + (i + 1) + "." + tmptitle + "</td><td class='text-right'>" + Shell.util.Date.toString(Shell.util.Date.getDate(jsona.list[i].PublisherDateTime), true) + "</td></tr>";
                        }
                        else {
                            tmphtml += "<tr><td height='20px'></td><td class='text-right'></td></tr>";
                        }
                    }
                    tmphtml = "<table width='100%' style='font-weight:bold;'>" + tmphtml + "</table>";
                    $("#newstd").html(tmphtml);
                } else {
                    var msg = '<b style="color:red;">' + "读取新闻错误！" + '</b>';
                    ShellComponent.messagebox.msg(msg);
                }
            });
        },
        //上一页
        PrePage:function()
        {
            if (shell_win.page <= 1) {
                shell_win.page = 1;
            }
            else {
                shell_win.page--;
            }
            shell_win.LoadNews();
        },
        //下一页
        NextPage:function()
        {
            if (shell_win.count>shell_win.page * shell_win.limit) {
                shell_win.page++;
            }
            shell_win.LoadNews();
        },
        openeven:'click',
        /**初始化*/
        init: function () {
            shell_win.LoadNews();
            //系统初始化
            $("#attd").on(shell_win.openeven, function () {
                //alert($(this).attr('data-url'));
                location.href = $(this).attr('data-url');
            });
            $("#tasktd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#receicvetasktd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#applytasktd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#oneaudittasktd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#twoaudittasktd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#publishtasktd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#checktasktd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#epltd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#History").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#applog").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#workdaylog").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#othertd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });

            $("#CustomerServiceAddtd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#CustomerServiceListtd").on(shell_win.openeven, function () {
                location.href = $(this).attr('data-url');
            });
            $("#pretd").on(shell_win.openeven, function () {
                shell_win.PrePage();
            });
            $("#nexttd").on(shell_win.openeven, function () {
                shell_win.NextPage();
            });

            ////页面初始化
            //shell_win.page.init();
            ////首页初始化
            //shell_win.home.init();
            ////初始化页面动作
            //shell_win.event.init();
            ////微信功能初始化
            ////shell_win.weixin.init();
            ////定位功能
            //var arr = window.document.location.href.split("#");
            //if(arr.length >= 2 && arr[1]!=''){
            //	shell_win.home.on_icon_touch(arr[1]);
            //}
        }
    };
    //公开全局对象
    shell_win_all = shell_win;
    //初始化页面
    shell_win_all.init();
});
function ShowNews(Id)
{
    location.href = "News/NewsInfo.html?ffileid=" + Id;
}