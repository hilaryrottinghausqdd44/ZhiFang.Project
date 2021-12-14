/**
 * @description 订单上传
 * @author longfc
 * @version 2017-11-21
 */
Ext.define('Shell.class.rea.client.order.uploadplatform.OrderGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderGrid',

	title: '订单上传',
	width: 800,
	height: 500,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**录入:entry/审核:check*/
	OTYPE: "upload",
	/**下拉状态默认值*/
	defaultStatusValue: "3",
	/**是否多选行*/
	checkOne: true,
	/**数据标志默认值*/
	defaultIOFlagValue: "0",
	/**用户UI配置Key*/
	userUIKey: 'order.uploadplatform.OrderGrid',
	/**用户UI配置Name*/
	userUIName: "订单上传列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-10);
		//系统运行参数"数据库是否独立部署"
		JcallShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false, null);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = '(reabmscenorderdoc.Status!=0 and reabmscenorderdoc.Status!=1 and reabmscenorderdoc.Status!=2)';

		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'ReaBmsCenOrderDoc_LabID',
			text: 'LabID',
			hidden: true,
			defaultRenderer: true
		});
		return columns;
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);
		//申请
		if(tempList[2]) itemArr.push(tempList[2]);
		//审核退回
		if(tempList[3]) itemArr.push(tempList[3]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	}
});