/**
 * 数据过滤条件列表
 * @author longfc
 * @version 2017-05-04
 */
Ext.define('Shell.class.sysbase.rowfilter.basic.DatafiltersGrid', {
	extend: 'Ext.grid.Panel',
	title: '数据过滤条件列表',
	width: 280,
	height: 380,
	//新插入的行是在选中的之前插入还是在选中行之后插入;1为选中行前;2 为选中行后
	chooseCheck: 2,
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),
	initComponent: function() {
		var me = this;
		me.addEvents('addRecordClick');
		me.addEvents('addOrClick');
		me.addEvents('btnSelect');
		me.dockedItems = me.createDockedItems();
		//数据列
		me.columns = me.createGridColumns();
		me.store = Ext.create('Ext.data.Store', {
			fields: ['InteractionField', 'CName', 'InteractionFieldTwo', 'CNameTwo', 'LogicalType', 'ColumnTypeList', 'OperationType', 'OperationName', 'Content', 'ContentTwo'],
			data: [],
			autoLoad: true,
			listeners: {
				load: function(store, records, successful) {}
			}
		});
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createButtontoolbar());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'radiogroup',
			itemId: 'radiogroupChoose',
			columns: 2,
			width: 145,
			vertical: true,
			items: [{
				boxLabel: "行前插入",
				name: 'choose',
				inputValue: 1,
				checked: false
			}, {
				boxLabel: "行后插入",
				name: 'choose',
				inputValue: 2,
				checked: true
			}],
			listeners: {
				change: {
					fn: function(rdgroup, checked) {
						me.chooseCheck = checked.choose;
					}
				}
			}
		}, '-', {
			xtype: 'button',
			text: '增加属性',
			itemId: 'btnAddRecord',
			cls: "btn btn-default btn-sm active",
			style: {
				marginLeft: '10px'
			},
			listeners: {
				click: function(com, e, eOpts) {
					me.fireEvent('addRecordClick');
				}
			}
		}, {
			xtype: 'button',
			text: '并且关系',
			itemId: 'btnaddAnd',
			cls: "btn btn-default btn-sm active",
			listeners: {
				click: function(com, e, eOpts) {
					me.fireEvent('addAndClick');
				}
			}
		}, {
			xtype: 'button',
			text: '或关系',
			itemId: 'btnaddOr',
			cls: "btn btn-default btn-sm active",
			listeners: {
				click: function(com, e, eOpts) {
					me.fireEvent('addOrClick');
				}
			}
		}, {
			xtype: 'button',
			text: '左括号',
			//hidden: true,
			itemId: 'addLeftBracket',
			//iconCls: 'button-add',
			cls: "btn btn-default btn-sm active",
			listeners: {
				click: function(com, e, eOpts) {
					me.fireEvent('addLeftBracketClick');
				}
			}
		}, {
			xtype: 'button',
			text: '右括号',
			itemId: 'addRightBracket',
			//iconCls: 'button-add',
			cls: "btn btn-default btn-sm active",
			listeners: {
				click: function(com, e, eOpts) {
					me.fireEvent('addRightBracketClick');
				}
			}
		}, '-', {
			xtype: 'button',
			text: '查看执行条件',
			itemId: 'btnSelect',
			iconCls: 'button-search',
			cls: "btn btn-default btn-sm active",
			style: {
				marginRight: '10px'
			},
			listeners: {
				click: function(com, e, eOpts) {
					me.fireEvent('btnSelect');
				}
			}
		}, {
			xtype: 'button',
			text: '维护预定义可选属性',
			hidden: true,
			iconCls: 'button-edit',
			itemId: 'btnPredefinedAttributes',
			cls: "btn btn-default btn-sm active",
			style: {
				marginLeft: '10px'
			},
			listeners: {
				click: function(com, e, eOpts) {
					me.openPredefinedAttributesWin();
				}
			}
		}, '->');
		var toolbar = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'buttomToolbar',
			items: items
		};
		return toolbar;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
				xtype: "actioncolumn",
				sortable: false,
				text: "操作",
				width: 60,
				align: "center",
				itemId: "Action",
				items: [{
					iconCls: "build-button-delete hand",
					tooltip: "删除",
					handler: function(grid, rowIndex, colIndex, item, e, record) {
						me.fireEvent('deleteClick', me, record, rowIndex);
					}
				}]
			},
			{
				text: '值类型',
				dataIndex: 'ColumnTypeList',
				sortable: false,
				hidden: true,
				width: 100
			},
			{
				text: '属性名称',
				hidden: false,
				sortable: false,
				width: 130,
				dataIndex: 'CName',
				renderer: function(value, meta, record, rowIndex, columnIndex, store) {
					me.setCellMetaStyle(meta, record);
					return value;
				}
			}, {
				text: '属性交互字段',
				hidden: true,
				width: 100,
				sortable: false,
				dataIndex: 'InteractionField'
			},
			{
				text: '关系',
				sortable: false,
				width: 120,
				dataIndex: 'OperationName',
				renderer: function(value, meta, record, rowIndex, columnIndex, store) {
					me.setCellMetaStyle(meta, record);
					return value;
				}
			}, {
				text: '关系运算符',
				dataIndex: 'OperationType',
				sortable: false,
				hidden: true,
				width: 100
			}, {
				text: '逻辑关系',
				dataIndex: 'LogicalType',
				sortable: false,
				hidden: true,
				width: 20
			},
			{
				text: '输入内容',
				dataIndex: 'Content',
				minWidth: 220,
				flex: 1,
				sortable: false,
				editor: {
					allowBlank: false,
					listeners: {
						change: function(com, newValue) {
							me.store.sync(); //与后台数据同步
						}
					}
				},
				renderer: function(value, meta, record, rowIndex, columnIndex, store) {
					me.setCellMetaStyle(meta, record);
					return value;
				}
			},
			{
				text: '对比值二',
				sortable: false,
				width: 115,
				dataIndex: 'CNameTwo'
			}, {
				text: '对比值二交互字段',
				hidden: true,
				sortable: false,
				dataIndex: 'InteractionFieldTwo'
			},
			{
				text: '输入内容二',
				dataIndex: 'ContentTwo',
				width: 140,
				sortable: false,
				editor: {
					allowBlank: false,
					listeners: {
						change: function(com, newValue) {
							me.store.sync(); //与后台数据同步
						}
					}
				}
			}
		];
		return columns;
	},
	setCellMetaStyle: function(meta, record) {
		switch(record.get("LogicalType")) {
			case "and":
			case "or":
			case "(":
			case ")":
				meta.style = 'color:red';
				break;
			default:
				break;
		}
	},
	/**
	 * 打开预定义可选属性设置页面
	 * @param {Object} paramObj
	 */
	openPredefinedAttributesWin: function() {
		var me = this;
		if(me.objectName) {
			var roleLists = [];
			var maxWidth = 360; // document.body.clientWidth * 0.42;
			var maxHeight = document.body.clientHeight * 0.96;
			var config = {
				width: maxWidth,
				height: maxHeight,
				maxWidth: maxWidth,
				maxHeight: maxHeight,
				moduleOperId: me.moduleOperId,
				objectName: me.objectName,
				objectCName: me.objectCName,
				layout: 'border',
				closable: true, //有关闭按钮
				draggable: true,
				listeners: {
					onsave: function(p, data) {
						if(data.success) {
							win.close();
						} else {
							JShell.Msg.error("预定义可选属性保存操作失败!<br />" + data.msg);
						}
					},
					oncancel: function() {
						win.close();
					}
				}
			};
			var win = JShell.Win.open('Shell.class.sysbase.rowfilter.datafilters.tree.PredefinedAttributesTree', config).show();

		} else {
			JShell.Msg.alert('获取不到模块操作的数据对象', null, 1000);
		}
	}
});