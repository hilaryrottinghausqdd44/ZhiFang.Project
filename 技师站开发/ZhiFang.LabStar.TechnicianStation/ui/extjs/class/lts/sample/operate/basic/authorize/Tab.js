/**
 * 预授权选择
 * @author Jcall	
 * @version 2020-09-08
 */
Ext.define('Shell.class.lts.sample.operate.basic.authorize.Tab', {
	extend:'Ext.tab.Panel',
	
	title:"预授权选择",
	width:800,
	height:400,
	
	//自定义,用于保存到内存中。检验确认人弹出Handler,审核人弹出Checker
	OperateType:'',
	//操作类型ID
	OperateTypeID:'',
	//操作类型名称，检验确认/审核
	OperateTypeText:'',
	//当前检验小组
	SectionID:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Pre.on({
			save:function(p,data){
				me.fireEvent('save',me,data);
			}
		});
		me.Choose.on({
			accept:function(p,data){
				me.fireEvent('accept',me,data);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.activeTab = 0;//初始页签
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Pre = Ext.create('Shell.class.lts.sample.operate.basic.authorize.Pre',{
			title:'快捷预授权临时(一次性)',itemId:'Pre',closable:false,border:false,
			SectionID:me.SectionID,OperateType:me.OperateType,
			OperateTypeID:me.OperateTypeID,OperateTypeText:me.OperateTypeText
		});
		
		me.Choose = Ext.create('Shell.class.lts.sample.operate.basic.authorize.Choose',{
			title:'选择已有授权',itemId:'Choose',closable:false,border:false,
			SectionID:me.SectionID,
			OperateTypeID:me.OperateTypeID
			
		});
		
		return [me.Pre,me.Choose];
	}
});