/**
 * 机构表单
 * @author liangyl
 * @version 2018-05-14
 */
Ext.define('Shell.class.rea.center.register.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '机构信息',
	
	width:240,
    height:300,
	/**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchCenOrgById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddCenOrg',
    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateCenOrgByField',
    /**获取机构编号服务地址*/
    getOrgNoUrl:'/ReaSysManageService.svc/ST_UDTO_GetMaxOrgNo',
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**内容周围距离*/
	bodyPadding:'10px 10px 0 10px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'add',
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 210,
		labelAlign: 'right'
	},
	/**带功能按钮栏*/
//	hasButtontoolbar:false,	/**功能按钮栏位置*/
	buttonDock:'top',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			xtype: 'label',text: '机构信息',
			margin: '0 0 8 5',style: "font-weight:bold;color:blue;",
			itemId: 'CenOrgInfo',name: 'CenOrgInfo'
		}];
		return items;
	},
	createItems:function(){
		var me = this,
			items = [];
		items.push({fieldLabel:'主键ID',name:'CenOrg_Id',hidden:true,type:'key'});
		//中文名
		items.push({
			fieldLabel:'中文名称',name:'CenOrg_CName',
			emptyText:'必填项',allowBlank:false,
			colspan: 2,width: me.defaults.width *2
		});
		//英文名
		items.push({
			fieldLabel:'英文名称',name:'CenOrg_EName'
		});
			//机构类型
		items.push({
			fieldLabel: '机构类型',
			name: 'CenOrg_OrgTypeName',
			itemId: 'CenOrg_OrgTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.cenorgtype.CheckGrid',
			emptyText:'必填项',allowBlank:false
		}, {
			fieldLabel: '机构类型主键ID',
			name: 'CenOrg_OrgTypeID',
			itemId: 'CenOrg_OrgTypeID',
			hidden: true
		});
		//机构代码
		items.push({
			fieldLabel:'机构代码',name:'CenOrg_ShortCode'
		});
		
		//机构编号
		items.push({
			fieldLabel:'机构编号',
			name:'CenOrg_OrgNo',
			itemId:'CenOrg_OrgNo',hidden:true,
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,
			editable:true,
			onTriggerClick:function(){
			}
		});

		//服务器IP
		items.push({
			fieldLabel:'服务器IP',name:'CenOrg_ServerIP'
		});
		//服务器端口
		items.push({
			fieldLabel:'服务器端口',name:'CenOrg_ServerPort'
		});
		//联系人
		items.push({
			fieldLabel:'联系人',name:'CenOrg_Contact'
		});
		//电话
		items.push({
			fieldLabel:'电话',name:'CenOrg_Tel'
		});
		//热线电话
		items.push({
			fieldLabel:'热线电话',name:'CenOrg_HotTel'
		});
		//显示次序
		items.push({
			fieldLabel:'显示次序',name:'CenOrg_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0,minValue:0
		});
		//启用
		items.push({
			fieldLabel:'启用',name:'CenOrg_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		});
		//邮箱
		items.push({
			fieldLabel:'邮箱',name:'CenOrg_Email'
		});
		//传真
		items.push({
			fieldLabel:'传真',name:'CenOrg_Fox'
		});
		
		//网址
		items.push({
			fieldLabel:'网址',name:'CenOrg_WebAddress',
			colspan: 1,width: me.defaults.width *1
		});
		//机构地址
		items.push({
			fieldLabel:'机构地址',name:'CenOrg_Address',
			colspan: 3,width: me.defaults.width *3
		});
		//备注
		items.push({
			xtype:'textarea',fieldLabel:'备注',
			name:'CenOrg_Memo',
			height:60,
			colspan: 3,width: me.defaults.width *3
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
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
			Memo:values.CenOrg_Memo,
			OrgTypeID:values.CenOrg_OrgTypeID
		};
		return entity;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			CenOrgTypeName = me.getComponent('CenOrg_OrgTypeName'),
			CenOrgTypeId = me.getComponent('CenOrg_OrgTypeID');
		
		CenOrgTypeName.on({
			check: function(p, record) {
				CenOrgTypeName.setValue(record ? record.get('CenOrgType_CName') : '');
				CenOrgTypeId.setValue(record ? record.get('CenOrgType_Id') : '');
				p.close();
			}
		});
	},
	
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me =this;
		var OrgTypeName = me.getComponent('CenOrg_OrgTypeName');
//		var OrgTypeID = me.getComponent('CenOrg_OrgTypeID');
//      var OrgTypeIDVal = me.CenOrgTypeEnum[data.CenOrg_OrgTypeID];
//      OrgTypeName.setValue(OrgTypeIDVal);
//      
//      var POrgName = me.getComponent('CenOrg_POrgName');
//		var POrgID = me.getComponent('CenOrg_POrgID');
//      var POrgVal = me.POrgEnum[data.CenOrg_POrgID];
//      POrgName.setValue(POrgVal);
		data.CenOrg_Visible = data.CenOrg_Visible == '1' ? true : false;
		return data;
	}
});