/**
 * 经销商项目阶梯价格表单
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dealer.contractprice.Form', {
	extend: 'Shell.ux.form.Panel',

	title: '经销商阶梯价格表单',
	date_error: '开始和截止时间保证为整月!',
	width: 240,
	height: 260,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/StatService.svc/Stat_UDTO_AddDContractStepPrice',
	/**修改服务地址*/
	editUrl: '/StatService.svc/Stat_UDTO_UpdateDContractPriceByField',
	//	ContractPriceType: '0',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/**经销商ID*/
	DealerId: null,
	/**经销商时间戳*/
	DealerDataTimeStamp: null,
	/**送检单位ID*/
	BLaboratoryId: null,
	/**送检单位时间戳*/
	BLaboratoryDataTimeStamp: null,
	/**项目ID*/
	ItemId: null,
	/**项目时间戳*/
	ItemDataTimeStamp: null,
	ContractNoIsDisabled: true,
	/**开始日期*/
	BeginDate: '',
	/**结束日期*/
	EndDate: '',
	/**合同编号*/
	ContractNo: '',
	/**编辑时，阶梯价为否数量不可编辑*/
	editIsStepPrice: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//var comBeginDate = me.getComponent("DContractPrice_BeginDate");
		//var com2 = me.getComponent("DContractPrice_EndDate");
		//comBeginDate.setReadOnly(true);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'DContractPrice_Id',
			hidden: true
		});

		items.push({
			x: 0,
			y: 10,
			fieldLabel: '开始日期',
			name: 'DContractPrice_BeginDate',
			itemId: 'DContractPrice_BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			hasReadOnly: true,
			disabled: true,
			//readonly:true,
			value: me.BeginDate,
			allowBlank: false
		});
		items.push({
			x: 0,
			y: 40,
			fieldLabel: '截止日期',
			name: 'DContractPrice_EndDate',
			itemId: 'DContractPrice_EndDate',
			xtype: 'datefield',
			disabled: true,
			value: me.EndDate,
			format: 'Y-m-d',
			allowBlank: false
		});
		items.push({
			x: 0,
			y: 70,
			//width: 210,
			fieldLabel: '合同编号',
			itemId: 'DContractPrice_ContractNo',
			locked: true,
			value: me.ContractNo,
			hasReadOnly: true,
			disabled: true,
			readonly: me.ContractNoIsDisabled,
			name: 'DContractPrice_ContractNo'
		});
		items.push({
			x: 0,
			y: 100,
			fieldLabel: '数量要求',
			itemId: 'DContractPrice_SampleCount',
			name: 'DContractPrice_SampleCount',
			xtype: 'numberfield',
			disabled: me.editIsStepPrice,
			allowBlank: false
		});
		items.push({
			x: 0,
			y: 130,
			fieldLabel: '价格',
			itemId: 'DContractPrice_StepPrice',
			name: 'DContractPrice_StepPrice',
			xtype: 'numberfield',
			allowBlank: false
		});
		items.push({
			x: 0,
			y: 160,
			labelWidth: 80,
			fieldLabel: '是否阶梯价',
			itemId: 'DContractPrice_IsStepPrice',
			name: 'DContractPrice_IsStepPrice',
			xtype: 'checkbox',
			checked: true,
			inputValue: 1
		});
		items.push({
			fieldLabel: '经销商ID',
			value: me.DealerId,
			itemId: 'DContractPrice_BDealer_Id',
			name: 'DContractPrice_BDealer_Id',
			hidden: true
		});
		items.push({
			fieldLabel: '项目ID',
			value: me.ItemId,
			itemId: 'DContractPrice_BTestItem_Id',
			name: 'DContractPrice_BTestItem_Id',
			hidden: true
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var IsStepPrice = values.DContractPrice_IsStepPrice;
		IsStepPrice = IsStepPrice == null ? 0 : IsStepPrice;
		var SampleCount = values.DContractPrice_SampleCount;
		SampleCount = SampleCount == null ? 0 : SampleCount;
		var entity = {
			BeginDate: JShell.Date.toServerDate(me.getComponent("DContractPrice_BeginDate").getValue()),
			EndDate: JShell.Date.toServerDate(me.getComponent("DContractPrice_EndDate").getValue()),
			SampleCount: SampleCount,
			StepPrice: values.DContractPrice_StepPrice,
			ContractNo: me.getComponent("DContractPrice_ContractNo").getValue(),
			IsStepPrice: IsStepPrice,
			BTestItem: {
				Id: me.ItemId
			},
			StepPriceMemo: '数量＞' + SampleCount +
				'时，价格=' + values.DContractPrice_StepPrice + '元'
		};

		if(me.DealerId != null) {
			entity.ContractType = 2;
			entity.BDealer = {
				Id: me.DealerId
			};
			if(me.DealerDataTimeStamp) {
				entity.BDealer.DataTimeStamp = me.DealerDataTimeStamp.split(',');
			}
		}
		if(me.BLaboratoryId != null) {
			entity.ContractType = 1;
			entity.BLaboratory = {
				Id: me.BLaboratoryId
			};
			if(me.BLaboratoryDataTimeStamp) {
				entity.BLaboratory.DataTimeStamp = me.BLaboratoryDataTimeStamp.split(',');
			}
		}
		var BTestItem = null;
		if(me.ItemId != null) {
			BTestItem = {
				Id: me.ItemId,
			};
			if(me.ItemDataTimeStamp) {
				BTestItem.DataTimeStamp = me.ItemDataTimeStamp.split(',');
			}
		}
		var listDUnitItem = [];
		if(BTestItem != null) {
			entity.BTestItem = BTestItem;
			var DUnitItem = {
				BTestItem: BTestItem,
				IsStepPrice: IsStepPrice
			};
			listDUnitItem.push(DUnitItem);
		}
		return {
			entity: entity,
			listDUnitItem: listDUnitItem
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var IsStepPrice = values.DContractPrice_IsStepPrice;
		IsStepPrice = IsStepPrice == null ? 0 : IsStepPrice;
		var SampleCount = values.DContractPrice_SampleCount;
		SampleCount = SampleCount == null ? 0 : SampleCount;
		var entity = {
			Id: values.DContractPrice_Id,
			//EndDate: JShell.Date.toServerDate(me.getComponent("DContractPrice_EndDate").getValue()),
			SampleCount: SampleCount,
			StepPrice: values.DContractPrice_StepPrice,
			IsStepPrice: IsStepPrice,
			StepPriceMemo: '数量＞' + SampleCount + '时，价格=' + values.DContractPrice_StepPrice + '元'
		}

		var params = {
			entity: entity,
			fields: 'Id,SampleCount,StepPrice,StepPriceMemo,IsStepPrice' //EndDate,ConfirmUser,ConfirmTime'
		};
		return params;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		data.DContractPrice_BeginDate = JShell.Date.getDate(data.DContractPrice_BeginDate);
		data.DContractPrice_EndDate = JShell.Date.getDate(data.DContractPrice_EndDate);
		return data;
	}
});