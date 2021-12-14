//依托于JcallShell.Cookie，需要先加载JcallShell文件

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

//判断是否已经登录
JcallShell.Login = {
    pageUrl: "/sysbase/main/login.html",
    isLogin:function () {
        var ACCOUNTNAME = JcallShell.Cookie.get(JcallShell.Cookie.map.ACCOUNTNAME);
        if (!ACCOUNTNAME) {
            var loginUrl = JcallShell.System.Path.UI + this.pageUrl;
            if (location.href != loginUrl) {
                location.href = loginUrl;
            }
            return false;
        } else {
            $("body").show();
            return true;
        }
    }
};

//判断是否登录
JcallShell.Login.isLogin();