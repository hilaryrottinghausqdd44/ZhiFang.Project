/**
 * 公共操作记录
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.blood.scoperation.SCOperation', {
	extend: 'Ext.panel.Panel',
	title: '公共操作记录',
	autoScroll: true,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchSCOperationByHQL?isPlanish=true',

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
	/**默认数据条件*/
	defaultWhere: '',
	/**内部数据条件*/
	internalWhere: '',
	/**外部数据条件*/
	externalWhere: '',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JcallShell.BLTF.StatusList.getStatusList(me.className, false, true, function(data) {
			if(data.success) {
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
		url += '&where='+me.getHqlWhere();//scoperation.BobjectID=' + me.PK;
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
	/**获取带查询参数的URL*/
	getHqlWhere: function() {
		var me = this,
			arr = [];
		if(me.PK){
			arr.push('scoperation.BobjectID=' + me.PK);
		}
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		return where;
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
			html.push("操作人:"+data.SCOperation_CreatorName + ' ');
			html.push(JShell.Date.toString(data.SCOperation_DataAddTime) + ' ');
			//var info = JShell.System.ClassDict.getClassInfoById(me.className, data.SCOperation_Type);
			var idKey=data.SCOperation_Type;
			var cName =JcallShell.BLTF.StatusList.Status[me.className].Enum[idKey];
			var infoBGColor =JcallShell.BLTF.StatusList.Status[me.className].BGColor[idKey];
			
			if(cName) {
				var style = [];
				if(infoBGColor) {
					style.push('color:' + infoBGColor);
				}

				html.push('<b style="' + style.join(';') + '">' + cName + '</b> ');

				if(data.SCOperation_Memo) {
					html.push('操作信息：<b>' + data.SCOperation_Memo + '</b>');
				}

				html.push('</div>');
			}
			html.push('</div>');
		}
		me.update(html.join(''));
	}
});