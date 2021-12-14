/**
 * 构建列表基础类
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.GridPanel',{
	extend:'Ext.grid.Panel',
	type:'gridpanel',
	alias:'widget.zhifanguxgridpanel',
	/**默认hql*/
	defaultWhere:'',
	/**内部hql*/
	internalWhere:'',
	/**外部hql*/
	externalWhere:'',
	/**默认选中的数据行,可以是true(选中)、false(不选)、数字(下标)[正数从上往下、负数从下往上,例如-1就是选中length-1行数据]、字符串(主键ID)*/
	autoSelect:true,
	/**被删除的行下标号*/
	deleteIndex:-1,
	/**默认加载数据*/
	defaultLoad:false,
	/**
	 * 空数据显示
	 * @type String
	 */
	emptyText:'没有找到数据',
	/**
	 * 排序属性对象,为以后列表显示重新排序做开口,
	 * 例如{Id:{sort:1},Name:{sort:2}}
	 * @type 
	 */
	sortconfig:null,
	/**
	 * 默认的数据列
	 * @type 
	 */
	defaultColumns:[],
	/**
	 * 渲染完后处理
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//数据集监听
		me.store.on({
		    beforeload:function(store,operation){me.beforeLoad(store,operation);},
		    load:function(store,records,successful,eOpts){me.afterLoad(store,records,successful,eOpts);}
		});
        
       	//默认加载数据
		if(me.defaultLoad){
			me.load(true);
		}else{
			me.disableControl();//禁用所有的操作功能
		}
		
		if(Ext.typeOf(me.callback)==='function'){me.callback(me);}
	},
	
	/**
	 * 根据where条件加载数据
	 * @public
	 * @param {} where
	 */
	load:function(where){
		var me = this;
		if(me.loaddata){
			me.setModuleOperCookie(me.loaddata);//设置cookie中的操作ID
		}
		if(where !== true){
			me.externalWhere = where;
		}
		me.internalWhere = me.getInternalWhere();
		
		me.store.currentPage = 1;
		me.store.proxy.url = me.getLoadUrl();
		me.store.load();
		me.setModuleOperCookie("");//清空cookie中的操作ID
	},
	/**
	 * 数据转化
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeData:function(response,bo){
		var me = this;
		var data = Ext.JSON.decode(response.responseText);
		var success = (data.success + "" == "true" ? true : false);
    	if(!success){
    		me.showError(data.ErrorInfo);
    	}
    	if(data.ResultDataValue && data.ResultDataValue != ''){
    		data.ResultDataValue =data.ResultDataValue.replace(/[\r\n]+/g,"");
    		var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	    	data.list = ResultDataValue.list;
	    	data.count = ResultDataValue.count;
    	}else{
    		data.list = [];
    		data.count = 0;
    	}
    	response.responseText = Ext.JSON.encode(data);;
    	bo && me.setCount(data.count);;//总条数数值赋值
    	return response;
	},
	/**
	 * 创建数据集
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createStore:function(config){
		var me = this;
		var cfg = {
			fields:config.fields,
			remoteSort:config.remoteSort,
			sorters:config.sorters,
			pageSize:config.PageSize,
			proxy:{
	            type:'ajax',
	            url:getRootPath() + '/' + config.url,
	            reader:{
	            	type:'json',
	            	root:'list',
	            	totalProperty:'count'
	            },
	            //内部数据匹配方法
	            extractResponseData:function(response){
			    	return me.changeData(response,config.hasCountToolbar); 
			  	}
	        },
	        onBeforeSort:function(){
	        	if(me.loaddata){
					me.setModuleOperCookie(me.loaddata);//设置cookie中的操作ID
				}
		        var groupers = this.groupers;
		        if (groupers.getCount() > 0) {
		            this.sort(groupers.items, 'prepend', false);
		        }
		    },
		    loadPage:function(page,options){
		    	if(me.loaddata){
					me.setModuleOperCookie(me.loaddata);//设置cookie中的操作ID
				}
	        	//条件处理
	        	this.proxy.url = me.getLoadUrl();
				//原组件的代码
		        this.currentPage = page;
		        // Copy options into a new object so as not to mutate passed in objects
		        options = Ext.apply({
		            page: page,
		            start: (page - 1) * this.pageSize,
		            limit: this.pageSize,
		            addRecords: !this.clearOnPageLoad
		        }, options);
		
		        if (this.buffered) {
		            return this.loadToPrefetch(options);
		        }
		        this.read(options);
	        }
		};
		if(config.buffered){
			cfg.buffered = config.buffered;
			cfg.leadingBufferZone = config.leadingBufferZone || config.PageSize;
		}
		
		var store = Ext.create('Ext.data.Store',cfg);
		return store;
	},
	/**
     * 获取带查询参数的URL
     * @private
     * @return {}
     */
    getLoadUrl:function(){
    	var me = this;
		var w = '';
		if(me.externalWhere && me.externalWhere != ''){
			if(me.externalWhere.slice(-1) == '^'){
				w += me.externalWhere;
			}else{
				w += '(' + me.externalWhere + ') and ';
			}
		}
		if(me.defaultWhere && me.defaultWhere != ''){
			w += '(' + me.defaultWhere.replace(/\%25/g,"%").replace(/\%27/g,"'") +') and ';
		}
		
		if(me.internalWhere && me.internalWhere != ''){
			w += '(' + me.internalWhere + ') and ';
		}
		w = w.slice(-5) == ' and ' ? w.slice(0,-5) : w;
		
		return me.url + '&where=' + encodeString(w);
    },
	/**
	 * 查询
	 * @private
	 */
	search:function(){
		this.load(true);
	},
	/**
	 * 显示总条数
	 * @private
	 * @param {} count
	 */
	setCount:function(count){
		var me = this;
		var pagingtoolbar = me.getComponent('pagingtoolbar');
		if(pagingtoolbar){
			var com = pagingtoolbar.getComponent('count');
			if(com){
				var str = '共'+count+'条';
				com.setText(str,false);
			}
		}
	},
	/**
	 * 创建数据列
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
		var sortconfig = me.sortconfig;
		var columns = [];
		if(sortconfig){
			columns = me.getSortColumns();
		}else{
			columns = me.defaultColumns || [];
		}
		for(var i in columns){
			columns[i].doSort = function(state){
		        var ds = this.up('tablepanel').store;
		        me.defaultLoad && ds.sort({
		            property:this.getSortParam(),
		            direction:state
		        });
		    };
		    if(columns[i].xtype == 'booleancolumn'){
		    	columns[i].renderer = function(value,meta,record){
	                var v = '';
					if(value === null || value === undefined){
						v = '';
					}else if(value === false || value === 'false' || value === 0 || value === '0'){
						v = '<span style="color:red;">否</span>';
					}else{
						v = '<span style="color:green;">是</span>';
					}
					return v;
			    };
		    }else if(!columns[i].xtype){
		    	columns[i].renderer = function(value,meta,record){
	                if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
	                return value;
			    };
		    }
		    
		    if(columns[i].HeadFont){
		    	columns[i].text = '<span style="' + columns[i].HeadFont + '">' + columns[i].text + '</span>';
		    }
		}
		return columns;
	},
	/**
	 * 获取排完序的列属性
	 * @private
	 * @return {}
	 */
	getSortColumns:function(){
		var me = this;
		var sortconfig = me.sortconfig;
		var defaultColumns = me.defaultColumns;
		
		if(sortconfig){
			for(var i in defaultColumns){
				defaultColumns['OrderNum'] = sortconfig[defaultColumns[i]['dataIndex']]['sort'];
			}
		}
		for(var i in defaultColumns){
			var kv = {OrderNum:defaultColumns[i]['OrderNum'],Index:i};
			map.push(kv);
		}
		
		var map = [];
		for(var i=0;i<map.length-1;i++){
			for(var j=i+1;j<map.length;j++){
				if(map[i].OrderNum > map[j].OrderNum){
					var temp = map[i];
					map[i] = map[j];
					map[j] = temp;
				}
			}
		}
		
		var list = [];
		for(var i in map){
	        list.push(defaultColumns[map[i].Index]);
		}
		return list;
	},
	/**
	 * 打开弹出窗口
	 * @private
	 * @param {} type
	 * @param {} id
	 * @param {} record
	 */
	openFormWin:function(type,id,record){
		var me = this;
		var appId = me.openFormId || "";
		if(appId == ''){
			alertError('没有配置弹出页面！');
		}else{
			var callback=function(info){
				if(info.success){
					var appInfo = info.appInfo;
					if(appInfo && appInfo != ''){
						var ClassCode = appInfo['BTDAppComponents_ClassCode'];
						if(ClassCode && ClassCode!=''){
							me.showFormWin(ClassCode,type,id,record);
						}else{
							alertError('没有类代码！');
						}
					}
				}else{
					alertError(info.ErrorInfo);
				}
			};
			getAppInfo(appId,callback,"BTDAppComponents_ClassCode");
		}
	},
	/**
	 * 弹出表单窗口
	 * @private
	 * @param {} ClassCode
	 * @param {} type
	 * @param {} id
	 * @param {} record
	 */
	showFormWin:function(ClassCode,type,id,record){
		var me = this;
		var maxHeight = document.body.clientHeight*0.98 - 80;
		var maxWidth = document.body.clientWidth*0.98;
		var panelParams = {
			type:type,
			maxWidth:maxWidth,
			dataId:id,
            selectionRecord:record,//自定义属性:修改,查看时选择中的行记录信息(以提供还原表单里的树节点)
			modal:true,//模态
			floating:true,//漂浮
			closable:true,//有关闭按钮
			resizable:true,//可变大小
			draggable:true//可移动
		};
		ClassCode = ClassCode.replace(/@@/g,"\\\'");
		var Class = eval(ClassCode);
		var panel = Ext.create(Class,panelParams);
		if(panel.height > maxHeight){panel.height = maxHeight;}
		panel.show();
		panel.on({
			saveClick:function(){
				panel.close();
				me.load(true);
				me.fireEvent('saveClick');
			}
		});
		if(type == 'add'){
			me.fireEvent('afterOpenAddWin',panel);
		}else if(type == 'edit'){
			me.fireEvent('afterOpenEditWin',panel);
		}else if(type == 'show'){
			me.fireEvent('afterOpenShowWin',panel);
		}
	},
	/**
	 * 设置cookie中的操作ID
	 * @private
	 * @param {} value
	 */
	setModuleOperCookie:function(value){
		var v = value || "";
		Ext.util.Cookies.set('000660',v);//模块操作ID
	},
	/**
	 * 显示错误信息
	 * @private
	 * @param {} value
	 */
	showError:function(value){
		var me = this;
		var html = "<center><b style='color:red;font-size:x-large'>" + value + "</b></center>";
		me.getView().update(html);
	},
	/**
	 * 获取内部条件
	 * @private
	 * @return {}
	 */
	getInternalWhere:function(){
		var me = this,
	    	toolbar=me.getComponent('buttonstoolbar');
	    
	    var where = '';
		//存在功能按钮栏
		if(toolbar){
			var searchText = toolbar.getComponent('searchText');
			if(searchText){//查询功能存在
				var value = searchText.getValue();
				
				var searchArray = me.searchArray;//查询的字段
				if(value&&value!=''){
					for(var i in searchArray){
						if(searchArray[i].slice(-2) == 'Id'){
							where += searchArray[i] + "=" + value + " or ";
						}else{
							where += searchArray[i] + " like '%" + value + "%' or ";
						}
					}
					where = where.length > 0 ? "(" + where.slice(0,-4) + ");" : "";
				}
			}
		}
		return where;
	},
	/**
  	 * 加载数据前
  	 * @private
  	 * @param {} store
  	 * @param {} operation
  	 */
  	beforeLoad:function(store,operation){
  		var me = this;
  		me.disableControl();//禁用 所有的操作功能
  	},
  	/**
  	 * 加载数据后
  	 * @private
  	 * @param {} store
  	 * @param {} records
  	 * @param {} successful
  	 * @param {} eOpts
  	 */
  	afterLoad:function(store,records,successful,eOpts){
		var me = this,
			autoSelect = me.autoSelect;
			
  		me.enableControl();//启用所有的操作功能
		//me.setModuleOperCookie("");//清空cookie中的操作ID
		
		if(successful && records.length > 0){
			var num=-1;//需要选中的行号
			if(me.deleteIndex && me.deleteIndex != '' && me.deleteIndex != -1){
				//选中删除下标的那一行或者最后一行
				num = (records.length-1 > me.deleteIndex) ? me.deleteIndex : records.length-1;
			}else{
				if(autoSelect){
					if(autoSelect === true){
						num=0;
					}else if(Ext.typeOf(autoSelect) === 'string'){//需要选中的行主键
						num = me.store.find(me.objectName+'_Id',autoSelect);
					}else if(Ext.typeOf(autoSelect) === 'number'){//需要选中的行号
						if(autoSelect >= 0){
                			num = autoSelect % records.length;
                		}else{
                			num = length - Math.abs(num) % length;
                		}
					}
				}
			}
			//还原参数
			me.deleteIndex = -1;
			//选中行号为num的数据行
			if(num >= 0){
				Shell.util.Action.delay(function(){
					me.getSelectionModel().select(num);
				},500);
			}
        }
  	},
  	/**
	 * 启用所有的操作功能
	 * @private
	 */
	enableControl:function(){
		var me = this,
			items = [],
			buttonstoolbar = me.getComponent('buttonstoolbar');
			pagingtoolbar = me.getComponent('pagingtoolbar');
		
		if(buttonstoolbar){
			items = items.concat(buttonstoolbar.items.items);
		}
		if(pagingtoolbar){
			items = items.concat(pagingtoolbar.items.items);
		}
		
		for(var i in items){
			items[i].enable();
		}
		me.defaultLoad = true;
	},
	/**
	 * 禁用所有的操作功能
	 * @private
	 */
	disableControl:function(){
		var me = this,
			items = [],
			buttonstoolbar = me.getComponent('buttonstoolbar');
			pagingtoolbar = me.getComponent('pagingtoolbar');
		
		if(buttonstoolbar){
			items = items.concat(buttonstoolbar.items.items);
		}
		if(pagingtoolbar){
			items = items.concat(pagingtoolbar.items.items);
		}
		
		for(var i in items){
			items[i].disable();
		}
		me.defaultLoad = false;
	}
});