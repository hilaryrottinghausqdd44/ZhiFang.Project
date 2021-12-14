/**
 * 组件初始值配置
 * 【可配参数】
 * entityName 对象名(赋值的时候,固定值就会有服务选择选项)
 * 
 * 【对外方法】
 * getDefaultValue 获取初始值
 * setDefaultValue(obj) 赋值，
 * 		obj{
 * 			valueType,//值类型
 *			fixedValueType,//固定值类型
 *			value,//初始化值
 *			valueField,//值字段
 *			displayField,//显示字段
 *			valueServer//服务地址
 * 		}
 * 【对外事件】
 * valueChange 初始值配置改变，返回值对象：
 * 		obj{
 * 			valueType,//值类型
 *			fixedValueType,//固定值类型
 *			value,//初始化值
 *			valueField,//值字段
 *			displayField,//显示字段
 *			valueServer//服务地址
 * 		}
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ComponentDefaultValueConfig',{
	extend:'Ext.form.FieldSet',
	alias:'widget.componentdefaultvalueconfig',
	//=====================可配参数=======================
	/**
	 * 对象名
	 * @type String
	 */
	entityName:'',
	/**
	 * 值类型
	 * @type String
	 */
	valueType:'1',
	/**
	 * 固定值类型
	 * @type String
	 */
	fixedValueType:'1',
	/**
	 * 固定值-手工输入
	 * @type String
	 */
	fixedValue:'',
	/**
	 * 固定值-服务选取-值字段
	 * @type String
	 */
	valueField:'',
	/**
	 * 固定值-服务选取-显示字段
	 * @type String
	 */
	displayField:'',
	/**
	 * 固定值-服务选取-服务
	 * @type String
	 */
	valueServer:'',
	/**
	 * 固定值-服务选取-默认值
	 * @type String
	 */
	valueList:'',
	/**
	 * 宏值
	 * @type String
	 */
	macro:'',
	/**
	 * Cookie值
	 * @type String
	 */
	cookie:'',
	//=====================内部参数=======================
	autoScroll:true,
	comLabelWidth:60,
	/**
     * 获取数据对象内容时后台接收的参数名称
     * @type String
     */
    objectPropertyParam:'EntityName',
	/**
     * 获取数据对象内容的服务地址
     * @type 
     */
	objectPropertyUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityFrameTree',
	/**
     * 查询对象属性所属字典服务列表时后台接收的参数名称
     * @type String
     */
    dictionaryListServerParam:'EntityPropertynName',
	/**
     * 查询对象属性所属字典服务列表的服务地址
     * @type 
     */
	dictionaryListServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName',
	/**
	 * 宏列表
	 * @type 
	 */
	macroList:[['today','今天'],['thisyear','今年'],['localtime','本地时间']],
	/**
	 * Cookie列表
	 * @type 
	 */
	cookieList:[['userId','登录用户ID'],['userName','登录用户名'],['userOrgId','登录用户部门ID'],['userOrgName','登录用户部门名称']],
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initListeners();
	},
	initComponent:function(){
		var me = this;
		//内部按钮组
		me.items = me.createItems();
		//注册事件
        me.initExpandEvent();
        
		me.callParent(arguments);
	},
	/**
	 * 初始化扩展的事件
	 * @private
	 */
	initExpandEvent:function(){
		var me = this;
		me.addEvents('valueChange');//值字段
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [];
		
		//是否有对象名且对象名不为空
		var hasEntityName = false;
		if(me.entityName && me.entityName != ""){
			hasEntityName = true;
		}
		
		//值类型
		items.push({
			xtype:'radiogroup',fieldLabel:'类型',labelWidth:30,style:'fontWeight:bold;',itemId:'valueType',
			items:[{
				boxLabel:'固定值',name:'valueType',inputValue:'1',
				checked:(me.valueType == '1' || me.valueType == '')
			},{
				boxLabel:'宏',name:'valueType',inputValue:'2',
				checked:(me.valueType == '2')
			},{
				boxLabel:'Cookie',name:'valueType',inputValue:'3',
				checked:(me.valueType == '3')
			}]
		});
		
		//固定值
		items.push({
			xtype:'radiogroup',fieldLabel:'固定值类型',labelWidth:70,itemId:'fixedValueType',
			items:[{
				boxLabel:'直接输入',name:'fixedValueType',inputValue:'1',
				checked:(me.fixedValueType == '1' || me.fixedValueType == '')
			},{
				boxLabel:'服务选取',name:'fixedValueType',inputValue:'2',hidden:!hasEntityName,
				checked:(me.fixedValueType == '2')
			}]
		});
		
		items.push({
			xtype:'textfield',emptyText:'请输入初始值',fieldLabel:'初始值',labelWidth:me.comLabelWidth,
			name:'fixedValue',itemId:'fixedValue',value:me.fixedValue
		});
		//值字段数据
		var valueFieldStore = new Ext.data.Store({ 
            fields:['value','text'],
            autoLoad:true,
            proxy:{
            	type:'ajax',
            	url:me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + me.entityName,
            	extractResponseData:function(response){
            		var data = Ext.JSON.decode(response.responseText);
            		var list = [];
            		if(data.ResultDataValue && data.ResultDataValue != ""){
            			var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                		for(var i in ResultDataValue){
                			list.push({
                				value:ResultDataValue[i].InteractionField,
                				text:ResultDataValue[i].text
                			});
                		}
            		}
            		response.responseText = Ext.JSON.encode(list);
                    return response;
            	}
            }
        });
		//显示字段数据
		var displayFieldStore = new Ext.data.Store({ 
            fields:['value','text'],
            autoLoad:true,
            proxy:{
            	type:'ajax',
            	url:me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + me.entityName,
            	extractResponseData:function(response){
            		var data = Ext.JSON.decode(response.responseText);
            		var list = [];
            		if(data.ResultDataValue && data.ResultDataValue != ""){
            			var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                		for(var i in ResultDataValue){
                			list.push({
                				value:ResultDataValue[i].InteractionField,
                				text:ResultDataValue[i].text
                			});
                		}
            		}
            		response.responseText = Ext.JSON.encode(list);
                    return response;
            	}
            }
        });
		//服务列表数据
        var valueServerStore = new Ext.data.Store({ 
            fields:['value','text'],                    
            autoLoad:true,
            proxy:{
            	type:'ajax',
            	url:me.dictionaryListServerUrl + "?" + me.dictionaryListServerParam + "=" + me.entityName + "_Id",
            	extractResponseData:function(response){
            		var data = Ext.JSON.decode(response.responseText);
            		var list = [];
            		if(data.ResultDataValue && data.ResultDataValue != ""){
            			var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                		for(var i in ResultDataValue){
                			list.push({
                				value:ResultDataValue[i].ServerUrl,
                				text:ResultDataValue[i].CName
                			});
                		}
            		}
            		response.responseText = Ext.JSON.encode(list);
                    return response;
            	}
            }
        });
        //默认值列表数据
        var valueListStore = new Ext.data.Store({ 
            fields:['value','text'],
            proxy:{
                type:'ajax',
                url:'',
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var list = [];
                    var defaultValue = me.getComponent('valueList');
                    if(data.ResultDataValue && data.ResultDataValue != ""){
                    	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                		for(var i in ResultDataValue.list){
                			list.push({
                				value:ResultDataValue.list[i][defaultValue.valueName],
                				text:ResultDataValue.list[i][defaultValue.textName]
                			});
                		}
                    }
                    //返回处理后的数据
                    response.responseText = Ext.JSON.encode(list);
                    return response;
                }
            }
        });
        
        //值字段
		items.push({
			xtype:'combobox',fieldLabel:'值字段',labelWidth:me.comLabelWidth,hidden:true,
            editable:true,typeAhead:true,forceSelection:true,queryMode:'local',
            displayField:'text',valueField:'value',
            itemId:'valueField',name:'valueField',
            store:valueFieldStore,
            value:(me.valueField != '' ? me.valueField : me.entityName+'_Id')
		});
		//显示字段
		items.push({
			xtype:'combobox',fieldLabel:'显示字段',labelWidth:me.comLabelWidth,hidden:true,
            editable:true,typeAhead:true,forceSelection:true,queryMode:'local',
            displayField:'text',valueField:'value',
            itemId:'displayField',name:'displayField',
            store:displayFieldStore,
            value:(me.displayField != '' ? me.displayField : me.entityName+'_Id')
		});
		//服务列表
		items.push({
			xtype:'combobox',fieldLabel:'服务列表',labelWidth:me.comLabelWidth,hidden:true,
            editable:false,typeAhead:true,forceSelection:true,queryMode:'local',
            displayField:'text',valueField:'value',
            itemId:'valueServer',name:'valueServer',
            store:valueServerStore,
            value:me.valueServer
		});
		//默认值
		items.push({
			xtype:'combobox',fieldLabel:'默认值',labelWidth:me.comLabelWidth,hidden:true,
            editable:true,typeAhead:true,forceSelection:true,queryMode:'local',
            displayField:'text',valueField:'value',
            itemId:'valueList',name:'valueList',
            valueName:(me.valueField != '' ? me.valueField : me.entityName+'_Id'),
            textName:(me.displayField != '' ? me.displayField : me.entityName+'_Id'),
            store:valueListStore,
            value:me.valueList
		});
		
		//宏
		items.push({
			xtype:'combobox',fieldLabel:'宏',labelWidth:me.comLabelWidth,hidden:true,
            mode:'local',editable:false,
            displayField:'text',valueField:'value',
            itemId:'macro',name:'macro',
            store:new Ext.data.SimpleStore({ 
                fields:['value','text'],                    
                data:me.macroList
            }),
            value:me.macro
		});
		//Cookie
		items.push({
			xtype:'combobox',fieldLabel:'Cookie',labelWidth:me.comLabelWidth,hidden:true,
            mode:'local',editable:false,
            displayField:'text',valueField:'value',
            itemId:'cookie',name:'cookie',
            store:new Ext.data.SimpleStore({ 
                fields:['value','text'],                    
                data:me.cookieList
            }),
            value:me.cookie
		});
		
		items.push({
			xtype:'button',
			text:'获取默认数据',
			handler:function(){
				var value = me.getDefaultValue();
				alert(value);
			}
		});
		return items;
	},
	/**
	 * 监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		//初始化内部组件显示与隐藏的监听
		me.initShowOrHideListeners();
		//值字段、显示字段、服务地址、初始值列表联动的监听
		me.initLinkListeners();
		//默认值变化监听
		me.initValueChangeListeners();
		//初始化处理
		me.changeShowOrHide();
	},
	/**
	 * 初始化内部组件显示与隐藏的监听
	 * @private
	 */
	initShowOrHideListeners:function(){
		var me = this;
		
		//值类型变化监听
		var valueType = me.getComponent('valueType');
		valueType.on({
			change:function(field,newValue,oldValue){
				me.changeShowOrHide();
			}
		});
		
		//固定值类型变化监听
		var fixedValueType = me.getComponent('fixedValueType');
		fixedValueType.on({
			change:function(field,newValue,oldValue){
				me.changeFixedValue(true);
			}
		});
	},
	/**
	 * 联动监听
	 * @private
	 */
	initLinkListeners:function(){
		var me = this;
		var valueField = me.getComponent('valueField');//值字段
		var displayField = me.getComponent('displayField');//显示字段
		var valueServer = me.getComponent('valueServer');//服务列表
		var valueList = me.getComponent('valueList');//默认值列表
		
		var changeValueList = function(){
			var valueFieldValue = valueField.getValue();
	        var displayFieldValue = displayField.getValue();
	        var url = valueServer.getValue();
	        
	        if(valueFieldValue && valueFieldValue != "" && displayFieldValue && displayFieldValue != "" && url && url != ""){
	        	var valueNameArr = valueFieldValue.split("_").slice(-2);
				var valueName = valueNameArr.join("_");
				
				var textNameArr = displayFieldValue.split("_").slice(-2);
				var textName = textNameArr.join("_");
	        
	        	valueList.valueName = valueName;
	        	valueList.textName = textName;
	        	valueList.store.load();
	        }else{
	        	//Ext.Msg.alert('提示','值字段、显示字段、服务都需要选择！');
	        }
		};
		
		//值字段下拉框监听
		valueField.on({
			change:function(field,newValue,oldValue){
				changeValueList();
			}
		});
		//显示字段下拉框监听
		displayField.on({
			change:function(field,newValue,oldValue){
				changeValueList();
			}
		});
		//服务列表下拉框监听
		valueServer.on({
			change:function(field,newValue,oldValue){
                valueList.store.removeAll();
                valueList.store.proxy.url = getRootPath() + "/" +newValue.split("?")[0]+ "?isPlanish=true&where=";
                changeValueList();
            }
		});
	},
	/**
	 * 默认值变化监听
	 * @private
	 */
	initValueChangeListeners:function(){
		var me = this;
		var fixedValue = me.getComponent('fixedValue');//手工输入值
		var valueList = me.getComponent('valueList');//默认值列表
		var macro = me.getComponent('macro');//宏
		var cookie = me.getComponent('cookie');//Cookie
		
		var valueType = me.getComponent('valueType');//值类型
		var fixedValueType = me.getComponent('fixedValueType');//固定值类型
		
		var valueField = me.getComponent('valueField');//值字段
		var displayField = me.getComponent('displayField');//显示字段
		var valueServer = me.getComponent('valueServer');//服务列表
		
		//数据处理
		var changeValue = function(){
			var value = me.getDefaultValue();
			var obj = {
				valueType:valueType.getValue().valueType,
				fixedValueType:fixedValueType.getValue().fixedValueType,
				value:value,
				valueField:valueField.getValue(),
				displayField:displayField.getValue(),
				valueServer:valueServer.getValue()
			};
			me.fireEvent('valueChange',obj);
		};
		
		fixedValue.on({
			change:function(field,newValue){
				changeValue();
			}
		});
		valueList.on({
			change:function(field,newValue){
				changeValue();
			}
		});
		macro.on({
			change:function(field,newValue){
				changeValue();
			}
		});
		cookie.on({
			change:function(field,newValue){
				changeValue();
			}
		});
	},
	/**
	 * 初始化内部组件的显示与隐藏
	 * @private
	 */
	changeShowOrHide:function(){
		var me = this;
		
		var macro = me.getComponent('macro');
		var cookie = me.getComponent('cookie');
		
		//值类型变化监听
		var valueType = me.getComponent('valueType').getValue().valueType;
		if(valueType == "1"){//固定值
			me.changeFixedValue(true);
			macro.hide();
			cookie.hide();
		}else if(valueType == "2"){//宏
			me.changeFixedValue(false);
			macro.show();
			cookie.hide();
		}else if(valueType == "3"){//Cookie
			me.changeFixedValue(false);
			macro.hide();
			cookie.show();
		}
	},
	/**
	 * 固定值类型变化处理
	 * @private
	 * @param {} bo
	 */
	changeFixedValue:function(bo){
		var me = this;
		
		var fixedValueType = me.getComponent('fixedValueType');//固定值类型
		var fixedValue = me.getComponent('fixedValue');
		var valueField = me.getComponent('valueField');//值字段
		var displayField = me.getComponent('displayField');//显示字段
		var valueServer = me.getComponent('valueServer');//服务列表
		var valueList = me.getComponent('valueList');//默认值列表
		
		if(bo){
			fixedValueType.show();
			if(fixedValueType.getValue().fixedValueType == "1"){
				fixedValue.show();
				valueField.hide();
				displayField.hide();
				valueServer.hide();
				valueList.hide();
			}else{
				fixedValue.hide();
				valueField.show();
				displayField.show();
				valueServer.show();
				valueList.show();
			}
		}else{
			fixedValueType.hide();
			fixedValue.hide();
			valueField.hide();
			displayField.hide();
			valueServer.hide();
			valueList.hide();
		}
	},
	//=====================对外公开方法=======================
	/**
	 * 获取初始值
	 * @public
	 * @return {}
	 */
	getDefaultValue:function(){
		var me = this;
		var valueType = me.getComponent('valueType').getValue().valueType;//值类型
		var fixedValueType = me.getComponent('fixedValueType').getValue().fixedValueType;//固定值类型
		var fixedValue = me.getComponent('fixedValue').getValue();//直接输入的值
		var valueList = me.getComponent('valueList').getValue();//默认值列表
		var macro = me.getComponent('macro').getValue();
		var cookie = me.getComponent('cookie').getValue();
		
		var result = null;
		if(valueType == "1"){//固定值
			if(fixedValueType == "1"){//直接输入
				result = fixedValue;
			}else{//服务选取
				result = valueList;
			}
		}else if(valueType == "2"){//宏
			result = macro;
		}else if(valueType == "3"){//Cookie
			result = cookie;
		}
		return result;
	},
	/**
	 * 赋值
	 * @public
	 * @param {} obj
	 */
	setDefaultValue:function(obj){
		var me = this;
		if(obj){
			var valueType = me.getComponent('valueType');//值类型
			
			if(obj.valueType){
				valueType.setValue({valueType:obj.valueType});
				if(obj.valueType == "1"){//固定值
					var fixedValueType = me.getComponent('fixedValueType');//固定值类型
					if(obj.fixedValueType){
						fixedValueType.setValue({fixedValueType:obj.fixedValueType});
						if(obj.fixedValueType == "1"){
							var fixedValue = me.getComponent('fixedValue');//直接输入的值
							fixedValue.setValue(obj.value);
						}else{
							var valueField = me.getComponent('valueField');//值字段
							valueField.value = obj.valueField;
							
							var displayField = me.getComponent('displayField');//显示字段
							displayField.value = obj.displayField;
							
							var valueServer = me.getComponent('valueServer');//服务列表
							valueServer.value = obj.valueServer;
							
							var valueList = me.getComponent('valueList');//默认值列表
							valueList.value = obj.value;
							valueList.store.proxy.url = getRootPath() + "/" +obj.valueServer.split("?")[0]+ "?isPlanish=true&where=";
						}
					}
				}else if(obj.valueType == "2"){//宏
					var macro = me.getComponent('macro');
					macro.setValue(obj.value);
				}else if(obj.valueType == "3"){//Cookie
					var cookie = me.getComponent('cookie');
					cookie.setValue(obj.value);
				}
			}
			me.changeShowOrHide();
		}
	}
});