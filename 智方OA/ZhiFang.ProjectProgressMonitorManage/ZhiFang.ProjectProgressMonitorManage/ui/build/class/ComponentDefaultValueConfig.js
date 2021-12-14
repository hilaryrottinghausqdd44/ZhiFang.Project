/**
 * 组件初始值配置
 * 【可配参数】
 * type：组件类型，必配值，如果不配，面板上将没有任何选项；可配的值：
 * 		文本框(textfield)、文本域(textareafield、textarea)、数字框(numberfield)、日期(datefield)、时间(timefield)、
 * 		下拉框(combobox、combo)、单选组(radiogroup)、复选组(checkboxgroup)、
 * 		复选框(checkboxfield、checkbox)
 * 【对外方法】
 * 【对外事件】
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ComponentDefaultValueConfig',{
	extend:'Ext.form.FieldSet',
	alias:'widget.componentdefaultvalueconfig',
	//=====================可配参数=======================
	/**
	 * 组件类型
	 * @type String
	 */
	type:'',
	/**
	 * 默认初始值
	 * @type String
	 */
	value:'',
	//----------单选组、多选组、下拉框必配属性---------
	/**
	 * 服务地址
	 * @type String
	 */
	serverUrl:'',
	/**
	 * 值字段
	 * @type String
	 */
	valueField:'',
	/**
	 * 显示字段
	 * @type String
	 */
	displayField:'',
	//--------文本框、文本域、数字框、日期框、时间框必配属性--------
	isFixedValue:true,
	//=====================内部参数=======================
	/**
	 * 值的类型
	 * 1:文本框(textfield)、文本域(textareafield、textarea)、数字框(numberfield)、日期(datefield)、时间(timefield)
	 * 2:下拉框(combobox、combo)、单选组(radiogroup)、复选组(checkboxgroup)、复选框(checkboxfield、checkbox)
	 * @type String
	 */
	valueType:'',
	/**
	 * 宏列表
	 * @type 
	 */
	macroValueList:[['','---空---'],['today','今天'],['thisyear','今年'],['localtime','本地时间']],
	//=====================视图渲染=======================
	/**
	 * 渲染后处理
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initListeners();
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.layout = "anchor";
		me.defaults = {anchor:'100%'};
		//内部按钮组
		me.items = me.createItems();
		//初始化扩展的事件
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
		//根据类型参数生成内部组件
		var items = me.createItemsByType();
		return items;
	},
	/**
	 * 根据类型参数生成内部组件
	 * @private
	 */
	createItemsByType:function(){
		var me = this;
		var items = [];
		
		if(me.type == 'textfield'){//文本框
			me.title = "文本框初始值";
			me.valueType = "1";
			items = me.createFixedValueItems('textfield');
		}else if(me.type == 'textareafield' || me.type == 'textarea'){//文本域
			me.title = "文本域初始值";
			me.valueType = "1";
			items = me.createFixedValueItems('textarea');
		}else if(me.type == 'numberfield'){//数字框
			me.title = "数字框初始值";
			me.valueType = "1";
			items = me.createFixedValueItems('numberfield');
		}else if(me.type == 'datefield'){//日期
			me.title = "日期框初始值";
			me.valueType = "1";
			items = me.createFixedValueItems('datefield');
		}else if(me.type == 'timefield'){//时间
			me.title = "时间框初始值";
			me.valueType = "1";
			items = me.createFixedValueItems('timefield');
		}else if(me.type == 'combobox' || me.type == 'combo'){//下拉框
			me.title = "下拉框初始值";
			if(me.serverUrl != '' && me.valueField != '' && me.displayField != ''){
				me.valueType = "2";
				items = me.createComboxItems();
			}else{
				items = me.createDefaultItems('服务地址、值字段、显示字段必须赋值！');
			}
		}else if(me.type == 'radiogroup'){//单选组
			me.title = "单选组初始值";
			if(me.serverUrl != '' && me.valueField != '' && me.displayField != ''){
				me.valueType = "2";
				items = me.createGroupItems('radiogroup');
			}else{
				items = me.createDefaultItems('服务地址、值字段、显示字段必须赋值！');
			}
		}else if(me.type == 'checkboxgroup'){//复选组
			me.title = "复选组初始值";
			if(me.serverUrl != '' && me.valueField != '' && me.displayField != ''){
				me.valueType = "2";
				items = me.createGroupItems('checkboxgroup');
			}else{
				items = me.createDefaultItems('服务地址、值字段、显示字段必须赋值！');
			}
		}else if(me.type == 'checkboxfield' || me.type == 'checkbox'){//复选框
			me.title = "复选框初始值";
			me.valueType = "2";
			items = me.createCheckboxItems();
		}else{//没有类型
			me.title = "错误的类型";
			items = me.createDefaultItems();
		}
		
		return items;
	},
	/**
	 * 定值初始值内部组件
	 * @private
	 * @param {} type
	 * @return {}
	 */
	createFixedValueItems:function(type){
		var me = this;
		var items = [];
		//定值
		var fixedValue = {
			xtype:type,
			itemId:'fixedValue',
			labelWidth:30,
			fieldLabel:'定值',
			emptyText:'固定的初始值',
			value:(me.isFixedValue ? me.value : '')
		};
		(type == "textarea") && (fixedValue.grow = true);
		(type == "datefield") && (fixedValue.format ="Y年m月d日");
		items.push(fixedValue);
		//宏
		items.push({
			xtype:'combobox',
			itemId:'macroValue',
			labelWidth:30,
			fieldLabel:'宏',
            mode:'local',
            editable:false,
            valueField:'value',
            displayField:'text',
            value:(me.isFixedValue ? '' : me.value),
			store:new Ext.data.SimpleStore({
				fields:['value','text'],
				data:me.macroValueList
			})
		});
		return items;
	},
	/**
	 * 单选组、多选组初始值内部组件
	 * @private
	 * @return {}
	 */
	createGroupItems:function(type){
		var me = this;
		var items = [];
		//选择项
		items.push({
			xtype:'label',
			itemId:'loadingText',
			text:'数据加载中...'
		});
		return items;
	},
	/**
	 * 下拉框初始值内部组件
	 * @private
	 * @return {}
	 */
	createComboxItems:function(){
		var me = this;
		var items = [];
		//下拉框数据集
		var store = new Ext.data.Store({
        	fields:['text','value'],
        	autoLoad:true,
        	proxy:{
        		type:'ajax',
        		url:me.serverUrl,
        		extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var list = [{value:'',text:'---空---'}];
                    if(data.ResultDataValue && data.ResultDataValue != ""){
                    	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    	var arr = ResultDataValue.list;
                		for(var i in arr){
                			var obj = {
                				value:arr[i][me.valueField],
                				text:arr[i][me.displayField]
                			};
                			list.push(obj);
                		}
                    }
                    //返回处理后的数据
                    response.responseText = Ext.JSON.encode(list);
                    return response;
                }
        	},
        	listeners:{
        		load:function(){
        			me.getComponent('value').setValue(me.value);
        		}
        	}
        });
		//下拉选值
		items.push({
			xtype:'combo',fieldLabel:'默认值',labelWidth:50,
            editable:false,itemId:'value',
            displayField:'text',valueField:'value',
            store:store
		});
		return items;
	},
	/**
	 * 复选框初始值内部组件
	 * @private
	 * @return {}
	 */
	createCheckboxItems:function(){
		var me = this;
		var items = [];
		//是否赋初始值选择
		items.push({
			xtype:'checkbox',
			itemId:'value',
			labelWidth:60,
			fieldLabel:'默认勾选',
			boxLabel:'勾选',
			checked:me.value
		});
		return items;
	},
	/**
	 * 默认的没有传递类型参数时的提示
	 * @private
	 * @return {}
	 */
	createDefaultItems:function(text){
		var me = this;
		var items = [];
		//没有类型的提示
		items.push({
			xtype:'label',
			text:text || '类型参数错误！',
			style:'fontWeight:bold;color:red;'
		});
		return items;
	},
	//=====================组件监听=======================
	/**
	 * 监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		if(me.type == 'radiogroup' || me.type == 'checkboxgroup'){
			me.initGroupItemsListeners();
		}
	},
	/**
	 * 单选组、多选组监听
	 * @private
	 */
	initGroupItemsListeners:function(){
		var me = this;
		var loadingText = me.getComponent('loadingText');
		if(me.serverUrl != '' && me.valueField != '' && me.displayField != ''){
			var callback = function(appInfo){
				if(appInfo.success){
					if(appInfo.data && appInfo.data.list && appInfo.data.list.length > 0){
						//更改单选组、多选组的内部组件内容
						me.changeGroupItems(appInfo.data.list);
					}else{
						loadingText.setText('没有获取到数据!');
					}
				}else{
					loadingText.setText(appInfo.errorInfo);
				}
			};
			me.getDataFromServer(callback);
		}
	},
	/**
	 * 更改单选组、多选组的内容
	 * @private
	 * @param {} list
	 */
	changeGroupItems:function(list){
		var me = this;
		me.remove('loadingText');
		var items = [];
		for(var i in list){
			var obj = list[i];
			var item = {
				name:'value',
				boxLabel:obj[me.displayField],
				inputValue:obj[me.valueField],
				checked:me.isCheckedValue(obj[me.valueField])
			};
			items.push(item);
		}
		me.add({
			columns:1,
        	vertical:true,
			xtype:me.type,
			itemId:'value',
			items:items
		});
	},
	/**
	 * 返回单选组、多选组的选项是否选中
	 * @private
	 * @param {} value
	 * @return {}
	 */
	isCheckedValue:function(value){
		var me = this;
		var bo = false;
		var v = me.value;
		if(Ext.typeOf(v) == 'array'){
			for(var i=0,l=v.length;i<l;i++){
				if(value == v[i]){
					bo = true;break;
				}
			}
		}else{
			bo = (value == v);
		}
		return bo;
	},
	//=====================加载数据=======================
	/**
	 * 从后台获取数据
	 * @private
	 * @param {} callback
	 */
	getDataFromServer:function(callback){
		var me = this;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,
            url:me.serverUrl, 
            method:'GET', 
            timeout:2000, 
            success:function(response,opts){ 
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){ 
                    var appInfo = {success:true,data:null};
                    if(result.ResultDataValue && result.ResultDataValue != ''){
                        appInfo.data = Ext.JSON.decode(result.ResultDataValue); 
                    } 
                    if(Ext.typeOf(callback) == 'function'){ 
                       callback(appInfo); //回调函数
                    } 
                }else{ 
                    if(Ext.typeOf(callback) == 'function'){ 
                       callback({success:false,errorInfo:result.ErrorInfo}); //回调函数
                    } 
                } 
            }, 
            failure:function(response,options){  
                if(Ext.typeOf(callback) == 'function'){ 
                   callback({success:false,errorInfo:'获取数据请求错误！'}); //回调函数
                } 
            } 
		});
	},
	//=====================公开方法=======================
	/**
	 * 获取当前配置的初始值
	 * @public
	 * @return {}
	 */
	getValue:function(){
		var me = this;
		var value = "";
		if(me.valueType == '1'){
			var fixedValue = me.getComponent('fixedValue').getValue();
			var macroValue = me.getComponent('macroValue').getValue();
			value = (fixedValue && fixedValue != '' ? fixedValue : macroValue);
			value = value || "";
		}else if(me.valueType == '2'){
			var v = me.getComponent('value').getValue();
			var type = Ext.typeOf(v);
			if(type == 'object'){
				var arr = [];
				var vType = Ext.typeOf(v.value);
				if(vType == 'string'){
					arr.push(v.value);
				}else if(vType == 'array'){
					arr = v.value;
				}
				value = arr;
			}else{
				value = v;
			}
		}else{
			value = null;
		}
		return value;
	},
	/**
	 * 
	 * @param {} value
	 */
	setValue:function(value){
		
	}
});