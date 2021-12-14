/**
 * 检验单信息，仪器样本信息
 * @author liangyl
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.dislocation.Grid',{
    extend:'Shell.class.lts.batch.Grid',
    title:'检验单信息，仪器样本信息',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    width:510,
    height:300,
    
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true',
	//显示成功信息
	showSuccessInfo:false,
	//消息框消失时间
	hideTimes:3000,
	
	//默认加载
	defaultLoad:false,
	//默认每页数量
	defaultPageSize:50,
	/**是否启用序号列*/
	hasRownumberer:true,
	//是否启用刷新按钮
	hasRefresh:true,
	defaultOrderBy:[{property:'LisTestForm_GTestDate',direction:'DESC'},{property:'LisTestForm_GSampleNoForOrder',direction:'DESC'}],
    /**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasPagingtoolbar:false,
	//检验日期默认值
	defaultDate:null,
	//小组ID
	SectionID:'',
	//fieldset布局
	fieldsetLayout:{type:'table',columns:5},
	//fieldset内部组件初始属性
	fieldsetDefaults: {labelWidth:70,width:180,labelAlign:'right'},
	//fieldset 宽度			
	fieldsetWidth:830,
	  //按钮是否可点击
	BUTTON_CAN_CLICK: true,
	//检验单范围所产生的样本号
	TestFormRangeSampleNo:"",
	//检验单范围所产生的样本号
	SampleNoList:[],
	//仪器样本单范围所产生的样本号
	EquipSampleNoList:[],
	defaultWhere :'listestform.MainStatusID>=0',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
        
		JShell.Action.delay(function() {
			//默认选择一个仪器
			me.initEqip();
		}, null, 200);
		
		//联动监听自动计算截止样本号
		me.initSampleNoListeners();
	},
	initComponent:function(){
		var me = this;
		//初始化检验日期
		me.initDate();
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'样本单ID',dataIndex:'LisTestForm_Id',width:100,
			sortable:false,menuDisabled:true,hidden:true
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本号',dataIndex:'LisTestForm_GSampleNo',width:100,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
		        return me.sampleNorenderer(value, meta, record, rowIndex, colIndex);
			}
		},{
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'状态',dataIndex:'LisTestForm_MainStatusID',width:100,hidden:false,
			sortable:false,menuDisabled:true,renderer : function(value, meta, record, rowIndex, colIndex) {
		        var html = me.onStatusRenderer(value, meta, record);
		        return html;
			}
		}, {
			xtype:'checkcolumn',text:'能否执行',dataIndex:'LisTestForm_IsExec',
			width:65,align:'center',sortable:false,menuDisabled:true,
			stopSelection:true,type:'boolean'
		},{
            text: '存在',dataIndex: 'LisTestForm_IsExist', width: 60,
            sortable:false,isBool: true,align: 'center',type: 'bool',defaultRenderer:true
        },{
			text:'←',dataIndex:'Direction',width:40,align:'center',
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'仪器样本单ID',dataIndex:'LisTestForm_EquipFormID',width:100,
			sortable:false,menuDisabled:true,hidden:true,defaultRenderer:true
		},{
			text:'检验仪器',dataIndex:'LisTestForm_EquipName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'仪器检验日期',dataIndex:'LisTestForm_EquipGTestDate',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本号',dataIndex:'LisTestForm_EquipSampleNo',width:100,
			sortable:false,menuDisabled:true,renderer : function(value, meta, record, rowIndex, colIndex) {
		        return me.sampleNorenderer(value, meta, record, rowIndex, colIndex);
			}
		} ,{
            text: '存在',dataIndex: 'LisTestForm_IsEquipExist', width: 60,
            sortable:false,isBool: true,align: 'center',type: 'bool',defaultRenderer:true
        }];
		
		return columns;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults,
			items = [{
	            xtype:'fieldset',title: '检验单范围',collapsible: true,itemId: 'Range',
	            defaultType: 'textfield',layout:layout,defaults:defaults,width:me.fieldsetWidth,
	            items :[{
	                 xtype: 'datefield',format: 'Y-m-d',fieldLabel:'检验日期',emptyText: '检验日期',itemId:'GTestDate',value:me.defaultDate
	            },{
	                fieldLabel:'开始样本号',itemId:'GSampleNo',emptyText: '开始样本号'
	            },{
		        	fieldLabel:'数量',emptyText: '数量',itemId:'GSampleNoForOrder',xtype:'numberfield'
		        },{
	                xtype:'displayfield',fieldLabel:'截止样本号',itemId:'EndGSampleNo',emptyText: '截止样本号'
	            }]
	        },{
	            xtype:'fieldset',title: '仪器样本范围',collapsible: true,itemId: 'EqipRange',
	            defaultType: 'textfield',layout:layout,defaults:defaults,width:me.fieldsetWidth,
	            items :[{
					xtype: 'uxCheckTrigger', fieldLabel: '仪器', itemId: 'EquipName',
					className: 'Shell.class.lts.sample.result.equip.extract.EquipCheckGrid',
					classConfig: {},emptyText: '仪器',
					listeners: {
						check: function (p, record) {
							me.getComponent('buttonsToolbar').getComponent('EqipRange').getComponent('EquipName').setValue(record ? record.get('LBEquip_CName') : '');
							me.getComponent('buttonsToolbar').getComponent('EqipRange').getComponent('EquipId').setValue(record ? record.get('LBEquip_Id') : '');
							p.close();
						}
					}
				}, {
					xtype: 'textfield',fieldLabel: '仪器编号',itemId: 'EquipId',hidden: true
				},{
	                 xtype: 'datefield',format: 'Y-m-d',fieldLabel:'检验日期',emptyText: '检验日期',itemId:'TGTestDate',value:me.defaultDate
	            },{
	                fieldLabel:'开始样本号',emptyText: '开始样本号',itemId:'TGSampleNo'
	            },{
	               xtype:'displayfield',fieldLabel:'截止样本号',itemId:'TEndGSampleNo'
	            },{
			    	xtype: 'button',text: '查询',tooltip: '查询',iconCls: 'button-search',width:65, margin: '0 0 0 20',
					handler:function(but,e){
						var buttonsToolbar = me.getComponent('buttonsToolbar'),
						    Range = buttonsToolbar.getComponent('Range'),
						    GTestDate = Range.getComponent('GTestDate'),
						   	EqipRange = buttonsToolbar.getComponent('EqipRange'),
						    TGTestDate = EqipRange.getComponent('TGTestDate');
                        
                        //时间校验
                        if(!GTestDate.isValid()) {
				        	me.hideMask();
				        	return;
				        }
                        if(!TGTestDate.isValid()) {
				        	me.hideMask();
				        	return;
				        }
		                me.onGridSearch();
			        }
				}]
	        },{
				xtype: 'label',
				text: '检验单、仪器样本信息',
				margin: '0 0 0 10',
				style: "font-weight:bold;color:blue;"
			}
	    ];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			//布局方式
			layout:'anchor',
			items: items
		});
	},
	
	/**初始化检验日期*/
	initDate: function() {
		var me = this;
		var Sysdate = JShell.System.Date.getDate();
		me.defaultDate =  JShell.Date.toString(Sysdate,true)
	},
	//仪器初始化
	initEqip : function(){
		var me = this;
		//设置下拉列表仪器 查询
		var EqipRange = me.getComponent("buttonsToolbar").getComponent('EqipRange');
		var picker = EqipRange.getComponent("EquipName").getPicker();
		if (me.SectionID)
			picker.externalWhere = 'sectionId=' + me.SectionID;
		else
			picker.externalWhere = 'sectionId=-1';
		//查询前清空之前数据
		EqipRange.getComponent('EquipName').onTriggerClick();
	},
		/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,params = [],
			obj = me.getSearchObj();		
		//小组Id
		if(me.SectionID) {
			params.push("listestform.LBSection.Id=" + me.SectionID + "");
		}
		if(obj.GTestDate){
			params.push("listestform.GTestDate='" + obj.GTestDate + "'");
		}
		if(me.TestFormRangeSampleNo){
			params.push("listestform.GSampleNo in(" + me.TestFormRangeSampleNo + ")");
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this,
		    obj = me.getSearchObj();
		data.count=obj.GSampleNoForOrder;
	    //数据处理返回list
		data.list = me.resultList(data);
		return data;
	},
	//返回list 处理
	resultList :function(data){
		var me = this,
		    list = [], obj = me.getSearchObj();
	    var SampleNoArr=[],IsExistArr=[],IsEquipExistArr=[];

		for(var i=0;i<data.count;i++ ){
			list.push({
				LisTestForm_GTestDate:obj.GTestDate,
				LisTestForm_EquipGTestDate:obj.TGTestDate,
				LisTestForm_GSampleNo:me.SampleNoList[i],
				LisTestForm_EquipSampleNo:me.EquipSampleNoList[i],
				LisTestForm_EquipName:obj.EquipID,
				Direction:'←',
				LisTestForm_IsEquipExist :'',
				LisTestForm_IsExist :'',
				LisTestForm_IsExec : "false"
			});
			for( var j=0;j<data.list.length;j++){
				if(data.list[j].LisTestForm_GSampleNo == me.SampleNoList[i]){
					list[i].LisTestForm_Id= data.list[j].LisTestForm_Id;
					list[i].LisTestForm_GSampleNo= data.list[j].LisTestForm_GSampleNo;
					list[i].LisTestForm_BarCode = data.list[j].LisTestForm_BarCode;
					list[i].LisTestForm_CName = data.list[j].LisTestForm_CName;
					list[i].LisTestForm_MainStatusID = data.list[j].LisTestForm_MainStatusID;
					list[i].LisTestForm_EquipFormID = data.list[j].LisTestForm_EquipFormID;
					list[i].LisTestForm_IsExist= '1';
					if(data.list[j].LisTestForm_EquipFormID)list[i].LisTestForm_IsEquipExist = '1';
					if(data.list[j].LisTestForm_MainStatusID==0){
						if(data.list[j].LisTestForm_EquipFormID)list[i].LisTestForm_IsExec = "true";
					}else{
						SampleNoArr.push('【'+me.SampleNoList[i]+'】');
					}
	                break;
				}
			}
			if(!list[i].LisTestForm_IsExist)IsExistArr.push('【'+me.SampleNoList[i]+'】');
			if(!list[i].LisTestForm_IsEquipExist)IsEquipExistArr.push('【'+me.SampleNoList[i]+'】');
		}
		me.tipMsg(SampleNoArr,IsExistArr,IsEquipExistArr);
		return list;
	},
	//不能执行原有提示（如果非状态中,样本单不存在，或者仪器样本单不存在，背景灰色显示，并提醒用户，不做错位处理。 ）
	tipMsg:function(SampleNoArr,IsExistArr,IsEquipExistArr){
		var me = this;
		var msg ="";
		if(SampleNoArr.length>0){
    		msg+='检验单样本号为'+SampleNoArr.join(',')+'的检验单状态不是检验中,不能执行错位处理!';
    	}
    	if(IsExistArr.length>0){
    		if(msg)msg+='<br/>';
    		msg+='检验单样本号为'+IsExistArr.join(',')+'的检验单不存在,不能执行错位处理!';
    	}
    	if(IsEquipExistArr.length>0){
    		if(msg)msg+='<br/>';
    		msg+='检验单样本号为'+IsEquipExistArr.join(',')+'的仪器样本单不存在,不能执行错位处理!';
    	}
    	if(msg)JShell.Msg.warning(msg);
	},
	getSearchObj : function(){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
		    Range = buttonsToolbar.getComponent('Range'),
		    GTestDate = Range.getComponent('GTestDate').getValue(),
		    GSampleNo = Range.getComponent('GSampleNo').getValue(),
		    GSampleNoForOrder = Range.getComponent('GSampleNoForOrder').getValue(),
		    EqipRange = buttonsToolbar.getComponent('EqipRange'),
		    EquipID = EqipRange.getComponent('EquipName').getValue(), 
		    TGTestDate = EqipRange.getComponent('TGTestDate').getValue(),
		    TGSampleNo = EqipRange.getComponent('TGSampleNo').getValue();
		return {
			GTestDate:JShell.Date.toString(GTestDate,true),
			GSampleNo:GSampleNo,
			GSampleNoForOrder:GSampleNoForOrder,
			TGTestDate:JShell.Date.toString(TGTestDate,true),
			EquipID:EquipID,
			TGSampleNo:TGSampleNo
		}
	},
	//合并前校验
	isValidMerge : function(){
		var me = this,
		    msg = '',str1='',str2='';
		var obj = me.getSearchObj();
        if(!obj.GTestDate)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
        //判断日期是否有效
//		if (!(obj.GTestDate instanceof Date))str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
        if(!obj.GSampleNo)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
        if(!obj.GSampleNoForOrder)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验单数量不能为空!';
        if(str1)msg+='</br><span style="font-weight:bold;color:blue;">检验单当前样本号的</span>'+str1;
		if(!obj.TGTestDate)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
		 //判断日期是否有效
//		if (!(obj.TGTestDate instanceof Date))str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
      
        if(!obj.TGSampleNo)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
        if(str2)msg+='</br><span style="font-weight:bold;color:blue;">检验单目标样本号的</span>'+str2;
        return msg;
	},
	initSampleNoListeners:function(){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
		    Range = buttonsToolbar.getComponent('Range'),
		    GSampleNo = Range.getComponent('GSampleNo'),
		    EndGSampleNo = Range.getComponent('EndGSampleNo'),
		    GSampleNoForOrder = Range.getComponent('GSampleNoForOrder'),
		    EqipRange = buttonsToolbar.getComponent('EqipRange'),
		    TGSampleNo = EqipRange.getComponent('TGSampleNo'),
		    TEndGSampleNo= EqipRange.getComponent('TEndGSampleNo');
		
		//样本号改变
		GSampleNo.on({
			change : function(com,newValue,oldValue,eOpts ){
				EndGSampleNo.setValue('');
				if(GSampleNoForOrder.getValue() && newValue){
					//查询样本截止样本单号
				    me.endSampleNo(newValue,GSampleNoForOrder.getValue(),EndGSampleNo);
				}
			}
		});
		
		//数量改变
		GSampleNoForOrder.on({
			change : function(com,newValue,oldValue,eOpts ){
				EndGSampleNo.setValue('');
				TEndGSampleNo.setValue('');
				if(GSampleNo.getValue() && newValue){
					//查询样本截止样本单号
				    me.endSampleNo(GSampleNo.getValue(),newValue,EndGSampleNo);
				}
				if(TGSampleNo.getValue() && newValue){
					//查询目标样本截止样本单号
				    me.endSampleNo(TGSampleNo.getValue(),newValue,TEndGSampleNo);
				}
			}
		});
		
		//目标样本号改变
		TGSampleNo.on({
			change : function(com,newValue,oldValue,eOpts ){
				TEndGSampleNo.setValue('');
				if(GSampleNoForOrder.getValue() && newValue){
					//查询目标样本截止样本单号
				    me.endSampleNo(newValue,GSampleNoForOrder.getValue(),TEndGSampleNo);
				}
			}
		});
	},
	sampleNorenderer:function(value, meta, record,rowIndex, colIndex){
		var me = this;
	    if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
	    meta.style = 'color:red';
        return value;
	},
    //获取检验单id和仪器检验单id字符列表
    getIdList:function(){
    	var me = this,
    		records = me.store.data.items;
    	var testFormIDList=[],equipFormIDList=[];
    	for(var i =0;i<records.length;i++){
    		if(records[i].data.LisTestForm_IsExec){
    			testFormIDList.push(records[i].data.LisTestForm_Id);
    		    equipFormIDList.push(records[i].data.LisTestForm_EquipFormID);
    		}
    	}
    	return {
    		testFormIDList:testFormIDList.join(','),
    		equipFormIDList:equipFormIDList.join(',')
    	};
    },
    onGridSearch:function(){
    	var me = this;
    	//确定前验证
	    var msg = me.isValidMerge(),
	        obj = me.getSearchObj();
	    if(msg)	{
			JShell.Msg.warning(msg);
			return;
		}
	    //检验单范围所产生的样本号
	    me.batchCreateSampleNo(obj.GSampleNo,obj.GSampleNoForOrder,function(data){
	    	if(!data){
	    		JShell.Msg.warning('请输入正确的样本号格式');
	    		return;
	    	}
	    	var SampleNoArr = data.split(',');
	    	me.SampleNoList=SampleNoArr;
	    	me.TestFormRangeSampleNo = "";
            for(var i=0;i<SampleNoArr.length;i++){
            	if(me.TestFormRangeSampleNo.length>1)me.TestFormRangeSampleNo+=",";
            	me.TestFormRangeSampleNo+="'"+SampleNoArr[i]+"'";
            }
             //查询目标样本号批量生成的新样本号
            me.batchCreateSampleNo(obj.TGSampleNo,obj.GSampleNoForOrder,function(data2){
	    	     if(!data2){
		    		JShell.Msg.warning('请输入正确的样本号格式');
		    		return;
		    	}
	    	    me.EquipSampleNoList = data2.split(',');
	    	    me.onSearch();
	    	});
	    });
    }
});