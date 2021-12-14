/**
 * 收费组合项目维护
 * @author longfc	
 * @version 2020-08-08
 */
Ext.define('Shell.class.sysbase.chargegroupitem.LinkGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '收费组合项目关系列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeGroupItemByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeGroupItem',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeGroupItemByField',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeGroupItem',
	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodChargeGroupItem_DispOrder',
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
	/**组合项目ID*/
	GChargeItemID: null,
	/**后台排序*/
	remoteSort: false,
	/**用户UI配置Key*/
	userUIKey: 'chargegroupitem.LinkGrid',
	/**用户UI配置Name*/
	userUIName: "收费组合项目关系列表",

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
			fields: ['bloodchargegroupitem.BloodChargeItem.CName', 'bloodchargegroupitem.BloodChargeItem.PinYinZiTou',
				'bloodchargegroupitem.BloodChargeItem.EName'
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
			dataIndex: 'BloodChargeGroupItem_BloodChargeItem_Id',
			text: '子费用项目编号',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'BloodChargeGroupItem_BloodChargeItem_CName',
			text: '子费用项目',
			minWidth: 150,
			//flex: 1,
			defaultRenderer: true
		}, {
			dataIndex: 'BloodChargeGroupItem_BloodChargeItem_SName',
			text: '子费用项目简称',
			minWidth: 100,
			defaultRenderer: true
		}, {
			text: '入价',
			dataIndex: 'BloodChargeGroupItem_BloodChargeItem_InPrice',
			width: 70,
			defaultRenderer: true,
			align: 'center'
		}, {
			text: '出价',
			dataIndex: 'BloodChargeGroupItem_BloodChargeItem_OutPrice',
			width: 70,
			defaultRenderer: true,
			align: 'center'
		}, {
			text: '<b style="color:blue;">使用</b>',
			xtype: 'checkcolumn',
			dataIndex: 'BloodChargeGroupItem_IsUse',
			width: 40,
			align: 'center',
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '<b style="color:blue;">显示次序</b>',
			dataIndex: 'BloodChargeGroupItem_DispOrder',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			text: '次序',
			dataIndex: 'BloodChargeGroupItem_BloodChargeItem_DispOrder',
			width: 70,
			defaultRenderer: true,
			hidden: true,
			align: 'center',
			type: 'int'
		}, {
			dataIndex: 'BloodChargeGroupItem_BloodChargeItem_ChargeItemSpec',
			text: '费用规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BloodChargeGroupItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = me.getComponent("buttonsToolbar").getComponent("Search").getValue(),
			params = [];

		me.internalWhere = '';

		if (me.GChargeItemID) {
			params.push('bloodchargegroupitem.GBloodChargeItem.Id=' + me.GChargeItemID);
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
		if (!me.GChargeItemID) {
			JShell.Msg.error('组合项目不能为空');
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
			me.updateOne(i,rec);
		}
	},
	updateOne: function(index,rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var DispOrder = rec.get('BloodChargeGroupItem_DispOrder');
		if (!DispOrder) {
			DispOrder = rec.get('BloodChargeGroupItem_BloodChargeItem_DispOrder');
		}
		var IsUse = rec.get('BloodChargeGroupItem_IsUse');
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
		var me = this;

		var defaultWhere = " bloodchargeitem.IsGroup=0 "; //BloodChargeItem.Visible=1
		/* var arrIdStr = [],
			idStr = "";
		me.store.each(function(record) {
			var bloodChargeItemId = record.get("BloodChargeGroupItem_BloodChargeItem_Id");
			if (bloodChargeItemId && Ext.Array.contains(bloodChargeItemId) == false) arrIdStr.push(bloodChargeItemId);
		});
		if (arrIdStr.length > 0) idStr = arrIdStr.join(",");
		if (idStr) defaultWhere = defaultWhere + " and bloodchargeitem.Id not in (" + idStr + ")"; */
		var linkWhere="";//关系表查询条件
		if(me.GChargeItemID){
			linkWhere='bloodchargegroupitem.GBloodChargeItem.Id=' + me.GChargeItemID;
		}
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;		
		JShell.Win.open('Shell.class.sysbase.chargegroupitem.choose.App', {
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
			me.onAddOneLink(p, records[i], i);
		}
	},
	/**新增关系数据*/
	onAddOneLink: function(p, record, index) {
		var me = this;
		var params = {
			entity: {
				GBloodChargeItem: {
					Id: me.GChargeItemID,
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
			JShell.Server.post(url, params, function(data) {
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
	}
});
