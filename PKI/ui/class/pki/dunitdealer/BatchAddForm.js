/**
 * 批量新增表单
 * @author longfc
 * @version 2016-05-18
 */
Ext.define('Shell.class.pki.dunitdealer.BatchAddForm', {
	extend: 'Shell.ux.form.Panel',

	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '经销商与送检单位批量',
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

	BDealerBBillingUnitId: null,
	BDealerBBillingUnitName: null,
	BDealerBBillingUnitDataTimeStamp: null,
	BDealerId: null,
	BDealerName: null,
	BDealerDataTimeStamp: null,
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.addEvents('onDealerAccept');
		items.push({
			fieldLabel: '主键ID',
			name: 'DUnitDealerRelation_Id',
			hidden: true
		});

		//开始日期
		items.push({
			fieldLabel: '开始日期',
			name: 'DUnitDealerRelation_BeginDate',
			emptyText: '必填',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});
		//截止日期
		items.push({
			fieldLabel: '截止日期',
			name: 'DUnitDealerRelation_EndDate',
			emptyText: '可以为空',
			xtype: 'datefield',
			format: 'Y-m-d'
				//allowBlank: false
		});

		//经销商
		items.push({
			fieldLabel: '经销商',
			allowBlank: false,
			emptyText: '必填',
			name: 'DUnitDealerRelation_BDealer_Name',
			itemId: 'DUnitDealerRelation_BDealer_Name',
			xtype: 'trigger',
			locked:true,
			readOnly: true,
			value: me.BDealerName,
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
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
			name: 'DUnitDealerRelation_BDealer_Id',
			itemId: 'DUnitDealerRelation_BDealer_Id'
		});
		items.push({
			fieldLabel: '经销商时间戳',
			hidden: true,
			value: me.BDealerDataTimeStamp,
			name: 'DUnitDealerRelation_BDealer_DataTimeStamp',
			itemId: 'DUnitDealerRelation_BDealer_DataTimeStamp'
		});

		//开票类型
		items.push({
			fieldLabel: '开票类型',
			allowBlank: false,
			emptyText: '必填',
			name: 'DUnitDealerRelation_BillingUnitType',
			itemId: 'DUnitDealerRelation_BillingUnitType',
			value: '2',
			locked:true,
			readOnly:true,
			xtype: 'uxSimpleComboBox',
			data: JShell.PKI.Enum.getList('UnitType'),
			listeners: {
				change: function(field, newValue) {
					me.onBillingUnitTypeChange(newValue);
				}
			}
		});
		//合作级别
		items.push({
			x: 0,
			y: 100,
			fieldLabel: '合作级别',
			xtype: 'uxSimpleComboBox',
			//hidden: true,
			name: 'DUnitDealerRelation_CoopLevel',
			value: '1',
			itemId: 'DUnitDealerRelation_CoopLevel',
			data: JShell.PKI.Enum.getList('CoopLevel')
		});
		//开票方
		items.push({
			fieldLabel: '开票方',
			//allowBlank: false,
			//hidden: true,
			value: me.BDealerBBillingUnitName,
			//emptyText: '必填',
			name: 'DUnitDealerRelation_BBillingUnit_Name',
			itemId: 'DUnitDealerRelation_BBillingUnit_Name',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
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
			value: me.BDealerBBillingUnitId,
			name: 'DUnitDealerRelation_BBillingUnit_Id',
			itemId: 'DUnitDealerRelation_BBillingUnit_Id'
		});
		items.push({
			fieldLabel: '开票方时间戳',
			hidden: true,
			value: me.BDealerBBillingUnitDataTimeStamp,
			name: 'DUnitDealerRelation_BBillingUnit_DataTimeStamp',
			itemId: 'DUnitDealerRelation_BBillingUnit_DataTimeStamp'
		});

		items.push({
			xtype: 'displayfield',
			//labelWidth:60,
			fieldLabel: '',
			hidden: true,
			itemId: "displayfieldText",
			name: 'displayfieldText',
//			style: {
//				fontSize: '16px',
//				fontWeight:'bold',
//				color: 'red'
//			},
			fieldStyle: "font-size:14px;font-weight:bold;color:blue;",
			value: '&nbsp;&nbsp;&nbsp;&nbsp;开票方采用送检单位的开票方'
		});
		return items;
	},
	/**开票类型发生变化*/
	onBillingUnitTypeChange: function(value) {
		var me = this;
		var ComName = me.getComponent('DUnitDealerRelation_BBillingUnit_Name');
		var displayfieldText = me.getComponent('displayfieldText');
		ComName.setVisible(false);
		if (displayfieldText) {
			displayfieldText.setVisible(false);
		}
		switch (value) {
			case '0': //没有开票方
				ComName.setVisible(false);
				if (displayfieldText) {
					displayfieldText.setVisible(false);
				}
				me.changeBillingUnit();

				break;
			case '1': //送检单位,开票类型送检单位时，显示‘开票方采用送检单位的开票方“文字
				ComName.setVisible(false);
				if (displayfieldText) {
					displayfieldText.setVisible(true);
				}

				me.changeBillingUnit(
					me.BDealerBBillingUnitId,
					me.BDealerBBillingUnitName,
					me.BDealerBBillingUnitDataTimeStamp
				);
				break;
			case '2': //经销商
				if (displayfieldText) {
					displayfieldText.setVisible(false);
				}
				ComName.setVisible(true);
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

		var Id = me.getComponent('DUnitDealerRelation_BDealer_Id');
		var Name = me.getComponent('DUnitDealerRelation_BDealer_Name');
		var DataTimeStamp = me.getComponent('DUnitDealerRelation_BDealer_DataTimeStamp');
		me.BDealerId = Id;
		me.BDealerName = Name;
		me.BDealerDataTimeStamp = DataTimeStamp;

		Id.setValue(record ? record.get('BDealer_Id') : '');
		Name.setValue(record ? record.get('BDealer_Name') : '');
		DataTimeStamp.setValue(record ? record.get('BDealer_DataTimeStamp') : '');

		me.BDealerBBillingUnitId = record ? record.get('BDealer_BBillingUnit_Id') : '';
		me.BDealerBBillingUnitName = record ? record.get('BDealer_BBillingUnit_Name') : '';
		me.BDealerBBillingUnitDataTimeStamp = record ? record.get('BDealer_BBillingUnit_DataTimeStamp') : '';

		var BillingUnitType = me.getComponent('DUnitDealerRelation_BillingUnitType');
		var v = BillingUnitType.getValue();
		if (v == '2') { //经销商
			me.changeBillingUnit(
				me.BDealerBBillingUnitId,
				me.BDealerBBillingUnitName,
				me.BDealerBBillingUnitDataTimeStamp
			);
		}
		me.fireEvent('onDealerAccept', record);
	},

	/**科室选择确认处理*/
	onDeptAccept: function(record) {
		var me = this;
		var Id = me.getComponent('DUnitDealerRelation_BDept_Id');
		var Name = me.getComponent('DUnitDealerRelation_BDept_CName');
		var DataTimeStamp = me.getComponent('DUnitDealerRelation_BDept_DataTimeStamp');

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
		var ComId = me.getComponent('DUnitDealerRelation_BBillingUnit_Id');
		var ComName = me.getComponent('DUnitDealerRelation_BBillingUnit_Name');
		var ComDataTimeStamp = me.getComponent('DUnitDealerRelation_BBillingUnit_DataTimeStamp');

		ComId.setValue(Id || '');
		ComName.setValue(Name || '');
		ComDataTimeStamp.setValue(DataTimeStamp || '');
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		this.fireEvent('save', this);
	}
});