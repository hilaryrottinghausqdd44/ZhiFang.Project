/**
 * 普通图表构建工具
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
Ext.define('Ext.build.BasicChart',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basicchart',
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
    buildTitle:'普通图表构建工具',
    /**
     * 构建图表的默认的ItemId
     * 在选择了数据对象后,默认取数据对象树里的第一个选中字段
     */
    chartItemId:'chartItemId',
    /**
     * 获取数据服务列表时后台接收的参数名称
     * @type String
     */
    objectServerParam:'EntityName',
    //数据对象配置private
    
    //标题字体设置
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
     * 获取图表数据源服务地址
     * @type 
     */
    chartServerUrl:'/SingleTableService.svc/',
    /**
     * 返回数据对象列表的值属性
     * 例如：
     *  返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,list:[{a:1}]}"}
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
     * 图表类型Stroe
     * @type 
     */   
    ChartTypeData:[
	    {"value":"Line", "name":"折线图"},
	   
	    //{"value":"Area", "name":"面积图"},
	
	    {"value":"Scatter", "name":"散点图"},
	    {"value":"Radar", "name":"雷达图"},
	    
	    //{"value":"Pie", "name":"饼状图"},
	    {"value":"Column", "name":"(水平/垂直)条形图"}
        //{"value":"Gauge", "name":"仪表图"},
	    //{"value":"Bar", "name":"(堆积/分组)条形图"}//先隐藏
    ],
    /**
     * 图表类型
     * @type 
     */
    comTypeList:[
        ['Line','折线图'],
       
        //['Area','面积图'],

        ['Scatter','散点图'],
        ['Radar','雷达图'],
        //['Pie','饼状图'],
        ['Column','水平/垂直条形图']
        // ['Gauge','仪表图'],
        //['Bar','堆积/分组条形图']
    ],
    /**
     * 图表坐标轴类型
     * @type 
     */
    axesTypeList:[
        ['Numeric','数字类型'],
        ['Category','分组类型'],
        ['TimeAxis','时间类型'],
        ['GaugeAxis','仪表类型']
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
     * 图表数据格式时x轴字段名称
     * @type Boolean
     */
    chartX:"\'name\'",
    /**
     * 图表数据格式时x轴字段名称
     * @type Boolean
     */
    chartY:'data,data1,data2,data3,data4,data5,data6,data7,data8,data9,data10',
    
    chartFields:this.chartX+','+this.chartY,
    /**
     * 是否显示名称
     * @type Boolean
     */
    hasLab:true,
    /**
     * 表单初始宽度
     * @type Number
     */
    defaultPanelWidth:620,
    /**
     * 表单初始高度
     * @type Number
     */
    defaultPanelHeight:320,
    
    /**
     * 图表的初始宽度
     * @type Number
     */
    defaultChartWidth:580,
    /**
     * 图表的初始高度
     * @type Number
     */
    defaultChartHeight:260,

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
        if(me.isJustOpen==true){
            //me.isJustOpen=false;
            var combo=me.getEastChartTypeCom();
            var isnn="'grid'";
            var values="{chart:["+isnn+"]}";
            var mynnJson=Ext.decode(values);
            combo.setValue(mynnJson);
         }
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
        south.height = 130;
        east.width = 280;
        
        //功能块收缩属性
        east.split = true;
        east.collapsible = true;
        
        south.split = true;
        south.collapsible = true;
        me.items = [north,center,east,south];
    },
    
    /**
     * 创建功能栏
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
                }
            ]
        };
        return com;
    },
    /**
     * 创建效果展示面板
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
                title:'图表展示区域',
                itemId:'center',
                width:me.defaultPanelWidth,
                height:me.defaultPanelHeight
            }]
        };
        return com;
    },
    //==================操作属性列表的某一控件属性,更新展示区域的控件显示效果==============

    
    //==============================某一控件属性更新==============================

    /**
     * 创建列属性列表
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
                {text:'交互字段',dataIndex:'InteractionField',disabled:true,editor:{readOnly:true}},
                {text:'表单标题名称',dataIndex:'DisplayName',
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setColumnParamsRecord(InteractionField,'DisplayName',newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'name',newValue);
                            }
                        }
                    }
                },
                {text:'数据源类型',dataIndex:'StoreType',width:80,align:'center'},
                {text:'图表类型',dataIndex:'Type',width:80,align:'center'},
                {text:'X坐标轴类型',dataIndex:'XType',width:80,align: 'center',
                    renderer:function(value, p, record){
                        var typelist = me.axesTypeList;
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
                            data:me.axesTypeList
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
                {text:'Y坐标轴类型',dataIndex:'YType',width:80,align: 'center',
                    renderer:function(value, p, record){
                        var typelist = me.axesTypeList;
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
                            data:me.axesTypeList
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
                {text:'宽度',dataIndex:'Width',width:50,value:me.defaultChartWidth,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:3000,
                        value:me.defaultPanelWidth,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setSouthRecordForNumberfield(InteractionField,'Width',newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'Width',newValue);
                                }
                            }
                        }
                    }
                },
                 {text:'高度',dataIndex:'Height',width:50,value:me.defaultChartHeight,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:2000,
                        value:me.defaultPanelHeight,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setSouthRecordByKeyValue(InteractionField,'Height',newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'Height',newValue);
                                }
                            }
                        }
                    }
                },
                {text:'主题',dataIndex:'Theme',width:50,align:'center'},
                {text:'X坐标轴显示名称',dataIndex:'XTitle',width:100,align:'center',
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setColumnParamsRecord(InteractionField,'XTitle',newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'XTitle',newValue);
                            }
                        }
                    }
                },
                {text:'X坐标轴显示位置',dataIndex:'XPosition',width:100,align: 'center'},
                {text:'X坐标轴最小显示值',dataIndex:'XMinimum',width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setSouthRecordForNumberfield(InteractionField,'XMinimum',newValue);
                                }
                            }
                        }
                    }
                },
               {text:'X坐标轴最大显示值',dataIndex:'XMaximum',width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setSouthRecordForNumberfield(InteractionField,'XMaximum',newValue);
                                }
                            }
                        }
                    }
                },
                {text:'X坐标轴显示日期格式',dataIndex:'XDateFormat',width:120,align:'center',
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setColumnParamsRecord(InteractionField,'XDateFormat',newValue);
                                }
                            }
                        }
                    }
                },
                {text:'X轴刻度标签显示方式',dataIndex:'XLabel',hidden:false,width:120},
                {text:'Y坐标轴显示名称',dataIndex:'YTitle',width:110,align:'center',
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setColumnParamsRecord(InteractionField,'YTitle',newValue);
                                me.setBasicParamsForInteractionField(InteractionField,'YTitle',newValue); 
                            }
                        }
                    }
                },
                {text:'Y坐标轴显示位置',dataIndex:'YPosition',width:110,align: 'center'},
                {text:'Y坐标轴最小显示值',dataIndex:'YMinimum',width:120,align:'center',
                    xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setSouthRecordForNumberfield(InteractionField,'YMinimum',newValue);
                                }
                            }
                        }
                    }
                },
               {text:'Y坐标轴最大显示值',dataIndex:'YMaximum',width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setSouthRecordForNumberfield(InteractionField,'YMaximum',newValue);
                                }
                            }
                        }
                    }
                },
               {text:'Y轴显示日期格式',dataIndex:'YDateFormat',width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                if(newValue!=null){
                                me.setSouthRecordForNumberfield(InteractionField,'YDateFormat',newValue);
                                }
                            }
                        }
                    }
                },
                {text:'Y轴刻度标签显示方式',dataIndex:'YLabel',hidden:false,width:120},
                
                {text:'Series类型',dataIndex:'SeriesType',hidden:false,width:90},
                {text:'突出显示标记',dataIndex:'HightLight',hidden:false,width:90},
                {text:'图表方向',dataIndex:'Orentation',hidden:false,width:90},
                 
                {text:'SeriesX字段',dataIndex:'XField',hidden:false,width:90},
                {text:'SeriesY字段',dataIndex:'YField',hidden:false,width:90},
                {text:'SeriesAxis',dataIndex:'SeriesAxis',hidden:false,width:90},
                
                {text:'图例对象设置',dataIndex:'Legend',hidden:false,width:90},
                {text:'条形图显示方式',dataIndex:'Column',hidden:false,width:90},
                {text:'条形图显示类型',dataIndex:'Stacked',hidden:false,width:90},
                {text:'饼图圆环显示',dataIndex:'ISDonut',hidden:false,width:90},
                {text:'饼图图例显示',dataIndex:'ShowInLegend',hidden:false,width:90},
                {text:'饼图半径',dataIndex:'InsetPadding',hidden:false,width:90},
                {text:'表盘的指针',dataIndex:'ISNeedle',hidden:false,width:90},
                
                {text:'数据地址',dataIndex:'ServerUrl',width:80},
                {text:'图表总字段集',dataIndex:'Fields',width:80},
				{text:'X轴字段集',dataIndex:'XAxesFields',width:80},
				{text:'Y轴字段集',dataIndex:'YAxesFields',width:80},
               {text:'X主刻度的间隔步距',dataIndex:'XMajorTickSteps',width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'XMajorTickSteps',newValue);
                            }
                        }
                    }
                },
               {text:'Y主刻度的间隔步距',dataIndex:'YMajorTickSteps',width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'YMajorTickSteps',newValue);
                            }
                        }
                    }
                },{text:'X轴标签显示方式',dataIndex:'XDegrees',width:120,minValue :-360,maxValue :360,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'XDegrees',newValue);
                            }
                        }
                    }
                },{text:'Y轴标签显示方式',dataIndex:'YDegrees',minValue :-360,maxValue :360,width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'YDegrees',newValue);
                            }
                        }
                    }
                },
                {text:'两个条形图间的空白间隔',dataIndex:'Gutter',minValue :0,maxValue :360,width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'Gutter',newValue);
                            }
                        }
                    }
                },
                {text:'仪表图数字显示位置',dataIndex:'Margin',minValue :0,maxValue :360,width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'Margin',newValue);
                            }
                        }
                    }
                },
                {text:'仪表图步距',dataIndex:'Steps',minValue :0,maxValue :360,width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'Steps',newValue);
                            }
                        }
                    }
                },{text:'仪表图突出显示效果时间',dataIndex:'HighlightDuration',minValue :0,maxValue :360,width:120,align:'center',xtype:'numbercolumn',format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue,oldValue,eOpts){
                                var record = com.ownerCt.editingPlugin.context.record;
                                var InteractionField = record.get('InteractionField');
                                me.setSouthRecordForNumberfield(InteractionField,'HighlightDuration',newValue);
                            }
                        }
                    }
                },
                {text:'X坐标轴网格线',dataIndex:'ISXGrid',width:80},
                {text:'Y坐标轴网格线',dataIndex:'ISYGrid',width:80}
            ],
            store:Ext.create('Ext.data.Store',{
                fields:[//图表基本配置属性
                    {name:me.columnParamsField.DisplayName,type:'string'},//显示名称
                    {name:me.columnParamsField.InteractionField,type:'string'},//交互字段
                    {name:'Type',type:'string'},//图表类型
                    {name:'StoreType',type:'string'},//图表类型
                    {name:'Width',type:'int'},//数据项宽度
                    {name:'Height',type:'int'},//高度
                    {name:'Theme',type:'string'},//主题
                    
                    //图表坐标轴的配置项的定义
                    {name:'ISXGrid',type:'string'},//X坐标轴网格线
                    {name:'ISYGrid',type:'string'},//Y坐标轴网格线
                    {name:'XTitle',type:'string'},//X坐标轴显示名称
                    {name:'XType',type:'string'},//X坐标轴显示类型
                    {name:'XPosition',type:'string'},//X坐标轴显示位置,left,bottom,right,top

                    {name:'XMinimum',type:'int'},//X坐标轴最小显示值
                    {name:'XMaximum',type:'int'},//X坐标轴最大显示值
                    {name:'XDateFormat',type:'string'},//X坐标轴显示日期格式
                    {name:'XLabel',type:'string'},//X坐标轴刻度上的标签的显示方式设置
                    
                    {name:'YTitle',type:'string'},//Y坐标轴显示名称
                    {name:'YType',type:'string'},//Y坐标轴显示类型
                    {name:'YPosition',type:'string'},//Y坐标轴显示位置

                    {name:'YMinimum',type:'int'},//Y坐标轴最小显示值
                    {name:'YMaximum',type:'int'},//Y坐标轴最大显示值
                    {name:'YDateFormat',type:'int'},//Y坐标轴显示日期格式
                    {name:'YLabel',type:'string'},//Y坐标轴刻度上的标签的显示方式设置
                    
                    //Series的配置项的定义(饼图没有XField,YField,Axis)
                    {name:'SeriesType',type:'string'},//Series类型
                    {name:'HightLight',type:'bool'},//为true时,当移动到图表的标记上面时,会突出显示该标记
                    {name:'Orentation',type:'string'},//设置图表的方向(horizontal:水平方向,vertical:垂直方向)
                    
                    //折线图配置项
                    {name:'XField',type:'string'},//Series的X字段
                    {name:'YField',type:'string'},//Series的Y字段
                    {name:'SeriesAxis',type:'string'},//声明数值在哪条坐标轴上
                    
                    //图例对象Legend的配置项,json对象,需要单独提供
                    //包括设置的项有boxFill,position:(top,bottom,right,float(需要设置x,y)),visible,x,y(xy,需要position设置为float才生效)
                    {name:'Legend',type:'string'},//图例对象设置
                    
                    //条形图Bar配置项
                    {name:'Column',type:'string'},//条形图显示方式,true为水平,false为垂直方式
                    {name:'Stacked',type:'string'},//条形图显示类型,true为堆积,false为分组
                    
                    //饼(仪表)图Pie配置项
                    {name:'ISDonut',type:'string'},//是否显示圆环图,true为是,false为否
                    {name:'ShowInLegend',type:'bool'},//饼图是否显示图例,true为堆积,false为分组
                    {name:'InsetPadding',type:'int'},//饼图半径
                    {name:'ISNeedle',type:'bool'},//仪表图图特有,表盘的指针设置,true:显示Margin
                    {name:'Margin',type:'int'},//仪表图图特有,表盘的数字显示位置,负数在表盘内显示
                    {name:'Steps',type:'int'},//仪表图图特有,仪表图的步距
                    {name:'HighlightDuration',type:'int'},//仪表图图特有,仪表图的突出显示效果时间
                    {name:'ServerUrl',type:'string'},//数据地址fields
                    
                    {name:'Fields',type:'string'},//图表总字段集
                    {name:'XAxesFields',type:'string'},//图表X坐标轴字段集
                    {name:'YAxesFields',type:'string'},//图表Y坐标轴字段集
                    {name:'XMajorTickSteps',type:'int'},//X主刻度的间隔步距
                    {name:'YMajorTickSteps',type:'int'},//Y主刻度的间隔步距
                    {name:'XDegrees',type:'int'},//X轴标签显示方式
                    {name:'YDegrees',type:'int'},//Y轴标签显示方式
                    {name:'Gutter',type:'int'}//设置两个条形图之间的空白间隔
                ],
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
     * 创建属性面板
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
        var dataObjOther = me.createDataObjOther();
        var formParamsPanel = {
            xtype:'form',
            itemId:'center' + me.ParamsPanelItemIdSuffix,
            title:'图表属性配置',
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            items:[appInfo,panelWH,title,dataObj,dataObjOther]
        };
        
        var com = {
            xtype:'form',
            title:'图表属性配置',
            autoScroll:true,
            items:[formParamsPanel]
        };
        return com;
    },
    /**
     * 表单的基本功能配置项
     * @private
     * @return {}
     */
    createAppInfo:function(){
        var me=this;
        var com = {
            xtype:'fieldset',title:'功能信息',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'appInfo',
            items:[
              {
                xtype:'combobox',fieldLabel:'图表类型',
                itemId:'formType',name:'formType',
                queryMode: 'local',
                labelWidth:55,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:false,typeAhead:true,
                forceSelection:true,
                emptyText:'请选择图表类型',
                displayField:'name',
                valueField:'value',
                value:'Line',
                store:new Ext.data.Store({ 
                        fields:['value', 'name'],
                        data :me.ChartTypeData
                        }),
                 listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.removeSouthAndCenterAll();
                        var objectName=me.getEastobjectName();
                            objectName.setValue('');
                        //获取对象结构
                        var objectPropertyTree=me.getEastobjectPropertyTree();
                        objectPropertyTree.nodeClassName = "";
                        objectPropertyTree.CName = objectName.rawValue;
                        objectPropertyTree.ClassName = objectName.getValue();
                        
                        objectPropertyTree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + objectName.getValue();
                        objectPropertyTree.store.load();
                    }
                }
            
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
                xtype:'textfield',fieldLabel:'显示名称',labelWidth:55,value:'图表',anchor:'100%',
                itemId:'titleText',name:'titleText'
            }]
        };
        
        return com;
    },

    /***
     * 创建数据对象
     * @param {} storeType
     * @return {}
     */
    createObjectName:function(storeType){
        var me=this;
        var myUrl=me.objectUrl;
        var myfieldLabel='';
        if(storeType=='chart'){//如果选择的数据源为图表类型,数据对象下拉框ValueField绑定ServerUrl
            me.objectValueField='EName';
            var strWhere=me.objectServerParam + "=Chart";
            myUrl=me.objectGetDataServerUrl + "?" +strWhere;
            myfieldLabel='获取数据';
        }else{
            me.objectValueField='ClassName';
            myUrl=me.objectUrl;
            myfieldLabel='数据对象';
        }
        var com=Ext.create('Ext.form.ComboBox', {
        
                fieldLabel:myfieldLabel,
                itemId:'objectName',name:'objectName',
                labelWidth:60,anchor:'100%',layout:'anchor',
                editable:true,
                width:200,
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
                        url:myUrl,
                        reader:{type:'json',root:me.objectRoot},
                        extractResponseData:me.changeStoreData
                    },autoLoad:true
                }),
                listeners:{
//                    change:function(owner,newValue,oldValue,eOpts){
//                        var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
//                        if(newValue && newValue != "" && index != -1){
//                            me.objectChange(owner,newValue,eOpts);
//                        }
//                    },
                     select:function(com2,records,eOpts ){
                          if(records!=null){
                            var newValue = com2.getValue();
                            me.objectChange(com2,newValue,eOpts);
                           }
                        }
                }
        });
        return com;
    },
    /**
     * 创建表单属性的数据对象配置项
     * @private
     * @return {}lfc
     */
    createDataObj:function(){
        var me = this;
        var objectName=me.createObjectName('grid');
        var com = {
            xtype:'fieldset',title:'数据对象',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',anchor:'100%',layout:'anchor',
            labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
            itemId:'dataObject',
            items:[{
                xtype: 'radiogroup',
                itemId:'storeType',
                name:'chart',
                labelWidth:80,
                fieldLabel:'数据源类型',
                columns:2,
                vertical:true,
                 listeners:{
                      change:function(combo,newValue,oldValue,eOpts ){
                        if(newValue!=null){ 
                            
                            me.removeSouthAndCenterAll();
                            var objectName=me.getEastobjectName();

                            //数据对象组件的删除和重新生成

                            var dataObject =me.getEastdataObject();
                            dataObject.remove(objectName);
                            var objectName2=me.createObjectName(newValue.chart);
                            dataObject.add(objectName2);
                           
                            //树的显示和隐藏
                            var dataObjectOther=me.getEastdataObjectOther();

                            var strWhere='';
                            if(newValue.chart=='chart'){
                                dataObjectOther.hide();
                                strWhere=me.objectServerParam + "=Chart";
                            }else{
                                dataObjectOther.show(); 
					            //获取对象结构
                                var objectPropertyTree=me.getEastobjectPropertyTree();
					            objectPropertyTree.nodeClassName = "";
					            objectPropertyTree.CName = objectName.rawValue;
					            objectPropertyTree.ClassName = objectName.getValue();
					            
					            objectPropertyTree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + objectName.getValue();
					            objectPropertyTree.store.load();
                            }
                            var getDataServerUrl =me.getEastgetDataServerUrl();
                            getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + strWhere;
                            getDataServerUrl.store.load();
                            
                            me.setColumnParamsRecord(me.chartItemId,'StoreType',newValue.chart);
                           }
                        }
                    },
                items:[
                    {boxLabel:'列表',name:'chart',inputValue:'grid',itemId:'generalType'},
                    {boxLabel:'图表',name:'chart',inputValue:'chart',itemId:'chartType'}
                ]
            },
            {xtype:objectName}]
        };
        return com;
    },
    /**
     * 创建表单属性的数据对象配置项
     * @private
     * @return {}lfc
     */
    createDataObjOther:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',anchor:'100%',layout:'anchor',
            itemId:'dataObjectOther',//hidden:true,
            items:[
            {
                xtype:'combobox',fieldLabel:'获取数据',
                itemId:'getDataServerUrl',name:'getDataServerUrl',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                labelWidth:55,anchor:'98%',
                forceSelection:true,
                emptyText:'请选择获取数据服务',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                listeners:{
                      change:function(combo,newValue,oldValue,eOpts ){
                          if(newValue!=null){
                            var url = newValue.split("?")[0];
                            me.setColumnParamsRecord(me.chartItemId,'ServerUrl',url);
                           }
                        },
                        select:function(com,records,eOpts ){
                          if(records!=null){
                            var url = com.getValue().split("?")[0];
                            me.setColumnParamsRecord(me.chartItemId,'ServerUrl',url);
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
                            var objectName = dataObject.getComponent('objectName');
                            var strWhere=me.objectServerParam + "=List" + objectName.value;
                            var chartType=me.getEastChartType();
                            if(chartType=='chart'){
                                strWhere=me.objectServerParam + "=Chart";
                            }else{
                                strWhere=me.objectServerParam + "=List" + objectName.value;
                            }
                            store.proxy.url = me.objectGetDataServerUrl + "?" +strWhere;
                        }
                    }
                })
            },
                {
                xtype:'treepanel',itemId:'objectPropertyTree',border:false,
                dockedItems:[{
                    xtype:'toolbar',anchor:'98%',layout:'anchor',
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
                store:me.getobjectPropertyFields()
            }]
        };
        return com;
    },
    
    /**
     * 创建表单属性面板的其他设置
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
                xtype:'textfield',itemId:'formHtml',name:'formHtml',hidden:true
            }]
        };
        return com;
    },
    /**
     * 创建表单属性面板的宽高配置项
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
                xtype:'numberfield',fieldLabel:'宽度',labelWidth:55,anchor:'100%',
                itemId:'Width',name:'Width',value:me.defaultPanelWidth,
                listeners:{
                    blur:function(com,The,eOpts){
                        var center = me.getCenterCom();
                        center.setWidth(com.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'高度',labelWidth:55,anchor:'100%',
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
        
        //图表参数
        var params = me.getPanelParams();
        var formType=params.formType;//构建图表类型
        
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
				AppType:me.appType,//应用类型(图表)
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
    objectChange:function(owner,newValue,eOpts){
        var me = this;
        var dataObject = owner.ownerCt;
        var dataObjectOther =me.getEastdataObjectOther();
        var objectPropertyTree = me.getEastobjectPropertyTree(); 
        me.removeSouthAndCenterAll();
        
        var strWhere=me.objectServerParam + "=List" + newValue;
        var chartType=me.getEastChartType();
        
        if(chartType=='chart'){
            dataObjectOther.hide();
            strWhere=me.objectServerParam + "=Chart";
            me.objectPropertyOKClick();
        }else{
            dataObjectOther.show();
            strWhere=me.objectServerParam + "=List" + newValue;
            //获取对象结构
	        objectPropertyTree.nodeClassName = "";
	        objectPropertyTree.CName = owner.rawValue;
	        objectPropertyTree.ClassName = newValue;
	        
	        objectPropertyTree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
	        objectPropertyTree.store.load();
        }

        //获取获取数据服务列表
        var getDataServerUrl = dataObjectOther.getComponent('getDataServerUrl');
        getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" +strWhere;
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
        var storeType=me.getEastChartType();
        var dataObject = me.getEastdataObject();
        var dataObjectOther = me.getEastdataObjectOther();
        var objectName=me.getEastobjectName();
        
        //图表参数
        var params = me.getPanelParams();
        var formType=params.formType;//构建图表类型
        var width =me.defaultChartWidth;
        var height =me.defaultChartHeight;
        var insetPadding=0;
        var iSNeedle=true;
        var steps=2;
        var xPosition='bottom';
        var yPosition='left';
        var hightLight=true;
        var showInLegend=true;
        var myServerUrl="";
        var xMinimum=0;
        var xMaximum=20;
        var yMinimum=0;
        var yMaximum=120;
        var theme="Green";
        var margin=-10;
        var gutter=10;
        var seriesAxis='left';
        var xType='Category';//X坐标轴显示类型
        var yType='Numeric';//Y坐标轴显示类型
        var seriesType='line';
        
        me.removeSouthAndCenterAll();
        var store = me.getComponent('south').store;

        var params = me.getPanelParams();
        var formType=params.formType;//图表类型选择
        var myClassName = "",myDisplayName="",fields="",xTitle='',yTitle='',xField="",yField="",xAxesFields="",yAxesFields="";

        //普通列表数据格式
        if(storeType=='grid'){
        var tree =me.getEastobjectPropertyTree();//对象属性树
        var data = tree.getChecked();
        //勾选节点数组
        var myFields =[];
        //列表中显示被勾选中的对象
        Ext.Array.each(data,function(item, index, record){
            if(item.get('leaf')){
                var myIndex = store.findExact('InteractionField',item.get(me.columnParamsField.InteractionField));
                var tempValue=item.get(me.columnParamsField.InteractionField);
                var arr=[];
                arr = tempValue.split("_");
                if(arr.length>=2){
                    //如果该字段的格式是HREmployee_HRDept_CName,截去第一个分隔符"_"前的字符串
                    tempValue = arr[arr.length-2]+"_"+arr[arr.length-1];
                }
                
                var tempText=item.get('text');
                myFields.push(tempValue);
                if(index<record.length-1){
                    fields=fields+(""+tempValue+",");
                }else{
                    fields=fields+tempValue;
                }
            }
            //给默认值
            if(index==0){
		        var arr = item.get(me.columnParamsField.InteractionField).split("_");
                me.chartItemId=item.get(me.columnParamsField.InteractionField);
                
                var tempValue=item.get(me.columnParamsField.InteractionField);
                var arr=[];
                arr = tempValue.split("_");
                if(arr.length>=2){
                    //如果该字段的格式是HREmployee_HRDept_CName,截去第一个分隔符"_"前的字符串
                    tempValue = arr[arr.length-2]+"_"+arr[arr.length-1];
                }
                
                xField=""+tempValue;
                xAxesFields=""+tempValue;//图表X坐标轴字段集
                
                xTitle=item.get('text');
		        for(var i=0;i<arr.length-1;i++){
		            myClassName = myClassName + arr[i] + "_";
		        }
		        if(myClassName != ""){
		            myClassName = myClassName.substring(0,myClassName.length-1);
		        }
            }
            //给默认值
            if(index==1){
                var tempValue=item.get(me.columnParamsField.InteractionField);
                var arr=[];
                arr = tempValue.split("_");
                if(arr.length>=2){
                    //如果该字段的格式是HREmployee_HRDept_CName,截去第一个分隔符"_"前的字符串
                    tempValue = arr[arr.length-2]+"_"+arr[arr.length-1];
                }
                
                yField=""+tempValue;
                yAxesFields=""+tempValue;//图表Y坐标轴字段集
                yTitle=item.get('text');
            }
        });
        
        var getDataServerUrl=me.getEastgetDataServerUrl();
        if(getDataServerUrl&&getDataServerUrl.getValue()!=null){
              var temp2=getDataServerUrl.getValue().split("?");
              myServerUrl=temp2[0];
          }
        }
        else if(storeType=='chart'){
            var tempUrl=objectName.getValue();
            me.chartItemId=objectName.getValue();
            tempUrl=me.chartServerUrl+tempUrl;
            xAxesFields='name';
            yAxesFields='data1';
            xField='name';
            yField='data1';
            myServerUrl=tempUrl;
            fields='name,data,data1,data2,data3,data4,data5,data6,data7,data8,data9'
        }

        switch(formType) {
        case 'Line':
            seriesAxis='left';
            seriesType='line';
            xType='Category';
            yType='Numeric';
            break;
        case 'Scatter':
           seriesAxis='left';
           seriesType='scatter';
            xType='Category';
            yType='Numeric';
            break;
        case 'Gauge'://仪表图
            seriesAxis='gauge';
            seriesType='gauge';
            insetPadding=25;
            iSNeedle=true;
            xPosition='gauge';
            steps=15;
            theme="Green";
            margin=-10;//仪表图图特有,表盘的数字显示位置,负数在表盘内显示
            break;
        case 'Radar':
           seriesAxis='left';
           seriesType='radar';
            break;
        case 'Bar':
           seriesAxis='left';
           seriesType='column';           
           xType='Category';
           yType='Numeric';
           xMinimum=0;
           xMaximum=120;
           yMinimum=0;
           yMaximum=20;
           gutter=5;
           xPosition='bottom';
           yPosition='left';
            break;
        case 'Column'://水平条形图
           seriesAxis='left';
           seriesType='column';           
           xType='Category';
           yType='Numeric';
           xMinimum=0;
           xMaximum=120;
           yMinimum=0;
           yMaximum=20;
           gutter=5;
           xPosition='bottom';
           yPosition='left';
           break;
        case 'Area':
           seriesAxis='left';
           seriesType='area';
           xType='Category';
           yType='Numeric';
            break;
        case 'Pie':
           seriesAxis='left';
           seriesType='pie';
            break;
         };
         
       //新建不存在的对象
       var rec = ('Ext.data.Model',{
            DisplayName:"图表",
            InteractionField:me.chartItemId,
            Fields:fields,//图表store字段集
            Type:formType|| 'Line',//图表类型
            StoreType:storeType,
            SeriesType:formType|| 'line',//Series类型
            SeriesAxis:seriesAxis,
            InsetPadding:insetPadding,
            ISNeedle:iSNeedle,
            ShowInLegend:showInLegend,//饼图是否显示图例,true为堆积,false为分组
            HightLight:hightLight,//为true时,当移动到图表的标记上面时,会突出显示该标记
            Margin:margin,
            Gutter:gutter,
            Steps:steps,
            Theme:theme,
            ISXGrid:'false',
            ISYGrid:'false',
            ISDonut:'false',
            Stacked:false,//条形图显示类型,true为堆积,false为分组
            XMinimum: xMinimum,
            XMaximum:xMaximum,
            YMinimum:yMinimum,
            YMaximum:yMaximum,
            Width:width,//数据项宽度
            Height:height,
            ServerUrl:myServerUrl,
            XType:xType,
            YType:yType,
            XTitle:xTitle||'X坐标轴',
            YTitle:yTitle||'Y坐标轴',
            XField:xField||'',
            YField:yField||'',
            XAxesFields:xAxesFields||'',
            YAxesFields:yAxesFields||'',
            XPosition:xPosition,
            YPosition:yPosition,
            YMajorTickSteps:5,//Y主刻度的间隔步距
            XMajorTickSteps:5,//X主刻度的间隔步距
            Orentation:'horizontal',//设置图表的方向(horizontal:水平方向,vertical:垂直方向)
            XDegrees:60,//X轴标签显示方式
            YDegrees:0
        });
        store.add(rec);

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
     * 创建图表表单所有组件
     * @private
     * @return {}
     */
    createComponents:function(){
        var me = this;
        
        var arr = {
            basicComArr:[],//一般
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
//            if(me.hasBorder){
//                com.border = 1;
//            }
//            
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
        //arr = me.createComponentsXY(arr);
        //合并组件数组
        var comArr = arr.basicComArr.concat(arr.otherComArr);
        
        var coms = [];
       
        for(var i=0;i<comArr.length;i++){
            var com = comArr[i];
            
           // me.setSouthRecordByKeyValue(com.itemId,'X',com.x);
            //me.setSouthRecordByKeyValue(com.itemId,'Y',com.y);
            me.setSouthRecordByKeyValue(com.itemId,'Width',com.width);
            me.setSouthRecordByKeyValue(com.itemId,'Height',com.height);
            me.setComListeners(com);//组件监听
            coms.push(com);
        }
        return coms;
    },
    /**
     * 创建所有数据项基础属性
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
            com.heigh = record.get('Heigh');//
            com.width = record.get('Width');//

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
        if(type == 'Line'){//折线图
            com = me.createLine(record);
        }else if(type == 'Pie'){//饼状图
            com = me.createPie(record);
        }else if(type == 'Area'){//面积图
            com = me.createArea(record);
        }
        else if(type == 'Column'){//创建普通(水平/垂直)条形图
            com = me.createColumnChart(record);
        }else if(type == 'Bar'){//创建堆积/分组条形图
            com = me.createGroupBar(record);
        }else if(type == 'Scatter'){//散点图
            com = me.createScatter(record);
        }else if(type == 'Gauge'){//仪表图
            com = me.createGauge(record);
        }else if(type == 'Radar'){//雷达图
            com = me.createRadar(record);
        }
        return com;
    },
    
    /***
     * 创建折线图测试数据
     * @return {}
     */
   createLineStoreTest:function(){
	   var store = Ext.create('Ext.data.JsonStore', {
	    fields: ['name', 'data1', 'data2', 'data3', 'data4', 'data'],
	    data: [
	        { 'name': 'metric one',   'data1': '10', 'data2': '12', 'data3': '14', 'data4': '8',  'data': '13' },
	        { 'name': 'metric two',   'data1': '7',  'data2': '8',  'data3': '16', 'data4': '10', 'data': '3'  },
	        { 'name': 'metric three', 'data1': '5',  'data2': '2',  'data3': '14', 'data4': '12', 'data': '7'  },
	        { 'name': 'metric four',  'data1': '2',  'data2': '14', 'data3': '6',  'data4': '1',  'data': '11' },
	        { 'name': 'metric five',  'data1': '4',  'data2': '4',  'data3': '3', 'data4': '13', 'data': '13' }
	    ]
	   });
       return store
   },

    /**
     * 创建折线图
     * @private
     * @param {} record
     * @return {}
     */
    createLine:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=false;
       if(record.get("ISXGrid")=='true'){
         xgrid=true;
       }else{
         xgrid=false;
       }
       var ygrid=false;
       if(record.get("ISYGrid")=='true'){
         ygrid=true;
       }else{
         ygrid=false;
       }
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
        
       var xType=record.get('XType');
       var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else{
          myStore=me.createListStore(record);
       }
       var com ={
                xtype:'chart',
			    width:record.get("Width"),
			    height:record.get("Height"),
                itemId:myItemId,
			    animate: true,
			    store:myStore,
                theme:record.get('Theme'),
	            listeners:{
	                click:function(e,opets){
	                      //切换组件属性配置面板
	                    me.switchParamsPanel(myItemId);
	                }
	            },

			    axes: [
			        {
			            type:record.get('YType'),
			            position:record.get('YPosition'),
			            fields:[myYAxesFields[0]],
			            label: {
                            rotate:{degrees:record.get("YDegrees")||0},//标签名称显示方式设置
			                renderer: Ext.util.Format.numberRenderer('0,0')
			            },
			            title:record.get('YTitle'),//Y主刻度名称
			            //是否显示网格线
                        grid: ygrid,
			            minimum:record.get('YMinimum'),//Y主刻度最小值
                        maximum: record.get('YMaximum'),//Y主刻度最大值
                        majorTickSteps:record.get('YMajorTickSteps')//Y主刻度的间隔步距
			        },
			        {
			            type:xType,
			            position:record.get("XPosition"),
                        minimum:record.get('XMinimum'),//X主刻度最小值
                        maximum: record.get('XMaximum'),//X主刻度最大值
                        grid: xgrid,//是否显示网格线
			            fields:[myXAxesFields[0]],
                        label: {
                            rotate:{degrees:record.get("XDegrees")}//标签名称显示方式设置
                        },
			            title:record.get('XTitle')
			        }
			    ],
			    series: [
			        {
			            type: record.get('SeriesType'),
                        axis:record.get('SeriesAxis'),
                        xField:[myxField[0]],
                        yField:[myyField[0]],
			            highlight: {
			                size: 7,
			                radius: 7
			            },
                        tips: {
                        trackMouse: true,
                        width: 120,
                        height: 42,
                        renderer: function(storeItem, item) {
                            this.setTitle(storeItem.get(myxField[0]));
                            this.update(storeItem.get(myyField[0]));
                        }
	                    },
	                     label: {
	                          display: 'insideEnd',
	                           'text-anchor': 'middle',
	                            field: [myxField[0]],
	                            orientation: 'horizontal',
	                            fill: '#fff',
	                            font: '17px Arial'
	                        },
	                    style: {
	                        fill: '#38B8BF'
	                    },
			            markerConfig: {
			                type: 'cross',
			                size: 4,
			                radius: 4,
			                'stroke-width': 0
			            }
			        }
			    ]
			   };
        return com;
    },
   
    /**
     * 创建饼状图
     * 饼状图没有坐标,使用XAxesFields代替
     * @private
     * @param {} record
     * @return {}
     */
    createPie:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myYAxesFields=record.get('YAxesFields').split(",");
       var myXAxesFields=record.get('XAxesFields').split(",");
       var donut=record.get('ISDonut')||false;
       var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else if(storeType=='grid'){
          myStore=me.createListStore(record);
       }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【请先选择数据源类型】</b>');
            return null;
       }
        var com ={
                width:record.get('Width'),
                height:record.get('Height'),
                xtype: 'chart',
                itemId:myItemId,
                animate: true,
                store:myStore,
	            shadow: true,
	            insetPadding: 60,
	            //theme: 'Base:gradients',
	            legend: {
	                position: 'right'
	            },
                listeners:{
                    click:function(e,opets){
                          //切换组件属性配置面板
                        me.switchParamsPanel(myItemId);
                    }
                },
                
			    series: [{
                type: 'pie',
                animate: true,
                showInLegend: true,
                donut: donut,
                field:myYAxesFields[0],
                tips: {
                  trackMouse: true,
                  width: 140,
                  height: 28,
                  renderer: function(storeItem, item) {
                    var total = 0;
                    store1.each(function(rec) {
                        total += rec.get("'"+myXAxesFields[0]+"'");
                    });
                    this.setTitle(storeItem.get("'"+myYAxesFields[0]+"'") + ': ' + Math.round(storeItem.get("'"+myXAxesFields[0]+"'") / total * 100) + '%');
                  }
                },
                highlight: {
                  segment: {
                    margin: 20
                  }
                },
                label: {
                    field:myYAxesFields[0],
                    display: 'rotate',
                    font: '18px Arial',
                    contrast: true
                },                                
                renderer: function(sprite, record, attr, index, store) {
                }
            }]
    };
        return com;
    },  
    /**
     * 创建面积图
     * @private
     * @param {} record
     * @return {}
     */
    createArea:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=false;
       if(record.get("ISXGrid")=='true'){
         xgrid=true;
       }else{
         xgrid=false;
       }
       var ygrid=false;
       if(record.get("ISYGrid")=='true'){
         ygrid=true;
       }else{
         ygrid=false;
       }
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
       var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else if(storeType=='grid'){
          myStore=me.createListStore(record);
       }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【请先选择数据源类型】</b>');
            return null;
       }
       
       var colors = ['rgb(47, 162, 223)',
                  'rgb(60, 133, 46)',
                  'rgb(234, 102, 17)',
                  'rgb(154, 176, 213)',
                  'rgb(186, 10, 25)',
                  'rgb(40, 40, 40)'];

	    Ext.chart.theme.Browser = Ext.extend(Ext.chart.theme.Base, {
	        constructor: function(config) {
	            Ext.chart.theme.Base.prototype.constructor.call(this, Ext.apply({
	                colors: colors
	            }, config));
	        }
	    });
	       
       
        var donut = record.get("ISDonut");
        var com ={
            xtype: 'chart',
            itemId:myItemId,
            width:record.get('Width'),
            height:record.get('Height'),
            style: 'background:#fff',
            animate: true,
            theme: 'Browser:gradients',
            defaultInsets: 30,
            legend: {
                position: 'right'
            },
            store:myStore,
            
            listeners:{
                click:function(e,opets){
                      //切换组件属性配置面板
                    me.switchParamsPanel(myItemId);
                }
            },
            theme: record.get('Theme'),
	        axes: [
	        {//Y坐标轴
	            type:record.get('YType'),
	            grid:ygrid,
	            position: record.get('YPosition'),
	            fields: [myYAxesFields[0]],
	            title:record.get('YTitle'),
                label: {
                    rotate: {
                        degrees:record.get('YDegrees')
                    }
                },
	            minimum:record.get('YMinimum'),
                minimum:record.get('YMaximum'),
	            adjustMinimumByMajorUnit: 0
	        },
	        {
	            type:record.get('XType'),
                grid:xgrid,
                position: record.get('XPosition'),
                fields: [myXAxesFields[0]],
                title:record.get('XTitle'),
                minimum:record.get('XMinimum'),
                minimum:record.get('XMaximum'),
	            label: {
	                rotate: {
	                    degrees:record.get('XDegrees')
	                }
	            }
	        }
	    ],
	    series: [{
	        //type:record.get('SeriesType'),
	        //axis:record.get('SeriesAxis'),
            type: 'area',
            axis: 'left',
            highlight:record.get('HightLight'),
	        xField: [myxField[0]],
	        yField: [myyField[0]],
            tips: {
              trackMouse: true,
              width: 170,
              height: 28,
              renderer: function(storeItem, item) {
                  this.setTitle(storeItem.get("'"+myXAxesFields[0]+"'"));
              }
            },
            style: {
		        lineWidth: 1,
		        stroke: '#666',
		        opacity: 0.86
            }

	    }]
        };

        return com;
    },  
    /**
     * 创建普通(水平/垂直)条形图
     * @private
     * @param {} record
     * @return {}
     */
    createColumnChart:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=false;
       if(record.get("ISXGrid")=='true'){
         xgrid=true;
       }else{
         xgrid=false;
       }

       var ygrid=false;
       if(record.get("ISYGrid")=='true'){
         ygrid=true;
       }else{
         ygrid=false;
       }
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
       var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else if(storeType=='grid'){
          myStore=me.createListStore(record);
       }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【请先选择数据源类型】</b>');
            return null;
       }
       
        var com ={
	            xtype: 'chart',
	            animate: true,
                width:record.get('Width'),
                height:record.get('Height'),
	            itemId:myItemId,
	            shadow: true,
	            store:myStore,
                
                listeners:{
                click:function(e,opets){
                    me.switchParamsPanel(myItemId);
                }
                },
	            axes: [{//Y坐标轴
	                type:record.get('YType'),
	                position:record.get('YPosition'),
	                fields:[myYAxesFields[0]],
	                title:record.get('YTitle'),
	                grid:ygrid,
	                minimum:record.get('YMinimum'),
	                maximum:record.get('YMaximum'),
                    label: {
                        rotate: {
                            degrees:record.get("YDegrees"),
                            renderer: Ext.util.Format.numberRenderer('0,0')
                        }
                    }
	            },{
	                type:record.get('XType'),
                    grid:xgrid,
                    minimum:record.get('XMinimum'),
                    maximum:record.get('XMaximum'),
	                position: record.get('XPosition'),
	                fields:[myXAxesFields[0]],
	                title: record.get('XTitle'),
	                label: {
	                    rotate: {
	                        degrees:record.get('XDegrees')
	                    }
	                }
	            }],
	            series: [{
	                type:record.get('SeriesType'),
                    axis:record.get('SeriesAxis'),//声明数值在哪条坐标轴上
                    highlight:record.get('HightLight'),
	                gutter:record.get('Gutter'),
	                xField: [myxField[0]],
	                yField: [myyField[0]],
	                tips: {
	                    trackMouse: true,
	                    width:120,
	                    height: 42,
	                    renderer: function(storeItem, item) {
	                        this.setTitle(storeItem.get(myxField[0]));
	                        this.update(storeItem.get(myyField[0]));
	                    }
	                },
		             label: {
		                  display: 'insideEnd',
		                   'text-anchor': 'middle',
		                    field: [myyField[0]],
		                    orientation: 'vertical',
		                    fill: '#fff',
		                    font: '17px Arial',
                            color: '#333'
		                },
	                style: {
	                    fill: '#38B8BF'
	                }
	            }]
	        };
        return com;
    },
    /**
     * 创建(堆积/分组)条形图
     * 堆积/分组条形图的数据格式后台如何提供?
     * @private
     * @param {} record
     * @return {}
     */
    createGroupBar:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=false;
       if(record.get("ISXGrid")=='true'){
         xgrid=true;
       }else{
         xgrid=false;
       }
      
       var ygrid=false;
       if(record.get("ISYGrid")=='true'){
         ygrid=true;
       }else{
         ygrid=false;
       }
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
       var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else{
          myStore=me.createListStore(record);
       }
       
       var com ={
                xtype: 'chart',
                animate: true,
                width:record.get('Width')||460,
                height:record.get('Height')||280,
                itemId:myItemId,
                shadow: true,
                store:me.createChartStore(record),
                listeners:{
                click:function(e,opets){
                      //切换组件属性配置面板
                    me.switchParamsPanel(myItemId);
                    }
                },
	            legend: {
	              position: 'right'  
	            },
                axes: [{//Y坐标轴
                type:record.get('YType'),
                position:record.get('YPosition'),
                fields:[myYAxesFields[0]],
                minimum:record.get('YMinimum'),
                maximum:record.get('YMaximum'),
                label: {
                    renderer: Ext.util.Format.numberRenderer('0,0'),
                    rotate: {
                        degrees:record.get('YDegrees')
                    }
                },
                grid:ygrid,
                title:record.get('YTitle')
            }, {//X坐标轴
                type:record.get('XType'),
                position:record.get('XPosition'),
                fields:[myXAxesFields[0]],
                minimum:record.get('XMinimum'),
                maximum:record.get('XMaximum'),
                label: {
                    renderer: Ext.util.Format.numberRenderer('0,0'),
                     rotate: {
                        degrees:record.get('XDegrees')
                    }
                },
                grid:xgrid,
                title:record.get('XTitle')
            }],
            series: [{
                type:record.get('SeriesType'),
                axis:record.get("SeriesAxis"),
                stacked:record.get("Stacked"),//条形图显示类型,true为堆积,false为分组
                xField: [myxField[0]],
                yField: [myyField[0]]
            }]
            };
        return com;
    },
    /**
     * 创建散点图
     * @private
     * @param {} record
     * @return {}
     */
    createScatter:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=false;
       if(record.get("ISXGrid")=='true'){
         xgrid=true;
       }else{
         xgrid=false;
       }
       var ygrid=false;
       if(record.get("ISYGrid")=='true'){
         ygrid=true;
       }else{
         ygrid=false;
       }
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
   
       var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else if(storeType=='grid'){
          myStore=me.createListStore(record);
       }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【请先选择数据源类型】</b>');
            return null;
       }
       var com ={
                xtype: 'chart',
                width:record.get('Width'),
                height:record.get('Height'),
                itemId:myItemId,
                animate: true,
                store:myStore,
                listeners:{
                click:function(e,opets){
                      //切换组件属性配置面板
                    me.switchParamsPanel(myItemId);
                    }
                },
			    theme:'Category2',
			    axes: [{
			        type:record.get('YType'),
			        position:record.get('YPosition'),
			        fields:[yAxesFields],
			        title: record.get('YTitle'),
			        grid:ygrid,
			        minimum:record.get('YMinimum'),
                    maximum:record.get('YMaximum'),
	                label: {
	                    renderer: Ext.util.Format.numberRenderer('0,0'),
	                     rotate: {
	                        degrees:record.get('YDegrees')
	                    }
	                }
			    },{
			        type:record.get('XType'),
			        position:record.get('XPosition'),
			        fields:[myXAxesFields[0]],
			        title:record.get("XTitle"),
                    grid:xgrid,
                    minimum:record.get('XMinimum'),
                    maximum:record.get('XMaximum'),
                    label: {
	                    rotate: {
	                        degrees:record.get('XDegrees')
	                    }
	                }
			    }],
			    series: [{
			        type: 'scatter',
                    axis:false,
                    xField:myxField[0],
                    yField:myyField[0],
                    tips: {
                        trackMouse: true,
                        width: 120,
                        height: 42,
                        renderer: function(storeItem, item) {
                            this.setTitle(storeItem.get(myxField[0]));
                            this.update(storeItem.get(myyField[0]));
                        }
                        },
//                     label: {
//                          display: 'insideEnd',
//                           'text-anchor': 'middle',
//                            field:myyField[0],
//                            orientation: 'horizontal',
//                            fill: '#FF0000',
//                            font: '17px Arial'
//                        },
                    style: {
                        fill: '#38B8BF'
                    },
                    markerConfig: {
                        type: 'cross',
                        size: 5,
                        radius: 5,
                        'stroke-width': 0
                    }
			    }]
			};
        return com;
    },
    /**
     * 创建仪表图
     * @private
     * @param {} record
     * @return {}
     */
    createGauge:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get('Fields').split(',');
       var myxField=record.get('XField').split(',');
       var myyField=record.get('YField');

      var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else if(storeType=='grid'){
          myStore=me.createListStore(record);
       }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【请先选择数据源类型】</b>');
            return null;
       }
       var com ={
                xtype: 'chart',
                width:record.get('Width'),
                height:record.get('Height'),
                itemId:myItemId,
	            insetPadding:record.get('InsetPadding'),
                style: 'background:#fff',
	            animate: {
	                easing: 'elasticIn',
	                duration: 500
	            },
                store:myStore,
                listeners:{
                click:function(e,opets){
                      //切换组件属性配置面板
                    me.switchParamsPanel(myItemId);
                    }
                },
                theme:record.get('Theme'),
				axes: [{
				    type: 'gauge',
				    position: 'gauge',
				    minimum:record.get('XMinimum'),
				    maximum:record.get('XMaximum'),
				    steps:record.get('Steps'),
				    margin: record.get('Margin')//控制数字的显示位置,负数时会显示在表盘内,默认显示在表盘外,没有刻度
				}],
	            series: [{
	                type:'gauge',
	                field:"'"+myxField[0]+"'",
	                donut:false,
                    needle:record.get('ISNeedle'),//表盘的指针是否显示
	                colorSet: ['#82B525', '#ddd'],//设置表盘的颜色(只需设置两种)
                    
                    tips: {
                    trackMouse: true,
                    width: 120,
                    height: 42,
                    renderer: function(storeItem, item) {
                        this.setTitle(storeItem.get("'"+myxField[0]+"'"));
                    }
                    },
                    label: {
                      display: 'insideEnd',
                       'text-anchor': 'middle',
                        field: "'"+myxField[0]+"'",
                        orientation: 'horizontal',
                        fill: '#fff',
                        font: '17px Arial'
                    }
	            }]
            };
        return com;
    },
    /**
     * 创建雷达图
     * @private
     * @param {} record
     * @return {}
     */
    createRadar:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get('Fields').split(',');
       var myxField=record.get('XField').split(',');
       var myyField=record.get('YField').split(',');
       
       var myYAxesFields=record.get('YAxesFields').split(',');
       var myXAxesFields=record.get('XAxesFields').split(',');
       
       var myStore=null;
       var storeType=record.get('StoreType');
       if(storeType=='chart'){
          myStore=me.createChartStore(record);
       }else if(storeType=='grid'){
          myStore=me.createListStore(record);
       }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【请先选择数据源类型】</b>');
            return null;
       }
        var com ={
                xtype: 'chart',
                width:record.get('Width'),
                height:record.get('Height'),
                itemId:myItemId,
                animate: true,
                store:myStore,
                theme:record.get('Theme'),
                legend:{postion:"left"},
                listeners:{
                click:function(e,opets){
                      //切换组件属性配置面板
                    me.switchParamsPanel(myItemId);
                    }
                },
                
			    axes: [{
			        type: 'Radial',
			        position: 'radial',
			        label: {
			            display: true
			        }
			    }],
			    series: [{
			        type:record.get('SeriesType'),
			        xField:myxField[0],
			        yField:myyField[0],
			        showInLegend:record.get('ShowInLegend'),
			        showMarkers: true,
                    
                    tips: {
                        trackMouse: true,
                        width: 120,
                        height: 42,
                        renderer: function(storeItem, item) {
                            this.setTitle(storeItem.get(myxField[0]));
                            this.update(storeItem.get(myyField[0]));
                        }
                        },

                    markerConfig: {
                        type: 'cross',
                        size: 5,
                        radius: 5,
                        'stroke-width': 0
                    },
                    style: {
                        fill: '#38B8BF'
                    }
			    }]
			};
        return com;
    },

    //=====================组件属性面板的创建与删除=======================
    /**
     * 删除展示区域表单中的图表组件
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
         var storeType=me.getEastChartType();
        if(storeType=='grid'){
	        //去掉勾选
	        me.uncheckedObjectTreeNode('InteractionField',componentItemId);
        }
    },
    /**
     * 删除展示区域表单中的图表组件
     * @private
     * @param {} componentItemId
     */
    removeCenterComponent:function(componentItemId){
        var me = this;
        //删除数据项组件
        var center = me.getCenterCom();
        center.remove(componentItemId);
        var storeType=me.getEastChartType();
        if(storeType=='grid'){
	        //去掉勾选
	        me.uncheckedObjectTreeNode('InteractionField',componentItemId);
        }
    },
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
    /***
     * 当图表配置项或者属性值更新后
     * @param {} componentItemId
     */
    changeChart:function(componentItemId){
        var me=this;
        me.removeCenterComponent(componentItemId);
        var center=me.getCenterCom();
        var record=me.getSouthRecordByKeyValue(me.columnParamsField.InteractionField,componentItemId);
        //图表参数
        var params = me.getPanelParams();
        var chartType=params.formType;//构建图表类型
        var com=me.createComponentsByType(chartType,record);
        center.add(com);
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
        
        if(type == 'Line'){//折线图
            otherItems = me.createLineItems(componentItemId);
        }else if(type == 'Pie'){//饼状图
            otherItems = me.createPieItems(componentItemId);
        }else if(type == 'Area'){//面积图
            otherItems = me.createAreaItems(componentItemId);
        }else if(type == 'Column'){////条形图(水平/垂直)
            otherItems = me.createColumnChartItems(componentItemId);
        }else if(type == 'Bar'){//堆积/分组条形图
            otherItems = me.createGroupBarItems(componentItemId);
        }else if(type == 'Scatter'){//散点图
            otherItems = me.createScatterItems(componentItemId);
        }else if(type == 'Gauge'){//仪表图
            otherItems = me.createGaugeItems(componentItemId);
        }else if(type == 'Radar'){//雷达图
            otherItems = me.createRadarItems(componentItemId);
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
                xtype:'textfield',fieldLabel:'显示名称',name:'name',labelWidth:55,anchor:'100%',
                itemId:'name',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'DisplayName',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'textfield',fieldLabel:'X轴标题',name:'XTitle',labelWidth:55,anchor:'100%',
                itemId:'XTitle',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XTitle',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'textfield',fieldLabel:'Y轴标题',name:'YTitle',labelWidth:55,anchor:'100%',
                itemId:'YTitle',
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YTitle',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'combobox',fieldLabel:'X坐标轴显示位置',
                itemId:'XPosition',name:'XPosition',
                queryMode: 'local',
                labelWidth:115,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,
                emptyText:'请选择',
                displayField:'name',
                valueField:'value',
                //value:'left',
                store:new Ext.data.Store({ 
                        fields:['value', 'name'],
                        data :[
                            {"value":"left", "name":"左"},
                            {"value":"right", "name":"右"},
                            {"value":"top", "name":"上"},
                            {"value":"bottom", "name":"下"}
                        ]}),
                 listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XPosition',newValue);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    },
                    select:function(combo,records,eOpts ){
                        //给组件的服务地址赋值
                        var tempValue =combo.getValue();
                        me.setColumnParamsRecord(componentItemId,'XPosition',tempValue);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            
            },{
                xtype:'combobox',fieldLabel:'Y坐标轴显示位置',
                itemId:'YPosition',name:'YPosition',
                queryMode: 'local',
                labelWidth:115,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,
                emptyText:'请选择',
                displayField:'name',
                valueField:'value',
                //value:'bottom',
                store:new Ext.data.Store({ 
                        fields:['value', 'name'],
                        data :[
                            {"value":"left", "name":"左"},
                            {"value":"right", "name":"右"},
                            {"value":"top", "name":"上"},
                            {"value":"bottom", "name":"下"}
                        ]}),
                 listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YPosition',newValue);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    },
                    select:function(combo,records,eOpts ){
                        var tempValue =combo.getValue();
                        me.setColumnParamsRecord(componentItemId,'YPosition',tempValue);
                       //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            
            },{
                xtype:'numberfield',fieldLabel:'高度',name:'Height',labelWidth:55,anchor:'100%',
                itemId:'Height',value:me.defaultChartHeight,minValue:1,
                listeners:{
//                    blur:function(com,The,eOpts){
//                        me.setColumnParamsRecord(componentItemId,'Height',this.value);
//                        //重新生成展示区域的图表
//                        me.changeChart(componentItemId);
//                    },
                    change:function(owner,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Height',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
              {
                xtype:'numberfield',fieldLabel:'宽度',name:'Width',labelWidth:55,anchor:'100%',
                itemId:'Width',value:me.defaultChartWidth,minValue:1,
                listeners:{
//                    blur:function(com,The,eOpts){
//                        me.setColumnParamsRecord(componentItemId,'Width',this.value);
//                        //重新生成展示区域的图表
//                        me.changeChart(componentItemId);
//
//                    },
                    change:function(owner,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Width',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'fieldcontainer',layout:'hbox',hidden:true,
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
                                //调用生成标题字体设置组件
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
     * 折线图特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createLineItems:function(componentItemId){
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }

        var items = [
            {
            xtype:'fieldset',title:'折线图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
                {
                xtype:'radiogroup',fieldLabel:'X坐标轴网格线',itemId:'ISXGrid',name:'ISXGrid',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISXGrid2',inputValue:'true'},
                    {boxLabel:'隐藏',name:'ISXGrid2',inputValue:'false',checked:true}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISXGrid',newValue.ISXGrid2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'radiogroup',fieldLabel:'Y坐标轴网格线',itemId:'ISYGrid',name:'ISYGrid',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISYGrid2',inputValue:'true'},
                    {boxLabel:'隐藏',name:'ISYGrid2',inputValue:'false',checked:true}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISYGrid',newValue.ISYGrid2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最小显示值',name:'XMinimum',labelWidth:55,anchor:'95%',
                itemId:'XMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最大显示值',name:'XMaximum',labelWidth:55,anchor:'95%',
                itemId:'XMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最小显示值',name:'YMinimum',labelWidth:55,anchor:'95%',
                itemId:'YMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最大显示值',name:'YMaximum',labelWidth:55,anchor:'95%',
                itemId:'YMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
             {
                xtype:'numberfield',fieldLabel:'X主刻度的间隔步距',name:'XMajorTickSteps',labelWidth:55,anchor:'95%',
                itemId:'XMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMajorTickSteps',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'Y主刻度的间隔步距',name:'YMajorTickSteps',labelWidth:55,anchor:'95%',
                itemId:'YMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMajorTickSteps',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
               {
                xtype:'numberfield',fieldLabel:'X轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XDegrees',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YDegrees',value:0,minValue:1,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'折线图X坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
						        //勾选节点数组
						        var myFields =[];var fields="",tempName="";
						        var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
						        var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
		                        var data = ColumnParams.getChecked();
						        //列表中显示被勾选中的对象
						        Ext.Array.each(data,function(item, index, record){
						            if(item.get('leaf')){
						                var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
						                myFields.push(tempValue);
						                if(index<record.length-1){
						                    fields=fields+(""+tempValue+",");
                                          
						                }else{
						                    fields=fields+tempValue;
						                }
						            }
						        });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                        //treeNodeCheckedChange(node,checked);
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'折线图Y坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsY',
            items:[
               {
                xtype:'treepanel',itemId:'YAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'YAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsY');
                                var ColumnParams = dataObject.getComponent('YAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                        //treeNodeCheckedChange(node,checked);
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];

        return items;
    },
    /**
     * 面积图特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createAreaItems:function(componentItemId){
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }

        var items = [
            {
            xtype:'fieldset',title:'面积图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最小显示值',name:'XMinimum',labelWidth:55,anchor:'95%',
                itemId:'XMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最大显示值',name:'XMaximum',labelWidth:55,anchor:'95%',
                itemId:'XMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最小显示值',name:'YMinimum',labelWidth:55,anchor:'95%',
                itemId:'YMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最大显示值',name:'YMaximum',labelWidth:55,anchor:'95%',
                itemId:'YMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
             {
                xtype:'numberfield',fieldLabel:'X主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMajorTickSteps',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'Y主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMajorTickSteps',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
               {
                xtype:'numberfield',fieldLabel:'X轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XDegrees',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XDegrees',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YDegrees',value:0,minValue:1,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YDegrees',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'X坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[];var fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
                                var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                        //treeNodeCheckedChange(node,checked);
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Y坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsY',
            items:[
               {
                xtype:'treepanel',itemId:'YAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'YAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsY');
                                var ColumnParams = dataObject.getComponent('YAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项X字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesX',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesXFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesXFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesX');
                                var ColumnParams = dataObject.getComponent('SeriesXFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XField',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项Y字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesY',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesYFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesYFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesY');
                                var ColumnParams = dataObject.getComponent('SeriesYFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text"); 
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YField',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];

        return items;
    },

    /**
     * 水平/垂直条形图特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createColumnChartItems:function(componentItemId){
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }

        var items = [
            {
            xtype:'fieldset',title:'条形图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
                  {
                xtype:'radiogroup',fieldLabel:'数值显示方式',itemId:'SeriesAxis',name:'SeriesAxis',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'垂直',name:'SeriesAxis2',inputValue:'column',checked:true},
                    {boxLabel:'水平',name:'SeriesAxis2',inputValue:'bar'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
		                //垂直类型
		                //Y坐标轴
		                var  ytype='Numeric',
		                yposition='left',
		                //X坐标轴
		                xtype='Category',
		                xposition='bottom',
		                //Series
		                seriesType='column',
		                seriesAxis= 'left';
		                //水平
		                if(newValue.SeriesAxis2=='bar'){
			                ytype= 'Numeric';
			                yposition='bottom';
			
			                type='Category';
			                xposition='left';
			
			                seriesType='bar';
			                seriesAxis= 'bottom';
		                }
                        me.setColumnParamsRecord(componentItemId,'XType',xtype);
                        me.setColumnParamsRecord(componentItemId,'YType',xtype);
                        me.setColumnParamsRecord(componentItemId,'XPosition',xposition);
                        me.setColumnParamsRecord(componentItemId,'YPosition',yposition);
                        me.setColumnParamsRecord(componentItemId,'SeriesType',seriesType);
                        me.setColumnParamsRecord(componentItemId,'SeriesAxis',seriesAxis);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
                },
                {
                xtype:'radiogroup',fieldLabel:'X坐标轴网格线',itemId:'ISXGrid',name:'ISXGrid',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISXGrid2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ISXGrid2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISXGrid',newValue.ISXGrid2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'radiogroup',fieldLabel:'Y坐标轴网格线',itemId:'ISYGrid',name:'ISYGrid',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISYGrid2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ISYGrid2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISYGrid',newValue.ISYGrid2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最小显示值',name:'XMinimum',labelWidth:55,anchor:'95%',
                itemId:'XMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最大显示值',name:'XMaximum',labelWidth:55,anchor:'95%',
                itemId:'XMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最小显示值',name:'YMinimum',labelWidth:55,anchor:'95%',
                itemId:'YMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最大显示值',name:'YMaximum',labelWidth:55,anchor:'95%',
                itemId:'YMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
             {
                xtype:'numberfield',fieldLabel:'X主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMajorTickSteps',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'Y主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMajorTickSteps',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
               {
                xtype:'numberfield',fieldLabel:'X轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XDegrees',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YDegrees',value:0,minValue:1,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'X坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[];var fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
                                var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text"); 
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Y坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsY',
            items:[
               {
                xtype:'treepanel',itemId:'YAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'YAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsY');
                                var ColumnParams = dataObject.getComponent('YAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text"); 
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项X字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesX',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesXFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesXFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesX');
                                var ColumnParams = dataObject.getComponent('SeriesXFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text"); 
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XField',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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

                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项Y字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesY',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesYFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesYFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesY');
                                var ColumnParams = dataObject.getComponent('SeriesYFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YField',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];

        return items;
    },
    
    /**
     * 堆积/分组条形图特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createGroupBarItems:function(componentItemId){
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }

        var items = [
            {
            xtype:'fieldset',title:'条形图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
                  {
                xtype:'radiogroup',fieldLabel:'数值显示方式',itemId:'SeriesAxis',name:'SeriesAxis',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'垂直',name:'SeriesAxis2',inputValue:'column',checked:true},
                    {boxLabel:'水平',name:'SeriesAxis2',inputValue:'bar'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        //垂直类型
                        //Y坐标轴
                        var  ytype='Numeric',
                        yposition='left',
                        //X坐标轴
                        xtype='Category',
                        xposition='bottom',
                        //Series
                        seriesType='column',
                        seriesAxis= 'left';
                        //水平
                        if(newValue.SeriesAxis2=='bar'){
                            ytype= 'Numeric';
                            yposition='bottom';
            
                            type='Category';
                            xposition='left';
            
                            seriesType='bar';
                            seriesAxis= 'bottom';
                        }
                        me.setColumnParamsRecord(componentItemId,'XType',xtype);
                        me.setColumnParamsRecord(componentItemId,'YType',xtype);
                        me.setColumnParamsRecord(componentItemId,'XPosition',xposition);
                        me.setColumnParamsRecord(componentItemId,'YPosition',yposition);
                        me.setColumnParamsRecord(componentItemId,'SeriesType',seriesType);
                        me.setColumnParamsRecord(componentItemId,'SeriesAxis',seriesAxis);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
                },                  
                {
                xtype:'radiogroup',fieldLabel:'数值类型',itemId:'Stacked',name:'Stacked',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'堆积',name:'Stacked2',inputValue:'true',checked:true},
                    {boxLabel:'分组',name:'Stacked2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Stacked',newValue.Stacked2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
                },{
                xtype:'numberfield',fieldLabel:'X坐标轴最小显示值',name:'XMinimum',labelWidth:55,anchor:'95%',
                itemId:'XMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最大显示值',name:'XMaximum',labelWidth:55,anchor:'95%',
                itemId:'XMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最小显示值',name:'YMinimum',labelWidth:55,anchor:'95%',
                itemId:'YMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最大显示值',name:'YMaximum',labelWidth:55,anchor:'95%',
                itemId:'YMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },             
                {
                xtype:'numberfield',fieldLabel:'X主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMajorTickSteps',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'Y主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMajorTickSteps',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
               {
                xtype:'numberfield',fieldLabel:'X轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XDegrees',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YDegrees',value:0,minValue:1,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'X坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[];var fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
                                var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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

                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Y坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsY',
            items:[
               {
                xtype:'treepanel',itemId:'YAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'YAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsY');
                                var ColumnParams = dataObject.getComponent('YAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text"); 
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项X字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesX',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesXFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesXFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesX');
                                var ColumnParams = dataObject.getComponent('SeriesXFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text"); 
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XField',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项Y字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesY',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesYFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesYFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesY');
                                var ColumnParams = dataObject.getComponent('SeriesYFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YField',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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

                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];

        return items;
    },
    /**
     * 散点图特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createScatterItems:function(componentItemId){
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }

        var items = [
            {
            xtype:'fieldset',title:'散点图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
                {
                xtype:'radiogroup',fieldLabel:'X坐标轴网格线',itemId:'ISXGrid',name:'ISXGrid',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISXGrid2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ISXGrid2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISXGrid',newValue.ISXGrid2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'radiogroup',fieldLabel:'Y坐标轴网格线',itemId:'ISYGrid',name:'ISYGrid',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISYGrid2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ISYGrid2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISYGrid',newValue.ISYGrid2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最小显示值',name:'XMinimum',labelWidth:55,anchor:'95%',
                itemId:'XMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X坐标轴最大显示值',name:'XMaximum',labelWidth:55,anchor:'95%',
                itemId:'XMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最小显示值',name:'YMinimum',labelWidth:55,anchor:'95%',
                itemId:'YMinimum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMinimum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y坐标轴最大显示值',name:'YMaximum',labelWidth:55,anchor:'95%',
                itemId:'YMaximum',minValue:0,maxValue:10000,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMaximum',this.value);
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'X主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XMajorTickSteps',this.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'Y主刻度的间隔步距',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YMajorTickSteps',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YMajorTickSteps',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
               {
                xtype:'numberfield',fieldLabel:'X轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'XDegrees',value:0,minValue:-360,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'XDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'Y轴标签显示方式',name:'Height',labelWidth:55,anchor:'95%',
                itemId:'YDegrees',value:0,minValue:1,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'YDegrees',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'X坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[];var fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
                                var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Y坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsY',
            items:[
               {
                xtype:'treepanel',itemId:'YAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'YAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsY');
                                var ColumnParams = dataObject.getComponent('YAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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

                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项X字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesX',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesXFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesXFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesX');
                                var ColumnParams = dataObject.getComponent('SeriesXFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XField',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'Series配置项Y字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsSeriesY',
            items:[
               {
                xtype:'treepanel',itemId:'SeriesYFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'SeriesYFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsSeriesY');
                                var ColumnParams = dataObject.getComponent('SeriesYFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YField',fields);
                                 me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                        //treeNodeCheckedChange(node,checked);
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];

        return items;
    },
    /**
     * 饼状图特有属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createPieItems:function(componentItemId){
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }

        var items = [
            {
            xtype:'fieldset',title:'饼状图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
                {
                xtype:'radiogroup',fieldLabel:'是否显示圆环',itemId:'ISDonut',name:'ISDonut',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISDonut2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ISDonut2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISDonut',newValue.ISDonut2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
             {
                xtype:'radiogroup',fieldLabel:'是否显示图例',itemId:'ShowInLegend',name:'ShowInLegend',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ShowInLegend2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ShowInLegend2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ShowInLegend',newValue.ShowInLegend2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
            {
                xtype:'numberfield',fieldLabel:'饼图半径',name:'InsetPadding',labelWidth:55,anchor:'95%',
                itemId:'InsetPadding',value:0,minValue:1,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'InsetPadding',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'饼状图值字段',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[];var fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
                                var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'饼状图名称字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsY',
            items:[
               {
                xtype:'treepanel',itemId:'YAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'YAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsY');
                                var ColumnParams = dataObject.getComponent('YAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];
        return items;
    },
    /**
     * 仪表图特有属性createRadarItems
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createGaugeItems:function(componentItemId){
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }

        var items = [
            {
            xtype:'fieldset',title:'仪表图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
                {
                xtype:'radiogroup',fieldLabel:'是否显示圆环',itemId:'ISDonut',name:'ISDonut',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISDonut2',inputValue:'true'},
                    {boxLabel:'隐藏',name:'ISDonut2',inputValue:'false',checked:true}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISDonut',newValue.ISDonut2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },
             {
                xtype:'radiogroup',fieldLabel:'是否显示图例',itemId:'ShowInLegend',name:'ShowInLegend',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ShowInLegend2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ShowInLegend2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ShowInLegend',newValue.ShowInLegend2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'radiogroup',fieldLabel:'是否显示指针',itemId:'ISNeedle',name:'ISNeedle',
                labelWidth:115,columns:2,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISNeedle2',inputValue:'true',checked:true},
                    {boxLabel:'隐藏',name:'ISNeedle2',inputValue:'false'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISNeedle',newValue.ISNeedle2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'仪表图半径',name:'InsetPadding',labelWidth:55,anchor:'95%',
                itemId:'InsetPadding',value:0,minValue:1,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'InsetPadding',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'仪表图步距',name:'Margin',labelWidth:55,anchor:'95%',
                itemId:'Margin',value:0,minValue:1,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Steps',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'数字显示位置',name:'Margin',labelWidth:55,anchor:'95%',
                itemId:'Margin',value:0,minValue:-100,maxValue:100,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'Margin',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'突出显示效果时间',name:'HighlightDuration',labelWidth:55,anchor:'95%',
                itemId:'HighlightDuration',value:150,minValue:1,maxValue:100,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'HighlightDuration',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'仪表图取值字段',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[];var fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
                                var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];
        return items;
    },
	/**
	 * 雷达图特有属性
	 * @private
	 * @param {} componentItemId
	 * @return {}
	 */
    createRadarItems:function(componentItemId){
    
        var me = this;
        var storeType=me.getEastChartType();
        var tempBool=true;
        if(storeType=='chart'){
            tempBool=true;
        }else{
            tempBool=false;
        }
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var arr = componentItemId.split("_");
        var myClassName = "";
        for(var i=0;i<arr.length-1;i++){
            myClassName = myClassName + arr[i] + "_";
        }
        if(myClassName != ""){
            myClassName = myClassName.substring(0,myClassName.length-1);
        }
        var tree = me.getEastobjectPropertyTree();
        var myStore = tree.store;
        var root = myStore.getRootNode();
        var node = null;
        if(root.data[me.objectPropertyValueField] ==myClassName){
            node = root;
        }else{
            node = root.findChild(me.objectPropertyValueField,myClassName);
        }
        var items = [
            {
            xtype:'fieldset',title:'雷达图特有属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'ParamsOther',
            items:[
                {
                xtype:'radiogroup',fieldLabel:'是否显示圆环',itemId:'ISDonut',name:'ISDonut',
                labelWidth:55,columns:3,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISDonut2',inputValue:'show',checked:true},
                    {boxLabel:'隐藏',name:'ISDonut2',inputValue:'hide'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISDonut',newValue.ISDonut2);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            },{
                xtype:'radiogroup',fieldLabel:'是否显示指针',itemId:'ISNeedle',name:'ISNeedle',
                labelWidth:55,columns:3,vertical:true,
                items:[
                    {boxLabel:'显示',name:'ISNeedle2',inputValue:'show',checked:true},
                    {boxLabel:'隐藏',name:'ISNeedle2',inputValue:'hide'}
                ],
                listeners:{
                    change:function(com,newValue,oldValue,eOpts){
                        me.setColumnParamsRecord(componentItemId,'ISNeedle',newValue.ISNeedle2);
                    }
                }
            },            {
                xtype:'numberfield',fieldLabel:'雷达图半径',name:'InsetPadding',labelWidth:55,anchor:'95%',
                itemId:'InsetPadding',value:0,minValue:1,maxValue:360,labelWidth:115,
                listeners:{
                    blur:function(com,The,eOpts){
                        me.setColumnParamsRecord(componentItemId,'InsetPadding',this.value);
                        //重新生成展示区域的图表
                        me.changeChart(componentItemId);
                    }
                }
            }
            ]
        },{
            xtype:'fieldset',title:'雷达图X坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsX',
            items:[
               {
                xtype:'treepanel',itemId:'XAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarX',
                    items:[{
                        xtype:'button',text:'确定',itemId:'XAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[];var fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsX');
                                var ColumnParams = dataObject.getComponent('XAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text");
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'XAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'XTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        },{
            xtype:'fieldset',title:'雷达图Y坐标轴字段集',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',hidden:tempBool,
            itemId:'ParamsY',
            items:[
               {
                xtype:'treepanel',itemId:'YAxesFields',border:false,
                dockedItems:[{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbarY',
                    items:[{
                        xtype:'button',text:'确定',itemId:'YAxesFieldsOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                //勾选节点数组
                                var myFields =[],fields="",tempName="";
                                var dataObject = me.getComponent('east').getComponent(componentItemId + me.ParamsPanelItemIdSuffix).getComponent('ParamsY');
                                var ColumnParams = dataObject.getComponent('YAxesFields');//对象属性树    
                                var data = ColumnParams.getChecked();
                                //列表中显示被勾选中的对象
                                Ext.Array.each(data,function(item, index, record){
                                    if(item.get('leaf')){
                                        var tempValue=item.get(me.columnParamsField.InteractionField);
                                        tempName=item.get("text"); 
                                        myFields.push(tempValue);
                                        if(index<record.length-1){
                                            fields=fields+(""+tempValue+",");
                                        }else{
                                            fields=fields+tempValue;
                                        }
                                    }
                                });
                                //设置数据项属性列表值
                                me.setColumnParamsRecord(componentItemId,'YAxesFields',fields);
                                me.setColumnParamsRecord(componentItemId,'YTitle',tempName);
                                //重新生成展示区域的图表
                                me.changeChart(componentItemId);
                            }
                        }
                    }]
                }],
                rootVisible:true,
                nodeClassName:'',
                CName:'',
                ClassName:myClassName,
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
                    }
                },
                store:me.getParamsProperty(componentItemId)
            }
            ]
        }
        ];
        return items;
    },
    
    
//===========================加载处理后台数据================================
    /***
     * 获取对象的属性树
     * @return {}
     */
    getEastobjectPropertyTree:function(){
        var me=this;
        var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObjectOther = formParamsPanel.getComponent('dataObjectOther');
        var objectPropertyTree = dataObjectOther.getComponent('objectPropertyTree');
        return objectPropertyTree;
    },
    /***
     * 获取图形数据源的类型
     * @return {}
     */
    getEastChartTypeCom:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var storeType = center.getComponent('dataObject').getComponent('storeType');
        return storeType;
    },
     /***
     * 获取图形数据源的类型
     * @return {}
     */
    getEastChartType:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var storeType = center.getComponent('dataObject').getComponent('storeType');
        var value=storeType.getValue();
        var result=value.chart;
        return result;
    },
    /***
     * 获取图形数据对象类型
     * @return {}
     */
    getEastobjectName:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var objectName = center.getComponent('dataObject').getComponent('objectName');
        return objectName;
    },
    /***
     * 获取数据对象类型
     * @return {}
     */
    getEastdataObjectOther:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObjectOther = center.getComponent('dataObjectOther');
        return dataObjectOther;
    },
    /***
     * 获取数据对象类型
     * @return {}
     */
    getEastgetDataServerUrl:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var getDataServerUrl = center.getComponent('dataObjectOther').getComponent('getDataServerUrl');
        return getDataServerUrl;
    },
    
    /***
     * 获取数据对象类型
     * @return {}
     */
    getEastdataObject:function(){
        var me=this;
        var center = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObject = center.getComponent('dataObject');
        return dataObject;
    },
    /***
    * 获取数据对象Store的方法处理
    * @return {}
    */
    getobjectPropertyFields:function(){
        var me=this;
        var myStore2=new Ext.data.TreeStore({ 
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
                                var objectPropertyTree = me.getEastobjectPropertyTree(); 
                                
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
                });
                return myStore2;
    },
    /***
     * 获取图表坐标的数据对象Store
     * @return {}
     */
    getParamsProperty:function(componentItemId){
        var me=this;
        var tempArr=[];
        tempArr=componentItemId.split("_");
        var className=(tempArr.length>1)?(tempArr[tempArr.length-2]):(tempArr[tempArr.length-1]);
        var myUrl=me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + className;
        var myStore=new Ext.data.TreeStore({ 
                    fields:me.objectPropertyFields,
                    proxy:{
                        type:'ajax',
                        url:myUrl,
                        extractResponseData:function(response){
                            var data = Ext.JSON.decode(response.responseText);
                            if(data.ResultDataValue && data.ResultDataValue != ""){
                                var children = Ext.JSON.decode(data.ResultDataValue);
                                for(var i in children){
                                    children[i].checked = false;
                                }
                                   data[me.objectRootProperty] = children;
                                }
                            response.responseText = Ext.JSON.encode(data);
                            return response;
                        }
                    },
                    defaultRootProperty:me.objectRootProperty,
                    root:{
                        text:'对象结构',
                        leaf:false,
                        expanded:false,
                        checked:false
                    },
                    autoLoad:false
                });
                return myStore;
    },
    /**
     * 移除展示区域的表单组件及树列属性的行记录
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
         var storeType=me.getEastChartType();
         var objectPropertyTree=me.getEastobjectPropertyTree();
         
         
         if(storeType=='chart'){
         
         }else{
	         //objectPropertyTree.removeAll();
	         //objectPropertyTree.store.load();
         }
         
        
    },
    /***
     * 获取图表的查询数据地址的集合
     * @param {} componentItemId
     * @return {}
     */
    serverUrlStore:function(componentItemId){
        var me=this;
        //面板ID
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var store=new Ext.data.Store({
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
                    var serverUrl = panel.getComponent('basicParams').getComponent('basicServerUrl');
                    serverUrl.setValue(records[0].get(me.objectServerValueField));
                }
            }
        }
    });
    return store;
    },
    /***
     * 获取图表的查询数据地址的集合
     * @param {} componentItemId
     * @return {}
     */
    getServerUrlStore:function(componentItemId){
        var me = this;
        var strWhere=me.dictionaryListServerParam + "=" + componentItemId;
        var chartType=me.getEastChartType();
        if(chartType=='chart'){
            strWhere=me.objectServerParam + "=Chart";
        }else{
            strWhere=me.dictionaryListServerParam + "=" + componentItemId;
        }
        var localData=[];
        Ext.Ajax.request({
            async:false,//非异步
            url:me.dictionaryListServerUrl + "?" + me.dictionaryListServerParam + "=" + componentItemId,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                
                var ResultDataValue = {count:0,list:[]};
                if(result["ResultDataValue"] && result["ResultDataValue"] != ""){
                    ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                }
                localData=ResultDataValue;
         
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

    /***
     * 创建获取普通列表图表的数据
     * @param {} record
     * @param {}HQL参数:hqlWhere
     * @return {}
     */
   createListStore:function(record,hqlWhere){
        var myUrl=record.get("ServerUrl");
        var myFields=[];
        myFields=record.get("Fields").split(",");
        if(myUrl!=""){
            myUrl=getRootPath() + "/"+myUrl+"?isPlanish=true&where=";
        }else if(storeType=='grid'){
          myStore=me.createListStore(record);
       }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【请先选择数据源类型】</b>');
            return null;
       }
        var localData=null;
        
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
               var result=result["ResultDataValue"];
               var ResultDataValue =(result!="")?(Ext.JSON.decode(result)):("");
               var count = ResultDataValue['count'];
               var lists=ResultDataValue['list'];
               var itemArr=[];
            Ext.Array.each(lists,function(list, index, countriesItSelf){
                var tempJson={};
                for(var i=0;i<myFields.length;i++){
                var keyName=myFields[i];
                var value=list[myFields[i]];
                tempJson[keyName]=value;
                }
               itemArr.push(tempJson);
            });
               var store = Ext.create('Ext.data.JsonStore', {
                fields:myFields,
                data:itemArr
               });
                localData=store;
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
   /***
     * 创建获取图表的数据
     * @param {} record
     * @param {}HQL参数:hqlWhere
     * @return {}
     */
   createChartStore:function(record,hqlWhere){
        var myUrl=record.get("ServerUrl");
        var myFields=[];
        myFields=record.get("Fields").split(",");
        var strWhere="bspecialty.Name!=''";
        if(hqlWhere&&(hqlWhere!='undefined'||hqlWhere!=null)){
           strWhere=hqlWhere;
        }else{
            strWhere='';
            //测试
//            if(myUrl=='/SingleTableService.svc/ST_UDTO_SearchBSpecialtyChartByHQL'){
//            strWhere="bspecialty.Name!=''";
//            }else{
//            strWhere='';
//            }
        }
        if(myUrl!=""){
            myUrl=getRootPath() + "/"+myUrl+"?isPlanish=true&where=";
        }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【没有配置获取数据服务地址！】</b>');
            return null;
        }
        var localData=null;
        
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            params :strWhere,
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
               var tempResult='{'+result['ResultDataValue']+'}';
               var ResultDataValue =(tempResult!="")?(Ext.JSON.decode(tempResult)):("");
               var lists=ResultDataValue['data'];
               var itemArr=[];
               itemArr=lists;
               var store = Ext.create('Ext.data.JsonStore', {
                fields:myFields,
                data:itemArr
               });
                localData=store;
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
        Ext.Array.each(array,function(obj,index, countriesItSelf){
            if(index==0){
            me.chartItemId=obj.InteractionField;
            }
            var rec = ('Ext.data.Model',obj);
            me.addSouthValueByRecord(rec);//添加组件记录
        });
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
            me.setOtherParamsPanelValuesByType(componentItemId,record);
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
        var myXTitle=basic.getComponent("XTitle");
        var myYTitle = basic.getComponent("YTitle");
        var myWidth = basic.getComponent("Width");
        var myHeight = basic.getComponent("Height");
        
        var myXPosition = basic.getComponent("XPosition");
        var myYPosition = basic.getComponent("YPosition");
        
        myXTitle.setValue(record.get('XTitle'));
        myYTitle.setValue(record.get('YTitle'));
        myWidth.setValue(record.get('Width'));
        myHeight.setValue(record.get('Height'));
        name.setValue(record.get('DisplayName'));
        
        myXPosition.setValue(record.get('XPosition'));
        myYPosition.setValue(record.get('YPosition'));
    },
    /**
     * 属性面板特有数据项赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setOtherParamsPanelValuesByType:function(componentItemId,record){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        //组件属性面板
        var panel = me.getComponent('east').getComponent(panelItemId);
        var basic = panel.getComponent("ParamsOther");
        var mytype=record.get('Type');
        if(mytype=='Line'||mytype=='Scatter'||mytype=='Column'){
        var myXMinimum = basic.getComponent("XMinimum");
        var myXMaximum=basic.getComponent("XMaximum");
        var myYMinimum = basic.getComponent("YMinimum");
        var myYMaximum = basic.getComponent("YMaximum");

        myXMinimum.setValue(record.get('XMinimum'));
        myXMaximum.setValue(record.get('XMaximum'));
        myYMinimum.setValue(record.get('YMinimum'));
        myYMaximum.setValue(record.get('YMaximum'));
        
        var myXMajorTickSteps = basic.getComponent("XMajorTickSteps");
        var myYMajorTickSteps=basic.getComponent("YMajorTickSteps");
        var myXDegrees = basic.getComponent("XDegrees");
        var myYDegrees = basic.getComponent("YDegrees");

        myXMajorTickSteps.setValue(record.get('XMajorTickSteps'));
        myYMajorTickSteps.setValue(record.get('YMajorTickSteps'));
        myXDegrees.setValue(record.get('XDegrees'));
        myYDegrees.setValue(record.get('YDegrees'));
        
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
            },
            click:{
                element:'el',
                fn:function(e){
                    //切换组件属性配置面板
                    me.switchParamsPanel(com.itemId);
                }
            },
            titlechange:function(title, index, eOpts ){
                me.switchParamsPanel(com.itemId);
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
        var storeType=me.getEastChartType();
        if(storeType=='grid'){
	        var treeStore =me.getEastobjectPropertyTree().store;
	        var items = treeStore.lastOptions.node.childNodes[0].store;
	        var record = items.findRecord(key,value);
	        if(record != null){
	            record.set('checked',false);
	            record.commit();
	        }
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

                    {name:'Type',type:'string'},//图表类型
                    {name:'XType',type:'string'},//X坐标轴类型
                    {name:'YType',type:'string'},//X坐标轴类型
                    {name:'Width',type:'int'},//数据项宽度
                    {name:'Height',type:'int'},//高度
                    {name:'Theme',type:'string'},//主题
                    
                    //图表坐标轴的配置项的定义
                    {name:'ISXGrid',type:'string'},//X坐标轴网格线
                    {name:'ISYGrid',type:'string'},//Y坐标轴网格线
                    {name:'XTitle',type:'string'},//X坐标轴显示名称
                    {name:'XType',type:'string'},//X坐标轴显示类型
                    {name:'XPosition',type:'string'},//X坐标轴显示位置,left,bottom,right,top

                    {name:'XMinimum',type:'int'},//X坐标轴最小显示值
                    {name:'XMaximum',type:'int'},//X坐标轴最大显示值
                    {name:'XDateFormat',type:'string'},//X坐标轴显示日期格式
                    {name:'XLabel',type:'string'},//X坐标轴刻度上的标签的显示方式设置
                    
                    {name:'YTitle',type:'string'},//Y坐标轴显示名称
                    {name:'YType',type:'string'},//Y坐标轴显示类型
                    {name:'YPosition',type:'string'},//Y坐标轴显示位置

                    {name:'YMinimum',type:'int'},//Y坐标轴最小显示值
                    {name:'YMaximum',type:'int'},//Y坐标轴最大显示值
                    {name:'YDateFormat',type:'int'},//Y坐标轴显示日期格式
                    {name:'YLabel',type:'string'},//Y坐标轴刻度上的标签的显示方式设置
                    
                    //Series的配置项的定义(饼图没有XField,YField,Axis)
                    {name:'SeriesType',type:'string'},//Series类型
                    {name:'HightLight',type:'bool'},//为true时,当移动到图表的标记上面时,会突出显示该标记
                    {name:'Orentation',type:'string'},//设置图表的方向(horizontal:水平方向,vertical:垂直方向)
                    
                    //折线图配置项
                    {name:'XField',type:'string'},//Series的X字段
                    {name:'YField',type:'string'},//Series的Y字段
                    {name:'SeriesAxis',type:'string'},//声明数值在哪条坐标轴上
                    
                    //图例对象Legend的配置项,json对象,需要单独提供
                    //包括设置的项有boxFill,position:(top,bottom,right,float(需要设置x,y)),visible,x,y(xy,需要position设置为float才生效)
                    {name:'Legend',type:'string'},//图例对象设置
                    
                    //条形图Bar配置项
                    {name:'Column',type:'bool'},//条形图显示方式,true为水平,false为垂直方式
                    {name:'Stacked',type:'bool'},//条形图显示类型,true为堆积,false为分组
                    
                    //饼(仪表)图Pie配置项
                    {name:'ISDonut',type:'string'},//是否显示圆环图,true为是,false为否
                    {name:'ShowInLegend',type:'bool'},//饼图是否显示图例,true为堆积,false为分组
                    {name:'InsetPadding',type:'int'},//饼图半径
                    {name:'ISNeedle',type:'bool'},//仪表图图特有,表盘的指针设置,true:显示Margin
                    {name:'Margin',type:'int'},//仪表图图特有,表盘的数字显示位置,负数在表盘内显示
                    {name:'Steps',type:'int'},//仪表图图特有,仪表图的步距
                    {name:'HighlightDuration',type:'int'},//仪表图图特有,仪表图的突出显示效果时间
                    
                    {name:'ServerUrl',type:'string'},//数据地址fields
                    
                    {name:'Fields',type:'string'},//图表总字段集
                    {name:'XAxesFields',type:'string'},//图表X坐标轴字段集
                    {name:'YAxesFields',type:'string'},//图表Y坐标轴字段集
                    {name:'XMajorTickSteps',type:'int'},//X主刻度的间隔步距
                    {name:'YMajorTickSteps',type:'int'},//Y主刻度的间隔步距
                    {name:'XDegrees',type:'int'},//X轴标签显示方式
                    {name:'YDegrees',type:'int'},//Y轴标签显示方式
                    {name:'Gutter',type:'int'}//设置两个条形图之间的空白间隔
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
            me.setObjData();
            me.setSouthRecordByArray(southParams);
            me.setPanelParams(panelParams);

            var dataObject =me.getEastdataObject();
            var dataObjectOther =me.getEastdataObjectOther();
            
	        //获取获取数据服务列表
	        var getDataServerUrl =me.getEastgetDataServerUrl();
	        var objectName =me.getEastobjectName();
            
	        var strWhere='';
	        var chartType=me.getEastChartType();
	        if(chartType=='chart'){
                var record=southParams[0];
                objectName.value =record.InteractionField;
	            //strWhere=me.objectServerParam + "=Chart";
	        }else{
                //对象内容勾选
                var tree =me.getEastobjectPropertyTree();
                tree.hide();
                tree.store.on({
                load:function(store,node,records,successful,e){
                    if(me.appId != -1 && me.isJustOpen && node == tree.getRootNode()){
                        //对象内容勾选
                        me.changeObjChecked(southParams);
                    }
                }
            });
	        }
	        //
            getDataServerUrl.value = panelParams.getDataServerUrl;
            //渲染效果
            me.browse();
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
        //数据对象类
        var objectName = me.getEastobjectName();
        objectName.store.load();
    },
    /**
     * 对象内容勾选
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
    /***
     * 生成保存图表的图片的类代码方法
     * @return {}
     */
    createsaveImageStr:function(){
        var me=this;
        var fun = 
        "function(){" + 
            "var form = this;" + 
            "var myitem=form.items.items;"+
            "var length=form.items.length;"+
            "for(var i=0;i<length;i++){"+
            "var ob =myitem[i];"+
            "var myType= ob.xtype;"+
            "if(myType === 'chart'){"+
                "Ext.MessageBox.confirm('确认保存', '你是否需要下载保存图片?', function(choice){"+
                    "if(choice == 'yes'){"+
                        "var chart =ob;"+
                        "chart.save({"+
                            "type: 'image/png'"+
                        "});"+
                    "}"+
                    "return;"+
                "});"+
                
            "}"+
            
            "}"+
  
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

        //内部组件代码
        var items = me.createComponentsStr();
        
        var formType=params.formType;
        //加载图表数据方法
        var loadData = me.createLoadDataStr(params.getDataServerUrl);
        
        //保存图表图片方法
        var saveImage = me.createsaveImageStr();
        
        //----------------------------------------------------------------------
        //背景html
        var html = params.formHtml;
        
        var storeType=me.getEastChartType();
       
        var appClass = 
        "Ext.define('" + params.appCode + "',{" + 
            "extend:'Ext.form.Panel'," + 
            "alias:'widget." + params.appCode + "'," + 
            "title:'" + params.titleText + "'," + 
            "width:" + params.Width + "," + 
            "height:" + params.Height + "," + 
            "autoScroll:true," + 
            "hqlWhere:''," +//数据源接收HQL参数
            "type:'add'," + //显示方式add（新增）、edit（修改）、show（查看）
            "layout:'absolute'," + 
            //hql功能后台未提供实现
             "internalWhere:''," + //内部hql
             "externalWhere:''," + //外部hql
                
            //创建内部图表组件获取数据源的Str
            ""+me.createComponentsStoreStr(storeType)+", "+ 
            
            //保存图表图片方法
            "saveImage:" + saveImage + ","+
            
            "initComponent:function(){" + 
                "var me=this;" + 
                //注册事件
                "me.addEvents('saveImageClick');" + 
                //对外公开方法

                //内部数据匹配方法
                "me.changeStoreData=function(response){" + 
                    "var data = Ext.JSON.decode(response.responseText);" + 
                    "var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);" + 
                    "data.ResultDataValue = ResultDataValue;" +
                    "data.List = ResultDataValue.List;" + 
                    "response.responseText = Ext.JSON.encode(data);" + 
                    "return response;" + 
                "};" + 
            
            //内部组件
            "me.items=" + items + ";";
            
            if(html!=undefined&&html != ""){
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

                "}" + 
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
        "function(id){" + 
            "Ext.Ajax.request({" + 
                "async:false," + 
                "url:getRootPath()+'/" + url + "&id='+id," + 
                "method:'GET'," + 
                "timeout:5000," + 
                "success:function(response,opts){" + 
                    "var result=Ext.JSON.decode(response.responseText);" + 
                    "if(result.success){" + 
                        "var data=Ext.JSON.decode(response.responseText);" + 
                        "var values=Ext.JSON.decode(data.ResultDataValue);" + 
                        "me.getForm().setValues(values);" + 
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
                    me.fireEvent('saveAsClick');
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
        var browse = north.getComponent('browse');
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
                    me.save();
                    me.fireEvent('saveClick');
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
        //标题字体设置监听
        //me.initTitleListener();
        //数据对象列表监听
        me.objectChange();
        //对象所有属性树监听
        //me.initObjectPropertyTreeListener();
        //查询项配置图标监听
        me.initSearchConfiguration();
    },

    /**
     * 查询项配置图标监听
     * @private
     */
    initSearchConfiguration:function(){
        var me = this;
        var listParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var configuration = listParamsPanel.getComponent('searchSet').getComponent('searchBar').getComponent('configuration');
        if(configuration){
            configuration.on({ 
                click:{
                    element:'el',
                    fn:function(){
                        alert("设置查询");
                    }
                }
            });
        }
    },
    /**
     * 对象所有属性树监听
     * @private
     */
    initObjectPropertyTreeListener:function(){},
    //===========================加载后台数据结束================================
   
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
                      me.setComponentLabFont(componentItemId,record,lastValue);
                   
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
    //===========================图表组件的类代码生成====================================
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
     * 类生成代码:创建内部图表组件获取数据源的Str
     * @private
     * @return {}
     */
    createComponentsStoreStr:function(storeType){
        var me = this;
        var records = me.getSouthRecords();
        var strStore = "";
        for(var i in records){
            var record = records[i];
            var fnName=""+record.get(me.objectPropertyValueField)+"Store";//函数名称:用行的itemId命名+后缀"Store"
            
            var fnStr ='';
            if(storeType=='grid'){
                fnStr =me.createListChartStoreStr(record);//获取图表的列表数据格式函数体
            }else if(storeType=='chart'){
                fnStr =me.createChartStoreStr(record);//获取图表数据格式函数体
            }
            strStore = strStore + (fnName+ ":"+fnStr + ",");
        }
        if(strStore.length > 1){
            strStore = strStore.substring(0,strStore.length-1);
        }
        
        return strStore;
    },
    /**
     * 根据组件类型生成组件类代码
     * @private
     * @param {} type
     * @param {} record
     * @return {}
     */
    createComStrByType:function(record){
        var me = this;
        var com = null;
        var type = record.get('Type');
        if(type == 'Line'){//折线图
            com = me.createLineStr(record);
        }else if(type == 'Pie'){//饼状图
            com = me.createPieStr(record);
        }else if(type == 'Area'){//面积图
            com = me.createAreaStr(record);
        }
        else if(type == 'Column'){//创建普通(水平/垂直)条形图
            com = me.createColumnChartStr(record);
        }else if(type == 'Bar'){//创建堆积/分组条形图
            com = me.createGroupBarStr(record);
        }else if(type == 'Scatter'){//散点图
            com = me.createScatterStr(record);
        }else if(type == 'Gauge'){//仪表图
            com = me.createGaugeStr(record);
        }else if(type == 'Radar'){//雷达图
            com = me.createRadarStr(record);
        }
        return com;
    },
    /***
     * 创建获取图表的数据
     * @param {} record
     * @return {}
     */
    createListChartStoreStr:function(record){
    
        var myUrl=record.get("ServerUrl");
        var myFields=[],fields="";
        myFields=record.get("Fields").split(',');
        for(var i=0;i<myFields.length;i++)
        {
        fields=(i<myFields.length-1)?(fields+("'"+myFields[i]+"',")):(fields+("'"+myFields[i]+"'"));
        }
        if(myUrl!=""){
            myUrl=""+myUrl+"?isPlanish=true&where=";
        }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【没有配获取数据服务地址！】</b>');
            return null;
        }
        var getStoreStr="";
        getStoreStr="function(hqlWhere){"+
        
        "var localData=null;"+
        "var myUrl2='"+myUrl+"';"+
        "var strWhere='';"+
        "if(hqlWhere&&(hqlWhere!='undefined'||hqlWhere!=null)){"+
           "strWhere=hqlWhere;"+
        "}else{"+
        
        "strWhere='';"+
//            //测试
//            "if(myUrl2=='/SingleTableService.svc/ST_UDTO_SearchBSpecialtyChartByHQL'){"+
//            "strWhere='';"+
//            "}else{"+
//            
//            "}"+
        "}"+
        
        "var myFields=[];"+
        "var tempFile='"+record.get("Fields")+"';"+
        "myFields=tempFile.split(',');"+
        "Ext.Ajax.request({"+
            "async:false,"+
            "url:getRootPath()+'/'+"+"'"+myUrl+"',"+
            "method:'GET',"+
            "params :strWhere,"+
            "timeout:5000,"+
            "success:function(response,opts){"+
            "var result = Ext.JSON.decode(response.responseText);"+
            "if(result.success){"+
               "var result=result['ResultDataValue'];"+
               
               "var ResultDataValue =(result!='')?(Ext.JSON.decode(result)):('');"+
               "var count = ResultDataValue['count'];"+
               "var lists=ResultDataValue['list'];"+
               "var itemArr=[];"+
            "Ext.Array.each(lists,function(list, index, countriesItSelf){"+
                "var tempJson={};"+
                "for(var i=0;i<myFields.length;i++){"+
                "var keyName=myFields[i];"+
                "var value=list[myFields[i]];"+
                "tempJson[keyName]=value;"+
                "}"+
               "itemArr.push(tempJson);"+
            "});"+
               "var store = Ext.create('Ext.data.JsonStore', {"+
                "fields:["+fields+"],"+
                "data:itemArr"+
               "});"+
                "localData=store;"+
                "}else{"+
                    "Ext.Msg.alert('提示','获取信息失败！');"+
                "}"+
            "},"+
            "failure : function(response,options){ "+
                "Ext.Msg.alert('提示','获取信息请求失败！');"+
            "}"+
        "});"+
        "return localData;"+
   "}"
   return getStoreStr;
   },
   /***
     * 创建获取图表的数据
     * @param {} record
     * @return {}
     */
    createChartStoreStr:function(record){
    
        var myUrl=record.get("ServerUrl");
        var myFields=[],fields="";
        myFields=record.get("Fields").split(',');
        for(var i=0;i<myFields.length;i++)
        {
        fields=(i<myFields.length-1)?(fields+("'"+myFields[i]+"',")):(fields+("'"+myFields[i]+"'"));
        }
        if(myUrl!=""){
            myUrl=""+myUrl+"?isPlanish=true&where=";
        }else{
            Ext.Msg.alert('提示','<b style="color:red">'+'【没有配获取数据服务地址！】</b>');
            return null;
        }
        var getStoreStr="";
        getStoreStr="function(hqlWhere){"+
        
        "var localData=null;"+
        "var myUrl2='"+myUrl+"';"+
        "var strWhere='';"+
        "if(hqlWhere&&(hqlWhere!='undefined'||hqlWhere!=null)){"+
           "strWhere=hqlWhere;"+
        "}else{"+
            //测试
            "if(myUrl2=='/SingleTableService.svc/ST_UDTO_SearchBSpecialtyChartByHQL'){"+
            "strWhere='';"+
            "}else{"+
            "strWhere='';"+
            "}"+
        "}"+
        
        "var myFields=[];"+
        "var tempFile='"+record.get("Fields")+"';"+
        "myFields=tempFile.split(',');"+
        "Ext.Ajax.request({"+
            "async:false,"+
            "url:getRootPath()+'/'+"+"'"+myUrl+"',"+
            "method:'GET',"+
            "params :strWhere,"+
            "timeout:5000,"+
            "success:function(response,opts){"+
            "var result = Ext.JSON.decode(response.responseText);"+
            "if(result.success){"+
               "var tempResult='{'+result['ResultDataValue']+'}';"+
               "var ResultDataValue =(tempResult!='')?(Ext.JSON.decode(tempResult)):('');"+
               "var lists=ResultDataValue['data'];"+
               "var itemArr=[];"+
               "itemArr=lists;"+
               
               "var store = Ext.create('Ext.data.JsonStore', {"+
                "fields:["+fields+"],"+
                "data:itemArr"+
               "});"+
                "localData=store;"+
                "}else{"+
                    "Ext.Msg.alert('提示','获取信息失败！');"+
                "}"+
            "},"+
            "failure : function(response,options){ "+
                "Ext.Msg.alert('提示','获取信息请求失败！');"+
            "}"+
        "});"+
        "return localData;"+
   "}"
   return getStoreStr;
   },
    /**
     * 创建折线图类代码
     * @private
     * @param {} record
     * @return {}
     */
    createLineStr:function(record){
       var me=this;
       
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=record.get("ISXGrid")||false;
       var ygrid=record.get("ISYGrid")||false;
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
        
       var myStore="me."+myItemId+"Store()";
       var com ="";
       
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate:"+ true+","+
                "store:"+myStore+","+
                "theme:'"+ record.get('Theme')+"',"+
                
                "axes: ["+ 
                    "{"+
                       "type:'"+record.get('YType')+"',"+
                        "position:'"+record.get('YPosition')+"',"+
                        "fields:["+yAxesFields+"],"+
                        "label: {"+
                            "rotate:{degrees:"+record.get("YDegrees")+"},"+
                            "renderer: Ext.util.Format.numberRenderer('0,0')"+
                        "},"+
                        "title:'"+record.get('YTitle')+"',"+
                        "grid:"+ygrid+","+
                        "minimum:"+record.get('YMinimum')+","+
                        "maximum: "+record.get('YMaximum')+","+
                        "majorTickSteps:"+record.get('YMajorTickSteps')+
                    "},"+
                    "{"+
                        "type:'"+record.get('XType')+"',"+
                        "position:'"+record.get("XPosition")+"',"+
                        "grid:"+xgrid+","+
                        "fields:["+xAxesFields+"],"+
                        "label: {"+
                            "rotate:{degrees:"+record.get("XDegrees")+"}"+
                        "},"+
                        "title:'"+record.get('XTitle')+"'"+
                    "}"+
                "],"+
                
                "series: ["+
                    "{"+
                        "type:'"+ record.get('SeriesType')+"',"+
                        "axis:'"+record.get('SeriesAxis')+"',"+
                        "xField:["+xField+"],"+
                        "yField:["+yField+"],"+
                        "highlight: {"+
                            "size: 7,"+
                            "radius: 7"+
                        "},"+
                        "tips: {"+
                        "trackMouse: true,"+
                        "width: 120,"+
                        "height: 42,"+
                        "renderer: function(storeItem, item) {"+
                            "this.setTitle(storeItem.get('"+myxField+"'));"+
                            "this.update(storeItem.get('"+myyField+"'));"+
                        "}"+
                        "},"+
                         "label: {"+
                              "display: 'insideEnd',"+
                               "'text-anchor': 'middle',"+
                                "field:['"+ myyField[0]+"'],"+
                                "orientation: 'horizontal',"+
                                "fill: '#fff',"+
                                "font: '17px Arial'"+
                            "},"+
                        "style: {"+
                            "fill: '#38B8BF'"+
                        "},"+
                        "markerConfig: {"+
                            "type: 'cross',"+
                            "size: 4,"+
                            "radius: 4,"+
                           " 'stroke-width': 0"+
                        "}"+
                        
                    "}"+
                "]"+
              "}"
        return com;
    },  
    /**
     * 创建散点图类代码
     * @private
     * @param {} record
     * @return {}
     */
    createScatterStr:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=record.get("ISXGrid")||false;
       var ygrid=record.get("ISYGrid")||false;
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
       var myStore="me."+myItemId+"Store()";
       var com ="";
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate:"+ true+","+
                "store:"+myStore+","+
                "theme:'"+ record.get('Theme')+"',"+
                
                "axes: ["+ 
                    "{"+
                    "type:'"+record.get('YType')+"',"+
                    "position:'"+record.get('YPosition')+"',"+
                    "fields:['"+myYAxesFields[0]+"'],"+
                    "title:'"+record.get('YTitle')+"',"+
                    "grid:"+record.get('ISYGrid')+","+
                    "minimum:"+record.get('YMinimum')+","+
                    "maximum:"+record.get('YMaximum')+","+
                    "label: {"+
                        "renderer: Ext.util.Format.numberRenderer('0,0'),"+
                         "rotate: {"+
                            "degrees:"+record.get('YDegrees')+
                        "}"+
                    "}"+
                "},{"+
                    "type:'"+record.get('XType')+"',"+
                    "position:'"+record.get('XPosition')+"',"+
                    "fields:['"+myXAxesFields[0]+"'],"+
                    "title:'"+record.get("XTitle")+"',"+
                    "grid:"+record.get('ISXGrid')+","+
                    "minimum:"+record.get('XMinimum')+","+
                    "maximum:"+record.get('XMaximum')+","+
                    "label: {"+
                        "rotate: {"+
                            "degrees:"+record.get('XDegrees')+
                        "}"+
                    "}"+
                "}],"+
                "series: [{"+
                    "type:'"+ record.get('SeriesType')+"',"+
                    "axis:'"+record.get("SeriesAxis")+"',"+
                    "xField:['"+myxField[0]+"'],"+
                    "yField:['"+myyField[0]+"'],"+
                    "tips: {"+
                        "trackMouse: true,"+
                        "width: 120,"+
                        "height: 42,"+
                        "renderer: function(storeItem, item) {"+
                            "this.setTitle(storeItem.get('"+myxField[0]+"'));"+
                            "this.update(storeItem.get('"+myyField[0]+"'));"+
                        "}"+
                        "},"+
                     "label: {"+
                          "display: 'insideEnd',"+
                           "'text-anchor': 'middle',"+
                            "field:['"+myyField[0]+"'],"+
                            "orientation: 'horizontal',"+
                            "fill: '#FF0000',"+
                            "font: '17px Arial'"+
                        "},"+
                    "style: {"+
                        "fill: '#38B8BF'"+
                    "},"+
                    "markerConfig: {"+
                        "type: 'cross',"+
                        "size: 5,"+
                        "radius: 5,"+
                        "'stroke-width': 0"+
                    "}"+
               " }]"+
            "}";
        return com;
    },
    /**
     * 创建仪表图类代码
     * @private
     * @param {} record
     * @return {}
     */
    createGaugeStr:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get('Fields').split(',');
       var myxField=record.get('XField').split(',');
       var myyField=record.get('YField').split(',');;

       var myStore="me."+myItemId+"Store()";
       var com ="";
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate: true,"+
                "store:"+myStore+","+
                "theme:'"+ record.get('Theme')+"',"+
                
                "axes: ["+ 
                    "{"+
                    "type: 'gauge',"+ 
                    "position: 'gauge',"+ 
                    "minimum:"+record.get('XMinimum')+","+ 
                    "maximum:"+record.get('XMaximum')+","+ 
                    "steps:"+record.get('Steps')+","+ 
                    "margin:"+record.get('Margin')+ 
                "}],"+ 
                "series: [{"+ 
                    "type:'gauge',"+ 
                    "field:"+"'"+myxField[0]+"',"+
                    "donut:false,"+
                    "needle:"+record.get('ISNeedle')+","+
                    "colorSet: ['#82B525', '#ddd'],"+ 
                    
                    "tips: {"+ 
                    "trackMouse: true,"+ 
                    "width: 120,"+ 
                    "height: 42,"+ 
                    "renderer: function(storeItem, item) {"+ 
                        "this.setTitle(storeItem.get("+"'"+myxField[0]+"'"+"));"+
                    "}"+
                    "},"+
                    "label: {"+
                      "display: 'insideEnd',"+
                       "'text-anchor': 'middle',"+
                        "field:"+"'"+myxField[0]+"',"+
                        "orientation: 'horizontal',"+
                        "fill: '#fff',"+
                        "font: '17px Arial'"+
                    "}"+
                "}]"+
            "}";
        return com;
    },
    /**
     * 创建雷达图类代码
     * @private
     * @param {} record
     * @return {}
     */
    createRadarStr:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get('Fields').split(',');
       var myxField=record.get('XField').split(',');
       var myyField=record.get('YField').split(',');
       var myYAxesFields=record.get('YAxesFields').split(',');
       var myXAxesFields=record.get('XAxesFields').split(',');
      
       var myStore="me."+myItemId+"Store()";
       var com ="";
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate: true,"+
                "store:"+myStore+","+
                "theme:'"+ record.get('Theme')+"',"+
                
                "axes: ["+ 
                    "{"+
                    
                    "type: 'Radial',"+
                    "position: 'radial',"+
                    "label: {"+
                        "display: true"+
                    "}"+
                "}],"+
                "series: [{"+
                    "type:'"+record.get('SeriesType')+"',"+
                    "xField:['"+myxField[0]+"'],"+
                    "yField:['"+myyField[0]+"'],"+
                    "showInLegend:"+record.get('ShowInLegend')+","+
                    "showMarkers: true,"+
                    
                    "tips: {"+
                        "trackMouse: true,"+
                        "width: 120,"+
                        "height: 42,"+
                        "renderer: function(storeItem, item) {"+
                            "this.setTitle(storeItem.get('"+myxField[0]+"'));"+
                            "this.update(storeItem.get('"+myyField[0]+"'));"+
                        "}"+
                        "},"+
                    "style: {"+
                        "fill: '#38B8BF'"+
                    "},"+
                    "markerConfig: {"+
                        "type: 'cross',"+
                        "size: 5,"+
                        "radius: 5,"+
                        "'stroke-width': 0"+
                   "}"+
                "}]"+
            "}";
        return com;
    },    
    /**
     * 创建普通(水平/垂直)条形图
     * @private
     * @param {} record
     * @return {}
     */
    createColumnChartStr:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=record.get("ISXGrid")||false;
       var ygrid=record.get("ISYGrid")||false;
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
       var myStore="me."+myItemId+"Store()";
       var com ="";
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate: true,"+
                "store:"+myStore+","+
                "theme:'"+ record.get('Theme')+"',"+
                
                "axes: ["+ 
                    "{"+
                    "type:'"+record.get('YType')+"',"+
                    "position:'"+record.get('YPosition')+"',"+
                    "fields:['"+myYAxesFields[0]+"'],"+
                    "title:'"+record.get('YTitle')+"',"+
                    "grid:"+record.get('ISYGrid')+","+
                    "minimum:"+record.get('YMinimum')+","+
                    "maximum:"+record.get('YMaximum')+","+
                    "label: {"+
                        "rotate: {"+
                            "degrees:"+record.get('YDegrees')+","+
                            "renderer: Ext.util.Format.numberRenderer('0,0')"+
                        "}"+
                   "}"+
                "},{"+
                    "type:'"+record.get('XType')+"',"+
                    "grid:"+record.get('ISXGrid')+","+
                    "minimum:"+record.get('XMinimum')+","+
                    "maximum:"+record.get('XMaximum')+","+
                    "position: '"+record.get('XPosition')+"',"+
                    "fields:['"+myXAxesFields[0]+"'],"+
                    "title:'"+record.get('XTitle')+"',"+
                    "label: {"+
                        "rotate: {"+
                            "degrees:"+record.get('XDegrees')+
                        "}"+
                    "}"+
               "}],"+
                "series: [{"+
                    "type:'"+record.get('SeriesType')+"',"+
                    "axis:'"+record.get('SeriesAxis')+"',"+
                    "highlight:"+record.get('HightLight')+","+
                    "gutter:"+record.get('Gutter')+","+
                    "xField:['"+myxField[0]+"'],"+
                    "yField:['"+myyField[0]+"'],"+
                    "tips: {"+
                        "trackMouse: true,"+
                        "width:120,"+
                        "height: 42,"+
                        "renderer: function(storeItem, item) {"+
                            "this.setTitle(storeItem.get('"+myxField[0]+"'));"+
                            "this.update(storeItem.get('"+myyField[0]+"'));"+
                        "}"+
                    "},"+
                     "label: {"+
                          "display: 'insideEnd',"+
                           "'text-anchor': 'middle',"+
                            "field: ['"+myyField[0]+"'],"+
                            "orientation: 'vertical',"+
                            "fill: '#fff',"+
                            "font: '17px Arial',"+
                            "color: '#333'"+
                        "},"+
                    "style: {"+
                        "fill: '#38B8BF'"+
                    "}"+
                "}]"+
            "}";
        return com;
    },
    /**
     * 创建饼状图类代码
     * 饼状图没有坐标,使用XAxesFields代替
     * @private
     * @param {} record
     * @return {}
     */
    createPieStr:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myYAxesFields=record.get('YAxesFields').split(",");;
       var myXAxesFields=record.get('XAxesFields').split(",");;
       var donut=record.get('ISDonut')||false;
       var showInLegend=record.get('ShowInLegend')||false;
       var myStore="me."+myItemId+"Store()";
       var com ="";
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate: true,"+
                "store:"+myStore+","+
                "theme:'"+ record.get('Theme')+"',"+
                "animate: true,"+
                "shadow: true,"+
                "insetPadding: 60,"+
                "legend: {"+
                    "position: 'right'"+
                "},"+
                "series: ["+ 
                    "{"+
                "type: 'pie',"+
                "animate: true,"+
                "showInLegend:"+showInLegend+","+
                "donut:'"+donut+"',"+
                "field:'"+myXAxesFields[0]+"',"+
                "tips: {"+
                  "trackMouse: true,"+
                  "width: 140,"+
                  "height: 28,"+
                  "renderer: function(storeItem, item) {"+
                    "var total = 0;"+
                    "store1.each(function(rec) {"+
                        "total += rec.get('"+myXAxesFields[0]+"');"+
                    "});"+
                    "this.setTitle(storeItem.get('"+myYAxesFields[0]+"') + ': ' + Math.round(storeItem.get('"+myXAxesFields[0]+"') / total * 100) + '%');"+
                  "}"+
                "},"+
                "highlight: {"+
                  "segment: {"+
                    "margin: 20"+
                  "}"+
                "},"+
                "label: {"+
                    "field:'"+myYAxesFields[0]+"',"+
                    "display: 'rotate',"+
                    "font: '18px Arial',"+
                    "contrast: true"+
                "},"+                                
                "renderer: function(sprite, record, attr, index, store) {"+
                "}"+
            "}]"+
    "}"
        return com;
    },
     /**
     * 创建(堆积/分组)条形图类代码
     * 堆积/分组条形图的数据格式后台如何提供?
     * @private
     * @param {} record
     * @return {}
     */
    createGroupBarStr:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=record.get("ISXGrid")||false;
       var ygrid=record.get("ISYGrid")||false;
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }
       var myStore="me."+myItemId+"Store()";
       var com ="";
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate: true,"+
                "store:"+myStore+","+
                "animate: true,"+
                "shadow: true,"+
                "legend: {"+
                  "position: 'right'"+ 
                "},"+
                "axes: [{"+ //Y坐标轴
                "type:'"+record.get('YType')+"',"+
                "position:'"+record.get('YPosition')+"',"+
                "fields:['"+myYAxesFields[0]+"'],"+
                "minimum:"+record.get('YMinimum')+","+
                "maximum:"+record.get('YMaximum')+","+
                "label: {"+ 
                    "renderer: Ext.util.Format.numberRenderer('0,0'),"+
                    "rotate: {"+ 
                        "degrees:"+record.get('YDegrees')+
                    "}"+ 
                "},"+ 
                "grid: "+record.get('ISYGrid')+","+
                "title:'"+record.get('YTitle')+"'"+
            "}, {"+ //X坐标轴
                "type:'"+record.get('XType')+"',"+
                "position:'"+record.get('XPosition')+"',"+
                "fields:['"+myXAxesFields[0]+"'],"+
                "minimum:"+record.get('XMinimum')+","+
                "maximum:"+record.get('XMaximum')+","+
                "label: {"+
                    "renderer: Ext.util.Format.numberRenderer('0,0'),"+
                     "rotate: {"+
                        "degrees:"+record.get('XDegrees')+
                    "}"+
                "},"+
                "grid:"+record.get('ISXGrid')+","+
                "title:'"+record.get('XTitle')+"'"+
            "}],"+
            "series: [{"+
                "type:'"+record.get('SeriesType')+"',"+
                "axis:'"+record.get("SeriesAxis")+"',"+
                "stacked:"+record.get("Stacked")+","+//条形图显示类型,true为堆积,false为分组
                "xField:['"+myxField[0]+"'],"+
                "yField:['"+myyField[0]+"']"+
            "}]"+
           " }"
        return com;
    },
    /**
     * 创建面积图类代码
     * @private
     * @param {} record
     * @return {}
     */
    createAreaStr:function(record){
       var me=this;
       var myItemId=record.get(me.objectPropertyValueField)||me.chartItemId;
       var myFields=record.get("Fields").split(",");
       
       var myxField=record.get("XField").split(","),xField="";
       var myyField=record.get("YField").split(","),yField="";
       var myYAxesFields=record.get("YAxesFields").split(","),yAxesFields="";
       var myXAxesFields=record.get("XAxesFields").split(","),xAxesFields="";
       
       var xgrid=record.get("ISXGrid")||false;
       var ygrid=record.get("ISYGrid")||false;
       
       for(var i=0;i<myyField.length;i++)
        {
            yField=(i<myyField.length-1)?(yField+("'"+myyField[i]+"',")):(yField+("'"+myyField[i]+"'"));
        }
        
       for(var i=0;i<myxField.length;i++)
        {
            xField=(i<myxField.length-1)?(xField+("'"+myxField[i]+"',")):(xField+("'"+myxField[i]+"'"));
        }
        
        for(var i=0;i<myYAxesFields.length;i++)
        {
            yAxesFields=(i<myYAxesFields.length-1)?(yAxesFields+("'"+myYAxesFields[i]+"',")):(yAxesFields+("'"+myYAxesFields[i]+"'"));
        }
        
        for(var i=0;i<myXAxesFields.length;i++)
        {
            xAxesFields=(i<myXAxesFields.length-1)?(xAxesFields+("'"+myXAxesFields[i]+"',")):(xAxesFields+("'"+myXAxesFields[i]+"'"));
        }

       
       var colors = ['rgb(47, 162, 223)',
                  'rgb(60, 133, 46)',
                  'rgb(234, 102, 17)',
                  'rgb(154, 176, 213)',
                  'rgb(186, 10, 25)',
                  'rgb(40, 40, 40)'];

        Ext.chart.theme.Browser = Ext.extend(Ext.chart.theme.Base, {
            constructor: function(config) {
                Ext.chart.theme.Base.prototype.constructor.call(this, Ext.apply({
                    colors: colors
                }, config));
            }
        });
           
       
       var donut = record.get("ISDonut");
        
       var myStore="me."+myItemId+"Store()";
       var com ="";
       com = "{"+
                "xtype:'chart'" + "," + 
                "width:"+record.get('Width')+","+
                "height:"+record.get('Height')+","+
                "itemId:'"+myItemId+"',"+
                "animate: true,"+
                "store:"+myStore+","+
                "theme:'"+ record.get('Theme')+"',"+
                "animate: true,"+
                "shadow: true,"+
                "insetPadding: 60,"+
                "legend: {"+
                    "position: 'right'"+
                "},"+
            "defaultInsets: 30,"+
            "axes: ["+
            "{"+//Y坐标轴
                "type:'"+record.get('YType')+"',"+
                "grid:"+record.get('ISYGrid')+","+
                "position:'"+record.get('YPosition')+"',"+
                "fields:['"+myYAxesFields[0]+"'],"+
                "title:'"+record.get('YTitle')+"',"+
                "label: {"+
                    "rotate: {"+
                        "degrees:"+record.get('YDegrees')+""+
                    "}"+
                "},"+
                "minimum:"+record.get('YMinimum')+","+
                "minimum:"+record.get('YMaximum')+","+
                "adjustMinimumByMajorUnit: 0"+
            "},"+
            "{"+
                "type:'"+record.get('XType')+"',"+
                "grid:"+record.get('ISXGrid')+","+
                "position:'"+record.get('XPosition')+"',"+
                "fields:['"+myXAxesFields[0]+"'],"+
                "title:'"+record.get('XTitle')+"',"+
                "minimum:"+record.get('XMinimum')+","+
                "minimum:"+record.get('XMaximum')+","+
                "label: {"+
                    "rotate: {"+
                        "degrees:"+record.get('XDegrees')+
                    "}"+
                "}"+
           " }"+
        "],"+
        "series: [{"+
            "type: 'area',"+
            "axis: 'left',"+
            "highlight:"+record.get('HightLight')+","+
            "xField: ['"+myxField[0]+"'],"+
            "yField:['"+myyField[0]+"'],"+
            "tips: {"+
              "trackMouse: true,"+
              "width: 170,"+
              "height: 28,"+
              "renderer: function(storeItem, item) {"+
                  "this.setTitle(storeItem.get("+"'"+myXAxesFields[0]+"'"+"));"+
              "}"+
            "},"+
            "style: {"+
                "lineWidth: 1,"+
                "stroke: '#666',"+
                "opacity: 0.86"+
            "}"+

        "}]"+
        "}"
        return com;
    }
        
});