/**
 * 出库表单
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.out.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '出库信息',
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '15px 0px 0px 0px',
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
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDocById?isPlanish=true',
	/**获取获取库房服务路径*/
	selectStorageUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
	/**获取获取库房货架权限服务路径*/
	selectStorageLinkUrl: '/ReaManageService.svc/RS_UDTO_SearchListByStorageAndLinHQL?isPlanish=true',

	PK: null,
	/**出库类型默认值*/
	defaluteOutType: '1',
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	TakerObj: {},
	/**权限库房数据*/
	StorageData: [],
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**直接出库时是否需要出库确认,1是,0否*/
	IsCheck: '1',
	/**移库总单状态Key*/
	ReaBmsOutDocStatus: 'ReaBmsOutDocStatus',
	ReaBmsOutDocOutType: 'ReaBmsOutDocOutType',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocStatus, false, false, null);
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocOutType, false, false, null);
		me.addEvents('setDefaultStorage');
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		return items;
	},
	/**@overwrite 创建领用人组件*/
	createTaker: function() {
		var me = this;
		var obj = {
			fieldLabel: '领用人',
			name: 'ReaBmsOutDoc_TakerName',
			itemId: 'ReaBmsOutDoc_TakerName',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.out.user.CheckApp',
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
		};
		return obj;
	},
	/**库房选择后领用人处理*/
	onStorageChange: function(newValue) {
		var me = this;
		//报损出库,退供应商出库
		if(me.defaluteOutType == '3' || me.defaluteOutType == '4') return;
		if(me.formtype == "edit") return;

		if(!me.StorageData) me.StorageData = [];
		var isMainStorage = false;
		Ext.Array.forEach(me.StorageData, function(item, index, allItems) {
			if(item["ReaStorage_Id"] == newValue) {
				isMainStorage = item["ReaStorage_IsMainStorage"];
				return false;
			}
		});
		if(isMainStorage == "true") isMainStorage = true;
		if(isMainStorage == "false") isMainStorage = false;

		var TakerID = me.getComponent('ReaBmsOutDoc_TakerID');
		var TakerName = me.getComponent('ReaBmsOutDoc_TakerName');
		var DeptID = me.getComponent('ReaBmsOutDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsOutDoc_DeptName');

		//当库房选择的为总库房时,领用人可以选择;
		if(isMainStorage) {
			TakerName.locked = false;
			TakerName.setReadOnly(false);
			DeptName.locked = false;
			DeptName.setReadOnly(false);
		} else {
			//当库房选择的为二级库房时,领用人默认取当前登录者,并锁定
			TakerName.locked = true;
			TakerName.setReadOnly(true);
			DeptName.locked = true;
			DeptName.setReadOnly(true);
			if(me.TakerObj.TakerId) {
				TakerID.setValue(me.TakerObj.TakerId);
				TakerName.setValue(me.TakerObj.TakerName);
				DeptID.setValue(me.TakerObj.DeptId);
				DeptName.setValue(me.TakerObj.DeptName);
			}
		}
	},
	/**领用人选择*/
	onUserAccept: function(record) {
		var me = this;
		var UseID = me.getComponent('ReaBmsOutDoc_TakerID');
		var UseName = me.getComponent('ReaBmsOutDoc_TakerName');
		UseName.setValue(record ? record.get('HREmployee_CName') : '');
		UseID.setValue(record ? record.get('HREmployee_Id') : '');

		var DeptID = me.getComponent('ReaBmsOutDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsOutDoc_DeptName');
		DeptID.setValue(record ? record.get('HREmployee_HRDept_Id') : '');
		DeptName.setValue(record ? record.get('HREmployee_HRDept_CName') : '');
	},
	/**使用部门选择*/
	onDepAccept: function(record) {
		var me = this;
		var DeptID = me.getComponent('ReaBmsOutDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsOutDoc_DeptName');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		DeptID.setValue(id);
		DeptName.setValue(text);
	},
	/**确认人选择*/
	onConfirmAccept: function(record) {
		var me = this;
		var UseID = me.getComponent('ReaBmsOutDoc_ConfirmId');
		var UseName = me.getComponent('ReaBmsOutDoc_ConfirmName');
		UseName.setValue(record ? record.get('HREmployee_CName') : '');
		UseID.setValue(record ? record.get('HREmployee_Id') : '');
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		return tempList;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var dataAddTime = data.ReaBmsOutDoc_DataAddTime;
		if (dataAddTime) {
			data.ReaBmsOutDoc_DataAddTime = JcallShell.Date.toString(dataAddTime.replace(/\//g, "-"));
		}
		var confirmTime = data.ReaBmsOutDoc_ConfirmTime;
		if (confirmTime) {
			data.ReaBmsOutDoc_ConfirmTime = JcallShell.Date.toString(confirmTime.replace(/\//g, "-"));
		}
		var checkTime = data.ReaBmsOutDoc_CheckTime;
		if (checkTime) {
			data.ReaBmsOutDoc_CheckTime = JcallShell.Date.toString(checkTime.replace(/\//g, "-"));
		}
		var approvalTime = data.ReaBmsOutDoc_ApprovalTime;
		if (approvalTime) {
			data.ReaBmsOutDoc_ApprovalTime = JcallShell.Date.toString(approvalTime.replace(/\//g, "-"));
		}
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Visible: 1
		};
		var OutType = me.getComponent('ReaBmsOutDoc_OutType');
		if(values.ReaBmsOutDoc_OutType) {
			entity.OutType = values.ReaBmsOutDoc_OutType;
			entity.OutTypeName = OutType.getRawValue();
		}
		var StorageID = me.getComponent('ReaBmsOutDoc_StorageID');
		if(values.ReaBmsOutDoc_StorageID) {
			entity.StorageID = values.ReaBmsOutDoc_StorageID;
			entity.StorageName = StorageID.getRawValue();
		}
		if(values.ReaBmsOutDoc_DeptID) {
			entity.DeptID = values.ReaBmsOutDoc_DeptID;
			entity.DeptName = values.ReaBmsOutDoc_DeptName;
		}
		if(values.ReaBmsOutDoc_TakerID) {
			entity.TakerID = values.ReaBmsOutDoc_TakerID;
			entity.TakerName = values.ReaBmsOutDoc_TakerName;
		}
		if(values.ReaBmsOutDoc_OutDocNo) {
			entity.OutDocNo = values.ReaBmsOutDoc_OutDocNo;
		}
		if(values.ReaBmsOutDoc_ConfirmId) {
			entity.ConfirmId = values.ReaBmsOutDoc_ConfirmId;
			entity.ConfirmName = values.ReaBmsOutDoc_ConfirmName;
		}
		if(values.ReaBmsOutDoc_TotalPrice) {
			entity.TotalPrice = values.ReaBmsOutDoc_TotalPrice;
		}

		if(values.ReaBmsOutDoc_Status) {
			entity.Status = values.ReaBmsOutDoc_Status;
		}
		if(values.ReaBmsOutDoc_Memo) {
			entity.Memo = values.ReaBmsOutDoc_Memo;
		}
					
		if(values.ReaBmsOutDoc_DataAddTime) {
			entity.DataAddTime = JShell.Date.toServerDate(values.ReaBmsOutDoc_DataAddTime);
		}
		return entity;
	},
	/**初始化库房信息*/
	initStorageData: function(isRefresh) {
		var me = this;
		if(!me.StorageData) me.StorageData = [];
		//绑定数据
		if(isRefresh == true) me.StorageData = [];
		if(me.IsEmpOut) {
			var empId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
			me.loadJurisdiction(empId, isRefresh);
		} else {
			me.loadAllJurisdiction(isRefresh);
		}
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
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
		return params;
	},
	/**（按领用人）获取库房货架权限的库房信息*/
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
	/**获取取库房信息,没有库房权限(排序是总库标志+显示次序,第一行）*/
	getAllStorageJurisdiction: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectStorageUrl;
		url += '&fields=ReaStorage_CName,ReaStorage_Id,ReaStorage_IsMainStorage';
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
	/**按出库人权限获取库房信息*/
	loadJurisdiction: function(empId, isRefresh) {
		var me = this;

		if(!me.StorageData) me.StorageData = [];
		if(isRefresh == true) me.StorageData = [];
		if(me.StorageData.length > 0) {
			me.loadStorageData();
			me.setStorageIDValue();
		} else {
			me.getStorageJurisdiction(empId, function(data) {
				if(data && data.value) {
					me.StorageData = data.value.list;
					me.loadStorageData();
					me.setStorageIDValue();
				}
			});
		}
	},
	/**不按出库人权限获取库房信息*/
	loadAllJurisdiction: function(isRefresh) {
		var me = this;
		if(!me.StorageData) me.StorageData = [];
		if(isRefresh == true) me.StorageData = [];
		if(me.StorageData.length > 0) {
			me.loadStorageData();
			me.setStorageIDValue();
		} else {
			me.getAllStorageJurisdiction(function(data) {
				if(data && data.value) {
					me.StorageData = data.value.list;
					me.loadStorageData();
					me.setStorageIDValue();
				}
			});
		}
	},
	/**获取库房列表*/
	getStorageData: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.ReaStorage_Id, obj.ReaStorage_CName]);
		}
		return data;
	},
	/**库房数据加载*/
	loadStorageData: function() {
		var me = this;
		if(!me.StorageData) me.StorageData = [];
		var storageID = me.getComponent('ReaBmsOutDoc_StorageID');
		if(storageID) storageID.loadData(me.getStorageData(me.StorageData));
	},
	/**库房默认值*/
	setStorageIDValue: function() {
		var me = this;
		if(me.formtype != "add") return;

		if(!me.StorageData) me.StorageData = [];
		var storageID = me.getComponent('ReaBmsOutDoc_StorageID');
		//默认显示
		var idValue = '',
			nameValue = '';
		if(me.StorageData.length > 0) {
			idValue = me.StorageData[0].ReaStorage_Id;
			nameValue = me.StorageData[0].ReaStorage_CName;
		}
		var oldIdVaule = "";
		if(storageID) {
			oldIdVaule = storageID.getValue();
			storageID.setValue(idValue);
		}
		if(idValue && idValue != oldIdVaule) me.fireEvent('setDefaultStorage', idValue, nameValue);
	},
	/**库房是否只读*/
	setStorageReadOnly: function(bo) {
		var me = this;
		var storageID = me.getComponent('ReaBmsOutDoc_StorageID');
		if(storageID) storageID.setReadOnly(bo);
	}
});