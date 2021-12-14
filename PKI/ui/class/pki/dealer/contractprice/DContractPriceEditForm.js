/**
 * 合同与价格编辑(可以修改经销商或送检单位的合同截止日期及合同编号)表单
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dealer.contractprice.DContractPriceEditForm', {
	extend: 'Shell.ux.form.Panel',
	title: '合同信息',
	date_error: '',
	width: 230,
	height: 210,
	formtype: 'edit',
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceById?isPlanish=true',
	/**新增服务地址*/

	editUrl: '/StatService.svc/Stat_UDTO_UpdateDContractPriceByField',
//	editUrl: '/StatService.svc/Stat_UDTO_UpdateDContractDateByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	hiddenIsStepPrice:true,
	ContractNoIsDisabled: true,
	/**开始日期*/
	BeginDate: '',
	/**结束日期*/
	EndDate: '',
	/**合同编号*/
	ContractNo: '',
	/**合同编号是否禁用*/
	IsContractNoEnable:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
			//width: 210,
			fieldLabel: '合同编号',
			itemId: 'DContractPrice_ContractNo',
			locked: true,
			value: me.ContractNo,
			hasReadOnly: true,
			disabled: me.IsContractNoEnable,
			readonly: me.ContractNoIsDisabled,
			name: 'DContractPrice_ContractNo'
		});
		items.push({
			x: 0,
			y: 40,
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
			y: 70,
			fieldLabel: '截止日期',
			name: 'DContractPrice_EndDate',
			itemId: 'DContractPrice_EndDate',
			xtype: 'datefield',
			//disabled: true,
			value: me.EndDate,
			format: 'Y-m-d',
			allowBlank: false
		});
	
		//		items.push({
		//			x: 0,
		//			y: 100,
		//			fieldLabel: '数量要求',
		//			itemId: 'DContractPrice_SampleCount',
		//			name: 'DContractPrice_SampleCount',
		//			xtype: 'numberfield',
		//			allowBlank: false
		//		});
		//		items.push({
		//			x: 0,
		//			y: 130,
		//			fieldLabel: '价格',
		//			itemId: 'DContractPrice_StepPrice',
		//			name: 'DContractPrice_StepPrice',
		//			xtype: 'numberfield',
		//			allowBlank: false
		//		});
		items.push({
			x: 0,
			y: 100,
			fieldLabel: '是否阶梯价',
			itemId: 'DContractPrice_IsStepPrice',
			name: 'DContractPrice_IsStepPrice',
			xtype: 'checkbox',
			labelWidth: 80,
			hidden:me.hiddenIsStepPrice,
			inputValue: 1
		});
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var IsStepPrice = values.DContractPrice_IsStepPrice;
		IsStepPrice = IsStepPrice == null ? 0 : IsStepPrice;
		var entity = {
			Id: values.DContractPrice_Id,
			EndDate: JShell.Date.toServerDate(values.DContractPrice_EndDate)
		}
		if(values.DContractPrice_ContractNo){
			entity.ContractNo=values.DContractPrice_ContractNo;
		}
		var fields='Id,EndDate';
		//合同编号不禁用时,修改合同编号 * @author liangyl @version 2017-08-07 */
		if(!me.IsContractNoEnable){
			fields=fields+',ContractNo';
		}
		
		var params = {
			entity: entity,
			fields: fields 
		};
		if(me.hiddenIsStepPrice==false){
			entity.IsStepPrice=IsStepPrice;
			params.fields='Id,EndDate,ContractNo,IsStepPrice';
		}
		return params;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		data.DContractPrice_BeginDate = JShell.Date.getDate(data.DContractPrice_BeginDate);
		data.DContractPrice_EndDate = JShell.Date.getDate(data.DContractPrice_EndDate);
		return data;
	}
});