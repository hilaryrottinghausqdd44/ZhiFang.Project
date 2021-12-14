/**
 * 仪器列表
 * @author longfc
 * @version 2015-09-29
 */
Ext.define('Shell.class.sysbase.bequip.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],

	title: '仪器列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	//selectUrl: '/ui/class/sysbase/bequip/test/list.json',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBEquipByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateBEquipByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelBEquip',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	//multiSelect: true,
	//selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否启用查看按钮*/
	hasShow: true,
	checkOne: true,
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BEquip_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddClick', me);
		me.addEvents('onEditClick', me);
		me.addEvents('onShowClick', me);
		me.addEvents('onInteractionClick', me);
		me.addEvents('onHistoryVersionClick', me);
		me.addEvents('onOperationRecordClick', me);

		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '仪器名称/型号/SQH号',
			isLike: true,
			fields: ['bequip.CName', 'bequip.Equipversion', 'bequip.Shortcode']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '名称',
			dataIndex: 'BEquip_CName',
			flex: 1,
			minWidth: 235,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '仪器分类',
			dataIndex: 'BEquip_EquipType_CName',
			width: 75,
			hideable: false
		}, {
			text: '仪器品牌',
			width: 85,
			dataIndex: 'BEquip_EquipFactoryBrand_CName',
			hideable: false
		}, {
			text: '型号',
			dataIndex: 'BEquip_Equipversion',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'SQH号',
			dataIndex: 'BEquip_Shortcode',
			width: 50,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'BEquip_IsUse',
			width: 40,
			hidden: false,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '创建时间',
			dataIndex: 'BEquip_DataAddTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '备注',
			dataIndex: 'BEquip_Memo',
			width: 150,
			sortable: false,
			hidden: true,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '次序',
			dataIndex: 'BEquip_DispOrder',
			width: 65,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}, {
			text: '主键ID',
			dataIndex: 'BEquip_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '仪器分类ID',
			dataIndex: 'BEquip_EquipType_Id',
			hidden: true,
			hideable: false
		}, {
			text: '仪器品牌ID',
			dataIndex: 'BEquip_EquipFactoryBrand_Id',
			hidden: true,
			hideable: false
		}];
		//查看操作列
		columns.push(me.createShowColumn());
		columns.push(me.createInteraction());
		columns.push(me.createHistoryVersion());
		columns.push(me.createOperationRecord());
		return columns;
	},
	/*创建交流列**/
	createInteraction: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '交流',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('onInteractionClick', grid, rec, rowIndex, colIndex);
				}
			}]
		};
	},

	/*创建交流列**/
	createShowColumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '查看',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('onShowClick', grid, rec, rowIndex, colIndex);
				}
			}]
		};
	},
	/*查看该仪器历史版本**/
	createHistoryVersion: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '历史版本',
			align: 'center',
			width: 55,
			hidden: false,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('onHistoryVersionClick', grid, rec, rowIndex, colIndex);

				}
			}]
		};
	},
	/*创建查看该仪器操作记录列**/
	createOperationRecord: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '操作记录',
			align: 'center',
			width: 55,
			hidden: false,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('onOperationRecordClick', grid, rec, rowIndex, colIndex);

				}
			}]
		};
	},

	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get('BEquip_IsUse');
			me.updateOneByIsUse(i, id, IsUse);
		}
	},
	updateOneByIsUse: function(index, id, IsUse) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsUse: IsUse
			},
			fields: 'Id,IsUse'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', this);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	onShowClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onShowClick', me, records[0]);
	}
});