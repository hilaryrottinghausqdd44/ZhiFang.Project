/**
 * 模板信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.attachment.MsgModelForm',{
    extend:'Shell.class.sysbase.attachment.MsgForm',
    
    title:'模板信息',
    /**文件路径*/
    FilePath:null,
    /**内容*/
    Content:'',
    
    /**修改模板文件服务地址*/
    editUrl:'/WorkManageService.svc/WM_UDTO_EditFileContent',
    /**表单的默认状态*/
    formtype:'add',
	
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			MsgCom = me.getComponent('html'),
			Msg = MsgCom.getValue(),
			entity = {};
		
		entity.FilePath = me.FilePath;
		entity.FileContent = Msg;
		
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		
		var params = me.getEditParams();
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.fireEvent('save',me,params.Msg);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});