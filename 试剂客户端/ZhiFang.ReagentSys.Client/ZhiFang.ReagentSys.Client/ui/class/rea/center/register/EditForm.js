/**
 * 机构表单
 * @author liangyl
 * @version 2018-05-14
 */
Ext.define('Shell.class.rea.center.register.EditForm', {
	extend: 'Shell.class.rea.center.register.Form',
	title: '机构信息',

	/**带功能按钮栏*/
	hasButtontoolbar:true,	/**功能按钮栏位置*/
	buttonDock:'bottom',
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'edit',
	selectCenOrgTypeUrl:'/ReaSysManageService.svc/ST_UDTO_SearchCenOrgTypeByHQL?isPlanish=true',
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchCenOrgById?isPlanish=true',
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['->','save','reset'];
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
		return {entity:entity};
	},/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = [
				'Id','CName','EName','ServerIP','ServerPort','ShortCode',
				'DispOrder','Visible','Address','Contact','Tel','Fox','Email',
				'WebAddress','Memo','OrgTypeID'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.CenOrg_Id;
		return entity;
	},
		/**返回数据处理方法*/
	changeResult: function(data) {
		var me =this;
		var OrgTypeName = me.getComponent('CenOrg_OrgTypeName');
		var OrgTypeID = me.getComponent('CenOrg_OrgTypeID');
		me.getCenOrgTypeInfo(data.CenOrg_OrgTypeID,function(data){
			if(data && data.value){
				if(data.value.list.length>0){
					var val =data.value.list[0].CenOrgType_CName;
					OrgTypeName.setValue(val);
				}
			}
		});
		data.CenOrg_Visible = data.CenOrg_Visible == '1' ? true : false;
		return data;
	},
	/**获取机构类型信息*/
	getCenOrgTypeInfo:function(val,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectCenOrgTypeUrl;
		url += "&fields=CenOrgType_CName,CenOrgType_Id&where=cenorgtype.Id="+val;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});