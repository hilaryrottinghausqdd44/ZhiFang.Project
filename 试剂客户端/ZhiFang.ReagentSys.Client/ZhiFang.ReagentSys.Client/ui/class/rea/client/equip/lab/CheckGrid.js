/**
 * 仪器选择列表
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.equip.lab.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '仪器选择列表',
	width: 450,
	height: 350,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?isPlanish=true',
	/**是否单选*/
	checkOne: false,
	/**用户UI配置Key*/
	userUIKey: 'equip.lab.CheckGrid',
	/**用户UI配置Name*/
	userUIName: "仪器选择列表",

	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 155,
			emptyText: '仪器名称/英文名称/代码',
			isLike: true,
			itemId: 'Search',
			fields: ['reatestequiplab.CName', 'reatestequiplab.EName', 'reatestequiplab.ShortCode']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaTestEquipLab_CName',
			text: '仪器名称',
			flex: 1,
			minWidth: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}];

		return columns;
	}
});