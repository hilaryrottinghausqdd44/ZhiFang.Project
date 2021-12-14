Ext.define('Shell.class.setting.siteQuery.empDeptLinks.form',{
    extend: 'Shell.ux.panel.AppPanel',
    formType:'',

	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.formType =='update'){
			me.getComponent("UserNo").setValue(me.record[0].data.UserNo);
			me.getComponent("CName").setValue([me.record[0].data.CName]);
			me.getComponent("ShortCode").setValue(me.record[0].data.ShortCode);
			me.getComponent("Password").setValue(me.record[0].data.Password);
			me.getComponent("Role").setValue(me.record[0].data.Role);
			me.getComponent("DispOrder").setValue(me.record[0].data.DispOrder);
		}
	},

	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var str = "";
		me.items = [
							{
								xtype:"textfield",itemId:"UserNo",width:350,
								style:"margin-left:20px;margin-top:20px",name:"UserNo",fieldLabel:"用户编号",
								hidden:true
							},
							{
								xtype:"textfield",itemId:"CName",width:350,
								style:"margin-left:20px;margin-top:20px",name:"CName",fieldLabel:"用户姓名"
							},
							{
								xtype:"textfield",itemId:"ShortCode",width:350,
								style:"margin-left:20px",name:"ShortCode",fieldLabel:"账号"
							},
							{
								xtype:"textfield",itemId:"Password",width:350,
								style:"margin-left:20px",name:"Password",fieldLabel:"密码",
							},
							{
								xtype:"textfield",itemId:"Role",width:350,
								style:"margin-left:20px",name:"Role",fieldLabel:"角色"
							},
							{
								xtype:"numberfield",itemId:"DispOrder",width:350,
								style:"margin-left:20px",name:"DispOrder",fieldLabel:"显示顺序"
							},
							{
								xtype:"button",itemId:"btn",width:200,
								style:"margin-left:100px;margin-top:10px",name:"submitBtn",text:"提交",
								handler:function(){
									//添加、修改保存事件
									var UserNo=me.getComponent("UserNo").value;
									var	CName=me.getComponent("CName").value;
									var	ShortCode=me.getComponent("ShortCode").value;
									var	Password=me.getComponent("Password").value;
									var	Role=me.getComponent("Role").value;
									var	DispOrder=me.getComponent("DispOrder").value;
									
									//数据不为空
									if(ShortCode != "" && ShortCode != null && Password != "" && Password != null){
										var str = "";//是否成功
										var entity = {
													"UserNo":UserNo,
													"CName": CName, 
													"ShortCode": ShortCode, 
													"Password" : Password,
													"Role": Role ? Role : null,
													"DispOrder": DispOrder?DispOrder:0
										};
										
										Ext.Ajax.defaultPostHeader = 'application/json';
										Ext.Ajax.request({
											method: 'post',
										    url: Shell.util.Path.rootPath +'/ServiceWCF/DictionaryService.svc/AddAndUpdatePUser',
									        params:Ext.JSON.encode({entity:entity}),
										    success: function(response){
										        str = Ext.JSON.decode(response.responseText);
										        me.fireEvent("save",me,str);
									    	}
										});
									}else{
										Ext.MessageBox.alert("操作提示","账户和密码不可为空！");
									}
								}
							}
						]
		;
		return me.items;
	}

});
