/**
 * 费用项目列表
 * @author longfc
 * @version 2020-07-07
 */
Ext.define('Shell.class.sysbase.chargeitem.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '费用项目列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeItem',

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
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BloodChargeItem_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'chargeitem.Grid',
	/**用户UI配置Name*/
	userUIName: "费用项目列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '名称',
			isLike: true,
			fields: ['bloodchargeitem.CName']
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
			text: '编号',
			dataIndex: 'BloodChargeItem_Id',
			isKey: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'BloodChargeItem_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'BloodChargeItem_ShortCode',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		},  {
			text: '<b style="color:blue;">系数</b>',
			dataIndex: 'BloodChargeItem_Modulus',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			text: '<b style="color:blue;">入价</b>',
			dataIndex: 'BloodChargeItem_InPrice',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			text: '<b style="color:blue;">出价</b>',
			dataIndex: 'BloodChargeItem_OutPrice',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			text: '<b style="color:blue;">是否组合</b>',
			xtype: 'checkcolumn',
			dataIndex: 'BloodChargeItem_IsGroup',
			width: 65,
			align: 'center',
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '<b style="color:blue;">使用</b>',
			xtype: 'checkcolumn',
			dataIndex: 'BloodChargeItem_IsUse',
			width: 40,
			align: 'center',
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '<b style="color:blue;">显示次序</b>',
			dataIndex: 'BloodChargeItem_DispOrder',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			text: '创建时间',
			dataIndex: 'BloodChargeItem_DataAddTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '费用规格',
			dataIndex: 'BloodChargeItem_ChargeItemSpec',
			width: 180,
			sortable: false,
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
			me.updateOne(i, rec);
		}
	},
	updateOne: function(index,rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var Modulus = rec.get('BloodChargeItem_Modulus');
		var InPrice = rec.get('BloodChargeItem_InPrice');
		var OutPrice = rec.get('BloodChargeItem_OutPrice');
		var DispOrder = rec.get('BloodChargeItem_DispOrder');
		var IsUse = rec.get('BloodChargeItem_IsUse');
		var IsGroup = rec.get('BloodChargeItem_IsGroup');
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
				IsGroup:IsGroup,
				Modulus: Modulus,
				InPrice: InPrice,
				OutPrice: OutPrice,
				DispOrder: DispOrder
			},
			fields: 'Id,IsGroup,IsUse,DispOrder,Modulus,InPrice,OutPrice'
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
			defaultWhere: "scoperation.BusinessModuleCode='BloodChargeItem'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
});
