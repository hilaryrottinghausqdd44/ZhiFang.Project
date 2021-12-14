/**
 * 库存标志维护
 * @author longfc
 * @version 2019-07-09
 */
Ext.define('Shell.class.rea.client.reastore.qtydtlmark.App', {
	extend: 'Ext.panel.Panel',

	title: '库存标志维护',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	/**库房是否按库房员工权限获取*/
	isEmpPermission: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Tree = me.getComponent('Tree');
		Tree.on({
			itemclick: function(v, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					me.setExternalWhere(record);
				}, null, 500);
			},
			select: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.setExternalWhere(record);
				}, null, 500);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.initIsUseEmp();
		//内部组件
		me.items = me.createItems();

		me.callParent(arguments);
	},
	initIsUseEmp: function() {
		var me = this;
		//系统运行参数"是否启用库存库房权限":1:是;2:否;
		var isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
		if(!isUseEmp) {
			JShell.REA.RunParams.getRunParamsValue("ReaBmsQtyDtlIsUseEmp", false, function(data) {
				isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
				if(isUseEmp && (isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true")) {
					me.isEmpPermission = true;
				}
			});
		} else {
			isUseEmp = "" + isUseEmp;
			if(isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true") {
				me.isEmpPermission = true;
			}
		}
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Tree = Ext.create('Shell.class.rea.client.shelves.Tree', {
			region: 'west',
			width: 265,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true,
			/**是否显示根节点*/
			rootVisible: false
		});

		me.Grid = Ext.create('Shell.class.rea.client.reastore.qtydtlmark.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Tree, me.Grid];
	},
	setExternalWhere: function(record) {
		var me = this;
		var hql = "";
		//判断选择行的是库房还是货架
		var leaf = record.get('leaf');
		var id = record.get('tid');
		me.Grid.StorageID = null;
		if(leaf == true) { //货架
			me.Grid.StorageID = record.parentNode.get('tid')
			hql = 'reabmsqtydtl.StorageID=' + me.Grid.StorageID+' and reabmsqtydtl.PlaceID=' + id;
			parentNode
		} else { //库房
			me.Grid.StorageID = id;
			hql = 'reabmsqtydtl.StorageID=' + id;
		}
		me.Grid.externalWhere = hql;
		me.Grid.onSearch();
	}
});