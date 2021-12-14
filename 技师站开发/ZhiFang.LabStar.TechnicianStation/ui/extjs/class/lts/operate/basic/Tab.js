/**
 * 预授权选择
 * @author liangyl	
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.basic.Tab', {
	extend:'Ext.tab.Panel',
	 //自定义,用于保存到内存中。检验确认人弹出Handler,审核人弹出Checker
	OperateType:'',
	//操作类型名称
	OperateTypeText:'检验确认',
	//操作类型ID
	OperateTypeID:'2',
	//当前检验小组
    SectionID:null,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.PreAuthorize.on({
			save : function(){
				me.fireEvent('save',me);
			}
		});
		me.ChooseAuthorize.on({
			accept : function(obj){
				me.fireEvent('accept',me,obj);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save','accept');
		me.activeTab = 0;//初始页签
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.PreAuthorize = Ext.create('Shell.class.lts.operate.basic.PreAuthorize',{
			closable:false,
			border:false,
			itemId:'PreAuthorize',
			SectionID:me.SectionID,
			OperateType:me.OperateType,
			//操作类型名称
			OperateTypeText:me.OperateTypeText,
			//操作类型ID
			OperateTypeID:me.OperateTypeID,
			title:'快捷预授权临时(一次性)'
		});
		
		me.ChooseAuthorize = Ext.create('Shell.class.lts.operate.basic.ChooseAuthorize',{
			closable:false,
			border:false,
			itemId:'ChooseAuthorize',
			//操作类型ID
			OperateTypeID:me.OperateTypeID,
			//当前小组
			SectionID:me.SectionID,
			title:'选择已有授权'
		});
		return [me.PreAuthorize,me.ChooseAuthorize];
	}
});