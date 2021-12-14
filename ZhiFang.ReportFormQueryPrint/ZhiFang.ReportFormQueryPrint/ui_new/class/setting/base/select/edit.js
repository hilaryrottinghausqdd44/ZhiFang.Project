Ext.define("Shell.class.setting.base.select.edit", {
    extend: 'Ext.form.Panel',
	
	width: 500,
    //height: 600,
    editStore:'',//修改前数据
    appType:'',
    EditUrl: '/ServiceWCF/DictionaryService.svc/UpdateSearchSetting',
    //渲染--为表单赋值
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.getComponent("CName").setValue(me.editStore.CName);
		me.getComponent("ShowName").setValue(me.editStore.ShowName);
		me.getComponent("SelectName").setValue(me.editStore.SelectName);
		me.getComponent("TextWidth").setValue(me.editStore.TextWidth);
		me.getComponent("Width").setValue(me.editStore.Width);
		me.getComponent("Site").setValue(me.editStore.Site);
		me.getComponent("ShowOrderNo").setValue(me.editStore.ShowOrderNo);
		me.getComponent("IsShow").setValue(me.editStore.IsShow);
		me.getComponent("STID").setValue(me.editStore.STID);
		me.getComponent("SID").setValue(me.editStore.SID);
		
		
		
	},
    //初始化
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        //me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    //创建子组件
	createItems:function(){
		var me = this;
		var states = Ext.create('Ext.data.Store', {//下拉框
			    fields: ['abbr', 'name'],
			    data : [
			        {"abbr":false, "name":"否"},//false
			        {"abbr":true, "name":"是"}//true
			    ]
			});

		me.items = [
							{xtype:"textfield",itemId:"CName",width:350,disabled:true,labelStyle:"opacity:1",style:"margin-top:10px;margin-left:70px;",name:"CName",fieldLabel:"列名"},
							{xtype:"textfield",itemId:"ShowName",width:350,style:"margin-left:70px",name:"ShowName",fieldLabel:"显示名称"},
							{xtype:"textfield",itemId:"SelectName",disabled:true,labelStyle:"opacity:1",width:350,style:"margin-left:70px",name:"SelectName",fieldLabel:"字段名"},
							{xtype:"numberfield",itemId:"TextWidth",width:350,style:"margin-left:70px",name:"TextWidth",fieldLabel:"文字宽度"},
							{xtype:"numberfield",itemId:"Width",width:350,style:"margin-left:70px",name:"Width",fieldLabel:"总体宽度"},
							{xtype:"textfield",itemId:"Site",width:350,style:"margin-left:70px",name:"Site",fieldLabel:"站点"},
							{xtype:"numberfield",itemId:"ShowOrderNo",width:350,style:"margin-left:70px",name:"ShowOrderNo",fieldLabel:"显示顺序"},
							{xtype:"combo",itemId:"IsShow",width:350,editable:false,style:"margin-left:70px",name:"IsShow",fieldLabel:"是否显示",store: states,queryMode: 'local',displayField: 'name',valueField: 'abbr'},
							{xtype:"textfield",itemId:"STID",width:350,hidden:true,style:"margin-left:70px",name:"STID",fieldLabel:"类型"},
							{xtype:"textfield",itemId:"SID",width:350,hidden:true,style:"margin-left:70px",name:"SID",fieldLabel:"类型"},							
							{xtype:"button",itemId:"btn",width:200,style:"margin:10px 0 10px 150px",name:"submitBtn",text:"提交",
								handler:function(){
									var arr = [];
									var rs = "";
									arr.push({
						                "STID": me.getComponent("STID").value,
						                "SID": me.getComponent("SID").value,
						                "IsShow": me.getComponent("IsShow").value,
						                "CName": me.getComponent("CName").value,
						                "ShowName": me.getComponent("ShowName").value,
						                "SelectName": me.getComponent("SelectName").value,
						                "Width": me.getComponent("Width").value,
						                "Site": me.getComponent("Site").value,
						                "TextWidth": me.getComponent("TextWidth").value,
						                "AppType": me.appType,
						                "ShowOrderNo": me.getComponent("ShowOrderNo").value						                
						            });
						            Ext.Ajax.defaultPostHeader = 'application/json';
							        Ext.Ajax.request({
							            url: Shell.util.Path.rootPath + me.EditUrl,
							            async: false,
							            method: 'POST',
							            params: Ext.encode({ "bSearchSetting": arr }),
							            success: function (response, options) {
							                 rs = Ext.JSON.decode(response.responseText);
							                 me.fireEvent("save",me,rs);
							            }
							        });
								}
							}
						];
		return me.items;
	}
});