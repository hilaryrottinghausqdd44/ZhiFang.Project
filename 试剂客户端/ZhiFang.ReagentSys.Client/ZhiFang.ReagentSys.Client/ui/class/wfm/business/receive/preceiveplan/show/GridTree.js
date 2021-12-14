/**
 * 收款计划查询
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.show.GridTree', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.SimpleGridTree',
	title: '收款计划查询列表',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认员工赋值*/
	hasDefaultUser: true,
	/**默认员工ID*/
	defaultUserID: null,
	/**默认员工名称*/
	defaultUserName: null,
	/**默认员工类型*/
	defaultUserType: '',
	/**默认时间类型*/
	defaultDateType: 'DataAddTime',
	/**时间类型列表*/
	DateTypeList: [
		['DataAddTime', '创建时间'],
		['ReceiveDate', '收款日期'],
		['ExpectReceiveDate', '预计回款时间'],
		['ReviewDate', '审核时间']
	],
	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['ReceiveManID', '收款负责人'],
		['InputerID', '录入人'],
		['ReviewManID', '审核人']
	],
	defaultWhere:'preceiveplan.IsUse=1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.paramsFilterListeners();
		me.store.on({
			load: function(store, records, successful, eOpts) {
				var roonodes = me.getRootNode().childNodes; //获取主节点
				if(roonodes.length > 0) {
					me.getSelectionModel().select(me.root.tid);
				}
			}
		});
	},
	createDockedItems: function() {
		var me = this;
		var items = [{
			iconCls: 'button-refresh',
			itemId: 'refresh',
			tooltip: '刷新数据',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			iconCls: 'button-arrow-in',
			itemId: 'minus',
			tooltip: '全部收缩',
			handler: function() {
				me.onMinusClick();
			}
		}, {
			iconCls: 'button-arrow-out',
			itemId: 'plus',
			tooltip: '全部展开',
			handler: function() {
				me.onPlusClick();
			}
		}, '-', {
			width: 170,
			labelWidth: 55,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			itemId: 'DeptName',
			emptyText  : '部门',
			fieldLabel: '部门',
			className: 'Shell.class.wfm.service.accept.CheckGrid',
			classConfig: {
				title: '部门选择'
			}
		}, {
			xtype: 'textfield',
			itemId: 'DeptID',
			fieldLabel: '部门主键ID',
			hidden: true
		}, '-', {
			width: 125,
			labelWidth: 30,
			labelAlign: 'right',
			xtype: 'uxSimpleComboBox',
			itemId: 'UserType',
			fieldLabel: '人员',
			data: me.UserTypeList,
			value: me.defaultUserType
		}, {
			width: 70,
			xtype: 'uxCheckTrigger',
			itemId: 'UserName',
			className: 'Shell.class.sysbase.user.CheckApp',
			value: me.defaultUserName
		}, {
			xtype: 'textfield',
			itemId: 'UserID',
			fieldLabel: '申请人主键ID',
			hidden: true,
			value: me.defaultUserID
		}, '-', {
			width: 140,
			labelWidth: 55,
			labelAlign: 'right',
			xtype: 'uxSimpleComboBox',
			itemId: 'DateType',
			fieldLabel: '时间范围',
			data: me.DateTypeList,
			value: me.defaultDateType
		}, {
			width: 90,
			labelWidth: 5,
			labelAlign: 'right',
			fieldLabel: '',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(field, newValue, oldValue) {

				}
			}
		}, {
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		}];

		return [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		}];
	},
    createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '合同名称',
			xtype:'treecolumn',
			dataIndex: 'PContractName',
			width: 180,
			sortable: false
		}, {		
				text: '收款分期',
				dataIndex: 'text',
				width: 100,
				sortable: false
			},{
			text: '收款金额',
			dataIndex: 'ReceivePlanAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '时间',
			dataIndex: 'ExpectReceiveDate',
			width: 100,
			sortable: false,
			menuDisabled: false,
			type: 'date',
			xtype: 'datecolumn',
			format: 'Y-m-d',
			editor: {
				xtype: 'datefield',
				allowBlank: false,
				format: 'Y-m-d',
				listeners:{
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						if(newValue != null && newValue != "") {
							if(newValue != null) {
								newValue = Ext.util.Format.date(newValue.toString(), 'Y-m-d');
							}
							if(newValue != "") {
								newValue = JcallShell.Date.getDate(newValue);
							}
						}
						alert(newValue);
						record.set('ExpectReceiveDate', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '责任人',
			dataIndex: 'ReceiveManName',
			width: 100,
			sortable: false
		},{
			text: '已收',
			dataIndex: 'ReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '待收',
			dataIndex: 'UnReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		} ,{
			text: '状态',
			dataIndex: 'Status',
			width: 65,
			sortable: false,
			renderer: function(value, meta) {
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('PReceivePlanStatus', v);
					if(info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		},{
			text: '计划内容ID',
			dataIndex: 'ReceiveGradationID',
			hidden: true,
			width: 100,
			sortable: false
		}, {
			text: '责任人ID',
			dataIndex: 'ReceiveManID',
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			hidden: true,
			hideable: false
		}, {
			text: '父收款计划ID',
			dataIndex: 'PPReceivePlanID',
			hidden: true,
			hideable: false
		},{		
			text: '合同Id',
			dataIndex: 'PContractID',
			width: 100,
			hidden:true,
			sortable: false
		}];
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			buttonsToolbar = me.getComponent('topToolbar');
		if(!buttonsToolbar) return;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=PReceivePlan_PContractID,PReceivePlan_PContractName,PReceivePlan_ReceiveGradationName,PReceivePlan_ReceivePlanAmount,PReceivePlan_ExpectReceiveDate,PReceivePlan_ReceiveManName,PReceivePlan_ReceiveAmount,PReceivePlan_UnReceiveAmount,PReceivePlan_Status,add,PReceivePlan_ReceiveGradationID,PReceivePlan_ReceiveManID,PReceivePlan_Id';
		
	    var EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			DateType = buttonsToolbar.getComponent('DateType').getValue(),
			UserType = buttonsToolbar.getComponent('UserType').getValue(),
			UserID = buttonsToolbar.getComponent('UserID').getValue(),
			DeptID = buttonsToolbar.getComponent('DeptID').getValue();
		var EndDate2 = JcallShell.Date.toString(EndDate, true),
			BeginDate2 = JcallShell.Date.toString(BeginDate, true);
		//时间
		if(DateType) {
			if(BeginDate2 && EndDate2) {
				arr.push("preceiveplan." + DateType + " between '" + BeginDate2 + ' 00:00:00 ' + "' and '" + EndDate2 + " 23:59:59'");
			}
		}
		//部门
		if(DeptID) {
			arr.push("preceiveplan.ReceiveManID in (" + DeptID + ")");
		}
		//员工
		if(UserType && UserID) {
			arr.push("preceiveplan." + UserType + "='" + UserID + "'");
		}
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	paramsFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('topToolbar');
		if(!buttonsToolbar) return;
		
        var DeptName = buttonsToolbar.getComponent('DeptName'),
			DeptID = buttonsToolbar.getComponent('DeptID');
		if(DeptName) {
			DeptName.on({
				check: function(p, record) {
					var Id='',Name='';
					if(record){
						var HRDeptId= record ? record.get('HRDept_Id') : '';
						Name=record ? record.get('HRDept_CName') : '';
						if(HRDeptId){
							me.getEmpInfoById(HRDeptId,function(data){
								if(data.value && data.value.count > 0){
									for(var i=0;i<data.value.count;i++){
										if(i>0){
										   Id += ",";	
										}
										Id +=data.value.list[i].HREmployee_Id;
									}
								}
							});
						}
					
					}		
					DeptName.setValue(Name);
					DeptID.setValue(Id);
					me.load();
					p.close();
				}
			});
		}
		
		//人员类型+人员
		var UserType = buttonsToolbar.getComponent('UserType'),
			UserName = buttonsToolbar.getComponent('UserName'),
			UserID = buttonsToolbar.getComponent('UserID');
		if(UserType) {
			UserType.on({
				change: function() {
					if(UserID.getValue()) {
						me.load();
					}
				}
			});
		}
		if(UserName) {
			UserName.on({
				check: function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change: function() {
					if(UserType.getValue() && UserID.getValue()) {
						me.load();
					}
				}
			});
		}
		//时间类型+时间
		var EndDate = buttonsToolbar.getComponent('EndDate');
		var BeginDate = buttonsToolbar.getComponent('BeginDate');
		var DateType = buttonsToolbar.getComponent('DateType');
		if(DateType) {
			DateType.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.load();
					}
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.load();
					}
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.load();
					}
				}
			});
		}
	},
	  /**根据部门得到员工id*/
	getEmpInfoById:function(id,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl('/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true');
		var fields = ['Id'];
		url += '&fields=HREmployee_' + fields.join(',HREmployee_');
		url += '&where=id=' + id;

		JShell.Server.get(url, function(data) {
			callback(data);
		},false);
	}

});