/**
 * 移库表单
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.transfer.accept.Form', {
	extend: 'Shell.class.rea.client.transfer.Form',

	title: '移库信息',
	layout: {
		type: 'table',
		columns: 5 //每行有几列
	},
	defaults: {
		labelWidth: 60,
		width: 190,
		labelAlign: 'right'
	},

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
		JShell.REA.StatusList.getStatusList(me.ReaBmsTransferDocStatus, false, true, null);
		items.push({
			fieldLabel: '移库Id',
			name: 'ReaBmsTransferDoc_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '移库单号',
			name: 'ReaBmsTransferDoc_TransferDocNo',
			hidden:true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '操作日期',
			name: 'ReaBmsTransferDoc_OperDate',
			itemId: 'ReaBmsTransferDoc_OperDate',
			hidden:true,
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push( {
			fieldLabel: '登记人Id',
			name: 'ReaBmsTransferDoc_CreaterID',
			itemId: 'ReaBmsTransferDoc_CreaterID',
			readOnly: true,
			locked: true,
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '申请日期',
			name: 'ReaBmsTransferDoc_DataAddTime',
			itemId: 'ReaBmsTransferDoc_DataAddTime',
			readOnly: true,
			locked: true,
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		} ,{
			fieldLabel: '申请人',
			name: 'ReaBmsTransferDoc_CreaterName',
			itemId: 'ReaBmsTransferDoc_CreaterName',
			hidden: false,
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		},{
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
			hasStyle: true,
			emptyText: '必填项',
			allowBlank: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var SStorageID = com.getValue();
					var SStorageName = com.getRawValue();
					me.fireEvent('setSDefaultStorage', SStorageID, SStorageName);
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '领用人Id',
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
		},  {
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
			hasStyle: true,
			emptyText: '必填项',
			allowBlank: false,
			readOnly: true,
			locked: true,
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
		items.push({
			height: 50,
			fieldLabel: '移库说明',
			emptyText: '移库说明',
			name: 'ReaBmsTransferDoc_Memo',
			xtype: 'textarea',
			colspan: 5,
			width: me.defaults.width * 5
		});
		return items;
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		var CheckName = me.getComponent('ReaBmsTransferDoc_CheckName');
		if(me.IsCheck == '1' || me.IsCheck == 1) { //需要审核，不允许为空
			CheckName.emptyText = '必填项';
			CheckName.allowBlank = false;
			CheckName.setValue('');
		}
		var DStorageID = me.getComponent('ReaBmsTransferDoc_DStorageID');
		DStorageID.setReadOnly(true);
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if(data.ReaBmsTransferDoc_CreaterID) me.setSStorage(data.ReaBmsTransferDoc_CreaterID);
		if(data.ReaBmsTransferDoc_TakerID) me.setDStorage(data.ReaBmsTransferDoc_TakerID);
		if(data.ReaBmsTransferDoc_DataAddTime)data.ReaBmsTransferDoc_DataAddTime = JShell.Date.getDate(data.ReaBmsTransferDoc_DataAddTime);
		if(data.ReaBmsTransferDoc_OperDate)data.ReaBmsTransferDoc_OperDate = JShell.Date.getDate(data.ReaBmsTransferDoc_OperDate);
		return data;
	},
	/**源库房
	 * 重写原因，申请的源库房不需要权限过滤
	 * */
	setSStorage: function(empID) {
		var me = this;
		//绑定数据
		me.SStorageData = [];
		me.getSStorageJurisdiction(empID, function(data) {
			if(data && data.value) {
				me.SStorageData = data.value.list;
			}
		});
		//目的库房
		var SStorageID = me.getComponent('ReaBmsTransferDoc_SStorageID');
		SStorageID.loadData(me.getStatusData(me.SStorageData));
	},
	/**获取库房权限关系的Hql*/
	getSStorageLinkHql: function(takerId) {
		var me = this;
		var operType = "2";//源库房信息按移库申请源库房
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
		params.push("operType=2");//源库房信息按移库申请源库房
		return params;
	},
	/**获取库房货架权限的库房信息（按领用人）*/
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
	}
});