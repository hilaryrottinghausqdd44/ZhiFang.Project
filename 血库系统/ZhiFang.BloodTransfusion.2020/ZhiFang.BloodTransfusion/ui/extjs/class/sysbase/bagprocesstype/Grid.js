/**
 * 加工类型
 * @author longfc
 * @version 2020-07-10
 */
Ext.define('Shell.class.sysbase.bagprocesstype.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '加工类型',
	width: 800,
	height: 500,
	
	//未修
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessTypeByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagProcessType',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 100,
	
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
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BloodBagProcessType_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'bagprocesstype.Grid',
	/**用户UI配置Name*/
	userUIName: "加工类型列表",
	
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
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '编码/名称',
			isLike: true,
			fields: ['BloodBagProcessType_Id', 'BloodBagProcessType_CName']
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
			text: '加工编号',
			dataIndex: 'BloodBagProcessType_Id',
			isKey: true,//是否为主键
			width: 100,
			hideable: false//使用false，用户无法隐藏该列
		},{
			text: '编码',
			dataIndex: 'BloodBagProcessType_BloodClass',
			width: 160,
			menuDisabled: true,//设置为false则禁用列菜单
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'BloodBagProcessType_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'BloodBagProcessType_SName',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'BloodBagProcessType_ShortCode',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},
		{
			text: '<b style="color:blue;">使用</b>',
			xtype: 'checkcolumn',
			dataIndex: 'BloodBagProcessType_IsUse',
			width: 40,
			align: 'center',
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		},{
			text: '<b style="color:blue;">显示次序</b>',
			dataIndex: 'BloodBagProcessType_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		} ,{
			text: '创建时间',
			dataIndex: 'BloodBagProcessType_DataAddTime',
			width: 130,
			isDate: true,
			hasTime: true
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
			me.updateOne(i, id, rec);
		}
	},
	updateOne: function(index,rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var DispOrder = rec.get('BloodBagProcessType_DispOrder');
		var IsLargeUse = rec.get('BloodBagProcessType_IsLargeUse');
		var IsUse = rec.get('BloodBagProcessType_IsUse');
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		var params = Ext.JSON.encode({
			empID:empID,
			empName:empName,
			entity: {
				Id: id,
				IsUse: IsUse,
				IsLargeUse:IsLargeUse,
				DispOrder: DispOrder
			},
			fields: 'Id,IsLargeUse,IsUse,DispOrder'
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
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
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
			defaultWhere: "scoperation.BusinessModuleCode='BloodBagProcessType'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
});
