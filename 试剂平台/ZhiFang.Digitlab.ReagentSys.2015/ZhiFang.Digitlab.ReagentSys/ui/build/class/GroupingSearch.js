/**
 * 分组查询构建工具
 * 【可配参数】
 * 
 * 服务地址配置
 * objectUrl：获取数据对象列表的服务地址
 * objectPropertyUrl：获取数据对象内容的服务地址
 * objectGetDataServerUrl：获取获取服务列表的服务地址
 * objectSaveDataServerUrl：获取保存服务列表的服务地址
 * dictionaryListServerUrl：查询对象属性所属字典服务列表的服务地址
 * addServerUrl：保存新增信息的后台服务地址
 * editServerUrl：保存修改信息的后台服务地址
 * getAppParamsServerUrl：获取参数的后台服务地址
 * 
 * buildTitle：构建名称
 * appId：列表元应用的ID，默认是-1，即新增，大于0的为修改
 * 
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.GroupingSearch',{
    extend:'Ext.panel.Panel',
    alias: 'widget.groupingsearch',
    //=====================可配参数=======================
    /***
     * 数据对象,修改还原用
     * @type String
     */
    objectName:'',
    
    /***
     *宏命令分类数组
     * @type 
     */
    macroCommandArr:[
        {'BTDMacroCommand_TypeName':'日期类型','BTDMacroCommand_TypeCode':'DateTime'},
        {'BTDMacroCommand_TypeName':'用户身份类型','BTDMacroCommand_TypeCode':'UserInfo'}
    ],
    /***
     * 分组按钮的新增或编辑类型
     * @type String
     */
    operationType:'',
    /***
     * 分组按钮X
     * @type String
     */
    comX:1,
    /***
     * 分组按钮Y
     * @type String
     */
    comY:1,
    /***
     * 分组按钮Width
     * @type String
     */
    comWidth:100,
    /***
     * 分组按钮Height
     * @type String
     */
    comHeight:20,
    
    
    /**
     * 初始宽度
     * @type Number
     */
    defaultPanelWidth:490,
    /**
     * 初始高度
     * @type Number
     */
    defaultPanelHeight:370,
    /**
     * 应用组件ID
     */
    appId:-1,
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
    /**
     * 时间戳字符串
     * @type String
     */
    DataTimeStamp:'',
    /**
     * 构建名称
     */
    buildTitle:'',
    
    btnDefaultwidth:100,//宏按钮的默认宽度
    /**
     * 展示区域的sql语句文本域名称
     * @type 
     */
    centerSQLName:"InteractionField",
    /**
     * 数据对象字段数组
     * @type 
     */
    objectFields:['ClassName','CName','EName','SysDic','Description','ShortCode'],
    /**
     * 获取数据对象列表的服务地址
     * @type 
     */
    objectUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityList',
    /**
     * 返回数据对象列表的值属性
     * 例如：
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,List:[{a:1}]}"}
     *  返回数据对象列表的值属性就是ResultDataValue
     * @type String
     */
    objectRoot:'ResultDataValue',
    /**
     * 数据对象的显示字段
     * @type String
     */
    objectDisplayField:'CName',
    /**
     * 数据对象的值字段
     * @type String
     */
    objectValueField:'ClassName',
    
    //对象属性配置private
    /**
     * 数据对象内容字段数组
     * @type 
     */
    objectPropertyFields:['text','InteractionField','RightID','leaf','icon','Tree','tid','checked','FieldClass'],
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
     * 返回数据对象内容的值属性
     * 例如：
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,List:[{a:1}]}"}
     *  返回数据对象内容的值属性就是ResultDataValue
     * @type String
     */
    objectRootProperty:'Tree',
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
    
    //数据服务配置private
    /**
     * 数据服务列表字段数组
     * @type 
     */
    objectServerFields:['CName','ServerUrl'],
    /**
     * 返回数据服务列表的值属性
     * 例如：
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,List:[{a:1}]}"}
     *  返回数据服务列表的值属性就是ResultDataValue
     * @type String
     */
    objectServerRoot:'ResultDataValue',
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
     * 获取数据服务列表时后台接收的参数名称
     * @type String
     */
    ObjectServerParam:'EntityName',
    /**
     * 获取获取服务列表的服务地址
     * @type 
     */
    objectGetDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName',
    /**
     * 获取保存服务列表的服务地址
     * @type 
     */
    objectSaveDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchParaEntityServiceListByEntityName',
    
    //查询对象属性所属字典服务列表
    /**
     * 查询对象属性所属字典服务列表的服务地址
     * @type 
     */
    dictionaryListServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName',
    /**
     * 查询对象属性所属字典服务列表时后台接收的参数名称
     * @type String
     */
    dictionaryListServerParam:'EntityPropertynName',
    
    /**
     * 新增保存的后台服务地址
     * @type 
     */
    addServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_AddBTDAppComponents',
    /**
     * 修改保存的后台服务地址
     * @type String
     */
    editServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_UpdateBTDAppComponents',
    /**
     * 获取应用信息的后台服务地址
     * @type String
     */
    getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    //查询宏命令(HQL)
    getSearchBTDMacroCommandByHQLUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDMacroCommandByHQL',
     /**
     * 列表中的数据列
     * @private
     * @return {}
     */
    getFields:function(){
        var me = this;
        var fields = [
            {name:me.fieldsObj.Id,type:'string'},//应用组件ID
            {name:me.fieldsObj.CName,type:'string'},//中文名称
            {name:me.fieldsObj.EName,type:'string'},//英文名称
            {name:me.fieldsObj.MacroCode,type:'string'},//功能编码
            {name:me.fieldsObj.ClassCode,type:'string'},//
            {name:me.fieldsObj.ClassCode,type:'string'},//
            {name:me.fieldsObj.TypeId,type:'string'},//
            {name:me.fieldsObj.TypeName,type:'string'}//
        ];
        return fields;
    },
    fields:[
    'BTDMacroCommand_Id',
    'BTDMacroCommand_CName',
    'BTDMacroCommand_EName',
    'BTDMacroCommand_MacroCode',
    'BTDMacroCommand_TypeCode',
    'BTDMacroCommand_TypeName'
    ],
    selectfields:'BTDMacroCommand_Id,BTDMacroCommand_CName,BTDMacroCommand_EName,BTDMacroCommand_MacroCode,BTDMacroCommand_TypeCode,BTDMacroCommand_TypeName',
    /**
     * 宏命令字段对象
     * @type 
     */
    fieldsObj:{
        /**
         * ID
         * @type String
         */
        Id:'BTDMacroCommand_Id',
        /**
         * 中文名称
         * @type String
         */
        CName:'BTDMacroCommand_CName',
        /**
         * 英文名称
         * @type String
         */
        EName:'BTDMacroCommand_EName',
        /**
         * 宏命令编码
         * @type String
         */
        MacroCode:'BTDMacroCommand_MacroCode',
        /**
         * 宏命令具体值
         * @type String
         */
        ClassCode:'BTDMacroCommand_ClassCode',
        /**
         * 宏命令类型
         * @type String
         */
        TypeId:'BTDMacroCommand_TypeCode',
        /**
         * 宏命令类型
         * @type String
         */
        TypeName:'BTDMacroCommand_TypeName'

    },

    //=====================内部变量=======================
    sQLStrWhere:[],//sql语句数组
 
    /**
     * 属性面板itemId后缀
     * @type String
     */
    ParamsPanelItemIdSuffix:'_ParamsPanel',
    /**
     * 当前打开的属性面板
     * @type String
     */
    OpenedParamsPanel:'center',
    
    /**
     * 是否显示名称
     * @type Boolean
     */
    hasLab:true,
    /**
     * 是否开启边框
     * @type Boolean
     */
    hasBorder:false,
    /**
     * 计数
     * @type Number
     */
    isJustOpenCount:0,

    //=====================内部视图渲染=======================
    /**
     * 初始化表单构建组件
     */
    initComponent:function(){
        var me = this;
        //初始化内部参数
        me.initParams();
        
        Ext.Loader.setPath('Ext.ux',getRootPath()+'/extjs/ux/');
        //初始化视图
        me.initView();
        this.addEvents('saveClick');
        me.callParent(arguments);
    },
    /**
     * 渲染完后执行
     * @author 
     * @private
     */
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
        //创建表单数据项sql语句文本域
        me.createSQLTextAndBtn();
    },
    /**
     * 初始化内部参数
     * @private
     */
    initParams:function(){
        var me = this;
        //边距
        me.bodyPadding = 2;
        //布局方式
        me.layout = {
            type:'border',
            regionWeights:{south:1,east:2,north:3}
        };
    },
    /**
     * 初始化赋值
     * @private
     */
    initSetValue:function(com){
        var me = this;
        
        if(com&&com!=null){
        //sql编辑文本框
        var comSQL=me.getCenterParams();
        var strwhere=com.btnWhere;
//        strwhere=strwhere.replace(/\%27/g,"'");//还原单引号
//        strwhere=strwhere.replace(/\%2B/g,'+');//还原加号
//        strwhere=strwhere.replace(/\%25/g,'%')////还原百分号
        strwhere=groupingSearchString(strwhere);
        strwhere=strwhere.substring(1,strwhere.length-1);//去掉首尾的单引号
        comSQL.setValue(strwhere);
        
        //表单参数
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var appInfo=formParamsPanel.getComponent('appInfo');
        var appCode=appInfo.getComponent('appCode');
        var appName=appInfo.getComponent('appCName');
        var appExplain=appInfo.getComponent('appExplain');
        appCode.setValue(com.itemId);
        appName.setValue(com.text);
        appExplain.setValue(com.btnExplain.substring(1,com.btnExplain.length-1));
        
        }
        //修改数据的数据对象还原
        if(me.objectName&&me.objectName!=''&&me.objectName.length>0){
            me.setObjData();
            var objectNameCom=me.getobjectName();
            objectNameCom.setValue(me.objectName);
        }
    },
    /**
     * 初始化视图
     * @private
     */
    initView:function(){
        var me = this;
        //功能按钮栏
        var north = me.createNorth();
        //效果展示区
        var center = me.createCenter();
        //属性面板
        var east = me.createEast();
        //后台提供的所有宏组件信息
        var south=me.createBTDMacroCommand();
        //数据项列属性列表
        
        //功能模块ItemId
        center.itemId = "center";
        north.itemId = "north";
        east.itemId = "east";
        south.itemId = "south";
        //south.layout='absolute';
        //功能块位置
        center.region = "center";
        north.region = "north";
        east.region = "east";
        south.region = "west";
        
        //功能块大小
        center.split = true;
        north.height = 10;
        south.height = 160;
        south.width=270;
        east.width = 300;
        south.split = true;
        south.autoScroll= true;
        //功能块收缩属性
        east.split = true;
        east.collapsible = true;
        center.autoScroll= true;
        
        me.items = [north,center,east,south];
    },
    /**
     * 功能栏--隐藏
     * @private
     * @return {}
     */
    createNorth:function(){
        var me = this;
        var com = {
            xtype:'toolbar',
            hidden:true,
            border:false,
            items:[  '-'
            ]
        };
        return com;
    },

    /**
     * 效果展示面板
     * @private
     * @return {}
     */
    createCenter:function(){
        var me = this;
        var com = {
            xtype:'panel',
            title:'',
            bodyPadding:'2 10 10 2',
            autoScroll:true,
            width:me.defaultPanelWidth,
            items:[{
                xtype:'form',
                title:'展示面板',
                itemId:'center',
                width:me.defaultPanelWidth-10
            }]
        };
        return com;
    },
    /**
     * 宏数据项列表
     * @private
     * @return {}
     */
    createSouth:function(comArr){
        var me = this;
        var com = {
            xtype:'panel',
            layout:'absolute',
            bodyPadding:'2 5 5 2',
            autoScroll:true,
            title:'宏数据项选择',
            items:comArr
        };
        return com;
    },
    /**
     * 属性面板
     * @private
     * @return {}
     */
    createEast:function(){
        var me = this;
        //功能信息
        var appInfo = me.createAppInfo();
        //数据对象
        var dataObj = me.createDataObj();
        var formParamsPanel = {
            xtype:'form',
            itemId:'center' + me.ParamsPanelItemIdSuffix,
            title:'分组按钮配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[appInfo,dataObj]
        };
        var com = {
            xtype:'form',
            title:'新分组查询按钮配置',
            autoScroll:true,
            items:[formParamsPanel]
        };
        return com;
    },
    /**
     * 功能信息
     * @private
     * @return {}
     */
    createAppInfo:function(){
        var com = {
            xtype:'fieldset',title:'按钮信息',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'appInfo',
            items:[
            {
                xtype:'textfield',fieldLabel:'按钮编码',labelWidth:65,anchor:'100%',
                itemId:'appCode',name:'appCode',labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textfield',fieldLabel:'按钮名称',labelWidth:65,anchor:'100%',
                itemId:'appCName',name:'appCName',labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textareafield',fieldLabel:'功能简介',labelWidth:65,anchor:'100%',grow:true,
                itemId:'appExplain',name:'appExplain'
            }]
        };
        return com;
    },
  
    /**
     * 数据对象
     * @private
     * @return {}lfc
     */
    createDataObj:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'数据对象',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'dataObject',
            items:[{
                xtype:'combobox',fieldLabel:'数据对象',
                itemId:'objectName',name:'objectName',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                emptyText:'请选择数据对象',
                displayField:me.objectDisplayField,
                valueField:me.objectValueField,
                store:new Ext.data.Store({
                    fields:me.objectFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectUrl,
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:me.changeStoreData
                    },autoLoad:true
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            me.objectChange(owner,newValue);
                        }
                    }
                }
            },{
                xtype:'treepanel',itemId:'objectPropertyTree',border:false,
                rootVisible:false,
                nodeClassName:'',
                CName:'',
                ClassName:'',
                listeners:{
                    beforeitemexpand:function(node){
                        this.nodeClassName = node.data[me.objectPropertyValueField];
                    },
                    beforeload:function(store){
                        if(this.nodeClassName != ""){
                            store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + this.nodeClassName;
                        }
                    },
                    
                    checkchange:function(node,checked){
						//获取构建表单展示区域的SQL语句文本域的组件
						var com=me.getCenterParams();
						var mybtnWhere=com.getValue();
                        var text=node.get(me.objectPropertyValueField);

                        var operations='=';//逻辑运算关系
                        var logical="";//关系运算符
                        if(mybtnWhere==""){
                            logical="";//SQL语句文本域为空值时,为第一次添加
                        }else{
                            logical="and";
                        }
                        
                        //新的查询条件项由选择中的字段的表名称和选择中的字段组成格式为
                        //对象名称.字段名
                        var defaultValueArr = text.split('_');
                        var myItemId = defaultValueArr[defaultValueArr.length-1];
                        var tempStr='';
			            for(var j=0;j<defaultValueArr.length-1;j++){
			                if(j==0){
			                    var tempVlue=defaultValueArr[j];
			                    tempStr=tempStr+tempVlue.toLowerCase()+'.';
			                }
			                else if(j<defaultValueArr.length-1){
			                    tempStr=tempStr+defaultValueArr[j]+'.';
			                }
			            }
			            myItemId =tempStr+defaultValueArr[defaultValueArr.length-1];
                        
                        var tempValue2=" ";//"''";
                        var newWhere=(" "+logical+" "+myItemId+operations+tempValue2+" ");
                        
                        if(checked==true){
                            //勾选中时添加
	                        //获取构建表单展示区域的SQL语句文本域的组件
	                        var com=me.getCenterParams();
	                        //取到(展示区域的SQL语句文本域)dom元素
	                        var input = com.el.dom.lastChild.lastChild.lastChild.lastChild;
	                        input.focus(); 
	                        if (Ext.isIE) {                  
	                          input = document.selection.createRange(); 
	                          document.selection.empty(); 
	                          input.text = newValue; 
	                         } 
	                        if (input.selectionStart || input.selectionStart == '0') { 
	                            var startPos = input.selectionStart; 
	                            var endPos = input.selectionEnd; 
	                            input.value = input.value.substring(0, startPos)+ 
	                            newWhere + input.value.substring(endPos, input.value.length); 
	                        }else { 
	                            input.value +=newWhere; 
	                         } 
	                        com.setValue(input.value);
	                        com.focus();
                        }
                        node.data.checked = false;
                        node.updateInfo({checked:false});
                    }
                },
                store:new Ext.data.TreeStore({ 
                    fields:me.objectPropertyFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectPropertyUrl,
                        extractResponseData:function(response){
			            	var data = Ext.JSON.decode(response.responseText);
			            	if(data.ResultDataValue && data.ResultDataValue != ""){
			            		var children = Ext.JSON.decode(data.ResultDataValue);
			            	
				            	for(var i in children){
				            		children[i].checked = false;
                                    var ename=children[i].InteractionField.split("_");
                                    children[i].text =children[i].text+" 【"+ename[ename.length-1]+" 】";
				            	}//〖〗
				            	
				            	var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
				            	dataObject = east.getComponent('dataObject');
								var objectPropertyTree = dataObject.getComponent('objectPropertyTree');	
								
								if(objectPropertyTree.nodeClassName != ""){
									data[me.objectRootProperty] = children;
								}else{
									data[me.objectRootProperty] = [{
					            		text:objectPropertyTree.CName+" 【"+objectPropertyTree.ClassName+" 】",
					            		InteractionField:objectPropertyTree.ClassName,
					            		leaf:false,
					            		expanded:true,
					            		checked:false,
					            		Tree:children
					            	}];
								}
			            	}
			            	response.responseText = Ext.JSON.encode(data);
			            	return response;
			          	}
                    },
                    defaultRootProperty:me.objectRootProperty,
		          	root:{
						text:'对象结构',
						leaf:false,
						expanded:true
					},
				    autoLoad:false
                })
            }]
        };
        return com;
    },

    //=====================功能按钮栏事件方法=======================
    /**
     * 创建表单数据项sql语句文本域
     * @private
     */
    createSQLTextAndBtn:function(){
        var me = this;
        var center = me.getCenterCom();
        var owner = center.ownerCt;
        var form = me.createForm();
        if(form){
            //删除原先的表单
            owner.remove(center);
            //添加新的表单
            owner.add(form);
        }
        var center = me.getCenterCom();
        //表单数据项sql语句文本域
        var itemSql = me.createComponents();
        var itemBtn = me.createsaveButton();
        var resetButton = me.createresetButton()
        	center.add(itemSql);
            center.add(itemBtn);
            center.add(resetButton);
    },
    //=====================属性面板事件方法=======================
    /**
     * 对象更改事件处理
     * @private
     */
    objectChange:function(owner,newValue){
        var me = this;
        var dataObject = owner.ownerCt;
        //获取对象结构
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree'); 
        objectPropertyTree.nodeClassName = "";
		objectPropertyTree.CName = owner.rawValue;
		objectPropertyTree.ClassName = newValue;
        objectPropertyTree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        objectPropertyTree.store.load();
        
    },

    //=====================组件的创建与删除=======================
    /**
     * 新建表单
     * @private
     * @return {}
     */
    createForm:function(){
        var me = this;
        var form = {
            xtype:'form',
            itemId:'center',
            layout:'absolute',
            autoScroll:true,
            title:'',
		    width:me.defaultPanelWidth,
		    height:me.defaultPanelHeight,
            resizable:{handles:'s e'}
        };

        form.preventHeader = true;
        var buttons = ['->'];
        if(buttons.length > 1){
            form.dockedItems = {
                xtype:'toolbar',
                dock:'bottom',
                itemId:'buttons',
                items:buttons
            };
        }
        return form;
    },
    /**
     * 创建sql语句文本域组件
     * @private
     * @return {}
     */
    createComponents:function(){
        var me = this;
        //数据项基础属性
		var com =Ext.create('Ext.form.TextArea', {
			width:430,
            x:5,y:5,
			rows:18,
            emptyText :"手工输入时请参考HQL查询条件的格式进行编辑,如第一个数据对象名称的字母全部为小写,对象名和属性名用小圆点分隔(  hremployee.Id='' and hremployee.HRDept.CName='');宏命令不需要单引号引起来;",
            border:0,
			itemId:me.centerSQLName,
            id:'sqlText'
		});
        return com;
    },
     /**
     * 创建确定按钮
     * @private
     * @return {}
     */
    createsaveButton:function(){
        var me = this;
        var com = {
                    xtype:'button',text:'确定',
                    itemId:'save',
                    iconCls:'build-button-save',
                    width:65,
                    x:180,
                    y:335,//190
                    tooltip:'确定',
                    handler:function(){
                        me.fireEvent('saveClick');
                    }
                    };
        return com;
    },
     /**
     * 创建重置按钮
     * @private
     * @return {}
     */
    createresetButton:function(){
        var me = this;
        var com = {
                    xtype:'button',text:'重置',
                    itemId:'reset',
                    tooltip:'重置',
                    iconCls:'build-button-reset',
                    width:65,
                    x:280,
                    y:335,
                    handler:function(){
                        var com=me.getCenterParams();
                             com.setValue("");
                        me.fireEvent('resetClick');
                    }
                    };
        return com;
    },
    //=====================设置获取参数=======================
    /**
     * 获取展示区域组件
     * @private
     * @return {}
     */
    getCenterCom:function(){
        var me = this;
        var form= me.getComponent('center');
        var center=form.getComponent('center');
	    return center;
    },
    /**
     * 获取构建表单展示区域的SQL语句文本域的组件
     * 如需要取表单类型的选择结果值时,就可以这样取
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getCenterParams:function(){
        var me = this;
        var form= me.getComponent('center');
        var com=form.getComponent('center');
        var params =com.getComponent(me.centerSQLName);
        return params;
    },
    /**
     * 获取构建表单面板配置参数集合
     * 如需要取表单类型的选择结果值时,就可以这样取
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getPanelParams:function(){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var params = formParamsPanel.getForm().getValues();
        return params;
    },
    /**
     * 给你面板配置参数赋值
     * @private
     * @param {} obj
     */
    setPanelParams:function(obj){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        formParamsPanel.getForm().setValues(obj);
    },
    /**
     * @private
     * @param {} key
     * @param {} value
     * @return {} record or null
     */
    getobjectName:function(){
        var me = this;
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        //数据对象
        var dataObject = paramsPanel.getComponent('dataObject');
        //数据对象类
        var objectName = dataObject.getComponent('objectName');
        return objectName;
    },
    /**
     * @private
     * @param {} key
     * @param {} value
     * @return {} record or null
     */
    getobjectNameValue:function(){
        var me = this;
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        //数据对象
        var dataObject = paramsPanel.getComponent('dataObject');
        //数据对象类
        var objectName = dataObject.getComponent('objectName');
        var value='';
        if(objectName&&objectName!=null){
            value=objectName.getValue();
        }else{
            value='';
        }
        return value;
    },
    /**
     * 获取所有提供的宏组件信息
     * @private
     * @return {}
     */
    getSouthRecords:function(){
        var me = this;
        var south = me.getComponent('south');
        var store = south.store;
        var records = [];
        store.each(function(record){
            records.push(record);
        });
        
        return records;
    },
    /**
     * 设置后台提供的所有宏组件信息
     * @private
     * @return {}
     */
    setSouthRecords:function(){
        var me = this;
        var records = [];
        records=me.getSearchBTDMacroCommandByHQLServer();
        return records;
    },

    /**
     * 给数据对象列表赋值
     * @private
     */
    setObjData:function(){
        var me = this;
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        //数据对象
        var dataObject = paramsPanel.getComponent('dataObject');
        //数据对象类
        var objectName = dataObject.getComponent('objectName');
        objectName.store.load();
    },
    //=====================后台获取&存储=======================
    /**
     * 从后台获取应用信息
     * @private
     * @param {} callback
     */
    getAppInfoFromServer:function(callback){
        var me = this;
        
        if(me.appId != -1){
            var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + me.appId;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:url,
                method:'GET',
                timeout:5000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        var appInfo = Ext.JSON.decode(result.ResultDataValue);
                        if(Ext.typeOf(callback) == "function"){
                            callback(appInfo);//回调函数
                        }
                    }else{
                        Ext.Msg.alert('提示','获取应用组件信息失败！错误信息【<b style="color:red">'+ result.errorInfo +"</b>】");
                    }
                },
                failure : function(response,options){ 
                    Ext.Msg.alert('提示','获取应用组件信息请求失败！');
                }
            });
        }
    },
    /**
     * 从后台查询宏命令(HQL)
     * @private
     * @param {} callback
     */
    getSearchBTDMacroCommandByHQLServer:function(){
        var me = this;
        var lists=[];
            var myUrl = me.getSearchBTDMacroCommandByHQLUrl + "?isPlanish=true&fields=" + me.getFieldsStr();
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:myUrl,
                method:'GET',
                timeout:5000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result[me.objectRoot] && result[me.objectRoot] != ""){
                        var ResultDataValue = Ext.JSON.decode(result[me.objectRoot]);
                        lists = ResultDataValue['list'];
                        //result.count = ResultDataValue['count'];
                    }else{
                        lists= [];
                        //result.count = 0;
                    }
                    if(!result.success){
                        Ext.Msg.alert('提示','获取应用列表失败！错误信息【<b style="color:red">'+result.ErrorInfo+'</b>】');
                    }
                },
                failure : function(response,options){ 
                    Ext.Msg.alert('提示','获取应用宏命令信息请求失败！');
                }
            });
       return lists;
    },
    /**
     * 获取需要的数据列Str
     * @private
     */
    getFieldsStr:function(){
        var me = this;
        var fieldsStr = "";
        var fields = me.getFields();
        for(var i in fields){
            fieldsStr += fields[i].name + ",";
        }
        if(fieldsStr != ""){
            fieldsStr = fieldsStr.substring(0,fieldsStr.length-1);
        }
        return fieldsStr;
    },
    createBTDMacroCommandStore:function(){
	    var me=this;
	    var store = Ext.create('Ext.data.Store', {
	            fields:me.fields,
	            remoteSort:true,
	            autoLoad:true,
	            sorters:[ {
	                property:'BTDMacroCommand_Id',
	                direction:'ASC'
	            } ],
	            pageSize:10000,
	            proxy:{
	                type:'ajax',
	                url:me.getSearchBTDMacroCommandByHQLUrl+'?isPlanish=true&fields='+me.selectfields,
	                reader:{
	                    type:'json',
	                    root:'list',
	                    totalProperty:'count'
	                },
	                extractResponseData:function(response) {
	                    var data = Ext.JSON.decode(response.responseText);
	                    if (!data.success) {
	                        Ext.Msg.alert('提示', '错误信息:' + data.ErrorInfo);
	                    }
	                    if (data.ResultDataValue && data.ResultDataValue != '') {
	                    data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
	                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	                    data.count = ResultDataValue.count;
	                    data.list = ResultDataValue.list;
	                    } else {
	                        data.list = [];
	                        data.count = 0;
	                    }
	                    response.responseText = Ext.JSON.encode(data);
	                    return response;
	                }
	            },
	            listenres:{
	                load:function(s, records, successful, eOpts) {
	                    if (!successful) {
	                        Ext.Msg.alert('提示', '获取数据服务错误！');
	                    }
	                }
	            }
	        });
        return store;
    },
    
    /**
     * 创建挂靠对象
     * @private
     * @return {}
     */
    createDockedItems:function(){
        var me = this;
        
        var toolbar ={
            xtype:'toolbar',
            itemId:'toolbar-top',
            items:[]
        };
        //开发方式
        toolbar.items.push({
            xtype:'combo',itemId:'searchBuildType',
            id:'searchBuildType',
            width:100,
            mode:'local',
            editable:true,typeAhead:true,
            forceSelection:true,
            queryMode:'local',
            displayField:'BTDMacroCommand_TypeName',
            valueField:'BTDMacroCommand_TypeCode',
            store:new Ext.data.Store({
                fields:['BTDMacroCommand_TypeName','BTDMacroCommand_TypeCode'],
                data:me.macroCommandArr
            }),
            listeners:{
                change:function(com,newValue,oldvalue,e,eOpts){
                    me.filterFn(newValue);
                },
                render:function(input){
                    new Ext.KeyMap(input.getEl(),[{
                        key:Ext.EventObject.ENTER,
                        fn:function(){
                            var searchText=Ext.getCmp('searchBuildType');
                            var value=searchText.getValue();
                            me.filterFn(value);
                        
                        }
                    }]);
                }
            }
        });
        //模糊查询框
        toolbar.items.push({
            xtype:'textfield',itemId:'searchText',id:'searchText',width:160,
            emptyText:'中文名称/宏命令值',
            listeners:{
                render:function(input){
                    new Ext.KeyMap(input.getEl(),[{
                        key:Ext.EventObject.ENTER,
                        fn:function(){
                            var searchText=Ext.getCmp('searchText');
	                        var value=searchText.getValue();
	                        me.filterFn(value);
                        }
                    }]);
                }
            }
        });
        toolbar.items.push({
            xtype:'button',text:'查询',iconCls:'search-img-16 ',
            tooltip:'按照宏命令进行查询',
            hidden:true,
            handler:function(button){
                me.filterFn();
            }
        });
        var dockedItems = [toolbar];
        return dockedItems;
    },
    /**
     * 模糊查询过滤函数
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         //获取右上模块操作选择列表
         var south = me.getComponent('south');
         var store = south.getStore(); 
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(" ");
         store.filterBy(function (record,id) {
             var data = record.data;
             for (var p in data) {
                 var porp = String(data[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = '^' + Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     },
    /**
     * 获取需要的数据列Str
     * @private
     */
    createBTDMacroCommand:function(){
        var me = this;
        var dockedItems = me.createDockedItems();
        var panel=Ext.create('Ext.grid.Panel', {
            title: '',
            autoScroll :true,
            columnLines:true,//在行上增加分割线
            dockedItems:dockedItems,
            store:me.createBTDMacroCommandStore(),
            columns: [
                { text: 'ID',  dataIndex:'BTDMacroCommand_Id',width:50,disabled:true,hidden:true},
                { text: '中文名称', dataIndex:me.fieldsObj.CName,width:150,disabled:false,hidden:false},
                { text: '英文名称', dataIndex: me.fieldsObj.EName,hidden:true},
                { text: '宏命令', dataIndex: me.fieldsObj.MacroCode,width:150,disabled:true,hidden:false},
                { text: '宏命令值', dataIndex: me.fieldsObj.ClassCode,hidden:true},
                { text: '英文名称', dataIndex: me.fieldsObj.TypeId,width:50,disabled:true,hidden:true},
                { text: '类型', dataIndex: me.fieldsObj.TypeName,width:80,disabled:true,hidden:true}
            ],
            listeners: {
                itemclick: function(view,record,item,index,e,epots) {
                    
                   //获取构建表单展示区域的SQL语句文本域的组件
                    var com=me.getCenterParams();
                    var mybtnWhere=com.getValue();
                    
                    var newValue=record.get(me.fieldsObj.MacroCode);
                    //取到(展示区域的SQL语句文本域)dom元素
                    var input = com.el.dom.lastChild.lastChild.lastChild.lastChild;
                    input.focus(); 
                    if (Ext.isIE) {                  
                      input = document.selection.createRange(); 
                      document.selection.empty(); 
                      input.text = newValue; 
                     } 
                    if (input.selectionStart || input.selectionStart == '0') { 
                        var startPos = input.selectionStart; 
                        var endPos = input.selectionEnd; 
                        input.value = input.value.substring(0, startPos)+ 
                        newValue + input.value.substring(endPos, input.value.length)+" "; 
                    }else { 
                        input.value +=newValue+" "; 
                     } 
                    com.setValue(input.value); 
                }
            }
        });
        return panel;
    },
    //=====================公共方法代码=======================
    /**
     * 将JSON对象转化成字符串
     * @private
     * @param {} obj
     * @return {}
     */
    JsonToStr:function(obj){
        var str = Ext.JSON.encode(obj);
        str = str.replace(/\\/g,"\\\\");
        str = str.replace(/\"/g,"\\\"");
        return str;
    },
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

//==============================公开方法===================
    /**
     * 获取到新增一个分组查询按钮的基本参数
     */
    getValue:function(){
        var me = this;
        var lastValue=[];
        var com=me.getCenterParams();
        var mybtnWhere=com.getValue();
        //表单参数
        var params = me.getPanelParams();
        var isOk = true;
        var message = "";
        
        if(mybtnWhere == ""){
            message += "【<b style='color:red'>SQL语句不能为空！</b>】\n";
            isOk = false;
        }
        if(params.appCode == ""){
            message += "【<b style='color:red'>功能编号不能为空！</b>】\n";
            isOk = false;
        }
        if(params.appCName == ""){
            message += "【<b style='color:red'>中文名称不能为空！</b>】\n";
            isOk = false;
        }
        
        if(!isOk){
            Ext.Msg.alert("提示",message);
            return;
        }else{
        //新增分组查询按钮的参数
        var btnItemId=params.appCode;//按钮itemId
        var btnCName=params.appCName;//按钮显示名称
        var btnExplain=params.appExplain;//说明△□§№
        
        var objectNameValue=''+me.getobjectNameValue();
        //where串先用特殊字符§替换单引号,在使用where串时再转换回单引号
        //mybtnWhere=mybtnWhere.replace(/'/g,'§');
        //先转换百分号(以供保存sql语句类代码及还原)
        mybtnWhere=mybtnWhere.replace(/\%/g,'%25')//% 
        mybtnWhere=mybtnWhere.replace(/'/g,'%27');
        //加号作转换处理(加号传入被过滤,需要作转换)
        mybtnWhere=mybtnWhere.replace(/\+/g,'%2B');
        mybtnWhere=mybtnWhere.replace(/\-/g,'%2D');
        //key字段名称要和store一样
        var type=me.operationType;//是新增还是修改类型
        if(type=='add'){
            me.comHeight='22';
        }
        var values={
	        operationType:type,
	        InteractionField:btnItemId,
	        Type:'button',
            X:me.comX,
            Y:me.comY,
            
	        Width:me.comWidth,
            Height:me.comHeight,
	        DisplayName:btnCName,
	        BtnWhere:''+mybtnWhere+'',
	        BtnExplain:btnExplain,
	        objectName:objectNameValue
        };
        lastValue.push(values);  
        return lastValue;
        }
 
    }
});