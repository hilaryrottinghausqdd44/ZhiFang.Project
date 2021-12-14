/**
 * 自定义按钮配置
 * 【可配参数】
 * 
 * 【对外方法】
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.CustomButtonFieldSet',{
	extend:'Ext.form.FieldSet',
	alias:'widget.custombuttonfieldset',
	//=====================可配参数=======================
	type:'fieldset',//'win':窗口方式;'fieldset':表单框方式
	appId:-1,
	title:'自定义按钮配置',
	buttonName:'buttonName',
	buttonItemId:'buttonItemId',
	buttonWidth:'buttonWidth',
	buttonHeight:'buttonHeight',
	buttonX:'buttonX',
	buttonY:'buttonY',
	openWin:'openWin',
	openWinType:'openWinType',
	openWinURL:'openWinURL',
	openWinAppName:'openWinAppName',
	openWinAppId:'openWinAppId',
	openWinWidth:'openWinWidth',
	openWinHeight:'openWinHeight',
	linkConfig:'linkConfig',
	
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
	linkConfigValue:'',
	
	comLabelWidth:60,
	comWidth:215,
	anchorValue:'100%',
	//=====================内部视图渲染=======================
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
		var linkConfigArea = me.createLinkConfigArea();
		
		var items = firstCloumn.concat(secondColumn).concat(linkConfigArea);
		
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
		var linkConfigArea = me.createLinkConfigArea();
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
		for(var i in linkConfigArea){
			linkConfigArea[i].width = me.comWidth * 2 + 10;
			linkConfigArea[i].x = 0;
			linkConfigArea[i].y = fy;
			if(linkConfigArea[i].name == me.openWinHeight){
				linkConfigArea[i].height = 100;
				linkConfigArea[i].padding = 0;
			}
			fy += num-2;
		}
		
		me.layout = "absolute";
		var items = firstCloumn.concat(secondColumn).concat(linkConfigArea).concat(buttons);
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
			fieldLabel:'按钮名称'
		});
		//按钮编号
		items.push({
			xtype:'textfield',
			name:me.buttonItemId,
			itemId:me.buttonItemId,
			value:me.buttonItemIdValue,
			fieldLabel:'按钮编号'
		});
		//按钮宽度
		items.push({
			xtype:'numberfield',
			name:me.buttonWidth,
			itemId:me.buttonWidth,
			value:me.buttonWidthValue,
			emptyText:'默认',
			fieldLabel:'按钮宽度'
		});
		//按钮高度
		items.push({
			xtype:'numberfield',
			name:me.buttonHeight,
			itemId:me.buttonHeight,
			value:me.buttonHeightValue,
			emptyText:'默认',
			fieldLabel:'按钮高度'
		});
		//按钮X轴
		items.push({
			xtype:'numberfield',
			name:me.buttonX,
			itemId:me.buttonX,
			value:me.buttonXValue,
			fieldLabel:'按钮X轴'
		});
		//按钮Y轴
		items.push({
			xtype:'numberfield',
			name:me.buttonY,
			itemId:me.buttonY,
			value:me.buttonYValue,
			fieldLabel:'按钮Y轴'
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
		//弹出窗口-类型
		items.push({
			xtype:'fieldcontainer',layout:'hbox',itemId:'openWin',padding:0,margin:0,
			items:[{
				xtype:'checkbox',boxLabel:'弹出窗口',width:70,style:'fontWeight:bold;',
				itemId:me.openWin,name:me.openWin,value:me.openWinValue
			},{	
				xtype:'radiogroup',itemId:me.openWinType,name:me.openWinType,
	            columns:2,vertical:true,width:100,value:me.openWinTypeValue,
	            items:[{
	            	boxLabel:'URL',name:'openWinType',inputValue:'html',checked:true
	            },{
	            	boxLabel:'应用',name:'openWinType',inputValue:'app'
	            }]
			}]
		});
		//弹出窗口-URL
		items.push({
			xtype:'textfield',
			name:me.openWinURL,
			itemId:me.openWinURL,
			value:me.openWinURLValue,
			fieldLabel:'链接路径'
		});
		//弹出窗口-应用选择(默认隐藏)
		items.push({
			xtype:'fieldcontainer',layout:'hbox',
			itemId:'openWinApp',hidden:true,
			items:[{
				xtype:'textfield',readOnly:true,
				name:me.openWinAppName,
				itemId:me.openWinAppName,
				labelWidth:me.comLabelWidth,
				width:me.comWidth - 24,
				value:me.openWinAppNameValue,
				fieldLabel:'应用名称'
			},{
				xtype:'button',iconCls:'build-button-configuration-blue',
				tooltip:'选择应用',margin:'0 0 0 2',
				itemId:'openWinApp-button',name:'openWinApp-button'
			}]
		});
		//弹出窗口-应用ID(不可见)
		items.push({
			xtype:'textfield',
			name:me.openWinAppId,
			itemId:me.openWinAppId,
			fieldLabel:'应用ID',
			value:me.openWinAppIdValue,
			hidden:true
		});
		//弹出窗口-宽度
		items.push({
			xtype:'numberfield',
			name:me.openWinWidth,
			itemId:me.openWinWidth,
			value:me.openWinWidthValue,
			emptyText:'默认',
			fieldLabel:'窗口宽度'
		});
		//弹出窗口-高度
		items.push({
			xtype:'numberfield',
			name:me.openWinHeight,
			itemId:me.openWinHeight,
			value:me.openWinHeightValue,
			emptyText:'默认',
			fieldLabel:'窗口高度'
		});
		return items;
	},
	/**
	 * 代码文本域
	 * @private
	 * @return {}
	 */
	createLinkConfigArea:function(){
		var me = this;
		var items = [];
		//代码文本域
		items.push({
			xtype:'label',
			text:'内嵌代码',
			style:{fontWeight:'bold'}
		});
		items.push({
			xtype:'textarea',
			padding:'4 0 0 0',
			name:me.linkConfig,
			itemId:me.linkConfig,
			value:me.linkConfigValue,
			grow:true,
			emptyText:'代码直接追加到按钮代码中'
		});
		//return items;
		return [];
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
	/**
	 * 打开应用列表窗口
	 * @private
	 */
	openAppListWin:function(){
		var me = this;
		var appList = Ext.create('Ext.build.AppListPanel',{
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:500,
			height:300,
			filterId:me.appId,
			defaultLoad:true,
			readOnly:true,
			pageSize:9//每页数量
    	}).show();
    	appList.on({
    		okClick:function(){
    			var records = appList.getSelectionModel().getSelection();
    			if(records.length == 0){
    				alertError("请选择一个应用！");
    			}else if(records.length == 1){
    				me.setWinformInfo(records[0]);
    				appList.close();//关闭应用列表窗口
    			}
    		},
    		itemdblclick:function(view,record,tem,index,e,eOpts){
		    	me.setWinformInfo(record);
    			appList.close();//关闭应用列表窗口
    		}
    	});
	},
	/**
	 * 设置弹出应用的属性
	 * @private
	 * @param {} record
	 */
	setWinformInfo:function(record){
		var me = this;
		var openWinAppName = me.getComponent('openWinApp').getComponent(me.openWinAppName);
		var openWinAppId = me.getComponent(me.openWinAppId);
		openWinAppName.setValue(record.get('BTDAppComponents_CName'));
		openWinAppId.setValue(record.get('BTDAppComponents_Id'));
		
		openWinAppName.tooltip = record.get('BTDAppComponents_ModuleOperInfo');
	},
	//=====================监听=======================
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		//弹出窗口-类型变化监听
		var winType = me.getComponent('openWin').getComponent(me.openWinType);
		winType.on({
        	change:function(field,newValue,oldValue){
        		if(newValue.openWinType == 'html'){
        			me.getComponent('openWinApp').hide();
        			me.getComponent(me.openWinURL).show();
        		}else{
        			me.getComponent(me.openWinURL).hide();
        			me.getComponent('openWinApp').show();
        		}
        	}
        });
        //弹出应用列表监听
        var openWinApp = me.getComponent('openWinApp');
        var openWinAppButton = openWinApp.getComponent('openWinApp-button');
        openWinAppButton.on({
        	click:function(but){
        		me.openAppListWin();
        	}
        });
        //应用名称
        var openWinAppName = me.getComponent('openWinApp').getComponent(me.openWinAppName);
        var tooltip = null;
        openWinAppName.on({
        	mouseover:{
        		element:'el',
        		fn:function(e,t,eOpts){
        			tooltip = Ext.create('Ext.tip.ToolTip',{
	        			html:"<b>应用名称：<font color='blue'>" + (openWinAppName.value || "无") + 
	        			"</font><br>功能简介：</b>" + (openWinAppName.tooltip || "无")
	        		});
	        		tooltip.showAt(e.getXY());//让右键菜单跟随鼠标位置
        		}
        	},
        	mouseout:{
        		element:'el',
        		fn:function(e,t,eOpts){
        			tooltip.hide();
        			tooltip = null;
        		}
        	}
        });
	},
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
		me.getComponent('openWin').getComponent(me.openWin).setValue(me.openWinValue);
		me.getComponent('openWin').getComponent(me.openWinType).setValue({openWinType:me.openWinTypeValue});
		me.getComponent(me.openWinURL).setValue(me.openWinURLValue);
		me.getComponent('openWinApp').getComponent(me.openWinAppName).setValue(me.openWinAppNameValue);
		me.getComponent('openWinApp').getComponent(me.openWinAppName).tooltip = null;
		me.getComponent(me.openWinAppId).setValue(me.openWinAppIdValue);
		me.getComponent(me.openWinWidth).setValue(me.openWinWidthValue);
		me.getComponent(me.openWinHeight).setValue(me.openWinHeightValue);
		me.getComponent(me.linkConfig).setValue(me.linkConfigValue);
	},
	/**
	 * 所有属性赋值
	 * @private
	 * @param {} obj
	 */
	setAllValues:function(obj){
		var me = this;
		for(var i in obj){
			if(i == me.openWin){
				me.getComponent('openWin').getComponent(me.openWin).setValue(obj[i]);
			}else if(i == me.openWinType){
				me.getComponent('openWin').getComponent(me.openWinType).setValue({openWinType:obj[i]});
			}else if(i == me.openWinAppName){
				me.getComponent('openWinApp').getComponent(me.openWinAppName).setValue(obj[i]);
			}else{
				var com = me.getComponent(i);
				if(com){
					com.setValue(obj[i]);
				}
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
		valuesStr += me.buttonWidth + ":" + me.getComponent(me.buttonWidth).getValue() + ",";
		valuesStr += me.buttonHeight + ":" + me.getComponent(me.buttonHeight).getValue() + ",";
		valuesStr += me.buttonX + ":" + me.getComponent(me.buttonX).getValue() + ",";
		valuesStr += me.buttonY + ":" + me.getComponent(me.buttonY).getValue() + ",";
		valuesStr += me.openWin + ":" + me.getComponent('openWin').getComponent(me.openWin).getValue() + ",";
		valuesStr += me.openWinType + ":'" + me.getComponent('openWin').getComponent(me.openWinType).getValue().openWinType + "',";
		valuesStr += me.openWinURL + ":'" + me.getComponent(me.openWinURL).getValue() + "',";
		valuesStr += me.openWinAppName + ":'" + me.getComponent('openWinApp').getComponent(me.openWinAppName).getValue() + "',";
		valuesStr += me.openWinAppId + ":'" + me.getComponent(me.openWinAppId).getValue() + "',";
		valuesStr += me.openWinWidth + ":" + me.getComponent(me.openWinWidth).getValue() + ",";
		valuesStr += me.openWinHeight + ":" + me.getComponent(me.openWinHeight).getValue() + ",";
		
		//文本域中的处理
		var linkConfigValue = me.getComponent(me.linkConfig).getValue();
		linkConfigValue = linkConfigValue.replace(/\n/g,"\\n");
		linkConfigValue = linkConfigValue.replace(/\\/g,"\\\\");
		linkConfigValue = linkConfigValue.replace(/'/g,"\\'");
		
		valuesStr += me.linkConfig + ":'" + linkConfigValue + "'";
		valuesStr += "}";
		return valuesStr;
	}
});