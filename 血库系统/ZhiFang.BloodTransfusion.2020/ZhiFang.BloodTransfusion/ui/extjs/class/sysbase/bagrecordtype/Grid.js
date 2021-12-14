/**
 * 血袋记录类型
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.bagrecordtype.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',

	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '血袋记录类型',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordTypeByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagRecordType',

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
		property: 'BloodBagRecordType_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'transrecordtype.Grid',
	/**用户UI配置Name*/
	userUIName: "输血过程记录分类列表",

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
			emptyText: '编号/名称',
			isLike: true,
			fields: ['bloodbagrecordtype.TypeCode', 'bloodbagrecordtype.CName']
		};

		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '编号',
			dataIndex: 'BloodBagRecordType_Id',
			isKey: true,
			width: 150,
			hideable: false
		}, {
			dataIndex: 'BloodBagRecordType_ContentTypeID',
			text: '内容分类',
			width: 120,
			renderer: function(value, meta) {
				var v = "";
				if (value == "1") {
					v = "输血记录项";
					meta.style = "color:green;";
				} else if (value == "2") {
					v = "不良反应分类";
					meta.style = "color:orange;";
				} else if (value == "3") {
					v = "临床处理措施";
					meta.style = "color:black;";
				} else if (value == "4") {
					v = "不良反应选择项";
					meta.style = "color:black;";
				} else if (value == "5") {
					v = "临床处理结果";
					meta.style = "color:black;";
				} else if (value == "6") {
					v = "临床处理结果描述";
					meta.style = "color:black;";
				} else if (value == "7") {
					v = "入库核对";
					meta.style = "color:black;";
				} else if (value == "8") {
					v = "库存检查";
					meta.style = "color:black;";
				} else if (value == "9") {
					v = "配血登记";
					meta.style = "color:black;";
				} else if (value == "10") {
					v = "出库领用";
					meta.style = "color:black;";
				} else if (value == "11") {
					v = "血袋交接";
					meta.style = "color:black;";
				} else if (value == "12") {
					v = "血袋回收";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'BloodBagRecordType_TransTypeId',
			text: '输血记录类型',
			width: 120,
			renderer: function(value, meta) {
				var v = "";
				if (value == "1") {
					v = "输血结束前";
					meta.style = "color:green;";
				} else if (value == "2") {
					v = "输血结束";
					meta.style = "color:orange;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '类型编码',
			dataIndex: 'BloodBagRecordType_TypeCode',
			width: 140,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'BloodBagRecordType_CName',
			width: 140,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '<b style="color:blue;">使用</b>',
			xtype: 'checkcolumn',
			dataIndex: 'BloodBagRecordType_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '<b style="color:blue;">显示次序</b>',
			dataIndex: 'BloodBagRecordType_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			text: '创建时间',
			dataIndex: 'BloodBagRecordType_DataAddTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '备注',
			dataIndex: 'BloodBagRecordType_Memo',
			width: 200,
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
			me.updateOne(i,rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var DispOrder = rec.get('BloodBagRecordType_DispOrder');
		var IsUse = rec.get('BloodBagRecordType_IsUse');
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
				DispOrder: DispOrder
			},
			fields: 'Id,IsUse,DispOrder'
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
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var len = data.list.length;
		for (var i = 0; i < len; i++) {
			var isVisible = data.list[i].BloodBagRecordType_IsUse;
			if (isVisible == 'True' || isVisible ==
				'true' || isVisible == '1' || isVisible == 1) {
				isVisible = true;
			} else {
				isVisible = false;
			}
			data.list[i].BloodBagRecordType_IsUse = isVisible;
		}
		return data;
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
			defaultWhere: "scoperation.BusinessModuleCode='BloodBagRecordType'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
});
