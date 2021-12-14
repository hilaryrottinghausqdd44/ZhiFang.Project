/**
 * 输血申请综合查询入口
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '输血申请综合查询',
	/**内容周围距离*/
	bodyPadding: '2px',
	/**His调用传入病区编码*/
	HISWARDID: "",
	/**按His调用传入病区编码获取到的病区编码*/
	WARDID: "",
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
		me.WARDID = me.defaultParams.WardId;
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
		var clientWidth = document.body.clientWidth;
		var width1 = 340;
		if (clientWidth > 1366) {
			width1 = 380;
		}
		var width2 = clientWidth - width1;
		//输血申请主单列表
		me.ReqDocGrid = Ext.create('Shell.class.blood.statistics.reqntegrated.req.DocGrid', {
			region: 'west',
			width: width1,
			header: true,
			itemId: 'ReqDocGrid',
			split: true,
			collapsible: false,
			wardId: me.WARDID
		});
		//右侧区域信息
		me.TabPanel = Ext.create("Shell.class.blood.statistics.reqntegrated.TabPanel", {
			region: 'center',
			width: width2,
			header: false,
			itemId: 'TabPanel'
		});
		return [me.ReqDocGrid, me.TabPanel];
	},
	/*列表事件监听**/
	onListenersPanel: function() {
		var me = this;
		me.ReqDocGrid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 300);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 300);
			},
			nodata: function(p) {
				me.onNodata();
			}
		});
	},
	loadData: function(record) {
		var me = this;
		me.TabPanel.loadData(record);
	},
	onNodata: function() {
		var me = this;
		me.TabPanel.onNodata();
	}
});
