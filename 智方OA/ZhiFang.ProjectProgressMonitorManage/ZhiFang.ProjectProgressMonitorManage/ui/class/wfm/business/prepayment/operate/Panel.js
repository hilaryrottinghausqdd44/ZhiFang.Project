/**
 * 报销单操作记录
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.prepayment.operate.Panel',{
    extend: 'Ext.panel.Panel',
    title:'报销单操作记录',
	autoScroll: true,
	/**获取数据服务路径*/
	selectUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCOperationByHQL?isPlanish=true',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	/**发票ID*/
	PK: null,
	bodyPadding: 10,
	StatusList: [],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PRepaymentStatus', function() {
			if(!JShell.System.ClassDict.PRepaymentStatus) {
				JShell.Msg.error('未获取到还款状态，请刷新列表');
				return;
			}
		});
		me.onLoadData();
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

			if(data.SCOperation_Type) {
				var obj = JShell.System.ClassDict.getClassInfoById('PRepaymentStatus', data.SCOperation_Type);
				if(obj) {
					var style = [];
					if(obj.BGColor) {
						style.push('color:' + obj.BGColor);
					}
					if(data.SCOperation_Memo) {
						html.push('<b style="' + style.join(';') + '">' + obj.Name + '</b>  ' + '   处理意见：<b>' + data.SCOperation_Memo + '</b>');
					} else {
						html.push('<b style="' + style.join(';') + '">' + obj.Name + '</b>  ');
					}
				}
			}
			
			html.push('</div>');
		}
		me.update(html.join(''));
	}
});