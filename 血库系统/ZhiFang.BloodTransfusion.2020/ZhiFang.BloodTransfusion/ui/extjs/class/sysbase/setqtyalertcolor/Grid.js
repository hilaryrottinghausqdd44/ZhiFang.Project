/**
 * 预警颜色列表
 * @author xiehz
 * @version 2020-08-03
 */

Ext.define('Shell.class.sysbase.setqtyalertcolor.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '预警颜色',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertColorByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSetQtyAlertColorByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodSetQtyAlertColor',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
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
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询框信息*/
	searchInfo: {
		width: 220,
		emptyText: '库存预警名称',
		isLike: true,
		fields: ['bloodsetqtyalertcolor.AlertName']
	},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		//创建插件网格才能修改
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '预警编号',
			dataIndex: 'BloodSetQtyAlertColor_Id',
			isKey: true,
			hideable: false
		},{
			text: '库存预警名称',
			dataIndex: 'BloodSetQtyAlertColor_AlertName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '预警分类',
			dataIndex: 'BloodSetQtyAlertColor_AlertTypeCName',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '预警颜色',
			dataIndex: 'BloodSetQtyAlertColor_AlertColor',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '下限值',
			dataIndex: 'BloodSetQtyAlertColor_StoreLower',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '上限值',
			dataIndex: 'BloodSetQtyAlertColor_StoreUpper',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '<b style="color:blue;">显示次序</b>',
			type:'int',
			dataIndex: 'BloodSetQtyAlertColor_DispOrder',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				xtype: 'numberfield'
			}
		},{
			text: '是否使用',
			type: 'boolean',
			//xtype: 'checkcolumn', //修改boolean列
     		dataIndex:'BloodSetQtyAlertColor_Visible',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}];
		return columns;
	},
	
	updateOneByVisible: function(index, id, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var DispOrder = rec.get('BloodSetQtyAlertColor_DispOrder');
		var Visible = rec.get('BloodSetQtyAlertColor_Visible');
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				Visible: Visible,
				DispOrder: DispOrder
			},
			fields: 'Id,Visible,DispOrder'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	
    /**@overwrite 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			me.updateOneByVisible(i, id, rec);
		}
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('editclick', me, records[0]);
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.ApplyGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.BloodTransfusion', //类域
			className: 'UpdateOperationType', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='BloodSetQtyAlertColor'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
})