/**
 * 操作记录
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.invoice.basic.OperatePanel', {
	extend: 'Ext.panel.Panel',
	title: '发票操作记录',
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
		me.getStatusListData();
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
			var obj = me.getInfoByID(data.SCOperation_Type);
			var Info = null;
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
			html.push('</div>');
		}
		me.update(html.join(''));
	},
	/**获取参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "PInvoiceStatus",
				"classnamespace": "ZhiFang.Entity.ProjectProgressMonitorManage"
			}]
		};
		return params;
	},
	/**获取操作记录信息*/
	getStatusListData: function(callback) {
		var me = this,
			params = {},
			url = JShell.System.Path.getRootUrl(me.classdicSelectUrl);
		var StatusListData = null;
		me.StatusList = [];
		params = Ext.encode(me.getParams());
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					StatusListData = data.value[0].PInvoiceStatus;
					me.StatusList = StatusListData;
				} else {
					StatusListData = null;
					me.StatusList = StatusListData;
				}
			} else {
				StatusListData = null;
				me.StatusList = StatusListData;
			}
		}, false);
		return StatusListData;
	},
	/**根据ID获取信息*/
	getInfoByID: function(value) {
		var me = this;
		for(var i in me.StatusList) {
			var obj = me.StatusList[i];
			if(obj.Id == value) {
				return obj;
			}
		}
		return null;
	}
});