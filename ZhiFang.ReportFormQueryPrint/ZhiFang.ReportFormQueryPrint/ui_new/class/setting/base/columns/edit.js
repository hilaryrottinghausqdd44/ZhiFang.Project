Ext.define("Shell.class.setting.base.columns.edit", {
    extend: 'Ext.form.Panel',
	
	width: 500,
    //height: 600,
    editStore:'',//修改前数据
    appType:'',
    EditUrl: '/ServiceWCF/DictionaryService.svc/UpdateColumnsSetting',
    //渲染--为表单赋值
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.getComponent("CName").setValue(me.editStore.CName);
		me.getComponent("ShowName").setValue(me.editStore.ShowName);
		me.getComponent("ColumnName").setValue(me.editStore.ColumnName);
		me.getComponent("Width").setValue(me.editStore.Width);
		me.getComponent("Site").setValue(me.editStore.Site);
		me.getComponent("OrderNo").setValue(me.editStore.OrderNo);
		me.getComponent("OrderFlag").setValue(me.editStore.OrderFlag);
		me.getComponent("OrderDesc").setValue(me.editStore.OrderDesc);
		me.getComponent("OrderMode").setValue(me.editStore.OrderMode);
		//me.getComponent("Render").setValue(me.editStore.Render);
		me.getComponent("IsShow").setValue(me.editStore.IsShow);
		me.getComponent("CTID").setValue(me.editStore.CTID);
		me.getComponent("ColID").setValue(me.editStore.ColID);
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
		var states = Ext.create('Ext.data.Store', {
			    fields: ['abbr', 'name'],
			    data : [
			        {"abbr":false, "name":"否"},//false
			        {"abbr":true, "name":"是"}//true
			    ]
			});
		var OrderModeStates = Ext.create('Ext.data.Store', {
		    fields: ['abbr', 'name'],
		    data : [
		        {"abbr":"Desc", "name":"倒序"},//Desc
		        {"abbr":"Asc", "name":"正序"}//Asc
		    ]
		});

		me.items = [
							{xtype:"textfield",itemId:"CName",width:350,disabled:true,labelStyle:"opacity:1",style:"margin-top:10px;margin-left:70px;",name:"CName",fieldLabel:"列名"},
							{xtype:"textfield",itemId:"ShowName",width:350,style:"margin-left:70px",name:"ShowName",fieldLabel:"显示名称"},
							{xtype:"textfield",itemId:"ColumnName",disabled:true,labelStyle:"opacity:1",width:350,style:"margin-left:70px",name:"ColumnName",fieldLabel:"字段名"},
							{xtype:"numberfield",itemId:"Width",width:350,style:"margin-left:70px",name:"Width",fieldLabel:"宽度"},
							{xtype:"textfield",itemId:"Site",width:350,style:"margin-left:70px",name:"Site",fieldLabel:"站点"},
							{xtype:"numberfield",itemId:"OrderNo",width:350,style:"margin-left:70px",name:"OrderNo",fieldLabel:"显示顺序"},
							{xtype:"combo",itemId:"OrderFlag",width:350,editable:false,style:"margin-left:70px",name:"OrderFlag",fieldLabel:"是否排序",store: states,queryMode: 'local',displayField: 'name',valueField: 'abbr'},
							{xtype:"numberfield",itemId:"OrderDesc",width:350,style:"margin-left:70px",name:"OrderDesc",fieldLabel:"排序顺序"},
							//{xtype:"textfield",itemId:"OrderMode",width:350,style:"margin-left:70px",name:"OrderMode",fieldLabel:"排序方式"},
							{xtype:"combo",itemId:"OrderMode",width:350,editable:false,style:"margin-left:70px",name:"OrderMode",fieldLabel:"排序方式",store: OrderModeStates,queryMode: 'local',displayField: 'name',valueField: 'abbr'},
							//{xtype:"textfield",itemId:"Render",width:350,disabled:true,labelStyle:"opacity:1",style:"margin-left:70px",name:"Render",fieldLabel:"自定义方法"},
							{xtype:"combo",itemId:"IsShow",width:350,editable:false,style:"margin-left:70px",name:"IsShow",fieldLabel:"是否显示",store: states,queryMode: 'local',displayField: 'name',valueField: 'abbr'},
							{xtype:"textfield",itemId:"ColID",width:350,hidden:true,style:"margin-left:70px",name:"ColID",fieldLabel:"类型"},
							{xtype:"textfield",itemId:"CTID",width:350,hidden:true,style:"margin-left:70px",name:"CTID",fieldLabel:"类型"},
							{xtype:"button",itemId:"btn",width:200,style:"margin:10px 0 10px 150px",name:"submitBtn",text:"提交",
								handler:function(){
									var arr = [];
									var rs = "";
									arr.push({
						                "ColID": me.getComponent("ColID").value,
						                "CTID": me.getComponent("CTID").value,
						                "IsShow": me.getComponent("IsShow").value,
						               // "Render": me.getComponent("Render").value,
						                "CName": me.getComponent("CName").value,
						                "ShowName": me.getComponent("ShowName").value,
						                "ColumnName": me.getComponent("ColumnName").value,
						                "Width": me.getComponent("Width").value,
						                "Site": me.getComponent("Site").value,
						                "OrderMode": me.getComponent("OrderMode").value,
						                "OrderFlag": me.getComponent("OrderFlag").value,
						                "OrderDesc": me.getComponent("OrderDesc").value,
						                "AppType": me.appType,
						                "OrderNo": me.getComponent("OrderNo").value
						            });
						            Ext.Ajax.defaultPostHeader = 'application/json';
							        Ext.Ajax.request({
							            url: Shell.util.Path.rootPath + me.EditUrl,
							            async: false,
							            method: 'POST',
							            params: Ext.encode({ "bColumnsSetting": arr }),
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