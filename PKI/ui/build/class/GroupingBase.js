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
Ext.define('Ext.build.GroupingBase',{
    extend:'Ext.panel.Panel',
    alias: 'widget.groupingbase',
    //=====================可配参数=======================
    /**
     * 应用组件ID
     */
    appId:-1,
    appType:-1,
    /**
     * 时间戳字符串
     * @type String
     */
    DataTimeStamp:'',
    /**
     * 构建名称
     */
    buildTitle:'分组查询构建工具(一)',
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
    /**
     * 往gridpane添加一行记录的临时变量
     * @type array
     */
    southAddStore:[],
    //数据对象配置private
    
    win:null,//创建和弹出选择器窗体
    win2:null,//创建和弹出选择器窗体
    winHeight:270,        //弹出选择器窗体高度像素
    winWidth:460,       //弹出选择器窗体宽度像素
    winTitle:'',        //弹出选择器窗体标题
    comHeight:20,
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
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,list:[{a:1}]}"}
     *  返回数据对象列表的值属性就是ResultDataValue
     * @type String
     */
    objectRoot:'ResultDataValue',
    /**
     * longfc
     * 读取数据对象列表的数据集合节点
     * @type String
     */
    objectRootTwo:'list',
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
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,list:[{a:1}]}"}
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
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,list:[{a:1}]}"}
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
    
    //=====================内部变量=======================
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
     * longfc
     * 数据项类型
     * @type 
     */
    comTypeList:[
        ['button','按钮']

    ],
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
     * 表单初始宽度
     * @type Number
     */
    defaultPanelWidth:680,
    /**
     * 表单初始高度
     * @type Number
     */
    defaultPanelHeight:240,
    /**
     * 计数
     * @type Number
     */
    isJustOpenCount:0,
    /**
     * 应用字段对象
     * @type 
     */
    fieldsObj:{
        /**
         * 应用组件ID
         * @type String
         */
       	Id:'BTDAppComponents_Id',
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
        me.addEvents('saveClick');//保存按钮
        me.addEvents('saveAsClick');//另存按钮
        me.callParent(arguments);
    },
    /**
     * 渲染完后执行
     * @author hujie eidt 2013-06-08
     * @private
     */
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
        if(me.appId!=""&&me.appId != -1){
    	   me.setAppParams();
        }
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
        //数据项列属性列表
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
        south.height = 180;
        east.width = 250;
        
        //功能块收缩属性
        east.split = true;
        east.collapsible = true;
        
        south.split = true;
        south.collapsible = true;
        
        me.items = [north,center,east,south];
    },
    /**
     * 功能栏--打开分组查询新增或者编辑界面
     * @private
     * @return {}
     */
    createNewCom:function(com){
        var me=this;
        var type = "groupingsearch";
        //先清空
        me.southAddStore=null;
        if(com&&com!=null){
            me.openAppEditWin(type,com);
        }else{
            me.openAppEditWin(type,null);
        }
    },
    /**
     * 打开应用设置页面
     * @private
     * @param {} button
     * @param {} e
     */
    openAppEditWin:function(type,com){
        var me = this;
        var title = "";
        var panel = "";
        if(type == "groupingsearch"){
            title = "分组查询按钮";
            panel = "Ext.build.GroupingSearch";
        }
        var appId = -1;
        var id=-1;
        var type='add';
        
        var comX=1;
        var comY=1;
        var comWidth=80;

        if(com){
            title = "修改"+title;
            appId = com.itemId;
            type='edit';
            comX=com.x;
            comY=com.y;
            comWidth=com.width;
            me.comHeight=com.height;
            
        }else{
            title = "新增"+title;
            type='add';
        }
        
        var win = Ext.create(panel,{
            title:title,
            operationType:type,
            /***
		     * 分组按钮X
		     * @type String
		     */
		    comX:comX,
		    /***
		     * 分组按钮Y
		     * @type String
		     */
		    comY:comY,
		    /***
		     * 分组按钮Width
		     * @type String
		     */
		    comWidth:comWidth,
		    /***
		     * 分组按钮Height
		     * @type String
		     */
		    comHeight:me.comHeight,
            width:'98%',
            height:'98%',
            appId:appId,
            modal:true,//模态
            resizable:true,//可变大小
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true//可移动
        }).show();
        
        if(com){  
            win.objectName=''+com.objectName;
            win.initSetValue(com);
        }else{
            //所有组件信息
            var southRecords = me.getSouthRecords();
            var objectName='';
            if(southRecords.length>0){
                objectName=''+southRecords[0].get('objectName');
                if(objectName==''&&southRecords.length>1){
                    objectName=''+southRecords[1].get('objectName');
                }
            }
            win.objectName=''+objectName;
            win.initSetValue(null);
        }
        //监听GroupingSearch的确定事件
        win.on({
            saveClick:function(){
                me.southAddStore=win.getValue();
		        if(me.southAddStore.length>0){
                    //判断是否已经存在了该分组名称
		            var south = me.getComponent('south');
		            var store =south.getStore();
                    var itemId=me.southAddStore[0].InteractionField;
                    var operationType=''+me.southAddStore[0].operationType;
                    var index = store.find(me.objectPropertyValueField,itemId);//是否存在这条记录
                    if(operationType=='add'){
                        store.add(me.southAddStore[0]);
                    }else if(operationType=='edit'){//修改分组查询按钮
                        if(index != -1){//修改时分组查询按钮的itemId名称不变时
                            var record = store.findRecord(me.objectPropertyValueField,itemId);
		                    if (record){
                                var BtnWhere=me.southAddStore[0].BtnWhere;
                                var BtnExplain=me.southAddStore[0].BtnExplain;
                                var DisplayName=me.southAddStore[0].DisplayName;
                                var objectName=me.southAddStore[0].objectName;
		                        record.set('BtnWhere', ''+BtnWhere);
	                            record.set('BtnExplain', BtnExplain);
	                            record.set('DisplayName', DisplayName);
	                            record.set('objectName', objectName);
		                        record.commit();
		                    }
                        }else{//修改时itemId名称改变时,直接新增一个分组查询按钮
                            store.add(me.southAddStore[0]);
                        }
                    }
                    me.browse();
		        }
                win.close();
            }
        });
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
               {xtype:'button',text:'新增',itemId:'btnAdd',iconCls:'build-button-add',
                    handler:function(){
                        me.createNewCom(null);//新增分组查询
                    }
                },'-',{xtype:'button',text:'保存',itemId:'save',iconCls:'build-button-save',
                    handler:function(){
                        me.save(true);
                    }
                },
                {xtype:'button',text:'另存',itemId:'saveAs',iconCls:'build-button-save',margin:'0 4 0 0',
                    handler:function(){
                        me.saveAs();
                    }
                },
                 '-',
                {xtype:'button',text:'浏览',itemId:'browse',iconCls:'build-button-see',
                    handler:function(){
                        me.browse();
                    }
                },'-',
                {xtype:'checkboxfield',itemId:'hasBorder',boxLabel:'开启边框',margin:'0 4 0 0',checked:me.hasBorder,
                    listeners:{
                        change:function(com,newValue,oldValue,eOpts){
                            me.hasBorder = newValue;
                            me.changeFieldBorder(newValue);
                        }
                    }
                }
                
            ]
        };
        return com;
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
            items:[{
                xtype:'form',
                title:'分组查询',
                itemId:'center',
                width:me.defaultPanelWidth,
                height:me.defaultPanelHeight
            }]
        };
        return com;
    },
    //==================操作属性列表的某一控件属性,更新展示区域的控件显示效果==============

    /**
     * 展示区域的某一控件的x轴属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} x:新x轴值;y:新y轴值;
     */
    setComponentXY:function(InteractionField,x,y){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
            tempItem.setPosition(x,y);
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
            tempItem.setHeight(newValue);
    },
     /**
     * 展示区域的某一控件的显示名称宽度属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值;record
     */
    setComponentLabWidth:function(InteractionField,newValue,oldValue,record,x,y){
        var me=this;
        var center=me.getCenterCom();
        var tempItem= me.getCenterCom().getComponent(InteractionField);
        
        var owner = center.ownerCt;
            center.remove(tempItem);
            
        var com =me.newfromItemForLabelWidth(InteractionField,newValue,oldValue,record,x,y)
            center.add(com);

    },

    //==============================某一控件属性更新==============================

    
    /**
     * 列属性列表
     * @private
     * @return {}
     */
    createSouth:function(){
        var me = this;
        var com = {
            xtype:'grid',
            title:'数据项属性列表',
            columnLines:true,//在行上增加分割线
            columns:[//列模式的集合
                {xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
                {text:'交互字段',dataIndex:'InteractionField',editor:{allowBlank:false}},//,disabled:true
                {text:'显示名称',dataIndex:'DisplayName',
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                record.set('DisplayName',newValue);
                                record.commit();
                                me.setComponentFieldLabel(InteractionField,newValue); 
                            }
                        }
                    }
                },
                {text:'数据项类型',dataIndex:'Type',width:80,align: 'center',hidden:true,
                    renderer:function(value, p, record){
                        var typelist = me.comTypeList;
                        for(var i=0;i<typelist.length;i++){
                            if(value == typelist[i][0]){
                                return Ext.String.format(typelist[i][1]);
                            }
                        }
                    },
                    editor:new Ext.form.field.ComboBox({
                        mode:'local',editable:false, 
                        displayField:'text',valueField:'value',
                        store:new Ext.data.SimpleStore({ 
                            fields:['value','text'], 
                            data:me.comTypeList
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
                {text:'位置X',dataIndex:'X',width:50,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:999,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                var y=record.get('Y');
                                me.setSouthRecordForNumberfield(InteractionField,'x',newValue);
                                me.setComponentXY(InteractionField,newValue,y);
                                var south = me.getComponent('south')
                                var store =south.getStore();
                                store.sort();
                            }
                        }
                    }
                },
                {text:'位置Y',dataIndex:'Y',width:50,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:999,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                var x=record.get('X');
                                me.setSouthRecordForNumberfield(InteractionField,'X',newValue);
                                me.setComponentXY(InteractionField,x,newValue);
    
                            }

                        }
                    }
                },
                {text:'宽度',dataIndex:'Width',width:70,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:999,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'Width',newValue);
                                me.setComponentWidth(InteractionField,newValue);
    
                            }
                        }
                    }
                },

                
                 {text:'高度',dataIndex:'Height',width:70,align:'center',
                    xtype:'numbercolumn',format:'0',emptyText:'默认',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:0,
                        maxValue:999,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'Height',newValue);
                                me.setComponentHeight(InteractionField,newValue);
                            }
                        }
                    }
                },
                
                {text:'HQL语句',dataIndex:'BtnWhere',width:270,editor:{allowBlank:false}},
                {text:'分组按钮描述',dataIndex:'BtnExplain',disabled:true,hidden:true},
                {text:'数据对象名称',dataIndex:'objectName',disabled:true},
                {text:'操作类型',dataIndex:'operationType',disabled:true,hidden:true}
                
            ],
            store:Ext.create('Ext.data.Store',{
                fields:me.getSouthStoreFields(),
                proxy:{
                    type:'memory',
                    reader:{type:'json',root:'list'}
                }
            }),
            plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:2})
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
        //保存信息
        var appInfo = me.createAppInfo();
        //表单宽高
        var panelWH = me.createWidthHieght();
        //标题设置
        var title = me.createTitle();
        //数据对象

        //其他设置
        var other = me.createOther();
        
        var formParamsPanel = {
            xtype:'form',
            itemId:'center' + me.ParamsPanelItemIdSuffix,
            title:'表单属性配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[appInfo,panelWH,title,other]
        };
        var com = {
            xtype:'form',
            title:'表单属性配置',
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
            xtype:'fieldset',title:'功能信息',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'appInfo',
            items:[
            {
                xtype:'textfield',fieldLabel:'功能编号',labelWidth:55,anchor:'100%',
                itemId:'appCode',name:'appCode',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textfield',fieldLabel:'中文名称',labelWidth:55,anchor:'100%',
                itemId:'appCName',name:'appCName',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textareafield',fieldLabel:'功能简介',labelWidth:55,anchor:'100%',grow:true,
                itemId:'appExplain',name:'appExplain'
            }]
        };
        return com;
    },

    /**
     * 面板标题属性
     * @private
     * @return {}
     */
    createTitle:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'标题',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'title',
            items:[{
                xtype:'checkbox',boxLabel:'显示标题(===)',checked:true,
                itemId:'hasTitle',name:'hasTitle',
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.browse();//浏览
                    }
                }
            },{
                xtype:'textfield',fieldLabel:'显示名称',labelWidth:55,value:'表单',anchor:'100%',
                itemId:'titleText',name:'titleText'
            }]
        };
        
        return com;
    },

    /**
     * 其他设置
     * @private
     * @return {}
     */
    createOther:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'其他',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'other',
            items:[{
                xtype:'checkbox',itemId:'openLayoutType',name:'openLayoutType',boxLabel:'是否开启排列方式'
            },{
                xtype:'radiogroup',
                itemId:'layoutType',
                fieldLabel:'排列方式',
                labelWidth:55,
                columns:3,
                vertical:true,
                items:[
                    {boxLabel:'四列',name:'layoutType',inputValue:'4'},
                    {boxLabel:'五列',name:'layoutType',inputValue:'5'},
                    {boxLabel:'六列',name:'layoutType',inputValue:'6',checked:true},
                    {boxLabel:'七列',name:'layoutType',inputValue:'7'},
                    {boxLabel:'八列',name:'layoutType',inputValue:'8'}
                ]
            },{
                xtype:'textfield',itemId:'formHtml',name:'formHtml',hidden:true
            }]
        };
        return com;
    },
    /**
     * 设置面板的宽高
     * @private
     * @return {}
     */
    createWidthHieght:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'表单宽高',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'WH',
            items:[{
                xtype:'numberfield',fieldLabel:'表单宽度',labelWidth:55,anchor:'100%',
                itemId:'Width',name:'Width',value:me.defaultPanelWidth,
                listeners:{
                    blur:function(com,The,eOpts){
                        var center = me.getCenterCom();
                        center.setWidth(com.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'表单高度',labelWidth:55,anchor:'100%',
                itemId:'Height',name:'Height',value:me.defaultPanelHeight,
                listeners:{
                    blur:function(com,The,eOpts){
                        var center = me.getCenterCom();
                        center.setHeight(com.value);
                    }
                }
            }]
        };
        return com;
    },
    //=====================功能按钮栏事件方法=======================
    /**
     * 更新表单数据
     * @private
     */
    loadFormValues:function(){
        var me = this;
        var center = me.getCenterCom();
        center.loadData();
    },
    /**
     * 更改表单内组件的显示名称
     * true：开启显示名称；
     * false：关闭显示名称；
     * @private
     * @param {} bo
     */
    changeFieldLabel:function(bo){
        var me = this;
        var store = me.getComponent('south').store;
        var center = me.getCenterCom();
        var items = center.items.items;
        
        if(bo){
            for(var i in items){
                var record = store.findRecord('InteractionField',items[i].itemId);
                items[i].setFieldLabel(record.get('DisplayName'));
            }
        }else{
            for(var i in items){
                items[i].setFieldLabel("");
            }
        }
    },
    /**
     * 更改表单内组件的边框
     * true：显示组件边框；
     * false：隐藏组件边框；
     * @private
     * @param {} bo
     */
    changeFieldBorder:function(bo){
        var me = this;
        var center = me.getCenterCom();
        var items = center.items.items;
        
        if(bo){
            for(var i in items){
                items[i].setBorder(1);
            }
        }else{
            for(var i in items){
                items[i].setBorder(0);
            }
        }
    },
    /**
     * 浏览处理
     * 先清除展示区域的原来内容,再更换组件属性面板
     * @private
     */
    browse:function(){
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
        //表单数据项
        var items = me.createComponents();
        for(var i in items){
        	center.add(items[i]);
        }
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
            //应用组件ID
			var id = bo ? me.appId : -1;
            //生成应用对象
            var BTDAppComponents = {
				Id:id,//应用组件ID
                LabID:'0',
                CName:params.appCName,//名称
                EName:'',
                
                ModuleOperCode:params.appCode,//功能编码
                ModuleOperInfo:params.appExplain,//功能简介
                InitParameter:params.defaultParams,//初始化参数
				AppType:me.appType,//8,//应用类型(分组查询)
                BuildType:1,//构建类型
                ExecuteCode:'',//执行代码
				DesignCode:me.JsonToStr(appParams),//设计代码
                Creator:'',
                Modifier:'',
                PinYinZiTou:'',
                ClassCode:appClass//类代码
            };
           	
			if(me.DataTimeStamp != ""){
				BTDAppComponents.DataTimeStamp = me.DataTimeStamp;//时间戳
			}
			
            var callback = function(){
                me.fireEvent('saveClick');
            }
            //后台保存数据
            me.saveToServer(BTDAppComponents,callback);
        }
    },

    //=====================属性面板事件方法=======================

    //=====================组件的创建与删除=======================
    /**
     * 新建表单
     * @private
     * @return {}
     */
    createForm:function(){
        var me = this;
        //表单配置参数
        var params = me.getPanelParams();
        //表单数据项
		var title = params.titleText;
		var formHtml = params.formHtml;
		var width = parseInt(params.Width);
		var height = parseInt(params.Height);
		
        var form = {
            xtype:'form',
            itemId:'center',
            layout:'absolute',
            autoScroll:true,
            title:title,
		    html:formHtml,
		    width:width,
		    height:height,
            resizable:{handles:'s e'}
        };
        //加载数据方法
        form.loadData = function(){
            //数据服务地址
            var params = me.getPanelParams();
            var url = getRootPath() + "/" + me.getDataUrl();
            Ext.Ajax.request({
                async:false,//非异步
                url:url,
                method:'GET',
                timeout:5000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        var values = Ext.JSON.decode(result.ResultDataValue);
                        var form = me.getCenterCom();
                        form.getForm().setValues(values);
                    }else{
                        Ext.Msg.alert('提示','获取表单数据失败！');
                    }
                },
                failure : function(response,options){ 
                    Ext.Msg.alert('提示','获取表单数据请求失败！');
                }
            });
        };
        
        if(!params.hasTitle){
            form.preventHeader = true;
        }
        form.listeners = {
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                //表单宽度和高度赋值
                formParamsPanel.getForm().setValues({Width:width,Height:height});
            }
        };
        
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
     * 创建所有组件
     * @private
     * @return {}
     */
    createComponents:function(){
        var me = this;
        var arr = {
            basicComArr:[],//一般组件
            otherComArr:[]//
        };
        //所有数据项基础属性
        var coms = me.createComponentsBasicInfo();
        
        for(var i in coms){
            var com = coms[i];
            //边框属性设置
            com.border = 0;
            //是否有边框
            if(me.hasBorder){
                com.border = 1;
            }
            com.draggable = true;//注释这一行,改变大小事件失效,拖放事件生效 long10
            arr.basicComArr.push(com);
        }
        
        //设置组件对象的XY值
        arr = me.createComponentsXY(arr);
        //合并组件数组
        var comArr = arr.basicComArr.concat(arr.otherComArr);
        var coms = [];
        
        for(var i=0;i<comArr.length;i++){
            var com = comArr[i];
            me.setColumnParamsRecord(com.itemId,'X',com.x);
            me.setColumnParamsRecord(com.itemId,'Y',com.y);
            me.setColumnParamsRecord(com.itemId,'Width',com.width);
            me.setColumnParamsRecord(com.itemId,'Height',com.height);
            me.setComListeners(com);//组件监听
            coms.push(com);
        }
        
        return coms;
    },
    /**
     * 所有数据项基础属性
     * @private
     * @return {}
     */
    createComponentsBasicInfo:function(){
        var coms = [];
        var me = this;
        var store = me.getComponent('south').store;
        var items = store.data.items;
        //所有组件信息
        var southRecords = me.getSouthRecords();
        for(var i in southRecords){
            var record = southRecords[i];
            //根据组件类型生成组件
            var com = me.createComponentsByType(record.get('Type'),record);
            com.type = record.get('Type');
            //公共属性
            com.itemId = record.get('InteractionField');
            com.x = record.get('X');
            com.y = record.get('Y');
            coms.push(com);
        }
        return coms;
    },
    /**
     * 设置组件对象的XY值
     * @private
     * @param {} comArr
     * @return {}
     */
    createComponentsXY:function(comArr){
        var arr = comArr;
        var me = this;
        var params = me.getPanelParams();
        var openLayoutType = params.openLayoutType;//是否开启排列方式
        var allWidth = params.allWidth;
        
        var defaultWidth = 80;
        
        if(allWidth && allWidth != ""){
            defaultWidth = parseInt(allWidth);
        }
        if(openLayoutType){
            var me = this;
            //列表配置参数
            var params = me.getPanelParams();
            //排列方式
            var layoutType = parseInt(params.layoutType);
            
            var x = 5;
            var y = 5;
            var height1 = 26;//一般组件的高度
            for(var i=0;i<arr.basicComArr.length;i++){
                var com = arr.basicComArr[i];
                if(!arr.basicComArr[i].hidden){
                com.x = x;
                com.y = y;
                
                x = x + defaultWidth + 10;
                if((i+1) % layoutType == 0){
                    y += height1;
                    x = 5;
                }
            }
         }   
            if(arr.basicComArr.length % layoutType != 0){
                x = 5;
                y += height1;
            }
            for(var i=0;i<arr.otherComArr.length;i++){
                var com = arr.otherComArr[i];
                if(!arr.otherComArr[i].hidden){
                com.x = x;
                com.y = y;
                y += com.height;
                y += 10;
            }
        }
       } 
        return arr;
    },
    
    /**
     * 根据组件类型生成组件
     * @private
     * @param {} type
     * @param {} record
     * @return {}
     */
    createComponentsByType:function(type,record){
        var me = this;
        var com = null;
        if(type == 'button'){//按钮
            com = me.createButton(record);
        }
        return com;
    },
    /**
     * 创建按钮组件
     * @private
     * @param {} record
     * @return {}
     */
    createButton:function(record){
        var me=this;
        var InteractionField=record.get('InteractionField');
        InteractionField=InteractionField.replace(/'/g,'');
        var BtnWhere=record.get('BtnWhere');
        BtnWhere=BtnWhere.replace(/'/g,'');
        var com = {
            xtype:'button',
            name:InteractionField,
            itemId:InteractionField,
            width:record.get('Width'),
            text:record.get('DisplayName'),
            btnExplain:"'"+record.get('BtnExplain')+"'",
            objectName:""+record.get('objectName')+"",//选择的数据对象
            btnWhere:"'"+record.get('BtnWhere')+"'"//分组查询每个按钮添加一个自定义属性(btnWhere),保存按钮属性自定义结果值,供外面调用得到where串
           
        };
        var height=record.get('Height');
        if(height!=""&&height!="默认"){
            com.height=height;
        }else{
            com.height=me.comHeight;
        }
        return com;
    },
    
    /**
     * 删除面表单中的组件
     * @private
     * @param {} componentItemId
     */
    removeComponent:function(componentItemId){
        var me = this;
        //删除数据项组件
        var center = me.getCenterCom();
        center.remove(componentItemId);
        //删除数据项属性面板
        me.getComponent('east').remove(componentItemId + me.ParamsPanelItemIdSuffix);
        //me.switchParamsPanel('center');
        //删除数据项属性列表中的当前数据项数据
        me.removeSouthValueByKeyValue('InteractionField',componentItemId);
    },
    /**
     * 修改展示区域表单中的某一组件的HQL的where串
     * @private
     * @param {} 
     */
    updateComponent:function(com){
        var me = this;
        //数据项组件
        if(com){
            me.createNewCom(com);
        }else{
            Ext.Msg.alert("提示","<b style='color:red'>本条记录已被删除！</b>");
        }
    },
  
    //=====================组件属性面板的创建与删除=======================
    /**
     * 给数据项属性列表赋值
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    setColumnParamsRecord:function(InteractionField,key,value){
        var me = this;
        var grid = me.getComponent('south');
        var store = grid.store;
        var record = store.findRecord('InteractionField',InteractionField);
        if(record != null){//存在
            record.set(key,value);
            record.commit();
        }
    },
    //===========================加载后台数据结束================================
    
    //=====================设置获取参数=======================
    /**
     * 获取展示区域组件
     * @private
     * @return {}
     */
    getCenterCom:function(){
        var me = this;
    var center = me.getComponent('center').getComponent('center');
    return center;
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
     * 给组件属性列表赋值
     * 当gridpanel组件是numberfield控件类型时才调用
     * 其他的调用调用或setColumnParamsRecord
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
     * 给属性面板赋值
     * @private
     * @param {} record
     */
    setParamsPanelValues:function(componentItemId){
        var me = this;
        if(componentItemId != "center"){
            var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
        
            //属性面板ItemId
            var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
            //组件属性面板
            var panel = me.getComponent('east').getComponent(panelItemId);
            
            var basic = panel.getComponent("basicParams");
            var name = basic.getComponent("name");
            var myX=basic.getComponent("X");
            var myY = basic.getComponent("Y");
            var myWidth = basic.getComponent("myWidth");
            var myHeight = basic.getComponent("myHeight");
            
            myX.setValue(record.get('X'));
            myY.setValue(record.get('Y'));
            myWidth.setValue(record.get('Width'));
            myHeight.setValue(record.get('Height'));
            
            name.setValue(record.get('DisplayName'));
        }
    },
    /**
     * 组件监听
     * @private
     * @param {} com
     */
    setComListeners:function(com){
        var me = this;
        com.listeners = {//move和resize有冲突
            //组件拖动监听
            move:function(com,x,y,eOpts){
                me.setColumnParamsRecord(com.itemId,'X',x);
                me.setColumnParamsRecord(com.itemId,'Y',y);
            },
            
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                var xy=com.getPosition(true);
                me.setColumnParamsRecord(com.itemId,'Width',width);
                me.setColumnParamsRecord(com.itemId,'Height',height);
            },
            click: function(btn,e,opt) { 
                me.fireEvent('selectClick');
             },
            contextmenu:{
                element:'el',
                fn:function(e,t,eOpts){
                    //禁用浏览器的右键相应事件 
                    e.preventDefault();e.stopEvent();
                    //右键菜单
                    new Ext.menu.Menu({
                        items:[
                            {
                            text:"删除",iconCls:'delete',tooltip:'修改删除按钮',
                            handler:function(){
                                me.removeComponent(com.itemId);
                            }
                        }, {
                            text:"修改",conCls:'build-button-edit',tooltip:'修改分组按钮',
                            handler:function(){
                                //分组按钮编号,分组按钮名称,分组按钮描述,HQL的where串
                                me.updateComponent(com);
                            }
                        }
                        ]
                    }).showAt(e.getXY());//让右键菜单跟随鼠标位置
                }
            }
        };
        return com;
    },
    /**
     * 更改所有的Width和LabelWidth
     * @private
     */
    changeComWidth:function(){
        var me = this;
        var params = me.getPanelParams();
        var store = me.getComponent('south').store;
        var data = store.data;
        if(params.allWidth != "" || params.allLabelWidth != ""){
            for(var i=0;i<data.length;i++){
                var record = data.getAt(i);
                if(params.allWidth != "")
                    record.set('Width',params.allWidth);
                if(params.allLabelWidth != "")
                    record.set('LabelWidth',params.allLabelWidth);
                record.commit();
            }
        }
    },
    /**
     * 获取组件属性列表Fields
     * @private
     * @return {}
     */
    getSouthStoreFields:function(){
        var me = this;
        var fields = [
            {name:me.columnParamsField.DisplayName,type:'string'},//显示名称
            {name:me.columnParamsField.InteractionField,type:'string'},//交互字段
            {name:'LabelWidth',type:'int'},//显示名称宽度
            {name:'Type',type:'string'},//数据项类型
            {name:'X',type:'int'},//位置X
            {name:'Y',type:'int'},//位置X
            {name:'Width',type:'int'},//数据项宽度
            {name:'Height',type:'int'},//高度
            {name:'BtnWhere',type:'string'},//显示名称按钮的where串
            {name:'BtnExplain',type:'string'},
            {name:'objectName',type:'string'}//选择的数据对象名称
        ];
        
        return fields;
    },
    /**
     * 获取设计代码
     * @private
     * @return {}
     */
    getAppParams:function(){
        var me = this;
        var panelParams = me.getPanelParams();
        
        var southParams = me.getSouthRocordInfoArray();
        var appParams = {
            panelParams:panelParams,
            southParams:southParams
        };
        return appParams;
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
            var southParams = appParams.southParams;
            me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];
            //赋值
            me.setSouthRecordByArray(southParams);
            me.setPanelParams(panelParams);
            var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
            //渲染效果
            setTimeout(function(southParams){me.browse();});
        };
        //从后台获取应用信息
        me.getAppInfoFromServer(callback);
    },


    //=====================生成需要保存的代码=======================
    /**
     * 创建类代码
     * @private
     * @return {}
     */
    createAppClass:function(){

        var me = this;
        //表单配置参数
        var params = me.getPanelParams();

        //内部组件代码
        var items = me.createComponentsStr();
        //普通表单构建的保存方法
        //----------------------------------------------------------------------
        //背景html
        var html = params.formHtml;
        var appClass = 
        "Ext.define('" + params.appCode + "',{" + 
            "extend:'Ext.form.Panel'," + 
            "alias:'widget." + params.appCode + "'," + 
            "title:'" + params.titleText + "'," + 
            "width:" + params.Width + "," + 
            "height:" + params.Height + "," + 
            "autoScroll:true," + 
            "type:'add'," + //显示方式add（新增）、edit（修改）、show（查看）
            "btnSelect:null,"+//当前选择中的按钮
            
            //对外公开的单个取值方法
            "lastValue:''," + 
             "getValue:function(){" + 
                "var me = this;" + 
                "if(me.btnSelect!=null){" +
	                "var v=''+me.btnSelect.btnWhere;" +
                    "me.lastValue=groupingSearchString(v);" +
                "}" +
                "else{" + 
                    "me.lastValue='';" + 
               " }" + 
                "return ''+me.lastValue;" + 
             "}," + 
             "layout:'absolute'," ;
            appClass=appClass+
            "initComponent:function(){" + 
                "var me=this;" + 
                //注册事件
                "me.addEvents('selectClick');" + 
                //对外公开方法
                //内部组件
                "me.items=" + items + ";";
                appClass = appClass + 
                "me.callParent(arguments);" + 
            "}," + 
            
            "afterRender:function(){" + 
            	"var me=this;" + 
                //现在的应用构建采用了引用ID的方式
                "me.callParent(arguments);" + 
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
            "}"+   
        "});";
       
        return appClass;
    },
    /**
     * 类生成代码:创建内部组件代码
     * @private
     * @return {}
     */
    createComponentsStr:function(){
        var me = this;
        var records = me.getSouthRecords();
        var items = "[";
        for(var i in records){
            var record = records[i];
            var comStr = me.createComStrByType(record);
            items = items + comStr + ",";
        }
        if(items.length > 1){
            items = items.substring(0,items.length-1);
        }
        items += "]";
        return items;
    },
    /**
     * 根据组件类型生成组件Str
     * @private
     * @param {} record
     * @return {}
     */
    createComStrByType:function(record){
        var me = this;
        var com = null;
        var type = record.get('Type');
            if(type == 'button'){//文本框
            com = me.createButtonStr(record);
        }
        
        return com;
    },
    /**
     * 创建按钮Str
     * @private
     * @param {} record
     * @return {}
     */
    createButtonStr:function(record){
        var me=this;
        var myHQL=""+record.get('BtnWhere');
        var height=record.get('Height');
        var InteractionField=record.get('InteractionField');
        InteractionField=InteractionField.replace(/'/g,'');
        var com = 
        
        "{" + 
            "xtype:'button'," + 
            "name:'" + InteractionField + "'," + 
            "itemId:'" + InteractionField + "'," + 
            "width:" + record.get('Width') + "," ;
            if(height!=""&&height!="默认"){
                com =com+"height:" + record.get('Height') + "," ;
            }
            com =com+"text:'" + record.get('DisplayName') + "'," + 
            "btnExplain:'" + record.get('BtnExplain') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "listeners: {" + 
            "click: function(btn,e,optes) {" + 
                "me.btnSelect=btn;" +
                "me.fireEvent('selectClick',btn,e,btn.btnWhere);" +
                //"me.getValue();" +
             "}" + 
            "}" + "," +  
            "btnWhere:'"+myHQL+ "'" +   
        "}";
        return com;
        
    },
    
    //=====================后台获取&存储=======================
    /**
     * 从后台获取应用信息
     * @private
     * @param {} callback
     */
    getAppInfoFromServer:function(callback){
        var me = this;
        
        if(me.appId != -1&&me.appId != ""&&me.appId!=undefined){
        	
        	var fields = "";
        	for(var i in me.fieldsObj){
        		if(me.fieldsObj[i] != me.fieldsObj.ClassCode){
        			fields += me.fieldsObj[i] + ",";
        		}
        	}
        	fields = fields == "" ? "" : fields.slice(0,-1);
            var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + me.appId + "&fields=" + fields;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:url,
                method:'GET',
                timeout:8000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                        if(ResultDataValue&&ResultDataValue!=""){
                            me.DataTimeStamp=ResultDataValue["BTDAppComponents_DataTimeStamp"];
                            me.appId=ResultDataValue["BTDAppComponents_Id"];
                        }
                        if(Ext.typeOf(callback) == "function"){
                            callback(ResultDataValue);//回调函数
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
            timeout:8000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    if(me.appId!=-1&&me.appId!=""){
                        me.getAppInfoFromServer(null);
                    }
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
    //=====================公共方法代码=======================
    /**
     * 设置表单配置参数
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
    }
});