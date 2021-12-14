/**
 * 项目分类信息
 * @author Jcall
 * @version 2016-12-29
 */
Ext.define('Shell.class.weixin.item.type.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'项目分类信息',
    width:240,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddOSItemProductClassTree',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTreeByField',
    
    /**获取用户账号列表服务*/
    getUserListUrl:'/ServerWCF/DictionaryService.svc/RBAC_UDTO_SearchRBACUserListByHQL?&isPlanish=true',
    
    bodyPadding:10,
    /**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:55,
        labelAlign:'right'
    },
	/**启用表单状态初始化*/
	openFormType:true,
	/**显示成功信息*/
	showSuccessInfo:true,
	/**保存后返回表单内容数据,*/
	isReturnData:true,
    
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
    /**父检验项目分类ID*/
	ParentID:null,
	/**父检验项目分类名称*/
	ParentName:'',
	editParentName:'',
	 /**区域ID*/
	AreaID:null,
	/**区域名称*/
	AreaName:'',
    AreaEnum:null,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initFromListeners();
	},
	initComponent:function(){
		var me = this;
		
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		var items = [
		    {fieldLabel:'上级分类',
				name:'OSItemProductClassTree_PItemProductClassTreeName',itemId:'OSItemProductClassTree_PItemProductClassTreeName',
				xtype:'uxCheckTrigger',className:'Shell.class.weixin.item.type.CheckApp',	
				IsnotField:true,emptyText:'父检验项目分类',//value:me.ParentName,
				classConfig:{
					selectId:me.ParentID,//默认选中节点ID
					hideNodeId:me.PK//默认隐藏节点ID
				}
			},
			{fieldLabel:'父检验项目分类ID',hidden:true,
				name:'OSItemProductClassTree_PItemProductClassTreeID',itemId:'OSItemProductClassTree_PItemProductClassTreeID'
		    },{
				fieldLabel: '区域',xtype: 'uxCheckTrigger',emptyText: '必填项',
				allowBlank: false,name: 'OSItemProductClassTree_Area',itemId: 'OSItemProductClassTree_Area',
				colspan: 1,className: 'Shell.class.weixin.hospital.area.CheckGrid'
			},
			{fieldLabel: '区域主键ID',hidden: true,name: 'OSItemProductClassTree_AreaID',itemId: 'OSItemProductClassTree_AreaID'},
			{fieldLabel:'名称',name:'OSItemProductClassTree_CName',emptyText:'必填项',allowBlank:false},
			{fieldLabel:'简称',name:'OSItemProductClassTree_SName'},
			{fieldLabel:'快捷码',name:'OSItemProductClassTree_Shortcode'},
			{fieldLabel:'备注',height:85,labelAlign:'top', //emptyText:'备注',//emptyText:'备注',//
				name:'OSItemProductClassTree_Memo',xtype:'textarea'
			},
			{boxLabel:'是否使用',name:'OSItemProductClassTree_IsUse',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'OSItemProductClassTree_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.OSItemProductClassTree_CName,
			SName:values.OSItemProductClassTree_SName,
			Shortcode:values.OSItemProductClassTree_Shortcode,
			IsUse:values.OSItemProductClassTree_IsUse ? true : false,
			Memo:values.OSItemProductClassTree_Memo
		};
		
		if(values.OSItemProductClassTree_AreaID){
			entity.AreaID=values.OSItemProductClassTree_AreaID;
		}
		var userId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) ;
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			entity.CreatorID=userId;
		}
		if(username){
			entity.CreatorName=username;
		}
		if(values.OSItemProductClassTree_PItemProductClassTreeID){
			entity.PItemProductClassTreeID=values.OSItemProductClassTree_PItemProductClassTreeID;
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			'CName','SName','Shortcode','IsUse',
			'Memo','PItemProductClassTreeID','Id','AreaID'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.OSItemProductClassTree_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		//区域
    	var Area = me.getComponent('OSItemProductClassTree_Area'),
			AreaID = me.getComponent('OSItemProductClassTree_AreaID');
			
		if(data.OSItemProductClassTree_AreaID){
		   var	v = me.AreaEnum[data.OSItemProductClassTree_AreaID];
		   Area.setValue(v);
		}else{
			Area.setValue('');
		}
		
        //父检验项目分类树
	    var PItemProductClassTreeName = me.getComponent('OSItemProductClassTree_PItemProductClassTreeName'),
		    PItemProductClassTreeID = me.getComponent('OSItemProductClassTree_PItemProductClassTreeID');
	    if(me.editParentName){
	      	PItemProductClassTreeName.setValue(me.editParentName);
	    }
		return data;
	},
	/**保存后返回数据*/
	returnData:function(id){
		var me = this,
			values = me.getForm().getValues(),
			data = me.callParent(arguments);
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
			//父检验项目分类树
		var PItemProductClassTreeName = me.getComponent('OSItemProductClassTree_PItemProductClassTreeName'),
			PItemProductClassTreeID = me.getComponent('OSItemProductClassTree_PItemProductClassTreeID');
        
    	if(PItemProductClassTreeName){
			PItemProductClassTreeName.on({
				check: function(p, record) {
					PItemProductClassTreeName.setValue(record ? record.get('text') : '');
					PItemProductClassTreeID.setValue(record ? record.get('tid') : '');
					p.close();
				}
			});
		}
    	//区域
    	var Area = me.getComponent('OSItemProductClassTree_Area'),
			AreaID = me.getComponent('OSItemProductClassTree_AreaID');
		Area.on({
			check: function(p, record) {
				Area.setValue(record ? record.get('ClientEleArea_AreaCName') : '');
				AreaID.setValue(record ? record.get('ClientEleArea_Id') : '');
				p.close();
			}
		});
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			//区域
    	    var Area = me.getComponent('OSItemProductClassTree_Area'),
			AreaID = me.getComponent('OSItemProductClassTree_AreaID');
			if(me.AreaID) {
				Area.setValue(me.AreaName);
				AreaID.setValue(me.AreaID);
			}else{
				Area.setValue('');
			}
			//父检验项目分类树
		    var PItemProductClassTreeName = me.getComponent('OSItemProductClassTree_PItemProductClassTreeName'),
			PItemProductClassTreeID = me.getComponent('OSItemProductClassTree_PItemProductClassTreeID');
            if(me.ParentID) {
				PItemProductClassTreeName.setValue(me.ParentName);
				PItemProductClassTreeID.setValue(me.ParentID);
			}
		} else {
			me.getForm().setValues(me.lastData);
		}
	}
});