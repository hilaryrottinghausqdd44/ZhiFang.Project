/**
 * 平台客户-其他信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.serviceclient.create.OtherInfoForm',{
    extend:'Shell.ux.form.Panel',
    
    title:'平台客户-其他信息',
	
	bodyPadding:'20px 20px 10px 20px',
	
    layout:{
        type:'table',
        columns:3//每行有几列
    },
    /**每个组件的默认属性*/
    defaults:{
        labelWidth:60,
        width:200,
        labelAlign:'right'
    },
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [{
			
		}];
		
		return items;
	}
});