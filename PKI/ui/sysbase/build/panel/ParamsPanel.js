/**
 * 属性设置面板
 * @author Jcall
 * @version 2014-08-21
 */
Ext.define('Shell.sysbase.build.panel.ParamsPanel',{
	extend:'Shell.ux.form.Panel',
	
	/**是否有数据对象信息设置*/
	hasObjectInfo:true,
	/**是否有布局信息设置*/
	hasLayoutInfo:true,
	
	/**内边距*/
	bodyPadding:5,
	/**默认容器属性*/
	defaults:{
		padding:'0 5 0 5',
		collapsible:true,
		layout:'anchor',
		defaultType:'textfield',
		defaults:{anchor:'100%',labelWidth:60}
	},
	/**面板初始默认宽度*/
	defaultPanelWidth:800,
	/**面板初始默认高度*/
	defaultPanelHeight:600,
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('appInfoBlur,appInfoChange,panelInfoBlur,panelInfoChange,layoutTypeChange');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		//创建功能信息区域
		items.push(me.createAppInfo());
		//创建面板信息区域
		items.push(me.createPanelInfo());
		//创建数据对象信息区域
		if(me.hasObjectInfo){
			items.push(me.createObjectInfo());
		}
		//创建布局信息区域
		if(me.hasLayoutInfo){
			items.push(me.createLayoutInfo());
		}
			
		return items;
	},
	/**创建功能信息区域*/
	createAppInfo:function(){
		var me = this,
			fieldset = {
			xtype:'fieldset',title:'功能信息',itemId:'appInfo',
	        items:[{
				fieldLabel:'功能编号',labelStyle:'color:red;',
				allowBlank:false,emptyText:'功能的唯一编号',
				itemId:'ModuleOperCode',name:'ModuleOperCode'
	        },{
				fieldLabel:'中文名称',labelStyle:'color:red;',
				allowBlank:false,emptyText:'功能的唯一名称',
                itemId:'CName',name:'CName'
	        },{
				xtype:'textareafield',fieldLabel:'功能简介',
				grow:true,emptyText:'对于该功能的一个简单说明',
	            itemId:'ModuleOperInfo',name:'ModuleOperInfo'
	        }]
		};
		
		var length = fieldset.items.length;
		for(var i=0;i<length;i++){
			fieldset.items[i].listeners = {
				blur:function(field){me.fireEvent('appInfoBlur',me,field);},
				change:function(field,newV,oldV){me.fireEvent('appInfoChange',me,field,newV,oldV);}
			};
		}
		
		return fieldset;
	},
	/**创建面板信息区域*/
	createPanelInfo:function(){
		var me = this,
			fieldset = {
			xtype:'fieldset',title:'面板信息',itemId:'panelInfo',
	        items:[{
	        	fieldLabel:'显示名称',labelStyle:'color:red;',
	        	allowBlank:false,emptyText:'面板的标题',
            	itemId:'TitleValue',name:'TitleValue'
	        },{
	        	xtype:'numberfield',fieldLabel:'面板宽度',
	        	emptyText:'默认',value:me.defaultPanelWidth,
            	itemId:'Width',name:'Width'
	        },{
	        	xtype:'numberfield',fieldLabel:'面板高度',
	        	emptyText:'默认',value:me.defaultPanelHeight,
            	itemId:'Height',name:'Height'
	        }]
		};
		
		var length = fieldset.items.length;
		for(var i=0;i<length;i++){
			fieldset.items[i].listeners = {
				blur:function(field){me.fireEvent('panelInfoBlur',me,field);},
				change:function(field,newV,oldV){me.fireEvent('panelInfoChange',me,field,newV,oldV);}
			};
		}
		
		return fieldset;
	},
	/**创建数据对象信息区域*/
	createObjectInfo:function(){
		var me = this,
			fieldset = {
			xtype:'fieldset',title:'数据对象',itemId:'dataInfo',
		    items:[{
		    	xtype:'uxcombogrid',fieldLabel:'数据对象',
		    	searchFields:['ClassName','CName','EName','Description','ShortCode'],
				searchEmptyText:'对象名称/中文名/英文名/对象简称/对象说明',
				displayField:'CName',valueField:'ClassName',
				matchFieldWidth:false,searchWidth:200,
		    	url:'/ConstructionService.svc/CS_BA_GetEntityList',
		    	columns:[
					{dataIndex:'ClassName',text:'对象名称',width:80},
					{dataIndex:'CName',text:'中文名',width:80},
					{dataIndex:'EName',text:'英文名',width:80},
					{dataIndex:'ShortCode',text:'对象简称',width:80},
					{dataIndex:'Description',text:'对象说明',width:80}
				],
		    	itemId:'dataObject',name:'dataObject'
		    }]
		};
		return fieldset;
	},
	/**创建布局信息区域*/
	createLayoutInfo:function(){
		var me = this,
			fieldset = {
			xtype:'fieldset',title:'布局信息',itemId:'layoutInfo',
			items:[{
				xtype:'trigger',enableKeyEvents:true,editable:false,
				triggerCls:'x-form-search-trigger',
				onTriggerClick:function(){me.onOpenLayout();},
				
				fieldLabel:'布局类型',labelStyle:'color:red;',
	        	allowBlank:false,emptyText:'必须选一个布局类型',
            	itemId:'TitleValue',name:'TitleValue'
			}]
		};
			
		return fieldset;
	},
	/**打开布局类型选择面板*/
	onOpenLayout:function(){
		var me = this,
			layoutType = me.getComponent('layoutInfo').getComponent('TitleValue').layoutType || 1,
			win = Shell.util.Win.open('Shell.sysbase.build.panel.LayoutPanel',{layoutType:layoutType});
		win.on({
			accept:function(panel,but,info){me.onLyoutTypeChange(info);win.close();},
			cancel:function(panel,but){win.close();}
		});
	},
	/**布局类型变化*/
	onLyoutTypeChange:function(v){
		var me = this,
			value = v.value,
			name = v.name,
			layout = v.layout || {},
			info = v.info,
			TitleValue = me.getComponent('layoutInfo').getComponent('TitleValue');
			
		if(value != TitleValue.layoutType){
			TitleValue.layoutType = value;
			TitleValue.setValue(name);
			
			me.fireEvent('layoutTypeChange',me,v);
		}
	}
});