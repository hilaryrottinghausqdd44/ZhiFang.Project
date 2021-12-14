/**
 * 机构表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorg.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '机构表单',
	
	width:240,
    height:300,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddCenOrg',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateCenOrgByField',
    /**获取机构编号服务地址*/
    getOrgNoUrl:'/ReagentService.svc/ST_UDTO_GetMaxOrgNo',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'10px 10px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:65,
        labelAlign:'right'
    },
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({fieldLabel:'主键ID',name:'CenOrg_Id',hidden:true,type:'key'});
		
		//机构类型
		items.push({
			fieldLabel: '机构类型',
			name: 'CenOrg_CenOrgType_CName',
			itemId: 'CenOrg_CenOrgType_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorgtype.CheckGrid',
			emptyText:'必填项',allowBlank:false
		}, {
			fieldLabel: '机构类型主键ID',
			name: 'CenOrg_CenOrgType_Id',
			itemId: 'CenOrg_CenOrgType_Id',
			hidden: true
		});
		//机构编号
		items.push({
			fieldLabel:'机构编号',
			name:'CenOrg_OrgNo',
			itemId:'CenOrg_OrgNo',
			emptyText:'必填项',
			allowBlank:false,
			
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,
			//editable:false,
			editable:true,
			onTriggerClick:function(){
				me.onInitOrgNo();
			}
		});
		//中文名
		items.push({
			fieldLabel:'中文名',name:'CenOrg_CName',
			emptyText:'必填项',allowBlank:false
		});
		//英文名
		items.push({
			fieldLabel:'英文名',name:'CenOrg_EName'
		});
		//服务器IP
		items.push({
			fieldLabel:'服务器IP',name:'CenOrg_ServerIP'
		});
		//服务器端口
		items.push({
			fieldLabel:'服务器端口',name:'CenOrg_ServerPort'
		});
		//机构代码
		items.push({
			fieldLabel:'机构代码',name:'CenOrg_ShortCode'
		});
		//显示次序
		items.push({
			fieldLabel:'显示次序',name:'CenOrg_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		});
		//启用
		items.push({
			fieldLabel:'启用',name:'CenOrg_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		});
		
		//机构地址
		items.push({
			fieldLabel:'机构地址',name:'CenOrg_Address'
		});
		//联系人
		items.push({
			fieldLabel:'联系人',name:'CenOrg_Contact'
		});
		//电话
		items.push({
			fieldLabel:'电话',name:'CenOrg_Tel'
		});
		//传真
		items.push({
			fieldLabel:'传真',name:'CenOrg_Fox'
		});
		//邮箱
		items.push({
			fieldLabel:'邮箱',name:'CenOrg_Email'
		});
		//网址
		items.push({
			fieldLabel:'网址',name:'CenOrg_WebAddress'
		});
		
		
		//备注
		items.push({
			xtype:'textarea',fieldLabel:'备注',
			name:'CenOrg_Memo',
			height:60
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CenOrgType:{
				Id:values.CenOrg_CenOrgType_Id
			},
			OrgNo:values.CenOrg_OrgNo,
			CName:values.CenOrg_CName,
			EName:values.CenOrg_EName,
			ServerIP:values.CenOrg_ServerIP,
			ServerPort:values.CenOrg_ServerPort,
			ShortCode:values.CenOrg_ShortCode,
			DispOrder:values.CenOrg_DispOrder,
			Visible:values.CenOrg_Visible ? 1 : 0,
			Address:values.CenOrg_Address,
			Contact:values.CenOrg_Contact,
			Tel:values.CenOrg_Tel,
			Fox:values.CenOrg_Fox,
			Email:values.CenOrg_Email,
			WebAddress:values.CenOrg_WebAddress,
			Memo:values.CenOrg_Memo
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = [
				'Id','CName','EName','ServerIP','ServerPort','ShortCode','OrgNo',
				'DispOrder','Visible','Address','Contact','Tel','Fox','Email',
				'WebAddress','Memo','CenOrgType_Id'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.CenOrg_Id;
		//delete entity.entity.OrgNo;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.CenOrg_Visible = data.CenOrg_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			CenOrgTypeName = me.getComponent('CenOrg_CenOrgType_CName'),
			CenOrgTypeId = me.getComponent('CenOrg_CenOrgType_Id');
		
		CenOrgTypeName.on({
			check: function(p, record) {
				CenOrgTypeName.setValue(record ? record.get('CenOrgType_CName') : '');
				CenOrgTypeId.setValue(record ? record.get('CenOrgType_Id') : '');
				p.close();
			}
		});
	},
	/**初始化机构编号*/
	onInitOrgNo:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.getOrgNoUrl;
			
		var CenOrg_OrgNo = me.getComponent('CenOrg_OrgNo');
		me.showMask('机构编号获取中...');//显示遮罩层
		JcallShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				CenOrg_OrgNo.setValue(data.value);
			}else{
				CenOrg_OrgNo.setValue('');
				JShell.Msg.error('机构编号获取失败，请重新获取',null,1000);
			}
		});
	},
	isAdd:function(){
		var me = this;
		me.callParent(arguments);
		var CenOrg_OrgNo = me.getComponent('CenOrg_OrgNo');
		CenOrg_OrgNo.enable();
		me.onInitOrgNo();
	},
	isEdit:function(id){
		var me = this;
		me.callParent(arguments);
		//var CenOrg_OrgNo = me.getComponent('CenOrg_OrgNo');
		//CenOrg_OrgNo.disable();
	}
});