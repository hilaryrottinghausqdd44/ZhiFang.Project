/**
 * 订货方/供货方表单
 * @author liangyl	
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.cenorg.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '订货方/供货方信息',
	
	width:440,
    height:390,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaCenOrg',
    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaCenOrgByField',
    /**检查机构编码是否存在*/
    selectUrl2:'/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**内容周围距离*/
	bodyPadding:'10px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 200,
		labelAlign: 'right'
	},
	/**机构类型 供货方0，订货方1*/
	OrgType:'0',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		me.on({
			
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({fieldLabel:'主键ID',name:'ReaCenOrg_Id',hidden:true,type:'key'});
		//机构编号
        items.push({
			fieldLabel:'机构编号',name:'ReaCenOrg_OrgNo',
			itemId:'ReaCenOrg_OrgNo',//xtype:'numberfield',
			colspan: 1, locked:true,readOnly:true,
			width: me.defaults.width * 1
		});
		//平台机构编号
        items.push({
			fieldLabel:'平台机构编号',name:'ReaCenOrg_PlatformOrgNo',
			itemId:'ReaCenOrg_PlatformOrgNo',xtype:'numberfield',
			colspan: 1, style:'color:red;',  
			width: me.defaults.width * 1
		});
		//中文名
		items.push({
			fieldLabel:'机构名称',name:'ReaCenOrg_CName',
			emptyText:'必填项',allowBlank:false,	colspan: 2,
			width: me.defaults.width * 2
		});
		//英文名
		items.push({
			fieldLabel:'英文名称',name:'ReaCenOrg_EName',	colspan: 2,
			width: me.defaults.width * 2
		});
		//联系人
		items.push({
			fieldLabel:'联系人',name:'ReaCenOrg_Contact',	colspan: 1,
			width: me.defaults.width * 1
		});
		
		//显示次序
		items.push({
			fieldLabel:'显示次序',name:'ReaCenOrg_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0,	colspan: 1,
			width: me.defaults.width * 1
		});
		//电话
		items.push({
			fieldLabel:'联系电话',name:'ReaCenOrg_Tel',	colspan: 1,
			width: me.defaults.width * 1
		});
		//启用
		items.push({
			fieldLabel:'启用',name:'ReaCenOrg_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true,	colspan: 1,
			width: me.defaults.width * 1
		});
		//传真
		items.push({
			fieldLabel:'传真',name:'ReaCenOrg_Fox',	colspan: 2,
			width: me.defaults.width * 1
		});
		//邮箱
		items.push({
			fieldLabel:'邮箱',name:'ReaCenOrg_Email',	colspan: 2,
			width: me.defaults.width * 2
		});
		//机构地址
		items.push({
			fieldLabel:'机构地址',name:'ReaCenOrg_Address',	colspan: 2,
			width: me.defaults.width * 2
		});
		//网址
		items.push({
			fieldLabel:'网址',name:'ReaCenOrg_WebAddress',	colspan: 2,
			width: me.defaults.width * 2
		});
		//备注
		items.push({
			xtype:'textarea',fieldLabel:'备注',
			name:'ReaCenOrg_Memo',
			height:60,	colspan: 2,
			width: me.defaults.width * 2
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			
			CName:values.ReaCenOrg_CName,
			EName:values.ReaCenOrg_EName,
			DispOrder:values.ReaCenOrg_DispOrder,
			Visible:values.ReaCenOrg_Visible ? 1 : 0,
			Address:values.ReaCenOrg_Address,
			Contact:values.ReaCenOrg_Contact,
			Tel:values.ReaCenOrg_Tel,
			Fox:values.ReaCenOrg_Fox,
			Email:values.ReaCenOrg_Email,
			WebAddress:values.ReaCenOrg_WebAddress,
			Memo:values.ReaCenOrg_Memo
		};
		if(values.ReaCenOrg_OrgNo){
			entity.OrgNo = values.ReaCenOrg_OrgNo;
		}
	    if(values.ReaCenOrg_PlatformOrgNo){
	    	entity.PlatformOrgNo=values.ReaCenOrg_PlatformOrgNo;
	    }
		if(me.OrgType){
			entity.OrgType=me.OrgType;
		}
	    var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
		if(JShell.Date.toServerDate(DataAddTime)){
			entity.DataUpdateTime=JShell.Date.toServerDate(DataAddTime);
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = [
				'Id','CName','EName','OrgNo',
				'DispOrder','Visible','Address','Contact','Tel','Fox','Email',
				'WebAddress','Memo',
//				'DataUpdateTime',
				'PlatformOrgNo'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.ReaCenOrg_Id;
		//delete entity.entity.OrgNo;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.ReaCenOrg_Visible = data.ReaCenOrg_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	
	isAdd:function(){
		var me = this;
		me.callParent(arguments);
	},
	isEdit:function(id){
		var me = this;
		me.callParent(arguments);
	},
	
	/**更改标题*/
	changeTitle:function(){
	}
});