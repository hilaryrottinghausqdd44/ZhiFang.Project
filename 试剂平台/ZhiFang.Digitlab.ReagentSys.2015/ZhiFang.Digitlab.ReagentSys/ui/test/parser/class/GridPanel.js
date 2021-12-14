/**
 * 构建列表基础类
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.GridPanel',{
	extend:'Ext.grid.Panel',
	alias:'widget.zhifangux_gridpanel',
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
		//加载数据后默认选中一行
		me.store.on({
			load:function(store,records,successful){
				me.setModuleOperCookie("");//清空cookie中的操作ID
				var autoSelect=me.autoSelect;
				if(!successful){
					alertError('获取数据服务错误!');
				}
				if(successful&&records.length>0){
					var num=0;//需要选中的行号
					if(me.deleteIndex&&me.deleteIndex!=''&&me.deleteIndex!=-1){
						//选中删除下标的那一行或者最后一行
						num = (records.length-1 > me.deleteIndex) ? me.deleteIndex : records.length-1;
					}else{
						if(autoSelect){
							if(autoSelect === true){
								num=0;
							}else if(Ext.typeOf(autoSelect) === 'string' && autoSelect.length==19){//需要选中的行主键
								var index=store.find(me.objectName+'_Id',autoSelect);
	                			if(index!=-1){num=index;}
							}else if(Ext.typeOf(autoSelect) === 'number'){//需要选中的行号
								if(autoSelect >= 0){
		                			num=autoSelect%records.length;
		                		}else{
		                			num=length-Math.abs(num)%length;
		                		}
							}
						}
					}
					//还原参数
					me.deleteIndex=-1;
					me.autoSelect=true;
					//选中行号为num的数据行
					me.getSelectionModel().select(num);
					//me.view.getEl().focus(); //定位光标
                }
			}
		});
        if(Ext.typeOf(me.callback)==='function'){me.callback(me);}
        me.defaultLoad && me.load(true);
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
		var w = '';
		if(me.externalWhere && me.externalWhere != ''){
			if(me.externalWhere.slice(-1) == '^'){
				w += me.externalWhere;
			}else{
				w += me.externalWhere+' and ';
			}
		}
		if(me.defaultWhere && me.defaultWhere != ''){
			w += me.defaultWhere+' and ';
		}
		
		if(me.internalWhere && me.internalWhere != ''){
			w += me.internalWhere + ' and ';
		}
		w = w.slice(-5) == ' and ' ? w.slice(0,-5) : w;
		me.store.currentPage = 1;
		me.store.proxy.url = me.url + '&where=' + w;
		me.store.load();
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
    	if(!data.success){
    		alertError(data.ErrorInfo);
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
	 * 创建爱你数据集
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
	 * 查询
	 * @private
	 */
	search:function(){
		var me = this;
		var toolbar=me.getComponent('buttonstoolbar');
		//存在功能按钮栏
		if(toolbar){
			var searchText = toolbar.getComponent('searchText');
			if(searchText){//查询功能存在
				var value = searchText.getValue();
				var where='';
				var searchArray = me.searchArray;//查询的字段
				if(value&&value!=''){
					for(var i in searchArray){
						if(searchArray[i].slice(-2) == 'Id'){
							where += searchArray[i] + "=" + value + " or ";
						}else{
							where += searchArray[i] + " like '%25" + value + "%25' or ";
						}
					}
					where = where.length > 0 ? "(" + where.slice(0,-4) + ");" : "";
				}
				me.internalWhere=where;
				me.load(true);
			}
		}
	},
	/**
	 * 显示总条数
	 * @private
	 * @param {} count
	 */
	setCount:function(count){
		var me = this;
		var bottomtoolbar = me.getComponent('toolbar-bottom');
		if(bottomtoolbar){
			var com = bottomtoolbar.getComponent('count');
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
		var store = [];
		if(sortconfig){
			store = me.getSortColumns();
		}else{
			store = me.defaultColumns || [];
		}
		return store;
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
	}
});