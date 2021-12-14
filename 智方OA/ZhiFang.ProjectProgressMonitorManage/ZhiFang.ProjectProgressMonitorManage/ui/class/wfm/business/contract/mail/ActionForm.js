/**
 * 合同邮寄
 * @author liangyl		
 * @version 2017-03-17
 */
Ext.define('Shell.class.wfm.business.contract.mail.ActionForm',{
    extend: 'Shell.class.wfm.business.contract.basic.ActionForm',
    title:'合同邮寄',
    /**通过*/
	onOver:function(){
		var me = this;
		
		if(me.OperMsgField){
			//处理意见
			JShell.Msg.confirm({
				title:'<div style="text-align:center;">处理意见</div>',
				msg:'',
				closable:false,
				multiline:true//多行输入框
			},function(but,text){
				if(but != "ok") return;
				me.OperMsg = text;
				me.onSaveClick(me.OverName);
			});
		}else{
			me.onSaveClick(me.OverName);
//			//确定进行该操作
//			JShell.Msg.confirm({
//				msg:'确定进行该操作？'
//			},function(but,text){
//				if(but != "ok") return;
//				me.onSaveClick(me.OverName);
//			});
		}
	},
    
	onSaveClick:function(StatusName){
		var me = this;
		me.openEditForm(StatusName);
		
//		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
//			if(!JShell.System.ClassDict.PContractStatus){
//  			JShell.Msg.error('未获取到合同状态，请重新保存');
//  			return;
//  		}
//  		var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus',StatusName);
//			me.onSave(info.Id);
//  	});
	},
	/**保存数据*/
	onSave:function(Status){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl),
			params = me.getParams(Status);
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.fireEvent('save',me,me.PK)
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	 /**弹出表单,填写邮寄信息*/
	openEditForm:function(StatusName){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.contract.mail.Form', {
			SUB_WIN_NO:'101',//内部窗口编号
            formtype: 'edit',
			title:'邮寄信息',
			PK:me.PK,
			ContractStatus:StatusName,
			listeners: {
				save: function(win,id) {
					JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
						if(!JShell.System.ClassDict.PContractStatus){
			    			JShell.Msg.error('未获取到合同状态，请重新保存');
			    			return;
			    		}
			    		var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus',StatusName);
						me.onSave(info.Id);
			    	});
			    	win.close();
				}
			}
		}).show();
	}
});