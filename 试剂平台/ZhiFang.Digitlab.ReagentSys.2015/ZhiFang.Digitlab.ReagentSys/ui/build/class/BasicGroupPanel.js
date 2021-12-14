/**
 * 普通表单构建工具
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
Ext.define('Ext.build.BasicGroupPanel',{
    extend:'Ext.panel.Panel',
    alias: 'widget.BasicGroupPanel',
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
    buildTitle:'普通表单构建工具',
    
    //展示区域的生成构建类型组itemId()
    groupItemId:"groupItemId",
    
    //数据对象配置private
    
    win:null,//创建和弹出选择器窗体
    win2:null,//创建和弹出选择器窗体
    winHeight:270,        //弹出选择器窗体高度像素
    winWidth:460,       //弹出选择器窗体宽度像素
    winTitle:'',        //弹出选择器窗体标题    
    
    //单、复选组属性设置
    DisplayName:'',          //显示名称
    labField:'labfield1',    //默认文本
    labFieldWidth:100,       //文本宽度
    labFieldAlign:'left',    //文本对齐方式
    value:'',                //当前选中值
    itemList:'',             //项目列表
    dataSourceType:'',       //数据源（本地、服务器）
    localData:'',            //当前页面数据集合
    serverUrl:'',            //后台服务地址
    keyField:'',             //数据项值字段
    textField:'',            //数据项显示字段
    itemHeight:600,               //高度
    itemWidth:200,                //宽度
    layoutType:'col',           //布局类型
    columnWidth:120,         //单（复）组列宽
    colRowCount:4,          //行（列）数量   
  
    
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
     * longfc
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
     * longfc
     * 数据项类型
     * @type 
     */
    comTypeList:[
        ['combobox','下拉框'],
        ['textfield','文本框'],
        ['textareafield','文本域'],
        ['numberfield','数字框'],
        ['datefield','日期框'],
        ['timefield','时间框'],
        ['datetimefield','日期时间'],
        ['checkboxgroup','复选框'],
        ['radiogroup','单选框'],
        ['label','纯文本'],
        ['image','图片'],
        ['htmleditor','超文本'],
        ['filefield','文件'],
        ['button','按钮'],
        ['dateintervals','日期区间'],
        ['datacombobox','定值下拉框']
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

     //对齐方式
    AlignTypeList:[
        ['left','左对齐'],
        ['center','居中'],
        ['right','右对齐']
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
    defaultPanelHeight:200,
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
        //me.initListeners();
        me.callParent(arguments);
        me.setAppParams();
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
        south.height = 200;
        east.width = 250;
        
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
                {xtype:'button',text:'更新表单数据',itemId:'loadFormValues',iconCls:'build-button-refresh',margin:'0 4 0 0',
                    handler:function(){
                        me.loadFormValues();
                    }
                },
                '-',
                {xtype:'button',text:'配置背景',itemId:'deployBgHtml',iconCls:'build-button-html',margin:'0 4 0 0',
                    handler:function(){
                        me.deployBgHtml();
                    }
                },
                {xtype:'checkboxfield',itemId:'hasLab',boxLabel:'显示名称',margin:'0 12 0 0',checked:me.hasLab,
                    listeners:{
                        change:function(com,newValue,oldValue,eOpts){
                            me.hasLab = newValue;
                            me.changeFieldLabel(newValue);
                        }
                    }
                },
                {xtype:'checkboxfield',itemId:'hasBorder',boxLabel:'开启边框',margin:'0 4 0 0',checked:me.hasBorder,
                    listeners:{
                        change:function(com,newValue,oldValue,eOpts){
                            me.hasBorder = newValue;
                            me.changeFieldBorder(newValue);
                        }
                    }
                },
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
                title:'单（复）选组',
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
     * 展示区域的某一控件的x轴属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} x:新x轴值;y:新y轴值;
     */
    setComponentX:function(InteractionField,x,y){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
            tempItem.setPosition(x,y);
    },
    /**
     * 展示区域的某一控件的y轴属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} x:新x轴值;y:新y轴值;
     */
    setComponentY:function(InteractionField,x,y){
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
            
        var value=record.get('valueField');
        var text=record.get('textField');
        if(com.xtype== 'checkboxgroup'||com.xtype== 'radiogroup'){
            var groudName=com.itemId;
            var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
            var radioItem=me.getCenterCom().getComponent(InteractionField);
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var defaultValue=record.get('defaultValue');
            var arrStr=[];
            if(defaultValue== ''||defaultValue==null||defaultValue==undefined){
                defaultValue=null;
                var data2= me.GetRadiogroupItems(url,groudName,value,text,defaultValue);
                radioItem.resetItems(data2);
            }else{                
                //单/复选组有默认值时的处理
                defaultValue=""+defaultValue;
                var tempArr=defaultValue.split(",");
                var arrStr2=[];
                for (var i = 0; i <tempArr.length; i++) { 
                    if(tempArr[i]!==null&&tempArr[i]!=="")
                    {
                          arrStr2.push(("\'"+tempArr[i]+"\'"));
                          arrStr.push(tempArr[i]);
                    }
                var data2= me.GetRadiogroupItems(url,groudName,value,text,arrStr);
                    radioItem.resetItems(data2);
                var values="{"+groudName+":["+arrStr2+"]}";
                var arrJson=Ext.decode(values);
                    radioItem.setValue(arrJson);
                }
                }
            }
        }
        if(com.xtype== 'combobox'){
            var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
            var defaultValue=record.get('defaultValue');
            var radioItem=me.getCenterCom().getComponent(InteractionField);
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var st= me.GetComboboxItems(url,value,text);
                radioItem.store=st;
            }
        }
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
        var value=record.get('valueField');
        var text=record.get('textField');
        //单/复选组数据加载处理
        if(com.xtype== 'checkboxgroup'||com.xtype== 'radiogroup'){
            var groudName=com.itemId;
            var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
            var radioItem=me.getCenterCom().getComponent(InteractionField);
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var defaultValue=record.get('defaultValue');
            
            var arrStr=[];
            //单/复选组没有默认值时的处理
            if(defaultValue== ''||defaultValue==null||defaultValue==undefined){
                defaultValue=null;
                var data2= me.GetRadiogroupItems(url,groudName,value,text,defaultValue);
                radioItem.resetItems(data2);
            }else{
                //单/复选组有默认值时的处理
                defaultValue=""+defaultValue;
                var tempArr=defaultValue.split(",");
                var arrStr2=[];
                for (var i = 0; i <tempArr.length; i++) { 
                    if(tempArr[i]!==null&&tempArr[i]!=="")
                    {
                          arrStr2.push(("\'"+tempArr[i]+"\'"));
                          arrStr.push(tempArr[i]);
                    }
                var data2= me.GetRadiogroupItems(url,groudName,value,text,arrStr);
                    radioItem.resetItems(data2);
                var values="{"+groudName+":["+arrStr2+"]}";
                var arrJson=Ext.decode(values);
                    radioItem.setValue(arrJson);
                }
            }
            }
        }
        //下拉列表数据加载处理
        if(com.xtype== 'combobox'){
            var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
            var defaultValue=record.get('defaultValue');
            var radioItem=me.getCenterCom().getComponent(InteractionField);
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var st= me.GetComboboxItems(url,value,text);
            
            if (st!=null || st!=undefine)
            {
               for(var i=0;i<st.data.items.length;i++)
               {
                 if(st.data.items[i].data.value==defaultValue)
                 {
                    var strValue=st.data.items[i].data.text;
                 }
                 
               }
            }
                radioItem.store=st;
                //下拉列表默认值时的处理
             //var strValue=radioItem.store.data.items[defaultValue-1].data.text;
               radioItem.setValue(strValue);
            }
        }
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
        var value=record.get('valueField');
        var text=record.get('textField');
        //单/复选组数据加载处理
        if(com.xtype== 'checkboxgroup'||com.xtype== 'radiogroup'){
            var groudName=com.itemId;
            var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
            var radioItem=me.getCenterCom().getComponent(InteractionField);
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var defaultValue=record.get('defaultValue');
            var arrStr=[];
            //单/复选组没有默认值时的处理
            if(defaultValue== ''||defaultValue==null||defaultValue==undefined){
                defaultValue=null;
                var data2= me.GetRadiogroupItems(url,groudName,value,text,defaultValue);
                radioItem.resetItems(data2);
            }else{
                //单/复选组有默认值时的处理
                defaultValue=""+defaultValue;
                var tempArr=defaultValue.split(",");
                var arrStr2=[];
                for (var i = 0; i <tempArr.length; i++) { 
                    if(tempArr[i]!==null&&tempArr[i]!=="")
                    {
                          arrStr2.push(("\'"+tempArr[i]+"\'"));
                          arrStr.push(tempArr[i]);
                    }
                var data2= me.GetRadiogroupItems(url,groudName,value,text,arrStr);
                    radioItem.resetItems(data2);
                var values="{"+groudName+":["+arrStr2+"]}";
                var arrJson=Ext.decode(values);
                    radioItem.setValue(arrJson);
                }
            }
            }
        }
        //下拉列表数据加载处理
        if(com.xtype== 'combobox'){
            var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
            var defaultValue=record.get('defaultValue');
            var radioItem=me.getCenterCom().getComponent(InteractionField);
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var st= me.GetComboboxItems(url,value,text);
                radioItem.store=st;
                //下拉列表默认值时的处理
                //radioItem.setValue("智方市场部");
            }
        }
    },
    //==============================某一控件属性更新==============================
    /**
     * longfcnew
     * 展示区域里的某一控件重新生成
     * @private
     * @return {}
     */
    newfromItem:function(InteractionField,record){
        var me = this,com =null,data2=[] ;
        com = me.createComponentsByType(record.get('Type'),record);
        var type=record.get('Type');
          if(type== 'combobox'){
            com.valueField = record.get('valueField');
            com.textField = record.get('textField');
            var defaultValue=[];
            defaultValue.push(record.get('defaultValue'));
            if(defaultValue.length>0){
                com.value=defaultValue;
               }
        }
        if(type== 'checkboxgroup'||type== 'radiogroup'){
            com.columnWidth = record.get('ColumnWidth');
            com.columns = record.get('Columns');
        }
        if(type== 'dateintervals'){
            com.fieldLabelTwo = record.get('fieldLabelTwo');
            //com.dateFormat='Y-m-d';
        }
            com.type = record.get('Type');
            //公共属性
            com.fieldLabel=record.get('DisplayName');
            com.labelWidth=record.get('LabelWidth');
            com.itemId = record.get('InteractionField');
            com.labelStyle=record.get('LabFont');
            com.x = record.get('X');
            com.y = record.get('Y');
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
          if(type== 'combobox'){
            com.valueField = record.get('valueField');
            com.textField = record.get('textField');
            var defaultValue=[];
            defaultValue.push(record.get('defaultValue'));
            if(defaultValue.length>0){
                com.value=defaultValue;
               }
        }
        if(type== 'checkboxgroup'||type== 'radiogroup'){
            com.columnWidth = record.get('ColumnWidth');
            com.columns = record.get('Columns');
        }
        if(type== 'dateintervals'){
            com.fieldLabelTwo = record.get('fieldLabelTwo');
        }
            com.type = record.get('Type');
            //公共属性
            com.fieldLabel=record.get('DisplayName');
            com.labelWidth=record.get('LabelWidth');
            com.itemId = record.get('InteractionField');
            com.labelStyle=labelStyle;
            com.x = record.get('X');
            com.y = record.get('Y');
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
    newfromItemForLabelWidth:function(InteractionField,newValue,oldValue,record,x,y){
        var me = this;
        var com = me.createComponentsByType(record.get('Type'),record);
        var type=record.get('Type');
        var tempValue = newValue-oldValue;
            com.type = record.get('Type');

            //公共属性
            com.itemId = record.get('InteractionField');
            com.labelStyle=record.get('LabFont');
            com.x = x;
            com.y = y;
            com.readOnly = record.get('IsReadOnly');//是否只读
            com.hidden = record.get('IsHidden');//是否隐藏
            com.height = record.get('Height')
            com.width = record.get('Width')+tempValue;
            com.labelWidth=newValue;
            com.draggable = true;//注释这一行,改变大小事件失效,拖放事件生效 long10
            
            if(type== 'combobox'){
                com.valueField = record.get('valueField');
                com.textField = record.get('textField');
            }
            if(type== 'checkboxgroup'||type== 'radiogroup'){
                com.columnWidth = record.get('ColumnWidth');
                com.columns = record.get('Columns');
            }
           if(type== 'dateintervals'){
            com.fieldLabelTwo = record.get('fieldLabelTwo');
           }
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
            //me.setSouthRecordByKeyValue(com.itemId,'Width',com.width);
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
                {text:'交互字段',dataIndex:'InteractionField',disabled:true},
                {text:'显示名称',dataIndex:'DisplayName',
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setColumnParamsRecord(InteractionField,'DisplayName',newValue);
                                if(record.get('Type')=='button'){
                                    me.setComponentFieldText(InteractionField,newValue); 
                                }else{
                                    me.setComponentFieldLabel(InteractionField,newValue);
                                }
                                me.setBasicParamsForInteractionField(InteractionField,'name',newValue);
                            }
                        }
                    }
                },                
                {text:'数据项类型',dataIndex:'Type',width:80,align: 'center',
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
                                me.setComponentX(InteractionField,newValue,y);
                                me.setBasicParamsForInteractionField(InteractionField,'X',newValue);
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
                                me.setComponentY(InteractionField,x,newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'Y',newValue);
                            }

                        }
                    }
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
                                 com.setValue(newValue);
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
                                var x=record.get('X');
                                var y=record.get('Y');
                                me.setColumnParamsRecord(InteractionField,'labelWidth',newValue);
                                var record2=record;
                                me.setComponentLabWidth(InteractionField,newValue,oldValue,record,x,y);
                                
                            }
                        }
                    }
                },
                {text:'对齐方式',dataIndex:'AlignType',width:60,align:'center',
                    renderer:function(value, p, record){
                    var typelist = me.AlignTypeList;
                    for(var i=0;i<typelist.length;i++){
                        if(value == typelist[i][0]){
                            return Ext.String.format(typelist[i][1]);
                        }
                    }
                    },
                    editor: new Ext.form.field.ComboBox({
                        mode:'local',editable:false, 
                        displayField:'text',valueField:'value',
                        store:new Ext.data.SimpleStore({ 
                            fields:['value','text'], 
                            data:me.AlignTypeList
                        }),
                        listClass: 'x-combo-list-small'
                    })
                 }
                ,
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
                {text:'显示名称字体内容',dataIndex:'LabFont',hidden:false},
                {text:'数据地址',dataIndex:'ServerUrl',width:70},
                {text:'默认值',dataIndex:'defaultValue',width:50},
                {text:'值字段(下拉/单,复选)',dataIndex:'valueField'},
                {text:'显示字段(下拉/单,复选)',dataIndex:'textField'},
                {text:'列数(单/复选)',dataIndex:'Columns',width:100,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var type1=record.get("Type");
                                if(type1=="radiogroup"||type1=="checkboxgroup"){
                                 me.setSouthRecordForNumberfield(InteractionField,'Columns',newValue);
                                }else{
                                }
                            }
                        }
                    }
                },
                //longfc
                {text:'列宽(单/复选)',dataIndex:'ColumnWidth',width:100,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                //record.set('ColumnWidth',newValue);
                                //record.commit();
                                var type1=record.get("Type");
                                if(type1=="radiogroup"||type1=="checkboxgroup"){
                                 me.setSouthRecordForNumberfield(InteractionField,'ColumnWidth',newValue);
                                }
                            }
                        }
                    }
                }, 
                {text:'行列方式',dataIndex:'RawOrCol'},//raw:行;col:列
                {text:'选择文件按钮文字',dataIndex:'SelectFileText'}
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
        //整体样式
        var panelStyle = me.createPanelStyle();
        //表单宽高
        var panelWH = me.createWidthHieght();
        //标题设置
        var title = me.createTitle();
        //数据对象
        var dataObj = me.createDataObj();
        //其他设置
        var other = me.createOther();
        
        var formParamsPanel = {
            xtype:'form',
            itemId:'center' + me.ParamsPanelItemIdSuffix,
            title:'单(复)选组属性配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[appInfo,panelStyle,panelWH,title,dataObj,other]
        };
        
        var com = {
            xtype:'form',
            title:'单(复)选组属性配置',
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
              /*{
                xtype:'combobox',fieldLabel:'表单类型',
                itemId:'formType',name:'formType',
                queryMode: 'local',
                labelWidth:55,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:false,typeAhead:true,
                forceSelection:true,
                emptyText:'请选择表单类型',
                displayField:'name',
                valueField:'value',
                value:'1',
                store:new Ext.data.Store({ 
                        fields:['value', 'name'],
                        data :[
                            {"value":"1", "name":"普通表单构建"}
                        ]})
            
            },*/
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
     * 面板整体样式
     * @private
     * @return {}
     */
    createPanelStyle:function(){
        var me = this;
        
        var com = {
            xtype:'fieldset',title:'表单样式',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'panelStyle',
            items:[{
                xtype:'combobox',fieldLabel:'整体样式',
                labelWidth:55,value:'',mode:'local',editable:false,
                displayField:'text',valueField:'value',
                itemId:'panelStyle',name:'panelStyle',
                store:new Ext.data.SimpleStore({ 
                    fields:['value','text'],                    
                    data:me.panelStyleList                    
                }),
                listeners:{
                        change:function(com,newValue,oldValue,eOpts){                           
                        /*var testcom=com;
                        alert(com.store.data.length);
                        for(var i=0;i<com.store.data.length;i++)
                        {
                            if(com.store.data.items[i].data.text==newValue)
                            {
                              alert(com.store.data.items[i].data.value);
                            }
                        
                        }
                        alert('设置整体样式！');*/
                    }
                    }
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
    /**
     * longfc:新增
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
     * longfc:新增
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
     * longfc:新增
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
                xtype: 'radiogroup',
                itemId:'grouptype',
                labelWidth:80,
                fieldLabel:'单（复）选组的类型',
                columns:2,
                vertical:true,
                items:[
                    {boxLabel:'单选组',name:'group',inputValue:'radiobox',checked:true,itemId:'radioboxgroup',
                        listeners :{
                           change:function(){
                               /*var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                               var treeQ = center.getComponent('dataObject').getComponent('objectPropertyTree');
                               treeQ.show();*/
                           }
                        }
                    },
                    {boxLabel:'复选组',name:'group',inputValue:'checkbox',itemId:'checkboxgroup',
                        listeners :{
                            change:function(){
                                /*var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                                var treeQD = center.getComponent('dataObject').getComponent('objectPropertyTree');
                                treeQD.hide();*/
                           }
                        }
                    }
                ]
            },
                {
                xtype:'combobox',fieldLabel:'数据对象',
                itemId:'objectName',name:'objectName',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                emptyText:'请选择数据对象',
                displayField:me.objectDisplayField,
                valueField:me.objectValueField,
                //value:'MEBSampleType',//测试用
                store:new Ext.data.Store({
                    fields:me.objectFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectUrl,
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:me.changeStoreData
                    },autoLoad:true
                })
                ,
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            me.objectChange(owner,newValue);
                        }
                    }
                }
            },                
                                   
            {xtype:'combobox',fieldLabel:'值字段',
                itemId:'valuePanelField',name:'valuePanelField',
                labelWidth:55,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择值字段',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,//me.objectProertyComboxFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    }
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            
                           /*//所有组件信息
                            var southRecords =me.getSouthRecords();
                          
                            var record = southRecords[0];
                             //添加组件属性面板
                            var componentItemId=record.get('InteractionField');                             
                          
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);*/
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
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
                itemId:'displayPanelValue',name:'displayPanelValue',
                labelWidth:55,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择显示值字段',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,//me.objectProertyComboxFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    }
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            
                           //所有组件信息
                            var southRecords =me.getSouthRecords();
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
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
             {
                xtype:'combobox',fieldLabel:'获取数据',
                itemId:'getDataServerUrl',name:'getDataServerUrl',
                labelWidth:55,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择数据地址服务',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
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
                            var objectName = dataObject.getComponent('objectName');
                            store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=List" + objectName.value;
                            
                        },
                             //store是数据地址的store,有两条记录如"查询部门"和"根据HQL条件查询部门"
                        load:function(store,records,successful,eOpts){
                            if(records != null){
                                var east = me.getComponent('east');
                                var southitemId = me.getComponent('south');
                                var panel = east.getComponent('center'+ me.ParamsPanelItemIdSuffix);
                                var serverUrl = panel.getComponent('dataObject').getComponent('getDataServerUrl');
                                //serverUrl.setValue(records[0].get(me.objectServerValueField));
                                var defaultValue = panel.getComponent('dataObject').getComponent('defaultValue_radio');
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
                itemId:'defaultValue_radio',name:'defaultValue_radio',
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
                        //long5
                        var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=radioItem.getItemId();
                        if(newValue && newValue != ""){
                            //给组件的默认值赋值
                            var arr="'"+newValue+"'";
                            var values="{"+groupName+":["+arr+"]}";
                            var arrJson=Ext.decode(values);
                            radioItem.setValue(arrJson);
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
                                //alert('确定加载属性列表数据并生成展示区组件！');
                                me.objectPropertyOKClick();                                
                            }
                        }
                    }]
                }]
             },
            {
                xtype:'textfield',fieldLabel:'默认条件',labelWidth:55,value:'',
                itemId:'defaultParams',name:'defaultParams'
            },{
                xtype:'textfield',fieldLabel:'测试条件',labelWidth:55,value:'',
                itemId:'testParams',name:'testParams'
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
                xtype:'checkbox',itemId:'openLayoutType',name:'openLayoutType',boxLabel:'是否开启排列方式',checked:false
            },{
                xtype:'radiogroup',
                itemId:'layoutType',
                fieldLabel:'排列方式',
                labelWidth:55,
                columns:3,
                vertical:true,
                items:[
                    {boxLabel:'两列',name:'layoutType',inputValue:'2'},
                    {boxLabel:'三列',name:'layoutType',inputValue:'3'},
                    {boxLabel:'四列',name:'layoutType',inputValue:'4',checked:true}
                ]
            },{
                xtype:'numberfield',itemId:'allLabelWidth',name:'allLabelWidth',
                labelWidth:130,emptyText:'默认',fieldLabel:'显示名称宽度【所有】',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.changeComWidth();
                    }
                }
            },{
                xtype:'numberfield',itemId:'allWidth',name:'allWidth',
                labelWidth:130,emptyText:'默认',fieldLabel:'数据项宽度【所有】',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.changeComWidth();
                    }
                }
            },{
                xtype:'checkbox',itemId:'hasSaveButton',name:'hasSaveButton',boxLabel:'保存按钮===='//,checked:true
            },{
                xtype:'checkbox',itemId:'hasResetButton',name:'hasResetButton',boxLabel:'重置按钮===='//,checked:true
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
     * 配置背景html
     * @private
     */
    deployBgHtml:function(){
        var me = this;
        
        //列表配置参数
        var params = me.getPanelParams();
        
        Ext.create('Ext.window.Window',{
            title:'HTML背景',
            closable:false,
            modal:true,layout:'fit',
            width:'90%',height:'90%',
            items:[{
                xtype:'htmleditor',name:'text',
                fieldLabel:'',hideLabel:true,
                margin:5,anchor:'100%',
                itemId:'htmlValue',
                value:params.formHtml
            }],
            dockedItems:[{//停靠
                xtype:'toolbar',dock:'bottom',
                items:[
                    '->',
                    {xtype:'button',text:'确定',iconCls:'build-button-ok',
                        handler:function(){
                            var win = this.ownerCt.ownerCt;
                            var htmlValue = win.getComponent('htmlValue').value;
                            //修改html数据
                            me.setPanelParams({formHtml:htmlValue});
                            //修改背景
                            var center = me.getCenterCom();
                            center.body.update(htmlValue);
                            //重新渲染
                            me.browse();
                            win.close();
                        }
                    },
                    {xtype:'button',text:'取消',iconCls:'delete',
                        handler:function(){
                            this.ownerCt.ownerCt.close();
                        }
                    }
                ]
            }]
        }).show();
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
     * longfc
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
    
    browseView:function(){
        var me=this;
        var ListView=me.getComponent('center');
        var owner = ListView.ownerCt;
        
        var lists=me.getComponent('south');
        var store=lists.getStore();
        //var index = store.findExact('InteractionField',me.groupItemId);
        
        var listParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObject = listParamsPanel.getComponent('dataObject');
        
        //取值字段做交互字段
        var valueFieldItemID=listParamsPanel.getComponent('dataObject');
        var InteractionField=valueFieldItemID.value;
        
        
        var value=me.getEastGroupType();   //......
        
        var index = store.findExact('InteractionField',InteractionField);
        if(index===-1){//新建不存在的对象
            var rec = ('Ext.data.Model',{
                        DisplayName:InteractionField,
                        InteractionField:InteractionField,
                        LabelWidth:55,//显示名称宽度
                        LabFont:'',//显示名称字体内容
                        Type:'radiogroup',//数据项类型   checkboxgroup
                        X:0,//位置X
                        Y:0,//位置X
                        Width:160  //数据项宽度
                        
                    });
                    store.add(rec);
            //store.add(me.addGroupItemId());
            
        }else
        {            
             var record=me.getSouthRecordByKeyValue('InteractionField',InteractionField);
             store.remove(record);
             store.add(me.addGroupItemId());         
        }
        
        /*var list = me.createList();........
        if(list){
            owner.remove(ListView);//删除原先的列表
            list.listeners = {
                beforerender:function(){

                }
            };
        }
        owner.add(list);
         if(list){
             me.changeParamsPanel();
         }*/
        
    },
    /**
     * 保存按钮事件处理
     * @private
     */
    save:function(bo){
        var me = this;
        
        //表单参数
        var params = me.getPanelParams();
        //var formType=params.formType;//构建表单类型:1--为构建普通表单;2---为构建一般查询

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
                //BTDModuleType:2//,//模块类型(表单)
                //ExecuteCode:appStr,//执行代码
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
    //====================事件监听================
    /**
     * 初始化监听事件
     * @param {} owner
     * @param {} newValue
     */
    initListeners:function(){
        var me=this;
        //属性面板监听
        me.initEastPanelListeners();
    },
    /**
     * 面板属性监听事件
     * @param {} owner
     * @param {} newValue
     */
    initEastPanelListeners:function()
    {
        var me=this;
        //数据对象列表监听
        me.initObjectNamelistener();
        
    },
    
    /**
     * 数据对象列表监听
     * @param {} owner
     * @param {} newValue
     */
    initObjectNamelistener:function(){
        var me = this;
        var listParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var objectName = listParamsPanel.getComponent('dataObject').getComponent('objectName');
        objectName.on({
            change:function(owner,newValue,oldValue,eOpts){
                var groupType=me.getEastGroupType();
               
                me.groupItemId=newValue+'_text';//取某个表里的表名称作构建单、复选组的itemId
                me.removeSouthAndCenterAll();
                var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
               
                if(newValue && newValue != "" && index != -1){
                    
                    var value=newValue.split("_");
                  
                    /*var dataObject = listParamsPanel.getComponent('dataObject');
                    //获取对象结构
                    var objectPropertyTree = dataObject.getComponent('objectPropertyTree'); 
                    var node = {text:owner.rawValue,checked:false,expanded:false};
                    objectPropertyTree.store.setRootNode(node);
                    objectPropertyTree.getRootNode().data.expanded = true;
                    objectPropertyTree.store.proxy.url = me.ObjectPropertyUrl + "?" + me.ObjectProperyParam + "=" + newValue;
                    objectPropertyTree.store.load();
                    
                    //获取获取数据服务列表
                    var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
                    getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=" + newValue;
                    getDataServerUrl.store.load();

                    //获取保存数据服务列表
                    var saveDataServerUrl = dataObject.getComponent('saveDataServerUrl');
                    saveDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=" + newValue;
                    saveDataServerUrl.store.load();*/
                    
                if (groupType==="radiobox"){
                    me.DisplayName='单选组';
                 
                    //me.browse();  
                    me.browseView();
                    var getDataServerUrl=listParamsPanel.getComponent('dataObject').getComponent('getDataServerUrl');
                    if(getDataServerUrl.getValue()!=null){
                        var url = getDataServerUrl.getValue().split("?")[0];
                        //url =url +"ToTree";//"?isPlanish=true&fields=";
                        me.setColumnParamsRecord(me.groupItemId,'ServerUrl',url);
                    } 
                   
                }else{
                    me.DisplayName='复选组';
                }

                  }else{
                    return ;
                }
  
            }
        });
        
    },
    //=====================属性面板事件方法=======================
    /**
     * 对象更改事件处理
     * @private
     */
    objectChange:function(owner,newValue){
        var me = this;
        var dataObject = owner.ownerCt;
        
        /*var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var tree = center.getComponent('dataObject').getComponent('objectPropertyTree');
        var store = tree.store;
        var root = store.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] == ClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,ClassName,true);
        }
        var data = [];
        if(node){
            var children = node.childNodes;
            for(var i in children){
                var record = children[i].data;
                data.push(record);
            }
        }*/
        
        //alert("数据对象名称："+newValue);
        //获取获取数据服务列表
        var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=List" + newValue;        
        getDataServerUrl.store.load();
        
        
        //获取新增数据服务列表值字段
        var getInterfact = dataObject.getComponent('valuePanelField');
        getInterfact.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        getInterfact.store.load();
        
        //获取新增数据服务列表值字段
        var displayPanelValue = dataObject.getComponent('displayPanelValue');
        displayPanelValue.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        displayPanelValue.store.load();
        
        /*//获取修改数据服务列表
        var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
        editDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.ObjectServerParam + "=" + newValue;
        editDataServerUrl.store.load();*/
    },
    /**
     * 对象树的勾选过,完成后点击确定按钮处理
     * @private
     * @param {} node
     * @param {} checked
     */
    objectPropertyOKClick:function(){
        var me = this;
        
        var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
        //获取数据地址itemID
        var dataServerUrlsouth=dataObject.getComponent('getDataServerUrl');
        //获取值字段itemID
        var valuePanelFieldsouth=dataObject.getComponent('valuePanelField');
        //获取显示字段itemID
        var displayPanelValuesouth=dataObject.getComponent('displayPanelValue');
        //获取默认值itemID
        var defaultValue_radiosouth=dataObject.getComponent('defaultValue_radio');
        var strvalue=valuePanelFieldsouth.value;
         if(valuePanelFieldsouth.value==='' || valuePanelFieldsouth.value==null)
         {
            Ext.Msg.alert('提示','获取值字段数据失败！');
         }
         if(displayPanelValuesouth.value===''|| displayPanelValuesouth.value==null)
         {
            Ext.Msg.alert('提示','获取显示值数据失败！');
         }
                
          var store = me.getComponent('south').store;
          
           //取值字段做交互字段
        var InteractionField=valuePanelFieldsouth.value;
        var index = store.findExact('InteractionField',InteractionField);
    
           if(index === -1){//新建不存在的对象
                      var rec = ('Ext.data.Model',{
                        DisplayName:InteractionField,
                        InteractionField:InteractionField,
                        LabelWidth:55,//显示名称宽度
                        LabFont:'',//显示名称字体内容
                        Type:'radiogroup',//数据项类型   checkboxgroup
                        X:0,//位置X
                        Y:0,//位置X
                        Width:160  //数据项宽度
                        
                    });
                    store.add(rec);
            }
  
        //me.browse();//展示效果
    },
    //=====================组件的创建与删除=======================
    
    /**
     * 新建单（复）选组表单
     * @return {}
     */
    createFormGroup:function()
    {
        var me=this;
        //表单配置参数
        var params=me.getPanelParams();
        
        var title = params.titleText;
        var formHtml = params.formHtml;
        var width = parseInt(params.Width);
        var height = parseInt(params.Height);
        var bgcolor=params.panelStyle;    //表单背景颜色
        
        var form={
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
    },
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
        //var items = me.createComponents();
        
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
            //items:items,
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
            otherComArr:[]//图片、超文本、文件
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
            
            com.draggable = true;//注释这一行,改变大小事件失效,拖放事件生效 long10
            //com.resizable = {handles:'w e'};//与move事件监听冲突
            
            if(com.type == 'image' || com.type == 'htmleditor' || com.type == 'textareafield' || com.type == 'filefield'){
                //图片、超文本、文本域
                //com.resizable = {handles:"all"};//与move事件监听冲突
                arr.otherComArr.push(com);
            }else{
                arr.basicComArr.push(com);
            }
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
            com.readOnly = record.get('IsReadOnly');//是否只读
            com.hidden = record.get('IsHidden');//是否隐藏
            
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
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
        
        var defaultWidth = 160;
        
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
            var height2 = 54;//图片、超文本的高度
            
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
                if(!com.height){
                    com.height = height2;
                }
                
                y += com.height;
                y += 10;
            }
        }
       } 
        return arr;
    },
    
    /**
     * longfc:修改
     * 根据组件类型生成组件
     * @private
     * @param {} type
     * @param {} record
     * @return {}
     */
    createComponentsByType:function(type,record){
        var me = this;
        var com = null;
        
        if(type == 'combobox'){//下拉框
            com = me.createComboboxfield(record);
        }else if(type == 'textfield'){//文本框
            com = me.createTextfield(record);
        }else if(type == 'textareafield'){//文本域
            com = me.createTextareafield(record);
        }else if(type == 'numberfield'){//数字框
            com = me.createNumberfield(record);
        }else if(type == 'checkboxgroup'){//复选框longfc
            com = me.createCheckboxfield(record);
        }else if(type == 'radiogroup'){//单选框longfc
            com = me.createRadio(record);
        }else if(type == 'button'){//按钮
            com = me.createButton(record);
        }else if(type == 'datacombobox'){//定值下拉框
            com = me.createDataCombox(record);
        }
        
        return com;
    },
    /**
     * 创建定值下拉框
     * @private
     */
    createDataCombox:function(record){
        var com = {
            xtype:'combobox',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            mode:'local',
            editable:false, 
            displayField:'text',
            valueField:'value',
            store:new Ext.data.SimpleStore({ 
                fields:['value','text'], 
                data:record.get('combodata') ? eval(record.get('combodata')) : []
            })
        };
        return com;
    },
    
    /**
     * 创建下拉框组件===================================
     * @private
     * @param {} record
     * @return {}
     */
    createComboboxfield:function(record){
        var com = {
            xtype:'combobox',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            mode:'local',
            editable:false, 
            displayField:'text',
            valueField:'value',
            store:new Ext.data.Store({
                fields:['value','text']
            })
        };
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
//            height:record.get('Height'),
//            x:record.get('X'),
//            y:record.get('Y')
        };
        return com;
    },
    /**
     * 创建文本域组件
     * @private
     * @param {} record
     * @return {}
     */
    createTextareafield:function(record){
        var com = {
            xtype:'textareafield',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width')
        };
        return com;
    },  
    /**
     * 创建数字框组件
     * @private
     * @param {} record
     * @return {}
     */
    createNumberfield:function(record){
        var com = {
            xtype:'numberfield',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width')
        };
        return com;
    },     
 

    /**
     * longfc:修改
     * 创建复选框组件
     * @private
     * @param {} record
     * @return {}
     */
    createCheckboxfield:function(record){
        var tempColumnWidth=(record.get('ColumnWidth')!==0)?record.get('ColumnWidth'):120;
        var tempColumns=(record.get('Columns')!==0)?record.get('Columns'):7;
        var com ={
            xtype: 'checkboxgroup',
            border:false,
            fieldLabel: record.get('DisplayName'),
            labelAlign:"left",
            height: 24,
            width:record.get('Width'),
            name:record.get('InteractionField'),//复选框组名称
            columnWidth :tempColumnWidth,
            columns:tempColumns,
            vertical: false,
            //重置单选框items
            resetItems:function(array){
                this.removeAll();
                this.add(array);
            },
            addItems:function(array){ 
                this.add(array);
            }
        }; 
          return com;
    },
    /**
     * longfc:修改
     * 创建单选框组件
     * @private
     * @param {} record
     * @return {}
     */
    createRadio:function(record){
        var com = {
            xtype:'radiogroup',
            columns:8,
            vertical: false,
            border:true,
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            //labelWidth:100,
            width:record.get('Width'),
            height:24,
            columnWidth :120,
            //重置单选框items
            resetItems:function(array){
                this.removeAll();
                this.add(array);
            },
            addItems:function(array){ 
                this.add(array);
            }
        };
        return com;
    },
    /**
     * 创建纯文本组件
     * @private
     * @param {} record
     * @return {}
     */
    createLabel:function(record){
        var com = {
            xtype:'label',
            name:record.get('InteractionField'),
            width:record.get('Width'),
            text:record.get('DisplayName')
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
        
        //去掉勾选
        me.uncheckedObjectTreeNode('InteractionField',componentItemId);
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
            //grid.getView().refresh();
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
        
        //组件基础属性 longfc
        var basicItems =[];
        //显示名称
        basicItems =me.createBasicItems(componentItemId);

        //组件特有属性
        var otherItems = [];
        
        if(type == 'combobox'){//下拉框
            otherItems = me.createComboboxfieldItems(componentItemId);
        }else if(type == 'textfield'){//文本框
            //不做处理['datetimefield','日期时间'],//longfc
        }else if(type == 'textareafield'){//文本域
            otherItems = me.createTextareafieldItems(componentItemId);
        }else if(type == 'numberfield'){//数字框
            otherItems = me.createNumberfieldItems(componentItemId);
        }else if(type == 'checkboxgroup'){//longfc:复选框
            otherItems = me.createCheckboxfieldItems(componentItemId);
        }else if(type == 'radiogroup'){//longfc单选框
            otherItems = me.createRadioItems(componentItemId);
        }else if(type == 'label'){//纯文本
            //不做处理
        }else if(type == 'button'){//按钮
            //不做处理
        }else if(type == 'datacombobox'){//定值下拉框
            otherItems = me.createhDataComboboxItems(componentItemId);
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
                xtype:'textfield',fieldLabel:'显示名称',name:'name',labelWidth:55,anchor:'95%',
                itemId:'name',
                listeners:{
                    blur:function(com,The,eOpts){
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setFieldLabel(this.value);
                        me.setColumnParamsRecord(componentItemId,'DisplayName',this.value);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setFieldLabel(newValue);
                        me.setColumnParamsRecord(componentItemId,'DisplayName',newValue);  
                    }
                }
            },{
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
                xtype:'numberfield',fieldLabel:'X轴',name:'X',labelWidth:55,anchor:'95%',
                itemId:'X',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        //更新设置展示区域的单选框的X轴                        
                        var x=this.value;                        
                        me.setColumnParamsRecord(componentItemId,'X',x);
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var y=radioItem.y;                        
                        me.setComponentX(componentItemId,x,y);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        var x=newValue;
                        me.setColumnParamsRecord(componentItemId,'X',x);
                        //更新设置展示区域的单选框的X轴
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var y=radioItem.y;
                        me.setComponentX(componentItemId,x,y); 
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'Y轴',name:'Y',labelWidth:55,anchor:'100%',
                itemId:'Y',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        var y=this.value;
                        me.setColumnParamsRecord(componentItemId,'Y',y);
                        //更新设置展示区域的单选框的Y轴
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var x=radioItem.x;
                        me.setComponentX(componentItemId,x,y);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        var y=newValue;
                        me.setColumnParamsRecord(componentItemId,'Y',y);
                        //更新设置展示区域的单选框的Y轴
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var x=radioItem.x;
                        me.setComponentX(componentItemId,x,y);
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
    /**
     * 定值下拉框特有属性
     * @private
     * @return {}
     */
    createhDataComboboxItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'定值数据设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'textarea',anchor:'100%',height:80,
                itemId:'datacomboValue',name:'datacomboValue',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'combodata',this.value);
                    }
                }
            }]
        }];
        return items;
    },
  
    /**
     * longfc:修改
     * 下拉框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createComboboxfieldItems:function(componentItemId){
        var me = this;
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        
        var arr = componentItemId.split("_");
        var ClassName = "";
        for(var i=0;i<arr.length-1;i++){
            ClassName = ClassName + arr[i] + "_";
        }
        if(ClassName != ""){
            ClassName = ClassName.substring(0,ClassName.length-1);
        }
        
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var tree = center.getComponent('dataObject').getComponent('objectPropertyTree');
        var store = tree.store;
        var root = store.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] == ClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,ClassName,true);
        }
        
        var data = [];
        if(node){
            var children = node.childNodes;
            for(var i in children){
                var record = children[i].data;
                data.push(record);
            }
        }
        
        var defaultValue = (data.length > 0) ? data[0][me.objectPropertyValueField] : "";
        
        var defaultValueArr = defaultValue.split("_");
        var text = defaultValueArr[defaultValueArr.length-2]+"_"+defaultValueArr[defaultValueArr.length-1];
        var value = defaultValueArr[defaultValueArr.length-2]+"_"+defaultValueArr[defaultValueArr.length-1];
        
        var items = [{
            xtype:'fieldset',title:'下拉框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'comboboxParams',
            items:[{
                xtype:'combobox',fieldLabel:'值字段',value:defaultValue,
                itemId:'valueField',name:'valueField',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    data:data
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'显示字段',value:defaultValue,
                itemId:'displayField',name:'displayField',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    data:data
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            text = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //显示字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',text);
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',value);
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'数据地址',
                itemId:'serverUrl',name:'serverUrl',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                //longfc
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                    proxy:{
                        type:'ajax',
                        url:me.dictionaryListServerUrl + "?" + me.dictionaryListServerParam + "=" + componentItemId,
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:me.changeStoreData
                    },
                    autoLoad:true,
                    listeners:{
                        load:function(store,records,successful,eOpts){
                            if(records != null){
                                var east = me.getComponent('east');
                                var panel = east.getComponent(panelItemId);
                                var serverUrl = panel.getComponent('comboboxParams').getComponent('serverUrl');
                                serverUrl.setValue(records[0].get(me.objectServerValueField));
                                
                                var defaultValue = panel.getComponent('comboboxParams').getComponent('defaultValue');
                                defaultValue.store.proxy.url = getRootPath() + "/" + records[0].get(me.objectServerValueField).split("?")[0] + "?isPlanish=true&where=";  
                                defaultValue.store.load();
                            }
                        }
                    }
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            //给组件的服务地址赋值
                            var serverUrl =newValue.split("?")[0];
                            me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                            
                            var east = me.getComponent('east');
                            var panel = east.getComponent(panelItemId);
                            var defaultValue = panel.getComponent('comboboxParams').getComponent('defaultValue');
                            defaultValue.store.proxy.url = getRootPath() + "/" +serverUrl+ "?isPlanish=true&where=";
                            defaultValue.store.load();
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'默认选中',//下拉框
                itemId:'defaultValue',name:'defaultValue',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                store:new Ext.data.Store({
                    fields:['text','value'],
                    proxy:{
                        type:'ajax',
                        reader:{type:'json',root:me.objectRoot},
                        
                        extractResponseData:function(response){
                            var data = Ext.JSON.decode(response.responseText);
                            var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                            data.ResultDataValue = ResultDataValue.List;
                            for(var i in data.ResultDataValue){
                                data.ResultDataValue[i].value = data.ResultDataValue[i][value];
                                data.ResultDataValue[i].text = data.ResultDataValue[i][text];
                            }
                            
                            //下拉框组件data赋值
                            var com = me.getCenterCom().getComponent(componentItemId);
                            com.store.loadData(data.ResultDataValue);
                            //返回处理后的数据
                            response.responseText = Ext.JSON.encode(data);
                            return response;
                        }
                    }
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        //var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        var tempitem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=tempitem.getItemId();
                        if(newValue && newValue != ""){
                            //给组件的默认值赋值
                            var tempValue=owner.getValue();
                            var arr="'"+tempValue+"'";
                            var values="{"+groupName+":["+arr+"]}";
                            var arrJson=Ext.decode(values);
                            tempitem.setValue(tempValue);
                            me.setColumnParamsRecord(componentItemId,'defaultValue',newValue);
                        }
                    }
                }
            }]
        }];
        return items;
    },
    
    /**
     * 复选框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createCheckboxfieldItems:function(componentItemId){
        //longfc add
        var me = this;
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        
        var arr = componentItemId.split("_");
        var ClassName = "";
        for(var i=0;i<arr.length-1;i++){
            ClassName = ClassName + arr[i] + "_";
        }
        if(ClassName != ""){
            ClassName = ClassName.substring(0,ClassName.length-1);
        }
        
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var tree = center.getComponent('dataObject').getComponent('objectPropertyTree');
        var store = tree.store;
        var root = store.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] == ClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,ClassName,true);
        }
        
        var data = [];
        if(node){
            var children = node.childNodes;
            for(var i in children){
                var record = children[i].data;
                data.push(record);
            }
        }
        
        var defaultValue = (data.length > 0) ? data[0][me.objectPropertyValueField] : "";
        
        var defaultValueArr = defaultValue.split("_");
        var text = defaultValueArr[defaultValueArr.length-2]+"_"+defaultValueArr[defaultValueArr.length-1];
        var value = defaultValueArr[defaultValueArr.length-2]+"_"+defaultValueArr[defaultValueArr.length-1];
        var items = [{
            xtype:'fieldset',title:'复选框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams_lfc',
            items:[
              {
                xtype:'numberfield',fieldLabel:'列宽',name:'columnWidth',labelWidth:55,anchor:'100%',
                itemId:'othercolumnWidth',value:100, maxValue:200,minValue:5,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ColumnWidth',this.value);
                        //更新设置展示区域的单选框的列宽
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:55,anchor:'100%',
                value:8, maxValue:10,minValue:2,
                itemId:'otherColumns',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Columns',this.value);
                        //更新设置展示区域的单选框的列数
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'值字段',value:defaultValue,
                itemId:'otherValueField',name:'valueField',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    data:data
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != "" ){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    },
                    select:function(combo,records,eOpts){
                    
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'显示字段',value:defaultValue,
                itemId:'otherDisplayField',name:'displayField',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    data:data
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            var text = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //显示字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',text);
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',value);
                        }
                    },
                    select:function(combo,records,eOpts){
                    
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'数据地址',
                itemId:'otherServerUrl',name:'serverUrl',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                    proxy:{
                        type:'ajax',
                        url:me.dictionaryListServerUrl + "?" + me.dictionaryListServerParam + "=" + componentItemId,
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:me.changeStoreData
                    },
                    autoLoad:true,
                    listeners:{
                        load:function(store,records,successful,eOpts){
                            if(records != null){
                                var east = me.getComponent('east');
                                var panel = east.getComponent(panelItemId);
                                var serverUrl = panel.getComponent('otherParams_lfc').getComponent('otherServerUrl');
                                serverUrl.setValue(records[0].get(me.objectServerValueField));
                                var defaultValue = panel.getComponent('otherParams_lfc').getComponent('otherDefaultValue');
                                var url=getRootPath() + "/" +serverUrl+ "?isPlanish=true&where=";
                                var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                                if(record!=null){
                                var value=record.get("valueField");
                                var text=record.get("textField");
                                var mystore=me.GetComboboxItems(url,value,text);
                                defaultValue.store=mystore;
                            }
                            }
                        }
                    }
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            //给组件的服务地址赋值
                            var serverUrl =newValue.split("?")[0];
                            me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                            var east = me.getComponent('east');
                            var panel = east.getComponent(panelItemId);
                            var defaultValue = panel.getComponent('otherParams_lfc').getComponent('otherDefaultValue');
                            var url=getRootPath() + "/" +serverUrl+ "?isPlanish=true&where=";
                             var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                            if(record!=null){
                            var value=record.get("valueField");
                            var text=record.get("textField");
                            var mystore=me.GetComboboxItems(url,value,text);
                            defaultValue.store=mystore;
                            }
                        }
                    },
                    select:function(combo,records,eOpts ){
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        var defaultValue = panel.getComponent('otherParams_lfc').getComponent('otherDefaultValue');
                        var url = getRootPath() + "/" + combo.getValue().split("?")[0] + "?isPlanish=true&where=";
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                       
                        var groudName=radioItem.getItemId();
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,groudName);
                        var value=record.get("valueField");
                        var text=record.get("textField");
                        var data2= me.GetRadiogroupItems(url,groudName,value,text,null);
                            radioItem.resetItems(data2);
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'默认选中',//复选框
                itemId:'otherDefaultValue',name:'defaultValue',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                store:new Ext.data.Store({
                fields:['value','text']
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=radioItem.getItemId();
                        if(newValue && newValue != ""){
                            //给组件的默认值赋值
                            //{checkboxgroupName: ['4',  '5', '6','8']};
                            var arr="'"+newValue+"'";
                            var values="{"+groupName+":["+arr+"]}";
                            var arrJson=Ext.decode(values);
                            radioItem.setValue(arrJson);
                            
                            me.setColumnParamsRecord(componentItemId,'defaultValue',newValue+",");
                        }
                    }
                }
            }
            
            ]
        }];
        return items;
    },
    /**
     * longfc(替换)
     * 单选框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createRadioItems:function(componentItemId){
        //longfc add
        var me = this;
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var ClassName = "";
        for(var i=0;i<arr.length-1;i++){
            ClassName = ClassName + arr[i] + "_";
        }
        if(ClassName != ""){
            ClassName = ClassName.substring(0,ClassName.length-1);
        }
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var tree = center.getComponent('dataObject').getComponent('objectPropertyTree');
        var store = tree.store;
        var root = store.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] == ClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,ClassName,true);
        }
        var data = [];
        if(node){
            var children = node.childNodes;
            for(var i in children){
                var record = children[i].data;
                data.push(record);
            }
        }
        var defaultValue = (data.length > 0) ? data[0][me.objectPropertyValueField] : "";
        var defaultValueArr = defaultValue.split("_");
        var text = defaultValueArr[defaultValueArr.length-2]+"_"+defaultValueArr[defaultValueArr.length-1];
        var value = defaultValueArr[defaultValueArr.length-2]+"_"+defaultValueArr[defaultValueArr.length-1];
        var items = [{
            xtype:'fieldset',title:'单选框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'radioParams_lfc',
            items:[{
                xtype:'numberfield',fieldLabel:'列宽',name:'columnWidth',labelWidth:55,anchor:'100%',
                itemId:'columnWidth_lfc',value:100, maxValue:200,minValue:5,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ColumnWidth',this.value);
                        //删除单选组、然后重新添加单选项
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);   
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:55,anchor:'100%',
                value:5, maxValue:20,minValue:2,
                itemId:'Columns_lfc',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Columns',this.value);
                        //更新设置展示区域的单选框的列数
                        //删除当前单选组重新创建列数                       
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'值字段',value:defaultValue,
                itemId:'valueField_lfc',name:'valueField',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    data:data
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',newValue);
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    },
                    select:function(combo,records,eOpts){
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'显示字段',value:defaultValue,
                itemId:'displayField_lfc',name:'displayField',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    data:data
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectPropertyValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            var arr = newValue.split("_");
                            text = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //显示字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',text);
                        }
                    },
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',value);
                        }
                    },
                    select:function(combo,records,eOpts){
                    
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'数据地址',
                itemId:'serverUrl_lfc',name:'serverUrl_lfc',
                labelWidth:55,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                    proxy:{
                        type:'ajax',
                        url:me.dictionaryListServerUrl + "?" + me.dictionaryListServerParam + "=" + componentItemId,
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:me.changeStoreData
                    },
                    autoLoad:true,
                    listeners:{
                        //store是数据地址的store,有两条记录如"查询部门"和"根据HQL条件查询部门"
                        load:function(store,records,successful,eOpts){
                            if(records != null){
                                var east = me.getComponent('east');
                                var panel = east.getComponent(panelItemId);
                                var serverUrl = panel.getComponent('radioParams_lfc').getComponent('serverUrl_lfc');
                                serverUrl.setValue(records[0].get(me.objectServerValueField));
                                var defaultValue = panel.getComponent('radioParams_lfc').getComponent('defaultValue_lfc');
                                var url= getRootPath() + "/" + records[0].get(me.objectServerValueField).split("?")[0] + "?isPlanish=true&where=";
                                var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                                if(record!=null){
                                var value=record.get("valueField");
                                var text=record.get("textField");
                                var mystore=me.GetComboboxItems(url,value,text);
                                defaultValue.store=mystore;
                                }
                            }
                        }
                        
                    }
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            //给组件的服务地址赋值
                            var serverUrl=newValue.split("?")[0];
                            me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                            var east = me.getComponent('east');
                            var panel = east.getComponent(panelItemId);
                            var defaultValue = panel.getComponent('radioParams_lfc').getComponent('defaultValue_lfc');
                            var url = getRootPath() + "/" + serverUrl + "?isPlanish=true&where=";
                            var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                                if(record!=null){
                                var value=record.get("valueField");
                                var text=record.get("textField");
                                var mystore=me.GetComboboxItems(url,value,text);
                                defaultValue.store=mystore;
                                }
                        }
                    },
                    select:function(combo,records,eOpts ){
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        var defaultValue = panel.getComponent('radioParams_lfc').getComponent('defaultValue_lfc');
                        var url = getRootPath() + "/" + combo.getValue().split("?")[0] + "?isPlanish=true&where=";
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var groudName=radioItem.getItemId();
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,groudName);
                        var value=record.get("valueField");
                        var text=record.get("textField");
                        var data2= me.GetRadiogroupItems(url,groudName,value,text,null);
                            radioItem.resetItems(data2);
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'默认选中',
                itemId:'defaultValue_lfc',name:'defaultValue_lfc',
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
                        //long5
                        var index = owner.store.find(me.objectServerValueField,newValue);//是否存在这条记录
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=radioItem.getItemId();
                        if(newValue && newValue != ""){
                            //给组件的默认值赋值
                            //{checkboxgroupName: ['4',  '5', '6','8']};
                            var arr="'"+newValue+"'";
                            var values="{"+groupName+":["+arr+"]}";
                            var arrJson=Ext.decode(values);
                            radioItem.setValue(arrJson);
                            me.setColumnParamsRecord(componentItemId,'defaultValue',newValue+",");
                        }
                    }
                }
            }
            ]
        }];
        return items;
    },
    /**
     * 文本域特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createTextareafieldItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'文本域特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'高度',name:'Height',labelWidth:55,anchor:'100%',
                itemId:'Height',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Height',this.value);
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 数字框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createNumberfieldItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'数字框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'最小值',name:'NumberMin',labelWidth:55,anchor:'100%',
                itemId:'NumberMin',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'NumberMin',this.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'最大值',name:'NumberMax',labelWidth:55,anchor:'100%',
                itemId:'NumberMax',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'NumberMax',this.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'增量',name:'NumberIncremental',labelWidth:55,anchor:'100%',
                itemId:'NumberIncremental',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'NumberIncremental',this.value);
                    }
                }
            }]
        }];
        return items;
    },
  
  
  


    //===========================加载处理后台数据================================
    /**
     * longfc9
     * 生成单/复选框组的子项数据
     * @param {} url
     * @param {} groupName
     * @param {} valueField
     * @param {} displayField
     * @param {} defaultValue[] 默认值
     * @return {}
     */
    GetRadiogroupItems:function(url,groupName,valueField,displayField,defaultValue2){
        var me = this;
        if(url==""||url==null){
            Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
            return null;
        }
        var mychecked=false;var arrStr=[];
        arrStr=defaultValue2;
        var localData=[];
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                var count = ResultDataValue['Count'];
            for (var i = 0; i <count; i++) { 
                var DeptID=ResultDataValue.List[i][valueField];
                var CName=ResultDataValue.List[i][displayField];
                 if(arrStr!==null&&arrStr.length>0){
                    mychecked=Ext.Array.contains(arrStr,DeptID);
                   }
                var tempItem={ 
                       checked:mychecked,
                       name:groupName,
                       boxLabel:CName,  
                       inputValue:DeptID
                    };
               localData.push(tempItem);
                }
                }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
        return localData;
    },
     /**
     * longfc9
     * 重新生成下拉列表数据
     * @param {} url
     * @param {} value
     * @param {} text
     * @return {}
     */
    GetComboboxItems:function(url,value2,text2){
        var me = this;
        if(url==""||url==null){
            Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
            return null;
        }
        var localData=[];
        var myGridStore=null;
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                var count = ResultDataValue['Count'];
            for (var i = 0; i <count; i++) { 
                var value=ResultDataValue.List[i][value2];
                var text=ResultDataValue.List[i][text2];
                var tempItem={ 
                       value:value,
                       text:text
                    };
               localData.push(tempItem);
                }
            myGridStore=Ext.create('Ext.data.Store', {
            fields:["text","value"], //实现数据项适配的功能
            data:localData
            });
                }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
        return myGridStore;
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
            //grid.getView().refresh();
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
     * 定值下拉框属性面板特有数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setDataComboParamsPanelValue:function(componentItemId,record){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        var datacomboValue = other.getComponent('datacomboValue');
        var value = record.get('combodata');
        if(!value || value == ""){
            value = "[]";
        }
        datacomboValue.setValue(value);
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
                //alert("组件拖动监听");
                
                me.setColumnParamsRecord(com.itemId,'X',x);
                me.setColumnParamsRecord(com.itemId,'Y',y);
            },
            
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                
                var xy=com.getPosition(true);
                
                me.setColumnParamsRecord(com.itemId,'Width',width);
                me.setColumnParamsRecord(com.itemId,'Height',height);
                //me.setSouthRecordByKeyValue(com.itemId,'X',xy[0]);
                //me.setSouthRecordByKeyValue(com.itemId,'Y',xy[1]);
             
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
     * 根据键值对去掉勾选的数据对象
     * @private
     * @param {} key
     * @param {} value
     */
    uncheckedObjectTreeNode:function(key,value){
        var me = this;
        var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
        var treeStore = dataObject.getComponent('objectPropertyTree').store;
        var items = treeStore.lastOptions.node.childNodes[0].store;
        var record = items.findRecord(key,value);
        if(record != null){
            record.set('checked',false);
            record.commit();
        }
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
            {name:'LabFieldAlign',type:'string'},//文本对齐方式
            {name:'LabFont',type:'string'},//显示名称字体内容
            {name:'Type',type:'string'},//数据项类型
            {name:'X',type:'int'},//位置X
            {name:'Y',type:'int'},//位置X
            {name:'Width',type:'int'},//数据项宽度
            {name:'Height',type:'int'},//高度
            //longfc:单选/复选组的列宽,列数
            {name:'ColumnWidth',type:'int'},//列宽
            {name:'Columns',type:'int'},//列数
            {name:'valueField',type:'string'},//值字段(下拉框)
            {name:'textField',type:'string'},//显示字段(下拉框)
            {name:'defaultValue',type:'auto'},//默认值
            {name:'ServerUrl',type:'string'},//数据地址
            {name:'RawOrCol',type:'string'},//行列方式
            {name:'SelectFileText',type:'string'}//选择文件按钮文字
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
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree');
        
        var callback = function(appInfo){
            var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
            var panelParams = appParams.panelParams;
            var southParams = appParams.southParams;
            me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];
            
            objectPropertyTree.store.on({
                load:function(store,node,records,successful,e){
                    if(me.appId != -1 && me.isJustOpen){
                        //对象内容勾选
                        me.changeObjChecked(southParams);
                    }
                }
            });
            
            //赋值
            me.setObjData();
            me.setSouthRecordByArray(southParams);
            me.setPanelParams(panelParams);
            
            var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
            var dataObject = paramsPanel.getComponent('dataObject');
            //获取获取数据服务列表
            var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
            getDataServerUrl.value = panelParams.getDataServerUrl;
            //获取保存数据服务列表
            var addDataServerUrl = dataObject.getComponent('addDataServerUrl');
            addDataServerUrl.value = panelParams.addDataServerUrl;
            //获取保存数据服务列表
            var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
            editDataServerUrl.value = panelParams.editDataServerUrl;
        };
        //从后台获取应用信息
        me.getAppInfoFromServer(callback);
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
     * 勾选数据对象内容
     * @private
     */
    changeObjChecked:function(southParams){
        var me = this;
        var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree');//对象属性树
        var rootNode = objectPropertyTree.getRootNode();
        
        //展开需要展开的所有父节点
        var expandParentNode = function(value){
            var arr = value.split("_");
            if(arr.length >1){
                var v = arr[0];
                for(var i=1;i<arr.length-1;i++){
                    v = v + "_" + arr[i];
                    var n = rootNode.findChild("InteractionField",v,true);
                    if(!n.isExpanded()){//节点没有展开
                        n.expand();
                    }
                }
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
            for(var i=0;i<nodeArr.length;i++){
                if(i == nodeArr.length-1){
                    objectPropertyTree.store.on({
                        load:function(store,node,records,successful,e){
                            if(me.appId != -1 && me.isJustOpen){
                                openNodes(nodeArr);
                                count++;
                                if(count == nodeArr.length){
                                    me.isJustOpen = false;
                                    me.browse();//渲染效果
                                }
                            }
                        }
                    });
                }
                expandParentNode(nodeArr[i]);
            }
        }
    },
    
     /***
     * 获取构建单、复选组的类型
     * @return {}
     */
    getEastGroupType:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var grouptype = center.getComponent('dataObject').getComponent('grouptype');
        var value=grouptype.getValue();
        var result=value.group;
        return result;
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
    
    /***
     * 获取构建单、复选组的类型
     * @return {}
     */
    getEastGroupType:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var grouptype = center.getComponent('dataObject').getComponent('grouptype');
        var value=grouptype.getValue();
        var result=value.group;
        return result;
    },
    
    /**
     * 给数据项属性列表增加单、复选组的属性行记录
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    addGroupItemId:function(){
      var me=this;
      var tableName=me.groupItemId.split("_");
      //var myUrl=""+me.objectgetTreeServerUrl+tableName[0]+"ToTree";
      var value={
           'InteractionField':me.groupItemId,
           DisplayName:me.DisplayName,
           ColumnWidth:me.columnWidth,
           Width:me.itemWidth,
           Height:me.itemHeight,
           labField:me.labField,
           LabelWidth:me.labFieldWidth,
           Columns:me.colRowCount,      //单复选组列数
           //SelectServerUrl:myUrl,           
           AlignType:me.labFieldAlign
       };
       return value;
    },
    
    //=====================生成需要保存的代码=======================
    /**
     * 创建类代码
     * @private
     * @return {}
     */
    createAppClass:function(){
        //this.query('[isCheckbox]' + ('[checked]'||''));
        var me = this;
        //表单配置参数
        var params = me.getPanelParams();

        //内部组件代码
        var items = me.createComponentsStr();
        //var formType=params.formType;
        //加载表单数据方法
        var loadData = me.createLoadDataStr(params.getDataServerUrl);
        //保存表单数据方法:依据构建表单类型生成表单的"保存/查询"按钮处理方法
        var submitData ="";
            submitData = me.createSubmitDataStr(params.addDataServerUrl);
        //dockedItems
        var dockedItems = me.createDockedItems();//停靠栏
        
        //--------------尽量提取出来，不要混在一起，不利于维护--------------
        //GetGroupItems
        var GetGroupItems = 
        "function(url2,valueField,displayField,groupName,defaultValue){"+
            "var url=url2;"+
            "if(url==''||url==null){" + 
                "Ext.Msg.alert('提示',url);return null;" + 
            "}" + 
            "var localData=[];" + 
            "Ext.Ajax.request({" + 
                "async:false" +","+ 
                "timeout:6000" +","+ 
                "url:url,"+    
                "method:'GET'" +","+ 
                "success:function(response,opts){" + 
                    "var result = Ext.JSON.decode(response.responseText);" +
                    "if(result.success){" +
                        "var ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);" +
                        "var count = ResultDataValue['Count'];" +
                        "var mychecked=false;var arrStr=[];"+
                        "if(defaultValue!=''){"+
                            "arrStr=defaultValue.split(',');"+
                        "}"+
                        "for(var i=0;i<count;i++){" +
                            "var DeptID=ResultDataValue.List[i][valueField];" +
                            "var CName=ResultDataValue.List[i][displayField];" +
                            "if(arrStr.length>0){"+
                                "mychecked=Ext.Array.contains(arrStr,DeptID);"+
                            "}"+
                            "var tempItem={" +
                                "checked:mychecked" +"," +
                                "name:groupName," +
                                "boxLabel:CName" +"," +
                                "inputValue:DeptID" +
                            "};" +
                            "localData.push(tempItem);" +
                        "}" +
                    "}else{" +
                        "Ext.Msg.alert('提示','获取信息失败！');" +
                    "}" +
                "}" +
            "});"+
            "return localData;"+
        "}";
        //longfc
        var lists=Ext.JSON.decode(items);
        var strtemp='';
        Ext.each(lists,function(item,index,itemAll){
            var type=item.xtype
            //如果控件类型为单选/复选组时,处理其类生成代码的加载数据显示
            if(type== 'checkboxgroup'||type== 'radiogroup'){
                var defaultValue=""+item.tempDefaultValue;
                var tempArr=defaultValue.split(",");
                var arrStr=[];
                 for (var i = 0; i <tempArr.length; i++) { 
                    if(tempArr[i]!==null&&tempArr[i]!=="")
                    {
                          arrStr.push(tempArr[i]);
                    }
                 }
                strtemp =strtemp+
                "var array"+index+"=me.GetGroupItems(getRootPath()+"+"'"+item.tempUrl+"',"+"'"+item.tempValue+"',"+"'"+item.tempText+"',"+"'"+item.tempGroupName+"',"+"'"+arrStr+"');"+                
                "var item"+index+"=me.getComponent('"+item.itemId+"');"+
                "item"+index+".removeAll();"+
                "item"+index+".add(array"+index+");"
             }
        });
        //----------------------------------------------------------------------
        //背景html
        var html = params.formHtml;
        //GetGroupItems为单选复选组的加载数据方法 last
       
        var appClass = 
        "Ext.define('" + params.appCode + "',{" + 
            "extend:'Ext.form.Panel'," + 
            "alias:'widget." + params.appCode + "'," + 
            "title:'" + params.titleText + "'," + 
            "width:" + params.Width + "," + 
            "height:" + params.Height + "," + 
            "addDataServerUrl:'" + params.addDataServerUrl + "'," + 
            "editDataServerUrl:'" + params.editDataServerUrl + "'," +
            "autoScroll:true," + 
            "type:'add'," + //显示方式add（新增）、edit（修改）、show（查看）
            "dataId:-1," + 
            "bodyCls:"+"'"+params.panelStyle+"'," +     //添加背景颜色*******************************
            "layout:'absolute'," + 
            "GetGroupItems:" + GetGroupItems + ","; 

            appClass=appClass+
            "initComponent:function(){" + 
                "var me=this;" + 
                //注册事件
                "me.addEvents('saveClick');" + 
                //对外公开方法
                "me.load=" + loadData + ";" + 
                "me.submit=" + submitData + ";" + 
                //内部数据匹配方法
                "me.changeStoreData=function(response){" + 
                    "var data = Ext.JSON.decode(response.responseText);" + 
                    "var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);" + 
                    "data.ResultDataValue = ResultDataValue;" +
                    "data.List = ResultDataValue.List;" + 
                    "response.responseText = Ext.JSON.encode(data);" + 
                    "return response;" + 
                "};" + 
                //内部是否只读方法
                "me.setReadOnly=function(bo){" + 
                    "var items = me.items.items;" + 
                    "for(var i in items){" + 
                        "items[i].setReadOnly(bo);" + 
                    "}" + 
                "};" + 
                //内部组件
                "me.items=" + items + ";";
                
            if(dockedItems && dockedItems != ""){
                appClass = appClass + 
                "me.dockedItems=" + dockedItems + ";";
            }
            
            if(html != ""){
                appClass = appClass + 
                "me.html='" + html.replace(/\"/g,"\\\"") + "';";
            }
                appClass = appClass + 
                "me.callParent(arguments);" + 
            "}," + 
            
            "afterRender:function(){" + 
                "var me=this;" + 
                "me.callParent(arguments);" + 
                
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 

                "if(me.type == 'edit'){" + 
                    "me.load(me.dataId);" + 
                "}else if(me.type == 'show'){" + 
                    "me.load(me.dataId);" + 
                    "me.setReadOnly(true);" + 
                "}" + 
                strtemp + 
            "}"+   
        "});";
       
        return appClass;
    },
    /**
     * 创建加载表单数据方法
     * @private
     * @param {} getDataServerUrl
     * @return {}
     */
    createLoadDataStr:function(){
        var me = this;
        //数据服务地址
        var url = me.getDataUrl();
        var fun = 
        "function(id){" + 
            "Ext.Ajax.request({" + 
                "async:false," + 
                "url:getRootPath()+'/" + url + "&id='+(id?id:-1)," + 
                "method:'GET'," + 
                "timeout:5000," + 
                "success:function(response,opts){" + 
                    "var result=Ext.JSON.decode(response.responseText);" + 
                    "if(result.success){" + 
                        "if(result.ResultDataValue&&result.ResultDataValue!=''){" + 
                            "me.dataId=id;" + 
                            "var values=Ext.JSON.decode(result.ResultDataValue);" + 
                            "me.getForm().setValues(values);" + 
                        "}" + 
                    "}else{" + 
                        "Ext.Msg.alert('提示','获取表单数据失败！');" + 
                    "}" + 
                "}," + 
                "failure:function(response,options){" + 
                    "Ext.Msg.alert('提示','获取表单数据请求失败！');" + 
                "}" + 
            "});" + 
        "}";
        return fun;
    },
    /**
     * 创建保存表单数据方法
     * @private
     * @param {} addDataServerUrl
     * @return {}
     */
    createSubmitDataStr:function(addDataServerUrl){
        var fun = 
        "function(){" + 
            "var me = this;" + 
            "if (!me.getForm().isValid()) return;" +
            "var values = me.getForm().getValues();" + 
            "var maxLength = 0;" + //最大的层数
            "for(var i in values){" + 
                "var arr = i.split('_');" + 
                "if(arr.length > maxLength){" + 
                    "maxLength = arr.length;" + 
                "}" + 
            "}" + 
            
            "var obj = {};" + 
            "var addObj = function(key,num,value){" + 
                "var keyArr = key.split('_');" + //键
                "var ob = 'obj';" + 
                "for(var i=1;i<keyArr.length;i++){" + 
                    "ob = ob + '[\\\"' + keyArr[i] + '\\\"]';" + 
                    "if(!eval(ob)){" + //对象不存在
                        "eval(ob + '={};');" + 
                    "}" +  
                "}" + 
                "if(keyArr.length == num+1){" + //当前层赋值
                    "eval(ob + '=value;');" + 
                "}" + 
            "};" + 
            
            "for(var i=1;i<maxLength;i++){" + 
                "for(var j in values){" + 
                    "var value = values[j];" + //值
                    "addObj(j,i,value);" + //键、层、值
                "}" + 
            "}" + 
            
            "var field = '';" + 
            
            "if(maxLength == 2){" + //没有子对象
                "for(var i in values){" + 
                    "var keyArr = i.split('_');" + 
                    "field = field + keyArr[1] + ',';" + 
                "}" +  
            "}" + 
            "if(field != ''){" + 
                "field = field.substring(0,field.length-1);" + 
            "}" + 
            "if(obj.Id == ''){" + 
                "obj.Id=-1;" + 
            "}" + 
            "if(obj.DataTimeStamp && obj.DataTimeStamp != ''){" + 
                "obj.DataTimeStamp = obj.DataTimeStamp.split(',');" + 
            "}" + 
            "var url = (me.dataId != -1) ? me.editDataServerUrl:me.addDataServerUrl;" +  
            "Ext.Ajax.defaultPostHeader = 'application/json';" + 
            "Ext.Ajax.request({" + 
                "async:false," + 
                "url:getRootPath()+'/'+url," + 
                "params:obj," + 
                "params:Ext.JSON.encode({entity:obj,field:field})," + 
                "method:'POST'," + 
                "timeout:5000," + 
                "success:function(response,opts){" + 
                    "var result = Ext.JSON.decode(response.responseText);" + 
                    "if(result.success){" + 
                        "me.fireEvent('saveClick');" + 
                    "}else{" + 
                        "Ext.Msg.alert('提示', '<b>处理错误！原因如下：<font style=\\\"color:red\\\">' + action.result.ErrorInfo + '</font></b>');" +  
                    "}" + 
                "}," + 
                "failure : function(response,options){" + 
                    "Ext.Msg.alert('提示','保存请求失败！');" + 
                "}" + 
            "});" + 
        "}";
        return fun;
    },

    /**
     * 创建按钮组
     * @private
     * @return {}
     */
    createDockedItems:function(){
        var me = this;
        var params = me.getPanelParams();
         //var formType=params.formType;
        var saveButton = "";//保存按钮
        var resetButton = "";//重置按钮
  
        if(params.hasSaveButton){//保存按钮
                 saveButton = 
                "{xtype:'button',text:'保存',iconCls:'build-button-save',handler:function(){" + 
                    "me.submit();" + 
                "}},";
        }
        if(params.hasResetButton){//重置按钮
            resetButton = 
                "{xtype:'button',text:'重置',iconCls:'build-button-refresh',handler:function(){" + 
                    "me.reset();" + 
                "}},";
        }
        
        var dockedItems = "";
        
        if(saveButton != "" || resetButton != ""){
            var items = saveButton + resetButton;
                items = "['->'," + items.substring(0,items.length-1) + "]";
            dockedItems = 
                "[{" + 
                    "xtype:'toolbar'," + 
                    "dock:'bottom'," + 
                    "items:" + items + 
                "}]";
        }
        
        return dockedItems;
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
        if(type == 'combobox'){//下拉框
            com = me.createComboboxStr(record);
        }else if(type == 'dateintervals'){//日期区间(两个日期)控件
            com = me.createDateIntervalsStr(record);
        }else if(type == 'textfield'){//文本框
            com = me.createTextStr(record);
        }else if(type == 'textareafield'){//文本域
            com = me.createTextareaStr(record);
        }else if(type == 'numberfield'){//数字框
            com = me.createNumberStr(record);
        }else if(type == 'datefield'){//日期框
            com = me.createDateStr(record);
        }else if(type == 'timefield'){//时间框
            com = me.createTimeStr(record);
        }else if(type == 'datetimefield'){//日期时间框
            com = me.createDateTimeStr(record);
        }else if(type == 'checkboxgroup'){//复选框
            com = me.createCheckboxStr(record);
        }else if(type == 'radiogroup'){//单选框
            com = me.createRadioStr(record);
        }else if(type == 'label'){//纯文本
            com = me.createLabelStr(record);
        }else if(type == 'image'){//图片
            com = me.createImageStr(record);
        }else if(type == 'htmleditor'){//超文本
            com = me.createHtmleditorStr(record);
        }else if(type == 'filefield'){//文件
            com = me.createFileStr(record);
        }else if(type == 'button'){//按钮
            com = me.createButtonStr(record);
        }else if(type == 'datacombobox'){//定值下拉框
            com = me.createDataComboxStr(record);
        }
        return com;
    },
    /**
     * 创建定值下拉框
     * @private
     * @param {} record
     * @return {}
     */
    createDataComboxStr:function(record){
        var me = this;
        
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'combobox'" + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            
            "value:'" + record.get('defaultValue') + "'," + 
            "mode:'local'," + 
            "editable:false," +  
            "displayField:'text'," + 
            "valueField:'value'," + 
            "store:new Ext.data.SimpleStore({" + 
                "fields:['value','text']," + 
                "data:" + record.get('combodata') + 
            "})" + 
        "}";
        
        return com;
    },
    /**
     * longfc
     * 创建文本框Str
     * @private
     * @param {} record
     * @return {}
     */
    createTextStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'textfield'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + 
        "}";
        return com;
    },
     /**
     * 创建日期区间组件Str
     * @private
     * @param {} record
     * @return {}
     */
    createDateIntervalsStr:function(record){
        var me=this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var fieldLabelTwo = me.hasLab ? record.get('fieldLabelTwo') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = "{" + 
            "xtype:'dateintervals'," + 
            "name:'" +record.get('InteractionField')+"'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" +fieldLabel+"'," + 
            "fieldLabelTwo:'" +fieldLabelTwo+"'," + 
            "labelWidth:" +record.get('LabelWidth')+"," + 
            "width:" +record.get('Width')+"," + 
            "layoutType:'" +record.get('LayoutType')+"'," + 
            "labelAlign:'left'" + "," + 
            "value:new Date()" + "," + 
            "valueTwo:new Date()" + "," + 
            "height:" +record.get('Height')+"," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "dateFormat:'Y-m-d'"+ 
        "}";
        return com;
    },
    /**
     * longfc
     * 创建下拉框Str
     * getRootPath是不是需要修改成在类代码还原后再取
     * @private
     * @param {} record
     * @return {}
     */
    createComboboxStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var url = record.get('ServerUrl').split("?")[0]+"?isPlanish=true&where=&";
        var valueField=record.get('valueField');
        var textField=record.get('textField');
        if((valueField==null||valueField=="")||(textField==null||textField=="")||(url==null||url=="")){
        Ext.Msg.alert('提示','错误信息【<b style="color:red">'+'请先配置下拉框的数据对象或数据服务'+"</b>】");
        return false;
        }
        var com = 
        "{" + 
            "xtype:'combobox'" + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
             "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            
            "value:'" + record.get('defaultValue') + "'," + 
            "mode:'local'," + 
            "editable:false," +  
            "displayField:'" + textField+ "'," + 
            "valueField:'" + valueField+ "'," + 
            "store:new Ext.data.Store({" + 
                "fields:['" + textField + "','" + valueField+ "']" + "," + 
                "proxy:{" + 
                    "type:'ajax'," + 
                    "async:false,"+
                    "url:'" +getRootPath()+"/"+ url + "'"  + "," + 
                    "reader:{type:'json',root:'" + me.objectRootTwo + "'}" + "," + 
                    "extractResponseData:me.changeStoreData" + 
                "},autoLoad:true" + 
            "})" + 
        "}";
        return com;
    },
    /**
     * 创建文本域Str
     * @private
     * @param {} record
     * @return {}
     */
    createTextareaStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'textareafield'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') +  
        "}";
        return com;
    },  
    /**
     * longfc
     * 创建数字框Str
     * @private
     * @param {} record
     * @return {}
     */
    createNumberStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'numberfield'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
             "labelStyle:'" + labelStyle + "'," +  
            "width:" + record.get('Width') + "," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            
            "minValue:" + record.get('NumberMin') + "," + 
            "maxValue:" + record.get('NumberMax') + "," + 
            "step:" + record.get('NumberIncremental') + 
        "}";
        return com;
    },
   /**
     * longfc(已修改)
     * 创建日期框Str
     * @private
     * @param {} record
     * @return {}
     */
    createDateStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'datetimefield'"  + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "LabField:'" + fieldLabel  + "'," + 
            "LabFieldWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "LabFieldAlign:'left'"+ ","+
            "DateFormat:'Y-m-d'" + 
        "}";
        return com;
    },  
    /**
     * 创建时间框Str
     * @private
     * @param {} record
     * @return {}
     */
    createTimeStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'datetimefield'" + "," +  
            "name:'" + record.get('InteractionField') + "'," + 
            "LabField:'" + fieldLabel  + "'," + 
            "LabFieldWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "LabFieldAlign:'left'"+ ","+
            "selectOnFocus:true"+ "," + 
            "TimeFormat:'H:i'" + 
        "}";
        return com;
    },
    /**
     * 创建日期时间框Str
     * @private
     * @param {} record
     * @return {}
     */
    createDateTimeStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'datetimefield'" + "," +  
            "name:'" + record.get('InteractionField') + "'," + 
            "LabField:'" + fieldLabel  + "'," + 
            "LabFieldWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "LabFieldAlign:'left'"+ ","+
            "selectOnFocus:true" + 
        "}";
        return com;
    },
    /**
     * tempUrl需要修改
     * 创建复选框Str
     * @private
     * @param {} record
     * @return {}
     */
    createCheckboxStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var groupName=record.get('InteractionField');
        var defaultValue=[];
        defaultValue.push(record.get('defaultValue'));
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var columns= (record.get('Columns')!="")?(record.get('Columns')):('2');
        var columnWidth= (record.get('ColumnWidth').lenght>0)?(record.get('ColumnWidth')):('100');
        var url = record.get('ServerUrl').split("?")[0]+"?isPlanish=true&where=&";
        var valueField=record.get('valueField');
        var textField=record.get('textField');
        if((valueField==null||valueField=="")||(textField==null||textField=="")||(url==null||url=="")){
        Ext.Msg.alert('提示','错误信息【<b style="color:red">'+'请先配置下拉框的数据对象或数据服务'+"</b>】");
        return false;
        }
        var com = 
        "{" + 
            "xtype:'checkboxgroup'" + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" + record.get('Height') + "," + 
            "columnWidth:"+ columnWidth + "," +
            "columns:"+columns + "," +
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "vertical: true"+ "," + 
            "padding:2"+ "," + 
            "autoScroll:true,"+
            "hidden:" + record.get('IsHidden')+ "," + 
            "tempUrl:'" +"/"+ url + "'"+ "," + 
            "tempValue:'" +record.get('valueField')+ "'"+ "," +
            "tempText:'" +record.get('textField')+ "'"+ "," +
            "tempGroupName:'" +groupName+ "'"+ "," +
            "tempDefaultValue:'" +defaultValue+ "'" +
        "}";
        return com;
    },
    /**
     * 创建单选框Str
     * @private
     * @param {} record
     * @return {}
     */
    createRadioStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        //long7
        var groupName=record.get('InteractionField');
        var defaultValue=[];
        defaultValue.push(record.get('defaultValue'));
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var columns= (record.get('Columns')!="")?(record.get('Columns')):('2');
        var columnWidth= (record.get('ColumnWidth').lenght>0)?(record.get('ColumnWidth')):('100');
        var url = record.get('ServerUrl').split("?")[0]+"?isPlanish=true&where=&";
        var valueField=record.get('valueField');
        var textField=record.get('textField');
        if((valueField==null||valueField=="")||(textField==null||textField=="")||(url==null||url=="")){
        Ext.Msg.alert('提示','错误信息【<b style="color:red">'+'请先配置下拉框的数据对象或数据服务'+"</b>】");
        return false;
        }
        //tempUrl,tempValue,tempGroupName,tempDefaultValue作类代码处理加载数据用
        var com = 
        "{" + 
            "xtype:'radiogroup'" + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" + record.get('Height') + "," + 
            "columnWidth:"+ columnWidth + "," +
            "columns:"+columns+ "," +
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," +
            "vertical: true"+ "," + 
            "padding:2"+ "," + 
            "autoScroll:true"+ "," + 
            "hidden:" + record.get('IsHidden')+ "," + 
            "tempUrl:'" +"/"+ url + "'"+ "," + 
            "tempValue:'" +record.get('valueField')+ "'"+ "," +
            "tempText:'" +record.get('textField')+ "'"+ "," +
            "tempGroupName:'" +groupName+ "'"+ "," +
            "tempDefaultValue:'" +defaultValue+ "'" +
        "}";
        return com;
    },
    /**
     * 创建纯文本Str
     * @private
     * @param {} record
     * @return {}
     */
    createLabelStr:function(record){
        var com = 
        "{" + 
            "xtype:'label'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "width:" + record.get('Width') + "," + 
            "text:'" + record.get('DisplayName') + "'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "hidden:" + record.get('IsHidden') + 
        "}";
        return com;
    },
    /**
     * 创建数图片Str
     * @private
     * @param {} record
     * @return {}
     */
    createImageStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var src = (record.get('ServerUrl') && record.get('ServerUrl') != "") ? record.get('ServerUrl') : "getRootPath()+'/ui/css/images/default/defaultImg.png'"
        var labelStyle= (record.get('LabFont').lenght>0)?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'image'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "src:'" + src + "'" + 
        "}";
        return com;
    },
    /**
     * 创建超文本Str
     * @private
     * @param {} record
     * @return {}
     */
    createHtmleditorStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var com = 
        "{" + 
            "xtype:'htmleditor'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "width:" + record.get('Width') + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + 
        "}";
        return com;
    },
    /**
     * 创建文件Str
     * @private
     * @param {} record
     * @return {}
     */
    createFileStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var buttonText = (record.get('SelectFileText') && record.get('SelectFileText') != "") ? record.get('SelectFileText') : "选择";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com = 
        "{" + 
            "xtype:'filefield'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:" +labelStyle+ "," + 
            "width:" + record.get('Width') + "," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden')+ "," + 
            
            "selectOnFocus:false" + "," + 
            "buttonOnly:false" + "," + 
            "buttonText:'" + buttonText + "'" + 
        "}";
        return com;
    },
    /**
     * 创建按钮Str
     * @private
     * @param {} record
     * @return {}
     */
    createButtonStr:function(record){
        var com = 
        "{" + 
            "xtype:'button'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "width:" + record.get('Width') + "," + 
            "text:'" + record.get('DisplayName') + "'," + 
            
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "hidden:" + record.get('IsHidden') + 
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