/**
 * 互动信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.oa.worklog.interaction.App',{
    extend:'Shell.class.sysbase.interaction.App',
	/**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogInteractionByHQL?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkLogInteraction',
    /**交流对象名*/
	objectName:'PWorkLogInteraction',
	/**交流关联对象名*/
	fObejctName:'',
	/**交流关联对象主键*/
	fObjectValue:'',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID:true,
	
	/*日志外键名称*/
	LogName:'',
	/**日志ID*/
	LogId:'',
	
	initComponent:function(){
		var me = this;
		
		me.fObejctName = me.LogName;
		me.fObjectValue = me.LogId;
		
		me.callParent(arguments);
	}
});
	