/**
 * 新增医生微信账号
 * @author Jcall
 * @version 2017-02-14
 */
Ext.define('Shell.class.weixin.doctor.weixin.AddAccount',{
    extend:'Shell.ux.form.Panel',
    
    title:'新增医生微信账号',
    width:220,
	height:400,
	bodyPadding:10,
    
    /**根据手机号获取列表数据服务路径*/
	selectListUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?isPlanish=true',
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_AddBWeiXinAccount',
    /**修改服务地址*/
    editUrl:'/ServerWCF/WeiXinAppService.svc/WXADS_DoctorAccountBindWeiXinAccountChange',
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:35,
        labelAlign:'right'
    },
    /**默认密码*/
    defaultPassword:'123456',
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			load:function(p,data){
				me.onShowWeixinInfo(data);
			}
		});
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [{
			padding:'5px 10px',xtype:'label',itemId:'Info',hidden:true,
			style:{textAlign:'center'}
		},{
			padding:'5px 10px',xtype:'image',itemId:'Img',hidden:true
		},{
			padding:'5px 10px',xtype:'label',itemId:'UserName',hidden:true,
			style:{textAlign:'center'}
		},{
			padding:'5px 10px',xtype:'label',itemId:'Name',hidden:true
		},{
			padding:'5px 10px',xtype:'label',itemId:'MobileCode',hidden:true
		},{
			padding:'5px 10px',xtype:'label',itemId:'PassWord',hidden:true
		},{
			fieldLabel:'手机',name:'BWeiXinAccount_MobileCode',
			itemId:'BWeiXinAccount_MobileCode',
			emptyText:'必填项',allowBlank:false,maxLength:11,minLength:11,
			hidden:me.PK ? true : false
		},{
			fieldLabel:'密码',name:'BWeiXinAccount_PassWord',value:me.defaultPassword,
			itemId:'BWeiXinAccount_PassWord',
			emptyText:'必填项',allowBlank:false,maxLength:20,
			hidden:me.PK ? true : false
		},{
			name:'BWeiXinAccount_UserName',hidden:true
		},{
			name:'BWeiXinAccount_Name',hidden:true
		},{
			name:'BWeiXinAccount_HeadImgUrl',hidden:true
		},{
			name:'BWeiXinAccount_WeiXinAccount',hidden:true
		},{
			padding:'5px 10px',xtype:'label',text:'提示（默认密码' + me.defaultPassword + '）'
		}];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			WeiXinAccount:values.BWeiXinAccount_MobileCode,
			UserName:values.BWeiXinAccount_MobileCode,
			MobileCode:values.BWeiXinAccount_MobileCode,
			PassWord:values.BWeiXinAccount_PassWord
		};
		
		return {entity:entity};
	},
	
	onShowWeixinInfo:function(data){
		var me = this,
			values = me.getForm().getValues(),
			Info = me.getComponent('Info'),
			Img = me.getComponent('Img'),
			UserName = me.getComponent('UserName'),
			Name = me.getComponent('Name'),
			MobileCode = me.getComponent('MobileCode'),
			BWeiXinAccount_MobileCode = me.getComponent('BWeiXinAccount_MobileCode'),
			BWeiXinAccount_PassWord = me.getComponent('BWeiXinAccount_PassWord');
			
		if(data.success){
			Img.setSrc(data.value.BWeiXinAccount_HeadImgUrl);
			UserName.setText(data.value.BWeiXinAccount_UserName);
			Name.setText('姓名：' + data.value.BWeiXinAccount_Name);
			MobileCode.setText('手机：' + data.value.BWeiXinAccount_MobileCode);
		}else{
			UserName.setText(data.msg);
		}
		
		if(values.BWeiXinAccount_WeiXinAccount == values.BWeiXinAccount_MobileCode){
			me.formtype = 'edit';//可以修改
			Info.setText('<b style="color:red;">用户待绑定</b>',false);
			BWeiXinAccount_PassWord.setValue(me.defaultPassword);
			
			Info.show();
			BWeiXinAccount_MobileCode.show();
			BWeiXinAccount_PassWord.show();
			me.showButtonsToolbar(true);//显示功能按钮栏
			me.setReadOnly(false);
			me.changeTitle();//标题更改
		}else{
			Info.setText('<b style="color:green;">用户已绑定</b>',false);
			Info.show();
			Img.show();
			UserName.show();
			Name.show();
			MobileCode.show();
		}
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this,
			values = me.getForm().getValues();
		
		if(!me.getForm().isValid()) return;
		
		var url = JShell.System.Path.ROOT + me.selectListUrl + 
			"&fields=BWeiXinAccount_Id&where=bweixinaccount.MobileCode='" +
			values.BWeiXinAccount_MobileCode + "'";
			
		if(me.PK){
			url += " and bweixinaccount.Id<>" + me.PK;
		}
		
		me.showMask(me.loadingText);//显示遮罩层
		JShell.Server.get(url,function(data){
    		me.hideMask();//隐藏遮罩层
    		if(data.success){
    			if(data.value && data.value.list && data.value.list.length > 0){
    				JShell.Msg.error('该手机号已经被绑定，请换一个手机号！');
                }else{
                	if(me.PK){
                		me.onEditInfo();
                	}else{
                		me.onAddInfo();
                	}
                }
    		}else{
    			JShell.Msg.error(data.msg);
    		}
    	});
	},
	/**新增保存处理*/
	onAddInfo:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = JShell.System.Path.ROOT + me.addUrl,
			params = me.getAddParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		me.fireEvent('beforesave',me);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object'){
					id = id.id;
				}
				
				if(me.isReturnData){
					me.fireEvent('save',me,me.returnData(id));
				}else{
					me.fireEvent('save',me,id);
				}
				
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**修改保存处理*/
	onEditInfo:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!me.getForm().isValid()) return;
		
		var url = JShell.System.Path.ROOT + me.editUrl + '?id=' + me.PK + 
			'&AccountCode=' + values.BWeiXinAccount_MobileCode + '&password=' + values.BWeiXinAccount_PassWord;
		
		me.showMask(me.saveText);//显示遮罩层
		
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				//if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				me.close();
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
	}
});