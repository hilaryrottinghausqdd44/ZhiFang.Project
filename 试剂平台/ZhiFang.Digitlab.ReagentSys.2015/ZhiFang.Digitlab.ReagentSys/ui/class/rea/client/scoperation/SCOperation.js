/**
 * 公共操作记录
 * @author longfc
 * @version 2017-11-07
 */
Ext.define('Shell.class.rea.client.scoperation.SCOperation', {
	extend: 'Ext.panel.Panel',
	title: '公共操作记录',
	autoScroll: true,
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/SC_UDTO_SearchSCOperationByHQL?isPlanish=true',

	bodyPadding: 10,

	/** @public
	 * 类域
	 * @example
	 * 		ZhiFang.Entity.ProjectProgressMonitorManage
	 */
	classNameSpace: '',
	/** @public
	 * 类名
	 * @example
	 * 		PContractStatus
	 */
	className: '',
	/**业务对象ID*/
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.System.ClassDict.init(me.classNameSpace, me.className, function() {
			if(JShell.System.ClassDict[me.className]) {
				me.onLoadData();
			} else {
				var html = '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">获取' + me.className + '的状态信息为空!</div>';
				me.update(html);
			}
		});
	},
	/**获取操作记录信息*/
	onLoadData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);

		var fields = ['Id', 'BobjectID', 'Type', 'Memo', 'DataAddTime', 'CreatorName'];
		url += '&fields=SCOperation_' + fields.join(',SCOperation_');
		url += '&where=scoperation.BobjectID=' + me.PK;
		url += '&sort=[{"property":"SCOperation_DataAddTime","direction":"ASC"}]';

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
			html.push(JShell.Date.toString(data.SCOperation_DataAddTime) + ' ');
			html.push(data.SCOperation_CreatorName + ' ');

			var info = JShell.System.ClassDict.getClassInfoById(me.className, data.SCOperation_Type);

			if(info) {
				var style = [];
				if(info.BGColor) {
					style.push('color:' + info.BGColor);
				}

				html.push('<b style="' + style.join(';') + '">' + info.Name + '</b> ');

				if(data.PTaskOperLog_OperateMemo) {
					html.push('处理意见：<b>' + data.SCOperation_Memo + '</b>');
				}

				html.push('</div>');
			}
			html.push('</div>');
		}
		me.update(html.join(''));
	}
});