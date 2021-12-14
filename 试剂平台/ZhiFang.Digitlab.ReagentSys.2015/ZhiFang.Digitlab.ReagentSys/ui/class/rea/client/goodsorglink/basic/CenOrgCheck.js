/**
 * 机构选择列表
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.CenOrgCheck', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '机构选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 525,
	height: 400,
	/**是否单选*/
	checkOne: false,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaCenOrg_OrgType',
		direction: 'ASC'
	},{
		property: 'ReaCenOrg_OrgNo',
		direction: 'ASC'
	}],
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reacenorg.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '机构名称/机构编码/英文名',
			fields: ['reacenorg.CName', 'reacenorg.EName', 'reacenorg.OrgNo']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaCenOrg_OrgType',
			text: '机构类型',
			width: 80,
			renderer: function(value, meta, record, rowIndex, colIndex) {
				var v = "";
				if(value == "0") {
					v = "供货方";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "订货方";
					meta.style = "color:orange;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},  {
			dataIndex: 'ReaCenOrg_CName',
			text: '机构名称',
			//flex: 1,
			minWidth: 260,
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_OrgNo',
			text: '机构编码',
			width: 90,
			type: 'int',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_EName',
			text: '英文名',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			OrgType = buttonsToolbar.getComponent('ReaCenOrgCenOrgType'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(OrgType && OrgType.getValue()) {
			params.push('reacenorg.OrgType=' + OrgType.getValue());
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	},
	initButtonToolbarItems: function() {
		var me = this;

		me.callParent(arguments);
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		var index=0;
		if(me.checkOne==true)index=1;
		me.buttonToolbarItems.splice(index, 0, {
			fieldLabel: ' 机构类型',
			width: 140,
			labelWidth: 65,
			name: 'ReaCenOrgCenOrgType',
			itemId: 'ReaCenOrgCenOrgType',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			style: {
				marginRight: "10px"
			},
			data: [
				['', '全部'],
				['0', '供货方'],
				['1', '订货方']
			],
			listeners: {
				change: function(newValue, oldValue) {
					me.onSearch();
				}
			}
		});
	}
});