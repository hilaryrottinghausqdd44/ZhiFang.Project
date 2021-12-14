$(function () {
    //当前显示的DIV
    var SHOW_DIV = null;
    //当前选中的底部功能按钮
    var SHOW_BOTTOM_BUTTON = null;
    
    var HOME_CARD_URL = JcallShell.System.Path.UI + '/mall/' + 
    	JcallShell.LocalStorage.User.getType() + '/config/home.json';
    
    //获取主页卡片信息
    function getHomeCrad(callback){
        JcallShell.Server.ajax({
            url: HOME_CARD_URL
        }, function (data) {
            $("#loading-div").modal("hide");
            callback(data);
        });
    }
	//初始化主页卡片内容
	function initHomeCard(){
		getHomeCrad(function(data){
			var html = createCardHtml(data);
			$("#card").html(html);
		});
	}
	//创建卡片
	function createCardHtml(data){
		var html = [];
		if(data.success){
			var list = data.data || [],
				len = list.length;
				
			for(var i=0;i<len;i++){
				html.push(
					'<div class="col-xs-6 col-md-4">' + 
						'<div class="home-module-div" data="' + list[i].Index + 
							'" onclick="location.href=\'' + JcallShell.System.Path.UI + list[i].Url + '\'">' +
							list[i].Name + 
						'</div>' +
					'</div>'
				);
			}
		}
		
		return html.join("");
	}
	
    //根据index显示页签内容
    function showInfoByIndex(index) {
        //按钮选中
        if (SHOW_BOTTOM_BUTTON) {
            SHOW_BOTTOM_BUTTON.removeClass("navbar-bottom-li-touch");
        }
        SHOW_BOTTOM_BUTTON = $("#navbar-bottom li[index='" + index + "']");
        SHOW_BOTTOM_BUTTON.addClass("navbar-bottom-li-touch");

        //DIV显示
        if (SHOW_DIV) {
            SHOW_DIV.hide();
        }
        SHOW_DIV = $("#home-div-" + index);
        SHOW_DIV.show();
    }
    
    //功按钮栏监听
    $("#navbar-bottom li").on("click", function () {
        var index = $(this).attr("index");
        JcallShell.LocalStorage.Home.addButtonIndex(index);
        //根据index显示内容
        showInfoByIndex(index);
    });
	
    //当前选中的用户设置
    var SHOW_USER_CONFIG = null;
    $("#home-user-config-div li").on("click", function () {
        if (SHOW_USER_CONFIG) {
            SHOW_USER_CONFIG.removeClass("home-user-config-div-li-touch");
        }
        SHOW_USER_CONFIG = $(this);
        SHOW_USER_CONFIG.addClass("home-user-config-div-li-touch");
    });

    //初始化首页信息
    (function () {
        var index = JcallShell.LocalStorage.Home.getButtonIndex();
        index = index ? index : '1';

        //根据index显示内容
        showInfoByIndex(index);
        initHomeCard();
    })();
});