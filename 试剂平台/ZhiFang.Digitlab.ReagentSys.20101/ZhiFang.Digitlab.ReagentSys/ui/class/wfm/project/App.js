/**
 * 项目管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.project.App',{
    extend:'Shell.ux.model.GridFormAttApp',
    
    title:'项目管理',
    GridClassName:'Shell.class.wfm.project.Grid',
    FormClassName:'Shell.class.wfm.project.Form',
    FormConfig:{width:260},
    AttachmentConfig:{
    	/**附属主体名*/
		PrimaryName:'F_Project',
		/**列表模板地址*/
		GridModelUrl:'/ui/class/wfm/project/model/Attachment.js'
    },
    GridConfig:{
    	/**其他信息模板路径*/
		OtherMsgModelUrl:'/ui/class/wfm/project/model/OtherMsg.js'
    },
    FormConfig:{
    	/**其他信息模板地址*/
		OtherMsgUrl:'/ui/class/wfm/project/model/OtherMsg.js'
    }
});