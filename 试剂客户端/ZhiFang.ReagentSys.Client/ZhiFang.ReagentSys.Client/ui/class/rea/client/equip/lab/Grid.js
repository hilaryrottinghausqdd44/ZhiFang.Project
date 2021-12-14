/**
 * 仪器维护列表
 * @author Sheldon
 * @version 2018-10-31
 */
Ext.define('Shell.class.rea.client.equip.lab.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '仪器维护列表',
	width: 800,
	height: 500,

	//**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaTestEquipLab',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipLabByField',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaTestEquipLab',
	/**获取获取供应商数据服务路径*/
	selectCenOrgUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	selectDictUrl: '/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true',
	/**获取数据服务路径*/
	//selectDeptUrl:'/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true',
	/**根据部门ID获取数据服务路径*/
	//selectDeptUrl:'/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true',
	selectDeptUrl: '/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id,HRDept_DataTimeStamp,HRDept_UseCode',
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaTestEquipLab_DispOrder',
		direction: 'ASC'
	}],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	/**默认每页数量*/
	defaultPageSize: 200,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**原始行数*/
	oldCount: 0,
	CenOrgList: [],
	CenOrgEnum: null,
	DictEnum: null,
	DictList: [],
	DeptEnum: null,
	DeptList: [],
	/*厂商*/
	ProdOrg: 'ProdOrg',
	/**部门*/
	Dept: 'Dept',
	/**用户UI配置Key*/
	userUIKey: 'equip.lab.Grid',
	/**用户UI配置Name*/
	userUIName: "仪器列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.getCenOrgInfo();
		me.getDictInfo();
		//me.getDeptInfo();
		//查询框信息
		me.searchInfo = {
			width: 215,
			emptyText: '仪器名称/英文名称/代码/Lis编码',
			isLike: true,
			itemId: 'Search',
			fields: ['reatestequiplab.CName', 'reatestequiplab.EName', 'reatestequiplab.ShortCode','reatestequiplab.LisCode']
		};

		me.buttonToolbarItems = ['refresh', '-', 'add', 'del', 'save', '-', {
			xtype: 'button',
			iconCls: 'file-excel',
			text: '获取LIS仪器',
			tooltip: '从LIS系统导入仪器信息',
			listeners: {
				click: function(but) {
					me.onExportLis();
				}
			}
		}, '->', {
			type: 'search',
			info: me.searchInfo
		}];
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		});

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
			dataIndex: 'ReaTestEquipLab_DeptName',
			text: '所属部门',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_CName',
			text: '中文名称',
			width: 150,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_EName',
			text: '英文名称',
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_ShortCode',
			text: '代码',
			hidden: true,
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_LisCode',
			text: 'LIS编码',
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_DispOrder',
			text: '次序',
			width: 50,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_ProdOrgName',
			text: '仪器厂商',
			sortable: false,
			width: 100,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var v = record.get('ReaTestEquipLab_ProdOrgID');
				if(me.DictEnum != null) {
					v = me.DictEnum[v];
				}
				return v;
			}
		}, {
			dataIndex: 'ReaTestEquipLab_ProdOrgID',
			text: '仪器厂商ID',
			sortable: false,
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_CompOrgName',
			text: '供应商',
			sortable: false,
			width: 100,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var v = record.get('ReaTestEquipLab_CompOrgID');
				if(me.CenOrgEnum != null) {
					v = me.CenOrgEnum[v];
				}
				return v;
			}
		}, {
			dataIndex: 'ReaTestEquipLab_CompOrgID',
			text: '供应商ID',
			sortable: false,
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_Memo',
			text: '描述',
			width: 150,
			editor: {},
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta);
				return v;
			}
		}, {
			dataIndex: 'Tab',
			text: '标记',
			hidden: true,
			width: 50,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}];

		return columns;
	},
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;

		var isError = false;
		var changedRecords = me.store.getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;

		if(len == 0) {
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			me.updateOne(i, changedRecords[i]);
		}
	},
	/**修改信息*/
	updateOne: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var entity = {
			Id: record.get('ReaTestEquipLab_Id'),
			CName: record.get('ReaTestEquipLab_CName'),
			EName: record.get('ReaTestEquipLab_EName'),
			LisCode: record.get('ReaTestEquipLab_LisCode'),
			ShortCode: record.get('ReaTestEquipLab_ShortCode'),
			Visible: record.get('ReaTestEquipLab_Visible') ? 1 : 0,
			DispOrder: record.get('ReaTestEquipLab_DispOrder'),
			Memo: record.get('ReaTestEquipLab_Memo')
		};
		var fields = [
			'CName', 'Id', 'ShortCode', 'EName',
			'LisCode', 'DispOrder', 'Memo', 'Visible'
		];
		var params = Ext.JSON.encode({
			entity: entity,
			fields: fields.join(',')
		});

		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
			} else {
				me.saveErrorCount++;
				if(record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					me.onSearch();
				} else {
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	},
	/**
	 * 从LIS系统导入仪器
	 */
	onExportLis: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_EditSyncLisTestEquipLabInfo';
		me.CenOrgEnum = {}, me.CenOrgList = [];
		me.showMask("获取LIS仪器信息中...");
		JShell.Server.get(url, function(data) {
			me.hideMask();
			if(data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	updateOneByIsUse: function(index, id, isUse) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsUse: isUse
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
	showMemoText: function(value, meta) {
		var me = this;
		var val = value.replace(/(^\s*)|(\s*$)/g, "");
		val = val.replace(/\\r\\n/g, "<br />");
		val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1 = v.indexOf("</br>");
		if(index1 > 0) v = v.substring(0, index1);
		if(v.length > 0) v = (v.length > 32 ? v.substring(0, 32) : v);
		if(value.length > 32) {
			v = v + "...";
		}
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return v
	},
	/**获取供应商/厂商信息*/
	getCenOrgInfo: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectCenOrgUrl;
		//url += '&fields=ReaCenOrg_CName,ReaCenOrg_Id';
		me.CenOrgEnum = {}, me.CenOrgList = [];
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						tempArr = [obj.ReaCenOrg_Id, obj.ReaCenOrg_CName];
						me.CenOrgEnum[obj.ReaCenOrg_Id] = obj.ReaCenOrg_CName;
						me.CenOrgList.push(tempArr);
					});
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取厂商信息*/
	getDictInfo: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectDictUrl;
		//url += "&fields=BDict_CName,BDict_Id&where=bdict.BDictType.DictTypeCode='" + me.ProdOrg + "'";
		me.DictEnum = {}, me.DictList = [];
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						tempArr = [obj.BDict_Id, obj.BDict_CName];
						me.DictEnum[obj.BDict_Id] = obj.BDict_CName;
						me.DictList.push(tempArr);
					});
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	}
});