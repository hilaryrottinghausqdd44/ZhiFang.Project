/**
 * 单站点授权
 * @author longfc
 * @version 2016-12-10
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.basic.ActionForm', {
	extend: 'Shell.class.wfm.authorization.ahsingle.show.ContentPanel',
	title: '授权内容',

	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateAHSingleLicenceByField',

	/**通过按钮文字*/
	OverButtonName: '',
	/**不通过按钮文字*/
	BackButtonName: '',

	/**通过状态文字*/
	OverName: '',
	/**不通过状态文字*/
	BackName: '',

	/**处理意见字段*/
	OperMsgField: '',
	/**处理时间字段*/
	AuditDataTimeMsgField: '',
	/**处理意见内容*/
	OperMsg: '',
	/**信息ID*/
	PK: null,
	/**需要保存的数据*/
	Entity: null,
	classNameSpace: 'ZhiFang.Entity.ProjectProgressMonitorManage', //类域
	className: 'LicenceStatus', //类名
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createDockedItems: function() {
		var me = this,
			items = ['->'];
		var dockedItems = [{
			xtype: 'toolbar',
			dock: 'bottom',
			items: items
		}];
		return dockedItems;
	},
	/**通过*/
	onOver: function() {},
	/**未通过*/
	onBack: function() {},
	onSaveClick: function(StatusName) {},
	/**保存数据*/
	onSave: function(Status) {},
	/**获取参数*/
	getParams: function(Status) {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		var params = {
			entity: {
				Id: me.PK,
				Status: Status
			},
			fields: 'Id,Status'
		};
		//处理意见
		if(me.OperMsgField) {
			params.entity[me.OperMsgField] = me.OperMsg;
		}
		if(me.AuditDataTimeMsgField && ReviewDateStr) {
			params.entity[me.AuditDataTimeMsgField] = ReviewDateStr;
		}
		//需要保存的数据
		if(me.Entity && Ext.typeOf(me.Entity) == 'object') {
			for(var i in me.Entity) {
				params.entity[i] = me.Entity[i];
			}
		}
		return params;
	}
});