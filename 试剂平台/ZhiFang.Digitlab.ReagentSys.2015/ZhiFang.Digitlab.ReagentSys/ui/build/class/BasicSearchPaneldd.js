/**
 * 高级(一般)查询构建工具
 * 包含一般查询和高级查询
 * 1.可以添加关系运算符,
 * 2.公开属性或方法设置全与或者全或的关系
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
Ext.define('Ext.build.BasicSearchPanel',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basicsearchpanel',
    //=====================可配参数=======================
    /**
     * 应用组件ID
     */
    appId:-1,
    appType:-1,
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
    buildTitle:'高级(一般)查询表单形式构建工具',
    addButtonId:'button_',
    N:0,
    /**
     * 获取数据服务列表时后台接收的参数名称
     * @type String
     */
    objectServerParam:'EntityName',
    /**
     * 按钮类型
     * @type 
     */
    btnTypeList:[
        ['select','查询按钮'],
        ['reset','重置按钮'],
        ['close','关闭按钮']
    ],
    //数据对象配置private
    
    win:null,//创建和弹出选择器窗体
    win2:null,//创建和弹出选择器窗体
    winHeight:270,        //弹出选择器窗体高度像素
    winWidth:460,       //弹出选择器窗体宽度像素
    winTitle:'',        //弹出选择器窗体标题

    operationid:'',
    logicalid:'',
    addButtomId:'',//按钮itemid
    keyField:'inputValue' , //数据项值字段,默认值为inputValue
    checkboxgroupName:"checkboxgroupName",//+Ext.idSeed,//复选组组名
    
    lastValues:[],   
    
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
    /***
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
	 * 左括号 
	 * @type 
	 */
	LeftType:[
        [null,''],
		['(','('],
		['((','(('],
		['(((','((('],
		['((((','((((']
	],
	 /**
	 * 右括号 
	 * @type 
	 */
	RightType:[
        [null,''],
		[')',')'],
		['))','))'],
		[')))',')))'],
		['))))','))))']
	],
	
	/**
	 *关系运算关系
	 * @type 
	 */
	OperationType:[['=', '等于'],['!=', '不等于'],['>', '大于'],['<', '小于'],['<=', '小于等于'],['>=', '大于等于'],['in', '包含'],['not in', '不包含']],
	 /**
	 * 逻辑运算符
	 * @type 
	 */
	LogicalType:[['and', '与'],['or', '或'],['not', '非']],
	
    /**
     * 数据项类型
     * @type 
     */
    comTypeList:[
        ['combobox','下拉框'],
        ['textfield','文本框'],
        ['textareafield','文本域'],
        ['numberfield','数字框'],
        ['daterange','日期区间新'],
        ['dateintervals','日期区间旧'],
        
        ['numbersintervals','数字区间'],
        ['timeintervals','时间区间'],
        ['datefield','日期框'],
        ['timefield','时间框'],
        ['datetimenew','日期时间'],
        
        ['checkboxgroup','复选框'],
        ['radiogroup','单选框'],
        ['datacombobox','定值下拉框'],
        ['datacheckboxgroup','定值复选组'],
        ['dataradiogroup','定值单选组'],
        ['label','纯文本'],
        //['image','图片'],
        //['htmleditor','超文本'],
        //['filefield','文件'],
        ['button','按钮']
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
                title:'表单',
                itemId:'center',
                width:me.defaultPanelWidth,
                height:me.defaultPanelHeight
            }]
        };
        return com;
    },
    //==================操作属性列表的某一控件属性,更新展示区域的控件显示效果==============
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
     * 重新生成展示区域的某一个控件
     * @param {} InteractionField:交互字段,某一控件的itemId
     * @param {} newValue:修改的值;record
     */
    setchangeComponent:function(InteractionField,record,labelStyle,labelWidthNew,labelWidthOld,x,y){
        var me=this;
        var center=me.getCenterCom();
        var tempItem= me.getCenterCom().getComponent(InteractionField);
        //var labelStyle=null;
        var owner = center.ownerCt;
            center.remove(tempItem);
        //重新生成新的控件
        var com =me.newfromItem(record,labelStyle,labelWidthNew,labelWidthOld,x,y);
            center.add(com); 
        var value=record.get('valueField');
        var text=record.get('textField');
        var myUrl=record.get('ServerUrl');
        var type=record.get('Type');
        //单/复选组数据加载处理
        if(type== 'checkboxgroup'||type== 'radiogroup'){
            var groudName=com.itemId;
            if(myUrl==''||myUrl==null){}else{
            var url=getRootPath() + "/" + myUrl.split("?")[0];
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
        }
        //下拉列表数据加载处理
        if(type== 'combobox'){
            if(myUrl==''||myUrl==null){}else{
            var url=getRootPath() + "/" + myUrl.split("?")[0];
            var defaultValue=record.get('defaultValue');
            var radioItem=me.getCenterCom().getComponent(InteractionField);
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}else{
            var st= me.GetComboboxItems(url,value,text);
                radioItem.store=st;
            }
        }
       }
    },
    //==============================某一控件属性更新==============================
    /**
     * 展示区域里的某一控件重新生成
     * @private
     * @return {}
     */
    newfromItem:function(record,labelStyle,labelWidthNew,labelWidthOld,x,y){
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
        if(type== 'daterange'){
            com.fieldLabelTwo = record.get('fieldLabelTwo');
            com.labelWidthTwo=record.get('LabelWidthTwo');;
        }
        if(type== 'datacombobox'){
            com.boundField = record.get('BoundField');
        }
            com.type = record.get('Type');
            //公共属性
            com.fieldLabel=record.get('DisplayName');
            if(labelWidthNew==0&&labelWidthOld==0){
	            var tempValue = labelWidthNew-labelWidthOld;  
	            com.width = record.get('Width');
	            com.labelWidth=record.get('LabelWidth');
                com.x = x;
                com.y = y;
            }else{
                var tempValue2 = labelWidthNew-labelWidthOld;
	            com.width = record.get('Width')+tempValue2;;
	            com.labelWidth=labelWidthNew;
                com.x = record.get('X');
                com.y = record.get('Y');
            }
            
            com.itemId = record.get('InteractionField');
            if(labelStyle!=null){
            com.labelStyle=labelStyle;
            }else{
            com.labelStyle=record.get('LabFont');
            }
            com.height = record.get('Height');
            com.readOnly=record.get('IsReadOnly');
            com.hidden=record.get('IsHidden');
            com.operationcontents=record.get('operationcontents');
            com.isOperation = record.get('isOperation');

            //是否有边框
            if(me.hasBorder){
                com.border = 1;
            }else{
            com.border = 0;
            }
            if(com.xtype!='daterange'&&com.xtype!='dateintervals'&&com.xtype!='timeintervals'&&com.xtype!='numbersintervals'){
                com.style = {
                    borderColor:'red',
                    borderStyle:'dashed'
                };
            }
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
            com.draggable = true;//注释这一行,改变大小事件失效,拖放事件生效
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
        var com = {
            xtype:'grid',
            title:'数据项属性列表',
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
                {text:'光标顺序',dataIndex:'sortNum',width:50,align:'center',
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
                {text:'数据项类型',dataIndex:'Type',width:100,align: 'center',
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
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                var y=record.get('Y');
                                me.setSouthRecordByKeyValue(InteractionField,'Y',y);
                                me.setSouthRecordByKeyValue(InteractionField,'X',newValue);
                                me.setComponentXY(InteractionField,newValue,y);
                               
                            }
                        }
                    }
                },
                {text:'位置Y',dataIndex:'Y',width:50,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                var x=record.get('X');
                                me.setSouthRecordByKeyValue(InteractionField,'Y',newValue);
                                me.setSouthRecordByKeyValue(InteractionField,'X',x);
                                me.setComponentXY(InteractionField,x,newValue);
                            }
                        }
                    }
                },
                {text:'宽度',dataIndex:'Width',width:50,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:3000,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordByKeyValue(InteractionField,'Width',newValue);
                                me.setComponentWidth(InteractionField,newValue);
                            }
                        }
                    }
                },
                 {text:'高度',dataIndex:'Height',width:50,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:2000,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordByKeyValue(InteractionField,'Height',newValue);
                                me.setComponentHeight(InteractionField,newValue);
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
                                me.setSouthRecordByKeyValue(InteractionField,'labelWidth',newValue);
                                me.setchangeComponent(InteractionField,record,null,newValue,oldValue,x,y);
                                
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
                            me.setSouthRecordByKeyValue(InteractionField,'LabFont',record.get('LabFont'));
                        }
                    }]
                },
                {text:'是否有运算关系',dataIndex:'isOperation',hidden:true,
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    },
                    listeners:{
                        change:function(com,The,eOpts){
                            var panelItemId = com + me.ParamsPanelItemIdSuffix;
                        }
                   }
                },

                {text:'运算符',dataIndex:'operationcontents',width:60,align:'center',hidden:true,
                    renderer:function(value, p, record){
                        var typelist = me.OperationType;
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
                            data:me.OperationType
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },

                {text:'只读',dataIndex:'IsReadOnly',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    },listeners:{
                            //change不生效
                            checkchange:function(com,rowIndex,checked, eOpts ){
                             //获取数据项属性列表所有组件信息
                             var southRecords=me.getSouthRecords();
                             //获取当前选中行数据
                             var record=southRecords[rowIndex];
                             var componentItemId=record.get('InteractionField');
                             me.setchangeComponent(componentItemId,record,null,0,0,0,0);
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
                                me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                            }
                        }
                },
                {text:'显示名称字体内容',dataIndex:'LabFont',hidden:true},
                {text:'数据地址',dataIndex:'ServerUrl',width:270,hidden:false},
                {text:'默认值',dataIndex:'defaultValue',width:50,hidden:true},
                {text:'值字段(下拉/单,复选)',dataIndex:'valueField',hidden:true},
                {text:'显示字段(下拉/单,复选)',dataIndex:'textField',hidden:true},
                {text:'列数(单/复选)',dataIndex:'Columns',width:100,align:'center',
                    xtype:'numbercolumn',format:'0',hidden:true,
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var type1=record.get("Type");
                                if(type1=="radiogroup"||type1=="checkboxgroup"){
                                 me.setSouthRecordByKeyValue(InteractionField,'Columns',newValue);
                                }else{
                                }
                            }
                        }
                    }
                },
                {text:'列宽(单/复选)',dataIndex:'ColumnWidth',width:100,align:'center',
                    xtype:'numbercolumn',format:'0',hidden:true,
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
                {text:'日期区间显示宽度二',dataIndex:'LabelWidthTwo',hidden:true,
                    xtype:'numbercolumn',format:'0',hidden:true,
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                record.set('LabelWidthTwo',newValue);
                                record.commit();
                                
                            }
                        }
                    }
                },
                {text:'日期区间分隔符一',dataIndex:'LabelSeparatorOne',hidden:true},
                {text:'日期区间分隔符二',dataIndex:'LabelSeparatorTwo',hidden:true},
                {text:'显示名称二',dataIndex:'fieldLabelTwo',hidden:true},
                {text:'行列方式',dataIndex:'RawOrCol',hidden:true},//raw:行;col:列
                {text:'数字最小值',dataIndex:'NumberMin',hidden:true},
                {text:'数字最大值',dataIndex:'NumberMax',hidden:true},
                {text:'数字增量',dataIndex:'NumberIncremental',hidden:true},
                {text:'显示格式',dataIndex:'ShowFomart',hidden:true},
                {text:'是否允许手输',dataIndex:'CanEdit',hidden:true},
                {text:'选择文件按钮文字',dataIndex:'SelectFileText',hidden:true},
                {text:'绑定字段',dataIndex:'BoundField'},
                {text:'按钮类型',dataIndex:'btnType'},
                {text:'定值下拉框数据',dataIndex:'combodata'}
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
                xtype:'combobox',fieldLabel:'逻辑关系',
                itemId:'logicalType',name:'logicalType',
                queryMode: 'local',
                labelWidth:55,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:false,typeAhead:true,
                forceSelection:true,
                emptyText:'请选择逻辑类型',
                displayField:'name',
                valueField:'value',
                value:'and',
                store:new Ext.data.Store({ 
                        fields:['value', 'name'],
                        data :[
                            {"value":"and", "name":"全与关系"},
                            {"value":"or", "name":"全或关系"}
                        ]})
            
            },{
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
                })
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
                    xtype:'label',text:'字体设置:',width:55,margin:'2 0 2 0',hidden:true
                },{
                    xtype:'textfield',hidden:true,value:'',
                    itemId:'titleStyle',name:'titleStyle'
                },{
                    xtype:'image',itemId:'configuration',hidden:true,
                    imgCls:'build-img-font-configuration hand',
                    width:16,height:16,
                    margin:'2 0 2 5',cls:'hand',
                    listeners:{
                        click:{
                            element:'el',
                            fn:function(){
                                //调用生成标题字体设置组件
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
        cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
        labelcls:'labelcls',//字体属性设置:label样式
        btnHidden: false,//确定或者取消按钮的显示false或者隐藏true
        listeners:{
                    onOKCilck:function(o){
                    //获取设置当前控件的文字属性结果值
                      lastValue=this.GetValue();
                      var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                     //重新生成展示区域的某一个控件
                      me.setchangeComponent(componentItemId,record,lastValue,0,0,0,0);
                   
                      var bm = Ext.getCmp('OpenCategoryWinTwo_id');
                       if(bm==undefined){ 
                          }else{
                          bm.close();
                          }
                          
                      var InteractionField=componentItemId;
                      me.setSouthRecordByKeyValue(InteractionField,'LabFont',lastValue);
                    },
                    onCancelCilck:function(o){
                    //获取设置当前控件的文字属性结果值
                      var bm = Ext.getCmp('OpenCategoryWinTwo_id');
                      if(bm==undefined){ 
                          }else{
                          bm.close();
                          }
                     me.setSouthRecordByKeyValue(InteractionField,'LabFont',"");
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
                    change:function(owner,newValue,oldValue,eOpts){
                        var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
                        if(newValue && newValue != "" && index != -1){
                            me.objectChange(owner,newValue);
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
                labelWidth:55,anchor:'100%',
                hidden:true,
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
                            store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + objectName.value;   
                        }
                    }
                })
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
                    {boxLabel:'四列',name:'layoutType',inputValue:'4',checked:false}
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
            },
            {
              xtype:'checkbox',itemId:'hasSelectButton',name:'hasSelectButton',boxLabel:'查询按钮===='//,checked:true
              ,listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        var selectItemId='SelectButton';//查询按钮itemId
                        //me.isJustOpen==false&&
                        if(newValue==true){//页面已经打开并是勾选时才处理
                          var list = me.getComponent('south');//列属性列表
	                      var count=list.getStore().getCount();
	                      if(count<=0){
                              Ext.Msg.alert('提示','请先选择数据对象后再操作');
	                          return;
	                      }else{
	                          var store = me.getComponent('south').store;
                              var record=store.findRecord('InteractionField',selectItemId);//查询按钮是否已经存在
                              if(record==null){//查询按钮不存在时才添加
                                  var obj={itemId:selectItemId,btnName:'查询',btnType:'select'};
		                          store.add(me.addButton(obj));
		                          var newRecord=me.getSouthRecordByKeyValue('InteractionField',selectItemId);
		                           //添加组件属性面板
		                          me.addParamsPanel(newRecord.get('Type'),newRecord.get('InteractionField'),newRecord.get('DisplayName'));
		                          me.setAllComXY(newRecord);
                              }
	                      }
                        }else if(newValue==false){//页面已经打开并是不勾选时处理
                            //删除表单的查询按钮及属性列表的行数据
                            var list = me.getComponent('south');//列属性列表
                            var count=list.getStore().getCount();
                            if(count>0){
	                            var store = me.getComponent('south').store;
	                            var record=store.findRecord('InteractionField',selectItemId);//查询按钮是否已经存在
	                            if(record&&record!=null){
	                                store.remove(record);
	                            }
                                var centerCom=me.getCenterCom();
                                var itemCom=centerCom.getComponent(selectItemId);
                                if(itemCom&&itemCom!=null){
                                    centerCom.remove(itemCom);
                                }
                            }
                        }
                    }
                }
            },{
              xtype:'checkbox',itemId:'hasResetButton',name:'hasResetButton',boxLabel:'重置按钮===='//,checked:true
              ,listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        var resetItemId='ResetButton';//查询按钮itemId
                        if(newValue==true){//页面已经打开并是勾选时才处理
                          var list = me.getComponent('south');//列属性列表
                          var store=list.getStore().getCount();
                          if(store<=0){
                              Ext.Msg.alert('提示','请先选择数据对象后再操作');
                              return;
                          }else{
                              var store = me.getComponent('south').store;
                              var record=store.findRecord('InteractionField',resetItemId);//查询按钮是否已经存在
                              if(record==null){//查询按钮不存在时才添加
                                  var obj={itemId:resetItemId,btnName:'重置',btnType:'reset'};
                                  store.add(me.addButton(obj));
                                  var newRecord=me.getSouthRecordByKeyValue('InteractionField',resetItemId);
                                   //添加组件属性面板
                                  me.addParamsPanel(newRecord.get('Type'),newRecord.get('InteractionField'),newRecord.get('DisplayName'));
                                  me.setAllComXY(newRecord);
                              }
                          }
                        }else if(newValue==false){//页面已经打开并是不勾选时处理
                            //删除表单的查询按钮及属性列表的行数据
                            var list = me.getComponent('south');//列属性列表
                            var count=list.getStore().getCount();
                            if(count>0){
                                var store = me.getComponent('south').store;
                                var record=store.findRecord('InteractionField',resetItemId);//查询按钮是否已经存在
                                if(record&&record!=null){
                                    store.remove(record);
                                }
                                var centerCom=me.getCenterCom();
                                var itemCom=centerCom.getComponent(resetItemId);
                                if(itemCom&&itemCom!=null){
                                    centerCom.remove(itemCom);
                                }
                            }
                        }
                    }
                }
            },{
              xtype:'checkbox',itemId:'hasColseButton',name:'hasColseButton',boxLabel:'关闭按钮===='//,checked:true
              ,listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        var colseButton='ColseButton';//查询按钮itemId
                        if(newValue==true){//页面已经打开并是勾选时才处理
                          var list = me.getComponent('south');//列属性列表
                          var store=list.getStore().getCount();
                          if(store<=0){
                              Ext.Msg.alert('提示','请先选择数据对象后再操作');
                              return;
                          }else{
                              var store = me.getComponent('south').store;
                              var record=store.findRecord('InteractionField',colseButton);//查询按钮是否已经存在
                              if(record==null){//查询按钮不存在时才添加
                                  var obj={itemId:colseButton,btnName:'关闭',btnType:'colse'};
                                  store.add(me.addButton(obj));
                                  var newRecord=me.getSouthRecordByKeyValue('InteractionField',colseButton);
                                   //添加组件属性面板
                                  me.addParamsPanel(newRecord.get('Type'),newRecord.get('InteractionField'),newRecord.get('DisplayName'));
                                  me.setAllComXY(newRecord);
                              }
                          }
                        }else if(newValue==false){//页面已经打开并是不勾选时处理
                            //删除表单的查询按钮及属性列表的行数据
                            var list = me.getComponent('south');//列属性列表
                            var count=list.getStore().getCount();
                            if(count>0){
                                var store = me.getComponent('south').store;
                                var record=store.findRecord('InteractionField',colseButton);//查询按钮是否已经存在
                                if(record&&record!=null){
                                    store.remove(record);
                                }
                                var centerCom=me.getCenterCom();
                                var itemCom=centerCom.getComponent(colseButton);
                                if(itemCom&&itemCom!=null){
                                    centerCom.remove(itemCom);
                                }
                            }
                        }
                    }
                }
            },
            {
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
     * 重新浏览处理
     * 先清除展示区域的原来内容,再更换组件属性面板
     * @private
     */
    changeBrowse:function(){
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
                CName:params.appCName,//名称
                ModuleOperCode:params.appCode,//功能编码
                ModuleOperInfo:params.appExplain,//功能简介
                InitParameter:params.defaultParams,//初始化参数
				AppType:me.appType,//7,//应用类型(高级查询)
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
        getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + newValue;
        getDataServerUrl.store.load();
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
                        LabelWidth:55,//显示名称宽度
                        LabFont:'',//显示名称字体内容
                        Type:record.get(me.columnParamsField.Type) || 'textfield',//数据项类型
                        X:0,//位置X
                        Y:0,//位置X
                        LabelSeparatorOne:':',//日期区间分隔符
                        LabelWidthTwo:0,//日期区间
                        Width:160,//数据项宽度
                        IsReadOnly:false,//只读
                        IsHidden:false,//隐藏
                        operationcontents:'',
                        logicalcontents:'',
                        brackets:''
                        
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
            if(com.xtype!='dateintervals'&&com.xtype!='timeintervals'&&com.xtype!='numbersintervals'){
	            com.style = {
	                borderColor:'red',
	                borderStyle:'dashed'
	            };
            }
            
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
        arr = me.createRelationXY(arr);
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
            com.isOperation = record.get('isOperation');//运算关系
            com.sortNum = record.get('sortNum');//光标顺序
            
            //是否显示名称
            if(!me.hasLab){
                com.fieldLabel = "";
            }
            coms.push(com);
        }
        return coms;
    },
    
    /**
     * 设置组件对象的XY值(组件和运算关系、逻辑关系的XY位置)
     * @private
     * @param {} comArr
     * @return {}
     */
    createRelationXY:function(comArr){
        
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
        }else if(type == 'datefield'){//日期框
            com = me.createDatefield(record);
        }else if(type == 'timefield'){//时间框
            com = me.createTimefield(record);
        }else if(type == 'datetimenew'){//日期时间框
            com = me.createDatetimefield(record);
        }else if(type == 'checkboxgroup'){//复选框
            com = me.createCheckboxfield(record);
        }else if(type == 'radiogroup'){//单选框
            com = me.createRadio(record);
        }else if(type == 'label'){//纯文本
            com = me.createLabel(record);
        }else if(type == 'image'){//图片
            com = me.createImage(record);
        }else if(type == 'htmleditor'){//超文本
            com = me.createHtmleditor(record);
        }else if(type == 'filefield'){//文件
            com = me.createFilefield(record);
        }else if(type == 'button'){//按钮
            com = me.createButton(record);
        }else if(type == 'dateintervals'){//日期区间旧
            com = me.createDateIntervals(record);
        }else if(type == 'daterange'){//日期区间新
            com = me.createDateRange(record);
        }else if(type == 'datacombobox'){//定值下拉框
            com = me.createDataCombox(record);
        }else if(type == 'datacheckboxgroup'){//定值复选组
            com = me.createDataCheckboxfield(record);
        }else if(type == 'dataradiogroup'){//定值单选组
            com = me.createDataRadio(record);
        }else if(type == 'numbersintervals'){//数字区间
            com = me.createNumbersintervals(record);
        }else if(type == 'timeintervals'){//时间区间
            com = me.createTimeintervals(record);
        }
        
        return com;
    },
    /**
     * 创建时间区间组件
     * @private
     * @param {} record
     * @return {}
     */
    createTimeintervals:function(record){
        var me=this;
        var width1=record.get('Width');
        var height1=record.get('Height');
        var rawOrCol=record.get('RawOrCol');
        if(rawOrCol==''){
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'RawOrCol','hbox');
            rawOrCol='hbox';
        }
        if(rawOrCol=='hbox'&&width1<320)//不分行,横向盒
        {
            width1=320;
            height1=28;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Width',width1);
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height1);
        }
         if(rawOrCol=="vbox"&&height1<56)//分行
        {
            height1=56;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height1);
            
        }
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='h:i';
            me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = //Ext.create('Ext.zhifangux.TimeIntervals', {
            {
            xtype:'timeintervals',
            //itemId:record.get('InteractionField'),
            //自定义属性,每个个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            fieldLabelTwo:record.get('fieldLabelTwo'),
            labelWidth:record.get('LabelWidth'),
            width:width1,
            layoutType:rawOrCol,//控件布局设置,默认为横布局(hbox),竖布局为(vbox)
            labelAlign:'left',
            value:new Date(),
            valueTwo:new Date(),
            height:height1,
            editable:canEdit,
            dateFormat:"'"+showFomart+"'",
            operationcontents:record.get('operationcontents')
        };
        
        return com;
    },
    /**
     * 创建数字区间
     * @private
     * @param {} record
     * @return {}
     */
    createNumbersintervals:function(record){
        var me=this;
        var width1=record.get('Width');
        var height1=record.get('Height');
        var rawOrCol=record.get('RawOrCol');
        if(rawOrCol==''){
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'RawOrCol','hbox');
            rawOrCol='hbox';
        }
        if(rawOrCol=='hbox'&&width1<325)//不分行
        {
            width1=325;
            height1=28;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Width',width1);
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height1);
        }
         if(rawOrCol=="vbox"&&height1<56)//分行
        {
            height1=56;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height1);
        }

        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = {//Ext.create('Ext.zhifangux.NumbersIntervals', {
            xtype:'numbersintervals',
            //自定义属性,每个个控件都有
            isOperation:record.get('isOperation'),
            //itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),//第一个控件的显示名称设置
            fieldLabelTwo:record.get('fieldLabelTwo'),////第二个控件的显示名称设置
            labelWidth:record.get('LabelWidth'),
            width:width1,
            layoutType:rawOrCol,//控件布局设置,默认为横布局(hbox),竖布局为(vbox)
            labelAlign:'left',
            maxValueOne:1000,
		    maxValueTwo:1000,
		    minValueOne:1000,
		    minValueTwo:1000,
//            value:'',
//            valueTwo:'',
            height:height1,
            editable:canEdit,
            operationcontents:record.get('operationcontents')
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
        var height= record.get('Height');
        var width= record.get('Width');
        var columnWidth= record.get('ColumnWidth');
        var columns= record.get('Columns');
        if(height==0||height==''){
            height=44;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height);
        }
        if(width==0||width==''){
            width=560;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Width',width);
        }
        if(columnWidth==0||columnWidth==''){
            columnWidth=120;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'ColumnWidth',columnWidth);
        }
        
        if(columns==0||columns==''){
            columns=5;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Columns',columns);
        }
        var com ={
            xtype: 'checkboxgroup',
            border:false,
            fieldLabel: record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            labelAlign:"left",
            height: height,
            width:width,
            itemId:record.get('InteractionField'),
            name:record.get('InteractionField'),//复选框组名称
            columnWidth :columnWidth,
            columns:columns,
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
        var me=this;
        var height= record.get('Height');
        var width= record.get('Width');
        var columnWidth= record.get('ColumnWidth');
        var columns= record.get('Columns');
        if(height==0||height==''){
            height=44;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height);
        }
        if(width==0||width==''){
            width=560;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Width',width);
        }
        if(columnWidth==0||columnWidth==''){
            columnWidth=120;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'ColumnWidth',columnWidth);
        }
        
        if(columns==0||columns==''){
            columns=5;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Columns',columns);
        }
        var com = {
            xtype:'radiogroup',
            columns:columns,
            vertical: false,
            border:true,
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:width,
            height:height,
            columnWidth :columnWidth,
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
        var com = {
            xtype:'combobox',
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            height:height,
            mode:'local',
            editable:true,
            allowBlank : true,
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
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
            me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = {
            xtype:'dateintervals',
            //自定义属性,每个个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            fieldLabelTwo:record.get('fieldLabelTwo'),
            labelWidth:record.get('LabelWidth'),
            width:width1,
            layoutType:rawOrCol,//控件布局设置,默认为横布局(hbox),竖布局为(vbox)
            labelAlign:'left',
            value:'',
            valueTwo:'',
            height:height1,
            editable:canEdit,
            dateFormat:"'"+showFomart+"'",
            operationcontents:record.get('operationcontents')
        };
        
        return com;
    },
    /**
     * 创建日期区间组件
     * @private
     * @param {} record
     * @return {}
     */
    createDateRange:function(record){
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
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
            me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = {
            xtype:'daterange',
            //自定义属性,每个个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),

            fieldLabelOne:record.get('DisplayName'),//第一个控件的显示名称设置
		    fieldLabelTwo:record.get('fieldLabelTwo'),//第二个控件的显示名称设置
		    //layoutType:'hbox',//控件布局设置
		    labelSeparatorOne:record.get('LabelSeparatorOne'),//分隔符
		    labelSeparatorTwo:record.get('LabelSeparatorTwo'),//分隔符
		    labelWidthTwo:record.get('LabelWidthTwo'),
		    labelWidthOne:record.get('LabelWidth'),
            
            width:width1,
            layoutType:rawOrCol,//控件布局设置,默认为横布局(hbox),竖布局为(vbox)
            labelAlign:'left',
            valueOne:null,
            valueTwo:null,
            height:height1,
            editable:canEdit,
            dateFormat:""+showFomart+"",
            operationcontents:record.get('operationcontents')
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
         
    	 var myStore = Ext.create('Ext.data.Store', {
    		 fields:['value','text'],
             data:[]
    	 });
    	 var opStore = Ext.create('Ext.data.SimpleStore', {
    		 fields:['value','text'],
    		 data :me.OperationType
    	 });
    	 var loStore = Ext.create('Ext.data.SimpleStore', {
    		 fields:['value','text'],
    		 data :me.LogicalType
    	 });
    	var strArr=record.get('InteractionField'.split('_'));
        var isOperation=Ext.Array.contains(strArr,'operation');
        var isLogical=Ext.Array.contains(strArr,'logical');
        var store1=null;
        var url =record.get('ServerUrl').split('?')[0];
    	if(isOperation==true){
        	store1=opStore;
    	}
    	else if(isLogical==true){
    		store1=loStore;
    	}
    	else if(url&&url!=undefined&&url.length>=0){
            var value=record.get("valueField");
            var text=record.get("textField");
            var myUrl = getRootPath() + "/" + url;
            myStore =me.GetComboboxItems(myUrl,value,text);
    		store1=myStore;
    	}else{
            store1=myStore;
        }
        var com = {
            xtype:'combobox',
            //自定义属性,每个个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            mode:'local',
            editable:true,
            allowBlank : true,
            displayField:'text',
            valueField:'value',
            store:store1
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
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'textfield',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
            width:record.get('Width')
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
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'textareafield',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
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
        var minValue=record.get('NumberMin');
        var maxValue=record.get('NumberMax');
        var step=record.get('NumberIncremental');
        
        if(minValue==''||minValue==0||minValue==null)
        {
            maxValue=0;
            me.setColumnParamsRecord(record.get('InteractionField'),'NumberMin',minValue);
        }
        if(maxValue==''||maxValue==0||maxValue==null)
        {
            maxValue=100000000;
            me.setColumnParamsRecord(record.get('InteractionField'),'NumberMax',maxValue);
        }
        if(step==''||step==0||step==null)
        {
            step=1;
            me.setColumnParamsRecord(record.get('InteractionField'),'NumberIncremental',step);
        }
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'numberfield',
            minValue :minValue,
            maxValue :maxValue,
            step : step,
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
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
        var canEdit=record.get('CanEdit');//是否允许编辑
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
	    var com = {
	         xtype:'datefield',
             editable:canEdit,
	         //自定义属性,每个控件都有
	        isOperation:record.get('isOperation'),
	        format:showFomart,
            editable:canEdit,
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
            width:record.get('Width'),
            readOnly:record.get('IsReadOnly'),
	        labFieldAlign:'left'
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
        var me = this;
        var showFomart=record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='H:i';
            me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'timefield',
            editable:canEdit,
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            format:showFomart,
            editable:canEdit,
            name:record.get('InteractionField'),
            increment: 1,//增量
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
            width:record.get('Width'),
            readOnly:record.get('IsReadOnly'),
            selectOnFocus:true,
            labFieldAlign:'left'
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
        var me = this;
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d H:i:s';
            me.setColumnParamsRecord(record.get('InteractionField'),'ShowFomart',showFomart);
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var width=record.get('Width');
        if(width==''||width==0||width==null||width<205)
        {
            width=210;
            me.setColumnParamsRecord(record.get('InteractionField'),'Width',width);
        }
        var height= record.get('Height');
        if(height==0||height==''){
            height=44;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height);
        }
        var com = {
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            xtype:'datetimenew',
            format:showFomart,
            editable:canEdit,
            setType:'datetime',//控件类型:datetime(日期时间),date(日期),time(时间),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
            width:width,
            labelAlign:'left',
            readOnly:record.get('IsReadOnly'),
            //value:new Date(),
            selectOnFocus:true
        };
        return com;
    },
    setSouthRecordByKeyValue:function(InteractionField,key,value){
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
     * 创建复选框组件
     * @private
     * @param {} record
     * @return {}
     */
    createCheckboxfield:function(record){
        var me=this;
        var height= record.get('Height');
        var width= record.get('Width');
        var columnWidth= record.get('ColumnWidth');
        var columns= record.get('Columns');
        if(height==0||height==''){
            height=44;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height);
        }
        if(width==0||width==''){
            width=560;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Width',width);
        }
        if(columnWidth==0||columnWidth==''){
            columnWidth=120;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'ColumnWidth',columnWidth);
        }
        
        if(columns==0||columns==''){
            columns=5;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Columns',columns);
        }
        var com ={
            xtype: 'checkboxgroup',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            border:false,
            fieldLabel: record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            labelAlign:"left",
            height:height,
            width:width,
            name:record.get('InteractionField'),//复选框组名称
            columnWidth :columnWidth,
            columns:columns,
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
        var value=record.get('valueField');
        var text=record.get('textField');
        var myUrl=record.get('ServerUrl');
        var groudName=com.itemId;
        if(myUrl==''||myUrl==null){}
        else{
	        var url=getRootPath() + "/" + myUrl.split("?")[0];
	        if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}
	        else{
		        var defaultValue=record.get('defaultValue');
		        var arrStr=[];
		        //单/复选组没有默认值时的处理
	            defaultValue=null;
	            var data2= me.GetRadiogroupItems(url,groudName,value,text,defaultValue);
                if(data2&&data2!=null){
	               com.items=data2;
                }
	        }
        }
          return com;
    },
    /**
     * 创建单选框组件
     * @private
     * @param {} record
     * @return {}
     */
    createRadio:function(record){
        var me=this;
        var height= record.get('Height');
        if(height==0||height==''){
            height=44;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Height',height);
        }
        var width= record.get('Width');
        if(width==0||width==''){
            width=560;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Width',width);
        }
        var columnWidth= record.get('ColumnWidth');
        if(columnWidth==0||columnWidth==''){
            columnWidth=120;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'ColumnWidth',columnWidth);
        }
        var columns= record.get('Columns');
        if(columns==0||columns==''){
            columns=5;
            me.setSouthRecordByKeyValue(record.get('InteractionField'),'Columns',columns);
        }
        var com = {
            xtype:'radiogroup',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            vertical: false,
            border:true,
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:width,
            height:height,
            columnWidth :columnWidth,
            columns:columns,
            //重置单选框items
            resetItems:function(array){
                this.removeAll();
                this.add(array);
            },
            addItems:function(array){ 
                this.add(array);
            }
        };
        var value=record.get('valueField');
        var text=record.get('textField');
        var myUrl=record.get('ServerUrl');
        var groudName=com.itemId;
        if(myUrl==''||myUrl==null){}
        else{
            var url=getRootPath() + "/" + myUrl.split("?")[0];
            if(value== ''||value==null||value==undefined||text== ''||text==null||text==undefined){}
            else{
                var defaultValue=record.get('defaultValue');
                var arrStr=[];
                //单/复选组没有默认值时的处理
                defaultValue=null;
                var data2= me.GetRadiogroupItems(url,groudName,value,text,defaultValue);
                if(data2&&data2!=null){
                   com.items=data2;
                }
            }
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
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            width:record.get('Width'),
            height:height,
            text:record.get('DisplayName')
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
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'image',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            width:record.get('Width'),
            height:height,
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
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'htmleditor',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
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
        var me=this;
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var com = {
            xtype:'filefield',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),//是否绑定有关系运算组件
            name:record.get('InteractionField'),
            fieldLabel:record.get('DisplayName'),
            labelWidth:record.get('LabelWidth'),
            height:height,
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
        var height=record.get('Height');
        if(height==''||height==0||height==null)
        {
            height=22;
            me.setColumnParamsRecord(record.get('InteractionField'),'Height',height);
        }
        var btnType=record.get('btnType');//按钮特有属性
        if(btnType==''){
            btnType='select';//查询按钮
            me.setColumnParamsRecord(componentItemId,'btnType',btnType);
        }
        var com = {
            xtype:'button',
            //自定义属性,每个控件都有
            isOperation:record.get('isOperation'),
            name:record.get('InteractionField'),
            height:height,
            width:record.get('Width'),
            text:record.get('DisplayName'),
            btnType:btnType//按钮特有属性
        };
        if(btnType=='select'){
            com.iconCls='search-img-16 ';
        }else if(btnType=='reset'){
            com.iconCls='build-button-refresh';
        }else if(btnType=='colse'){
            com.iconCls='imgdiv-close-show-16';
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
        //删除数据项属性列表中的当前数据项数据
        me.removeSouthValueByKeyValue('InteractionField',componentItemId);
        //去掉勾选
        me.uncheckedObjectTreeNode('InteractionField',componentItemId);
        me.switchParamsPanel('center');
    },
    /**
     * 删除面表单中的关系运算组件
     * @private
     * @param {} componentItemId
     */
    removeOperationCom:function(componentItemId,operationItemId){
        var me = this;
        //删除数据项组件
        var center = me.getCenterCom();
        center.remove(operationItemId);

        //删除数据项属性列表中的当前数据项数据
        me.setBasicParamsForInteractionField(componentItemId,'isOperation',false);
        me.setColumnParamsRecord(componentItemId,'isOperation',false);
        me.setColumnParamsRecord('InteractionField',operationItemId);
        //删除数据项属性列表中的当前数据项数据
        me.removeSouthValueByKeyValue('InteractionField',operationItemId);
          var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
          var labelWidth=record.get("LabelWidth");
          var bools=(labelWidth>134);
          if(bools){
            var newLabelWidth=labelWidth-80;
            me.setSouthRecordByKeyValue(componentItemId,"LabelWidth",newLabelWidth);
            me.setchangeComponent(componentItemId,record,null,newLabelWidth,labelWidth,record.get("X"),record.get("Y"));
          }
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
        var store = me.getComponent('south').store;
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

        if(type == 'combobox'){
            otherItems = me.createComboboxfieldItems(componentItemId);
        }else if(type == 'dateintervals'){//日期区间
             otherItems = me.createDateIntervalsfieldItems(componentItemId);
        }else if(type == 'daterange'){//日期区间新
             otherItems = me.createDateRangefieldItems(componentItemId);
        }else if(type == 'textfield'){//文本框
            
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
        }else if(type == 'checkboxgroup'){//复选框
            otherItems = me.createCheckboxfieldItems(componentItemId);
        }else if(type == 'radiogroup'){//单选框
            otherItems = me.createRadioItems(componentItemId);
        }else if(type == 'label'){//纯文本
            //不做处理
        }else if(type == 'image'){//图片
        }else if(type == 'htmleditor'){//超文本
            otherItems = me.createhHmleditorItems(componentItemId);
        }else if(type == 'filefield'){//文件
            otherItems = me.createhFilefieldItems(componentItemId);
        }else if(type == 'button'){//按钮
        	otherItems = me.createbuttonItems(componentItemId);
        }else if(type == 'datacombobox'){//定值下拉框
            otherItems = me.createhDataComboboxItems(componentItemId);
        }else if(type == 'dataradiogroup'){//定值单选组
            otherItems = me.createDataRadiogroupItems(componentItemId);
        }else if(type == 'datacheckboxgroup'){//定值复选组
            otherItems = me.createhDataCheckboxgroupItems(componentItemId);
        }else if(type == 'numbersintervals'){//数字区间
            otherItems = me.createNumbersintervalsItems(componentItemId);
        }else if(type == 'timeintervals'){//时间区间
            otherItems = me.createTimeintervalsItems(componentItemId);
        }
        
        //合并属性(基本属性加上特殊的属性合并渲染)
        var items = basicItems.concat(otherItems);
        com.items = items;
        return com;
    },
    
    /**
     * 基础属性抽离（共有）
     * @private
     * @return {}
     */
    createbase:function(componentItemId){
    	var me=this;
        var com = {
             xtype:'textfield',fieldLabel:'显示名称',name:'name',labelWidth:55,anchor:'100%',
             itemId:'name',
             listeners:{
                 blur:function(com,The,eOpts){
                    var grid = me.getComponent('south');
                    var store = grid.store;
                    var record = store.findRecord('InteractionField',componentItemId);
                    var radioItem=me.getCenterCom().getComponent(componentItemId);
                    var type=record.get('Type') ;
                    if(type== "button"){//存在
                        radioItem.setText(this.value);   
                    }else if(type== "image"){
                         radioItem.setFieldLabel(this.value);
                    }else{
                        radioItem.setFieldLabel(this.value);
                    }
                    me.setColumnParamsRecord(componentItemId,'DisplayName',this.value);
                 }
             }    
        };
        return com;
    },
    /**
     * 基础属性Y轴抽离（共有）
     * @private
     * @return {}
     */
    createbaseY:function(componentItemId){
        var me=this;
        var com = {
                xtype:'numberfield',fieldLabel:'Y轴',name:'Y',labelWidth:60,anchor:'100%',
                itemId:'Y',minValue:0,
                listeners:{
                    blur:function(com,The,eOpts){
                        var y=this.value;
                        me.setColumnParamsRecord(componentItemId,'Y',y);
                        //更新设置展示区域的单选框的Y轴
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var x=radioItem.x;
                        me.setComponentXY(componentItemId,x,y);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        var y=newValue;
                        me.setColumnParamsRecord(componentItemId,'Y',y);
                        //更新设置展示区域的单选框的Y轴
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var x=radioItem.x;
                        me.setComponentXY(componentItemId,x,y);
                    }
                }
            };
        return com;
    },
     /**
     * 基础属性X轴抽离（共有）
     * @private
     * @return {}
     */
    createbaseX:function(componentItemId){
        var me=this;
        var com = {
                xtype:'numberfield',fieldLabel:'X轴',name:'X',labelWidth:60,anchor:'100%',
                itemId:'X',minValue:0,
                listeners:{
                    blur:function(com,The,eOpts){
                        //更新设置展示区域的单选框的X轴                        
                        var x=this.value;                        
                        me.setColumnParamsRecord(componentItemId,'X',x);
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var y=radioItem.y;                        
                        me.setComponentXY(componentItemId,x,y);
                    },change:function(com,  newValue,  oldValue,  eOpts ){
                        var x=newValue;
                        me.setColumnParamsRecord(componentItemId,'X',x);
                        //更新设置展示区域的单选框的X轴
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        var y=radioItem.y;
                        me.setComponentXY(componentItemId,x,y); 
                    }
                }
            };
        return com;
    },
    createbaseziti:function(componentItemId){
    	var me=this;
        var com = {
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
        };
        return com;
    },
    createoperation:function(componentItemId){
    	var me=this;
        var com = {
    		xtype:'checkbox',fieldLabel:'是否是运算关系',name:'isOperation',labelWidth:90,anchor:'100%',
            itemId:'isOperation',labelAlign:'right',
            listeners:{
        	    change:function(com,newValue,oldValue,eOpts){
                       
                       me.operationid=componentItemId+'_operation';
                       var operationId=componentItemId+'_operation';
                       var store = me.getComponent('south').store;
                       var index = store.findExact('InteractionField',operationId);
                       var record=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
                       
                       if(index===-1&&newValue==true){
                          me.setColumnParamsRecord(componentItemId,"isOperation",newValue);
                          var labelWidth=record.get("LabelWidth");
                          var bools=(labelWidth<56);
                          if(bools){
                            var newLabelWidth=labelWidth+80;
                            me.setColumnParamsRecord(componentItemId,"LabelWidth",newLabelWidth);
                            me.setchangeComponent(componentItemId,record,null,newLabelWidth,labelWidth,record.get("X"),record.get("Y"));
                          }
                          var x=record.get("X")+51;
                          var y=record.get("Y");
                          store.add(me.operationCombobox(componentItemId,x,y));
                          var newRecord=me.getSouthRecordByKeyValue('InteractionField',operationId);
                          //添加组件属性面板
                          me.addParamsPanel(newRecord.get('Type'),newRecord.get('InteractionField'),newRecord.get('DisplayName'));
                          me.setAllComXY(newRecord);
                       } else if(index>0&&newValue==true){
                          me.setColumnParamsRecord(componentItemId,"isOperation",newValue);
                       }
                       else if(newValue==false){
                         var part="operation";
                         var itemId=componentItemId+"_"+part;
                         me.removeOperationCom(componentItemId,itemId);
                       }
                }
            }  
        };
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
      
        var  base=me.createbase(componentItemId);
        var  baseX=me.createbaseX(componentItemId);
        var  baseY=me.createbaseY(componentItemId);
        var ziti= me.createbaseziti(componentItemId);
        //是否是运算关系
        var operation= me.createoperation(componentItemId);
        var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
        var item=null;
        if(record.get('Type')=='button'){
        	item=[base,baseX,baseY,ziti];
        //如果是运算关系符下拉列表组件
        }else if(arr[arr.length-1]=='operation'){
           item=[base,baseX,baseY];
        }
        else{
        	item=[base,baseX,baseY,ziti,operation];
        }
        var items = [{
            xtype:'fieldset',title:'组件基础属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'basicParams',
            items:item
        }
        ];
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
                itemId:'ColumnWidth',maxValue:200,minValue:0,value:100,
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
                maxValue:10,minValue:0,value:2,
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
                            var combodata=me.transformItems(this.value,componentItemId,null);
                            if(combodata&&combodata.length>0){
                                me.setColumnParamsRecord(componentItemId,'combodata',combodata);
                                
                                if(this.value.length>2&&this.value!="[]"){
                                    var com=me.createDataDefaultValue(componentItemId,this.value);
                                    var otherParams=me.getOtherParams(componentItemId);
                                    var oldCom=otherParams.getComponent("defaultValue");
                                    if(oldCom){
                                        otherParams.remove(oldCom);
                                    }
                                    if(com){
                                        otherParams.add(com);
                                    }
                                }
                                
                            }else{
                                me.setColumnParamsRecord(componentItemId,'combodata','');
                            }
                        }
                    }
                }
            }]
        }];
        return items;
    },
    getOtherParams:function(componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        return other;
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
                xtype:'numberfield',fieldLabel:'列宽',name:'ColumnWidth',labelWidth:60,anchor:'100%',
                itemId:'ColumnWidth', maxValue:200,minValue:0,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ColumnWidth',this.value);
                        //删除单选组、然后重新添加单选项
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record);   
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:60,anchor:'100%',maxValue:20,minValue:0,
                itemId:'Columns',
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
                xtype:'textarea',anchor:'100%',height:80,
                itemId:'datacomboValue',name:'datacomboValue',
                listeners:{
                    blur:function(com,The,eOpts){
                        if(this.value!=null&&this.value!=''){
                            var combodata=me.transformItems(this.value,componentItemId,null);
                            if(combodata&&combodata.length>0){
                                me.setColumnParamsRecord(componentItemId,'combodata',combodata);
                            }else{
                                me.setColumnParamsRecord(componentItemId,'combodata','');
                            }
                        }
                    }
                }
            }]
        }];
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
                        if(this.value.length>2&&this.value!="[]"){
                            var com=me.createDataDefaultValue(componentItemId,this.value);
                            var otherParams=me.getOtherParams(componentItemId);
                            var oldCom=otherParams.getComponent("defaultValue");
                            if(oldCom){
                                otherParams.remove(oldCom);
                            }
                            if(com){
                                otherParams.add(com);
                            }
                        }
                    }
                }
            }
            ]
        }];
        return items;
    },
    /**
     * 定值单选组属性面板特有数据赋值
     * @private 
     * @param {} componentItemId
     * @param {} record
     */
    setDataradiogroupParamsPanelValue:function(componentItemId,record){
        var me = this;
        var other = me.getOtherParams(componentItemId);
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
        
        if(datacomboValue.getValue().length>2&&datacomboValue.getValue()!="[]"){
            var com=me.createDataDefaultValue(componentItemId,datacomboValue.getValue());
            var otherParams=me.getOtherParams(componentItemId);
            var oldCom=otherParams.getComponent("defaultValue");
            if(oldCom){
                otherParams.remove(oldCom);
            }
             if(com){
                otherParams.add(com);
            }
        }
        
        var columns = other.getComponent('Columns');
        var columnWidth = other.getComponent('ColumnWidth');
        
        var columnsValue = record.get('Columns');
        var columnWidthValue = record.get('ColumnWidth');
        columns.setValue(columnsValue);
        columnWidth.setValue(columnWidthValue);
    },
    /**
     * 创建定值下拉框/单选组特有属性:默认选中组件
     * @private
     * @return {}
     */
    createDataDefaultValue:function(componentItemId,arrTemp2){
       var me=this;
       var com={
                xtype:'combobox',fieldLabel:'默认选中',//下拉框
                itemId:'defaultValue',name:'defaultValue',
                labelWidth:60,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                displayField:'text',
                valueField:'value',
                store:new Ext.data.SimpleStore({ 
                fields:['value','text'], 
                data:arrTemp2 ? eval(arrTemp2) : []
            }),
                listeners:{
                    select:function(combo,records,eOpts){
                        var newValue=combo.getValue();
                        var tempitem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=tempitem.getItemId();
                        var newValue=combo.getValue();
                        if(newValue && newValue != ""){
                            var record=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
                            if(record.get('Type')=='datacombobox'){
                                //给组件的默认值赋值
                                var tempValue=combo.getValue();
                                var arr="'"+tempValue+"'";
                                var values="{"+groupName+":["+arr+"]}";
                                var arrJson=Ext.decode(values);
                                tempitem.setValue(tempValue);
                            }   
                        }
                    },change:function(combo, newValue,oldValue,eOpts){
                        var tempitem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=tempitem.getItemId();
                        var newValue=combo.getValue();
                        if(newValue && newValue != ""){
                            me.setColumnParamsRecord(componentItemId,'defaultValue',newValue);
                            var record=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
                            if(record.get('Type')=='dataradiogroup'){
                                var combodata=me.updateTransformItems(record.get('combodata'),componentItemId,newValue);
                                me.setColumnParamsRecord(componentItemId,'combodata',combodata);
                                var newRecord=me.getSouthRecordByKeyValue('InteractionField',componentItemId);
                                me.setchangeComponent(componentItemId,newRecord,null,0,0,0,0);
                            }
                        }
                    }
                }
            
            };
            return com;
    }, 
    
    /**
     * 数字区间特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createNumbersintervalsItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'数字区间特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[
            {
                xtype:'textfield',fieldLabel:'名称二',name:'fieldLabelTwo',labelWidth:55,anchor:'100%',
                itemId:'fieldLabelTwo',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'fieldLabelTwo',this.value);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            },
             {
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:55,anchor:'100%',
                itemId:'ShowFomart',hidden:true,
                listeners:{
                    blur:function(com,The,eOpts){
                       // me.setColumnParamsRecord(componentItemId,'ShowFomart',this.value);
                    }
                }
            },{
                xtype:'checkboxfield',boxLabel:'是否允许手输',name:'CanEdit',anchor:'100%',
                itemId:'CanEdit',hidden:true,
                listeners:{
                    blur:function(com,The,eOpts){
                        //me.setColumnParamsRecord(componentItemId,'CanEdit',this.value);
                    }
                }
            },{
                xtype:'radiogroup',fieldLabel:'行列方式',itemId:'RawOrCol',name:'RawOrCol',
                labelWidth:55,columns:3,vertical:true,
                items:[
                    {boxLabel:'行',name:'RawOrCol',inputValue:'hbox'},
                    {boxLabel:'列',name:'RawOrCol',inputValue:'vbox'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'RawOrCol',newValue.RawOrCol);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 时间区间特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createTimeintervalsItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'时间区间特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[
            {
                xtype:'textfield',fieldLabel:'名称二',name:'fieldLabelTwo',labelWidth:55,anchor:'100%',
                itemId:'fieldLabelTwo',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'fieldLabelTwo',this.value);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            },
             {
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:55,anchor:'100%',
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
                labelWidth:55,columns:3,vertical:true,
                items:[
                    {boxLabel:'行',name:'RawOrCol',inputValue:'hbox',checked:true},
                    {boxLabel:'列',name:'RawOrCol',inputValue:'vbox'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'RawOrCol',newValue.RawOrCol);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            }]
        }];
        return items;
    },
    /**
     * 日期区间新特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createDateRangefieldItems:function(componentItemId){
        var me = this;
        
        var items = [{
            xtype:'fieldset',title:'日期区间特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[
            {
                xtype:'textfield',fieldLabel:'显示名称二',name:'fieldLabelTwo',labelWidth:85,anchor:'100%',
                itemId:'fieldLabelTwo',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'fieldLabelTwo',this.value);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            },
            {
                xtype:'textfield',fieldLabel:'名称一分隔符',name:'LabelSeparatorOne',labelWidth:85,anchor:'100%',
                itemId:'LabelSeparatorOne',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'LabelSeparatorOne',this.value);
                        //var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        //me.setchangeComponent(componentItemId,record,record.get('LabelSeparatorTwo'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            },
            {
                xtype:'textfield',fieldLabel:'名称二分隔符',name:'LabelSeparatorTwo',labelWidth:85,anchor:'100%',
                itemId:'LabelSeparatorTwo',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'LabelSeparatorTwo',this.value);
                        //var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        //me.setchangeComponent(componentItemId,record,record.get('LabelSeparatorTwo'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'名称一宽度',name:'LabelWidth',labelWidth:85,anchor:'100%',
                itemId:'LabelWidth',maxValue:200,minValue:0,
                //value:100,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'LabelWidth',this.value);
                        //更新设置展示区域的单选框的列宽
                        //var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        //me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'名称二宽度',name:'LabelWidthTwo',labelWidth:85,anchor:'100%',
                itemId:'LabelWidthTwo',maxValue:200,minValue:0,
                //value:100,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'LabelWidthTwo',this.value);
                        //更新设置展示区域的单选框的列宽
                        //var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        //me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                    }
                }
            },
             {
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:85,anchor:'100%',
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
                labelWidth:55,columns:3,vertical:true,
                items:[
                    {boxLabel:'行',name:'RawOrCol',inputValue:'hbox',checked:true},
                    {boxLabel:'列',name:'RawOrCol',inputValue:'vbox'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'RawOrCol',newValue.RawOrCol);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
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
        
        var items = [{
            xtype:'fieldset',title:'日期区间特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[
            {
                xtype:'textfield',fieldLabel:'名称二',name:'fieldLabelTwo',labelWidth:55,anchor:'100%',
                itemId:'fieldLabelTwo',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'fieldLabelTwo',this.value);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            },
             {
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:55,anchor:'100%',
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
                labelWidth:55,columns:3,vertical:true,
                items:[
                    {boxLabel:'行',name:'RawOrCol',inputValue:'hbox',checked:true},
                    {boxLabel:'列',name:'RawOrCol',inputValue:'vbox'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'RawOrCol',newValue.RawOrCol);
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,record.get('LabFont'),0,0,record.get('X'),record.get('Y'));
                    }
                }
            }]
        }];
        return items;
    },
    
    /**
     * 
     *按钮特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createbuttonItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'按钮特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:true,
            itemId:'otherParams',
            items:[{
                xtype:'textfield',fieldLabel:'内部编码',name:'InteractionField',labelWidth:55,anchor:'100%',
                itemId:'InteractionField',hidden:true,
                listeners:{
                    blur:function(com,The,eOpts){
                        var grid = me.getComponent('south');
                        var store = grid.store;
                        var values=this.value;
                        var record = store.findRecord('InteractionField',values);

                        if(record != null){//存在
                            Ext.Msg.alert('提示','该交互字段已经存在,请重新命名交互字段名称！');
                        }else{
                            if(values==null||values==""){
                            Ext.Msg.alert('提示','交互字段不能为空！');
                            return ;
                            }else{
                            me.setColumnParamsRecord(componentItemId,'InteractionField',values);
                            me.browse();
                            }
                        }
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'按钮类型',hidden:true,
                labelWidth:55,value:'',mode:'local',editable:false,
                displayField:'text',valueField:'value',
                //value:'select',
                itemId:'btnType',name:'btnType',
                store:new Ext.data.SimpleStore({ 
                    fields:['value','text'], 
                    data:me.btnTypeList
                }),
                listeners:{ 
                    select:function(combo, records, eOpts ){
                        var newValue=this.getValue( );
                        me.setColumnParamsRecord(componentItemId,'btnType',newValue);
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
            defaultType:'textfield',name:'otherParams',
            itemId:'otherParams',
            items:[{
                xtype:'combobox',fieldLabel:'值字段',
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
                            	var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
            		            if(record.get('isOperation')==true){
            		            	return;
            		          	}
            		          	else if(record.get('logical')==true){
            		          		return;
            		          	}else{
            		          		 var east = me.getComponent('east');
                                     var panel = east.getComponent(panelItemId);
                                     var serverUrl = panel.getComponent('otherParams').getComponent('serverUrl');
                                     var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                                     var myUrl='';
                                     if(records.length>1){
                                        myUrl=records[1].get(me.objectServerValueField);
                                     }else{
                                        myUrl=records[0].get(me.objectServerValueField);
                                     }
                                     
                                     serverUrl.setValue(myUrl);
                                     defaultValue.store.proxy.url = getRootPath() + "/" + myUrl.split("?")[0] + "?isPlanish=true&where=";
                                     defaultValue.store.load();
                                     
            		          	}

                            }
                        }
                    }
                }),
                listeners:{
                    focus:function(owner,The,eOpts){
                     //给组件的服务地址赋值
                        var serverUrl =owner.getValue().split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                    },
                    select:function(combo,records,eOpts ){
                         //给组件的服务地址赋值
                        var serverUrl =combo.getValue().split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                        //给组件的服务地址赋值
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                        defaultValue.store.proxy.url = getRootPath() + "/" +serverUrl+ "?isPlanish=true&where=";
                        defaultValue.store.load();
                        //改变下拉框初始值
                        me.changeComboParamsPanelDefaultValue(componentItemId);
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
                            data.ResultDataValue = ResultDataValue.list;
                            for(var i in data.ResultDataValue){
                                data.ResultDataValue[i].value = data.ResultDataValue[i][value];
                                data.ResultDataValue[i].text = data.ResultDataValue[i][text];
                            }
                            
                            //下拉框组件data赋值
                            var com = me.getCenterCom().getComponent(componentItemId);
                            if(com&&com!=undefined){
                                com.store.loadData(data.ResultDataValue);
                                //返回处理后的数据
                                response.responseText = Ext.JSON.encode(data);
                            
                            }
                            return response;
                            
                        }
                    }
                }),
                listeners:{
                    select:function(combo,records,eOpts ){
                        var tempitem=me.getCenterCom().getComponent(componentItemId);
                        var groupName=tempitem.getItemId();
                        var newValue=combo.getValue();
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
            }
            ]
        }];
        return items;
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
            xtype:'fieldset',title:'复选组特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'高度',name:'Height',labelWidth:55,anchor:'100%',
                itemId:'Height',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Height',this.value);
                        //更新设置展示区域的复选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(undefined, this.value);
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'宽度',name:'Width',labelWidth:55,anchor:'100%',
                itemId:'Width',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Width',this.value);
                        //更新设置展示区域的复选框的宽度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(this.value);
                    }
                }
            },
              {
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
                maxValue:10,minValue:1,
                itemId:'Columns',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Columns',this.value);
                        //更新设置展示区域的单选框的列宽
                        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
                        me.setchangeComponent(componentItemId,record,null,0,0,0,0);
                    }
                }
            },
              {
                xtype:'combobox',fieldLabel:'值字段',
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
                        }
                    }
                }),
                listeners:{
                    select:function(combo,records,eOpts ){
                        var serverUrl =combo.getValue().split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                        var url = getRootPath() + "/" + combo.getValue().split("?")[0];
                        
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
     * 单选框特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createRadioItems:function(componentItemId){
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
            itemId:'otherParams',
            items:[{
                xtype:'numberfield',fieldLabel:'高度',name:'Height',labelWidth:55,anchor:'100%',
                itemId:'Height',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Height',this.value);
                        //更新设置展示区域的单选框的高度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(undefined,this.value);
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'宽度',name:'Width',labelWidth:55,anchor:'100%',
                itemId:'Width',minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Width',this.value);
                        //更新设置展示区域的单选框的宽度
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.setSize(this.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'列宽',name:'ColumnWidth',labelWidth:55,anchor:'100%',
                itemId:'ColumnWidth',maxValue:200,minValue:1,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ColumnWidth',this.value);
                        //更新设置展示区域的单选框的列宽
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.columnWidth=this.value;
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'列数',name:'Columns',labelWidth:55,anchor:'100%',
                maxValue:20,minValue:1,
                itemId:'Columns',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Columns',this.value);
                        //更新设置展示区域的单选框的列宽
                        var radioItem=me.getCenterCom().getComponent(componentItemId);
                        radioItem.columns=this.value;
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'值字段',
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
                    listeners:{}
                }),
                listeners:{
                    select:function(combo,records,eOpts ){
                        var serverUrl=combo.getValue().split("?")[0];
                        me.setColumnParamsRecord(componentItemId,'ServerUrl',serverUrl);
                        var east = me.getComponent('east');
                        var panel = east.getComponent(panelItemId);
                        var defaultValue = panel.getComponent('otherParams').getComponent('defaultValue');
                        var url = getRootPath() + "/" + combo.getValue().split("?")[0];
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
    createTextareafieldItems:function(componentItemId){
        var me = this;
        var items = [];
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
                    },
                    select:function(combo,records,eOpts ){
                       var value=combo.getValue();
                       me.setColumnParamsRecord(componentItemId,'NumberMin',value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'最大值',name:'NumberMax',labelWidth:55,anchor:'100%',
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
                xtype:'numberfield',fieldLabel:'增量',name:'NumberIncremental',labelWidth:55,anchor:'100%',
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
            }]
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
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:55,anchor:'100%',
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
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:55,anchor:'100%',
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
                xtype:'textfield',fieldLabel:'显示格式',name:'ShowFomart',labelWidth:55,anchor:'100%',
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
     * 图片特有属性---不用
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
     * 文件特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createhFilefieldItems:function(componentItemId){
        var me = this;
        var store = me.getComponent('south').store;
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
                xtype:'textfield',fieldLabel:'按钮文字',name:'SelectFileText',labelWidth:55,anchor:'100%',
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
    updateTransformItems:function(arrTemp,groupName,defaultValue){
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
            var value=arrStr[i]['inputValue'];
            var text=arrStr[i]['boxLabel'];
            var checkedStr="";
            if(defaultValue&&defaultValue!=null){
                if(defaultValue==value){
                    checkedStr="checked:true,";
                }else{
                    checkedStr="checked:false,";
                }
            }else{
                    checkedStr="checked:false,";
                }
            tempItem=
                "{"+
               checkedStr+
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
     * 转换定值单/复选框组的子项数据
     * @return {}
     */
    transformItems:function(arrTemp,groupName,defaultValue){
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
            var checkedStr="";
            if(defaultValue&&defaultValue!=null){
                if(defaultValue==value){
                    checkedStr="checked:true,";
                }else{
                    checkedStr="checked:false,";
                }
            }else{
                    checkedStr="checked:false,";
                }
            tempItem=
                "{"+
               checkedStr+
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
    GetRadiogroupItems:function(url,groupName,valueField,displayField,defaultValue2){
        var me = this;
        var myUrl='';
        if(url==""||url==null){
            Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
            return null;
        }else{
            myUrl=url+("?isPlanish=true&start=0&limit=10000&fields="+valueField+","+displayField);
        }
        var mychecked=false;var arrStr=[];
        arrStr=defaultValue2;
        var localData=[];
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                
                var ResultDataValue = {count:0,list:[]};
                if(result['ResultDataValue'] && result["ResultDataValue"] != ""){
                    ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
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
        var myUrl='';
        if(url==""||url==null){
            Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
            return null;
        }else{
            myUrl=url+("?isPlanish=true&start=0&limit=10000&fields="+value2+","+text2);
        }
        var localData=[];
        var myGridStore=null;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var ResultDataValue = {count:0,list:[]};
                var arrLists=[];
                var tempValue=result["ResultDataValue"];
                if(tempValue && tempValue!= ""){
                    ResultDataValue = Ext.decode(tempValue);
                    arrLists=ResultDataValue .list;
                }
               var count = ResultDataValue['count'];
               for (var i = 0; i <count; i++) { 
	                var value=arrLists[i][value2];
	                var text=arrLists[i][text2];
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
        var store = me.getComponent('south').store;
        var record = store.findRecord('InteractionField',InteractionField);
        if(record != null){//存在
            record.set(key,value);
            record.commit();
            store.sync();//与后台数据同步
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
        if(com===undefined){
        
        }else{
            com.setValue(newValue);
        }
    }, 
     /**
     * 当行记录信息的某一列的值改变后,属性面板的组件特有属性相应的值更新
     * @param {} componentItemId:交互字段,某一控件的itemId
     * @param {} newValue:修改的值
     */
    setOtherParamsForInteractionField:function(InteractionField,itemId,newValue){
        var me=this;
        var record = me.getSouthRecordByKeyValue('InteractionField',InteractionField);
        //属性面板ItemId
        var panelItemId = InteractionField + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var others=panel.getComponent("otherParams");
        var com=others.getComponent(itemId);
        if(com===undefined){
        }else{
            com.setValue(newValue);
        }
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
            var InteractionField = record.get('InteractionField');
            
            me.setBasicParamsForInteractionField(InteractionField,'name',record.get('DisplayName'));
            //属性面板ItemId
            var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
            //组件属性面板
            var panel = me.getComponent('east').getComponent(panelItemId);
            var others=panel.getComponent("otherParams");
            var basic=panel.getComponent("basicParams");
            //给按钮特有属性设置默认值
            if(record.get('Type')=='button'){
                var interactionField=others.getComponent("InteractionField");
                interactionField.setValue(record.get('InteractionField'));
            }

            if(record.get('Type')!='button'){
	            me.setBasicParamsForInteractionField(InteractionField,'isOperation',record.get('isOperation'));
            }
            //属性面板特有数据赋值
            me.setParamsPanelValuesByType(componentItemId,record);
        }
    },
    /***
     * 给下拉列表特有属性设置默认值
     * @param {} record
     * @param {} InteractionField
     */
    setComboboxValue:function(record,InteractionField){
        var me=this;
        var serverUrl=record.get("ServerUrl");
        var valueField=record.get("valueField");
        var displayField=record.get("textField");
        var value=record.get("valueField");
        var text=record.get("textField");
        var objectTemp="";
        var arr=InteractionField.split("_");
        var count=0;
        if(arr.length>2){
            count=arr.length-2;
        }
        else if(arr.length==2){
            count=0;
        }else{
            count=-1;
        }
        
        for(var i=0;i<=count;i++){
            objectTemp=(""+objectTemp+(arr[i]+"_"));
        }
        
        if(valueField.length>0){
            var value2=value.split("_");
            valueField=(objectTemp+value2[value2.length-1]);
        }
        if(displayField.length>0){
            var text2=text.split("_");
            displayField=objectTemp+text2[text2.length-1];
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
    /***
     * 按钮特有属性赋值
     * @param {} record
     * @param {} InteractionField
     */
    setButtonParamsPanelValue:function(record,InteractionField){
        var me=this;
        var interactionField  =record.get("InteractionField");
        var btnType=record.get("btnType");
        me.setOtherParamsForInteractionField(InteractionField,'InteractionField',interactionField);
        me.setOtherParamsForInteractionField(InteractionField,'btnType',btnType);
    },
    /***
     * 给单选组,复选组特有属性设置默认值
     * @param {} record
     * @param {} InteractionField
     */
    setCheckboxgroupValue:function(record,InteractionField){
        var me=this;
        var serverUrl=record.get("ServerUrl");
        var valueField=record.get("valueField");
        var displayField=record.get("textField");
        var value=record.get("valueField");
        var text=record.get("textField");
        var objectTemp="";
        var arr=InteractionField.split("_");
        
        var height= record.get('Height');
        var width= record.get('Width');
        var columnWidth= record.get('ColumnWidth');
        var columns= record.get('Columns');
        
        me.setOtherParamsForInteractionField(InteractionField,'Height',height);
        me.setOtherParamsForInteractionField(InteractionField,'Width',width);
        me.setOtherParamsForInteractionField(InteractionField,'ColumnWidth',columnWidth);
        me.setOtherParamsForInteractionField(InteractionField,'Columns',columns);
        
        var count=0;
        if(arr.length>2){
            count=arr.length-2;
        }
        else if(arr.length==2){
            count=0;
        }else{
            count=-1;
        }
        
        for(var i=0;i<=count;i++){
            objectTemp=(""+objectTemp+(arr[i]+"_"));
        }
        
        if(valueField.length>0){
            var value2=value.split("_");
            valueField=(objectTemp+value2[value2.length-1]);
        }
        if(displayField.length>0){
            var text2=text.split("_");
            displayField=objectTemp+text2[text2.length-1];
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
        }
         //给定值单选组特有属性设置默认值
        if(type == "dataradiogroup"){
            me.setDataradiogroupParamsPanelValue(componentItemId,record);
        }
        //给定值复选组特有属性设置默认值
        if(type == "datacheckboxgroup"){
            me.setDataCheckboxParamsPanelValue(componentItemId,record);
        }
        //数字框特有属性设值
        if(type == "numberfield"){
            me.setNumberParamsPanelValue(record,componentItemId);
        }
        //给下拉列表特有属性设置默认值
        if(type=='combobox'){
            me.setComboboxValue(record,componentItemId);
        }
        //给单选组,复选组特有属性设置默认值
        if(type=='radiogroup'||type=='checkboxgroup'){
            me.setCheckboxgroupValue(record,componentItemId);
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
        else if(type == "dateintervals"){
            me.setDateIntervalsParamsPanelValue(record,componentItemId);
        }
        //日期区间新特有属性设值
        else if(type == "daterange"){
            me.setDateRangeParamsPanelValue(record,componentItemId);
        }
        
        //时间区间特有属性设值
        else if(type == "timeintervals"){
            me.setTimeintervalsParamsPanelValue(record,componentItemId);
        }//数字区间特有属性设值
        else if(type == "numbersintervals"){
            me.setNumbersintervalsParamsPanelValue(record,componentItemId);
        }
        //按钮特有属性设值
        else if(type == "button"){
            me.setButtonParamsPanelValue(record,componentItemId);
        }
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
            me.setColumnParamsRecord(componentItemId,'ShowFomart',showFomart);
        }
        
        
        showFomartCom.setValue(showFomart);
        canEditCom.setValue(canEdit);
        
    },
    /**
     * 日期区间特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setDateRangeParamsPanelValue:function(record,componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        
        var labelWidthCom = other.getComponent('LabelWidth');
        var fieldLabelTwoCom = other.getComponent('fieldLabelTwo');
        var showFomartCom = other.getComponent('ShowFomart');
        var canEditCom = other.getComponent('CanEdit');
        var rawOrColCom = other.getComponent('RawOrCol');
        
        var labelSeparatorOneCom = other.getComponent('LabelSeparatorOne');
        var labelSeparatorTwoCom = other.getComponent('LabelSeparatorTwo');
        
        var labelWidthTwoCom = other.getComponent('LabelWidthTwo');
        
        var showFomart = record.get('ShowFomart');
        var canEdit = record.get('CanEdit');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d';
            me.setColumnParamsRecord(componentItemId,'ShowFomart',showFomart);
        }
        fieldLabelTwoCom.setValue(record.get('fieldLabelTwo'));
        showFomartCom.setValue(showFomart);
        canEditCom.setValue(canEdit);
        
        labelWidthCom.setValue(record.get('LabelWidth'));
        labelSeparatorOneCom.setValue(record.get('LabelSeparatorOne'));
        labelSeparatorTwoCom.setValue(record.get('LabelSeparatorTwo'));
        labelWidthTwoCom.setValue(record.get('LabelWidthTwo'));
        
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
            me.setColumnParamsRecord(componentItemId,'ShowFomart',showFomart);
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
     * 数字区间特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setNumbersintervalsParamsPanelValue:function(record,componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var other = panel.getComponent("otherParams");
        var fieldLabelTwoCom = other.getComponent('fieldLabelTwo');
        var rawOrColCom = other.getComponent('RawOrCol');
        var fieldLabelTwo = record.get('fieldLabelTwo');
        fieldLabelTwoCom.setValue(fieldLabelTwo);

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
     * 时间区间特有属性设值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setTimeintervalsParamsPanelValue:function(record,componentItemId){
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
            showFomart='h:i';
            me.setColumnParamsRecord(componentItemId,'ShowFomart',showFomart);
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
        if(showFomart==''||showFomart==""||showFomart==null){
            var type = record.get('Type');
            if(type == "timefield"){
                showFomart='H:i';
            }else if(type == "datetimenew"){
                showFomart='Y-m-d H:i:s';
            }else if(type == "datefield"){
                showFomart='Y-m-d';
            }
            me.setColumnParamsRecord(componentItemId,'ShowFomart',showFomart);
        }
        var canEdit = record.get('CanEdit');
        
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
    /**
     * 定值下拉框属性面板特有数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setDataComboParamsPanelValue:function(componentItemId,record){
        var me = this;
        var other = me.getOtherParams(componentItemId)
        var datacomboValue = other.getComponent('datacomboValue');
        var value = record.get('combodata');
        if(!value || value == ""){
            value = "[]";
        }
        datacomboValue.setValue(value);
        
        if(datacomboValue.getValue().length>2&&datacomboValue.getValue()!="[]"){
            var com=me.createDataDefaultValue(componentItemId,datacomboValue.getValue());
            var otherParams=me.getOtherParams(componentItemId);
            var oldCom=otherParams.getComponent("defaultValue");
            if(oldCom){
                otherParams.remove(oldCom);
            }
             if(com){
                otherParams.add(com);
            }
        }
        
    },
   /**
     * 定值复选组属性面板特有数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setDataCheckboxParamsPanelValue:function(componentItemId,record){
        var me = this;
        var other = me.getOtherParams(componentItemId);
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
            //组件拖动监听
            move:function(com,x,y,eOpts){
                me.setColumnParamsRecord(com.itemId,'X',x);
                me.setColumnParamsRecord(com.itemId,'Y',y);
                var record=me.getSouthRecordByKeyValue('InteractionField',com.itemId);
                me.setBasicParamsPanelValuesForXY(com.itemId,record);
            },
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
                    var type=com.xtype;
                    if(type='button'){
                        var btnType=com.btnType;//按钮特有属性
                        if(btnType=="select"){//查询按钮
                            var lastValue=me.createSubmitSelect();
                            alert(lastValue);
		                }else if(btnType=="reset"){//重置按钮
                            //alert('重置按钮');
		                }else if(btnType=="close"){//关闭按钮
                            //alert('关闭按钮');
		                }
                    }
                }
            },
            contextmenu:{
                element:'el',
                fn:function(e,t,eOpts){
                    //禁用器的右键相应事件 
                    e.preventDefault();e.stopEvent();
                    //右键菜单
                    new Ext.menu.Menu({
                        items:[{
                            text:"删除",iconCls:'delete',
                            handler:function(){
                                //删除表单中的关系运算组件
                                if(com.itemId.indexOf("operation") != -1){
			                         var oldItemId=com.itemId;
                                     oldItemId=oldItemId.substring(0,oldItemId.length-10);//去掉"_operation"
			                         me.removeOperationCom(oldItemId,com.itemId);
                                }else{
                                    if(com.isOperation==true){//删除表单中的组件中存在关系运算组件
                                         var componentItemId=com.itemId;
	                                     var operationItemId=com.itemId+"_operation";
	                                     me.removeOperationCom(componentItemId,operationItemId);
                                    }
                                    me.removeComponent(com.itemId);
                                } 
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
     * longfc10
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
            {name:'LabelWidthTwo',type:'int'},//日期区间第二个控件labelj显示宽度
            {name:'LabelSeparatorOne',type:'string'},//日期区间第二个控件显示分隔符
            {name:'LabelSeparatorTwo',type:'string'},//日期区间第二个控件显示分隔符
            //{name:'LabelSeparatorTwo',type:'string'},//日期区间第二个控件显示分隔符
            {name:'isOperation',type:'bool'},//运算关系
            {name:'operationcontents',type:'string'},//运算符号
            {name:'combodata',type:'string'},//定值下拉框的内容
            {name:'sortNum',type:'int'},//光标顺序号
            {name:'btnType',type:'string'},//按钮类型
            {name:'BoundField',type:'string'}//在生成的逻辑关系下拉框,运算关系下拉框里,添加自定义属性
            
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
                    if(me.appId != -1 && me.isJustOpen && node == objectPropertyTree.getRootNode()){
                        //对象内容勾选
                        me.changeObjChecked(southParams);
                    }
                }
            });
            
            //赋值
            me.setSouthRecordByArray(southParams);//数据项列表赋值
            me.setObjData();//数据对象赋值
            me.setPanelParams(panelParams);//属性面板赋值
            
            //获取获取数据服务列表
            var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
            getDataServerUrl.value = panelParams.getDataServerUrl;
            //渲染效果
            setTimeout(function(southParams){me.browse();});
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
        var loadData = me.createLoadDataStr();
        //保存表单数据方法:依据构建表单类型生成表单的"保存/查询"按钮处理方法
        var getValue ="";
        //如构建表单类型为"一般查询",需要把输入项的信息处理拼装成SQL查询条件串
            getValue = me.createSubmitSelectStr();
        var isbtnSelectType=false;//是否存在查询按钮
        var isbtnResetType=false;//是否存在重置按钮
        var isbtnCloseType=false;//是否存在关闭按钮
        
        var logicalType=params.logicalType;
        //--------------尽量提取出来，不要混在一起，不利于维护--------------

        var GetComboboxItems = 
        "function(url2,valueField,displayField,groupName,defaultValue){"+
        	"var myUrl=url2;"+
        	"if(myUrl==''||myUrl==null){" + 
            	"Ext.Msg.alert('提示',myUrl);return null;" + 
            "}" + 
            "else{" + 
                "myUrl=getRootPath()+'/'+myUrl+'?isPlanish=true&page=1&start=0&limit=10000&fields='+valueField+','+displayField;" + 
            "}" + 
            "var localData=[];" + 
            "Ext.Ajax.defaultPostHeader = 'application/json';"+
			"Ext.Ajax.request({" + 
            	"async:false" +","+ 
                "timeout:6000" +","+ 
                "url:myUrl,"+    
                "method:'GET'" +","+ 
                "success:function(response,opts){" + 
                	"var result = Ext.JSON.decode(response.responseText);" +
                    "if(result.success){" +
                    
                    	"var ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);" +
                    	"var count = ResultDataValue['count'];" +
                        
                        "var mychecked=false;var arrStr=[];"+
                        "if(defaultValue!=''){"+
                        	"arrStr=defaultValue.split(',');"+
                        "}"+
                        "for(var i=0;i<count;i++){" +
                        	"var DeptID=ResultDataValue.list[i][valueField];" +
                            "var CName=ResultDataValue.list[i][displayField];" +
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
        var lists=Ext.JSON.decode(items);
        var strtemp='';
        Ext.each(lists,function(item,index,itemAll){
            if(item!=null){
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
	                "var array"+index+"=me.GetGroupItems("+"'"+item.tempUrl+"',"+"'"+item.tempValue+"',"+"'"+item.tempText+"',"+"'"+item.tempGroupName+"',"+"'"+arrStr+"');"+                
	                "var item"+index+"=me.getComponent('"+item.itemId+"');"+
	                "item"+index+".removeAll();"+
	                "item"+index+".add(array"+index+");"
                }
             }else if(type== 'button'){//如果组件类型为按钮类型
                var btnType=item.btnType;
                if(btnType=='select'){//按钮类型为查询按钮
                    isbtnSelectType=true;
                }else if(btnType=='reset'){
                   isbtnResetType=true;
                }else if(btnType=='close'){
                   isbtnCloseType=true;
                }
             }
            }
        });
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
            "layout:'absolute'," + 
            
            "isHiddenbtnSelect:false," + //是否隐藏查询按钮,false:不隐藏
            "isHiddenbtnReset:false," + //是否隐藏重置按钮,false:不隐藏
            "isHiddenbtnClose:false,"+//是否隐藏关闭按钮,false:不隐藏
            
            "logicalType:' " + logicalType + " ',"+//设置全与(and)或者设置全或(or)关系
            "GetGroupItems:" + GetComboboxItems + ",";
            
            //一般查询全与关系时公开getValue()方法,返回where查询字符串
            appClass=appClass+ "getValue:" + getValue + "," ;
            
            appClass=appClass+
            "initComponent:function(){" + 
                "var me=this;" ;
                //注册事件
               if(isbtnSelectType==true){//按钮类型为查询按钮
                   appClass=appClass+"me.addEvents('selectClick');" //查询公开事件
                }
                if(isbtnResetType==true){
                   appClass=appClass+"me.addEvents('resetClick');"  //重置公开事件
                }
                if(isbtnCloseType==true){
                   appClass=appClass+"me.addEvents('closeClick');"  //关闭按钮公开事件
                }
                
                //对外公开方法
                appClass=appClass+ "me.load=" + loadData + ";" + 
                //内部数据匹配方法
                "me.changeStoreData=function(response){" + 
                    "var data = Ext.JSON.decode(response.responseText);" + 
                    "var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);" + 
                    "data.ResultDataValue = ResultDataValue;" +
                    "data.list = ResultDataValue.list;" + 
                    "response.responseText = Ext.JSON.encode(data);" + 
                    "return response;" + 
                "};" + 
                //内部是否只读方法
               	"me.setReadOnly=function(bo){" + 
               		"var items = me.items;" + 
               		"for(var i in items){" + 
               			"items[i].readOnly=bo;" + 
               		"}" + 
               	"};" + 
                //内部组件
                "me.items=" + items + ";";

            if(html != ""){
                appClass = appClass + 
                "me.html='" + html.replace(/\"/g,"\\\"") + "';";
            }
                appClass = appClass + 
                "me.callParent(arguments);" + 
            "}," + 
            
            "afterRender:function(){" + 
            	"var me=this;" + 

            	"if(me.type == 'edit'){" + 
            		"me.load();" + 
            	"}else if(me.type == 'show'){" + 
            		"me.load();" + 
            		"me.setReadOnly(true);" + 
            	"}" + 
            	strtemp + 
            	"me.callParent(arguments);" + 
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 

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
        "function(){" + 

        "}";
        return fun;
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
        }else if(type == 'daterange'){//日期区间新(两个日期)控件
            com = me.createDaterangeStr(record);
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
        }else if(type == 'datetimenew'){//日期时间框
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
        }else if(type == 'dataradiogroup'){//定值单选组
            com = me.createDataRadioStr(record);
        }else if(type == 'datacheckboxgroup'){//定值复选组
            com = me.createDataCheckboxStr(record);
        }else if(type == 'numbersintervals'){//数字区间
            com = me.createNumbersintervalsStr(record);
        }else if(type == 'timeintervals'){//时间区间
            com = me.createTimeintervalsStr(record);
        }
        return com;
    },
    /**
     * 创建数字区间
     * @private
     * @param {} record
     * @return {}
     */
    createNumbersintervalsStr:function(record){
        var me=this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var width1=record.get('Width');
        var height1=record.get('Height');
        var rawOrCol=record.get('RawOrCol');
        if(rawOrCol==''){
            rawOrCol='hbox';
        }
        if(rawOrCol=='hbox'&&width1<325)//不分行
        {
            width1=325;
            height1=28;
        }
         if(rawOrCol=="vbox"&&height1<56)//分行
        {
            height1=56;
        }
        var fieldLabelTwo=''+record.get('fieldLabelTwo');//
        var operationcontents=''+record.get('operationcontents');//运算符

        var com = "{"+
            "xtype:'numbersintervals'"+ "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "hidden:" + record.get('IsHidden')+ "," + 
           //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + width1+ "," + 
            "height:" + height1+ "," + 
            "hidden:" + record.get('IsHidden')+ "," +
            "fieldLabelTwo:'" +fieldLabelTwo+ "'," +//第二个控件的显示名称设置
            "layoutType:'" +rawOrCol+ "'," +//控件布局设置,默认为横布局(hbox),竖布局为(vbox)
            "labelAlign:'left'"+ 
        "}";
        
        return com;
    },
    /**
     * 创建时间区间组件
     * @private
     * @param {} record
     * @return {}
     */
    createTimeintervalsStr:function(record){
        var me=this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var width1=record.get('Width');
        var height1=record.get('Height');
        var rawOrCol=record.get('RawOrCol');
        if(rawOrCol==''){
            rawOrCol='hbox';
        }
        if(rawOrCol=='hbox'&&width1<320)//不分行,横向盒
        {
            width1=320;
            height1=28;
        }
         if(rawOrCol=="vbox"&&height1<56)//分行
        {
            height1=56; 
        }
        var showFomart=''+record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='H:i';
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var operationcontents=''+record.get('operationcontents');//运算符
        var fieldLabelTwo=''+record.get('fieldLabelTwo');//
        var com = //Ext.create('Ext.zhifangux.TimeIntervals', {
            "{"+
            "xtype:'timeintervals'"+ "," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "hidden:" + record.get('IsHidden')+ "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + width1+ "," + 
            "height:" + height1+ "," + 
            "hidden:" + record.get('IsHidden')+ "," +
            "fieldLabelTwo:'" + fieldLabelTwo+ "'," +
            "layoutType:'" +rawOrCol+ "'," +//控件布局设置,默认为横布局(hbox),竖布局为(vbox)
            "labelAlign:'left'"+ "," +
            "value:new Date()," + 
            "valueTwo:new Date()," + 
            "editable:" + canEdit+ "," + 
            "dateFormat:" + "'"+showFomart+"'"+ 
            //",operationcontents:" + operationcontents+ 
       "}";
        
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
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var groupName=record.get('InteractionField');
        var defaultValue=[];
        defaultValue.push(record.get('defaultValue'));
        
        var columns= (record.get('Columns')!="")?(record.get('Columns')):('2');
        var columnWidth= (record.get('ColumnWidth').lenght>0)?(record.get('ColumnWidth')):('100');

        var combodata=record.get('combodata');
        if(combodata&&combodata.length>0){
            combodata=record.get('combodata').replace(/"/g,"'");
        }else{
            combodata=[];
        }
        var operationcontents=''+record.get('operationcontents');//运算符
        
        var com =
         "{"+
            "xtype:'checkboxgroup'" + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
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
            "hidden:" + record.get('IsHidden')+ "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "vertical: true"+ "," + 
            "padding:2"+ "," + 
            "autoScroll:true,"+
            
            "isdataValue:true"+ "," + //是否是定值单/复选组,单/复选组特有自定义添加的属性
            "items:" +combodata+ "," +
            "tempGroupName:'" +groupName+ "'" +
             "}";
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
        var columns= (record.get('Columns')!="")?(record.get('Columns')):(2);
        var columnWidth= (record.get('ColumnWidth').lenght>0)?(record.get('ColumnWidth')):(100);
        
        var combodata=record.get('combodata');
        if(combodata&&combodata.length>0){
            combodata=record.get('combodata').replace(/"/g,"'");
        }else{
            combodata=[];
        }
        var com =
        "{" + 
            "xtype:'radiogroup'" + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
             //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
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
            "tempGroupName:'" +groupName+ "'"+
            "}";
        return com;
    },
    /**
     * 创建监听代码---现在不用
     * @private
     * @return {}
     */
    createListenersStr:function(records){
        var sortCount = 0;
        for(var i in records){
            if(records[i].get('sortNum') > 0){
                sortCount++;
            }
        }
        var com = 
        ",listeners:{" + 
            "scope:this," + 
            "specialkey:function(field,e){" + 
                "var iNum = 1;" + 
                "var sNumField = 'sortNum';" + 
                "var form = field.ownerCt;" + 
                "var num = field[sNumField];" + 
                "var items = form.items.items;" + 
                "var max = " + sortCount + ";" + 
                "e.preventDefault();" + 
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
     * 创建定值运算关系下拉框
     * @private
     * @param {} record
     * @return {}
     */
    createDataComboxStr:function(record){
        var me = this;
        var fieldLabel = me.hasLab ? record.get('DisplayName') : "";
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var combodata=[];
        combodata=record.get('combodata');
        var data='';
        var myValue='';//设置默认值
        
        if(combodata.length>0){
            var arr=[];
            arr=Ext.decode(combodata);
	        for(var i=0;i<arr.length;i++){
               var tempArr=arr[i]
	           var tempValue=tempArr[0];
               var tempText=tempArr[1];
               if(i==0){
                 myValue="'"+tempValue+"'";
               }
               data=data+"{'value':"+"'"+tempValue+"',"+"'text':"+"'"+tempText+"'" +"},";
	        }
        }
        if(data.length>0){//截掉字符串最后一个字符","
            data=data.substring(0,data.length-1);
        }else{
            data="{'value':'=', 'text':'等于'},"+
                 "{'value':'!=','text':'不等于'},"+
                 "{'value':'>','text':'大于'},"+
                 "{'value':'<','text ':'小于'},"+
                 "{'value':'≥','text':'大于等于'},"+
                 "{'value':'≤','text':'小于等于'},"+
                 "{'value':'in','text':'包含'},"+
                 "{'value':'not in','text':'不包含'}";
                  myValue="'='";
        }
        //取数据源有问题
        var myStore = "Ext.create('Ext.data.Store', {"+
            "fields:['value','text']" + "," + 
            "data :["+data+"]"+
      "})";

        var com = 
        "{" + 
            "xtype:'combobox'" + "," + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符

            //自定义属性,定值运算关系下拉框特有
            "boundField:'" + record.get('BoundField') + "'," +
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "value:"+ myValue + "," +
            "allowBlank: true," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," +
            "displayField:'text'," + 
            "valueField:'value'," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "mode:'local'," + 
            //"editable:false," +  
            "store:" + myStore + "" + 
        "}";
        return com;
    },
    /**
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
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
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
        var com = "{" + 
            "xtype:'dateintervals'," + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "editable:" + canEdit + "," + 
            "name:'" +record.get('InteractionField')+"'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" +fieldLabel+"'," + 
            "fieldLabelTwo:'" +fieldLabelTwo+"'," + 
            "labelWidth:" +record.get('LabelWidth')+"," + 
            "width:" +record.get('Width')+"," + 
            "height:" +record.get('Height')+"," + 
            "layoutType:'" +rawOrCol+"'," + 
            "labelAlign:'left'" + "," + 
            "value:''" + "," + 
            "valueTwo:''" + "," + 
            "height:" +record.get('Height')+"," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "hidden:" + record.get('IsHidden') + "," + 
            //"operationcontents:" + record.get('operationcontents') + "," +  
            "dateFormat:'" + showFomart + "'"  +
            //"dateFormat:'Y-m-d'"+ 
        "}";
        return com;
    },
    /**
     * 创建日期区间新组件Str
     * @private
     * @param {} record
     * @return {}
     */
    createDaterangeStr:function(record){
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
        var com = "{" + 
            "xtype:'daterange'," + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "editable:" + canEdit + "," + 
            "name:'" +record.get('InteractionField')+"'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "fieldLabelOne:'" +fieldLabel+"'," + 
            "fieldLabelTwo:'" +fieldLabelTwo+"'," + 
            "labelWidthOne:" +record.get('LabelWidth')+"," + 
            "labelWidthTwo:" +record.get('LabelWidthTwo')+"," +
            "labelSeparatorOne:'" +record.get('LabelSeparatorOne')+"'," + 
            "labelSeparatorTwo:'" +record.get('LabelSeparatorTwo')+"'," + 
            "width:" +record.get('Width')+"," + 
            "height:" +record.get('Height')+"," + 
            "layoutType:'" +rawOrCol+"'," + 
            "labelAlign:'left'" + "," + 
            "valueOne:''" + "," + 
            "valueTwo:''" + "," + 
            "height:" +record.get('Height')+"," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "hidden:" + record.get('IsHidden') + "," + 
            //"operationcontents:" + record.get('operationcontents') + "," +  
            "dateFormat:'" + showFomart + "'"  +
            //"dateFormat:'Y-m-d'"+ 
        "}";
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
        var url = record.get('ServerUrl').split("?")[0];
        var valueField=record.get('valueField');
        var textField=record.get('textField');
        if((valueField==null||valueField=="")||(textField==null||textField=="")||(url==null||url=="")){
        Ext.Msg.alert('提示','错误信息【<b style="color:red">'+'请先配置下拉框的数据对象或数据服务'+"</b>】");
        return false;
        }else{
            url = url+"?isPlanish=true&fields="+valueField+","+textField;
        }
        var com = 
        "{" + 
            "xtype:'combobox'" + "," + 
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "name:'" + record.get('InteractionField') + "'," + 
            //自定义属性,每个个控件都有
            "fieldLabel:'" + fieldLabel + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "value:'" + record.get('defaultValue') + "'," + 
            "mode:'local'," + 
            "allowBlank: true," + 
            
            "displayField:'" + textField+ "'," + 
            "valueField:'" + valueField+ "'," + 
            "store:new Ext.data.Store({" + 
                "fields:['" + textField + "','" + valueField+ "']" + "," + 
                "pageSize:1000,"+
                "proxy:{" + 
                    "type:'ajax'," + 
                    "async:false,"+
                    "url:getRootPath()+"+"'/'+"+"'"+url+"',"+//修改getRootPath()的路径问题
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
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
             //自定义属性,每个个控件都有
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
            "operationcontents:" + record.get('operationcontents') + "," + 
            "hidden:" + record.get('IsHidden') +  
        "}";
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
        "{" + 
            "xtype:'numberfield'," + 
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
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
            "step:" + record.get('NumberIncremental') + 
        "}";
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
        "{" + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "xtype:'datefield'" + "," +  
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
            "hidden:" + record.get('IsHidden'); 
            if(showFomart==''||showFomart==""||showFomart==null){
                
            }else{
                com=com+",format:'" + showFomart + "'" ; 
            }
            com=com+ "}";
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
        "{" + 
            "xtype:'timefield'" + "," +  
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            //自定义属性,每个个控件都有
            "itemId:'" + record.get('InteractionField') + "'," + 
            "labFieldAlign:'left'"+ ","+
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
                
            }else{
                com=com+",format:'" + showFomart + "'" ; 
            }
            com=com+ "}";
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
        var showFomart=record.get('ShowFomart');
        if(showFomart==''||showFomart==""||showFomart==null){
            showFomart='Y-m-d H:i:s';
        }
        var canEdit=record.get('CanEdit');//是否允许编辑
        var com = 
        "{" + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "height:" +record.get('Height')+"," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "xtype:'datetimenew'" + "," +  
            "editable:" + canEdit + "," + 
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "labelStyle:'" + labelStyle + "'," + 
            "width:" + record.get('Width') + "," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "readOnly:" + record.get('IsReadOnly') + "," + 
            "hidden:" + record.get('IsHidden') + "," + 
            "labelAlign:'left'"+ ","+
            //"value:new Date()"+ ","+
            "setType:'datetime'"+ ","+//控件类型:datetime(日期时间),date(日期),time(时间),
            "selectOnFocus:true";
            if(showFomart==''||showFomart==""||showFomart==null){
                
            }else{
                com=com+",format:'" + showFomart + "'" ; 
            }
            com=com+ "}";
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
        var url = record.get('ServerUrl').split("?")[0];
        var valueField=record.get('valueField');
        var textField=record.get('textField');
        if((valueField==null||valueField=="")||(textField==null||textField=="")||(url==null||url=="")){
        Ext.Msg.alert('提示','错误信息【<b style="color:red">'+'请先配置下拉框的数据对象或数据服务'+"</b>】");
        return false;
        }
        var com = 
        "{" + 
            "xtype:'checkboxgroup'" + "," + 
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            //自定义属性,每个个控件都有
    
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
        var groupName=record.get('InteractionField');
        var defaultValue=[];
        defaultValue.push(record.get('defaultValue'));
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var labelStyle= (record.get('LabFont')!="")?(record.get('LabFont')):('font-style:normal;');
        var columns= (record.get('Columns')!="")?(record.get('Columns')):('2');
        var columnWidth= (record.get('ColumnWidth').lenght>0)?(record.get('ColumnWidth')):('100');
        var url = record.get('ServerUrl').split("?")[0];
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
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
  
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
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
  
            "name:'" + record.get('InteractionField') + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "text:'" + record.get('DisplayName') + "'," + 
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," + 
            "hidden:" + record.get('IsHidden') + 
        "}";
        return com;
    },
    /**
     * 创建图片Str
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
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            //自定义属性,每个个控件都有
   
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
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
        
            "name:'" + record.get('InteractionField') + "'," + 
            "fieldLabel:'" + fieldLabel  + "'," + 
            "labelWidth:" + record.get('LabelWidth') + "," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
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
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            
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
        var btnType=record.get('btnType');
        var com = 
        "{" + 
            "xtype:'button'," + 
            //自定义属性,每个个控件都有
            "isOperation:" + record.get('isOperation') + "," + //是否绑定带有运算关系符
            "name:'" + record.get('InteractionField') + "'," + 
            "width:" + record.get('Width') + "," + 
            "height:" +record.get('Height')+"," + 
            "text:'" + record.get('DisplayName') + "'," + 
            "btnType:'" + record.get('btnType') + "'," + //自定义属性,按钮类型:查询或重置,关闭
            "itemId:'" + record.get('InteractionField') + "'," + 
            "x:" + record.get('X') + "," + 
            "y:" + record.get('Y') + "," 

              if(btnType=="select"){//查询按钮
                  com=com+"hidden:me.isHiddenbtnSelect,"+"iconCls:'bsearch-img-16',";
                 
              }else if(btnType=="reset"){//重置按钮
                 com=com+"hidden:me.isHiddenbtnReset,"+"iconCls:'build-button-refresh',"; 
              }else if(btnType=="colse"){//关闭按钮
                 com=com+"hidden:me.isHiddenbtnClose,"+"iconCls:'imgdiv-close-show-16',"; 
              }else{
                com=com+"hidden:" + record.get('IsHidden')+ ",";
              }
              
             com=com+"listeners: {"+
                "click: function() {";
                if(btnType=="select"){//查询按钮
                  com=com+ "me.fireEvent('selectClick');";
                  com=com+"var lastValue=me.getValue();";
  
                }else if(btnType=="reset"){//重置按钮
                  com=com+
                  "me.getForm().reset();" + 
                  "me.fireEvent('resetClick');" ;
                }else if(btnType=="close"){//关闭按钮
                  com=com+
                  "me.close();" + 
                  "me.fireEvent('closeClick');" ;
                }
                com=com+"}"+
           " }"+
            
        "}";
        return com;
        
    },
  
    /**
     * 创建高级查询的提交数据处理的方法
     * 如果表单里的控件类型为"label","button","image","filefield","htmleditor"时,不作取值处理
     * 如果表单里的控件类型为"checkboxgroup",
     * 自定义属性(leftBrackets,rightBrackets,brackets),每个个控件都有
     * @private
     * @param {} saveDataServerUrl
     * @return {}
     */
    createSubmitSelectStr:function(){
        var me=this;
        var fun = 
        "function(){" + 
            "var form = this;" + 
            "var logicalType= form.logicalType;" + 
            "if(logicalType === ''||logicalType ==undefined){" +
                "logicalType=' and ';" +
            "}"+
            "var items=form.items.items;"+
            "var length=form.items.length;"+
            "var lastValue='';"+ //myValue:单个组件的结果值;lastValue:最后where串
            "var operation=' like ';"+//关系运算符,默认为like
            //保证有值
            "lastValue=selectFormChangeHQL(items,length,logicalType);"+
           "return lastValue;"+
        "}";
        return fun;
    },
    /**
     * 创建高级查询的提交数据处理的方法
     * 如果表单里的控件类型为label,button,image,filefield,htmleditor时,不作取值处理
     * 如果表单里的控件类型为checkboxgroup,
     * 自定义属性(leftBrackets,rightBrackets,brackets),每个个控件都有
     * @private
     * @param {} saveDataServerUrl
     * @return {}
     */
    createSubmitSelect:function(){
        var me=this;
        var form = me.getCenterCom();
        var params = me.getPanelParams();
        var logicalType=params.logicalType;
        var items=form.items.items;
        var length=form.items.length;
        var lastValue=selectFormChangeHQL(items,length,logicalType);
        return lastValue;
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
                    //me.appId = result.id;
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
    /**
     * 添加运算关系符组件行记录
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    operationCombobox:function(componentItemId,x,y){
        var me=this;
        var tempValue="[['=', '等于'],['!=', '不等于'],['>', '大于'],['<', '小于'],"
        +"['<=', '小于等于'],['>=', '大于等于'],['in', '包含'],['not in', '不包含']]";
        var value={
	        'InteractionField':me.operationid,
	        Width:80,
            Height:22,
	        LabFont:'',//显示名称字体内容
            Type:'datacombobox',//数据项类型--//定值下拉框
            DisplayName:'',//显示名称字体内容
            value:'datacombobox',
            BoundField:componentItemId,
            combodata:tempValue,//定值下拉框默认值
            X:x,//位置X
            Y:y,//位置X
	        editable:false
	        //isOperation:true
        };
       return value;
    },

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
     * 添加按钮
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    addButton:function(obj){
        var me=this;
        var value={
    	   'InteractionField':obj.itemId,
 	        Width:80,
            Height:22,
 	        DisplayName:''+obj.btnName,//显示名称
            Type:'button',//数据项类型
            value:'按钮',
            X:5,//位置X
            Y:5,//位置X
            btnType:""+obj.btnType+"",
 	        editable:false
        };
       return value;
    },
 
	/**
	 * 生成复选组的子项数据
	 * @param {} name2:复选项的名称
	 * @return {} myCheckboxItems:复选组的子项数组
	 */
    getCheckboxgroupStore:function(){
        var me = this;
        var localData=[];
        var south = me.getComponent('south');
        var store = south.store;
        var records = [];
        store.each(function(record){
        	var itemId=record.get('InteractionField');
	    	var tempStr=itemId.split("_");
	    	var fields=(""+tempStr[tempStr.length-1]+"");
	        if(fields==='operation'){
	        	return;
	        }else if(fields==='logical'){
	        	return;
	        }
	        else{
	        	var record=record;
	        	records.push(record);
	        }
        });
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
        for (var i = 0; i < arr.length; i++) {  
	        var textField =arr[i].DisplayName;  
	        var keyField = arr[i].InteractionField;
	        var tempItem={  
	             boxLabel : textField,  
	             inputValue: keyField,
	             name:me.checkboxgroupName,//复选子项的名称
	             itemId: me.keyField +i//'checkbox_id'+i
	               
	         };
	        localData.push(tempItem);
        }
        return localData;
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
    /***
     * 更新展示区域里的所有组件的x,y轴
     * @param {} record
     */
    setAllComXY:function(record){
        var me=this;
        var records=me.getSouthRecords();
        var center=me.getCenterCom();
       //如果新增组件为关系运算符,需要处理x,y值
       var x=0;
       var y=0;
       if(record.get('Type')=='datacombobox'){
           x=record.get('X');
           y=record.get('Y');
       }
        //添加新的控件
       var com =me.newfromItem(record,null,0,0,x,y);
            center.add(com); 
            
        Ext.Array.each(records,function(record){
            var itemId =record.get(me.columnParamsField.InteractionField);
            me.setComponentXY(itemId,record.get('X'),record.get('Y'));
        });
    },
    /**
     * 【获取当前选中的项目的值】
     * @param {} checkboxgroup:复选组控件
     * @return {}返回值为所有选中的复选项的属性inputValue的所有值,如”1,2,3,4,5”
     */
    getCheckValue:function(){
        return me.lastValues;
    }
});