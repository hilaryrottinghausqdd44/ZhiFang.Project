/**
 * 自定义Display字段配置
 * 【可配参数】
 * 
 * 【对外方法】
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.CustomDisplayFieldSet',{
	extend:'Ext.form.FieldSet',
	alias:'widget.customdisplayfieldset',
	//=====================可配参数=======================
	type:'fieldset',//'win':窗口方式;'fieldset':表单框方式
	appId:-1,
	title:'自定义Display字段配置',
	buttonName:'DisplayName',
	buttonItemId:'InteractionField',
	buttonWidth:'Width',
	buttonHeight:'Height',
	buttonX:'X',
	buttonY:'Y',

	//linkConfig:'linkConfig',
    defaultValueItemId:'defaultValue',
	
	buttonNameValue:'',
	buttonItemIdValue:'',
	buttonWidthValue:'',
	buttonHeightValue:'',
	buttonXValue:0,
	buttonYValue:0,
    
	openWinValue:false,
	openWinTypeValue:'html',
	openWinURLValue:'',
	openWinAppNameValue:'',
	openWinAppIdValue:-1,
	openWinWidthValue:'',
	openWinHeightValue:'',
    
	//linkConfigValue:'',
	defaultValue:'',
    
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
		var secondColumn = me.createSecondColumn();
		var defaultValueArea = me.createDefaultValueArea();
		var buttons = me.createButtons();
		
		var num = 25;
		var fy = 0;
		for(var i in firstCloumn){
			firstCloumn[i].width = me.comWidth;
			firstCloumn[i].labelWidth = me.comLabelWidth;
			
			if(firstCloumn[i].name == me.buttonItemId){
				firstCloumn[i].x = me.comWidth + 10;
				firstCloumn[i].y = 0;
			}else{
				firstCloumn[i].x = 0;
				firstCloumn[i].y = fy;
				fy += num;
			}
		}
		fy = num;
		for(var i in secondColumn){
			secondColumn[i].width = me.comWidth;
			secondColumn[i].labelWidth = me.comLabelWidth;
			secondColumn[i].x = me.comWidth + 10;
			secondColumn[i].y = secondColumn[i].hidden ? fy-= num : fy;
			fy += num;
		}
		fy = num * 5;
		for(var i in defaultValueArea){
			defaultValueArea[i].width = me.comWidth * 2 + 10;
			defaultValueArea[i].x = 0;
			defaultValueArea[i].y = fy;
			//if(defaultValueArea[i].name == me.openWinHeight){
				defaultValueArea[i].height = 100;
				defaultValueArea[i].padding = 0;
			//}
			fy += num-2;
		}
		
		me.layout = "absolute";
		var items = firstCloumn.concat(secondColumn).concat(defaultValueArea).concat(buttons);
		return items;
	},
	/**
	 * 第一列数据
	 * @private
	 * @return {}
	 */
	createFirstCloumn:function(){
		var me = this;
		var items = [];
		//按钮名称
		items.push({
			xtype:'textfield',
			name:me.buttonName,
			itemId:me.buttonName,
			value:me.buttonNameValue,
			fieldLabel:'显示名称'
		});
		//按钮编号
		items.push({
			xtype:'textfield',
			name:me.buttonItemId,
			itemId:me.buttonItemId,
			value:me.buttonItemIdValue,
			fieldLabel:'内部编号'
		});
		//按钮宽度
		items.push({
			xtype:'numberfield',
			name:me.buttonWidth,
			itemId:me.buttonWidth,
			value:me.buttonWidthValue,
			emptyText:'默认',
			fieldLabel:'宽度'
		});
		//高度
		items.push({
			xtype:'numberfield',
			name:me.buttonHeight,
			itemId:me.buttonHeight,
			value:me.buttonHeightValue,
			emptyText:'默认',
			fieldLabel:'高度'
		});
		return items;
	},
	/**
	 * 第二列数据
	 * @private
	 * @return {}
	 */
	createSecondColumn:function(){
    
        var me = this;
        var items = [];
        //X轴
        items.push({
            xtype:'numberfield',
            name:me.buttonX,
            itemId:me.buttonX,
            value:me.buttonXValue,
            fieldLabel:'X轴'
        });
        //Y轴
        items.push({
            xtype:'numberfield',
            name:me.buttonY,
            itemId:me.buttonY,
            value:me.buttonYValue,
            fieldLabel:'Y轴'
        });
        return items;
    
    },
	/**
	 * 代码文本域
	 * @private
	 * @return {}
	 */
	createDefaultValueArea:function(){
		var me = this;
		var items = [];
		//代码文本域
		items.push({
			xtype:'label',
			text:'显示内容',
			style:{fontWeight:'bold'}
		});
		items.push({
			xtype:'htmleditor',
			padding:'4 0 0 0',
			name:me.defaultValueItemId,
			itemId:me.defaultValueItemId,
			value:me.defaultValue,
			grow:true,
			emptyText:'请输入需要的显示内容'
		});
		return items;
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
		me.getComponent(me.buttonName).setValue(me.buttonNameValue);
		me.getComponent(me.buttonItemId).setValue(me.buttonItemIdValue);
		me.getComponent(me.buttonWidth).setValue(me.buttonWidthValue);
		me.getComponent(me.buttonHeight).setValue(me.buttonHeightValue);
		me.getComponent(me.buttonX).setValue(me.buttonXValue);
		me.getComponent(me.buttonY).setValue(me.buttonYValue);
		me.getComponent(me.defaultValueItemId).setValue(me.defaultValue);
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
				com.setValue(obj[i]);
			}
		}
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
	getAllValuesStr:function(){
		var me = this;
		var valuesStr = "{";
		valuesStr += me.buttonName + ":'" + me.getComponent(me.buttonName).getValue() + "',";
		valuesStr += me.buttonItemId + ":'" + me.getComponent(me.buttonItemId).getValue() + "',";
        valuesStr +="Type:'displayfield',";
		valuesStr += me.buttonWidth + ":" + me.getComponent(me.buttonWidth).getValue() + ",";
		valuesStr += me.buttonHeight + ":" + me.getComponent(me.buttonHeight).getValue() + ",";
		valuesStr += me.buttonX + ":" + me.getComponent(me.buttonX).getValue() + ",";
		valuesStr += me.buttonY + ":" + me.getComponent(me.buttonY).getValue() + ",";
		//文本域中的处理
		var defaultValue = me.getComponent(me.defaultValueItemId).getValue();
		defaultValue = defaultValue.replace(/\n/g,"\\n");
		defaultValue = defaultValue.replace(/\\/g,"\\\\");
		defaultValue = defaultValue.replace(/'/g,"\\'");
		defaultValue = defaultValue.replace(/"/g,"\\'");
        
		valuesStr += me.defaultValueItemId + ":'" + defaultValue + "'";
		valuesStr += "}";
		return valuesStr;
	}
});