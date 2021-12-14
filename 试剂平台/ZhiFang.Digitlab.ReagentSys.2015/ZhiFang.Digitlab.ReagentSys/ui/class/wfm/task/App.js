/**
 * 任务管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.App',{
    extend:'Shell.ux.model.GridFormAttApp',
    
    title:'任务管理',
    GridClassName:'Shell.class.wfm.task.Grid',
    FormClassName:'Shell.class.wfm.task.Form',
    FormConfig:{width:260},
    AttachmentConfig:{
    	/**附属主体名*/
		PrimaryName:'F_Task',
		/**列表模板地址*/
		GridModelUrl:'/ui/class/wfm/task/model/Attachment.js'
    },
    GridConfig:{
    	/**其他信息模板路径*/
		OtherMsgModelUrl:'/ui/class/wfm/task/model/OtherMsg.js'
    },
    FormConfig:{
    	/**其他信息模板地址*/
		OtherMsgUrl:'/ui/class/wfm/task/model/OtherMsg.js'
    },
    /**项目表主体名*/
    PrimaryName:null,
    /**项目表数据ID*/
	PrimaryID:null,
	/**项目名称*/
	ProjectName:null,
	
	initComponent:function(){
		var me = this;
		
		if(!me.PrimaryName || !me.PrimaryID || !me.ProjectName){
			me.items = [{
				region: 'center',
				header: false,
				html:'<div style="margin:10px;text-align:center;color:red;"><b>请配置PrimaryName、PrimaryID、ProjectName参数</b></div>'
			}];
		}else{
			me.GridConfig = me.GridConfig || {};
			me.GridConfig.defaultWhere = "ftask.PrimaryName='" + me.PrimaryName + 
				"' and ftask.PrimaryID='" + me.PrimaryID + "'";
				
			me.FormConfig = me.FormConfig || {};
			me.FormConfig.PrimaryName = me.PrimaryName;
			me.FormConfig.PrimaryID = me.PrimaryID;
			me.FormConfig.ProjectName = me.ProjectName;
		}
		
		me.callParent(arguments);
	},
});