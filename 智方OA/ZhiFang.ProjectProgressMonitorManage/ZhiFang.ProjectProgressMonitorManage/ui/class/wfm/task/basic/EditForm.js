/**
 * 任务修改页面
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.basic.EditForm',{
	extend:'Shell.class.wfm.task.basic.Form',

    title:'任务修改页面',
    width:790,//670,
	height:370,
	
	/**每个组件的默认属性*/
    defaults:{
        labelWidth:85,
        width:240,//200,
        labelAlign:'right'
    },
    
    /**父任务ID*/
	ParentTaskId:null,
	/**父任务名称*/
	ParentTaskName:null,
    
    /**当前状态*/
	STATUS_ID:null,
	
	/**处理中ID*/
	IngId:'',
	/**处理中文字*/
	IngName:'',
	
	/**通过ID*/
	OverId:'',
	/**通过文字*/
	OverName:'',
	
	/**退回ID*/
	BackId:'',
	/**退回文字*/
	BackName:'',
	
	/**点击退回按钮*/
	BACK_BUTTON_CHECKED:false,
	
    /**基础修改服务地址*/
    basicEditUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskByField',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			load:function(p,data){
				//初始化受理信息
				me.initIngInfo(data);
			},
			save:function(p,id){
				//任务操作记录
				me.onSavePTaskOperLog(id,me.STATUS_ID,function(data){
					if(data.success){
						me.fireEvent('aftersave',me,me.PK,me.BACK_BUTTON_CHECKED);
					}else{
						JShell.Msg.error(data.msg);
					}
				});
			}
		});
	},
    initComponent:function(){
		var me = this;
		
		me.buttonToolbarItems = ['->'];
		//通过按钮
		if(me.OverName){
			me.buttonToolbarItems.push({
				text:me.OverName,
				tooltip:me.OverName,
				iconCls:'button-save',
				handler:function(){me.onSave(true);}
			});
		}
		//退回按钮
		if(me.BackName){
			me.buttonToolbarItems.push({
				text:me.BackName,
				tooltip:me.BackName,
				iconCls:'button-save',
				handler:function(){me.onSave(false);}
			});
		}
		me.buttonToolbarItems.push('reset');
		
		me.callParent(arguments);
	},
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = [];
			
		//信息行
		me.InfoLabel.colspan = 3;
		me.InfoLabel.style = "text-align:center;";
		me.InfoLabel.width = me.defaults.width * me.InfoLabel.colspan;
		items.push(me.InfoLabel);
		
		//任务名称
		me.PTask_CName.colspan = 2;
		me.PTask_CName.width = me.defaults.width * me.PTask_CName.colspan;
		items.push(me.PTask_CName);
		//任务类别
		me.PTask_TypeName.colspan = 1;
		items.push(me.PTask_TypeName);
		
		//任务内容
		me.PTask_Contents.colspan = 3;
		me.PTask_Contents.width = me.defaults.width * me.PTask_Contents.colspan;
		items.push(me.PTask_Contents);
		
		//--------------------------------------------
		//客户选择
		me.PTask_PClient_Name.colspan = 1;
		items.push(me.PTask_PClient_Name);
		//紧急程度
		me.PTask_Urgency_CName.colspan = 1;
		items.push(me.PTask_Urgency_CName);
		//执行方式
		me.TaskExecutType.colspan = 1;
		items.push(me.TaskExecutType);
		
		//要求完成时间
		me.PTask_ReqEndTime.colspan = 1;
		items.push(me.PTask_ReqEndTime);
		
		//执行地点     
		//隐藏执行地点 @author liangyl @version 2017-07-13
		me.PTask_ExecutAddr.colspan = 2;
		me.PTask_ExecutAddr.hidden = true;
		me.PTask_ExecutAddr.width = me.defaults.width * me.PTask_ExecutAddr.colspan;
		items.push(me.PTask_ExecutAddr);
		
		//任务分类    @author liangyl @version 2017-07-13
		me.PTask_PClassName.colspan = 2;
		items.push(me.PTask_PClassName);
		
		//任务状态
		me.PTask_Status_CName.hidden = true;
		items.push(me.PTask_Status_CName);
		
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Id:values.PTask_Id,//主键ID
			CName:values.PTask_CName,//标题
			Contents:values.PTask_Contents.replace(/\\/g,'&#92'),//内容
			ExecutAddr:values.PTask_ExecutAddr,//执行地点
			ReqEndTime:JShell.Date.toServerDate(values.PTask_ReqEndTime)//要求完成时间
		};
		
		//任务类别
		if(values.PTask_TypeID){
			entity.TypeID = values.PTask_TypeID;
			entity.TypeName = values.PTask_TypeName;
		}
		//任务大类
		if(values.PTask_PTypeID){
			entity.PTypeID = values.PTask_PTypeID;
			entity.PTypeName = values.PTask_PTypeName;
		}
		//任务状态
		if(values.PTask_Status_Id){
			entity.Status = {
				Id:values.PTask_Status_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.StatusName = values.PTask_Status_CName;
		}
		//执行方式
		if(values.PTask_ExecutType_Id){
			entity.ExecutType = {
				Id:values.PTask_ExecutType_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.ExecutTypeName = values.PTask_ExecutType_CName;
		}
		//紧急程度
		if(values.PTask_Urgency_Id){
			entity.Urgency = {
				Id:values.PTask_Urgency_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.UrgencyName = values.PTask_Urgency_CName;
		}
		//客户选择
		if(values.PTask_PClient_Id){
			entity.PClient= {
				Id:values.PTask_PClient_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		//任务分类    @author liangyl @version 2017-07-13
		if(values.PTask_PClassID){
			entity.PClassID = values.PTask_PClassID;
			entity.PClassName = values.PTask_PClassName;
		}
		var fields = [
			'CName','Contents','ExecutAddr','ReqEndTime',
			
			'PTypeID','TypeID','ExecutType_Id','Urgency_Id',
			'PTypeName','TypeName','ExecutTypeName','UrgencyName',
			
			'Status_Id','StatusName',
			'Id','PClient_Id',
			'PClassID','PClassName'
			
		];
		
		return {
			entity:entity,
			fields:fields.join(',')
		};
	},
	/**保存按钮点击处理方法*/
	onSave:function(isOver){
		var me = this,
			values = me.getForm().getValues();
			
		if(!me.getForm().isValid()) return;
		
		if(isOver){//通过
			if(!me.isOverValid()) return;//校验
			me.getForm().setValues({
				PTask_Status_CName:me.OverName,
				PTask_Status_Id:me.OverId
			});
			me.STATUS_ID = me.OverId;
			me.BACK_BUTTON_CHECKED = false;
		}else{//退回
			if(!me.isBackValid()) return;//校验
			me.getForm().setValues({
				PTask_Status_CName:me.BackName,
				PTask_Status_Id:me.BackId
			});
			me.STATUS_ID = me.BackId;
			me.BACK_BUTTON_CHECKED = true;
		}
		
		//处理意见
		JShell.Msg.confirm({
			title:'<div style="text-align:center;">处理意见</div>',
			msg:'',
			closable:false,
			multiline:true//多行输入框
		},function(but,text){
			if(but != "ok") return;
			me.OperMsg = text;
			me.onSaveClick();
		});
	},
	/**受理*/
	onAccept:function(callback){
		var me = this,
			values = me.getForm().getValues(),
			url = JShell.System.Path.getRootUrl(me.editUrl);
		
		var params = {
			entity:{
				Id:values.PTask_Id,
				Status:{
					Id:me.IngId,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				},
				StatusName:me.IngName
			},
			fields:'Id,Status_Id,StatusName'
		};
		var id = params.entity.Id;
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				if(Ext.typeOf(callback) == 'function'){
					callback();
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**初始化受理信息*/
	initIngInfo:function(data){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
			
		if(data.success){
			var StatusId = data.value.PTask_Status_Id;
			
			//是否显示处理按钮
			var showIngButton = me.showIngButton(StatusId);
			
			if(showIngButton === null) return;
			
			if(showIngButton){
				//创建受理中按钮
				me.createIngButton();
			}else{
				//创建受理中文字
				me.createIngLabel();
			}
		}
	},
	/**创建受理中按钮*/
	createIngButton:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		
		buttonsToolbar.insert(1,{
			text:'受理',
			iconCls:'button-save',
			tooltip:'受理',
			handler:function(){
				var button = this;
				me.getForm().setValues({
					PTask_Status_CName:me.IngName,
					PTask_Status_Id:me.IngId
				});
				
				me.STATUS_ID = me.IngId;
				me.onAccept(function(){
					//任务操作记录
					me.onSavePTaskOperLog(me.PK,me.STATUS_ID,function(data){
						if(data.success){
							button.hide();
							//创建受理中文字
							me.createIngLabel();
						}else{
							JShell.Msg.error(data.msg);
						}
					});
				});
			}
		});
	},
	/**创建受理中文字*/
	createIngLabel:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
			
		buttonsToolbar.insert(1,{
			xtype:'label',
			text:'受理中',
			style:'font-weight:bold;color:blue;margin-right:20px;'
		});
		buttonsToolbar.insert(2,{
			text:'临时存储',
			iconCls:'button-save',
			tooltip:'临时存储',
			handler:function(){
				//保存临时存储的内容
				me.onUpdateInfo();
			}
		});
		buttonsToolbar.insert(3,{
			xtype:'tbseparator'
		});
	},
	/**保存临时存储的内容*/
	onUpdateInfo:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = JShell.System.Path.getRootUrl(me.basicEditUrl);
		
		var params = Ext.JSON.encode(me.getEditParams());
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 是否显示处理按钮*/
	showIngButton:function(StatusId){
		JShell.Msg.overwrite('showIngButton');
		return null;
	},
	/**@overwrite 通过按钮校验*/
	isOverValid:function(){
		return true;
	},
	/**@overwrite 退回按钮校验*/
	isBackValid:function(){
		return true;
	}
});