/**
 * 库存变化跟踪
 * @author longfc
 * @version 2018-03-15
 */
Ext.define('Shell.class.rea.client.qtyoperation.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '库存变化跟踪',
	width: 700,
	height: 480,

	/**内容周围距离*/
	bodyPadding: '1px',
	/**对外公开，1显示所有部门树，0只显示用户自己的树*/
	ISOWN: '0',
	/**库房是否按库房员工权限获取*/
	isEmpPermission: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.initIsUseEmp();
		//内部组件
		me.items = me.createItems();

		me.callParent(arguments);
	},
	onListeners: function() {
		var me = this;
		me.Tree.on({
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
	initIsUseEmp: function() {
		var me = this;
		//系统运行参数"是否启用库存库房权限":1:是;2:否;
		var isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
		if (!isUseEmp) {
			JShell.REA.RunParams.getRunParamsValue("ReaBmsQtyDtlIsUseEmp", false, function(data) {
				isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
				if (isUseEmp && (isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true")) {
					me.isEmpPermission = true;
				}
			});
		} else {
			isUseEmp = "" + isUseEmp;
			if (isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true") {
				me.isEmpPermission = true;
			}
		}
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Tree = Ext.create('Shell.class.rea.client.shelves.Tree', {
			region: 'west',
			width: 300,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		});

		me.Grid = Ext.create('Shell.class.rea.client.qtyoperation.Grid', {
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
		if (leaf == true) { //货架
			hql = 'reabmsqtydtloperation.PlaceID=' + id;
		} else { //库房
			if (id == 0 || id == "0") {
				if (me.isEmpPermission) {
					var tidArr = [];
					var childNodes = record.childNodes;
					//找出根节点下的所有库房信息
					me.Tree.getRootNode().eachChild(function(child) {
						if (child && child.data)
							tidArr.push(child.data.tid)
					});
					hql = 'reabmsqtydtloperation.StorageID in (' + tidArr.join(',') + ")";
				}
			} else {
				hql = 'reabmsqtydtloperation.StorageID=' + id;
			}
		}
		me.Grid.externalWhere = hql;
		me.Grid.onSearch();
	}
});
