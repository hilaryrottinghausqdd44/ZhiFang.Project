/**
 * 普通选择器构建工具
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
Ext.define('Ext.build.BasicSelector',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basicselector',
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
    buildTitle:'普通选择器构建工具',
    
    //展示区域的生成构建类型组itemId()
    groupItemId:"groupItemId",
    
    //数据对象配置private
    
    win:null,//创建和弹出选择器窗体
    win2:null,//创建和弹出选择器窗体
    winHeight:270,        //弹出选择器窗体高度像素
    winWidth:460,       //弹出选择器窗体宽度像素
    winTitle:'',        //弹出选择器窗体标题    
    
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
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{Count:1,List:[{a:1}]}"}
     *  返回数据对象列表的值属性就是ResultDataValue
     * @type String
     */
    objectRoot:'ResultDataValue',
    /**
     * 读取数据对象列表的数据集合节点
     * @type String
     */
    objectRootTwo:'List',
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
     * 数据对象内容字段匹配下拉框数组
     * @type String
     */
    objectProertyComboxFields:['text','InteractionField'],
    
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
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{Count:1,List:[{a:1}]}"}
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
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{Count:1,List:[{a:1}]}"}
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
     * 数据项类型
     * @type 
     */
    comTypeList:[
        ['panelcheckboxgroup','复选组选择器'],
        ['uxradiogroup','单选组选择器']
    ],
    /**
     * 面板样式
     * @type 
     */
    panelStyleList:[
        ['','默认'],
        ['red','喜庆红'],
        ['blue','金典蓝'],
        ['pink','温馨粉']
    ],
    /**
     * 表单背景颜色
     * @type String
     */
    bgcolor:'',       //设置表单背景颜色
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
    defaultPanelWidth:700,
    /**
     * 表单初始高度
     * @type Number
     */
    defaultPanelHeight:300,
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
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
        Ext.Loader.setPath('Ext.zhifangux',getRootPath()+'/extjs/zhifangux/');
        //初始化视图
        me.initView();
        
        //注册事件
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
        //初始化监听
        me.callParent(arguments);
        me.setAppParams();
        me.isJustOpen=false;
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
        south.height = 140;
        east.width = 280;
        
        //功能块收缩属性
        east.split = true;
        east.collapsible = true;
        
        south.split = true;
        south.collapsible = true;
        
        //组件属性列表是否默认收缩
        //south.collapsed = true;
        
        me.items = [north,center,east,south];
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
                {xtype:'button',text:'浏览',itemId:'browse',iconCls:'build-button-see',
                    handler:function(){
                        me.browse();
                    }
                },
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
                title:'选择器构建',
                itemId:'center',
                titleAlign :"left",
                bodyStyle: 'background:#ffc; padding:10px;',
                width:me.defaultPanelWidth,
                height:me.defaultPanelHeight
            }]
        };
        return com;
    },
    //==================操作属性列表的某一控件属性,更新展示区域的控件显示效果==============
     /**
     * 当行记录信息的某一列的值改变后,属性面板的组件基础属性相应的值更新
     * @param {} componentItemId:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setBasicParamsForInteractionField:function(componentItemId,keys,newValue){
        var me=this;
        var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
            //属性面板ItemId
            var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
            //组件属性面板
            var panel = me.getComponent('east').getComponent(panelItemId);
            var basic = panel.getComponent("basicParams");
            var com = basic.getComponent(keys);
            com.setValue(newValue);
    }, 
     /**
     * 当行记录信息的某一列的值改变后,属性面板的组件特有属性相应的值更新
     * @param {} componentItemId:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setOtherParamsForInteractionField:function(componentItemId,keys,newValue){
        var me=this;
        var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
            //属性面板ItemId
            var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
            //组件属性面板
            var panel = me.getComponent('east').getComponent(panelItemId);
            var others=panel.getComponent("otherParams");
            var interactionField=others.getComponent(keys);
                interactionField.setValue(newValue);
    },   
    /**
     * 展示区域的某一控件的显示名称更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setComponentFieldLabel:function(InteractionField,newValue){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
            tempItem.setFieldLabel(newValue);
    },
    /**
     * 展示区域的按钮控件的显示名称更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setComponentFieldText:function(InteractionField,newValue){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
            tempItem.setText(newValue);
    },
    /**
     * 展示区域的某一控件的只读属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setComponentReadOnly:function(InteractionField,newValue){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
            tempItem.setReadOnly(newValue);
    },
    /**
     * 展示区域的某一控件的隐藏属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setComponentHidden:function(InteractionField,newValue){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
            tempItem.hidden=newValue;
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
     * 重新生成展示区域的某一个控件
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值;record
     */
    setchangeComponent:function(InteractionField,record){
        var me=this;
        var center=me.getCenterCom();
        var tempItem= me.getCenterCom().getComponent(InteractionField);
        var labelStyle=null;
        var owner = center.ownerCt;
            center.remove(tempItem);
        //重新生成新的控件
        var com =me.newfromItem(InteractionField,record);
            center.add(com);
    },
    /**
     * 展示区域的某一控件的显示名称宽度属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值;record
     */
    setComponentLabFont:function(InteractionField,record,labelStyle){
        var me=this;
        var center=me.getCenterCom();
        var tempItem= me.getCenterCom().getComponent(InteractionField);
        var labelStyle=labelStyle;
        var owner = center.ownerCt;
            center.remove(tempItem);
            
        //重新生成新的控件    
        var com =me.newfromItemForLabFont(InteractionField,record,labelStyle);
            center.add(com);
    },
    //==============================某一控件属性更新==============================
    /**
     * 展示区域里的某一控件重新生成
     * @private
     * @return {}
     */
    newfromItem:function(InteractionField,record){
        var me = this,com =null,data2=[] ;
        com = me.createComponentsByType(record.get('Type'),record);
        var type=record.get('Type');
            com.type = record.get('Type');
            //公共属性
            com.fieldLabel=record.get('DisplayName');
            com.labelWidth=record.get('LabelWidth');
            com.itemId = record.get('InteractionField');
            com.labelStyle=record.get('LabFont');
            com.width = record.get('Width');
            com.height = record.get('Height');
            
            com.readOnly=record.get('IsReadOnly');
            com.hidden=record.get('IsHidden');
            //是否有边框Ext.AbstractComponent
 
            if(me.hasBorder){
                com.border = 1;
            }else{
            com.border = 0;
            }
            com.style = {
                borderColor:'red',
                borderStyle:'dashed'
            };
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
            com.draggable = true;//注释这一行,改变大小事件失效,拖放事件生效 long10
            me.setComListeners(com);//组件监听
            return com;
    },
     /**
     * 展示区域里的某一控件重新生成:控件样式属性修改后
     * @private
     * @return {}
     */
    newfromItemForLabFont:function(InteractionField,record,labelStyle){
        var me = this,com =null,data2=[] ;
        com = me.createComponentsByType(record.get('Type'),record);
        var type=record.get('Type');
            com.type = record.get('Type');
            //公共属性
            com.fieldLabel=record.get('DisplayName');
            com.labelWidth=record.get('LabelWidth');
            com.itemId = record.get('InteractionField');
            com.labelStyle=labelStyle;
            com.width = record.get('Width');
            com.height = record.get('Height');
            //是否有边框
            if(me.hasBorder){
                com.border = 1;
            }else{
            com.border = 0;
            }
            com.style = {
                borderColor:'red',
                borderStyle:'dashed'
            };
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
            com.draggable = true;//注释这一行,改变大小事件失效,拖放事件生效 long10
            me.setComListeners(com);//组件监听
            return com;
    },
    /**
     * 展示区域里的某一控件重新生成:控件显示名称宽度属性修改后
     * @private
     * @return {}
     */
    newfromItemForLabelWidth:function(InteractionField,newValue,oldValue,record){
        var me = this;
        var com = me.createComponentsByType(record.get('Type'),record);
        var type=record.get('Type');
        var tempValue = newValue-oldValue;
            com.type = record.get('Type');

            //公共属性
            com.itemId = record.get('InteractionField');
            com.labelStyle=record.get('LabFont');
            com.height = record.get('Height')
            com.width = record.get('Width')+tempValue;
            com.labelWidth=newValue;
            com.draggable = true;//注释这一行,改变大小事件失效,拖放事件生效 long10
            //是否有边框
            if(me.hasBorder){
                com.border = 1;
            }else{
            com.border = 0;
            }
            com.style = {
                borderColor:'red',
                borderStyle:'dashed'
            };
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
            me.setComListeners(com);//组件监听
            return com;
            
    },
    /**
     * 单、复组属性列表
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
                {text:'交互字段',dataIndex:'InteractionField',editor:{readOnly:true},disabled:true},
                {text:'显示名称',dataIndex:'DisplayName',
                    editor:{
                        allowBlank:true,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setColumnParamsRecord(InteractionField,'DisplayName',newValue);
                            }
                        }
                    }
                },                
                {text:'数据项类型',dataIndex:'Type',width:110,align: 'center',
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
                {text:'宽度',dataIndex:'Width',width:70,value:me.itemWidth,align:'center',
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
                                me.setBasicParamsForInteractionField(InteractionField,'myWidth',newValue);
                                
                            }
                        }
                    }
                },                
                 {text:'高度',dataIndex:'Height',width:70,align:'center',
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
                                me.setSouthRecordForNumberfield(InteractionField,'Height',newValue);
                                me.setComponentHeight(InteractionField,newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'myHeight',newValue);

                            }
                        }
                    }
                },
                {text:'列数',dataIndex:'Columns',width:100,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                 me.setSouthRecordForNumberfield(InteractionField,'Columns',newValue);
                            }
                        }
                    }
                },
                {text:'列宽',dataIndex:'ColumnWidth',width:100,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                 me.setSouthRecordForNumberfield(InteractionField,'ColumnWidth',newValue);
                            }
                        }
                    }
                }, 
                {text:'显示名称宽度',dataIndex:'LabelWidth',width:70,align: 'center',
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

                                me.setColumnParamsRecord(InteractionField,'labelWidth',newValue);
                                var record2=record;
                                me.setComponentLabWidth(InteractionField,newValue,oldValue,record);
                                
                            }
                        }
                    }
                },
                {xtype:'actioncolumn',text:'显示名称字体',width:70,align:'center',
                    items:[{
                        iconCls:'build-img-font-configuration hand',
                        tooltip: '显示名称字体设置',
                        handler: function(grid, rowIndex, colIndex) {
                            var record = grid.getStore().getAt(rowIndex);
                            var InteractionField = record.get('InteractionField');
                            me.OpenCategoryWinTwo(InteractionField);
                            me.setColumnParamsRecord(InteractionField,'LabFont',record.get('LabFont'));
                        }
                    }]
                },
                {text:'显示名称字体内容',dataIndex:'LabFont',hidden:true},
                {text:'数据地址',dataIndex:'ServerUrl',width:70},
                {text:'默认值',dataIndex:'defaultValue',width:50,hidden:true},
                {text:'值字段',dataIndex:'valueField',hidden:true},
                {text:'显示字段',dataIndex:'textField',hidden:true},
                {text:'行列方式',dataIndex:'RawOrCol',hidden:true},
                
                {text:'确定/取消按钮的显示/隐藏',dataIndex:'btnHidden',hidden:true},
                {text:'是否隐藏确定按钮',dataIndex:'butOkBool',hidden:true},
                {text:'是否隐藏取消按钮',dataIndex:'butCancleBoll',hidden:true},
                {text:'组名',dataIndex:'checkboxgroupName',hidden:true}
                
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
        var dataObj = me.createDataObj();
        
        var formParamsPanel = {
            xtype:'form',
            itemId:'center' + me.ParamsPanelItemIdSuffix,
            title:'属性配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[appInfo,panelWH,title,dataObj]
        };
        
        var com = {
            xtype:'form',
            title:'选择器属性配置',
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
                itemId:'appCode',name:'appCode',labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textfield',fieldLabel:'中文名称',labelWidth:55,anchor:'100%',
                itemId:'appCName',labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",name:'appCName'
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
                xtype:'textfield',fieldLabel:'显示名称',labelWidth:55,value:'选择器配置',anchor:'100%',
                itemId:'titleText',name:'titleText'
            },{
                xtype:'fieldcontainer',layout:'hbox',
                itemId:'titleStyle',
                items:[{
                    xtype:'label',text:'字体设置:',width:55,margin:'2 0 2 0'
                },{
                    xtype:'textfield',hidden:true,value:'',
                    itemId:'titleStyle',name:'titleStyle'
                },{
                    xtype:'image',itemId:'configuration',
                    imgCls:'build-img-font-configuration hand',
                    width:16,height:16,
                    margin:'2 0 2 5',cls:'hand',
                    listeners:{
                        click:{
                            element:'el',
                            fn:function(){
                                //调用生成标题字体设置组件longfc
                                me.OpenCategoryWin();
                            }
                        }
                    }
                }]
            }]
        };
        
        return com;
    },
    grouptype:[
        ['','请选择'],
        ['uxradiogroup','单选组'],
        ['panelcheckboxgroup','复选组']
    ],
    btnHidden:[
        ['','请选择'],
        ['false','显示确定取消按钮'],
        ['true','隐藏确定取消按钮']
    ],
    /**
     * 数据对象
     * @private
     * @return {}lfc
     */
    createDataObj:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'选择器配置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'dataObject',
            items:[
            {
            xtype:'combobox',
            fieldLabel:'选择类型',
            itemId:'grouptype',
            name:'grouptype',
            labelWidth:55,anchor:'100%',
            editable:true,typeAhead:true,
            forceSelection:true,
            queryMode:'local',
            labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
            emptyText:'请选择选择器类型',
            displayField:'text',
            valueField:'value',
            store:new Ext.data.SimpleStore({ 
                fields:['value','text'],
                data:me.grouptype 
            }),
            listeners:{
                select:function(owner,records,eOpts){
                
                }
            }
            }, 
             {
                xtype:'combobox',fieldLabel:'数据对象',
                itemId:'objectName',
                name:'objectName',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
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
                    select:function(owner,records,eOpts){
                       var newValue=owner.getValue();
                       if(me.isJustOpen==false){
                            me.objectChange(owner,newValue);
                        } 
                    }
                }
            },                                      
            {xtype:'combobox',fieldLabel:'值字段',
                itemId:'valuePanelField',
                name:'valuePanelField',
                labelWidth:55,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择值字段',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    }
                }),
                listeners:{
                    select:function(owner,records,eOpts){
                    //值字段赋值
                    var objectName=me.getObjectName();
                    var componentItemId=objectName.getValue();
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    }
                }
             },
             {xtype:'combobox',fieldLabel:'显示字段',
                itemId:'displayPanelValue',
                name:'displayPanelValue',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                labelWidth:55,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择显示值字段',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    }
                }),
                listeners:{
                    select:function(owner,records,eOpts){
                    //值字段赋值
                    var objectName=me.getObjectName();
                    var componentItemId=objectName.getValue();
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',value);
                        }
                    }
                }
                
             },
             {
                xtype:'combobox',fieldLabel:'获取数据',
                itemId:'getDataServerUrl',
                name:'getDataServerUrl',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                labelWidth:55,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择数据地址服务',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                listeners:{
                    select:function(owner,records,eOpts){
                    //值字段赋值
                    var objectName=me.getObjectName();
                    var componentItemId=objectName.getValue();
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var value = newValue.split("?")[0];
                            me.setColumnParamsRecord(componentItemId,'ServerUrl',value);
                        }
                    }
                },
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    },
                    listeners:{
                        beforeload:function(store,operation,eOpts){
                            var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                            var dataObject = formParamsPanel.getComponent('dataObject');
                            var objectName = me.getObjectName();
                            store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=List" + objectName.value;
                        },
                        load:function(store,records,successful,eOpts){
                            if(records != null){
                                var east = me.getComponent('east');
                                var southitemId = me.getComponent('south');
                                var panel = east.getComponent('center'+ me.ParamsPanelItemIdSuffix);
                                var serverUrl = me.getDataServerUrl();
                               
                                var defaultValue = me.getDefaultValueRadio();
                                var url= getRootPath() + "/" + records[0].get(me.objectServerValueField).split("?")[0] + "?isPlanish=true&where=";
                                //设置属性列表对应值
                                var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,southitemId);
                                if(record!=null){
                                var value=record.get("valueField");
                                var text=record.get("textField");
                                var mystore=me.GetComboboxItems(url,value,text);
                                defaultValue.store=mystore;
                                }
                            }
                        }
                    }
                })
            }, 
             {
                xtype:'combobox',fieldLabel:'默认选中',
                itemId:'defaultValue_radio',
                name:'defaultValue_radio',
                hidden:true,
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                store:new Ext.data.Store({
                    fields:['text','value']
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=radioItem.getItemId();
                        if(newValue && newValue != ""){
                            //给组件的默认值赋值
                            var arr="'"+newValue+"'";
                            var values="{"+groupName+":["+arr+"]}";
                            var arrJson=Ext.decode(values);
                            radioItem.setValue(arrJson);
                            var objectName=me.getObjectName();
                            var componentItemId=objectName.getValue();
                            me.setColumnParamsRecord(componentItemId,'defaultValue',newValue+",");
                        }
                    }
                }
            
            },
            {
	            xtype:'panel',itemId:'objectPropertyPanel',border:false,
	            dockedItems:[{
	                xtype:'toolbar',
	                style:{background:'#fff'},
	                itemId:'objectPropertyToolbar',
	                items:[{
	                    xtype:'button',text:'确定',itemId:'objectPropertyOK',
	                    iconCls:'build-button-ok',
	                    listeners:{
	                        click:function(){
	                            if(me.isJustOpen==false){
	                                me.objectPropertyOKClick(); 
	                            }
	                        }
	                    }
	                }]
	            }]
             },
             {
            xtype:'combobox',
            fieldLabel:'按钮设置',
            itemId:'btnHidden',
            name:'btnHidden',
            labelWidth:55,anchor:'100%',
            editable:true,typeAhead:true,
            forceSelection:true,
            queryMode:'local',
            //labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
            emptyText:'请选择显示或者隐藏按钮',
            displayField:'text',
            valueField:'value',
            store:new Ext.data.SimpleStore({ 
                fields:['value','text'],
                data:me.btnHidden 
            }),
            listeners:{
                select:function(owner,records,eOpts){
                   var newValue=owner.getValue();
                   if(me.isJustOpen==false){
                    var objectName=me.getObjectName();
                    var componentItemId=objectName.getValue();
                    me.setColumnParamsRecord(componentItemId,'btnHidden',newValue);
                    } 
                }
            }
            },
            {
             xtype:'textfield',fieldLabel:'默认条件',labelWidth:55,value:'',
             itemId:'defaultParams',name:'defaultParams'
            },
            {
             xtype:'textfield',fieldLabel:'组名',labelWidth:55,value:'',
             itemId:'checkboxgroupName',name:'checkboxgroupName'
            }
            ]
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
            //更换组件属性面板
            me.changeParamsPanel();
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
                CName:params.appCName,//名称
                ModuleOperCode:params.appCode,//功能编码
                ModuleOperInfo:params.appExplain,//功能简介
                InitParameter:params.defaultParams,//初始化参数
                AppType:me.appType,//应用类型
                BuildType:1,//构建类型
                DesignCode:me.JsonToStr(appParams),//设计代码
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
    /**
     * 另存按钮事件处理
     * @private
     */
    saveAs:function(){
        var me = this;
        me.save(false);
    },
    //=====================属性面板事件方法=======================
    /**
     * 对象更改事件处理
     * @private
     */
    objectChange:function(owner,newValue){
        var me = this;
        if(me.isJustOpen==false){
            me.removeSouthAndCenterAll();
        }
        var dataObject = owner.ownerCt;
        //获取获取数据服务列表
        var getDataServerUrl = me.getDataServerUrl();
        getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=List" + newValue;        
        getDataServerUrl.store.load();
        
        //获取新增数据服务列表值字段
        var getInterfact = me.getValuePanelField();
        getInterfact.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        getInterfact.store.load();
        
        //获取新增数据服务列表值字段
        var displayPanelValue = dataObject.getComponent('displayPanelValue');
        displayPanelValue.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        displayPanelValue.store.load();
    },

    /**
     * 对象树的勾选过,完成后点击确定按钮处理
     * @private
     * @param {} node
     * @param {} checked
     */
    objectPropertyOKClick:function(){
        var me = this;
        var objectName=me.getObjectName();
        var type=me.getEastGroupType();
        if(type==undefined||type=='undefined'||type==''){
            Ext.Msg.alert('提示','请配置选择器类型！');
            return ;
        }
        //获取数据地址itemID
        var serverUrl=me.getDataServerUrl();
        //获取值字段itemID
        var keyValue=me.getValuePanelField();
        //获取显示字段itemID
        var text=me.getDisplayPanelValue();
        //获取默认值itemID
        var itemID=me.getDefaultValueRadio();
        
        if(objectName.getValue()==='' || objectName.getValue()==null)
         {
            Ext.Msg.alert('提示','请配置数据对象！');
            return ;
         }
         if(serverUrl.getValue()==='' || serverUrl.getValue()==null)
         {
            Ext.Msg.alert('提示','请配置数据服务！');
            return ;
         }
         if(keyValue.getValue()===''|| keyValue.getValue()==null)
         {
            Ext.Msg.alert('提示','请配置值字段！');
            return ;
         }
         
         if(text.getValue()===''|| text.getValue()==null)
         {
            Ext.Msg.alert('提示','请配置显示值字段！');
            return ;
         }
         
        if(me.isJustOpen==false){
            me.removeSouthAndCenterAll();
        } 
         
        var store = me.getComponent('south').store;
        //取值字段做交互字段
        var InteractionField=objectName.getValue();
        
        var index = store.findExact('InteractionField',InteractionField);
        if(index === -1){//新建不存在的对象
            var rec = ('Ext.data.Model',{
            DisplayName:objectName.getRawValue( ),
            InteractionField:InteractionField,
            LabelWidth:55,//显示名称宽度
            ServerUrl:serverUrl.getValue().split('?')[0],
            valueField:keyValue.getValue(),
            textField:text.getValue(),
            LabFont:'',//显示名称字体内容
            Type:type,//数据项类型
            ColumnWidth:120,
            Height:260,        //容器高度像素,默认值为260
            Width:600,       //容器宽度像素,,默认值为600
            Columns:5,
            defaultValue:itemID.getValue(),
            RawOrCol:'columns',
            Height: 220,
            
            checkboxgroupName:"checkboxgroupName",//复选组组名
            btnHidden: false,//确定或者取消按钮的显示false或者隐藏true,默认值为显示false
            butOkBool:false,     //是否隐藏确定按钮
            butCancleBoll:false, //是否隐藏取消按钮
            
            Width:666  //数据项宽度
                    });
           store.add(rec);
            }
  
        me.browse();//展示效果
    },
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
        var bgcolor=params.panelStyle;    //表单背景颜色        
       
        var form = {
            xtype:'form',
            itemId:'center',
            layout:'absolute',
            autoScroll:true,
            title:title,
            html:formHtml,
            width:width,
            height:height,
            bodyCls:bgcolor, //获取样式表参数
            resizable:{handles:'s e'}
        };
        //加载数据方法
        form.loadData = function(){
            //数据服务地址
            var params = me.getPanelParams();
            var url = getRootPath() + "/" + me.getDataUrl();
            if(params.testParams){
                url =  url + "&" + params.testParams;
            }
            
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
        
        form.header = {
            listeners:{
                click:function(){
                    //切换组件属性配置面板
                    me.switchParamsPanel('center');
                }
            }
        };
        
        form.listeners = {
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                //表单宽度和高度赋值
                formParamsPanel.getForm().setValues({Width:width,Height:height});
            }
        };
        
        var buttons = ['->'];
        
        if(params.hasSaveButton){//保存按钮
            var item = {
                xtype:'button',text:'保存',itemId:'save',
                iconCls:'build-button-save',
                handler:function(){
                    var addDataServerUrl = params.addDataServerUrl;
                    if(addDataServerUrl){
                        Ext.Msg.alert('提示','保存数据服务地址='+addDataServerUrl);
                    }else{
                        Ext.Msg.alert('提示','<b style="color:red">'+'【没有配置保存数据服务地址！】</b>');
                    }
                }
            };
            buttons.push(item);
        }
        
        if(params.hasResetButton){//重置按钮
            var item = {
                xtype:'button',text:'重置',itemId:'reset',
                iconCls:'build-button-refresh',
                handler:function(){
                    var form = this.ownerCt.ownerCt;
                    form.getForm().reset();
                }
            };
            buttons.push(item);
        };
        
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
            com.style = {
                borderColor:'red',
                borderStyle:'dashed'
            };
            //是否有边框
            if(me.hasBorder){
                com.border = 1;
            }
            
            com.draggable = false;//注释这一行,改变大小事件失效,拖放事件生效
            com.resizable = {handles:'w e'};//与move事件监听冲突
            arr.basicComArr.push(com);
        }
        //合并组件数组
        var comArr = arr.basicComArr.concat(arr.otherComArr);
        
        var coms = [];
        
        for(var i=0;i<comArr.length;i++){
            var com = comArr[i];
           
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
            var type=record.get('Type');
	        if(type==undefined||type=='undefined'||type==''){
	            Ext.Msg.alert('提示','请配置选择器类型！');
	            return ;
	        }
            var com = me.createComponentsByType(type,record);
            com.type = type;
            //公共属性
            com.itemId = record.get('InteractionField');
            
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
            coms.push(com);
        }
        
        return coms;
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
        
        if(type == 'textfield'){//文本框
            com = me.createTextfield(record);
        }else if(type == 'panelcheckboxgroup'){
            com = me.createComfield(record);
        }else if(type == 'uxradiogroup'){
            com = me.createComfield(record);
        }else if(type == 'button'){//按钮
            com = me.createButton(record);
        }
        
        return com;
    },

    /**
     * 创建文本框组件
     * @private
     * @param {} record
     * @return {}
     */
    createTextfield:function(record){
        var com = {
            xtype:'textfield',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width')
        };
        return com;
    },
    /**
     * 创建组件
     * @private
     * @param {} record
     * @return {}
     */
    createComfield:function(record){
        var tempColumnWidth=(record.get('ColumnWidth')==0)?120:record.get('ColumnWidth');
        var tempColumns=(record.get('Columns')==0)?7:record.get('Columns');
        var myServerUrl=record.get('ServerUrl');
        if(myServerUrl==""){
            Ext.Msg.alert('提示','没有配置数据服务地址！');
            return null;
        }else{
            myServerUrl=(getRootPath() + '/' + myServerUrl.split('?')[0] + '?isPlanish=true&where=');
        }
        
        var comType=record.get("Type");
        if(comType==undefined||comType=='undefined'||comType==''){
            Ext.Msg.alert('提示','请配置选择器类型！');
            return ;
        }
       
        var labFieldWidth=record.get("LabelWidth");
        var labFieldAlign='left';//records[0].get("LabFieldAlign");
        
        var iKeyField2=record.get("valueField");
        var iTextField2=record.get("textField");
         if(iKeyField2===''|| iKeyField2==null)
         {
            Ext.Msg.alert('提示','请配置值字段！');
            return ;
         }
         
         if(iTextField2===''|| iTextField2==null)
         {
            Ext.Msg.alert('提示','请配置显示字段！');
            return ;
         }
         
         var btnHidden2=record.get("btnHidden");
        if(btnHidden2==''||btnHidden2==null)
        {
            btnHidden2==false;
        }
        var butOkBool2=record.get("butOkBool");
        if(butOkBool2==''||butOkBool2==null)
        {
            butOkBool2==false;
        }
        var butCancleBoll2=record.get("butCancleBoll");
        if(butCancleBoll2==''||butCancleBoll2==null)
        {
            butCancleBoll2==false;
        }
        
        var myCheckboxgroupName=record.get("checkboxgroupName");
        if(myCheckboxgroupName==''||myCheckboxgroupName==null)
        {
            myCheckboxgroupName==record.get("InteractionField");
        }
        var com ={
            xtype: comType,
            border:false,
            title: '',//标题,默认值为''
            
            labField: record.get('DisplayName'),
            height: record.get('Height'),
            width:record.get('Width'),
            name:record.get('InteractionField'),//复选框组名称
            dataSourceType:'server', //数据源类型（本地:local、服务器:server）
		    labFieldWidth:record.get('labFieldWidth'),   //数据组的label文本宽度,默认值为100
		    labFieldAlign:'left',   //文本对齐方式,默认值为left
            serverUrl:myServerUrl,   //后台服务地址
            iKeyField:record.get('valueField') , //keyField数据项匹配字段,
            iTextField:record.get('textField'),   //textField数据项匹配字段
            
            colRowCount:tempColumns,     //容器行/列数量,默认值为5
            columnWidth :tempColumnWidth,

            btnHidden: btnHidden2,//确定或者取消按钮的显示false或者隐藏true,默认值为显示false
            butCancleBoll:butCancleBoll2,
            butOkBool:butOkBool2,
            checkboxgroupName:myCheckboxgroupName,
            vertical: false
           
        }; 
          return com;
    },
    /**
     * 创建按钮组件
     * @private
     * @param {} record
     * @return {}
     */
    createButton:function(record){
        var com = {
            xtype:'button',
            name:record.get('InteractionField'),
            width:record.get('Width'),
            text:record.get('DisplayName')
        };
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
        me.switchParamsPanel('center');
        //删除数据项属性列表中的当前数据项数据
        me.removeSouthValueByKeyValue('InteractionField',componentItemId);
    },
    //=====================组件属性面板的创建与删除=======================
    /**
     * 切换组件属性配置面板
     * @private
     * @param {} componentItemId
     */
    switchParamsPanel:function(componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var east = me.getComponent('east');
        var panel = east.getComponent(panelItemId);
        
        if(panel && componentItemId != me.OpenedParamsPanel){
            var OpenedParamsPanel = east.getComponent(me.OpenedParamsPanel + me.ParamsPanelItemIdSuffix);
            if(OpenedParamsPanel){
                OpenedParamsPanel.hide();//隐藏
            }
            east.setTitle(panel.title);//设置标题
            
            me.setParamsPanelValues(componentItemId);//给属性面板赋值
            
            panel.show();//打开
            me.OpenedParamsPanel = componentItemId;
        }
    },
    /**
     * 更换组件属性面板
     * @private
     */
    changeParamsPanel:function(){
        var me = this;
        var centerItemId = 'center' + me.ParamsPanelItemIdSuffix;
        var east = me.getComponent('east');
        var items = east.items.items;
        
        var removeArr = [];//需要删除的属性面板
        
        for(var i in items){
            if(items[i].itemId != centerItemId){
                removeArr.push(items[i].itemId);
            }else{
                east.setTitle(items[i].title);
                me.OpenedParamsPanel = "center";
                items[i].show();
            }
        }
        for(var i in removeArr){
            east.remove(removeArr[i]);
        }
        //所有组件信息
        var southRecords = me.getSouthRecords();
        for(var i in southRecords){
            var record = southRecords[i];
            //添加组件属性面板
            me.addParamsPanel(record.get('Type'),record.get('InteractionField'),record.get('DisplayName'));
        }
    },
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
    
    /**
     * 添加组件属性面板
     * @private
     * @param {} type
     * @param {} componentItemId
     * @param {} title
     */
    addParamsPanel:function(type,componentItemId,title){
        var me = this;
        var east = me.getComponent('east');
        //创建组件属性面板
        var panel = me.createParamsPanel(type,componentItemId,title);
        //添加面板
        east.add(panel);
    },
    /**
     * longfc:修改
     * 创建组件属性面板
     * @private
     * @param {} type
     * @param {} componentItemId
     * @param {} title
     * @return {}
     */
    createParamsPanel:function(type,componentItemId,title){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        
        var com = {
            xtype:'form',
            itemId:panelItemId,
            title:title,
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            hidden:true
        };
        
        //组件基础属性
        var basicItems =[];
        //显示名称
        basicItems =me.createBasicItems(componentItemId);

        //组件特有属性
        var otherItems = [];
        
        if(type == 'textfield'){//文本框
        }else if(type == 'panelcheckboxgroup'){
            //otherItems = me.createCheckboxfieldItems(componentItemId);
        }else if(type == 'uxradiogroup'){
            //otherItems = me.createCheckboxfieldItems(componentItemId);
        }else if(type == 'button'){//按钮
            //不做处理
        }
        //合并属性(基本属性加上特殊的属性合并渲染)
        var items = basicItems.concat(otherItems);
        com.items = items;
        return com;
    },
    /**
     * 组件基础属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createBasicItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'组件基础属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'basicParams',
            items:[{
                xtype:'numberfield',fieldLabel:'高度',name:'myHeight',labelWidth:55,anchor:'95%',
                itemId:'myHeight',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Height',this.value);
                        //更新设置展示区域的单选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(undefined,this.value);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        me.setColumnParamsRecord(componentItemId,'Height',newValue);
                        //更新设置展示区域的单选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(undefined,newValue);
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'宽度',name:'myWidth',labelWidth:55,anchor:'95%',
                itemId:'myWidth',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Width',this.value);
                        //更新设置展示区域的单选框的宽度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(this.value);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        me.setColumnParamsRecord(componentItemId,'Width',newValue);
                        //更新设置展示区域的单选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(newValue);
                    }
                }
            },{
                xtype:'fieldcontainer',layout:'hbox',
                itemId:'basicStyle',
                items:[{
                    xtype:'label',text:'字体设置:',width:55,margin:'2 0 2 0'
                },{
                    xtype:'textfield',hidden:true,value:'',
                    itemId:'basicItemStyle',name:'basicItemStyle'
                },{
                    xtype:'image',itemId:'basicConfiguration',
                    imgCls:'build-img-font-configuration hand',
                    width:16,height:16,
                    margin:'2 0 2 5',cls:'hand',
                    listeners:{
                        click:{
                            element:'el',
                            fn:function(){
                                //调用生成标题字体设置组件longfc
                                me.OpenCategoryWinTwo(componentItemId);
                            }
                        }
                    }
                }]
            }
            ]
        }
        ];
        return items;
    },
    
    //=====================设置获取参数=======================
    /***
     * 获取构建单、复选组的类型
     * @return {}
     */
    getEastGroupType:function(){
        var me=this;
        var grouptype = me.getDataObject().getComponent('grouptype');
        var result=grouptype.getValue();
        return result;
    },
    /***
     * 获取构建单、复选组的类型
     * @return {}
     */
    getGroupType:function(){
        var me=this;
        var grouptype = me.getDataObject().getComponent('grouptype');
        return grouptype;
    },
    getDataObject:function(){
    var me = this;
    var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
    return dataObject;
    },
    getDataServerUrl:function(){
        var me = this;
        var dataObject=me.getDataObject();
        var serverUrl=dataObject.getComponent('getDataServerUrl');
        return serverUrl;
    },
    getValuePanelField:function(){
        var me = this;
        var dataObject=me.getDataObject();
        var valuePanelField=dataObject.getComponent('valuePanelField');
        return valuePanelField;
    },
    getDisplayPanelValue:function(){
        var me = this;
        var dataObject=me.getDataObject();
        var displayPanelValue=dataObject.getComponent('displayPanelValue');
        return displayPanelValue;
    },
    getDefaultValueRadio:function(){
        var me = this;
        var dataObject=me.getDataObject();
        var defaultValueRadio=dataObject.getComponent('defaultValue_radio');
        return defaultValueRadio;
    },
    getObjectName:function(){
        var me = this;
        var dataObject=me.getDataObject();
        var objectName=dataObject.getComponent('objectName');
        return objectName;
    }, 
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
     * 其他的调用调用setSouthRecordByKeyValue或setSouthRecord
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
            url = url + "?isPlanish=true&fields=" + fields;
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
     * 给属性面板赋值
     * @private
     * @param {} record
     */
    setParamsPanelValues:function(componentItemId){
        var me = this;
        if(componentItemId != "center"){
            var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
            me.setBasicParamsPanelValues(componentItemId,record);
            me.setParamsPanelValuesByType(componentItemId,record);
        }
    },
    /**
     * 属性面板基础数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setBasicParamsPanelValues:function(componentItemId,record){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var basic = panel.getComponent("basicParams");

        var myWidth = basic.getComponent("myWidth");
        var myHeight = basic.getComponent("myHeight");

        myWidth.setValue(record.get('Width'));
        myHeight.setValue(record.get('Height'));

    },
    /**
     * 属性面板特有数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setParamsPanelValuesByType:function(componentItemId,record){
        var me = this;
        var type = record.get('Type');
        if(type == "datacombobox"){
            me.setDataComboParamsPanelValue(componentItemId,record);
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
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                me.setColumnParamsRecord(com.itemId,'Width',width);
                me.setColumnParamsRecord(com.itemId,'Height',height);
             
            },
            click:{
                element:'el',
                fn:function(e){
                    //切换组件属性配置面板
                    me.switchParamsPanel(com.itemId);
                }
            },
            contextmenu:{
                element:'el',
                fn:function(e,t,eOpts){
                    //禁用浏览器的右键相应事件 
                    e.preventDefault();e.stopEvent();
                    //右键菜单
                    new Ext.menu.Menu({
                        items:[{
                            text:"删除",iconCls:'delete',
                            handler:function(){
                                me.removeComponent(com.itemId);
                            }
                        }]
                    }).showAt(e.getXY());//让右键菜单跟随鼠标位置
                }
            }
        };
        
        return com;
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
            
            {name:'labField',type:'string'},//数据项的label名称,默认值为''
            {name:'dataSourceType',type:'string'},//数据源类型（本地、服务器）
            {name:'layoutType',type:'string'},//容器布局类型(行rows/列布局columns),默认值为columns列布局
            
            
            {name:'LabelWidth',type:'int'},//显示名称宽度
            {name:'LabFieldAlign',type:'string'},//文本对齐方式
            {name:'LabFont',type:'string'},//显示名称字体内容
            {name:'Type',type:'string'},//数据项类型
            {name:'Width',type:'int'},//数据项宽度
            {name:'Height',type:'int'},//高度
            {name:'ColumnWidth',type:'int'},//列宽
            {name:'Columns',type:'int'},//列数
            {name:'valueField',type:'string'},//值字段(下拉框)
            {name:'textField',type:'string'},//显示字段(下拉框)
            {name:'defaultValue',type:'auto'},//默认值
            {name:'ServerUrl',type:'string'},//数据地址
            {name:'RawOrCol',type:'string'},//行列方式
            
            {name:'checkboxgroupName',type:'string'},//复选组组名
            {name:'btnHidden',type:'bool'},//确定或者取消按钮的显示false或者隐藏true,默认值为显示false
            {name:'butOkBool',type:'bool'},
            {name:'butCancleBoll',type:'bool'}
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
        panelParams.openLayoutType = false;
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
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObject = paramsPanel.getComponent('dataObject');
        
        var callback = function(appInfo){
            var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
            var panelParams = appParams.panelParams;
            var southParams = appParams.southParams;
            me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];

            //赋值
            me.setSouthRecordByArray(southParams);
            me.setObjData();
            me.setPanelParams(panelParams);
            //获取新增数据服务列表值字段
            var objectName=me.getObjectName();
            
            var grouptype=me.getGroupType();
            grouptype.value=panelParams.grouptype;
            
            //获取获取数据服务列表
	        var getDataServerUrl = me.getDataServerUrl();
	        getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=List" + objectName.getValue();        
	        getDataServerUrl.store.load();
            
            //获取值字段
	        var getInterfact = me.getValuePanelField();
	        getInterfact.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + objectName.getValue();
	        getInterfact.store.load();
	        
	        //获取显示字段
	        var displayPanelValue = dataObject.getComponent('displayPanelValue');
	        displayPanelValue.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + objectName.getValue();
	        displayPanelValue.store.load();
        
            //获取获取数据服务列表
            var getDataServerUrl = me.getDataServerUrl();
            getDataServerUrl.value = panelParams.getDataServerUrl;
            
            var keyValue=me.getValuePanelField();
            keyValue.value = panelParams.valuePanelField;
            
            var textValue=me.getDisplayPanelValue();
            textValue.value = panelParams.displayPanelValue;
            
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
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        //数据对象
        var dataObject = paramsPanel.getComponent('dataObject');
        //数据对象类
        var objectName = dataObject.getComponent('objectName');
        objectName.store.load();
    },

    /**
     * 移除展示区域的表单组件及单复选组的列表属性的行记录
     * @private
     * @param {} record
     */
    removeSouthAndCenterAll:function(){
        var me = this;
        var list = me.getComponent('south');//列属性列表
        var store = list.store;
        var count=store.count();
        if(count>0){
            store.removeAll();
        }
        
        var center = me.getCenterCom();
        if(center){
            center.removeAll();
         }
        
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
        var records=me.getSouthRecords();
        
        var comType=records[0].get("Type");
        if(comType==undefined||comType=='undefined'||comType==''){
            Ext.Msg.alert('提示','请配置选择器类型！');
            return ;
        }
        var fieldLabel='';//records[0].get("Type");
        var labField='';//records[0].get("Type");
        var labFieldWidth=records[0].get("LabelWidth");
        var labFieldAlign='left';//records[0].get("LabFieldAlign");
        
        var serverUrl=records[0].get("ServerUrl");
        var iKeyField=records[0].get("valueField");
        var iTextField=records[0].get("textField");
        
        if(serverUrl==='' || serverUrl==null)
         {
            Ext.Msg.alert('提示','请配置数据服务！');
            return ;
         }
         if(iKeyField===''|| iKeyField==null)
         {
            Ext.Msg.alert('提示','请配置值字段！');
            return ;
         }
         
         if(iTextField===''|| iTextField==null)
         {
            Ext.Msg.alert('提示','请配置显示字段！');
            return ;
         }
        var layoutType=records[0].get("RawOrCol");
        var colRowCount=records[0].get("Columns");
        
        var columnWidth=records[0].get("ColumnWidth");
        var columns=records[0].get("Columns");
        
        var btnHidden=records[0].get("btnHidden");
        if(btnHidden==''||btnHidden==null)
        {
            btnHidden==false;
        }
        var butOkBool=records[0].get("butOkBool");
        if(butOkBool==''||butOkBool==null)
        {
            butOkBool==false;
        }
        var butCancleBoll=records[0].get("butCancleBoll");
        if(butCancleBoll==''||butCancleBoll==null)
        {
            butCancleBoll==false;
        }
        
        var checkboxgroupName=records[0].get("checkboxgroupName");
        if(checkboxgroupName==''||checkboxgroupName==null)
        {
            checkboxgroupName==records[0].get("InteractionField");
        }
        var extend='';
        if(comType=='panelcheckboxgroup'){
            extend="extend:'Ext.zhifangux.PanelCheckboxGroup'," ;
        }else{
            extend="extend:'Ext.zhifangux.UXRadioGroup'," ;
        }
        
        var appClass = 
        "Ext.define('" + params.appCode + "',{" + 
        
            extend + 
            
            "alias:'widget." + params.appCode + "'," + 
            "title:'" + params.titleText + "'," + 
            "width:" + params.Width + "," + 
            "height:" + params.Height + "," + 
            
            "fieldLabel:'" + fieldLabel+ "'," + //
            "labFieldWidth:" + labFieldWidth + "," + 
            "labFieldAlign:'" + labFieldAlign+ "'," + //
            
            "serverUrl:getRootPath()+"+"'/'+"+"'"+serverUrl+ "?isPlanish=true'," + //后台服务地址:
            "iKeyField:'" + iKeyField+ "'," + //keyField数据项匹配字段,
            "iTextField:'" + iTextField+ "'," + //textField数据项匹配字段
            
            
            "layoutType:'" + layoutType+ "'," + //容器布局类型(行rows/列布局columns),默认值为columns列布局
            "colRowCount:" + colRowCount+ "," + //容器行/列数量,默认值为5
            "columnWidth:" + columnWidth+ "," + //每一项的宽度,默认值为120
            "columns:" + columns+ "," + //容器行/列数量,默认值为5
            
            "checkboxgroupName:'" + checkboxgroupName+ "'," +
            "btnHidden:" + btnHidden+ "," + //确定或者取消按钮的显示false或者隐藏true,默认值为显示false
            "butOkBool:" + butOkBool+ "," +
            "butCancleBoll:" + butCancleBoll+ "," +
            
            "autoScroll:true," + 
            "type:'add'," + //显示方式add（新增）、edit（修改）、show（查看）
            "dataId:-1," + 
            "internalWhere:'" + params.defaultParams + "'," + //内部hql
            "externalWhere:''," + //外部hql
            
            "layout:'absolute'," ;
            
            appClass=appClass+
            "initComponent:function(){" + 
                "var me=this;" + 
                //组件监听
                "me.listeners=me.listeners||[];"+
	            "me.listeners.onChanged=function(newValue, oldValue, eOpts){" + 
	              //需要放在这里才生效,不能往前放
	           " };"+
	            "me.listeners.onOKCilck=function(com, e, eOpts){" + 
	            "};"+
	            "me.listeners.onCancelCilck=function(com, e, eOpts){" + 
	            "};" + 
                  
                //加载数据的方法
                "me.load=function(where){" + 
                    "me.externalWhere=where;" + 
                    "var w='';" + 
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
                    "me.store.proxy.url=me.serverUrl+'&where='+w;" + 
                    "me.store.load();" + 
                "};" + 
                
                //注册事件
                "me.addEvents('onChanged');" + 
                "me.addEvents('onOKCilck');" + 
                "me.addEvents('onCancelCilck');" ;
                
                //对外公开方法
                appClass = appClass + 
                "me.callParent(arguments);" + 
            "}," + 
            
            "afterRender:function(){" + 
                "var me=this;" + 
                "me.callParent(arguments);" + 
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
            "}"+   
        "});";
       
        return appClass;
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
     * 打开并操作标题字体设置窗体
     * @param {} 
     */
    OpenCategoryWin:function(){
        var me=this;
        var xy=me.getPosition(true);
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
            cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white   //bg-white
            labelcls:'labelcls',//字体属性设置:label样式
            btnHidden: false,//确定或者取消按钮的显示false或者隐藏true
            listeners:{
                        //公开的事件
                        onOKCilck:function(o){
                        //获取设置当前控件的文字属性结果值
                          var lastValue=this.GetValue();
                          var obj ={titleStyle:lastValue};
                          me.setFormValues(obj);
                          var a = me.getPanelParams();
                          var bm = Ext.getCmp('MyRDS_wintemp');
                          if(bm==undefined){ 
                          }else{
                          bm.close();
                          }
                        },
                        //公开的事件
                        onCancelCilck:function(o){
                        //获取设置当前控件的文字属性结果值
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
     * 打开并操作标题字体设置窗体
     * @param {}
     */
    OpenCategoryWinTwo:function(componentItemId){
    var me=this;
    var lastValue=null;
    var xy=me.getPosition(true);
    var myxtype=null;
       if(!myxtype){
        myxtype=Ext.create('Ext.zhifangux.FontStyleSet', {
        itemId:'OpenCategoryWinTwo_id',
        titleAlign :"center",
        autoScroll : true,
        height:270,        //容器高度像素
        width:460,      //容器宽度像素
        bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
        cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white   //bg-white
        labelcls:'labelcls',//字体属性设置:label样式
        btnHidden: false,//确定或者取消按钮的显示false或者隐藏true
        listeners:{
                    onOKCilck:function(o){
                    //获取设置当前控件的文字属性结果值
                      lastValue=this.GetValue();
                      var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                     //重新生成展示区域的某一个控件
                      me.setComponentLabFont(componentItemId,record,lastValue);
                   
                      var bm = Ext.getCmp('OpenCategoryWinTwo_id');
                       if(bm==undefined){ 
                          }else{
                          bm.close();
                          }
                          
                      var InteractionField=componentItemId;
                      me.setColumnParamsRecord(InteractionField,'LabFont',lastValue);
                    },
                    onCancelCilck:function(o){
                    //获取设置当前控件的文字属性结果值
                      var bm = Ext.getCmp('OpenCategoryWinTwo_id');
                      if(bm==undefined){ 
                          }else{
                          bm.close();
                          }
                     me.setColumnParamsRecord(InteractionField,'LabFont',"");
                    }
                } 
        });
       }
         me.win2=null;
         me.win2 = Ext.create('widget.window', {
                title:me.winTitle,
                id:"OpenCategoryWinTwo_id",
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
            me.win2.show();
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
    }
    
});