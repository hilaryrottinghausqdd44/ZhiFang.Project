/**
 * 设置页面的登录页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'layer', 'util'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		layer = layui.layer, util = layui.util,
		$ = layui.jquery;
	var app = {};
	app.logincount = 0;
	/**倒计时*/
	tackTime= 5;
		staticTackTime= '';
		isstopsubmit= true;
				
	//服务地址
	app.url = {
		/**登录服务地址*/
		loginUrl:uxutil.path.ROOT+'/ServiceWCF/ReportFormService.svc/StaticUserLogin'
	};
	
	//初始化  
	app.init = function () {
		var me = this;
		me.listeners();
		

	};
	//监听事件	
	app.listeners = function () {
		//登录按钮
		$('#Button_Login').on('click', function () {
			var password = $('#password').val();
			if (password) {
				console.log(password);
				var url = app.url.loginUrl + '?Account=' + password ;
				uxutil.server.ajax({
					url: url,
					async: false
				}, function (data) {
					if (data.success) {
						window.location.href = uxutil.path.UI + '/layui/setting.html';

						
					} else {
						layer.msg("登录失败！");
						//me.logincount = me.logincount + 1;
						//if (me.logincount == 5) {
						//	me.isstopsubmit = false;
						//	me.staticTackTime = 0;
						//	me.staticTackTime = me.staticTackTime + me.tackTime;
						//	me.task = {
						//		run: function () {
						//			me.onMsgChange("登录失败次数过多当前限制剩余时间:" + me.staticTackTime + "秒");
						//			if (me.staticTackTime == 0) {
						//				me.isstopsubmit = true;
						//				Ext.TaskManager.stop(me.task);
						//				me.logincount = 0;
						//				me.onMsgChange("");
						//			}
						//			me.staticTackTime--;
						//		},
						//		interval: 1000
						//	};
						//	Ext.TaskManager.start(me.task);//启动计时器		
						//} else {
						//	me.onMsgChange('登录失败！');
						//}
						//me.onAccountFocus(me.focusTimes);
					}
				});
			} else {
				layer.msg("请输入密码");
			}
			
		})
	}

	app.init();
});