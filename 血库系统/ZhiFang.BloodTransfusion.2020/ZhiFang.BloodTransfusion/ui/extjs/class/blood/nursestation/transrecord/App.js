/**
 * 输血过程记录入口
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '输血过程记录',
	/**内容周围距离*/
	bodyPadding: '2px',
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**His调用传入的就诊号*/
	ADMID: "",
	/**接收His调用传入的参数*/
	defaultParams: {},

	//接收传入参数
	getParams: function() {
		var me = this;
		var params = JShell.Page.getParams();
		//His病区Id
		if (params["wardId"]) me.defaultParams.WardId = params["wardId"];
		//his科室Id
		if (params["deptId"]) me.defaultParams.DeptId = params["deptId"];
		//his医生Id/his护士Id
		if (params["doctorId"]) me.defaultParams.DoctorId = params["doctorId"];
		//His的就诊号
		if (params["admId"]) me.defaultParams.AdmId = params["admId"];
		me.ADMID = me.defaultParams.AdmId;
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListenersPanel();
	},
	initComponent: function() {
		var me = this;
		me.getParams();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var maxWidth = document.body.clientWidth;
		var width2 = 280;
		if (maxWidth > 1366) {
			width2 = 460;
		}
		var width1 = maxWidth - width2;
		//输血过程记录项信息
		me.ShowPanel = Ext.create('Shell.class.blood.nursestation.transrecord.search.ShowPanel', {
			region: 'center',
			header: false,
			width: width1,
			itemId: 'ShowPanel',
			/**His调用传入的就诊号*/
			AdmId: me.ADMID,
			split: true,
			collapsible: false
		});
		//不良反应相关信息
		me.AdverseReactionsPanel = Ext.create("Shell.class.blood.nursestation.transrecord.adversereaction.ShowPanel", {
			region: 'east',
			width: width2,
			header: false,
			//border: false,
			split: true,
			itemId: 'AdverseReactionsPanel'
		});
		return [me.ShowPanel, me.AdverseReactionsPanel];
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
	clearData: function() {
		var me = this;
		me.AdverseReactionsPanel.PK = null;
		me.AdverseReactionsPanel.formtype = "show";
		//me.ShowPanel.clearData();
		me.AdverseReactionsPanel.clearData();
	},
	loadData: function(record) {
		var me = this;
		var id = record.get("BloodBOutItem_BloodTransForm_Id");
		me.AdverseReactionsPanel.formtype = "show";
		me.AdverseReactionsPanel.PK = id;
		me.AdverseReactionsPanel.loadData();
	},
	/*列表事件监听**/
	onListenersPanel: function() {
		var me = this;
		me.ShowPanel.on({
			select: function(p, record) {
				me.clearData();
				me.loadData(record);
			},
			nodata: function(p) {
				me.clearData();
			},
			save: function(p, record) {
				//me.ShowPanel.onSearch();
			}
		});
	}
});
