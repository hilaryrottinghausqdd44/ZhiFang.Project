/**
 * 信息面板类
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.panel.InfoForm',{
	extend:'Shell.ux.form.Panel',
	alias:'widget.uxinfoform',
	
	bodyPadding:5,
	
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'show',
	/**主键字段*/
	PKField:'Id',
	/**内容主键*/
	PK:null,
	/**获取数据服务*/
	searchUrl:'',
	/**新增数据服务*/
	addUrl:'',
	/**修改数据服务*/
	editUrl:'',
	
	/**开启加载数据遮罩层*/
	hasLoadMask:true,
	/**加载数据提示*/
	loadingText:'数据加载中...',
	
	/**请求头信息*/
	defaultPostHeader:null,
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//监听
		me.on({
			boxready:function(){me.initInfo();},
			expand:function(p,d){
				if(me.isCollapsed){me.load(me.PK);}
				me.isCollapsed = false;
			}
		});
	},
	/**初始化信息数据*/
	initInfo:function(){
		var me = this,
			type = me.formtype,
			id = me.PK;
		
		if(type == 'add'){
            me.infoAdd();
        }else if(type == 'edit'){
            if(me.PK){me.infoEdit(id);}
        }else if(type == 'show'){
             if(me.PK){me.infoShow(id);}
        }
	},
	/**重写初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('load','save');
		me.defaultTitle = me.title;
		me.initPKField();//初始化主键字段
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
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
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			tool = [{dock:'bottom',buttons:['->','save','reset']}];
			
		if(me.floating){tool[0].buttons.push('cancel');};
			
		var toolbars = me.toolbars || tool,
			length = toolbars.length,
			dockedItems = [];
		
		for(var i=0;i<length;i++){
			dockedItems.push({
				autoScroll:true,
				dock:toolbars[i].dock || 'top',
				xtype:'uxbuttonstoolbar',
				buttons:toolbars[i].buttons,
				listeners:{
					click:function(but,type){
						me.onButtonClick(but,type);
					}
				}
			});
		}
			
		return dockedItems;
	},
	/**重写保存处理*/
	onSaveClick:function(but){
		var me = this,
			url = Shell.util.Path.rootPath + (me.formtype == 'add' ? me.addUrl : me.editUrl),
			params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
			
		if(!me.isValid()) return;
		
		params = Ext.JSON.encode(params);
			
		me.postToServer(url,params,function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				me.fireEvent('save',me);
			}else{
				me.showError(result.ErrorInfo);
			}
		});
	},
	/**获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getValues(),
			arr;
			
		for(var i in values){
			arr = i.split('_');
			if(arr.slice(-1) == 'DataTimeStamp' && arr.length == 2){
				delete values[i];//消除本身的时间戳
			}
		}
		
		values[me.PKField] = -1;
			
		var entity = Shell.util.Object.toStereo(values),
			params = {entity:entity};
			
		return params;
	},
	/**获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getValues(),
			arr;
			
		for(var i in values){
			arr = i.split('_');
			if(arr[arr,length-1] == 'DataTimeStamp' && arr.length > 2){
				delete values[i];//消除外键的时间戳
			}
		}
		
		var entity = Shell.util.Object.toStereo(values),
			params = {
				entity:entity,
				fields:me.getEditFields()
			};
			
		return params;
	},
	/**获取需要提交的修改字段*/
	getEditFields:function(){
		var me = this,
			fields = me._thisfields || [],
			length = fields.length,
			fieldsNameArr = [];
			
		for(var i=0;i<length;i++){
			if(fields[i].xtype != 'displayfield'){
				fieldsNameArr.push(fields[i].itemId.split('_').slice(1));
			}
		}
		
		return fieldsNameArr.join(',');
	},
	/**重写重置按钮处理*/
	onResetClick:function(but){
		var me = this,
			type = me.formtype;
			
		if(type == 'edit'){
			me.load(me.PK);
		}else{
			me.infoAdd();
		}
	},
	onCancelClick:function(but){
		this.fireEvent('cancel',this);
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this,
			type = me.formtype;
			
		if(type == 'add'){
    		me.setTitle(me.defaultTitle + '-新增'); 
    	}else if(type == 'edit'){
    		me.setTitle(me.defaultTitle + '-修改'); 
    	}else if(type == 'show'){
    		me.setTitle(me.defaultTitle + '-查看'); 
    	}else{
    		me.setTitle(me.defaultTitle);
    	}
	},
	/**重置表单数据*/
	reset:function(){
		var me = this,
		form = me.getForm();
		form.reset();
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
    
	/**加载数据*/
	load:function(id){
		var me = this,
			url = me.searchUrl,
			collapsed = me.getCollapsed();
			
		if(!id) return;
		
		me.PK = id;//面板主键
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.changeTitle();//标题更改
    	
    	if(me.hasLoadMask){me.body.mask(me.loadingText);}//显示遮罩层
    	url = Shell.util.Path.rootPath + url;
    	url += (url.indexOf('?') == -1 ? "?" : "&" ) + "id=" + id;
    	
    	me.getToServer(url,function(text){
    		var result = me.responseTextToForm(text);
    		if(result.success){
    			if(result.values){
    				me.clearData();
                    me.setValues(result.values);
                }
    		}else{
    			me.showError(result.ErrorInfo);
    		}
    		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
    		me.fireEvent('load',me);
    	},false);
	},
	/**@public 新增信息*/
	infoAdd:function(){
		var me = this;
		me.formtype='add';
        me.changeTitle();
        
        me.enableControl();
        me.setReadOnly(false);
        me.reset();
    },
    /**@public 修改信息*/
    infoEdit:function(id){
    	var me = this;
    	me.formtype='edit';
    	me.changeTitle();
    	
        me.enableControl();
        me.setReadOnly(false);
       	me.load(id);
    },
    /**@public 查看信息*/
    infoShow:function(id){
        var me = this;
        me.formtype='show';
        me.changeTitle();
        
        me.disableControl();
        me.setReadOnly(true);
        me.load(id);
    },
    /**@public 清空数据,禁用功能按钮*/
    clearData:function(){
    	var me = this;
    	me.disableControl();//禁用 所有的操作功能
    	me.reset();//还原数据
    }
});