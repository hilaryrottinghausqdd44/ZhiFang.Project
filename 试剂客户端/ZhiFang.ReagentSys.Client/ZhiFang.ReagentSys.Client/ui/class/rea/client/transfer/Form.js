/**
 * 移库表单
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.transfer.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '移库信息',
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '15px 0px 0px 0px',
	formtype: "edit",
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsTransferDoc',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField',
	/**获取获取供应商数据服务路径*/
	selectCenOrgUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	/**获取获取库房服务路径*/
	selectStorageUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
	/**获取获取库房货架权限服务路径*/
	selectStorageLinkUrl: '/ReaManageService.svc/RS_UDTO_SearchListByStorageAndLinHQL?isPlanish=true',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 5 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 60,
		width: 170,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	CenOrgEnum: {},
	/**入库类型键值*/
	defaultOutType: '1',
	/**入库类型名称*/
	defaultOutTypeName: '仪器使用',
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	PK: null,
	/**移库总单状态Key*/
	ReaBmsTransferDocStatus: 'ReaBmsTransferDocStatus',
	IsCheck: '1',
	/**源库房*/
	SStorageData: [],
	/**目的库房*/
	DStorageData: [],
	/**是否按权限移库*/
	IsTransferDocIsUse: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('setSDefaultStorage', 'setDDefaultStorage', 'onChangeTaker');
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		if(me.formtype == 'add') {
			/**布局方式*/
			me.layout = {
				type: 'table',
				columns: 5 //每行有几列
			};
			me.defaults = {
				labelWidth: 60,
				width: 190,
				labelAlign: 'right'
			};
		}
		JShell.REA.StatusList.getStatusList(me.ReaBmsTransferDocStatus, false, true, null);
		items.push({
			fieldLabel: '移库Id',
			name: 'ReaBmsTransferDoc_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '单据状态',
			name: 'ReaBmsTransferDoc_Status',
			itemId: 'ReaBmsTransferDoc_Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].List,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '操作日期',
			name: 'ReaBmsTransferDoc_OperDate',
			itemId: 'ReaBmsTransferDoc_OperDate',
			hidden: true,
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '移库单号',
			name: 'ReaBmsTransferDoc_TransferDocNo',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '登记人Id',
			name: 'ReaBmsTransferDoc_CreaterID',
			itemId: 'ReaBmsTransferDoc_CreaterID',
			readOnly: true,
			locked: true,
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '申请人',
			name: 'ReaBmsTransferDoc_CreaterName',
			itemId: 'ReaBmsTransferDoc_CreaterName',
			hidden: false,
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '移库日期',
			name: 'ReaBmsTransferDoc_DataAddTime',
			itemId: 'ReaBmsTransferDoc_DataAddTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '货品总额',
			name: 'ReaBmsTransferDoc_TotalPrice',
			itemId: 'ReaBmsTransferDoc_TotalPrice',
			emptyText: '必填项',
			allowBlank: false,
			readOnly: true,
			locked: true,
			xtype: 'numberfield',
			value: 0
		}, {
			fieldLabel: '源库房',
			name: 'ReaBmsTransferDoc_SStorageID',
			itemId: 'ReaBmsTransferDoc_SStorageID',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			emptyText: '必填项',
			allowBlank: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var SStorageID = com.getValue();
					var SStorageName = com.getRawValue();
					var DStorageID = me.getComponent('ReaBmsTransferDoc_DStorageID').getValue();
					var DStorageName = me.getComponent('ReaBmsTransferDoc_DStorageID').getRawValue();
					me.fireEvent('setSDefaultStorage', SStorageID, SStorageName);
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '领用人id',
			name: 'ReaBmsTransferDoc_TakerID',
			itemId: 'ReaBmsTransferDoc_TakerID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '领用人',
			name: 'ReaBmsTransferDoc_TakerName',
			itemId: 'ReaBmsTransferDoc_TakerName',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '领用人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					me.onUserAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '使用部门',
			name: 'ReaBmsTransferDoc_DeptName',
			itemId: 'ReaBmsTransferDoc_DeptName',
			xtype: 'uxCheckTrigger',
			emptyText: '必填项',
			allowBlank: false,
			className: 'Shell.class.rea.client.out.basic.CheckTree',
			classConfig: {
				title: '部门选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					if(record && record.get("tid") == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDepAccept(record);
					p.close();
				}
			},
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '部门id',
			name: 'ReaBmsTransferDoc_DeptID',
			itemId: 'ReaBmsTransferDoc_DeptID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '审核人id',
			name: 'ReaBmsTransferDoc_CheckID',
			itemId: 'ReaBmsTransferDoc_CheckID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '审核人',
			name: 'ReaBmsTransferDoc_CheckName',
			itemId: 'ReaBmsTransferDoc_CheckName',
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '审核人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					var UseID = me.getComponent('ReaBmsTransferDoc_CheckID');
					var UseName = me.getComponent('ReaBmsTransferDoc_CheckName');
					UseName.setValue(record ? record.get('HREmployee_CName') : '');
					UseID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '目的库房',
			name: 'ReaBmsTransferDoc_DStorageID',
			itemId: 'ReaBmsTransferDoc_DStorageID',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			emptyText: '必填项',
			allowBlank: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var DStorageID = com.getValue();
					var DStorageName = com.getRawValue();
					me.fireEvent('setDDefaultStorage', DStorageID, DStorageName);
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		});
		var cols = 5;
		items.push({
			height: 50,
			fieldLabel: '移库说明',
			emptyText: '移库说明',
			name: 'ReaBmsTransferDoc_Memo',
			xtype: 'textarea',
			colspan: cols,
			width: me.defaults.width * cols
		});
		return items;
	},
	onDepAccept: function(record) {
		var me = this;
		var DeptID = me.getComponent('ReaBmsTransferDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsTransferDoc_DeptName');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		DeptID.setValue(id);
		DeptName.setValue(text);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Visible: 1
		};
		if(values.ReaBmsTransferDoc_OperDate) {
			entity.OperDate = JShell.Date.toServerDate(values.ReaBmsTransferDoc_OperDate);
		}
		if(values.ReaBmsTransferDoc_DeptID) {
			entity.DeptID = values.ReaBmsTransferDoc_DeptID;
		}
		entity.DeptName = values.ReaBmsTransferDoc_DeptName;
		if(values.ReaBmsTransferDoc_TakerID) {
			entity.TakerID = values.ReaBmsTransferDoc_TakerID;
			entity.TakerName = values.ReaBmsTransferDoc_TakerName;
		}
		if(values.ReaBmsTransferDoc_TransferDocNo) {
			entity.TransferDocNo = values.ReaBmsTransferDoc_TransferDocNo;
		}
		if(values.ReaBmsTransferDoc_CheckID) {
			entity.CheckID = values.ReaBmsTransferDoc_CheckID;
			entity.CheckName = values.ReaBmsTransferDoc_CheckName;
		}
		if(values.ReaBmsTransferDoc_TotalPrice) {
			entity.TotalPrice = values.ReaBmsTransferDoc_TotalPrice;
		}
		if(values.ReaBmsTransferDoc_Memo) {
			entity.Memo = values.ReaBmsTransferDoc_Memo;
		}

		var SStorageID = me.getComponent('ReaBmsTransferDoc_SStorageID');
		if(values.ReaBmsTransferDoc_SStorageID) {
			entity.SStorageID = values.ReaBmsTransferDoc_SStorageID;
			entity.SStorageName = SStorageID.getRawValue();
		}

		var DStorageID = me.getComponent('ReaBmsTransferDoc_DStorageID');
		if(values.ReaBmsTransferDoc_DStorageID) {
			entity.DStorageID = values.ReaBmsTransferDoc_DStorageID;
			entity.DStorageName = DStorageID.getRawValue();
		}

		var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
		if(DataAddTime) {
			entity.DataUpdateTime = JShell.Date.toServerDate(DataAddTime) ? JShell.Date.toServerDate(DataAddTime) : null;
			entity.DataAddTime = JShell.Date.toServerDate(DataAddTime) ? JShell.Date.toServerDate(DataAddTime) : null;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId) {
			entity.CreaterID = userId;
			entity.CreaterName = userName;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if(data.ReaBmsTransferDoc_CreaterID) me.setSStorage(data.ReaBmsTransferDoc_CreaterID);
		if(data.ReaBmsTransferDoc_TakerID) me.setDStorage(data.ReaBmsTransferDoc_TakerID);
		if(data.ReaBmsTransferDoc_DataAddTime) data.ReaBmsTransferDoc_DataAddTime = JShell.Date.getDate(data.ReaBmsTransferDoc_DataAddTime);
		if(data.ReaBmsTransferDoc_OperDate) data.ReaBmsTransferDoc_OperDate = JShell.Date.getDate(data.ReaBmsTransferDoc_OperDate);
		return data;
	},
	/**目的库房*/
	setDStorage: function(EmpID) {
		var me = this;
		//绑定数据
		me.DStorageData = [];
		if(me.IsTransferDocIsUse) {
			me.getStorageJurisdiction(EmpID, function(data) {
				var id = '',
					name = '';
				if(data && data.value) {
					me.DStorageData = data.value.list;
				}
			});
		} else {
			me.getAllStorageJurisdiction(function(data) {
				var id = '',
					name = '';
				if(data && data.value) {
					me.DStorageData = data.value.list;
				}
			});
		}

		//目的库房
		var DStorageName = me.getComponent('ReaBmsTransferDoc_DStorageID');
		DStorageName.loadData(me.getStatusData(me.DStorageData));
	},
	/**(直接移库)源库房*/
	setSStorage: function(empID) {
		var me = this;
		//绑定数据
		me.SStorageData = [];
		me.getSStorageJurisdiction(empID, function(data) {
			var id = '',
				name = '';
			if(data && data.value) {
				me.SStorageData = data.value.list;
			}
		});
		//目的库房
		var SStorageID = me.getComponent('ReaBmsTransferDoc_SStorageID');
		SStorageID.loadData(me.getStatusData(me.SStorageData));
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;

	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**领用人选择*/
	onUserAccept: function(record) {
		var me = this;
		var TakerID = me.getComponent('ReaBmsTransferDoc_TakerID');
		var TakerName = me.getComponent('ReaBmsTransferDoc_TakerName');
		TakerName.setValue(record ? record.get('HREmployee_CName') : '');
		TakerID.setValue(record ? record.get('HREmployee_Id') : '');

		var DeptID = me.getComponent('ReaBmsTransferDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsTransferDoc_DeptName');
		DeptID.setValue(record ? record.get('HREmployee_HRDept_Id') : '');
		DeptName.setValue(record ? record.get('HREmployee_HRDept_CName') : '');

		me.setDStorage(record.get('HREmployee_Id'));
		me.defaultStorage();
		me.fireEvent('onChangeTaker', record.get('HREmployee_Id'));
	},
	defaultStorage: function() {
		var me = this;
		var SStorageName = me.getComponent('ReaBmsTransferDoc_SStorageID');
		var DStorageName = me.getComponent('ReaBmsTransferDoc_DStorageID');
		//源库房默认显示
		if(me.SStorageData && me.SStorageData.length > 0) {
			var ssid = me.SStorageData[0].ReaStorage_Id;
			var name = me.SStorageData[0].ReaStorage_CName;
			SStorageName.setValue(ssid);
		}
		//目的库房默认显示
		if(me.DStorageData && me.DStorageData.length > 0) {
			var dsid1 = me.DStorageData[0].ReaStorage_Id;
			var name1 = me.DStorageData[0].ReaStorage_CName;
			DStorageName.setValue(dsid1);
		}
	},
	isAdd: function() {
		var me = this;
		me.setReadOnly(false);
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle(); //标题更改
		me.enableControl(); //启用所有的操作功能
		me.onResetClick();
		var Sysdate = JcallShell.System.Date.getDate();
		var value = JcallShell.Date.toString(Sysdate, true);
		var DataAddTime = me.getComponent('ReaBmsTransferDoc_DataAddTime')
		DataAddTime.setValue(value);
		var CreaterName = me.getComponent('ReaBmsTransferDoc_CreaterName')
		var Status = me.getComponent('ReaBmsTransferDoc_Status');
		Status.setVisible(false);
		var CheckName = me.getComponent('ReaBmsTransferDoc_CheckName');
		if(me.IsCheck == '1' || me.IsCheck == 1) { //需要审核，不允许为空
			CheckName.emptyText = '必填项';
			CheckName.allowBlank = false;
			CheckName.setValue('');
		}
		var Status = me.getComponent('ReaBmsTransferDoc_Status');
		Status.setValue('6');
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var TakerID = me.getComponent('ReaBmsTransferDoc_TakerID');
		var TakerName = me.getComponent('ReaBmsTransferDoc_TakerName');
		CreaterName.setValue(userName);
		TakerID.setValue(userId);
		TakerName.setValue(userName);
		me.fireEvent('onChangeTaker', userId);

		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		var DeptID = me.getComponent('ReaBmsTransferDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsTransferDoc_DeptName');
		DeptID.setValue(deptId);
		DeptName.setValue(deptName);

		me.setSStorage(userId);
		me.setDStorage(userId);
		me.defaultStorage();
	},
	isEdit: function(id) {
		var me = this;
		me.setReadOnly(false);
		me.formtype = 'edit';
		me.changeTitle(); //标题更改
		me.load(id);
	},
	isShow: function(id) {
		var me = this;
		me.setReadOnly(true);
		me.formtype = 'show';
		me.changeTitle(); //标题更改
		me.disableControl();
		me.load(id);
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		var fields = [
			'Id', 'OperDate', 'DeptID', 'DeptName',
			'TakerID', 'TakerName', 'TransferDocNo', 'CheckID',
			'CheckName', 'TotalPrice', 'Memo', 'DataUpdateTime', 'DataAddTime'
		];

		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaGoods_Id;
		return entity;
	},
	/**获取库房列表*/
	getStatusData: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.ReaStorage_Id, obj.ReaStorage_CName]);
		}
		return data;
	},
	/**获取库房权限关系的Hql*/
	getSStorageLinkHql: function(takerId) {
		var me = this;
		var operType = "4"; //源库房信息按直接移库源库房类型
		var linkHql = "reauserstoragelink.OperType=" + operType + " and reauserstoragelink.OperID=" + takerId;
		return linkHql;
	},
	/**获取库房权限关系的Url*/
	getSStorageLinkUrl: function(takerId) {
		var me = this;
		var params = [];
		params.push(JShell.System.Path.ROOT + me.selectStorageLinkUrl);
		params.push("fields=ReaStorage_CName,ReaStorage_Id,ReaStorage_IsMainStorage");
		params.push("storageHql=reastorage.Visible=1");
		var linkHql = "linkHql=" + me.getSStorageLinkHql(takerId);
		params.push(linkHql);
		params.push("sort=[{property:'ReaStorage_IsMainStorage',direction:'DESC'},{property:'ReaStorage_DispOrder',direction:'ASC'}]");
		params.push("operType=4"); //源库房信息按直接移库源库房类型
		return params;
	},
	/**(直接移库)源库房
	 * 如果用户给某库房或某用户添加了(直接)移库源库房的权限,那所有进行直接移库登记的用户都需要分配源库房权限;
	 * 如果(直接)移库源库房没有分配权限,直接移库源库房选择范围就是所有的库房(按总库标志+显示次序排序)
	 * */
	getSStorageJurisdiction: function(takerId, callback) {
		var me = this;
		if(!takerId) {
			JShell.Msg.alert('领用人不能为空,请选择');
			return;
		}
		var url = me.getSStorageLinkUrl(takerId);
		if(url) url = url.join("&");
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取库房权限关系的Hql*/
	getStorageLinkHql: function(takerId) {
		var me = this;
		var operType = "1";
		var linkHql = "reauserstoragelink.OperType=" + operType;
		linkHql += ' and reauserstoragelink.OperID=' + takerId;
		return linkHql;
	},
	/**获取库房权限关系的Url*/
	getStorageLinkUrl: function(takerId) {
		var me = this;
		var params = [];
		params.push(JShell.System.Path.ROOT + me.selectStorageLinkUrl);
		params.push("fields=ReaStorage_CName,ReaStorage_Id,ReaStorage_IsMainStorage");
		params.push("storageHql=reastorage.Visible=1");
		params.push("linkHql=" + me.getStorageLinkHql(takerId));
		params.push("sort=[{property:'ReaStorage_IsMainStorage',direction:'DESC'},{property:'ReaStorage_DispOrder',direction:'ASC'}]");
		params.push("operType=1");
		return params;
	},
	/**获取库房货架权限的库房信息（按领用人）*/
	getStorageJurisdiction: function(takerId, callback) {
		var me = this;
		if(!takerId) {
			JShell.Msg.alert('领用人不能为空,请选择');
			return;
		}
		var url = me.getStorageLinkUrl(takerId);
		if(url) url = url.join("&");
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**取库房默认值(排序是总库标志+显示次序,第一行）
	 * 	没有对权限控制
	 * */
	getAllStorageJurisdiction: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectStorageUrl;
		url += '&fields=ReaStorage_CName,ReaStorage_Id';
		url += '&where=reastorage.Visible=1';
		url += "&sort=[{property:'ReaStorage_IsMainStorage',direction:'DESC'},{property:'ReaStorage_DispOrder',direction:'ASC'}]";
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**不按权限过滤*/
	loadAllJurisdiction: function() {
		var me = this;
		var StorageName = me.getComponent('ReaBmsTransferDoc_SStorageID');
		me.SStorageData = [];
		me.getAllStorageJurisdiction(function(data) {
			var id = '',
				name = '';
			if(data && data.value) {
				me.SStorageData = data.value.list;
				me.DStorageData = data.value.list;
			}
		});
		me.defaultStorage();

	},
	/**源库房是否只读*/
	setStorageReadOnly: function(bo) {
		var me = this;
		var StorageName = me.getComponent('ReaBmsTransferDoc_SStorageID');
		StorageName.setReadOnly(bo);
	}

});