/**
 * 列表解析器
 * @version 1.0
 * @author Jcall
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ParserList',{
	extend:'Ext.build.ParserBase',
	/**解析器版本号*/
	version:'ParserList 1.0.0',
	/**公开的操作列表*/
	operateList:[
		{AppComOperateKeyWord:"loaddata",AppComOperateName:"获取数据"},
		{AppComOperateKeyWord:"loaddata4",AppComOperateName:"获取数据4"}
	],
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
		me.panelParams = appParams.panelParams;
		me.southParams = appParams.southParams;
		
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
		if(id == "" || id == "-1"){//新增
			appInfo.BTDAppComponentsOperateList = me.changeString(Ext.JSON.encode(me.operateList));
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
			appInfo.BTDAppComponentsOperateList = list && list.length > 0 ? me.changeString(Ext.JSON.encode(list)) : null;
		}
		return appInfo;
	},
	/**
	 * 获取类代码
	 * @private
	 */
	getClassCode:function(){
		var me = this;
		var panelParams = me.panelParams;
		var ClassCode = 
		"Ext.define('" + panelParams.appCode + "',{" + 
			"extend:'Ext.zhifangux.GridPanel'," + 
			"alias:'widget." + panelParams.appCode + "'," + 
			"title:'" + panelParams.titleText + "'," + 
			"width:" + panelParams.Width + "," + 
			"height:" + panelParams.Height + "," + 
			"objectName:'" + panelParams.objectName +"'," + //对象名，用于自动主键匹配
			"openFormId:'" + (panelParams['winform-id'] || "") + "'," + //弹出页面的ID
			"defaultLoad:" + (panelParams.autoLoad == '1' ? true : false) + "," + //默认加载数据
			"defaultWhere:'" + (panelParams.defaultParams || "").replace(/@@/g,"%25").replace(/##/g,"%27") + "',";//默认hql,@@=%;##='
			//开启复选
			ClassCode += panelParams.hasCheckBox ? "selType:'checkboxmodel',multiSelect:true," : "";
			//菜单中是否有排序选项
			ClassCode += "sortableColumns:" + me.isSortableColumns() + ",";
			
			ClassCode += 
			"initComponent:function(){" + 
				"var me=this;" + 
				"Ext.Loader.setPath('Ext.ux',getRootPath()+'/ui/extjs/ux');" + 
				//服务地址
				"me.url=getRootPath()+'/" + me.getUrl() + "';" + 
				//查询字段数组
				"me.searchArray=[" + me.getSearchArray().join(',') + "];" + 
				//数据集合
				"me.store=" + me.createStoreStr() + ";" + 
                //数据列
				"me.defaultColumns=" + me.createColumnsStr() + ";" + 
                "me.columns=me.createColumns();";
                //挂靠
                var dockedItems = me.createDockedItemsStr();
                ClassCode += dockedItems == "" ? "" : "me.dockedItems=" + dockedItems + ";";
                
                //编辑列表处理
                if(panelParams['panelEditor1'] == 'row' || panelParams['panelEditor1'] == 'column'){//行编辑列表,单元格编辑列表
	                //行、列编辑的保存方法
	                ClassCode = ClassCode + "me.saveToTable=" + me.createSaveToTableStr() + ";";
	                //列表编辑方式
	                ClassCode = ClassCode + me.createEditorPlugins() + ";"
                }
                
                //删除数据的方法处理
                if(panelParams['toolbar-del-checkbox'] || panelParams['actioncolumn-del-checkbox']){
                    ClassCode = ClassCode + "me.deleteInfo=" + me.createDeleteInfoStr() + ";" ;
                }else{
                    ClassCode = ClassCode + "me.deleteInfo=function(id,callback){};"  
                }
                
				//公开监听事件
				ClassCode = ClassCode + me.createEvent();
				ClassCode = ClassCode + "this.callParent(arguments);" + 
			"}" + 
		"});";
		return ClassCode;
	},
	/**
	 * 菜单选项内容排序是否可见
	 * @private
	 * @return {}
	 */
	isSortableColumns:function(){
		var me = this;
		var columnParams = me.getSortList();//排完序的列属性列表
		var sortableColumns = false;
		for(var i in columnParams){
			if(columnParams[i].CanSort && !columnParams[i].CannotSee){
				sortableColumns = true;
			}
		}
		return sortableColumns;
	},
	/**
	 * 获取排完序的列属性列表
	 * @private
	 * @return {}
	 */
	getSortList:function(){
		var me = this;
		var list = [];
		var southParams = me.southParams;
		var map = [];
		for(var i in southParams){
			var kv = {OrderNum:southParams[i]['OrderNum'],Index:i};
			map.push(kv);
		}
		
		for(var i=0;i<map.length-1;i++){
			for(var j=i+1;j<map.length;j++){
				if(map[i].OrderNum > map[j].OrderNum){
					var temp = map[i];
					map[i] = map[j];
					map[j] = temp;
				}
			}
		}
		for(var i in map){
	        list.push(southParams[map[i].Index]);
		}
		return list;
	},
	/**
	 * 获取列表的获取数据服务URL
	 * @private
	 * @return {}
	 */
	getUrl:function(){
		var me = this;
		var panelParams = me.panelParams;
		//前台需要显示的字段
		var fields = me.getFields().join(',') || "";
		//数据服务地址
		var url = panelParams.getDataServerUrl || "";
		if(url != ""){
			url = url.split("?")[0];
			url = url + "?isPlanish=true&fields=" + fields;
		}
		return url;
	},
	/**
	 * 前台需要的字段
	 * @private
	 * @return {}
	 */
	getFields:function(){
		var me =this;
		var list = me.getSortList();
		var fields = [];
		for(var i in list){
			fields.push(list[i].InteractionField);
		}
		return fields;
	},
	/**
	 * 获取默认排序属性
	 * @private
	 * @return {}
	 */
	getSortArr:function(){
		var me = this;
		var sortArr = [];
		//排好序的列属性
		var list = me.southParams;
		for(var i in list){
			if(list[i].DefaultSort){
				 sortArr.push({
				 	property:list[i].InteractionField,
				 	direction:list[i].SortType
				 });
			}
		}
		return sortArr;
	},
	/**
	 * 创建类代码数据集
	 * @private
	 * @return {}
	 */
	createStoreStr:function(){
		var me = this;
		//配置参数
		var panelParams = me.panelParams;
		//分页模式
		var PageList = panelParams.pageType;
		
		//数据字段
		var fieldsArr = me.getFields();
		var fields = "";
		for(var i in fieldsArr){
			fields = fields + "'" + fieldsArr[i] + "',";
		}
		fields = fields.length > 0 ? fields.slice(0,-1) : "";
		
    	//默认排序属性
		var sortArr = me.getSortArr();
		var sortStr = "[";
		for(var i in sortArr){
			sortStr += "{property:'" +sortArr[i].property + "',direction:'" + sortArr[i].direction + "'},";
		}
		sortStr = sortStr.length > 1 ? sortStr.slice(0,-1) : sortStr;
		sortStr += "]";
		
		//前后台排序设置
		var remoteSort = panelParams.orderByType == 'ui' ? false : true;
		//每页条数
		var PageSize = panelParams.pageSize;
		//前后页数
		var BufferPage = panelParams.bufferPage;
		//有无总条数栏
		var hasCountToolbar = false;
		//是否开启缓存
		var buffered = false;
		//缓存的数量
		var leadingBufferZone = null;
		
		if(PageList == "5"){//无限分页
			buffered = true;
			leadingBufferZone = BufferPage * PageSize;
		}else if(PageList == "1"){//不分页
			PageSize = 1000;
			hasCountToolbar = true;
		}
		
	    var storeStr = 
	    "me.createStore({" + 
	    	"fields:[" + fields + "]," + //数据集字段
	    	"url:'" + me.getUrl() + "'," + //服务地址
	    	"remoteSort:" + remoteSort + "," + //前后台排序设置
	    	"sorters:" + sortStr + "," + //排序字段
	    	"PageSize:" + PageSize + "," + //每页数据条数
	    	"hasCountToolbar:" + hasCountToolbar + "," + //有无总条数栏
	    	"buffered:" + buffered + "," + //是否开启缓存
	    	"leadingBufferZone:" + leadingBufferZone + //缓存的数量
	    "})";
	    
		return storeStr;
	},
	/**
	 * 获取查询字段数组
	 * @private
	 * @return {}
	 */
	getSearchArray:function(){
		var me = this;
		var panelParams = me.panelParams;
		var hasSearchBar = panelParams.hasSearchBar;
		
		var change = function(value){
			var str = "";
			var arr = value.split('_');
			if(arr.length > 0){
				str += arr[0].toLowerCase() + ".";//转小写
				for(var i=1;i<arr.length;i++){
					str += arr[i] + ".";
				}
			}
			str = (str.length > 0 ? str.slice(0,-1) : "");
			return str;
		};
		
		var searchArray = [];
		
		if(hasSearchBar){
			var list = me.southParams;
			for(var i in list){
				var searchColumn = list[i]['SearchColumn'];
				if(searchColumn){
					searchArray.push("'" + change(list[i]['InteractionField']) + "'");
				}
			}
		}
		return searchArray;
	},
	/**
	 * 创建列表列
	 * @private
	 * @return {}
	 */
	createColumnsStr:function(){
		var me = this;
		var panelParams = me.panelParams;
		//排完序的列属性
		var columnParams = me.getSortList();
		
		var columnsStr = "";
        var colEditer = "";		        
		for(var i in columnParams){
			//设置列编辑、默认不允许编辑
            if(columnParams[i].Editor){
	            var columnType = columnParams[i].ColumnType;
                //如果列类型为下拉型
	            if(columnType == "combobox"){
	                var typeChoose = columnParams[i].typeChoose || "";
	                
	                if(typeChoose == "false"){
	                    var valueField = columnParams[i].valueField || "";//下拉列表绑定值字段
	                    var textField = columnParams[i].textField || "";//下拉列表绑定显示字段
	                    var comboboxServerUrl = columnParams[i].comboboxServerUrl + "?isPlanish=true&fields=" + valueField + "," + textField;//下拉列表绑定加载的数据地址
                    	
	                    colEditer = 
	                    "editor:new Ext.form.field.ComboBox({" + 
	                        "mode:'local'," + 
	                        "editable:false,typeAhead:true," + 
	                        "forceSelection:true," + 
	                        "queryMode:'local'," + 
	                        "displayField:'" + textField+"'," + 
	                        "valueField:'" + valueField+"'," + 
	                        "listClass:'x-combo-list-small'," + 
	                        "store:new Ext.data.Store({" + 
		                        "fields:['" + valueField + "','" + textField + "']," + 
		                        "proxy:{" + 
		                            "type:'ajax'," + 
		                            "url:getRootPath()+'/" + comboboxServerUrl + "'," + 
		                            "reader:{type:'json',root:'list'}," + 
		                            "extractResponseData:function(response){" + 
		                                "return me.changeData(response);" + 
		                            "}" + 
		                        "}," + 
		                        "autoLoad:true" + 
	                        "})" + 
	                   "}),";
                	}else if(typeChoose=='true'){
		                var combodata="";
		                var tempStr = columnParams[i].combodata || "";
		                if(tempStr != "" && tempStr.length > 0){
	                        combodata = combodata.slice(1,-1);
		                }else{
		                    combodata="[]";
		                }
	                	colEditer = 
	                	"editor:new Ext.form.field.ComboBox({" + 
	                        "mode:'local'," + 
	                        "editable:false,typeAhead:true," + 
	                        "forceSelection:true," + 
	                        "queryMode:'local'," + 
	                        "displayField:'value'," + 
	                        "valueField:'text'," + 
	                        "listClass:'x-combo-list-small'," + 
	                        "store:new Ext.data.SimpleStore({ " + 
	                            "fields:['value','text']," +  
	                            "data:[" + combodata + "]" + 
	                        "})"+
	                   	"}),"
                	}
               	}else if(columnType=='colorscombobox'){
                    //如果列类型为颜色列
                    colEditer="editor:new Ext.zhifangux.colorscombobox({mode:'local'}),"
                }else{
               		if(columnType == "number"){
               			var format = columnParams[i].format && columnParams[i].format != "" ? ",format:'" + columnParams[i].format + "'" : "";
                  		colEditer="editor:{allowBlank:true,xtype:'numberfield'" + format + "},";
                    }else{
                    	colEditer="editor:{allowBlank:true},";
                    }
                }
            }else{
               colEditer="";
			}
            
			var col = 
			"{" + 
				"text:'" + columnParams[i].DisplayName + "'," + 
				"dataIndex:'" + columnParams[i].InteractionField + "'," + 
				"width:" + columnParams[i].Width + "," + 
				"locked:" + columnParams[i].IsLocked + ",";
				
				if(columnParams[i].ColumnType == "bool"){
	            	col += 
	            	"xtype:'booleancolumn'," + 
					"trueText:'是'," + 
					"falseText:'否'," + 
					"defaultRenderer:function(value){" + 
				        "if(value===undefined){return this.undefinedText;}" + 
				        "if(!value||value==='false'||value==='0'||value===0){return this.falseText;}" + 
				    	"return this.trueText;" + 
				    "},";
	            }else if(columnParams[i].ColumnType == "number"){
	            	col += "xtype:'numbercolumn',";
	            }else if(columnParams[i].ColumnType == "date"){
	            	col += "xtype:'datecolumn',";
	            }
	            
	            if(columnParams[i].format && columnParams[i].format != ""){
	            	col += "format:'" + columnParams[i].format + "',";
	            }
				
                var isColorColumn=columnParams[i].IsColorColumn;
                //是否是颜色列
	            if(isColorColumn){
	                //如果该列是颜色列时,该列需要绑定颜色值的字段名
	                var colorValueField = columnParams[i].ColorValueField || "";
	                col +=
	                "renderer:function(value, cellmeta, record, rowIndex, columnIndex, store){" + 
	                	"var colorValue=record.get('"+colorValueField+"');" + 
	                    "if(colorValue==''){colorValue='#FFFFFF';}" + //默认为白色
	                    "cellmeta.style='background-color:'+colorValue;" + //设置背景色#0000ff
	                    "return value;" + 
	               " },";
                    
	            }
                
				col += 
				"sortable:" + columnParams[i].CanSort + "," + 
				"hidden:" + (columnParams[i].IsHidden || columnParams[i].CannotSee) + "," + 
				"hideable:" + !columnParams[i].CannotSee + "," + 
                colEditer + //是否可以修改行或单元格
				"align:'" + columnParams[i].AlignType + "'" + 
			"}";
			columnsStr = columnsStr + col + ",";
		}
        
		//操作列代码,只有当操作列复选项勾选时才生成
        if(panelParams['actioncolumn-all']){
        	var actioncolumn = me.createActionColumnStr();
            columnsStr += actioncolumn == "" ? "" : actioncolumn + ",";
        }
        
		columnsStr = columnsStr == "" ? "" : columnsStr.slice(0,-1);
		
		var rownumberer = panelParams.hasRowNumber ? "{xtype:'rownumberer',text:'序号',width:35,align:'center'}," : "";
		
		var columnsStr = "[" + rownumberer + columnsStr + "]";
		return columnsStr;
	},
	/**
	 * 创建功能列
	 * @private
	 * @return {}
	 */
	createActionColumnStr:function(){
		var me = this;
		var panelParams = me.panelParams;
		var arr = [];
		//修改按钮
		if(panelParams['actioncolumn-edit-checkbox']){
			var value = 
			"{" +
				"type:'edit'," + 
				"tooltip:'" + panelParams['actioncolumn-edit-text'] + "'," + 
				"iconCls:'build-button-edit hand'," + 
				"handler:function(grid,rowIndex,colIndex,item,e,record){";
			if(panelParams['winform-checkbox']){
				value += 
					"var id=''+record.get('" + panelParams['winform-combobox'] + "');" + 
					"me.openFormWin(item.type,id,record);";
			}else{
				value += 
					"me.fireEvent('editClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:panelParams['actioncolumn-edit-number'],
				value:value
			});
		}
		//查看按钮
		if(panelParams['actioncolumn-show-checkbox']){
			var value = 
			"{" +
				"type:'show'," + 
				"tooltip:'" + panelParams['actioncolumn-show-text'] + "'," + 
				"iconCls:'build-button-see hand'," + 
				"handler:function(grid,rowIndex,colIndex,item,e,record){";
			if(panelParams['winform-checkbox']){
				value += 
					"var id=''+record.get('" + panelParams['winform-combobox'] + "');" + 
					"me.openFormWin(item.type,id,record);";
			}else{
				value += 
					"me.fireEvent('showClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:panelParams['actioncolumn-show-number'],
				value:value
			});
		}
		//删除按钮
		if(panelParams['actioncolumn-del-checkbox']){
			var value = 
			"{" +
				"tooltip:'" + panelParams['actioncolumn-del-text'] + "'," + 
				"iconCls:'build-button-delete hand'," + 
				"handler:function(grid,rowIndex,colIndex,item,e,record){" + 
					"Ext.Msg.confirm('提示','确定要删除吗？',function (button){" + 
						"if(button=='yes'){" + 
							"var id = record.get('" + panelParams['winform-combobox'] + "');" + 
							"var callback=function(){me.deleteIndex=rowIndex;me.load(true);};" + 
							"me.deleteInfo(id,callback);" + 
						"}" + 
					"});" + 
				"}" + 
			"}";
			arr.push({
				key:panelParams['actioncolumn-del-number'],
				value:value
			});
		}
		
		var itemsStr = "";
		for(var i=1;i<4;i++){
			for(var j in arr){
				if(arr[j].key == i){
					itemsStr += arr[j].value + ",";
				}
			}
		}
		itemsStr = itemsStr == "" ? "" : itemsStr.slice(0,-1);
		
		var actioncolumn = "";
		if(itemsStr != ""){
			actioncolumn = 
			"{" + 
				"xtype:'actioncolumn',text:'操作',width:60,align:'center'," + 
				"sortable:false,hidden:false,hideable:false," + 
				"items:[" + itemsStr + "]" + 
			"}";
		}
		return actioncolumn;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItemsStr:function(){
		var me = this;
		//挂靠
		var dockedItemds = "";
		//分页代码
		var pagingtoolbar = me.createPagingtoolbarStr();
		dockedItemds += pagingtoolbar == "" ? "" : pagingtoolbar + ",";
		
		//生成功能栏按钮组代码
		var funbut = me.createFunButStr();
		dockedItemds += funbut == "" ? "" : funbut + ",";
		
		dockedItemds = dockedItemds == "" ? "" : "[" + dockedItemds.slice(0,-1) + "]";
		return dockedItemds;
	},
	/**
	 * 创建分页代码
	 * @private
	 * @return {}
	 */
	createPagingtoolbarStr:function(){
		var me = this;
		var pagingtoolbar = "";
		
		var panelParams = me.panelParams;
		var PageList = panelParams.pageType;//分页模式
		if(PageList == "2"){//数字分页
			pagingtoolbar =	
			"{" + 
				"xtype:'pagingtoolbar'," + 
				"store:me.store," + 
				"dock:'bottom'," + 
				"displayInfo:true" + 
			"}";
		}else if(PageList == "3"){//滚动分页
			pagingtoolbar =	
			"{" + 
				"xtype:'pagingtoolbar'," + 
				"store:me.store," + 
				"dock:'bottom'," + 
				"plugins:Ext.create('Ext.ux.SlidingPager',{})," + 
				"displayInfo:true" + 
			"}";
		}else if(PageList == "4"){//进度条分页
			pagingtoolbar =	
			"{" + 
				"xtype:'pagingtoolbar'," + 
				"store:me.store," + 
				"dock:'bottom'," + 
				"plugins:Ext.create('Ext.ux.ProgressBarPager',{}),"
				"displayInfo:true" + 
			"}";
		}else if(PageList == "1"){//不分页
			pagingtoolbar =	
			"{" + 
				"xtype:'toolbar'," + 
				"dock:'bottom'," + 
				"itemId:'toolbar-bottom'," + 
				"items:[{xtype:'label',itemId:'count',text:'共0条'}]" + 
			"}";
		}
		return pagingtoolbar;
	},
	/**
	 * 生成功能栏按钮组代码
	 * @private
	 * @return {}
	 */
	createFunButStr:function(){
		var me = this;
		var panelParams = me.panelParams;
        
		var arr = [];
		//刷新按钮
		if(panelParams['toolbar-refresh-checkbox']){
			var value = 
			"{" +
				"type:'refresh'," + 
				"itemId:'refresh'," + 
				"text:'" + panelParams['toolbar-refresh-text'] + "'," + 
				"iconCls:'build-button-refresh'," + 
				"handler:function(but,e){" + 
					"var com = but.ownerCt.ownerCt;" + 
					"com.load(true);" + 
				"}" + 
			"}";
			arr.push({
				key:panelParams['toolbar-refresh-number'],
				value:value
			});
		}
		//新增按钮
		if(panelParams['toolbar-add-checkbox']){
			var value = 
			"{" +
				"type:'add'," + 
				"itemId:'add'," + 
				"text:'" + panelParams['toolbar-add-text'] + "'," + 
				"iconCls:'build-button-add'," + 
				"handler:function(but,e){";
			if(panelParams['winform-checkbox']){
				value += 
					"me.openFormWin(but.type,-1,null);";
			}else{
				value += 
					"me.fireEvent('addClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:panelParams['toolbar-add-number'],
				value:value
			});
		}
		//修改按钮
		if(panelParams['toolbar-edit-checkbox']){
			var value = 
			"{" +
				"type:'edit'," + 
				"itemId:'edit'," + 
				"text:'" + panelParams['toolbar-edit-text'] + "'," + 
				"iconCls:'build-button-edit'," + 
				"handler:function(but,e){";
			if(panelParams['winform-checkbox']){
				value += 
					"var records = me.getSelectionModel().getSelection();" + 
					"if(records.length == 1){" + 
						"var id = records[0].get('" + panelParams['winform-combobox'] + "');" + 
						"me.openFormWin(but.type,id,records[0]);" + 
					"}else{" + 
						"alertInfo('请选择一条数据进行操作！');" + 
					"}";
			}else{
				value += 
					"me.fireEvent('editClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:panelParams['toolbar-edit-number'],
				value:value
			});
		}
		//查看按钮
		if(panelParams['toolbar-show-checkbox']){
			var value = 
			"{" +
				"type:'show'," + 
				"itemId:'show'," + 
				"text:'" + panelParams['toolbar-show-text'] + "'," + 
				"iconCls:'build-button-see'," + 
				"handler:function(but,e){";
			if(panelParams['winform-checkbox']){
				value += 
					"var records = me.getSelectionModel().getSelection();" + 
					"if(records.length == 1){" + 
						"var id = records[0].get('" + panelParams['winform-combobox'] + "');" + 
						"me.openFormWin(but.type,id,records[0]);" + 
					"}else{" + 
						"alertInfo('请选择一条数据进行操作！');" + 
					"}";
			}else{
				value += 
					"me.fireEvent('showClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:panelParams['toolbar-show-number'],
				value:value
			});
		}
		//删除按钮
		if(panelParams['toolbar-del-checkbox']){
            //添加对删除服务的判断处理(删除服务在构建可以不配置,
            //在应用联动处理时才对删除按钮进行功能操作,这样可以满足特殊处理,如删除操作时只是更新某一行的字段信息,并不是真正删除该行记录)
            var delServerUrl = panelParams['del-server-combobox'];
            delServerUrl = delServerUrl ? delServerUrl.split("?")[0] : "";
     
			var value = 
			"{" +
				"type:'del'," + 
				"itemId:'del'," + 
				"text:'" + panelParams['toolbar-del-text'] + "'," + 
				"iconCls:'build-button-delete'," + 
				"handler:function(but,e){";
                 //只有删除服务配置时才添加这段代码
                 if(delServerUrl != ''){
					value = value+ 
					"var records = me.getSelectionModel().getSelection();" + 
					"if(records.length > 0){" + 
						"Ext.Msg.confirm('提示','确定要删除吗？',function(button){" + 
							"var createFunction=function(id){" + 
								"var f=function(){" + 
									"var rowIndex=me.store.find('" + panelParams['winform-combobox'] + "',id);" + 
									"me.deleteIndex=rowIndex;" + 
									"me.load(true);" + 
									"me.fireEvent('delClick');" + 
								"};" + 
								"return f;" + 
							"};" + 
							"if(button=='yes'){" + 
								"for(var i in records){" + 
									"var id=records[i].get('" + panelParams['winform-combobox'] + "');" + 
									"var callback=createFunction(id);" + 
									"me.deleteInfo(id,callback);" +  
								"}" + 
							"}" + 
						"});" + 
					"}else{" + 
						"alertInfo('请选择数据进行操作！');" + 
					"}";
                 }else{
                 	value = value + 
                 	"me.fireEvent('delClick');";
                 }
				value = value + 
				"}" + 
			"}";
			arr.push({
				key:panelParams['toolbar-del-number'],
				value:value
			});
		}
		
        //单元格编辑才出现保存按钮
        if(panelParams['panelEditor1'] == 'column'){
			var value = 
			"{" + 
	            "xtype:'button'," + 
	            "text:'修改保存'," + 
	            "iconCls:'build-button-save'," + 
	            "margin:'0 0 0 2',"+ 
	            "itemId:'save-button'," + 
	            "handler:function(but){" + 
					"var editer_id=me.objectName+'_Id';" + 
		            "var editerFields='';"+
                    "var arrFields=[" + me.getEditFields() + "];" + //获取需要修改的字段                 
                        "arrFields.push(editer_id);" + 
                        "var strCount=me.store.getModifiedRecords();" + //获取修改后的行记录    
						"Ext.Msg.confirm('警告','确定要修改保存吗？',function (button){" + 
                         	"if(button=='yes'){" + 
                         		"for(var i=0;i<strCount.length;i++){" + 
                                	"for(var j=0;j<arrFields.length;j++){"+                                 
                                		"editerFields=editerFields+arrFields[j]+"+"\\\":'\\\""+"+strCount[i].get(arrFields[j])+"+"\\\"',\\\""+
                                	"}"+
	                                "editerFields=editerFields.slice(0,-1);"+
	                                "editerFields='{'+editerFields+'}';"+  
	                                "var editerJSON=Ext.JSON.decode(editerFields);"+ 
	                                "me.saveToTable(editerJSON);"+
	                                "editerFields='';"+
                                "}"+  
								"me.store.load();" + 
								"me.fireEvent('savechangesClick');" + 
							"}else{" + 
                           		"me.store.load();" + 
                           	"}" +                      
						"});" +  
					"}" + 
				"}" + 
			"}";
            
            arr.push({
                key:6,
                value:value
            });
		}
		
		var itemsStr = "";
		for(var i=1;i<7;i++){
			for(var j in arr){
				itemsStr += arr[j].key == i ? arr[j].value + "," : "";
			}
		}
		
		itemsStr += me.createSearchComponentStr();//创建功能查询组件
		itemsStr = itemsStr == "" ? "" : itemsStr.slice(0,-1);
		
		var toolbarStr = "";
		if(itemsStr != ""){
			toolbarStr = 
			"{" + 
				"xtype:'toolbar'," + 
				"itemId:'buttonstoolbar'," + 
				"dock:'" + panelParams['toolbar-position'] + "'," + 
				"items:[" + itemsStr + "]" + 
			"}";
		}
		return toolbarStr;
	},
	/**
	 * 获取需要修改的字段
	 * @private
	 * @return {}
	 */
	getEditFields:function(){
        var me = this;
        var list = me.southParams;
        var fields="";
        for(var i in list){
        	fields += list[i]['IsEditer'] ? "'" + list[i]['InteractionField'] + "'," : "";
        }
        fields = fields == "" ? "" : fields.slice(0,-1);
        return fields;
    },
    /**
	 * 创建查询功能Str
	 * @private
	 * @return {}
	 */
	createSearchComponentStr:function(){
		var me = this;
		var panelParams = me.panelParams;
		
		var emptyText = "";
		var list = me.southParams;
		for(var i in list){
			var searchColumn = list[i]['SearchColumn'];
			emptyText += searchColumn ? list[i]['DisplayName'] + "/" : "";
		}
		emptyText = emptyText == "" ? "" : emptyText.slice(0,-1);
		
		var itemsStr = "";
		if(panelParams.hasSearchBar){
			itemsStr = 
			"'->',{" + 
				"xtype:'textfield'," + 
				"itemId:'searchText'," + 
				"width:" + (panelParams.searchWidth || 160) + "," + 
				"emptyText:'" + emptyText + "'," + 
				"listeners:{" + 
					"render:function(input){" + 
						"new Ext.KeyMap(input.getEl(),[{" + 
							"key:Ext.EventObject.ENTER," + 
							"fn:function(){me.search();}" + 
						"}]);" + 
					"}" + 
				"}" + 
			"},{" + 
				"xtype:'button',text:'查询',iconCls:'search-img-16 '," + 
				"tooltip:'按照" + emptyText + "进行查询'," + 
				"handler:function(button){me.search();}" + 
			"},";
		}
		return itemsStr
	},
	/**
     * 编辑列表时才用到
     * 行、列编辑保存方法
     * @return {}
     */
    createSaveToTableStr:function(){
        var me = this;
        //获取属性面板ID
        var paramsPanel = me.panelParams;
        //获取修改服务URL
        var url = paramsPanel['editDataServerUrl'] || "";
        fun = 
        "function(strobj){" + 
            "var url='" + url + "';" + 
            "if(url!=''){" + 
            	"url=getRootPath()+'/'+url;" + 
            "}else{" + 
	            "alertError('没有配置获取数据服务地址！);" + 
	            "return null;" + 
            "}" + 
            "var values=strobj;" + 
            "var obj=getObjByValue(values);" + 
            "var fields='';" + 
            "for(var i in values){" + 
                "var keyArr=i.split('_');" + 
                "fields+=keyArr.slice(1).join('_')+',';" + 
            "}" + 
            "fields=fields==''?'':fields.slice(0,-1);" + 
            "var params=Ext.JSON.encode({entity:obj,fields:fields});" + 
            "var callback=function(text){" + 
            	"var result=Ext.JSON.decode(response.responseText);" + 
            	"if(result.success){" + 
                	"alertInfo('保存成功！');"+
                "}else{" + 
                    "alertError(result.ErrorInfo);" + 
                "}" + 
            "};" + 
            "postToServer(url,params,callback);" + 
		"}";
		return fun;
    },
    /**
     * 编辑列表时才用到
     * 创建编辑方式代码
     * @param {} callback
     */
    createEditorPlugins:function(){
        var me=this;
        var modelcode = "";
        var panelParams = me.panelParams;
        if(panelParams['panelEditor1'] == 'row'){//行编辑列表
        	modelcode = 
            "me.plugins=Ext.create('Ext.grid.plugin.RowEditing',{" + 
				"clicksToMoveEditor:2,autoCancel:false," + 
               	"listeners:{" + 
					"canceledit:function(){}," + 
                   	"edit:function(editor,e){" + 
	                	"var records=e.record.data;" + 
	                   	"me.saveToTable(records);" + 
               		"}" + 
               	"}"+
			"});";
        }else if(panelParams['panelEditor1'] == 'column'){//单元格编辑列表
        	modelcode = "me.plugins=Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});";
        }
        return modelcode;
    },
   	/**
	 * 创建监听代码
	 * @private
	 * @return {}
	 */
	createEvent:function(){
		var me = this;
		var panelParams = me.panelParams;
        //注册事件
		var com = "";
		//新增
		if(panelParams['toolbar-add-checkbox']){
			com += "me.addEvents('addClick');me.addEvents('afterOpenAddWin');";
		}
		//修改
		if(panelParams['toolbar-eidt-checkbox'] || panelParams['actioncolumn-edit-checkbox']){
			com += "me.addEvents('editClick');me.addEvents('afterOpenEditWin');";
		}
		//查看
		if(panelParams['toolbar-show-checkbox'] || panelParams['actioncolumn-show-checkbox']){
			com += "me.addEvents('showClick');me.addEvents('afterOpenShowWin');";
		}
		//删除
		if(panelParams['toolbar-del-checkbox'] || panelParams['actioncolumn-del-checkbox']){
			com += "me.addEvents('delClick');";
		}
        //单元格编辑修改保存
        if(panelParams['panelEditor1'] == 'column'){
           com += "me.addEvents('savechangesClick');"; 
        }
		return com;
	},
	/**
	 * 删除数据代码
	 * @private
	 * @return {}
	 */
	createDeleteInfoStr:function(){
		var me = this;
		var panelParams = me.panelParams;
		var fun = "function(id,callback){}";
		if(panelParams['del-server-combobox']){
			fun = 
			"function(id,callback){" + 
				"var url=getRootPath()+'/" + panelParams['del-server-combobox'].split("?")[0] + "?id='+id;" + 
				"var c=function(text){" + 
					"var result=Ext.JSON.decode(text);" + 
					"if(result.success){" + 
						"if(Ext.typeOf(callback)=='function'){" + 
							"callback();" + 
						"}" + 
					"}else{" + 
						"alertError(result.ErrorInfo);" + 
					"}" + 
				"};" + 
				"getToServer(url,c);" + 
			"}";
		}
		return fun;
	}
});