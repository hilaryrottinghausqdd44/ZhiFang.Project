$(function () {
    //测试
    JcallShell.Cookie.set(JcallShell.Cookie.map.IsReadAgreement, "1");
    //我的用户账户信息
    var MytInfo = JcallShell.LocalStorage.User.getAccount(true) || {};
    //我的医生账户信息
    var MyDoctInfo = JcallShell.LocalStorage.User.getDoctor(true) || {};
    //卡片配置文件地址
    var HOME_CARD_URL = '../config/home.json?v=' + new Date().getTime();
    //设置是否开启密码登录服务
    var PASSWORD_FLAG_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_IsPasswordLogin";
    //强制密码
    var USE_PASSWORD = false;

    //初始化广告图片幻灯片
    function initTopSwiper() {
        var items = [
			{ "Name": "", "Url": "../../../images/home/top/001.png" },
			{ "Name": "", "Url": "../../../images/home/top/002.png" },
			{ "Name": "", "Url": "../../../images/home/top/003.png" },
            { "Name": "", "Url": "../../../images/home/top/004.png" },
			{ "Name": "", "Url": "../../../images/home/top/005.png" },
			{ "Name": "", "Url": "../../../images/home/top/006.png" }
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
						'<div class="weui-grid__icon">' +
							'<img src="' + list[i].icon + '" alt="" style="width:35px;height:35px;">' +
						'</div>' +
						'<p class="weui-grid__label" style="top:2px">' + list[i].Name + '</p>' +
					'</a>'
				);
            }
        }

        return html.join("");
    }

    //初始化我的账户信息
    function initMyDoctInfo() {
        var src = MytInfo.BWeiXinAccount_HeadImgUrl || JcallShell.System.Path.UI + '/images/default/head.png';
        $("#AccountHeadImg").width("64px"); //头像
        $("#AccountHeadImg").attr("src", src); //头像

        var Name = MyDoctInfo.Name;
        $("#AccountName").html(Name || ""); //姓名

        $("#Doctor_Area").html("区域：" + MyDoctInfo.AreaName || "");//所属区域
        $("#Doctor_Hospital").html("医院：" + MyDoctInfo.HospitalName || "");//医院名称
        $("#Doctor_Dept").html("科室：" + MyDoctInfo.HospitalDeptName || "");//所属科室


        //强制密码
        if (MytInfo.BWeiXinAccount_LoginInputPasswordFlag) {
            USE_PASSWORD = true;
            $("#usePassword").attr("checked", "checked");
        }
        $("#usePasswordDiv").show();
    }

    //初始化监听
    function initListeners() {
        //二维码点击
        $("#barcodeButton").on("click", function () {
            location.href = '../../patient/user/barcode.html';//用户信息二维码页面地址
        });
        //用户详细信息
        $("#userInfo").on("click", function () {
            location.href = "../user/info.html"; //跳转用户信息页
        });
        //用户条码
        $("#userBarcode").on("click", function () {
            location.href = '../../patient/user/barcode.html';//用户信息二维码页面地址
        });
        //修改密码
        $("#editPassword").on("click", function () {

        });
        //转至登录
        $("#toLogin").on("click", function () {
            location.href = "../login/login.html"; //跳转到登录页
        });
        //强制密码
        $("#usePassword").on("click", function () {
            USE_PASSWORD = !USE_PASSWORD;
            console.log(USE_PASSWORD);

            $.showLoading("处理中...");
            var url = PASSWORD_FLAG_URL + '?isPassword=' + USE_PASSWORD;
            JcallShell.Server.ajax({
                url: url
            }, function (data) {
                $.hideLoading();
                if (data.success) {
                    MyDoctInfo.BWeiXinAccount_LoginInputPasswordFlag = USE_PASSWORD;
                } else {
                    $("#usePasswordDiv").hide();
                    $.alert("服务错误！", "错误提示");
                }
            });
        });
        //医学检验执行细则
        $("#MedicalRules").on("click", function () {
            location.href = "http://mp.weixin.qq.com/s/1YQpE2M54PEHZm_B_OzNeg";
        });
        //用户服务协议
        $("#userAgreement").on("click", function () {
            location.href = "http://mp.weixin.qq.com/s/yN83HBhYzrpFqlsK4SVn8g";
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
        $("#BuyTesti").attr("src", "../../../images/Home/BuyTest1.png");
        $("#Newsi").attr("src", "../../../images/Home/News1.png");
        $("#ShopCarti").attr("src", "../../../images/Home/ShopCart1.png");
        $("#UserCenteri").attr("src", "../../../images/Home/UserCenter1.png");
        if (index == 1) {
            $("#BuyTesti").attr("src", "../../../images/Home/BuyTest.png");
        }
        if (index == 2) {
            $("#Newsi").attr("src", "../../../images/Home/News.png");
        }
        if (index == 3) {
            $("#ShopCarti").attr("src", "../../../images/Home/ShopCart.png");
        }
        if (index == 4) {
            $("#UserCenteri").attr("src", "../../../images/Home/UserCenter.png");
        }

        $("#bottomTabbar a[index='" + index + "']").click();
    }

    //是否已阅读并同意用户协议
    function IsConfirmAgreement(callback) {
        //是否已阅读并同意用户协议
        var IsReadAgreement = JcallShell.Cookie.get(JcallShell.Cookie.map.IsReadAgreement);
        if (IsReadAgreement == '1') {
            callback();
        } else {
            location.href = "../agreement/index.html";
        }
    }

    //初始化首页信息
    (function () {
        //是否已阅读并同意用户协议
        IsConfirmAgreement(function () {
            var index = JcallShell.LocalStorage.Home.getButtonIndex();
            index = index ? index : '1';
            showInfoByIndex(index);

            //初始化广告图片幻灯片
            initTopSwiper();
            //初始化用户信息
            initMyDoctInfo();
            //初始化功能卡片内容
            initHomeCard();
            //初始化监听
            initListeners();
        });
    })();
});