Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var other = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true
	});
	
	var textfield = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'textfield',
		isFixedValue:false,
		value:'localtime'
	});
	var textareafield = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'textareafield',
		value:'localtime'
	});
	var numberfield = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'numberfield',
		value:123
	});
	var datefield = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'datefield',
		value:'1111年11月11日'
	});
	var timefield = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'timefield',
		value:'4:22 上午'
	});
	var radiogroup = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'radiogroup',
		valueField:'HREmployee_Id',
		displayField:'HREmployee_CName',
		serverUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
		value:'5047418739615210386'
	});
	var checkboxgroup = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'checkboxgroup',
		valueField:'HREmployee_Id',
		displayField:'HREmployee_CName',
		serverUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
		value:['5047418739615210386','4']
	});
	var combobox = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'combobox',
		valueField:'HREmployee_Id',
		displayField:'HREmployee_CName',
		serverUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
		value:'4'
	});
	var checkboxfield = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		collapsible:true,
		type:'checkboxfield',
		value:true
	});
	
	var buttons = [{
		xtype:'button',text:'文本框值',
		handler:function(){alert(textfield.getValue());}
	},{
		xtype:'button',text:'文本域值',
		handler:function(){alert(textareafield.getValue());}
	},{
		xtype:'button',text:'数字框值',
		handler:function(){alert(numberfield.getValue());}
	},{
		xtype:'button',text:'日期框值',
		handler:function(){alert(datefield.getValue());}
	},{
		xtype:'button',text:'时间框值',
		handler:function(){alert(timefield.getValue());}
	},{
		xtype:'button',text:'单选组值',
		handler:function(){alert(radiogroup.getValue());}
	},{
		xtype:'button',text:'复选组值',
		handler:function(){alert(checkboxgroup.getValue().join(","));}
	},{
		xtype:'button',text:'下拉框值',
		handler:function(){alert(combobox.getValue());}
	},{
		xtype:'button',text:'复选框值',
		handler:function(){alert(checkboxfield.getValue());}
	}];
	//总体布局
	Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[{
			xtype:'form',
			autoScroll:true,
			title:'参数面板',
			bodyPadding:5,
			dockedItems:{
				xtype:'toolbar',
        		dock:'top',
				items:buttons
			},
			items:[other,textfield,textareafield,numberfield,datefield,timefield,radiogroup,checkboxgroup,combobox,checkboxfield]
		}]
	});
});