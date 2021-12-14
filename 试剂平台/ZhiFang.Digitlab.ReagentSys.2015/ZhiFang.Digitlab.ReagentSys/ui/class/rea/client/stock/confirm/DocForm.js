/**
 * 客户端入库
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.stock.confirm.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		  'Shell.ux.form.field.CheckTrigger'
	],
	title: '入库信息',

	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddBmsCenSaleDocConfirm',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateBmsCenSaleDocConfirm',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 5 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},
	
	formtype:'edit',
	/**入库类型List*/
	InTypeNameList:[
	    ['1', '正常入库']
	],
	/**入库类型键值*/
	defaultInType:'1',
	/**入库类型名称*/
    defaultInTypeName:'正常入库',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.width = me.width || 405;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 160) me.defaults.width = 160;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//供应商
		items.push( {
			fieldLabel: '供应商',name: 'BmsCenSaleDocConfirm_ReaCompName',itemId: 'BmsCenSaleDocConfirm_ReaCompName',
			emptyText: '必填项',allowBlank: false,xtype: 'uxCheckTrigger',
			colspan: 2, width: me.defaults.width * 2,
			readOnly: true,locked: true,
			className: 'Shell.class.rea.client.cenorg.basic.CheckGrid',
			classConfig: {
				title: '供应商选择',checkOne:true,
				defaultWhere:'reacenorg.OrgType=0',
				width:300
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(record);
					p.close();
				}
			}
		},{
			fieldLabel: '供货方主键ID',
			hidden: true,
			name: 'BmsCenSaleDocConfirm_ReaCompID',
			itemId: 'BmsCenSaleDocConfirm_ReaCompID'
		},{
			fieldLabel: '入库类型',
			emptyText: '必填项',
			allowBlank: false,
			name: 'BmsCenSaleDocConfirm_InTypeName',
			itemId: 'BmsCenSaleDocConfirm_InTypeName',
//			xtype: 'uxCheckTrigger',
            xtype: 'uxSimpleComboBox',
			hasStyle: true,
//			value: me.defaultInTypeName,
			data: me.InTypeNameList,
			colspan: 1,
			width: me.defaults.width * 1
			
//			readOnly: true,locked: true,
//			className: 'Shell.class.rea.client.storagetype.CheckGrid',
//			listeners: {
//				check: function(p, record) {
//					me.onStorageTypeAccept(record);
//					p.close();
//				}
//			}
		},{
			fieldLabel: '入库总单号',
			name: 'BmsCenSaleDocConfirm_InDocNo',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '入库类型ID',
			hidden: true,
			value:me.defaultInType,
			name: 'BmsCenSaleDocConfirm_InType',
			itemId: 'BmsCenSaleDocConfirm_InType'
		},{
			fieldLabel: '打印次数',
			name: 'BmsCenSaleDocConfirm_PrintTimes',
			colspan: 1,hidden:true,
			width: me.defaults.width * 1
		},{
			fieldLabel: '主键ID',
			name: 'BmsCenSaleDocConfirm_Id',
			hidden: true,
			type: 'key'
		},{
			fieldLabel: '总单金额',
			name: 'BmsCenSaleDocConfirm_TotalPrice',
			colspan: 1,readOnly: true,locked: true,
			width: me.defaults.width * 1
		},{
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'BmsCenSaleDocConfirm_Memo',
			itemId: 'BmsCenSaleDocConfirm_Memo',
			colspan:5,
			width: me.defaults.width * 5,
			height: 50
		});
		return items;
	},
	isEdit:function(id){
		var me = this;
		me.setReadOnly(false);
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		me.load(id);
	    var InType = me.getComponent('BmsCenSaleDocConfirm_InType');
        InType.setValue(me.defaultInType);
		var InTypeName = me.getComponent('BmsCenSaleDocConfirm_InTypeName');
        InTypeName.setValue(me.defaultInTypeName);
	},
	isAdd:function(){
		var me = this;
		me.setReadOnly(false);
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
	},
		/**更改标题*/
	changeTitle:function(){
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			InDocNo:values.BmsCenSaleDocConfirm_InDocNo,
			Memo:values.BmsCenSaleDocConfirm_Memo
		};
		if(values.BmsCenSaleDocConfirm_TotalPrice){
			entity.TotalPrice=values.BmsCenSaleDocConfirm_TotalPrice;
		}
		if(values.BmsCenSaleDocConfirm_ReaCompID){
	    	entity.CompanyID=values.BmsCenSaleDocConfirm_ReaCompID;
	        entity.CompanyName=values.BmsCenSaleDocConfirm_ReaCompName;
	   }
	   if(values.BmsCenSaleDocConfirm_InType){
	   	   entity.InType=values.BmsCenSaleDocConfirm_InType;
	   }
	   if(values.BmsCenSaleDocConfirm_InTypeName){
	   	   entity.InTypeName=values.BmsCenSaleDocConfirm_InTypeName;
	   }
	   return entity;
	}
	
});