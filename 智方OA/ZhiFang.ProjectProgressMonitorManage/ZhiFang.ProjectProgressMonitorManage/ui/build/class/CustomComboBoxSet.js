/**
 * 构建列表的数据类型为下拉框时
 * (1)定值下拉列表类型
 * (2)取后台数据的下拉框类型
 * 【可配参数】
 * 
 * 【对外方法】
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.CustomComboBoxSet',{
	extend:'Ext.form.FieldSet',
	alias:'widget.customcomboboxset',
	//=====================可配参数=======================
	type:'fieldset',//'win':窗口方式;'fieldset':表单框方式
	appId:-1,
	title:'自定义下拉框配置',
    initTypeChoose:'false',
    valueField:'valueField',//下拉列表绑定值字段
    textField:'textField',//下拉列表绑定显示字段
    comboboxServerUrl:'comboboxServerUrl',//下拉列表绑定加载的数据地址
    
    combodata:'combodata',//下拉列表定值数据
    
    valueFieldValue:'value',//下拉列表绑定值字段
    textFieldValue:'text',//下拉列表绑定显示字段
    comboboxServerUrlValue:'',//下拉列表绑定加载的数据地址
    combodataValue:'',
    comboBoxDatas:[],////CustomComboBoxSet自定义属性,保存下拉列表的选择字段
    
	comLabelWidth:60,
	comWidth:215,
	anchorValue:'100%',
	//=====================内部视图渲染=======================
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//内部按钮组
		me.items = me.createItems();
		 //注册事件
        me.addEvents('okClick');//确定按钮
        me.addEvents('resetClick');//重置按钮
		me.callParent(arguments);
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [];
		
		if(me.type == 'fieldset'){
			items = me.createFieldSetItems();
		}else{
			items = me.createWinItems();
		}
		
		return items;
	},
	/**
	 * 创建表单框方式的内容排布
	 * @private
	 * @return {}
	 */
	createFieldSetItems:function(){
		var me = this;
		var firstCloumn = me.createFirstCloumn();
		var secondColumn = me.createSecondColumn();
		var defaultValueArea = me.createDefaultValueArea();
		
		var items = firstCloumn.concat(secondColumn).concat(defaultValueArea);
		
		for(var i in items){
			items[i].width = me.comWidth;
			items[i].labelWidth = me.comLabelWidth;
			items[i].anchor = me.anchorValue;
		}
		return items;
	},
	/**
	 * 创建窗口方式的内容排布
	 * @private
	 * @return {}
	 */
	createWinItems:function(){
		var me = this;
		var firstCloumn = me.createFirstCloumn();
		var buttons = me.createButtons();
		
		var num = 25;
		var fy = 0;
		for(var i in firstCloumn){
            if(firstCloumn[i].xtype=='textarea'){
			    firstCloumn[i].width = 445;
            }else{
                firstCloumn[i].width = me.comWidth;
            }
			firstCloumn[i].labelWidth = me.comLabelWidth;
		
			firstCloumn[i].x = 0;
			firstCloumn[i].y = fy;
			fy += num;
		}

		me.layout = "absolute";
		var items = firstCloumn.concat(buttons);
		return items;
	},
    setComShow:function(value){
        var me=this;
        var valueField=me.getComponent(me.valueField);
        var textField=me.getComponent(me.textField);
        var comboboxServerUrl=me.getComponent(me.comboboxServerUrl);
        var combodataLabel=me.getComponent('combodataLabel');
        var combodata=me.getComponent(me.combodata);
        if (value==='true'){
            valueField.hide();
            textField.hide();
            comboboxServerUrl.hide();
            combodataLabel.show();
            combodata.show();
        }else{
            valueField.show();
            textField.show();
            comboboxServerUrl.show();
            combodataLabel.hide();
            combodata.hide();
        }
    },
	/**
	 * 第一列数据
	 * @private
	 * @return {}
	 */
	createFirstCloumn:function(){
		var me = this;
		var items = [];
//        var partOneArr=[];
//        var partTwoArr=[];
        items.push({
                xtype: 'radiogroup',
                itemId:'typeChoose',
                labelWidth:180,
                style:{fontWeight:'bold'},
                fieldLabel:'类型选择',
                columns:2,
                vertical:true,
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                        var value=newValue.typeChoose;
                        me.setComShow(value);
                    }
                },   
                items:[
                    {boxLabel:'定值',name:'typeChoose',inputValue:'true'},
                    {boxLabel:'后台服务',name:'typeChoose',inputValue:'false'}
                ]
            });
            
         //
        items.push({
            name:me.valueField,
            itemId:me.valueField,
            value:me.valueFieldValue,
            xtype:'combobox',fieldLabel:'值字段',
            labelWidth:60,
            editable:true,typeAhead:true,
            forceSelection:true,
            queryMode:'local',
            displayField:me.objectPropertyDisplayField,
            valueField:me.objectPropertyValueField,
            store:new Ext.data.Store({
                fields:me.objectPropertyFields,
                data:me.comboBoxDatas
            }),
            listeners:{
                focus:function(owner,The,eOpts){},
                select:function(combo,records,eOpts){
                 var newValue=combo.getValue();
                
                }
            }
            
        });
		//名称
		items.push({
			name:me.textField,
			itemId:me.textField,
			value:me.textFieldValue,
			xtype:'combobox',fieldLabel:'显示字段',
            labelWidth:60,
            editable:true,typeAhead:true,
            forceSelection:true,
            queryMode:'local',
            displayField:me.objectPropertyDisplayField,
            valueField:me.objectPropertyValueField,
            store:new Ext.data.Store({
                fields:me.objectPropertyFields,
                data:me.comboBoxDatas
            })
		});

        items.push({
            name:me.comboboxServerUrl,
            itemId:me.comboboxServerUrl,
            value:me.comboboxServerUrlValue,
            xtype:'combobox',fieldLabel:'数据地址',
            labelWidth:60,
            editable:true,typeAhead:true,
            forceSelection:true,
            queryMode:'local',
            displayField:me.objectServerDisplayField,
            valueField:me.objectServerValueField,
            store:new Ext.data.Store({
                fields:me.objectServerFields,
                proxy:{
                    type:'ajax',
                    url:me.dictionaryListServerUrl + "?" + me.dictionaryListServerParam + "=" + me.interactionField,
                    reader:{type:'json',root:me.objectRoot},
                    extractResponseData:me.changeStoreData
                },
                autoLoad:true,
                listeners:{
                    load:function(store,records,successful,eOpts){
                        if(records != null){
                            
                        }
                    }
                }
            })
        });
        
        items.push({
            xtype:'label',
            itemId:'combodataLabel',
            text:'定值数据设置',
            style:{fontWeight:'bold'}
        });
        items.push({
            xtype:'textarea',
            //padding:'4 0 0 0',
            anchor:'100%',
            width:445,
            height:110,
            emptyText :"输入的定值格式为:[['combobox','下拉框'],['textfield','文本框']]",
            name:me.combodata,
            itemId:me.combodata,
            value:me.combodataValue
            //grow:true
        });
//        if(me.initTypeChoose=='false'){
//            items.concat(partOneArr);
//            items.concat(partTwoArr);
//        }else{
//            items.concat(partTwoArr);
//            items.concat(partOneArr);
//        }
		return items;
	},
    interactionField:'',//
    /**
     * 数据对象内容的显示字段
     * @type String
     */
    objectPropertyDisplayField:'text',
    /**
     * 数据对象内容的值字段
     * @type String
     */
    objectPropertyValueField:'InteractionField',
    /**
     * 数据对象内容字段数组
     * @type 
     */
    objectPropertyFields:['text','InteractionField','RightID','leaf','icon','Tree','tid','checked','FieldClass'],

    /**
     * 数据服务列表的显示字段
     * @type String
     */
    objectServerDisplayField:'CName',
    /**
     * 数据服务列表的值字段
     * @type String
     */
    objectServerValueField:'ServerUrl',
    /**
     * 数据服务列表字段数组
     * @type 
     */
    objectServerFields:['CName','ServerUrl'],
    /**
     * 查询对象属性所属字典服务列表的服务地址
     * @type 
     */
    dictionaryListServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName',
    objectRoot:'ResultDataValue',
    /**
     * 查询对象属性所属字典服务列表时后台接收的参数名称
     * @type String
     */
    dictionaryListServerParam:'EntityPropertynName',
     /**
     * 数据适配
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreData: function(response){
        var data = Ext.JSON.decode(response.responseText);
        var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
        data.ResultDataValue = ResultDataValue;
        response.responseText = Ext.JSON.encode(data);
        return response;
    },

	/**
	 * 创建按钮组
	 * @private
	 * @return {}
	 */
	createButtons:function(){
		var me = this;
		var items = [];
		//确定按钮
		items.push({
			xtype:'button',
			itemId:'ok',
			text:'确定',
			width:50,
			x:(me.comWidth*2-100),
			y:(25*6+102),
			handler:function(but){
				var value = me.getAllValues();
				me.fireEvent('okClick',value);
			}
		});
		//重置按钮
		items.push({
			xtype:'button',
			itemId:'reset',
			text:'重置',
			width:50,
			x:(me.comWidth*2-40),
			y:(25*6+102),
			handler:function(but){
				me.reset();
				me.fireEvent('resetClick');
			}
		});
		return items;
	},

	//=====================监听=======================
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){},
	//=====================私有方法=======================
	
	//=====================对外公开方法=======================
	reset:function(){
		var me = this;
		me.getComponent(me.valueField).setValue(me.valueFieldValue);
		me.getComponent(me.textField).setValue(me.textFieldValue);
		me.getComponent(me.comboboxServerUrl).setValue(me.comboboxServerUrlValue);
        me.getComponent(me.combodata).setValue(me.combodataValue);
	},
	/**
	 * 所有属性赋值
	 * @private
	 * @param {} obj
	 */
	setAllValues:function(obj){
		var me = this;
		for(var i in obj){
			var com = me.getComponent(i);
			if(com){
				if(i=='typeChoose'){
			        var isnn2="'"+obj.typeChoose+"'";
			        var valuesnn2="{typeChoose:["+isnn2+"]}";
			        var mynnJson2=Ext.decode(valuesnn2);
			        com.setValue(mynnJson2);
                }else{
                    com.setValue(obj[i]);
                }
			}
		}
        var value=obj.typeChoose;
        me.setComShow(value);
	},
	/**
	 * 获取所有属性值
	 * @public
	 * @return {}
	 */
	getAllValues:function(){
		var me = this;
		var valuesStr = me.getAllValuesStr();
		var values = eval("(" + valuesStr + ")");
		return values;
	},
	/**
	 * 获取属性值字符串
	 * @public
	 * @return {}
	 */
	getAllValuesStr:function(){;
		var me = this;
        var tempUrl=me.getComponent(me.comboboxServerUrl).getValue();
        if(tempUrl!=null&&tempUrl.length>0){
            tempUrl="'"+tempUrl.split('?')[0]+"'";
        }else{
            tempUrl='';
        }
        //类型选择:后台服务--false,定值:---true
        var typeChoose=""+me.getComponent('typeChoose').getValue().typeChoose;
        var combodata='';
        combodata=""+me.getComponent(me.combodata).getValue();
        var valuesStr = "{";
        valuesStr +="typeChoose:" + typeChoose + ",";
        //定值:---true
        if(typeChoose!=null&&typeChoose=='true'){
            valuesStr += me.valueField + ":'',";
            valuesStr += me.textField + ":'',";
        }else{//后台服务
            //值字段的处理
            var tempValueStr=me.getComponent(me.valueField).getValue();
            var tempValueArr=[];
            var valueField='';
            if(tempValueStr.length>0){
                tempValueArr=tempValueStr.split('_');
                if(tempValueArr.length>2){
                    valueField="'"+(tempValueArr[tempValueArr.length-2]+'_'+tempValueArr[tempValueArr.length-1])+"'" ;
                }else{
                    valueField="'"+tempValueStr+"'";
                }
            }
            //显示字段的处理
            var temptextStr=me.getComponent(me.textField).getValue();
            var temptextArr=[];
            var textField='';
            if(temptextStr.length>0){
                temptextArr=temptextStr.split('_');
                if(tempValueArr.length>2){
                    textField="'"+(temptextArr[temptextArr.length-2]+'_'+temptextArr[temptextArr.length-1])+"'" ;
                }else{
                    textField="'"+temptextStr+"'";
                }
            }
            alert(temptextStr+tempValueStr);
            valuesStr += me.valueField + ":" + valueField+ ",";
            valuesStr += me.textField + ":" +textField+ ",";
        }
		
        if(tempUrl==""){
		    valuesStr += me.comboboxServerUrl + ":'',";
        }else{
            valuesStr += me.comboboxServerUrl + ":" + tempUrl + ",";
        }
        if(combodata.length>0)
        {
            valuesStr += me.combodata + ":"+'"'+combodata+'"';
        }else{
            valuesStr += me.combodata + ":''";
        }
        
		valuesStr += "}";
		return valuesStr;
	}
});