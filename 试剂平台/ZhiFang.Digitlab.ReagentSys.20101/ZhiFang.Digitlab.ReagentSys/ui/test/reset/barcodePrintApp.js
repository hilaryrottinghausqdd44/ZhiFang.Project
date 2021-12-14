/***
 * 条码拆分:
 * 1.取当前登录站点的站点参数(当前站点没有站点参数时,取默认所有站点参数信息)
 * (1)条码拆分-查询选择类型
 * (2)条码拆分-科室过滤选择条件(渲染完视图后获取到)
 * nodeTableSickType:站点就诊类型
 */
Ext.ns('Ext.mept');
Ext.define('Ext.mept.barcode.barcodePrintApp',{
	extend:'Ext.panel.Panel',
	alias:'widget.barcodePrintApp',
    layout:{
        type:"border",
        regionWeights:{
            north:4,
            south:3,
            west:2,
            east:1
        }
    },
    /****
     * 删除样本单同时删除样本单项目(级联删除)
     * @type Boolean
     */
    deleteDelMEPTSampleFormUrl:getRootPath() +"/MEPTService.svc/MEPT_UDTO_DelMEPTSampleForm",
    autoScroll:true,
    /***
     * 需要显示的列表类型组
     * 1.门诊医嘱单列表及门诊条码拆分样本单列表为一组(默认):outpatients
     * 2.住院条码拆分样本单列表为一组:inpatient
     * 3.门诊&住院条码拆分样本单查询列表:inquire
     * 4:体检:examination
     * @type String
     */
    gridType:'outpatients',
    /***
     * 站点就诊类型
     * 1:住院
     * 2:门诊
     * 3:体检
     * @type String
     */
    nodeTableSickType:'2',
    /***
     * 科室过滤的ID集合的hqlWhere串
     * @type String
     */
    deptIdLists:'',
    /***
     * 依传入的参数将相关医嘱单拆分成样本单服务
     * 服务待定
     * @type 
     */
    splitMEPTOrderFormUrl:getRootPath() + '/MEPTService.svc/MEPT_UDTO_PartitionMEPTOrderForm',
	/**
	 * 查询参数列表服务地址
	 * @type 
	 */
	searchBParameterUrl:getRootPath() + '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
    fieldsBParameter:"BParameter_Id,BParameter_Name,BParameter_ParaType,BParameter_ParaValue,BParameter_BNodeTable_Id,BParameter_BNodeTable_Name,BParameter_ParaNo,BParameter_GroupNo",
    
	/**
	 * 查询取单时间类型列表地址
	 * @type 
	 */
	searchMEPTGetReportTime:getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchMEPTBSpecialTimeTypeByHQL?isPlanish=true',
	/**
	 * 查询医嘱单列表服务地址
	 * @type 
	 */
	searchMEPTOrderFormUrl:getRootPath() + "/MEPTService.svc/MEPT_UDTO_SearchMEPTOrderFormByHQL?isPlanish=true&sort=[{property:'MEPTOrderForm_DataAddTime','direction':'desc'}]",
   
    /**
	 * 查询医嘱单项目列表服务地址
	 * @type 
	 */
	searcItemAllItemUrl:getRootPath() + '/SingleTableService.svc/ST_UDTO_SearchItemAllItemByHQL?isPlanish=true',
	/***
     * 获取拆分成功的样本单列表服务
     * 获取数据的条件:样本单的打印次数为0,样本单的样本状态为空
     * @type 
	 */
    searchMEPTSampleFormUrl:getRootPath() + "/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleFormByHQL?isPlanish=true",
    /***
     * 依样本单id查询样本单项目信息列表
     * 样本单项目名称
     */
    searchMEPTSampleItemUrl:getRootPath() +"/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleItemByHQL?isPlanish=true&sort=[{property:'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id','direction':'desc'}]",
    fieldsMEPTSampleItem:'MEPTSampleItem_MEPTGetReportTime_TimeComment,MEPTSampleItem_SampleFrom_BarCode,MEPTSampleItem_ItemAllItem_Id,MEPTSampleItem_Id,MEPTSampleItem_SampleFrom_Id,MEPTSampleItem_SampleFrom_BSampleStatus_Id,MEPTSampleItem_ReceiveFlag,MEPTSampleItem_SampleFrom_PrintCount,MEPTSampleItem_SampleFrom_BSampleType_Name,MEPTSampleItem_SampleFrom_MEPTSamplingGroup_MEPTSamplingTube_ColorValue,MEPTSampleItem_SampleFrom_MEPTSamplingGroup_CName,MEPTSampleItem_ItemAllItem_CName,MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id',
    /**
     * 医嘱列表的列信息
     * @type 
     */
    MEPTSampleFormListColumns:[
        {text:'HIS申请单号',dataIndex:'MEPTSampleForm_MEPTOrderForm_SerialNo'},
        {text:'医嘱时间',dataIndex:'MEPTSampleForm_MEPTOrderForm_DataAddTime'},
        {text:'就诊卡号',dataIndex:'MEPTSampleForm_MEPTOrderForm_PatCardNo'},
        {text:'zdy1',dataIndex:'MEPTSampleForm_MEPTOrderForm_Zdy1'},
        {text:'zdy2',dataIndex:'MEPTSampleForm_MEPTOrderForm_Zdy2'},
        {text:'zdy3',dataIndex:'MEPTSampleForm_MEPTOrderForm_Zdy3'}
    ],
    /***
     * 查询条码类型
     * @type 
     */
    typeBParameterLists:[
        {text:'住院号',value:'InpatientNo'},
        {text:'就诊卡号',value:'PatCardNo'},
        {text:'姓名',value:'CName'},
        {text:'HIS申请单号',value:'SerialNo'}
    ],
    /***
     * 还原界面之前的配置
     * @type 
     */
    revertUIConfigure:{
    'ShowSearchPanel':'true',//显示条件查询框
    'typeBParameter':'PatCardNo',//查询条件选择类型
    'isRetained':'true',//是否保留查询条件值
    'getSingleTime':''//取单时间选中项
    },
    /**
	 * 门诊医嘱申请单列表的列信息
	 * @type 
	 */
	ApplicationListColumns:[
        {text:'Id',dataIndex:'MEPTOrderForm_Id',editor:{readOnly:true},hidden:true},
        {
            text:"住院号",
            dataIndex:"MEPTSampleForm_MEPTOrderForm_InpatientNo",
            disabled:false,
            width:100,
            locked:false,
            sortable:true,
            hidden:false,
            hideable:true,
            editor:{readOnly:true}
        },
		{text:'HIS申请单号',dataIndex:'MEPTOrderForm_SerialNo',editor:{readOnly:true}},
        {text:'就诊卡号',dataIndex:'MEPTOrderForm_PatCardNo',editor:{readOnly:true}},
         {
            text:"就诊类型",
            dataIndex:"MEPTOrderForm_BSickType_Name",
            width:80,
            locked:false,
            sortable:true,
            hidden:false,
            hideable:true
        },
		{text:'医嘱时间',dataIndex:'MEPTOrderForm_DataAddTime',width:150,
            renderer:function(value, cellmeta, record, rowIndex, columnIndex, store) {
                if (value != "") {
                    var datetime=Ext.util.Format.date(value,'Y-m-d H:i');
                    return datetime;
                }else{
                    return value;
                } 
            }
        },
        {text:'就诊卡号',dataIndex:'MEPTOrderForm_PatCardNo',editor:{readOnly:true}},
       {
            text:"病历号",
            dataIndex:"MEPTOrderForm_PatNo",
            width:80,
            locked:false,
            sortable:true,
            hidden:true,
            hideable:true
        },
       {
            text:"姓名",
            dataIndex:"MEPTOrderForm_CName",
            width:80,
            locked:false,
            sortable:true,
            hidden:true,
            hideable:true
        },
      {
            text:"性别",
            dataIndex:"MEPTOrderForm_BPatientInfo_BSex_Name",
            width:80,
            locked:false,
            sortable:true,
            hidden:true,
            hideable:true
        },
       {
            text:"年龄:",
            dataIndex:"MEPTOrderForm_Age",
            width:80,
            locked:false,
            sortable:true,
            hidden:true,
            hideable:true
        },
       {
            text:"科室:",
            dataIndex:"MEPTOrderForm_HRDept_CName",
            width:80,
            locked:false,
            sortable:true,
            hidden:true,
            hideable:true
        },
        {
            text:"床号:",
            dataIndex:"MEPTOrderForm_Bed",
            width:80,
            locked:false,
            sortable:true,
            hidden:true,
            hideable:true
        },
         {
            text:"医生:",
            dataIndex:"MEPTOrderForm_Doctor",
            width:80,
            locked:false,
            sortable:true,
            hidden:true,
            hideable:true
        },
		{text:'zdy1',dataIndex:'MEPTOrderForm_Zdy1',editor:{readOnly:true}},
		{text:'zdy2',dataIndex:'MEPTOrderForm_Zdy2',editor:{readOnly:true}},
		{text:'zdy3',dataIndex:'MEPTOrderForm_Zdy3',editor:{readOnly:true}}
	],
	
	/**
	 * 日期格式
	 * @type String
	 */
	dateformat:'Y-m-d',
	/**
	 * 用户信息属性
	 * @type 
	 */
	userInfo:{
		'MEPTOrderForm_PatNo':'病历号:',
		'MEPTOrderForm_CName':'姓名:',
		'MEPTOrderForm_BPatientInfo_BSex_Name':'性别:',
		'MEPTOrderForm_Age':'年龄:',
		'MEPTOrderForm_HRDept_CName':'科室:',
		'MEPTOrderForm_Bed':'床号:',
		'MEPTOrderForm_Doctor':'医生:'
	},
    meptOrderFormFields:'MEPTSampleForm_MEPTOrderForm_BSickType_Id,MEPTOrderForm_SerialNo,MEPTOrderForm_DataAddTime,MEPTOrderForm_PatCardNo',
    userInfoFields:'MEPTOrderForm_Doctor,MEPTOrderForm_Bed,MEPTOrderForm_HRDept_CName,MEPTOrderForm_Age,MEPTOrderForm_PatNo,MEPTOrderForm_CName,MEPTOrderForm_BPatientInfo_BSex_Name',

    initLink:function() {
        var me = this;
        //查询表单
        var selectF=me.getComponent('barcodeSearchForm');
        /***
         * 查询表单查询按钮事件
         */
        selectF.on({
            selectClick:function(com,e){
                me.searchButClick();
            },
            deleteButClick:function(com,e){
                me.deleteClick();
            },
            advancedButClick:function(com,e){
                
            }
        });
        //当前的登录站点Id值
        var nodeTableId=getBNodeTableInfo("BNodeTableID");
        
        //查询站点参数表--查询条件选择类型--还需要加上站点名称查询
        var typeBParameter=me.getTypeBParameter();
        var url=me.searchBParameterUrl+"&fields="+me.fieldsBParameter;
        //查询参数类型为'条件选择类型',参数编码为"MEPT"
        var hqlWhere="bparameter.ParaType='"+getBParameterParaType("MEPT")+"'"+" and bparameter.ParaNo='"+getBParameter("SplitBarCodeQueryType")+"'";
        if(nodeTableId!=null&nodeTableId!=""){
            hqlWhere=hqlWhere+" and "+"bparameter.BNodeTable.Id='"+nodeTableId+"'";
        }
        var lists=getServerLists(url,hqlWhere,false);
        if(lists.length>0){
           var paraValue=lists[0]["BParameter_ParaValue"];
           if(paraValue!=null&&paraValue!=""){
	           var list=Ext.decode(paraValue);
	           typeBParameter.store.loadData(list);
           }
        }else{
            typeBParameter.store.loadData(me.typeBParameterLists);
        }
        
        //查询参数类型为'条码拆分-科室过滤选择条件',参数编码为"MEPT"
        var DeptHQLWhere="bparameter.ParaType='"+getBParameterParaType("MEPT")+"'"+" and bparameter.ParaNo='"+getBParameter("SplitBarCodeDeptHQLWhere")+"'";
        if(nodeTableId!=null&nodeTableId!=""){
            DeptHQLWhere=DeptHQLWhere+" and "+"bparameter.BNodeTable.Id='"+nodeTableId+"'";
        }
        var DeptIdLists=getServerLists(url,DeptHQLWhere,false);
        if(DeptIdLists.length>0){
           var paraValue=DeptIdLists[0]["BParameter_ParaValue"];
           if(paraValue!=null&&paraValue!=""){
                me.deptIdLists=paraValue;
           }
        }
        
        setTimeout(function(){
            //还原界面之前的配置项
	        if(me.revertUIConfigure){
	            var typeBParameterValue=me.revertUIConfigure["typeBParameter"];
	            typeBParameter.setValue(typeBParameterValue);
	            
	            var showSearchPanel=me.getShowSearchPanel();
	            var showSearchPanelValue=me.revertUIConfigure["ShowSearchPanel"];
	            showSearchPanel.setValue(showSearchPanelValue);
	            
	            var isRetained=me.getIsRetained();
	            var isRetainedValue=me.revertUIConfigure["isRetained"];
	            isRetained.setValue(isRetainedValue);
	            
	            var getSingleTime=me.getSingleTime();
	            var getSingleTimeValue=me.revertUIConfigure["getSingleTime"];
	            getSingleTime.setValue(getSingleTimeValue);
	        }
        },1000);
        
    },

    setGridVisible:function(){
        var me = this;
        //门诊医嘱单列表
        var applicationlist=me.getApplicationlist();
        //门诊条码拆分列表
        var mergergrid=me.getMergergrid();
        var inpatientList=me.getInpatientList();
        var inquireList=me.getInquireList();

        var isOutpatients=false;
        var isMergergrid=false;
        var isInpatient=true;
        var isInquire=true;
        var selectF=me.getComponent('barcodeSearchForm');
        var hqlWhere=""+selectF.getValue();
        
        var gridType=me.gridType;
        if(hqlWhere!=""&&hqlWhere!="1=1"&hqlWhere!=undefined){
            
	        switch(gridType)
	            {
	            case 'outpatients':
	              //门诊列表组显示,其他列表隐藏
	                isOutpatients=true;
	                isMergergrid=true;
	                isInpatient=false;
	                isInquire=false;

	                break;
	            case 'inpatient':
	                //住院列表组显示,其他列表隐藏
	                isOutpatients=false;
	                isMergergrid=false;
	                isInpatient=true;
	                isInquire=false;  
	
	              break;
	           case 'examination':
	                //体检列表组显示,其他列表隐藏
	                isOutpatients=false;
	                isMergergrid=false;
	                isInpatient=true;
	                isInquire=false;  
	
	              break;
	              case 'inquire':
	              //查询列表组显示,其他列表隐藏
	                isOutpatients=false;
	                isMergergrid=false;
	                isInpatient=false;
	                isInquire=true;
                    //如果科室过滤不为空
		            if(me.deptIdLists!=null&&me.deptIdLists!=""){
		                hqlWhere=hqlWhere+" and (meptsampleform.MEPTOrderForm.HRDept.Id in ("+me.deptIdLists+")) ";
		            }
	                inquireList.load(hqlWhere);
	              break;
	            default:
	              //门诊列表组显示,其他列表隐藏
	                isOutpatients=true;
	                isMergergrid=true;
	                isInpatient=false;
	                isInquire=false;
	                
	            }
           
           if(applicationlist&&applicationlist!=undefined){
              applicationlist.setVisible(isOutpatients);
            }
            if(mergergrid&&mergergrid!=undefined){
              mergergrid.setVisible(isMergergrid);
            }
            if(inpatientList&&inpatientList!=undefined){
              inpatientList.setVisible(isInpatient);
            } 
            if(inquireList&&inquireList!=undefined){
              inquireList.setVisible(isInquire);
            }
        }
    },
	/**
	 * 初始化面板属性
	 * @private
	 */
	initComponent:function(){
		var me = this;
        //查询登录站点的就诊类型
        //当前的登录站点Id值
        var nodeTableId=getBNodeTableInfo("BNodeTableID");
        //查询站点参数表--查询条件选择类型--还需要加上站点名称查询
        var url=me.searchBParameterUrl+"&fields="+me.fieldsBParameter;
        
        var hqlWhere2="bparameter.ParaType='"+getBParameterParaType("MEPT")+"'"+" and bparameter.ParaNo='"+getBParameter("NodeTableSickType")+"'";
        if(nodeTableId!=null&nodeTableId!=""){
            hqlWhere2=hqlWhere2+" and "+"bparameter.BNodeTable.Id='"+nodeTableId+"'";
        }
        var sickTypeLists=getServerLists(url,hqlWhere2,false);
        if(sickTypeLists.length>0){
           var paraValue=sickTypeLists[0]["BParameter_ParaValue"];
           if(paraValue!=null&&paraValue!=""){
               if(paraValue=="2"){//门诊
                    me.nodeTableSickType=paraValue;
                    me.gridType="outpatients";
                }else if(paraValue=="1"){//住院
                    me.nodeTableSickType=paraValue;
                    me.gridType="inpatient";
                }else if(paraValue=="3"){//体检
                    me.nodeTableSickType=paraValue;
                    me.gridType="examination";
                }else{//
                    me.nodeTableSickType=paraValue;
                    me.gridType="outpatients";
                }
           }else{
                me.nodeTableSickType="2";
                me.gridType="outpatients";
           }
        }else{
            me.nodeTableSickType="2";
            me.gridType="outpatients";
        }
        
		me.initView();//初始化视图
		me.callParent(arguments);
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
	},
    /**
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
        var me = this;
        me.initLink();
        me.callParent(arguments);
        //初始化监听
        me.initListeners();
        me.setGridVisible();
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
    },
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.zhifangux.mergergrid', getRootPath()+'/ui/zhifangux/mergergrid.js');
        Ext.Loader.setPath('Ext.mept.barcode.inpatientList', getRootPath() +'/ui/mept/class/barcode/inpatientList.js');
        
		//功能挂靠
		me.dockedItems = me.createDockedItems();
		//内部功能
		me.items = me.createItems();
     
	},
	/**
	 * 创建监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		me.initPanelListners();//面板监听
		me.initButtonsToolbarListeners();//顶部按钮功能栏监听
		me.initConditionToolbarListeners();//顶部条件栏监听
		me.initSearchToolbarListeners();//底部查询栏监听
        
        me.applicationlistListeners();//门诊医嘱申请单列表
        me.inpatientListListeners();//住院条码拆分样本单列表
        me.inquireListListeners();//查询条码拆分样本单列表
	},
    /***
     * 门诊医嘱申请单列表
     */
    applicationlistListeners:function(){
        var me = this;
        var comList=me.getApplicationlist();
        if(comList&&comList!=undefined){
	        comList.on({
	            select:function(rowModel,record, index, eOpts){
                    me.setUserInfoToolbar(1,record);
	                var orderFormId=record.get("MEPTOrderForm_Id");
	                me.splitItemsCode(orderFormId);
	            }
	        });
	    }
    },
    /***
     * 住院条码拆分样本单列表
     */
   inpatientListListeners:function(){
        var me = this;
        var comList=me.getInpatientList();
        if(comList&&comList!=undefined){
            comList.on({
                select:function(rowModel,record, index, eOpts){
                    me.setUserInfoToolbar(2,record);
                }
            });
        }
    },
   /***
     * 查询条码拆分样本单列表
     */
   inquireListListeners:function(){
        var me = this;
        var comList=me.getInquireList();
        if(comList&&comList!=undefined){
            comList.on({
                select:function(rowModel,record, index, eOpts){
                    me.setUserInfoToolbar(2,record);
                }
            });
        }
    },
	/**
	 * 创建功能挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var dockedItems = [];
		
		var buttonsToolbar = me.createButtonsToolbar();//上部按钮功能栏
		buttonsToolbar && dockedItems.push(buttonsToolbar);
		
		var conditionToolbar = me.createConditionToolbar();//条件栏
		conditionToolbar && dockedItems.push(conditionToolbar);
		var infoToolbar = me.createInfoToolbar();//信息栏
		infoToolbar && dockedItems.push(infoToolbar);
		
		return dockedItems;
	},
	/**
	 * 创建顶部按钮功能栏
	 * @private
	 * @return {}
	 */
	createButtonsToolbar:function(){
		var me = this;
		var toolbar = {
			xtype:'toolbar',
			itemId:'buttonstoolbar',
			dock:'top',
			items:[{
				xtype:'button',
				itemId:'DepartmentsFilter',
				text:'科室过滤',
				iconCls:'build-button-configuration-blue'
			},{
				xtype:'button',
				itemId:'PrintCertificate',
				text:'打印取单凭证',
				iconCls:'print'
			},{
				xtype:'button',
				itemId:'Reader',
				text:'读卡',
				iconCls:'build-button-configuration-blue'
			},{
				xtype:'button',
				itemId:'PrintChecked',
				text:'打印',
				iconCls:'print',
				handler:function(){
					//是否可以进行报告打印
					var state = canPrintReport();
					if(!state){
						alertInfo("报告打印机正在打印中，请等待...");
					}else{
						//前台测试代码
						var info = "a.pdf,b.pdf";
						PrintReport(info);
						/*
						//实际中需要先生成报告文件，后台返回生成的文件名
						var callback = function(text){
							//文件名数组串，以逗号隔开
							var files = text;
							PrintReport(info);
						}
						//前后台的数据契约需要确定
						getToServer(url,callback);
						*/
					}
				}
			},{
				xtype:'button',
				itemId:'Print',
				text:'打印选中',
                iconCls:'print',
				handler:function(){
					//是否可以进行条码打印
					var state = canPrintCode();
					if(!state){
						alertInfo("条码打印机正在打印中，请等待...");
					}else{
						var arr = [];
						//前台测试代码
						for(var i=0;i<10;i++){
							var str = "{BarCode:'" + (i+1) + 
							"',CName:'李四',SexName:'女',Age:'15',AgeUnit:'岁',DeptName:'血液科',SampleType:'血清',ItemName:'钾;钠;镁'}";
							arr.push(str);
						}
						var info = "[" + arr.join(",") + "]";
						
						PrintCode(info);
					}
				}
			},{
				xtype:'button',
				itemId:'Close',
				text:'关闭',
				iconCls:'build-button-configuration-blue'
			},{
				xtype:'checkbox',
				itemId:'ShowSearchPanel',
                name:'ShowSearchPanel',
				boxLabel:'显示查询条件框'
			}]
		};
		return toolbar;
	},

	/**
	 * 创建顶部条件栏
	 * @private
	 * @return {}
	 */
	createConditionToolbar:function(){
		var me = this;
		var toolbar = {
			xtype:'toolbar',
			itemId:'conditiontoolbar',
            name:'conditiontoolbar',
			dock:'top',
			items:[{
				xtype:'combobox',
				itemId:'typeBParameter',
                name:'typeBParameter',
				editable:true,
				typeAhead:true,
				queryMode:'local',
				valueField:'value',
				displayField:'text',
	            store:new Ext.data.Store({
	                fields:['value','text'],
                    data:[]
	            }),
	            listeners:{
	            	beforequery:me.comboboxFuzzyQuery
	            },
	            width:120
			},{
				xtype:'checkbox',
				itemId:'isRetained',
                name:'isRetained'
			},{
				xtype:'textfield',
				itemId:'searchText',
                name:'searchText',
                value:'',//1307240108
				width:200
			},{
				xtype:'combobox',
				itemId:'getSingleTime',
                name:'getSingleTime',
				fieldLabel:'取单时间',
				width:260,
	            labelWidth:60,
	            labelAlign:'right',
				editable:true,
				typeAhead:true,
				queryMode:'local',
				valueField:'MEPTBSpecialTimeType_Id',
				displayField:'MEPTBSpecialTimeType_CName',
				store:new Ext.data.Store({
	                fields:['MEPTBSpecialTimeType_Id','MEPTBSpecialTimeType_CName'],
	                autoLoad:true,
	                pageSize:5000,
	                proxy:{
		                type:'ajax',
		                url:me.searchMEPTGetReportTime + '&fields=MEPTBSpecialTimeType_Id,MEPTBSpecialTimeType_CName',
		                reader:{type:'json',totalProperty:'count',root:'list'},
		                extractResponseData:function(response){
		                	return me.changeStoreData(response);
		                }
		            }
	            }),
	            listeners:{
	            	beforequery:me.comboboxFuzzyQuery
	            }
			}]
		};
		return toolbar;
	},
	/**
	 * 创建底部信息栏
	 * @private
	 * @return {}
	 */
	createInfoToolbar:function(){
		var me = this;
		var toolbar = {
			xtype:'toolbar',
			itemId:'infotoolbar',
			dock:'bottom',
			items:[{
				xtype:'label',
				width:400,
				value:'过滤科室：A,B,C'
			}]
		};
		return toolbar;
	},
	/**
	 * 创建内部功能
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [];
        var userInfoToolbar = me.createUserInfoToolbar();//用户信息栏
        userInfoToolbar && items.push(userInfoToolbar);
        
         var barcodeSearchForm = me.createBarcodeSearchForm();//用户信息栏
        barcodeSearchForm && items.push(barcodeSearchForm);
               
		var gridType=me.gridType;
        switch(gridType)
            {
            case 'outpatients':
	          //门诊站点显示信息
              var applicationList =  me.createApplicationList();//申请列表
              var inquireList =me.createInquireList();
		      applicationList && items.push(applicationList);
              inquireList && items.push(inquireList);
		      var mergerList = me.createMergerList('');//合并列表
		      mergerList && items.push(mergerList);
              break;
            case 'inpatient':
               //住院站点/查询显示信息
		        var inpatientList =me.createInpatientList();
                var inquireList =me.createInquireList();
		        items.push(inpatientList);
                items.push(inquireList);
              break;
           case 'examination':
               //体检站点/查询显示信息
                var inpatientList =me.createInpatientList();
                var inquireList =me.createInquireList();
                items.push(inpatientList);
                items.push(inquireList);
              break;
           case 'inquire':
              var inquireList =me.createInquireList();
              inquireList && items.push(inquireList);  
              break;
            default:
            }
            return items;
	},
	/**
	 * 创建病人基本信息栏
	 * @private
	 * @return {}
	 */
	createUserInfoToolbar:function(){
		var me = this;
		var top = 4;
		var left = 10;
		var toolbar = {
			xtype:'toolbar',
			itemId:'userinfotoolbar',
			region:'north',
			layout:'absolute',
			height:30,
			border:false,
			defaults:{
				xtype:'displayfield',
                labelWidth:50,
				style:'fontWeight:bold'
			},
			items:[{
				width:120,
				itemId:'MEPTOrderForm_PatNo',
				fieldLabel:me.userInfo['MEPTOrderForm_PatNo'],
				x:left,
				y:top
			},{
				width:120,
				itemId:'MEPTOrderForm_CName',
				fieldLabel:me.userInfo['MEPTOrderForm_CName'],
				x:left+120,
				y:top
			},{
				width:120,
				itemId:'MEPTOrderForm_BPatientInfo_BSex_Name',
				fieldLabel:me.userInfo['MEPTOrderForm_BPatientInfo_BSex_Name'],
				x:left+240,
				y:top
			},{
				width:120,
				itemId:'MEPTOrderForm_Age',
				fieldLabel:me.userInfo['MEPTOrderForm_Age'],
				x:left+360,
				y:top
			},{
                width:120,
                itemId:'MEPTOrderForm_Bed',
                fieldLabel:me.userInfo['MEPTOrderForm_Bed'],
                x:left+480,
                y:top
            },{
				width:120,
				itemId:'MEPTOrderForm_Doctor',
				fieldLabel:me.userInfo['MEPTOrderForm_Doctor'],
				x:left+600,
				y:top
			},{
                width:180,
                itemId:'MEPTOrderForm_HRDept_CName',
                fieldLabel:me.userInfo['MEPTOrderForm_HRDept_CName'],
                x:left+720,
                y:top
            }]
		};
		return toolbar;
	},
    /**
     * 创建住院医嘱申请单条码拆分列表
     * @private
     * @return {}
     */
    createInpatientList:function(){
        var me = this;
        var list = Ext.create('Ext.mept.barcode.inpatientList',{
            header:false,
            plugins: getCellEditing(),
            region:'center',
            itemId:'inpatientList',
            name:'inpatientList',
            border:false,
            columnLines:true,
            rowLines:true,
            sortableColumns:false,
            multiSelect:true,//允许多选
            split:false,
            autoScroll:true,
            collapsible:true,
            collapsed:false
        });
        return list;
    },
    /**
     * 创建查询医嘱申请单条码拆分列表
     * @private
     * @return {}
     */
    createBarcodeSearchForm:function(){
        var me = this;
        var barcodeSearchForm = Ext.create('Ext.mept.barcode.barcodeSearchForm',{
            header:false,
            itemId:'barcodeSearchForm',
            name:'barcodeSearchForm',
            region:'south',
            split:true,
            border:false,
            collapsible:true,
            collapsed:false
        });
        return barcodeSearchForm;
    }, 
    /**
     * 创建查询医嘱申请单条码拆分列表
     * @private
     * @return {}
     */
    createInquireList:function(){
        var me = this;
        var list = Ext.create('Ext.mept.barcode.inpatientList',{
            header:false,
            plugins: getCellEditing(),
            region:'center',
            itemId:'inquireList',
            name:'inquireList',
            border:false,
            hidden:true,
            columnLines:true,
            rowLines:true,
            sortableColumns:false,
            multiSelect:true,//允许多选
            split:false,
            autoScroll:true,
            collapsible:true,
            collapsed:false
        });
        return list;
    },    
	/**
	 * 创建门诊医嘱申请单列表
	 * @private
	 * @return {}
	 */
	createApplicationList:function(){
		var me = this;
		var arr = me.ApplicationListColumns;
		
		var columns = [{text:'行号',xtype:'rownumberer',width:35,align:'center'}];
		var fields = [];//需要加载的数据列
		
		for(var i in arr){
			columns.push({
				text:arr[i].text,
				dataIndex:"'"+arr[i].dataIndex+"'",
				editor:{readOnly:true}
			});
			fields.push(arr[i].dataIndex);
		}
		
		var urlFields = [];//服务的fields参数
		for(var i in fields){
			urlFields.push(fields[i]);// "MEPTOrderForm_" +
		}
		me.meptOrderFormFields=urlFields.join(",");
		var list = {
			xtype:'grid',
			itemId:'applicationlist',
			region:'north',
			border:false,
			collapsible:true,
			split:true,
			header:false,
            //hidden:true,
			height:100,
			autoScroll:true,
			columnLines:true,
			plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
			columns:me.ApplicationListColumns,
			store:Ext.create('Ext.data.Store',{
				fields:fields,
				autoLoad:false,
                pageSize:5000,
                proxy:{
	                type:'ajax',
	                url:me.searchMEPTOrderFormUrl + "&fields=" + me.meptOrderFormFields,
	                reader:{type:'json',totalProperty:'count',root:'list'},
	                extractResponseData:function(response){
	                	return me.changeStoreData(response);
	                }
	            }

			})
		};
		return list;
	},
	/**
	 * 创建门诊条码拆分合并列表
     * 合并的主列:采样组设置ID:MEPTSampleForm_MEPTSamplingGroup_Id
     * 背景色列:采样管的颜色值
	 * @private
	 * @return {}
	 */
	createMergerList:function(hqlWhere){
		var me = this;
        var data=[];
        if(hqlWhere!=""){
            data=getServerLists(me.searchMEPTSampleItemUrl+"&fields="+me.fieldsMEPTSampleItem,hqlWhere,false);
        }else{
            data=[];
        }
        
		var list = Ext.create('Ext.zhifangux.mergergrid',{
			mergedMainColumn:'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id',//合并的主列
			mergedOtherColumns:[
            'MEPTSampleItem_SampleFrom_BSampleStatus_Id',
            'MEPTSampleItem_SampleFrom_BSampleType_Name',
            'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_CName',
            'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id',
            'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_MEPTSamplingTube_ColorValue'],//需要合并的列
			bgcolorColumns:['MEPTSampleItem_SampleFrom_MEPTSamplingGroup_MEPTSamplingTube_ColorValue'],//背景色列
			searchUrl:me.searchMEPTSampleItemUrl+"&fields="+me.fieldsMEPTSampleItem,
			region:'center',
			itemId:'mergergrid',
            name:'mergergrid',
			border:false,
			columnLines:true,
			rowLines:true,
			sortableColumns:false,
			selType:'checkboxmodel',//复选框
			multiSelect:true,//允许多选
			hideHeaders:true,//不要列标题行
			columns:[
				{dataIndex:'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id',text:'采样组Id',editor:{readOnly:false}},//
				{dataIndex:'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_MEPTSamplingTube_ColorValue',text:'',width:10},
                {dataIndex:'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_CName',text:'采样组'},
                {dataIndex:'MEPTSampleItem_ItemAllItem_CName',text:'项目',width:210},
                {dataIndex:'MEPTSampleItem_ItemAllItem_Id',text:'项目Id',hidden:false,editor:{readOnly:false}},
				{dataIndex:'MEPTSampleItem_SampleFrom_BSampleType_Name',text:'样本类型'},
				{dataIndex:'MEPTSampleItem_ReceiveFlag',text:'核收状态',width:60,hidden:true,
	                renderer:function(value, cellmeta, record, rowIndex, columnIndex, store) {
		                var value2 = record.get("MEPTSampleItem_ReceiveFlag");
		                if (value2 == "1") {
		                    value='核收';
		                }
                        else if (value2 == "0") {
                            value='未核收';
                        }else{
                            value='';
                        }
		               
		                return value;
		            }
                },
                {dataIndex:'isNumLine',text:'isNumLine',width:110,hidden:true,editor:{readOnly:true}},//是否是数字行
                {dataIndex:'MEPTSampleItem_Id',text:'样本单项目Id',width:110,hidden:true,editor:{readOnly:true}},
                {dataIndex:'MEPTSampleItem_SampleFrom_BarCode',text:'条码号',width:170,hidden:false,editor:{readOnly:true}},
                {dataIndex:'MEPTSampleItem_SampleFrom_Id',text:'样本单Id',width:110,hidden:true},
                {dataIndex:'mainValue',text:'mainValue',width:110,editor:{readOnly:false},hidden:true},//采样组的值
                {dataIndex:'MEPTSampleItem_MEPTGetReportTime_TimeComment',text:'取单时间',width:300,editor:{readOnly:false},hidden:false},
				{dataIndex:'MEPTSampleItem_SampleFrom_BSampleStatus_Id',text:'样本状态',width:110,editor:{readOnly:false},hidden:true}
			],
			plugins:Ext.create('Ext.grid.plugin.CellEditing',
	                {clicksToEdit:1,
	                 listeners : {
                            validateedit : function(editor, e) {
                                var isNumLine=e.record.get("isNumLine");
                                if(isNumLine==false){
                                    e.cancel = true;
                                }
                            },
	                        edit : function(editor, e) {
	                            var record = e.record;
                                var data = e.record.data;
                                var editor=editor;
                                var grid=e.grid;
                                var row=e.row;
                                var column=e.column;
                                var value=''+e.value;
                                var originalValue=e.originalValue;
                                var mainValue=e.record.get("mainValue");//采样组Id
                                var isNumLine=e.record.get("isNumLine");//是否是数字行
                                var field=e.field;
                                if(isNumLine){//分组数字行
                                    //e.record.set('MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id',originalValue);
                                    var store=grid.getStore();
                                    store.each(function(record){
                                        var groupId=record.get("MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id");
                                        if(groupId==mainValue){
                                            value=value.replace(originalValue, "", "gi");
                                            //record.set('MEPTSampleItem_SampleFrom_BarCode',value);
                                            //record.commit();
                                        }
                                    });
                                   //e.cancel = true;
                                }else{
                                    e.cancel = true;
                                }
	                        }
	                    }
	                }
            
            ),
            store:new Ext.data.Store({
                autoLoad:true,
                fields:['MEPTSampleItem_MEPTGetReportTime_TimeComment','MEPTSampleItem_SampleFrom_BarCode','mainValue','isNumLine','SerialNo','MEPTSampleItem_ItemAllItem_Id','MEPTSampleItem_Id','MEPTSampleItem_SampleFrom_Id','MEPTSampleItem_SampleFrom_BSampleStatus_Id',
                'MEPTSampleItem_ReceiveFlag','MEPTSampleItem_SampleFrom_BSampleType_Name','MEPTSampleItem_SampleFrom_MEPTSamplingGroup_MEPTSamplingTube_ColorValue',
                'MEPTSampleItem_SampleFrom_MEPTSamplingGroup_CName','MEPTSampleItem_ItemAllItem_CName','MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id'],
                data:data
            })

		});  
        
		return list;
	},
    /***
     * 保存样本项目重新拆分
     * @param {} records
     * @return {}
     */
    saveToTable:function(records) {
        var me=this;
        var url = 'MEPTService.svc/MEPT_UDTO_UpdateMEPTSampleItemByField';
        if (url != '') {
            url = getRootPath() + '/' + url;
        } else {
            alertError('没有配置获取数据服务地址!');
            return null;
        }
        var id=records["MEPTDistributeRuleDetail_Id"];
        var maxSampleNo=records["MEPTDistributeRuleDetail_MaxSampleNo"];
        var fields = 'Id,MaxSampleNo';
        var entity={"Id":id,"MaxSampleNo":maxSampleNo};
        var params = Ext.JSON.encode({
                    entity : entity,
                    fields : fields
                });
        var callback = function(text) {
            var result = Ext.JSON.decode(text);
            if (result.success&&me.isSuccessMsg==true) {
                alertInfo('保存成功!');
            } else {
                alertError(result.ErrorInfo);
            }
        };
        //postToServer(url, params, callback);
    },
	/**
	 * 下拉框模糊查询
	 * @private
	 * @param {} e
	 * @return {Boolean}
	 */
	comboboxFuzzyQuery:function(e){
		var combo = e.combo;
        if(!e.forceAll){
        	var value = e.query;
        	combo.store.filterBy(function(record,id){
        		var text = record.get(combo.displayField);
        		return (text.indexOf(value) != -1);
        	});
        	combo.expand();
        	return false;
        }
	},
	/**
	 * 初始化顶部按钮功能栏监听
	 * @private
	 */
	initButtonsToolbarListeners:function(){
		var me = this;
		var buttonstoolbar = me.getComponent('buttonstoolbar');//顶部按钮功能栏
		
		//科室过滤按钮监听
		var DepartmentsFilter = buttonstoolbar.getComponent('DepartmentsFilter');
		DepartmentsFilter.on({
			click:function(but){
				me.openDepartmentsFilterWin();
			}
		});
		
		//是否显示查询条件框的监听
		var ShowSearchPanel = me.getShowSearchPanel();
		ShowSearchPanel.on({
			change:function(field,newValue,oldValue){
				var searchtoolbar = me.getComponent('barcodeSearchForm');
				newValue ? searchtoolbar.show() : searchtoolbar.hide();
			}
		});
	},

	/**
	 * 初始化顶部条件栏监听
	 * @private
	 */
	initConditionToolbarListeners:function(){
		var me = this;
		//查询条件框回车监听
        var searchText = me.getsearchText();
        new Ext.KeyMap(searchText.getEl(),[{
            key:Ext.EventObject.ENTER,
            fn:function(){
                me.searchTextEnter();
            }
        }]);
	},
	/**
	 * 打开科室过滤页面
	 * @private
	 */
	openDepartmentsFilterWin:function(){
        var me=this;
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.mept.barcode.DepartmentsFilter', getRootPath() +'/ui/mept/class/barcode/DepartmentsFilter.js');
        
		var win=openWin('Ext.mept.barcode.DepartmentsFilter');
        win.on({
            saveAfterClick:function(ParaValue){
                me.deptIdLists=""+ParaValue;
            }
        });
	},

	/**
	 * 顶部条件栏查询条件框回车处理
	 * @private
	 */
	searchTextEnter:function(){
		var me = this;
        me.setUserInfoToolbar(1,null);
        if(me.nodeTableSickType=="2"){//门诊
            me.gridType="outpatients";
        }else if(me.nodeTableSickType=="1"){//住院
            me.gridType="inpatient";
        }else if(me.nodeTableSickType=="3"){//体检
            me.gridType="examination";
        }else{//查询
            me.gridType="inquire";
        }
        me.setGridVisible();
        var typeBParameterValue=me.getTypeBParameterValue();
        var userinfotoolbar=me.getUserinfotoolbar();

        var typeValue = me.getTypeBParameterValue();//核收类型
        
        var searchText = me.getsearchText();//条件
        var singleTimeValue = me.getSingleTimeValue();//取单时间
        var searchTextValue=me.getsearchTextValue();
        var gridType=me.gridType;
        var hqlWhere="";
        if(typeValue==""||typeValue==null){
            Ext.Msg.alert("提示","请选择查询条件类型");
            me.getTypeBParameter().focus(true,100);
            return ;
        }
        if(searchTextValue==""||searchTextValue==null){
            Ext.Msg.alert("提示","请输入查询值");
            searchText.focus(true,100);
            return ;
        }
        if(singleTimeValue==""||singleTimeValue==null){
            Ext.Msg.alert("提示","请选择取单时间");
            me.getSingleTime().focus(true,100);
            return ;
        }
        else{
			var callback = function(){
				//是否清空条件输入框
				!me.getIsRetainedValue() && searchText.reset();
	            var data=[];
	            var orderFormId="";
	            var hqlWhere="";
                var fields="";
                var url="";
                me.setUserInfoToolbar(1,null);
	            if(searchTextValue!=""){
	                var sort="";
					//更新列表信息
					var list =null;
                    
			        switch(gridType)
			            {
			            case 'outpatients':
                            var sort="[{'property':'MEPTOrderForm_DataAddTime','direction':'desc'}]";
		                    hqlWhere="meptorderform."+typeValue+"='"+searchTextValue+"'";//+ "&sort="+sort;
		                    if(hqlWhere!=""){
                                url=me.searchMEPTOrderFormUrl;
                                //就诊类型
                                if(me.nodeTableSickType!=""){
                                    hqlWhere=hqlWhere+" and meptorderform.BSickType.Id="+me.nodeTableSickType;
                                }
		                        fields=me.userInfoFields+","+me.meptOrderFormFields;
		                        data=getServerLists(url+"&fields="+fields,hqlWhere,false);
		                   
		                    }else{
		                        data=[];
		                    }
	                          //门诊站点显示信息
	                        list = me.getApplicationlist();
	                           if(list&&list!=undefined){
			                       list.store.loadData(data);
			                       var store=list.getStore();
			                        if(store&&store!=null){
			                            var counts=store.getCount( );
			                            if(counts&&counts>0){
			                                list.getSelectionModel().select(0);
				                        }else if(counts==0){
                                            me.splitItemsCode("");
                                        }
				                    }
			                     }
			              
			              break;
			            case 'inpatient':
				               //住院站点/查询显示信息
				               var listCom = me.getInpatientList();
	                           hqlWhere="meptsampleform.MEPTOrderForm."+typeValue+"='"+searchTextValue+"'";
                               //就诊类型
                                if(me.nodeTableSickType!=""){
                                    hqlWhere=hqlWhere+" and meptsampleform.MEPTOrderForm.BSickType.Id="+me.nodeTableSickType;
                                }
	                           if(listCom&&listCom!=undefined){
	                                listCom.load(hqlWhere);
                                    listCom.store.load();
	                           }
			              break;
                          case 'examination':
                               //体检站点/查询显示信息
                               var listCom = me.getInpatientList();
                               hqlWhere="meptsampleform.MEPTOrderForm."+typeValue+"='"+searchTextValue+"'";
                               //就诊类型
                                if(me.nodeTableSickType!=""){
                                    hqlWhere=hqlWhere+" and meptsampleform.MEPTOrderForm.BSickType.Id="+me.nodeTableSickType;
                                }
                               if(listCom&&listCom!=undefined){
                                    listCom.load(hqlWhere);
                                    listCom.store.load();
                               }
                          break;
			              case 'inquire'://查询列表
				              list = me.getInquireList();
	                          hqlWhere="meptsampleform.MEPTOrderForm."+typeValue+"='"+searchTextValue+"'";
//                              //就诊类型
//                                if(me.nodeTableSickType!=""){
//                                    hqlWhere=hqlWhere+" and meptorderform.MEPTOrderForm.BSickType.Id="+me.nodeTableSickType;
//                                }
	                           if(list&&list!=undefined){
	                                list.load(hqlWhere);
	                           }
			              break;
			            default:
                            //list = me.getApplicationlist();
			            }
	            }
			};
            
            //将医嘱单拆分成样本单服务待定
            var hqlWhere2="meptorderform."+typeValue+"='"+searchTextValue+"'";
            //如果科室过滤不为空
            if(me.deptIdLists!=null&&me.deptIdLists!=""){
                hqlWhere2=hqlWhere2+" and meptorderform.HRDept.Id in ("+me.deptIdLists+") ";
            }

            hqlWhere2=hqlWhere2+"&longSpecialTimeTypeID="+singleTimeValue+"";//+"&isWholeForm=false";
	        //调用后台服务先进行拆分,拆分成功后返回拆分成功的医嘱单Id
            me.handlerData(hqlWhere2,null,callback);
        }  
	},
    /***
     * record为空时,设置空值
     * type:1.医嘱单:2:样本单
     * @param {} type
     * @param {} record
     */
    setUserInfoToolbar:function(type,record){
        var me = this;
        //选中某一行后,给病人基本信息赋值
        var userinfotoolbar=me.getUserinfotoolbar();
        var items=userinfotoolbar.items.items;
        var itemId='';
        var value="";
        var com=null;
        for(var i=0;i<items.length;i++){
            com=items[i];
            itemId=com.itemId;
            if(type==2){
                itemId="MEPTSampleForm_"+itemId;
            }
            if(record&&record!=null){
                value=record.get(itemId);
            }else{
                value="";
            }
            com.setValue(value);
        }
    },

    /**
     * 选中某一行医嘱单后,拆分条码处理数据
     * @private
     * @param {} callback
     */
    handlerData:function(hqlWhere,params,callback){
        var me = this;
        var c = function(text){
            var result = Ext.JSON.decode(text);
            if(result.success){
                if(Ext.typeOf(callback) == "function"){callback();}
            }else{
                alertError(result.ErrorInfo);
            }
        };
        var defaultPostHeader='application/json';
        var async=false;
        var url=me.splitMEPTOrderFormUrl+"?where="+hqlWhere;
        postToServer(url,null,c,defaultPostHeader,async,null);
        //c("{success:true}");
    },
    /**
     * 门诊
     * 选中某一行医嘱单后,拆分条码处理数据
     * @private
     * @param {} callback
     */
    splitItemsCode:function(orderFormID,callback){
        var me = this;
        if(orderFormID==""){
            //测试:ReceiveFlag=1为已核收,不能再拆分,查询PrintCount打印次数为0且样本状态为空
            var hqlWhere="";
            var mergergrid=me.getMergergrid();
            var mergerListCom = me.createMergerList(hqlWhere);//合并列表
            me.remove(mergergrid);
            me.add(mergerListCom);
            mergerListCom.getSelectionModel().selectAll();
        }else if(orderFormID!=""){
            //测试:ReceiveFlag=1为已核收,不能再拆分,查询PrintCount打印次数为0且样本状态为空
            var hqlWhere='';
            hqlWhere="meptsampleitem.ReceiveFlag!=1 and meptsampleitem.SampleFrom.MEPTOrderForm.Id='"+orderFormID+"'";//+" and meptsampleItem.SampleFrom.PrintCount=0  and meptsampleItem.SampleFrom.SampleStatusID is null";
            var mergergrid=me.getMergergrid();
            var mergerListCom = me.createMergerList(hqlWhere);//合并列表
            me.remove(mergergrid);
            me.add(mergerListCom);
            mergerListCom.getSelectionModel().selectAll();
        }
    }, 
    getShowSearchPanel:function(){
        var me=this;
        var buttonstoolbar = me.getComponent('buttonstoolbar');
        var com = buttonstoolbar.getComponent('ShowSearchPanel');
        return com;
    },
    getShowSearchPanelValue:function(){
        var me=this;
        var com = me.getShowSearchPanel();
        var value=com.getValue();
        return com;
    },
    /**
     * 是否保留（是否保留条件框的值）
     * @private
     * @return {}
     */
    getIsRetained:function(){
        var me = this;
        var com = me.getConditiontoolbar().getComponent('isRetained');
        return com;
    },
    getIsRetainedValue:function(){
        var me = this;
        var com = me.getIsRetained();
        var bo = com.getValue();
        return bo;
    },
    /***
     * 顶部条件栏--取单时间
     * @return {}
     */
    getSingleTime:function(){
        var me=this;
        var conditiontoolbar = me.getConditiontoolbar();
        var com=conditiontoolbar.getComponent('getSingleTime');
        return com;
    },
    getSingleTimeValue:function(){
        var me=this;
        var com=me.getSingleTime();//取单时间
        var value=com.getValue();
        return value;
    },

    /***
     * 用户基本信息列表
     * @return {}
     */
    getUserinfotoolbar:function(){
        var me = this;
        var com=me.getComponent("userinfotoolbar");
        return com;
    },
    getConditiontoolbar:function(){
        var me = this;
        var com=me.getComponent("conditiontoolbar");
        return com;
    },
    getTypeBParameter:function(){
        var me = this;
        var com=me.getConditiontoolbar().getComponent("typeBParameter");
        return com;
    },
    getTypeBParameterValue:function(){
        var me = this;
        var com=me.getTypeBParameter();
        var value=com.getValue();
        return value;
    },
    getsearchTextValue:function(){
        var me=this;
        var com=me.getsearchText();//条件
        var value=com.getValue();
        return value;
    },
    /***
     * 门诊条码拆分列表
     * @return {}
     */
    getMergergrid:function(){
        var me = this;
        var com=me.getComponent("mergergrid");
        return com;
    },
    /***
     * 住院医嘱申请单条码拆分样本单列表
     * @return {}
     */
    getInpatientList:function(){
        var me=this;
        var com = me.getComponent('inpatientList');
        return com;
    },
    /***
     * 查询条码拆分样本单列表
     * @return {}
     */
    getInquireList:function(){
        var me=this;
        var com = me.getComponent('inquireList');
        return com;
    },
    /***
     * 门诊医嘱申请单列表
     * @return {}
     */
    getApplicationlist:function(){
        var me=this;
        var com = me.getComponent('applicationlist');
        return com;
    },
    /***
     * 顶部条件栏
     * @return {}
     */
    getConditiontoolbar:function(){
        var me=this;
        var com = me.getComponent('conditiontoolbar');
        return com;
    },
    getsearchText:function(){
        var me=this;
        var conditiontoolbar = me.getConditiontoolbar();//顶部条件栏
        var com=conditiontoolbar.getComponent('searchText');
        return com;
    },

	/**
     * 列表格式数据匹配方法
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreData:function(response,callback){
        var result = Ext.JSON.decode(response.responseText);
        result.count = 0;result.list = [];
        
        if(result.ResultDataValue && result.ResultDataValue !=''){
            var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
            result.count = ResultDataValue.count;
            result.list = ResultDataValue.list;
            result.ResultDataValue = "";
        }
        if(Ext.typeOf(callback) === 'function'){
        	callback(result);
        }
        response.responseText = Ext.JSON.encode(result);
        return response;
    },
    /**
     * 初始化底部查询栏监听
     * @private
     */
    initSearchToolbarListeners:function(){
    	var me = this;
    	var searchtoolbar = me.getComponent('barcodeSearchForm');//顶部条件栏
    	
		var dataAddTime = searchtoolbar.getDataAddTime();//日期
		var BarCode = searchtoolbar.getComponent('MEPTSampleForm_BarCode');//
		var PatNo = searchtoolbar.getComponent('MEPTSampleForm_MEPTOrderForm_PatNo');//
		var CName = searchtoolbar.getComponent('MEPTSampleForm_MEPTOrderForm_CName');
        
        var PatCardNo = searchtoolbar.getComponent('MEPTSampleForm_MEPTOrderForm_PatCardNo');//
        var InpatientNo = searchtoolbar.getComponent('MEPTSampleForm_MEPTOrderForm_InpatientNo');//
		var arr = [dataAddTime,BarCode,PatNo,CName,PatCardNo,InpatientNo];
		initENTERListeners(arr,function(){me.searchButClick();});

    },
    /**
     * 创建面板监听
     * @private
     */
    initPanelListners:function(){
    	var me = this;
    	new Ext.KeyMap(me.getEl(),[{
	      	key:[Ext.EventObject.F],//F键
	      	shift:true,//同时按shift键
	      	fn:function(){me.searchButClick();},
	      	scope:this
     	},{
     		key:[Ext.EventObject.D],//D键
     		shift:true,//同时按shift键
	      	fn:function(){me.deleteClick();},
	      	scope:this
     	}]);
    },
    /**
     * 查询按钮点击处理
     * @private
     */
    searchButClick:function(){
    	var me = this;
        me.setUserInfoToolbar(1,null);
//        if(me.nodeTableSickType=="2"||me.nodeTableSickType=="3"||me.nodeTableSickType=="1"){//门诊如何处理
//            me.gridType="inquire";
//        }
        me.gridType="inquire";
        me.setGridVisible();
    },
    
    /**
     * 删除按钮点击处理
     * @private
     */
    deleteClick:function(){
    	var me = this;
	    var gridList=null;
        //门诊样本单拆分列表
	    if(me.nodeTableSickType=='2'&&me.gridType=='outpatients'){
	       gridList=me.getMergergrid();
	    }else if(me.nodeTableSickType=='2'&&me.gridType=='inquire'){
           gridList=me.getInquireList();
        }else if(me.nodeTableSickType=='1'&&me.gridType=='inpatient'){
           gridList=me.getInpatientList();
        }else if(me.nodeTableSickType=='1'&&me.gridType=='inquire'){
           gridList=me.getInquireList();
        }else if(me.nodeTableSickType=='3'&&me.gridType=='examination'){
           gridList=me.getInpatientList();
        }else if(me.nodeTableSickType=='3'&&me.gridType=='inquire'){
           gridList=me.getInquireList();
        }
        var store=gridList.getStore();
        var url="",c ="",id="",checkSelect=false;
        if(me.nodeTableSickType=='2'&&me.gridType=='outpatients'){
            //门诊的删除
            var records = gridList.getSelectionModel().getSelection();
            var record=null,removeArr=[];
           
            if (records.length>0) {
                var groupId="",groupIdNew='';
                Ext.Array.each(records,function(record,index){
                    removeArr.push(record);
                   id=record.get("MEPTSampleItem_SampleFrom_Id");
                   var isNumLine=record.get("isNumLine");
                   groupIdRow=record.get("mainValue");
                   if(isNumLine&&groupIdRow!=""&&groupIdRow!=undefined){//有采样组情况
                       store.each(function(model,rowIndex){
                            groupId=model.get("MEPTSampleItem_SampleFrom_MEPTSamplingGroup_Id");
                            if(groupIdRow==groupId){
                                if(model.get("isNumLine")==false){//不是数字行时,取出样本单Id
	                                id=model.get("MEPTSampleItem_SampleFrom_Id");
                                    removeArr.push(model);
	                            }
                            }
                            
                       });
                       
                       if(id!=""&&id!=undefined){
	                       c = function(text) {
	                            var result = Ext.JSON.decode(text);
	                            if (result.success) {
	                                
	                            } else {
	                                alertError(result.ErrorInfo);
	                            }
	                        };
	                       url=me.deleteDelMEPTSampleFormUrl+"?id="+id;
	                       getToServer(url, c);
                       }
                       
                   }
                });
                store.remove(removeArr);
            } else if (records.length == 0) {
                alertInfo('请选择一条数据进行操作！');
            }
        }else{
            var removeArr=[];
	        store.each(function(model,rowIndex){
	            //住院,体检,查询列表的删除
                checkSelect=model.get("checkSelect");
                if(checkSelect){
                   id=model.get("MEPTSampleForm_Id");
                   removeArr.push(model);
                   c = function(text) {
                        var result = Ext.JSON.decode(text);
                        if (result.success) {
                            gridList.deleteIndex = rowIndex;
                            //gridList.load(true);  
                        } else {
                            removeArr.remove(model);
                            alertError(result.ErrorInfo);
                        }
                    };
                    url=me.deleteDelMEPTSampleFormUrl+"?id="+id;
                   getToServer(url, c);
                }
	        });
            store.remove(removeArr);
	    }
        if(store.getCount()==0){
            me.setUserInfoToolbar(1,null);
            if(me.nodeTableSickType=='2'&&me.gridType=='outpatients'){
                var grid=me.getApplicationlist();
                var records=grid.getSelectionModel().getSelection();
                grid.store.remove(records);
            }
        }
	}
});