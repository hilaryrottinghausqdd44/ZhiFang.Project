/**
 * 样本检验列表
 * @author Jcall
 * @version 2019-11-05
 * 默认日期+样本号order正序排序，其他字段各自排序，点击日期或者样本号都是日期+样本号order正序排序；
 * 默认查询日期：按设置的参数来
 */
Ext.define('Shell.class.lts.sample.Grid2',{
    extend:'Shell.ux.grid.Panel',
    requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
    title:'样本检验列表',
    width:410,
    height:800,
    
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true',
    //修改服务地址
	editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestFormByField',
	//删除数据服务路径
	delUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_DelLisTestForm',
	
	//显示成功信息
	showSuccessInfo:false,
	//消息框消失时间
	hideTimes:3000,
	
	//默认加载
	defaultLoad:false,
	//默认每页数量
	defaultPageSize:200,
	/**是否启用序号列*/
	hasRownumberer:false,

	//复选框
	multiSelect:true,
	selType:'checkboxmodel',
	
	//查询日期类型列表
	DATE_TYPE_LIST:[['GTestDate','检验日期'],['CheckTime','审核日期']],
	//查询日期类型默认值
	DEFAULT_DATE_TYPE_VALUE:'GTestDate',
	//查询日期默认值
	DEFAULT_DATE_VALUE:null,
	
	//过滤条件复选框属性列表
    STATUS_LIST:[
    	{text:'验中',iconText:'检',color:'#FFB800',where:'listestform.MainStatusID=0',checked:true},
    	{text:'验确认',iconText:'检',color:'#5FB878',where:'listestform.MainStatusID=2',checked:true},
    	{text:'核',iconText:'审',color:'#009688',where:'listestform.MainStatusID=3',checked:true},
    	{text:'审',iconText:'反',color:'#1E9FFF',where:'(listestform.MainStatusID=0 and listestform.CheckTime<>null)'},
    	{text:'',iconText:'废',color:'#c2c2c2',where:'listestform.MainStatusID=-2'},
    	{text:'检',iconText:'复',color:'#2F4056',where:'listestform.RedoStatus=1'}
    ],
	//状态样色
	ColorMap:{
		"危急值":{text:'危急值',iconText:'危',color:'red'},
		"检验完成":{text:'√检验完成',iconText:'√',color:'green'},
		"急诊":{text:'急诊',iconText:'急',color:'red'},
		"特殊样本":{text:'特殊样本',iconText:'特',color:'red'},
		"阳性保卡":{text:'阳性保卡',iconText:'阳',color:'#F08080'},
		"打印":{text:'急诊',iconText:'急',color:'#00BFFF'},
		
		"智能审核通过":{text:'√智能审核通过',iconText:'√',color:'green'},
		"智能审核未通过":{text:'×智能审核未通过',iconText:'×',color:'red'},
		"仪器通过":{text:'√仪器状态通过',iconText:'仪',color:'green'},
		"仪器警告":{text:'仪器状态警告',iconText:'仪',color:'orange'},
		"仪器异常":{text:'×仪器状态异常',iconText:'仪',color:'red'}
	},
    
    //主状态样式模板
    MainStatusTemplet:'<span style="margin-right:1px;border:1px solid {color};color:#fff;background-color:{color};"><a>{text}</a></span>',
    //颜色条模板
    ColorLineTemplet:'<span style="padding:0 1px;margin-right:1px;border:1px solid {color};background:{color};"></span>',
    //悬浮内容样式
    TipsTemplet:'<span style=\'margin:1px 1px 0 0;border:1px solid {color};color:{color};\'>{text}</span>',
    //打印图标样式
    PrintTemplet:'<img  src="'+JShell.System.Path.ROOT+'/ui/extjs/css/images/buttons/print1.png"/>',
	//查询项列表
	SEARCH_TYPE_LIST:[
		['GSampleNo','样本号'],['BarCode','条码号'],
		['CName','病人姓名'],['PatNo','病历号'],['PatNo','卡号']
	],
	//查询项默认值
	DEFAULT_SEARCH_TYPE_VALUE:'GSampleNo',
	
	//默认排序字段
	defaultOrderBy:[
		{property:'LisTestForm_GTestDate',direction:'DESC'},
		{property:'LisTestForm_GSampleNoForOrder',direction:'DESC'}
	],
	//记录日期的排序类型
	dateDirection: 'DESC',
	//小组ID
    sectionId:null,
    //第一次加载数据
    firstLoad:true,
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.onChangeDateByDays(-2);
	},
	initComponent:function(){
		var me = this;
		//默认数据条件
		if(me.sectionId){
			me.defaultWhere = 'listestform.LBSection.Id=' + me.sectionId;
		}
		//初始化查询日期
		me.initDefaultDate();
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//初始化查询日期
	initDefaultDate:function(){
		var me = this;
			date=new Date();
			
		//me.DEFAULT_DATE_VALUE = {start:date,end:date};
	},
	
	//创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'状态',dataIndex:'LisTestForm_StatusID',width:75,
			sortable:false,menuDisabled:true,renderer:me.onStatusRenderer
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,renderer:function(value,meta,record){
				var v = JShell.Date.toString(value,true) || '',
					tipText = v;
					
				//日期列背景色-智能审核->ZFSysCheckStatus,-1:智能审核不通过;0:无;1:智能审核通过; 
				var ZFSysCheckStatus = record.get('LisTestForm_ZFSysCheckStatus');
				if(ZFSysCheckStatus == '1'){
					v = '<span style="margin-right:1px;color:' + me.ColorMap.智能审核通过.color + ';"><a>' + 
						me.ColorMap.智能审核通过.iconText + '</a></span>'  + v;
					tipText = me.TipsTemplet.replace(/{color}/g,me.ColorMap.智能审核通过.color).
						replace(/{text}/g,me.ColorMap.智能审核通过.text) + tipText;
				}else if(ZFSysCheckStatus == '-1'){
					v = '<span style="margin-right:1px;color:' + me.ColorMap.智能审核未通过.color + ';"><a>' + 
						me.ColorMap.智能审核未通过.iconText + '</a></span>'  + v;
					tipText = me.TipsTemplet.replace(/{color}/g,me.ColorMap.智能审核未通过.color).
						replace(/{text}/g,me.ColorMap.智能审核未通过.text) + tipText;
				}
				
				meta.tdAttr = 'data-qtip="' + tipText + '"';
				return v;
			}
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:80,renderer:function(value,meta,record){
				var v = value,
					tipText = v;
				
				//姓名背景色-标注样本（特殊样本）->SampleSpecialDesc!=''
				var SampleSpecialDesc = record.get('LisTestForm_SampleSpecialDesc');
				if(SampleSpecialDesc != ''){
					v = me.TipsTemplet.replace(/{color}/g,me.ColorMap.特殊样本.color).replace(/{text}/g,me.ColorMap.特殊样本.iconText) + v;
					tipText = me.TipsTemplet.replace(/{color}/g,me.ColorMap.特殊样本.color).replace(/{text}/g,me.ColorMap.特殊样本.text) + tipText;
				}
				
				meta.tdAttr = 'data-qtip="' + tipText + '"';
				return v;
			}
		},{
			text:'病历号',dataIndex:'LisTestForm_PatNo',width:100,hidden:true
		},{
			text:'样本号',dataIndex:'LisTestForm_GSampleNoForOrder',width:60,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNo'),
					tipText = v;
				
				//样本号背景色-急诊->UrgentState!=''
				var RedoStatus = record.get('LisTestForm_UrgentState');
				if(RedoStatus){
					v = me.TipsTemplet.replace(/{color}/g,me.ColorMap.急诊.color).replace(/{text}/g,me.ColorMap.急诊.iconText) + v;
					tipText = me.TipsTemplet.replace(/{color}/g,me.ColorMap.急诊.color).replace(/{text}/g,me.ColorMap.急诊.text) + tipText;
				}
				
				meta.tdAttr = 'data-qtip="' + tipText + '"';
				return v;
			}
		},{
			text:'样本号排序',dataIndex:'LisTestForm_GSampleNo',width:150,hidden:true,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNoForOrder');
				meta.tdAttr = 'data-qtip="' + v + '"';
				return v;
			}
		},{
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:120,defaultRenderer:true
		},{
			text:'性别',dataIndex:'LisTestForm_LisPatient_GenderName',width:40,defaultRenderer:true
		},{
			text:'样本类型',dataIndex:'LisTestForm_GSampleType',width:70,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LisTestForm_Id',width:190,isKey:true,hidden:true,hideable:false
		},{
			text:'小组ID',dataIndex:'LisTestForm_LBSection_Id',width:190,hidden:true,hideable:false
		},{
			text:'医嘱单ID',dataIndex:'LisTestForm_LisOrderForm_Id',width:190,hidden:true,hideable:false
		},{
			text:'检验单主状态',dataIndex:'LisTestForm_MainStatusID',width:100,hidden:true,hideable:false
		},{
			text:'仪器ID',dataIndex:'LisTestForm_EquipFormID',width:100,hidden:true,hideable:false
		},{
			text:'主状态',dataIndex:'LisTestForm_MainStatusID',width:60,hidden:true,hideable:false
		},{
			text:'打印次数',dataIndex:'LisTestForm_PrintCount',width:80,hidden:true,hideable:false
		},{
			text:'审核时间',dataIndex:'LisTestForm_CheckTime',width:80,hidden:true,hideable:false
		},{
			text:'复检',dataIndex:'LisTestForm_RedoStatus',width:80,hidden:true,hideable:false
		},{
			text:'急诊',dataIndex:'LisTestForm_UrgentState',width:80,hidden:true,hideable:false
		},{
			text:'标注样本',dataIndex:'LisTestForm_SampleSpecialDesc',width:80,hidden:true,hideable:false
		},{
			text:'仪器状态',dataIndex:'LisTestForm_ESendStatus',width:80,hidden:true,hideable:false
		},{
			text:'报告状态',dataIndex:'LisTestForm_ReportStatusID',width:80,hidden:true,hideable:false
		},{
			text:'检验完成',dataIndex:'LisTestForm_TestAllStatus',width:80,hidden:true,hideable:false
		},{
			text:'只能审核',dataIndex:'LisTestForm_ZFSysCheckStatus',width:80,hidden:true,hideable:false
		},{
			text:'检验单信息基本完成状态',dataIndex:'LisTestForm_FormInfoStatus',width:80,hidden:true,hideable:false
		}];
		
		return columns;
	},
	//鼠标悬浮显示
	onShowTips:function(record){
		var me = this,
			html = [];
			
		html.push(
			'<div style=\'text-align:center;\'><b>状态信息</b></div>' +
			'<div>' +
				'<a>检验日期：' + JShell.Date.toString(record.get('LisTestForm_GTestDate'),true) + '</a>' +
				'<a style=\'margin-left:10px;\'>' + record.get('LisTestForm_CName') + 
					'(' + record.get('LisTestForm_LisPatient_GenderName') + 
				')</a>' +
			'</div>'
		);
		
		//主状态->检0、初2、终3、反(0 and CheckTime<>null)、废-2
		var MainStatusID = record.get('LisTestForm_MainStatusID'),
			CheckTime = record.get('LisTestForm_CheckTime');
			
		if(MainStatusID == '0'){
			if(!CheckTime){
				html.push(me.TipsTemplet.replace(/{color}/g,me.STATUS_LIST[0].color).replace(/{text}/g,me.STATUS_LIST[0].text));
			}else{
				html.push(me.TipsTemplet.replace(/{color}/g,me.STATUS_LIST[4].color).replace(/{text}/g,me.STATUS_LIST[4].text));
			}
		}else if(MainStatusID == '2'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.STATUS_LIST[1].color).replace(/{text}/g,me.STATUS_LIST[1].text));
		}else if(MainStatusID == '3'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.STATUS_LIST[2].color).replace(/{text}/g,me.STATUS_LIST[2].text));
		}else if(MainStatusID == '-2'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.STATUS_LIST[4].color).replace(/{text}/g,me.STATUS_LIST[4].text));
		}
		
		//检验完成->TestAllStatus,0:未开始;1:已开始;2:检验完成:绿色;
		var TestAllStatus = record.get('LisTestForm_TestAllStatus');
		if(TestAllStatus == '1'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.检验完成.color).replace(/{text}/g,me.ColorMap.检验完成.text));
		}
		
		//日期列背景色-智能审核->ZFSysCheckStatus,-1:智能审核不通过;0:无;1:智能审核通过; 
		var ZFSysCheckStatus = record.get('LisTestForm_ZFSysCheckStatus');
		if(ZFSysCheckStatus == '1'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.智能审核通过.color).replace(/{text}/g,me.ColorMap.智能审核通过.text));
		}else if(ZFSysCheckStatus == '-1'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.智能审核未通过.color).replace(/{text}/g,me.ColorMap.智能审核未通过.text));
		}
		
		//复检->RedoStatus,0/1
		var RedoStatus = record.get('LisTestForm_RedoStatus');
		if(RedoStatus == '1'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.STATUS_LIST[5].color).replace(/{text}/g,me.STATUS_LIST[5].text));
		}
		
		//样本号背景色-急诊->UrgentState!=''
		var RedoStatus = record.get('LisTestForm_UrgentState');
		if(RedoStatus){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.急诊.color).replace(/{text}/g,me.ColorMap.急诊.text));
		}
		
		//姓名背景色-标注样本（特殊样本）->SampleSpecialDesc!=''
		var SampleSpecialDesc = record.get('LisTestForm_SampleSpecialDesc');
		if(SampleSpecialDesc != ''){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.特殊样本.color).replace(/{text}/g,me.ColorMap.特殊样本.text));
		}
		
		//仪器状态->ESendStatus,0:无;1:通过 绿色;2:警告 黄色;3:异常 红色;
		var ESendStatus = record.get('LisTestForm_ESendStatus') + '';
		if(ESendStatus == '1'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.仪器通过.color).replace(/{text}/g,me.ColorMap.仪器通过.text));
		}else if(ESendStatus == '2'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.仪器警告.color).replace(/{text}/g,me.ColorMap.仪器警告.text));
		}else if(ESendStatus == '3'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.仪器异常.color).replace(/{text}/g,me.ColorMap.仪器异常.text));
		}
		
		//危急值->ReportStatusID,倒数第2位=1
		var ReportStatusID = record.get('LisTestForm_ReportStatusID') + '';
		if(ReportStatusID && ReportStatusID.length >= 2 && ReportStatusID.charAt(ReportStatusID.length-2) == '1'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.危急值.color).replace(/{text}/g,me.ColorMap.危急值.text));
		}
		//阳性保卡->ReportStatusID,倒数第1位=1
		if(ReportStatusID && ReportStatusID.length >= 1 && ReportStatusID.charAt(ReportStatusID.length-1) == '1'){
			html.push(me.TipsTemplet.replace(/{color}/g,me.ColorMap.阳性保卡.color).replace(/{text}/g,me.ColorMap.阳性保卡.text));
		}
		
		//打印->PrintCount>0
		var PrintCount = record.get('LisTestForm_PrintCount');
		if(PrintCount && PrintCount > 0){
			html.push('<div>已打印' + PrintCount + '次</div>');
		}
		
		return html.join('</BR>');
		
	},
	//状态列显示处理
	onStatusRenderer:function(value,meta,record){
		var me = this,
			html = [];
		
		//主状态->检0、初2、终3、反(0 and CheckTime<>null)、废-2
		var MainStatusID = record.get('LisTestForm_MainStatusID'),
			CheckTime = record.get('LisTestForm_CheckTime');
			
		if(MainStatusID == '0'){
			if(!CheckTime){
				html.push(me.MainStatusTemplet.replace(/{color}/g,me.STATUS_LIST[0].color).replace(/{text}/g,me.STATUS_LIST[0].iconText));
			}else{
				html.push(me.MainStatusTemplet.replace(/{color}/g,me.STATUS_LIST[3].color).replace(/{text}/g,me.STATUS_LIST[3].iconText));
			}
		}else if(MainStatusID == '2'){
			html.push(me.MainStatusTemplet.replace(/{color}/g,me.STATUS_LIST[1].color).replace(/{text}/g,me.STATUS_LIST[1].iconText));
		}else if(MainStatusID == '3'){
			html.push(me.MainStatusTemplet.replace(/{color}/g,me.STATUS_LIST[2].color).replace(/{text}/g,me.STATUS_LIST[2].iconText));
		}else if(MainStatusID == '-2'){
			html.push(me.MainStatusTemplet.replace(/{color}/g,me.STATUS_LIST[4].color).replace(/{text}/g,me.STATUS_LIST[4].iconText));
		}
		
		//危急值->ReportStatusID,倒数第2位=1
		var ReportStatusID = record.get('LisTestForm_ReportStatusID') + '';
		if(ReportStatusID && ReportStatusID.length >= 2 && ReportStatusID.charAt(ReportStatusID.length-2) == '1'){
			html.push(me.MainStatusTemplet.replace(/{color}/g,me.ColorMap.危急值.color).replace(/{text}/g,me.ColorMap.危急值.iconText));
		}
		
		//检验完成->TestAllStatus,0:未开始;1:检验完成:绿色;
		var TestAllStatus = record.get('LisTestForm_TestAllStatus');
		if(TestAllStatus == '1'){
			html.push(me.MainStatusTemplet.replace(/{color}/g,me.ColorMap.检验完成.color).replace(/{text}/g,me.ColorMap.检验完成.iconText));
		}

		//复检->RedoStatus,0/1
		var RedoStatus = record.get('LisTestForm_RedoStatus');
		if(RedoStatus == '1'){
			html.push(me.ColorLineTemplet.replace(/{color}/g,me.STATUS_LIST[5].color));
		}
		
		//仪器状态->ESendStatus,0:无;1:通过 绿色;2:警告 黄色;3:异常 红色;
		var ESendStatus = record.get('LisTestForm_ESendStatus') + '';
		if(ESendStatus == '1'){
			html.push(me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.仪器通过.color));
		}else if(ESendStatus == '2'){
			html.push(me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.仪器警告.color));
		}else if(ESendStatus == '3'){
			html.push(me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.仪器异常.color));
		}
		
		//阳性保卡->ReportStatusID,倒数第1位=1
		var ReportStatusID = record.get('LisTestForm_ReportStatusID') + '';
		if(ReportStatusID && ReportStatusID.length >= 1 && ReportStatusID.charAt(ReportStatusID.length-1) == '1'){
			html.push(me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.阳性保卡.color));
		}
		
		//打印->PrintCount>0
		var PrintCount = record.get('LisTestForm_PrintCount');
		if(PrintCount && PrintCount > 0){
			html.push(me.PrintTemplet);
//			html.push(me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.打印.color));
		}
		
		meta.tdAttr = 'data-qtip="' + me.onShowTips(record) + '"';
		
		return html.join('');
	},
	
	//创建挂靠功能栏
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
			
		//创建日期快捷栏
		items.push(me.createDateToolbar());
		//创建状态说明栏
		items.push(me.createColorToolbar());
		//创建内容查询栏
		items.push(me.createValueSearchToolbar());
		//创建日期查询栏
		items.push(me.createDateSearchToolbar());
		//创建条码查询栏
        items.push(me.createBarcodeToolbar());
        //创建分页
         items.push(me.createPagingtoolbar());
		//创建状态过滤栏
		if(me.STATUS_LIST && me.STATUS_LIST.length > 0){
			items.push(me.createStatusToolbar());
		}
		
		return items;
	},
	//创建状态过滤栏
	createStatusToolbar:function(){
		var me = this,
			items = [];
		
		 if(me.STATUS_LIST && me.STATUS_LIST.length > 0){
		 	items.push('->');
	    	for(var i in me.STATUS_LIST){
	    		var iconCls = me.MainStatusTemplet.replace(/{color}/g,me.STATUS_LIST[i].color).replace(/{text}/g,me.STATUS_LIST[i].iconText);
	    		
	    		items.push({
	    			xtype:'checkbox',
	    			style:{
						//color:me.STATUS_LIST[i].color,
						marginRight:(i == me.STATUS_LIST.length-1) ? '10px' : ''
					},
	    			boxLabel:iconCls + me.STATUS_LIST[i].text,
	    			where:me.STATUS_LIST[i].where,
	    			checked:me.STATUS_LIST[i].checked,
	    			listeners:{
	    				change:function(com,newValue){
	    					JShell.Action.delay(function(){
	    						me.onSearch();
	    					},null,200);
	    					
	    				}
	    			}
	    		});
	    	}
	    }
		var dockedItems = {
			xtype:'toolbar',dock:'top',itemId:'StatusToolbar',
			items:items
		};
		
		return dockedItems;
	},
	//创建日期快捷栏
	createDateToolbar:function(){
		var me = this;
		
		var dockedItems = {
			xtype:'toolbar',dock:'top',itemId:'DateToolbar',
			items:[{
				xtype:'button',itemId:'CurDay',text:'今天',iconCls:'button-search',
				handler:function(button,e){me.onChangeDateByDays(0);}
			},{
				xtype:'button',itemId:'Day2',text:'两天',iconCls:'button-search',
				handler:function(button,e){me.onChangeDateByDays(-1);}
			},{
				xtype:'button',itemId:'Day3',text:'三天 ',iconCls:'button-search',
				handler:function(button,e){me.onChangeDateByDays(-2);}
			},{
				xtype:'button',itemId:'Day7',text:'七天',iconCls:'button-search',
				handler:function(button,e){me.onChangeDateByDays(-6);}
			},'-',{
				xtype:'button',itemId:'DayAll',text:'全部',iconCls:'button-search',
				handler:function(button,e){me.onChangeDateByDays(null);}
			},{
				xtype:'button',itemId:'DayConfig',text:'设置',iconCls:'button-config',hidden:true,
				handler:function(button,e){
					
				}
			}]
		};
		return dockedItems;
	},
	//时间范围赋值
	onChangeDateByDays: function(days){
		var me = this,
			DateSearchToolbar = me.getComponent('DateSearchToolbar'),
			DateValue = DateSearchToolbar.getComponent('DateValue');
			
		if(DateValue){
			var start = null,
				end=null;
			if(days != null){
				end = new Date();
				start = JShell.Date.getNextDate(end,days);
			}
			DateValue.setValue({start:start,end:end});
		}
		
		me.onSearch();
	},
	//创建日期查询栏
	createDateSearchToolbar:function(){
		var me = this;
		var items = {
			xtype:'toolbar',dock:'top',itemId:'DateSearchToolbar',
			items:[{
				width:80,labelWidth:0,labelAlign:'right',
				xtype:'uxSimpleComboBox',itemId:'DateType',fieldLabel:'',
				data:me.DATE_TYPE_LIST,
				value:me.DEFAULT_DATE_TYPE_VALUE,
				listeners:{
					change:function(com,newValue,oldValue,eOpts){
						me.onSearch();
					}
				}
			},{
				xtype:'uxdatearea',itemId:'DateValue',width:200,
				labelWidth:0,labelAlign:'right',fieldLabel:'',
				value:me.DEFAULT_DATE_VALUE,
				listeners:{
					enter:function(){me.onSearch();}
				}
			},'-',{
				text:'查询',tooltip:'查询',iconCls:'button-search',
				handler:function(button,e){me.onSearch();}
			}]
		};
			
		return items;
	},
	//创建条码查询栏
	createBarcodeToolbar:function(){
		var me = this;
		var items = {
			xtype:'toolbar',dock:'top',itemId:'BarcodeToolbar',
			items:[{
				xtype:'checkboxfield',margin:'0 5 0 5',boxLabel:'',
				name: 'cbSelect',itemId:'cbSelect',
				checked:false,labelSeparator:'',
				listeners:{
					change:function(com,newValue,oldValue,eOpts){
						var SampleNum = com.ownerCt.getComponent('SampleNum');
						if(newValue){
							SampleNum.emptyText='自动新增';
							SampleNum.setValue('');
							SampleNum.setReadOnly(true);
						}else{
							SampleNum.setValue('');
							SampleNum.emptyText='';
							SampleNum.setReadOnly(false);
						}
						SampleNum.applyEmptyText();
					}
				}
			},{
				width:110,labelSeparator:'',itemId:'SampleNum',xtype:'textfield',emptyText:''
			},{
				xtype:'trigger',triggerCls:'x-form-search-trigger',
				enableKeyEvents: true, tooltip: '请在此扫描条码号',
				emptyText: '请在此扫描条码号', width: 200, hidden: me.sectionId ? false : true,
				onTriggerClick:function(){},
				listeners:{
					keyup:{
						fn:function(field,e){
							if(e.getKey() == Ext.EventObject.ESC){
								
							}else if(e.getKey() == Ext.EventObject.ENTER){
								
							}
						}
					}
				}
			}]
		};
		
		return items;
	},
	//创建内容查询栏
	createValueSearchToolbar:function(){
		var me = this;
		
		var dockedItems = {
			xtype:'toolbar',dock:'bottom',itemId:'ValueSearchToolbar',
			items:[{
				width:90,labelWidth:0,labelAlign:'right',
				xtype:'uxSimpleComboBox',itemId:'FieldType',fieldLabel:'',
				data:me.SEARCH_TYPE_LIST,
				value:me.DEFAULT_SEARCH_TYPE_VALUE,
				listeners:{
					change:function(com,newValue,oldValue,eOpts ){
						var FieldValue = com.ownerCt.getComponent('FieldValue').getValue();
						if(FieldValue){
							me.onSearch();
						}
					}
				}
			},{
				xtype:'trigger',triggerCls:'x-form-search-trigger',
				enableKeyEvents:true,tooltip:'输入值后请回车',itemId:'FieldValue',
				emptyText:'输入值后请回车',width:160,
				onTriggerClick:function(){me.onSearch();},
				listeners:{
					keyup:{
						fn:function(field,e){
							if(e.getKey() == Ext.EventObject.ESC){
								field.setValue('');
							}else if(e.getKey() == Ext.EventObject.ENTER){
								me.onSearch();
							}
						}
					}
				}
			},{
				xtype:'checkboxfield',margin:'0 5 0 5',boxLabel:'模糊匹配',itemId:'isLike',
				checked:true,labelSeparator:'',
				listeners:{
					change:function(com,newValue,oldValue,eOpts){
						var FieldValue = com.ownerCt.getComponent('FieldValue').getValue();
						if(FieldValue){
							me.onSearch();
						}
					}
				}
			}]
		};
		return dockedItems;
	},
	//创建状态说明栏
	createColorToolbar: function(){
		var me = this;
		
		var dockedItems = {
			xtype:'toolbar',dock:'bottom',itemId:'ColorToolbar',
			defaultType:'label',
			items:[{
				xtype:'label',text:'状态说明:',itemId:'ItemName',name:'ItemName'
			},{
				html:me.MainStatusTemplet.replace(/{color}/g,me.ColorMap.检验完成.color).replace(/{text}/g,me.ColorMap.检验完成.iconText) + '检验完成'
			},{
				html:'&nbsp;智能审核:'+'<span style="margin-right:1px;color:' + me.ColorMap.智能审核通过.color + ';"><a>' + 
					me.ColorMap.智能审核通过.iconText + '</a></span>成功' +
					',<span style="margin-right:1px;color:' + me.ColorMap.智能审核未通过.color + ';"><a>' + 
					me.ColorMap.智能审核未通过.iconText + '</a></span>失败'
			},{
				html:me.TipsTemplet.replace(/{color}/g,me.ColorMap.急诊.color).replace(/{text}/g,me.ColorMap.急诊.iconText) + '急诊'
//				html:me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.急诊.color) + '急诊'
			},{
			    html:me.TipsTemplet.replace(/{color}/g,me.ColorMap.特殊样本.color).replace(/{text}/g,me.ColorMap.特殊样本.iconText) + '特殊样本'
//				html:me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.特殊样本.color) + '特殊样本'
			},{
				xtype:'button',text:'其他',handler:function(){
					JShell.Win.open('Shell.class.lts.status.Info',{
						ColorMap:me.ColorMap,
						TipsTemplet:me.TipsTemplet,
						ColorLineTemplet:me.ColorLineTemplet,
						MainStatusTemplet:me.MainStatusTemplet,
						PrintTemplet:me.PrintTemplet,
						maximizable:false,resizable:false
					}).show();
				}
			}]
		};
		return dockedItems;
	},
	
	//加载数据前
	onBeforeLoad: function (m, operation){
		var me = this,
			where = [];
		me.fireEvent('AllGroupOnBeforeLoad', me);
		//日期查询栏
		var DateSearchToolbar = me.getComponent('DateSearchToolbar'),
			DateType = DateSearchToolbar.getComponent('DateType').getValue(),
			DateValue = DateSearchToolbar.getComponent('DateValue').getValue();
			
		if(DateType && DateValue){
			where.push("listestform." + DateType + ">='" + JShell.Date.toString(DateValue.start,true) + "'");
			where.push("listestform." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(DateValue.end),true) + "'");
		}
		
		//内容查询栏
		var ValueSearchToolbar = me.getComponent('ValueSearchToolbar'),
			FieldType = ValueSearchToolbar.getComponent('FieldType').getValue(),
			FieldValue = ValueSearchToolbar.getComponent('FieldValue').getValue(),
			isLike = ValueSearchToolbar.getComponent('isLike').getValue();
			
		if(FieldType && FieldValue){
			if(isLike){
				where.push("listestform." + FieldType + " like '%" + FieldValue + "%'");
			}else{
				where.push("listestform." + FieldType + "='" + FieldValue + "'");
			}
		}
		
		//状态过滤栏
		if(me.STATUS_LIST && me.STATUS_LIST.length > 0){
			var StatusToolbar = me.getComponent('StatusToolbar'),
				items = StatusToolbar.items.items,
				len = items.length,
				StatusWhere = [];
				
			for(var i=0;i<len;i++){
				if(items[i].checked){
					if(items[i].where){
						StatusWhere.push(items[i].where);
					}
				}
			}
			if(StatusWhere.length > 0){
				where.push('(' + StatusWhere.join(' or ') + ')');
			}
		}
		
		//内部条件
		me.internalWhere = where.join(" and ");
		
		//排序：检验日期+样本号
		if (!me.firstLoad) {
			me.store.proxy.extraParams = {};
			var first = me.store.sorters.first();
			if(first && (first.property == 'LisTestForm_GTestDate' || first.property == 'LisTestForm_GSampleNoForOrder')){
				//me.store.sorters.removeAll(me.store.sorters.items);
				if (first.property == 'LisTestForm_GTestDate') me.dateDirection = first.direction;
				me.store.sorters.addAll([
					{ property: 'LisTestForm_GTestDate', direction: me.dateDirection },
					{ property: 'LisTestForm_GSampleNoForOrder', direction: first.direction }
				]);

				//点击检验日期，顺序 检验日期顺序 + 样本号顺序
				//逆序 检验日期逆序 + 样本号逆序

				//点击样本号： 顺序 检验日期(原本的排序保持) + 样本号顺序
				//逆序 检验日期(原本的排序保持) + 样本号逆序

				//设置外部自定义参数排序  -- 替换默认排序
				me.store.proxy.extraParams = {//一个带有属性值的对象，用来给每一个使用当前对象发出的请求附加额外的参数。 Session信息以及你需要在每个请求中传递的其它信息一般都会放在这里
					sort: JSON.stringify([
						{ property: 'LisTestForm_GTestDate', direction: me.dateDirection },
						{ property: 'LisTestForm_GSampleNoForOrder', direction: first.direction }
					])
				};
			}
		}else{
			me.firstLoad = false;
		}
		me.callParent(arguments);
	}
});