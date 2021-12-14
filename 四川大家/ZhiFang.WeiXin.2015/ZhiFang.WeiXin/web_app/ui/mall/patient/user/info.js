$(function () {
    //修改账号信息服务地址
    var EDIT_ACCOUNT_URL = JcallShell.System.Path.ROOT +
		"/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBWeiXinAccountByField";
    //生日值
    var BIRTHDAY = null;

    $("#SexID").select({
        title: "选择性别",
        items: ["男", "女"]
    });
    //初始化出生年月
    //$("#Birthday").calendar({
    //    dateFormat: 'yyyy-mm-dd',
    //    onChange: function (p, values, displayValues) {
    //        BIRTHDAY = values[0];
    //    }
    //});

    //保存按钮监听
    $("#saveButton").on("click", function () {
        onSave(function () {
            var info = JcallShell.LocalStorage.User.getAccount(true);//账户信息
            info.BWeiXinAccount_Name = $("#Name").val();
            info.BWeiXinAccount_SexID = $("#SexID").val() == "男" ? "1" : "2";
            info.BWeiXinAccount_Birthday = BIRTHDAY;
            info.BWeiXinAccount_MobileCode = $("#MobileCode").val();
            info.BWeiXinAccount_IDNumber = $("#IDNumber").val();

            info = JcallShell.JSON.encode(info);//转码
            JcallShell.LocalStorage.User.setAccount(info);//初始化账户信息

            $.alert("保存成功！", "提示");
        });
    });

    //获取账户信息数据
    function initAccountData(callback) {
        var info = JcallShell.LocalStorage.User.getAccount(true);//账户信息
        if (!info) {
            $.alert("非法访问，账户信息不存在！", "警告！");
            return;
        }

        var SexName = info.BWeiXinAccount_SexID == "1" ? "男" : "女";
        if (info.BWeiXinAccount_Birthday) {
            BIRTHDAY = JcallShell.Date.toString(info.BWeiXinAccount_Birthday, true);
        }

        $("#Name").val(info.BWeiXinAccount_Name);
        $("#MobileCode").val(Number(info.BWeiXinAccount_MobileCode));
        $("#IDNumber").val(info.BWeiXinAccount_IDNumber);

        //初始化组件
        $("#SexID").select("update", {
            input: SexName
        });
        if (BIRTHDAY) {
            //$("#Birthday").calendar("setValue", ["2016-12-12"]);
             $("#Birthday").attr("value", BIRTHDAY);
        }
    }
    //保存账户信息
    function onSave(callback) {
        if (!onFromValidator()) { return; }//表单数据格式校验

        var SexID = $("#SexID").val();
        SexID = SexID == "男" ? "1" : "2";

        var info = JcallShell.LocalStorage.User.getAccount(true);//账户信息

        if (!info) {
            $.alert("非法访问，账户信息不存在！", "警告！");
            return;
        }
        BIRTHDAY = $("#Birthday").val();
        var data = {
            entity: {
                Id:info.BWeiXinAccount_Id,
                Name: $("#Name").val(),
                SexID: SexID,
                Birthday: JcallShell.Date.toServerDate(BIRTHDAY),
                MobileCode: $("#MobileCode").val(),
                IDNumber: $("#IDNumber").val()
            },
            fields: 'Id,Name,SexID,Birthday,MobileCode,IDNumber'
        };

        data = JcallShell.JSON.encode(data);

        $.showLoading("处理中...");
        JcallShell.Server.ajax({
            url: EDIT_ACCOUNT_URL,
            type: 'post',
            showError: true,
            data: data
        }, function (data) {
            $.hideLoading();
            if (data.success) {
                callback();
            } else {
                $.alert("服务错误！", "错误提示");
            }
        });
    }

    //表单数据格式校验
    function onFromValidator() {
        var Name = $("#Name").val(),
			SexID = $("#SexID").val(),
			MobileCode = $("#MobileCode").val(),
			IDNumber = $("#IDNumber").val();
            BIRTHDAY = $("#IDNumber").val();

        if (!Name) {
            $.toptip('姓名内容项都不能为空!');
            return false;
        } else if (!SexID) {
            $.toptip('性别内容项都不能为空!');
            return false;
        } else if (!BIRTHDAY) {
            $.toptip('生日内容项都不能为空!');
            return false;
        } else if (!MobileCode) {
            $.toptip('手机内容项都不能为空!');
            return false;
        }
        //else if (!IDNumber) {
        //    $.toptip('身份证内容项都不能为空!');
        //    return false;
        //}
        else if (Name.length > 20) {
            $.toptip('姓名长度必须在1到20位之间');
            return false;
        } else if (!JcallShell.Isvalid.isCellPhoneNo(MobileCode)) {
            $.toptip('手机号格式有错误！');
            return false;
        } else if (!JcallShell.Isvalid.isIdCardNo(IDNumber)) {
            $.toptip('身份证号格式有错误！');
            return false;
        } else {
            return true;
        }
    }

    setTimeout(function () {
        initAccountData();
    }, 100);
});