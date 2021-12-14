/**
 * CS试剂客户端升级BS
 * @author longfc
 * @version 2018-10-17
 */
Ext.define('Shell.class.rea.client.cstobs.ScheduleInfo', {
	extend: 'Ext.panel.Panel',
	title: '当前CS试剂客户端升级BS进度',
	autoScroll: true,
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_GetCSUpdateToBSInfo',
	bodyPadding: 10,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//me.loadData();
	},
	/**获取操作记录信息*/
	loadData: function() {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);

		JShell.Server.post(url, "", function(data) {
			if(data.success) {
				if(data.value) {
					me.changeHtml(data.value.list);
				} else {
					var html = '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">没有初始化记录</div>';
					me.update(html);
				}
			} else {
				var html = '<div style="color:red;text-align:center;margin:20px 10px;font-weight:bold;">' + '没有初始化记录' + '</div>';
				me.update(html);
			}
		});
	},
	/**更改页面内容*/
	changeHtml: function(list) {
		var me = this,
			arr = list || [],
			len = arr.length,
			html = [];
		var cssStr = '<link rel="stylesheet" type="text/css" href="../web_src/bootstrap-3.3.2-dist/css/bootstrap.min.css" />';
		html.push(cssStr);
		html.push('<div class="container-fluid">');

		var cols = 4;
		//向上取整,有小数就整数部分加1
		var rows = Math.ceil(len / cols);
		var col = 0
		//console.log("rows:" + rows);
		for(var row = 0; row < rows; row++) {
			html.push('<div class="row">');
			for(i = 0; i < cols; i++) {
				var data = arr[col];
				if(data) {
					html.push('<div class="col-md-3">');
					html.push('<span style="font-size:12px;">' + data.CName + '</span>');
					var style = [];
					if(data.Sataus == '1') {
						style.push('color:#BA55D3;');
					} else if(data.Sataus == '2') {
						style.push('color:#0000FF;');
					} else {
						style.push('color:#FF0000;');
					}
					html.push('<b style="' + style.join(';') + '"><span style="font-size:12px;">' + data.SatausName + '</span></b> ');
					html.push('</div>');
				}
				col++;
			}
			html.push('</div>');
		}
		html.push('</div>');
		me.update(html.join(''));
	}
});