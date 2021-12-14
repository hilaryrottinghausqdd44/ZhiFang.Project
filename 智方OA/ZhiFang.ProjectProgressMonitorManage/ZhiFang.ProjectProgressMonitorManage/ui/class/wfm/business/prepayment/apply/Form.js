/**
 * 还款表单
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.prepayment.apply.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '还款申请',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 300,
	height: 240,
	bodyPadding: '20px 20px 10px 20px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPRepaymentById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPRepayment',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePRepaymentByField',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/*项目类别*/
	ProjectTypeName: 'ItemType',
	/*内容类型*/
	PRepaymentContentTypeName: 'LoanBillContentType',
	Status: 1,
	/*所属部门ID*/
	DeptID: '',
	/*所属部门*/
	DeptName: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '内容类型',
			name: 'PRepayment_PRepaymentContentTypeID',
			itemId: 'PRepayment_PRepaymentContentTypeID',
			hidden: true
		}, {
			fieldLabel: '还款内容',
			name: 'PRepayment_PRepaymentContentTypeName',
			xtype: 'uxCheckTrigger',
			itemId: 'PRepayment_PRepaymentContentTypeName',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '内容类型选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.PRepaymentContentTypeName + "'"
			}
		}, {
			fieldLabel: '还款人',
			name: 'PRepayment_ApplyManID',
			itemId: 'PRepayment_ApplyManID',
			hidden: true
		}, {
			fieldLabel: '还款人',
			name: 'PRepayment_ApplyMan',
			itemId: 'PRepayment_ApplyMan',
			hidden: true,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.business.prepayment.basic.CheckApp'
		}, {
			fieldLabel: '还款金额',
			name: 'PRepayment_PRepaymentAmount',
			itemId: 'PRepayment_PRepaymentAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			height: 80,
			emptyText: '还款说明',
			fieldLabel: '还款说明',
			//			fieldWidth: 4,
			name: 'PRepayment_PRepaymentMemo',
			xtype: 'textarea'
		}, {
			fieldLabel: '主键ID',
			name: 'PRepayment_Id',
			hidden: true
		}, {
			fieldLabel: '部门',
			name: 'PRepayment_DeptID',
			itemId: 'PRepayment_DeptID',
			hidden: true
		}, {
			fieldLabel: '部门',
			hidden: true,
			name: 'PRepayment_DeptName',
			itemId: 'PRepayment_DeptName'
		});
		return items;
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//		//还款类型
		//		var ApplyMan = me.getComponent('PRepayment_ApplyMan'),
		//			ApplyManID = me.getComponent('PRepayment_ApplyManID');
		//		var DeptName = me.getComponent('PRepayment_DeptName'),
		//			DeptID = me.getComponent('PRepayment_DeptID');
		//		if(ApplyMan) {
		//			ApplyMan.on({
		//				check: function(p, record) {
		//					ApplyMan.setValue(record ? record.get('HREmployee_CName') : '');
		//					ApplyManID.setValue(record ? record.get('HREmployee_Id') : '');
		//					DeptName.setValue(record ? record.get('HREmployee_HRDept_CName') : '');
		//					DeptID.setValue(record ? record.get('HREmployee_HRDept_Id') : '');
		//					p.close();
		//				}
		//			});
		//		}
		//字典监听
		var dictList = ['PRepaymentContentType'];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}
	},
	/**字典监听*/
	doDictListeners: function(name) {
		var me = this;
		var CName = me.getComponent('PRepayment_' + name + 'Name');
		var Id = me.getComponent('PRepayment_' + name + 'ID');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				p.close();
			}
		});
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Status: me.Status,
			IsUse: true
		};
		if(values.PRepayment_PRepaymentContentTypeName) {
			entity.PRepaymentContentTypeName = values.PRepayment_PRepaymentContentTypeName;
			entity.PRepaymentContentTypeID = values.PRepayment_PRepaymentContentTypeID;
		}

		if(values.PRepayment_PRepaymentMemo) {
			entity.PRepaymentMemo = values.PRepayment_PRepaymentMemo.replace(/\\/g, '&#92');
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var DeptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		var DeptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		if(userId) {
			entity.ApplyManID = userId;
			entity.ApplyMan = userName;
			entity.DeptName = DeptName;
			entity.DeptID = DeptId;
		}
		if(values.PRepayment_PRepaymentAmount) {
			entity.PRepaymentAmount = values.PRepayment_PRepaymentAmount;
		}
		//申请时间
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyDate = JcallShell.Date.toString(Sysdate);
		if(ApplyDate) {
			entity.ApplyDate = JShell.Date.toServerDate(ApplyDate);
		}
		//		entity.OperationMemo = '还款申请';
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = [
			'DeptID', 'Id', 'Status', 'DeptName', 'PRepaymentContentTypeName', 'PRepaymentAmount',
			'PRepaymentContentTypeID', 'PRepaymentTypeID', 'PRepaymentMemo'
		];
		entity.fields = fields.join(',');
		if(values.PFinanceReceive_Id != '') {
			entity.entity.Id = values.PRepayment_Id;
		}
		return entity;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			items.push({
				text: '暂存',
				iconCls: 'button-save',
				tooltip: '暂存',
				handler: function() {
					me.onSave(false);
				}
			});
			if(me.hasSave) items.push({
				text: '提交',
				iconCls: 'button-save',
				tooltip: '提交',
				handler: function() {
					me.onSave(true);
				}
			});

			if(me.hasSaveas) items.push('saveas');
			if(me.hasReset) items.push('reset');
			if(me.hasCancel) items.push('cancel');
			items.push({
				text: '关闭',
				iconCls: 'button-del',
				tooltip: '关闭',
				handler: function() {
					me.close();
				}
			});
			if(items.length > 0) items.unshift('->');
		}

		if(items.length == 0) return null;
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: hidden
		});
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit) {
		var me = this,
			values = me.getForm().getValues();
		if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return;
		}
		if(isSubmit) { //提交
			//申请状态
			me.Status = 2;
			me.onSaveClick();
		} else { //暂存
			//暂存状态
			me.Status = 1;
			me.onSaveClick();
		}
	}
});