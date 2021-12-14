/**
 * 输血过程记录:发血记录主单列表
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.search.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '发血记录信息',
	bodyPadding: '0px',
	//输血过程记录主单ID
	PK: null,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**His调用传入的就诊号*/
	AdmId: "",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('select', 'nodata','save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		//发血记录信息
		me.OutPanel = Ext.create("Shell.class.blood.nursestation.transrecord.out.OutPanel", {
			//region: 'north',
			region: 'center',
			border: false,
			header: false,
			itemId: 'OutPanel',
			/**His调用传入的就诊号*/
			AdmId: me.AdmId
		});
		var width1=me.width;
		//输血过程记录项信息
		me.TransPanel = Ext.create('Shell.class.blood.nursestation.transrecord.search.TransPanel', {
			region: 'south',
			header: false,
			height: 390,
			width: width1,
			itemId: 'TransPanel',
			split: true,
			collapsible: false
		});
		return [me.OutPanel, me.TransPanel];
	},
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;
		me.OutPanel.on({
			dtlselect: function(grid, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.clearData();
			},
			save: function(p) {
				me.fireEvent('save', me);
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**外部调用*/
	onSearch: function() {
		var me = this;
		me.OutPanel.onSearch();
	},
	clearData: function() {
		var me = this;
		me.TransPanel.PK =null;
		me.TransPanel.clearData();
		me.fireEvent('nodata', me);
	},
	loadData: function(record) {
		var me = this;
		//发血血袋对应的输血记录主单ID
		var transFormId = record.get("BloodBOutItem_BloodTransForm_Id");
		if (!transFormId) transFormId = null;
		me.PK = transFormId;
		me.TransPanel.PK = transFormId;
		me.TransPanel.loadData(record);
		me.fireEvent('select', me,record);
	}
});
