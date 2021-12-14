/**
 * 仪器列表
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.statistics.consume.basic.EquipGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '仪器列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?isPlanish=true',
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaTestEquipLab_DispOrder',
		direction: 'ASC'
	}],
	/**仪器是否多选*/
	isMultiple: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function(p) {
				me.getView().update();
				var obj = {
					ReaTestEquipLab_CName: '全部仪器',
					ReaTestEquipLab_LisCode: '',
					ReaTestEquipLab_Id: ''
				};
				me.store.insert(me.getStore().getCount(), obj);
			}
		});
	},
	initComponent: function() {
		var me = this;
		//默认只显示在用的仪器
		me.externalWhere = "reatestequiplab.Visible=1";
		//查询框信息
		me.searchInfo = {
			width: 105,
			emptyText: 'Lis编码/仪器名称',
			isLike: true,
			itemId: 'Search',
			fields: ['reatestequiplab.LisCode', 'reatestequiplab.CName']
		};
		//'refresh', 
		me.buttonToolbarItems = [{
			boxLabel: '多仪器',
			name: 'cbsIsMultiple',
			itemId: 'cbsIsMultiple',
			xtype: 'checkboxfield',
			inputValue: false,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onIsMultChoose(com, newValue, oldValue);
				}
			}
		}, '-',{
			boxLabel: '包含禁用',
			name: 'cbsIsVisible',
			itemId: 'cbsIsVisible',
			xtype: 'checkboxfield',
			inputValue: false,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if (newValue == true) {
						me.externalWhere = "";
					} else {
						me.externalWhere = "reatestequiplab.Visible=1";
					}
					me.onSearch();
				}
			}
		}, {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: 'btnMultChoose',
			text: '选择',
			tooltip: '仪器选择',
			hidden: true,
			handler: function() {
				me.onMultChoose();
			}
		}, '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestEquipLab_LisCode',
			text: 'LIS编码',
			width: 75,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_CName',
			text: '仪器名称',
			flex: 1,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaTestEquipLab_DeptName',
			text: '所属部门',
			width: 80,
			defaultRenderer: true
		}, ];
		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var result = {},
			list = [],
			arr = [],
			obj = {};
		//添加全部行
		obj = {
			ReaTestEquipLab_CName: '全部仪器',
			ReaTestEquipLab_LisCode: '',
			ReaTestEquipLab_Id: ''
		};
		if (data.value) {
			list = data.value.list;
			list.splice(0, 0, obj);
		} else {
			list = [];
			list.push(obj);
		}
		result.list = data.value.list;
		return result;
	},
	onIsMultChoose: function(com, newValue, oldValue) {
		var me = this;
		me.isMultiple = newValue;
		var btnMult = me.getComponent('buttonsToolbar').getComponent('btnMultChoose');
		var search = me.getComponent('buttonsToolbar').getComponent('Search');
		if (btnMult) {
			btnMult.setVisible(newValue);
		}
		if (search) {
			search.setVisible(!newValue);
		}
		if (newValue == false) {
			me.onSearch();
		} else {
			me.store.removeAll();
		}
	},
	onMultChoose: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		var defaultWhere = "";
		JShell.Win.open('Shell.class.rea.client.equip.choose.App', {
			resizable: true,
			width: maxWidth,
			height: height,
			leftDefaultWhere: defaultWhere,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onAccept(p, records);
				}
			}
		}).show();
	},
	/**仪器选择后*/
	onAccept: function(p, records) {
		var me = this;
		me.store.removeAll();
		if (!records || records.length <= 0) {
			p.close();
			return;
		}
		var list = [];
		for (var i in records) {
			var data = {
				"ReaTestEquipLab_Id": records[i].get('ReaTestEquipLab_Id'),
				"ReaTestEquipLab_CName": records[i].get('ReaTestEquipLab_CName'),
				"ReaTestEquipLab_LisCode": records[i].get('ReaTestEquipLab_LisCode'),
			};
			list.push(data);
		}
		me.store.loadData(list);
		p.close();
	}
});
