/**
 * 实验室订货单审核-总单表单
 * @author Jcall
 * @version 2017-12-20
 */
Ext.define('Shell.class.rea.order.lab.audit.DocForm', {
	extend: 'Shell.class.rea.order.basic.DocForm',
	title: '实验室订货单审核-总单表单',
	
	createItems:function(){
		var me = this,
			items = me.callParent(arguments),
			len = items.length;
			
		for(var i=0;i<len;i++){
			if(items[i].name == 'BmsCenOrderDoc_Memo'){
				delete items[i];
			}else if(items[i].name == 'BmsCenOrderDoc_CompMemo'){
				items[i].locked = true;
				items[i].readOnly = true;
			}
		}
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			params = me.callParent(arguments);
			
		delete params.entity.CompMemo;
		delete params.entity.Memo;
		
		return params;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
			
		var fields = [
			'Id','OrderDocNo','UserID','UserName','CompanyName','LabMemo',
			'OperDate','Status','UrgentFlag','Lab_Id','Comp_Id'
		];
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.BmsCenOrderDoc_Id;
		return entity;
	}
});