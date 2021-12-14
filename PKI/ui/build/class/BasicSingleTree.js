/**
 * 单列树构建工具
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicSingleTree',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basicsingletree',
    testAppId:-1,
    appId:-1,//应用组件ID
    appCName:'',//中文名称
    appExplain:'',//组件简介
    buildTitle:'单列树构建工具',//构建名称
    appType:-1,
    //标题字体设置
    win:null,//创建和弹出选择器窗体
    winHeight:270,//弹出选择器窗体高度像素
    winWidth:460,//弹出选择器窗体宽度像素
    winTitle:'',//弹出选择器窗体标题
    /**
     * 删除服务列表字段数组
     * @type String
     */
    delServerFields:['CName','ServerUrl'],
    /**
     * 获取数据服务列表时后台接收的参数名称
     * @type String
     */
    objectServerParam:'EntityName',
    //数据对象配置private
    objectFields:['ClassName','CName','EName','SysDic','Description','ShortCode'],
    objectUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityList',
    objectRoot:'ResultDataValue',
    objectDisplayField:'CName',
    objectValueField:'ClassName',
    //对象属性配置private
    ObjectPropertyFields:['text','InteractionField','RightID','leaf','icon','Tree','tid','checked','FieldClass'],
    ObjectProperyParam:'EntityName',
    ObjectPropertyUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityFrameTree',
    /**
     * 获取应用信息的后台服务地址
     * @type String
     */
    getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    //树的属性设置
    Root:'模块',//根节点名称
    DisplayName:'',//树显示名称
    AlignType:'left',
    Filterfield:'text,tid',

    /**
     * 删除服务列表显示属性
     * @type String
     */
    delServerDisplayField:'CName',
    /**
     * 删除服务列表值属性
     * @type String
     */
    delServerValueField:'ServerUrl',
    /**
     * 删除服务列表地址
     * @type String
     */
    delServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName',
    /**
     * 删除服务列表字段数组
     * @type String
     */
    delServerFields:['CName','ServerUrl','EName'],
 
    /***
     * 对象字段数组,代替moduleFields
     * 基础字段数组
     * @type 
     */
    defaultFields:[
        {name:'ParentID',type:'auto'},//ParentID
        {name:'text',type:'auto'},//默认的现实字段
        {name:'expanded',type:'auto'},//是否默认展开
        {name:'leaf',type:'auto'},//是否叶子节点
        {name:'icon',type:'auto'},//图标
        {name:'url',type:'auto'},//地址
        {name:'tid',type:'auto'},//默认ID号
        {name:'Id',type:'auto'},//默认ID号
        {name:'value',type:'auto'}//模块对象
    ], 
    /***
     * 对象字段数组,代替moduleFields
     * 基础字段数组
     * @type 
     */
    defaultFieldsChecked:[
        {name:'ParentID',type:'auto'},//ParentID
        {name:'text',type:'auto'},//默认的现实字段
        {name:'expanded',type:'auto'},//是否默认展开
        {name:'leaf',type:'auto'},//是否叶子节点
        {name:'icon',type:'auto'},//图标
        {name:'url',type:'auto'},//地址
        {name:'tid',type:'auto'},//默认ID号
        {name:'Id',type:'auto'},//默认ID号
        {name:'value',type:'auto'},//模块对象
        {name:'checked',type:'bool'}//模块对象
    ], 
    /**
     * 数据项列表字段
     * @type 
     */
    columnParamsField:{
        /**
         * 交互字段
         * @type String
         */
        InteractionField:'InteractionField',
        /**
         * 显示名称
         * @type String
         */
        DisplayName:'DisplayName',
        /**
         * 组件类型
         * @type String
         */
        Type:'Type'
    },
    /**
     * 应用字段对象
     * @type 
     */
    fieldsObj:{
        /**
         * 应用组件ID
         * @type String
         */
        AppComID:'BTDAppComponents_AppComID',
        /**
         * 中文名称
         * @type String
         */
        CName:'BTDAppComponents_CName',
        /**
         * 英文名称
         * @type String
         */
        EName:'BTDAppComponents_EName',
        /**
         * 功能编码
         * @type String
         */
        ModuleOperCode:'BTDAppComponents_ModuleOperCode',
        /**
         * 功能简介
         * @type String
         */
        ModuleOperInfo:'BTDAppComponents_ModuleOperInfo',
        /**
         * 初始化参数
         * @type String
         */
        InitParameter:'BTDAppComponents_InitParameter',
        /**
         * 应用类型
         * @type String
         */
        AppType:'BTDAppComponents_AppType',
        /**
         * 构建类型
         * @type String
         */
        BuildType:'BTDAppComponents_BuildType',
        /**
         * 模块类型
         * @type String
         */
        BTDModuleType:'BTDAppComponents_BTDModuleType',
        /**
         * 执行代码
         * @type String
         */
        ExecuteCode:'BTDAppComponents_ExecuteCode',
        /**
         * 设计代码
         * @type String
         */
        DesignCode:'BTDAppComponents_DesignCode',
        /**
         * 类代码
         * @type String
         */
        ClassCode:'BTDAppComponents_ClassCode',
        /**
         * 创建者
         * @type String
         */
        Creator:'BTDAppComponents_Creator',
        /**
         * 修改者
         * @type String
         */
        Modifier:'BTDAppComponents_Modifier',
        /**
         * 汉字拼音字头
         * @type String
         */
        PinYinZiTou:'BTDAppComponents_PinYinZiTou',
        /**
         * 数据加入时间
         * @type String
         */
        DataAddTime:'BTDAppComponents_DataAddTime',
        /**
         * 数据更新时间
         * @type String
         */
        DataUpdateTime:'BTDAppComponents_DataUpdateTime',
        /**
         * 实验室ID
         * @type String
         */
        LabID:'BTDAppComponents_LabID',
        /**
         * 时间戳
         * @type String
         */
        DataTimeStamp:'BTDAppComponents_DataTimeStamp'
    },
    
    //数据服务配置private
    objectServerFields:['CName','EName','ServerUrl','Description'],
    objectServerRoot:'ResultDataValue',
    objectServerDisplayField:'CName',
    objectServerValueField:'ServerUrl',
    objectServerEName:'EName',
    objectGetDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName',
    objectSaveDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchParaEntityServiceListByEntityName',
    //保存的后台服务地址
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
    /**
     * 获取应用列表服务地址
     * @type String
     */
    getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
    /**
     * 上传图片文件服务地址
     * @type 
     */
    updateFileServerUrl:getRootPath()+'/ConstructionService.svc/ReceiveModuleIconService',
    childrenField:'Tree',
 
    //属性面板itemId后缀
    ParamsPanelItemIdSuffix:'_ParamsPanel',
    //当前打开的属性面板
    OpenedParamsPanel:'center',
    //刚刚打开
    isJustOpen:true,
    /**
     * 初始化列表构建组件
     */
    initComponent:function(){
        var me = this;
        //初始化内部参数
        me.initParams();
        Ext.Loader.setPath('Ext.ux', getRootPath()+'/ui/extjs/ux/');
        //初始化视图
        me.initView();
        me.addEvents('saveClick');//保存按钮
        //注册事件
        me.addEvents('btnsaveClick');//保存按钮
        me.addEvents('saveAsClick');//另存按钮
        
        me.callParent(arguments);
        var delDataServerUrl = me.getEastDelDataServerUrl();
        delDataServerUrl.store.proxy.url = me.delServerUrl+"?EntityName=Bool"; 
        delDataServerUrl.store.load();
        //获取获取数据服务列表
        var getDataServerUrl =me.getEastGetDataServerUrl();
        getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=Tree";
        getDataServerUrl.store.load();
        me.setObjData();//数据对象赋值
    },
    /**
	 * 另存按钮事件处理
	 * @private
	 */
	saveAs:function(){
		var me = this;
		me.save(false);
	},
    /**
     * 渲染完组件后处理
     * @private
     */
    afterRender: function(){
        var me = this;
        //初始化监听
        me.initListeners();
        me.callParent(arguments);
        me.setAppParams();
        
    },
    /**
     * 给设计代码赋值
     * @private
     */
    setAppParams:function(){
        var me = this;
        var callback = function(appInfo){
            var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
            var panelParams = appParams.panelParams;
            var BasicParams = appParams.BasicParams;
            var ToolParams = appParams.ToolParams;
            me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];
            //赋值
            me.setPanelParams(panelParams);//属性面板赋值
            me.setBasicParams(BasicParams);//基础属性属性面板赋值
            me.setToolParams(ToolParams);//属性面板赋值
            me.setObjData();//数据对象赋值
            
            //获取获取数据服务列表
            var getDataServerUrl =me.getEastGetDataServerUrl();
            getDataServerUrl.value = panelParams.getDataServerUrl;
            var delDataServerUrl = me.getEastDelDataServerUrl();
            delDataServerUrl.value = panelParams.delDataServerUrl; 
			//设置删除服务列表下拉框赋值
            var formParamsPanel = me.getComponent('east').getComponent('centertool' + me.ParamsPanelItemIdSuffix);
			var buttonsconfig = formParamsPanel.getComponent('buttonsconfig');
			buttonsconfig.setWinFormComboboxValue(panelParams['winform-combobox']);
            
            var editDataServerUrl=me.geteditDataServerUrl();
            editDataServerUrl.value = panelParams.editDataServerUrl ;
        };
        //从后台获取应用信息
        me.getAppInfoFromServer(callback);
        me.browse();
    },
    /**
     * 给数据对象列表赋值
     * @private
     */
    setObjData:function(){
        var me = this;
        //数据对象类
        var objectName =me.getobjectName();
        objectName.store.load();
    },
    /**
	 * 按钮组设置赋值
	 * @private
	 * @param {} panelParams
	 */
	setButSetValues:function(panelParams){
		var me = this;
		var funbut = me.getFunButFieldSet();
		funbut.setDelServerValue(panelParams['del-server-combobox']);
		funbut.setWinFormComboboxValue(panelParams['winform-combobox']);
	},
    /**
     * 初始化内部参数
     * @private
     */
    initParams:function(){
        var me = this;
        //边距
        me.bodyPadding = 4;
        //布局方式
        me.layout = {
            type:'border',
            regionWeights:{east:2,north:3}
        };
    },
    /**
     * 初始化视图
     * @private
     */
    initView:function(){
        var me = this;
        //功能栏
        var north = me.createNorth();
        //效果展示区
        var center = me.createCenter();
        //属性面板
        var east = me.createEast();
        //列属性列表
        center.itemId = "center";
        north.itemId = "north";
        east.itemId = "east";
        //功能块位置
        center.region = "center";
        north.region = "north";
        east.region = "east";
        
        //功能块大小
        north.height = 30;
        east.width = 260;
        
        //功能块收缩属性
        east.split = true;
        east.collapsible = true;
        me.items = [north,center,east];  
    },
    /**
     * 效果展示面板
     * @private
     * @return {}
     */
    createCenter:function(){
        var me = this;
        var form={
            xtype:'form',
            title:'单列树',
            itemId:'center',
            border:0,
            autoScroll:true,
            bodyPadding:'1 10 10 1',
            items:[form]
        };
        return form;
    },
    
    /**
     * 功能栏
     * @private
     * @return {}
     */
    createNorth:function(){
        var me = this;
        var com = {
            xtype:'toolbar',
            border:false,
            items:[
                '<b>'+me.buildTitle+'</b>',
                '->',
                '-',
                {xtype:'button',text:'浏览',itemId:'browseX',iconCls:'build-button-see'},
                {xtype:'button',text:'保存',itemId:'save',iconCls:'build-button-save',
                    handler:function(){
                        me.save(true);
                    }
                },
                {xtype:'button',text:'另存',itemId:'saveAs',iconCls:'build-button-save',margin:'0 4 0 0',
					handler:function(){
						me.saveAs();
					}
				}
            ]
        };
        return com;
    },
 
    /**
     * 创建字段
     * @private
     * @return {}
     */
    createFields:function(){
        var me = this;
        var checked2=false;
        checked2=me.getcheckedtypeValue();//获取是否带复选框的值
        var fields=[];
        var fieldsStr='';
        if(checked2==true){
        	fieldsStr=me.defaultFieldsChecked;
        	fields=fieldsStr;
        }else{
        	fieldsStr=	me.defaultFields;
        	fields=fieldsStr;
        }
        return fields;
    },
    /**
     * 属性面板
     * @private
     * @return {}
     */
    createEast:function(){
        var me = this;
        //组件基础属性
        var basicItems =me.createBasicItems();
        //保存信息
        var appInfo = me.createAppInfo();
        //标题设置
        var title =me.createTreeTitleSet();
        //右键菜单设置
        var menuItems =me.createMenuItems();
        //数据对象
        var dataObj = me.createDataObj();
        //功能栏设置
        var tools = me.createButtons();
        //其他设置
        var other = me.createOther();
        var listParamsPanel = {
            xtype:'form',
            itemId:'center' + me.ParamsPanelItemIdSuffix,
            title:'树属性配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[appInfo,dataObj,other]
        };
        var basicParamsPanel = {
            xtype:'form',
            itemId:'centerbasic' + me.ParamsPanelItemIdSuffix,
            title:'组件基础属性',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[basicItems,title,menuItems]
         };
        var toolParamsPanel = {
            xtype:'form',
            itemId:'centertool' + me.ParamsPanelItemIdSuffix,
            title:'功能按钮配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[tools]
         };	
        var com = {
            xtype:'tabpanel',
            title:'树属性配置',
            autoScroll:true,
            items:[listParamsPanel,basicParamsPanel,toolParamsPanel]
        };
        return com;
    },
    /**
     * 功能信息
     * @private
     * @return {}
     */
    createAppInfo:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'功能信息',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'appInfo',
            items:[{
                xtype:'textfield',fieldLabel:'功能编号',labelWidth:55,anchor:'100%',
                itemId:'appCode',name:'appCode',labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textfield',fieldLabel:'中文名称',labelWidth:55,anchor:'100%',
                itemId:'appCName',name:'appCName',labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textareafield',fieldLabel:'功能简介',labelWidth:55,anchor:'100%',grow:true,
                itemId:'appExplain',name:'appExplain'
            }]
        };
        return com;
    },
    /**
     * 列表树标题栏设置
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createTreeTitleSet:function(){
        var me = this;
        var title=me.createTitle();
        var com = {
            xtype:'fieldset',title:'标题栏设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParamsTitleSet',
            name:'otherParamsTitleSet',
            items:title
        };
        return com;
    },
    /**
     * 树标题栏属性
     * @private
     * @return {}
     */
    createTitle:function(){
        var me=this;
        var com = [{
            xtype: 'radiogroup',
            itemId:'IsShowTitle',
            labelWidth:80,
            fieldLabel:'标题栏设置',
            columns:2,
            vertical:true,   
            items:[
                {boxLabel:'显示',name:'IsShowTitle',inputValue:'true'},
                {boxLabel:'隐藏',name:'IsShowTitle',inputValue:'false'}
            ]
        },{
            xtype:'textfield',fieldLabel:'标题栏名称',labelWidth:80,anchor:'100%',
            itemId:'titleName',name:'titleName'
        }];
        return com;
    },
    /**
     * 创建数据对象
     * @private
     * @return {}
     */
    createDataObj:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'数据对象',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'dataObject',
            items:[{
                xtype:'combobox',fieldLabel:'获取数据',
                itemId:'getDataServerUrl',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                name:'getDataServerUrl',
                labelWidth:55,anchor:'100%',
                editable:true,
                typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                mode:'local',
                emptyText:'请选择获取数据服务',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                listeners:{
                    select:function(combo, records, eOpts ){
                    var newValue=combo.getValue();
                    if(newValue!=null&&me.isJustOpen==false){
                             me.browse();
                         }
                    },
                    change:function(combo,newValue,oldValue,eOpts ){
                         if(newValue!=null&&me.isJustOpen==true){
                            me.isJustOpen=false;
                         }
                     }
                },
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl+ "?" + me.objectServerParam + "=Tree",
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData  //数据适配  
                    },//autoLoad:true,
                    listeners:{
                        beforeload:function(store,operation,eOpts){ 
                            store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=Tree";
                       }
                    }
                })
            },
            {xtype:'combobox',fieldLabel:'删除数据',
                itemId:'delDataServerUrl',
                name:'delDataServerUrl',
                labelWidth:55,anchor:'100%',
                editable:true,
                typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                mode:'local',
                emptyText:'请选择删除数据服务',
                displayField:me.delServerDisplayField,
                valueField:me.delServerValueField,
                listeners:{
                  select:function(combo, records, eOpts ){
                    },
                  change:function( combo,newValue,oldValue,eOpts ){
                    
                    }
                },
                store:new Ext.data.Store({
                    fields:me.delServerFields,
                       proxy:{
                        type:'ajax',
                        url:me.delServerUrl+"?EntityName=Bool",
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    },
                    listeners:{
                        beforeload:function(store,operation,eOpts){
                            store.proxy.url = me.delServerUrl+"?EntityName=Bool"; 
                        }
                    }
                })  
            },{
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
                       
                    },
                    beforequery:function(e){
                        var combo = e.combo;
                        if(!e.forceAll){
                            var value = e.query;
                            combo.store.filterBy(function(record,id){
                                var text = record.get(combo.displayField);
                                return (text.indexOf(value)!=-1);
                            });
                            combo.expand();
                            return false;
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'修改数据',
                itemId:'editDataServerUrl',name:'editDataServerUrl',
                labelWidth:55,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择修改数据服务',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectSaveDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    },
                    listeners:{
                        beforeload:function(store,operation,eOpts){
                            var objectName = me.getobjectName();
                            store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + objectName.value;
                            
                        }
                    }
                })
            },
             {
                xtype:'textfield',fieldLabel:'默认条件',labelWidth:55,value:'',
                itemId:'defaultParams',name:'defaultParams',hidden:true
            }]
        };
        return com;
    },
    
    /**
     * 创建其他设置
     * @private
     * @return {}
     */
    createOther:function(){
        var me=this;
        var com = {
            xtype:'fieldset',title:'其他',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'other',
            items:[{
                xtype:'textfield',fieldLabel:'空数据提示', labelWidth:55,anchor:'100%',value:'没有数据！',
                itemId:'emptyText',name:'emptyText'
            },{
                xtype:'textfield',fieldLabel:'加载提示', labelWidth:55,anchor:'100%',value:'获取数据中，请等待...',
                itemId:'loadingText',name:'loadingText'
            },{
                xtype:'checkbox',boxLabel:'开启遮罩层',checked:true,
                fieldLabel:'',hideLabel:true,labelWidth:65,
                itemId:'hasLoadMask',name:'hasLoadMask'
            }]
        };
        return com;
    },
    
    browse:function(){
        var me = this;
        var center=me.getCenterCom();
        var owner = center.ownerCt;
            center.removeAll();
            //重新生成新的控件
            var com =me.createTree();
            var coms=[];
            coms.push(com);
            center.add(coms);
   },

    /**
     * 获取展示区域树组件
     * @private
     * @return {}
     */
    getTreeCom:function(){
        var me = this;
        var objectName=me.getobjectName();
        var center = me.getComponent('center').getComponent('center');
        return center;
    },
   /**
    * 获取展示区域树组件
    * @private
    * @return {}
    */
   getCenterTreeCom:function(){
       var me = this;
       var getDataServerUrl=me.getDataServerUrl();
       var center = me.getComponent('center').getComponent(getDataServerUrl.getValue());
       return center;
   },
    /**
     * 获取数据对象控件
     * @private
     * @return {}
     */
    getdataObject:function(){
        var panel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var dataObject = panel.getComponent('dataObject');
        return dataObject;
    }, 
    /**
     * 获取数据对象控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getDataServerUrl:function(){
        var dataObject =this.getdataObject();
        var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        return getDataServerUrl;
    },
    /**
     * 获取展示区域
     * @private
     * @return {}
     */
    getCenterCom:function(){
        var me = this;
        var center = me.getComponent('center');//.getComponent('center');
        return center;
    },
    /**
     * 获取展示区域树组件
     * @private
     * @return {}
     */
    getCenterTreeCom:function(){
        var me = this;
        var getDataServerUrl=me.getDataServerUrl();
        var center = me.getComponent('center').getComponent(getDataServerUrl.getValue());
        return center;
    },
    /**
     * 菜单选项内容是否可见
     * @private
     * @param {} columnParams
     * @return {}
     */
    createSortableColumns:function(columnParams){
        var sortableColumns = false;
        for(var i in columnParams){
            if(columnParams[i].CanSort && !columnParams[i].CannotSee){
                sortableColumns = true;
            }
        }
        return sortableColumns;
    },
    //=====================事件处理=======================
    /**
     * 保存按钮事件处理
     * @private
     */
    save:function(bo){
        var me = this;
        //表单参数
        var params = me.getPanelParams();
        var isOk = true;
        var message = "";
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
        }else{              
            //设计代码（还原代码）
            var appParams = me.getAppParams();
            //类代码
            var appClass = me.createAppClass();
            //应用组件ID
            var id = bo ? me.appId : -1;
            //生成应用对象
            var BTDAppComponents = {
                Id:id,//应用组件ID
                CName:params.appCName,//名称
                ModuleOperCode:params.appCode,//功能编码
                ModuleOperInfo:params.appExplain,//功能简介
                InitParameter:params.defaultParams,//初始化参数
                AppType:me.appType,//10,//应用类型(单列树)
                BuildType:1,//构建类型
                DesignCode:me.JsonToStr(appParams),//设计代码
                ClassCode:appClass//类代码
            };
            
            if(me.DataTimeStamp != ""){
                BTDAppComponents.DataTimeStamp = me.DataTimeStamp;//时间戳
            };
            
            var callback = function(){
                me.fireEvent('saveClick');
            };
            //后台保存数据
            me.saveToServer(BTDAppComponents,callback);
        }     
    },
    /**
     * 将构建结果保存到数据库中
     * @author hujie eidt 2013-06-08
     * @private
     * @param {} obj
     * @param {} callback
     */
    saveToServer:function(obj,callback){
        var me = this;
        var url = "";
        if(obj.Id != -1){
            url = me.editServerUrl;//修改
        }else{
            url = me.addServerUrl;//新增
        }
        Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            params:obj,
            //params:Ext.JSON.encode({entity:obj}),
            method:'POST',
            timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    Ext.Msg.alert('提示','保存成功！');
                    if(Ext.typeOf(callback) == "function"){
                        callback();//回调函数
                    }
                }else{
                    Ext.Msg.alert('提示','保存应用组件失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','保存应用组件请求失败！');
            }
        });
    },
//============================修改类代码用================    
    /**
     * 获取设计代码
     * @private
     * @return {}
     */
    getAppParams:function(){
        var me = this;
        //树属性面板
        var panelParams = me.getPanelParams();
        //基础属性面板
        var BasicParams = me.getPanelBasicParams();
        //按钮设置属性面板
        var ToolParams = me.getPanelToolParams();
	    var appParams = {
	        panelParams:panelParams,
	        BasicParams:BasicParams,
	        ToolParams:ToolParams
	    }; 
        return appParams;
    },
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
                timeout:8000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        if(result.ResultDataValue && result.ResultDataValue != ""){
                            var appInfo = Ext.JSON.decode(result.ResultDataValue);
                            if(Ext.typeOf(callback) == "function"){
                                callback(appInfo);//回调函数
                            }
                        }else{
                            Ext.Msg.alert('提示','没有获取到应用信息！');
                        }
                    }else{
                        Ext.Msg.alert('提示','获取应用信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                    }
                },
                failure : function(response,options){ 
                    Ext.Msg.alert('提示','获取应用信息请求失败！');
                }
            });
        }
    },    
//============================修改类代码用================

    createColumnsStr:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getColumnParams();
        var columnsStr = "[";
        for(var i in columnParams){
            var col = 
            "{" + 
                "text:'" + columnParams[i].DisplayName + "'," + 
                "dataIndex:'" + columnParams[i].InteractionField + "'," + 
                "align:'" + columnParams[i].AlignType + "'," + 
                "sortable:" + columnParams[i].CanSort + "'" +
            "}";
            columnsStr = columnsStr + col + ",";
        }
        if(columnsStr.length > 1){
            columnsStr = columnsStr.substring(0,columnsStr.length-1);
        }
        columnsStr += "]";
        return columnsStr;
    },
    /**
     * 获取组件属性列表Fields
     * @private
     * @return {}
     */
    getSouthStoreFields:function(){
        var me = this;
        var fields = [
            {name:'DisplayName',type:'string'},//树类型显示名称
            {name:'InteractionField',type:'string'},//交互字段
            {name:'CanSort',type:'bool'},//可排序
            {name:'DefaultSort',type:'bool'},//默认排序
            {name:'SortType',type:'string'},//排序方式
            {name:'OrderNum',type:'int'}//排布顺序
        ];
        return fields;
    },

    /**
     * 保存代码
     * @private
     * @param {} data
     */
    saveApp:function(data){
        var me = this;
        var url = "";
        if(me.appId != -1){
            url = me.editServerUrl;//修改
        }else{
            url = me.addServerUrl;//新增
        }
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            params:data,
            method:'POST',
            timeout:2000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    Ext.Msg.alert('提示','保存成功！');
                }else{
                    Ext.Msg.alert('提示','保存应用组件失败！错误信息【<b style="color:red">'+ result.errorInfo +"</b>】");
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','保存应用组件请求失败！');
            }
        });
    },
    //================================事件监听=======================================
    /**
     * 初始化监听
     * @private
     */
    initListeners:function(){
        var me = this;
        //功能栏监听
        me.initNorthListener();
        //属性面板监听
        me.initEastListener();
    },
    /**
     * 功能栏监听
     * @private
     */
    initNorthListener:function(){
        var me = this;
        //浏览按钮事件    
        me.initBrowseListener();
        //保存按钮事件    
        me.initSaveListener();
    },
    
    /**
     * 功能栏浏览按钮监听
     * @private
     */
    initBrowseListener:function(){
        var me = this,
        north = me.getComponent('north');  
        //浏览按钮事件    
        var browse = north.getComponent('browseX');
        if(browse){
        	browse.on({
                click:function(){
                    me.browse();
                }
            });
        }
    },
    
    /**
     * 功能栏保存按钮监听
     * @private
     */
    initSaveListener:function(){
        var me = this,
        north = me.getComponent('north');
        //保存按钮事件    
        var save = north.getComponent('save');
        if(save){
            save.on({
                click:function(){
                    me.fireEvent('btnsaveClick');
                }
            });
        }
    },

    //======================生成保存代码======================
    /***
     * 真树处理
     * @return {}
     */
    createSetNodeStr:function(){
        var me=this;
        var setNode="";
            setNode="function(n) {" +
            "var me=this;" +
            "n.expand();" +
            "n.data.checked = true;" +
            "n.updateInfo({ checked: true });" +
            "var childs=n.childNodes;" +
                "for (var i = 0; i < childs.length; i++) {" +
                    "childs[i].data.checked = true;" +
                    "childs[i].updateInfo({ checked: true });" +
                    "if(childs[i].data.leaf==false)" +
                    "{" +
                       "me.setNode(childs[i]);" +
                    "}" +
                "}" +
        "}";
        
      return setNode;  
    },
    /***
     * 真树处理
     * @return {}
     */
    createSetNodefalseStr:function(){
        var me=this;
        var setNodefalse="";
        setNodefalse=
            "function(n) {" +
                "var me=this;" +
                "n.data.checked = false;" +
                "n.updateInfo({ checked: false });" +
                "var childs=n.childNodes;" +
                "for (var i = 0; i < childs.length; i++) {" +
                    "childs[i].data.checked = false;" +
                    "childs[i].updateInfo({ checked: false });" +
                    "if(childs[i].data.leaf==false){" +
                       "me.setNodefalse(childs[i]);" +
                    "}" +
                 "}" +
             "}";
      return setNodefalse;  
    },  
    /***
     * 遍历子结点 选中 与取消选中操作
     * @return {}
     */
    createChdStr:function(){
        var me=this;
        var chd="";
        chd=
            "function(node,check){" +
            "node.set('checked',check);" +
            "if(node.isNode){" +
                "node.eachChild(function(child){" +
                    "if (node.isLeaf){" +
                        
                    "}" +
                    "else" +
                    "{" +
                       "chd(child,check);" +
                    "}" +
                "}); " +
            "}" +
        "}";
      return chd;  
    }, 
    /***
     * 监听树的选择事件
     * @return {}
     */
    createListenersCheckchangeStr:function(){
        var me=this;
        var checkchange="";
        checkchange=
            "function(node,checked){"+ 
                "var me = this;"+ 
                "if (me.isTure ==false) {"+ //假树
                    "if(checked){"+ 
                           //打开父节点
                        "node.expand();"+ 
                        //遍历子节点
                        "node.eachChild(function (child){"+ 
                            "child.data.checked=true;"+ 
                            "child.updateInfo({checked:true});"+ 
                          "if(child.data.leaf==false){"+ 
                              "me.setNode(child);"+ 
                          "}"+ 
                            "me.chd(child,true);"+ 
                        "});"+ 
                    "}else{"+ 
                         "node.expand();"+ 
                         //遍历子节点
                        "node.eachChild(function (child){"+ 
                            "child.data.checked=false;"+ 
                            "child.updateInfo({checked:false}); "+ 
                             "if(child.data.leaf==false){"+ 
                                 "me.setNodefalse(child);"+ 
                             "}"+ 
                            "me.chd(child,false);"+ 
                       " });"+ 
                    "}"+ 
                    "me.parentnode(node);"+ //进行父级选中操作 
                    
               "}else{ "+          
                    "if(node.data.leaf==false){"+      //当选中的不是叶子时
                        "if(checked){"+ 
                             //打开父节点
                             "node.expand();"+ 
                             //遍历子节点
                             "node.eachChild(function(n){"+ 
                                  "n.data.checked=true;"+ 
                                  "n.updateInfo({checked:true});"+ 
                                  "if(n.data.leaf==false){"+ 
                                      "me.setNode(n);"+ 
                                  "}"+ 
                             "});"+ 
                          "}else{"+ 
                             " node.expand();"+ 
                                  //遍历子节点
                             " node.eachChild(function(n){"+ 
                                  "n.data.checked=false;"+ 
                                  "n.updateInfo({checked:false}); "+ 
                                  "if(n.data.leaf==false){"+ 
                                      "me.setNodefalse(n);"+ 
                                  "}"+ 
                              "});"+ 
                          "}"+ 
                     "}"+ 
                     "else{"+ 
                        "node.parentNode.data.checked=false;"+ 
                        "node.parentNode.updateInfo({checked:false}); "+ 
                     "}"+ 
                "}"+ 
                "me.fireEvent('OnChanged');"+ 
           "}";
      return checkchange;  
    }, 
     /***
     * 监听树的行双击事件
     * @return {}
     */
    createListenersItemdblclickStr:function(){
        var me=this;
        var itemdblclick="";
        itemdblclick=
            "itemdblclick:function(myTree,record, item, index,e,eOpts ){"+ 
                "var me = this;"+ 
                "me.fireEvent('itemdblclick');"+ 
           "}";
      return itemdblclick;  
    }, 
    /**
     * 树的获取结果行的公开方法
     * @private
     * @return {}
     */
    getValueStr:function(){
        var me=this;
        var fun="";
        fun="function(){"+
        "var myTree = this;"+
        "var arrTemp =[];" +
        "arrTemp =myTree.getSelectionModel().getSelection();"+
        "return arrTemp;"+
       "}";
        return fun;
    },
    /**
     * 从数据库中删除记录
     * @private
     * @return {}
     */
    deleteModuleServerStr:function(){
        var me=this;
        var fun="";
        fun="function(id,record){"+
        "var me = this;"+
        "var url = me.deleteServerUrl + '?id=' + id;"+
        "Ext.Ajax.request({"+
			"async:false,"+//非异步
			"url:url,"+
			"method:'GET',"+
			"timeout:2000,"+
			"success:function(response,opts){"+
				"var result = Ext.JSON.decode(response.responseText);"+
				"if(result.success){"+
					"callback();"+
					"me.load();"+
				"}else{"+
					"Ext.Msg.alert('提示','删除模块失败！');"+
				"}"+
			"},"+
			"failure:function(response,options){ "+
				"Ext.Msg.alert('提示','连接删除模块服务出错！');"+
			"}"+
		"});"+
       "}";
          return fun;
    },
    /**
     * 删除模块
     * @private
     * @return {}
     */
    deleteModuleStr:function(){
        var me=this;
        var fun="";
        fun="function(record){"+
        "var me = this;"+
        "Ext.Msg.confirm('警告','确定要删除吗？',function (button){"+
			"if(button == 'yes'){"+
			   "var recorda=me.getSelectionModel().getSelection(); "+
			   	"for(var i in recorda){"+
    				"var id = recorda[i].get('tid');"+
    			"}"+
				//删除后的处理
				"var callback = function(){"+
					"var node = me.getRootNode().findChild('tid',id,true);"+
		    		"node.remove();"+
				"};"+
				"if(recorda[i].get('Url') == '../../manage/file/modulemanage.html'){"+
					"Ext.Msg.alert('提示','不能删除此功能！');"+
				"}else{"+
					"me.deleteModuleServer(id,callback);"+
				"}"+
			"}"+
		"});"+
       "}";
         return fun;
    },
    /***
    * 创建右键菜单的删除按钮
    * @return {}
    */
   createContextmenuDeletebtnStr:function(){
       var me=this;
       var fun="";
       fun="function(){"+
       "var me = this;"+
       "var com='';" +
       "com={" +
             "text:'删除'," +
             "iconCls:'delete'," +
             "handler:function(btn,e,optes){" +
                 "me.fireEvent('delClick');" +
                 "me.deleteModule();" +
             "}" +
         "};"+
       "return com;"+
      "}";
       return fun;
   },
    /**
     * 创建类代码
     * @private
     * @return {}
     */
    createAppClass:function(){
       var me = this;
        //表单配置参数
       var params = me.getPanelParams();
       //获取树的服务地址
       var getDataServerUrl =me.getDataServerUrl();
       if(getDataServerUrl.getValue()==null){
    	   selectServerUrl='';
    	   return;
       }
       selectServerUrl=""+getDataServerUrl.getValue().split('?')[0]; 
       
       var delServerUrl =me.getdelDataServerUrl();
       if(delServerUrl.getValue()==null){
    	   deleteServerUrl='';
       }else{
           deleteServerUrl=""+delServerUrl.getValue().split('?')[0]; 
       }
       
       var editDataServerUrl =me.geteditDataServerUrl();
       if(editDataServerUrl.getValue()==null){
           editDataServerUrl='';

       }else{
           editDataServerUrl=""+editDataServerUrl.getValue().split('?')[0]; 
       }
       var title2='';
       var isSetTitle=false;//取标题栏设置是否显示的值
       isSetTitle=me.getisShowTitleValue();
       
       //获取标题名称的值
       title2=me.gettitleNameValue();
       if(isSetTitle==false){
            title2='';
       }
       var chd=me.createChdStr();//遍历子结点 选中 与取消选中操作
       var setNode=me.createSetNodeStr();//真树     
       var setNodefalse=me.createSetNodefalseStr();//假树
       var parentnodeStr=me.parentnodeStr();
       var nodep=me.createNodepStr();//向上遍历父节点
       var mycreateStore=me.createStoreStr();//创建加载数据源
       var changeStoreData=me.changeStoreDataStr();
       var checkchangeClick=me.createListenersCheckchangeStr();//真,假树判断
       
       width=me.gettreeWidthValue();
       height=me.gettreeHeightValue();
       var lines=false;
       //获取样式选择的的值
       lines=me.getlinestypeValue();
       var useArrows=true;
       if(lines==false){
           useArrows=true;
       }else{
           useArrows=false;
       }
       var rootVisible=false;
       rootVisible=me.getrootVisibletypeValue();
       var checked=false;
       checked=me.getcheckedtypeValue();//获取是否带复选框的值
       var drag=false;// 是否是否允许拖拽
       drag=me.getdrogtypeValue();

       var isTure=false;//是否级联
       isTure=me.getisTuretypeValue();
      
       //过滤
       var clearFilterStr =me.clearFilterStr();
       var filterByTextStr=me.filterByTextStr();
       var filterByStr=me.filterByStr();
       //删除
       var deleteModuleServer=me.deleteModuleServerStr();//从数据库中删除记录
       var deleteModule=me.deleteModuleStr();//删除模块方法
       var values = me.getButtonsConfigValues();

       //右键菜单
       var isMenu=false;//
       isMenu=me.getisShowMenuValue();
       var isDelMenuBtn=false;//取是否显示右键菜单删除按钮的值
       isDelMenuBtn=me.getisDelMenuBtnValue(); 

       var Menu=[];
       if(isMenu==true){
       	if (isDelMenuBtn==true){
       		Menu="[{" +
	       		"text:'删除'," +
	            "iconCls:'delete'," +
	            "handler:function(grid,rowIndex,colIndex,item,e,record){" +
	                "me.deleteModule(record);" +
			        "me.fireEvent('delClick');" +
	            "}" +
	            "}" +
       		"]";
       	}else{
       		Menu="[{}]";
       	}
       }else{
    	   Menu="[{}]";
       }
 
       //获取应用列表服务地址
       var getAppListServerUrl="ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL";
       //上传图片文件服务地址
       var updateFileServerUrl="ConstructionService.svc/ReceiveModuleIconService";
       
       var vConfig="";//是否允许拖拽
       var drag=false;// 是否是否允许拖拽
       drag=me.getdrogtypeValue();
       if(drag==true){
            vConfig="{" +
                "plugins: {" +
                    "ptype:'treeviewdragdrop'," +
                    "allowLeafInserts:true" +
                "}," +
                "listeners:{" +
                    "beforedrop:function(node,data,overModel,dropPosition,dropFunction,eOpts ){}" +
               "}" +
           "}" ;
        }else{
            vConfig="''";
        }
        var appClass =  
            "Ext.define('" + params.appCode + "',{" + 
                "extend:'Ext.tree.Panel'," + 
                "alias:'widget." + params.appCode + "'," + 
                "title:'" + title2 + "'," + 
                
                "getAppInfoServerUrl:getRootPath()+"+"'/'+"+"'"+"ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById"+"',"+
                "getAppListServerUrl:getRootPath()+"+"'/'+"+"'"+getAppListServerUrl+"',"+
                "updateFileServerUrl:getRootPath()+"+"'/'+"+"'"+updateFileServerUrl+"',"+
                "selectServerUrl:getRootPath()+"+"'/'+"+"'"+selectServerUrl+"',"+  //树的查询数据地址
                "deleteServerUrl:getRootPath()+"+"'/"+deleteServerUrl + "',"+ //树的删除操作数据地址
                "editDataServerUrl:getRootPath()+"+"'/'+"+"'"+editDataServerUrl+"',"+  //树的修改数据地址
                
                "autoScroll:true," +
                "filterfield:'text'," +//过滤文本框过滤匹配字段
                "childrenField:'Tree',"+
                "chd:"+chd+"," + //遍历子结点 选中 与取消选中操作
                "setNodefalse:"+setNodefalse+"," + //假树 
                "setNode:"+setNode+"," + // 真树判断 
                "viewConfig: " + vConfig + "," + 
                "parentnode:"+ parentnodeStr+"," +
                "width:" + width + "," +
                "height:" + height + "," + 
                "lines:" + lines + "," +
                "isTure:" + isTure + "," + 
                "useArrows:" + useArrows + "," + 
                "rootVisible:" + rootVisible + "," + 
                "checked:" + checked + "," + 
                //过滤
                "clearFilter:"+clearFilterStr+","+ //过滤
                "filterByText:"+filterByTextStr+"," +//过滤
                "filterBy:"+filterByStr+"," +//过滤
      
                "internalWhere:"+'\\"' + params.defaultParams +'\\",' + //内部hql
                "externalWhere:''," + //外部hql
                
                "nodep:"+ nodep+"," +//向上遍历父节点方法
                "createStore:"+ mycreateStore+"," +//创建数据源:数据加载方法
                "changeStoreData:"+changeStoreData+",";//数据适配方法

                appClass=appClass+
                "deleteModuleServer:"+deleteModuleServer+"," +//从数据库中删除记录方法
                "deleteModule:"+deleteModule+"," +//删除模块方法

                
                //删除数据的方法
                "getValue:"+me.getValueStr()+"," +//树的获取结果行的方法
                "openFormWin:" + me.createOpenFormWinStr() + "," + 
                "getAppInfoFromServer:" + me.getAppInfoFromServerStr() + "," ;

                appClass=appClass+"afterRender:function(){" + 
                "var me=this;" + 
                "me.callParent(arguments);" + 
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
               "}," ;
               //树节点移动事件处理
                if(drag==true){
            	   appClass=appClass+"updateNode:"+me.updateNodeStr()+"," ;//树和列表树的节点改变方法
                }
                appClass=appClass+
                 "initComponent:function(){" + 
                   "var me=this;" ;
	              //挂靠
					var dockedItems = me.createDockedItemsStr();
				  	if(dockedItems != ""){
						appClass = appClass + "me.dockedItems=" + dockedItems + ";";
					}
             //树事件监听
            appClass=appClass+ "me.listeners=me.listeners||[];"
            
            appClass=appClass+ "me.listeners.checkchange="+checkchangeClick+";";//真假树事件监听
                       
            //右键菜单事件处理
	        if(isMenu==true){ 
	             appClass=appClass+ "me.listeners.contextmenu={"+
		                 "element:'el',"+
		                 "fn:function(e,t,eOpts){"+
	                     //禁用浏览器的右键相应事件 
	                     "e.preventDefault();e.stopEvent();"+
	                     //右键菜单
	                     "new Ext.menu.Menu({"+
	                         "items:" + Menu+ "" +//右键菜单
	                     "}).showAt(e.getXY());"+//让右键菜单跟随鼠标位置
                        "}"+
	                 "};";
	        }
                    //树节点移动事件处理
        if(drag==true){
            appClass=appClass+ 
                //节点移动完成后
                "me.listeners.itemmove=function( nodeInterface, oldParent, newParent, index, eOpts ){" +
                    //更新树
                    "var newParent3=newParent.data;" +
                    "var node3=nodeInterface.data;" +
                    "var node2={Id:node3.tid.toString(),ParentID:newParent3.tid.toString()};" +
                    "var boolResult=me.updateNode('edit',node2);"+
                    //"me.load('');"+
                "};";
                
              }  
                    appClass=appClass+ 
                    "me.addEvents('addClick');"+ //树的新增按钮单击事件
                    "me.addEvents('editClick');"+ //树的查看按钮单击事件
                    "me.addEvents('delClick');"+ //树的删除按钮单击事件
                    "me.addEvents('cancelClick');"+ //树的取消按钮单击事件
                    "me.addEvents('okClick');"+ //树的确定按钮单击事件
                    "me.addEvents('OnChanged');"+ //
                    "me.store=me.createStore();"+
		                //加载数据的方法
		             "me.load=function(whereStr){" + 
		                "var w='?isPlanish=true&where=';" + 
                        "var myUrl=me.selectServerUrl+w;"+ 
		                "me.store.proxy.url=myUrl;" + 
		                "me.store.load();" + 
		            "};" + 
                     "this.callParent(arguments);" + 
                "}" + 
            "});"; 
        return appClass;
    },  
    /***
     * 树和列表树的节点改变方法
     * type可以是add，也可以是edit
     * @return {}
     */
    updateNodeStr:function(){
        var updateNode="";
        updateNode="function(type,node){" + 
                "var me=this;" + 
                "var url = '';" +
                "if(type=='edit'){" +
                    "url = ''+me.editDataServerUrl;" +//修改节点服务 editDataServerUrl
                "}else if(type=='add'){" +
                    "url = '';" +//新增节点服务addNodeServerUrl
                "}" +
                
                "var field='Id,ParentID';"+
                "Ext.Ajax.defaultPostHeader = 'application/json';"+
                "Ext.Ajax.request({" +
                    "async:false," +//非异步
                    "url:url," +
                    "params:Ext.JSON.encode({entity:node,fields:field})," +
                    "method:'POST'," +
                    "timeout:5000," +
                    "success:function(response,opts){" +
                        "var result = Ext.JSON.decode(response.responseText);" +
                        "if(result.success){" +
//                            "if(Ext.typeOf(callback) == 'function'){" +
//                               " callback();" +//回调函数
//                            "}"+
                        "}else{" +
                            "Ext.Msg.alert('提示','操作请求失败！');" +
                        "}" +
                    "}," +
                    "failure : function(response,options){ " +
                        "Ext.Msg.alert('提示','操作请求失败！');" +
                    "}" +
                "});" +
         "}";
         return updateNode;
    },
    /***
     * 树节点移动后保存方法
     * @return {}
     */
    updateNode:function(type,node){
        var me=this; 
        var url = '';
        if(type=='edit'){
            var editDataServerUrl =me.geteditDataServerUrl().getValue();
            if(editDataServerUrl.length>0){
                editDataServerUrl=editDataServerUrl.split('?')[0];
                url = getRootPath()+'/'+editDataServerUrl;//修改节点服务 editDataServerUr
            }else{
            Ext.Msg.alert('提示','请选择修改数据项');
            return false;
            }
        }else if(type=='add'){
            url = '';//新增节点服务addNodeServerUrl
        }
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            //params:node,
            params:Ext.JSON.encode({entity:node,fields:'Id,ParentID'}),
            method:'POST',
            timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
//                    if(Ext.typeOf(callback) == 'function'){
//                        callback();//回调函数
//                    }
                }else{
                    Ext.Msg.alert('提示','操作请求失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','操作请求失败！');
            }
        });
    },
 
      /**
       * 创建字段
       * @private
       * @return {}
       */
      createFieldsStr:function(){
          var me = this;
          var checked2=false;
          checked2=me.getcheckedtypeValue();//获取是否带复选框的值
          var fields=[];
          var fieldsStr='';
          if(checked2==true){
        	  fieldsStr=Ext.encode(me.defaultFieldsChecked);
        	  fields=fieldsStr.replace(/"/g,"'");
          }else{
        	  fieldsStr=Ext.encode(me.defaultFields);
        	  fields=fieldsStr.replace(/"/g,"'");
          }
          
          return fields;
      },
    /**
     * 创建数据集
     * @private
     * @return {}
     */
    createStoreStr:function(){
        //服务地址
        var me=this;
        var fields=""; 
        var fields = me.createFieldsStr();
        rootName=me.getrootNameValue();
        var fun="";
        fun="function(where){"+
          "var me = this;"+
          "var w='?isPlanish=true&where=';" + 
	        "if(me.internalWhere){" + 
	            "w+=me.internalWhere;" + 
	        "}" + 
	        "if(where&&where!=''){" + 
	            "if(w!=''){" + 
	                "w+=' and '+where;" + 
	            "}else{" + 
	                "w+=where;" + 
	            "}" + 
	        "}" + 
            "var myUrl=me.selectServerUrl+w;"+         
            "var store = Ext.create('Ext.data.TreeStore',{"+
                "fields:"+fields+","+
                "proxy:{"+
                    "type:'ajax',"+
                     "url:myUrl," + //me.selectServerUrl
                    "extractResponseData:function(response){return me.changeStoreData(response);}"+
                "},"+
                "defaultRootProperty:me.childrenField,"+
                "root:{"+
                    "text:'"+rootName+"',"+
                    "leaf:false,"+
                    "ParentID:0,"+
                    "Id:0,"+
                    "tid:0"+
                    ",expanded:true"+
                "},"+
                
                //添加对刷新按钮的控制
                "listeners: {"+
                "load:function(treeStore, node,records,successful,eOpts){"+
                    "var treeToolbar=me.getComponent('treeToolbar');"+
                    "if(treeToolbar==undefined||treeToolbar==''){"+
                       "treeToolbar=tree.getComponent('treeToolbarTwo');"+
                    "}"+
                    "treeToolbar=treeToolbar.getComponent('treeToolbar');"+
                    "if(treeToolbar&&treeToolbar!=undefined){"+
                        "var refresh=treeToolbar.getComponent('refresh');"+
                        "if(refresh&&refresh!=undefined){"+
                            //数据获取成功后,刷新按钮可用
                            "refresh.disabled=false;"+
                        "}"+
                    "}"+
                    
                "}"+
            "}"+
                
            "});"+
            "return store;"+
            "}";
          return fun;
    },

    /***
     * 父节点选中
     * @return {}
     */
    parentnodeStr:function(){
        var parentnode="";
         parentnode="function (node){" +
            "var me=this;" +
            "if(node.parentNode != null){" +
                "if(me.nodep(node.parentNode)){" +
                    "node.parentNode.set('checked',true);" +
                "}else{ " +
                    "node.parentNode.set('checked',false);" +
                "}" +
                "this.parentnode(node.parentNode);" +
            "}" +
         "}";
         return parentnode;
    },
    /***
     * 向上遍历父节点
     * @return {}
     */
    createNodepStr:function(){
        var nodep="";
         nodep="function (node){" +
            "var me=this;" +
        "var bnode=true;" +
        "Ext.Array.each(node.childNodes,function(v){ " +
            "if(!v.data.checked){" +
                "bnode=false;" +
                " return;" +
            "}" +
        "});" +
        "return bnode;" +
         "}";
         return nodep;
    },
    /**
     * 数据适配类代码Str
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreDataStr:function(){
        var fun="";
        fun="function(response){"+
        "var me = this;"+
        "var data = Ext.JSON.decode(response.responseText);"+
        "var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);"+
        "data[me.childrenField] = ResultDataValue.Tree;"+
        "var changeNode = function(node){"+
            "var value = node['value'];"+
            "for(var i in value){"+
                "node[i] = value[i];"+
           "}"+
            //时间处理
            "node['DataAddTime'] = getMillisecondsFromStr(node['DataAddTime']);"+
            "node['DataUpdateTime'] = getMillisecondsFromStr(node['DataUpdateTime']);"+
            //图片地址处理
            "if(node['icon'] && node['icon'] != ''){"+
               " node['icon'] = getIconRootPathBySize(16) + '/' + node['icon'];"+
           " }"+
            
            "var children = node[me.childrenField];"+//childrenField属性
            "if(children){"+
                "changeChildren(children);"+
            "}"+
        "};"+
        
        "var changeChildren = function(children){"+
            "Ext.Array.each(children,changeNode);"+
        "};"+
        "var children = data[me.childrenField];"+//childrenField
        "changeChildren(children);"+
        "response.responseText = Ext.JSON.encode(data);"+
        "return response;"+
    "}";
    return fun;
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
    //======================功能栏按钮的方法====================

    /**
     * 数据适配
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreData: function(response){
        var data = Ext.JSON.decode(response.responseText);
        var ResultDataValue = [];
        if(data.ResultDataValue && data.ResultDataValue != ""){
            ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
        }
        data.ResultDataValue = ResultDataValue;
        response.responseText = Ext.JSON.encode(data);
        return response;
    },
    /**
     * 数据适配（处理树）
     * @private
     * @param {} response
     * @return {}
     */
    changeTreeStoreData: function(response){
        var me = this;
        var data = Ext.JSON.decode(response.responseText);
        var ResultDataValue = [];
        if(data.ResultDataValue && data.ResultDataValue != ""){
            ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
        }
        data[me.childrenField] = ResultDataValue.Tree;
        
        var changeNode = function(node){
            var value = node['value'];
            for(var i in value){
                node[i] = value[i];
            }
            //时间处理
            node['DataAddTime'] = getMillisecondsFromStr(node['DataAddTime']);
            node['DataUpdateTime'] = getMillisecondsFromStr(node['DataUpdateTime']);
            //图片地址处理
            if(node['icon'] && node['icon'] != ""){
                node['icon'] = getIconRootPathBySize(16) + "/" + node['icon'];
            }
            
            var children = node[me.childrenField];
            if(children){
                changeChildren(children);
            }
        };
        
        var changeChildren = function(children){
            Ext.Array.each(children,changeNode);
        };
        
        var children = data[me.childrenField];
        changeChildren(children);
        
        response.responseText = Ext.JSON.encode(data);
        return response;
    },

   
   //=================================创建======================================    
    /**
     * 创建参数代码
     * @private
     * @return {}
     */
    createAppParams:function(){
        var appParams = {};
        var me = this;
        //列表配置参数
        var params = me.getListParams();
        //列属性(已排序)
        var columnParams = me.getColumnParams();
        
        appParams.params = params;
        appParams.columnParams = columnParams;
        return appParams;
    },
    /**
     * 创建树数据集
     * @private
     * @return {}
     */
    createStore1:function(myUrl){
        var me = this;
        //配置参数
        var params = me.getPanelParams();
        //创建字段
        var myFields = me.createFields();
        //HQL串
        var where ="?isPlanish=true&where=";
        if(params.defaultParams){
            where += params.defaultParams;
        }
        var myUrlTemp = myUrl+ where;
        var store = Ext.create('Ext.data.TreeStore',{
            fields:myFields,
            proxy:{
                type:'ajax',
                url:myUrlTemp,
                extractResponseData:function(response){
                    return me.changeTreeStoreData(response);  
                }
            },
            
            defaultRootProperty:me.childrenField,
            root:{
                text:me.Root,
                leaf:false,
                ParentID:0,
                Id:0,
                tid:0
                //,expanded:true
            },
            folderSort: true,
            sorters: [{
                property: 'text',
                direction: 'ASC'
            }],
            listeners: {
                load:function(treeStore, node,records,successful,eOpts){
            	    var values = me.getButtonsConfigValues();
            	    if (values["toolbar-refresh-checkbox"] == true) {
            	        var tree = me.getTreeCom();
            	        if (tree && tree != undefined) {
            	            var treeToolbar = tree.getComponent("treeToolbar");
            	            if (treeToolbar == undefined || treeToolbar == "") {
            	                treeToolbar = tree.getComponent("treeToolbarTwo");
            	            }
            	            treeToolbar = treeToolbar.getComponent("treeToolbar");
            	            if (treeToolbar && treeToolbar != undefined) {
            	                var refresh = treeToolbar.getComponent("refresh");
            	                if (refresh && refresh != undefined) {
            	                    //数据获取成功后,刷新按钮可用
            	                    refresh.disabled = false;
            	                }
            	            }
            	        }
            	    }
                }
            }
        });
        return store;
    },   

    /**
	 * 功能按钮设置
	 * @private
	 * @return {}
	 */
	createButtons:function(){
		var me = this;
		var com = Ext.create('Ext.build.FunButFieldSetTree',{
			itemId:'buttonsconfig',
			defaults:{anchor:'100%'},
			layout:'anchor',
			padding:'0 5 0 5',
			collapsible:true,
			delServerDisplayField:me.objectServerDisplayField,
			delServerValueField:me.objectServerValueField,
			delServerUrl:me.objectGetDataServerUrl,
			delServerFields:me.objectServerFields,
			keyDisplayField:'DisplayName',
			keyValueField:'InteractionField',
			appId:me.appId//应用ID
		});
		return com;
	},
    
    /**
     * 创建基础属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createBasicItems:function(){
        var me = this;
        var basicItems={
            xtype:'fieldset',title:'基础属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'basicParams',
            name:'basicParams',
            items:[{
                xtype:'textfield',fieldLabel:'根节点名称',name:'Root',labelWidth:95,anchor:'100%',itemId:'Root'
            },{
                xtype:'numberfield',fieldLabel:'树宽',labelWidth:95,anchor:'100%',
                itemId:'treeWidth',name:'treeWidth',value:300,
                listeners:{
                }  
            },{
                xtype:'numberfield',fieldLabel:'树高',labelWidth:95,
                anchor:'100%',
                itemId:'treeHeight',name:'treeHeight',value:280,
                listeners:{
                }  
            },{
                xtype: 'radiogroup',
                itemId:'Linestype',
                labelWidth:95,
                fieldLabel:'样式选择',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'树线',name:'Linestype',inputValue:'true'},
                    {boxLabel:'箭头',name:'Linestype',inputValue:'false'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'rootVisibletype',
                labelWidth:95,
                fieldLabel:'是否显示根节点',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'是',name:'rootVisibletype',inputValue:'true'},
                    {boxLabel:'否',name:'rootVisibletype',inputValue:'false'}
                ]
            },{
                xtype: 'radiogroup',
                itemId:'Drogtype',
                labelWidth:95,
                fieldLabel:'是否允许拖拽',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'是',name:'Drogtype',inputValue:'true'},
                    {boxLabel:'否',name:'Drogtype',inputValue:'false'}
                ]
            },{
                xtype: 'radiogroup',
                itemId:'checked',
                labelWidth:95,
                fieldLabel:'是否带复选框',
                columns:2,
                vertical:true,
                 items:[
                    {boxLabel:'是',name:'Checkedtype',inputValue:'true'},
                    {boxLabel:'否',name:'Checkedtype',inputValue:'false'}
                ],
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                    }
                    
                   }
            },{
                xtype: 'radiogroup',
                itemId:'IsTuretype',
                labelWidth:95,
                fieldLabel:'是否级联',
                columns:2,
                vertical:true,
                listeners:{
                },               
                items:[
                    {boxLabel:'是',name:'IsTuretype',inputValue:'true'},
                    {boxLabel:'否',name:'IsTuretype',inputValue:'false'}
                ]
            }
            ]
        };
        return basicItems;
    },
	/***
	 * 右键菜单删除按钮
	 * @return {}
	 */
	createMenuDelBtn:function(){
       var me=this;
       menumelbtn={
	       text:"删除",iconCls:'delete',
		   handler:function(grid,rowIndex,colIndex,item,e,record){
		       me.deleteModule(record);
		       me.fireEvent('delClick');
	       }
       };
       return menumelbtn;
     },
	   /**
		 * 删除模块
		 * @private
		 */
	deleteModule:function(record){
		var me = this;
		Ext.Msg.confirm("警告","确定要删除吗？",function (button){
			if(button == "yes"){
			   var myTree=me.getCenterCom().getComponent('center');
			   var recorda=myTree.getSelectionModel().getSelection(); 
			   	for(var i in recorda){
    				var id = recorda[i].get('tid');
    			}
				//删除后的处理
				var callback = function(){
					var node = myTree.getRootNode().findChild("tid",id,true);
		    		node.remove();
				};
				if(recorda[i].get('Url') == "../../manage/file/modulemanage.html"){
					Ext.Msg.alert('提示','不能删除此功能！');
				}else{
					me.deleteModuleServer(id,callback);
				}
			}
		});
	},
	/**
	 * 从数据库中删除记录
	 * @private
	 * @param {} id
	 */
	deleteModuleServer:function(id,callback){
		var me = this;
	    var delServerUrl =me.getdelDataServerUrl();
        if(delServerUrl.getValue()==null){
        	deleteUrl='';
        }else{
        	deleteUrl=""+delServerUrl.getValue().split('?')[0]; 
        }
        var myUrl =""+getRootPath()+ "/"+deleteUrl + "?id=" + id;
    	Ext.Ajax.request({
			async:false,//非异步
			url:myUrl,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					callback();
					
				}else{
					Ext.Msg.alert("提示","删除模块失败！错误信息【<b style='color:red'>'"+ result.ErrorInfo +"</b>】");
				}
			},
			failure:function(response,options){ 
				Ext.Msg.alert("提示","连接删除模块服务出错！");
			}
		});
	},
    /**
     * 给grid添加视图属性
     * @private
     * @param {} grid
     */
	setGridViewConfig:function(grid){
		var me = this;
		var params = me.getPanelParams();
		if(Ext.typeOf(grid) === 'object'){
			grid.viewConfig = {};
			grid.viewConfig.emptyText = params.emptyText;
			grid.viewConfig.loadingText = params.loadingText;
			grid.viewConfig.loadMask = params.hasLoadMask == 'on' ? true : false;
		}
	},
    /***
     * 创建单列树
     * @param {} store1
     * @return {}
     */
    createTree:function(){
        var me = this;
     
      //功能栏按钮组（一）
		var buttontoobar = me.createButtonToolbar();
	  //功能栏按钮组（二）
		var buttontoobartwo = me.createButtonToolbarTwo();
		//过滤栏
		var filterbar = me.createFilterbar();
        var rootName='';//根节点名称
        //根节点名称
        rootName=me.getrootNameValue();
        var getDataServerUrl =me.getDataServerUrl();//树的查询数据地址
        var selectServerUrl="";
        if(getDataServerUrl.getValue()==null){
             return treeCom;
        }else{
            selectServerUrl=""+getDataServerUrl.getValue().split('?')[0]; 
        }
        
        var myUrl2 =""+getRootPath()+"/"+selectServerUrl;
        var store1=me.createStore1(myUrl2,rootName);
        var width=300;//
        var height=280;//
        width=me.gettreeWidthValue();
		height=me.gettreeHeightValue();
        var lines2=false;
        //获取样式选择的的值
        lines2=me.getlinestypeValue();
        var useArrows2=true;
        if(lines2==false){
            useArrows2=true;
        }else{
            useArrows2=false;
        }
        var rootVisible2=false;
        rootVisible2=me.getrootVisibletypeValue();
        var checked2=false;
        checked2=me.getcheckedtypeValue();//获取是否带复选框的值
        var drag=false;// 是否是否允许拖拽
        drag=me.getdrogtypeValue();
        var isTure2=false;//是否级联
        isTure2=me.getisTuretypeValue();
        var title2='';
        var isShowTitle=false;//取标题栏设置是否显示的值
        isShowTitle=me.getisShowTitleValue();
        //获取标题名称的值
        title2=me.gettitleNameValue();
        if(isShowTitle==false){
            title2='';
        }
   	   var myViewConfig={
            plugins: {
                ptype:'treeviewdragdrop',
                allowParentInsert :true,
                allowLeafInserts:true
            }
        };
        var hasBeenDeleted=false;//
        var isMenu=false;//
        isMenu=me.getisShowMenuValue();
        var isDelMenuBtn=false;//取是否显示右键菜单删除按钮的值
        isDelMenuBtn=me.getisDelMenuBtnValue();
        var Menu=[];
        if(isMenu==true){
        	if (isDelMenuBtn==true){
        		Menu=[me.createMenuDelBtn()];
        	}else{
        		Menu=[{}];
        	}
        }else{
        	Menu='';
        }
        var treeCom = {
    	   xtype:'treepanel',
    	   itemId:'center',
           store:store1,
           //属性配置
           lines:lines2,//树线
           useArrows:useArrows2,//是否使用箭头样式
           rootVisible:rootVisible2 ,
           checkModel:'cascade', //对树的级联多选
           width:width,
           height:height,
           title:title2,
           border:true,
           enableDD:drag,
           isTure:isTure2,
           checked:checked2,
           resizable:{handles:'s e'},
           root:{
               text:rootName,
               leaf:false
               ,expanded:true//预防数据加载两次
           }
           //viewConfig:(drag)?(myViewConfig):('') 
        };
        
      //给grid添加视图属性
		me.setGridViewConfig(treeCom);
		
		
        if(drag==true){
            treeCom.viewConfig=myViewConfig;
        }
        treeCom.listeners={
            contextmenu:{
                element:'el',
                fn:function(e,t,eOpts){
                    //禁用浏览器的右键相应事件 
                    e.preventDefault();e.stopEvent();
                    //右键菜单
                    new Ext.menu.Menu({
                         items:Menu
                    }).showAt(e.getXY());//让右键菜单跟随鼠标位置
                }
            },

            resize:function(com,width,height,oldWidth,oldHeight,eOpts){//列表大小变化
                 //列表宽度和高度赋值
                var mytextwidth =me.gettreeWidth();
                var mytextheight =me.gettreeHeight();
                mytextwidth.setValue(width);
                mytextheight.setValue(height);
            },
            checkchange:function(node,checked){
                if(isTure2==true){
                if(node.data.leaf==false){     //当选中的不是叶子时
                    if(checked){
                         //打开父节点
                         node.expand();
                         //遍历子节点
                         node.eachChild(function(n){
                              n.data.checked=true;
                              n.updateInfo({checked:true});
                              if(n.data.leaf==false){
                                  me.setNode(n);
                              }
                         });
                      }else{
                          node.expand();
                              //遍历子节点
                          node.eachChild(function(n){
                              n.data.checked=false;
                              n.updateInfo({checked:false}); 
                              if(n.data.leaf==false){
                                  me.setNodefalse(n);
                              }
                          });
                      }
                 }
                 else{
                    node.parentNode.data.checked=false;
                    node.parentNode.updateInfo({checked:false}); 
                 }
                }else{
                    if(checked){
                        //打开父节点
                        node.expand();
                        //遍历子节点
                        node.eachChild(function (child){
                            child.data.checked=true;
                            child.updateInfo({checked:true});
                          if(child.data.leaf==false){
                              me.setNode(child);
                          }
                            me.chd(child,true);
                        });
                    }else{
                         node.expand();
                         //遍历子节点
                        node.eachChild(function (child){
                            child.data.checked=false;
                            child.updateInfo({checked:false}); 
                             if(child.data.leaf==false){
                                 me.setNodefalse(child);
                             }
                            me.chd(child,false);
                        });
                    }
                me.parentnode(node);//进行父级选中操作 
                }
            },

            //节点插入完成后
            iteminsert:function( nodeInterface, node, refNode, eOpts ){
                var node3=nodeInterface.data;
                var newParent3=node.data;
            },
            //节点移动完成后
            itemmove:function( nodeInterface, oldParent, newParent, index, eOpts ){ 
                var node3=nodeInterface.data;
                var newParent3=newParent.data;
                var node2={Id:""+node3.tid+"",ParentID:""+newParent3.tid+""};//,DataTimeStamp:dataTimeStamp
                me.updateNode('edit',node2);
                //treeCom.store.load();
            }
        };
        var values = me.getButtonsConfigValues();
        var filterposition=values['filter-position'];//过滤停放位置
        var toolbarposition=values['toolbar-position'];//按钮组一停放位置
        var toolbarpositiontwo=values['toolbar-position-two'];//按钮组二停放位置
        
        filterbarArr=[];//过滤
        buttontoobarArr=[];//按钮组一
        buttontoobartwoArr=[];//按钮组二
        filterbarArr.push(filterbar);
        buttontoobarArr.push(buttontoobar);
        buttontoobartwoArr.push(buttontoobartwo);
        
        var	arr='';
        arrtwo='';
        toobarArr='';
	    if(filterbar!=null && buttontoobartwo==null  && buttontoobar==null){
	    	toobarArr=[{
				xtype:'toolbar',
                itemId:'treeToolbar',
                dock:filterposition,
                items:arr
            }];
	    	toolbarposition=filterposition;
	    	toolbarpositiontwo=filterposition;
	    }
	    if(filterbar!=null && buttontoobartwo!=null  && buttontoobar==null){
    		if(values['filter-number']==1 && values['toolbar-number-two']==2){
	    	    arr=filterbarArr.concat(buttontoobartwoArr);  
    		}else{
    			arr=buttontoobartwoArr.concat(filterbarArr); 
    		}
	    	toobarArr=[{
				xtype:'toolbar',
                itemId:'treeToolbar',
                dock:filterposition,
                items:arr
            }];
	    	toolbarposition=filterposition;
	    }
	    if(filterbar!=null && buttontoobartwo==null  && buttontoobar!=null){
	    	if(values['filter-number']==1 && values['toolbar-number']==2){
	    	    arr=filterbarArr.concat(buttontoobarArr);  
	    	}else{
	    	    arr=buttontoobarArr.concat(filterbarArr);  
	    	}
	    	toobarArr=[{
				xtype:'toolbar',
                itemId:'treeToolbar',
                dock:filterposition,
                items:arr
            }];
	    	toolbarpositiontwo=filterposition;
	    }
	    if(filterbar==null && buttontoobartwo!=null  && buttontoobar==null){  
	    	toobarArr=[{
				xtype:'toolbar',
                itemId:'treeToolbar',
                dock:toolbarpositiontwo,
                items:buttontoobartwoArr
            }];
	    	toolbarposition=toolbarpositiontwo;
	    	filterposition=toolbarpositiontwo;
	    }
	    if(filterbar==null && buttontoobartwo!=null  && buttontoobar!=null){
	    	if(values['toolbar-number']==1 && values['toolbar-number-two']==2){
	    	    arr=buttontoobarArr.concat(buttontoobartwoArr);  
	    	}else{
	    		 arr=buttontoobartwoArr.concat(buttontoobarArr);  
	    	}
	    	toobarArr=[{
				xtype:'toolbar',
                itemId:'treeToolbar',
                dock:toolbarpositiontwo,
                items:arr
            }];
	    	filterposition=toolbarpositiontwo;
	    }
	    if(filterbar==null && buttontoobartwo==null  && buttontoobar!=null){
	    	toobarArr=[{
				xtype:'toolbar',
                itemId:'treeToolbar',
                dock:toolbarposition,
                items:buttontoobartwoArr
            }];
	    	toolbarpositiontwo=toolbarposition;
	    	filterposition=toolbarposition;
	    }
	 
        if(buttontoobar || buttontoobartwo || filterbar){
            //停靠位置相同(顶部,底部)
        	if(toolbarpositiontwo == toolbarposition  && toolbarposition==filterposition ){
        		if(values['filter-number']==1 && values['toolbar-number']==2 && values['toolbar-number-two']==3){
        			arr=filterbarArr.concat(buttontoobarArr,buttontoobartwoArr);  
        		}else if (values['filter-number']==1 && values['toolbar-number']==3 && values['toolbar-number-two']==2){
        			arr=filterbarArr.concat(buttontoobartwoArr,buttontoobarArr); 
        		}else if (values['filter-number']==1 && values['toolbar-number']==3 && values['toolbar-number-two']==2){
        			arr=filterbarArr.concat(buttontoobartwoArr,buttontoobarArr); 
        		}else if (values['filter-number']==2 && values['toolbar-number']==1 && values['toolbar-number-two']==3){
        			arr=buttontoobarArr.concat(filterbarArr,buttontoobartwoArr);  
        		}else if (values['filter-number']==2 && values['toolbar-number']==3 && values['toolbar-number-two']==1){
        			arr=buttontoobartwoArr.concat(filterbarArr,buttontoobarArr);  
        		}else if (values['filter-number']==3 && values['toolbar-number']==2 && values['toolbar-number-two']==1){
        			arr=buttontoobartwoArr.concat(buttontoobarArr,filterbarArr);  
        		}else if (values['filter-number']==3 && values['toolbar-number']==1 && values['toolbar-number-two']==2){
        			arr=buttontoobarArr.concat(buttontoobartwoArr,filterbarArr);  
        		}
        		toobarArr=[{
    				xtype:'toolbar',
                    itemId:'treeToolbar',
                    dock:filterposition,
                    items:arr
                }];
        	}
        	//当过滤栏和按钮组二（相同）、按钮组二 停靠位置（不同）
        	if(filterposition != toolbarposition && toolbarposition==toolbarpositiontwo ){
        		if( values['toolbar-number']==1 && values['toolbar-number-two']==2){
        		    arr=buttontoobarArr.concat(buttontoobartwoArr); 
        		}else{
        			arr=buttontoobartwoArr.concat(buttontoobarArr); 
        		}
        		arrtwo=filterbarArr;
        		toobarArr=[{//合并组
        			    xtype:'toolbar',
                        itemId:'treeToolbar',
                        dock:toolbarpositiontwo,
                        items:arr
                    },{
                    	xtype:'toolbar',
                        itemId:'treeToolbarTwo',
                        dock:filterposition,
                        items:arrtwo
                    }
                ];
        	}
        	//过滤栏和按钮组一（相同）   和按钮组二 （不同）
        	if(filterposition == toolbarposition && toolbarposition!=toolbarpositiontwo ){
        		if( values['filter-number']==1 && values['toolbar-number']==2){
        		      arr=filterbarArr.concat(buttontoobarArr);  
        		}else{
        			 arr=buttontoobarArr.concat(filterbarArr);  
        		}
        		arrtwo=buttontoobartwoArr;
        		toobarArr=[{//合并组
        			    xtype:'toolbar',
                        itemId:'treeToolbar',
                        dock:filterposition,
                        items:arr
                    },{
                    	xtype:'toolbar',
                        itemId:'treeToolbarTwo',
                        dock:toolbarpositiontwo,
                        items:arrtwo
                    }
                ];
        	}
        	//当过滤栏的停靠位置和按钮组二相同   和按钮组一停靠位置不相同时
        	if(filterposition == toolbarpositiontwo && toolbarposition!=toolbarpositiontwo ){
        		if( values['filter-number']==1 && values['toolbar-number']==2){
        		    arr=filterbarArr.concat(buttontoobartwoArr);  
        		}else{
        			 arr=buttontoobartwoArr.concat(filterbarArr); 
        		}
        		arrtwo=buttontoobarArr;
        		toobarArr=[{//合并组
        			    xtype:'toolbar',
                        itemId:'treeToolbar',
                        dock:filterposition,
                        items:arr
                    },{
                    	xtype:'toolbar',
                        itemId:'treeToolbarTwo',
                        dock:toolbarposition,
                        items:arrtwo
                    }
                ];
        	}
        	treeCom.dockedItems = toobarArr;
        }
        return treeCom;
    },
    //====================功能按钮设置=================
    /**
	 * 获取按钮组设置信息
	 * @private
	 * @return {}
	 */
	getButtonsConfigValues:function(){
		var me = this;
		var fieldset = me.getFunButFieldSet();
		var values = fieldset.getFieldSetValues();
		return values;
	},
	/**
	 * 按钮组设置赋值
	 * @private
	 * @param {} panelParams
	 */
	setButSetValues:function(panelParams){
		var me = this;
		var funbut = me.getFunButFieldSet();
		funbut.setDelServerValue(panelParams['del-server-combobox']);
		funbut.setWinFormComboboxValue(panelParams['winform-combobox']);
	},
	 /**
	 * 功能按钮过滤栏
	 * @private
	 * @return {}
	 */
	createFilterbar:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		var order1=values['toolbar-filter-number'];
		//过滤栏
		if(values['toolbar-filter']){
			arr.push({
				type:'filter',
				text:values['toolbar-filter-text'],
				order:order1,
				iconCls:'build-button-refresh',
				xtype: 'textfield',	
	            fieldLabel: '检索过滤' ,
	            labelAlign:'right',
                labelWidth:60,
	            enableKeyEvents:true,           
                listeners: {   
                    keyup: {                   
                        fn: function (field, e) {                       
                             if (Ext.EventObject.ESC == e.getKey()) {
                            	 
                                 this.setValue('');               
                                 me.clearFilter();                     
                             }else { 
                                 me.filterByText(this.getRawValue());                       
                             }                   
                        }               
                   }
                } 
			});
		}
		var items = [];
		for(var i=1;i<2;i++){
			for(var j in arr){
				if(arr[j].order == i){
					arr[j].handler = function(but,e){
                		 if(but.type == "filter"){
                			but.ownerCt.ownerCt.ownerCt.store.load();
                		}
	                };
					items.push(arr[j]);
				}
			}
		}
		var com = null;
		if(items.length > 0){
			com = {
				xtype:'toolbar',
				border:0,
				dock:values['filter-position'],
				items:items
			};
		}
		return com;
	},
    /**
	 * 功能栏按钮组（一）
	 * @private
	 * @return {}
	 */
	createButtonToolbar:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		var order1=values['toolbar-refresh-number'];
		//刷新按钮
		if(values['toolbar-refresh-checkbox']){
			arr.push({
				type:'refresh',
                itemId:'refresh',
				text:values['toolbar-refresh-text'],
				order:order1,
				tooltip:'刷新数据',
				iconCls:'build-button-refresh'
			});
		}
		//收缩按钮
		if(values['toolbar-Minus-checkbox']){
			arr.push({
				type:'minus',
                itemId:'minus',
				tooltip:'收缩数据',
				text:values['toolbar-Minus-text'],
				order:values['toolbar-Minus-number'],
				iconCls:'build-button-arrow-in'
			});
		}
		//展开按钮
		if(values['toolbar-plus-checkbox']){
			arr.push({
				type:'plus',
                itemId:'plus',
				tooltip:'展开数据',
				text:values['toolbar-plus-text'],
				order:values['toolbar-plus-number'],
				iconCls:'build-button-arrow-out'
			});
		}
		//新增按钮
		if(values['toolbar-add-checkbox']){
			arr.push({
				type:'add',
				tooltip:'新增数据',
				text:values['toolbar-add-text'],
				order:values['toolbar-add-number'],
				iconCls:'build-button-add'
			});
		}
		//修改按钮
		if(values['toolbar-edit-checkbox']){
			arr.push({
				type:'edit',
				tooltip:'修改数据',
				text:values['toolbar-edit-text'],
				order:values['toolbar-edit-number'],
				iconCls:'build-button-edit'
			});
		}
		//查看按钮
		if(values['toolbar-show-checkbox']){
			arr.push({
				type:'show',
				tooltip:'查看数据',
				text:values['toolbar-show-text'],
				order:values['toolbar-show-number'],
				iconCls:'build-button-see',
				handler:function(but,e){

				}

			});
		}
		//删除按钮
		if(values['toolbar-del-checkbox']){
			arr.push({
				type:'del',
				tooltip:'删除数据',
				text:values['toolbar-del-text'],
				order:values['toolbar-del-number'],
				iconCls:'delete'
			});
		}
		var items = [];
		for(var i=1;i<8;i++){
			for(var j in arr){
				if(arr[j].order == i){
					arr[j].handler = function(but,e){
						var records = but.ownerCt.ownerCt.ownerCt.getSelectionModel().getSelection();
                		if(but.type == "del"){
                			if(records.length > 0){
                				Ext.Msg.confirm('提示','确定要删除吗？',function (button){
									if(button == 'yes'){
			                			for(var i in records){
			                    			var id = records[i].get('tid');
			                				//删除后的处理
			                				var callback = function(){
			                					var node = but.ownerCt.ownerCt.ownerCt.getRootNode().findChild("tid",id,true);
			                		    		node.remove();
			                				};
			                				me.deleteModuleServer(id,callback);
			                			}
									}
                				});
                			}else{
                				Ext.Msg.alert("提示","请选择数据进行操作！");
                			}
                		}else if(but.type == "refresh"){
                			but.ownerCt.ownerCt.ownerCt.store.load();
                            but.handler=function(event,toolEl,owner,tool){
			                var tree=me.getTreeCom();
			                
			                if(tree&&tree!=undefined){
			                    var treeToolbar=tree.getComponent('treeToolbar'); 
			                    if(treeToolbar==undefined||treeToolbar==''){
			                       treeToolbar=tree.getComponent('treeToolbarTwo');
			                    }
                                treeToolbar=treeToolbar.getComponent('treeToolbar');
			                    if(treeToolbar&&treeToolbar!=undefined){
			                        var refresh=treeToolbar.getComponent('refresh');
			                        if(refresh&&refresh!=undefined){
			                            refresh.disabled=true;//不可用
			                        }
			                    }
			                }    
			                me.load();
			            };
                			
                		}else if(but.type == "minus"){
                			but.ownerCt.ownerCt.ownerCt.collapseAll();
                			but.ownerCt.ownerCt.ownerCt.getRootNode().expand();
                		}else if(but.type == "plus"){
                			but.ownerCt.ownerCt.ownerCt.expandAll();
                		}
                        else if(but.type == "add"){
                			me.openFormWin(but.type,-1);
                		}
                        else if(but.type == "show"){
                        	if(records.length == 1){
								for(var i in records){
	                    			var id = records[i].get('tid');
	                			}
                        	}
                        	me.openFormWin(but.type,id);
                		}else if(but.type == "edit"){
                        	if(records.length == 1){
								for(var i in records){
	                    			var id = records[i].get('tid');
	                			}
                        	}
                        	
                        	
                        	me.openFormWin(but.type,id);
                		}
                        else{
						    Ext.Msg.alert("提示","请选择一条数据进行操作！");
                		}
	               };
				 items.push(arr[j]);
				}
			}
		}
		var com = null;
		if(items.length > 0){
			com = {
				xtype:'toolbar',
                itemId:'treeToolbar',
				border:0,
				dock:values['toolbar-position'],
				items:items
			};
		}
		return com;
	},
	
	
	 /**
	 * 功能栏按钮组（二）
	 * @private
	 * @return {}
	 */
	createButtonToolbarTwo:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var order1=values['toolbar-confirm-number'];
		var arr = [];
		//确定按钮
		if(values['toolbar-confirm-checkbox']){
			arr.push({
				type:'confirm',
				tooltip:'确定',
				text:values['toolbar-confirm-text'],
				order:order1,
				iconCls:'build-button-save'
			});
		}
		//取消按钮
		if(values['toolbar-cancel-checkbox']){
			arr.push({
				type:'cancel',
				tooltip:'取消',
				text:values['toolbar-cancel-text'],
				order:values['toolbar-cancel-number'],
				iconCls:'build-button-delete'
			});
		}
		var items = [];
		for(var i=1;i<3;i++){
			for(var j in arr){
				if(arr[j].order == i){
					arr[j].handler = function(but,e){
						var records = but.ownerCt.ownerCt.ownerCt.getSelectionModel().getSelection();
                		 if(but.type == "confirm"){
//                			 me.fireEvent('okClick');
//                			but.ownerCt.ownerCt.ownerCt.store.load();
                		}else if(but.type == "cancel"){
//                			me.addEvents('okClick');
//                			but.ownerCt.ownerCt.ownerCt.store.load();
                			
                		}
	                };
					items.push(arr[j]);
				}
			}
		}
		var com = null;
		if(items.length > 0){
			com = {
				xtype:'toolbar',
                itemId:'treeToolbartwo',
				border:0,
				dock:values['toolbar-position-two'],
				items:items
			};
		}
		return com;
	},
	/**
	 * 弹出表单
	 * @private
	 * @param {} type
	 * @param {} id
	 */
	openFormWin:function(type,id){
		var me = this;
		var values = me.getButtonsConfigValues();
		var winId = values['winform-id'];
		var callback = function(appInfo){
			if(appInfo && appInfo != ''){
				var ClassCode = appInfo.BTDAppComponents_ClassCode;
				if(ClassCode&&ClassCode!=''){
					var panelParams = {
						type:type,
						dataId:id,
						modal:true,
						floating:true,
						closable:true,
						draggable:true
					};
					var Class = eval(ClassCode);
					var panel = Ext.create(Class,panelParams).show();
					panel.on({
						saveClick:function(){
							panel.close();
							me.getCenterCom().load();
						}
					});
				}else{
					Ext.Msg.alert('提示','没有类代码！');
				} 
			 }
		}; 
		me.getAppInfoFromServer1(winId,callback);
	},
	getAppInfoFromServer1:function(id,callback){
		var me=this;
		me.getAppInfoServerUrl=getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById';
		var url=me.getAppInfoServerUrl+'?isPlanish=true&id='+id;
		Ext.Ajax.defaultPostHeader='application/json';
		Ext.Ajax.request({
			async:false,url:url,method:'GET',
			timeout:2000,
			success:function(response,opts){
			    var result=Ext.JSON.decode(response.responseText);
 	     			if(result.success){
 	     				var appInfo='';
 	     				if(result.ResultDataValue&&result.ResultDataValue!=''){
 	     					appInfo=Ext.JSON.decode(result.ResultDataValue);
 	     				}if(appInfo!=''){
 	     					if(Ext.typeOf(callback)=='function'){callback(appInfo);}
 	     					
 	     				}else{Ext.Msg.alert('提示','没有获取到应用组件信息！');}
 	     				
 	     			}else{}},
 	     			failure:function(response,options){Ext.Msg.alert('提示','获取应用组件信息请求失败！');}
 	     			
		});},
	/**
	 * 根据ID删除数据
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	deleteInfo:function(id,callback){
		var me = this;
		var values = me.getButtonsConfigValues();
		//获取数据服务列表
        var delDataServerUrl = me.getdelDataServerUrl();
        var selectServerUrl="";
        if(delDataServerUrl.getValue()==null){
             return;
        }else{
            selectServerUrl=""+delDataServerUrl.getValue().split('?')[0]; 
        }
        var url =""+getRootPath()+"/"+selectServerUrl;
		Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					if(Ext.typeOf(callback) == "function"){
						callback();//回调函数
					}
				}else{
					Ext.Msg.alert('提示','删除信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
				}
			},
			failure:function(response,options){ 
				Ext.Msg.alert('提示','删除信息请求失败！');
			}
		});
	},
    
//===============================获取及设置参数======================
    /**
     * 获取数据对象控件
     * @private
     * @return {}
     */
    getdataObject:function(){
        var panel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var dataObject = panel.getComponent('dataObject');
        return dataObject;
    },
    /**
     * 获取获取服务控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getDataServerUrl:function(){
        var dataObject = this.getdataObject();
        var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        return getDataServerUrl;
    },
    /**
     * 获取获取服务控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getobjectName:function(){
        var dataObject = this.getdataObject();
        var objectName = dataObject.getComponent('objectName');
        return objectName;
    },
    /**
     * 获取获取服务控件
     * var params=getPanelParams();
     * @private
     * @return {}
     */
    getdelDataServerUrl:function(){
        var dataObject = this.getdataObject();
        var getdelDataServerUrl = dataObject.getComponent('delDataServerUrl');
        return getdelDataServerUrl;
    },
    /**
     * 修改服务控件
     * var params=getPanelParams();
     * @private
     * @return {}
     */
    geteditDataServerUrl:function(){
        var dataObject = this.getdataObject();
        var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
        return editDataServerUrl;
    },
    
    /***
     * 基本属性的区域
     * @return {}
     */
   getbasicParams:function(){
        var me = this;
        var panel = me.getComponent('east').getComponent('centerbasic' + this.ParamsPanelItemIdSuffix);
        //组件属性面板
        var basic = panel.getComponent("basicParams");
        return basic;
    },

    /***
     * 基本属性的右键菜单区域
     * @return {}
     */
   getbasicMenuParams:function(){
        var me = this;
        var panel = me.getComponent('east').getComponent('centerbasic' + this.ParamsPanelItemIdSuffix);
        //组件属性面板
        var basic = panel.getComponent("menuItems");
        return basic;
    },
	/**
	 * 基本属性的功能按钮设置
	 * @private
	 * @return {}
	 */
	getFunButFieldSet:function(){
		var me = this;
		var formParamsPanel = me.getComponent('east').getComponent('centertool' + me.ParamsPanelItemIdSuffix);
		var com = formParamsPanel.getComponent('buttonsconfig');
		return com;
	},
    getotherParamsTitleSet:function(){
        var me = this;
        var panel = me.getComponent('east').getComponent('centerbasic' + me.ParamsPanelItemIdSuffix);
        var otherParamsTitleSet = panel.getComponent('otherParamsTitleSet');
        return otherParamsTitleSet;
    },
    /**
     * 标题栏设置是否显示
     * @private
     * @return {}
     */
    getisShowTitle:function(){
        var me = this;
        var isShowTitle =me.getotherParamsTitleSet().getComponent('IsShowTitle');
        return isShowTitle;
    },
    /**
     * 单选组:取标题栏设置是否显示的值
     * @private
     * @return {}
     */
    getisShowTitleValue:function(){
        var me = this;
        var value =me.getotherParamsTitleSet().getComponent('IsShowTitle').getValue().IsShowTitle;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
    },
    /**
     * 获取标题名称
     * @private
     * @return {}
     */
    gettitleName:function(){
        var me = this;
        var titleN = me.getotherParamsTitleSet().getComponent('titleName');
        return titleN;
    },
    /**
     * 获取标题名称的值
     * @private
     * @return {}
     */
    gettitleNameValue:function(){
        var me = this;
        var titleN = me.getotherParamsTitleSet().getComponent('titleName').getValue();
        return titleN;
    },
    /***
     * 基本属性的区域:根节点名称
     * @return {}
     */
    getrootName:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("Root");
        return com;
    },
    /***
     * 基本属性的区域:根节点名称
     * @return {}
     */
    getrootNameValue:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("Root").getValue();
        return com;
    },
    /***
     * 树宽
     * @return {}
     */
    gettreeWidth:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("treeWidth");
        return com;
    },
    /***
     * 树宽
     * @return {}
     */
    gettreeWidthValue:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("treeWidth").getValue();
        return com;
    },
    /***
     * 树高
     * @return {}
     */
    gettreeHeight:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("treeHeight");
        return com;
    },
    /***
     * 树高
     * @return {}
     */
    gettreeHeightValue:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("treeHeight").getValue();
        return com;
    },
    /***
     * 样式选择
     * @return {}
     */
    getlinestype:function(){
        var me = this;
        var basicParams = me.getbasicParams();
        var com = basicParams.getComponent("Linestype");
        return com;
    },
    /***
     * 单选组:获取样式选择的的值
     * @return {}
     */
    getlinestypeValue:function(){
        var me = this;
        var basicParams = me.getbasicParams();
        var value = basicParams.getComponent("Linestype").getValue().Linestype;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
    },
    /***
     * 单选组:是否带复选框
     * @return {}
     */
    getcheckedtype:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("checked");
        return com;
    },
    /***
     * 单选组:获取是否带复选框的值
     * @return {}
     */
    getcheckedtypeValue:function(){
        var basicParams = this.getbasicParams();
        var value = basicParams.getComponent("checked").getValue().Checkedtype;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
    },
    /***
     * 单选组:是否允许拖
     * @return {}
     */
    getdrogtype:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("Drogtype");
        return com;
    },
    
    /***
     * 单选组:是否允许拖
     * @return {}
     */
    getdrogtypeValue:function(){
        var basicParams = this.getbasicParams();
        var value = basicParams.getComponent("Drogtype").getValue().Drogtype;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
    },
    /***
     * 单选组:是否显示根节点
     * @return {}
     */
    getrootVisibletype:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("rootVisibletype");
        return com;
    },
    /***
     * 单选组:是否显示根节点
     * @return {}
     */
    getrootVisibletypeValue:function(){
        var me = this;
        //组件属性面板
        var basicParams = me.getbasicParams();
        var value = basicParams.getComponent("rootVisibletype").getValue().rootVisibletype;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
    },
    /***
     * 单选组:是否级联
     * @return {}
     */
    getisTuretype:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("IsTuretype");
        return com;
    },
    /***
     * 单选组:是否级联
     * @return {}
     */
    getisTuretypeValue:function(){
        var basicParams = this.getbasicParams();
        var value = basicParams.getComponent("IsTuretype").getValue().IsTuretype;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
    },
    /**
     * 是否开启右键菜单
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getEastIsMenu:function(){
        var me = this;
        var basicParams = me.getbasicMenuParams();
        var menu = basicParams.getComponent('IsMenu');
        return menu;
    },
    /**
     * 复选框:取是否开启右键菜单的值
     * @private
     * @return {}
     */
    getisShowMenuValue:function(){
        var me = this;
        //属性面板ItemId
        var basicParams = me.getbasicMenuParams();
        var value = basicParams.getComponent('IsMenu').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
    },
    /**
     * 右键菜单复选框区域
     * @private
     * @return {}
     */
    getmenuDelBtn:function(){
    	var me =this;
    	var basicParams = me.getbasicMenuParams();
        var menu = basicParams.getComponent('menuItemsBtns');
        return menu;
    },
    /***
     * 复选框:是否显示右键菜单删除按钮
     * @return {}
     */
    getisDelMenuBtn:function(){
        var isDelMenuBtn =this.getmenuDelBtn().getComponent('IsDelMenuBtn');
        return isDelMenuBtn;
    },
    /***
     * 复选框:取是否显示右键菜单删除按钮的值
     * @return {}
     */
    getisDelMenuBtnValue:function(){
        var value =this.getmenuDelBtn().getComponent('IsDelMenuBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true;
        }else{
            value =false;
        }
        return value;
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
     *基础属性
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getPanelBasicParams:function(){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('centerbasic' + me.ParamsPanelItemIdSuffix);
        var params = formParamsPanel.getForm().getValues();
        return params;
    },
    
    /**
     *功能按钮设置
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getPanelToolParams:function(){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('centertool' + me.ParamsPanelItemIdSuffix);
        var params = formParamsPanel.getForm().getValues();
        return params;
    },
     /**
     * 获取获取服务控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getEastGetDataServerUrl:function(){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObject = formParamsPanel.getComponent('dataObject');
        var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        return getDataServerUrl;
    },
    /**
     * 获取表单的数据服务URL
     * @private
     * @return {}
     */
    getDataUrl:function(){
        
        var me = this;
        //表单配置参数
        var params = me.getPanelParams();
        //前台需要显示的字段
        var fields = me.getFormFields();
        if(!fields){
            fields = "";
        }
        //数据服务地址
        var url = params.getDataServerUrl;
        if(url){
            url = url.split("?")[0];
            url = getRootPath() + "/" + url + "?isPlanish=true&fields=" + fields;
        }else{
            url = "";
        }
        return url;
    },
    /**
     * 获取列表配置参数
     * @private
     * @return {}
     */
    getListParams:function(){
        var me = this;
        var listParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var params = listParamsPanel.getForm().getValues();
        return params;
    },
    
    /**
     * 获取列表配置参数
     * @private
     * @return {}
     */
    getListParamsbasic:function(){
        var me = this;
        var basicParamsPanel = me.getComponent('east').getComponent('centerbasic' + me.ParamsPanelItemIdSuffix);
        var params = basicParamsPanel.getForm().getValues();
        return params;
    },
    /**
     * 获取树属性配置参数
     * @private
     * @return {}
     */
    getFormParams:function(){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var params = formParamsPanel.getForm().getValues();
        return params;
    },
    /**
     * 获取元应用列表参数
     * @return {}
     */
    getParams:function(){
        var me = this;
        var url = me.getAppInfoServerUrl+'?id='+me.appId;
        var params = {};
        Ext.Ajax.request({
            url:url,
            method:'GET',
            timeout:2000,
            async:false,//同步
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){//服务器端数据成功
                    params = Ext.JSON.decode(result.params);
                }else{
                    Ext.Msg.alert('提示','获取参数失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取参数请求失败！');
            }
        });
        return params;
    },

    /**
     * 前台需要的字段
     * @private
     * @return {}
     */
    getListFields:function(){
        var me =this;
        var columnParams = me.getColumnParams();
        var fields = [];
        for(var i in columnParams){
            fields.push(columnParams[i].InteractionField);
        }
        return fields.toString();
    },

    /**
     * 查询条件
     * @private
     */
    getWhere:function(){
        var me = this;
        //列表配置参数
        var params = me.getListParams();
        var where = params.testParams;
        return where;
    },
    /**
     * 获取展示区域组件
     * @private
     * @return {}
     */
    getCenterCom:function(){
        var me = this;
        var center = me.getComponent('center');
        return center;
    },
    /**
     * 设置树属性配置参数
     * @private
     * obj={titleStyle:"fdsafd"}
     * @return {}
     */
    setFormValues:function(obj){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        formParamsPanel.getForm().setValues(obj);
        
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
     * 给你组件基础属性配置参数赋值
     * @private
     * @param {} obj
     */
    setBasicParams:function(obj){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('centerbasic' + me.ParamsPanelItemIdSuffix);
        formParamsPanel.getForm().setValues(obj);
    },
    
    /**
     * 给你面板按钮设置配置参数赋值
     * @private
     * @param {} obj
     */
    setToolParams:function(obj){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('centertool' + me.ParamsPanelItemIdSuffix);
        formParamsPanel.getForm().setValues(obj);
    },


    //==================右键菜单============================
    /**
     * 右键菜单设置
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createMenuItems:function(){
        var me = this;
        var items = {
            xtype:'fieldset',title:'右键菜单设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'menuItems',
            name:'menuItems',
            items:[{
                xtype:'checkbox',name:'IsMenu',itemId:'IsMenu',boxLabel:'开启右键菜单'
            },{
                xtype: 'checkboxgroup',
                fieldLabel: '',
                columns: 2,
                itemId:'menuItemsBtns',
                name:'menuItemsBtns',
                vertical: true,
                items: me.createismenuArr()
            }]
        };
        return items;
    },
   
    /***
     * 创建右键菜单按钮组
     * @return {}
     */
    createismenuArr:function(){
       var me=this;
       var istoolsArr=[
           {boxLabel: '删除',name:'IsDelMenuBtn',itemId:'IsDelMenuBtn',
           listeners:{
               change:function(com, newValue,oldValue,eOpts){
                  
               }
           }  
         }
      ];
     return istoolsArr;
   },
   
    /**
     * 
     * 打开并操作标题字体设置窗体
     * @param {} 
     */
    OpenCategoryWin:function(){
        var me=this;
        var xy=me.getPosition();
        var myxtype=null;
            if(!myxtype){
                myxtype=Ext.create('Ext.zhifangux.FontStyleSet', {
                    // title: '字体属性设置',
                    itemId:'vartestobj_id',
                    titleAlign :"center",
                    autoScroll : true,
                    height:270,        //容器高度像素
                    width:460,      //容器宽度像素
                    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
                    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
                    labelcls:'labelcls',//字体属性设置:label样式
                    btnHidden: false,//确定或者取消按钮的显示false或者隐藏true
                    listeners:{
                        //公开的事件
                        onOKCilck:function(o){
                            //获取设置当前控件的文字属性结果值
                            var lastValue=this.GetValue();
                            var obj ={titleStyle:lastValue};
                            me.setFormValues(obj);
                            var a = me.getFormParams();
                            var bm = Ext.getCmp('MyRDS_wintemp');
                            if(bm==undefined){ 
                            }else{
                                bm.close();
                            }
                        },
                        //公开的事件
                        onCancelCilck:function(o){
                            //获取设置当前控件的文字属性结果值
                             var obj ={titleStyle:''};
                             me.setFormValues(obj);
                             var a = me.getFormParams();
                             var bm = Ext.getCmp('MyRDS_wintemp');
                                 if(bm==undefined){ 
                                 }else{
                                     bm.close();
                                 }
                         }
                   }
                });
           }
             me.win=null;
             me.win = Ext.create('widget.window', {
                title:me.winTitle,
                id:"MyRDS_wintemp",
                autoScroll : true,
                border : false,//边框线显示 true,或隐藏false
                width: me.winWidth,
                height:me.winHeight,// me.SetWinWidth(),
                minWidth: me.winWidth,
                minHeight: me.winHeight,
                maxWidth: me.winWidth+5,
                maxHeight: me.winHeight+10,
                x:xy[0]+238,y:xy[1]+30,
                layout: {
                    type: 'border',
                    padding: 5
                },
                items: [{
                    xtype:myxtype
                }]
            });
            me.win.show();
     },
     /**
      * 展示区域的某一控件的根节点名称更新
      * @param {} InteractionField:交互字段,某一控件的itemId
      * @param {} newValue:修改的值
      */
     setComponentRootNode:function(InteractionField,newValue){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(me.treeItemId);
            tempItem.setRootNode(newValue);
     },   
     /**
      * 展示区域的某一控件的宽度属性更新
      * @param {} InteractionField:交互字段,某一控件的itemId
      * @param {} newValue:修改的值
      */
     setComponentWidth:function(InteractionField,newValue){
         var me=this;
         var tempItem= me.getCenterCom().getComponent(InteractionField);
             tempItem.setSize(newValue);
     },
     /**
      * 展示区域的某一控件的高度属性更新
      * @param {} InteractionField:交互字段,某一控件的itemId
      * @param {} newValue:修改的值
      */
     setComponentHeight:function(InteractionField,newValue){
         var me=this;
         var tempItem= me.getCenterCom().getComponent(InteractionField);
             tempItem.setSize(undefined,newValue);
     },
     /**
      * 属性面板监听
      * @private
      */
     initEastListener:function(){
         var me = this;
         //数据对象列表监听
         me.initObjectNamelistener();
         me.initgetDataServerUrllistener();
     },
     /**
      * 数据对象列表监听
      * @private
      */
     initgetDataServerUrllistener:function(){
         var me = this;
         var getDataServerUrl =me.getDataServerUrl();
         getDataServerUrl.on({
             select:function(owner,records,eOpts){
                 me.setBasicParamsPanelValues();
                 me.setMenuParamsPanelValues();
             },
             change:function(owner,newValue,oldValue,eOpts){
                 var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
                 if(newValue && newValue != "" && index != -1){
                     var value=newValue.split("_");
                     var dataObject =me.getdataObject();
                     //获取对象结构
                     var objectPropertyTree =me.getobjectPropertyTree(); 
                     var node = {text:owner.rawValue,checked:false,expanded:false};
                     objectPropertyTree.store.setRootNode(node);
                    
                     objectPropertyTree.getRootNode().data.expanded = true;
                     var myobjectPropertyTreeUrl=me.ObjectPropertyUrl + "?" + me.ObjectProperyParam + "=" + newValue;
                     objectPropertyTree.store.proxy.url = myobjectPropertyTreeUrl;
                     objectPropertyTree.store.load();
                     //获取数据服务列表
                     var getDataServerUrl = me.getDataServerUrl();
                     var  treeSelectUrl=me.objectGetDataServerUrl + "?" + me.objectServerParam + "=Tree"+ newValue; ;
                     getDataServerUrl.store.proxy.url =treeSelectUrl;
                     getDataServerUrl.store.load();
                     
                     //获取数据服务列表
                     var delDataServerUrl = me.getdelDataServerUrl();
                     delDataServerUrl.store.proxy.url = me.delServerUrl + "?" + "EntityName" + "=" + "Bool";
                     delDataServerUrl.store.load();
                     me.DisplayName='';

                }else{
                    return ;
                }
             }
         });
    
     },
     /**
      * 数据对象列表监听
      * @private
      */
     initObjectNamelistener:function(){
         var me = this;
         var objectName =me.getobjectName();
         objectName.on({
             change:function(owner,newValue,oldValue,eOpts){
                 var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
                 if(newValue && newValue != "" && index != -1){
                     var value=newValue.split("_");
                     //获取修改数据服务列表
                    var editDataServerUrl =me.geteditDataServerUrl();
                    editDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.ObjectServerParam + "=" + newValue;
                    editDataServerUrl.store.load();

                }else{
                    return ;
                }
             }
         });
    
     },
     /**
      * 属性面板菜单基础数据赋值
      * @private
      * @param {} componentItemId
      * @param {} record
      */
     setMenuParamsPanelValues:function(){
    	 var me=this;
    	 //右键菜单是否选中
    	  var isShowMenu=me.getEastIsMenu();
    	  var isShowMenuValue=me.getisShowMenuValue();
    	  isShowMenu.setValue(isShowMenuValue);
    	  
    	  //按钮是否选中
    	  var isDelMenu=me.getisDelMenuBtn();
    	  var isDelMenuValue=me.getisDelMenuBtnValue();
    	  isDelMenu.setValue(isDelMenuValue);
     },
     /**
     * 属性面板基础数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setBasicParamsPanelValues:function(){
        var me = this;
        var myrootName=me.getrootName();
        var mytextwidth =me.gettreeWidth();
        var mytextheight =me.gettreeHeight();
        var myCheckedtype=me.getcheckedtype();
        var myDrogtype = me.getdrogtype();
        var myrootVisibletype =me.getrootVisibletype();
        var IsTuretype =me.getisTuretype();
        mytextwidth.setValue(300);
        mytextheight.setValue(280);
        var rootName="";
        myrootName.setValue(rootName);
        var isSetTitle=me.getisShowTitle();//取标题栏设置是否显示的值
        var arrisSetTitle=false;
        var valuesisSetTitle="{IsShowTitle:["+arrisSetTitle+"]}";
        var myisSetTitleJson=Ext.decode(valuesisSetTitle);
        isSetTitle.setValue(myisSetTitleJson);
        
        //线样式
        var myLinestype = me.getlinestype();
        var arrLines=false;
        var valuesLines="{Linestype:["+arrLines+"]}";
        var myLinesJson=Ext.decode(valuesLines);
        myLinestype.setValue(myLinesJson);
        

        
        //是否带复选框
        var arrChecke=false;
        var values="{Checkedtype:["+arrChecke+"]}";
        var myCheckedtypeJson=Ext.decode(values);
        myCheckedtype.setValue(myCheckedtypeJson);
        
        //是否
        var arrDrag=false;
        var valuesDrag="{Drogtype:["+arrDrag+"]}";
        var myDragJson=Ext.decode(valuesDrag);
        myDrogtype.setValue(myDragJson);
        
        //是否显示根节点
        var arrrootVisible=false;
        var valuesarrrootVisible="{rootVisibletype:["+arrrootVisible+"]}";
        var myrootVisibleJson=Ext.decode(valuesarrrootVisible);
        myrootVisibletype.setValue(myrootVisibleJson);
        
        //是否级联
        var arrIsTure=false;
        var valuesIsTure="{IsTuretype:["+arrIsTure+"]}";
        var myIsTurejson=Ext.decode(valuesIsTure);
        IsTuretype.setValue(myIsTurejson);
    },
     //向上遍历父节点
     nodep:function (node){ 
        var bnode=true;
        Ext.Array.each(node.childNodes,function(v){ 
            if(!v.data.checked){
                bnode=false;
                return;
            }
        });
        return bnode;
    },
    parentnode:function (node){
        if(node.parentNode != null){
            if(this.nodep(node.parentNode)){
                node.parentNode.set('checked',true);
            }else{ 
                node.parentNode.set('checked',false);
            }
            this.parentnode(node.parentNode);
        }
    },
    /**
     * 遍历子结点 选中 与取消选中操作
     * */
    chd:function (node,check){
        node.set('checked',check);
        if(node.isNode){
            node.eachChild(function(child){
                if (node.isLeaf){
                    
                }
                else
                {
                   chd(child,check);
                }
            }); 
        }
    },
    //假树
  setNodefalse:function(n) {
      var me=this;
      n.data.checked = false;
      n.updateInfo({ checked: false });
      var childs=n.childNodes;//alert(childs.length+'这个方法在这里调用不到');
      for (var i = 0; i < childs.length; i++) {
          childs[i].data.checked = false;
          childs[i].updateInfo({ checked: false });
          if(childs[i].data.leaf==false){
              me.setNodefalse(childs[i]);
          }
      }
   },
 setNode:function(n) {
      var me=this;
      n.expand();
      n.data.checked = true;
      n.updateInfo({ checked: true });
      var childs=n.childNodes;//alert(childs.length+'内层调用');
          for (var i = 0; i < childs.length; i++) {
              childs[i].data.checked = true;
              childs[i].updateInfo({ checked: true });
              if(childs[i].data.leaf==false)
              {
                 me.setNode(childs[i]);
              }
          }
   },

    //=====================后台获取&存储=======================
   /**
     * 获取删除服务控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getEastDelDataServerUrl:function(){
        var me = this;
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObject = formParamsPanel.getComponent('dataObject');
        var delDataServerUrl = dataObject.getComponent('delDataServerUrl');
        return delDataServerUrl;
    },

    //=================模糊查询==============
    /**
     * 模糊查询过滤函数(）
     * @param {} value
     */
    filterByText: function(text) {   
        this.filterBy(text,this.Filterfield); 
    }, 
    /***
     * 
     * @param {} text
     * @param {} by
     */
    filterBy: function(text, by) {  
        var me = this;  
        this.clearFilter(); 
        var tempItem= me.getCenterCom().getComponent('center');
        var view = tempItem.getView();  //获得面板视图
        var tempValue='';
        nodesAndParents = [];                 
        tempItem.getRootNode().cascadeBy(function(tree, view){ 
            var textValue=text.toLowerCase();
            var byValue=by.toString().toLowerCase().split(",");
            if(isNaN(parseInt(text,10))){
            //字符串里的字母统一转换为小写格式,以匹配支持gridpanel的store里所有大小写字母字符串
            textValue=String(text.toLowerCase()).trim();
            }else{
                textValue=String(text).trim();
            }
            for(var i=0;i<byValue.length;i++){
                var currNode = this; 
                if(currNode && currNode.data[byValue[i]] && currNode.data[byValue[i]].indexOf(textValue) > -1) { 
                    tempItem.expandPath(currNode.getPath());                   
                    while(currNode.parentNode) {                       
                        nodesAndParents.push(currNode.id);    
                        currNode = currNode.parentNode;                   
                    }               
                } 
           }
        }, null, [tempItem, view]);// 隐藏所有的节点     
        tempItem.getRootNode().cascadeBy(function(tree, view){               
            var uiNode = view.getNodeByRecord(this);      
            if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {                   
                Ext.get(uiNode).setDisplayed('none');               
            }           
        }, null, [me, view]);     
     },
     /***
      * 
      */
     clearFilter: function() {  
         var me =this;
         var tempItem= me.getCenterCom().getComponent('center');
         var view = tempItem.getView();  //获得面板视图
         tempItem.getRootNode().cascadeBy(function(tree, view){               
             var uiNode = view.getNodeByRecord(this);   
            
             if(uiNode) {                   
                 Ext.get(uiNode).setDisplayed('table-row');               
                 }           
             }, null, [this, view]);       
     },
   /**
    * 刷新列表树
    * @public
    */
   load:function(){
       var me=this;
       var center=me.getCenterCom().getComponent('center').getStore();
       center.load();
   },
    /***
     * 模糊查询生成类代码Str
     * @return {}
     */
     clearFilterStr: function() { 
         var  clearFilter="";
         clearFilter="function (){"+
         "var me =this;"+
         "var view = this.getView();"+  //获得面板视图
         "this.getRootNode().cascadeBy(function(tree, view){ "+              
             "var uiNode = view.getNodeByRecord(this); "+  
             "if(uiNode) { "+                  
                 "Ext.get(uiNode).setDisplayed('table-row');"+               
                 "}  "+         
             "}, null, [this, view]);"  +
             "}";
             return  clearFilter;
     },
    /***
     * 
     * @return {}
     */     
    filterByStr: function() {  
        var filterBy="";
        filterBy="function (text, by){" +
        "var me = this;  " +
        "this.clearFilter(); " +
        "var view = this.getView();" +  //获得面板视图
        "var tempValue='';" +
        "nodesAndParents = [];" +                 
        "this.getRootNode().cascadeBy(function(tree, view){" + 
            "var textValue=text.toLowerCase();" +
            "var byValue=by.toString().toLowerCase().split(',');" +
            "if(isNaN(parseInt(text,10))){" +
            //字符串里的字母统一转换为小写格式,以匹配支持gridpanel的store里所有大小写字母字符串
            "textValue=String(text.toLowerCase()).trim();" +
            "}else{" +
                "textValue=String(text).trim();" +
            "}" +
            "for(var i=0;i<byValue.length;i++){" +
                "var currNode = this; " +
                "if(currNode && currNode.data[byValue[i]] && currNode.data[byValue[i]].indexOf(textValue) > -1) { " +
                    "me.expandPath(currNode.getPath());" +                   
                    "while(currNode.parentNode) { " +                      
                        "nodesAndParents.push(currNode.id); " +   
                        "currNode = currNode.parentNode;  " +                 
                    "}" +               
                "} " +
           "}" +
        "}, null, [this, view]);" +// 隐藏所有的节点     
        "this.getRootNode().cascadeBy(function(tree, view){ " +              
            "var uiNode = view.getNodeByRecord(this); " +     
            "if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {" +                   
                "Ext.get(uiNode).setDisplayed('none'); " +              
            "} " +          
        "}, null, [me, view]); "+
        "}";
        return filterBy;
     },
     /***
      * 
      * @return {}
      */
    filterByTextStr: function() {  
        var filterByText="";
        filterByText="function (text){" +
            "this.filterBy(text,this.filterfield);"+
        "}";
        return filterByText;
    },  
    /***
     * 监听树的过滤事件
     * @return {}
     */
    createListenersFilterfieldStr:function(){
        var me=this;
        var Filterfieldchange="";
        Filterfieldchange=
            "keyup: {"+                   
                "fn: function (field, e) { "+                      
                  "if (Ext.EventObject.ESC == e.getKey()) { "+ 
                      "this.setValue(''); "+              
                      "me.clearFilter();"+                     
                  "} else { "+
                      "me.filterByText(this.getRawValue());   "+                    
                  "}  "+                 
              "}  "+             
          "}";
        return Filterfieldchange;
    },

	/**
	 * 功能按钮过滤栏类代码
	 * @private
	 * @return {}
	 */
	createFilterbarStr:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		//过滤栏
		if(values['toolbar-filter']){
			var value=
			"{" +
				"type:'filter'," + 
				"text:'" + values['toolbar-filter-text'] + "'," + 
				"iconCls:'build-button-refresh'," + 
				"xtype:'textfield'," +	
	            "fieldLabel: '检索过滤' ," +
	            "labelAlign:'right'," +
                "labelWidth:60," +
	            "enableKeyEvents:true," +           
                "listeners: {" +   
                    "keyup: {  " +                 
                        "fn: function (field, e) {" +                       
                             "if (Ext.EventObject.ESC == e.getKey()) {" +
                                 "this.setValue('');" +               
                                 "me.clearFilter(); " +                    
                             "}else { " +
                                 "me.filterByText(this.getRawValue()); " +                      
                             "} " +                  
                        "}" +               
                   "}" +
                "}" +
			"}";
			arr.push({
				key:values['toolbar-filter-number'],
				value:value
			});
		}
		
		var itemsStr = "";
		for(var i=1;i<3;i++){
			for(var j in arr){
				if(arr[j].key == i){
					itemsStr += arr[j].value + ",";
				}
			}
		}
		if(itemsStr != ""){
			itemsStr = itemsStr.substring(0,itemsStr.length-1);
		}
		var toolbarStr = "";
		if(itemsStr != ""){
			toolbarStr = 
			"{" + 
				"xtype:'toolbar'," + 
				"border:0," + 
				"dock:'" + values['filter-position'] + "'," + 
				"items:[" + itemsStr + "]" + 
			"}";
		}
		return toolbarStr;
	},
	  /**
	 * 功能栏按钮组（一）
	 * @private
	 * @return {}
	 */
	createButtonToolbarStr:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		
		//刷新按钮
		if(values['toolbar-refresh-checkbox']){
			var value = 
			"{" +
				"type:'refresh'," + 
                "itemId:'refresh'," + 
				"text:'" + values['toolbar-refresh-text'] + "'," + 
				"iconCls:'build-button-refresh'," +
				"tooltip:'刷新数据'," +
				"handler:function(but,e){" + 
                
                    "var treeToolbar=me.getComponent('treeToolbar');"+
                    "if(treeToolbar==undefined||treeToolbar==null){"+//在按钮组一找不到时
                        "treeToolbar=me.getComponent('treeToolbarTwo');"+
                    "}"+
                    "treeToolbar=treeToolbar.getComponent('treeToolbar');"+
                    "if(treeToolbar&&treeToolbar!=undefined){"+
                        "var refresh=treeToolbar.getComponent('refresh');"+
                        "if(refresh&&refresh!=undefined){"+
                            //数据获取成功后,刷新按钮不可用
                            "refresh.disabled=true;"+
                        "}"+
                    "}"+
                    
					"me.load();"+
					
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-refresh-number'],
				value:value
			});
		}
		//收缩按钮
		if(values['toolbar-Minus-checkbox']){
			var value = 
			"{" +
				"type:'minus'," +
                "itemId:'minus'," +
				"text:'" + values['toolbar-Minus-text'] + "'," + 
				"iconCls:'build-button-arrow-in'," + 
				"tooltip:'收缩数据'," +
				"handler:function(but,e){" + 
					"me.collapseAll();"+
		            "me.getRootNode().expand();"+
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-Minus-number'],
				value:value
			});
		}
		//展开按钮
		if(values['toolbar-plus-checkbox']){
			var value = 
			"{" +
				"type:'plus'," + 
                "itemId:'plus'," + 
				"text:'" + values['toolbar-plus-text'] + "'," + 
				"iconCls:'build-button-arrow-out'," + 
				"tooltip:'展开数据'," +
				"handler:function(but,e){" + 
				    "me.expandAll();"+ 
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-plus-number'],
				value:value
			});
		}
		//新增按钮
		if(values['toolbar-add-checkbox']){
			
		    var winform_checkbox='';
		    if(values['winform-checkbox']){
		    	winform_checkbox=
		    	"var records = me.getSelectionModel().getSelection();"+
			    "me.openFormWin(but.type,-1);";
		    }
			
			var value = 
			"{" +
				"type:'add'," + 
				"text:'" + values['toolbar-add-text'] + "'," + 
				"iconCls:'build-button-add'," + 
				 "tooltip:'新增数据'," +
				"handler:function(but,e){"+
				"" +winform_checkbox +"" + 
			     	"me.fireEvent('addClick');"+
				"}" +  
			"}";
			arr.push({
				key:values['toolbar-add-number'],
				value:value
			});
		}
		//修改按钮
		if(values['toolbar-edit-checkbox']){
			var value = 
			"{" +
				"type:'edit'," + 
				"text:'" + values['toolbar-edit-text'] + "'," + 
				"iconCls:'build-button-edit'," + 
				 "tooltip:'修改数据'," +
				"handler:function(but,e){" + 
					"var records = me.getSelectionModel().getSelection();" + 
					"if(records.length == 1){" + 
					    "var record=me.getSelectionModel().getSelection(); "+
					   	"for(var i in record){"+
							"var id = record[i].get('tid');"+
						"}"+
						"me.openFormWin(but.type,id);" + 
						"me.fireEvent('editClick');"+
					"}else{" + 
						"Ext.Msg.alert('提示','请选择一条数据进行操作！');" + 
					"}"+
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-edit-number'],
				value:value
			});
		}
		//查看按钮
		if(values['toolbar-show-checkbox']){
			var value = 
			"{" +
				"type:'show'," + 
				"text:'" + values['toolbar-show-text'] + "'," + 
				"iconCls:'build-button-see'," + 
				"tooltip:'查看数据'," +
				"handler:function(but,e){" + 
					"var records = me.getSelectionModel().getSelection();" + 
					"if(records.length == 1){" + 
					    "var record=me.getSelectionModel().getSelection(); "+
					   	"for(var i in record){"+
							"var id = record[i].get('tid');"+
						"}"+
						"me.openFormWin(but.type,id);" + 
					"}else{" + 
						"Ext.Msg.alert('提示','请选择一条数据进行操作！');" + 
					"}"+
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-show-number'],
				value:value
			});
		}
		//删除按钮
		if(values['toolbar-del-checkbox']){
			var value = 
				"{" +
					"type:'del'," + 
					"text:'" + values['toolbar-del-text'] + "'," + 
					"iconCls:'delete'," + 
					"tooltip:'删除数据'," +
					"handler:function(but,e){" + 
						"var records = me.getSelectionModel().getSelection();" + 
						"if(records.length > 0){" + 
							"Ext.Msg.confirm('提示','确定要删除吗？',function (button){" + 
								"if(button == 'yes'){" + 
									"for(var i in records){" + 
		                    			"var id = records[i].get('tid');" +
		                				//删除后的处理
		                				"var callback = function(){" +
		                					"var node = me.getRootNode().findChild('tid',id,true);" +
		                		    		"node.remove();" +
		                				"};" +
		                				"me.deleteModuleServer(id,callback);" +
									"}" + 
								"}" + 
							"});" + 
						"}else{Ext.Msg.alert('提示','请选择数据进行操作！');}" + 
					"}" + 
				"}";
			arr.push({
				key:values['toolbar-del-number'],
				value:value
			});
		}
		var itemsStr = "";
		for(var i=1;i<8;i++){
			for(var j in arr){
				if(arr[j].key == i){
					itemsStr += arr[j].value + ",";
				}
			}
		}
		if(itemsStr != ""){
			itemsStr = itemsStr.substring(0,itemsStr.length-1);
		}
		var toolbarStr = "";
		if(itemsStr != ""){
			toolbarStr = 
			"{" + 
				"xtype:'toolbar'," + 
                "itemId:'treeToolbar'," + 
				"border:0," + 
				"dock:'" + values['toolbar-position'] + "'," + 
				"items:[" + itemsStr + "]" + 
			"}";
		}
		return toolbarStr;
	},
	 /**
	 * 功能栏按钮组（二）
	 * @private
	 * @return {}
	 */
	createButtonToolbarTwoStr:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		//确定按钮
		if(values['toolbar-confirm-checkbox']){
			var value = 
			"{" +
				"type:'confirm'," + 
				"text:'" + values['toolbar-confirm-text'] + "'," + 
				"iconCls:'build-button-save'," + 
				 "tooltip:'确定'," +
				"handler:function(but,e){" + 
				    "me.fireEvent('okClick');" +
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-confirm-number'],
				value:value
			});
		}
		//取消按钮
		if(values['toolbar-cancel-checkbox']){
			var value = 
			"{" +
				"type:'cancel'," + 
				"text:'" + values['toolbar-cancel-text'] + "'," + 
				"iconCls:'build-button-delete'," + 
				 "tooltip:'取消'," +
				"handler:function(but,e){" + 
				    "me.fireEvent('cancelClick');" +
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-cancel-number'],
				value:value
			});
		}
		var itemsStr = "";
		for(var i=1;i<3;i++){
			for(var j in arr){
				if(arr[j].key == i){
					itemsStr += arr[j].value + ",";
				}
			}
		}
		if(itemsStr != ""){
			itemsStr = itemsStr.substring(0,itemsStr.length-1);
		}
		var toolbarStr = "";
		if(itemsStr != ""){
			toolbarStr = 
			"{" + 
				"xtype:'toolbar'," + 
                "itemId:'treeToolbartwo'," + 
				"border:0," + 
				"dock:'" + values['toolbar-position-two'] + "'," + 
				"items:[" + itemsStr + "]" + 
			"}";
		}
		return toolbarStr;
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
		
		//生成过滤
		var filterbar = me.createFilterbarStr();
		//按钮组一
		var buttontoobar = me.createButtonToolbarStr();
		//按钮组二
		var buttontoobartwo = me.createButtonToolbarTwoStr();
		
	    var values = me.getButtonsConfigValues();
        var filterposition=values['filter-position'];//过滤停放位置
        var toolbarposition=values['toolbar-position'];//按钮组一停放位置
        var toolbarpositiontwo=values['toolbar-position-two'];//按钮组二停放位置
       
        filterbarArr=[];//过滤
        buttontoobarArr=[];//按钮组一
        buttontoobartwoArr=[];//按钮组二
        filterbarArr.push(filterbar);
        buttontoobarArr.push(buttontoobar);
        buttontoobartwoArr.push(buttontoobartwo);
  
        var toobarArr='';
        arr='';
        if(filterbar!="" && buttontoobartwo==""  && buttontoobar==""){
        	arrtwo=filterbarArr;
	    	toobarArr=
	    		"[{"+
				"xtype:'toolbar',"+
                "itemId:'treeToolbar',"+
                "dock:'" + filterposition + "'," +
                "items:[" + arrtwo + "]" + 
            "}];";
	    	toolbarposition=filterposition;
	    	toolbarpositiontwo=filterposition;
    		dockedItemds += toobarArr + "";
	    }
	    if(filterbar!="" && buttontoobartwo!=""  && buttontoobar=="" ){
    		if(values['filter-number']==1 && values['toolbar-number-two']==3){
	    	    arr=filterbarArr.concat(buttontoobartwoArr);  
    		}else{
    			arr=buttontoobartwoArr.concat(filterbarArr); 
    		}
	    	toobarArr=
	    	"[{"+
	    		"xtype:'toolbar',"+
                "itemId:'treeToolbar',"+
	    		 "dock:'" + filterposition + "'," +
	    		 "items:[" + arr + "]" + 
	    	"}];";
	    	toolbarposition=filterposition;
    		dockedItemds += toobarArr + "";
    		
	    }
	    if(filterbar!="" && buttontoobartwo==""  && buttontoobar!="" ){
	    	if(values['filter-number']==1 && values['toolbar-number']==2){
	    	    arr=filterbarArr.concat(buttontoobarArr);  
	    	}else{
	    	    arr=buttontoobarArr.concat(filterbarArr);  
	    	}
	    	toobarArr=
	    		"[{"+
	    		"xtype:'toolbar',"+
                "itemId:'treeToolbar',"+
	    		"dock:'" + filterposition + "'," +
	    		 "items:[" + arr + "]" + 
	 	    	"}];";
	    	
	    	toolbarpositiontwo=filterposition;
    		dockedItemds += toobarArr + "";
	    }
	    if(filterbar=="" && buttontoobartwo!=""  && buttontoobar==""){
	    	arr=buttontoobartwoArr;  
	    	toobarArr=
	    		"[{"+
	    		"xtype:'toolbar',"+
                "itemId:'treeToolbar',"+
                "dock:'" + toolbarpositiontwo + "'," +
                "items:[" + arr + "]" + 
	 	    	"}];";
	    	toolbarposition=toolbarpositiontwo;
	    	filterposition=toolbarpositiontwo;
    		dockedItemds += toobarArr + "";
	    }
	    if(filterbar=="" && buttontoobartwo!=""  && buttontoobar!="" ){
	    	if(values['toolbar-number']==1 && values['toolbar-number-two']==2){
	    	    arr=buttontoobarArr.concat(buttontoobartwoArr);  
	    	}else{
	    		 arr=buttontoobartwoArr.concat(buttontoobarArr);  
	    	}
	    	toobarArr=
                "[{"+
	    		"xtype:'toolbar',"+
                "itemId:'treeToolbar',"+
	    		"dock:'" + toolbarpositiontwo + "'," +
	    		 "items:[" + arr + "]" + 
	 	    	"}];";
	    	filterposition=toolbarpositiontwo;
    		dockedItemds += toobarArr + "";
	    }
	    if(filterbar=="" && buttontoobartwo==""  && buttontoobar!=""){
	    	arr=buttontoobarArr;  
	    	toobarArr=
	    		"[{"+
	    		"xtype:'toolbar',"+
                "itemId:'treeToolbar',"+
	    		"dock:'" + filterposition + "'," +
	    		 "items:[" + arr + "]" + 
	 	    	"}];";
	    	toolbarpositiontwo=toolbarposition;
	    	filterposition=toolbarposition;
    		dockedItemds += toobarArr + "";
	    }
    	if(filterbar != "" || buttontoobar !="" || buttontoobartwo !=""){
    		 //停靠位置相同(顶部,底部)
        	if(toolbarpositiontwo === toolbarposition   && toolbarposition===filterposition ){
	    		if(values['filter-number']==1 && values['toolbar-number']==2 && values['toolbar-number-two']==3){
	    			arr=filterbarArr.concat(buttontoobarArr,buttontoobartwoArr);  
	    		}else if (values['filter-number']==1 && values['toolbar-number']==3 && values['toolbar-number-two']==2){
	    			arr=filterbarArr.concat(buttontoobartwoArr,buttontoobarArr); 
	    		}else if (values['filter-number']==1 && values['toolbar-number']==3 && values['toolbar-number-two']==2){
	    			arr=filterbarArr.concat(buttontoobartwoArr,buttontoobarArr); 
	    		}else if (values['filter-number']==2 && values['toolbar-number']==1 && values['toolbar-number-two']==2){
	    			arr=buttontoobarArr.concat(filterbarArr,buttontoobartwoArr); 
	    		}else if (values['filter-number']==2 && values['toolbar-number']==3 && values['toolbar-number-two']==1){
	    			arr=buttontoobartwoArr.concat(filterbarArr,buttontoobarArr); 
	    		}else if (values['filter-number']==3 && values['toolbar-number']==2 && values['toolbar-number-two']==1){
        			arr=buttontoobartwoArr.concat(buttontoobarArr,filterbarArr);  
        		}else if (values['filter-number']==3 && values['toolbar-number']==1 && values['toolbar-number-two']==2){
        			arr=buttontoobarArr.concat(buttontoobartwoArr,filterbarArr);  
        		}
	    		toobarArr=
				"[{"+
				    "xtype:'toolbar',"+
                    "itemId:'treeToolbar',"+
		            "dock:'" + toolbarposition + "'," +
		            "items:[" + arr + "]" + 
	            "}];";
				dockedItemds += toobarArr + "";
		}
      	//当过滤栏（不同)、按钮组二 和按钮组一停靠位置（同）
    	if(filterposition != toolbarposition && toolbarposition==toolbarpositiontwo ){
    		if(values['toolbar-number']==2 && values['toolbar-number-two']==3){
    		    arr=buttontoobarArr.concat(buttontoobartwoArr);  
    		}else{
    			 arr=buttontoobartwoArr.concat(buttontoobarArr); 
    		}
    		arrtwo=filterbarArr;
    	    toobarArr=
    	    "[{"+
			    "xtype:'toolbar',"+
                "itemId:'treeToolbar',"+
			    "dock:'" + toolbarposition + "'," +
			    "items:[" + arr + "]" + 
            "},{"+
            	"xtype:'toolbar',"+
                "itemId:'treeToolbarTwo',"+
            	"dock:'" + filterposition + "'," +
            	 "items:[" + arrtwo + "]" + 
            "}];";
    		dockedItemds += toobarArr + "";
    	}
    	//过滤栏和按钮组一（相同）   和按钮组二 （不同）
    	if(filterposition == toolbarposition && toolbarposition!=toolbarpositiontwo ){
    		if(values['filter-number']==1 && values['toolbar-number']==2 ){
    		arr=filterbarArr.concat(buttontoobarArr);  
    	    }else{
    	    	arr=buttontoobarArr.concat(filterbarArr);
    	    }
    		arrtwo=buttontoobartwoArr;
    		toobarArr=
    			"[{"+
    			    "xtype:'toolbar',"+
                    "itemId:'treeToolbar',"+
                    "dock:'" + filterposition + "'," +
                    "items:[" + arr + "]" + 
                "},{"+
                	"xtype:'toolbar',"+
                    "itemId:'treeToolbarTwo',"+
                    "dock:'" + toolbarpositiontwo + "'," +
                    "items:[" + arrtwo + "]" + 
                "}];";
           dockedItemds += toobarArr + "";
       }
    	//当过滤栏的停靠位置和按钮组二相同   和按钮组一停靠位置不相同时
    	if(filterposition == toolbarpositiontwo && toolbarposition!=toolbarpositiontwo ){
    		if(values['filter-number']==1 && values['toolbar-number-two']==2 ){
    		    arr=filterbarArr.concat(buttontoobartwoArr);  
    		}else{
    			arr=buttontoobartwoArr.concat(filterbarArr);  
    		}
    		arrtwo=buttontoobarArr;
    		toobarArr=
    			"[{" +
    			    "xtype:'toolbar'," +
                    "itemId:'treeToolbar',"+
    			    "dock:'" + filterposition + "'," +
    			    "items:[" + arr + "]" + 
                "},{" +
                	"xtype:'toolbar'," +
                    "itemId:'treeToolbarTwo',"+
                	"dock:'" + toolbarposition + "'," +
                	 "items:[" + arrtwo + "]" + 
                "}];";
    		 dockedItemds += toobarArr + "";
        }
		if(dockedItemds != ""){
			dockedItemds = dockedItemds.substring(0,dockedItemds.length-1);
		}
		return dockedItemds;
	    }
    },
	/**
	 * 弹出表单代码
	 * @private
	 * @return {}
	 */
	createOpenFormWinStr:function(){
        var me=this;
        var fun="";
        var values = me.getButtonsConfigValues();
        var winId = values['winform-id'];
        
        fun=
        	"function(type,id){" + 
        	"var me=this;" + 
			"var winId='" + winId +"';" + 
			"var callback=function(appInfo){" + 
				"if(appInfo&&appInfo!=''){" + 
					"var ClassCode=appInfo.BTDAppComponents_ClassCode;" + 
					"if(ClassCode&&ClassCode!=''){" + 
						"var panelParams = {" + 
							"type:type," + 
							"dataId:id," + 
							"modal:true," + 
							"floating:true," + 
							"closable:true," + 
							"draggable:true" + 
						"};" + 
						"var Class = eval(ClassCode);" + 
						"var panel = Ext.create(Class,panelParams).show();" + 
						"panel.on({saveClick:function(){panel.close();me.load();me.fireEvent('saveClick');}});" + 
					"}else{" + 
						"Ext.Msg.alert('提示','没有类代码！');" + 
					"}" + 
				 "}" + 
			"};" + 
			"me.getAppInfoFromServer(winId,callback);" + 
		"}"; 
        return fun;
    },
    /**
     * 从后台获取应用信息
     * @private
     * @param {} callback
     */
    getAppInfoFromServerStr:function(){
    	var fun="";
    	fun="function(id,callback){" + 
				"var me=this;" + 
				"me.getAppInfoServerUrl=getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById';" + 
				"var url=me.getAppInfoServerUrl+'?isPlanish=true&id='+id;" + 
				"Ext.Ajax.defaultPostHeader='application/json';" + 
				"Ext.Ajax.request({" + 
					"async:false," + 
					"url:url," + 
					"method:'GET'," + 
					"timeout:2000," + 
					"success:function(response,opts){" + 
						"var result=Ext.JSON.decode(response.responseText);" + 
						"if(result.success){" + 
							"var appInfo='';" + 
							"if(result.ResultDataValue&&result.ResultDataValue!=''){" + 
								"appInfo=Ext.JSON.decode(result.ResultDataValue);" + 
							"}" + 
							"if(appInfo!=''){" + 
								"if(Ext.typeOf(callback)=='function'){" + 
									"callback(appInfo);" + 
								"}" + 
							"}else{" + 
								"Ext.Msg.alert('提示','没有获取到应用组件信息！');" + 
							"}" + 
						"}else{" + 
//							"Ext.Msg.alert('提示','获取应用组件信息失败！错误信息【<b style=\\\"color:red\\\">'+result.ErrorInfo+'</b>】');" + 
						"}" + 
					"}," + 
					"failure:function(response,options){" + 
						"Ext.Msg.alert('提示','获取应用组件信息请求失败！');" + 
					"}" + 
				"});" + 
			"}";
        return fun;
    }
});


//树的拖拽  重写子节点变为父节点
Ext.override(Ext.tree.ViewDropZone, {
    getPosition: function (e, node) {
        var view = this.view,
        record = view.getRecord(node),
        y = e.getPageY(),
        noAppend = record.isLeaf(),
        noBelow = false,
        region = Ext.fly(node).getRegion(),
        fragment;
        if (record.isRoot()) {
            return 'append';
        }
        if (this.appendOnly) {
            return noAppend ? false : 'append';
        }
        if (!this.allowParentInsert) {
            noBelow = this.allowLeafInserts || (record.hasChildNodes() && record.isExpanded());
        }
        fragment = (region.bottom - region.top) / (noAppend ? 2 : 3);
        if (y >= region.top && y < (region.top + fragment)) {
            return 'before';
        }
        else if (!noBelow && (noAppend || (y >= (region.bottom - fragment) && y <= region.bottom))) {
            return 'after';
        }
        else {
            return 'append';
        }
    },
    handleNodeDrop: function (data, targetNode, position) {
        var me = this,
        view = me.view,
        parentNode = targetNode.parentNode,
        store = view.getStore(),
        recordDomNodes = [],
        records, i, len,
        insertionMethod, argList,
        needTargetExpand,
        transferData,
        processDrop;
        if (data.copy) {
            records = data.records;
            data.records = [];
            for (i = 0, len = records.length; i < len; i++) {
                data.records.push(Ext.apply({}, records[i].data));
            }
        }
        me.cancelExpand();
        if (position == 'before') {
            insertionMethod = parentNode.insertBefore;
            argList = [null, targetNode];
            targetNode = parentNode;
        }
        else if (position == 'after') {
            if (targetNode.nextSibling) {
                insertionMethod = parentNode.insertBefore;
                argList = [null, targetNode.nextSibling];
            }
            else {
                insertionMethod = parentNode.appendChild;
                argList = [null];
            }
            targetNode = parentNode;
        }
        else {
            if (this.allowLeafInserts) {
                if (targetNode.get('leaf')) {
                    targetNode.set('leaf', false);
                    targetNode.set('expanded', true);
                }
            }
            if (!targetNode.isExpanded()) {
                needTargetExpand = true;
            }
            insertionMethod = targetNode.appendChild;
            argList = [null];
        }

        transferData = function () {
            var node;
            for (i = 0, len = data.records.length; i < len; i++) {
                argList[0] = data.records[i];
                node = insertionMethod.apply(targetNode, argList);
                if (Ext.enableFx && me.dropHighlight) {
                    recordDomNodes.push(view.getNode(node));
                }
            }
            if (Ext.enableFx && me.dropHighlight) {
                Ext.Array.forEach(recordDomNodes, function (n) {
                    if (n) {
                        Ext.fly(n.firstChild ? n.firstChild : n).highlight(me.dropHighlightColor);
                    }
                });
            }
        };
        if (needTargetExpand) {
            targetNode.expand(false, transferData);
        }
        else {
            transferData();
        }
        }
    });
Ext.override(Ext.tree.plugin.TreeViewDragDrop, {
    allowLeafInserts: true,
    onViewRender: function (view) {
        var me = this;
        if (me.enableDrag) {
            me.dragZone = Ext.create('Ext.tree.ViewDragZone', {
                view: view,
                allowLeafInserts: me.allowLeafInserts,
                ddGroup: me.dragGroup || me.ddGroup,
                dragText: me.dragText,
                repairHighlightColor: me.nodeHighlightColor,
                repairHighlight: me.nodeHighlightOnRepair
            });
        }
        if (me.enableDrop) {
            me.dropZone = Ext.create('Ext.tree.ViewDropZone', {
                view: view,
                ddGroup: me.dropGroup || me.ddGroup,
                allowContainerDrops: me.allowContainerDrops,
                appendOnly: me.appendOnly,
                allowLeafInserts: me.allowLeafInserts,
                allowParentInserts: me.allowParentInserts,
                expandDelay: me.expandDelay,
                dropHighlightColor: me.nodeHighlightColor,
                dropHighlight: me.nodeHighlightOnDrop
            });
        }
    }
});