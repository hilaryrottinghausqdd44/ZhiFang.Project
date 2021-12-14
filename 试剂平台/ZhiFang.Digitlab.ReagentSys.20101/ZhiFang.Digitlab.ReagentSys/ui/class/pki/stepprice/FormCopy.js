/**
 * 经销商项目阶梯价格表单-复制
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.stepprice.FormCopy',{
    extend:'Shell.ux.form.Panel',
    
    title:'经销商阶梯价格-复制',
    width:430,
    height:140,
    
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddDStepPrice',
    
    /**合同编号*/
    ContractNo:'',
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({x:0,y:10,fieldLabel:'开始日期',name:'DStepPrice_BeginDate',
			xtype:'datefield',format:'Y-m-d',allowBlank:false});
		items.push({x:210,y:10,fieldLabel:'截止日期',name:'DStepPrice_EndDate',
			xtype:'datefield',format:'Y-m-d',allowBlank:false});
		
		items.push({x:0,y:40,width:410,fieldLabel:'合同编号',
			name:'DStepPrice_ContractNo',value:me.ContractNo});
		
		return items;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var values = me.getForm().getValues();
		
		me.fireEvent('save',me,{
			BeginDate:JShell.Date.toServerDate(values.DStepPrice_BeginDate),
			EndDate:JShell.Date.toServerDate(values.DStepPrice_EndDate),
			ContractNo:values.DStepPrice_ContractNo
		});
	}
});