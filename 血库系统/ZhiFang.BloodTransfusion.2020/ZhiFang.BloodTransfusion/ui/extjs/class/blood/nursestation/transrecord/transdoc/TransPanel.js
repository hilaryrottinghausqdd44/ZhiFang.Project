/**
 * 输血过程记录:发血记录主单列表
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.transdoc.TransPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '发血记录信息',
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
		//发血记录信息
		me.OutPanel = Ext.create("Shell.class.blood.nursestation.transrecord.out.OutPanel", {
			//region: 'north',
			region: 'center',			
			border:false,
			header: false,
			itemId: 'OutPanel'
		});
		//输血过程主单信息
		me.DocForm = Ext.create("Shell.class.blood.nursestation.transrecord.transdoc.DocForm", {
			region: 'south',
			header: false,
			//border:false,
			height: 95,
			itemId: 'DocForm'
		});
		//病人体征信息
		me.DtlForm = Ext.create('Shell.class.blood.nursestation.transrecord.transdtl.DtlForm', {
			region: 'south',
			height: 195,
			header: false,
			//border:false,
			itemId: 'DtlForm',
			split: false,
			collapsible: false
		});
		return [me.OutPanel, me.DocForm, me.DtlForm];
	},
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;
		me.OutPanel.on({
			itemclick: function(OutPanel, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					var id = record.get(me.OutPanel.PKField);

				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.OutPanel.PKField);

				}, null, 500);
			},
			nodata: function(p) {

			}
		});
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
	onNodata: function() {
		var me = this;
		me.fireEvent('nodata', me);
	},
	loadDtl: function(record) {
		var me = this;
	}
});
