/**
 * 列表树构建工具
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicTreePanel',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basictreepanel',
    testAppId:-1,
    appId:-1,//应用组件ID
    appCName:'',//中文名称
    appExplain:'',//组件简介
    buildTitle:'列表树构建工具',//构建名称
    /**
     * 默认选中节点ID
     * @type 
     */
    selectId:null,
    /**
     * 默认隐藏ID，隐藏该节点及所有子孙节点
     * @type 
     */
    hideNodeId:null,
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
    ObjectPropertyRoot:'ResultDataValue',
    /**
     * 获取应用信息的后台服务地址
     * @type String
     */
    getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    //树的属性设置
    filterfield:'text',

    defaultWidth:720,//表
    defaultHeight:230,

    defaultPanelWidth:780,  //初始宽度
    defaultPanelHeight:240, //初始高 度

    testId:0,
    appType:-1,
   
    /***
     * 对象字段数组,代替moduleFields
     * 基础字段数组
     * @type 
     */
    defaultFields:[
        {name:'text',type:'auto'},//默认的现实字段
        {name:'expanded',type:'auto'},//是否默认展开
        {name:'leaf',type:'auto'},//是否叶子节点
        {name:'icon',type:'auto'},//图标
        {name:'url',type:'auto'},//地址
        {name:'tid',type:'auto'},//默认ID号
        {name:'Id',type:'auto'},//ID
        {name:'ParentID',type:'auto'},//ParentID
        {name:'hasBeenDeleted',type:'auto'},//ID
        {name:'value',type:'auto'}//模块对象
    ], 
    /***
     * 对象字段数组,代替moduleFields
     * 基础字段数组
     * @type 
     */
    defaultFieldsChecked:[
        {name:'text',type:'auto'},//默认的现实字段
        {name:'expanded',type:'auto'},//是否默认展开
        {name:'leaf',type:'auto'},//是否叶子节点
        {name:'icon',type:'auto'},//图标
        {name:'url',type:'auto'},//地址
        {name:'tid',type:'auto'},//默认ID号
        {name:'Id',type:'auto'},//ID
        {name:'ParentID',type:'auto'},//ParentID
        {name:'value',type:'auto'},//模块对象
        {name:'hasBeenDeleted',type:'auto'},//模块对象
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
    objectGetDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName',
    objectSaveDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchParaEntityServiceListByEntityName',
   
    //获取树的服务地址(前半部分加相关的数据表的名称)如"getRootPath()+'/RBACService.svc/RBAC_UDTO_Search"+"RBACModule"+"ToTree"
    objectgetTreeServerUrl:'/RBACService.svc/RBAC_UDTO_Search',

    //保存的后台服务地址
    addServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_AddBTDAppComponents',
    
    /**
     * 修改保存的后台服务地址
     * @type String
     */
    editServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_UpdateBTDAppComponents',
    
    //获取参数的后台服务地址
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

    childrenField:'Tree',
    //展示区域的生成树itemId()
    treeItemId:"treeItemId",
    
    //表格树的一般显示列集合(表格树列集合分为主列(第一列),一般显示列,操作列)
    //表格树列集合分为主列(第一列)
    firstolumn:{
        xtype:'treecolumn',text:'中文名称',
        width:200,sortable:true,
        dataIndex:'text'
    },
    
        
    //对齐方式
    AlignTypeList:[
        ['left','左对齐'],
        ['center','居中'],
        ['right','右对齐']
    ],

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
        me.addEvents('addClick');//操作列添加
        me.addEvents('editClick');//操作列编辑
        me.addEvents('showClick');//操作列查看
        me.addEvents('delClick');//操作列删除
        
        me.callParent(arguments);
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
        var objectPropertyTree =me.getobjectPropertyTree();
        var callback = function(appInfo){
            var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
            var panelParams = appParams.panelParams;
            var southParams = appParams.southParams;
            me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];
            var appComID=appParams.appComID;//选择表单的Id
            //赋值
            me.setSouthRecordByArray(southParams);//数据项列表赋值
            me.setObjData();//数据对象赋值
            objectPropertyTree.store.on({
                load:function(store,node,records,successful,e){
                    if(me.appId != -1 && me.isJustOpen && node == objectPropertyTree.getRootNode()){
                        //对象内容勾选
                        me.changeObjChecked(southParams);
                    }
                }
            });
            me.setPanelParams(panelParams);//属性面板赋值
            //获取获取数据服务列表
            var getDataServerUrl =me.getDataServerUrl();
            getDataServerUrl.value = panelParams.getDataServerUrl;

            var delDataServerUrl=me.getdelDataServerUrl();
            delDataServerUrl.value = panelParams.delDataServerUrl ;
            
            var editDataServerUrl=me.geteditDataServerUrl();
            editDataServerUrl.value = panelParams.editDataServerUrl ;
            var appComID2=me.getappComID();//选择表单的Id
            appComID2.setValue(appComID);//还原选择表单的应用Id
        
        };
        //从后台获取应用信息
        me.getAppInfoFromServer(callback);
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
            regionWeights:{south:1,east:2,north:3}
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
        var south = me.createSouth();
        //功能模块ItemId
        center.itemId = "center";
        north.itemId = "north";
        east.itemId = "east";
        south.itemId = "south";
        
        //功能块位置
        center.region = "center";
        north.region = "north";
        east.region = "east";
        south.region = "south";
        
        //功能块大小
        north.height = 30;
        south.height = 200;
        east.width = 250;
        
        //功能块收缩属性
        east.split = true;
        east.collapsible = true;
        south.split = true;
        south.collapsible = true;
        me.items = [north,center,east,south];
        
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
                title:'列表树',
                itemId:'center',
                border:0,
	            autoScroll:true,
                bodyPadding:'1 10 10 1'
	            //items:[form]
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
                
                {xtype:'button',text:'更新树数据',itemId:'loadFormValues',iconCls:'build-button-refresh',margin:'0 4 0 0',
                    handler:function(){
                	    me.load();
                    }
                },
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
     * 树列属性列表
     * @private
     * @return {}
     */
    createSouth:function(){
        var me = this;
        var com = {
            xtype:'grid',
            title:'树列属性列表',
            columnLines:true,//在行上增加分割线
            columns:[//列模式的集合
                {xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
                {text:'交互字段',dataIndex:'InteractionField',editor:{readOnly:true},disabled:true},
                {text:'显示名称',dataIndex:'DisplayName',
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                record.set('DisplayName',newValue);
                                record.commit();
                            }
                        }
                    }   
                },
                {text:'列宽',dataIndex:'Width',width:50,align:'center',
                    xtype:'numbercolumn',
                    format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:480
                    }
                }, 
            {text:'列头字体内容',dataIndex:'HeadFont',hidden:true},
            {xtype:'actioncolumn',text:'列头字体',width:60,align:'center',hidden:true,
                items:[{
                    iconCls:'build-img-font-configuration hand',
                    tooltip: '列头字体设置',
                    handler: function(grid, rowIndex, colIndex) {
                        var rec = grid.getStore().getAt(rowIndex);
                        me.OpenCategoryWinTwo(rec,"HeadFont");
                    }
                }]
            },
            {text:'标题设置',dataIndex:'IsSetTitle',width:80,align:'center',hidden:true,
                xtype:'checkcolumn', 
                editor:{
                    xtype:'checkbox',
                    cls:'x-grid-checkheader-editor'
                }
            },
            {text:'锁定',dataIndex:'IsLocked',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'隐藏',dataIndex:'IsHidden',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'不可见',dataIndex:'CannotSee',width:50,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'可排序',dataIndex:'CanSort',width:50,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'默认排序',dataIndex:'DefaultSort',width:60,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text: '排序方式',dataIndex:'SortType',width:60,align:'center',
                    renderer:function(value, p, record){
                        if(value == 'ASC'){
                            return Ext.String.format('正序');
                        }else{
                            return Ext.String.format('倒序');
                        }
                    },
                    editor:new Ext.form.field.ComboBox({
                        mode:'local',editable:false,
                        displayField:'text',valueField:'value',
                        store:new Ext.data.SimpleStore({ 
                            fields:['value','text'], 
                            data:[['ASC','正序'],['DESC','倒序']]
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
                {text: '列次序',dataIndex:'OrderNum',width:60,align:'center',
                    xtype:'numbercolumn',
                    format:'第0列',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:999
                    }
                }
            ],
            store:Ext.create('Ext.data.Store',{
                fields:me.getSouthStoreFields(),
                proxy:{
                    type:'memory',
                    reader:{type:'json',root:'list'}
                }
            }),
            plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1})
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
       var fields = [];
       var checked2=false;
       checked2=me.getcheckedtypeValue();//获取是否带复选框的值
       var columnParams2=[];
       if(checked2==true){
            columnParams2=me.defaultFieldsChecked;
       }else{
            columnParams2=me.defaultFields;
       }
       fields=columnParams2;
       var columnParams = me.getColumnParams();
       for(var i in columnParams){
            var tempArr=columnParams[i].InteractionField.split('_');
            var tempField=tempArr[tempArr.length-1];
            var showFile={name:tempField,type:'auto'};//.split('_');
            fields.push(showFile);
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
        //保存信息
        var appInfo = me.createAppInfo();
        //数据对象
        var dataObj = me.createDataObj();
        //其他设置
        var other = me.createOther();
         
        var arrItems =[];
        arrItems.push(appInfo);
        arrItems.push(dataObj);
        
        //组件基础属性
        var basicItems =me.createBasicItems();
        var titleSet =me.createTreeTitleSet();
        var toolsBarSet = me.createTreeToolsBarSet();
        var columnSettings =me.createColumnSettings();
        var menuItems =me.createMenuItems();
        
        arrItems.push(titleSet);
        arrItems.push(basicItems);
        arrItems.push(toolsBarSet);
        arrItems.push(columnSettings);
        arrItems.push(menuItems);
        arrItems.push(other);
        var listParamsPanel = {
            xtype:'form',
            itemId:'center' + me.ParamsPanelItemIdSuffix,
            title:'列表树构建配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:arrItems
        };
        
        var com = {
            xtype:'form',
            title:'列表树构建配置',
            autoScroll:true,
            items:[listParamsPanel]
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
     * 标题栏设置
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
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
         
                    }
                },   
                items:[
                    {boxLabel:'显示',name:'IsShowTitle',inputValue:'true'},
                    {boxLabel:'隐藏',name:'IsShowTitle',inputValue:'false'}
                ]
            },{
                xtype:'textfield',fieldLabel:'标题栏名称',labelWidth:80,anchor:'100%',
                itemId:'titleName',name:'titleName',
                listeners:{
                    blur:function(com,The,eOpts){
                    }
                } 
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
                xtype:'combobox',fieldLabel:'数据对象',
                itemId:'objectName',name:'objectName',
                labelWidth:55,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                emptyText:'请选择数据对象',
                displayField:me.objectDisplayField,
                valueField:me.objectValueField,
                store:            
                new Ext.data.Store({
                    fields:me.objectFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectUrl,
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:me.changeStoreData
                    },
                    autoLoad:true
                })
            },
            {
                xtype:'combobox',fieldLabel:'获取数据',
                itemId:'getDataServerUrl',
                name:'getDataServerUrl',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                emptyText:'请选择获取数据服务',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                listeners:{
                    change:function(combo,newValue,oldValue,eOpts ){
                     }
                },
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl+ "?" + me.objectServerParam + "=Tree",
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData  //数据适配  
                    },
                    listeners:{
                        beforeload:function(store,operation,eOpts){ 
                            var objectName =me.getobjectName();
                            var myurl = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=Tree" + objectName.value;
                           store.proxy.url =myurl;
                       }
                    }
                })
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
                            var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                            var dataObject = formParamsPanel.getComponent('dataObject');
                            var objectName = dataObject.getComponent('objectName');
                            store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + objectName.value;
                            
                        }
                    }
                })
            },
            {
                xtype:'treepanel',itemId:'objectPropertyTree',border:false,
                //dockedItems:[],
                enableDD:false,
                rootVisible:true,
                useArrows: true,
                nodeClassName:'',
                listeners:{
                    beforeitemexpand:function(node){
                        this.nodeClassName = node.data.InteractionField;
                    },
                    beforeload:function(store){
                        if(this.nodeClassName != ""){
                            store.proxy.url = me.ObjectPropertyUrl + "?" + me.ObjectProperyParam + "=" + this.nodeClassName;
                        }
                    }
                    
                },
                store:new Ext.data.TreeStore({ 
                    fields:me.ObjectPropertyFields,
                    proxy:{
                        type:'ajax',
                        url:me.ObjectPropertyUrl,
                        extractResponseData: function(response){
                            var data = Ext.JSON.decode(response.responseText);
                            var children = Ext.JSON.decode(data.ResultDataValue);
                            for(var i in children){
                                children[i].checked = false;
                            }
                            data = data.children = children;
                            response.responseText = Ext.JSON.encode(data);
                            return response;
                        }
                    },
                    autoLoad:false,
                    defaultRootProperty:me.ObjectPropertyRoot
                })
            },
            {xtype:'combobox',fieldLabel:'删除数据',
                itemId:'delDataServerUrl',name:'delDataServerUrl',
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
                  change:function( combo,newValue,oldValue,eOpts ){
                        if(newValue!=null){}
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
            },
            {
	            xtype:'toolbar',
	            style:{background:'#fff'},
	            itemId:'objectPropertyToolbar',
                border:false,
	            items:[{
	                xtype:'button',text:'确定',itemId:'objectPropertyOK',
	                iconCls:'build-button-ok'
	            }]
             },
            {
                xtype:'textfield',fieldLabel:'默认条件',labelWidth:55,value:'',
                itemId:'defaultParams',name:'defaultParams',hidden:true
            } ]
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
                xtype:'textfield',fieldLabel:'空数据提示',labelWidth:65,value:'没有数据！',hidden:true,
                itemId:'emptyText',name:'emptyText'
            },{
                xtype:'textfield',fieldLabel:'加载提示',labelWidth:65,value:'获取数据中，请等待...',hidden:true,
                itemId:'loadingText',name:'loadingText'
            },{
                xtype:'checkbox',boxLabel:'弹出表单',
                fieldLabel:'',hideLabel:true,labelWidth:65,
                itemId:'hasformChoosek',name:'hasformChoosek',
                handler: function(com, newValue, oldValue, eOpts ){
                        var value=newValue;
                        if(value==false||value=='false'||value=='0'||value=='off'){
	                       var com=me.getnewAddformChoose();
	                       com.setValue('');
	                       var appComID=me.getappComID();
	                       appComID.setValue('');
                        }
                }
            },{
                xtype:'fieldcontainer',layout:'hbox',itemId:'winformapp',
                items:[{
                    xtype:'textfield',emptyText:'选择(新增/修改/查看/)表单',
                    width:185,readOnly:true,//appComID:'',//appComID类代码应用id属性
                    itemId:'newAddformChoose',name:'newAddformChoose',fieldLabel:'表单选择',labelWidth:85
                },
                 {//隐藏Id保存
			        xtype: 'hiddenfield',
			        name: 'appComID',
			        itemId:'appComID'
                 },
                {
                    xtype:'button',iconCls:'build-button-configuration-blue',
                    tooltip:'选择表单',margin:'0 0 0 2',
                    itemId:'winformbutton',name:'winformbutton',
                    handler: function(){
                        me.openAppListWin();
                    }
                }]
            }
        ]
        };
        return com;
       
    },
    /***
     * 创建功能栏按钮组(一)(部分)按钮
     * @return {}
     */
    createtoolsBtnOther:function(){
        var me=this;
        var otherArr=[
            { boxLabel: '刷新数据', name: 'toolsType', inputValue: 'false' ,itemId:'IsreFreshBtn',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
 
                    }
                }   
            },
            { boxLabel: '展开全部', name: 'toolsType', inputValue: 'false',itemId:'IsMinusBtn',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
 
                    }
                 }  
            },
            { boxLabel: '收缩全部', name: 'toolsType', inputValue: 'false',itemId:'IsPlusBtn',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){

                    }
              }
            }
           ];
        return otherArr;
    },
    browse:function(){
        var me = this;
         var center=me.getCenterCom();
         var tempItem= me.getCenterTreeCom();
         var owner = center.ownerCt;
             center.remove(tempItem);
         //重新生成新的控件
         var com =me.createTableTree();
         var coms=[];
         coms.push(com);
         center.add(coms);
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
     * 删除勾选的应用
     * @private
     */
    deleteAppInfo:function(record){
    	var me = this;
    	var myTree=me.getCenterTreeCom();
    	var records = myTree.getSelectionModel().getSelection();
    	var id = record.get('Id');
    	if(records.length > 0){
    		Ext.Msg.confirm("警告","确定要删除吗？",function (button){
    			if(button == "yes"){
    				Ext.Array.each(records,function(record){
		    			//没有被删除的才去后台删除
			    		if(record.get('hasBeenDeleted') != "true"){
			    			
			    			me.deleteAppServer(id,record);
			    		}
		    		});
    			}
    		});
    	}else{
    		Ext.Msg.alert("提示","<b style='color:red'>请选择需要删除的行记录！</b>");
    	}
    },
    /**
     * 后台删除应用信息
     * @private
     * @param {} id
     */
    deleteAppServer:function(id,record){
    	var me = this;
        var delServerUrl=me.getdelDataServerUrl();
        var deleteServerUrl='';
        if(delServerUrl.getValue()==null){
            Ext.Msg.alert('提示','请配置删除数据');
            return ;
        }else{
            deleteServerUrl=""+delServerUrl.getValue().split('?')[0]; 
        } 
        var url =""+getRootPath()+ "/"+deleteServerUrl.getValue().split('?')[0] + "?id=" + id;
         
    	Ext.Ajax.defaultPostHeader = 'application/json';
    	Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
		    		record.set("hasBeenDeleted","true");
		    		record.commit();
				}else{
					record.set("hasBeenDeleted","false");
		    		record.commit();
				}
				
			},
			failure:function(response,options){ 
				record.set("hasBeenDeleted","false");
		    	record.commit();
		    	
			}
		});
    },
    
    
    //=====================与后台交互=======================
    /**
     * 后台删除应用信息
     * @private
     * @param {} id
     */
    deletemenuServer:function(record){
        var me = this;

        var delServerUrl=me.getdelDataServerUrl();
        var deleteServerUrl='';
        if(delServerUrl.getValue()==null){
            Ext.Msg.alert('提示','请配置删除数据');
            return ;
	    }else{
	        deleteServerUrl=""+delServerUrl.getValue().split('?')[0]; 
	    } 
        var id = record.get('Id');
        var url =""+getRootPath()+ "/"+deleteServerUrl.getValue().split('?')[0] + "?id=" + id;
    	Ext.Ajax.defaultPostHeader = 'application/json';
    	Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
		    		record.set("hasBeenDeleted","true");
		    		record.commit();
				}else{
					record.set("hasBeenDeleted","false");
		    		record.commit();
				}
			},
			failure:function(response,options){ 
				record.set("hasBeenDeleted","false");
		    	record.commit();
			}
		});
    },

    /**
     * 功能栏删除按钮方法
     * @private
     */
    deleteModuleTools:function(){
        var me = this;
        var myTree=me.getCenterTreeCom();
        var records = myTree.getSelectionModel().getSelection();
    	if(records.length > 0){
    		Ext.Msg.confirm("警告","确定要删除吗？",function (button){
    			if(button == "yes"){
    				Ext.Array.each(records,function(record){
		    			//没有被删除的才去后台删除
			    		if(record.get('hasBeenDeleted') != "true"){
			    			me.deletemenuServer(record);
			    		}
		    		});
    			}
    		});
    	}else{
    		Ext.Msg.alert("提示","<b style='color:red'>请选择需要删除的记录！</b>");
    	}
    	
    }, 
    /**
     * 数据行上的功能按钮点击事件处理
     * @private
     * @param {} type
     * @param {} record
     */
    itemButtonClick:function(type,record){
        var me = this;
        var obj = {
            Id:record.get('Id'),//模块ID
            ParentID:record.get('ParentID'),//树形结构父级ID
            LevelNum:parseInt(record.get('LevelNum'))+1,//树形结构层级
            TreeCatalog:parseInt(record.get('TreeCatalog'))+1//树形结构层级Code
        };
        //弹出模块页面
        me.openModuleEditWin(type,obj);
    },
    
    /**
     * 弹出模块页面
     * @private
     * @param {} type
     * @param {} id
     */
    openModuleEditWin:function(type,obj){
        var me = this;
        var title = "";
        var Id = -1;
        var ParentID = obj.ParentID;
        var LevelNum = obj.LevelNum;
        var TreeCatalog = obj.TreeCatalog;
        if(type == "add"){
            title = "新增";
        }else if(type == "edit"){
            title = "修改";
            Id = obj.Id;
        }else if(type == "show"){
            title = "查看";
            Id = obj.Id;
        }
        var modelForm = Ext.create('Ext.manage.ModuleForm',{
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true,//可移动
            isWindow:true,//窗口打开
            getAppListServerUrl:me.getAppListServerUrl,
            updateFileServerUrl:me.updateFileServerUrl,
            title:title,
            type:type,
            Id:Id,
            ParentID:ParentID,
            LevelNum:LevelNum,
            TreeCatalog:TreeCatalog
        }).show();
        modelForm.on({
            btnsaveClick:function(){me.load();},
            saveAsClick:function(){me.load();}
        });
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
            if(appClass==''){
                isOk = false;
                return isOk;
            }else{
	            //应用组件ID
	            var id = bo ? me.appId : -1;
	            //生成应用对象
	            var BTDAppComponents = {
	                Id:id,//应用组件ID
	                CName:params.appCName,//名称
	                ModuleOperCode:params.appCode,//功能编码
	                ModuleOperInfo:params.appExplain,//功能简介
	                InitParameter:params.defaultParams,//初始化参数
	                AppType:me.appType,//应用类型(列表树)
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
        var panelParams = me.getPanelParams();
        var southParams = me.getSouthRocordInfoArray();
        var appComID2=me.getappComID();//选择表单的Id
        var appComID=appComID2.getValue();
        
        var appParams = {
            panelParams:panelParams,
            southParams:southParams,
            appComID:appComID
        };
        return appParams;
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
     * 勾选数据对象内容
     * @private
     */
    changeObjChecked:function(southParams){
        var me = this;
        var objectPropertyTree =me.getobjectPropertyTree();//对象属性树
        var rootNode = objectPropertyTree.getRootNode();
        
        //展开需要展开的所有父节点
        var expandParentNode = function(value,callback){
            var arr = value.split("_");
            if(arr.length >1){
                var v = arr[0];
                var num = 1;
                var open = function(){
                    if(num < arr.length-1){
                        v = v + "_" + arr[num];
                        var n = rootNode.findChild("InteractionField",v,true);
                        if(!n.isExpanded()){//节点没有展开
                            num++;
                            n.expand(false,open);
                        }else{
                            num++;
                            open();
                        }
                    }else{
                        callback();//完成
                    }
                };
                open();
            }else{
                callback();
            }
        }
        
        //选中节点
        var checkedNode = function(value){
            var node = rootNode.findChild("InteractionField",value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                treeNodeCheckedChange(node,true);
            }
        }
        
        var nodeArr = [];//没展开的节点数组
        for(var i in southParams){
            var value = southParams[i].InteractionField;
            var node = rootNode.findChild("InteractionField",value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                treeNodeCheckedChange(node,true);
            }else{//节点不存在
                nodeArr.push(value);
            }
        }
        //勾选展开后的节点
        var openNodes = function(nodes){
            for(var i in nodes){
                checkedNode(nodes[i]);
            }
        }
        if(nodeArr.length == 0){
            me.isJustOpen = false;
            me.browse();//渲染效果
        }else{
            var count = 0;
            var changeNodes = function(num){
                var callback =function(){
                    if(num == nodeArr.length-1){
                        if(me.appId != -1 && me.isJustOpen){
                            openNodes(nodeArr);
                            me.isJustOpen = false;
                            me.browse();//渲染效果
                        }
                    }else{
                        changeNodes(++num);
                    }
                }
                expandParentNode(nodeArr[num],callback);
            }
            changeNodes(0);
        }
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
    /**
     * 创建可执行代码
     * @return {}
     */
    createHtmlApp:function(){
        var me = this;
        //列表配置参数
        var params = me.getListParams();
        //数据集合
        var storeStr = me.createStoreStr();
        var storeName = params.appCode + "_store";
        var store = "var " + storeName + "=" + storeStr;
        //可执行代码
        var appStr = me.createApp();
        var appName = params.appCode + "_app";
        var app = "var " + appName + "=" + appStr;
        
        var result = 
        "Ext.onReady(function(){" + 
            "Ext.QuickTips.init();" + 
            "Ext.Loader.setConfig({enabled:true});" + 
            store + ";" + app + ";" +  
            storeName + ".load();" + 
            "var viewport=Ext.create('Ext.container.Viewport',{" + 
                "padding:'4 4 4 4'," + 
                "layout:'fit'," + 
                "items:[" + appName + "]" + 
            "});" + 
        "});";
        
        return result;
    },
    
    /**
     * 创建HTML (暂时不启用)
     * @private
     * @return {}
     */
    createHtml:function(app){       
        var me = this;
        //列表配置参数
        var params = me.getListParams();
        //数据集合
        var storeStr = me.createStoreStr();
        var storeName = params.appCode + "_store";
        var store = "var " + storeName + "=" + storeStr;
        //可执行代码
        var appStr = me.createApp();
        var appName = params.appCode + "_app";
        var app = "var " + appName + "=" + appStr;
        
        var html = 
        "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" + 
        "<html>" + 
            "<head>" + 
                "<title>" + me.appCName + "</title>" + 
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>" + 

                "<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/extjs/resources/css/ext-all.css'/>" + 
                "<script type='text/javascript' src='" + getRootPath() + "/ui/extjs/ext-all-debug.js'></script>" + 
                "<script type='text/javascript' src='" + getRootPath() + "/ui/extjs/locale/ext-lang-zh_CN.js'></script>" + 
                
                "<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/extjs/ux/css/CheckHeader.css'/>" + 
                "<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/css/icon.css'/>" + 
                "<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/css/style.css'/>" + 
                
                "<script type='text/javascript'>" + 
                    "Ext.onReady(function(){" + 
                        "Ext.QuickTips.init();" + 
                        "Ext.Loader.setConfig({enabled: true});" + 
                        "Ext.Loader.setPath('Ext.ux','" + getRootPath() + "/ui/extjs/ux');" + 
                        store + ";" + app + ";" +  
                        storeName + ".load();" + 
                        "var viewport=Ext.create('Ext.container.Viewport',{" + 
                            "padding:'4 4 4 4'," + 
                            "layout:'fit'," + 
                            "items:[" + appName + "]" + 
                        "});" + 
                    "});" + 
                "</script>" + 
            "</head>" + 
            "<body></body>" + 
        "</html>";
        
        return html;
    },
    
    /**
     * 可执行代码
     * @private
     * @return {}
     */
    createApp:function(){
        var me = this;
        
        //列表配置参数
        var params = me.getListParams();
        //列属性(已排序)
        var columnParams = me.getColumnParams();
        //列
        var columns = me.createFields(columnParams);
        //是否有菜单选项
        var sortableColumns = me.createSortableColumns(columnParams);
        var app = 
        "{" + 

        "}";
        
        return app;
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
            {name:'Width',type:'int'},//列宽
            {name:'hasBeenDeleted',type:'bool'},//删除标记
            {name:'IsLocked',type:'bool'},//默认锁定
            {name:'IsHidden',type:'bool'},//默认隐藏
            {name:'CannotSee',type:'bool'},//不可见
            {name:'CanSort',type:'bool'},//可排序
            {name:'DefaultSort',type:'bool'},//默认排序
            {name:'SortType',type:'string'},//排序方式
            {name:'OrderNum',type:'int'},//排布顺序
            {name:'IsLocked',type:'bool'}//默认锁定
            
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
        //另存按钮事件
        me.initSaveAsListener();
    },
    /**
     * 功能栏另存按钮监听
     * @private
     */
    initSaveAsListener:function(){
        var me = this,
        north = me.getComponent('north');
        //保存按钮事件    
        var saveAs = north.getComponent('saveAs');
        if(saveAs){
            saveAs.on({
                click:function(){
                    me.testAppId = me.appId;
                    me.appId = -1;
                    me.save();
                    me.fireEvent('saveAsClick1');
                }
            });
        }
    },
    
    /**
     * 功能栏浏览按钮监听
     * @private
     */
    initBrowseListener:function(){
        var me = this,
        north = me.getComponent('north');
        //浏览按钮事件    
        var browseX = north.getComponent('browseX');
        if(browseX){
            browseX.on({
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
    /**
     * 属性面板监听
     * @private
     */
    initEastListener:function(){
        var me = this;
        //数据对象列表监听
        me.initObjectNamelistener();
        //对象所有属性树监听
        me.initObjectPropertyTreeListener();

    },

    /**
     * 数据对象列表监听
     * @private
     */
    initObjectNamelistener:function(){
        var me = this;
        var objectName =me.getobjectName();
        objectName.on({
            select:function(owner,records,eOpts){
                var center=me.getCenterCom();
                var owner = center.ownerCt;
                     center.removeAll();
                var list = me.getComponent('south');
                    list.store.removeAll();   
                me.setBasicParamsPanelValues();
                me.setColumnSettingsValues();//列属性赋值
                me.setfunctionBarSettingsValues();//功能按钮属性赋值
                me.setTreeMenuValues();
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
                    
                    //获取修改数据服务列表
			        var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
			        editDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + newValue;
			        editDataServerUrl.store.load();
                    me.DisplayName='';

               }else{
                   return ;
               }
            }
        });
   
    },
    /**
     * 对象所有属性树监听
     * @private
     */
    initObjectPropertyTreeListener:function(){
        /*向上遍历父结点*/ 
        var nodep=function(node){ 
            var bnode=true;
            Ext.Array.each(node.childNodes,function(v){ 
                if(!v.data.checked){
                    bnode=false;
                    return;
                }
            });
            return bnode;
        };
        var parentnode=function(node){
            if(node.parentNode != null){
                if(nodep(node.parentNode)){
                    node.parentNode.set('checked',true);
                }else{ 
                    node.parentNode.set('checked',false);
                }
                parentnode(node.parentNode);
            }
        };
        /*遍历子结点 选中 与取消选中操作*/
        var chd=function(node,check){
            node.set('checked',check);
            if(node.isNode){
                node.eachChild(function(child){
                    chd(child,check);
                }); 
            }
        };
        var me = this;
        
        var dataObject =me.getdataObject();
        var objectName =me.getobjectName();
        var objectPropertyTree =me.getobjectPropertyTree();
        objectPropertyTree.on('checkchange',function(node,checked){
            if(checked){
                node.eachChild(function (child){
                    chd(child,true);
                });
            }else{
                node.eachChild(function (child){
                    chd(child,false);
                });
            }
            parentnode(node);//进行父级选中操作 
        },objectPropertyTree);
        
        objectPropertyOK =me.getobjectPropertyOK();
        objectPropertyOK.on({
            click:function(){
                ok();
            }
        });
        //表格树事件
        var ok = function(){
            var ColumnParams =me.getobjectPropertyTree();//对象属性树
            //获取获取数据服务列表
            var selectServerUrl =me.getDataServerUrl();
            if(selectServerUrl.getValue()===null ){
                alert('请选择获取数据服务');
                return ;
             }
        var data = ColumnParams.getChecked();
        var store = me.getComponent('south').store;
        
        //勾选节点数组
        var dataArray = [];
        //列表中显示被勾选中的对象
        Ext.Array.each(data,function(record){
            if(record.get('leaf')){
                var index = store.findExact('InteractionField',record.get(me.columnParamsField.InteractionField));
                dataArray.push(record.get(me.columnParamsField.InteractionField));
                
                if(index === -1){//新建不存在的对象
                    var rec = ('Ext.data.Model',{
                        DisplayName:record.get('text'),
                        InteractionField:record.get(me.columnParamsField.InteractionField),
                        
                        IsLocked:false,//默认锁定
                        IsHidden:false,//默认隐藏
                        CannotSee:false,//不可见
                        CanSort:false,//可排序
                        DefaultSort:false,//默认排序
                        SortType:'ASC',//排序方式
                        OrderNum:1,//排布顺序
                        IsLocked:false,//默认锁定
                        AlignType:'left',//对齐方式
                        Width:100//数据项宽度
                    });
                    store.add(rec);
                }
            }
        });
        
        //删除没有被勾选的列
        var bo = false;
        var arrayToRemove = [];//需要被删除的列
        store.each(function(record){
            if(record && record.get('InteractionField') != null && record.get('InteractionField') != ""){
                bo = false;
                for(var i in dataArray){
                    if(record.get('InteractionField') === dataArray[i]){
                        bo = true; break;
                    }
                }
                if(!bo){
                    arrayToRemove.push(record);
                }
            }
        });
        for(var i in arrayToRemove){
            store.remove(arrayToRemove[i]);
        }
        var num = 0;
        store.each(function(record){
            num++;
            record.set('OrderNum',num);
            record.commit();
        });
        me.browse();//展示效果
        };
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
                
                "if (me.linkageType ==false) {"+ //假树
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
    
  
    /**
     * 创建功能栏按钮组
     * @private
     * @return {}
     */
    ctreatetoolsBarStr:function(){
        var dockedItems='';

        dockedItems=dockedItems+
        "function(){" +
        //过滤和面板功能
        "var me=this;"+
        "var filterBarArr= [];"+//功能栏按钮组一:收缩所有,展开所有,刷新按钮
        "var tt='';"+
        "var filtrationArr = [];"+//过滤栏按钮组
        "var buttonBarArr  = [];";//功能栏按钮组二:确定,取消按钮
          
          //过滤栏按钮组
          dockedItems=dockedItems+
          "if(me.isFiltration==true){"+
             "filtrationArr.push(me.createtoolsAddFilter());"+
          "}"+
          
          //功能栏按钮组一
          "if(me.isFunction==true){"+
	           "if(me.isreFreshBtn==true){"+
	                "filterBarArr.push(me.createtoolsreFreshBtn());"+
	            "}"+
	           "if(me.isPlusBtn==true){"+
	                "filterBarArr.push(me.createtoolsPlusBtn());"+
	           "}"+
	           "if(me.isMinusBtn==true){"+
	               "filterBarArr.push(me.createtoolsMinusBtn());"+
	            "}"+
	            "if(me.istoolsAddBtn==true){"+//新增,修改,查看,删除(需要添加开关控制)
                   "filterBarArr.push(me.createtoolsAddBtn());"+
	           "}"+
	           "if(me.istoolsShowBtn==true){"+
	              "filterBarArr.push(me.createtoolsShowBtn());"+
	           "}"+
	           "if(me.istoolsEditBtn==true){"+
	               "filterBarArr.push(me.createtoolsEditBtn());"+
	           "}"+
	           "if(me.istoolsDelBtn==true){"+
	               "filterBarArr.push(me.createtoolsDelBtn());"+
	           "}"+
           "}";
           
           //功能栏按钮组二:确定,取消按钮
           dockedItems=dockedItems+
           "if(me.isbuttonBar==true){"+
	           "if(me.isConfirmBtn==true){"+
	               "buttonBarArr.push(me.createisConfirmBtn);"+
	           "}"+
	           "if(me.isCancelBtn==true){"+
	               "buttonBarArr.push(me.createisCancelBtn());"+
	           "}"+
           "}";
           
           //各按钮组停放位置
            //当过滤栏的停靠位置和功能栏按钮组一,二位置相同(同时为顶部或底部时)
            dockedItems=dockedItems+
           
            "if((me.filterBar===me.functionBar)&&(me.filterBar===me.positionBar)){"+
            
            "var tempArr=filtrationArr.concat(buttonBarArr);"+
            "tempArr=tempArr.concat(filterBarArr);"+
            "tt=[{"+
                 "xtype:'toolbar',"+
                 "itemId:'treeToolbar',"+
                 "dock:me.filterBar,"+
                 "items:tempArr"+
                 "}];"+
            "}"+
            //当过滤栏的停靠位置和功能栏按钮组一停靠位置不相同时
             "else {"+
                "var tempArrOne=[];"+
                "var positionOne='';"+ 
                "var tempArrTwo=[];"+
                "var positionTwo='';"+ 
		         
                //当过滤栏的停靠位置和功能栏按钮组二相同    
	             "if(me.filterBar===me.positionBar){"+
	                "tempArrOne=buttonBarArr.concat(filterBarArr);"+
                    "positionOne=me.filterBar;"+ 
                    
                    "tempArrTwo=filtrationArr;"+//功能栏按钮组一
                    "positionTwo=me.functionBar;"+ //功能栏按钮组一的停靠位置
	              "}"+
                  
                  //当过滤栏的停靠位置和功能栏按钮组一相同    
                 "else if(me.filterBar===me.functionBar){"+
                    "tempArrOne=filterBarArr.concat(filtrationArr);"+
                    "positionOne=me.filterBar;"+ 
                    
                    "tempArrTwo=buttonBarArr;"+//功能栏按钮组二
                    "positionTwo=me.positionBar;"+ //功能栏按钮组二的停靠位置
                  "}"+
                  
                  //当功能栏按钮组一停靠位置和功能栏按钮组二相同
                  "else if(me.functionBar===me.positionBar){"+
                    "tempArrOne=buttonBarArr.concat(filtrationArr);"+
                    "positionOne=me.functionBar;"+ 
                    
                    "tempArrTwo=filterBarArr;"+//过滤栏
                    "positionTwo=me.filterBar;"+ //过滤栏的停靠位置
                  "}"+
                  
                  
                   "if(tempArrOne.length>0&&tempArrTwo.length>0){"+
                    "tt=[{"+//合并组
	                   "xtype:'toolbar',"+
	                   "itemId:'treeToolbar',"+
	                   "dock:positionOne,"+
	                   "items:tempArrOne"+
                    "},{"+//功能栏按钮组
                       "xtype:'toolbar',"+
                       "itemId:'treeToolbarTwo',"+
                       "dock:positionTwo,"+
                       "items:tempArrTwo"+
                       "}];"+
                "}"+
                "else if(tempArrOne.length>0&&tempArrTwo.length==0){"+
                    "tt=[{"+//合并组一
                    "xtype:'toolbar',"+
                    "itemId:'treeToolbar',"+
                    "dock:positionOne,"+
                    "items:tempArrOne"+
                    "}];"+
                "}else if(tempArrOne.length==0&&tempArrTwo.length>0){"+
                    "tt=[{"+//功能栏合并组一
                       "xtype:'toolbar',"+
                       "itemId:'treeToolbar',"+
                       "dock:positionTwo,"+
                       "items:tempArrTwo"+
                       "}];"+
                "}"+
                "else{"+
                "tt='';"+
                "}"+

            "}"+
            
            //过滤树和功能栏都不开启勾选时
          "if(me.isFiltration==false && me.isFunction==false&& me.isbuttonBar==false){"+
            " tt='';"+
          "}"+
          
            "return tt;"+
            "}";
       return dockedItems
    },
    
    /**
     * 根据ID获取一条应用信息
     * @private
     * @param {} id
     * @param {} callback
     */
    getInfoByIdFormServerStr:function(id,callback){
        var com="";
        com=com+
        "function(id,callback){" +
        "var me = this;" +
        "var url = me.getAppInfoServerUrl+'?isPlanish=true&id='+id;" +
        "Ext.Ajax.defaultPostHeader = 'application/json';" +
        "Ext.Ajax.request({" +
           " async:false," +//非异步
            "url:url," +
            "method:'GET'," +
            "timeout:2000," +
            "success:function(response,opts){" +
                "var result = Ext.JSON.decode(response.responseText);" +
                "if(result.success){" +
                   " var appInfo = '';" +
                    "if(result.ResultDataValue && result.ResultDataValue != ''){" +
                       " appInfo = Ext.JSON.decode(result.ResultDataValue);" +
                    "}" +
                    "if(Ext.typeOf(callback) == 'function'){" +
                       "callback(appInfo);" +//回调函数
                    "}" +
                "}else{" +
                    "Ext.Msg.alert('提示','获取应用信息失败！');" +
               " }" +
            "}," +
           " failure:function(response,options){ " +
               " Ext.Msg.alert('提示','获取应用信息请求失败！');" +
            "}" +
        "});" +
        "}";
        return com;
    },
    /**
     * 打开应用效果窗口
     * @private
     * @param {} title
     * @param {} ClassCode
     */
    openAppShowWinStr:function(){
        var com="";
        com=com+
	    "function(title,classCode,node,type){" +
	        "var me = this;"+
	        "var panel = eval(classCode);"+
	        "var maxHeight = document.body.clientHeight*0.98;"+
	        "var maxWidth = document.body.clientWidth*0.98;"+
	        "var win=Ext.create(panel,{"+
	            "maxWidth:maxWidth,"+
                "type:type," + 
	            "maxHeight:maxHeight,"+
	            "autoScroll:true,"+
	            "model:true,"+//模态
	            "floating:true,"+//漂浮
	           " closable:true,"+//有关闭按钮
	            "draggable:true"+//可移动
	        "}).show();"+
            //如果树节点存在
        "if(win&&node&&node!=undefined&&node!=null){"+
            "var id=node.data.Id;"+
           " win.load(id);"+//加载表单数据
           
            "var objectName=win.objectName;"+
            //构建表单里父节点Id组件,由表单的数据对象和后缀'_ParentID'组成的itemId
            "var ParentID=win.getComponent(''+objectName+'_ParentID');"+
            "if(ParentID){"+
                "var parentNode=node.parentNode;"+//取当前节点的父节点
                "var value='0';"+
                "var text='';"+
                "if(parentNode){"+
                    "var parentID=parentNode.data.Id;"+//父节点的Id
                    "if(parentID==''||parentID==null){"+
                        "value=0;"+
                        "text=parentNode.data.text;"+
                    "}else{"+
                        "value=parentID;"+
                        "text=parentNode.data.text;"+
                    "}"+
                "}else{"+
                    "value=='0';"+
                    "text=parentNode.data.text;"+
                "}"+
                //父节点Id组件赋值
                "var arrTemp=[[value,text]];"+
                "ParentID.store=Ext.create('Ext.data.SimpleStore',{"+  
                    "fields:['value','text'], "+
                    "data:arrTemp,"+
                    "autoLoad:true"+
                "});"+
                "ParentID.setValue(value);"+
            "}"+
            
        "}"+
        "}";
        return com;
    },

      /***
       * 功能栏/操作列的新增按钮事件
       */
      createAddBtnClickStr:function(){
        var com="";
        com=
        "function(){" +
         "var me=this;"+
         "var appComID=me.addAppComID;"+//新增表单的元应用的应用id
         //处理代码
            "var callback = function(appInfo){"+
                //中文名称
                "var title = me.addAppComCName;"+//新增表单的元应用的应用名称;
                //类代码
                "var ClassCode = '';"+
                "if(appInfo && appInfo != ''){"+
                    "ClassCode = appInfo[me.ClassCode];"+
                "}"+
                
                "if(ClassCode && ClassCode != ''){"+
                    //打开应用效果窗口
                    "var records=me.getSelectionModel().getSelection();"+
                    "var node=null;"+
                    "if(records&&records.length>0){"+
                        "node=records[0];"+
                    "}else{"+
                        "node=null;"+
                    "}"+
                    "me.openAppShowWin(title,ClassCode,node,'add');"+
               " }else{"+
                   " Ext.Msg.alert('提示','没有类代码！');"+
                "}"+
                
            "};"+
            
            //与后台交互
            "me.getInfoByIdFormServer(appComID,callback);"+
           
            "}" ;
           return com;
      },
    
      /***
       * 功能栏/操作列的修改按钮事件
       */
      createEditBtnClickStr:function(){
        var com="";
        com=
        "function(){" +
         "var me=this;"+
         "var appComID=me.editAppComID;"+//新增表单的元应用的应用id
         //处理代码
            "var callback = function(appInfo){"+
                //中文名称
                "var title = me.editAppComCName;"+//新增表单的元应用的应用名称;
                //类代码
                "var ClassCode = '';"+
                "if(appInfo && appInfo != ''){"+
                    "ClassCode = appInfo[me.ClassCode];"+
                "}"+
                
                "if(ClassCode && ClassCode != ''){"+
                    //打开应用效果窗口
                    "var records=me.getSelectionModel().getSelection();"+
                    "var node=null;"+
                    "if(records&&records.length>0){"+
                        "node=records[0];"+
                    "}else{"+
                        "node=null;"+
                    "}"+
                    "me.openAppShowWin(title,ClassCode,node,'edit');"+
               " }else{"+
                   " Ext.Msg.alert('提示','没有类代码！');"+
                "}"+
                
            "};"+
            
            //与后台交互
            "me.getInfoByIdFormServer(appComID,callback);"+
           
            "}" ;
           return com;
      },
    
      /***
       * 功能栏/操作列的查看按钮事件
       */
      createShowBtnClickStr:function(){
        var com="";
        com=
        "function(){" +
         "var me=this;"+
         "var appComID=me.showAppComID;"+//新增表单的元应用的应用id
         //处理代码
            "var callback = function(appInfo){"+
                //中文名称
                "var title = me.showAppComCName;"+//新增表单的元应用的应用名称;
                //类代码
                "var ClassCode = '';"+
                "if(appInfo && appInfo != ''){"+
                    "ClassCode = appInfo[me.ClassCode];"+
                "}"+
                
                "if(ClassCode && ClassCode != ''){"+
                    //打开应用效果窗口
                    "var records=me.getSelectionModel().getSelection();"+
                    "var node=null;"+
                    "if(records&&records.length>0){"+
                        "node=records[0];"+
                    "}else{"+
                        "node=null;"+
                    "}"+
                    "me.openAppShowWin(title,ClassCode,node,'show');"+
               " }else{"+
                   " Ext.Msg.alert('提示','没有类代码！');"+
                "}"+
                
            "};"+
            
            //与后台交互
            "me.getInfoByIdFormServer(appComID,callback);"+
           
            "}" ;
           return com;
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
          "var arrTemp =myTree.getSelectionModel().getSelection();"+
          
          "return arrTemp;"+
          
         "}"
            return fun;
      },
    /***
     * 创建表格树的列集合
     * 列集合分为主列(第一列),展示列,操作列
     * @param {} componentItemId
     * @return {}
     */
    createColumnsStr:function(){
        var me = this;
        var showcolumn="";
         showcolumn="function (){" +
            "var me=this;" +
            "return me.columnsStr" +
         "}";
        return showcolumn;
    },
    /**
     * 创建类代码
     * @private
     * @return {}
     */
    createAppClass:function(){
       var me = this;
       var appClass='';
        //表单配置参数
       var params = me.getPanelParams();

       var getDataServerUrl =me.getDataServerUrl();
       var selectServerUrl="";
       if(getDataServerUrl.getValue()==null){
            Ext.Msg.alert('提示','请配置获取数据');
            appClass='';
            return appClass;
       }else{
           selectServerUrl=""+getDataServerUrl.getValue().split('?')[0]; 
       }  
       
       var delServerUrl =me.getdelDataServerUrl();
       var deleteServerUrl="";
       if(delServerUrl.getValue()==null){
            deleteServerUrl='';
            //Ext.Msg.alert('提示','请配置删除数据');
            //appClass='';
            //return appClass;
       }else{
           deleteServerUrl=''+delServerUrl.getValue().split('?')[0]; 
       } 
       
       var editDataServerUrl =me.geteditDataServerUrl();
       if(editDataServerUrl.getValue()==null){
           editDataServerUrl='';
           //return;
       }else{
           editDataServerUrl=''+editDataServerUrl.getValue().split('?')[0]; 
       }
       var rootName='';//根节点名称
       //根节点名称
       rootName=me.getrootNameValue();
        var width=658;//
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
        
        drag=me.getdrogtypeValue()
        var isTure2=false;//是否级联
        
       isTure2=me.getisTuretypeValue();
       var title2='';
       var isSetTitle=false;//取标题栏设置是否显示的值
       isSetTitle=me.getisShowTitleValue();
       
       //获取标题名称的值
       title2=me.gettitleNameValue();
       if(isSetTitle==false){
            title2='';
       }
       
       var isShowFilterTree=false;//过滤栏开关
       isShowFilterTree=me.getisShowFilterTreeValue();
       var myFilterBar= 'top';//取过滤栏停放位置的值
       myFilterBar=me.getfilterPositionValue();
       
       var isFunction=false;//取是否显示功能栏按钮组(一)
       isFunction=me.getisShowToolsBtnsOneValue();
       var myFunctionBar='top';//取功能栏功能栏按钮组(一)停放位置的值
       myFunctionBar=me.gettoolsBtnsOnePositionValue();
       
       var istoolsAddBtn=false;//取功能栏按钮组(一)新增按钮的值
       istoolsAddBtn=me.getistoolsAddBtnValue();
       var istoolsShowBtn=false;//取功能栏按钮组(一)查看按钮的值
       istoolsShowBtn= me.getistoolsShowBtnValue();
       var istoolsEditBtn=false;//取功能栏按钮组(一)修改按钮的值
       istoolsEditBtn=me.getistoolsEditBtnValue();
       var istoolsDelBtn=false;////取功能栏按钮组(一)删除按钮的值
       istoolsDelBtn=me.getistoolsDelBtnValue();
      
       var isreFreshBtn=false;//取功能栏按钮组一:刷新按钮的值
       isreFreshBtn=me.getisreFreshBtnValue();
       var isMinusBtn=false;////取功能栏按钮组一:展开全部的值
       isMinusBtn=me.getisMinusBtnValue();
       var isPlusBtn=false;//取功能栏按钮组一:收缩全部的值
       isPlusBtn=me.getisPlusBtnValue();
       
       var isbuttonBar=false;//取是否显示功能栏按钮组(二)的値
       isbuttonBar=me.getisShowToolsBtnsTwoValue();
       var mypositionBar='top'; //功能栏按钮组二的停靠位置
       mypositionBar=me.gettoolsBtnsTwoPositionValue();
       
       var isCancelBtn=false;//功能栏取消按钮开关
       isCancelBtn=me.getIsCancelBtnValue();
       var isConfirmBtn=false;//功能栏确定按钮开关
       isConfirmBtn=me.getIsConfirmBtnValue();

       var hasBeenDeleted=false;//
       var isMenu=false;//
       isMenu=me.getisShowMenuValue();
       var isDelMenuBtn=false;//取是否显示右键菜单删除按钮的值
       isDelMenuBtn=me.getisDelMenuBtnValue();
       
       //获取是否显示一般列的值
	   var isShowGeneralColumn=me.getgeneralColumnValue();

	   //获取是否显示操作列的值
	   var isShowAction=me.getactionBarValue();
       
        //获取操作列的新增按钮的选择值
	   var isAddBtn=me.getoperateAddValue();
		//获取操作列的修改按钮的选择值
	   var isEditBtn=me. getoperateEditValue();
        //获取操作列的查看按钮的选择值
       var isShowBtn=me.getoperateShowValue();
        //获取操作列的删除按钮的选择值
       var isDelBtn=me.getoperateDelValue();
        
       var isShowDeleteAlag=me.getdelFlagTypeValue();

        //############################必须的代码#################################
        

       var parentnodeStr=me.parentnodeStr();
       var getValue=me.getValueStr();
       var mycreateStore=me.createStoreStr(rootName);//创建加载数据源

       var changeStoreData=me.changeStoreDataStr();
       var afterRenderStr=me.afterRenderStr();
   
        //获取应用列表服务地址
       var getAppListServerUrl="ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL";
       //上传图片文件服务地址
       var updateFileServerUrl="ConstructionService.svc/ReceiveModuleIconService";
       //############################必须的代码#################################
       
       //过滤
       var filterByTextStr=me.filterByTextStr();
       var filterByStr=me.filterByStr();
       var clearFilterStr=me.clearFilterStr();
       var chd=me.createChdStr();//遍历子结点 选中 与取消选中操作
       var setNode=me.createSetNodeStr();//真树     
       var setNodefalse=me.createSetNodefalseStr();//假树
       var linkageType=false;//真,假树默认值设置
       if(isTure2==true){
            linkageType=true;
       }else{
            linkageType=false;
       }
       var checkchange=me.createListenersCheckchangeStr();//真,假树判断
       var nodep=me.createNodepStr();//向上遍历父节点
       var newAddformChoose=me.getnewAddformChoose();
       var appComID=me.getappComID();

       var vConfig="";//是否允许拖拽
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
        //表格树的数据列
        var strTemp="";
        strTemp=Ext.encode( me.createColumns());
        var columnsStr =strTemp.replace(/"/g,"'");
        //去掉原数组中的中括号
        var columnsStrT =columnsStr.substring(1,columnsStr.length-1);
        
        //############################必须代码###########################
        var whereFields=me.getWhereFields();
        appClass =  
            "Ext.define('" + params.appCode + "',{" + 
                "extend:'Ext.tree.Panel'," + 
                "alias:'widget." + params.appCode + "'," + 
                "title:'" + title2 + "'," + 
                "selectId:null," + //默认选中节点ID(外部调用时传入)
                "hideNodeId:null," + //默认隐藏ID，隐藏该节点及所有子孙节点(外部调用时传入)
                "columnsStr:[" + columnsStrT + "]," + 
                
                "getAppInfoServerUrl:getRootPath()+"+"'/'+"+"'"+"ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById"+"',"+
                "getAppListServerUrl:getRootPath()+"+"'/'+"+"'"+getAppListServerUrl+"',"+
                "updateFileServerUrl:getRootPath()+"+"'/'+"+"'"+updateFileServerUrl+"',"+
                "selectServerUrl:getRootPath()+"+"'/'+"+"'"+selectServerUrl+"',"+  //树的查询数据地址
                "editDataServerUrl:getRootPath()+"+"'/'+"+"'"+editDataServerUrl+"',"+  //树的修改数据地址
                "whereFields:'"+whereFields+"',"+
                
                "autoScroll:true," +
                "filterfield:'text'," +//过滤字段
                "childrenField:'Tree',"+
                
                "linkageType:" + linkageType+ "," + //真假树的处理,true为真树,false为假树
                "chd:"+chd+"," + //遍历子结点 选中 与取消选中操作
                "setNodefalse:"+setNodefalse+"," + //假树 
                "nodep:"+ nodep+"," +//向上遍历父节点方法
                "setNode:"+setNode+"," + // 真树判断 
                "viewConfig: " + vConfig + "," + 
                "getValue:"+ getValue+"," +
                "parentnode:"+ parentnodeStr+"," +

                //树的hql功能后台未提供实现
                "internalWhere:''," + //内部hql
                "externalWhere:''," + //外部hql
                
                "afterRender:"+afterRenderStr+"," ;
                //树节点移动事件处理
                if(drag==true){
                    appClass =appClass+"updateNode:"+me.updateNodeStr()+"," ;//树和列表树的节点改变方法
                }
                //被调用的新增表单的应用id和名称
                 appClass =appClass+"ClassCode:'BTDAppComponents_ClassCode'," +//类代码字段
                 
                "width:" + width + "," +//树宽
                "height:" + height + "," + 
                "lines:" + lines2 + "," +
                "useArrows:" + useArrows2 + "," + 
                
                "isTure:" + isTure2 + "," + 
                "rootVisible:" + rootVisible2 + "," + 
                "checked:" + checked2 + "," + 
                
                "filterBar:'" + myFilterBar+ "'," +//显示过滤栏停放位置
                "functionBar:'" + myFunctionBar+ "'," +//功能栏功能栏按钮组(一)停放位置
                "positionBar:'" + mypositionBar+ "'," +//功能栏功能栏按钮组(二)停放位置
                
                "isShowAction:" + isShowAction+ "," +//是否显示操作列开关
                "isShowCeneral:" + isShowGeneralColumn+ ","+ //是否显示表格树一般列集合开关
                "isShowDeleteAlag:" + isShowDeleteAlag+ ","+ //是否显示表格树删除标记列开关
             
                //功能栏各按钮开关
                "isFiltration:" + isShowFilterTree+ "," +//是否显示过滤栏开关
                "isFunction:" + isFunction+ "," +//是否显示功能栏功能栏按钮组(一)开关
                "isbuttonBar:" + isbuttonBar+ "," +//是否显示功能栏功能栏按钮组(二)开关
                
                "isMinusBtn:" + isMinusBtn+ "," +//展开全部按钮开关
                "isPlusBtn:" + isPlusBtn+ "," +//收缩全部按钮开关
                "isreFreshBtn:" + isreFreshBtn+ "," +//刷新按钮开关
                
                "istoolsDelBtn:" + istoolsDelBtn+ "," +//功能栏的删除按钮开关
                "istoolsEditBtn:" + istoolsEditBtn+ "," +//功能栏的编辑按钮开关
                "istoolsShowBtn:" + istoolsShowBtn+ "," +//功能栏的查看按钮开关
                "istoolsAddBtn:" + istoolsAddBtn+ "," +//功能栏的新增按钮开关
                
                 //操作列按钮开关
                "isShowBtn:" + isShowBtn+ "," +//列表树查看操作列开关
                "isAddBtn:" + isAddBtn+ "," + //列表树新增操作列开关
                "isEditBtn:" + isEditBtn+ "," + //列表树修改操作列开关
                "isDelBtn:" + isDelBtn+ "," + //列表树删除操作列开关
                
               // 确定取消开关
                "isConfirmBtn:" + isConfirmBtn+ "," +//确定开关
                "isCancelBtn:" + isCancelBtn+ "," + //取消开关
                
                //右键菜单
                "isMenu:" + isMenu+ "," +//右键菜单开关
                "isDelMenuBtn:" + isDelMenuBtn+ "," +//右键菜单删除开关
                
                "createStore:"+ mycreateStore+"," +//创建数据源:数据加载方法
                "changeStoreData:"+changeStoreData+"," +//数据适配方法
                "createTreeColumns:"+ me.createColumnsStr()+"," +//列表树列信息
                
                "getInfoByIdFormServer:"+ me.getInfoByIdFormServerStr()+"," +//
                "openAppShowWin:"+ me.openAppShowWinStr()+",";
                
        //############################必须代码(结束)###############################
                
        //功能栏按钮组的处理  
        if(isShowFilterTree==true||isFunction==true||isbuttonBar==true){
           appClass=appClass+
                "ctreatetoolsBar:"+me.ctreatetoolsBarStr()+",";//
        }
        
        //################################右键菜单模块处理代码###################### 
        if(isMenu==true){ 
            appClass=appClass+
                "createContextmenu:" + me.createContextmenuStr()+ ","; //右键菜单
            if(isDelMenuBtn==true){ 
                appClass=appClass+
                    "createContextmenuDeletebtn:" + me.createContextmenuDeletebtnStr()+ ",";//右键菜单删除按钮
            }
        }
        //################################右键菜单模块处理代码(结束)################        
                
                
        //##############################删除模块处理代码############################
        
        //操作列的删除按钮或者功能栏的删除按钮,右键菜单删除开关单击事件,
        if(isDelBtn==true||istoolsDelBtn==true||isDelMenuBtn==true){       
            appClass=appClass+
                "deleteServerUrl:getRootPath()+"+"'/"+deleteServerUrl + "',"+ //树的删除操作数据地址
                "deleteModuleServer:"+me.deleteModuleServerStr()+"," +//从数据库中删除记录方法
                "deleteModule:"+me.deleteModuleStr()+"," ;//删除模块方法
        }
                
       //################################操作列/功能栏的相关按钮事件处理代码######## 
        
        //操作列的新增按钮或者功能栏的新增按钮单击事件       
        if(isAddBtn==true||istoolsAddBtn==true){
            appClass=appClass+
                "addAppComID:'" + appComID.getValue()+ "'," +//列表树绑定的新增表单的元应用id
                "addAppComCName:'" + newAddformChoose.getValue()+ "'," +//列表树绑定的新增表单的元应用名称
                "createAddBtnClick:"+ me.createAddBtnClickStr()+"," ;
        }
        
        //操作列的新增按钮或者功能栏的新增按钮单击事件
        if(isEditBtn==true||istoolsEditBtn==true){
            appClass=appClass+ 
                "editAppComID:'" + appComID.getValue()+ "'," +
                "editAppComCName:'" + newAddformChoose.getValue()+ "'," +
                "createEditBtnClick:"+ me.createEditBtnClickStr()+",";
        }
        
        //操作列的查看按钮或者功能栏的查看按钮单击事件
        if(isShowBtn==true||istoolsShowBtn==true){
            appClass=appClass+  
                "showAppComID:'" + appComID.getValue()+ "'," +
                "showAppComCName:'" + newAddformChoose.getValue()+ "'," +
                "createShowBtnClick:"+ me.createShowBtnClickStr()+",";//   
        }
        //################################操作列/功能栏的相关按钮事件处理代码(结束)##########
        
        
        //################################操作列按钮组控制处理代码####################  
        if(isShowAction==true){
            appClass=appClass+
            "setActionColumn:"+ me.createActionColumnStr()+",";//操作列按钮组控制
            if(isDelBtn==true){
                appClass=appClass+
                "createdelBtn:"+ me.createdelBtnStr()+"," ;//操作列删除按钮创建
            }
            if(isAddBtn==true){
                appClass=appClass+
                "createaddBtn:"+ me.createaddBtnStr()+"," ;//操作列新增按钮创建
            }
            if(isEditBtn==true){
                appClass=appClass+
                "createeditBtn:"+ me.createeditBtnStr()+",";//操作列编辑按钮创建
            }
            if(isShowBtn==true){
                appClass=appClass+
                "createshowBtn:"+ me.createshowBtnStr()+"," ;//操作列查看按钮创建
            }
         }
        //################################操作列按钮组控制处理代码(结束)####################  
         
         
         //################################过滤栏控制处理代码##############################
        if(isShowFilterTree==true){
           appClass=appClass+
                "createtoolsAddFilter:"+ me.createtoolsAddFilterStr()+",";//功能栏按钮组的过滤栏创建
           appClass=appClass+
                "filterByText:"+filterByTextStr+"," +//过滤
                "filterBy:"+filterByStr+"," +//过滤
                "clearFilter:"+clearFilterStr+","; //过滤
        }
        
        //功能栏按钮组二:确定,取消按钮
        if(isbuttonBar==true){  
           if(isConfirmBtn==true){
               appClass=appClass+
                "createisConfirmBtn:"+ me.createisConfirmBtnStr()+"," ;//确定
           }
           if(isCancelBtn==true){
               appClass=appClass+
                "createisCancelBtn:"+me.createisCancelBtnStr()+",";//取消
           }
         }
        //################################过滤栏控制处理代码(结束)#################### 
         
        
        //################################功能栏按钮组(一)控制 #################### 

        if(isFunction==true){//功能栏按钮组一开关为true
           if(isreFreshBtn==true){
               appClass=appClass+
                "createtoolsreFreshBtn:"+ me.refreshBtnStr()+"," ;//功能栏按钮组的刷新
            }
           if(isPlusBtn==true){
               appClass=appClass+
                "createtoolsPlusBtn:"+ me.plusBtnStr()+"," ;//功能栏按钮组的收缩
           }
           if(isMinusBtn==true){
              appClass=appClass+
                "createtoolsMinusBtn:"+ me.minusBtnStr()+"," ;//功能栏按钮组的展开
           }     
        //   新增,修改,查看,删除
         if(istoolsAddBtn==true){
             appClass=appClass+
                "createtoolsAddBtn:"+ me.createistoolsAddBtnStr()+",";////功能栏按钮组的新增按钮创建
          }
          if(istoolsShowBtn==true){
             appClass=appClass+
                "createtoolsShowBtn:"+ me.createistoolsShowBtnStr()+",";//功能栏按钮组的查看按钮创建
          }
          if(istoolsEditBtn==true){
             appClass=appClass+
                "createtoolsEditBtn:"+ me.createistoolsEditBtnStr()+",";//功能栏按钮组的编辑创建
          }
          if(istoolsDelBtn==true){
             appClass=appClass+
                "createtoolsDelBtn:"+ me.createistoolsDelBtnStr()+"," ;//功能栏按钮组的删除按钮创建
          }
          
         }
         //################################功能栏按钮组(一)控制(结束)#################### 
         
       appClass=appClass+
         "initComponent:function(){" + 
            "var me=this;" +
            "me.columns=me.createTreeColumns();"+
            "me.addEvents('okClick');"+//确定
            "me.addEvents('cancelClick');"; //取消
            
       //功能栏按钮组的处理  
        if(isShowFilterTree==true||isFunction==true||isbuttonBar==true){  
             //创建挂靠栏
	         appClass=appClass+"me.dockedItems=me.ctreatetoolsBar();";
        }else{
	         appClass=appClass+"me.dockedItems='';";
        }
                        
        //操作列的新增按钮或者功能栏的新增按钮单击事件       
        if(isAddBtn==true||istoolsAddBtn==true){
            appClass=appClass+"me.addEvents('addClick');";
        }
        
        //操作列的新增按钮或者功能栏的新增按钮单击事件
        if(isEditBtn==true||istoolsEditBtn==true){
            appClass=appClass+"me.addEvents('editClick');";
        }
        
        //操作列的查看按钮或者功能栏的查看按钮单击事件
        if(isShowBtn==true||istoolsShowBtn==true){
            appClass=appClass+"me.addEvents('showClick');";//   
        }
        
        //操作列的删除按钮或者功能栏的删除按钮,右键菜单删除开关单击事件,
        if(isDelBtn==true||istoolsDelBtn==true||isDelMenuBtn==true){                 
            appClass=appClass+"me.addEvents('delClick');me.addEvents('delafterClick');";//操作列删除
        }
       
        
       appClass=appClass+ "me.listeners=me.listeners||[];"; 
        //真假树事件监听
       appClass=appClass+ "me.listeners.checkchange="+checkchange+";";
        
        //右键菜单事件处理
        if(isMenu==true){ 
	         appClass=appClass+ "me.listeners.contextmenu={"+
                 "element:'el',"+
                 "fn:function(e,t,eOpts){"+
                     //禁用浏览器的右键相应事件 
                     "e.preventDefault();e.stopEvent();"+
                     //右键菜单
                     "new Ext.menu.Menu({"+
                         "items:me.createContextmenu()"+
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
                    "var node2={Id:node3.Id.toString(),ParentID:newParent3.Id.toString()};" +
                    "var boolResult=me.updateNode('edit',node2);"+
                    //"me.load('');"+
                "};";
                
              }
         
        //加载数据的方法
       appClass=appClass+  "me.store=me.createStore();"+
        "me.load=function(whereStr){" + 
            "var w='?fields='+me.whereFields;" + 
            
            "var myUrl=me.selectServerUrl+w;"+ 
            "me.store.proxy.url=myUrl;" + 
            "me.store.load();" + 
        "};" + 
        
         "this.callParent(arguments);" + 
	    "}" + 
	"});";
   
        return appClass;
    },
    /**
     * 创建数据集
     * @private
     * @return {}
     */
    createStoreStr:function(root){
        //服务地址
        var me=this;

        //数据字段
       var fieldsArr = me.createFields();
       var fields = "";
       fields=Ext.encode(fieldsArr);
       fields=fields.replace(/"/g,"'");
       var fun="";
       var whereFields=me.getWhereFields();
        fun="function(where){"+
        "var me = this;"+
        "var w='?fields="+whereFields+"';" + 
            
            "var myUrl=me.selectServerUrl+w;"+
            "var store = Ext.create('Ext.data.TreeStore',{"+
                //"fields:[" + fields + "]," + 
                "fields:" + fields + "," + 
                "proxy:{"+
                    "type:'ajax',"+
                     "url:myUrl," + 
                    "extractResponseData:function(response){return me.changeStoreData(response);}"+
                "},"+
                "defaultRootProperty:me.childrenField,"+
                "root:{"+
                    "text:'"+root+"',"+
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
                       "treeToolbar=me.getComponent('treeToolbarTwo');"+
                    "}"+
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
            "}"
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
	                "record.set('hasBeenDeleted','true');"+
		    		"record.commit();"+
//                    "callback();"+
                "}else{"+
	                "record.set('hasBeenDeleted','false');"+
		    		"record.commit();"+
                   " Ext.Msg.alert('提示','删除信息失败！');"+
                "}"+
            "},"+
            "failure:function(response,options){ "+
	            "record.set('hasBeenDeleted','false');"+
	    		"record.commit();"+
                "Ext.Msg.alert('提示','连接删除服务出错！');"+
            "}"+
        "});"+
       "}"
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
        fun="function(){"+
        "var me = this;"+
    	"var records = me.getSelectionModel().getSelection();"+
    	"if(records.length > 0){"+
    		"Ext.Msg.confirm('警告','确定要删除吗？',function (button){"+
    			"if(button == 'yes'){"+
    				"Ext.Array.each(records,function(record){"+
		    			//没有被删除的才去后台删除
                        "var id = record.get('Id');"+
			    		"if(record.get('me.hasBeenDeleted') != 'true'){"+
			    			"me.deleteModuleServer(id,record);"+
			    		"}"+
		    		"});"+
                    "me.load('');" +
                    "me.getRootNode().expand();" +
                    "me.fireEvent('delafterClick');" +//删除后的事件操作
    			"}"+
    		"});"+
           // "me.load('');" +
    	"}else{"+
    		"Ext.Msg.alert('提示','请选择需要删除的行记录！');"+
    	"}"+
       "}"
         return fun;
    },
    /***
     * 过滤
     * @return {}
     */
    createtoolsAddFilterStr:function(){
        var addBtn="";
         addBtn="function (){" +
            "var me=this;" +
            "var filter = {  "+  
                "xtype: 'textfield',fieldLabel: '检索过滤', itemId:'filterText',"+
                "labelAlign:'right',labelWidth:65,enableKeyEvents:true,"+   
                "listeners: { "+  
                    "keyup: {"+                   
                        "fn: function (field, e) { "+                     
                             "if (Ext.EventObject.ESC == e.getKey()) {"+
                                 "this.setValue('');"+          
                                 "me.clearFilter(); "+                    
                             "}else { "+
                                 "me.filterByText(this.getRawValue());"+                   
                             "} "+                  
                        "}"+               
                  " }"+
                "}"+ 
           "};"+
           "return filter;" +
         "}";
         return addBtn;
    },
     /**
     * 列表树功能栏新增
     * @return {}
     */
    createistoolsAddBtnStr:function(){
        var addBtn="";
         addBtn="function (node){" +
            "var me=this;" +
            "addBtn={" +
               "iconCls:'build-button-add hand',tooltip:'新增',itemId:'istoolsAddBtn'," +
               "handler:function(grid,rowIndex,colIndex,item,e,record){" +
                   "me.fireEvent('addClick');" +
	               "if (me.addAppComID!='' && me.addAppComCName!='')"+
	               "{" +
		               "me.createAddBtnClick();" +
	               "}"+
	           "}" +
           
            "};" +
           "return addBtn;" +
         "}";
         return addBtn;
    },
    /***
     * 列表树功能栏修改按钮
     * @return {}
     */
    createistoolsEditBtnStr:function(){
        var editBtn="";
         editBtn="function (node){" +
            "var me=this;" +
           "editBtn={" +
               "iconCls:'build-button-edit hand',tooltip:'修改信息',itemId:'istoolsEditBtn'," +
               "handler:function(grid,rowIndex,colIndex,item,e,record){" +
                    "me.fireEvent('editClick');" +
	               "if (me.addAppComID!='' && me.addAppComCName!='')"+
	               "{" +
		               "me.createEditBtnClick();" +
	               "}" +
	           "}" +
               
           "};" +
          "return editBtn;" +
         "}";
         return editBtn;
    },

    /***
     * 列表树删除操作按钮
     * @return {}
     */
    createistoolsDelBtnStr:function(){
        var delBtn="";
         delBtn="function (node){" +
            "var me=this;" +
            "delBtn={" +
                "iconCls:'build-button-delete hand',tooltip:'删除',itemId:'istoolsDelBtn'," +
                "handler:function(btn,e,optes){" +
                  "me.fireEvent('delClick');" +
                    "var record=me.getSelectionModel().getSelection();" +
                    "if(record!=null){" +
                        "me.deleteModule();" +
                        //"me.load('');" +
                        //"me.getRootNode().expand();" +
                    "}" +
                    
                "}" +
            "};" +
           "return delBtn;" +
         "}";
         return delBtn;
    },
    /***
     * 列表树查看操作列
     * @return {}
     */
    createistoolsShowBtnStr:function(){
        var showBtn="";
         showBtn="function (node){" +
            "var me=this;" +
            "showBtn={" +
                 "iconCls:'build-button-see hand',tooltip:'查看',itemId:'istoolsShowBtn'," +
                 "handler:function(grid,rowIndex,colIndex,item,e,record){" +
                    "me.fireEvent('showClick');" +
	                 "if (me.addAppComID!='' && me.addAppComCName!='')"+
	                 "{" +
		                 "me.createShowBtnClick();" +
	                 "}" +
	             "}" +
            "};" +
           "return showBtn" +
         "}";
         return showBtn;
    },   
     /***
     * 列表树操作列新增按钮
     * @return {}
     */
    createaddBtnStr:function(){
        var addBtn="";
         addBtn="function (node){" +
            "var me=this;" +
           "addBtn={" +
               "iconCls:'build-button-add hand',tooltip:'新增'," +
               "handler:function(grid,rowIndex,colIndex,item,e,record){" +
                    "me.fireEvent('addClick');" +
                   "if (me.addAppComID!='' && me.addAppComCName!='')"+
                   "{" +
	                   "me.createAddBtnClick();" +
                   "}"+
               "}" +
            "};" +
           "return addBtn;" +
         "}";
         return addBtn;
    },
    
    /***
     * 列表树修改操作列
     * @return {}
     */
    createeditBtnStr:function(){
        var editBtn="";
         editBtn="function (node){" +
            "var me=this;" +
           "editBtn={" +
               "iconCls:'build-button-edit hand',tooltip:'修改信息'," +
               "handler:function(grid,rowIndex,colIndex,item,e,record){" +
                    "me.fireEvent('editClick');" +
                   "if (me.editAppComID!='' && me.editAppComCName!='')"+
                   "{" +
	                   "me.createEditBtnClick();" +
                   "}"+
               "}" +
           "};" +
          "return editBtn;" +
         "}";
         return editBtn;
    },
    
    /***
     * 列表树删除操作列
     * @return {}
     */
    createdelBtnStr:function(){
        var delBtn="";
         delBtn="function (node){" +
            "var me=this;" +
            "delBtn={" +
                "iconCls:'build-button-delete hand',tooltip:'删除'," +
                "handler:function(grid,rowIndex,colIndex,item,e,record){" +
                    "me.fireEvent('delClick');" +
                   " me.deleteModule();" +
                    //"me.load('');" +
                "}" +
            "};" +
           "return delBtn;" +
         "}";
         return delBtn;
    },
    /***
     * 列表树查看操作列
     * @return {}
     */
    createshowBtnStr:function(){
        var showBtn="";
         showBtn="function (node){" +
            "var me=this;" +
            "showBtn={" +
                 "iconCls:'build-button-see hand',tooltip:'查看'," +
                 "handler:function(grid,rowIndex,colIndex,item,e,record){" +
                     "me.fireEvent('showClick');" +
                     "if (me.showAppComID!='' && me.showAppComCName!='')"+
                     "{" +
	                     "me.createShowBtnClick();" +
                     "}"+ 
                 "}" +
            "};" +
           "return showBtn" +
         "}";
         return showBtn;
    },
    /***
     * 是否显示表格树操作列
     * @return {}
     */
    createActionColumnStr:function(){
        var setActionColumn="";
         setActionColumn="function (node){" +
            "var me=this;" +
            "var itemsArr=[];" +
            "if(me.isAddBtn==true){" +
               "itemsArr.push(me.createaddBtn());" +
            "}" +
            "if(me.isEditBtn==true){" +
               "itemsArr.push(me.createeditBtn());" +
            "}" +
            "if(me.isShowBtn==true){" +
               "itemsArr.push(me.createshowBtn());" +
            "}" +
            "if(me.isDelBtn==true){" +
               "itemsArr.push(me.createdelBtn());" +
            "}" +
            "var actionColumn={xtype:'actioncolumn',text:'操作列',width:100,align:'center', itemId:'Action',items:itemsArr};" +
               "return actionColumn;" +
         "}";
         return setActionColumn;
    },
    

    /**
     * 列表树功能栏确定
     * @return {}
     */
    createisConfirmBtnStr:function(){
        var confirmBtn="";
        confirmBtn=confirmBtn+
        	"function (){" +
            "var me=this;" +
           "var confirmBtn={"+
	           "xtype:'button',text:'确定',tooltip:'确定'," +
	           "iconCls:'build-button-save'," +
	           "handler:function(){" +
	        	   "me.fireEvent('okClick');" +
	            "}" +
           "};" +
          "return confirmBtn;" +
         "}";
         return confirmBtn;
    },
      /**
       * 列表树功能栏取消
       * @return {}
       */
      //面板功能展开全部节点
      createisCancelBtnStr:function(){
          var  cancelBtn="";
          cancelBtn=cancelBtn+
          "function () {" +
          "var me=this;"+
          "var cancelBtn={" +
	    	  "xtype:'button',text:'确定',tooltip:'取消'," +
	          "iconCls:'build-button-refresh'," +
	          "handler:function(){" +
	    	     "me.fireEvent('cancelClick');" +
	          "}" +
          "};"+
          "return cancelBtn;" +
         "}";
          return cancelBtn;
      },

    /***
     * 创建表格树的列集合
     * 列集合分为主列(第一列),展示列,操作列
     * @param {} componentItemId
     * @return {}
     */
    Str:function(){
        var me = this;
        var showcolumn="";
         showcolumn="function (){" +
            "var me=this;" +
            "return me.columnsStr" +
         "}";
        return showcolumn;
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
     * 数据适配Str
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreDataStr:function(){
        var fun="";
        fun="function(response){"+
        "var me = this;"+
        "var data = Ext.JSON.decode(response.responseText);"+
        "var ResultDataValue = [];"+
        "if(data.ResultDataValue && data.ResultDataValue != ''){"+
            "ResultDataValue = Ext.JSON.decode(data.ResultDataValue);"+
        "}"+
        "data[me.childrenField] = ResultDataValue.Tree;"+
        
        "var changeNode = function(node){"+
        
            "var value = node['value'];"+
            "if(value&&value!=null&&value!=''){"+
	            "if(value.Id == me.hideNodeId){"+//需要剔除的节点
	                "return true;"+
	            "}"+
	            
            "}"+
            "for(var i in value){"+
                    "node[i] = value[i];"+
                "}"+
            //时间处理
            "node['DataAddTime'] = getMillisecondsFromStr(node['DataAddTime']);"+
            "node['DataUpdateTime'] = getMillisecondsFromStr(node['DataUpdateTime']);"+
            //图片地址处理
            "if(node['icon'] && node['icon'] != ''){"+
                "node['icon'] = getIconRootPathBySize(16) + '/' + node['icon'];"+
            "}"+
            
            "var children = node[me.childrenField];"+
            "if(children){"+
                "changeChildren(children);"+
            "}"+
            "return false;"+
        "};"+
        
//        var changeChildren = function(children){
//            Ext.Array.each(children,changeNode);
//        };
        
        "var changeChildren = function(children){"+
            "for(var i=0;i<children.length;i++){"+
                "var bo = changeNode(children[i]);"+
                "if(bo){"+
                    "children.splice(i,1);"+
                    "i--;"+
                "}"+
            "}"+
        "};"+
        
        "var children = data[me.childrenField];"+
        "changeChildren(children);"+
        
        "response.responseText = Ext.JSON.encode(data);"+
        "return response;"+
    
    "}";
    return fun;
    },
    /***
     * 
     * @return {}
     */
    afterRenderStr:function(){
        var afterRender="";
        afterRender="function(){" + 
				"var me=this;" + 
				"me.callParent(arguments);" +
				"if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
         "}";
         return afterRender;
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
//		                    "if(Ext.typeOf(callback) == 'function'){" +
//		                       " callback();" +//回调函数
//		                    "}"+
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
                var editDataServerUrl2=editDataServerUrl.split('?')[0];
                url = getRootPath()+'/'+editDataServerUrl2;//修改节点服务 editDataServerUr
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
            params:Ext.JSON.encode({'entity':node,'fields':'Id,ParentID'}),//,DataTimeStamp,Owner
            method:'POST',
            timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
//                    if(Ext.typeOf(callback) == 'function'){
//                        //callback();//回调函数
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
            if(value&&value!=null&&value!=''){//需要剔除的节点
                if(value.Id == me.hideNodeId){
                    return true;
                }
                
            }
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
            return false;
        };
        
//        var changeChildren = function(children){
//            Ext.Array.each(children,changeNode);
//        };
        
        var changeChildren = function(children){
            for(var i=0;i<children.length;i++){
                var bo = changeNode(children[i]);
                if(bo){
                    children.splice(i,1);
                    i--;
                }
            }
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
    createStore1:function(myUrl,rootName){
        var me = this;
        //数据字段
        var myFields = me.createFields();
        var whereFields=me.getWhereFields();
        //HQL串
        var w='?fields='+whereFields;
        var myUrlTemp = myUrl+w;
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
                text:rootName,
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
                    var tree=me.getCenterTreeCom();
                    if(tree&&tree!=undefined){
		                var treeToolbar=tree.getComponent('treeToolbar');
	                    if(treeToolbar==undefined||treeToolbar==null){//在按钮组一找不到时
	                        treeToolbar=me.getComponent('treeToolbarTwo');
	                    }
	                    if(treeToolbar&&treeToolbar!=undefined){
			                var refresh=treeToolbar.getComponent('refresh');
		                    if(refresh&&refresh!=undefined){
		                        //数据获取成功后,刷新按钮可用
		                        refresh.disabled=false;
		                    }
	                    }
                    }
                }
            }
            
        });
        return store;
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
                xtype:'textfield',fieldLabel:'根节点名称',name:'rootName',labelWidth:95,anchor:'100%',itemId:'rootName'
            },{
                xtype:'numberfield',fieldLabel:'树宽',labelWidth:95,anchor:'100%',
                itemId:'treeWidth',name:'treeWidth',
                listeners:{
                }  
            },{
                xtype:'numberfield',fieldLabel:'树高',labelWidth:95,
                anchor:'100%',
                itemId:'treeHeight',name:'treeHeight',
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
                itemId:'Checkedtype',
                labelWidth:95,
                fieldLabel:'是否带复选框',
                columns:2,
                vertical:true,
                 items:[
                    {boxLabel:'是',name:'Checkedtype',inputValue:'true'},
                    {boxLabel:'否',name:'Checkedtype',inputValue:'false'}
                ]
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
     * 检索过滤
     * @return {}
     */
    createfilter:function(){
        var me=this;
        var filter={    
                xtype: 'textfield',
                fieldLabel: '检索过滤', 
                itemId:'filter', 
                labelAlign:'right',
                labelWidth:65,
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
           };
        return filter;
    },
    
    /**
     * 创建列表树
     * @param {} store1
     * @param {} columns
     * @return {}
     */
    createTableTree:function(){
       var me = this;
       var treeCom =null;
       var params = me.getListParams();
       var objectName=me.getobjectName();
       
       var getDataServerUrl =me.getDataServerUrl();//树的查询数据地址
       var selectServerUrl="";
       
       if(getDataServerUrl.getValue()==null){
            Ext.Msg.alert('提示','请配置获取数据');
            return treeCom;
       }else{
           selectServerUrl=""+getDataServerUrl.getValue().split('?')[0]; 
       }
       var rootName='';//根节点名称
       //根节点名称
       rootName=me.getrootNameValue();
       
       var myUrl2 =""+getRootPath()+"/"+selectServerUrl;
       var store1=me.createStore1(myUrl2,rootName);

       var width=658;//
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
        drag=me.getdrogtypeValue()
        
        var isTure2=false;//是否级联
       isTure2=me.getisTuretypeValue();
       var title2='';
       var isSetTitle=false;//取标题栏设置是否显示的值
       isSetTitle=me.getisShowTitleValue();
       
       //获取标题名称的值
       title2=me.gettitleNameValue();
       if(isSetTitle==false){
            title2='';
       }
       
       var isShowFilterTree=false;//过滤栏开关
       isShowFilterTree=me.getisShowFilterTreeValue();
       var myFilterBar= 'top';//取过滤栏停放位置的值
       myFilterBar=me.getfilterPositionValue();
       
       var isFunction=false;//取是否显示功能栏按钮组(一)
       isFunction=me.getisShowToolsBtnsOneValue();
       var myFunctionBar='top';//取功能栏功能栏按钮组(一)停放位置的值
       myFunctionBar=me.gettoolsBtnsOnePositionValue();
       
       var istoolsAddBtn=false;//取功能栏按钮组(一)新增按钮的值
       istoolsAddBtn=me.getistoolsAddBtnValue();
       var istoolsShowBtn=false;//取功能栏按钮组(一)查看按钮的值
       istoolsShowBtn= me.getistoolsShowBtnValue();
       var istoolsEditBtn=false;//取功能栏按钮组(一)修改按钮的值
       istoolsEditBtn=me.getistoolsEditBtnValue();
       var istoolsDelBtn=false;////取功能栏按钮组(一)删除按钮的值
	   istoolsDelBtn=me.getistoolsDelBtnValue();
      
       var isreFreshBtn=false;//取功能栏按钮组一:刷新按钮的值
       isreFreshBtn=me.getisreFreshBtnValue();
       var isMinusBtn=false;////取功能栏按钮组一:展开全部的值
       isMinusBtn=me.getisMinusBtnValue();
       var isPlusBtn=false;//取功能栏按钮组一:收缩全部的值
       isPlusBtn=me.getisPlusBtnValue();
       
       var isbuttonBar=false;//取是否显示功能栏按钮组(二)的値
       isbuttonBar=me.getisShowToolsBtnsTwoValue();
       var mypositionBar='top'; //功能栏按钮组二的停靠位置
       mypositionBar=me.gettoolsBtnsTwoPositionValue();
       
       var isCancelBtn=false;//功能栏取消按钮开关
       isCancelBtn=me.getIsCancelBtnValue();
       var isConfirmBtn=false;//功能栏确定按钮开关
       isConfirmBtn=me.getIsConfirmBtnValue();

       var hasBeenDeleted=false;//
       var isMenu=false;//
       isMenu=me.getisShowMenuValue();
       var isDelMenuBtn=false;//取是否显示右键菜单删除按钮的值
       isDelMenuBtn=me.getisDelMenuBtnValue();
        
        //过滤和面板功能
        var filtrationArr = [];//功能栏按钮组一:收缩所有,展开所有,刷新按钮
        var tt='';
        var filterBarArr = [];//过滤栏按钮组
        var buttonBarArr  = [];//功能栏按钮组二:确定,取消按钮
        
        //过滤树和功能栏都不勾选时
        if(isShowFilterTree!=true && isFunction!=true&&isbuttonBar!=true){
              tt='';
          }else{
        
        //过滤树
        if(isShowFilterTree==true){
            var item =me.createfilter();
           filterBarArr.push(item);
        }
       
        //功能栏按钮组一
        if(isFunction==true){
            //收缩,展开,刷新
           if(isreFreshBtn==true){
                var refreshBtn=me.refreshBtn();
                filtrationArr.push(refreshBtn);
            }
           if(isPlusBtn==true){
               var plusBtn=me.plusBtn();
               filtrationArr.push(plusBtn);
           }
           if(isMinusBtn==true){
               var minusBtn=me.minusBtn();
               filtrationArr.push(minusBtn);
           }     
        //   新增,修改,查看,删除
         if(istoolsAddBtn==true){
              var toolsAddBtn=me.istoolsAddBtn();
              filterBarArr.push(toolsAddBtn);
          }
          if(istoolsShowBtn==true){
              var ShowBtn=me.istoolsShowBtn();
              filterBarArr.push(ShowBtn);
          }
          if(istoolsEditBtn==true){
              var EditBtn=me.istoolsEditBtn();
              filterBarArr.push(EditBtn);
          }
          if(istoolsDelBtn==true){
              var DelBtn=me.istoolsDelBtn();
              filterBarArr.push(DelBtn);
          }
         }
         
         //功能栏按钮组二:确定,取消按钮
         if(isbuttonBar==true){  
           if(isConfirmBtn==true){
               buttonBarArr.push(me.isConfirmBtn());
           }
           if(isCancelBtn==true){
               buttonBarArr.push(me.isCancelBtn());
           }
         }
         
         //当过滤栏的停靠位置相同(顶部,底部)
        if((myFilterBar===mypositionBar)&&(myFunctionBar===mypositionBar)){
            
            var tempArr=filterBarArr.concat(buttonBarArr);
            tempArr=tempArr.concat(filtrationArr);
            
            tt=[{
                 xtype:'toolbar',
                 itemId:'treeToolbar',
                 dock:myFilterBar,
                 items:tempArr
                 }
            ];
        //当过滤栏的停靠位置和功能栏按钮组一停靠位置不相同时
        }else {
            var tempArrOne=[];
            var tempArrTwo=[];
            var positionOne=''; 
            var positionTwo=''; 
            
            //当过滤栏的停靠位置和功能栏按钮组一相同    
             if(myFilterBar===myFunctionBar){
                tempArrOne=filterBarArr.concat(filtrationArr);
                positionOne=myFilterBar;
                
                tempArrTwo=buttonBarArr;//功能栏按钮组二
                positionTwo=mypositionBar; //功能栏按钮组二的停靠位置
              }
              //当过滤栏的停靠位置和功能栏按钮组二相同    
             else if(myFilterBar===mypositionBar){
                tempArrOne=filterBarArr.concat(buttonBarArr);
                positionOne=myFilterBar;
                
                tempArrTwo=filtrationArr;//功能栏按钮组一
                positionTwo=myFunctionBar; //功能栏按钮组一的停靠位置
              }
              //当功能栏按钮组一停靠位置和功能栏按钮组二相同
              else if(myFunctionBar===mypositionBar){
                tempArrOne=buttonBarArr.concat(filtrationArr);
                positionOne=myFunctionBar;
                
                tempArrTwo=filterBarArr;//过滤栏
                positionTwo=myFilterBar; //过滤栏的停靠位置
              }
              
              if(tempArrOne.length>0&&tempArrTwo.length>0){
	                tt=[{//合并组
	                xtype:'toolbar',
                    itemId:'treeToolbar',
	                dock:positionOne,
	                items:tempArrOne
	                },{//功能栏按钮组
	                   xtype:'toolbar',
	                   dock:positionTwo,
                       itemId:'treeToolbarTwo',
	                   items:tempArrTwo
	                   }];
                }
                else if(tempArrOne.length>0&&tempArrTwo.length==0){
                    tt=[{//合并组一
                    xtype:'toolbar',
                    itemId:'treeToolbar',
                    dock:positionOne,
                    items:tempArrOne
                    }];
                }else if(tempArrOne.length==0&&tempArrTwo.length>0){
                    tt=[{//功能栏合并组一
                       xtype:'toolbar',
                       itemId:'treeToolbar',
                       dock:positionTwo,
                       items:tempArrTwo
                       }];
                }
                else{
                tt='';
                }

        }
        }

        var myViewConfig={
            plugins: {
                ptype:'treeviewdragdrop',
                allowLeafInserts:true,
                //allowParentInsert :true,
                listeners:{
	                drop:function(node, data, overModel, dropPosition,  eOpts ){
	                    
	                }
                }
            }
        };
        
        var Menu=[];
        if(isMenu==true){
        	if (isDelMenuBtn==true){
	        		Menu=[me.isMenuDelBtn()];
        	}else{
        		Menu=[{}];
        	}
        }else{
        	Menu='';
        }
       //列表配置参数
       var columns = me.createColumns();
       
        //表格树的列集合分为主列(第一列),展示列,操作列
       treeCom ={
           xtype:'treepanel',
           itemId:objectName.getValue(),
           store:store1,
           dockedItems:tt, 
           columns:columns,
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
               ,expanded:true //预防数据加载两次
           }
        };
        if(drag==true){
            treeCom.viewConfig=myViewConfig;
        }else{
            treeCom.viewConfig='';
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
                columnresize:function(ct,column,width,e,eOpts){//列宽度改变
                    var dataIndex = column.dataIndex;
                    me.setColumnWidth(dataIndex,width);
                },
                columnmove:function(ct,column,fromIdx,toIdx,eOpts){//列位置移动
 
                    var dataIndex = column.dataIndex;
                    me.setColumnOrderNum(dataIndex,fromIdx+1,toIdx+1);
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
                itemclick:function(com, record,  item,  index,  e,  eOpts ){
                    //var data=record;
                    //alert(record.get('ParentID'));
                },
                //节点移动完成后
                itemmove:function( nodeInterface, oldParent, newParent, index, eOpts ){
                    var node3=nodeInterface.data;
                    var newParent3=newParent.data;
                    var oldParent3=oldParent.data;
                    var node2={Id:""+node3.Id+"",ParentID:""+newParent3.Id+""};//,DataTimeStamp:dataTimeStamp
                    me.updateNode('edit',node2);
                    //treeCom.store.load();
                }
            };
            
        return treeCom;
    },
    /**
     * 设置标头宽度
     * @private
     * @param {} dataIndex
     * @param {} width
     */
    setColumnWidth:function(dataIndex,width){
        var me = this;
        var list = me.getComponent('south');//列属性列表
        var store = list.store;
        var objectName=me.getobjectName();
        if(objectName){
            dataIndex=objectName.getValue()+"_"+dataIndex;
        }
        var index = store.findExact('InteractionField',dataIndex);//是否存在这条记录
        if(index != -1){
            var item = store.getAt(index);
            item.set('Width',width);
            item.commit();
        }
    },
    /**
     * 设置列次序
     * @private
     * @param {} dataIndex
     * @param {} fromIdx
     * @param {} toIdx
     */
    setColumnOrderNum:function(dataIndex,fromIdx,toIdx){
        var me = this;
        var list = me.getComponent('south');//列属性列表
        var store = list.store;
        var index = store.findExact('InteractionField',dataIndex);//是否存在这条记录
        if(index != -1){
            var it;
            var ind;
            if(fromIdx < toIdx){
                ind = store.findExact('OrderNum',fromIdx);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',toIdx);
                    it.commit();
                }
                for(var i=fromIdx+1;i<toIdx;i++){
                    ind = store.findExact('OrderNum',i);
                    if(ind != -1){
                        it = store.getAt(ind);
                        it.set('OrderNum',i-1);
                        it.commit();
                    }
                }
                ind = store.findExact('OrderNum',toIdx);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',toIdx-1);
                    it.commit();
                }
            }else{
                ind = store.findExact('OrderNum',fromIdx);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',1111);
                    it.commit();
                }
                for(var i=fromIdx;i>toIdx;i--){
                    ind = store.findExact('OrderNum',i-1);
                    if(ind != -1){
                        it = store.getAt(ind);
                        it.set('OrderNum',i);
                        it.commit();
                    }
                }
                ind = store.findExact('OrderNum',1111);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',toIdx);
                    it.commit();
                }
            }
        }
    },
    
    /***
     * 创建表格树的列集合
     * 列集合分为主列(第一列),展示列,操作列
     * @param {} componentItemId
     * @return {}
     */
    createColumns:function(){
       var me = this;
       //表格树的列集合分为主列(第一列),展示列,操作列
       var isShowCeneral=false;//是否显示一般列
       isShowCeneral=me.getgeneralColumnValue();
       var isShowAction=false;//是否显示操作列
       isShowAction=me.getactionBarValue();
       
       var isShowDeleteAlag=false;//
       isShowDeleteAlag=me.getdelFlagTypeValue();
       
       var columns = [];
       columns.push(me.firstolumn);
        //列属性(已排序)
        var columnParams = me.getColumnParams();
        for(var i in columnParams){
            var cmConfig = {};
            cmConfig.text = columnParams[i].DisplayName;
            var showFile=columnParams[i].InteractionField.split('_');
            cmConfig.dataIndex =showFile[showFile.length-1];
            cmConfig.width = columnParams[i].Width;
            if(columnParams[i].IsLocked){
                cmConfig.locked = columnParams[i].IsLocked;
            }
            
            cmConfig.sortable = columnParams[i].CanSort;
            cmConfig.hidden = (columnParams[i].IsHidden || columnParams[i].CannotSee);
            cmConfig.hideable = !columnParams[i].CannotSee;
            cmConfig.align = columnParams[i].AlignType;
            if(columnParams[i].ColumnType == "bool"){
                cmConfig.xtype = "booleancolumn";
                cmConfig.trueText = "是";
                cmConfig.falseText = "否";   
            }
            
            columns.push(cmConfig);
        }
        //删除标记
        if(isShowDeleteAlag==true){
             columns.push(me.delcolumn);
        }
        //操作列
        if(isShowAction==true){
             columns.push(me.setActionColumn());
        }
        return columns;
    },
//===============================获取及设置参数====================== 
    /**
     * 获取构建表单面板配置参数集合
     * 如需要取表单类型的选择结果值时,就可以这样取
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getPanelParams:function(){
        var me = this;
        var panel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var params = panel.getForm().getValues();
        return params;
    },
    getotherParamsTitleSet:function(){
        var me = this;
        var panel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
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
            value =true
        }else{
            value =false
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
    /**
     * 过滤栏停放位置
     * @private
     * @return {}
     */
    getfilterPosition:function(){
        var me = this;
        var parkPosition =me.getfunctionBarSettings().getComponent('filterPosition');
        return parkPosition;
    },
    /**
     * 单选组:取过滤栏停放位置的值
     * @private
     * @return {}
     */
    getfilterPositionValue:function(){
        var me = this;
        var value =me.getfunctionBarSettings().getComponent('filterPosition').getValue().filterPosition;
        if(value=='undefined'){
            value='top';
        }
        return value;
    },
    /**
     * 功能栏功能栏按钮组(一)停放位置
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    gettoolsBtnsOnePosition:function(){
        var me = this;
        var nn = me.getfunctionBarSettings().getComponent('toolsBtnsOnePosition');
        return nn;
    },
    /**
     * 单选组:取功能栏功能栏按钮组(一)停放位置的值
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    gettoolsBtnsOnePositionValue:function(){
        var me = this;
        var value = me.getfunctionBarSettings().getComponent('toolsBtnsOnePosition').getValue().toolsBtnsOnePosition;
        if(value=='undefined'){
            value='top';
        }
        return value;
    },
    /**
     * 功能栏按钮组一(刷新,展开,收缩)组区域
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    gettoolsType:function(){
        var me = this;
        var toolsType =me.getfunctionBarSettings().getComponent('toolsType');
        return toolsType;
    },
    /***
     * 复选框:功能栏按钮组一:刷新按钮
     * @return {}
     */
    getisreFreshBtn:function(){
        var me = this;
        var isreFreshBtn =me.gettoolsType().getComponent('IsreFreshBtn');
        return isreFreshBtn;
    },
    /***
     * 复选框:取功能栏按钮组一:刷新按钮的值
     * @return {}
     */
    getisreFreshBtnValue:function(){
        var me = this;
        var value =me.gettoolsType().getComponent('IsreFreshBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 复选框:功能栏按钮组一:展开全部按钮
     * @return {}
     */
    getisMinusBtn:function(){
        var me = this;
        var isreFreshBtn =me.gettoolsType().getComponent('IsMinusBtn');
        return isreFreshBtn;
    },
    /***
     * 复选框:取功能栏按钮组一:展开全部的值
     * @return {}
     */
    getisMinusBtnValue:function(){
        var me = this;
        var value =me.gettoolsType().getComponent('IsMinusBtn').value
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 复选框:功能栏按钮组一:收缩全部按钮
     * @return {}
     */
    getisPlusBtn:function(){
        var me = this;
        var isPlusBtn =me.gettoolsType().getComponent('IsPlusBtn');
        return isPlusBtn;
    },
    /***
     * 复选框:取功能栏按钮组一:收缩全部的值
     * @return {}
     */
    getisPlusBtnValue:function(){
        var me = this;
        var value =me.gettoolsType().getComponent('IsPlusBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:功能栏按钮组(一)区域
     * @private
     * @return {}
     */
    gettoolsBtnsOne:function(){
        var me = this;
        var toolsBtnsOne = me.getfunctionBarSettings().getComponent('toolsBtnsOne');
        return toolsBtnsOne;
    },
    
    /***
     * 复选框:设置功能栏按钮组(一)新增,修改,查看,删除的按钮值
     * @param {} value
     */
    setToolsBtnsOneValue:function(value){
        var me=this;
        me.gettoolsBtnsOne().getComponent('IstoolsAddBtn').setValue(value);
        me.gettoolsBtnsOne().getComponent('IstoolsEditBtn').setValue(value);
        me.gettoolsBtnsOne().getComponent('IstoolsShowBtn').setValue(value);
        me.gettoolsBtnsOne().getComponent('IstoolsDelBtn').setValue(value);
    },
    /**
     * 复选框:功能栏按钮组(一)删除按钮
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getistoolsDelBtn:function(){
        var me=this;
        var istoolsEditBtn=me.gettoolsBtnsOne().getComponent('IstoolsDelBtn');
        return istoolsEditBtn;
    },
    /**
     * 复选框:取功能栏按钮组(一)删除按钮的值
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getistoolsDelBtnValue:function(){
        var me=this;
        var value=me.gettoolsBtnsOne().getComponent('IstoolsDelBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:功能栏按钮组(一)新增按钮
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getistoolsAddBtn:function(){
        var me=this;
        var istoolsAddBtn=me.gettoolsBtnsOne().getComponent('IstoolsAddBtn');
        return istoolsAddBtn;
    },
    /**
     * 复选框:取功能栏按钮组(一)新增按钮的值
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getistoolsAddBtnValue:function(){
        var me=this;
        var value=me.gettoolsBtnsOne().getComponent('IstoolsAddBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:功能栏按钮组(一)修改按钮
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getistoolsEditBtn:function(){
        var me=this;
        var istoolsEditBtn=me.gettoolsBtnsOne().getComponent('IstoolsEditBtn');
        return istoolsEditBtn;
    },
    /**
     * 复选框:取功能栏按钮组(一)修改按钮的值
     * @private
     * @return {}
     */
    getistoolsEditBtnValue:function(){
        var me=this;
        var value=me.gettoolsBtnsOne().getComponent('IstoolsEditBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:功能栏按钮组(一)修改按钮
     * @private
     * @return {}
     */
    getistoolsShowBtn:function(){
        var me=this;
        var istoolsShowBtn=me.gettoolsBtnsOne().getComponent('IstoolsShowBtn');
        return istoolsShowBtn;
    },
    /**
     * 复选框:取功能栏按钮组(一)查看按钮的值
     * @private
     * @return {}
     */
    getistoolsShowBtnValue:function(){
        var me=this;
        var value=me.gettoolsBtnsOne().getComponent('IstoolsShowBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    
    /**
     * 单选组:功能栏按钮组(二)停放位置
     * @private
     * @return {}
     */
    gettoolsBtnsTwoPosition:function(){
        var me = this;
        var toolsBtnsTwoPosition =me.getfunctionBarSettings().getComponent('toolsBtnsTwoPosition');
        return toolsBtnsTwoPosition;
    },
    /**
     * 单选组:取功能栏按钮组(二)停放位置的值
     * @private
     * @return {}
     */
    gettoolsBtnsTwoPositionValue:function(){
        var me = this;
        var value =me.getfunctionBarSettings().getComponent('toolsBtnsTwoPosition').getValue().toolsBtnsTwoPosition;
        if(value=='undefined'){
            value='top';
        }
        return value;
    },
    /**
     * 功能栏按钮组(二)区域
     * @private
     * @return {}
     */
    gettoolsBtnsTwo:function(){
        var me = this;
        var toolsBtnsTwo =me.getfunctionBarSettings().getComponent('toolsBtnsTwo');
        return toolsBtnsTwo;
    },
    /**
     * 复选框:功能栏按钮组(二)区域:确定按钮
     * @private
     * @return {}
     */
    getIsConfirmBtn:function(){
        var me = this;
        var isConfirmBtn =me.gettoolsBtnsTwo().getComponent('IsConfirmBtn');
        return isConfirmBtn;
    },
    /**
     * 复选框:取功能栏按钮组(二)区域:确定按钮的值
     * @private
     * @return {}
     */
    getIsConfirmBtnValue:function(){
        var me = this;
        var value =me.gettoolsBtnsTwo().getComponent('IsConfirmBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:功能栏按钮组(二)区域:取消按钮
     * @private
     * @return {}
     */
    getIsCancelBtn:function(){
        var me = this;
        var isConfirmBtn =me.gettoolsBtnsTwo().getComponent('IsCancelBtn');
        return isConfirmBtn;
    },
    /**
     * 复选框:取功能栏按钮组(二)区域:取消按钮的值
     * @private
     * @return {}
     */
    getIsCancelBtnValue:function(){
        var me = this;
        var value =me.gettoolsBtnsTwo().getComponent('IsCancelBtn').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },    
    /***
     * 复选框:是否显示功能栏按钮组(二)
     * @return {}
     */
    getisShowToolsBtnsTwo:function(){
        var me = this;
        var toolsBtnsTwo =me.getfunctionBarSettings().getComponent('isShowToolsBtnsTwo');
        return toolsBtnsTwo;
    },
    /***
     * 复选框:取是否显示功能栏按钮组(二)的値
     * @return {}
     */
    getisShowToolsBtnsTwoValue:function(){
        var me = this;
        var value =me.getfunctionBarSettings().getComponent('isShowToolsBtnsTwo').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:是否开启右键菜单
     * @private
     * @return {}
     */
    getisShowMenu:function(){
        var me = this;
        //属性面板ItemId
        var panel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var menu = panel.getComponent('menuItems').getComponent('isShowMenu');
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
        var panel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var value = panel.getComponent('menuItems').getComponent('isShowMenu').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 右键菜单复选框区域
     * @private
     * @return {}
     */
    getmenuDelBtn:function(){
        var panel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var menu = panel.getComponent('menuItems').getComponent('menuDelBtn');
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
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:是否显示功能栏按钮组(一)
     * @private
     * @return {}
     */
    getisShowToolsBtnsOne:function(){
        var tools = this.getfunctionBarSettings().getComponent('isShowToolsBtnsOne');
        return tools;
    },
    /**
     * 复选框:取是否显示功能栏按钮组(一)
     * @private
     * @return {}
     */
    getisShowToolsBtnsOneValue:function(){
        var value = this.getfunctionBarSettings().getComponent('isShowToolsBtnsOne').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 复选框:功能栏区域:是否开启过滤栏
     * @private
     * @return {}
     */
    getisShowFilterTree:function(){
        var isShowFilterTree =this.getfunctionBarSettings().getComponent('isShowFilterTree');
        return isShowFilterTree;
    },
    /**
     * 复选框:功能栏区域:取是否开启过滤栏的值
     * @private
     * @return {}
     */
    getisShowFilterTreeValue:function(){
        var value =this.getfunctionBarSettings().getComponent('isShowFilterTree').value;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 功能栏区域
     * @return {}
     */
    getfunctionBarSettings:function(){
        var panel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var functionBarSettings = panel.getComponent('functionBarSettings');
        return functionBarSettings;
    },
    /**
     * 获取数据对象控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getobjectName:function(){
        var dataObject =this.getdataObject();
        var objectName = dataObject.getComponent('objectName');
        return objectName;
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
    getobjectPropertyOK:function(){
        var dataObject = this.getdataObject();
        var objectPropertyOK = dataObject.getComponent('objectPropertyToolbar').getComponent('objectPropertyOK');
        return objectPropertyOK;
    },
     /**
     * 获取删除服务控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getdelDataServerUrl:function(){
        var dataObject = this.getdataObject();
        var delDataServerUrl = dataObject.getComponent('delDataServerUrl');
        return delDataServerUrl;
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
    /**
     * 获取对象树控件
     * @private
     * @return {}
     */
    getobjectPropertyTree:function(){
        var dataObject =this.getdataObject();
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree');
        return objectPropertyTree;
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
     * @private
     * @return {}
     */
    getoperateBtns:function(){
        var otherParams = this.getcolumnSettings();
        var operateType = otherParams.getComponent("operateBtns");
        return operateType;
    },

    /**
     * 操作列按钮组是否全部未选中
     * @private
     * @return {}
     */
    isActioncolumnAllNotChecked:function(){
        var ac=this.getcolumnSettings();
        var add =this.getoperateAddValue();
        var edit =this.getoperateEditValue()
        var show = this.getoperateShowValue();
        var del =this.getoperateDelValue();
        return !(add || edit || show || del);
    },
   /***
    * 操作列的新增按钮
    * @return {}
    */
   getoperateAdd:function(){
       var ac=this.getcolumnSettings();
       var add =this.getoperateBtns().getComponent('operateAdd');
       return add;
    },
   /***
    * 复选框:获取操作列的新增按钮的选择值
    * @return {}
    */
   getoperateAddValue:function(){
       var ac=this.getcolumnSettings();
       var value = this.getoperateBtns().getComponent('operateAdd').value;
       if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
   getoperateEdit:function(){
       var ac=this.getcolumnSettings();
       var add = this.getoperateBtns().getComponent('operateEdit');
       return add;
    },
   /***
    * 复选框:获取操作列的修改按钮的选择值
    * @return {}
    */
   getoperateEditValue:function(){
       var ac=this.getcolumnSettings();
       var value = this.getoperateBtns().getComponent('operateEdit').value;
       if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
   getoperateShow:function(){
       var ac=this.getcolumnSettings();
       var add = this.getoperateBtns().getComponent('operateShow');
       return add;
    },
   /***
    * 复选框:获取操作列的查看按钮的选择值
    * @return {}
    */
   getoperateShowValue:function(){
       var me = this;
       var ac=me.getcolumnSettings();
       var value = me.getoperateBtns().getComponent('operateShow').value;
       if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
   getoperateDel:function(){
       var me = this;
       var ac=me.getcolumnSettings();
       var add = me.getoperateBtns().getComponent('operateDel');
       return add;
    },
   /***
    * 复选框:获取操作列的删除按钮的选择值
    * @return {}
    */
   getoperateDelValue:function(){
       var me = this;
       var ac=me.getcolumnSettings();
       var value = me.getoperateBtns().getComponent('operateDel').value;
       if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 设置操作列按钮组状态
     * @private
     * @param {} checked
     */
    checkActioncolumnAll:function(checked){
        this.getoperateAdd().setValue(checked);
        this.getoperateEdit().setValue(checked);
        this.getoperateShow().setValue(checked);
        this.getoperateDel().setValue(checked);
    },
    /**
     * 获取操作列全选控件
     * @private
     * @return {}
     */
    getactioncolumnAll:function(){
        var all = this.getcolumnSettings();
        var actioncolumn = all.getComponent("actioncolumn-all");
        return actioncolumn;
    },
    /***
     * 复选框:是否显示一般列
     * @return {}
     */
    getgeneralColumn:function(){
        var all = this.getcolumnSettings();
        var generalColumn = all.getComponent("generalColumn");
        return generalColumn;
    },
    /***
     * 单选组:获取是否显示一般列的值
     * @return {}
     */
    getgeneralColumnValue:function(){
        var all = this.getcolumnSettings();
        var value = all.getComponent("generalColumn").getValue().generalColumn;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 单选组:是否显示操作列
     * @return {}
     */
    getactionBar:function(){
        var all = this.getcolumnSettings();
        var actionBar = all.getComponent("actionBar");
        return actionBar;
    },
    /***
     * 单选组:获取是否显示操作列的值
     * @return {}
     */
    getactionBarValue:function(){
        var all = this.getcolumnSettings();
        var value = all.getComponent("actionBar").getValue().actionBar;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 单选组:是否显示删除标记
     * @return {}
     */
    getdelFlagType:function(){
        var all = this.getcolumnSettings();
        var delFlagType = all.getComponent("delFlagType");
        return delFlagType;
    },
    /***
     * 单选组:获取是否显示删除标记的值
     * @return {}
     */
    getdelFlagTypeValue:function(){
        var all = this.getcolumnSettings();
        var value = all.getComponent("delFlagType").getValue().delFlagType;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 列设置
     * @return {}
     */
   getcolumnSettings:function(){
        var panel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var otherParams = panel.getComponent("ColumnSettings");
        return otherParams;
    }, 
    /**
     * 获取弹出新增表单控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getnewAddformChoose:function(){
        var paramsPanel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var newAddformChoose = paramsPanel.getComponent('other').getComponent('winformapp').getComponent('newAddformChoose');
        return newAddformChoose;
    },
    /**
     * 获取表单隐藏Id值
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getappComID:function(){
        var paramsPanel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var appComID = paramsPanel.getComponent('other').getComponent('winformapp').getComponent('appComID');
        return appComID;
    },
    /**
     * 获取弹出表单值
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    gethasformChoosek:function(){
        var paramsPanel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var hasformChoosek = paramsPanel.getComponent('other').getComponent('hasformChoosek');
        return hasformChoosek;
    },
    /**
     * 获取弹出表单值
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    gethasformChoosekValue:function(){
        var paramsPanel = this.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        var hasformChoosek = paramsPanel.getComponent('other').getComponent('hasformChoosek');
        var value=hasformChoosek.getValue();
        if(value==true||value=='true'||value=='1'||value=='on'){
           value==true;
        }else{
            value==false;
        }
        return value;
    },
    /**
     * 获取弹出新增表单控件
     * var params=getPanelParams();params.formType
     * @private
     * @return {}
     */
    getnewAddform:function(){
        var me = this;
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var newAddform = paramsPanel.getComponent('other').getComponent('newAddform');
        return newAddform;
    },
    /***
     * 基本属性的区域
     * @return {}
     */
   getbasicParams:function(){
        var me = this;
        var panel = me.getComponent('east').getComponent('center' + this.ParamsPanelItemIdSuffix);
        //组件属性面板
        var basic = panel.getComponent("basicParams");
        return basic;
    },
    /***
     * 基本属性的区域:根节点名称
     * @return {}
     */
    getrootName:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("rootName");
        return com;
    },
    /***
     * 基本属性的区域:根节点名称
     * @return {}
     */
    getrootNameValue:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("rootName").getValue();
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
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 单选组:是否带复选框
     * @return {}
     */
    getcheckedtype:function(){
        var basicParams = this.getbasicParams();
        var com = basicParams.getComponent("Checkedtype");
        return com;
    },
    /***
     * 单选组:获取是否带复选框的值
     * @return {}
     */
    getcheckedtypeValue:function(){
        var basicParams = this.getbasicParams();
        var value = basicParams.getComponent("Checkedtype").getValue().Checkedtype;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
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
            value =true
        }else{
            value =false
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
            value =true
        }else{
            value =false
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
            value =true
        }else{
            value =false
        }
        return value;
    },
    /**
     * 获取所有组件属性信息（简单对象数组）
     * @private
     * @return {}
     */
    getSouthRocordInfoArray:function(){
        var me = this;
        var records = me.getSouthRecords();
        var fields = me.getSouthStoreFields();
        var arr = [];
        //model转化成简单对象
        var getObjByRecord = function(record){
            var obj = {};
            Ext.Array.each(fields,function(field){
                obj[field.name] = record.get(field.name);
            });
            return obj;
        };
        //组装简单对象数组
        Ext.Array.each(records,function(record){
            var obj = getObjByRecord(record);
            arr.push(obj);
        });
        return arr;
    },

    /**
     * 添加组件属性记录
     * @private
     * @param {} record
     */
    addSouthValueByRecord:function(record){
        var me = this;
        var list = me.getComponent('south');//列属性列表
        var store = list.store;
        store.add(record);
    },
    /**
     * 移除组件属性记录
     * @private
     * @param {} record
     */
    removeSouthValueByRecord:function(record){
        var me = this;
        var list = me.getComponent('south');//列属性列表
        var store = list.store;
        store.remove(record);
    },
    /**
     * 根据键值对移除组件属性信息
     * @private
     * @param {} key
     * @param {} value
     */
    removeSouthValueByKeyValue:function(key,value){
        var me = this;
        var store = me.getComponent('south').store;
        var record = me.getSouthRecordByKeyValue(key,value);
        if(record){
            store.remove(record);
        }
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
     * 前台需要的字段
     * @private
     * @return {}
     */
    getFormFields:function(){
        var me =this;
        var southRecords = me.getSouthRecords();
        var fields = "";
        for(var i in southRecords){
            var record = southRecords[i];
            fields = fields + record.get('InteractionField') + ",";
        }
        if(fields.length > 0){
            fields = fields.substring(0,fields.length-1);
        }
        return fields;
    },
    /**
     * 获取所有组件属性信息
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
     * 获取查询字段集
     * @private
     * @return {}
     */
    getWhereFields:function(){
        var fields ='';
        var me = this;
        var list = me.getComponent('south');
        var store = list.getStore();
        var items=store.data.items
        Ext.Array.each(items,function(record){
            var field = record.get('InteractionField');
            fields=fields+field+',';
        });
        fields=fields.substring(0,fields.length-1);
        return fields;
    },
    /**
     * 获取列属性数据(已按列次序排序)
     * @private
     * @return {}
     */
    getColumnParams:function(){
        var myItems = [];
        var me = this;
        var list = me.getComponent('south');
        var items = list.store.data.items;
        
        var map = [];
        for(var i in items){
            var kv = {OrderNum:items[i].get('OrderNum'),Index:i};
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
            var record = {};
            var item = items[map[i].Index];
            
            record.InteractionField = item.get('InteractionField');
            record.DisplayName = item.get('DisplayName');
            record.IsLocked = item.get('IsLocked');
            record.IsHidden = item.get('IsHidden');
            record.CannotSee = item.get('CannotSee');
            record.CanSort = item.get('CanSort');
            record.DefaultSort = item.get('DefaultSort');
            record.SortType = item.get('SortType');
            
            record.OrderNum = item.get('OrderNum');
            record.Width = item.get('Width');
            record.AlignType = item.get('AlignType');
            myItems.push(record);
        }
        
        return myItems;
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
        var objectName=me.getobjectName();
        var center = me.getComponent('center').getComponent(objectName.getValue());
        return center;
    },
     /**
     * 根据键值对从应用组件属性列表中获取信息
     * @private
     * @param {} key
     * @param {} value
     * @return {} record or null
     */
    getSouthRecordByKeyValue:function(key,value){
        var me = this;
        var store = me.getComponent('south').store;
        var record = store.findRecord(key,value);
        return record;
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
     * 给组件属性列表赋值
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    setSouthRecordByKeyValue:function(InteractionField,key,value){
        var me = this;
        var grid = me.getComponent('south');
        var store = grid.store;
        var record = store.findRecord('InteractionField',InteractionField);
        if(record != null){//存在
            record.set(key,value);
            record.commit();
        }
    },

    /**
     * 给组件属性列表赋值
     * 当gridpanel组件是numberfield控件类型时才调用
     * 其他的调用调用setSouthRecordByKeyValue或setColumnParamsRecord
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    setSouthRecordForNumberfield:function(InteractionField,key,value){
        var me = this;
        var grid = me.getComponent('south');
        var store = grid.store;
        var record = store.findRecord('InteractionField',InteractionField);
        if(record != null){//存在
            record.set(key,value);
            record.commit();
            grid.getView().refresh();
        }
    },
    
    /**
     * 给组件记录列表赋值
     * @private
     * @param {} array
     */
    setSouthRecordByArray:function(array){
        var me = this;
        Ext.Array.each(array,function(obj){
            var rec = ('Ext.data.Model',obj);
            me.addSouthValueByRecord(rec);//添加组件记录
            
        });
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
      * 功能栏设置
      * @private
      * @param {} componentItemId
      * @return {}
      */
     createTreeToolsBarSet:function(){
         var me = this;
         var items = {
             xtype:'fieldset',title:'功能栏设置',padding:'0 5 0 5',collapsible:true,
             defaultType:'textfield',
             itemId:'functionBarSettings',
             name:'functionBarSettings',
             items:[
             {
                xtype:'checkbox',itemId:'isShowFilterTree',name:'isShowFilterTree',boxLabel:'开启过滤栏',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                        var parkPosition =me.getfilterPosition();
                         if (com.value==true){
                             parkPosition.show();
                         }else{
                             parkPosition.hide();
                        }
                    }
                }
            },{
                xtype:'radiogroup',
                itemId:'filterPosition',
                fieldLabel:'停放位置',
                labelWidth:80,
                columns:2,
                vertical:true,
                listeners:{
                },    
                items:[{
                    boxLabel:'顶部',name:'filterPosition',inputValue:'top'
                },{
                    boxLabel:'底部',name:'filterPosition',inputValue:'bottom'
                }]
            }, {
                xtype:'checkbox',itemId:'isShowToolsBtnsOne',name:'isShowToolsBtnsOne',boxLabel:'功能栏按钮组(一)',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                        var toolsBtnsOnePosition =me.gettoolsBtnsOnePosition();
                        var toolsBtnsOne =me.gettoolsBtnsOne();
                        var toolsType = me.gettoolsType();
                         if (com.getValue()==true){
                             toolsBtnsOnePosition.show();
                             toolsBtnsOne.show();
                             toolsType.show();
                         }else{
                             toolsBtnsOnePosition.hide();
                             toolsBtnsOne.hide();
                             toolsType.hide();
                         }
                    }
                  }
            },{
                xtype:'radiogroup',
                itemId:'toolsBtnsOnePosition',
                fieldLabel:'停放位置',
                labelWidth:80,
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[{
                    boxLabel:'顶部',name:'toolsBtnsOnePosition',inputValue:'top'
                },{
                    boxLabel:'底部',name:'toolsBtnsOnePosition',inputValue:'bottom'
                }]
            },
            {
                xtype: 'checkboxgroup',
                fieldLabel: '',
                columns: 2,
                itemId:'toolsBtnsOne',
                vertical: true,
                items: me.createtoolsBtnsOne()
            },
            {
                xtype: 'checkboxgroup',
                fieldLabel: '',
                columns: 2,
                itemId:'toolsType',
                vertical: true,
                items: me.createtoolsBtnOther()
            },
            {
                xtype:'checkbox',itemId:'isShowToolsBtnsTwo',name:'isShowToolsBtnsTwo',boxLabel:'功能栏按钮组(二)',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                         var savePosition =me.gettoolsBtnsTwoPosition();
                         var toolsBtnsTwo =me.gettoolsBtnsTwo();
                         if (com.value==true){
                             savePosition.show();
                             toolsBtnsTwo.show();
                         }else{
                             savePosition.hide();
                             toolsBtnsTwo.hide();
                         }
                    }
                }
            },{
                xtype:'radiogroup',
                itemId:'toolsBtnsTwoPosition',
                fieldLabel:'停放位置',
                labelWidth:80,
                hidden:true,
                columns:2,
                vertical:true,
                listeners:{
                },    
                items:[{
                    boxLabel:'顶部',name:'toolsBtnsTwoPosition',inputValue:'top'
                },{
                    boxLabel:'底部',name:'toolsBtnsTwoPosition',inputValue:'bottom'
                }]
            },{
                xtype: 'checkboxgroup',
                fieldLabel: '',
                hidden:true,
                columns: 2,
                itemId:'toolsBtnsTwo',
                vertical: true,
                items: me.createtoolsBtnsTwo()
            }
            ]
         };
         return items;
     },
     /**
      * 列表树列设置
      * @private
      * @param {} componentItemId
      * @return {}
      */
     createColumnSettings:function(){
         var me = this;
         var items = {
             xtype:'fieldset',title:'列设置',padding:'0 5 0 5',collapsible:true,
             defaultType:'textfield',
             itemId:'ColumnSettings',
             name:'ColumnSettings',
             items:[{
                xtype: 'radiogroup',
                itemId:'generalColumn',
                labelWidth:60,
                fieldLabel:'一般列',
                columns:2,
                vertical:true,
                listeners:{
  
                },
                items:[
                    {boxLabel:'显示',name:'generalColumn',inputValue:'true'},
                    {boxLabel:'隐藏',name:'generalColumn',inputValue:'false'}
                ]
            },{
                xtype: 'radiogroup',
                itemId:'delFlagType',
                labelWidth:60,
                fieldLabel:'提示列',
                columns:2,
                vertical:true,
                listeners:{
  
                },
                items:[
                    {boxLabel:'显示',name:'delFlagType',inputValue:'true'},
                    {boxLabel:'隐藏',name:'delFlagType',inputValue:'false'}
                ]
            },{
                 xtype: 'radiogroup',
                 itemId:'actionBar',
                 labelWidth:60,
                 fieldLabel:'操作列',
                 columns:2,
                 vertical:true,
                 listeners:{
                  change:function(com, newValue,oldValue,eOpts){
                         var value=newValue.actionBar;
                         var operateType=me.getoperateBtns();
                         if(value=='false'){
                             me.checkActioncolumnAll(false);
                             operateType.hide();
                             
                         }else{
                            operateType.show();
                         }
                     }
                 },
                 items:[
                     {boxLabel:'显示',name:'actionBar',inputValue:'true'},
                     {boxLabel:'隐藏',name:'actionBar',inputValue:'false'}
                 ]
             }, {
                xtype:'checkbox',boxLabel:'全选',hidden:true,
                itemId:'actioncolumn-all',
                listeners:{
                    change:function(field,newValue){
                        //操作列按钮组是否全部选中
                        var isActioncolumnAllChecked = me.isActioncolumnAllChecked();
                        //操作列按钮组是否全部未选中
                        var isActioncolumnAllNotChecked = me.isActioncolumnAllNotChecked();
                        //在全选中的状态下全不选、在全未选中的状态下全选
                        if((!newValue && isActioncolumnAllChecked) || (newValue && isActioncolumnAllNotChecked)){
                            me.checkActioncolumnAll(newValue);
                        }
                    }
                 }
            },
            {
                xtype: 'checkboxgroup',
                fieldLabel: '',
                columns: 2,
                itemId:'operateBtns',
                vertical: true,
                items:me.createoperateArr()
            }
            ]
         };
         return items;
     },
     
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
             items:[{
                 xtype:'checkbox',name:'isShowMenu',itemId:'isShowMenu',boxLabel:'开启右键菜单',
                 listeners:{
                     change:function(com, newValue,oldValue,eOpts){
		            	 var menuDelBtn=me.getmenuDelBtn();
		                 if (com.value==true){
		                	 menuDelBtn.show();
		                 }else{
		                	 menuDelBtn.hide();
		                 }
                     }
                 }
                
             },
             {
                 xtype: 'checkboxgroup',
                 fieldLabel: '',
                 columns: 2,
                 itemId:'menuDelBtn',
                 vertical: true,
                 items: me.createismenuArr()
             }
             ]
         };
         return items;
     },
     /***
      * 创建功能栏按钮组(二)
      * @return {}
      */
    createtoolsBtnsTwo:function(){
        var me=this;
        var istoolsArr=[
            { boxLabel: '确定', name:'toolsBtnsTwo',itemId:'IsConfirmBtn',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                    }
                }   
            },
            { boxLabel: '取消', name:'toolsBtnsTwo',itemId:'IsCancelBtn',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                    }
                 }  
            }
           ];
      return istoolsArr;
    },
    
     /***
      * 创建功能栏功能栏按钮组(一)
      * @return {}
      */
    createtoolsBtnsOne:function(){
        var me=this;
        var istoolsArr=[
            { boxLabel: '新增', name:'toolsBtnsOne',itemId:'IstoolsAddBtn',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                    }
                }   
            },
            { boxLabel: '修改', name:'toolsBtnsOne',itemId:'IstoolsEditBtn',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
  
                    }
                 }  
            },
            { boxLabel: '查看', name:'toolsBtnsOne',itemId:'IstoolsShowBtn',
                listeners:{
                change:function(com, newValue,oldValue,eOpts){
                  
                }
             }
            },
            { boxLabel: '删除',name:'toolsBtnsOne',itemId:'IstoolsDelBtn',
                listeners:{
                change:function(com, newValue,oldValue,eOpts){
                  
                }
             }  
            }
           ];
      return istoolsArr;
    },
    
    /***
     * 创建右键菜单按钮组
     * @return {}
     */
    createismenuArr:function(){
       var me=this;
       var istoolsArr=[
           {boxLabel: '删除',name:'menuDelBtn',itemId:'IsDelMenuBtn',
           listeners:{
	           change:function(com, newValue,oldValue,eOpts){
	               
	           }
           }  
         }
      ];
     return istoolsArr;
   },
   
     /***
      * 创建功能栏/操作列按钮组
      * @return {}
      */
    createoperateArr:function(){
        var me=this;
        var operateArr=[
            { boxLabel: '新增', name: 'operateBtns',itemId:'operateAdd',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                       
                      
                    }
                }   
            },
            { boxLabel: '修改', name: 'operateBtns',itemId:'operateEdit',
                listeners:{
                    change:function(com, newValue,oldValue,eOpts){
                        
                    }
                 }  
            },
            { boxLabel: '查看', name: 'operateBtns',itemId:'operateShow',
                listeners:{
                change:function(com, newValue,oldValue,eOpts){
                   
                }
             }
            },
            { boxLabel: '删除', name: 'operateBtns',itemId:'operateDel',
                listeners:{
                change:function(com, newValue,oldValue,eOpts){
                   
                }
             }  
            }
           ];
      return operateArr;
    },  
     /**
     * 属性面板功能栏设置赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setfunctionBarSettingsValues:function(){
        var me = this;
        var filter=me.getisShowFilterTree();  //是否开启过滤栏
        filter.setValue(false);
        
        var filterBar='top';//过滤栏位置
        //过滤栏位置设值
        var parkPosition=me.getfilterPosition();
        var isparkPosition="'"+filterBar+"'";
        var valuesparkPosition="{filterPosition:["+isparkPosition+"]}";
        var myisparkPositionJson=Ext.decode(valuesparkPosition);
        parkPosition.setValue(myisparkPositionJson);
        parkPosition.hide();
        
        var toolsBtnsOnePosition = me.gettoolsBtnsOnePosition();//功能栏按钮组(一)停放位置
        var isShowToolsBtnsOne =me.getisShowToolsBtnsOne();//是否显示功能栏按钮组(一)
        isShowToolsBtnsOne.setValue(false);
        //功能栏按钮组(一)位置设值
        var functionBar='top'; //功能栏位置
        var isnn="'"+functionBar+"'";
        var valuesnn="{toolsBtnsOnePosition:["+isnn+"]}";
        var mynnJson=Ext.decode(valuesnn);
        toolsBtnsOnePosition.setValue(mynnJson);
        
        me.setToolsBtnsOneValue(false);
        //功能栏按钮组(一)按钮隐藏设值
        var toolsBtnsOnePosition =me.gettoolsBtnsOnePosition();
        var toolsBtnsOne =me.gettoolsBtnsOne();//功能栏按钮(新,修,删除,查看)组
        var toolsType = me.gettoolsType();//功能栏按钮(刷新,展开,收缩)组
        toolsBtnsOnePosition.hide();
        toolsBtnsOne.hide();
        toolsType.hide();
        
        //功能栏按钮组(二)
        var isShowToolsBtnsTwo =me.getisShowToolsBtnsTwo();
        isShowToolsBtnsTwo.setValue(false);
        var toolsBtnsTwoPosition =me.gettoolsBtnsTwoPosition();
        var savePosition2='top'; //功能栏位置
        var isnn2="'"+savePosition2+"'";
        var valuesnn2="{toolsBtnsTwoPosition:["+isnn2+"]}";
        var mynnJson2=Ext.decode(valuesnn2);
        toolsBtnsTwoPosition.setValue(mynnJson2);

        var toolsBtnsTwo =me.gettoolsBtnsTwo();
        toolsBtnsTwoPosition.hide();
        toolsBtnsTwo.hide();
            
    },
    /**
     * 属性面板树的右键菜单赋初始值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setTreeMenuValues:function(){
        var me=this;
        var isShowMenu=me.getisShowMenu();
            isShowMenu.setValue(false);
        var menuDelBtn=me.getmenuDelBtn();
            menuDelBtn.hide();
    },
    /**
     * 属性面板基础数据赋初始值
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
        
        mytextwidth.setValue(658);
        mytextheight.setValue(280);
        
        var rootName="所有模块";
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
    /**
     * 列设置
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setColumnSettingsValues:function(){
        var me = this;
        var other =me.getcolumnSettings();
        
        var myShowCeneraltype=me.getgeneralColumn();//是否显示一般列
        var myShowActiontype = me.getactionBar();//是否显示操作列
        var myShowDeleteAlag = me.getdelFlagType();//是否显示删除标记
        var myactioncolumnAll =me.getactioncolumnAll();;
        
        var isShowCeneral=true;
        var valuesisShowCeneral="{generalColumn:["+isShowCeneral+"]}";
        var myisShowCeneralJson=Ext.decode(valuesisShowCeneral);
        myShowCeneraltype.setValue(myisShowCeneralJson);
        
        var isShowAction=false;
        var valuesisShowAction="{actionBar:["+isShowAction+"]}";
        var myisisShowActionJson=Ext.decode(valuesisShowAction);
        myShowActiontype.setValue(myisisShowActionJson);
        
        var isShowDeleteAlag=false;
        var valuesisShowDelete="{delFlagType:["+isShowDeleteAlag+"]}";
        var myShowDeleteAlagActionJson=Ext.decode(valuesisShowDelete);
        myShowDeleteAlag.setValue(myShowDeleteAlagActionJson);
        
        //操作列全部不选中
        me.checkActioncolumnAll(false);

    },
     //真树假树
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
	      var childs=n.childNodes;
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
	      var childs=n.childNodes;
	          for (var i = 0; i < childs.length; i++) {
	              childs[i].data.checked = true;
	              childs[i].updateInfo({ checked: true });
	              if(childs[i].data.leaf==false)
	              {
	                 me.setNode(childs[i]);
	              }
	          }
	   },
   
   //============================================
     /**
     * 根据ID获取一条应用信息
     * @private
     * @param {} id
     * @param {} callback
     */
    getInfoByIdFormServer:function(id,callback){
        var me = this;
        var url = me.getAppInfoServerUrl+'?isPlanish=true&id='+id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:2000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    var appInfo = "";
                    if(result.ResultDataValue && result.ResultDataValue != ""){
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if(Ext.typeOf(callback) == "function"){
                        callback(appInfo);//回调函数
                    }
                }else{
                    Ext.Msg.alert('提示','获取应用信息失败！');
                }
            },
            failure:function(response,options){ 
                Ext.Msg.alert('失败提示','获取应用信息请求失败！');
            }
        });
    },
    /**
     * 打开应用效果窗口
     * @private
     * @param {} title
     * @param {} ClassCode
     */
    openAppShowWin:function(title,classCode,node,type){
        var me = this;
        var panel = eval(classCode);
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
        var win=Ext.create(panel,{
            maxWidth:maxWidth,
            maxHeight:maxHeight,
            type:type,
            autoScroll:true,
            model:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true//可移动
        }).show();
        //如果树节点存在
        if(win&&node&&node!=undefined&&node!=null){
            var id=node.data.Id;//
            win.load(id);
            
            var objectName=win.objectName;
            //构建表单里父节点Id组件,由表单的数据对象和后缀'_ParentID'组成的itemId
            var ParentID=win.getComponent(''+objectName+'_ParentID');
            if(ParentID){
                var parentNode=node.parentNode;//取当前节点的父节点
                var value='0';
                var text='';
                if(parentNode){
                    var parentID=parentNode.data.Id;//父节点的Id
                    if(parentID==''||parentID==null){
	                    value=0;
	                    text=parentNode.data.text;
                    }else{
	                    value=parentID;
	                    text=parentNode.data.text;
                    }
                }else{
                    value=='0';
                    text=parentNode.data.text;
                }
                //父节点Id组件赋值
	            var arrTemp=[[value,text]];
	            ParentID.store=Ext.create('Ext.data.SimpleStore',{  
	                fields:['value','text'], 
	                data:arrTemp,
	                autoLoad:true
	            });
	            ParentID.setValue(value);
            }
        }
    },
     /***
      * 功能栏/操作列的新增按钮事件
      */
    createAddBtnClick:function(){
        var me=this;
        var newAddformChoose=me.getnewAddformChoose();
         var appComID2=me.getappComID();
         var appComID=appComID2.getValue();
         //处理代码
        var callback = function(appInfo){
                //中文名称
        var title = newAddformChoose.getValue();
        var ClassCode = "";
        if(appInfo && appInfo != ""){
            ClassCode = appInfo[me.fieldsObj.ClassCode];
        }
        
        if(ClassCode && ClassCode != ""){
            //打开应用效果窗口
            var myTree=me.getCenterTreeCom();
            var records=myTree.getSelectionModel().getSelection();
            var node=null;
            if(records&&records.length>0){
                node=records[0];
            }else{
                node=null;
            }
            me.openAppShowWin(title,ClassCode,node,'add');
        }else{
            Ext.Msg.alert("提示","没有类代码！");
        }
        }
            //与后台交互
            me.getInfoByIdFormServer(appComID,callback);
            
      },
      /***
       * 功能栏/操作列的修改按钮事件
       */
      createEditBtnClick:function(){
        var me=this;
         var newAddformChoose=me.getnewAddformChoose();
         var appComID2=me.getappComID();
         var appComID=appComID2.getValue();
         //处理代码
            var callback = function(appInfo){
                //中文名称
                var title = newAddformChoose.getValue();
                var ClassCode = "";
                if(appInfo && appInfo != ""){
                    ClassCode = appInfo[me.fieldsObj.ClassCode];
                }
                if(ClassCode && ClassCode != ""){
                    var myTree=me.getCenterTreeCom();
                    var records=myTree.getSelectionModel().getSelection();
                    var node=null;
                    if(records&&records.length>0){
                        node=records[0];
                    }else{
                        node=null;
                    }
                    //打开应用效果窗口
                    me.openAppShowWin(title,ClassCode,node,'edit');
                }else{
                    Ext.Msg.alert("提示","没有类代码！");
                }
            }
            //与后台交互
            me.getInfoByIdFormServer(appComID,callback);
      },
      /***
       * 功能栏/操作列的查看按钮事件
       */
      createShowBtnClick:function(){
         var me=this;
         var newAddformChoose=me.getnewAddformChoose();
         var appComID2=me.getappComID();
         var appComID=appComID2.getValue();
         //处理代码
            var callback = function(appInfo){
                //中文名称
                var title = newAddformChoose.getValue();
                var ClassCode = "";
                if(appInfo && appInfo != ""){
                    ClassCode = appInfo[me.fieldsObj.ClassCode];
                }
                if(ClassCode && ClassCode != ""){
                    //打开应用效果窗口
		            var myTree=me.getCenterTreeCom();
		            var records=myTree.getSelectionModel().getSelection();
		            var node=null;
		            if(records&&records.length>0){
		                node=records[0];
		            }else{
		                node=null;
		            }
                    me.openAppShowWin(title,ClassCode,node,'show');
                }else{
                    Ext.Msg.alert("提示","没有类代码！");
                }
            }
            //与后台交互
            me.getInfoByIdFormServer(appComID,callback);
      },
    
      //右键菜单删除按钮
      isMenuDelBtn:function(){
           var me=this;
           menumelbtn={
           text:"删除",iconCls:'delete',
              handler:function(btn,e,optes){
                   var myTree=me.getCenterTreeCom();
                   var node=myTree.getSelectionModel().getSelection();
                       if(node!=null){
                           me.deleteModuleTools();
                       }
                       me.fireEvent('delClick');
                 }
           };
           return menumelbtn;
       },  

    //面板功能栏新增按钮
     istoolsAddBtn:function(){
          var me=this;
          toolsAdd={
              iconCls:'build-button-add hand',text:'',
              itemId:'IstoolsAddBtn',tooltip:'新增信息',
              handler:function(btn,e,optes){
                 me.createAddBtnClick();
                 me.fireEvent('istoolsAddBtn');
              }
          };
          return toolsAdd;
      },

      //==================================================
      //面板功能栏查看按钮
      istoolsShowBtn:function(){
        var me=this;
        toolsShow={
            iconCls:'build-button-see hand',text:'',
            tooltip:'查看信息',itemId:'IstoolsShowBtn',
            handler:function(btn,e,optes){
                 me. createShowBtnClick();
                 me.fireEvent('showClick');
            }
        };
        return toolsShow;
     },
     //面板功能栏修改按钮
     istoolsEditBtn:function(){
       var me=this;
       toolsEdit={
           iconCls:'build-button-edit hand',tooltip:'修改',text:'',
           itemId:'IstoolsEditBtn', 
           handler:function(btn,e,optes){
                 me.createEditBtnClick();
                 me.fireEvent('editClick');
           }
       };
       return toolsEdit;
    },
    //面板功能栏删除按钮
    istoolsDelBtn:function(){
      var me=this;
      toolsDel={
          iconCls:'build-button-delete hand',tooltip:'删除',text:'',
          itemId:'IstoolsDelBtn',
          handler:function(btn,e,optes){
            var myTree=me.getCenterTreeCom();
            var records=myTree.getSelectionModel().getSelection();
                if(records!=null){
                    me.deleteModuleTools();
                }
                me.fireEvent('delClick');
          }
      };
      return toolsDel;
   },
  
   //面板功能栏确定按钮
   isConfirmBtn:function(){
        var me=this;
        confirmbtn={
            xtype:'button',text:'',itemId:'save',tooltip:'确定',
            iconCls:'build-button-save',
            handler:function(){
                me.fireEvent('isConfirmBtn');
            }
        };
        return confirmbtn;
    },  
    //面板功能栏取消按钮
    isCancelBtn:function(){
         var me=this;
         cancelbtn={
             xtype:'button',text:'',itemId:'reset',tooltip:'取消',
             iconCls:'build-button-refresh',
             handler:function(){
                 me.fireEvent('isCancelBtn');
             }
         };
         return cancelbtn;
     },  
    //表格树操作列
    setActionColumn:function (){
        var me=this;
        var itemsArr=[];
        var isAddBtn=me.getoperateAddValue();
        var isShowBtn=me.getoperateShowValue();
        var isEditBtn=me.getoperateEditValue();
        var isDelBtn=me.getoperateDelValue();
        if(isAddBtn==true){
            itemsArr.push(me.addBtn());
        }
        if(isShowBtn==true){
            itemsArr.push(me.showBtn());
        }
        if(isEditBtn==true){
            itemsArr.push(me.editBtn());
        }
        if(isDelBtn==true){
            itemsArr.push(me.delBtn());
        }
        var actionColumn={xtype:'actioncolumn',text:'操作列',width:100,align:'center', itemId:'Action',
            items:itemsArr};
           return actionColumn;
     }, 
      
   //删除标记
   delcolumn:{
        dataIndex:'hasBeenDeleted',text:'删除标记',width:60,
        renderer:function(value, p, record){
            if(value == "true"){
                return Ext.String.format("<b style='color:gray'>已删除</b>");
            }else if(value == "false"){
                return Ext.String.format("<b style='color:red'>删除失败</b>");
            }else{
                return Ext.String.format("");
            }
        }
    },
    
  //表格树新增操作列
   addBtn:function(){
       var me=this;
       addBtn={
           iconCls:'build-button-add hand',tooltip:'新增',
           handler:function(grid,rowIndex,colIndex,item,e,record){
               var appComID=me.getappComID();
               if (appComID.getValue()!==""){
                   me.createAddBtnClick();
                   me.fireEvent('addClick');
               }else{
                   return;
               }
           }
        };
       return addBtn;
    },
    
   //表格树修改操作列
   editBtn:function(){
       var me=this;
       editBtn={
           iconCls:'build-button-edit hand',tooltip:'修改信息',
           handler:function(grid,rowIndex,colIndex,item,e,record){
               var appComID=me.getappComID();
              if (appComID.getValue()!==""){
                  me.createEditBtnClick();
                  me.fireEvent('editClick');
              }else{
                  return;
              } 
           }
       };
      return editBtn;
    },    
    
    //表格树删除操作列
    delBtn:function(){
        var me=this;
        delBtn={
            iconCls:'build-button-delete hand',tooltip:'删除',
            handler:function(grid,rowIndex,colIndex,item,e,record){
                me.deleteAppInfo(record);
                me.fireEvent('delClick');
            }
        };
       return delBtn;
     }, 
     
     //表格树查看操作列
     showBtn:function(){
        var me=this;
        showBtn={
             iconCls:'build-button-see hand',tooltip:'查看',
             handler:function(grid,rowIndex,colIndex,item,e,record){
		         var appComID2=me.getappComID();
		         var appComID=appComID2.getValue();
                 if(appComID!==""){
                      me.createShowBtnClick();
                      me.fireEvent('showClick');
                 }else{
                     return;
                 }   
             }
        };
       return showBtn;
      }, 
   
    /**
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
    //面板功能栏刷新按钮
    refreshBtn:function(){
        var me=this;
        refreshBtn={
            iconCls:'build-button-refresh',tooltip:'刷新数据',text:'',
            itemId:'refresh',
            type:'refresh',
            handler:function(event,toolEl,owner,tool){
                var tree=me.getCenterTreeCom();
                if(tree&&tree!=undefined){
	                var treeToolbar=tree.getComponent('treeToolbar');
	                if(treeToolbar==undefined||treeToolbar==''){
	                   treeToolbar=tree.getComponent('treeToolbarTwo');
	                }
	                if(treeToolbar&&treeToolbar!=undefined){
		                var refresh=treeToolbar.getComponent('refresh');
		                if(refresh&&refresh!=undefined){
		                    refresh.disabled=true;//不可用
		                }
	                }
                }    
                me.load();
            }
        };
        return refreshBtn;
    },
     //面板功能展开全部节点
    minusBtn:function(){
        var me=this;
        minusBtn={
            iconCls:'build-button-arrow-out',tooltip:'全部展开',text:'',
            itemId:'IsMinusBtn',
            type:'plus',
            name:'plus',
            handler:function(event,toolEl,owner,tool){
                var objectName=me.getobjectName();
                var center=me.getCenterTreeCom();
                center.expandAll();
            }
        };
        return minusBtn;
    },
     //面板功能收缩全部节点
    plusBtn:function(){
        var me=this;
        plusBtn={
            text:'',iconCls:'build-button-arrow-in',tooltip:'全部收缩',
            itemId:'minus',
            type:'minus',
            handler:function(event,toolEl,owner,tool){
                var center=me.getCenterTreeCom();
                center.collapseAll();
                center.getRootNode().expand();
            }
        };
        return plusBtn;
    },
    
   /**
    * 刷新列表树
    * @public
    */
   load:function(){
       var me=this;
       var center=me.getCenterTreeCom().getStore();
       center.load();
   },
    //=================模糊查询==============
    /**
     * 模糊查询过滤函数(）
     * @param {} value
     */
    filterByText: function(text) {   
        this.filterBy(text,this.filterfield); 
    },         
    filterBy: function(text, by) {  
        var me = this;  
        this.clearFilter(); 
        var tempItem= me.getCenterTreeCom();
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
     clearFilter: function() {  
         var me =this;
         var tempItem= me.getCenterTreeCom();
         var view = tempItem.getView();  //获得面板视图
         tempItem.getRootNode().cascadeBy(function(tree, view){               
             var uiNode = view.getNodeByRecord(this);   
            
             if(uiNode) {                   
                 Ext.get(uiNode).setDisplayed('table-row');               
                 }           
             }, null, [this, view]);       
     },
    ///模糊查询生成Str
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
             "}"
             return  clearFilter;
     },
     
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
        "}"
        return filterBy;
     },
    filterByTextStr: function() {  
        var filterByText="";
        filterByText="function (text){" +
            "this.filterBy(text,this.filterfield);"+
        "}"
        return filterByText;
    },  

   //面板功能栏刷新按钮
    refreshBtnStr:function(){
       var  refreshBtn="";
        refreshBtn= refreshBtn+
         "function () {" +
         "var me=this;"+
          "var refreshBtn={iconCls:'build-button-refresh',type:'refresh',itemId:'refresh',tooltip:'刷新数据',text:'',"+
            "handler:function(event,toolEl,owner,tool){"+
            
                "var treeToolbar=me.getComponent('treeToolbar');"+
                "if(treeToolbar==undefined||treeToolbar==''){"+
                    "treeToolbar=me.getComponent('treeToolbarTwo');"+
                "}"+
                    "if(treeToolbar&&treeToolbar!=undefined){"+
                        "var refresh=treeToolbar.getComponent('refresh');"+
                        "if(refresh&&refresh!=undefined){"+
                            //数据获取成功后,刷新按钮不可用
                            "refresh.disabled=true;"+
                        "}"+
                    "}"+
                    
                "me.load('');" +
            "}"+
            "};"+
        "return refreshBtn;" +
      "}";
        return refreshBtn;
    },
   //面板功能展开全部节点
   minusBtnStr:function(){
       var  minusBtn="";
       minusBtn=minusBtn+
       "function () {" +
       "var me=this;"+
       "var minusBtn={iconCls:'build-button-arrow-out',type:'plus',itemId:'plus',tooltip:'全部展开',text:'',"+
           "handler:function(event,toolEl,owner,tool){"+
            "me.expandAll();"+
           "}"+
           "};"+
       "return minusBtn;" +
      "}";
       return minusBtn;
   },
   //面板功能收缩全部节点
   plusBtnStr:function(){
       var plusBtn="";
       plusBtn=plusBtn+
       "function () {" +
       "var me=this;"+
       "var plusBtn={text:'',iconCls:'build-button-arrow-in',type:'minus',itemId:'minus',tooltip:'全部收缩',"+
            "handler:function(event,toolEl,owner,tool){"+
            "me.collapseAll();"+
            "me.getRootNode().expand();"+
            "}"+
            "};"+
        "return plusBtn;" +
      "}";
       return plusBtn;
   },
/***
    * 创建右键菜单
    * @return {}
    */
   createContextmenuStr:function(){
      var com='';
      fun=
      "function(){"+
      "var me=this;var menuItems=[];"+
      "if(me.isMenu==true){"+
           //创建右键菜单的删除按钮
           "if (me.isDelMenuBtn==true){"+
               "menuItems.push(me.createContextmenuDeletebtn());"+
           "}"+
           //创建右键菜单的其他按钮
           
       "}else{"+
           "menuItems=[];"+
       "}"+
      "return menuItems;"+
      "}"
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
                     //"me.load('');" +
                 "}" +
             "};"+
       "return com;"+
      "}"
       return fun;
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
            getAppListServerUrl:me.getAppListServerUrl,
            defaultLoad:true,
            readOnly:true,
            pageSize:9//每页数量
        }).show();
    var where = "";
    where += "btdappcomponents.AppType= 2";
    appList.load(where);
        appList.on({
            okClick:function(){
                var records = appList.getSelectionModel().getSelection();
                if(records.length == 0){
                    Ext.Msg.alert("提示","请选择一个应用！");
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
     * 设置弹出表单的属性
     * @private
     * @param {} record
     */
    setWinformInfo:function(record){
        var me = this;
        var winformtext=me.getnewAddformChoose();
        var appComID2=me.getappComID();
        appComID2.setValue(record.get('BTDAppComponents_Id'));
        winformtext.setValue(record.get('BTDAppComponents_CName'));  
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