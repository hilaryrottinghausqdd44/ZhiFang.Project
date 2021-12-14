/**
 * 合同价格表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.Form', {
	extend: 'Shell.ux.form.Panel',

	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '合同价格表单',
	width: 430,
	height: 200,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/StatService.svc/Stat_UDTO_AddDContractPrice',
	/**修改服务地址*/
	editUrl: '/StatService.svc/Stat_UDTO_UpdateDContractPriceByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	BDealerId: null,
	/**送检单位ID*/
	LaboratoryId: null,
	/**送检单位时间戳*/
	LaboratoryDataTimeStamp: null,

	/**送检单位默认开票方ID*/
	LaboratoryBillingUnitId: null,
	/**送检单位默认开票方名称*/
	LaboratoryBillingUnitName: null,
	/**送检单位默认开票方时间戳*/
	LaboratoryBillingUnitDataTimeStamp: null,

	/** 每个组件的默认属性*/
	defaults: {
		width: 200,
		labelWidth: 70,
		labelAlign: 'right'
	},

	afterRender: function() {
		var me = this;

		me.on('load', function() {
			var Id = me.getComponent('DContractPrice_BDealer_BBillingUnit_Id');
			var Name = me.getComponent('DContractPrice_BDealer_BBillingUnit_Name');
			var DataTimeStamp = me.getComponent('DContractPrice_BDealer_BBillingUnit_DataTimeStamp');
			//经销商信息
			me.BDealerBBillingUnitId = Id.getValue();
			me.BDealerBBillingUnitName = Name.getValue();
			me.BDealerBBillingUnitDataTimeStamp = DataTimeStamp.getValue();
		});

		me.callParent(arguments);
		//		if(me.BDealerId!=null){
		//			me.getComponent('DContractPrice_BDealer_Id').setValue(me.BDealerId);
		//		}
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'DContractPrice_Id',
			hidden: true,
			type: 'key'
		});

		items.push({
			x: 0,
			y: 10,
			fieldLabel: '开始日期',
			name: 'DContractPrice_BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});
		items.push({
			x: 210,
			y: 10,
			fieldLabel: '截止日期',
			name: 'DContractPrice_EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});

		//经销商
		items.push({
			x: 0,
			y: 40,
			fieldLabel: '经销商',
			allowBlank: false,
			name: 'DContractPrice_BDealer_Name',
			itemId: 'DContractPrice_BDealer_Name',
			xtype: 'trigger',
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
			name: 'DContractPrice_BDealer_Id',
			itemId: 'DContractPrice_BDealer_Id'
		});
		items.push({
			fieldLabel: '经销商时间戳',
			hidden: true,
			name: 'DContractPrice_BDealer_DataTimeStamp',
			itemId: 'DContractPrice_BDealer_DataTimeStamp'
		});

		items.push({
			fieldLabel: '经销商开票方主键ID',
			hidden: true,
			name: 'DContractPrice_BDealer_BBillingUnit_Id',
			itemId: 'DContractPrice_BDealer_BBillingUnit_Id'
		});
		items.push({
			fieldLabel: '经销商开票方名称',
			hidden: true,
			name: 'DContractPrice_BDealer_BBillingUnit_Name',
			itemId: 'DContractPrice_BDealer_BBillingUnit_Name'
		});
		items.push({
			fieldLabel: '经销商开票方时间戳',
			hidden: true,
			name: 'DContractPrice_BDealer_BBillingUnit_DataTimeStamp',
			itemId: 'DContractPrice_BDealer_BBillingUnit_DataTimeStamp'
		});

		//检验项目
		items.push({
			x: 210,
			y: 40,
			fieldLabel: '检验项目',
			allowBlank: false,
			name: 'DContractPrice_BTestItem_CName',
			itemId: 'DContractPrice_BTestItem_CName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				me.openBtestItemCheckGrid();
			}
		});
		items.push({
			fieldLabel: '检验项目主键ID',
			hidden: true,
			name: 'DContractPrice_BTestItem_Id',
			itemId: 'DContractPrice_BTestItem_Id'
		});
		items.push({
			fieldLabel: '检验项目时间戳',
			hidden: true,
			name: 'DContractPrice_BTestItem_DataTimeStamp',
			itemId: 'DContractPrice_BTestItem_DataTimeStamp'
		});

		items.push({
			fieldLabel: '检验项目主键ID',
			hidden: true,
			name: 'DContractPrice_DUnitItem_Id',
			itemId: 'DContractPrice_DUnitItem_Id'
		});
		items.push({
			fieldLabel: '检验项目时间戳',
			hidden: true,
			name: 'DContractPrice_DUnitItem_DataTimeStamp',
			itemId: 'DContractPrice_DUnitItem_DataTimeStamp'
		});

		items.push({
			x: 0,
			y: 70,
			fieldLabel: '合同编号',
			name: 'DContractPrice_ContractNo'
		});

		//科室-可以为空
		items.push({
			x: 210,
			y: 70,
			fieldLabel: '科室',
			name: 'DContractPrice_BDept_CName',
			itemId: 'DContractPrice_BDept_CName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.pki.labdept.CheckGrid', {
					resizable: false,
					defaultWhere: 'blabdept.BLaboratory.Id=' + me.LaboratoryId,
					listeners: {
						accept: function(p, record) {
							me.onDeptAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel: '科室主键ID',
			hidden: true,
			name: 'DContractPrice_BDept_Id',
			itemId: 'DContractPrice_BDept_Id'
		});
		items.push({
			fieldLabel: '科室时间戳',
			hidden: true,
			name: 'DContractPrice_BDept_DataTimeStamp',
			itemId: 'DContractPrice_BDept_DataTimeStamp'
		});
		//开票类型-可以为空
		items.push({
			x: 0,
			y: 100,
			fieldLabel: '开票类型',
			xtype: 'uxSimpleComboBox',
			name: 'DContractPrice_BillingUnitType',
			itemId: 'DContractPrice_BillingUnitType',
			data: JShell.PKI.Enum.getList('UnitType'),
			listeners: {
				change: function(field, newValue) {
					me.onBillingUnitTypeChange(newValue);
				}
			}
		});
		//开票方
		items.push({
			x: 210,
			y: 100,
			fieldLabel: '开票方',
			allowBlank: false,
			name: 'DContractPrice_BBillingUnit_Name',
			itemId: 'DContractPrice_BBillingUnit_Name',
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
			name: 'DContractPrice_BBillingUnit_Id',
			itemId: 'DContractPrice_BBillingUnit_Id'
		});
		items.push({
			fieldLabel: '开票方时间戳',
			hidden: true,
			name: 'DContractPrice_BBillingUnit_DataTimeStamp',
			itemId: 'DContractPrice_BBillingUnit_DataTimeStamp'
		});

		//是否阶梯价
		//		items.push({
		//			x:0,y:130,fieldLabel:'是否阶梯价',xtype:'uxBoolComboBox',
		//			name:'DContractPrice_IsStepPrice',
		//			itemId:'DContractPrice_IsStepPrice'
		//		});

		return items;
	},
	/**开票类型发生变化*/
	onBillingUnitTypeChange: function(value) {
		var me = this;

		switch (value) {
			case '0': //没有开票方
				me.changeBillingUnit(null, null, null);
				break;
			case '1': //送检单位
				me.changeBillingUnit(
					me.LaboratoryBillingUnitId,
					me.LaboratoryBillingUnitName,
					me.LaboratoryBillingUnitDataTimeStamp
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
		DataTimeStamp.setValue(record ? record.get('BDealer_DataTimeStamp') : '');

		me.BDealerBBillingUnitId = record ? record.get('BDealer_BBillingUnit_Id') : '';
		me.BDealerBBillingUnitName = record ? record.get('BDealer_BBillingUnit_Name') : '';
		me.BDealerBBillingUnitDataTimeStamp = record ? record.get('BDealer_BBillingUnit_DataTimeStamp') : '';

		var BillingUnitType = me.getComponent('DContractPrice_BillingUnitType');
		var v = BillingUnitType.getValue();
		if (v == '2') { //经销商
			me.changeBillingUnit(
				me.BDealerBBillingUnitId,
				me.BDealerBBillingUnitName,
				me.BDealerBBillingUnitDataTimeStamp
			);
		}
		//清空检验项目
		me.changeBTestItem();
	},
	/**检验项目选择确认处理*/
	onUnitItemAccept: function(record) {
		var me = this;
		if (record) {
			me.changeBTestItem(
				record.get('DUnitItem_BTestItem_Id'),
				record.get('DUnitItem_BTestItem_CName'),
				record.get('DUnitItem_BTestItem_DataTimeStamp'),
				record.get('DUnitItem_Id'),
				record.get('DUnitItem_DataTimeStamp')
			);
		} else {
			me.changeBTestItem();
		}
	},
	/**更改检验项目信息*/
	changeBTestItem: function(Id, Name, DataTimeStamp, DUnitItemName, DUnitItemDataTimeStamp) {
		var me = this;
		var ComId = me.getComponent('DContractPrice_BTestItem_Id');
		var ComName = me.getComponent('DContractPrice_BTestItem_CName');
		var ComDataTimeStamp = me.getComponent('DContractPrice_BTestItem_DataTimeStamp');

		var ComDUnitItemName = me.getComponent('DContractPrice_DUnitItem_Id');
		var ComDUnitItemDataTimeStamp = me.getComponent('DContractPrice_DUnitItem_DataTimeStamp');

		ComId.setValue(Id || '');
		ComName.setValue(Name || '');
		ComDataTimeStamp.setValue(DataTimeStamp || '');
		ComDUnitItemName.setValue(DUnitItemName || '');
		ComDUnitItemDataTimeStamp.setValue(DUnitItemDataTimeStamp || '');

	},
	/**打开项目选择列表*/
	openBtestItemCheckGrid: function() {
		var me = this;
		var Id = me.getComponent('DContractPrice_BDealer_Id').getValue();

		if (!Id) {
			JShell.Msg.error('请先选择经销商!');
			return;
		}

		//经销商和送检单位的项目
		//		var where = '(dunititem.UnitType=2 and dunititem.UnitID=' + Id +
		//			') or (dunititem.UnitType=1 and dunititem.UnitID=' + me.LaboratoryId + ')';

		var where = 'dunititem.UnitType=2 and dunititem.UnitID=' + Id;

		JShell.Win.open('Shell.class.pki.unititem.CheckGrid', {
			resizable: false,
			defaultWhere: where,
			listeners: {
				accept: function(p, record) {
					me.onUnitItemAccept(record);
					p.close();
				}
			}
		}).show();
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
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var listdunititem = [];
		var DUnitItem = {
			Id: values.DContractPrice_BTestItem_Id,
			DataTimeStamp: values.DContractPrice_BTestItem_DataTimeStamp.split(',')
		};
		var BTestItem = {
			Id: values.DContractPrice_DUnitItem_Id,
			DataTimeStamp: values.DContractPrice_DUnitItem_DataTimeStamp.split(',')
		};
		DUnitItem.BTestItem = BTestItem;
		listdunititem.push(DUnitItem);
		var entity = {
			BeginDate: JShell.Date.toServerDate(values.DContractPrice_BeginDate),
			EndDate: JShell.Date.toServerDate(values.DContractPrice_EndDate),
			ContractNo: values.DContractPrice_ContractNo,
			//IsStepPrice:values.DContractPrice_IsStepPrice == true ? '1' : '0',

			BDealer: {
				Id: values.DContractPrice_BDealer_Id,
				DataTimeStamp: values.DContractPrice_BDealer_DataTimeStamp.split(',')
			},
			//			BTestItem: {
			//				Id: values.DContractPrice_BTestItem_Id,
			//				DataTimeStamp: values.DContractPrice_BTestItem_DataTimeStamp.split(',')
			//			},

			BBillingUnit: {
				Id: values.DContractPrice_BBillingUnit_Id,
				DataTimeStamp: values.DContractPrice_BBillingUnit_DataTimeStamp.split(',')
			}
		};
		if (me.LaboratoryId != null && me.LaboratoryDataTimeStamp) {
			entity.BLaboratory = {
				Id: me.LaboratoryId,
				DataTimeStamp: me.LaboratoryDataTimeStamp.split(',')
			};
		}
		//开票类型
		if (values.DContractPrice_BillingUnitType) {
			entity.BillingUnitType = values.DContractPrice_BillingUnitType;
		}
		//科室
		if (values.DContractPrice_BDept_Id && values.DContractPrice_BDealer_DataTimeStamp) {
			entity.BDept = {
				Id: values.DContractPrice_BDept_Id,
				DataTimeStamp: values.DContractPrice_BDept_DataTimeStamp.split(',')
			};
		}

		//alert(Ext.encode(entity));
		return {
			entity: entity,
			listDUnitItem: listdunititem
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

		for (var i in fields) {
			var arr = fields[i].split('_');
			if (arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		entity.fields += ',BDealer_Id,BTestItem_Id,BBillingUnit_Id,BDept_Id'; //
		if (entity.BLaboratory != null) {
			entity.fields += ",BLaboratory_Id";
		}
		entity.entity.Id = values.DContractPrice_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		data.DContractPrice_BeginDate = JShell.Date.getDate(data.DContractPrice_BeginDate);
		data.DContractPrice_EndDate = JShell.Date.getDate(data.DContractPrice_EndDate);
		data.DContractPrice_IsStepPrice = data.DContractPrice_IsStepPrice == '1' ? true : false;
		return data;
	}
});