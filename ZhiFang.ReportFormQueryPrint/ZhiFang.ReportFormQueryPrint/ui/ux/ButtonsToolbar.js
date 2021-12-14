/**
 * 功能按钮栏类
 * 
 * 列表、表单的功能按钮栏都将采用统一的方式
 * 按钮权限统一管理
 */
Ext.define('Shell.ux.ButtonsToolbar',{
	extend:'Ext.toolbar.Toolbar',
	alias:'widget.uxbuttonstoolbar',
	
	requires:['Shell.ux.Button','Shell.ux.form.field.RadioGroup'],
	buttons:[],
	
	/**查询组件内部编号*/
	searchTextItemId:'searchtext',
	/**查询的字段数组*/
	searchFields:null,
	
	/**渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//视图准备完毕
		me.on({
			boxready:function(){me.boxIsReady();}
		});
		//确定所有下拉框组件
		me.initComboFields(me.items.items);
	},
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me._combofields = [];
		me.addEvents('click','search');//添加事件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			buttons = me.buttons,
			length = buttons.length,
			items = [];
		
		for(var i=0;i<length;i++){
			var but = Ext.clone(buttons[i]);
			if(!but) continue;
			
			if(but.xtype){
				items.push(Ext.apply({
					handler:function(but){
						me.onButtonClick(but);
					}
				},but));
				continue;
			}
			
			var type = Ext.typeOf(but);
			var button = null;
			if(type == 'string'){
				switch(but){
					case ' ' : button = {xtype:'tbspacer'};break;
					case '-' : button = {xtype:'tbseparator'};break;
					case '->' : button = {xtype:'tbfill',height:0};break;
					case 'searchtext' : button = me.createSearchText(but);break;
					default : button = me.createButton(but);
				}
			}else if(type == 'object'){
				var btype = but.btype;
				switch(btype){
					case 'combo' : button = me.createCombo(but);break;
					case 'datearea' : button = me.createDatearea(but);break;
					case 'searchtext' : button = me.createSearchText(but);break;
					case null : break;
					case undefined : break;
					default : button = me.createButton(but);
				}
			}
			if(button){items.push(button);}
		}
			
		return items;
	},
	/**创建按钮*/
	createButton:function(config){
		var me = this,
			type = Ext.typeOf(config),
			button = {xtype:'uxbutton'};
			
		if(type == 'string'){
			button.btype = config;
			button.handler = function(but){
				me.onButtonClick(but);
			}
		}else if(type == 'object'){
			button.btype = config.btype;
			if(config.text != null){button.text = config.text;}
			button.handler = config.handler || function(but){
				me.onButtonClick(but);
			}
			button = Ext.applyIf(button,config);
		}
		
		return button;
	},
	/**创建查询组件*/
	createSearchText:function(config){
		var me = this,
			type = Ext.typeOf(config),
			search = {
				xtype:'trigger',
				itemId:me.searchTextItemId,
				triggerCls:'x-form-search-trigger',
				enableKeyEvents:true,
				onTriggerClick:function(){me.onSearch();},
				listeners:{
		            keyup:{
		                fn:function(field,e){
		                	if(e.getKey() == Ext.EventObject.ESC){
		                		field.setValue('');
		                		me.onSearch();
		                	}else if(e.getKey() == Ext.EventObject.ENTER){
		                		me.onSearch();
		                	}
		                }
		            }
		        }
			};
	    
		return Ext.apply(search,config);
	},
	/**创建下拉组件*/
	createCombo:function(config){
		var me = this,
			combo = {
				xtype:'combo',
				margin:'1 1 1 4',
				displayField:'text',
				valueField:'value',
				editable:false,
				store:new Ext.data.SimpleStore({
					fields:['text','value'],
					data:config.data
				}),
				listeners:{change:function(){me.onSearch();}}
			};
			
		return Ext.applyIf(combo,config);
	},
	/**创建日期区域*/
	createDatearea:function(config){
		
	},
	/**按钮事件处理*/
	onButtonClick:function(but){
		var me = this;
		me.fireEvent('click',but,but.itemId);
	},
	/**执行查询*/
	onSearch:function(){
		var me = this,
			search = me.getComponent(me.searchTextItemId);
			
		if(!search) return;
		
		var value = search.getValue();
		value = me.getWhere(value);
		me.fireEvent('search',me,search,value);
	},
	/**获取条件*/
	getWhere:function(value){
		var me = this,
			type = Ext.typeOf(me.searchFields),
			fields = type == 'string' ? me.searchFields.split(',') : type == 'array' ? me.searchFields : [],//查询的字段
			length = fields.length,
			combowhere = me.getComboWhere(),
	    	where = '';
	    	
	    if(!value && !combowhere) return;
	    
	    if(fields.length == 0 && !combowhere) return;
	    
	    if(value){
	    	for(var i=0;i<length;i++){
				where += fields[i] + " like '%" + value + "%' or ";
			}
			where = where.slice(-4) == ' or ' ? where.slice(0,-4) : "";
	    }
	    
		if(combowhere){
			if(where){
				where = "(" + where + ") and (" + combowhere + ")";
			}else{
				where = combowhere;
			}
		}
		
		return where == "" ? "" : "(" + where + ")";
	},
	/**获取下拉框的条件*/
	getComboWhere:function(){
		var me = this,
			fields = me._combofields || [],
			length = fields.length,
			whereArr = [];
			
		for(var i=0;i<length;i++){
			var value = fields[i].getValue();
			if(value != null){
				whereArr.push(fields[i].searchField + "='" + value + "'");
			}
		}
		
		return whereArr.join(' and ');
	},
	/**确定所有下拉框组件*/
	initComboFields:function(items){
		var me = this,
			length = items.length || 0,
			fields = [];
			
		for(var i=0;i<length;i++){
			var item = items[i];
			if(item.xtype === 'combo'){
				me._combofields.push(item);
			}
		}
	},
	boxIsReady:function(){}
});