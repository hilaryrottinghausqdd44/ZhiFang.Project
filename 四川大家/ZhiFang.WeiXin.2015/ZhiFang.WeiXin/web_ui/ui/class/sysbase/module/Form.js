/**
 * 模块表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.module.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'模块信息',
    width:570,
	height:320,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACModuleById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACModule',
    /**修改服务地址*/
    editUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateRBACModuleByField',
    /**上传图片文件服务地址*/
	uploadFileUrl:'/ServerWCF/ConstructionService.svc/ReceiveModuleIconService',
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/** 每个组件的默认属性*/
    defaults:{
    	width:170,
        labelWidth:55,
        labelAlign:'right'
    },
    /**模块图标*/
    ModuleIconList:[
		['默认','default.PNG'],
		['文件夹','package.PNG'],
		['列表','list.PNG'],
		['检索','search.PNG'],
		['执行程序','program.PNG'],
		['设置','configuration.PNG'],
		['字典','dictionary.PNG']
	],
	/**小图标根目录*/
	ModuleIconPath16:JShell.System.Path.getModuleIconPathBySize(16),
	/**大图标根目录*/
	ModuleIconPath64:JShell.System.Path.getModuleIconPathBySize(64),
	
	/**机构ID*/
	LabId:0,
	/**拥有者*/
	Owner:'',
	
	/**上级模块ID*/
	ParentID:0,
	/**上级模块名称*/
	ParentName:'',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.formtype == 'add'){
			me.onModuleIconChange(me.ModuleIconList[0][1]);
		}
		me.on({
			load:function(p){
				var PicFile = me.getComponent('RBACModule_PicFile').getValue();
				me.onModuleIconChange(PicFile);
			}
		});
		
		var RBACModule_CName = me.getComponent('RBACModule_CName');
		RBACModule_CName.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								RBACModule_PinYinZiTou:value,
								RBACModule_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						RBACModule_PinYinZiTou:"",
						RBACModule_Shortcode:""
					});
				}
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(
			{x:10,y:10,fieldLabel:'中文名称',name:'RBACModule_CName',
				itemId:'RBACModule_CName',emptyText:'必填项',allowBlank:false},
			{x:10,y:35,fieldLabel:'英文名称',name:'RBACModule_EName'},
			{x:10,y:60,fieldLabel:'拼音字头',name:'RBACModule_PinYinZiTou'}
		);
		
		items.push(
			{x:190,y:10,fieldLabel:'模块简称',name:'RBACModule_SName'},
			{x:190,y:35,fieldLabel:'显示次序',name:'RBACModule_DispOrder',
				xtype:'numberfield',value:0,allowBlank:false},
			{x:190,y:60,fieldLabel:'模块类型',xtype:'uxSimpleComboBox',
				name:'RBACModule_ModuleType',
				itemId:'RBACModule_ModuleType',
				data:JShell.System.Enum.getList('ModuleType'),
				allowBlank:false
			}
		);
		
		items.push(
			{x:370,y:10,fieldLabel:'系统代码',width:180,labelWidth:65,name:'RBACModule_UseCode'},
			{x:370,y:35,fieldLabel:'标准代码',width:180,labelWidth:65,name:'RBACModule_StandCode'},
			{x:370,y:60,fieldLabel:'开发商代码',width:180,labelWidth:65,name:'RBACModule_DeveCode'}
		);
		
		items.push(
			{x:10,y:85,fieldLabel:'快捷码',name:'RBACModule_Shortcode'},
			{x:190,y:85,fieldLabel:'上级模块',emptyText:'必填项',allowBlank:false,
				itemId:'RBACModule_ParentName',IsnotField:true,
				xtype:'trigger',triggerCls:'x-form-search-trigger',
				enableKeyEvents:false,editable:false,value:me.ParentName,
				onTriggerClick:function(){
					JShell.Win.open('Shell.class.sysbase.module.CheckTree',{
						resizable:false,
						selectId:me.ParentID,//默认选中节点ID
						hideNodeId:me.PK,//默认隐藏节点ID
						listeners:{
							accept:function(p,record){me.onParentModuleAccept(record);p.close();}
						}
					}).show();
				}
			},{
				fieldLabel:'上级模块主键ID',hidden:true,value:me.ParentID,
				name:'RBACModule_ParentID',itemId:'RBACModule_ParentID'
			},
			
			{x:370,y:85,width:16,height:16,xtype:'image',itemId:'ModuleIcon',margin:'3px 0'},
			
			{x:390,y:85,width:80,text:'选择图标',xtype:'splitbutton',
				handler:function(btn,e){
	   				me.onCheckImgFiles();
				},
				menu:me.getModuleIconMenu()
			},
			{x:485,y:85,boxLabel:'是否使用',name:'RBACModule_IsUse',
				width:70,xtype:'checkbox',checked:true
			}
		);

		items.push(
			{x:10,y:110,fieldLabel:'入口地址',width:540,name:'RBACModule_Url'},
			{x:10,y:135,fieldLabel:'入口参数',width:540,name:'RBACModule_Para'},
			{x:10,y:160,fieldLabel:'模块描述',width:540,height:85,
				name:'RBACModule_Comment',xtype:'textarea'
			}
		);
		
		//隐藏组件
		items.push(
			{xtype:'filefield',itemId:'ModuleIconFile',IsnotField:true,hidden:true,
				listeners:{
					change:function(field,value){
						me.onModuleIconFileChange();
					}
				}
			}
		);
		
		items.push(
			{fieldLabel:'主键ID',name:'RBACModule_Id',hidden:true},
			{fieldLabel:'时间戳',name:'RBACModule_DataTimeStamp',hidden:true},
			{fieldLabel:'图片名称',name:'RBACModule_PicFile',itemId:'RBACModule_PicFile',
				hidden:true,value:me.ModuleIconList[0][1]},
			{fieldLabel:'机构ID',name:'RBACModule_LabID',itemId:'RBACModule_LabID',
				hidden:true,value:me.LabId},
			{fieldLabel:'拥有者',name:'RBACModule_Owner',itemId:'RBACModule_Owner',
				hidden:true,value:me.Owner}
		);
		
		//LabID,LevelNum,TreeCatalog,*,Owner
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.RBACModule_CName,
			EName:values.RBACModule_EName,
			PinYinZiTou:values.RBACModule_PinYinZiTou,
			
			SName:values.RBACModule_SName,
			DispOrder:values.RBACModule_DispOrder,
			ModuleType:values.RBACModule_ModuleType,
			
			UseCode:values.RBACModule_UseCode,
			StandCode:values.RBACModule_StandCode,
			DeveCode:values.RBACModule_DeveCode,
			
			Shortcode:values.RBACModule_Shortcode,
			ParentID:values.RBACModule_ParentID ? values.RBACModule_ParentID : "0",
			IsUse:values.RBACModule_IsUse ? true : false,
			
			Url:values.RBACModule_Url,
			Para:values.RBACModule_Para,
			Comment:values.RBACModule_Comment,
			
			PicFile:values.RBACModule_PicFile,
			
			LabID:values.RBACModule_LabID,
			Owner:values.RBACModule_Owner
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.RBACModule_Id;
		entity.entity.DataTimeStamp = values.RBACModule_DataTimeStamp.split(',')
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**模块图表菜单*/
	getModuleIconMenu:function(){
		var me = this;
		var list = me.ModuleIconList;
		var menu = [];
		
		var len = list.length;
		for(var i=0;i<len;i++){
			var icon = me.ModuleIconPath16 + '/' + list[i][1];
			menu.push({
				text:list[i][0],
				icon:icon,
				imgName:list[i][1],
				hideOnClick:false,
				listeners:{
					click:function(btn){
						me.onModuleIconChange(btn.imgName);
					}
				}
			});
		}
		
		return menu;
	},
	/**选择图标文件*/
	onCheckImgFiles:function(){
		var me = this,
			ModuleIconFile = me.getComponent('ModuleIconFile'),
			fileInputEl = ModuleIconFile['fileInputEl'];
		fileInputEl.dom.click();
	},
	/**更改图标*/
	onModuleIconChange:function(value){
		var me = this,
			RBACModule_PicFile = me.getComponent('RBACModule_PicFile'),
			ModuleIcon = me.getComponent('ModuleIcon');
			
		RBACModule_PicFile.setValue(value);
		var src = me.ModuleIconPath16 + '/' + value;
		ModuleIcon.setSrc(src);
	},
	/**模块图标文件更改*/
	onModuleIconFileChange:function(){
		var me = this;
		JShell.Msg.log('onModuleIconFileChange');
		
		if (!me.getForm().isValid()) return;
		
		var url = JShell.System.Path.ROOT + me.uploadFileUrl;
		
		me.getForm().submit({
			//waitMsg:'正在提交数据',
			//waitTitle:'提示',
			url:url,
			success:function(form,action){
				me.onModuleIconChange(action.result.ResultDataValue);
			},
			failure:function(form,action){
				JShell.Msg.error('上传图片文件失败!');
			}
		});
	},
	/**选择上级模块*/
	onParentModuleAccept:function(record){
		var me = this,
			ParentID = me.getComponent('RBACModule_ParentID'),
			ParentName = me.getComponent('RBACModule_ParentName');
		
		
		ParentID.setValue(record.get('tid') || '');
		ParentName.setValue(record.get('text') || '');
	}
});