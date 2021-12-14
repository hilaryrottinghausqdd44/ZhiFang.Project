/**
 * @name：layui.ux.zf.account 账号类 
 * @author：Jcall
 * @version 2020-09-21
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil','layer'],function(exports){
    "use strict";

	var uxutil = layui.uxutil,
		MOD_NAME = 'zfAccount';

	//外部接口
	var account = {
		set:function(value){
			uxutil.sessionStorage.set('account',value);
		},
		get:function(){
			return uxutil.sessionStorage.get('account');
		},
		//是否唯一，判断当前登录账号是否与cookie中一致
		onlyone:function(){
			var loginAccount = this.get(),
				nowAccount = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME);
			return (loginAccount && nowAccount && loginAccount == nowAccount);
		},
		//启动自动判断
		initValid:function(callback){
			var me = this;
			var isValid = me.onlyone();
			if(isValid){
				setTimeout(function(){
					me.initValid(callback);
				},1000);
			}else{
				//不一致，直接提示重新登录
				callback();
			}
		}
	};
	//暴露接口
	exports(MOD_NAME,account);
});