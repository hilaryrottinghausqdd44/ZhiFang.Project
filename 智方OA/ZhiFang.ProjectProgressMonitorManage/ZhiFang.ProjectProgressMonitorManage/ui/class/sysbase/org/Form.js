/**
 * 机构表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.org.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
    	'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'机构信息',
    width:570,
	height:320,
    
    /**获取数据服务路径*/
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchHRDeptById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/RBACService.svc/RBAC_UDTO_AddHRDept',
    /**修改服务地址*/
    editUrl:'/RBACService.svc/RBAC_UDTO_UpdateHRDeptByField', 
	
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
	
	/**上级机构ID*/
	ParentID:0,
	/**上级机构名称*/
	ParentName:'',
	
	/**是否存在上级*/
	hasManager:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var HRDept_CName = me.getComponent('HRDept_CName');
		HRDept_CName.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								HRDept_PinYinZiTou:value,
								HRDept_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						HRDept_PinYinZiTou:"",
						HRDept_Shortcode:""
					});
				}
			}
		});
		//初始化检索监听
		me.initFilterListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(
			{x:10,y:10,fieldLabel:'中文名称',name:'HRDept_CName',
				itemId:'HRDept_CName',emptyText:'必填项',allowBlank:false},
			{x:10,y:35,fieldLabel:'英文名称',name:'HRDept_EName'},
			{x:10,y:60,fieldLabel:'拼音字头',name:'HRDept_PinYinZiTou'}
		);
		
		items.push(
			{x:190,y:10,fieldLabel:'机构简称',name:'HRDept_SName'},
			{x:190,y:35,fieldLabel:'显示次序',name:'HRDept_DispOrder',
				xtype:'numberfield',value:0,allowBlank:false},
			{x:190,y:60,fieldLabel:'上级机构',emptyText:'必填项',allowBlank:false,
				name:'HRDept_ParentName',itemId:'HRDept_ParentName',
				xtype:'uxCheckTrigger',className:'Shell.class.sysbase.org.CheckTree',
				IsnotField:true,value:me.ParentName,
				classConfig:{
					selectId:me.ParentID,//默认选中节点ID
					hideNodeId:me.PK//默认隐藏节点ID
				}
			},{
				fieldLabel:'上级机构主键ID',hidden:true,value:me.ParentID,
				name:'HRDept_ParentID',itemId:'HRDept_ParentID'
			}
		);
		
		items.push(
			{x:370,y:10,fieldLabel:'系统代码',width:180,labelWidth:65,name:'HRDept_UseCode',emptyText:'必填项',allowBlank:false},
			{x:370,y:35,fieldLabel:'标准代码',width:180,labelWidth:65,name:'HRDept_StandCode'},
			{x:370,y:60,fieldLabel:'开发商代码',width:180,labelWidth:65,name:'HRDept_DeveCode'}
		);
		
		items.push(
			{x:10,y:85,fieldLabel:'快捷码',name:'HRDept_Shortcode'},
			{x:190,y:85,fieldLabel:'联系人',emptyText:'组织机构联系人',
				name:'HRDept_Contact'
			},
			{x:370,y:85,fieldLabel:'部门电话',width:180,labelWidth:65,name:'HRDept_Tel'}
		);
		
		items.push(
			{x:10,y:110,fieldLabel:'邮编',name:'HRDept_ZipCode'},
			{x:190,y:110,fieldLabel:'传真',name:'HRDept_Fax'}
		);
		
		if(me.hasManager){
			items.push({
				x:370,y:110,fieldLabel:'管理人员',width:180,labelWidth:65,
				emptyText:'必填项',allowBlank:false,
				name:'HRDept_ManagerName',itemId:'HRDept_ManagerName',
				xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp'
			},{
				fieldLabel:'管理人员主键ID',hidden:true,
				name:'HRDept_ManagerID',itemId:'HRDept_ManagerID'
			});
		}
		
		items.push(
			{x:10,y:135,fieldLabel:'机构地址',width:465,name:'HRDept_Address'},
			{x:485,y:135,boxLabel:'是否使用',name:'HRDept_IsUse',
				width:70,xtype:'checkbox',checked:true
			},
			{x:10,y:160,fieldLabel:'模块描述',width:540,height:85,
				name:'HRDept_Comment',xtype:'textarea'
			}
		);
		
		items.push(
			{fieldLabel:'主键ID',name:'HRDept_Id',hidden:true}
		);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.HRDept_CName,
			EName:values.HRDept_EName,
			PinYinZiTou:values.HRDept_PinYinZiTou,
			
			SName:values.HRDept_SName,
			DispOrder:values.HRDept_DispOrder,
			
			UseCode:values.HRDept_UseCode,
			StandCode:values.HRDept_StandCode,
			DeveCode:values.HRDept_DeveCode,
			
			Shortcode:values.HRDept_Shortcode,
			ParentID:values.HRDept_ParentID,
			IsUse:values.HRDept_IsUse ? true : false,
			
			Tel:values.HRDept_Tel,
			Fax:values.HRDept_Fax,
			ZipCode:values.HRDept_ZipCode,
			
			Address:values.HRDept_Address,
			Comment:values.HRDept_Comment,
			Contact:values.HRDept_Contact
		};
		
		if(me.hasManager){
			entity.ManagerID = values.HRDept_ManagerID;
			entity.ManagerName = values.HRDept_ManagerName;
		}
		
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
		
		entity.entity.Id = values.HRDept_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			ParentID = me.getComponent('HRDept_ParentID'),
			ParentName = me.getComponent('HRDept_ParentName');
		
		//上级部门监听
		if(ParentName){
			ParentName.on({
				check: function(p, record) {
					ParentName.setValue(record ? record.get('text') : '');
					ParentID.setValue(record ? record.get('tid') : '');
					p.close();
				}
			});
		}
		
		if(me.hasManager){
			var ManagerID = me.getComponent('HRDept_ManagerID'),
				ManagerName = me.getComponent('HRDept_ManagerName');
			//管理人员监听
			if(ManagerName){
				ManagerName.on({
					check: function(p, record) {
						ManagerName.setValue(record ? record.get('HREmployee_CName') : '');
						ManagerID.setValue(record ? record.get('HREmployee_Id') : '');
						p.close();
					}
				});
			}
		}
	}
});