/**
 * 员工信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'员工信息',
    
    /**获取数据服务路径*/
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchHREmployeeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/RBACService.svc/RBAC_UDTO_AddHREmployee',
    /**修改服务地址*/
    editUrl:'/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField', 
    
    bodyPadding:10,
    /**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**启用表单状态初始化*/
	openFormType:true,
	/**显示成功信息*/
	showSuccessInfo:false,
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		//拼音字头监听
		me.initPinYinZiTouListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		//隶属部门
		items.push({
			fieldLabel:'隶属部门',
			emptyText:'必填项',allowBlank:false,
			name:'HREmployee_HRDept_CName',
			itemId:'HREmployee_HRDept_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.org.CheckTree'
		},{
			fieldLabel:'隶属部门主键ID',hidden:true,
			name:'HREmployee_HRDept_Id',
			itemId:'HREmployee_HRDept_Id'
		},{
			fieldLabel:'隶属部门时间戳',hidden:true,
			name:'HREmployee_HRDept_DataTimeStamp',
			itemId:'HREmployee_HRDept_DataTimeStamp'
		});
				
		//姓名
		items.push({
			fieldLabel:'姓',name:'HREmployee_NameL',itemId:'HREmployee_NameL',
			emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'名',name:'HREmployee_NameF',itemId:'HREmployee_NameF',
			emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'名称',name:'HREmployee_CName',readOnly:true,locked:true,
			itemId:'HREmployee_CName',hidden:true
		});
		
		//拼音字头、快捷码
		items.push({
			fieldLabel:'拼音字头',name:'HREmployee_PinYinZiTou',itemId:'HREmployee_PinYinZiTou'
			//emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'快捷码',name:'HREmployee_Shortcode',itemId:'HREmployee_Shortcode'
			//emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'员工代码',name:'HREmployee_UseCode',itemId:'HREmployee_UseCode'
			//emptyText:'必填项',allowBlank:false
		});
		
		//项目类别
		items.push({
			fieldLabel:'性别',
			//emptyText:'必填项',allowBlank:false,
			name:'HREmployee_BSex_CName',
			itemId:'HREmployee_BSex_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.sex.CheckGrid'
		},{
			fieldLabel:'性别主键ID',hidden:true,
			name:'HREmployee_BSex_Id',
			itemId:'HREmployee_BSex_Id'
		},{
			fieldLabel:'性别时间戳',hidden:true,
			name:'HREmployee_BSex_DataTimeStamp',
			itemId:'HREmployee_BSex_DataTimeStamp'
		});
		//出生日期
		items.push({
			fieldLabel:'出生日期',name:'HREmployee_Birthday',
			xtype:'datefield',format:'Y-m-d'
		});
		
		items.push({
			boxLabel:'是否在职',name:'HREmployee_IsEnabled',
			xtype:'checkbox',checked:true
		},{
			boxLabel:'是否使用',name:'HREmployee_IsUse',
			xtype:'checkbox',checked:true
		},{
			fieldLabel:'主键ID',name:'HREmployee_Id',hidden:true
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			HRDept:{
				Id:values.HREmployee_HRDept_Id,
				DataTimeStamp:values.HREmployee_HRDept_DataTimeStamp.split(',')
			},
			NameL:values.HREmployee_NameL,
			NameF:values.HREmployee_NameF,
			CName:values.HREmployee_CName,
			
			PinYinZiTou:values.HREmployee_PinYinZiTou,
			Shortcode:values.HREmployee_Shortcode,
			UseCode:values.HREmployee_UseCode,
			
			Birthday:JShell.Date.toServerDate(values.HREmployee_Birthday),
			IsEnabled:values.HREmployee_IsEnabled ? 1 : 0,
			
			IsUse:values.HREmployee_IsUse ? true : false
		};
		
		if(values.HREmployee_BSex_Id){
			entity.BSex = {
				Id:values.HREmployee_BSex_Id,
				DataTimeStamp:values.HREmployee_BSex_DataTimeStamp.split(',')
			};
		}
		
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
		
		var fields = [
			'HRDept_Id','NameL','NameF','CName',
			'PinYinZiTou','Shortcode','UseCode',
			'Birthday','IsEnabled','IsUse','BSex_Id','Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.HREmployee_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.HREmployee_Birthday = JShell.Date.getDate(data.HREmployee_Birthday);
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//部门监听
		var dictList = ['HRDept'];
		
		for(var i=0;i<dictList.length;i++){
			me.doHRDeptListeners(dictList[i]);
		}
	},
	/**员工监听*/
	doHRDeptListeners:function(name){
		var me = this;
		var CName = me.getComponent('HREmployee_' + name + '_CName');
		var Id = me.getComponent('HREmployee_' + name + '_Id');
		var DataTimeStamp = me.getComponent('HREmployee_' + name + '_DataTimeStamp');
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('text') : '');
				Id.setValue(record ? record.get('tid') : '');
				DataTimeStamp.setValue(record ? record.get('value').DataTimeStamp : '');
				p.close();
			}
		});
	},
	/**拼音字头监听*/
	initPinYinZiTouListeners:function(){
		var me = this,
			HREmployee_NameL = me.getComponent('HREmployee_NameL'),
			HREmployee_NameF = me.getComponent('HREmployee_NameF');
			
		HREmployee_NameL.on({
			change:function(field,newValue,oldValue,eOpts){
				me.changePinYinZiTou(newValue);
			}
		});
		HREmployee_NameF.on({
			change:function(field,newValue,oldValue,eOpts){
				me.changePinYinZiTou(newValue);
			}
		});
	},
	changePinYinZiTou:function(data){
		var me = this,
			HREmployee_NameL = me.getComponent('HREmployee_NameL'),
			HREmployee_NameF = me.getComponent('HREmployee_NameF'),
			HREmployee_CName = me.getComponent('HREmployee_CName'),
			HREmployee_PinYinZiTou = me.getComponent('HREmployee_PinYinZiTou'),
			HREmployee_Shortcode = me.getComponent('HREmployee_Shortcode');
			
		var name = HREmployee_NameL.getValue() + HREmployee_NameF.getValue();
		
		HREmployee_CName.setValue(name);
			
		if(name != ""){
			JShell.Action.delay(function(){
				JShell.System.getPinYinZiTou(name,function(value){
					me.getForm().setValues({
						HREmployee_PinYinZiTou:value,
						HREmployee_Shortcode:value
					});
				});
			},null,200);
		}else{
			me.getForm().setValues({
				HREmployee_PinYinZiTou:"",
				HREmployee_Shortcode:""
			});
		}
	}
});