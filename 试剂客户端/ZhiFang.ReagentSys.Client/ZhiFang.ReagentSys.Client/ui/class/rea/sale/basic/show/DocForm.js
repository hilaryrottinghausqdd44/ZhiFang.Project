/**
 * 供货总单表单-基础查看
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.basic.show.DocForm', {
	extend: 'Shell.class.rea.sale.basic.DocForm',
	title: '供货总单表单-基础查看',
	
	formtype:'show',
	
	createItems:function(){
		var me = this,
			items = me.callParent(arguments);
		
		items.push({
			fieldLabel:'主验收人',
			name:'BmsCenSaleDoc_AccepterName',
			itemId:'BmsCenSaleDoc_AccepterName',
			readOnly:true,allowBlank:false,locked:true
		},{
			fieldLabel:'次验收人',
			name:'BmsCenSaleDoc_SecAccepterName',
			itemId:'BmsCenSaleDoc_SecAccepterName',
			readOnly:true,allowBlank:false,locked:true
		},{
			fieldLabel:'验收时间',format:'Y-m-d H:i:s',
			name:'BmsCenSaleDoc_AccepterTime',
			itemId:'BmsCenSaleDoc_AccepterTime',
			readOnly:true,allowBlank:false,locked:true
		},{
			fieldLabel:'发票号',
			name:'BmsCenSaleDoc_InvoiceNo',xtype:'textarea',height:60,
			itemId:'BmsCenSaleDoc_InvoiceNo',
			readOnly:true,allowBlank:false,locked:true
		},{
			fieldLabel:'验收备注',xtype:'textarea',height:60,
			name:'BmsCenSaleDoc_AccepterMemo',
			itemId:'BmsCenSaleDoc_AccepterMemo',
			readOnly:true,allowBlank:false,locked:true
		});
		
		return items;
	}
});