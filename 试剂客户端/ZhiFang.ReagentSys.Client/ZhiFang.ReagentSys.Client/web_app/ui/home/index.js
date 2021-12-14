$(function () {
	//我的账户信息
    var ACCOUNT_INFO = JcallShell.LocalStorage.User.getAccount(true) || {};
    //卡片配置文件地址
    var HOME_CARD_URL = '../config/home.json?v=' + new Date().getTime();
    //初始化广告图片幻灯片
    function initTopSwiper() {
        var items = [
			{ "Name": "", "Url": "../images/home/top/001.png" },
			{ "Name": "", "Url": "../images/home/top/002.png" }
        ];

        var len = items.length,
			html = [];

        for (var i = 0; i < len; i++) {
            html.push(
				'<div class="swiper-slide"><img style="height:120px;" src="' + items[i].Url + '" /></div>'
			);
        }
        $("#topSwiper").html(html.join(""));
        $(".swiper-container").swiper({
            loop: true,
            autoplay: 3000
        });
    }

    //初始化功能卡片内容
    function initHomeCard() {
        getHomeCrad(function (data) {
            var html = createCardHtml(data);
            $("#card").html(html);
        });
    }
    //获取功能卡片信息
    function getHomeCrad(callback) {
        JcallShell.Server.ajax({
            url: HOME_CARD_URL
        }, function (data) {
            callback(data);
        });
    }
    //创建卡片
    function createCardHtml(data) {
        var html = [];
        if (data.success) {
            var list = data.data || [],
				len = list.length;

            for (var i = 0; i < len; i++) {
                html.push(
					'<a href="' + JcallShell.System.Path.UI + list[i].Url + '" class="weui-grid js_grid">' +
						'<div class="weui-grid__icon" style="width:35px;height:35px;">' +
							'<img src="' + list[i].icon + '" alt="" style="height:100%;">' +
						'</div>' +
						'<p class="weui-grid__label">' + list[i].Name + '</p>' +
					'</a>'
				);
            }
        }

        return html.join("");
    }

    //初始化我的账户信息
    function initMyDoctInfo() {
        var src = ACCOUNT_INFO.BWeiXinAccount_HeadImgUrl || JcallShell.System.Path.UI + '/images/default/head.png';
        $("#AccountHeadImg").width("64px"); //头像
        $("#AccountHeadImg").attr("src", src); //头像
		
        $("#AccountName").html(ACCOUNT_INFO.UserName || ""); //姓名
    }

    //初始化监听
    function initListeners() {
        //二维码点击
        $("#barcodeButton").on("click", function () {
            //location.href = '../../patient/user/barcode.html';//用户信息二维码页面地址
        });
        //用户详细信息
        $("#userInfo").on("click", function () {
            //location.href = "../user/info.html"; //跳转用户信息页
        });
        //用户条码
        $("#userBarcode").on("click", function () {
            //location.href = '../../patient/user/barcode.html';//用户信息二维码页面地址
        });
        //修改密码
        $("#editPassword").on("click", function () {

        });
        //转至登录
        $("#toLogin").on("click", function () {
            location.href = "../login/index.html"; //跳转到登录页
        });
        
        //安全退出
        $("#close").on("click", function () {

        });

        //功按钮栏监听
        $("#bottomTabbar a").on("click", function () {
            var index = $(this).attr("index");
            JcallShell.LocalStorage.Home.setButtonIndex(index);
            //根据index显示内容
            showInfoByIndex(index);
        });
    }

    //根据index显示页签内容
    function showInfoByIndex(index) {
        $("#BuyTesti").attr("src", "../images/Home/BuyTest1.png");
        $("#Newsi").attr("src", "../images/Home/News1.png");
        $("#ShopCarti").attr("src", "../images/Home/ShopCart1.png");
        $("#UserCenteri").attr("src", "../images/Home/UserCenter1.png");
        
        switch (index){
        	case "1": $("#BuyTesti").attr("src", "../images/Home/BuyTest.png");break;
        	case "2": $("#Newsi").attr("src", "../images/Home/News.png");break;
        	case "3": $("#ShopCarti").attr("src", "../images/Home/ShopCart.png");break;
        	case "4": $("#UserCenteri").attr("src", "../images/Home/UserCenter.png");break;
        	default: break;
        }
    }
	
    //初始化首页信息
    (function () {
        var index = JcallShell.LocalStorage.Home.getButtonIndex();
        index = index ? index : '1';
        showInfoByIndex(index);
        $("#bottomTabbar a[index='" + index + "']").click();

        //初始化广告图片幻灯片
        initTopSwiper();
        //初始化用户信息
        initMyDoctInfo();
        //初始化功能卡片内容
        initHomeCard();
        //初始化监听
        initListeners();
    })();
});