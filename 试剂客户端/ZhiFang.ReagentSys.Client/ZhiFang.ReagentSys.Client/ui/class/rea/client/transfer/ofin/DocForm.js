/**
 * 入库移库:移库信息
 * @author longfc
 * @version 2019-03-28
 */
Ext.define('Shell.class.rea.client.transfer.ofin.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '移库信息',
	height: 125,
	/**内容周围距离*/
	bodyPadding: '15px 5px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 60,
		width: 170,
		labelAlign: 'right'
	},
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
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	PK: null,
	/**目的库房*/
	DStorageData: [],
	/**是否按权限移库*/
	IsTransferDocIsUse: false,
	//formtype: "add",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getIsTransferDocIsUse(function(val) {
			//me.isAdd();
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onDStorageChange', 'onChangeTaker');
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
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
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			classConfig: {
				title: '领用人选择',
				checkOne: true
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != oldValue) {
						var takerID = me.getComponent('ReaBmsTransferDoc_TakerID').getValue();
						me.initDStorage(takerID);
					}
				},
				check: function(p, record) {
					me.onTakerAccept(record);
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
			colspan: 2,
			width: me.defaults.width * 2,
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
			}
		}, {
			fieldLabel: '部门id',
			name: 'ReaBmsTransferDoc_DeptID',
			itemId: 'ReaBmsTransferDoc_DeptID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '目的库房',
			name: 'ReaBmsTransferDoc_DStorageID',
			itemId: 'ReaBmsTransferDoc_DStorageID',
			hasStyle: true,
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxSimpleComboBox',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.setDStorageName(com.getRawValue());
					me.fireEvent('onDStorageChange', me, com.getValue(), com.getRawValue());
				}
			}
		}, {
			fieldLabel: '目的库房',
			name: 'ReaBmsTransferDoc_DStorageName',
			itemId: 'ReaBmsTransferDoc_DStorageName',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			height: 50,
			fieldLabel: '移库说明',
			emptyText: '移库说明',
			name: 'ReaBmsTransferDoc_Memo',
			xtype: 'textarea',
			colspan: 4,
			width: me.defaults.width * 4
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Visible: 1
		};
		if(values.ReaBmsTransferDoc_DeptID) {
			entity.DeptID = values.ReaBmsTransferDoc_DeptID;
		}
		entity.DeptName = values.ReaBmsTransferDoc_DeptName;
		if(values.ReaBmsTransferDoc_TakerID) {
			entity.TakerID = values.ReaBmsTransferDoc_TakerID;
			entity.TakerName = values.ReaBmsTransferDoc_TakerName;
		}

		if(values.ReaBmsTransferDoc_DStorageID) {
			entity.DStorageID = values.ReaBmsTransferDoc_DStorageID;
			entity.DStorageName = values.ReaBmsTransferDoc_DStorageName;
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
		if(values.ReaBmsTransferDoc_Memo) {
			entity.Memo = values.ReaBmsTransferDoc_Memo;
		}
		return entity;
	},
	/**获取是否按移库人权限移库参数
	 * 1*/
	getIsTransferDocIsUse: function(callback) {
		var me = this;
		//是否按移库人权限移库
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsTransferDocIsUseEmpOut", false, function(data) {
			if(data.success) {
				var paraValue = 2;
				var obj = data.value;
				me.IsTransferDocIsUse = false;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					if(paraValue == '1' || paraValue == 1) {
						me.IsTransferDocIsUse = true;
					}
					if(callback) callback(me.IsTransferDocIsUse);
				}
			}
		});
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		var Sysdate = JcallShell.System.Date.getDate();
		var value = JcallShell.Date.toString(Sysdate, true);
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var takerID = me.getComponent('ReaBmsTransferDoc_TakerID');
		var takerName = me.getComponent('ReaBmsTransferDoc_TakerName');
		takerID.setValue(userId);
		takerName.setValue(userName);
		me.fireEvent('onChangeTaker', userId);

		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		var DeptID = me.getComponent('ReaBmsTransferDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsTransferDoc_DeptName');
		DeptID.setValue(deptId);
		DeptName.setValue(deptName);
	},
	onDepAccept: function(record) {
		var me = this;
		var deptID = me.getComponent('ReaBmsTransferDoc_DeptID');
		var deptName = me.getComponent('ReaBmsTransferDoc_DeptName');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		deptID.setValue(id);
		deptName.setValue(text);
	},
	/**领用人选择*/
	onTakerAccept: function(record) {
		var me = this;
		var userId = record ? record.get('HREmployee_Id') : '';
		var takerID = me.getComponent('ReaBmsTransferDoc_TakerID');
		var takerName = me.getComponent('ReaBmsTransferDoc_TakerName');
		takerName.setValue(record ? record.get('HREmployee_CName') : '');
		takerID.setValue(userId);

		var deptID = me.getComponent('ReaBmsTransferDoc_DeptID');
		var deptName = me.getComponent('ReaBmsTransferDoc_DeptName');
		deptID.setValue(record ? record.get('HREmployee_HRDept_Id') : '');
		deptName.setValue(record ? record.get('HREmployee_HRDept_CName') : '');
		//me.initDStorage(record.get('HREmployee_Id'));
		me.fireEvent('onChangeTaker', record.get('HREmployee_Id'));
		JShell.Action.delay(function() {
			me.initDStorage(userId);
		}, null, 500);
	},
	/**目的库房*/
	initDStorage: function(empID) {
		var me = this;
		//绑定数据
		me.DStorageData = [];
		if(me.IsTransferDocIsUse) {
			me.getStorageJurisdiction(empID, function(data) {
				var id = '',
					name = '';
				if(data && data.value) {
					me.DStorageData = data.value.list;
					me.loadDStorageData();
				}
			});
		} else {
			me.getAllStorageJurisdiction(function(data) {
				var id = '',
					name = '';
				if(data && data.value) {
					me.DStorageData = data.value.list;
					me.loadDStorageData();
				}
			});
		}
	},
	/**获取库房列表*/
	getStorageDatas: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.ReaStorage_Id, obj.ReaStorage_CName]);
		}
		return data;
	},
	/**加载目的库房数据*/
	loadDStorageData: function() {
		var me = this;
		if(!me.DStorageData) me.DStorageData = [];

		//目的库房
		var dStorageID = me.getComponent('ReaBmsTransferDoc_DStorageID');
		if(dStorageID) dStorageID.loadData(me.getStorageDatas(me.DStorageData));
		me.setDefaultStorage();
	},
	/**设置目的库房默认值*/
	setDefaultStorage: function() {
		var me = this;
		//目的库房默认显示
		var id1 = '',
			name1 = '';
		if(me.DStorageData.length > 0) {
			id1 = me.DStorageData[0].ReaStorage_Id;
			name1 = me.DStorageData[0].ReaStorage_CName;
		}
		me.setDStorageID(id1);
		me.setDStorageName(name1);
	},
	/**设置目的库房值*/
	setDStorageID: function(value) {
		var me = this;
		var dStorageID = me.getComponent('ReaBmsTransferDoc_DStorageID');
		if(dStorageID) dStorageID.setValue(value);
	},
	/**设置目的库房值*/
	setDStorageName: function(value) {
		var me = this;
		var dStorageName = me.getComponent('ReaBmsTransferDoc_DStorageName');
		if(dStorageName) dStorageName.setValue(value);
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
		if(url) url=url.join("&");
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

	}
});