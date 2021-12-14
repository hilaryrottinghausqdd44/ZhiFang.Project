/**
 * 合同内容
 * @author Jcall
 * @version 2016-11-19
 */
Ext.define('Shell.class.wfm.business.contract.basic.ActionForm',{
    extend: 'Shell.class.wfm.business.contract.basic.ContentPanel',
    title:'合同内容',
    
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdatePContractStatus',
    
    /**通过按钮文字*/
    OverButtonName:'',
    /**不通过按钮文字*/
    BackButtonName:'',
    
    /**通过状态文字*/
	OverName:'',
	/**不通过状态文字*/
	BackName:'',
	
	/**处理意见字段*/
	OperMsgField:'',
	/**处理意见内容*/
	OperMsg:'',
	
	/**信息ID*/
	PK:null,
	
	/**需要保存的数据*/
	Entity:null,
    
    initComponent: function () {
        var me = this;
        
        me.addEvents('save');
        
		me.dockedItems = me.createDockedItems();
        
        me.callParent(arguments);
    },
    createDockedItems:function(){
    	var me = this,
    		items = ['->'];
    		
    	if(me.OverButtonName){
    		items.push({
        		iconCls:'button-save',
				text:me.OverButtonName,
				tooltip:me.OverButtonName,
				handler:function(){
					me.onOver();
				}
			});
    	}
    	if(me.BackButtonName){
    		items.push({
				iconCls:'button-save',
				text:me.BackButtonName,
				tooltip:me.BackButtonName,
				handler:function(){
					me.onBack();
				}
			});
    	}
    		
    	var dockedItems = [{
        	xtype:'toolbar',
        	dock:'bottom',
        	items:items
        }];
        
        return dockedItems;
    },
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
			//确定进行该操作
			JShell.Msg.confirm({
				msg:'确定进行该操作？'
			},function(but,text){
				if(but != "ok") return;
				me.onSaveClick(me.OverName);
			});
		}
	},
    /**未通过*/
	onBack:function(){
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
				me.onSaveClick(me.BackName);
			});
		}else{
			//确定进行该操作
			JShell.Msg.confirm({
				msg:'确定进行该操作？'
			},function(but,text){
				if(but != "ok") return;
				me.onSaveClick(me.BackName);
			});
		}
	},
	onSaveClick:function(StatusName){
		var me = this;
		
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请重新保存');
    			return;
    		}
    		var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus',StatusName);
			me.onSave(info.Id);
    	});
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
	/**获取参数*/
	getParams:function(Status){
		var me = this;
		
		var params = {
			entity:{
				Id:me.PK,
				ContractStatus:Status
			},
			fields:'Id,ContractStatus'
		};
		
		//处理意见
		if(me.OperMsgField){
			params.entity[me.OperMsgField] = me.OperMsg;
			//params.fields += ',' + me.OperMsgField;
		}
		
//		//需要保存的数据
//		if(me.Entity && Ext.typeOf(me.Entity) == 'object'){
//			for(var i in me.Entity){
//				params.entity[i] = me.Entity[i];
//				params.fields += ',' + i;
//			}
//		}
		
		return params;
	}
});