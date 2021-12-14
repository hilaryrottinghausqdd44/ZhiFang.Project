/**
 * Created by mowa on 2015/10/14.
 */

//var user=Shell.util.Path.getRequestParams().user,
//    password=Shell.util.Path.getRequestParams().password;

$(function () {

    var user = getRequestParams().user;
    var password = getRequestParams().password;

    $.ajax({
        type: 'get',
        contentType: 'application/json',
        url: getRootPath() + '/ServiceWCF/PublisherEvent.svc/GetReportQueryUI',
        dataType: 'json',
        data: { user: user, password: password },
        success: function (data) {
            if (data.success == true) {
                    window.location.href = 'http://localhost/WebLis/ui/report/print/index.html';
            } else {
                $.messager.alert('提示', '登录用户名，密码错误。' + data.msg);
            }
        }
    });

    function getRequestParams(){
        var url = location.search;//获取url中"?"符后的字串

        if(url.indexOf("?") == -1) return {};

        var str = url.substr(1),
            strs = str.split("&"),
            len = strs.length,
            params = {};

        for(var i=0;i<len;i++){
            var arr = strs[i].split("=");
            params[arr[0]] = decodeURI(arr[1]);
        }

        return params;
    }

   function  getRootPath(){
        //获取当前网址,如:http://localhost:8080/Web/ui/test.html
        var href = window.document.location.href;
        //获取主机地址之后的目录,如:Web/ui/test.html
        var pathname = window.document.location.pathname;
        var pos = href.indexOf(pathname);
        //获取主机地址,如:http://localhost:8080
        var localhostPaht=href.substring(0,pos);
        //获取带"/"的项目名,如:/Web
        var projectName = pathname.substring(0,pathname.substr(1).indexOf('/')+1);
        return(localhostPaht + projectName);
    }
});
