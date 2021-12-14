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
Ext.define('Ext.build.BasicFormPanel',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basicformpanel',
    //=====================可配参数=======================
    /**
     * 应用组件ID
     */
    appId:-1,
    /**
     * 时间戳字符串
     * @type String
     */
    DataTimeStamp:'',
    /**
     * 构建名称
     */
    buildTitle:'普通表单构建工具',
    //数据对象配置private
    win:null,//创建和弹出选择器窗体
    win2:null,//创建和弹出选择器窗体
    winHeight:270,        //弹出选择器窗体高度像素
    winWidth:460,       //弹出选择器窗体宽度像素
    winTitle:'',        //弹出选择器窗体标题    
    
    classCode:'BTDAppComponents_ClassCode',//应用列表对象的类代码字段
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
    /***
     * 功能按钮ItemId后缀
     * @type String
     */
    functionBtnSuffix:'FunctionBtn',
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
    /**
     * 获取应用信息集合
     * @type String
     */
    getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
    //=====================内部变量=======================
    /**
     * 表单默认的标题
     * @type String
     */
    defaultFormTitle:'表单',
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
        ['combobox','下拉框'],
        ['textfield','文本框'],
        ['textareafield','文本域'],
        ['numberfield','数字框'],
        ['datefield','日期框'],
        ['timefield','时间框'],
        ['datetimenew','日期时间'],
        //['dateintervals','日期区间'],//暂时不用
        //['checkboxgroup','复选组'],//暂时不用
        ['radiogroup','单选组'],
        ['datacombobox','定值下拉框'],
        //['datacheckboxgroup','定值复选组'],//暂时不用
        ['dataradiogroup','定值单选组'],
        ['label','纯文本'],
        ['image','图片'],
        ['htmleditor','超文本'],
        ['filefield','文件'],
        ['gridcombobox','多列下拉框'],
        ['displayfield','文本内容'],
        ['colorscombobox','颜色选择器'],
        //['radiofield','设定单选框'],//暂时不用,//只支持单个的单选框,提交值为'true'(选中)或者'false'(未选中)
        ['checkboxfield','布尔勾选框']//只支持单个的复选框,提交值为'true'(勾选中)或者'false'(未勾选中)
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
	 * 对齐方式
	 * @type 
	 */
	AlignTypeList:[
		['left','左对齐'],
		['top','顶部'],
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
    hasLab:false,
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
    /**表单解析器*/
	ParserForm:null,
	/**表单解析器路径*/
	ParserUrl:'Ext.build.ParserForm',
    //=====================内部视图渲染=======================
    /**
     * 初始化表单构建组件
     */
    initComponent:function(){
        var me = this;
        //初始化内部参数
        me.initParams();

        Ext.QuickTips.init();
        Ext.Loader.setConfig({enabled: true});
        Ext.Loader.setPath('Ext.ux',getRootPath()+'/extjs/ux/');
        Ext.Loader.setPath('Ext.zhifangux',getRootPath()+'/extjs/zhifangux/');

        me.ParserForm = Ext.create(me.ParserUrl);
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
    	me.callParent(arguments);
    	me.setAppParams();
    },
    /**
     * 初始化内部参数
     * @private
     */
    initParams:function(){
        var me = this;
        Ext.Loader.setPath('Ext.ux',getRootPath()+'/ui/extjs/ux');
		Ext.Loader.setPath('Ext.zhifangux',getRootPath()+'/ui/zhifangux');
        //边距
        me.bodyPadding = 2;
        //布局方式
        me.layout = {
            type:'border',
            //regionWeights:{south:1,east:2,north:3}
            regionWeights:{west:1,east:2,north:3}
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
        //south.region = "south";
        south.region = "west";
        
        //功能块大小
        north.height = 30;
        //south.height = 200;
        south.width = 350;
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
                {xtype:'button',text:'新增按钮',itemId:'insertButton',iconCls:'build-button-add',margin:'0 4 0 0',
                    handler:function(){
                    	me.openAddButtonWin();
                    }
                },
                '-',
                {xtype:'button',text:'新增Display字段',itemId:'insertDisplay',iconCls:'build-button-add',margin:'0 4 0 0',
                    handler:function(){
                        me.openAddDisplayWin();
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
                            var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
							var params = formParamsPanel.getForm().setValues({hasLab:newValue});
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
        
        var form = {
            xtype:'form',
            itemId:'center',
            layout:'absolute',
            autoScroll:true,
            title:me.defaultFormTitle,
		    width:me.defaultPanelWidth,
		    height:me.defaultPanelHeight,
            resizable:{handles:'s e'}
        };
        
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
        
        var com = {
            xtype:'panel',
            title:'',
            bodyPadding:'2 10 10 2',
            autoScroll:true,
            items:form
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
            if(com&&com!=undefined){
                com.setValue(newValue);
            }
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
            if(interactionField&&interactionField!=undefined){
                interactionField.setValue(newValue);
            }
    },
     /**
     * 属性面板特有属性区域
     * @param {} 
     * @param {} 
     */
    getOtherParams:function(componentItemId){
        var me=this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var others=panel.getComponent("otherParams");
        return others;
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
        if(tempItem&&tempItem!=undefined){
            tempItem.setPosition(x,y);
        }
    },
    /**
     * 展示区域的某一控件的y轴属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} x:新x轴值;y:新y轴值;
     */
    setComponentY:function(InteractionField,x,y){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
        if(tempItem&&tempItem!=undefined){
            tempItem.setPosition(x,y);
        }
    },
     /**
     * 展示区域的某一控件的宽度属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setComponentWidth:function(InteractionField,newValue){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
        if(tempItem&&tempItem!=undefined){
            tempItem.setSize(newValue);
        }
    },
    /**
     * 展示区域的某一控件的x轴属性更新
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} x:新x轴值;y:新y轴值;
     */
    setComponentXY:function(InteractionField,x,y){
        var me=this;
        var tempItem= me.getCenterCom().getComponent(InteractionField);
        if(tempItem&&tempItem!=undefined){
            tempItem.setPosition(x,y);
        }
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
            }
        }
    },
    //==============================某一控件属性更新==============================
    /**
     * 展示区域里的某一控件生成功能按钮
     * @private
     * @return {}
     */
    createfunctionButton:function(record){
        var me = this,com =null;
            com = me.createButton(record);
            com.readOnly=record.get('IsReadOnly');
            com.hidden=record.get('IsHidden');
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
     * 展示区域里的某一控件重新生成
     * @private
     * @return {}
     */
    newfromItem:function(InteractionField,record){
        var me = this,com =null,data2=[] ;
        var itemIdTemp=[];
        itemIdTemp=InteractionField.split('_');

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
	        if(type== 'textField'||type== 'datacombobox'){
	            com.IsFunctionBtn = record.get('IsFunctionBtn');
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
            return com;
            
    },
    /**
     * 列属性列表
     * @private
     * @return {}
     */
    createSouth:function(){
        var me = this;
        var south1 = me.createSouth1();
        var south2 = me.createSouth2();
        //数据项属性列表
        south1.store.on({
           load:function(Store,records,successful,eOpts)
           {
            alert('数据项列表属性');
            
           },
           refresh:function(Store,eOpts)
           {
            alert('数据项列表属性store刷新后监听');
           }
        });
        south1.listeners={            
            select:function(RowModel,record,index,eOpts )
            {
                //列表是否有数据
                var count=record.store.count();
                if(count>0)
                {   
                    //获取对应展示区按钮组件ID
	                var controlId=record.data['InteractionField'];
	                var centerId=me.getCenterCom();
	                var id=centerId.getComponent(controlId);
	                id.focus(); //设置焦点
/*	                    id.on({
	                    focus:function(com,The,eOpts){
	                        alert("当前控件焦点");
	                    }
	                    });*/
                }
            }
        };
        //按钮属性列表
        south2.listeners={           
            select:function(RowModel,record,index,eOpts )
            {
                //列表是否有数据
                var count=record.store.count();
                if(count>0)
                {   
                    //获取对应展示区应用组件ID
                    var butid=record.data['buttonItemId'];
                    var centerId=me.getCenterCom();
                    var butCustomid=centerId.getComponent(butid);
                    butCustomid.focus(); //设置焦点
                }
            }
        };
        
        south1.itemId = "south1";
        south2.itemId = "south2";
        
        var com = {
            xtype:'tabpanel',
            header:false,
            items:[south1,south2]
        };
        return com;
    },
    /**
     * 列属性列表
     * @private
     * @return {}
     */
    createSouth1:function(){
        var me = this;
        var com = {
            xtype:'grid',
            title:'数据项属性列表',
            columnLines:true,//在行上增加分割线
            columns:[//列模式的集合
                {xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
                {text:'交互字段',dataIndex:'InteractionField',width:150,disabled:true,editor:{readOnly:true},locked:true},
                {text:'显示名称',dataIndex:'DisplayName',locked:true,
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
                {text:'只读',dataIndex:'IsReadOnly',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    },listeners:{
                            checkchange:function(com,rowIndex,checked, eOpts ){
                             //获取数据项属性列表所有组件信息
                             var southRecords=me.getSouthRecords();
                             //获取当前选中行数据
                             var record=southRecords[rowIndex];
                             var componentItemId=record.get('InteractionField');
                             me.setchangeComponent(componentItemId,record);
                            }
                        }
                },
                {text:'隐藏',dataIndex:'IsHidden',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    },listeners:{
                            checkchange:function(com,rowIndex,checked, eOpts ){
                             //获取数据项属性列表所有组件信息
                                var southRecords = me.getSouthRecords();
                                 //获取当前选中行数据
                                var record = southRecords[rowIndex];
                                var componentItemId=record.get('InteractionField');
                                me.setchangeComponent(componentItemId,record);
                            }
                        }
                },
                {text:'光标顺序',dataIndex:'sortNum',width:60,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'sortNum',newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'sortNum',newValue);                                
                            }
                        }
                    }
                },
                {text:'数据项类型',dataIndex:'Type',width:100,align: 'left',
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
                               
                                me.setBasicParamsForInteractionField(InteractionField,'X',newValue);
                                //当文本框的宽度改变后,文本框组件绑定的功能树选择按钮的x轴值处理
                                var type=record.get('Type');
                                var isFunctionBtn=record.get('isFunctionBtn');
                                if(type=='textfield'&&isFunctionBtn==true){
                                    var tempValue=newValue-oldValue;
                                    var btnX=tempValue+record.get('btnX');
                                    me.setSouthRecordForNumberfield(InteractionField,'btnX',btnX);
                                    me.setAllCenterComXY();
                                }else{
                                     me.setComponentX(InteractionField,newValue,y);
                                }
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
                                me.setBasicParamsForInteractionField(InteractionField,'Y',newValue);
                                
                                var type=record.get('Type');
		                        var isFunctionBtn=record.get('isFunctionBtn');
		                        if(type=='textfield'&&isFunctionBtn==true){
		                            me.setSouthRecordForNumberfield(InteractionField,'btnY',newValue);
                                    me.setAllCenterComXY();
		                        }else{
                                    me.setComponentY(InteractionField,x,newValue);
                                }
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
                                 com.setValue(newValue);
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'Width',newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'myWidth',newValue);
                                //当文本框的宽度改变后,文本框组件绑定的功能树选择按钮的x轴值处理
                                var type=record.get('Type');
                                var isFunctionBtn=record.get('isFunctionBtn');
                                if(type=='textfield'&&isFunctionBtn==true){
                                    var tempValue=newValue-oldValue;
                                    var btnX=tempValue+record.get('btnX');
                                    me.setSouthRecordForNumberfield(InteractionField,'btnX',btnX);
		                            me.setAllCenterComXY();
                                }else{
                                    me.setComponentWidth(InteractionField,newValue);
                                }
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
		            editor:new Ext.form.field.ComboBox({
		                mode:'local',editable:false, 
						displayField:'text',valueField:'value',
		                store:new Ext.data.SimpleStore({ 
						    fields:['value','text'], 
						    data:me.AlignTypeList
						}),
		                listClass: 'x-combo-list-small'
		            })
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
                {text:'值字段',dataIndex:'valueField',hidden:false},
                {text:'显示字段',dataIndex:'textField',hidden:false},
                {text:'列数(单/复选)',dataIndex:'Columns',width:100,align:'center',hidden:true,
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
                {text:'列宽(单/复选)',dataIndex:'ColumnWidth',width:100,align:'center',hidden:true,
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                record.set('ColumnWidth',newValue);
                                record.commit();
                                var type1=record.get("Type");
                                if(type1=="radiogroup"||type1=="checkboxgroup"){
                                
                                }
                            }
                        }
                    }
                },
                {text:'日期区间布局方式',dataIndex:'LayoutType',hidden:true},
                {text:'显示名称二',dataIndex:'fieldLabelTwo',hidden:true},
                {text:'行列方式',dataIndex:'RawOrCol',hidden:true},//raw:行;col:列
                {text:'数字最小值',dataIndex:'NumberMin',hidden:false},
                {text:'数字最大值',dataIndex:'NumberMax',hidden:false},
                {text:'数字增量',dataIndex:'NumberIncremental',hidden:false},
                {text:'显示格式',dataIndex:'ShowFomart',hidden:false},
                {text:'是否允许手输',dataIndex:'CanEdit',hidden:true},
                {text:'选择文件按钮文字',dataIndex:'SelectFileText',hidden:true},
                {text:'定值下拉框数据',dataIndex:'combodata'},
                
                {text:'是否开启功能按钮',dataIndex:'isFunctionBtn',
                    xtype:'checkcolumn',hidden:true,
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    },
                    listeners:{
                        change:function(com,The,eOpts){
                            
                        }
                   }
                },
                {text:'文本框绑定的元应用的显示值',dataIndex:'appComCName',hidden:true},
                {text:'文本框绑定的元应用的ID值',dataIndex:'appComID',hidden:true},
                {text:'功能按钮绑定相关文本框itemId',dataIndex:'boundField',hidden:true},
                {text:'功能按钮itemId',dataIndex:'functionBtnId',hidden:false},
                {text:'功能按钮X轴',dataIndex:'btnX',hidden:true},
                {text:'功能按钮Y轴',dataIndex:'btnY',hidden:true},
                {text:'多列下拉框列英文字段',dataIndex:'gridcomboboxColumns',width:200,hidden:false},
                {text:'多列下拉框列中文字段',dataIndex:'gridcomboboxColumnsCName',width:100,hidden:false},
                {text:'多列下拉框匹配字段数组',dataIndex:'queryFields',width:100,hidden:false},
                {text:'多列下拉框最小宽度',dataIndex:'minWidth',hidden:true},
                {text:'多列下拉框最大高度',dataIndex:'maxHeight',hidden:true}
                
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
     * 按钮属性列表
     * @private
     * @return {}
     */
    createSouth2:function(){
        var me = this;
        var com = {
            xtype:'grid',
            title:'按钮属性列表',
            columns:[
                {xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
                {text:'按钮名称',dataIndex:'buttonName',editor:{allowBlank:false}},
                {text:'内部编号',dataIndex:'buttonItemId',editor:{allowBlank:false}},
                {text:'显示宽度',dataIndex:'buttonWidth',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:false}
                },
                {text:'显示高度',dataIndex:'buttonHeight',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:false}
                },
                {text:'显示X轴',dataIndex:'buttonX',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:false}
                },
                {text:'显示Y轴',dataIndex:'buttonY',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:false}
                },
                {text:'弹出窗口',dataIndex:'openWin',xtype:'checkcolumn',align:'center',width:60,
                    editor:{xtype:'checkbox',cls:'x-grid-checkheader-editor'}
                },
                {text:'窗口类型',dataIndex:'openWinType',editor:{allowBlank:false}},
                {text:'链接路径',dataIndex:'buttonURL',editor:{allowBlank:false}},
                {text:'应用名称',dataIndex:'openWinAppName',editor:{allowBlank:false}},
                {text:'应用ID',dataIndex:'openWinAppId',editor:{allowBlank:false}},
                {text:'窗口宽度',dataIndex:'openWinWidth',xtype:'numbercolumn',format:'0',align:'center',
                    editor:{xtype:'numberfield',allowBlank:false}
                },
                {text:'窗口高度',dataIndex:'openWinHeight',xtype:'numbercolumn',format:'0',align:'center',
                    editor:{xtype:'numberfield',allowBlank:false}
                }
            ],
            store:Ext.create('Ext.data.Store',{
                fields:me.getSouth2StoreFields(),
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
            title:'表单属性配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[appInfo,panelStyle,panelWH,title,dataObj,other]
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
                xtype:'textfield',fieldLabel:'功能编号',labelWidth:60,anchor:'100%',
                itemId:'appCode',name:'appCode',labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
            },{
                xtype:'textfield',fieldLabel:'中文名称',labelWidth:60,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                itemId:'appCName',name:'appCName'
            },{
                xtype:'textareafield',fieldLabel:'功能简介',labelWidth:60,anchor:'100%',grow:true,
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
                labelWidth:60,value:'',mode:'local',editable:false,
                displayField:'text',valueField:'value',
                itemId:'panelStyle',name:'panelStyle',
                store:new Ext.data.SimpleStore({ 
                    fields:['value','text'],                    
                    data:me.panelStyleList                    
                }),
                listeners:{
                        change:function(com,newValue,oldValue,eOpts){                           

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
                xtype:'checkbox',boxLabel:'显示标题(===)',checked:true,hidden:true,
                itemId:'hasTitle',name:'hasTitle',inputValue: 'true',
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.browse();//浏览
                    }
                }
            },{
                xtype:'textfield',fieldLabel:'显示名称',labelWidth:60,value:me.defaultFormTitle,anchor:'100%',
                itemId:'titleText',name:'titleText'
            },{
                xtype:'fieldcontainer',layout:'hbox',
                itemId:'titleStyle',
                items:[{
                    xtype:'label',text:'字体设置:',width:60,margin:'2 0 2 0'
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
                xtype:'combobox',fieldLabel:'数据对象',
                itemId:'objectName',name:'objectName',
                labelWidth:60,anchor:'100%',
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
                }),
                listeners:{
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            me.objectChange(owner,newValue);
                        }
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
                xtype:'treepanel',itemId:'objectPropertyTree',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbar',
                    items:[{
                        xtype:'button',text:'确定',itemId:'objectPropertyOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                me.objectPropertyOKClick();
                            }
                        }
                    }]
                }],
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
                        //树节点的勾选处理(util公共方法)
                        treeNodeCheckedChange(node,checked);
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
                                }
                                
                                var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
                                dataObject = east.getComponent('dataObject');
                                var objectPropertyTree = dataObject.getComponent('objectPropertyTree'); 
                                
                                if(objectPropertyTree.nodeClassName != ""){
                                    data[me.objectRootProperty] = children;
                                }else{
                                    data[me.objectRootProperty] = [{
                                        text:objectPropertyTree.CName,
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
            },{
                xtype:'combobox',fieldLabel:'获取数据',
                itemId:'getDataServerUrl',name:'getDataServerUrl',
                labelWidth:60,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择获取数据服务',
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
                            store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=" + objectName.value;
                            
                        }
                    }
                })
            },{
                xtype:'combobox',fieldLabel:'新增数据',
                itemId:'addDataServerUrl',name:'addDataServerUrl',
                labelWidth:60,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择新增数据服务',
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
                            store.proxy.url = me.objectSaveDataServerUrl + "?" + me.ObjectServerParam + "=" + objectName.value;
                            
                        }
                    }
                })
            },{
                xtype:'combobox',fieldLabel:'修改数据',
                itemId:'editDataServerUrl',name:'editDataServerUrl',
                labelWidth:60,anchor:'100%',
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
                            store.proxy.url = me.objectSaveDataServerUrl + "?" + me.ObjectServerParam + "=" + objectName.value;
                            
                        }
                    }
                })
            },{
                xtype:'textfield',fieldLabel:'默认条件',labelWidth:60,value:'',
                itemId:'defaultParams',name:'defaultParams'
            },{
                xtype:'textfield',fieldLabel:'测试条件',labelWidth:60,value:'',
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
                fieldLabel:'列数',
                labelWidth:30,
                columns:4,
                vertical:true,
                items:[
                	{boxLabel:'一',name:'layoutType',inputValue:'1'},
                    {boxLabel:'二',name:'layoutType',inputValue:'2'},
                    {boxLabel:'三',name:'layoutType',inputValue:'3'},
                    {boxLabel:'四',name:'layoutType',inputValue:'4',checked:true}
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
                xtype:'combobox',itemId:'allLabelAlign',name:'allLabelAlign',
                labelWidth:130,emptyText:'默认',fieldLabel:'显示名称对齐方式',
                mode:'local',editable:false,
				displayField:'text',valueField:'value',
                store:new Ext.data.SimpleStore({ 
				    fields:['value','text'], 
				    data:[['','默认']].concat(me.AlignTypeList)
				}),
                listeners:{
                    blur:function(com,The,eOpts){
                        me.changeComLabelAlign();
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
                //xtype:'checkbox',itemId:'hasSaveButton',name:'hasSaveButton',boxLabel:'保存按钮'
            	xtype:'fieldcontainer',layout:'hbox',itemId:'SaveButton',
				items:[{
					xtype:'checkbox',boxLabel:'保存按钮',flex:1,
					itemId:'hasSaveButton',name:'hasSaveButton'
				},{
					xtype:'textfield',value:'保存',height:22,padding:'1 2 0 2',
					emptyText:'显示名称',flex:2,
					itemId:'SaveButtonText',name:'SaveButtonText'
				}]
            },{
                //xtype:'checkbox',itemId:'hasResetButton',name:'hasResetButton',boxLabel:'重置按钮'
                xtype:'fieldcontainer',layout:'hbox',itemId:'ResetButton',
				items:[{
					xtype:'checkbox',boxLabel:'重置按钮',flex:1,
					itemId:'hasResetButton',name:'hasResetButton'
				},{
					xtype:'textfield',value:'重置',height:22,padding:'1 2 0 2',
					emptyText:'显示名称',flex:2,
					itemId:'ResetButtonText',name:'ResetButtonText'
				}]
            },{
                xtype:'textfield',itemId:'formHtml',name:'formHtml',hidden:true
            },{
                xtype:'fieldcontainer',layout:'hbox',itemId:'Default1Button',
				items:[{
					xtype:'checkbox',boxLabel:'自定义一',flex:1,
					itemId:'hasDefault1Button',name:'hasDefault1Button'
				},{
					xtype:'textfield',value:'自定义一',height:22,padding:'1 2 0 2',
					emptyText:'显示名称',flex:2,
					itemId:'Default1ButtonText',name:'Default1ButtonText'
				}]
            },{
                xtype:'fieldcontainer',layout:'hbox',itemId:'Default2Button',
				items:[{
					xtype:'checkbox',boxLabel:'自定义二',flex:1,
					itemId:'hasDefault2Button',name:'hasDefault2Button'
				},{
					xtype:'textfield',value:'自定义二',height:22,padding:'1 2 0 2',
					emptyText:'显示名称',flex:2,
					itemId:'Default21ButtonText',name:'Default2ButtonText'
				}]
            },{
                xtype:'fieldcontainer',layout:'hbox',itemId:'Default3Button',
				items:[{
					xtype:'checkbox',boxLabel:'自定义三',flex:1,
					itemId:'hasDefault3Button',name:'hasDefault3Button'
				},{
					xtype:'textfield',value:'自定义三',height:22,padding:'1 2 0 2',
					emptyText:'显示名称',flex:2,
					itemId:'Default3ButtonText',name:'Default3ButtonText'
				}]
            },{
                xtype:'combobox',fieldLabel:'默认方式',
                itemId:'defaultType',name:'defaultType',
                labelWidth:60,anchor:'100%',
                queryMode:'local',
                emptyText:'请选择数据对象',
                displayField:'text',
                valueField:'value',
                value:'show',
                store:new Ext.data.Store({
                    fields:['text','value'],
                    data:[
                    	{text:'新增方式',value:'add'},
                    	{text:'修改方式',value:'edit'},
                    	{text:'查看方式',value:'show'}
                    ]
                })
            },{
            	xtype:'textfield',itemId:'hasLab',name:'hasLab',hidden:true
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
                xtype:'numberfield',fieldLabel:'表单宽度',labelWidth:60,anchor:'100%',
                itemId:'Width',name:'Width',value:me.defaultPanelWidth,
                listeners:{
                    blur:function(com,The,eOpts){
                        var center = me.getCenterCom();
                        center.setWidth(com.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'表单高度',labelWidth:60,anchor:'100%',
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
        
        var params = me.getPanelParams()
        if(params.testParams){
             var center = me.getCenterCom();
            center.loadData();
        }else{
            Ext.Msg.alert('提示','没有填写测试条件！');
        }
    },
    /**
     * 配置背景html
     * @private
     */
    deployBgHtml:function(){
        var me = this;
        
        //列表配置参数
        var params = me.getPanelParams();
        
        Ext.create('Ext.form.Panel',{
            title:'HTML背景',
            autoScroll:true,
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			resizable:true,//可变大小
			draggable:true,//可移动
            width:'90%',height:'90%',
            items:[{
                xtype:'image',name:'image',itemId:'image'
            },{
            	xtype:'textfield',hidden:true,
            	itemId:'htmlValue',value:params.formHtml
            }],
            dockedItems:[{//停靠
                xtype:'toolbar',dock:'bottom',
                items:[{
                	xtype:'filefield',buttonConfig:{iconCls:'search-img-16',text:''},
                	fieldLabel:'背景图片',itemId:'file'
                },'->',
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
        var store = me.getSouthCom().store;
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
        //在表单上创建所有的自定义按钮
        me.createButtonsToCenter();
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
        	me.disablePanel(true);//禁用保存按钮
	     	//设计代码（还原代码）
	    	var appParams = me.getAppParamsStr();//me.getAppParams();
            //类代码
            //var appClass = me.createAppClass();
            //应用组件ID
            var id = bo ? me.appId : -1;
            //生成应用对象
            var BTDAppComponents = {
                Id:id,//应用组件ID
                CName:params.appCName,//名称
                ModuleOperCode:params.appCode,//功能编码
                ModuleOperInfo:params.appExplain,//功能简介
                InitParameter:params.defaultParams,//初始化参数
                AppType:2,//应用类型(表单)
                BuildType:1,//构建类型
                //BTDModuleType:2//,//模块类型(表单)
                //ExecuteCode:appStr,//执行代码
				DesignCode:appParams//,//设计代码
                //ClassCode:appClass//类代码
            };
            
            if(me.LabID && me.LabID != ''){BTDAppComponents.LabID = me.LabID;}
			if(me.ModuleTypeID && me.ModuleTypeID != ''){BTDAppComponents.ModuleTypeID = me.ModuleTypeID;}
			if(me.EName && me.EName != ''){BTDAppComponents.EName = me.EName;}
			if(me.ExecuteCode && me.ExecuteCode != ''){BTDAppComponents.ExecuteCode = me.ExecuteCode;}
			if(me.Creator && me.Creator != ''){BTDAppComponents.Creator = me.Creator;}
			if(me.Modifier && me.Modifier != ''){BTDAppComponents.Modifier = me.Modifier;}
			if(me.PinYinZiTou && me.PinYinZiTou != ''){BTDAppComponents.PinYinZiTou = me.PinYinZiTou;}
			if(me.DataAddTime && me.DataAddTime != ''){BTDAppComponents.DataAddTime = me.DataAddTime;}
			if(me.DataUpdateTime && me.DataUpdateTime != ''){BTDAppComponents.DataUpdateTime = me.DataUpdateTime;}
			if(me.DataTimeStamp && me.DataTimeStamp != ''){BTDAppComponents.DataTimeStamp = me.DataTimeStamp;}
			//保存数据到后台
			var saveToServer = function(){
	            var callback = function(text){
					var result = Ext.JSON.decode(text);
					var appId = -1;
					if(result.success){
						if(result.ResultDataValue && result.ResultDataValue != ""){
							var data = Ext.decode(result.ResultDataValue);
							appId = data.id;
						}
						
						var c = function(appInfo){
							me.setAppInfo(appInfo);
							me.disablePanel(false);//启用面板
							me.fireEvent('saveClick');
						};
						me.appId = (me.appId == -1 ? appId : me.appId);
						//从后台获取应用信息
						//me.getAppInfoFromServer(me.appId,c);
						me.ParserForm.getAppInfoById(me.appId,c);
						me.disablePanel(false);//启用面板
						alertInfo('保存成功');
					}else{
						me.disablePanel(false);//启用面板
						alertError(result.ErrorInfo);
					}
	            }
	            //后台保存数据
	            //me.saveToServer(BTDAppComponents,callback);
	            me.ParserForm.saveAppInfo(BTDAppComponents,callback);
			};
            //名称重复校验
            var call = function(text){
            	var result = Ext.JSON.decode(text);
	        	if(result.success){
	        		var count = 0;
                    if(result.ResultDataValue && result.ResultDataValue != ""){
                    	var data = Ext.JSON.decode(result.ResultDataValue);
                    	count = data.count;
                    }
                    if(count == 0){
                		saveToServer();
                	}else{
                		me.disablePanel(false);//启用保存按钮
                		Ext.Msg.alert('提示','<b style="color:red">名称已经存在！</b>');
                	}
                }else{
                	me.disablePanel(false);//启用保存按钮
                    Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ result.errorInfo +"</b>】");
                }
            };
            
            var where = "";
            if(id == -1){//新增
            	where = "btdappcomponents.CName='" + params.appCName + "'";
            }else{
            	where = "btdappcomponents.Id<>" + id + " and btdappcomponents.CName='" + params.appCName + "'";
            }
            var url = me.getAppListServerUrl + "?isPlanish=true&fields=BTDAppComponents_Id&where=" + where;
            //util-GET方式与后台交互
            getToServer(url,call);
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
        var dataObject = owner.ownerCt;
        //获取对象结构
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree'); 
        
        objectPropertyTree.nodeClassName = "";
        objectPropertyTree.CName = owner.rawValue;
        objectPropertyTree.ClassName = newValue;
        
        objectPropertyTree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        objectPropertyTree.store.load();
        
        //获取获取数据服务列表
        var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.ObjectServerParam + "=" + newValue;
        getDataServerUrl.store.load();
        //获取新增数据服务列表
        var addDataServerUrl = dataObject.getComponent('addDataServerUrl');
        addDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.ObjectServerParam + "=" + newValue;
        addDataServerUrl.store.load();
        //获取修改数据服务列表
        var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
        editDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.ObjectServerParam + "=" + newValue;
        editDataServerUrl.store.load();
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
        var ColumnParams = dataObject.getComponent('objectPropertyTree');//对象属性树
        var data = ColumnParams.getChecked();
        
        var store = me.getSouthCom().store;
        
        //获取显示名称全称
        var getTextStr = function(node){
        	var v = '';
        	var f = function(n){
        		if(!n.isRoot()){
        			v = n.get('text') + '_' + v;
        			f(n.parentNode);
        		}
        	};
        	f(node);
        	v = v=='' ? v : v.slice(0,-1);
        	var arr = v.split('_');
        	if(arr.length > 1){
        		arr = arr.slice(1);
        	}
        	v = arr.join('_');
        	return v;
        };
        
        //勾选节点数组
        var dataArray = [];
        //列表中显示被勾选中的对象
        Ext.Array.each(data,function(record){
            if(record.get('leaf')){
                var index = store.findExact('InteractionField',record.get(me.columnParamsField.InteractionField));
                dataArray.push(record.get(me.columnParamsField.InteractionField));
                
                if(index === -1){//新建不存在的对象
                    var rec = ('Ext.data.Model',{
                        //DisplayName:record.get('text'),
                        DisplayName:getTextStr(record),
                        InteractionField:record.get(me.columnParamsField.InteractionField),
                        LabelWidth:60,//显示名称宽度
                        LabFont:'',//显示名称字体内容
                        Type:record.get(me.columnParamsField.Type) || 'textfield',//数据项类型
                        X:0,//位置X
                        Y:0,//位置X
                        Width:160,//数据项宽度
                        IsReadOnly:false,//只读
                        IsHidden:false,//隐藏
                        sortNum:1,//光标顺序
                        AlignType:'left'//对齐方式
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
                        if(result.ResultDataValue && result.ResultDataValue != ""){
                             var values = Ext.JSON.decode(result.ResultDataValue);
                            var form = me.getCenterCom();
                            form.getForm().setValues(values);
                        }
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
        
        var buttons = [];
        if(params.hasDefault1Button){//自定义一
        	var item = {
        		xtype:'button',text:params.Default1ButtonText,itemId:'default1button',
                handler:function(but){
                    alertInfo('按钮内部编号:default1button</br>按钮显示名称:'+but.text+'</br>表单中的监听:default1buttonClick');
                }
        	};
        	buttons.push(item);
        }
        if(params.hasDefault2Button){//自定义二
        	var item = {
        		xtype:'button',text:params.Default2ButtonText,itemId:'default2button',
                handler:function(but){
                    alertInfo('按钮内部编号:default2button</br>按钮显示名称:'+but.text+'</br>表单中的监听:default2buttonClick');
                }
        	};
        	buttons.push(item);
        }
        if(params.hasDefault3Button){//自定义三
        	var item = {
        		xtype:'button',text:params.Default3ButtonText,itemId:'default3button',
                handler:function(but){
                    alertInfo('按钮内部编号:default3button</br>按钮显示名称:'+but.text+'</br>表单中的监听:default3buttonClick');
                }
        	};
        	buttons.push(item);
        }
        buttons.push('->');
        if(params.hasSaveButton){//保存按钮
            var item = {
                xtype:'button',itemId:'save',text:params.SaveButtonText,
                iconCls:'build-button-save',
                handler:function(but){
                    var addDataServerUrl = params.addDataServerUrl;
                    if(addDataServerUrl){
                        alertInfo('提示','保存数据服务地址='+addDataServerUrl);
                    }else{
                        alertError('没有配置保存数据服务地址!');
                    }
                }
            };
            buttons.push(item);
        }
        
        if(params.hasResetButton){//重置按钮
            var item = {
                xtype:'button',itemId:'reset',text:params.ResetButtonText,
                iconCls:'build-button-refresh',
                handler:function(but){
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
                itemId:'dockedItems-buttons',
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
            
            me.setComListeners(com,comArr);//组件监听
            
            coms.push(com);
            
        }
        //按光标顺序排序
        for(var i=1;i<coms.length;i++){
            for(var j=0;j<coms.length-i;j++){ 
                if(coms[j].sortNum>coms[j+1].sortNum){//比较交换相邻元素
                    var temp=coms[j];
                    coms[j]=coms[j+1];
                    coms[j+1]=temp;
                } 
            } 
        }
        var num = 0;
        for(var i=0;i<coms.length;i++){
            coms[i].sortNum = (coms[i].sortNum > 0) ? ++num : 0;
            //更新属性列表中的光标顺序数据
            me.setSouthRecordByKeyValue(coms[i].itemId,"sortNum",coms[i].sortNum);
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
        
        var store = me.getSouthCom().store;
        var items = store.data.items;
        
        //所有组件信息
        var southRecords = me.getSouthRecords();
        for(var i in southRecords){
            var record = southRecords[i];
            //根据组件类型生成组件
            var type = record.get('Type');
            var com = me.createComponentsByType(type,record);
            com.type = record.get('Type');
            //公共属性
            com.itemId = record.get('InteractionField');
            com.labelAlign = record.get('AlignType');
            com.x = record.get('X');
            com.y = record.get('Y');

            com.readOnly = record.get('IsReadOnly');//是否只读
            com.hidden = record.get('IsHidden');//是否隐藏
            com.sortNum = record.get('sortNum');//光标顺序
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
            coms.push(com);
            
            //添加功能按钮
            if((com.type =='datacombobox'||com.type =='textfield')&&com.isFunctionBtn==true){

                var centor=me.getCenterCom();
                var functionBtnCom=centor.getComponent(record.get('functionBtnId'));
                if(functionBtnCom){}else{
	                var btnCom=me.createfunctionButton(record);
	                btnCom.itemId = record.get('functionBtnId');
	                btnCom.x = record.get('btnX');
	                btnCom.y = record.get('btnY');
	                btnCom.sortNum = record.get('sortNum');//光标顺序
	                coms.push(btnCom);
                }
            }
            
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
        }else if(type == 'datefield'){//日期框
            com = me.createDatefield(record);
        }else if(type == 'timefield'){//时间框
            com = me.createTimefield(record);
        }else if(type == 'datetimenew'){//日期时间框
            com = me.createDatetimefield(record);
        }else if(type == 'checkboxgroup'){//复选组
            com = me.createCheckboxfield(record);
        }else if(type == 'radiogroup'){//单选组
            com = me.createRadiogroup(record);
        }else if(type == 'label'){//纯文本
            com = me.createLabel(record);
        }else if(type == 'image'){//图片
            com = me.createImage(record);
        }else if(type == 'htmleditor'){//超文本
            com = me.createHtmleditor(record);
        }else if(type == 'filefield'){//文件
            com = me.createFilefield(record);
        }else if(type == 'button'){//按钮
        }else if(type == 'dateintervals'){//日期区间
            com = me.createDateIntervals(record);
        }else if(type == 'datacombobox'){//定值下拉框
            com = me.createDataCombox(record);
        }else if(type == 'datacheckboxgroup'){//定值复选组
            com = me.createDataCheckboxfield(record);
        }else if(type == 'dataradiogroup'){//定值单选组
            com = me.createDataRadio(record);
        }else if(type == 'gridcombobox'){//多列下拉框
            com = me.createGridcombobox(record);
        }else if(type == 'displayfield'){//displayfield
            com = me.createDisplayfield(record);
        }else if(type == 'colorscombobox'){
            com = me.createColorscombobox(record);
        }else if(type == 'radiofield'){
            com = me.createSettingRadio(record);//设定单选框
        }else if(type == 'checkboxfield'){
            com = me.createSettingCheckBox(record);//设定复选框
        }
        return com;
    },
    /***
     * 单选框
     * @param {} record
     * @return {}
     */
    createSettingRadio:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }    
        var com =
            {
            xtype: 'radiofield',
            x:record.get('X'),
            y:record.get('Y'),
            itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),//复选框组名称
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            height:height,
            readOnly:record.get('IsReadOnly'),
            boxLabel:'',
            inputValue:'true',//提交值，默认为“on”
            uncheckedValue:'false'//设置当复选框未选中时向后台提交的值，默认为undefined
            };
          return com; 
    },
    /***
     * 复选框
     * @param {} record
     * @return {}
     */
    createSettingCheckBox:function(record){
        var me=this;
        var height=record.get('Height');
        var defaultValue=record.get('defaultValue');
        
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        } 
        var checked2=false;
        if(defaultValue==true||defaultValue==false){
            checked2="'"+defaultValue+"'";
        }else{
            checked2="'"+false+"'";;
        }
        var com =
            {
            xtype: 'checkboxfield',
            x:record.get('X'),
            y:record.get('Y'),
            itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),//复选框组名称
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            height:height,
            boxLabel:'',
            readOnly:record.get('IsReadOnly'),  //新增复选框只读属性
            inputValue:'true',//提交值，默认为“on”
            uncheckedValue:'false',//设置当复选框未选中时向后台提交的值，默认为undefined
            checked:checked2
            };
        
          return com; 
    },
    /***
     * 颜色下拉选择器
     * @param {} record
     * @return {}
     */
    createColorscombobox:function(record){
        var me=this;
        var minWidth= record.get('minWidth');
        var maxHeight= record.get('maxHeight');
        if(minWidth==0){
            minWidth=140;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'minWidth',minWidth);
        }
        if(maxHeight==0){
            maxHeight=200;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'maxHeight',maxHeight);
        }
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }    
        var com =
            {
            xtype: 'colorscombobox',
            x:record.get('X'),
            y:record.get('Y'),
            itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),//复选框组名称
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            height:height,
            minWidth:minWidth,
            maxHeight:maxHeight,
            forceSelection:true//true:所选的值必须存在于列表中;false:允许设置任意文本
            };
          return com; 
    },
    
    /***
     * 多列下拉框
     * @param {} record
     * @return {}
     */
    createGridcombobox:function(record){
        var me=this;
        var minWidth= record.get('minWidth');
        var maxHeight= record.get('maxHeight');
        if(minWidth==0){
            minWidth=300;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'minWidth',minWidth);
        }
        if(maxHeight==0){
            maxHeight=200;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'maxHeight',maxHeight);
        }
        
        //处理列字段信息
        var columns=[];
        var tempColumns=""+record.get('gridcomboboxColumns')+"";
        if(tempColumns&&tempColumns.length>0){
            var arrKeys=tempColumns.toString().split(',');
            var tempColumnsTexts=""+record.get('gridcomboboxColumnsCName');
            var arrTexts=tempColumnsTexts.split(',');
            for(var i=0;i<arrKeys.length;i++){
                var tempText=""+arrTexts[i];
                var tempDataIndex=""+arrKeys[i]+"";
                var tempDataTimeStamp=arrKeys[i].split('_');
                var tempJson={};
                if(tempDataTimeStamp[tempDataTimeStamp.length-1]=='DataTimeStamp'){
                    tempJson={text:tempText,dataIndex:tempDataIndex,width:120,hidden:true};
                }else{
                    tempJson={text:tempText,dataIndex:tempDataIndex,width:120};
                }
                columns.push(tempJson);
            }
        }else{
            //第一次为空值时的数据
            var tempText=""+record.get('DisplayName');
            var tempDataIndex=""+record.get('InteractionField')+"";
	        columns:[
	                {text:tempText,dataIndex:tempDataIndex,width:60}
	            ];
        }
        //处理匹配字段数组(queryFields)及查询传参的字段集fields
        var tempQueryFields=record.get('queryFields');
        var queryFields=[];
        var fields='';
        if(tempQueryFields&&tempQueryFields.toString().length>0)
        {
            var queryFields2='';
            var arrKeys=tempQueryFields.split(',');
            for(var i=0;i<arrKeys.length;i++){
                queryFields2=queryFields2+(""+arrKeys[i]+",");
                fields=fields+(""+arrKeys[i]+",");
                queryFields.push(""+arrKeys[i]);
            }
            //queryFields=[queryFields2.substring(0,queryFields2.length-1)];
            fields=fields.substring(0,fields.length-1);
        }else{
            var tempStr=""+record.get('InteractionField');
            queryFields.push(""+tempStr[i]);
            fields=""+record.get('InteractionField'); 
        }

        var myUrl=record.get('ServerUrl');
        var myStroe=null;
            if(myUrl!=''){
                myUrl=getRootPath()+"/"+myUrl+"?isPlanish=true&fields="+fields;
	            myStroe=new Ext.data.Store({
	                fields:queryFields,
	                proxy:{
	                    type:'ajax',
	                    url:myUrl,
	                    reader:{type:'json',root:'list'},
	                    extractResponseData:function(response){
	                        var data = Ext.JSON.decode(response.responseText);
                            if(data.ResultDataValue && data.ResultDataValue !=''){
                                var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                                data.ResultDataValue = ResultDataValue;
                                data.list = ResultDataValue.list;
                            }else{
                                data.list=[];
                            } 
                            response.responseText = Ext.JSON.encode(data);
                            return response;
	                }
	                },autoLoad:true
	            });
            }else{
	            myStroe=new Ext.data.Store({ 
                fields:queryFields, 
                data:[]
            });
            }
            
        var com =
            {
            xtype: 'gridcombobox',
            x:record.get('X'),
            y:record.get('Y'),
            store:myStroe,
            itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),//复选框组名称
            columns:columns,
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            valueField:record.get('valueField'),//值
            displayField:record.get('textField'),//显示文字
            width:record.get('Width'),
            height:(record.get('Height')==0)?(22):record.get('Height'),
            minWidth:minWidth,
            maxHeight:maxHeight,
            queryFields:queryFields,
            forceSelection:true//true:所选的值必须存在于列表中;false:允许设置任意文本
            };
          return com;
    },
    /**
     * 创建定值复选组
     * @private
     * @param {} record
     * @return {}
     */
    createDataCheckboxfield:function(record){
        var me=this;
        var tempColumnWidth=(record.get('ColumnWidth')!==0)?record.get('ColumnWidth'):120;
        var tempColumns=(record.get('Columns')!==0)?record.get('Columns'):7;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com ={
            xtype: 'checkboxgroup',
            border:false,
            fieldLabel: record.get('DisplayName'),
            labelAlign:"left",
            x:record.get('X'),
            y:record.get('Y'),
            height:height,
            width:record.get('Width'),
            itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),//复选框组名称
            columnWidth :tempColumnWidth,
            columns:tempColumns,
            vertical: false,
            items:record.get('combodata') ? eval(record.get('combodata')) : []
        }; 
          return com;
    },
    /**
     * 创建定值单选组
     * @private
     * @param {} record
     * @return {}
     */
    createDataRadio:function(record){
        var me=this;
        var tempColumnWidth=(record.get('ColumnWidth')!==0)?record.get('ColumnWidth'):120;
        var tempColumns=(record.get('Columns')!==0)?record.get('Columns'):7;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'radiogroup',
            columns:tempColumns,
            x:record.get('X'),
            y:record.get('Y'),
            vertical: false,
            border:true,
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            width:record.get('Width'),
            height:height,
            columnWidth :tempColumnWidth,
            items:record.get('combodata') ? eval(record.get('combodata')) : []
        };
        return com;
    },
    /**
     * 创建定值下拉框
     * @private
     */
    createDataCombox:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var defaultValue=record.get('defaultValue');
        var isFunctionBtn=record.get('isFunctionBtn');
        var com = {
            xtype:'combobox',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            //自定义是否开启功能按钮属性,文本控件特有
            isFunctionBtn:isFunctionBtn,
            //当定值下拉框带有功能按钮时,appComID属性用以保存隐藏的应用id值
            appComID:"'" + record.get('appComID')+"'",
            appComCName:record.get('appComCName'),
            height:height,
            mode:'local',
            editable:false, 
            readOnly:record.get('IsReadOnly'),
            displayField:'text',
            valueField:'value',
            value:""+defaultValue+"",
            store:Ext.create('Ext.data.SimpleStore',{  
                fields:['value','text'], 
                data:record.get('combodata') ? eval(record.get('combodata')) : []
            })
        };
        return com;
    },
     /**
     * 创建日期区间组件
     * @private
     * @param {} record
     * @return {}
     */
    createDateIntervals:function(record){
        var me=this;
        var width1=record.get('Width');
        var height1=record.get('Height');
        var rawOrCol=record.get('RawOrCol');
        if(rawOrCol==''){
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'RawOrCol','hbox');
            rawOrCol='hbox';
        }
        if(rawOrCol=='hbox'&&width1<320)
        {
            width1=320;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Width',width1);
        }
         if(rawOrCol=="vbox"&&height1<56)
        {
            height1=56;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height1);
            
        }
        var showFomart=record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = {
            xtype:'dateintervals',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            fieldLabelTwo:record.get('fieldLabelTwo'),
            labelWidth:record.get('LabelWidth'),
            width:width1,
            layoutType:rawOrCol,//控件布局设置,默认为横布局(hbox),竖布局为(vbox)
            //labelAlign:'left',
            value:new Date(),
            valueTwo:new Date(),
            height:height1,
            editable:canEdit,
            dateFormat:showFomart
        };
        
        return com;
    },
    /**
     * 创建下拉框组件
     * @private
     * @param {} record
     * @return {}
     */
    createComboboxfield:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'combobox',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            height:height,
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
        var me=this;
        var isFunctionBtn=record.get('isFunctionBtn');
        var com =null;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        if(isFunctionBtn==true){
            com = {
            xtype:'textfield',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            //自定义是否开启功能按钮属性,文本控件特有
            isFunctionBtn:isFunctionBtn,
            //当文本框带有功能按钮时,appComID属性用以保存隐藏的id值(如应用id)
            appComID:"'" + record.get('appComID')+"'",
            treeNodeID:'',//选择中的树节点的id
            value:record.get('appComCName'),
            readOnly:record.get('IsReadOnly'),
            height:height,
            width:record.get('Width'),
            appComCName:record.get('appComCName'),
            
            getValue:function() {
                var val = this.treeNodeID;
                return val;
             }
             
        };  
        }else{
            com = {
                xtype:'textfield',
                name:record.get('InteractionField'),
                fieldLabel:record.get('DisplayName'),
                labelWidth:record.get('LabelWidth'),
                //自定义是否开启功能按钮属性,文本控件特有
                isFunctionBtn:isFunctionBtn,
                //当文本框带有功能按钮时,appComID属性用以保存隐藏的id值(如应用id)
                appComID:"'" + record.get('appComID')+"'",
                treeNodeID:'',//选择中的树节点的id
                value:record.get('appComCName'),
                readOnly:record.get('IsReadOnly'),
                height:height,
                width:record.get('Width')
            };
        }
        return com;
    },
    /**
     * 创建文本域组件
     * @private
     * @param {} record
     * @return {}
     */
    createTextareafield:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'textareafield',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
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
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'numberfield',
            minValue :record.get('NumberMin'),
            maxValue :record.get('NumberMax'),
            step : record.get('NumberIncremental'),
            
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
            width:record.get('Width')
        };
        return com;
    },  
    /**
     * 创建日期框组件
     * @private
     * @param {} record
     * @return {}
     */
    createDatefield:function(record){
        var me=this;
        var showFomart=record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
            me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = {
                xtype:'datefield',
                format:showFomart,
                editable:canEdit,
                name:record.get('InteractionField'),
                fieldLabel:record.get('DisplayName'),
                labelWidth:record.get('LabelWidth'),
                height:height,
                width:record.get('Width'),
                readOnly:record.get('IsReadOnly')
//                ,setValue:function(value){//2013/11/13 08:00:00
//                    var com=this;
//	                if(value&&(value!=undefined||value!='')){
//                        //转换日期格式
//	                  var newValue=''+Ext.util.Format.date(value,'Y-m-d H:i:s');
//	                  var arr = newValue.split(" ");
//	                  var date = arr[0];
//	                  this.value=date;
//	                }
//                    //com.callParent();
//				    //com.applyEmptyText();
//				    return com;
//                },
//                listeners: {
//                    select:function(com,date, eOpts ){
//                        var newValue=''+Ext.util.Format.date(date,'Y-m-d H:i:s');
//                        var arr = newValue.split(" ");
//                        var date2 = arr[0];
//                        this.setValue(date2);
//                    }
//                }         
        };
        return com;
    },  
    /**
     * 创建时间框组件
     * @private
     * @param {} record
     * @return {}
     */
    createTimefield:function(record){
        var me=this;
        var showFomart=record.get('ShowFomart');
        if(showFomart==''&&showFomart==""||showFomart==null){
            showFomart='H:i';
            me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = {
            xtype:'timefield',
            format:showFomart,
            editable:canEdit,
            name:record.get('InteractionField'),
            increment: 1,//增量
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
            width:record.get('Width'),
            readOnly:record.get('IsReadOnly'),
            selectOnFocus:true
        };
        return com;
    },
     /**
     * 创建日期时间框组件
     * @private
     * @param {} record
     * @return {}
     */
    createDatetimefield:function(record){
        var me=this;
        var showFomart=record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d H:i:s';
             me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var width=record.get('Width');
        if(width==''||width==0||width==null||width<205)
        {
            width=210;
            me.setColumnParamsRecord(record.get('InteractionField'),'Width',width);
        }
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'datetimenew',
            format:showFomart,
            editable:canEdit,
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
            width:width,
            readOnly:record.get('IsReadOnly'),
            selectOnFocus:true
        };
        return com;
    },
    /**
     * 创建复选组组件
     * @private
     * @param {} record
     * @return {}
     */
    createCheckboxfield:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var tempColumns=record.get('Columns');
        if(tempColumns==''||tempColumns==0||tempColumns==null)
        {
            tempColumns=2;
            me.setColumnParamsRecord(record.get('InteractionField'),'Columns',tempColumns);
        }
        var tempColumnWidth=record.get('ColumnWidth');
        if(tempColumnWidth==''||tempColumnWidth==0||tempColumnWidth==null)
        {
            tempColumnWidth=100;
            me.setColumnParamsRecord(record.get('InteractionField'),'ColumnWidth',tempColumnWidth);
        }
        //时间戳
        var InteractionField = record.get('InteractionField');
        var InteractionFieldArr = InteractionField.split('_');
        InteractionFieldArr[InteractionFieldArr.length-1] = "DataTimeStamp";
        var DataTimeStampCom = InteractionFieldArr.join("_");//表单里的相关组件的时间戳itemId
        var DataTimeStampField = InteractionFieldArr.slice(-2).join("_");//查询后台数据时取值用
        
        var com ={
            xtype: 'checkboxgroup',
            border:false,
            x:record.get('X'),
            y:record.get('Y'),
            fieldLabel: record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            labelAlign:"left",
            height:height,
            width:record.get('Width'),
            itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),//复选框组名称
            columnWidth :tempColumnWidth,
            columns:tempColumns,
            vertical: false,
            //重置复选组items
            resetItems:function(array){
                this.removeAll();
                this.add(array);
            },
            addItems:function(array){ 
                this.add(array);
            }
        }; 
        //第一次生成组件时复选组数据加载处理
        var value=record.get('valueField');
        var text=record.get('textField');
        var groudName=com.itemId;
        var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
        if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var defaultValue=record.get('defaultValue');
                defaultValue=null;
                var data2= me.GetRadiogroupItems(url,groudName,value,text,defaultValue);
                com.items=data2;
        }
          return com;
    },
    /**
     * 创建单选组组件
     * @private
     * @param {} record
     * @return {}
     */
    createRadiogroup:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var tempColumns=record.get('Columns');
        if(tempColumns==''||tempColumns==0||tempColumns==null)
        {
            tempColumns=2;
            me.setColumnParamsRecord(record.get('InteractionField'),'Columns',tempColumns);
        }
        var tempColumnWidth=record.get('ColumnWidth');
        if(tempColumnWidth==''||tempColumnWidth==0||tempColumnWidth==null)
        {
            tempColumnWidth=100;
            me.setColumnParamsRecord(record.get('InteractionField'),'ColumnWidth',tempColumnWidth);
        }
        //复选组初始值没有时间戳修正
        var InteractionField = record.get('InteractionField');
        var InteractionFieldArr = InteractionField.split('_');
        InteractionFieldArr[InteractionFieldArr.length-1] = "DataTimeStamp";
        var DataTimeStampCom = InteractionFieldArr.join("_");//表单里的相关组件的时间戳itemId
        var DataTimeStampField = InteractionFieldArr.slice(-2).join("_");//查询后台用的相关组件的时间戳
     
        var com = {
            xtype:'radiogroup',
            vertical: false,
            border:true,
            name:record.get('InteractionField'),
            x:record.get('X'),
            y:record.get('Y'),
            labelWidth:record.get('LabelWidth'),
            fieldLabel:record.get('DisplayName'),
            width:record.get('Width'),
            height:height,
            columnWidth :tempColumnWidth,
            columns:tempColumns,
            //重置单选框items
            resetItems:function(array){
                this.removeAll();
                this.add(array);
            },
            addItems:function(array){ 
                this.add(array);
            }
        };
        //第一次生成组件时单选组数据加载处理
        var value=record.get('valueField');
        var text=record.get('textField');
        var groudName=com.itemId;
        var url=getRootPath() + "/" + record.get('ServerUrl').split("?")[0] + "?isPlanish=true&where=";
        if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
	        var defaultValue=record.get('defaultValue');
	            defaultValue=null;
	            var data2= me.GetRadiogroupItems(url,groudName,value,text,defaultValue);
	            com.items=data2;
        }
        return com;
    },
    /**
     * 创建纯文本组件
     * @private
     * @param {} record
     * @return {}
     */
    createLabel:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'label',
            name:record.get('InteractionField'),
            height:height,
            width:record.get('Width'),
            text:record.get('DisplayName')
        };
        return com;
    },
    /**
     * 创建displayfield纯文本组件
     * @private
     * @param {} record
     * @return {}
     */
    createDisplayfield:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont').lenght>0)?(record.get('LabFont')):('font-style:normal;');
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'displayfield',
            fieldLabel:fieldLabel,
            labelStyle:"'" + labelStyle + "'" ,
            labelWidth:record.get('LabelWidth'),
            name:"'"+record.get('InteractionField')+"'",
            itemId:"'"+record.get('InteractionField')+"'",
            height:height,
            width:record.get('Width'),
            value:''+record.get('defaultValue')+''
        };
        return com;
    },
    /**
     * 创建数图片组件
     * @private
     * @param {} record
     * @return {}
     */
    createImage:function(record){
        var com = {
            xtype:'image',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            height:(record.get('Height')==0)?(22):record.get('Height'),
            src:getRootPath()+'/ui/css/images/default/defaultImg.png'
        };
        
        if(record.get('ServerUrl') && record.get('ServerUrl') != ""){
            me.src = record.get('ServerUrl');
        }
        
        return com;
    },
    /**
     * 创建超文本组件
     * @private
     * @param {} record
     * @return {}
     */
    createHtmleditor:function(record){
        var com = {
            xtype:'htmleditor',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:(record.get('Height')==0)?(22):record.get('Height'),
            width:record.get('Width')
            
        };
        return com;
    },
    /**
     * 创建文件组件
     * @private
     * @param {} record
     * @return {}
     */
    createFilefield:function(record){
        var com = {
            xtype:'filefield',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:(record.get('Height')==0)?(22):record.get('Height'),
            width:record.get('Width'),
            selectOnFocus: false,
            buttonOnly:false,
            buttonText:'选择'
        };
        
        if(record.SelectFileText && record.SelectFileText != ""){
            com.buttonText = record.SelectFileText;
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
        var isFunctionBtn=record.get('isFunctionBtn');
        var com = {
            xtype:'button',
            name:record.get('functionBtnId'),
            itemId:record.get('functionBtnId'),
            width:22,
            height:22
        };

            com.iconCls='build-button-configuration-blue';
            com.tooltip='请选择树节点';
            com.x=record.get("btnX");
            com.y=record.get("btnY");
            com.margin='0 0 0 2';
            com.isFunctionBtn=true;//是否是功能按钮
            com.boundField=record.get('InteractionField');//record.get('boundField');//功能按钮绑定哪一个的文本框的itemId
        return com;
    },
    /**
     * 打开某一树应用效果窗口
     * @private
     * @param {} title
     * @param {} ClassCode
     */
    openAppShow:function(title,classCode,btnCom,textCom){ 
        var me = this;
        var panel = eval(classCode);
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
        var appList = Ext.create(panel,{
            maxWidth:maxWidth,
            maxHeight:maxHeight,
            autoScroll:true,
            modal:false,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true,//可移动
            selectId:textCom.getValue()//默认选中节点ID(外部调用时传入)
            //hideNodeId:me.Id//默认选中节点ID(外部调用时传入)
            //LevelNum:LevelNum,
            //TreeCatalog:TreeCatalog,
            //ParentName:ParentName
        }).show();

         appList.on({
         //树的确定事件
            okClick:function(){
                var records=appList.getValue();
                if(records.length == 0){
                    Ext.Msg.alert('提示','请选择一个应用！');
                }else if(records.length == 1){
                    me.setTextInfo(records[0],btnCom);
                    appList.close();//关闭应用列表窗口
                }
            },
            //树的双击事件
            itemdblclick:function(view,record,item,index,e,eOpts){
                me.setTextInfo(record,btnCom);
                appList.close();//关闭应用列表窗口
            }
        });
    },
    /***
     * 给展示区域表单绑定功能选择树节点按钮的组件设值
     * @param {} record
     * @param {} com
     */
    setTextInfo:function(record,com){ 
        var me = this;
        //com为绑定的文本框的功能按钮,record为当前选择中的树的节点
        var itemId=com.boundField;
        var winformtext=me.getCenterCom().getComponent(itemId);
        var value=record.get('Id');
        var text=record.get('text');
        
        var strArr="[['"+value+"','"+text+"']]";
        me.setColumnParamsRecord(itemId,'combodata',strArr);
        if(winformtext.xtype=='combobox'){
            var arrTemp=[[value,text]];
            winformtext.store=new Ext.data.SimpleStore({ 
                fields:['value','text'], 
                data:arrTemp 
                ,autoLoad:true
            });
            me.setSouthRecordByKeyValue(itemId,'defaultValue',value);
            //时间戳赋值处理
            var tempArr=itemId.split('_');
            var tempItemId='';
            for(var i=0;i<tempArr.length-1;i++){
                if(i<tempArr.length-1){
                    tempItemId=tempItemId+tempArr[i]+'_';
                }
            }
            var dataTimeStampValue=''+record.get('DataTimeStamp');
            tempItemId=tempItemId+'DataTimeStamp';
            var dataTimeStampCom=me.getCenterCom().getComponent(tempItemId);
            if(dataTimeStampCom){
                dataTimeStampCom.setValue(dataTimeStampValue);
            }
            
            winformtext.setValue(value);
        }else{
	        winformtext.treeNodeID=value;
	        winformtext.setValue(text);
        }
     },
    /***
     * 创建文本框或者定制下拉框的相关的功能按钮的单击事件
     * 弹出一个功能选择树
     */
    createFunctionBtnClick:function(btnCom){ 
         var me=this;
         var textItemId=btnCom.boundField;//功能按钮绑定的文本框的itemId
         var record=me.getSouthRecordByKeyValue("InteractionField",btnCom.boundField);
         var textCom=me.getCenterCom().getComponent(textItemId);//功能按钮绑定的文本框的itemId
         var appComID=record.get("appComID");//取出保存在文本框的应用Id 
          //处理代码
         if(appComID!=""&&appComID!=null&&appComID!=undefined){
            var callback = function(appInfo){
                //中文名称
                var title ='';//
                //类代码
                var ClassCode = '';
                if(appInfo && appInfo != ''){
                    ClassCode = appInfo[me.classCode];
                }
                if(ClassCode && ClassCode != ''){
                    //打开应用效果窗口
                    me.openAppShow(title,ClassCode,btnCom,textCom);
                }else{
                    Ext.Msg.alert('提示','没有类代码！');
                }
            };
            //与后台交互
            me.getInfoByIdFormServer(appComID,callback);
            }else{
                Ext.Msg.alert('提示','功能按钮没有绑定应用！');
            }
    },
    getInfoByIdFormServer:function(id,callback){ 
        var me = this; 
        var myUrl = me.getAppInfoServerUrl+'?isPlanish=true&id='+id; 
        Ext.Ajax.defaultPostHeader = 'application/json'; 
        Ext.Ajax.request({ 
            async:false, //非异步
            url:myUrl, 
            method:'GET', 
            timeout:2000, 
            success:function(response,opts){ 
                var result = Ext.JSON.decode(response.responseText); 
                if(result.success){ 
                    var appInfo = ''; 
                    if(result.ResultDataValue && result.ResultDataValue != ''){ 
                        appInfo = Ext.JSON.decode(result.ResultDataValue); 
                    } 
                    if(Ext.typeOf(callback) == 'function'){ 
                       callback(appInfo); //回调函数
                    } 
                }else{ 
                    Ext.Msg.alert('提示','获取应用信息失败！'); 
                } 
            }, 
            failure:function(response,options){  
                Ext.Msg.alert('提示','获取应用信息请求失败！'); 
            } 
        }); 
     },
    /***
     * 更新展示区域里的所有组件的x,y轴
     * (功能按钮第一次新增)
     * @param {} record
     */
    setAllComXY:function(functionBtnId,newRecord){
        var me=this;
        var records=me.getSouthRecords();
        var center=me.getCenterCom();
       if(newRecord&&newRecord!=undefined){
            //添加新的控件
           var com =me.createfunctionButton(newRecord);
                center.add(com); 
       }    
        Ext.Array.each(records,function(record){
            var itemId =record.get(me.columnParamsField.InteractionField);
            me.setComponentXY(itemId,record.get('X'),record.get('Y'));
            var type=record.get('Type');
            var isFunctionBtn=record.get('isFunctionBtn');
            if(type=='textfield'&&isFunctionBtn==true){
                var functionBtnId=record.get('functionBtnId');
                me.setComponentXY(functionBtnId,record.get('btnX'),record.get('btnY'));
            }
        });
    },
    /***
     * 重新给表单的组件的x,y轴的值设置
     * (功能按钮已经存在)
     */
    setAllCenterComXY:function(){
        var me=this;
        var records=me.getSouthRecords();   
        Ext.Array.each(records,function(record){
            var itemId =record.get(me.columnParamsField.InteractionField);
            me.setComponentXY(itemId,record.get('X'),record.get('Y'));
            var type=record.get('Type');
            var isFunctionBtn=record.get('isFunctionBtn');
            if(type=='textfield'&&isFunctionBtn==true){
                var functionBtnId=record.get('functionBtnId');
                me.setComponentXY(functionBtnId,record.get('btnX'),record.get('btnY'));
            }
        });
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
        
        //选择树的功能按钮处理
        var tempArr=componentItemId.split('_');
        var tempStr=tempArr[tempArr.length-1];
        if(tempStr==me.functionBtnSuffix){
          var record=me.getSouthRecordByKeyValue('functionBtnId',componentItemId);
          if(record){
	          var InteractionField=record.get('InteractionField');
	          me.setSouthRecordByKeyValue(InteractionField,'isFunctionBtn',false);
	          me.setSouthRecordByKeyValue(InteractionField,'IsReadOnly',false);
	          me.setSouthRecordByKeyValue(InteractionField,'functionBtnId','');
	          me.setSouthRecordByKeyValue(InteractionField,'btnX','');
	          me.setSouthRecordByKeyValue(InteractionField,'btnY','');
	          me.setSouthRecordByKeyValue(InteractionField,'appComID','');
	          me.setSouthRecordByKeyValue(InteractionField,'appComCName','');
	          me.setSouthRecordByKeyValue(InteractionField,'boundField','');
          }
        }
        
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
        var grid = me.getSouthCom();
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
        
        if(type == 'combobox'){//下拉框
            otherItems = me.createComboboxfieldItems(componentItemId);
        }else if(type == 'dateintervals'){//日期区间
             otherItems = me.createDateIntervalsfieldItems(componentItemId);
        }else if(type == 'textfield'){//文本框
            otherItems = me.createTextfieldItems(componentItemId);
        }else if(type == 'textareafield'){//文本域
            //otherItems = me.createTextareafieldItems(componentItemId);
        }else if(type == 'numberfield'){//数字框
            otherItems = me.createNumberfieldItems(componentItemId);
        }else if(type == 'datefield'){//日期框
            otherItems = me.createDatefieldItems(componentItemId);
        }else if(type == 'timefield'){//时间框
            otherItems = me.createTimefieldItems(componentItemId);
        }else if(type == 'datetimenew'){//日期时间
            otherItems = me.createDatetimefieldItems(componentItemId);
        }else if(type == 'checkboxgroup'){//:复选组
            otherItems = me.createCheckboxfieldItems(componentItemId);
        }else if(type == 'radiogroup'){//单选组
            otherItems = me.createRadiogroupItems(componentItemId);
        }else if(type == 'label'){//纯文本
            //不做处理
        }else if(type == 'image'){//图片
            //otherItems = me.createImageItems(componentItemId);
        }else if(type == 'htmleditor'){//超文本
            otherItems = me.createhHmleditorItems(componentItemId);
        }else if(type == 'filefield'){//文件
            otherItems = me.createhFilefieldItems(componentItemId);
        }else if(type == 'button'){//按钮
        }else if(type == 'datacombobox'){//定值下拉框
            otherItems = me.createhDataComboboxItems(componentItemId);
        }else if(type == 'dataradiogroup'){//定值单选组
            otherItems = me.createDataRadiogroupItems(componentItemId);
        }else if(type == 'datacheckboxgroup'){//定值复选组
            otherItems = me.createhDataCheckboxgroupItems(componentItemId);
        }else if(type == 'gridcombobox'){//多列下拉框
            otherItems = me.createGridcomboboxItems(componentItemId);
        }else if(type == 'displayfield'){//displayfield
            otherItems = me.createDisplayfieldItems(componentItemId);
        }else if(type == 'colorscombobox'){
            //otherItems = me.createColorscombobox(componentItemId);
        }else if(type == 'radiofield'){//

        }else if(type == 'checkboxfield'){//
            otherItems = me.createSettingCheckBoxItems(componentItemId);
        }
        
        //合并属性(基本属性加上特殊的属性合并渲染)
        var items = basicItems.concat(otherItems);
        com.items = items;
        return com;
    },
    /**
     * 复选框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createSettingCheckBoxItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'复选框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[
              {
                xtype:'textfield',anchor:'100%',
                fieldLabel:'默认值',
                labelWidth:60,
                itemId:'defaultValue',name:'defaultValue',
                emptyText :'true为选中,false为不选中',
                listeners:{
                    blur:function(com,The,eOpts){
                        if(this.value!=null&&this.value!=''){
                            if(this.value&&this.value.length>0){
                                me.setColumnParamsRecord(componentItemId,'defaultValue',this.value);
                            }else{
                                me.setColumnParamsRecord(componentItemId,'defaultValue','');
                            }
                        }else{
                            me.setColumnParamsRecord(componentItemId,'defaultValue','');
                        }
                    }
                }
            }]
        }];
        return items;
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
                xtype:'textfield',fieldLabel:'交互字段',name:'dataIndex',labelWidth:60,anchor:'95%',
                itemId:'dataIndex',readOnly:true
            },{
                xtype:'textfield',fieldLabel:'显示名称',name:'name',labelWidth:60,anchor:'95%',
                itemId:'name',
                listeners:{
                    blur:function(com,The,eOpts){
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var grid = me.getSouthCom();
                        var store = grid.store;
                        var record = store.findRecord('InteractionField',componentItemId);
                        if(record.get('Type') != "button"||record.get('Type') != "image"){//存在
                             radioItem.setFieldLabel(this.value);
                        }else if(record.get('Type') == "button"){
                             radioItem.setText(this.value);
                         }
                        me.setColumnParamsRecord(componentItemId,'DisplayName',this.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'光标顺序',name:'sortNum',labelWidth:60,anchor:'95%',
                itemId:'sortNum',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'sortNum',this.value);
                        //更新设置展示区域的单选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.sortNum = this.value;
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        me.setColumnParamsRecord(componentItemId,'sortNum',newValue);
                        //更新设置展示区域的单选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.sortNum = this.value;
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'高度',name:'myHeight',labelWidth:60,anchor:'95%',
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
            },{
                xtype:'numberfield',fieldLabel:'宽度',name:'myWidth',labelWidth:60,anchor:'95%',
                itemId:'myWidth',minValue:1,
                listeners:{
                    change:function(com,  newValue,oldValue,  eOpts ){
                        me.setColumnParamsRecord(componentItemId,'Width',newValue);
                        //更新设置展示区域的单选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        
                         //当文本框的宽度改变后,文本框组件绑定的功能树选择按钮的x轴值处理
                        var record=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
                        var type=record.get('Type');
                        var isFunctionBtn=record.get('isFunctionBtn');
                        if(type=='textfield'&&isFunctionBtn==true){
                            var tempValue=newValue-oldValue;
                            var btnX=tempValue+record.get('btnX');
                            me.setColumnParamsRecord(componentItemId,'btnX',btnX);
                        
                            me.setAllCenterComXY();
                        }else{
                            radioItem.setSize(newValue);
                        }
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'X轴',name:'X',labelWidth:60,anchor:'95%',
                itemId:'X',minValue:0,
                listeners:{
                    blur:function(com,The,eOpts){
                       
                    },
                    change:function(com,  newValue,  oldValue,  eOpts ){
                         //更新设置展示区域的单选框的X轴                        
                        var x=newValue;                        
                        me.setColumnParamsRecord(componentItemId,'X',x);
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var y=radioItem.y;                        
                        
                        //当文本框的宽度改变后,文本框组件绑定的功能树选择按钮的x轴值处理
                        var record=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
                        var type=record.get('Type');
                        var isFunctionBtn=record.get('isFunctionBtn');
                        if(type=='textfield'&&isFunctionBtn==true){
                            if(oldValue==undefined){
                                oldValue=0;
                            }
                            var tempValue=newValue-oldValue;
                            var btnX=tempValue+record.get('btnX');
                            me.setSouthRecordForNumberfield(componentItemId,'btnX',btnX);
                            me.setAllCenterComXY();
                        }else{
                             me.setComponentX(componentItemId,x,y);
                        }
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'Y轴',name:'Y',labelWidth:60,anchor:'100%',
                itemId:'Y',minValue:0,
                listeners:{
                    change:function(com,  newValue,  oldValue,  eOpts ){
                        var y=newValue;
                        me.setColumnParamsRecord(componentItemId,'Y',y);
                        //更新设置展示区域的单选框的Y轴
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var x=radioItem.x;
                        
                        //当文本框的宽度改变后,文本框组件绑定的功能树选择按钮的y轴值处理
                        var record=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
                        var type=record.get('Type');
                        var isFunctionBtn=record.get('isFunctionBtn');
                        if(type=='textfield'&&isFunctionBtn==true){
                            me.setColumnParamsRecord(componentItemId,'btnY',y);
                           
                            me.setAllCenterComXY();
                        }else{
                            me.setComponentX(componentItemId,x,y);
                        }
                    }
                }
            },{
                xtype:'fieldcontainer',layout:'hbox',
                itemId:'basicStyle',
                items:[{
                    xtype:'label',text:'字体设置:',width:60,margin:'2 0 2 0'
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
            }]
        }];
        return items;
    },
    /**
     *文本框特有属性
     * @private
     * @return {}
     */
    createTextfieldItems:function(componentItemId){ 
        var me = this;
        var itemsArr=[];
        var functionBtn=me.createFunctionBtn(componentItemId);
        itemsArr.push(functionBtn);
        
        var functionBtn2=me.setAppValueBtn(componentItemId);
        itemsArr.push(functionBtn2);
        var items = [{
            xtype:'fieldset',title:'选择树配置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:itemsArr
        }];
        return items;
    },
   /**
     * 功能按钮特有属性--功能按钮给文本框赋值
     * @private
     * @return {}
     */
    setAppValueBtn:function(componentItemId){
        var me=this;
        var com={
                xtype:'fieldcontainer',layout:'hbox',itemId:'winformapp',
                items:[{
                    xtype:'textfield',emptyText:'选择树',
                    width:185,readOnly:true,appComID:'',//appComID类代码应用id属性
                    itemId:'setAppValue',name:'setAppValue',fieldLabel:'选择树',labelWidth:85
                },{
                    xtype:'button',iconCls:'build-button-configuration-blue',
                    tooltip:'选择树',margin:'0 0 0 2',
                    itemId:'setAppValueBtn',name:'setAppValueBtn',
                    handler: function(com,e,eOpts){
                        me.openAppListWin(componentItemId);
                    }
                }]
            };
        return com;
    },
   getEastSetAppValue:function(componentItemId){
        var me=this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var basic = panel.getComponent("otherParams");
        var winformapp = basic.getComponent("winformapp");
        var com = winformapp.getComponent("setAppValue");
        return com;
    },
   getEastisFunctionBtn:function(componentItemId){
        var me=this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var basic = panel.getComponent("otherParams");
        var com = basic.getComponent("isFunctionBtn");
        return com;
    }, 
    /**
     * 文本框,定值下拉框特有属性--功能选择树节点按钮
     * componentItemId:相关文本框的itemId
     * @private
     * @return {}
     */
    createFunctionBtn:function(componentItemId){
        var me=this;
        var com = {
            xtype:'checkbox',fieldLabel:'开启功能按钮',
            name:'isFunctionBtn',labelWidth:90,anchor:'100%',
            itemId:'isFunctionBtn',labelAlign:'right',
            listeners:{
                change:function(com,newValue,oldValue,eOpts){
                     if(newValue==true){
                        //功能按钮的itemId:由绑定的文本框的itemId加后缀名'FunctionBtn'组成
                       var tempItemId=componentItemId.replace(/_/g,".");//不能用下划线
                       var functionBtnId=tempItemId+"."+me.functionBtnSuffix;
                       var center=me.getCenterCom();
                       var functionBtnCom=center.getComponent(functionBtnId);
                       if(functionBtnCom){}else{//功能按钮不存在时才添加
                        
	                       var record=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
	                       if(record){
	                          me.setSouthRecordByKeyValue(componentItemId,"isFunctionBtn",newValue);
	                          var btnX=record.get("X")+165;
	                          var btnY=record.get("Y");
	                          me.setSouthRecordByKeyValue(componentItemId,'functionBtnId',functionBtnId);
	                          me.setSouthRecordByKeyValue(componentItemId,'btnX',btnX);
	                          me.setSouthRecordByKeyValue(componentItemId,'btnY',btnY);
	                          me.setSouthRecordByKeyValue(componentItemId,'isFunctionBtn',true);
	                          var boolValue=true;
	                          me.setSouthRecordByKeyValue(componentItemId,'IsReadOnly',boolValue);
	                          //me.setSouthRecordByKeyValue(componentItemId,'appComID','');
	                          //me.setSouthRecordByKeyValue(componentItemId,'appComCName','');
	                          me.setSouthRecordByKeyValue(componentItemId,'boundField',record.get("InteractionField"));
	                          
	                          var newRecord=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
	                          me.setAllComXY(functionBtnId,newRecord);
	                          //添加组件属性面板
	                          me.addParamsPanel(newRecord.get('Type'),functionBtnId,newRecord.get('DisplayName'));
	                       }
                       }
                     }
                     else if(newValue==false){
                          me.removeComponentTwo(componentItemId,functionBtnId);
                          me.setSouthRecordByKeyValue(componentItemId,'isFunctionBtn',false);
                          me.setSouthRecordByKeyValue(componentItemId,'IsReadOnly',false);
                          me.setSouthRecordByKeyValue(componentItemId,'functionBtnId','');
                          me.setSouthRecordByKeyValue(componentItemId,'btnX','');
                          me.setSouthRecordByKeyValue(componentItemId,'btnY','');
                          me.setSouthRecordByKeyValue(componentItemId,'appComID','');
                          me.setSouthRecordByKeyValue(componentItemId,'appComCName','');
                          me.setSouthRecordByKeyValue(componentItemId,'boundField','');
                          me.setSouthRecordByKeyValue(componentItemId,'defaultValue','');
                          //me.browse();
                       }
                }
            }  
        };
        return com;
    },
    /**
     * 删除展示区域中的表单中的功能按钮组件
     * @private
     * @param {} componentItemId
     */
    removeComponentTwo:function(componentItemId,functionBtnId){
        var me = this;
        //删除数据项组件
        var center = me.getCenterCom();
        center.remove(functionBtnId);//删除功能按钮
        var com=center.getComponent(componentItemId);
            com.setValue('');
        var setAppValue=me.getEastSetAppValue(componentItemId);
        setAppValue.setValue('');

    },
    //=====================功能按钮应用列表=============
    /**
     * 打开应用列表窗口
     * @private
     */
    openAppListWin:function(itemId){
        var me = this;
        var appList = Ext.create('Ext.build.AppListPanel',{
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true,//可移动
            resizable:true,//可变大小
            width:500,
            height:300,
            getAppListServerUrl:me.getAppListServerUrl,
            defaultLoad:false,
            readOnly:true,
            pageSize:9//每页数量
        }).show();
    var where = "";
    //只查询单列树及列表树
    where += "btdappcomponents.AppType=4 or btdappcomponents.AppType=10";
    appList.load(where);
        appList.on({
            okClick:function(){
                var records = appList.getSelectionModel().getSelection();
                if(records.length == 0){
                    Ext.Msg.alert("提示","请选择一个应用！");
                }else if(records.length == 1){
                    me.setWinformInfo(records[0],itemId);
                    appList.close();//关闭应用列表窗口
                }
            },
            itemdblclick:function(view,record,item,index,e,eOpts){
                me.setWinformInfo(record,itemId);
                appList.close();//关闭应用列表窗口
            }
        });
     }, 
    /**
     * 设置弹出应用列表窗口选择行记录后后,表单的相关文本框赋值
     * @private
     * @param {} record
     */
    setWinformInfo:function(record,itemId){
        var me = this;
        if(itemId&&itemId!=undefined&&record!=null){
            var id=record.get('BTDAppComponents_Id');
            var value=record.get('BTDAppComponents_CName');
            me.setSouthRecordByKeyValue(itemId,'IsReadOnly',true);
            me.setSouthRecordByKeyValue(itemId,'appComID',id);
            me.setSouthRecordByKeyValue(itemId,'appComCName',value);
            var com3=me.getEastSetAppValue(itemId);
            com3.setValue(value);
        }
    },
    
    /**
     * 定值下拉框特有属性
     * @private 
     * @return {}
     */
    createhDataComboboxItems:function(componentItemId){
        var me = this;
        var itemsArr=[];
        var oneItem={
                xtype:'textarea',anchor:'100%',height:80,
                itemId:'datacomboValue',name:'datacomboValue',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'combodata',this.value);
                    }
                }
            };
        itemsArr.push(oneItem);
        var defaultValue={
                xtype:'textfield',anchor:'100%',fieldLabel :'默认值',labelWidth:55,
                emptyText:'请输入定值数据的键数据为默认值',
                itemId:'defaultValue',name:'defaultValue',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'defaultValue',this.value);
                    }
                }
            };
        itemsArr.push(defaultValue);
        var functionBtn=me.createFunctionBtn(componentItemId);
        itemsArr.push(functionBtn);
        
        var functionBtn2=me.setAppValueBtn(componentItemId);
        itemsArr.push(functionBtn2);
        
        var items = [{
            xtype:'fieldset',title:'定值数据设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:itemsArr
        }];
        return items;
    },
    /**
     * Displayfield特有属性
     * @private 
     * @return {}
     */
    createDisplayfieldItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'Displayfield设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'htmleditor',anchor:'100%',height:100,
                itemId:'defaultValue',name:'defaultValue',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'defaultValue',this.value);
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 定值单选组特有属性
     * @private
     * @return {}
     */
    createDataRadiogroupItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'定值数据设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'列宽',name:'ColumnWidth',labelWidth:55,anchor:'100%',
                itemId:'ColumnWidth',maxValue:200,minValue:0,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ColumnWidth',this.value);
                        //更新设置展示区域的单选框的列宽
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:55,anchor:'100%',
                maxValue:10,minValue:0,
                itemId:'Columns',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Columns',this.value);
                        //更新设置展示区域的单选框的列宽
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                    }
                }
            },{
                xtype:'textarea',anchor:'100%',height:80,
                itemId:'datacomboValue',name:'datacomboValue',
                listeners:{
                    blur:function(com,The,eOpts){
                        if(this.value!=null&&this.value!=''){
                            var combodata=me.transformItems(this.value,componentItemId);
                            if(combodata&&combodata.length>0){
                                me.setColumnParamsRecord(componentItemId,'combodata',combodata);
                            }else{
                                me.setColumnParamsRecord(componentItemId,'combodata','');
                            }
                        }else{
                            me.setColumnParamsRecord(componentItemId,'combodata','');
                        }
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 定值复选组特有属性
     * @private
     * @return {}
     */
    createhDataCheckboxgroupItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'定值数据设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'列宽',name:'ColumnWidth',labelWidth:55,anchor:'100%',
                itemId:'ColumnWidth',maxValue:200,minValue:5,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ColumnWidth',this.value);
                        //更新设置展示区域的单选框的列宽
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:55,anchor:'100%',
                maxValue:10,minValue:0,
                itemId:'Columns',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Columns',this.value);
                        //更新设置展示区域的单选框的列宽
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                    }
                }
            },{
                xtype:'textarea',anchor:'100%',height:80,
                itemId:'datacomboValue',name:'datacomboValue',
                listeners:{
                    blur:function(com,The,eOpts){
                        if(this.value!=null&&this.value!=''){
                            var combodata=me.transformItems(this.value,componentItemId);
                            if(combodata&&combodata.length>0){
                                me.setColumnParamsRecord(componentItemId,'combodata',combodata);
                            }else{
                                me.setColumnParamsRecord(componentItemId,'combodata','');
                            }
                        }else{
                            me.setColumnParamsRecord(componentItemId,'combodata','');
                        }
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 日期区间特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createDateIntervalsfieldItems:function(componentItemId){
        var me = this;
        //面板ID
        
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        
        var arr = componentItemId.split("_");
        var ClassName = "";
        for(var i=0;i<arr.length-1;i++){
            ClassName = ClassName + arr[i] + "_";
        }
        if(ClassName != ""){
            ClassName = ClassName.slice(0,-1);
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
            xtype:'fieldset',title:'日期区间特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[
            {
                xtype:'textfield',fieldLabel:'名称二',name:'fieldLabelTwo',labelWidth:60,anchor:'100%',
                itemId:'fieldLabelTwo',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'fieldLabelTwo',this.value);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);
                    }
                }
            },
             {
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:60,anchor:'100%',
                itemId:'ShowFomart',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ShowFomart',this.value);
                    }
                }
            },{
                xtype:'checkboxfield',boxLabel:'是否允许手输',name:'CanEdit',anchor:'100%',
                itemId:'CanEdit',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'CanEdit',this.value);
                    }
                }
            },{
                xtype:'radiogroup',fieldLabel:'行列方式',itemId:'RawOrCol',name:'RawOrCol',
                labelWidth:60,columns:3,vertical:true,
                items:[
                    {boxLabel:'行',name:'RawOrCol',inputValue:'hbox'},
                    {boxLabel:'列',name:'RawOrCol',inputValue:'vbox'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'RawOrCol',newValue.RawOrCol);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);
                    }
                }
            }]
        }];
        return items;
    },
    /**
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
            ClassName = ClassName.slice(0,-1);
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
        
        var FieldClass = "";
        if(node){
        	FieldClass = node.data.FieldClass;
            var children = node.childNodes;
            for(var i in children){
                var record = Ext.clone(children[i].data);
                var d = record[me.objectPropertyValueField];
                var arr = d.split('_');
                arr[arr.length-2] = FieldClass;
                record[me.objectPropertyValueField] = arr.slice(-2).join('_');
                data.push(record);
            }
        }
        
        var defaultValue = (data.length > 0) ? data[0][me.objectPropertyValueField] : "";
        
        var defaultValueArr = defaultValue.split("_");
        var text = defaultValueArr.slice(-2).join('_');
        var value = defaultValueArr.slice(-2).join('_');
        
        var items = [{
            xtype:'fieldset',title:'下拉框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'combobox',fieldLabel:'值字段',
                itemId:'valueField',name:'valueField',
                labelWidth:60,anchor:'100%',
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
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr.slice(-2).join("_");
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    },
                    select:function(combo,records,eOpts){
                     var newValue=combo.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr.slice(-2).join("_");
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                            //改变下拉框初始值
                            me.changeComboParamsPanelDefaultValue(componentItemId);
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'显示字段',
                itemId:'displayField',name:'displayField',
                labelWidth:60,anchor:'100%',
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
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr.slice(-2).join("_");
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',value);
                        }
                    },
                    select:function(combo,records,eOpts){
                        
                     var newValue=combo.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr.slice(-2).join("_");
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',value);
                            //改变下拉框初始值
                            me.changeComboParamsPanelDefaultValue(componentItemId);
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'数据地址',
                itemId:'serverUrl',name:'serverUrl',
                labelWidth:60,anchor:'100%',
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
                                var serverUrl = panel.getComponent('otherParams').getComponent('serverUrl');
                                var myUrl='';
                                if(records.length>1){
                                    myUrl=records[1].get(me.objectServerValueField);
                                }else{
                                    myUrl=records[0].get(me.objectServerValueField);
                                }
                                var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                                defaultValue.store.proxy.url = getRootPath() + "/" + myUrl.split("?")[0] + "?isPlanish=true&where=";
                                //改变下拉框初始值
                            	me.changeComboParamsPanelDefaultValue(componentItemId);
                            }
                        }
                    }
                }),
                listeners:{
                    select:function(combo,records,eOpts){
                        //给组件的服务地址赋值
                        var newValue=combo.getValue();
                        var serverUrl =newValue.split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                        
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                        defaultValue.store.proxy.url = getRootPath() + "/" +serverUrl+ "?isPlanish=true&where=";
                        //改变下拉框初始值
                        me.changeComboParamsPanelDefaultValue(componentItemId);
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'默认选中',//下拉框
                itemId:'defaultValue',name:'defaultValue',
                labelWidth:60,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                valueName:value,
                textName:text,
                store:new Ext.data.Store({
                    fields:['text','value'],
                    proxy:{
                        type:'ajax',
                        reader:{type:'json',root:me.objectRoot},
                        
                        extractResponseData:function(response){
                        	var east = me.getComponent('east');
                            var panel = east.getComponent(panelItemId);
                            var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                            
                            var data = Ext.JSON.decode(response.responseText);
                            var ResultDataValue = {count:0,list:[]};
                            if(data.ResultDataValue && data.ResultDataValue != ""){
                            	ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                            }
                            data.ResultDataValue = ResultDataValue.list;
                            for(var i in data.ResultDataValue){
                                data.ResultDataValue[i].value = data.ResultDataValue[i][defaultValue.valueName];
                                data.ResultDataValue[i].text = data.ResultDataValue[i][defaultValue.textName];
                            }
                            
                            //下拉框组件data赋值
                            var com = me.getCenterCom().getComponent(componentItemId);
                            com.store.loadData(data.ResultDataValue);
                            
                            var east = me.getComponent('east');
	                        var panel = east.getComponent(panelItemId);
	                        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                            com.setValue(defaultValue.getValue());
                            //返回处理后的数据
                            response.responseText = Ext.JSON.encode(data);
                            return response;
                        }
                    }
                }),
                listeners:{
                    select:function(combo,records,eOpts){
                        var tempitem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=tempitem.getItemId();
                        var newValue=combo.getValue();
                        if(newValue && newValue != ""){
                            //给组件的默认值赋值
                            var tempValue=combo.getValue();
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
    /***
     * 获取对象树勾选中的字段集
     */
    gettreeCheckedField:function(componentItemId){
        var me = this;
        var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
        var tree = dataObject.getComponent('objectPropertyTree');//对象属性树
        var arr = componentItemId.split("_");
        var ClassName = "";
        for(var i=0;i<arr.length-1;i++){
            ClassName = ClassName + arr[i] + "_";
        }
        if(ClassName != ""){
            ClassName = ClassName.slice(0,-1);
        }
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
        
        return data;
    },
    /**
     * 多列下拉框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createGridcomboboxItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'多列下拉框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'combobox',fieldLabel:'值字段',
                itemId:'valueField',name:'valueField',
                labelWidth:60,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                mode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({ 
                    fields:me.objectPropertyFields,
                    data:(me.isJustOpen==false)?(me.gettreeCheckedField(componentItemId)):[],
                    autoLoad :true
                }),
                listeners:{
                    focus:function(owner,The,eOpts){
                       //值字段赋值
                        owner.store=new Ext.data.Store({ 
                                fields:me.objectPropertyFields, 
                                data:me.gettreeCheckedField(componentItemId),
                                autoLoad :true
                        });
                    },
                    select:function(combo,records,eOpts){
                     var newValue=combo.getValue();
                     if(newValue && newValue != ""){
                            //var arr = newValue.split("_");
                            //var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',newValue);
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'显示字段',
                itemId:'displayField',name:'displayField',
                labelWidth:60,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                mode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({ 
                    fields:me.objectPropertyFields,
                    data:(me.isJustOpen==false)?(me.gettreeCheckedField(componentItemId)):[],
                    autoLoad :true
                }),
                listeners:{
                    focus:function(owner,The,eOpts){
                    //值字段赋值
                     owner.store=new Ext.data.Store({ 
                                fields:me.objectPropertyFields,
                                data:me.gettreeCheckedField(componentItemId),
                                autoLoad :true
                        });
                    },
                    select:function(combo,records,eOpts){
                     var newValue=combo.getValue();
                     if(newValue && newValue != ""){
                            //var arr = newValue.split("_");
                            //var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',newValue);
        
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'数据地址',
                itemId:'serverUrl',name:'serverUrl',
                labelWidth:60,anchor:'100%',
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
                            }
                        }
                    }
                }),
                listeners:{
                    select:function(combo,records,eOpts){
                        //给组件的服务地址赋值
                        var newValue=combo.getValue();
                        var serverUrl =newValue.split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'默认选中',//下拉框
                itemId:'defaultValue',name:'defaultValue',
                labelWidth:60,anchor:'100%',hidden:true,
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                valueName:'',
                textName:'',
                store:new Ext.data.Store({
                    fields:['text','value'],
                    proxy:{
                        type:'ajax',
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:function(response){}
                    }
                })
            },
             {
                xtype:'numberfield',fieldLabel:'最小宽度',name:'minWidth',labelWidth:60,anchor:'100%',
                itemId:'minWidth',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'minWidth',this.value);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        me.setColumnParamsRecord(componentItemId,'minWidth',newValue);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'最大高度',name:'maxHeight',labelWidth:60,anchor:'100%',
                itemId:'maxHeight',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'maxHeight',this.value);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        me.setColumnParamsRecord(componentItemId,'maxHeight',newValue);
                    }
                }
            },{xtype:'combobox',fieldLabel:'列字段',
                multiSelect:true,
                itemId:'gridcomboboxColumns',name:'gridcomboboxColumns',
                labelWidth:60,anchor:'100%',
                editable:true,
                typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                mode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({ 
                    fields:me.objectPropertyFields,
                    data:(me.isJustOpen==false)?(me.gettreeCheckedField(componentItemId)):[],
                    autoLoad :true
                }),
                listeners:{
                    focus:function(owner,The,eOpts){
                        owner.store=new Ext.data.Store({ 
                                fields:me.objectPropertyFields,
                                data:me.gettreeCheckedField(componentItemId),
                                autoLoad :true
                        });
                    },
                    select:function(combo,records,eOpts){
                     var newValue=combo.getValue();
                     if(newValue && newValue != ""){
                            //匹配字段数组赋值
                        var gridcomboboxColumnsCName=combo.getRawValue();
                            me.setColumnParamsRecord(componentItemId,'gridcomboboxColumns',newValue);
                            me.setColumnParamsRecord(componentItemId,'gridcomboboxColumnsCName',gridcomboboxColumnsCName);
                        }
                    }
                }
                },
            {
                xtype:'combobox',fieldLabel:'匹配字段',
                multiSelect:true,
                itemId:'queryFields',name:'queryFields',
                labelWidth:60,anchor:'100%',
                editable:true,
                typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                mode:'local',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({ 
                    fields:me.objectPropertyFields,
                    data:(me.isJustOpen==false)?(me.gettreeCheckedField(componentItemId)):[],
                    autoLoad :true
                }),
                listeners:{
                    focus:function(owner,The,eOpts){
                        owner.store=new Ext.data.Store({ 
                                fields:me.objectPropertyFields,
                                data:me.gettreeCheckedField(componentItemId),
                                autoLoad :true
                        });
                    },
                    select:function(combo,records,eOpts){
                     var newValue=combo.getValue();
                     if(newValue && newValue != ""){
                            //匹配字段数组赋值
                            me.setColumnParamsRecord(componentItemId,'queryFields',newValue);
                        }
                    }
                }
            }
            ]
        }];
        return items;
    },
    /**
     * 复选组特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createCheckboxfieldItems:function(componentItemId){
        var me = this;
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        
        var arr = componentItemId.split("_");
        var ClassName = "";
        for(var i=0;i<arr.length-1;i++){
            ClassName = ClassName + arr[i] + "_";
        }
        if(ClassName != ""){
            ClassName = ClassName.slice(0,-1);
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
            xtype:'fieldset',title:'复选组特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[
              {
                xtype:'numberfield',fieldLabel:'列宽',name:'ColumnWidth',labelWidth:60,anchor:'100%',
                itemId:'ColumnWidth', maxValue:200,minValue:1,
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
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:60,anchor:'100%',
                 maxValue:10,minValue:1,
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
                xtype:'combobox',fieldLabel:'值字段',
                itemId:'valueField',name:'valueField',
                labelWidth:60,anchor:'100%',
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
                     var newValue=combo.getValue();
                     if(newValue && newValue != "" ){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'显示字段',
                itemId:'displayField',name:'displayField',
                labelWidth:60,anchor:'100%',
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
                     var newValue=combo.getValue();
                     if(newValue && newValue != "" ){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'textField',value);
                        }
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'数据地址',
                itemId:'serverUrl',name:'serverUrl',
                labelWidth:60,anchor:'100%',
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
                    }
                }),
                listeners:{
                    select:function(combo,records,eOpts ){
                        var serverUrl =combo.getValue().split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                        var url = getRootPath() + "/" + combo.getValue().split("?")[0] + "?isPlanish=true&where=";
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                       
                        var groudName=radioItem.getItemId();
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,groudName);
                        var value=record.get("valueField");
                        var text=record.get("textField");
                        var data2= me.GetRadiogroupItems(url,groudName,value,text,null);
                            radioItem.resetItems(data2);
                        if(record!=null){
                            var value=record.get("valueField");
                            var text=record.get("textField");
                            var mystore=me.GetComboboxItems(url,value,text);
                            defaultValue.store=mystore;
                        }
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'默认选中',//复选框
                itemId:'defaultValue',name:'defaultValue',
                labelWidth:60,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                store:new Ext.data.Store({
                fields:['value','text']
                }),
                listeners:{
                    select:function(combo,records,eOpts ){
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=radioItem.getItemId();
                        var newValue=combo.getValue();
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
     * 单选组特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createRadiogroupItems:function(componentItemId){
        var me = this;
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var ClassName = "";
        for(var i=0;i<arr.length-1;i++){
            ClassName = ClassName + arr[i] + "_";
        }
        if(ClassName != ""){
            ClassName = ClassName.slice(0,-1);
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
            xtype:'fieldset',title:'单选组特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'列宽',name:'ColumnWidth',labelWidth:60,anchor:'100%',
                itemId:'ColumnWidth', maxValue:200,minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ColumnWidth',this.value);
                        //删除单选组、然后重新添加单选项
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);   
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:60,anchor:'100%',maxValue:20,minValue:1,
                itemId:'Columns',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Columns',this.value);
                        //更新设置展示区域的单选框的列数                  
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'值字段',
                itemId:'valueField',name:'valueField',
                labelWidth:60,anchor:'100%',
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
                     var newValue=combo.getValue();
                     if(newValue && newValue != "" ){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'显示字段',
                itemId:'displayField',name:'displayField',
                labelWidth:60,anchor:'100%',
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
                     var newValue=combo.getValue();
                     if(newValue && newValue != "" ){
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
                labelWidth:60,anchor:'100%',
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
                    listeners:{}
                }),
                listeners:{
                    select:function(combo,records,eOpts ){
                        var serverUrl =combo.getValue().split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        
                        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                        
                        var url = getRootPath() + "/" + combo.getValue().split("?")[0] + "?isPlanish=true&where=";
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var groudName=radioItem.getItemId();
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,groudName);
                        var value=record.get("valueField");
                        var text=record.get("textField");
                        var data2= me.GetRadiogroupItems(url,groudName,value,text,null);
                            radioItem.resetItems(data2);
                            
                       if(record!=null){
                            var value=record.get("valueField");
                            var text=record.get("textField");
                            var mystore=me.GetComboboxItems(url,value,text);
                            defaultValue.store=mystore;
                       }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'默认选中',
                itemId:'defaultValue',name:'defaultValue',
                labelWidth:60,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                store:new Ext.data.Store({
                    fields:['text','value']
                }),
                listeners:{
                    select:function(combo,records,eOpts ){
                        var newValue=combo.getValue();
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
    createTextareafieldItems:function(componentItemId){},
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
                xtype:'numberfield',fieldLabel:'最小值',name:'NumberMin',labelWidth:60,anchor:'100%',
                itemId:'NumberMin',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'NumberMin',this.value);
                    },
                    select:function(combo,records,eOpts ){
                       var value=combo.getValue();
                       me.setColumnParamsRecord(componentItemId,'NumberMin',value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'最大值',name:'NumberMax',labelWidth:60,anchor:'100%',
                itemId:'NumberMax',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'NumberMax',this.value);
                    },
                    select:function(combo,records,eOpts ){
                       var value=combo.getValue();
                       me.setColumnParamsRecord(componentItemId,'NumberMax',value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'增量',name:'NumberIncremental',labelWidth:60,anchor:'100%',
                itemId:'NumberIncremental',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'NumberIncremental',this.value);
                    },
                    select:function(combo,records,eOpts ){
                       var value=combo.getValue();
                       me.setColumnParamsRecord(componentItemId,'NumberIncremental',value);
                    }
                    }
                }
            ]
        }];
        return items;
    },
    /**
     * 
     * 日期框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createDatefieldItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'日期框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:60,anchor:'100%',
                itemId:'ShowFomart',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ShowFomart',this.value);
                    }
                }
            },{
                xtype:'checkboxfield',boxLabel:'是否允许手输',name:'CanEdit',anchor:'100%',
                itemId:'CanEdit',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'CanEdit',this.value);
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 时间框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createTimefieldItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'时间框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:60,anchor:'100%',
                itemId:'ShowFomart',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ShowFomart',this.value);
                    }
                }
            },{
                xtype:'checkboxfield',boxLabel:'是否允许手输',name:'CanEdit',anchor:'100%',
                itemId:'CanEdit',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'CanEdit',this.value);
                    }
                }
            }]
        }];
        return items;
    },
     /**
     * 日期时间框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createDatetimefieldItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'日期时间框特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:60,anchor:'100%',
                itemId:'ShowFomart',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ShowFomart',this.value);
                    }
                }
            },{
                xtype:'checkboxfield',boxLabel:'是否允许手输',name:'CanEdit',anchor:'100%',
                itemId:'CanEdit',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'CanEdit',this.value);
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 图片特有属性--不用
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createImageItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'图片特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'高度',name:'Height',labelWidth:60,anchor:'100%',
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
     * 超文本特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createhHmleditorItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'超文本特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'高度',name:'Height',labelWidth:60,anchor:'100%',
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
     * 文件特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createhFilefieldItems:function(componentItemId){
        var me = this;
        
        var store = me.getSouthCom().store;
        var record = store.findRecord('InteractionField',componentItemId);//是否存在这条记录
        var SelectFileText = "选择";
        if(record){
            SelectFileText = record.get('SelectFileText');
        }
        
        var items = [{
            xtype:'fieldset',title:'文件特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',value:SelectFileText,
            itemId:'otherParams',
            items:[{
                xtype:'textfield',fieldLabel:'按钮文字',name:'SelectFileText',labelWidth:60,anchor:'100%',
                itemId:'SelectFileText',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'SelectFileText',this.value);
                    }
                }
            }]
        }];
        return items;
    },
    //===========================加载处理后台数据================================
    /**
     * 转换定值单/复选框组的子项数据
     * @param {} url
     * @param {} groupName
     * @param {} valueField
     * @param {} displayField
     * @param {} defaultValue[] 默认值
     * @return {}
     */
    transformItems:function(arrTemp,groupName){
        var me=this;
        var arrStr=[];
        var localData="";
        if(arrTemp){
            arrStr=Ext.decode(arrTemp);
        }else{
            arrStr=[];
        }
        
        var count = arrStr.length;
        var tempItem="";
        localData="[";
        for (var i = 0; i <count; i++) { 
            var value=arrStr[i][0];
            var text=arrStr[i][1];
            tempItem=
                "{"+
               "checked:false,"+
               "name:'"+groupName+"',"+
               "inputValue:'"+value+"'," +
               "boxLabel:'"+text+"'"+
            "}";
            if(i<arrStr.length-1){
            tempItem=tempItem+",";
            }
               localData=localData+tempItem;
        }
        localData=localData+"]";
        return localData;
    },
    /**
     * 生成单/复选框组的子项数据
     * @param {} url
     * @param {} groupName
     * @param {} valueField
     * @param {} displayField
     * @param {} defaultValue[] 默认值
     * @return {}
     */
    GetRadiogroupItems:function(myUrl,groupName,valueField,displayField,defaultValue2){
        var me = this;
        var localData=[];
        if(myUrl==""||myUrl==null){
            Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
            return null;
        }else{
        var mychecked=false;var arrStr=[];
        arrStr=defaultValue2;
        
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var ResultDataValue = {count:0,list:[]};
                if(result["ResultDataValue"] && result["ResultDataValue"] != ""){
                    ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                }
                var count = ResultDataValue['count'];
            for (var i = 0; i <count; i++) { 
                var DeptID=ResultDataValue.list[i][valueField];
                var CName=ResultDataValue.list[i][displayField];
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
    }
        return localData;
    },
     /**
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
                var ResultDataValue = {count:0,list:[]};
                if(result["ResultDataValue"] && result["ResultDataValue"] != ""){
                    ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                }
                var count = ResultDataValue['count'];
            for (var i = 0; i <count; i++) { 
                var value=ResultDataValue.list[i][value2];
                var text=ResultDataValue.list[i][text2];
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
     * 获取展示区域
     * @private
     * @return {}
     */
    getCenterCom:function(){
        var me = this;
        var center = me.getComponent('center').getComponent('center');
        return center;
    },
    /**
     * 获取数据项属性列表组件
     * @private
     * @return {}
     */
    getSouthCom:function(){
        var me = this;
        var south = me.getComponent('south').getComponent('south1');
        return south;
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
        
        params.Width = parseInt(params.Width);
        params.Height = parseInt(params.Height);
        
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
        var store = me.getSouthCom().store;
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
        var south = me.getSouthCom();
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
        var grid = me.getSouthCom();
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
        var grid = me.getSouthCom();
        var store = grid.store;
        var record = store.findRecord('InteractionField',InteractionField);
        var index=store.find('InteractionField',InteractionField);
        if(record != null){//存在
            record.set(key,value);
            record.commit();
            grid.getView().refresh();               
            grid.getSelectionModel().select(index); 
           //alert('调用次数显示');
            
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
        var list = me.getSouthCom();//列属性列表
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
        var list = me.getSouthCom();//列属性列表
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
        var store = me.getSouthCom().store;
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
            fields = fields.slice(0,-1);
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
        //光标顺序
        basic.getComponent("sortNum").setValue(record.get('sortNum'));
        
        name.setValue(record.get('DisplayName'));
        
        var dataIndex = basic.getComponent("dataIndex");
        dataIndex.setValue(record.get('InteractionField'));
    },
    /**
     * 属性面板基础数据X,Y赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setBasicParamsPanelValuesForXY:function(componentItemId,record){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var basic = panel.getComponent("basicParams");

        var myX=basic.getComponent("X");
        var myY = basic.getComponent("Y");
        myX.setValue(record.get('X'));
        myY.setValue(record.get('Y'));
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
            var isFunctionBtn=me.getEastisFunctionBtn(componentItemId);
            var setAppValue=me.getEastSetAppValue(componentItemId);
            setAppValue.setValue(record.get("appComCName"));
            if(record.get("isFunctionBtn")==true){
                isFunctionBtn.setValue(true); 
            }else{
                isFunctionBtn.setValue(false);
            }
        }
        //给定值单选组,复选组特有属性设置默认值
        if(type == "dataradiogroup"||type == "datacheckboxgroup"){
            me.setDataCheckboxParamsPanelValue(componentItemId,record);
        }
        //文本框特有属性设值
        if(type == "textfield"){
            var isFunctionBtn=me.getEastisFunctionBtn(componentItemId);
            var setAppValue=me.getEastSetAppValue(componentItemId);
            setAppValue.setValue(record.get("appComCName"));
            if(record.get("isFunctionBtn")==true){
                isFunctionBtn.setValue(true); 
            }else{
                isFunctionBtn.setValue(false);
            }
        }
        //数字框特有属性设值
        else if(type == "numberfield"){
            me.setNumberParamsPanelValue(record,componentItemId);
        }
        //给下拉列表,单选组,复选组特有属性设置默认值
        else if(type=='combobox'||type=='gridcombobox'){
            me.setComboboxValue(record,componentItemId);
        }
        //日期,日期时间特有属性设值
        else if(type == "datefield"||type == "datetimenew"){
            me.setDatetimeParamsPanelValue(record,componentItemId);
        }
        //时间特有属性设值
        else if(type == "timefield"){
            me.setTimeParamsPanelValue(record,componentItemId);
        }
        //日期区间特有属性设值
        else if(type =="dateintervals"){
            me.setDateIntervalsParamsPanelValue(record,componentItemId);
        }
        //设置复选框特有属性设值
        else if(type =="checkboxfield"){
            me.setSettingCheckBoxValue(record,componentItemId);
        }
        //设置单/复选组特有属性设值
        else if(type=='radiogroup'||type=='checkboxgroup'){
            me.setRadiogroupValue(record,componentItemId);
        }
    },
    /**
     * 设置单选组特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setRadiogroupValue:function(record,InteractionField){
        var me = this;
        //属性面板ItemId
        var panelItemId = InteractionField + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        
        var me=this;
        var type= record.get('Type');
        var serverUrl=record.get("ServerUrl");
        var valueField=record.get("valueField");
        var displayField=record.get("textField");
        
        var columnWidth =record.get("ColumnWidth");
        var columns=record.get("Columns");
       
        me.setOtherParamsForInteractionField(InteractionField,'ColumnWidth',columnWidth);
        me.setOtherParamsForInteractionField(InteractionField,'Columns',columns);
        var arr = InteractionField.split("_");
        if(arr.length>2){
            var objectName=me.getobjectNameValue();
            valueField=objectName+'_'+valueField; 
            displayField=objectName+'_'+displayField;
        }
        me.setOtherParamsForInteractionField(InteractionField,'valueField',valueField);
        me.setOtherParamsForInteractionField(InteractionField,'displayField',displayField);
        if(serverUrl!=""&&serverUrl!=null){
            if(serverUrl.slice(-5) == "ByHQL"){
                var parts="?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}";
                serverUrl=(serverUrl+parts);
            }
            me.setOtherParamsForInteractionField(InteractionField,'serverUrl',serverUrl);
       }
    
    },
    /**
     * 设置复选框特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setSettingCheckBoxValue:function(record,componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        var defaultValue = record.get('defaultValue');
        var defaultValueCom = other.getComponent('defaultValue');
        defaultValueCom.setValue(defaultValue);
    },
    /**
     * 日期区间特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setDateIntervalsParamsPanelValue:function(record,componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        
        var fieldLabelTwoCom = other.getComponent('fieldLabelTwo');
        var showFomartCom = other.getComponent('ShowFomart');
        var canEditCom = other.getComponent('CanEdit');
        var rawOrColCom = other.getComponent('RawOrCol');
        
        var fieldLabelTwo = record.get('fieldLabelTwo');
        var showFomart = record.get('ShowFomart');
        var canEdit = record.get('CanEdit');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
            me.setSouthRecordByKeyValue(componentItemId,'ShowFomart',showFomart);
        }
        fieldLabelTwoCom.setValue(fieldLabelTwo);
        showFomartCom.setValue(showFomart);
        canEditCom.setValue(canEdit);
        
        var rawOrCol=record.get('RawOrCol');
        if(rawOrCol==''||rawOrCol==""||rawOrCol==null){
            rawOrCol='hbox';
        }
        var isRawOrCol="'"+rawOrCol+"'";
        var valueRawOrCol="{RawOrCol:["+isRawOrCol+"]}";
        var myRawOrColJson=Ext.decode(valueRawOrCol);
        rawOrColCom.setValue(myRawOrColJson);
        
    },
    /**
     * 时间特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setTimeParamsPanelValue:function(record,componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        
        var showFomartCom = other.getComponent('ShowFomart');
        var canEditCom = other.getComponent('CanEdit');
       
        var showFomart = record.get('ShowFomart');
        var canEdit = record.get('CanEdit');
        
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='H:i';
            me.setSouthRecordByKeyValue(componentItemId,'ShowFomart',showFomart);
        }
        
        
        showFomartCom.setValue(showFomart);
        canEditCom.setValue(canEdit);
        
    },
    /**
     * 日期,时间,日期时间特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setDatetimeParamsPanelValue:function(record,componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        
        var showFomartCom = other.getComponent('ShowFomart');
        var canEditCom = other.getComponent('CanEdit');
       
        var showFomart = record.get('ShowFomart');
        var canEdit = record.get('CanEdit');
        
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d H:i:s';
            me.setSouthRecordByKeyValue(componentItemId,'ShowFomart',showFomart);
        }
        showFomartCom.setValue(showFomart);
        canEditCom.setValue(canEdit);
        
    },
    /**
     * 数字框特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setNumberParamsPanelValue:function(record,componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        
        var numberMinCom = other.getComponent('NumberMin');
        var NumberMaxCom = other.getComponent('NumberMax');
        var numberIncrementalCom = other.getComponent('NumberIncremental');
       
        var numberMin = record.get('NumberMin');
        var numberMax = record.get('NumberMax');
        var numberIncremental = record.get('NumberIncremental');
        
        numberMinCom.setValue(numberMin);
        NumberMaxCom.setValue(numberMax);
        numberIncrementalCom.setValue(numberIncremental);
    },
    /***
     * 给下拉列表,单选组,复选组特有属性设置默认值
     * @param {} record
     * @param {} InteractionField
     */
    setComboboxValue:function(record,InteractionField){
        var me=this;
        var type= record.get('Type');
        var serverUrl=record.get("ServerUrl");
        var valueField=record.get("valueField");
        var displayField=record.get("textField");
        var value=record.get("valueField");
        var text=record.get("textField");
        //多列下拉框
        if(type=='gridcombobox'){
            var queryFields=record.get('queryFields');
            me.setOtherParamsForInteractionField(InteractionField,'queryFields',queryFields);
            var minWidth= record.get('minWidth');
            var maxHeight= record.get('maxHeight');
            me.setOtherParamsForInteractionField(InteractionField,'minWidth',minWidth);
            me.setOtherParamsForInteractionField(InteractionField,'maxHeight',maxHeight);
            //属性面板
            var componentItemId=record.get('InteractionField');
	        var panelItemId = record.get('InteractionField') + me.ParamsPanelItemIdSuffix;
	        //组件属性面板
	        var panel = me.getComponent('east').getComponent(panelItemId);
	        var other = panel.getComponent("otherParams");
	        var valueField = other.getComponent('valueField');
            var displayField = other.getComponent('displayField');
            var gridcomboboxColumns = other.getComponent('gridcomboboxColumns');
            var queryFields = other.getComponent('queryFields');
            var datacomboValue = other.getComponent('datacomboValue');
	        var value = record.get('combodata');
	        if(!value || value == ""){
	            value = "[]";
	        }else{
                datacomboValue.setValue(value);
            }
	        valueField.store=new Ext.data.Store({ 
                        fields:me.objectPropertyFields, 
                        data:me.gettreeCheckedField(componentItemId),
                        autoLoad :true
            });
            displayField.store=new Ext.data.Store({ 
                        fields:me.objectPropertyFields, 
                        data:me.gettreeCheckedField(componentItemId),
                        autoLoad :true
            });
            gridcomboboxColumns.store=new Ext.data.Store({ 
                        fields:me.objectPropertyFields, 
                        data:me.gettreeCheckedField(componentItemId),
                        autoLoad :true
            });
            queryFields.store=new Ext.data.Store({ 
                        fields:me.objectPropertyFields, 
                        data:me.gettreeCheckedField(componentItemId),
                        autoLoad :true
            });
            //
            var gridcomboboxColumns=record.get('gridcomboboxColumns');
            if(gridcomboboxColumns.length>0){
                me.setOtherParamsForInteractionField(InteractionField,'gridcomboboxColumns',gridcomboboxColumns);
            }
            var queryFields=record.get('queryFields');
            if(queryFields.length>0){
                me.setOtherParamsForInteractionField(InteractionField,'queryFields',queryFields);
            }
        }
        me.setOtherParamsForInteractionField(InteractionField,'valueField',valueField);
        me.setOtherParamsForInteractionField(InteractionField,'displayField',displayField);
        
        if(serverUrl!=""&&serverUrl!=null){
            if(serverUrl.slice(-5) == "ByHQL"){
                var parts="?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}";
                serverUrl=(serverUrl+parts);
            }
            me.setOtherParamsForInteractionField(InteractionField,'serverUrl',serverUrl);
            }
     	if(type=='combobox'){
     		//改变下拉框初始值
     		var defaultValue = record.get('defaultValue');
     		if(defaultValue){
     			var panelItemId = InteractionField + me.ParamsPanelItemIdSuffix;
		    	var east = me.getComponent('east');
		        var panel = east.getComponent(panelItemId);
		        var defaultValueCom = panel.getComponent('otherParams').getComponent('defaultValue');
		        defaultValueCom.value = defaultValue;
     		}
            me.changeComboParamsPanelDefaultValue(InteractionField);
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
        var defaultValueCom = other.getComponent('defaultValue');
        var defaultValue = record.get('defaultValue');
        defaultValueCom.setValue(defaultValue);
    },
    /**
     * 定值单/复选组属性面板特有数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setDataCheckboxParamsPanelValue:function(componentItemId,record){
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
        }else{
            var arrTemp=[];
            arrTemp=Ext.decode(value);
            if(arrTemp){
                value = "[";
                for(var i=0;i<arrTemp.length;i++){
                    var inputValue=arrTemp[i]['inputValue'];
                    var boxLabel=arrTemp[i]['boxLabel']
                    value =value+ ("["+"'"+inputValue+"',"+"'"+boxLabel+"'"+"]");
                    if(i<arrTemp.length-1){
                        value =value+","
                    }
                }
                value =value+ "]";
            }else{
                value = "[]";
            }
        }
        datacomboValue.setValue(value);
        
        var columns = other.getComponent('Columns');
        var columnWidth = other.getComponent('ColumnWidth');
        var columnsValue = record.get('Columns');
        var columnWidthValue = record.get('ColumnWidth');
        columns.setValue(columnsValue);
        columnWidth.setValue(columnWidthValue);
    },
    /**
     * 组件监听
     * @private
     * @param {} com
     * @param {} arr
     */
    setComListeners:function(com,arr){
        var me = this;
        var sortCount = 0;
        for(var i in arr){
            if(arr[i].sortNum > 0){
                sortCount++;
            }
        }
        com.listeners = {//move和resize有冲突
            scope:this,
            //组件拖动监听
            move:function(com,x,y,eOpts){
                if(com.xtype=='button'){
                    me.setColumnParamsRecord(com.boundField,'btnX',x);
                    me.setColumnParamsRecord(com.boundField,'btnY',y);
                }else{
	                me.setColumnParamsRecord(com.itemId,'X',x);
	                me.setColumnParamsRecord(com.itemId,'Y',y);

                }
            },
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                if(com.xtype=='button'){
                }else{
	                me.setColumnParamsRecord(com.itemId,'Width',width);
	                me.setColumnParamsRecord(com.itemId,'Height',height);
                }
            },
            click:{
                element:'el',
                fn:function(e){
                    //如果是功能按钮
                    if(com.xtype=='button'){
                        var isFunctionBtn=com.isFunctionBtn;
                        var textItemId=com.boundField;
                        if(isFunctionBtn==true){
                           me.createFunctionBtnClick(com);
                        }
                    }else{
                        //切换组件属性配置面板
                        me.switchParamsPanel(com.itemId);
                    }
                    
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
            },
            specialkey:function(field,e){
                //光标跳跃的间隔数
                var iNum = 1;
                //组件中的排序属性字段
                var sNumField = "sortNum";
                var form = field.ownerCt;
                var num = field[sNumField];
                var items = form.items.items;
                //整个表单的所有内部组件的个数
                var max = sortCount;
                //屏蔽浏览器默认的动作
                if(e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB){
                    e.preventDefault();
                }
                
                //回车键监听、ATB键,num=0是不需要光标的组件
                if(num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)){
                    if(!e.shiftKey){//同时按下shift键
                        num = (num+iNum > max) ? num+iNum-max : num+iNum;
                    }else{
                        num = (num-iNum < 1) ? num-iNum+max : num-iNum;
                    }
                    for(var i in items){
                        if(items[i].sortNum == num){
                            items[i].focus(false,100);break;
                        }
                    }
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
        var store = me.getSouthCom().store;
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
     * 更改所有数据项的对齐方式
     * @private
     */
    changeComLabelAlign:function(){
        var me = this;
        var params = me.getPanelParams();
        var store = me.getSouthCom().store;
        var data = store.data;
        if(params.allLabelAlign != ""){
            for(var i=0;i<data.length;i++){
                var record = data.getAt(i);
                if(params.allLabelAlign != "")
                    record.set('AlignType',params.allLabelAlign);
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
            {name:'LabFont',type:'string'},//显示名称字体内容
            {name:'Type',type:'string'},//数据项类型
            {name:'X',type:'int'},//位置X
            {name:'Y',type:'int'},//位置X
            {name:'Width',type:'int'},//数据项宽度
            {name:'Height',type:'int'},//高度
            {name:'NumberMin',type:'int'},//数字最小值
            {name:'NumberMax',type:'int'},//数字最大值
          
            {name:'ColumnWidth',type:'int'},//列宽
            {name:'Columns',type:'int'},//列数
            {name:'valueField',type:'string'},//值字段(下拉框)
            {name:'textField',type:'string'},//显示字段(下拉框)
            {name:'defaultValue',type:'auto'},//默认值
            {name:'IsReadOnly',type:'bool'},//只读
            {name:'IsHidden',type:'bool'},//隐藏
            {name:'NumberIncremental',type:'int'},//数字增量
            {name:'ShowFomart',type:'string'},//显示格式
            {name:'CanEdit',type:'bool'},//是否允许手输
            {name:'ServerUrl',type:'string'},//数据地址
            {name:'RawOrCol',type:'string'},//行列方式
            {name:'SelectFileText',type:'string'},//选择文件按钮文字
            {name:'LayoutType',type:'string'},//日期区间布局方式
            {name:'fieldLabelTwo',type:'string'},//日期区间第二个控件显示名称
            {name:'combodata',type:'string'},//定值下拉框的内容
            {name:'sortNum',type:'int'},//光标顺序号
            {name:'AlignType',type:'string'},//对齐方式
            
            //文本框功能按钮相关
            {name:'isFunctionBtn',type:'bool'},//是否开启功能按钮
            {name:'appComID',type:'string'},//文本框绑定的功能按钮选择后的隐藏值
            {name:'appComCName',type:'string'},//文本框绑定的功能按钮选择后的显示值
            {name:'boundField',type:'string'},//功能按钮绑定相关文本框itemId
            {name:'btnX',type:'int'},//功能按钮X轴
            {name:'btnY',type:'int'},//功能按钮Y轴
            {name:'functionBtnId',type:'string'},//功能按钮绑定相关文本框itemId
            
            {name:'gridcomboboxColumns',type:'string'},//多列下拉框列英文字段
            {name:'gridcomboboxColumnsCName',type:'string'},//多列下拉框列中文字段
            {name:'queryFields',type:'string'},//多列下拉框匹配字段数组
            {name:'minWidth',type:'int'},//多列下拉列表框的最小宽度
            {name:'maxHeight',type:'int'}//多列下拉列表框的最大高度
            
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
        var south2Params = me.getSouth2RocordInfoArray();
        var appParams = {
            panelParams:panelParams,
            southParams:southParams,
            south2Params:south2Params
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
            //var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
            
            var appParams = Ext.JSON.decode(appInfo['DesignCode']);
			me.BTDAppComponentsOperateList = appInfo['BTDAppComponentsOperateList'];
            
            var panelParams = appParams.panelParams;
            var southParams = appParams.southParams;
            var south2Params = appParams.south2Params;
            if(panelParams.formHtml && panelParams.formHtml != ""){
            	panelParams.formHtml = panelParams.formHtml.replace(/@@/g,"'");
            }
            var hasLab = panelParams.hasLab || false;
            me.getComponent('north').getComponent('hasLab').setValue(hasLab);
            
            me.setAppInfo(appInfo);
            
            objectPropertyTree.store.on({
                load:function(store,node,records,successful,e){
                    if(me.appId != -1 && me.isJustOpen && node == objectPropertyTree.getRootNode()){
                        //对象内容勾选
                        me.changeObjChecked(southParams);
                    }
                }
            });
            //设置面板大小
			var center = me.getCenterCom();
			center.setWidth(panelParams.Width);
			center.setHeight(panelParams.Height);
	       	//赋值
	       	me.setSouthRecordByArray(southParams);//数据项列表赋值
	       	me.setSouth2RecordByArray(south2Params);//自定义按钮属性列表赋值
	       	me.setObjData();//数据对象赋值
	       	me.setPanelParams(panelParams);//属性面板赋值
	       	
	       	if(!panelParams.objectName || panelParams.objectName == ""){
	       		me.browse();
	       	}
			
			//获取获取数据服务列表
			var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
			getDataServerUrl.value = panelParams.getDataServerUrl;
			//获取新增数据服务列表
			var addDataServerUrl = dataObject.getComponent('addDataServerUrl');
			addDataServerUrl.value = panelParams.addDataServerUrl;
			//获取修改数据服务列表
			var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
			editDataServerUrl.value = panelParams.editDataServerUrl;
        };
        //从后台获取应用信息
        //me.getAppInfoFromServer(me.appId,callback);
        me.ParserForm.getAppInfoById(me.appId,callback);
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
     * 获取数据对象名称
     * @private
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
     * 获取数据对象名称
     * @private
     */
    getobjectNameValue:function(){
        var me = this;
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        //数据对象
        var dataObject = paramsPanel.getComponent('dataObject');
        //数据对象类
        var objectName = dataObject.getComponent('objectName');
        var value=objectName.getValue();
        return value;
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
            //changeNodes(0);
            //延时500毫秒处理
            setTimeout(function(){changeNodes(0);},500);
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

        //内部组件代码
        var items = me.createComponentsStr();
        
        //加载表单数据方法
        var loadData = me.createLoadDataStr(params.getDataServerUrl);
        //保存表单数据方法:依据构建表单类型生成表单的"保存/查询"按钮处理方法
        var submitData ="";
            submitData = me.createSubmitDataStr(params.addDataServerUrl);
        var dockedItems = me.createDockedItems();//停靠栏
        var hasTitle=params.hasTitle;
        if(hasTitle==undefined){
            hasTitle=false;
        }
        //--------------尽量提取出来，不要混在一起，不利于维护--------------
        var GetGroupItems = 
        "function(url2,valueField,displayField,groupName,defaultValue,dataTimeStampField){"+
            "var myUrl=url2;"+
            "if(myUrl==''||myUrl==null){" + 
                "Ext.Msg.alert('提示',myUrl);return null;" + 
            "}" + 
            "else{" + 
                "myUrl=getRootPath()+myUrl;" + 
            "}" + 
            "var localData=[];" + 
            "Ext.Ajax.request({" + 
                "async:false" +","+ 
                "timeout:6000" +","+ 
                "url:myUrl,"+    
                "method:'GET'" +","+ 
                "success:function(response,opts){" + 
                    "var result = Ext.JSON.decode(response.responseText);" +
                    "if(result.success){" +
                        "var ResultDataValue = {count:0,list:[]};" + 
                        "if(result['ResultDataValue'] && result['ResultDataValue'] != ''){" + 
                            "ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);" + 
                        "}" + 
                        "var count = ResultDataValue['count'];" +
                        "var mychecked=false;var arrStr=[];"+
                        "if(defaultValue!=''){"+
                            "arrStr=defaultValue.split(',');"+
                        "}"+
                        "for(var i=0;i<count;i++){" +
                            "var DeptID=ResultDataValue.list[i][valueField];" +
                            "var CName=ResultDataValue.list[i][displayField];" +
                            "var dataTimeStamp=ResultDataValue.list[i][dataTimeStampField];" +
                            "if(arrStr.length>0){"+
                                "mychecked=Ext.Array.contains(arrStr,DeptID);"+
                            "}"+
                            "var tempItem={" +
                                "checked:mychecked" +"," +
                                "name:groupName," +
                                "boxLabel:CName" +"," +
                                "inputValue:DeptID," +
                                "DataTimeStamp:''+dataTimeStamp" +
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
        
        var lists=Ext.JSON.decode(items);
        var strtemp='';
        Ext.each(lists,function(item,index,itemAll){
            var type=item.xtype
            //如果控件类型为单选/复选组时,处理其类生成代码的加载数据显示
            if(type== 'checkboxgroup'||type== 'radiogroup'){
                var isdataValue=item.isdataValue;
                if(isdataValue==false){//不是定值的单/复选组才添加
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
                    "var array"+index+"=me.GetGroupItems("+"'"+item.tempUrl+"',"+"'"+item.tempValue+"',"+"'"+item.tempText+"',"+"'"+item.tempGroupName+"',"+"'"+arrStr+"',"+"'"+item.DataTimeStampField+"'"+");"+                
                    "var item"+index+"=me.getComponent('"+item.itemId+"');"+
                    "item"+index+".removeAll();"+
                    "item"+index+".add(array"+index+");"
             }
            }
        });
        //----------------------------------------------------------------------
        //自定义按钮Str
        var customButtons = me.createCustomButtonsStr();
        items = items.slice(0,-1) + ((items == "[]") ? "" : ",") + customButtons + "]";
        
        //背景html
        var html = params.formHtml;
        //获取应用列表服务地址
        var getAppListServerUrl="ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL";
        var appClass = 
        "Ext.define('" + params.appCode + "',{" + 
            "extend:'Ext.form.Panel'," + 
            "alias:'widget." + params.appCode + "'," + 
            "title:'" + params.titleText + "'," + 
            "width:" + params.Width + "," + 
            "height:" + params.Height + "," + 
            //表单的拼音头事件的处理
            "isLoadingComplete:false"+ "," + 
            "ParentID:0," + //树形结构父级ID
            "LevelNum:1," + //树形结构层级
            "TreeCatalog:1," + //树形结构层级Code
            "ParentName:''," + //树形结构父级名称
            "setdataTimeStampValue:true," +//功能按钮弹出选择树后,双击选择节点后是否更新dataTimeStamp的值,默认更新
            
            "header:" + hasTitle + "," +  //标题显示/隐藏:false
			"objectName:'" + params.objectName +"'," + //对象名，用于自动主键匹配
            "isSuccessMsg:true"+ "," + //保存成功后是否弹出提示信息开关,是:true,否:false
            
            //依应用id获取某一元应用信息url
            "getAppInfoServerUrl:getRootPath()+"+"'/'+"+"'"+"ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById"+"',"+
            
            "addDataServerUrl:'" + params.addDataServerUrl + "'," + 
            "editDataServerUrl:'" + params.editDataServerUrl + "'," +
            
            "classCode:'BTDAppComponents_ClassCode'," +//应用列表对象的类代码字段
            "autoScroll:true," + 
            "type:'" + (params.defaultType || "show") + "'," + //显示方式add（新增）、edit（修改）、show（查看）
            "bodyCls:"+"'"+params.panelStyle+"'," +     //添加背景颜色*******************************
            "layout:'absolute'," + 
            
            "setWinformInfo:"+ me.setWinformInfoStr()+"," +//
            
            "getInfoByIdFormServer:"+ me.getInfoByIdFromServerStr()+"," +//
            "functionBtnClick:"+ me.createFunctionBtnClickStr()+"," +//创建文本框的相关的功能按钮的单击事件
            "openAppShowWin:"+ me.openAppShowWinStr()+"," +//打开某一元应用效果窗口
            
            "GetGroupItems:" + GetGroupItems + ","; 
            
            appClass=appClass+
            "beforeRender:function(){" +
            "var me=this;" + 
            "me.callParent(arguments);" +
            "if (!(me.header === false)) {" +
                "me.updateHeader();" +
             "}" +
            "},";
              
            appClass=appClass+
            "initComponent:function(){" + 
                "var me=this;" + 
                "me.defaultTitle=me.title||'';" + 
                //注册事件
                "me.addEvents('beforeSave');" + 
                "me.addEvents('saveClick');" + 
                "me.addEvents('functionBtnClick');" + 
                "me.addEvents('default1buttonClick');" + //自定义一
                "me.addEvents('default2buttonClick');" + //自定义二
                "me.addEvents('default3buttonClick');" + //自定义三
                //功能按钮单击事件
                //"me.fireEvent('functionBtnClick');"+
                //自定义按钮弹出窗口的监听
                me.createCustomButtonsEventStr() + 
                
                //对外公开方法
                "me.load=" + loadData + ";" + 
                "me.submit=" + submitData + ";" + 
                "me.isAdd = function(){" + 
                	"me.setTitle(me.defaultTitle+'-新增');" + 
                    "me.getForm().reset();" + 
                    "var buts = me.getComponent('dockedItems-buttons');" + 
                    "if(buts){" +
                        "if(me.type == 'show'){me.setHeight(me.getHeight()+25);}" + 
                        "buts.show();" + 
                    "}" + 
                    "me.type='add';" + 
                    "me.setReadOnly(false);" + 
                    "me.getForm().reset();" + 
                "};" + 
                "me.isEdit = function(id){" + 
                    "var buts = me.getComponent('dockedItems-buttons');" + 
                    "if(buts){" +
                        "if(me.type == 'show'){me.setHeight(me.getHeight()+25);}" + 
                        "buts.show();" +
                    "}" + 
                    "me.type='edit';" + 
                    "me.setReadOnly(false);" + 
                    "if(id&&id!=-1&id!='-1'){me.load(id);}" + 
                "};" + 
                "me.isShow = function(id){" + 
                    "var buts = me.getComponent('dockedItems-buttons');" + 
                    "if(buts){" +
                        "if(me.type != 'show'){me.setHeight(me.getHeight()-25);}" + 
                        "buts.hide();" +
                    "}" + 
                    "me.type='show';" + 
                    "me.setReadOnly(true);" + 
                    "if(id&&id!=-1&id!='-1'){me.load(id);}" + 
                "};" + 
                //根据内部编号赋值（公开）
                "me.setValueByItemId=function(key,value){" + 
                	"me.getForm().setValues([{id:key,value:value}]);" + 
                "};" + 
                //内部数据匹配方法
                "me.changeStoreData=function(response){" + 
                    "var data = Ext.JSON.decode(response.responseText);" + 
                    "if(data.ResultDataValue && data.ResultDataValue !=''){" + 
                        "var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);" + 
                        "data.ResultDataValue = ResultDataValue;" +
                        "data.list = ResultDataValue.list;" + 
                    "}else{" + 
                        "data.list=[];" + 
                    "}" + 
                    "response.responseText = Ext.JSON.encode(data);" + 
                    "return response;" + 
                "};" + 
                //内部是否只读方法
                "me.setReadOnly=function(bo){" + 
                    "var items2 = me.items.items;" + 
                    "for(var i in items2){" + 
                    	"if(!items2[i].hasReadOnly){" + 
	                    	"var type=items2[i].xtype; " + 
	                    	"if(type=='button'||type=='label'){" +//button没有setReadOnly方法
                             "items2[i].setDisabled(bo);" +
//	                    		"if(bo){" + 
//	                    			"items2[i].disable();" + 
//	                    		"}else{" + 
//	                    			"items2[i].enable();" + 
//	                    		"}" + 
	                        "}else{" +
	                        	"items2[i].setReadOnly(bo);" +
	                        "}" + 
	                	"}" + 
                    "}" + 
                "};" + 
                //内部组件
                "me.items=" + items + ";";
                
            if(dockedItems && dockedItems != ""){
                appClass = appClass + 
                "if(me.type == 'show'){" + 
                	"me.dockedItems=" + dockedItems + ";" + 
                    "me.height -= 25;" + 
                "}else{" + 
                    "me.dockedItems=" + dockedItems + ";" + 
                "}";
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

                "if(me.type == 'add'){" + 
                    "me.isAdd();" + 
                "}else if(me.type == 'edit'){" + 
                    "me.isEdit(me.dataId);" + 
                "}else if(me.type == 'show'){" + 
                    "me.isShow(me.dataId);" + 
                    "var buts = me.getComponent('dockedItems-buttons');" + 
                    "if(buts){" +
                        "buts.hide();" +
                    "}" + 
                "}" + 
                strtemp + 
            "}"+   
        "});";
       
        return appClass;
    },
    /**
     * 打开某一树应用效果窗口
     * @private
     * @param {} title
     * @param {} ClassCode
     */
    openAppShowWinStr:function(){
        var com="";
        com=com+
        "function(title,classCode,com,textCom){" +
        "var me = this;"+
        "var panel = eval(classCode);"+
        "var maxHeight = document.body.clientHeight*0.98;"+
        "var maxWidth = document.body.clientWidth*0.98;"+
        "var appList = Ext.create(panel,{"+
            "maxWidth:maxWidth,"+
            "maxHeight:maxHeight,"+
            "autoScroll:true,"+
            "model:true,"+//模态
            "floating:true,"+//漂浮
            "closable:true,"+//有关闭按钮
            "draggable:true,"+//可移动:
            "Id:'',"+//(外部打开时传入选中的树节点Id)
            //"LevelNum:'',"+//(外部打开时传入选中的树节点)
            //"TreeCatalog:'',"+//(外部打开时传入选中的树节点)
            "selectId:textCom.getValue()"+//默认选中节点ID(外部调用时传入)
        "}).show();"+

         "appList.on({"+
         //树的确定事件
            "okClick:function(){"+
                "var records=appList.getValue();"+
                "if(records.length == 0){"+
                    "Ext.Msg.alert('提示','请选择一个应用！');"+
                "}else if(records.length == 1){"+
                    "me.setWinformInfo(records[0],com);"+
                    //"appList.close();"+//关闭应用列表窗口
                "}"+
            "},"+
            //树的双击事件
            "itemdblclick:function(view,record,item,index,e,eOpts){"+
                "me.setWinformInfo(record,com);"+
                "appList.close();"+//关闭应用列表窗口(存在bug)
            "}"+
        "});"+
        
        "}";
        return com;
    }, 
    /**
     * 根据ID获取一条应用信息
     * @private
     * @param {} id
     * @param {} callback
     */
    getInfoByIdFromServerStr:function(id,callback){
        var com="";
        com=com+
        "function(id,callback){" +
        "var me = this;" +
        "var myUrl = me.getAppInfoServerUrl+'?isPlanish=true&id='+id;" +
        "Ext.Ajax.defaultPostHeader = 'application/json';" +
        "Ext.Ajax.request({" +
           " async:false," +//非异步
            "url:myUrl," +
            "method:'GET'," +
            "timeout:2000," +
            "success:function(response,opts){" + 
                "var result = Ext.JSON.decode(response.responseText);" +
                "if(result.success){" +
                   " var appInfo = '';" +
                    "if(result.ResultDataValue && result.ResultDataValue != ''){" +
                    	"result.ResultDataValue = result.ResultDataValue.replace(/\\\\n/g,'\\\\\\\\u000a');" + 
                        "appInfo = Ext.JSON.decode(result.ResultDataValue);" +
                    "}" +
                    "if(Ext.typeOf(callback) == 'function'){" +
                       "if(appInfo == ''){" + 
                    		"Ext.Msg.alert('提示','没有获取到应用组件信息！');" + 
                    	"}else{" + 
                    		"callback(appInfo);" + 
                    	"}" + 
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
      /***
       * 创建文本框的相关的功能按钮的单击事件
       */
      createFunctionBtnClickStr:function(){
        var com="";
        com=
        "function(com,e,optes){" +
         "var me=this;"+
         
         "var textItemId=com.boundField;"+//功能按钮绑定的文本框的itemId
         "var textCom=me.getComponent(textItemId);"+//功能按钮绑定的文本框的itemId
         "var appComID=textCom.appComID;"+//取出保存在文本框的应用Id 
          //处理代码
         "if(appComID!=''&&appComID!=null&&appComID!=undefined){"+
            "var callback = function(appInfo){"+
                //中文名称
                "var title = textCom.getValue();"+//新增表单的元应用的应用名称;
                //类代码
                "var ClassCode = '';"+
                "if(appInfo && appInfo != ''){"+
                    "ClassCode = appInfo[me.classCode];"+
                "}"+
                
                "if(ClassCode && ClassCode != ''){"+
                    //打开应用效果窗口
                    "me.openAppShowWin(title,ClassCode,com,textCom);"+
               " }else{"+
                   " Ext.Msg.alert('提示','没有类代码！');"+
                "}"+
                
            "};"+
                "me.getInfoByIdFormServer(appComID,callback);"+
             "}else{"+
                "Ext.Msg.alert('提示','功能按钮没有绑定应用！');"+
            "}"+
          "}";
           return com;
      }, 
    /***
     * 功能按钮选择后给相关的文本框赋值
     * @return {}
     */
    setWinformInfoStr:function(){
        var com="";
            com=
            "function(record,com){" +
                "var me = this;"+
                //com为绑定的文本框的功能按钮,record为当前选择中的树的节点
                "var itemId=com.boundField;"+
		        "var value=record.get('Id');"+
		        "var text=record.get('text');" +
                "var winformtext=me.getComponent(itemId);"+
                "winformtext.treeNodeID=record.get('Id');"+
		        "if(winformtext.xtype=='combobox'){"+
		            "var arrTemp=[[value,text]];"+
		            "winformtext.store=new Ext.data.SimpleStore({"+
		                "fields:['value','text'],"+
		                "data:arrTemp ,"+
		                "autoLoad:true"+
		            "});"+
		           "winformtext.setValue(value);"+
                   "var tempArr=itemId.split('_');"+
		            "var tempItemId='';"+
		            "for(var i=0;i<tempArr.length-1;i++){"+
		                "if(i<tempArr.length-1){"+
		                    "tempItemId=tempItemId+tempArr[i]+'_';"+
		                "}"+
		            "}"+
                    //对DataTimeStamp设置值加个开关
                    "if(me.setdataTimeStampValue&&me.setdataTimeStampValue==true){"+
			            "var dataTimeStampValue=''+record.get('DataTimeStamp');"+
			            "tempItemId=tempItemId+'DataTimeStamp';"+
			            "var dataTimeStampCom=me.getComponent(tempItemId);"+
	                    "if(dataTimeStampCom){"+
			                  "dataTimeStampCom.setValue(dataTimeStampValue);"+
	                    "}"+
                    "}"+
                   
		        "}else{"+
		            "winformtext.treeNodeID=value;"+
		            "winformtext.setValue(text); "+ 
		       " }"+
            "}"; 
        return com;    
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
	        //添加对表单的拼音头事件的处理
	        "me.isLoadingComplete=false;" + 
        
        	"if(me.type=='edit'){" + 
        		"me.setTitle(me.defaultTitle+'-修改');" +  
        	"}else if(me.type=='show'){" + 
        		"me.setTitle(me.defaultTitle+'-查看');" +  
        	"}else{" + 
        		"me.setTitle(me.defaultTitle);" + 
        	"}" + 
            "Ext.Ajax.request({" + 
                "async:false," + 
                "url:getRootPath()+'/" + url + "&id='+(id?id:-1)," + 
                "method:'GET'," + 
                "timeout:5000," + 
                "success:function(response,opts){" + 
                    "var result=Ext.JSON.decode(response.responseText);" + 
                    "if(result.success){" + 
                        "if(result.ResultDataValue&&result.ResultDataValue!=''){" + 
                        	"result.ResultDataValue =result.ResultDataValue.replace(/[\\\\r\\\\n]+/g,'<br/>');" + 
                            "if(me.type == 'add'){me.type='edit';}" + 
                            "var values=Ext.JSON.decode(result.ResultDataValue);" +	
                            "values=changeObj(values);" + 
                            "me.getForm().setValues(values);" + 
                            "me.isLoadingComplete=true;" + 
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
            "var bo = me.fireEvent('beforeSave');" + 
            "if(!bo) return;" + 
            "if (!me.getForm().isValid()) return;" +
            "if(me.type == 'show'){" + 
                "Ext.Msg.alert('提示','查看页面不能提交！');" + 
                "return;" + 
            "}" + 
            "var url = '';" +  
            "if(me.type == 'add'){" + 
                "url = me.addDataServerUrl;" + 
            "}else if(me.type == 'edit'){" + 
                "url = me.editDataServerUrl;" + 
            "}" + 
            //"me.fireEvent('saveClick');" + //移动事件到这里
            "if(url == '') return;" + 
            "var values = me.getForm().getValues();" + 
            "for(var i in values){" + 
		    	"if(!values[i] || values[i] == ''){" + 
		    		"delete values[i];" + 
		    	"}" + 
		    "}" + 
            "var maxLength = 0;" + //最大的层数
            "for(var i in values){" + //表单的所有组件和结果值
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
                    "if(Ext.typeOf(eval(ob))==='undefined'){" + //对象不存在
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
//                    "if(value==''){" +
//                        "value =null;" +
//                    "}" +
                    "var tempArr=j.split('_');" +
                    "var tempStr=tempArr[tempArr.length-1];" +
                     //去掉
//                    "if(tempStr=='Id'){" +
//                        "value=parseInt(value);" +
//                    "}" +
                    
                    //日期时间值转换成后台需要的格式"\/Date(1359779125000)\/"
                    //出生日期,数据加入时间,数据更新时间
                    "if(tempStr=='Birthday'||tempStr=='DataAddTime'||tempStr=='DataUpdateTime'||tempStr=='UpdateTime'){" +
                        "value=convertJSONDateToJSDateObject(value);"+
                    "}" +
                    
	    			"addObj(j,i,value);" + //键、层、值
	    		"}" + 
	    	"}" + 
	    	
	    	"if(obj.Id == ''||obj.Id ==null){" + 
	    		"obj.Id=-1;" + 
	    	"}" + 
	    	
	    	//删除掉不需要的元素
	    	"var isEmptyObject=function(obj){for(var name in obj){return false;}return true;};" + 
	    	"var deleteNodeArr=[];" + 
	    	"for(var i in obj){" + 
	    		"if(isEmptyObject(obj[i])){deleteNodeArr.push(i);}" + 
	    	"}" + 
	    	"for(var i in deleteNodeArr){" + 
	    		"delete obj[deleteNodeArr[i]];" + 
	    	"}" + 
            
            //时间戳字符串转化为数组对象
            "var changeDataTimeStamp=function(obj){" + 
                "for(var i in obj){" + 
                    "if(Ext.typeOf(obj[i]) == 'object'){" + 
                        "changeDataTimeStamp(obj[i]);" + 
                    "}else{" + 
                        "if(i == 'DataTimeStamp' && obj[i] && obj[i] != ''){" + 
                            "obj[i]=obj[i].split(',');" + 
                        "}" + 
                        "if(i == 'DataTimeStamp' && me.type == 'edit'){" + 
                            "delete obj[i];" + 
                        "}" + 
                    "}" + 
                "}" + 
            "};" + 
            "changeDataTimeStamp(obj);" + 
            "var params={entity:obj};" + 
            "if(me.type=='edit'){" + 
                "var field = '';" + 
                "for(var i in values){" + 
                    "var keyArr = i.split('_');" + 
                    "if(keyArr.slice(-1) != 'DataTimeStamp'){" + //过滤掉时间戳
                        "field = field + keyArr.slice(1).join('_') + ',';" + 
                    "}" + 
                "}" + 
                "if(field != ''){" + 
                    "field = field.slice(0,-1);" + 
                "}" + 
                "params.fields=field;" + 
            "}" + 
            
            "Ext.Ajax.defaultPostHeader = 'application/json';" + 
            "Ext.Ajax.request({" + 
                "async:false," + 
                "url:getRootPath()+'/'+url," + 
                "params:Ext.JSON.encode(params)," + 
                "method:'POST'," + 
                "timeout:5000," + 
                "success:function(response,opts){" + 
                    "var result = Ext.JSON.decode(response.responseText);" + 
                    "if(result.success){" + 
                    	"if(result.ResultDataValue&&result.ResultDataValue!=''){" + 
                    		"var key=me.objectName+'_Id';" + 
                    		"var data=Ext.JSON.decode(result.ResultDataValue);" + 
							"var id=data.id;" + 
                    		"me.getForm().setValues([{id:key,value:id}]);" + 
                    	"}" + 
                    	"me.fireEvent('saveClick');" + 
                        "if(me.isSuccessMsg==true){" + 
                            "alertInfo('保存成功!');" + 
                        "}" + 
                    "}else{" + 
                        "alertError(action.result.ErrorInfo);" +  
                    "}" + 
                "}," + 
                "failure:function(response,options){" + 
                    "alertError('保存请求失败!');" + 
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
        
        var buttons = [];
        if(params.hasDefault1Button){//自定义一
        	var but = "{xtype:'button',text:'" + params.Default1ButtonText + "',handler:function(but){me.fireEvent('default1buttonClick',but);}}";
        	buttons.push(but);
        }
        if(params.hasDefault2Button){//自定义一
        	var but = "{xtype:'button',text:'" + params.Default2ButtonText + "',handler:function(but){me.fireEvent('default2buttonClick',but);}}";
        	buttons.push(but);
        }
        if(params.hasDefault3Button){//自定义一
        	var but = "{xtype:'button',text:'" + params.Default3ButtonText + "',handler:function(but){me.fireEvent('default3buttonClick',but);}}";
        	buttons.push(but);
        }
  		buttons.push("'->'");
        if(params.hasSaveButton){//保存按钮
        	var but = "{xtype:'button',text:'" + params.SaveButtonText + "',iconCls:'build-button-save',handler:function(but){me.submit();}}";
        	buttons.push(but);
        }
        if(params.hasResetButton){//重置按钮
        	var but = "{xtype:'button',text:'" + params.ResetButtonText + "',iconCls:'build-button-refresh',handler:function(but){me.getForm().reset();}}";
        	buttons.push(but);
        }
        
        var dockedItems = "";
        if(buttons.length > 1){
        	dockedItems = 
            "[{" + 
                "xtype:'toolbar'," + 
                "dock:'bottom'," + 
                "itemId:'dockedItems-buttons'," + 
                "items:[" + buttons.join(",") + "]" + 
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
        
        //按光标顺序排序
        for(var i=1;i<records.length;i++){
            for(var j=0;j<records.length-i;j++){ 
                if(records[j].get('sortNum')>records[j+1].get('sortNum')){//比较交换相邻元素
                    var temp=records[j];
                    records[j]=records[j+1];
                    records[j+1]=temp; 
                } 
            } 
        }
        var num = 0;
        for(var i=0;i<records.length;i++){
            var sortNum = records[i].get('sortNum');
            sortNum = sortNum > 0 ? ++num : 0;
            records[i].set('sortNum',sortNum);
        }
        
        for(var i in records){
            var record = records[i];
            var comStr = me.createComStrByType(record);
            var type=record.get('Type');
            var hasReadOnly ='';
            var isFunctionBtn=record.get('isFunctionBtn');
            
            var AlignType = record.get('AlignType');
            var labelAlign = AlignType || "left";
            labelAlign = ",labelAlign:'" + labelAlign + "'";
            
            //处理绑定带有的功能选择树节点按钮(文本框或者定制下拉框)
            if((type=='textfield'||type=='datacombobox')&&isFunctionBtn==true){
                var sortNum = ",sortNum:" + record.get('sortNum');
                items = items + "{" + comStr + sortNum;
                hasReadOnly = ",hasReadOnly:" + records[i].get('IsReadOnly');
                items = items + hasReadOnly + labelAlign + "},";
                
                //添加文本框或者定制下拉框的功能选择树节点按钮
                var comBtnStr = me.createButtonStr(record);
                items = items + "{" + comBtnStr + sortNum;
                
                hasReadOnly = ",hasReadOnly:false";
                items = items + hasReadOnly + "},";
            }
            else{//原来的
                var listeners = me.createListenersStr(record,records);
                var sortNum = ",sortNum:" + records[i].get('sortNum');
                items = items + "{" + comStr + sortNum + listeners;
                hasReadOnly = ",hasReadOnly:" + records[i].get('IsReadOnly');
                items = items + hasReadOnly + labelAlign + "},";
            }
            
            
        }
        
        if(items.length > 1){
            items = items.slice(0,-1);
        }
        
        items += "]";
        return items;
    },
    /**
     * 创建监听代码
     * @private
     * @param {} record
     * @param {} records
     * @return {}
     */
    createListenersStr:function(record,records){
        var com = ",listeners:{";
        
        var type = record.get('Type');
        if(type == 'combobox'){//下拉框
            var InteractionField = record.get('InteractionField');
            var InteractionFieldArr = InteractionField.split('_');
            InteractionFieldArr[InteractionFieldArr.length-1] = "DataTimeStamp";
            var DataTimeStampCom = InteractionFieldArr.join("_");
            var DataTimeStampField = InteractionFieldArr.slice(-2).join("_");
            com += 
            "select:function(combo,records){" + 
                "var com=combo.ownerCt.getComponent('" + DataTimeStampCom + "');" + 
                "if(com){" + 
                    //时间戳匹配处理
                    "for(var key in records[0].data){" + 
                        "var arr=key.split('_');" + 
                        "if(arr[arr.length-1]==='DataTimeStamp'){" + 
		                    "var value=records[0].get(key);" + 
		                    "com.setValue(value);" + 
                         "}" + 
                    " }" + 
                "}" + 
            "},";
        } 
        //选择项改变后表单里的相关时间戳赋值处理
        else if(type == 'radiogroup'||type == 'radiogroup'){//单选组,复选组
            var InteractionField = record.get('InteractionField');
            var InteractionFieldArr = InteractionField.split('_');
            InteractionFieldArr[InteractionFieldArr.length-1] = "DataTimeStamp";
            var DataTimeStampCom = InteractionFieldArr.join("_");
            var DataTimeStampField = InteractionFieldArr.slice(-2).join("_");
            
            com += 
            "change:function(combo,newValue,oldValue,eOpts){" + 
	            "if(combo.xtype=='radiogroup'){" + //单选组的表单里相关时间戳赋值处理
	                "var com=combo.ownerCt.getComponent('" + DataTimeStampCom + "');" + 
	                "if(com){" + 
	                    "var valueArr = combo.getChecked( );" +
                        "if(valueArr.length>0){" +
                            "var value=''+valueArr[0].DataTimeStamp;" +
                            "com.setValue(value);" +
                        "}" +
	                "}" + 
	              "}" +   
            "},";
        }
        
        var sortCount = 0;
        for(var i in records){
            if(records[i].get('sortNum') > 0){
                sortCount++;
            }
        }
        com = com + 
            "scope:this," + 
            "specialkey:function(field,e){" + 
                "var iNum = 1;" + 
                "var sNumField = 'sortNum';" + 
                "var form = field.ownerCt;" + 
                "var num = field[sNumField];" + 
                "var items = form.items.items;" + 
                "var max = " + sortCount + ";" + 
                "if(e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB){" + 
                    "e.preventDefault();" + 
                "}" + 
                "if(num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)){" + 
                    "if(!e.shiftKey){" + 
                        "num = (num+iNum > max) ? num+iNum-max : num+iNum;" + 
                    "}else{" + 
                        "num = (num-iNum < 1) ? num-iNum+max : num-iNum;" + 
                    "}" + 
                    "for(var i in items){" + 
                        "if(items[i].sortNum == num){" + 
                            "items[i].focus(false,100);break;" + 
                        "}" + 
                    "}" + 
                "}" + 
            "}" + 
        "}";
        return com;
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
            com = me.createTextfieldStr(record);
        }else if(type == 'textareafield'){//文本域
            com = me.createTextareaStr(record);
        }else if(type == 'numberfield'){//数字框
            com = me.createNumberStr(record);
        }else if(type == 'datefield'){//日期框
            com = me.createDateStr(record);
        }else if(type == 'timefield'){//时间框
            com = me.createTimeStr(record);
        }else if(type == 'datetimenew'){//日期时间框
            com = me.createDateTimeStr(record);
        }else if(type == 'checkboxgroup'){//复选组
            com = me.createCheckboxStr(record);
        }else if(type == 'radiogroup'){//单选组
            com = me.createRadiogroupStr(record);
        }else if(type == 'label'){//纯文本
            com = me.createLabelStr(record);
        }else if(type == 'image'){//图片
            com = me.createImageStr(record);
        }else if(type == 'htmleditor'){//超文本
            com = me.createHtmleditorStr(record);
        }else if(type == 'filefield'){//文件
            com = me.createFileStr(record);
        }else if(type == 'button'){//按钮
           
        }else if(type == 'datacombobox'){//定值下拉框
            com = me.createDataComboxStr(record);
        }else if(type == 'dataradiogroup'){//定值单选组
            com = me.createDataRadioStr(record);
        }else if(type == 'datacheckboxgroup'){//定值复选组
            com = me.createDataCheckboxStr(record);
        }else if(type == 'gridcombobox'){//多列下拉框
            com = me.createGridcomboboxStr(record);
        }else if(type == 'displayfield'){//displayfield
            com = me.createDisplayfieldStr(record);
        }else if(type == 'colorscombobox'){//
            com = me.createColorscomboboxStr(record);
        }else if(type == 'radiofield'){//
            com = me.createSettingRadioStr(record);
        }else if(type == 'checkboxfield'){//
            com = me.createSettingCheckBoxStr(record);
        }
        return com;
    },
    /***
     * 单选框
     * @param {} record
     * @return {}
     */
    createSettingRadioStr:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
        } 
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com =
            "xtype: 'radiofield',"+
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," +
            "height:" + record.get('Height') + "," + 
            "boxLabel:''"+ "," + 
            "inputValue:'true'"+ "," + //提交值，默认为“on”
            "uncheckedValue:'false'";//设置当复选框未选中时向后台提交的值，默认为undefined
          return com; 
    },
    /***
     * 复选框
     * @param {} record
     * @return {}
     */
    createSettingCheckBoxStr:function(record){
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
        }
        var defaultValue=record.get('defaultValue');
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var checked2=false;
        if(defaultValue==true||defaultValue==false){
            checked2="'"+defaultValue+"'";
        }else{
            checked2="'"+false+"'";
        }
        var com =
            "xtype: 'checkboxfield',"+
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," +
            "height:" + record.get('Height') + "," + 
            "boxLabel:''"+ "," + 
            "inputValue:'true'"+ "," + //提交值，默认为“on”
            "uncheckedValue:'false'"+//设置当复选框未选中时向后台提交的值，默认为undefined
            ",checked:"+checked2 + "";

            
          return com; 
    },
    /***
     * 颜色下拉选择器
     * @param {} record
     * @return {}
     */
    createColorscomboboxStr:function(record){
        var me=this;
        var minWidth= record.get('minWidth');
        var maxHeight= record.get('maxHeight');
        if(minWidth==0){
            minWidth=140;
        }
        if(maxHeight==0){
            maxHeight=200;
        }
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com =
            "xtype: 'colorscombobox',"+
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" + record.get('Height') + "," + 
            "minWidth:"+minWidth+ "," + 
            "maxHeight:"+maxHeight+ "," + 
            "forceSelection:true";
          return com; 
    },
    /***
     * 多列下拉框
     * @param {} record
     * @return {}
     */
    createGridcomboboxStr:function(record){
        var me=this;
        var minWidth= record.get('minWidth');
        var maxHeight= record.get('maxHeight');
        if(minWidth==0){
            minWidth=300;
        }
        if(maxHeight==0){
            maxHeight=200;
        }
        //处理列字段信息
        var columnsStr='';
        var tempColumns=""+record.get('gridcomboboxColumns')+"";
        if(tempColumns&&tempColumns.length>0){
            var arrKeys=tempColumns.toString().split(',');
            var tempColumnsTexts=""+record.get('gridcomboboxColumnsCName');
            var arrTexts=tempColumnsTexts.split(',');
            var tempJson='';
            for(var i=0;i<arrKeys.length;i++){
                var tempDataTimeStamp=arrKeys[i].split('_');
                if(tempDataTimeStamp[tempDataTimeStamp.length-1]=='DataTimeStamp'){
                    tempJson="{text:'"+arrTexts[i]+"',dataIndex:'"+arrKeys[i]+"',width:120,hidden:true}"+',';
                }else{
                    tempJson="{text:'"+arrTexts[i]+"',dataIndex:'"+arrKeys[i]+"',width:120}"+',';
                }
                columnsStr=columnsStr+tempJson;
            }
            columnsStr=columnsStr.substring(0,columnsStr.length-1);
        }
        //处理匹配字段数组(queryFields)及查询传参的字段集fields
        var tempQueryFields=""+record.get('queryFields');
        var queryFields='';
        var fields='';
        if(tempQueryFields&&tempQueryFields.toString().length>0)
        {
            var arrKeys=tempQueryFields.split(',');
            for(var i=0;i<arrKeys.length;i++){
	            queryFields=queryFields+("'"+arrKeys[i]+"',");
	            fields=fields+(""+arrKeys[i]+",");
            }
            queryFields="["+queryFields.substring(0,queryFields.length-1)+"]";
            fields=fields.substring(0,fields.length-1);
        }
        var myUrl=record.get('ServerUrl');
        var myStroe=null;
            if(myUrl==''){
               Ext.Msg.alert('提示','错误信息【<b style="color:red">'+'请先配置下拉框的数据对象或数据服务'+"</b>】");
                return false;
            }
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var com =

            "xtype: 'gridcombobox',"+
            "store:new Ext.data.Store({"+
                    "fields:"+queryFields+","+
                    "proxy:{"+
                        "type:'ajax',"+
                        "url:getRootPath()+'/'+"+ "'"+myUrl+"?isPlanish=true&fields="+fields+ "'," + 
                        "reader:{type:'json',root:'ResultDataValue.list'},"+
                        "extractResponseData:function(response){"+
                            "var data = Ext.JSON.decode(response.responseText);"+
                             "var ResultDataValue = [];"+
                            "if(data.ResultDataValue && data.ResultDataValue != ''){"+
                                "ResultDataValue = Ext.JSON.decode(data.ResultDataValue);"+
                            "}"+
                        "data.ResultDataValue = ResultDataValue;"+
                        "response.responseText = Ext.JSON.encode(data);"+
                        "return response;"+
                    "}"+
                    "},autoLoad:true"+
                "})," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "columns:["+columnsStr+"]"+ "," + 
           
            "valueField:'"+record.get('valueField')+ "'," + 
            "displayField:'"+record.get('textField')+ "'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" + record.get('Height') + "," + 
            "minWidth:"+minWidth+ "," + 
            "maxHeight:"+maxHeight+ "," + 
            "queryFields:"+queryFields+ "," + 
            "forceSelection:true";
         
          return com;
    },
     /**
     * 定值复选组Str
     * @private
     * @param {} record
     * @return {}
     */
    createDataCheckboxStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var groupName=record.get('InteractionField');
        var defaultValue=[];
        defaultValue.push(record.get('defaultValue'));
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var columns= (record.get('Columns')!="")?(record.get('Columns')):('2');
        var columnWidth= (record.get('ColumnWidth').lenght>0)?(record.get('ColumnWidth')):('100');

        var combodata=record.get('combodata');
        if(combodata&&combodata.length>0){
           combodata=record.get('combodata').replace(/"/g,"'");
        }else{
            combodata=[];
        }
        var com =
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
            "isdataValue:true"+ "," + //是否是定值单/复选组,单/复选组特有自定义添加的属性
            "items:" +combodata+ "," +
            "tempGroupName:'" +groupName+ "'" ;
            //"tempDefaultValue:'" +defaultValue+ "'";
        return com;
    },
    /**
     * 创建定值单选框Str
     * @private
     * @param {} record
     * @return {}
     */
    createDataRadioStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var groupName=record.get('InteractionField');
        var defaultValue=[];
        defaultValue.push(record.get('defaultValue'));
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var columns= (record.get('Columns')!="")?(record.get('Columns')):('2');
        var columnWidth= (record.get('ColumnWidth').lenght>0)?(record.get('ColumnWidth')):('100');
        
        var combodata=record.get('combodata');
        if(combodata&&combodata.length>0){
            //combodata=Ext.encode(Ext.encode(record.get('combodata').replace(/"/g,"'")));
            combodata=record.get('combodata').replace(/"/g,"'");
        }else{
            combodata='[]';
        }
        var com =
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
            "isdataValue:true"+ "," + //是否是定值单/复选组,单/复选组特有自定义添加的属性
            "items:" +combodata+ "," +
            "tempGroupName:'" +groupName+ "'" ;
            //"tempDefaultValue:'" +defaultValue+ "'";
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
        var datas=record.get('combodata');
        if(datas==null||datas==''){
            datas='[]';
        }
        var defaultValue="'"+record.get('defaultValue')+"'";
        var isFunctionBtn=record.get('isFunctionBtn');
        var com = 
            "xtype:'combobox'" + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," +
            "isFunctionBtn:" + isFunctionBtn + ","+
            //当定值下拉框带有功能按钮时,appComID属性用以保存隐藏的应用id值
            "appComID:'" + record.get('appComID') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "value:" + defaultValue + "," +
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "mode:'local'," + 
            "editable:false," +  
            "displayField:'text'," + 
            "valueField:'value'," + 
            "store:new Ext.data.SimpleStore({" + 
                "fields:['value','text']," + 
                "autoLoad:true," +
                "data:" +datas + 
            "})";
        
        return com;
    },
    /**
     * 创建文本框Str
     * @private
     * @param {} record
     * @return {}
     */
    createTextfieldStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var isFunctionBtn=record.get('isFunctionBtn');//是否绑定功能按钮
        var com = 
            "xtype:'textfield'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            //文本框自定义属性,文本框绑定的功能按钮选择后的隐藏值/显示值
            "appComID:'" + record.get('appComID') + "'," + 
            "treeNodeID:''," + //选择中的树节点的id,提供返回给后台的值,需要重写getValue()
            "value:'" + record.get('appComCName') + "'," + 
            "isFunctionBtn:" + isFunctionBtn + "," ;
            //如果该文本框绑定了功能按钮,重写getValue().返回treeNodeID值
            if(isFunctionBtn==true){
                 com = com +
                 //改写这个用getFrom().getValues取值不生效
                 "getValue: function() {" + 
                    "var val = '';" + 
                    "val = this.treeNodeID;" +
                    "if(val==null||val==undefined){" +
                        "val ='';" +
                     "}" +
                    "return val;" + 
                "}," +
                //改写这个用getFrom().getValues取值生效
                "getRawValue: function() {" + 
                    "var val = '';" + 
                    "val = this.treeNodeID;" +
                    "if(val==null||val==undefined){" +
                        "val ='';" +
                     "}" +
                    "return val;" + 
                "}," ;
            }
                
            com = com +"hidden:" + record.get('IsHidden');
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
        var canEdit=record.get('CanEdit');//是否允许编辑
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
        }
        var rawOrCol=record.get('RawOrCol');
        if(rawOrCol==''){
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'RawOrCol','hbox');
            rawOrCol='hbox';
        }
        var com =
            "xtype:'dateintervals'," + 
            "name:'" +record.get('InteractionField')+"'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "editable:" + canEdit + "," + 
            "fieldLabel:'" +fieldLabel+"'," + 
            "fieldLabelTwo:'" +fieldLabelTwo+"'," + 
            "labelWidth:" +record.get('LabelWidth')+"," + 
            "width:" +record.get('Width')+"," + 
            "layoutType:'" +rawOrCol+"'," + 
            //"labelAlign:'" +record.get('AlignType')+"',"+
            "value:new Date()" + "," + 
            "valueTwo:new Date()" + "," + 
            "height:" +record.get('Height')+"," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "dateFormat:'" + showFomart + "'" 
        return com;
    },
    /**
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
        
        var DataTimeStampFieldArr = textField.split("_");
        DataTimeStampFieldArr[DataTimeStampFieldArr.length-1] = "DataTimeStamp";
        var DataTimeStamp = DataTimeStampFieldArr.join("_");
        
        if((valueField==null||valueField=="")||(textField==null||textField=="")||(url==null||url=="")){
            Ext.Msg.alert('提示','错误信息【<b style="color:red">'+'请先配置下拉框的数据对象或数据服务'+"</b>】");
            return false;
        }
        //下拉框初始值没有时间戳修正
        var InteractionField = record.get('InteractionField');
        var InteractionFieldArr = InteractionField.split('_');
        InteractionFieldArr[InteractionFieldArr.length-1] = "DataTimeStamp";
        var DataTimeStampCom = InteractionFieldArr.join("_");
        var DataTimeStampField = InteractionFieldArr.slice(-2).join("_");
        var com = 
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
            "height:" +record.get('Height')+"," + 
            "value:'" + record.get('defaultValue') + "'," + 
            "mode:'local'," + 
            "editable:false," +  
            "displayField:'" + textField+ "'," + 
            "valueField:'" + valueField+ "'," + 
            "store:new Ext.data.Store({" + 
                "fields:['" + textField + "','" + valueField+ "','" + DataTimeStamp + "']" + "," + 
                "proxy:{" + 
                    "type:'ajax'," + 
                    "async:false,"+
                    //"url:'" +getRootPath()+"/"+ url + "'"  + "," + 
                    "url:getRootPath()+"+"'/'+"+"'"+url+"',"+//修改getRootPath()的路径问题
                    "reader:{type:'json',root:'" + me.objectRootTwo + "'}" + "," + 
                    "extractResponseData:me.changeStoreData" + 
                "},autoLoad:true," + 
                "listeners:{" +
                	"load:function(s,records,successful){" +
                		"var combo=me.getComponent('" + record.get('InteractionField') + "');" + 
                		"var com=me.getComponent('" + DataTimeStampCom + "');" + 
		                "if(com){" + 
		                	"var record=s.findRecord('" + valueField + "',combo.getValue());" + 
                            "if(record!=null&&record!=''){" + 
			                    "var value=record.get('" + DataTimeStampField + "');" + 
			                    "com.setValue(value);" + 
                            "}" + 
		                "}" + 
                	"}" + 
                "}" + 
            "})";
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
            "xtype:'textareafield'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden');
        return com;
    },  
    /**
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
            "xtype:'numberfield'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," +  
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            
            "minValue:" + record.get('NumberMin') + "," + 
            "maxValue:" + record.get('NumberMax') + "," + 
            "step:" + record.get('NumberIncremental');
        return com;
    },
   /**
     * 创建日期框Str
     * @private
     * @param {} record
     * @return {}
     */
    createDateStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = 
            "xtype:'zhifangux_datefield'"  + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," +  
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "editable:" + canEdit + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') ;
            if(showFomart==''||showFomart==""||showFomart==null){
                
            }else{
                com=com+",format:'" + showFomart + "'" ; 
            }
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
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='H:i';
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com =
            "xtype:'zhifangux_timefield'" + "," +  
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "increment:" + 1+ "," + //增量
            "editable:" + canEdit + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "selectOnFocus:true";
            if(showFomart==''||showFomart==""||showFomart==null){
            //showFomart='Y-m-d';
            }else{
                com=com+",format:'" + showFomart + "'" ; 
            } 
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
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d H:i:s';
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com =
            "xtype:'datetimenew'" + "," +  
            "editable:" + canEdit + "," + 
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
            //"labelAlign:'left'"+ ","+
            "selectOnFocus:true";
            if(showFomart==''||showFomart==""||showFomart==null){
                
            }else{
                com=com+",format:'" + showFomart + "'" ; 
            }
        return com;
    },
    /**
     * 创建复选组Str
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
        //时间戳
        var InteractionField = record.get('InteractionField');
        var InteractionFieldArr = InteractionField.split('_');
        InteractionFieldArr[InteractionFieldArr.length-1] = "DataTimeStamp";
        var DataTimeStampCom = InteractionFieldArr.join("_");//表单里的相关组件的时间戳itemId
        var DataTimeStampField = InteractionFieldArr.slice(-2).join("_");//查询后台数据时取值用
        
        var com =
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
            "isdataValue:false"+ "," + //是否是定值单/复选组,单/复选组特有自定义添加的属性
            "tempGroupName:'" +groupName+ "'"+ "," +
            "DataTimeStampCom:'" +DataTimeStampCom+ "'"+ "," +
            "DataTimeStampField:'" +DataTimeStampField+ "'"+ "," + 
            "tempDefaultValue:'" +defaultValue+ "'";
        return com;
    },
    /**
     * 创建单选组Str
     * @private
     * @param {} record
     * @return {}
     */
    createRadiogroupStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
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

        //时间戳
        var InteractionField = record.get('InteractionField');
        var InteractionFieldArr = InteractionField.split('_');
        InteractionFieldArr[InteractionFieldArr.length-1] = "DataTimeStamp";
        var DataTimeStampCom = InteractionFieldArr.join("_");//表单里的相关组件的时间戳itemId
        var DataTimeStampField = InteractionFieldArr.slice(-2).join("_");//查询后台数据时取值用
        
        //tempUrl,tempValue,tempGroupName,tempDefaultValue作类代码处理加载数据用
        var com =
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
            "isdataValue:false"+ "," + //是否是定值单/复选组,单/复选组特有自定义添加的属性
            "tempGroupName:'" +groupName+ "'"+ "," +
            "tempDefaultValue:'" +defaultValue+ "',"+
            "DataTimeStampCom:'" +DataTimeStampCom+ "'"+ "," +//表单里单/复选组的时间戳itemId
            "DataTimeStampField:'" +DataTimeStampField+ "'";//查询后台数据时取值用
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
            "xtype:'label'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "width:" + record.get('Width') + "," + 
            "text:'" + record.get('DisplayName') + "'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "height:" +record.get('Height')+"," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "hidden:" + record.get('IsHidden');
        return com;
    },
    /***
     * 创建DisplayfieldStr
     * @param {} record
     * @return {}
     */
    createDisplayfieldStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont').lenght>0)?(record.get('LabFont')):('font-style:normal;');
        var com = 
            "xtype:'displayfield'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "width:" + record.get('Width') + "," + 
            "text:'" + record.get('DisplayName') + "'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "height:" +record.get('Height')+"," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            //是不是需要换成双引号+'\\"'+
            "value:'"+record.get('defaultValue')+"'," + 
            "hidden:" + record.get('IsHidden');
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
            "xtype:'image'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" + record.get('Height') + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "src:'" + src + "'";
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
            "xtype:'htmleditor'," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden');
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
            "buttonText:'" + buttonText + "'";
        return com;
    },
    /**
     * 创建(功能)按钮Str
     * @private
     * @param {} record
     * @return {}
     */
    createButtonStr:function(record){
        //是否是功能按钮
        var IsReadOnly=record.get('IsReadOnly');
        var isFunctionBtn=record.get('isFunctionBtn');
        var com =
            "xtype:'button'," + 
            "name:'" + record.get('functionBtnId') + "'," + 
            "disabled :" +IsReadOnly+ ","+
            "width:22," + 
            "height:22," + 
            "itemId:'" + record.get('functionBtnId') + "'," + 
            "x:" + record.get('btnX') + "," + 
            "y:" + record.get('btnY') + "," ; 
            
            //功能按钮自定义属性,按钮绑定的
            com=com+"boundField:'" + record.get('boundField') + "'," + 
            "isFunctionBtn:" + isFunctionBtn + "," + 
            "tooltip:'选择树节点'," + 
            "iconCls:'build-button-configuration-blue'," + 
            "margin:'0 0 0 2'," + 
            "listeners :{" + 
                "click:function(com,e,op){" + 
                    "var isFunctionBtn=com.isFunctionBtn;" + 
                    "var textItemId=com.boundField;" + 
                    "if(isFunctionBtn==true){" + 
                            //功能按钮的事件
                           "me.functionBtnClick(com);" + 
                           "me.fireEvent('functionBtnClick');" + 
                           
                        "}" + 
                "}" + 
            "}," ;
            
            com=com+"hidden:" + record.get('IsHidden');
        return com;
        
    },
    
    //=====================后台获取&存储=======================
    /**
     * 从后台获取应用信息
     * @private
     * @param {} callback
     */
    getAppInfoFromServer:function(id,callback){
        var me = this;
        
        if(id != -1){
            var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
            //回调函数
            var c = function(text){
	        	var result = Ext.JSON.decode(text);
	        	if(result.success){
                    var appInfo = "";
                    if(result.ResultDataValue && result.ResultDataValue != ""){
                    	result.ResultDataValue =result.ResultDataValue.replace(/\n/g,"\\u000a");
                    	appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if(Ext.typeOf(callback) == "function"){
                    	if(appInfo == ""){
                    		me.disablePanel(false);//启用保存按钮
                    		Ext.Msg.alert('提示','没有获取到应用组件信息！');
                    	}else{
                    		callback(appInfo);//回调函数
                    	}
                    }
                }else{
                	me.disablePanel(false);//启用保存按钮
                    Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ result.errorInfo +"</b>】");
                }
            };
           	//util-POST方式交互
        	getToServer(url,c);
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
        
        //回调函数
        var c = function(text){
	        var result = Ext.JSON.decode(text);
	        if(result.success){
                if(Ext.typeOf(callback) == "function"){
                	var id = null;
                	if(result.ResultDataValue && result.ResultDataValue != ''){
                		var data = Ext.JSON.decode(result.ResultDataValue);
						id = data.id + "";
                	}
					callback(id);//回调函数
                }
            }else{
            	me.disablePanel(false);//启用保存按钮
                Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ result.ErrorInfo +'</b>】');
            }
        };
        //请求头信息
        var defaultPostHeader = 'application/x-www-form-urlencoded';
        //util-POST方式交互
        postToServer(url,obj,c,defaultPostHeader);
    },
    //=====================公共方法代码=======================
    getAppParamsStr:function(){
		var me = this;
		var appParams = me.getAppParams();
		//获取对象字符串
		var getObjStr = function(key,value){
			var bo = (Ext.typeOf(value) === "string");
			var result = "";
			if(bo){
				result = key + ":'" + value.replace(/'/g,"\\\\'") + "',";
			}else{
				result = key + ":" + value + ",";
			}
			return result;
		};
		//面板属性
		var panelParams = appParams.panelParams;
		var panelParamsStr = "{";
		for(var i in panelParams){
			if(Ext.typeOf(panelParams[i])){
				panelParamsStr += getObjStr(i,panelParams[i]);
			}
		}
		if(panelParamsStr.length > 1){panelParamsStr = panelParamsStr.slice(0,-1);}
		panelParamsStr += "}";
		
		//数据项属性
		var southParams = appParams.southParams;
		var southParamsStr = "[";
		for(var i in southParams){
			var objStr = "{";
			for(var j in southParams[i]){
				if(Ext.typeOf(southParams[i][j])){
					objStr += getObjStr(j,southParams[i][j]);
				}
			}
			if(objStr.length > 1){objStr = objStr.slice(0,-1);}
			objStr += "}";
			southParamsStr += objStr + ",";
		}
		if(southParamsStr.length > 1){southParamsStr = southParamsStr.slice(0,-1);}
		southParamsStr += "]";
		
		//自定义按钮属性
		var south2Params = appParams.south2Params;
		var south2ParamsStr = "[";
		for(var i in south2Params){
			var objStr = "{";
			for(var j in south2Params[i]){
				if(Ext.typeOf(south2Params[i][j])){
					objStr += getObjStr(j,south2Params[i][j]);
				}
			}
			if(objStr.length > 1){objStr = objStr.slice(0,-1);}
			objStr += "}";
			south2ParamsStr += objStr + ",";
		}
		if(south2ParamsStr.length > 1){south2ParamsStr = south2ParamsStr.slice(0,-1);}
		south2ParamsStr += "]";
		
		//总体属性
		var appParamsStr = "{panelParams:" + panelParamsStr + ",southParams:" + southParamsStr + ",south2Params:" + south2ParamsStr + "}";
		
		appParamsStr = appParamsStr.replace(/\"/g,"\\\"");
		
		return appParamsStr;
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
    },
    //=====================新增按钮方法代码=======================
    /**
     * 按钮属性列表
     * @private
     * @return {}
     */
    createSouth2:function(){
    	var me = this;
    	var com = {
    		xtype:'grid',
    		title:'按钮属性列表',
    		columns:[
    			{xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
    			{text:'按钮名称',dataIndex:'buttonName',editor:{allowBlank:false}},
    			{text:'内部编号',dataIndex:'buttonItemId',editor:{allowBlank:false}},
    			{text:'按钮宽度',dataIndex:'buttonWidth',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:true,emptyText:'默认'}
                },
    			{text:'按钮高度',dataIndex:'buttonHeight',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:true,emptyText:'默认'}
                },
    			{text:'显示X轴',dataIndex:'buttonX',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:false}
                },
    			{text:'显示Y轴',dataIndex:'buttonY',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:false}
                },
    			{text:'弹出窗口',dataIndex:'openWin',xtype:'checkcolumn',align:'center',width:60,
                    editor:{xtype:'checkbox',cls:'x-grid-checkheader-editor'}
                },
    			{text:'窗口类型',dataIndex:'openWinType',width:60,
    				renderer:function(value, p, record){
                        if(value == 'html'){
                            return Ext.String.format('URL');
                        }else{
                        	return Ext.String.format('应用');
                        }
                    },
                    editor:new Ext.form.field.ComboBox({
                        mode:'local',editable:false, 
                        displayField:'text',valueField:'value',
                        store:new Ext.data.SimpleStore({ 
                            fields:['value','text'], 
                            data:[['html','URL'],['app','应用']]
                        }),
                        listClass: 'x-combo-list-small'
                    })
    			},
    			{text:'窗口宽度',dataIndex:'openWinWidth',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:true,emptyText:'默认'}
                },
    			{text:'窗口高度',dataIndex:'openWinHeight',xtype:'numbercolumn',format:'0',align:'center',width:60,
                    editor:{xtype:'numberfield',allowBlank:true,emptyText:'默认'}
                },
    			{text:'链接路径',dataIndex:'openWinURL'},
    			{text:'应用名称',dataIndex:'openWinAppName'},
    			{text:'应用ID',dataIndex:'openWinAppId'},
                {text:'内嵌代码',dataIndex:'linkConfig'}
    		],
    		store:Ext.create('Ext.data.Store',{
                fields:me.getSouth2StoreFields(),
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
     * 弹出新增按钮窗口
     * @private
     */
    openAddButtonWin:function(){
    	var me = this;
    	var maxHeight = document.body.clientHeight*0.98;
		var maxWidth = document.body.clientWidth*0.98;
		
		var fieldset = Ext.create('Ext.build.CustomButtonFieldSet',{
			autoScroll:true,
			type:'win',
			width:485,
			height:350
		});
		
		var win = Ext.create('Ext.form.Panel',{
			maxWidth:maxWidth,
			autoScroll:true,
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:495,
			height:350,
			title:'按钮设置',
			bodyPadding:'0 5 0 5',
			layout:'fit',
			items:fieldset
    	});
    	
		if(win.height > maxHeight){
			win.height = maxHeight;
		}
		
		win.show();
		
		fieldset.on({
			okClick:function(obj){
				var buttonItemId = obj.buttonItemId;
				if(buttonItemId && buttonItemId != ""){
					var callback = function(bo){
						if(bo){
							win.close();
						}else{
							Ext.Msg.alert('提示','内部编号已存在！请换一个内部编号');
						}
					}
					me.insertButtonInfo(obj,callback);
				}else{
					Ext.Msg.alert('提示','按钮编号不能为空！');
				}
			}
		});
    },
    /**
     * 添加按钮记录
     * @private
     * @param {} obj
     */
    insertButtonInfo:function(obj,callback){
    	var me = this;
    	var record1 = me.getSouthRecordByKeyValue('InteractionField',obj.buttonItemId);
    	var record2 = me.getSouth2RecordByKeyValue('buttonItemId',obj.buttonItemId);
    	if(record1 || record2){
    		callback(false);
    	}else{
    		callback(true);
    		//给按钮记录列表赋值
    		var arr = [obj];
    		me.setSouth2RecordByArray(arr);
    		//生成按钮
    		me.insertButtonComToCenter(obj);
    		//生成面板
    		me.insertButtonParamsPanel(obj);
    	}
    },
    
    /**
     * 弹出新增Display字段窗口
     * @private
     */
    openAddDisplayWin:function(){
        var me = this;
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
        
        var fieldset = Ext.create('Ext.build.CustomDisplayFieldSet',{
            autoScroll:true,
            type:'win',
            width:485,
            height:350
        });
        
        var win = Ext.create('Ext.form.Panel',{
            maxWidth:maxWidth,
            autoScroll:true,
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true,//可移动
            width:495,
            height:350,
            title:'Display字段设置',
            bodyPadding:'0 5 0 5',
            layout:'fit',
            items:fieldset
        });
        
        if(win.height > maxHeight){
            win.height = maxHeight;
        }
        
        win.show();
        
        fieldset.on({
            okClick:function(obj){
                var InteractionField = obj.InteractionField;
                if(InteractionField && InteractionField != ""){
                    var callback = function(bo){
                        if(bo){
                            win.close();
                        }else{
                            Ext.Msg.alert('提示','内部编号已存在！请换一个内部编号');
                        }
                    }
                    me.insertDisplayInfo(obj,callback);
                }else{
                    Ext.Msg.alert('提示','Display字段编号不能为空！');
                }
            }
        });
    },
    insertDisplayInfo:function(obj,callback){
        var me = this;
        var record1 = me.getSouthRecordByKeyValue('InteractionField',obj.InteractionField);
        var record2 = me.getSouth2RecordByKeyValue('buttonItemId',obj.InteractionField);
        if(record1 || record2){
            callback(false);
        }else{
            callback(true);
            //给Display字段记录列表赋值
            var arr = [obj];
            me.addDisplayFieldToSouth1(arr);
            //生成Display字段
            me.insertDisplayComToCenter(obj);
            //生成Display字段面板
            me.insertDisplayParamsPanel(obj);
        }
    },
    addDisplayFieldToSouth1:function(arr){
	    var me=this;
	    Ext.Array.each(arr,function(obj){
	            var rec = ('Ext.data.Model',obj);
	            me.addSouth1ValueByRecord(rec);//添加组件记录
	        });
    
    },
    /**
     * 获取按钮属性列表组件
     * @private
     * @return {}
     */
    getSouth2Com:function(){
    	var me = this;
    	var south = me.getComponent('south').getComponent('south2');
    	return south;
    },
    /**
     * 获取按钮属性列表组件
     * @private
     * @return {}
     */
    getSouth1Com:function(){
        var me = this;
        var south = me.getComponent('south').getComponent('south1');
        return south;
    },
    /**
     * 获取按钮属性列表Fields
     * @private
     * @return {}
     */
    getSouth2StoreFields:function(){
    	var me = this;
    	var fields = [
    		{name:'buttonName',type:'string'},
    		{name:'buttonItemId',type:'string'},
    		{name:'buttonWidth',type:'int'},
    		{name:'buttonHeight',type:'int'},
    		{name:'buttonX',type:'int'},
    		{name:'buttonY',type:'int'},
    		{name:'openWin',type:'bool'},
    		{name:'openWinType',type:'string'},
    		{name:'openWinURL',type:'string'},
    		{name:'openWinAppName',type:'string'},
    		{name:'openWinAppId',type:'string'},
    		{name:'openWinWidth',type:'int'},
    		{name:'openWinHeight',type:'int'},
    		{name:'linkConfig',type:'string'}
    	];
    	return fields;
    },
    /**
     * 根据键值对从按钮属性列表中获取信息
     * @private
     * @param {} key
     * @param {} value
     * @return {} record or null
     */
    getSouth2RecordByKeyValue:function(key,value){
        var me = this;
        var store = me.getSouth2Com().store;
        var record = store.findRecord(key,value);
        return record;
    },
    /**
     * 获取所有按钮属性信息
     * @private
     * @return {}
     */
    getSouth2Records:function(){
        var me = this;
        var south = me.getSouth2Com();
        var store = south.store;
        var records = [];
        store.each(function(record){
            records.push(record);
        });
        
        return records;
    },
    /**
     * 获取所有按钮属性信息（简单对象数组）
     * @private
     * @return {}
     */
    getSouth2RocordInfoArray:function(){
        var me = this;
        var records = me.getSouth2Records();
        var fields = me.getSouth2StoreFields();
        
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
     * 给按钮记录列表赋值
     * @private
     * @param {} array
     */
    setSouth2RecordByArray:function(array){
        var me = this;
        Ext.Array.each(array,function(obj){
            var rec = ('Ext.data.Model',obj);
            me.addSouth2ValueByRecord(rec);//添加组件记录
        });
    },
    /**
     * 给按钮属性列表赋值
     * @private
     * @param {} buttonItemId
     * @param {} key
     * @param {} value
     */
    setSouth2RecordByKeyValue:function(buttonItemId,key,value){
        var me = this;
        var grid = me.getSouth2Com();
        var store = grid.store;
        var record = store.findRecord('buttonItemId',buttonItemId);
        if(record != null){//存在
            record.set(key,value);
            record.commit();
        }
    },
    /**
     * 添加按钮属性记录
     * @private
     * @param {} record
     */
    addSouth2ValueByRecord:function(record){
        var me = this;
        var list = me.getSouth2Com();
        var store = list.store;
        store.add(record);
    },
    /**
     * 添加属性记录
     * @private
     * @param {} record
     */
    addSouth1ValueByRecord:function(record){
        var me = this;
        var list = me.getSouth1Com();
        var store = list.store;
        store.add(record);
    },
    /**
     * 删除面表单中的按钮
     * @private
     * @param {} componentItemId
     */
    removeButton:function(componentItemId){
        var me = this;
        //删除数据项组件
        var center = me.getCenterCom();
        center.remove(componentItemId);
        //删除数据项属性面板
        me.getComponent('east').remove(componentItemId + me.ParamsPanelItemIdSuffix);
        me.switchParamsPanel('center');
        
        //删除按钮属性列表中的当前数据项数据
        me.removeSouth2ValueByKeyValue('buttonItemId',componentItemId);
    },
    /**
     * 根据键值对移除按钮属性信息
     * @private
     * @param {} key
     * @param {} value
     */
    removeSouth2ValueByKeyValue:function(key,value){
        var me = this;
        var store = me.getSouth2Com().store;
        var record = me.getSouth2RecordByKeyValue(key,value);
        if(record){
            store.remove(record);
        }
    },
    /**
     * 切换按钮属性配置面板
     * @private
     * @param {} componentItemId
     */
    switchButtonParamsPanel:function(componentItemId){
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
            
            me.setButtonParamsPanelValues(componentItemId);//给属性面板赋值
            
            panel.show();//打开
            me.OpenedParamsPanel = componentItemId;
        }
    },
    /**
     * 切换按钮属性配置面板
     * @private
     * @param {} componentItemId
     */
    switchDisplayParamsPanel:function(componentItemId){
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
            
            me.setButtonParamsPanelValues(componentItemId);//给属性面板赋值
            
            panel.show();//打开
            me.OpenedParamsPanel = componentItemId;
        }
    },
    /**
     * 给按钮属性面板赋值
     * @private
     * @param {} record
     */
    setButtonParamsPanelValues:function(componentItemId){
        var me = this;
        if(componentItemId != "center"){
            var record = me.getSouth2RecordByKeyValue('buttonItemId',componentItemId);
            var obj = {};
            var fields = me.getSouth2StoreFields();
            Ext.Array.each(fields,function(field){
                obj[field.name] = record.get(field.name);
            });
            //属性面板ItemId
	        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
	        //组件属性面板
	        var panel = me.getComponent('east').getComponent(panelItemId);
            var custombuttonfieldset = panel.getComponent('custombuttonfieldset');
            custombuttonfieldset.setAllValues(obj);
        }
    },
    /**
     * 在表单上创建按钮
     * @private
     * @param {} obj
     */
    insertButtonComToCenter:function(obj){
    	var me = this;
    	var center = me.getCenterCom();
    	
    	//右键菜单
    	var menuItems = [];
    	//弹出窗口
    	if(obj.openWin){
    		menuItems.push({
    			text:"弹出窗口",iconCls:'build-button-configuration-blue',
            	handler:function(){
        			me.openButtonWin(obj);
            	}
    		});
    	}
    	//切换组件属性配置面板
    	menuItems.push({
	    	text:"属性面板",iconCls:'build-button-configuration-blue',
	    	handler:function(){
				me.switchButtonParamsPanel(obj.buttonItemId);
	    	}
	    });
	    //删除按钮
    	menuItems.push({
            text:"删除",iconCls:'delete',
            handler:function(){
                me.removeButton(obj.buttonItemId);
            }
        });
    	
        //按钮对象
    	var but = {
    		xtype:'button',
    		text:obj.buttonName,
    		itemId:obj.buttonItemId,
    		x:obj.buttonX,
    		y:obj.buttonY,
    		draggable:true,
            resizable:{handles:'w e'},
    		listeners:{
    			click:function(){
    				me.switchButtonParamsPanel(obj.buttonItemId);
    			},
    			//组件拖动监听
				move:function(com,x,y,eOpts){
		            me.setSouth2RecordByKeyValue(obj.buttonItemId,'buttonX',x);
		            me.setSouth2RecordByKeyValue(obj.buttonItemId,'buttonY',y);
				},
				//大小变化监听
				boxready:function(com,width,height,e){
					me.setSouth2RecordByKeyValue(obj.buttonItemId,'buttonWidth',width);
		            me.setSouth2RecordByKeyValue(obj.buttonItemId,'buttonHeight',height);
				},
				//大小变化监听
				resize:function(com,width,height,oldWidth,oldHeight,e){
		        	me.setSouth2RecordByKeyValue(obj.buttonItemId,'buttonWidth',width);
		            me.setSouth2RecordByKeyValue(obj.buttonItemId,'buttonHeight',height);
				},
    			contextmenu:{
	                element:'el',
	                fn:function(e,t,eOpts){
	                    //禁用浏览器的右键相应事件 
	                    e.preventDefault();e.stopEvent();
	                    //右键菜单
	                    new Ext.menu.Menu({
	                        items:menuItems
	                    }).showAt(e.getXY());//让右键菜单跟随鼠标位置
	                }
	            }
    		}
    	};
    	if(obj.buttonWidth){
    		but.width = obj.buttonWidth;
    	}
    	if(obj.buttonHeight){
    		but.buttonHeight = obj.buttonHeight;
    	}
    	center.add(but);
    },
    /**
     * 创建按钮属性面板
     * @private
     * @param {} obj
     */
    insertButtonParamsPanel:function(obj){
    	var me = this;
        var east = me.getComponent('east');
        //属性面板ItemId
        var panelItemId = obj.buttonItemId + me.ParamsPanelItemIdSuffix;
        
        var par = {};
        for(var i in obj){
        	par[i+"Value"] = obj[i];
        }
        par.itemId = "custombuttonfieldset";
        
        var fieldset = Ext.create('Ext.build.CustomButtonFieldSet',par);
        //内部的每个值变化都要监听更改对应按钮属性列表的值
        var fields = fieldset.items.items;
        for(var i in fields){
        	if(fields[i].name){
        		fields[i].on({
	        		change:function(com,value){
	        			me.setSouth2RecordByKeyValue(obj.buttonItemId,com.name,value);
	        		}
	        	});
        	}
        }
        
        //创建组件属性面板
        var panel = {
        	xtype:'form',
            itemId:panelItemId,
            title:obj.buttonName,
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            hidden:true,
            items:fieldset
        };
        
        //添加面板
        east.add(panel);
    },
    /**
     * 创建Display属性面板
     * @private
     * @param {} obj
     */
    insertDisplayParamsPanel:function(obj){
        var me = this;
        var east = me.getComponent('east');
        //属性面板ItemId
        var panelItemId = obj.InteractionField + me.ParamsPanelItemIdSuffix;
        
        var par = {};
        for(var i in obj){
            par[i+"Value"] = obj[i];
        }
        par.itemId = "custombuttonfieldset";
        
        var fieldset = Ext.create('Ext.build.CustomDisplaynFieldSet',par);
        //内部的每个值变化都要监听更改对应按钮属性列表的值
        var fields = fieldset.items.items;
        for(var i in fields){
            if(fields[i].name){
                fields[i].on({
                    change:function(com,value){
                        me.setSouthRecordByKeyValue(obj.InteractionField,com.name,value);
                    }
                });
            }
        }
        
        //创建组件属性面板
        var panel = {
            xtype:'form',
            itemId:panelItemId,
            title:obj.DisplayName,
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            hidden:true,
            items:fieldset
        };
        
        //添加面板
        east.add(panel);
    },
    /**
     * 在表单上创建Display
     * @private
     * @param {} obj
     */
    insertDisplayComToCenter:function(obj){
        var me = this;
        var center = me.getCenterCom();
        
        //右键菜单
        var menuItems = [];
        //弹出窗口

        //切换组件属性配置面板
        menuItems.push({
            text:"属性面板",iconCls:'build-button-configuration-blue',
            handler:function(){
                me.switchDisplayParamsPanel(obj.InteractionField);
            }
        });
        //删除按钮
        menuItems.push({
            text:"删除",iconCls:'delete',
            handler:function(){
                me.removeButton(obj.InteractionField);
            }
        });
        
        //Display对象
        var but = {
            xtype:'displayfield',
            fieldLabel:obj.DisplayName,
            //text:obj.DisplayName,
            name:obj.InteractionField,
            itemId:obj.InteractionField,
            x:obj.X,
            y:obj.Y,
            value:""+obj.defaultValue+"",
            draggable:true,
            resizable:{handles:'w e'},
            listeners:{
                click:function(){
                    me.switchDisplayParamsPanel(obj.InteractionField);
                },
                focus:function( com,The, eOpts )
                {
                    me.switchDisplayParamsPanel(obj.InteractionField);
                },
                //组件拖动监听
                move:function(com,x,y,eOpts){
                    me.setSouthRecordByKeyValue(obj.InteractionField,'X',x);
                    me.setSouth2RecordByKeyValue(obj.InteractionField,'Y',y);
                },
                //大小变化监听
                boxready:function(com,width,height,e){
                    me.setSouth2RecordByKeyValue(obj.InteractionField,'Width',width);
                    me.setSouth2RecordByKeyValue(obj.InteractionField,'Height',height);
                },
                //大小变化监听
                resize:function(com,width,height,oldWidth,oldHeight,e){
                    me.setSouth2RecordByKeyValue(obj.InteractionField,'Width',width);
                    me.setSouth2RecordByKeyValue(obj.InteractionField,'Height',height);
                },
                contextmenu:{
                    element:'el',
                    fn:function(e,t,eOpts){
                        //禁用浏览器的右键相应事件 
                        e.preventDefault();e.stopEvent();
                        //右键菜单
                        new Ext.menu.Menu({
                            items:menuItems
                        }).showAt(e.getXY());//让右键菜单跟随鼠标位置
                    }
                }
            }
        };
        if(obj.Width){
            but.width = obj.Width;
        }
        if(obj.Height){
            but.height = obj.Height;
        }
        center.add(but);
    },
    /**
     * 创建Display属性面板
     * @private
     * @param {} obj
     */
    insertDisplayParamsPanel:function(obj){
        var me = this;
        var east = me.getComponent('east');
        //属性面板ItemId
        var panelItemId = obj.buttonItemId + me.ParamsPanelItemIdSuffix;
        
        var par = {};
        for(var i in obj){
            par[i+"Value"] = obj[i];
        }
        par.itemId = "custombuttonfieldset";
        
        var fieldset = Ext.create('Ext.build.CustomButtonFieldSet',par);
        //内部的每个值变化都要监听更改对应按钮属性列表的值
        var fields = fieldset.items.items;
        for(var i in fields){
            if(fields[i].name){
                fields[i].on({
                    change:function(com,value){
                        //me.setSouth2RecordByKeyValue(obj.buttonItemId,com.name,value);
                    }
                });
            }
        }
        
        //创建组件属性面板
        var panel = {
            xtype:'form',
            itemId:panelItemId,
            title:obj.buttonName,
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            hidden:true,
            items:fieldset
        };
        
        //添加面板
        east.add(panel);
    },
    /**
     * 按钮弹出窗口
     * @private
     * @param {} obj
     */
    openButtonWin:function(obj){
    	var me = this;
		var maxHeight = document.body.clientHeight*0.98;
		var maxWidth = document.body.clientWidth*0.98;
		
		if(obj.openWinType == 'html'){
			var win = Ext.create('Ext.panel.Panel',{
				maxWidth:maxWidth,
				autoScroll:true,
	    		modal:true,//模态
	    		floating:true,//漂浮
				closable:true,//有关闭按钮
				draggable:true,//可移动
				resizable:true,//可变大小
				width:obj.openWinWidth || 600,
				height:obj.openWinHeight || 300,
				title:obj.buttonName,
				html:"<html><body><iframe src='"+obj.openWinURL+"' style='height:100%;width:100%' frameborder='no'></iframe></body></html>"
			});
			if(win.height > maxHeight){win.height = maxHeight;}
			win.show();
		}else{
			var callback = function(appInfo){
				var ClassCode = appInfo[me.fieldsObj.ClassCode];
				var cl = eval(ClassCode);
				var par = {
					maxWidth:maxWidth,
					autoScroll:true,
		    		modal:true,//模态
		    		floating:true,//漂浮
					closable:true,//有关闭按钮
					draggable:true,//可移动
					resizable:true,//可变大小
    				title:obj.openWinAppName
    			};
    			if(obj.openWinWidth){par.width = obj.openWinWidth};
    			if(obj.openWinHeight){par.height = obj.openWinHeight};
    			
				var win = Ext.create(cl,par);
    			if(win.height > maxHeight){win.height = maxHeight;}
    			win.show();
			}
			me.getAppInfoFromServer(obj.openWinAppId,callback);
		}
    },
    /**
     * 在表单上创建所有按钮
     * @private
     */
    createButtonsToCenter:function(){
    	var me = this;
    	var arr = me.getSouth2RocordInfoArray();
    	for(var i in arr){
    		me.insertButtonComToCenter(arr[i]);
    		me.insertButtonParamsPanel(arr[i]);
    	}
    },
    /**
     * 创建自定义按钮的弹出窗口监听
     * @private
     * @return {}
     */
    createCustomButtonsEventStr:function(){
    	var me = this;
    	var records = me.getSouth2Records();
    	var event = "";
    	for(var i in records){
    		var record = records[i];
    		if(record.get('openWin') && record.get('openWinType') != 'html'){
    			event += "me.addEvents('AfterOpenWin" + record.get('buttonItemId') + "');";
    		}
    	}
    	return event;
    },
    /**
     * 创建所有自定义按钮Str
     * @private
     * @return {}
     */
    createCustomButtonsStr:function(){
    	var me = this;
    	
    	var records = me.getSouth2Records();
    	
    	var items = "";
    	
    	for(var i in records){
    		var but = me.createCustomButtonStr(records[i]);
    		items += but + ",";
    	}
    	
    	if(items != ""){items = items.slice(0,-1);}
    	items += "";
    	
    	return items;
    },
    /**
     * 创建自定义按钮Str
     * @private
     * @param {} record
     */
    createCustomButtonStr:function(record){
    	var me = this;
    	var but = "";
    	
    	but += "{";
    	but += "xtype:'button'";
    	but += ",text:'" + record.get('buttonName') + "'";
    	but += ",itemId:'" + record.get('buttonItemId') + "'";
    	but += ",x:" + record.get('buttonX');
    	but += ",y:" + record.get('buttonY');
    	
    	if(record.get('buttonWidth') && record.get('buttonWidth') > 0){but += ",width:" + record.get('buttonWidth');}
    	if(record.get('buttonHeight') && record.get('buttonHeight') > 0){but += ",height:" + record.get('buttonHeight');}
    	if(record.get('openWin')){but += ",handler:function(button,e){" + me.createOpenWinStr(record) + "}";}
    	
    	but += "}";
    	return but;
    },
    /**
     * 创建自定义按钮弹出窗口Str
     * @private
     * @param {} record
     * @return {}
     */
    createOpenWinStr:function(record){
    	var me = this;
    	var openWin = "";
    	
    	openWin += "var maxHeight=document.body.clientHeight*0.98;";
		openWin += "var maxWidth=document.body.clientWidth*0.98;";
    	
    	if(record.get('openWinType') == 'html'){
    		openWin += 
    		"var win = Ext.create('Ext.panel.Panel',{" + 
				"maxWidth:maxWidth," + 
				"autoScroll:true," + 
	    		"modal:true," + 
	    		"floating:true," + 
				"closable:true," + 
				"draggable:true," + 
				"resizable:true," + 
				"width:" + (record.get('openWinWidth') || 600) + "," + 
				"height:" + (record.get('openWinHeight') || 300) + "," + 
				"title:'" + record.get('buttonName') + "'," + 
				"html:'<html><body><iframe src=\\\"" + record.get('openWinURL') + "\\\" style=\\\"height:100%;width:100%\\\" frameborder=\\\"no\\\"></iframe></body></html>'" + 
			"});" + 
			"if(win.height > maxHeight){win.height = maxHeight;}" + 
			"win.show();";
    	}else{
    		openWin += 
    		"var callback = function(appInfo){" + 
				"var ClassCode = appInfo['" + me.fieldsObj.ClassCode + "'];" + 
				"var cl = eval(ClassCode);" + 
				"var par = {" + 
					"maxWidth:maxWidth," + 
					"autoScroll:true," + 
		    		"modal:true," + 
		    		"floating:true," + 
					"closable:true," + 
					"draggable:true," + 
					"resizable:true," + 
    				"title:'" + record.get('buttonName') + "'" + 
    			"};";
    			if(record.get('openWinWidth')){
    				openWin += "par.width=" + record.get('openWinWidth') + ";";
    			};
    			if(record.get('openWinHeight')){
    				openWin += "par.height=" + record.get('openWinHeight') + ";";
    			};
    		openWin += 
				"var win = Ext.create(cl,par);" + 
    			"if(win.height > maxHeight){win.height = maxHeight;}" + 
    			"win.show();" + 
    			"me.fireEvent('AfterOpenWin" + record.get('buttonItemId') + "',win);" + 
			"};" + 
			"me.getInfoByIdFormServer('" + record.get('openWinAppId') + "',callback);";
    	}
    	
    	return openWin;
    },
    /**
     * 改变下拉框初始值
     * @private
     * @param {} componentItemId
     */
    changeComboParamsPanelDefaultValue:function(componentItemId){
    	var me = this;
    	var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
    	var east = me.getComponent('east');
        var panel = east.getComponent(panelItemId);
        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
        var valueField = panel.getComponent('otherParams').getComponent('valueField');
        var displayField = panel.getComponent('otherParams').getComponent('displayField');
        var serverUrl = panel.getComponent('otherParams').getComponent('serverUrl');
        
        
        var valueFieldValue = valueField.getValue();
        var displayFieldValue = displayField.getValue();
        var url = serverUrl.getValue();
        if(valueFieldValue && valueFieldValue != "" && displayFieldValue && displayFieldValue != "" && url && url != ""){
        	var valueNameArr = valueFieldValue.split("_").slice(-2);
			var valueName = valueNameArr.join("_");
			
			var textNameArr = displayFieldValue.split("_").slice(-2);
			var textName = textNameArr.join("_");
        
        	defaultValue.valueName = valueName;
        	defaultValue.textName = textName;
        	defaultValue.store.load();
        }else{
        	//Ext.Msg.alert('提示','值字段、显示字段、服务都需要选择！');
        }
    },
    /**
	 * 给应用属性赋值
	 * @private
	 * @param {} appInfo
	 */
	setAppInfo:function(appInfo){
		var me = this;
		me.LabID = appInfo.LabID;
		me.ModuleTypeID = appInfo.ModuleTypeID;
		me.EName = appInfo.EName;
		me.ExecuteCode = appInfo.ExecuteCode;
		me.Creator = appInfo.Creator;
		me.Modifier = appInfo.Modifier;
		me.PinYinZiTou = appInfo.PinYinZiTou;
		me.DataTimeStamp = appInfo.DataTimeStamp;
		
		me.DataAddTime = appInfo.DataAddTime;
		me.DataUpdateTime = appInfo.DataUpdateTime;
	},
	/**
	 * 是否禁用保存按钮
	 * @private
	 * @param {} bo
	 */
	disablePanel:function(bo){
		var me = this;
		var north = me.getComponent('north');
		var save = north.getComponent('save');
		var saveAs = north.getComponent('saveAs');
		if(bo){
			save.disable();
			saveAs.disable();
		}else{
			save.enable();
			saveAs.enable();
		}
	}
});