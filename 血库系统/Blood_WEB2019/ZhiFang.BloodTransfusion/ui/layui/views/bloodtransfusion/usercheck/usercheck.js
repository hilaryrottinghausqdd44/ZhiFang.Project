layui.extend({
  uxutil: 'ux/util',
  bloodsconfig: 'config/bloodsconfig'
}).define(['layer', 'uxutil', 'bloodsconfig'], function(exports){
	"use strict";
	var $ = layui.jquery;
	var layer = layui.layer;
	var uxutil = layui.uxutil;
	var bloodsconfig = layui.bloodsconfig;
	var LOGIN_URL = uxutil.path.ROOT + bloodsconfig.Common.LOGINOFPUSER_URL;
	var checkUserUrl = uxutil.path.ROOT + '/ui/layui/views/bloodtransfusion/usercheck/index.html';
	//验证用户
	var usercheck = {};
	
	//获取用户数据
	usercheck.getUserData = function(layero){
        var framedoc = layero.find('iframe').contents();
        var userEle = framedoc.find('#user_workno');
        var PasswordEle = framedoc.find('#user_password');
        var UserText = userEle.find('option:selected').text();
        var UserNo = userEle.val(); //用户Id
        var PassWord = PasswordEle.val();
        var ShortCode = UserText.split('-');
        if (ShortCode.length > 1) {
        	ShortCode = ShortCode[1];
        };	
        //返回用户数据
        return {"UserNo":UserNo, "ShortCode":ShortCode, "PassWord": PassWord};
	}
	
	//打开且校验用户
	usercheck.openCheckUser = function(callback){
		var me = this;
		layer.open({			
			type: 2,
			title: "操作用户确认",
			area: ['350px', '250px'],
			content: [checkUserUrl,'no'],
			id: "lay-app-form-open-user-check",
			btn: ['确定', '取消'], //
			btnAlign:"c",
			success: function (layero, index) {
				return false; 
			},
			yes: function (index, layero) {
        //确认
        var userData = me.getUserData(layero);
        layer.close(index);
        me.checkuser(userData, callback)
				return false; 
			},
			btn2: function (index, layero) {
        //取消
        layer.close(index);
				if (callback && typeof callback == 'function') callback({});
				return false; 
			},
			end: function () {				
            
			},
			cancel: function (index, layero) {
        //if (callback && typeof callback == 'function') callback({});				
			}
		});		
	}
	//校验用户
	usercheck.checkuser = function(userdata, callback){
		var ShortCode = userdata["ShortCode"];
		var password = userdata["PassWord"];
        var url = LOGIN_URL + '?strUserAccount=' + ShortCode + '&strPassWord=' + password;
		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url: url
		}, function(data) {
			layer.closeAll('loading');
			var success = data.success;
			if (success == undefined || success == null) success = data;
			//临时登录帐号
			if (success === true) {
          callback(userdata);
			} else {
				layer.msg('账号或密码错误！');
				callback({});
			}
		});		
	}
	
	exports('usercheck', usercheck);
})
