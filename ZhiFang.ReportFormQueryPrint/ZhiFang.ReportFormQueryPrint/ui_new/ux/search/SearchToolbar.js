/**
 * 查询组件
 * @author Jcall
 * @version 2014-07-24
 */
Ext.define('Shell.ux.search.SearchToolbar',{
	extend:'Ext.toolbar.Toolbar',
	alias:'widget.uxsearchtoolbar',
	
	requires:[
		'Shell.ux.Button',
		'Shell.ux.form.field.DateArea'
	],
	
	mixins:['Shell.ux.server.Ajax'],
	
	title: '查询框',
    /**收缩*/
    headCollapsed:false,
	
	/**帮助按钮*/
	help:null,
	
	/**内部有name的组件*/
	_fields:{},
	
    /*是否区分大小写*/
    isCaseSensitive:false,
    
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initFields();
		//视图准备完毕
		me.on({
			boxready:function(){me.boxIsReady();}
		});
		if (me.headCollapsed) { me.collapse(); }
		
	},
	
	initComponent:function(){
		var me = this;
		me.layout = 'vbox';
		me.padding = 1;
		me.margin = '0 0 4 0';
		me.split = false;
		
		me.addEvents('search');
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this,
			config = me.items || [],
			length = config.length,
			items = [],
			type = null;
			
		if(length == 0) return []; 	
		
		if(Ext.typeOf(config[0]) === 'object'){
			var container = me.createContainer(config);
			items.push(container);
		}
			
		if(Ext.typeOf(config[0]) === 'array'){
			for(var i=0;i<length;i++){
				var container = me.createContainer(config[i]);
				items.push(container);
			}
		}
		
		//收缩按钮
		if(me.collapsible){
			me.collapsible = false;
			me.header = me.createHeader();
			items.unshift(me.header);
		}
		
		return items;
	},
	/**
	 * 创建标题栏
	 * @private
	 * @return {}
	 */
	createHeader:function(){
		var me = this,
			headerTextStyle = 'color:#04408c;font-size:12px;font-weight:bold;' +
					'font-family:tahoma,arial,verdana,sans-serif;line-height:17px;';
		
		var header = new Ext.container.Container({
			type:'header',
			width:'100%',
			layout:{type:'hbox',pack:'end'},
			items:[{
				xtype:'label',padding:'0 5 0 5',
				text:me.title,
				//uiCls:['x-panel-header-text']
				style:headerTextStyle
			},{
				xtype:'tbfill',
				height:0
			}]
		});
		
		if (me.toolButtons) {
		    for (var i in me.toolButtons) {
		        header.add(new Ext.panel.Tool(me.toolButtons[i]));
		    }
		}

		if(me.help){
			header.helpbutton = new Ext.panel.Tool(Ext.apply({
				type:'help',tooltip:'<b>帮助</b>',
				handler:function(){me.onHelpClick(this);}
			},me.help));
			header.add(header.helpbutton);
		}
		
		header.upbutton = new Ext.panel.Tool({
			type:'up',
			handler:function(){me.collapse();},
			hidden: me.headCollapsed ? true : false
		});
		
		header.downbutton = new Ext.panel.Tool({
			type:'down',
			handler:function(){me.expand();},
			hidden: me.headCollapsed ? false : true
		});
		
		header.add(header.upbutton,header.downbutton);
		
		return header;
	},
	/**
	 * 创建一行按钮
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createContainer:function(con){
		var me = this,
			config = Ext.clone(con);
			
		for(var i in config){
			config[i] = me.applyConfig(config[i]);
			if(config[i].text){config[i].text = "<b>" + config[i].text + "</b>"}
			if(config[i].tooltip){config[i].tooltip = "<b>" + config[i].tooltip + "</b>"}
		}
		var container = {
			xtype:'container',
			layout:'hbox',
			items:config
		};
		return container;
	},
	/**
	 * 适配合适的组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	applyConfig:function(config){
		var me = this;
		
		if(config.type == 'search'){
			var xtype = config.xtype;
			switch(xtype){
			    case 'combo': return me.applyCombo(config);
			    case 'cbox': return me.applyCombo(config);
				case 'textfield': return me.applyTextfield(config);
				case 'numberfield': return me.applynumberfield(config);
			    case 'uxdatearea': return me.applyDatearea(config);
			    case 'checkbox': return config;
			}
		}else if(config.type == 'searchbut'){
			return me.applySearch(config);
		} else if (config.type == 'other') {
		    return config;
		}else{
			return me.applyButton(config);
		}
	},
	/**
	 * 适配按钮(分组查询按钮)
	 * @private
	 */
	applyButton:function(config){
		var me = this;
		
		return Ext.applyIf(config,{
			xtype:'uxbutton',
			margin:2,
			iconCls:'button-search',
			handler:function(but){me.onGroupSearch(but);}
		});
	},
	/**
	 * 适配查询按钮(一般查询)
	 * @private
	 * @return {}
	 */
	applySearch:function(config){
		var me = this;
		config.xtype = 'uxbutton';
		
		return Ext.applyIf(config,{
			margin:'1 1 1 4',
			text:'查询',
			iconCls:'button-search',
			handler:function(but){me.onSearch(but);}
		});
	},
	/**
	 * 适配下拉框(一般查询)
	 * @private
	 * @return {}
	 */
	applyCombo:function(config){
		var me = this,
			store = null;
			
		if(Ext.typeOf(config.data) === 'array'){
			store = new Ext.data.SimpleStore({
				fields:['text','value'],
				data:config.data
			});
		}else if(Ext.typeOf(config.url) === 'string'){
			store = new Ext.data.Store({
				fields:[config.displayField,config.valueField],
    			pageSize:5000,
				proxy:{
					type:'ajax',
					url:Shell.util.Path.rootPath + config.url,
					reader:{type:'json',totalProperty:'count',root:'list'},
					extractResponseData:me.responseToList
				}
			});
		}
		
		return Ext.applyIf(config,{
			xtype:'combo',
			margin:'1 1 1 4',
			displayField:'text',
			valueField:'value',
			editable:false,
			store:store,
			labelAlign:'right'
		});
	},
	/**
	 * 适配输入框
	 * @private
	 * @param {} config
	 * @return {}
	 */
	applyTextfield:function(config){
		return Ext.applyIf(config,{
			xtype:'textfield',
			margin:'1 1 1 4',
			labelAlign:'right'
		});
	},
	/**
	 * 适配输入框
	 * @private
	 * @param {} config
	 * @return {}
	 */
	applynumberfield:function(config){
		return Ext.applyIf(config,{
			xtype:'numberfield',
			margin:'1 1 1 4',
			labelAlign:'right'
		});
	},
	/**创建日期区域*/
	applyDatearea:function(config){
		return Ext.applyIf(config,{
			xtype:'uxdatearea',
			margin:'1 1 1 4',
			labelAlign:'right'
		});
	},
	/**
	 * 一般查询按钮
	 * @private
	 * @param {} but
	 */
	onSearch:function(but){
		var me = this,
			where = me.getWhere();
			
		if(where == null) return;
			
		where = where ? '(' + where + ')' : '';
		me.fireEvent('search',me,where);
	},
	/**
	 * 分组查询按钮
	 * @private
	 * @param {} but
	 */
	onGroupSearch:function(but){
		var me = this,
			where = but.where || '',
			where2 = me.getWhere(),
			arr = [];
			
		if(where2 == null) return;
			
		if(where){arr.push("(" + where + ")");}
		if(where2){arr.push("(" + where2 + ")");}
			
		where = arr.join(" and ");
		me.fireEvent('search',me,where);
	},
	/**
	 * 获取一般查询where条件
	 * @public
	 * @return {}
	 */
	getWhere:function(){
		var me = this,
			values = me.getValues(),
			where = [];
		
		if(!values) return null;
			
		for(var i in values){
			if(values[i]){where.push(values[i]);}
		}
			
		where = where.join(' and ');
		return where;
	},
	/**
	 * 获取一般查询的所有值
	 * @private
	 * @return {}
	 */
	getValues:function(){
		var me = this,
			items = me.items.items,
			length = items.length,
			values = {};
		
		for (var i = 0; i < length; i++) {
			if(items[i].type == 'header') continue;

			var fields = items[i].items.items,
				fLen = fields.length;
				
			for(var j=0;j<fLen;j++){
				var field = fields[j];
				if(field.xtype == 'uxbutton') continue;
				if (field.type == 'other') continue;
				if(!field.isValid()) return null;//不符合校验
					
				var value = field.getValue(true);
				if(value != null && value != ''){
					var xtype = field.xtype;
					switch(xtype){
						case 'textfield' : values[field.name] = me.getTextfieldWhere(field,value);break;
						case 'numberfield' : values[field.name] = me.getTextfieldWhere(field,value);break;
						case 'combo' : values[field.name] = me.getComboWhere(field,value);break;
						case 'uxdatearea' : values[field.name] = me.getDateareaWhere(field,value);break;
					}
				}else{
					values[field.name] = value;
				}
			}
		}
		return values;
	},
	/**文本框条件*/
	getTextfieldWhere:function(field,value){
		var me = this;
		var mark = field.mark || "like";
		if(mark == "like"){
			return field.name + " like '%" + value + "%'";
		}
		if (mark == "in") {
		    return field.name + "  in " + value;
		}
		var textfield ="";
		if(me.isCaseSensitive){
			textfield = field.name + mark + "'" + value + "' collate Chinese_PRC_CS_AI_WS";
		}else{
			textfield = field.name + mark + "'" + value + "'";
		}
		return textfield;
	},
	/**下拉框条件*/
	getComboWhere:function(field,value){
		value = Ext.typeOf(value) == 'string' ? "'" + value + "'" : value;
		return field.name + '=' + value;
	},
	/**日期区间条件*/
	getDateareaWhere:function(field,value){
		var start = value.start,
			end = value.end,
			arr = [];
			
		if(start){
			arr.push(
				field.name + ">='" + Shell.util.Date.toString(start,true) + "'"
			);
		}
		if(end){
			arr.push(
				field.name + "<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(end),true) + "'"
			);
		}
			
		if(arr.length == 0) return null;
		if(arr.length == 1) return arr[0];
		if(arr.length == 2) return "(" + arr.join(" and ") + ")";
		return null;
	},
	
	/**
	 * 收缩
	 * @private
	 */
	collapse:function(){
		var me = this;
		me.showItems(false);
	},
	/**
	 * 展开
	 * @private
	 */
	expand:function(){
		var me = this;
		me.showItems(true);
	},
	/**
	 * 显示查询内容
	 * @private
	 * @param {} bo
	 */
	showItems:function(bo){
		var me = this,
			items = me.items.items,
			length = items.length;
		
		for(var i=0;i<length;i++){
			items[i][bo ? 'show' : 'hide']();
		}
		
		me.header.upbutton[bo ? 'show' : 'hide']();
		me.header.downbutton[bo ? 'hide' : 'show']();
		me.header.show();
	},
	boxIsReady:function(){},
	/**帮助按钮处理*/
	onHelpClick:function(){
		Shell.util.Msg.showOverrideInfo('onHelpClick');
	},
	
	/**记录内部带name的组件*/
	initFields:function(){
		var me = this,
			items = me.items.items,
			length = items.length,
			values = {};
		
		for(var i=0;i<length;i++){
			if(items[i].type == 'header') continue;
			
			var fields = items[i].items.items,
				fLen = fields.length;
				
			for(var j=0;j<fLen;j++){
				var field = fields[j];
				if(field.xtype == 'uxbutton') continue;
				
				if(field.name){
					me._fields[field.name] = field;
				}
			}
		}
	},
	
	/**根据name获取内部组件*/
	getFieldsByName:function(name){
		var me = this,
			fields = me._fields;
			
		if(!name) return null;
			
		for(var i in fields){
			if(i == name) return fields[i];
		}
		
		return null;
	}
});