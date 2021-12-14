/**
 * 采购申请单操作记录
 * @author longfc
 * @version 2017-11-01
 */
Ext.define('Shell.class.rea.client.reareqoperation.Panel', {
	extend: 'Ext.panel.Panel',
	
	title: '操作记录',
	autoScroll: true,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaReqOperationByHQL?isPlanish=true',

	bodyPadding: 10,

	classNameSpace: 'ZhiFang.Entity.ReagentSys.Client', //类域
	className: '', //类名
	/**业务对象ID*/
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.REA.StatusList.getStatusList(me.className, false, true, function(data) {
			if(data.success) {
				me.onLoadData();
			} else {
				var html = '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">获取' + me.className + '的状态信息为空!</div>';
				me.update(html);
			}
		});
		//		JShell.System.ClassDict.init(me.classNameSpace, me.className, function() {
		//			if(JShell.System.ClassDict[me.className]) {
		//				me.onLoadData();
		//			} else {
		//				var html = '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">获取' + me.className + '的状态信息为空!</div>';
		//				me.update(html);
		//			}
		//		});
	},
	/**获取操作记录信息*/
	onLoadData: function(callback) {
		var me = this;
		if(me.PK) {
			var url = JShell.System.Path.getRootUrl(me.selectUrl);

			var fields = ['Id', 'BobjectID', 'Type', 'Memo', 'DataAddTime', 'CreatorName'];
			url += '&fields=ReaReqOperation_' + fields.join(',ReaReqOperation_');
			url += '&where=reareqoperation.BobjectID=' + me.PK;
			url += '&sort=[{"property":"ReaReqOperation_DataAddTime","direction":"ASC"}]';

			JcallShell.Server.get(url, function(data) {
				if(data.success) {
					if(data.value) {
						me.changeHtml(data.value.list);
					} else {
						var html = '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">没有操作记录</div>';
						me.update(html);
					}
				} else {
					var html = '<div style="color:red;text-align:center;margin:20px 10px;font-weight:bold;">' + data.msg + '</div>';
					me.update(html);
				}
			});
		} else {
			var html = '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">传入参数(PK值)为空!</div>';
			me.update(html);
		}
	},

	/**更改页面内容*/
	changeHtml: function(list) {
		var me = this,
			arr = list || [],
			len = arr.length,
			html = [];

		for(var i = 0; i < len; i++) {
			var data = arr[i];
			html.push('<div style="margin:5px;">');
			html.push(JShell.Date.toString(data.ReaReqOperation_DataAddTime) + ' ');
			html.push(data.ReaReqOperation_CreatorName + ' ');
			//var baseInfo = JShell.System.ClassDict.getClassInfoById(me.className, data.ReaReqOperation_Type);
			var idKey=data.ReaReqOperation_Type;
			var cName =JShell.REA.StatusList.Status[me.className].Enum[idKey];
			var infoBGColor =JShell.REA.StatusList.Status[me.className].BGColor[idKey];
			if(cName) {
				var style = [];
				if(infoBGColor) {
					style.push('color:' + infoBGColor);
				}

				html.push('<b style="' + style.join(';') + '">' + cName + '</b> ');

				if(data.ReaReqOperation_Memo) {
					html.push('处理意见：<b>' + data.ReaReqOperation_Memo + '</b>');
				}
				html.push('</div>');
			}
			html.push('</div>');
		}
		me.update(html.join(''));
	}
});