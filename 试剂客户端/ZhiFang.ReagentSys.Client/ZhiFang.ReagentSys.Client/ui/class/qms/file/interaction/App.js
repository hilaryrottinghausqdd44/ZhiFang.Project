/**
 * 互动信息
 * @author 
 * @version 2016-06-29
 */
Ext.define('Shell.class.qms.file.interaction.App',{
    extend:'Shell.class.sysbase.interaction.App',
	/**获取数据服务路径*/
	selectUrl:'/CommonService.svc/QMS_UDTO_SearchFFileInteractionByHQL?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/CommonService.svc/QMS_UDTO_AddFFileInteraction',
    /**附件对象名*/
	objectName:'FFileInteraction',
	/**附件关联对象名*/
	fObejctName:'FFile',
	/**附件关联对象主键*/
	fObjectValue:'',
    FormPosition:'s',
	/**文档ID*/
	FileId:'',
	
	initComponent:function(){
		var me = this;
		me.fObjectValue = me.FileId;
//		alert(me.fObjectValue);
		me.callParent(arguments);
	}
});
	