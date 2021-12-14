/**
 * 收费类型费用项目关系列表
 * @author longfc	
 * @version 2020-08-08
 */
Ext.define('Shell.class.sysbase.chargeitemlink.LinkGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '收费类型费用项目关系列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemLinkByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeItemLink',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemLinkByField',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeItemLink',
	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodChargeItemLink_DispOrder',
		direction: 'ASC'
	}],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**是否启用新增按钮*/
	hasAdd: true,
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	
	/**默认每页数量*/
	defaultPageSize: 50,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**收费类型ID*/
	ChargeTypeID: null,
	/**用户UI配置Key*/
	userUIKey: 'chargeitemlink.LinkGrid',
	/**用户UI配置Name*/
	userUIName: "收费类型费用项目关系列表",

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
			width: 225,
			emptyText: '拼音字头/名称/英文名称',
			isLike: true,
			itemId: 'Search',
			fields: ['bloodchargeitemlink.BloodChargeItem.CName', 'bloodchargeitemlink.BloodChargeItem.PinYinZiTou',
				'bloodchargeitemlink.BloodChargeItem.EName'
			]
		};
		/* me.buttonToolbarItems = ['refresh', '-', 'add', 'del', '->', {
			type: 'search',
			info: me.searchInfo
		}]; */
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BloodChargeItemLink_BloodChargeItem_Id',
			text: '费用项目编号',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'BloodChargeItemLink_BloodChargeItem_CName',
			text: '费用项目',
			minWidth: 150,
			//flex: 1,
			defaultRenderer: true
		}, {
			text: '<b style="color:blue;">费用系数</b>',
			dataIndex: 'BloodChargeItemLink_Modulus',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			editor: {
				xtype: 'numberfield'
			}
		},{
			text: '<b style="color:blue;">使用</b>',
			xtype: 'checkcolumn',
			dataIndex: 'BloodChargeItemLink_IsUse',
			width: 40,
			align: 'center',
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '<b style="color:blue;">显示次序</b>',
			dataIndex: 'BloodChargeItemLink_DispOrder',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			dataIndex: 'BloodChargeItemLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = me.getComponent("buttonsToolbar").getComponent("Search").getValue(),
			params = [];
		me.internalWhere = '';
		if (me.ChargeTypeID) {
			params.push('bloodchargeitemlink.ChargeType.Id=' + me.ChargeTypeID);
		}
		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if (search) {
			if (me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = "(" + me.getSearchWhere(search) + ")";
			}
		}
		return me.callParent(arguments);
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if (!me.ChargeTypeID) {
			JShell.Msg.error('收费类型不能为空');
			return;
		}
		me.load(null, true, autoSelect);
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
			me.updateOne(i,  rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var DispOrder = rec.get('BloodClass_DispOrder');
		var IsUse = rec.get('BloodClass_IsUse');
		var Modulus=rec.get('BloodChargeItemLink_Modulus');
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
				DispOrder: DispOrder,
				Modulus:Modulus
			},
			fields: 'Id,IsUse,DispOrder,Modulus'
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
		var me = this;
	
		var defaultWhere = " bloodchargeitem.IsGroup=0 "; //BloodChargeItem.Visible=1
		/* var arrIdStr = [],
			idStr = "";
		me.store.each(function(record) {
			var bloodChargeItemId = record.get("BloodChargeItemLink_BloodChargeItem_Id");
			if (bloodChargeItemId && Ext.Array.contains(bloodChargeItemId) == false) arrIdStr.push(bloodChargeItemId);
		});
		if (arrIdStr.length > 0) idStr = arrIdStr.join(",");
		if (idStr) defaultWhere = defaultWhere + " and bloodchargeitem.Id not in (" + idStr + ")"; */
		var linkWhere="";//关系表查询条件
		if(me.ChargeTypeID){
			linkWhere='bloodchargeitemlink.ChargeType.Id=' + me.ChargeTypeID;
		}
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		JShell.Win.open('Shell.class.sysbase.chargeitemlink.choose.App', {
			resizable: true,
			width: maxWidth,
			height: height,
			linkWhere:linkWhere,
			leftDefaultWhere: defaultWhere,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onSave(p, records);
				}
			}
		}).show();
	},
	/**保存关系数据*/
	onSave: function(p, records) {
		var me = this;
	
		if (records.length == 0) return;
	
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
	
		for (var i in records) {
			me.onAddOneLink(p,records[i],i);
		}
	},
	/**新增关系数据*/
	onAddOneLink: function(p,record,index) {
		var me = this;
		var params = {
			entity: {
				ChargeType: {
					Id: me.ChargeTypeID,
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				},
				BloodChargeItem: {
					Id: record.get('BloodChargeItem_Id'),
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				},
				IsUse: 1
			}
		};
		params = Ext.JSON.encode(params);
		var url = JShell.System.Path.ROOT + me.addUrl;
		setTimeout(function() {
			//提交数据到后台
			JShell.Server.post(url, params,function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					p.close();
					//if (me.saveErrorCount == 0) me.onSearch();
					me.onSearch();
				}
			});
		}, 100 * index);
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
			defaultWhere: "scoperation.BusinessModuleCode='BloodChargeItemLink'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
});
