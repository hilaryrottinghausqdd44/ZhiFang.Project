/**
 * 新增合同表单
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dealercontract.AddForm', {
	extend: 'Shell.ux.form.Panel',

	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '新增合同表单',
	width: 280,
	height: 250,

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: false,

	layout: 'anchor',
	bodyPadding: 10,
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 60,
		labelAlign: 'right'
	},
	BDealerId: null,
//	BDealerDataTimeStamp: null,
	BDealerCName: "",
	/**送检单位默认开票方ID*/
	LaboratoryBillingUnitId: null,
	/**送检单位默认开票方名称*/
	LaboratoryBillingUnitName: null,
//	/**送检单位默认开票方时间戳*/
//	LaboratoryBillingUnitDataTimeStamp: null,
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'DContractPrice_Id',
			hidden: true
		});

		//开始日期
		items.push({
			fieldLabel: '开始日期',
			name: 'DContractPrice_BeginDate',
			emptyText: '必填',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});
		//截止日期
		items.push({
			fieldLabel: '截止日期',
			name: 'DContractPrice_EndDate',
			emptyText: '必填',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});
		//合同编号-可以为空
		items.push({
			fieldLabel: '合同编号',
			name: 'DContractPrice_ContractNo',
			allowBlank: false,
			emptyText: '必填',
		});

		items.push({
			fieldLabel: '勾选同时维护关系',
			labelAlign: 'right',
			itemId: 'checkUpdate',
			name: 'checkUpdate',
			labelWidth: 100,
			hidden: true,
			checked: false,
			value: false,
			inputValue: false,
			xtype: 'checkbox',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.defaultSetValue(newValue);
				}
			}
		});
		//经销商
		items.push({
			fieldLabel: '经销商',
			allowBlank: false,
			emptyText: '必填',
			hidden: true,
			locked: true,
			readOnly: true,
			name: 'DContractPrice_BDealer_Name',
			itemId: 'DContractPrice_BDealer_Name',
			xtype: 'trigger',
			value: me.BDealerCName,
			triggerCls: 'x-form-search-trigger',
			//enableKeyEvents:false,editable:false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.pki.dealer.CheckGrid', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onDealerAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel: '经销商主键ID',
			hidden: true,
			value: me.BDealerId,
			name: 'DContractPrice_BDealer_Id',
			itemId: 'DContractPrice_BDealer_Id'
		});
		items.push({
			fieldLabel: '经销商时间戳',
			hidden: true,
//			value: me.BDealerDataTimeStamp,
			name: 'DContractPrice_BDealer_DataTimeStamp',
			itemId: 'DContractPrice_BDealer_DataTimeStamp'
		});

		//科室-可以为空
		//		items.push({
		//			fieldLabel:'科室',
		//			name:'DContractPrice_BDept_CName',
		//			itemId:'DContractPrice_BDept_CName',
		//			xtype:'trigger',triggerCls:'x-form-search-trigger',
		//			enableKeyEvents:false,editable:false,
		//			onTriggerClick:function(){
		//				JShell.Win.open('Shell.class.pki.labdept.CheckGrid',{
		//					resizable:false,
		//					defaultWhere:'blabdept.BLaboratory.Id=' + me.LaboratoryId,
		//					listeners:{
		//						accept:function(p,record){me.onDeptAccept(record);p.close();}
		//					}
		//				}).show();
		//			}
		//		});
		//		items.push({
		//			fieldLabel:'科室主键ID',hidden:true,
		//			name:'DContractPrice_BDept_Id',
		//			itemId:'DContractPrice_BDept_Id'
		//		});
		//		items.push({
		//			fieldLabel:'科室时间戳',hidden:true,
		//			name:'DContractPrice_BDept_DataTimeStamp',
		//			itemId:'DContractPrice_BDept_DataTimeStamp'
		//		});

		//开票类型
		items.push({
			fieldLabel: '开票类型',
			hidden: true,
			//allowBlank: false,
			emptyText: '必填',
			name: 'DContractPrice_BillingUnitType',
			itemId: 'DContractPrice_BillingUnitType',
			value: '1',
			xtype: 'uxSimpleComboBox',
			data: JShell.PKI.Enum.getList('UnitType'),
			listeners: {
				change: function(field, newValue) {
					me.onBillingUnitTypeChange(newValue);
				}
			}
		});
		//开票方
		items.push({
			fieldLabel: '开票方',
			hidden: true,
			//allowBlank: false,
			emptyText: '必填',
			name: 'DContractPrice_BBillingUnit_Name',
			itemId: 'DContractPrice_BBillingUnit_Name',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			value: me.LaboratoryBillingUnitName,
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.pki.billingunit.CheckGrid', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onBillingUnitAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel: '开票方主键ID',
			hidden: true,
			value: me.LaboratoryBillingUnitId,
			name: 'DContractPrice_BBillingUnit_Id',
			itemId: 'DContractPrice_BBillingUnit_Id'
		});
		items.push({
			fieldLabel: '开票方时间戳',
			hidden: true,
//			value: me.LaboratoryBillingUnitDataTimeStamp,
			name: 'DContractPrice_BBillingUnit_DataTimeStamp',
			itemId: 'DContractPrice_BBillingUnit_DataTimeStamp'
		});
		//
		//
		//		//合作级别
		//		items.push({
		//			x:0,y:100,fieldLabel:'合作级别',xtype:'uxSimpleComboBox',
		//			name:'DContractPrice_CoopLevel',value:'1',
		//			itemId:'DContractPrice_CoopLevel',
		//			data:JShell.PKI.Enum.getList('CoopLevel')
		//		});

		return items;
	},
	defaultSetValue: function(newValue) {
		var me = this;
		var BDealerName = me.getComponent('DContractPrice_BDealer_Name');
		var BillingUnitType = me.getComponent('DContractPrice_BillingUnitType');
		var BBillingUnitName = me.getComponent('DContractPrice_BBillingUnit_Name');
		//var DContractPriceCoopLevel = me.getComponent('DContractPrice_CoopLevel');
		if (BDealerName) {
			BDealerName.setReadOnly(!newValue);
			BDealerName.allowBlank = !newValue;
		}
		if (BillingUnitType) {
			BillingUnitType.setReadOnly(!newValue);
			BillingUnitType.allowBlank = !newValue;
		}
		if (BBillingUnitName) {
			BBillingUnitName.setReadOnly(!newValue);
			BBillingUnitName.allowBlank = !newValue;
		}
		//		if (DContractPriceCoopLevel) {
		//			DContractPriceCoopLevel.setReadOnly(!newValue);
		//			DContractPriceCoopLevel.allowBlank = !newValue;
		//		}
	},
	/**开票类型发生变化*/
	onBillingUnitTypeChange: function(value) {
		var me = this;

		switch (value) {
			case '0': //没有开票方
				me.changeBillingUnit();
				break;
			case '1': //送检单位
				me.changeBillingUnit(
					me.LaboratoryBillingUnitId,
					me.LaboratoryBillingUnitName
//					me.LaboratoryBillingUnitDataTimeStamp
				);
				break;
			case '2': //经销商
				me.changeBillingUnit(
					me.BDealerBBillingUnitId,
					me.BDealerBBillingUnitName,
					me.BDealerBBillingUnitDataTimeStamp
				);
				break;
		}
	},
	/**经销商选择确认处理*/
	onDealerAccept: function(record) {
		var me = this;
		var Id = me.getComponent('DContractPrice_BDealer_Id');
		var Name = me.getComponent('DContractPrice_BDealer_Name');
		var DataTimeStamp = me.getComponent('DContractPrice_BDealer_DataTimeStamp');

		Id.setValue(record ? record.get('BDealer_Id') : '');
		Name.setValue(record ? record.get('BDealer_Name') : '');
		//DataTimeStamp.setValue(record ? record.get('BDealer_DataTimeStamp') : '');
//      DataTimeStamp.setValue('0,0,0,0,0,0,0,0');

		me.BDealerBBillingUnitId = record ? record.get('BDealer_BBillingUnit_Id') : '';
		me.BDealerBBillingUnitName = record ? record.get('BDealer_BBillingUnit_Name') : '';
//		me.BDealerBBillingUnitDataTimeStamp = record ? record.get('BDealer_BBillingUnit_DataTimeStamp') : '';
//      me.BDealerBBillingUnitDataTimeStamp='0,0,0,0,0,0,0,0';
		var BillingUnitType = me.getComponent('DContractPrice_BillingUnitType');
		var v = BillingUnitType.getValue();
		if (v == '2') { //经销商
			me.changeBillingUnit(
				me.BDealerBBillingUnitId,
				me.BDealerBBillingUnitName
//				me.BDealerBBillingUnitDataTimeStamp
			);
		}
	},

	/**科室选择确认处理*/
	onDeptAccept: function(record) {
		var me = this;
		var Id = me.getComponent('DContractPrice_BDept_Id');
		var Name = me.getComponent('DContractPrice_BDept_CName');
		var DataTimeStamp = me.getComponent('DContractPrice_BDept_DataTimeStamp');

		Id.setValue(record ? record.get('BLabDept_BDept_Id') : '');
		Name.setValue(record ? record.get('BLabDept_BDept_CName') : '');
		DataTimeStamp.setValue(record ? record.get('BLabDept_BDept_DataTimeStamp') : '');
	},
	/**开票方选择确认处理*/
	onBillingUnitAccept: function(record) {
		var me = this;
		if (record) {
			me.changeBillingUnit(
				record.get('BBillingUnit_Id'),
				record.get('BBillingUnit_Name'),
				record.get('BBillingUnit_DataTimeStamp')
			);
		} else {
			me.changeBillingUnit();
		}
	},
	/**更改开票方信息*/
	changeBillingUnit: function(Id, Name, DataTimeStamp) {
		var me = this;
		var ComId = me.getComponent('DContractPrice_BBillingUnit_Id');
		var ComName = me.getComponent('DContractPrice_BBillingUnit_Name');
		var ComDataTimeStamp = me.getComponent('DContractPrice_BBillingUnit_DataTimeStamp');

		ComId.setValue(Id || '');
		ComName.setValue(Name || '');
		ComDataTimeStamp.setValue(DataTimeStamp || '');
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		this.fireEvent('save', this);
	}
});