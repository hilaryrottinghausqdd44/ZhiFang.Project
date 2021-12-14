/**
 * 新增经销商与送检单位表单
 * @author longfc
 * @version 2016-05-18
 */
Ext.define('Shell.class.pki.dunitdealer.AddForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '经销商与送检单位',
	width: 520,
	height: 220,
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDUnitDealerRelationById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BaseService.svc/ST_UDTO_AddDUnitDealerRelation',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateDUnitDealerRelationByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: false,

	//layout: 'anchor',
	bodyPadding: 10,
	/** 每个组件的默认属性*/
	defaults: {
		//anchor: '100%',
		labelWidth: 60,
		labelAlign: 'right'
	},

	BDealerBBillingUnitId: null,
	BDealerBBillingUnitName: null,
	BDealerBBillingUnitDataTimeStamp: null,
	BDealerId: null,
	BDealerName: null,
	BDealerDataTimeStamp: null,
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		//开始日期格式化
		if(data.DUnitDealerRelation_BeginDate&&data.DUnitDealerRelation_BeginDate.toString().length>0){
			data.DUnitDealerRelation_BeginDate=Ext.util.Format.date(data.DUnitDealerRelation_BeginDate, 'Y-m-d');
		}
		//截止日期格式化
		if(data.DUnitDealerRelation_EndDate&&data.DUnitDealerRelation_EndDate.toString().length>0){
			data.DUnitDealerRelation_EndDate=Ext.util.Format.date(data.DUnitDealerRelation_EndDate, 'Y-m-d');
		}
		return data;
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'DUnitDealerRelation_Id',
			hidden: true
		});

		//开始日期
		items.push({
			fieldLabel: '开始日期',
			x: 0,
			y: 10,
			name: 'DUnitDealerRelation_BeginDate',
			emptyText: '必填',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});
		//截止日期
		items.push({
			fieldLabel: '截止日期',
			x: 235,
			y: 10,
			name: 'DUnitDealerRelation_EndDate',
			emptyText: '可以为空',
			xtype: 'datefield',
			format: 'Y-m-d'
			//allowBlank: false
		});
		//项目
		items.push({
			fieldLabel: '项目',
			x: 0,
			y: 40,
			allowBlank: false,
			emptyText: '必填',
			name: 'DUnitDealerRelation_BTestItem_CName',
			itemId: 'DUnitDealerRelation_BTestItem_CName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.pki.item.CheckGrid', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onBTestItemAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel: '项目ID',
			hidden: true,
			name: 'DUnitDealerRelation_BTestItem_Id',
			itemId: 'DUnitDealerRelation_BTestItem_Id'
		});
		items.push({
			fieldLabel: '项目时间戳',
			hidden: true,
			name: 'DUnitDealerRelation_BTestItem_DataTimeStamp',
			itemId: 'DUnitDealerRelation_BTestItem_DataTimeStamp'
		});
		//送检单位
		items.push({
			fieldLabel: '送检单位',
			allowBlank: false,
			x: 235,
			y: 40,
			emptyText: '必填',
			name: 'DUnitDealerRelation_BLaboratory_CName',
			itemId: 'DUnitDealerRelation_BLaboratory_CName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.pki.laboratory.CheckGrid', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onLaboratoryAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel: '送检单位ID',
			hidden: true,
			name: 'DUnitDealerRelation_BLaboratory_Id',
			itemId: 'DUnitDealerRelation_BLaboratory_Id'
		});
		items.push({
			fieldLabel: '送检单位时间戳',
			hidden: true,
			name: 'DUnitDealerRelation_BLaboratory_DataTimeStamp',
			itemId: 'DUnitDealerRelation_BLaboratory_DataTimeStamp'
		});
		//开票类型
		items.push({
			fieldLabel: '开票类型',
			allowBlank: false,
			x: 0,
			y: 70,
			value: '2',
			emptyText: '必填',
			name: 'DUnitDealerRelation_BillingUnitType',
			itemId: 'DUnitDealerRelation_BillingUnitType',
			value: '2',
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
			//allowBlank: false,
			x: 235,
			y: 70,
			//hidden: true,
			//emptyText: '必填',
			value: me.BDealerBBillingUnitName,
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

		//合作级别
		items.push({
			x: 0,
			y: 100,
			fieldLabel: '合作级别',
			value: '1',
			xtype: 'uxSimpleComboBox',
			//hidden: true,
			name: 'DUnitDealerRelation_CoopLevel',
			value: '1',
			itemId: 'DUnitDealerRelation_CoopLevel',
			data: JShell.PKI.Enum.getList('CoopLevel'),
			listeners: {
				change: function(field, newValue) {
					//me.onBillingUnitTypeChange(newValue);
					var com=me.getComponent("DUnitDealerRelation_BDept_CName");

					switch (newValue){
						case 2:
							com.setVisible(true);
							break;
						default:
						com.setVisible(true);
							break;
					}
				}
			}
		});
		//科室-可以为空
		items.push({
			fieldLabel: '科室',
			hidden: true,
			x: 235,
			y: 100,
			name: 'DUnitDealerRelation_BDept_CName',
			itemId: 'DUnitDealerRelation_BDept_CName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				var laboratoryId = me.getComponent("DUnitDealerRelation_BLaboratory_Id");
				if (laboratoryId && laboratoryId.getValue().toString().length > 0) {
					JShell.Win.open('Shell.class.pki.labdept.CheckGrid', {
						resizable: false,
						defaultWhere: 'blabdept.BLaboratory.Id=' + laboratoryId.getValue().toString(),
						listeners: {
							accept: function(p, record) {
								me.onDeptAccept(record);
								p.close();
							}
						}
					}).show();
				} else {
					JShell.Msg.alert("请选择送检单位后再选择科室", null, me.hideTimes);
				}
			}
		});
		items.push({
			fieldLabel: '科室主键ID',
			hidden: true,
			name: 'DUnitDealerRelation_BDept_Id',
			itemId: 'DUnitDealerRelation_BDept_Id'
		});
		items.push({
			fieldLabel: '科室时间戳',
			hidden: true,
			name: 'DUnitDealerRelation_BDept_DataTimeStamp',
			itemId: 'DUnitDealerRelation_BDept_DataTimeStamp'
		});
		//经销商
		items.push({
			fieldLabel: '经销商',
			//allowBlank: false,
			hidden: true,
			value: me.BDealerName,
			emptyText: '必填',
			name: 'DUnitDealerRelation_BDealer_Name',
			itemId: 'DUnitDealerRelation_BDealer_Name',
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
		//合同编号-可以为空
		//		items.push({
		//			fieldLabel: '合同编号',
		//			hidden: true,
		//			name: 'DUnitDealerRelation_ContractNo',
		//			//allowBlank: false,
		//			emptyText: '必填',
		//		});
		return items;
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
					me.BDealerBBillingUnitId,
					me.BDealerBBillingUnitName,
					me.BDealerBBillingUnitDataTimeStamp
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
	onLaboratoryAccept: function(record) {
		var me = this;
		var Id = me.getComponent('DUnitDealerRelation_BLaboratory_Id');
		var Name = me.getComponent('DUnitDealerRelation_BLaboratory_CName');
		var DataTimeStamp = me.getComponent('DUnitDealerRelation_BLaboratory_DataTimeStamp');

		Id.setValue(record ? record.get('BLaboratory_Id') : '');
		Name.setValue(record ? record.get('BLaboratory_CName') : '');
		DataTimeStamp.setValue(record ? record.get('BLaboratory_DataTimeStamp') : '');

	},
	/*项目选择列表*/
	onBTestItemAccept: function(record) {
		var me = this;
		var Id = me.getComponent('DUnitDealerRelation_BTestItem_Id');
		var Name = me.getComponent('DUnitDealerRelation_BTestItem_CName');
		var DataTimeStamp = me.getComponent('DUnitDealerRelation_BTestItem_DataTimeStamp');

		Id.setValue(record ? record.get('BTestItem_Id') : '');
		Name.setValue(record ? record.get('BTestItem_CName') : '');
		DataTimeStamp.setValue(record ? record.get('BTestItem_DataTimeStamp') : '');

	},
	/**经销商选择确认处理*/
	onDealerAccept: function(record) {
		var me = this;
		var Id = me.getComponent('DUnitDealerRelation_BDealer_Id');
		var Name = me.getComponent('DUnitDealerRelation_BDealer_Name');
		var DataTimeStamp = me.getComponent('DUnitDealerRelation_BDealer_DataTimeStamp');

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
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			BeginDate: JShell.Date.toServerDate(values.DUnitDealerRelation_BeginDate),
			BDealer: {
				Id: me.BDealerId,
				DataTimeStamp: me.BDealerDataTimeStamp.split(',')
			}
		};
		if (values.DUnitDealerRelation_EndDate && values.DUnitDealerRelation_EndDate != null) {
	entity.EndDate = JShell.Date.toServerDate(values.DUnitDealerRelation_EndDate);
}
		//检验项目
		if (values.DUnitDealerRelation_BTestItem_Id) {
			entity.BTestItem = {
				Id: values.DUnitDealerRelation_BTestItem_Id,
				DataTimeStamp: values.DUnitDealerRelation_BTestItem_DataTimeStamp.split(',')
			};
		}
		//送检单位
		if (values.DUnitDealerRelation_BLaboratory_Id) {
			entity.BLaboratory = {
				Id: values.DUnitDealerRelation_BLaboratory_Id,
				DataTimeStamp: values.DUnitDealerRelation_BLaboratory_DataTimeStamp.split(',')
			};
		}
		//开票类型
		if (values.DUnitDealerRelation_BillingUnitType) {
			entity.BillingUnitType = values.DUnitDealerRelation_BillingUnitType;
		}

		//开票方
		if (values.DUnitDealerRelation_BBillingUnit_Id) {
			entity.BBillingUnit = {
				Id: values.DUnitDealerRelation_BBillingUnit_Id,
				DataTimeStamp: values.DUnitDealerRelation_BBillingUnit_DataTimeStamp.split(',')
			};
			//经销商的开票方子对象封装
			entity.BDealer.BBillingUnit=entity.BBillingUnit;
			//送检单位的开票方子对象封装
			if(entity.BLaboratory){
				entity.BLaboratory.BBillingUnit=entity.BBillingUnit;
			}
		}
		//合作级别
		if (values.DUnitDealerRelation_CoopLevel) {
			entity.CoopLevel = values.DUnitDealerRelation_CoopLevel;
		}
		//科室
		if (values.DUnitDealerRelation_BDept_Id) {
			entity.BDept = {
				Id: values.DUnitDealerRelation_BDept_Id,
				DataTimeStamp: values.DUnitDealerRelation_BDept_DataTimeStamp.split(',')
			};
		}
		return {
			entity: entity
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
		entity.fields += ',BDealer_Id,BTestItem_Id,BLaboratory_Id,BBillingUnit_Id'; //
		if (entity.BLaboratory != null) {
			entity.fields += ",DUnitDealerRelation_Id";
		}
		entity.entity.Id = values.DUnitDealerRelation_Id;
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

		if (!me.getForm().isValid()) return;

		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if (!params) return;

		params = Ext.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				me.fireEvent('save', me);
				if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if (msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '经销商代码有重复';
				}
				JShell.Msg.error(msg);
			}
		});
		//this.fireEvent('save', this);
	}
});