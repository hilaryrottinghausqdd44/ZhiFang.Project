/**
 * 表单解析器
 * @version 1.0
 * @author Jcall
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ParserForm',{
	extend:'Ext.build.ParserBase',
	/**解析器版本号*/
	version:'ParserForm 1.0.0',
	/**公开的操作列表*/
	operateList:[],
	/**是否有数据项名称*/
	hasLab:true,
	/**
	 * 解析方法
	 * @public
	 * @param {} DesignCode
	 * @return {}
	 */
	resolve:function(DesignCode){
		var me = this;
		if(!DesignCode){return null;}
		var appParams = me.getDesignCode(DesignCode);
		me.panelParams = appParams.panelParams || {};
		me.southParams = appParams.southParams || [];
		me.south2Params = appParams.south2Params || [];
		me.hasLab = (me.panelParams.hasLab + '' == 'true' ? true :false);
		var ClassCode = me.getClassCode();
		return ClassCode;
	},
	/**
	 * 应用参数处理
	 * @private
	 * @param {} appInfo
	 */
	getAppParams:function(appInfo){
		var me = this;
		var id = appInfo.Id + "";
		var list = [];
		if(id == "" || id == "-1"){//新增
			list = me.operateList;
		}else{
			var list = [];
			var oList = Ext.clone(appInfo.BTDAppComponentsOperateList);
			for(var i in me.operateList){
				var bo = false;
				for(var j in oList){
					if(me.operateList[i]['AppComOperateKeyWord'] == oList[j]['AppComOperateKeyWord']){
						oList[j]['DataTimeStamp'] = oList[j]['DataTimeStamp'].split(',');
						list.push(oList[j]);
						bo = true;break;
					}
				}
				if(!bo){
					list.push(me.operateList[i]);
				}
			}
		}
		appInfo.BTDAppComponentsOperateList = list && list.length > 0 ? me.changeString(list) : null;
		return appInfo;
	},
	/**
	 * 获取类代码
	 * @private
	 */
	getClassCode:function(){
		var me = this;
		var params = me.panelParams;
		
		var ClassCode = 
		"Ext.define('" + params.appCode + "',{" + 
            "extend:'Ext.zhifangux.FormPanel'," + 
            "alias:'widget." + params.appCode + "'," + 
            "title:'" + (params.titleText || "") + "'," + 
            "defaultTitle:'" + (params.titleText || "") + "'," + //原始标题
            "width:" + params.Width + "," + 
            "height:" + params.Height + "," + 
            "objectName:'" + params.objectName +"'," + //对象名，用于自动主键匹配
            "addUrl:'" + (params.addDataServerUrl || "") + "'," + //新增服务名
            "editUrl:'" + (params.editDataServerUrl || "") + "'," + //修改服务名
            "selectUrl:'" + (params.getDataServerUrl ? params.getDataServerUrl.split('?')[0] : "") + "'," + //获取数据服务名
            "type:'" + (params.defaultType || "show") + "'," + //显示方式add（新增）、edit（修改）、show（查看）
            "bgFielName:'" + (params.formHtml || "") + "'," + //背景图名称
            //"bodyCls:"+"'"+params.panelStyle+"'," + 
            //=============================================
            "initComponent:function(){" + 
                "var me=this;" + 
                "if(me.type=='show'){me.height -= 25;}" + 
                //"me.initBasicEvents();" + 
                "me.fields='" + me.createFields() + "';" + 
                "me.items=" + me.createItems() + ";";
				//挂靠
				var dockedItems = me.createDockedItems();
				ClassCode = ClassCode + (dockedItems && dockedItems != '' ? ("me.dockedItems=" + dockedItems + ";") : "");
				
				ClassCode = ClassCode + "me.changeConfig();me.callParent(arguments);" + 
			"}" + 
		"});";
		
		//宏映射转化
		ClassCode = me.changeMapping(ClassCode);
		
		return ClassCode;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var params = me.panelParams;
		var buttons = [];
        if(params.hasDefault1Button){//自定义一
        	var but = "{xtype:'button',iconCls:'build-button-configuration-blue',text:'" + params.Default1ButtonText + "',handler:function(but){me.fireEvent('default1buttonClick',but);}}";
        	buttons.push(but);
        }
        if(params.hasDefault2Button){//自定义二
        	var but = "{xtype:'button',iconCls:'build-button-configuration-blue',text:'" + params.Default2ButtonText + "',handler:function(but){me.fireEvent('default2buttonClick',but);}}";
        	buttons.push(but);
        }
        if(params.hasDefault3Button){//自定义三
        	var but = "{xtype:'button',iconCls:'build-button-configuration-blue',text:'" + params.Default3ButtonText + "',handler:function(but){me.fireEvent('default3buttonClick',but);}}";
        	buttons.push(but);
        }
  		buttons.push("'->'");
        if(params.hasSaveButton){//保存按钮
        	var but = "{xtype:'button',text:'保存',iconCls:'build-button-save',handler:function(but){me.submit(but);}}";
        	buttons.push(but);
        }
        if(params.hasResetButton){//重置按钮
        	var but = "{xtype:'button',text:'重置',iconCls:'build-button-refresh',handler:function(but){me.getForm().reset();}}";
        	buttons.push(but);
        }
        
        var dockedItems = "";
        if(buttons.length > 1){
        	dockedItems = 
            "[{" + 
                "xtype:'toolbar'," + 
                "dock:'bottom'," + 
                "itemId:'bottomtoolbar'," + 
                "items:[" + buttons.join(",") + "]" + 
            "}]";
        }
        return dockedItems;
	},
	/**
	 * 创建所有的数据项
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		//数据项
		var southParams = me.southParams;
		//按光标顺序排序
        for(var i=1;i<southParams.length;i++){
            for(var j=0;j<southParams.length-i;j++){
                if(southParams[j]['sortNum'] > southParams[j]['sortNum']){//比较交换相邻元素
                    var temp = southParams[j];
                    southParams[j] = southParams[j+1];
                    southParams[j+1] = temp; 
                } 
            } 
        }
        var items = [];
        for(var o in southParams){
        	items.push(me.createCom(southParams[o]));
        }
        //自定义按钮
        var south2Params = me.south2Params;
        for(var o in south2Params){
        	items.push(me.createCustomButton(south2Params[o]));
        }
        
        return "[" + items.join(",") + "]";
	},
	/**
	 * 根据参数创建数据项
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createCom:function(config){
		var me = this;
        var com = null;
        var type = config['Type'];
        
        if(type == 'textfield'){//文本框
            com = me.createTextCom(config);
        }else if(type == 'textareafield'){//文本域
        	com = me.createTextAreaCom(config);
        }else if(type == 'numberfield'){//数字框
        	com = me.createNumberCom(config);
        }else if(type == 'datefield'){//日期框
            com = me.createDateCom(config);
        }else if(type == 'timefield'){//时间框
            com = me.createTimeCom(config);
        }else if(type == 'datetimenew'){//日期时间框
            com = me.createDateTimeCom(config);
        }else if(type == 'combobox'){//下拉框
            com = me.createComboCom(config);
        }else if(type == 'datacombobox'){//定值下拉框
            com = me.createDataComboCom(config);
        }else if(type == 'label'){//纯文本
            com = me.createLabelCom(config);
        }else if(type == 'radiogroup'){//单选组
            com = me.createRadiogroupCom(config);
        }else if(type == 'dataradiogroup'){//定值单选组
            com = me.createDataRadiogroupCom(config);
        }else if(type == 'displayfield'){//displayfield
            com = me.createDisplayfieldCom(config);
        }else if(type == 'colorscombobox'){//颜色选择器
            com = me.createColorscomboboxCom(config);
        }else if(type == 'checkboxfield'){//布尔勾选框
            com = me.createSettingCheckBoxCom(config);
        }else if(type == 'comboboxbut'){//带按钮的只读下拉框
            com = me.createComboboxbutCom(config);
        }
        
        return com;
	},
	/**
	 * 创建数据项公共属性
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createBasicCom:function(config){
		var me = this;
		var com = 
			"type:'" + config['Type'] + "'," + 
			"itemId:'" + config['InteractionField'] + "'," + 
            "name:'" + config['InteractionField'] + "'," + 
            "labelWidth:" + config['LabelWidth'] + "," + 
            "height:" + config['Height']+"," + 
            "x:" + config['X'] + "," + 
            "y:" + config['Y'] + "," + 
            "readOnly:" + config['IsReadOnly'] + "," + 
            "labelStyle:'" + (config['LabFont'] != "" ? config['LabFont'] : "font-style:normal") + "'," + 
            "fieldLabel:'" + (me.hasLab ? config['DisplayName'] : "") + "'," + 
            "labelAlign:'" + (config['AlignType'] || "right") + "'," + 
            "sortNum:" + config['sortNum'] + "," + 
            "hasReadOnly:" + config['IsReadOnly'] + "," + 
            "hidden:" + config['IsHidden'];
        
        if(config['Type'] == 'comboboxbut'){//功能按钮的处理
        	com += ",width:" + config['Width'] + "-23";
        }else{
        	com += ",width:" + config['Width'];
        }
        
        if(config['defaultValue'] && config['defaultValue'] != "" && config['defaultValue'] != "undefined"){
        	com += ",value:'" + config['defaultValue'] + "'";
        }
        
        if(config['Type'] == 'combobox'){//下拉框监听
        	com += ",listeners:me.createComboListeners()";
        }else if(config['Type'] == 'radiogroup'){//单选组监听
        	com += ",listeners:me.createRadiogroupListeners()";
        }else{//基础监听
        	com += ",listeners:me.createBasicListeners()";
        }
        
		return com;
	},
	/**
	 * 创建文本组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createTextCom:function(config){
		var me = this;
		var com = "{xtype:'textfield',";
		
		//必填
        if(config.NotNull){
        	com += "allowBlank:false,";
        }
        //空值提示
        if(config.EmptyText){
        	com += "emptyText:'" + config['EmptyText'] + "',";
        }
		
		com += me.createBasicCom(config) + "}";
		
		return com;
	},
	/**
	 * 创建文本域组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createTextAreaCom:function(config){
		var me = this;
		var com = "{xtype:'textarea',";
		
		//必填
        if(config.NotNull){
        	com += "allowBlank:false,";
        }
        //空值提示
        if(config.EmptyText){
        	com += "emptyText:'" + config['EmptyText'] + "',";
        }
		
		com += me.createBasicCom(config) + "}";
		
		return com;
	},
	/**
	 * 创建数字组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createNumberCom:function(config){
		var me = this;
		var com = "{xtype:'numberfield',";
		
		if(config['NumberMin'] && config['NumberMin'] != ""){//最大值
			com += "minValue:" + config['NumberMin'] + ",";
		}
		if(config['NumberMax'] && config['NumberMax'] != ""){//最小值
			com += "maxValue:" + config['NumberMax'] + ",";
		}
		if(config['NumberIncremental'] && config['NumberIncremental'] != ""){//步距
			com += "step:" + config['NumberIncremental'] + ",";
		}
		com += me.createBasicCom(config) + "}";
		
		return com;
	},
	/**
	 * 创建日期组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createDateCom:function(config){
		var me = this;
		var com = "Ext.create('Ext.zhifangux.DateField',{";
		com += "editable:" + config['CanEdit'] + ",";//是否允许编辑

		if(config.ShowFomart && config.ShowFomart != ""){
			com += "format:'" + config.ShowFomart + "'," ;
		}
		
		com += me.createBasicCom(config) + "})";
		return com;
	},
	/**
	 * 创建时间组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createTimeCom:function(config){
		var me = this;
		var com = "Ext.create('Ext.zhifangux.TimeField',{";
		com += "selectOnFocus:true,";
		com += "editable:" + config['CanEdit'] + ",";//是否允许编辑
		
		if(config.ShowFomart && config.ShowFomart != ""){
			com += "format:'" + config.ShowFomart + "'," ;
		}
		
		com += me.createBasicCom(config) + "})";
		return com;
	},
	/**
	 * 创建日期时间组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createDateTimeCom:function(config){
		var me = this;
		var com = "Ext.create('Ext.zhifangux.DateTimeNew',{";
		com += "selectOnFocus:true,";
		com += "editable:" + config['CanEdit'] + ",";//是否允许编辑
		
		if(config.ShowFomart && config.ShowFomart != ""){
			com += "format:'" + config.ShowFomart + "'," ;
		}
		
		com += me.createBasicCom(config) + "})";
		return com;
	},
	/**
	 * 创建下拉框组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createComboCom:function(config){
		var me = this,
			textField = config['textField'],
			valueField = config['valueField'],
			InteractionField = config['InteractionField'],
			DataTimeStamp = textField.split("_").slice(0,-1).join("_") + "_DataTimeStamp",
			DataTimeStampField = InteractionField.split("_").slice(0,-1).join("_") + "_DataTimeStamp",
			url = config['ServerUrl'].split("?")[0] + "?isPlanish=true",
			fields = textField + "," + valueField + "," + DataTimeStamp;
			
		if(config.Where){
			url += "&where=" + config.Where;
		}
		
		var com = 
		"{xtype:'combobox'," + 
			"editable:true,typeAhead:true," + 
			"queryMode:'local'," + 
			"defaultValue:'" + config['defaultValue'] + "'," + 
			"displayField:'" + config['textField'] + "'," + 
        	"valueField:'" + config['valueField'] + "'," + 
        	"DataTimeStampField:'" + DataTimeStampField + "'," + 
        	"store:me.createComboStore({" + 
        		"fields:'" + fields + "'," + 
        		"url:'" + url + "'," + 
        		"InteractionField:'" + InteractionField + "'," + 
        		"DataTimeStampField:'" + DataTimeStampField + "'," + 
        		"valueField:'" + valueField + "'" + 
        	"}),";
        	
        //必填
        if(config.NotNull){
        	com += "allowBlank:false,";
        }
        //空值提示
        if(config.EmptyText){
        	com += "emptyText:'" + config['EmptyText'] + "',";
        }
        	
		com += me.createBasicCom(config) + "}";
		return com;
	},
	/**
	 * 创建定值下拉框
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createDataComboCom:function(config){
		var me = this;
		var com = 
		"{xtype:'combobox'," + 
			"hasButton:" + (config['isFunctionBtn'] ? true : false) + "," + 
			"mode:'local'," + 
            "editable:false," +  
            "displayField:'text'," + 
            "valueField:'value'," + 
            "store:new Ext.data.SimpleStore({" + 
                "fields:['value','text']," + 
                "data:" + (config['combodata'] || "[]") + 
            "}),";
            
        //必填
        if(config.NotNull){
        	com += "allowBlank:false,";
        }
        //空值提示
        if(config.EmptyText){
        	com += "emptyText:'" + config['EmptyText'] + "',";
        }
		    
        com += me.createBasicCom(config) + "}";
        
        if(config['isFunctionBtn']){
        	com += 
        	",{xtype:'button',width:22,height:22," + 
        		"itemId:'" + config['functionBtnId'] + "'," + 
        		"name:'" + config['functionBtnId'] + "'," + 
	            "disabled :" + config['IsReadOnly'] + "," + 
	            "x:" + config['btnX'] + "," + 
	            "y:" + config['btnY'] + "," + 
	            "boundField:'" + config['boundField'] + "'," + 
	            "appComID:'" + config['appComID'] + "'," + 
	            "iconCls:'build-button-configuration-blue'," + 
	            "margin:'0 0 0 2'," + 
	            "handler:function(com){" + 
                    "me.funBtnClick(com);" + 
                "}" + 
			"}";
        }
        
		return com;
	},
	/**
	 * 创建纯文本组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createLabelCom:function(config){
		var me = this;
		var com = 
        "{" + 
        	"xtype:'label'," + 
        	"type:'label'," + 
			"itemId:'" + config['InteractionField'] + "-fieldLabel'," + 
	        "name:'" + config['InteractionField'] + "-fieldLabel'," + 
	        "width:" + config['LabelWidth'] + "," + 
	        "height:" + config['Height']+"," + 
	        "x:" + config['X'] + "," + 
	        "y:" + config['Y'] + "," + 
	        "labelStyle:'" + (config['LabFont'] != "" ? config['LabFont'] : "font-style:normal") + "'," + 
	        "text:'" + (me.hasLab ? config['DisplayName'] : "") + "'," + 
	        "hidden:" + config['IsHidden'] +  
        "},{" + 
        	"xtype:'label'," + 
        	"type:'label'," + 
			"itemId:'" + config['InteractionField'] + "'," + 
	        "name:'" + config['InteractionField'] + "'," + 
	        "width:" + (parseInt(config['Width']) - parseInt(config['LabelWidth'])) + "," + 
	        "height:" + config['Height']+"," + 
	        "x:" + (parseInt(config['X']) + parseInt(config['LabelWidth'])) + "," + 
	        "y:" + config['Y'] + "," + 
	        "hidden:" + config['IsHidden'] + 
        "}";
        return com;
	},
	/**
	 * 创建单选组组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createRadiogroupCom:function(config){
		var me = this,
			valueField = config['valueField'],
        	textField = config['textField'],
        	DataTimeStampField = config['InteractionField'].split("_").slice(0,-1).join("_") + "_DataTimeStamp",
        	url = config['ServerUrl'].split("?")[0] + "?isPlanish=true";
        	
		var com = 
		"{xtype:'radiogroup',vertical:true,padding:2,autoScroll:true,isdataValue:false," + 
			"columnWidth:"+ config['columnWidth'] + "," + 
        	"columns:" + config['Columns'] + "," + 
        	"DataTimeStampField:'" + DataTimeStampField + "'," + 
	       	"store:me.createRadiogroupStore({" + 
	       		"itemId:'" + config['InteractionField'] + "'," + 
	        	"defaultValue:'" + config['defaultValue'] + "'," + 
	        	"url:'" + url + "'," + 
	        	"valueField:'" + valueField + "'," + 
	        	"displayField:'" + textField + "'," + 
	        	"DataTimeStampField:'" + DataTimeStampField + "'" + 
	       	"}),";
       	
        com += me.createBasicCom(config) + "}";
		return com;
	},
	/**
	 * 创建定值单选组
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createDataRadiogroupCom:function(config){
		var me = this;
		var com = "{xtype:'radiogroup',vertical:true,padding:2,autoScroll:true,isdataValue:true,";
		com += "columnWidth:"+ config['columnWidth'] + ",";
        com += "columns:" + config['Columns'] + ",";
        
        com += "itemId:'" + config['InteractionField'] + "',";
        com += "items:" + (config['combodata'] == "" ? "[]" : config['combodata'].replace(/\"/g,"'")) + ",";
        
        com += me.createBasicCom(config) + "}";
		return com;
	},
	/**
	 * 创建文本内容组件
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createDisplayfieldCom:function(config){
		var me = this;
		var com = "{xtype:'displayfield',";
		com += "value:'" + config['defaultValue'] + "',";
		com += me.createBasicCom(config) + "}";
		return com;
	},
	/**
	 * 创建颜色选择器
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createColorscomboboxCom:function(config){
		var me = this;
		var com = "Ext.create('Ext.zhifangux.colorscombobox',{";
		com += "forceSelection:true,";
		com += "minWidth:" + (config['minWidth'] + "" == "0" ? "140" : config['minWidth']) + ",";
		com += "maxHeight" + (config['maxHeight'] + "" == "0" ? "200" : config['maxHeight']) + ",";
		com += me.createBasicCom(config) + "})";
		return com;
	},
	/**
	 * 创建布尔勾选框
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createSettingCheckBoxCom:function(config){
		var me = this;
		var com = "{xtype:'checkbox',boxLabel:'',inputValue:'true',uncheckedValue:'false',";
		com += "checked:" + (config['defaultValue'] + "" == "true" ? "true" : "false") + ",";
		com += me.createBasicCom(config) + "}";
		return com;
	},
	/**
	 * 创建带按钮的定值下拉框
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createComboboxbutCom:function(config){
		var me = this;
		var con = Ext.clone(config);
		con.IsReadOnly = true;
		var com = 
		"{xtype:'combobox'," + 
			"hasButton:true," + 
			"mode:'local'," + 
            "editable:false," +  
            "displayField:'text'," + 
            "valueField:'value'," + 
            "store:new Ext.data.SimpleStore({" + 
                "fields:['value','text']," + 
                "data:[]" + 
            "}),";
        com += me.createBasicCom(con) + "}";
        
    	com += 
    	",{xtype:'button'," + 
    		"itemId:'" + config['InteractionField'] + "-button'," + 
            "disabled :" + config['IsReadOnly'] + "," + 
            "x:" + config['X'] + "+" + config['Width'] + "-22," + 
            "y:" + config['Y'] + "," + 
            "sortNum:0," + 
            "iconCls:'build-button-configuration-blue'," + 
            "hidden:" + config['IsHidden'] + "," + 
            "appComID:'" + config['appComID'] + "'," + 
            "textField:'" + config['textField'] + "'," + 
            "boundField:'" + config['InteractionField'] + "'," + 
            "listeners:{" + 
                "click:function(com,e,op){" + 
                    "me.funBtnListClick(com);" + 
                "}" + 
            "}" + 
		"}";
        
		return com;
	},
	/**
	 * 创建自定义按钮
	 * @private
	 * @param {} config
	 * @return {}
	 */
	createCustomButton:function(config){
		var me = this;
		var com = 
		"{" + 
			"xtype:'button'" + 
			",text:'" + config['buttonName'] + "'" + 
			",itemId:'" + config['buttonItemId'] + "'" + 
			",x:" + config['buttonX'] + 
			",y:" + config['buttonY'];
    		
    	if(config['buttonWidth'] && config['buttonWidth'] > 0){com += ",width:" + config['buttonWidth'];}
    	if(config['buttonHeight'] && config['buttonHeight'] > 0){com += ",height:" + config['buttonHeight'];}
    	if(config['openWin']){
    		if(config['openWinType'] == 'html'){
    			com += 
    			",handler:function(button,e){" + 
    				"openHtmlWin({" + 
    					"width:" + (config['openWinWidth'] || 600) + "," + 
    					"height:" + (config['openWinHeight'] || 300) + "," + 
    					"title:'" + (config['buttonName'] || "") + "'," + 
    					"url:'" + (config['openWinURL'] || "") + "'" + 
    				"},function(win){me.fireEvent('AfterOpenWin" + config['buttonItemId'] + "',win);});" + 
    			"}";
			}else{
				var con = [];
				if(config['openWinWidth'] && config['openWinWidth'] > 0){
					con.push("width:" + config['openWinWidth']);
				}
				if(config['openWinHeight'] && config['openWinHeight'] > 0){
					con.push("height:" + config['openWinHeight']);
				}
				if(config['buttonName'] && config['buttonName'] != ""){
					con.push("title:'" + config['buttonName'] + "'");
				}
    			com += 
    			",handler:function(button,e){" + 
    				"openFormWin('" + config['openWinAppId'] + "',{" + 
    					con.join(",") + 
    				"},function(win){me.fireEvent('AfterOpenWin" + config['buttonItemId'] + "',win);});" + 
    			"}";
			}
    	}
    	
    	com += "}";
		return com;
	},
	/**@private
	 * 创建需要的数据字段
	 * @return {}
	 */
	createFields:function(){
		var me = this;
		var southParams = me.southParams;
		var fields = "";
		
		for(var i in southParams){ 
			fields += southParams[i].InteractionField + ",";
		}
		fields = fields == "" ? "" : fields.slice(0,-1);
		
		return fields;
	},
	/**
	 * 宏映射转化
	 */
	changeMapping:function(value){
		var v = value,
			regex =  /('\{SYS_.*?\}')/g,
			arr = v.match(regex) || [],
			len = arr.length;
			
		for(var i=0;i<len;i++){
			v = v.replace(eval("/" + arr[i] + "/g"),"Shell.util.SysInfo.get" + arr[i].slice(2,-2) + "()");
		}
		
		return v;
	}
});