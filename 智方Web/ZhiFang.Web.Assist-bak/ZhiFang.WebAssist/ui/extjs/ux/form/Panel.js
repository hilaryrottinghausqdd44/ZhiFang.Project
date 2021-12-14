/**
 * 基础表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.Panel',{
    extend:'Ext.form.Panel',
    alias:'widget.uxFormPanel',
	
	/**启用表单状态初始化*/
	openFormType:false,
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'show',
	/**主键字段*/
	PKField:'Id',
	/**数据主键*/
	PK:'',
	/**获取数据服务路径*/
    selectUrl:JShell.System.Path.DEFAULT_ERROR_URL,
	/**新增服务地址*/
    addUrl:JShell.System.Path.DEFAULT_ERROR_URL,
    /**修改服务地址*/
    editUrl:JShell.System.Path.DEFAULT_ERROR_URL,
	
	/**显示成功信息*/
	showSuccessInfo:true,
	/**开启加载数据遮罩层*/
	hasLoadMask:true,
	/**加载数据提示*/
	loadingText:JShell.Server.LOADING_TEXT,
	/**保存数据提示*/
	saveText:JShell.Server.SAVE_TEXT,
	/**内容自动显示*/
	autoScroll:true,
	/**内容周围距离*/
	//bodyPadding:10,
	/**布局方式*/
	layout:'absolute',
    /**默认组件*/
    defaultType:'textfield',
    /** 每个组件的默认属性*/
    defaults:{
    	width:200,
        labelWidth:60,
        labelAlign:'right'
    },
	/**消息框消失时间*/
	hideTimes: 500,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用保存按钮*/
	hasSave:false,
	/**是否启用另存按钮*/
	hasSaveas:false,
	/**是否重置按钮*/
	hasReset:false,
	/**是否启用取消按钮*/
	hasCancel:false,
	/**自定义按钮功能栏*/
	buttonToolbarItems:null,
	/**功能按钮栏位置*/
	buttonDock:'bottom',
	
	/**错误信息样式*/
	errorFormat:'<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**消息信息样式*/
	msgFormat:'<div style="color:blue;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//确定所有取值组件
		me.initValueFields(me.items.items);
		//监听
		me.on({
			boxready:function(){me.initInfo();},
			expand:function(p,d){
				if(me.isCollapsed){me.load(me.PK);}
				me.isCollapsed = false;
			}
		});
	},
	
	initComponent:function(){
		var me = this;
		me.addEvents('changeResult','load','save');
		me.defaultTitle = me.title;
		me.items = me.items || me.createItems();
		me._thisfields = [];
		me.initPKField();//初始化主键字段
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0){
			me.dockedItems = dockedItems;
		}
		
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var error = 'createItems ' + JShell.Msg.OVERWRITE;
		this.update(this.errorFormat.replace(/{msg}/,error));
		return [];
	},
	initValueFields:function(items){
		var me = this,
			len = items.length;
			
		for(var i=0;i<len;i++){
			var item = items[i];
			if(Ext.typeOf(item.getValue) == 'function'){
				me._thisfields.push(item);
			}else if(item.items && Ext.typeOf(item.items.items) == 'array'){
				me.initValueFields(item.items.items);
			}
		}
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this,
			items = me.dockedItems || [];
			
		if(me.hasButtontoolbar){
			var buttontoolbar = me.createButtontoolbar();
			if(buttontoolbar) items.push(buttontoolbar);
		}
		
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		
		if(items.length == 0){
			if(me.hasSave) items.push('save');
			if(me.hasSaveas) items.push('saveas');
			if(me.hasReset) items.push('reset');
			if(me.hasCancel) items.push('cancel');
			if(items.length > 0) items.unshift('->');
		}
		
		if(items.length == 0) return null;
		
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:hidden
		});
	},
	
	/**初始化信息数据*/
	initInfo:function(){
		var me = this,
			type = me.formtype,
			id = me.PK;
		
		if(type == 'add'){
            me.isAdd();
        }else if(type == 'edit'){
            if(me.PK){
            	me.isEdit(id);
            }
        }else if(type == 'show'){
			if(me.PK){
				me.isShow(id);
			}else{
            	me.setReadOnly(true);
            }
        }
	},
	/**初始化主键字段*/
	initPKField:function(){
		var me = this,
			items = me.items || [],
			length = items.length;
			
		for(var i=0;i<length;i++){
			if(items[i].type == 'key'){
				me.PKField = items[i].itemId;
				break;
			}
		}
	},
	/**根据主键ID加载数据*/
	load:function(id){
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
			
		if(!id) return;
		
		me.PK = id;//面板主键
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
    	
    	me.showMask(me.loadingText);//显示遮罩层
    	url = (url.slice(0,4) == 'http' ? '' : me.getPathRoot()) + url;
    	url += (url.indexOf('?') == -1 ? "?" : "&" ) + "id=" + id;
    	url += '&fields=' + me.getStoreFields().join(',');
    	
    	JShell.Server.get(url,function(data){
    		me.hideMask();//隐藏遮罩层
    		if(data.success){
    			if(data.value){
    				data.value = JShell.Server.Mapping(data.value);
    				me.lastData = me.changeResult(data.value);
                    me.getForm().setValues(data.value);
                }
    		}else{
    			JShell.Msg.error(data.msg);
    		}
    		me.fireEvent('load',me,data);
    	});
	},
	/**创建数据字段*/
	getStoreFields:function(){
		var me = this,
			items = me.items.items || [],
			len = items.length,
			fields = [];
			
		for(var i=0;i<len;i++){
			if(items[i].name && !items[i].IsnotField){
				fields.push(items[i].name);
			}
		}
		
		return fields;
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this,
			type = me.formtype;
			
		if(type == 'add'){
    		me.setTitle(me.defaultTitle + '-' + JShell.All.ADD); 
    	}else if(type == 'edit'){
    		me.setTitle(me.defaultTitle + '-' + JShell.All.EDIT); 
    	}else if(type == 'show'){
    		me.setTitle(me.defaultTitle + '-' + JShell.All.SHOW); 
    	}else{
    		me.setTitle(me.defaultTitle);
    	}
	},
	/**启用所有的操作功能*/
	enableControl:function(bo){
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];
		
		for(var i=0;i<length;i++){
			if(toolbars[i].xtype == 'header') continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}
		
		var iLength = items.length;
		for(var i=0;i<iLength;i++){
			items[i][enable ? 'enable' : 'disable']();
		}
		if(bo){me.defaultLoad = true;}
	},
	/**禁用所有的操作功能*/
	disableControl:function(){
		this.enableControl(false);
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		if(me.hasLoadMask){me.body.mask(text);}//显示遮罩层
    	me.disableControl();//禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
    	me.enableControl();//启用所有的操作功能
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : me.getPathRoot()) + url;
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value.id : id;
				id += '';
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		JShell.Msg.overwrite('getAddParams');
		return;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		JShell.Msg.overwrite('getEditParams');
		return;
	},
	/**@overwrite 另存按钮点击处理方法*/
	onSaveasClick:function(){
		JShell.Msg.overwrite('onSaveasClick');
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		if(!me.PK){
			me.getForm().reset();
		}else{
			me.getForm().setValues(me.lastData);
		}
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick:function(){
		//JShell.Msg.overwrite('onCancelClick');
		this.close();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	
	isAdd:function(){
		var me = this;
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar){
			buttonsToolbar.show();
		}
		
		me.setReadOnly(false);
		
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
	},
	isEdit:function(id){
		var me = this;
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar){
			buttonsToolbar.show();
		}
		
		me.setReadOnly(false);
		
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.load(id);
	},
	isShow:function(id){
		var me = this;
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar){
			buttonsToolbar.hide();
		}
		
		me.setReadOnly(true);
		
		me.formtype = 'show';
		me.changeTitle();//标题更改
		me.disableControl();
		me.load(id);
	},
	/**数据项是否只读处理*/
    setReadOnly:function(bo){
    	var me = this,
			fields = me._thisfields,
			length = fields.length,
			field;
			
		for(var i=0;i<length;i++){
			field = fields[i];
			if(!field.locked){
				if(Ext.typeOf(field.setReadOnly) == 'function'){
					field.setReadOnly(bo);
				}
			}
		}
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.getForm().reset();
		me.isShow();
	},
	/**获取ROOTURL*/
	getPathRoot:function(){
		return JShell.System.Path.ROOT;
	}
});