Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.FormPanel',{
	extend:'Ext.form.Panel',
	type:'formpanel',
	alias:'widget.zhifanguxformpanel',
	/**数据是否加载完成*/
	isLoadingComplete:true,
	/**默认弹出保存成功信息*/
	isSuccessMsg:true,
	/**自动产生滚动条*/
	autoScroll:true,
	/**绝对定位布局*/
	layout:'absolute',
	bodyPadding:20,
	/**表单的默认状态,add(新增)edit(修改)show(查看)*/
	type:'show',
	/**应用对象类代码字段*/
	classCode:'BTDAppComponents_ClassCode',
	/**
	 * 需要的数据字段
	 * @type String
	 */
	fields:'',
	/**默认加载数据时启用遮罩层*/
	hasLoadMask:true,
	/**
	 * 初始化表单基础公开事件
	 * @private
	 */
	initBasicEvents:function(){
		var me = this;
		me.addEvents('beforeSave');//用于做校验处理
		me.addEvents('changeValue');//用于对需要提交的数据进行处理
		me.addEvents('saveClick');//保存
		me.addEvents('default1buttonClick');//自定义一
        me.addEvents('default2buttonClick');//自定义二
        me.addEvents('default3buttonClick');//自定义三
	},
	/**
	 * 新增表单
	 * @public
	 */
	isAdd:function(){
		var me = this;
    	me.setTitle(me.defaultTitle+'-新增');
        me.hideButtons(false);
        me.type='add';
        me.setReadOnly(false);
        me.reset();
    },
    /**
     * 修改表单
     * @public
     * @param {} id
     * @param {} callback
     */
    isEdit:function(id,callback){
    	var me = this;
        me.hideButtons(false);
        me.type='edit';
        me.setReadOnly(false);
        if(id && id !=-1 && id != '-1'){me.load(id,callback);}
    },
    /**
     * 查看表单
     * @public
     * @param {} id
     * @param {} callback
     */
    isShow:function(id,callback){
        var me = this;
        me.hideButtons(true);
        me.type='show';
        me.setReadOnly(true);
        if(id && id != -1 & id != '-1'){me.load(id,callback);}
    },
    /**
     * 根据内部编号赋值
     * @public
     * @param {} key
     * @param {} value
     */
    setValueByItemId:function(key,value){
    	this.getForm().setValues([{id:key,value:value}]);
    },
    /**
     * 是否隐藏功能栏
     * @private
     * @param {} bo
     */
    hideButtons:function(bo){
    	var me = this;
    	var buts = me.getComponent('bottomtoolbar');
        if(buts){
        	if(bo){
        		me.setHeight(me.getHeight()-25);
            	buts.hide();
        	}else{
        		me.setHeight(me.getHeight()+25);
            	buts.show();
        	}
        }
    },
    /**
     * 数据项是否只读
     * @private
     * @param {} bo
     */
    setReadOnly:function(bo){
    	var me = this;
    	var items = me.items.items;//这只是一层，需要改进###
    	for(var i in items){
    		var item = items[i];
    		if(!item.hasReadOnly){
    			var type = item.type;
    			if(type == 'button' || type == 'label'){
                 	item.setDisabled(bo);
                }else if(type == 'radiogroup'){//单选组
                	item.store.readOnly = bo;
                	var arr = item.items.items;
                	if(Ext.typeOf(arr) === 'array' && arr.length > 0){
                		for(var o in arr){
	                		arr[o].setReadOnly(bo);
	                	}
                	}
                }else if(type == 'dataradiogroup'){//定值单选组
                	var arr = item.items.items;
                	if(Ext.typeOf(arr) === 'array' && arr.length > 0){
                		for(var o in arr){
	                		arr[o].setReadOnly(bo);
	                	}
                	}
                }else{ +
                	item.setReadOnly(bo);
                }
    		}
    	}
    },
    /**
     * 根据ID获取数据
     * @public
     * @param {} id
     * @param {} callback
     */
    load:function(id,callback){
    	var me = this;
        me.isLoadingComplete=false;
    
    	if(me.type == 'edit'){
    		me.setTitle(me.defaultTitle + '-修改'); 
    	}else if(me.type == 'show'){
    		me.setTitle(me.defaultTitle + '-查看'); 
    	}else{
    		me.setTitle(me.defaultTitle);
    	}
    	
    	var c = function(text){
    		var result=Ext.JSON.decode(text);
    		var info = {success:true,ErrorInfo:''};
    		if(result.success){
    			if(result.ResultDataValue&&result.ResultDataValue!=''){
    				if(me.type == 'add'){me.type = 'edit';}
                	result.ResultDataValue =result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    var values=Ext.JSON.decode(result.ResultDataValue);	
                    values=changeObj(values);//数据转换
                    me.setValues(values);
                    me.isLoadingComplete=true;
                    if(Ext.typeOf(callback) === 'function'){callback(me);}
                }
    		}else{
    			info.ErrorInfo = result.ErrorInfo;
    		}
    		if(me.hasLoadMask && me.mk){me.mk.hide();}//隐藏遮罩层
    		me.fireEvent('load',info);
    	};
    	if(me.hasLoadMask){
			me.mk = me.mk || new Ext.LoadMask(me.getEl(),{msg:'数据加载中...',removeMask:true});
			me.mk.show();//显示遮罩层
    	}
    	var url = getRootPath() + "/" + me.selectUrl + "?isPlanish=true&fields=" + me.fields + "&id=" + id;
    	getToServer(url,c);
    },
    /**
     * 保存表单信息
     * @private
     * @param {} but
     */
    submit:function(but){
        var me = this;
        
        var bo = me.fireEvent('beforeSave',me);//用于做校验处理
        if(!bo) return;
        
        if (!me.getForm().isValid()) return;
        
        var url = me.type == 'add' ? me.addUrl : me.editUrl;
        if(url == ''){alertInfo('没有配置服务!');return;};
        url = getRootPath() + "/" + url;
        
        var values2 = me.getForm().getFieldValues();
        
        //消掉子对象的非Id和时间戳字段的值
        for(var i in values2){
        	var arr = i.split('_');
        	if(arr.length > 2){
        		var lastWord = arr.slice(-1);
	        	if(lastWord != 'Id' && lastWord !='DataTimeStamp'){
	        		delete values2[i];
	        	}
        	}
        }
        
        var values = Ext.clone(values2);
        
        var obj={entity:strToObj(values,me.type=='edit')};
        
        if(me.type=='edit'){
            var field = '';
            for(var i in values2){
                if(i.split('_').slice(-1) != 'DataTimeStamp'){//过滤掉时间戳
                    field += i.split('_').slice(1).join('_') + ",";
                }
            }
            obj.fields = field == "" ? "" : field.slice(0,-1);
        }
        me.fireEvent('changeObject',me,obj);//用于对需要提交的数据进行处理
        
        var params = Ext.JSON.encode(obj);
        var callback = function(text){
        	var result = Ext.JSON.decode(text);
        	if(result.success){
        		if(result.ResultDataValue&&result.ResultDataValue!=''){
            		var key = me.objectName + '_Id';
            		var data = Ext.JSON.decode(result.ResultDataValue);
					var id = data.id;
            		me.getForm().setValues([{id:key,value:id}]);
        		}
        		if(but){but.enable();}//启用调用保存方法的按钮
            	me.fireEvent('saveClick',me);
                if(me.isSuccessMsg){alertInfo('保存成功!');}
        	}else{
        		if(but){but.enable();}//启用调用保存方法的按钮
            	if(me.isSuccessMsg){alertError(result.ErrorInfo);}
            }
        };
        if(but){but.disable();}//禁用调用保存方法的按钮
        postToServer(url,params,callback);
    },
    /**
     * 创建下拉框数据集
     * @private
     * @param {} config
     * @return {}
     */
    createComboStore:function(config){
    	var me = this,
    		fields = config.fields,
    		url = config.url,
    		InteractionField = config.InteractionField,
    		DataTimeStampField = config.DataTimeStampField,
    		valueField = config.valueField;
    	var store = Ext.create('Ext.data.Store',{
    		autoLoad:true,
    		fields:fields.split(","),
    		pageSize:5000,
    		proxy:{
                type:'ajax',
                url:getRootPath() + "/" + url + "&fields=" + fields,
                reader:{type:'json',totalProperty:'count',root:'list'},
                extractResponseData:function(response){
                	if(store.data.length > 0){
                		var items = store.data.items,
                			len = items.length,
                			list = [];
                		for(var i=0;i<len;i++){list.push(items[i].data);}
                		var result = {count:list.length,list:list};
                		response.responseText = Ext.JSON.encode(result);
                	}else{
                		response =  me.changeStoreData(response);
                	}
                	return response;
                }
            },
            listeners:{
            	load:function(s,records,successful){
            		var combo = me.getComponent(InteractionField);
            		if(combo){
            			combo.setValue(combo.defaultValue || '');
            		}
            		var com = me.getComponent(DataTimeStampField);
	                if(com){
	                	var record = s.findRecord(valueField,combo.getValue());
                        if(record != null && record != ""){
		                    var value=record.get(DataTimeStampField.split("_").slice(-2).join("_"));
		                    com.setValue(value);
                        }
	                }
            	}
            }
    	});
    	return store;
    },
    /**
     * 创建单选组数据集
     * @private
     * @param {} config
     * @return {}
     */
    createRadiogroupStore:function(config){
    	var me = this,
    		itemId = config.itemId,//单选组内部ID
    		defaultValue = config.defaultValue,//默认选种值
    		groupName = config.itemId,//组名
    		url = config.url,
    		valueField = config.valueField,
    		displayField = config.displayField,
    		DataTimeStampField = config.DataTimeStampField,
    		fields = valueField + "," + displayField + "," + DataTimeStampField;//需要的数据字段
    		
    	var store = Ext.create('Ext.data.Store',{
    		autoLoad:true,
    		defaultValue:defaultValue,
    		fields:fields.split(","),
    		readOnly:false,
    		proxy:{
                type:'ajax',
                url:getRootPath() + "/" + url + "&fields=" + fields,
                reader:{type:'json',totalProperty:'count',root:'list'},
                extractResponseData:function(response){
                	var callback = function(data){
                		var radios = [];
                		var com = me.getComponent(itemId);
                		var list = data.list;
                		for(var i in list){
                			var obj = list[i];
                			var radio = {
                				checked:(obj[valueField] == store.defaultValue),
                				readOnly:store.readOnly,
                				name:groupName,
                				boxLabel:obj[displayField],
                				inputValue:obj[valueField],
                				DataTimeStamp:obj[DataTimeStampField]
                			};
                			radios.push(radio);
                		}
                		if(com){
                			com.removeAll();
                			com.add(radios);
                		}
                	};
                	return me.changeStoreData(response,callback);
                }
            }
    	});
    	return store;
    },
    /**
     * 列表格式数据匹配方法
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreData:function(response,callback){
        var result = Ext.JSON.decode(response.responseText);
        result.count = 0;result.list = [];
        
        if(result.ResultDataValue && result.ResultDataValue !=''){
            var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
            result.count = ResultDataValue.count;
            result.list = ResultDataValue.list;
            result.ResultDataValue = "";
        }
        if(Ext.typeOf(callback) === 'function'){
        	callback(result);
        }
        response.responseText = Ext.JSON.encode(result);
        return response;
    },
    /**
     * 功能按钮点击弹出树面板
     * @private
     * @param {} com
     */
    funBtnClick:function(com){
    	var me = this;
        //处理代码
        if(com.appComID && com.appComID != ""){//弹出的应用Id
        	var callback = function(info){
        		if(info.success){
        			var appInfo = info.appInfo;
        			var ClassCode = appInfo[me.classCode];
        			var selectId = me.getComponent(com.boundField).getValue();
        			var config = {
        				selectId:selectId//默认选中
        			};
        			me.openTreeWin(ClassCode,com,config);
        		}else{
        			alertError(info.ErrorInfo);
        		}
            };
			getAppInfo(com.appComID,callback);
        }else{
        	alertError('功能按钮没有绑定应用!');
    	}
    },
    /**
     * 打开树
     * @param {} title
     * @param {} classCode
     * @param {} com
     * @param {} selectId
     */
    openTreeWin:function(classCode,com,config){
        var me = this;
        var win = me.getAppWin(classCode,config)
		win.show();
		
		win.on({
			okClick:function(){
                var records = win.getValue();
                if(records.length == 1){
                    me.setWinformInfo(records[0],com);
                    win.close();
                }else{
                	alertInfo('请选择一行数据!');
                }
            },
            //树的双击事件
            itemdblclick:function(view,record,item,index,e,eOpts){
                me.setWinformInfo(record,com);
                win.close();//关闭应用列表窗口(存在bug)
            }
        });
	},
	/**
	 * 功能按钮弹出页面选中一行数据后先表单反填数据
	 * @private
	 * @param {} record
	 * @param {} com
	 */
	setWinformInfo:function(record,com){
		var me = this;
        var itemId = com.boundField;
        var value = record.get('Id');
        var text = record.get('text');
        var combo = me.getComponent(itemId);
        combo.treeNodeID = record.get('Id');//????
        if(combo.type == 'datacombobox'){
            combo.store.loadData([[value,text]]);
           	combo.setValue(value);
           	var dataTimeStampComId = me.getDataTimeStampStrByIdStr(itemId);
           	var dataTimeStampCom=me.getComponent(dataTimeStampComId);
            dataTimeStampCom && dataTimeStampCom.setValue(record.get('DataTimeStamp') + "");
        }else{
            combo.setValue(text);
       	}
	},
	/**
	 * 表单赋值
	 * @public
	 * @param {} values
	 */
	setValues:function(values){
		var me = this;
		me.getForm().setValues(values);
		var items = me.items.items;
		for(var i in items){
			var item = items[i];
			var type = item.type;
			if(type == 'label'){//label组件的赋值使用setText
				var value = values[item.name];
				if(Ext.typeOf(value) !== 'undefined'){
					item.setText(value);
				}
			}else if(type == 'radiogroup'){//单选组
				var value = values[item.name];
				item.store.defaultValue = value;
				var arr = item.items;
            	if(Ext.typeOf(arr) === 'array' && arr.length > 0){
            		if(arr[o].inputValue == value){
						arr[o].setValue(true);
					}
            	}
			}else if(type == 'dataradiogroup'){//定值单选组
				var value = values[item.name];
				item.defaultValue = value;
				var arr = item.items;
            	if(Ext.typeOf(arr) === 'array' && arr.length > 0){
            		if(arr[o].inputValue == value){
						arr[o].setValue(true);
					}
            	}
			}else if(type == 'combobox'){
				var value = values[item.name];
				item.defaultValue = value;
			}
		}
	},
	/**
	 * 渲染完毕后处理
	 * @private
	 */
	afterRender:function(){
        var me=this;
        me.initBasicEvents();
        me.callParent(arguments);
        if(Ext.typeOf(me.callback)=='function'){me.callback(me);}
        if(me.type == 'add'){
            me.isAdd();
        }else if(me.type == 'edit'){
            me.isEdit(me.dataId);
        }else if(me.type == 'show'){
             me.isShow(me.dataId);
            var buts = me.getComponent('bottomtoolbar');
            buts && buts.hide();
        }
    },
    /**
     * 属性值变更
     * @private
     */
    changeConfig:function(){
    	var me = this;
    	if(me.bgFielName && me.bgFielName != ""){
    		me.html = "<img src='" + getHtmlBackgroundPictureRootPath()+"/" + me.bgFielName + "'/>";
    	}
    	Ext.Loader.setConfig({enabled: true});//允许动态加载
		Ext.Loader.setPath('Ext.zhifangux',getRootPath() + '/ui/zhifangux');
    	me.requires = ['Ext.zhifangux.*'];
    },
    /**
     * 创建基础组件监听
     * @private
     * @return {}
     */
    createBasicListeners:function(){
    	var me = this;
    	var listeners = {};
    	return listeners;
    },
    /**
     * 创建下拉框监听
     * @private
     * @return {}
     */
    createComboListeners:function(){
    	var me = this;
    	var listeners = {};
    	//选中时监听
    	listeners.select = function(combo,records){
            var com = combo.ownerCt.getComponent(combo.DataTimeStampField);
            if(com){
                //时间戳匹配处理
            	var data = records[0].data;
                for(var i in data){
                	var arr = i.split('_');
                	var lastWord = arr.slice(-1);
                	//var lastWord = i.split('_').slice(-1);
                    if(lastWord == 'DataTimeStamp'){
                    	var value = data[i]
	                    com.setValue(value);
                     }
                 }
            }
        };
        //模糊匹配
        listeners.beforequery = function(e){
        	var combo = e.combo;
            if(!e.forceAll){
            	var value = e.query;
            	combo.store.filterBy(function(record,id){
            		var text = record.get(combo.displayField);
            		return (text.indexOf(value) != -1);
            	});
            	combo.expand();
            	return false;
            }
        };
    	//追加基础监听
    	var basicListeners = me.createBasicListeners();
    	for(var i in basicListeners){
    		listeners[i] = basicListeners[i];
    	}
    	
    	return listeners;
    },
    /**
     * 创建单选组监听
     * @private
     * @return {}
     */
    createRadiogroupListeners:function(){
    	var me = this;
    	var listeners = {};
    	//单选组的表单里相关时间戳赋值处理
    	listeners.change = function(combo,newValue,oldValue,eOpts){
	        var com = combo.ownerCt.getComponent(combo.DataTimeStampField);
            if(com){
                var valueArr = combo.getChecked();
                if(valueArr.length > 0){
                    var value = valueArr[0].DataTimeStamp;
                    com.setValue(value);
                }
            }
    	};
    	//追加基础监听
    	var basicListeners = me.createBasicListeners();
    	for(var i in basicListeners){
    		listeners[i] = basicListeners[i];
    	}
    	return listeners;
    },
    /**
     * 重置
     * @private
     */
    reset:function(){
    	var me = this,
    		form = me.getForm(),
    		items = me.items.items;
    		
    	form.reset();
    	//初始化时处理
    	for(var i in items){
    		var item = items[i];
    		var type = item.type;
    		if(type == 'combobox'){//下拉框
    			var record = item.store.findRecord(item.valueField,item.getValue());
    			if(record){
    				var str2 = me.getDataTimeStampStrByIdStr(item.itemId);
    				var com = me.getComponent(str2);
    				if(com){
    					var str = me.getDataTimeStampStrByIdStr(item.valueField); 
	    				var DataTimeStamp = record.get(str);
    					com.setValue(DataTimeStamp);
    				}
    			}
    		}else if(type == 'radiogroup'){//单选组
    			var value = "";
    			var obj = item.getValue();
    			for(var i in obj){
    				value = obj[i];
    			}
    			var arr = item.items.items;
    			var DataTimeStamp = "";
    			if(Ext.typeOf(arr) === 'array' && arr.length > 0){
    				for(var o in arr){
    					if(arr[o].inputValue == value){
							DataTimeStamp = arr[o].DataTimeStamp;
						}
    				}
            	}
            	if(DataTimeStamp != ""){
            		var str = me.getDataTimeStampStrByIdStr(item.itemId);
            		var com = me.getComponent(str);
            		if(com){
            			com.setValue(DataTimeStamp);
            		}
            	}
    		}
    	}
    },
    /**
     * 根据ID属性名称获取时间戳属性名称
     * @private
     * @param {} value
     * @return {}
     */
    getDataTimeStampStrByIdStr:function(value){
    	var v = value || "";
    	var result = v == "" ? "" : v.split('_').slice(0,-1).join('_') + "_DataTimeStamp";
    	return result;
    },
    /**
     * 给带功能按钮的定值下拉框赋值
     * @public
     * @param {} config
     */
    setDataComboboxValue:function(config){
    	var me = this,
    		itemId,//定值下拉框itemId
    		name,//显示值
    		IdValue,//交互值
    		DataTimeStampValue;//时间戳值
    		
    	if(Ext.typeOf(config) !== 'object'){
    		alertError("表单setDataComboboxValue方法接收的参数格式错误!");
    		return;
    	}else{
    		itemId = config.itemId,
    		name = config.Name,
    		IdValue = config.Id,
    		DataTimeStampValue = config.DataTimeStamp;
    		var bo = true;
    		var errorInfo = "";
    		if(Ext.typeOf(itemId) !== 'string'){bo = false;errorInfo += "参数itemId格式错误！</br>";}
    		if(Ext.typeOf(name) !== 'string'){bo = false;errorInfo += "参数Name格式错误！</br>";}
    		if(Ext.typeOf(IdValue) !== 'string'){bo = false;errorInfo += "参数Id格式错误！</br>";}
    		if(Ext.typeOf(DataTimeStampValue) !== 'string'){bo = false;errorInfo += "参数DataTimeStamp格式错误！</br>";}
    		if(!bo){
    			alertError(errorInfo);
    			return;
    		}
    	}
    	
    	var combobox = me.getComponent(itemId);
    	if(combobox && combobox.hasButton){
    		combobox.store.loadData([[IdValue,name]]);//更换数据集
    		combobox.setValue(IdValue);
    		var str = me.getDataTimeStampStrByIdStr(itemId);
    		var com = me.getComponent(str);
    		if(com){
    			com.setValue(DataTimeStampValue);
    		}
    	}
    },
    /**
     * 获取弹出的应用组件
     * @private
     * @param {} classCode
     * @param {} config
     * @return {}
     */
    getAppWin:function(classCode,config){
    	var me = this;
        var panel = eval(classCode);
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
        var con = {
        	maxWidth:maxWidth,
			autoScroll:true,
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			resizable:true,//可变大小
			draggable:true//可移动
        };
        for(var i in config){
        	con[i] = config[i];
        }
        var win = Ext.create(panel,con);
		if(win.height > maxHeight){win.height = maxHeight;}
		return win;
    },
    /**
     * 功能按钮弹出列表面板
     * @private
     * @param {} com
     */
    funBtnListClick:function(com){
    	var me = this;
        //处理代码
        if(com.appComID && com.appComID != ""){//弹出的应用Id
        	var callback = function(info){
        		if(info.success){
        			var appInfo = info.appInfo;
        			var ClassCode = appInfo[me.classCode];
        			var selectId = me.getComponent(com.boundField).getValue();
        			var config = {
        				selectId:selectId//默认选中
        			};
        			me.openListWin(ClassCode,com,config);
        		}else{
        			alertError(info.ErrorInfo);
        		}
            };
			getAppInfo(com.appComID,callback);
        }else{
        	alertError('功能按钮没有绑定应用!');
    	}
    },
    /**
     * 弹出列表
     * @private
     * @param {} classCode
     * @param {} com
     * @param {} config
     */
    openListWin:function(classCode,com,config){
    	var me = this;
        var win = me.getAppWin(classCode,config)
		win.show();
		
		win.on({
            //树的双击事件
            itemdblclick:function(view,record,item,index,e,eOpts){
                var obj = {
                	itemId:com.boundField,
                	Id:record.get(win.objectName + "_Id"),
                	Name:record.get(com.textField),
                	DataTimeStamp:record.get(win.objectName + "_DataTimeStamp")
                };
                me.setDataComboboxValue(obj);
                win.close();//关闭应用列表窗口(存在bug)
            }
        });
    }
});