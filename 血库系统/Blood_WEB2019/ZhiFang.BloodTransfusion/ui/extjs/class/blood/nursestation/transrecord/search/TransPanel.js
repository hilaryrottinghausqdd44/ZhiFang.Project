/**
 * 输血过程记录:左区域(发血记录主单)
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.search.TransPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '发血记录信息',
	bodyPadding: '0px',
	//输血过程记录主单ID
	PK:null,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('select', 'nodata');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var width1=me.width;
		//输血过程主单信息south
		me.DocForm = Ext.create("Shell.class.blood.nursestation.transrecord.transdoc.DocForm", {
			region: 'north',
			header: false,
			height: 70,
			//border: false,
			width: width1,
			itemId: 'DocForm'
		});
		//病人体征信息
		me.DtlForm = Ext.create('Shell.class.blood.nursestation.transrecord.transdtl.DtlForm', {
			region: 'center',
			header: false,
			itemId: 'DtlForm',
			width: width1,
			border: false,
			split: false,
			collapsible: false
		});
		return [me.OutPanel, me.DocForm, me.DtlForm];
	},
	/**显示遮罩*/
	showMask: function (text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		//me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function () {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		//me.enableControl(); //启用所有的操作功能
	},
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;
		me.DocForm.on({
			nodata: function(p) {

			}
		});
	},
	clearData: function() {
		var me = this;
		me.DocForm.PK=null;
		me.DtlForm.PK=null;
		me.DocForm.getForm().reset();
		me.DtlForm.getForm().reset();

		//me.fireEvent('nodata', me);
	},
	loadData: function(record) {
		var me = this;

		//发血血袋对应的输血记录主单ID
		var transFormId = record.get("BloodBOutItem_BloodTransForm_Id");
		if (!transFormId) transFormId = null;
		me.PK = transFormId;

		me.DocForm.getForm().reset();
		me.DtlForm.getForm().reset();

		//输血过程记录主单ID
		me.DocForm.PK=me.PK;
		me.DocForm.isShow(me.PK);
		
		me.DtlForm.PK=me.PK;
		me.DtlForm.isShow(me.PK);
	}
});
