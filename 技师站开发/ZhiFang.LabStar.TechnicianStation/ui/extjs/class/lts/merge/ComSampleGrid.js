/**
 * 确认合并样本列表
 * @author liangyl	
 * @version 2019-11-19
 */
Ext.define('Shell.class.lts.merge.ComSampleGrid', {
	extend: 'Shell.ux.grid.Panel',	
	title: '确认合并样本列表 ',
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: true,
	//过滤条件复选框属性列表
	STATUS_LIST: [
		{ text: '验中', iconText: '检', color: '#FFB800', where: 'listestform.MainStatusID=0', checked: true },
		{ text: '验确认', iconText: '检', color: '#5FB878', where: 'listestform.MainStatusID=2', checked: true },
		{ text: '核', iconText: '审', color: '#009688', where: 'listestform.MainStatusID=3', checked: true },
		{ text: '审', iconText: '反', color: '#1E9FFF', where: '(listestform.MainStatusID=0 and listestform.CheckTime<>null)' },
		{ text: '', iconText: '废', color: '#c2c2c2', where: 'listestform.MainStatusID=-2' },
		{ text: '检', iconText: '复', color: '#2F4056', where: 'listestform.RedoStatus=1' }
	],
	//状态样色
	ColorMap: {
		"危急值": { text: '危急值', iconText: '危', color: 'red' },
		"检验完成": { text: '√检验完成', iconText: '√', color: 'green' },
		"急诊": { text: '急诊', iconText: '急', color: 'red' },
		"特殊样本": { text: '特殊样本', iconText: '特', color: 'red' },
		"阳性保卡": { text: '阳性保卡', iconText: '阳', color: '#F08080' },
		"打印": { text: '急诊', iconText: '急', color: '#00BFFF' },

		"智能审核通过": { text: '√智能审核通过', iconText: '√', color: 'green' },
		"智能审核未通过": { text: '×智能审核未通过', iconText: '×', color: 'red' },
		"仪器通过": { text: '√仪器状态通过', iconText: '仪', color: 'green' },
		"仪器警告": { text: '仪器状态警告', iconText: '仪', color: 'orange' },
		"仪器异常": { text: '×仪器状态异常', iconText: '仪', color: 'red' }
	},

	//主状态样式模板
	MainStatusTemplet: '<span style="margin-right:1px;border:1px solid {color};color:#fff;background-color:{color};"><a>{text}</a></span>',
	//颜色条模板
	ColorLineTemplet: '<span style="padding:0 1px;margin-right:1px;border:1px solid {color};background:{color};"></span>',
	//悬浮内容样式
	TipsTemplet: '<span style=\'margin:1px 1px 0 0;border:1px solid {color};color:{color};\'>{text}</span>',

	defaultOrderBy:[{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
        var items = me.headerCt.items.items;
        for(var i=0;i<items.length;i++){
        	var dataIndex = items[i].initialConfig.dataIndex ;
			if (dataIndex == 'LisTestForm_GTestDate' || dataIndex == 'LisTestForm_GSampleNo'
				|| dataIndex == 'LisTestForm_CName' || dataIndex == 'LisTestForm_PatNo' || dataIndex == 'LisTestForm_MainStatusID' 
        	|| dataIndex == 'LisTestForm_BarCode' || dataIndex == 'IsExist'){
        		items[i].el.setStyle("background", '#F08080');
        	}
        	if(dataIndex == 'LisTestForm_DGTestDate' || dataIndex == 'LisTestForm_DGSampleNo'
				|| dataIndex == 'LisTestForm_DCName' || dataIndex == 'LisTestForm_DPatNo'
				|| dataIndex == 'LisTestForm_DBarCode' || dataIndex == 'LisTestForm_DMainStatusID') {
        		items[i].el.setStyle("color", 'white');
        		items[i].el.setStyle("background", '#228B22');
        	}
        }
	},
	initComponent: function() {
		var me = this;
		me.addEvents('cleardata');
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'源检验单ID',dataIndex:'LisTestForm_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'就诊信息ID',dataIndex:'LisTestForm_LisPatient_Id',hidden:true,hideable:false
		},{
			text:'打印次数',dataIndex:'LisTestForm_PrintCount',width:80,hidden:true,hideable:false
		},{
			text:'审核时间',dataIndex:'LisTestForm_CheckTime',width:80,hidden:true,hideable:false
		},{
			text:'复检',dataIndex:'LisTestForm_RedoStatus',width:80,hidden:true,hideable:false
		},{
			text:'仪器状态',dataIndex:'LisTestForm_ESendStatus',width:80,hidden:true,hideable:false
		},{
			text:'报告状态',dataIndex:'LisTestForm_ReportStatusID',width:80,hidden:true,hideable:false
		},{
			text:'检验完成',dataIndex:'LisTestForm_TestAllStatus',width:80,hidden:true,hideable:false
		},{
			text:'小组',dataIndex:'LisTestForm_SectionID',width:85,
			hidden:true,hideable:false
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本号',dataIndex:'LisTestForm_GSampleNo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'病历号',dataIndex:'LisTestForm_PatNo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:90,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本单主状态',dataIndex:'LisTestForm_MainStatusID',width:85,hidden:false,
			sortable:false,menuDisabled:true,renderer:me.onStatusRenderer
		},{
			text:'→',dataIndex:'Direction',width:40,align:'center',
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'小组',dataIndex:'LisTestForm_DSectionID',width:85,
			hidden:true,hideable:false
		},{
			text:'检验日期',dataIndex:'LisTestForm_DGTestDate',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本号',dataIndex:'LisTestForm_DGSampleNo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'姓名',dataIndex:'LisTestForm_DCName',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'病历号',dataIndex:'LisTestForm_DPatNo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'条码号',dataIndex:'LisTestForm_DBarCode',width:90,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'存在',dataIndex:'IsExist',width:50,
			sortable:false,menuDisabled:true,
			isBool: true,align: 'center',type: 'bool',defaultRenderer:true
		},{
			text:'目标检验单ID',dataIndex:'LisTestForm_DId',hidden:true,hideable:false
		},{
			text:'就诊信息ID',dataIndex:'LisTestForm_LisPatient_DId',hidden:true,hideable:false
		},{
			text:'打印次数',dataIndex:'LisTestForm_DPrintCount',width:80,hidden:true,hideable:false
		},{
			text:'审核时间',dataIndex:'LisTestForm_DCheckTime',width:80,hidden:true,hideable:false
		},{
			text:'复检',dataIndex:'LisTestForm_DRedoStatus',width:80,hidden:true,hideable:false
		},{
			text:'仪器状态',dataIndex:'LisTestForm_DESendStatus',width:80,hidden:true,hideable:false
		},{
			text:'报告状态',dataIndex:'LisTestForm_DReportStatusID',width:80,hidden:true,hideable:false
		},{
			text:'检验完成',dataIndex:'LisTestForm_DTestAllStatus',width:80,hidden:true,hideable:false
		},{
			text:'目标样本单主状态',dataIndex:'LisTestForm_DMainStatusID',width:100,hidden:false,
				sortable: false, menuDisabled: true, renderer: function (value, meta, record) {
					return me.onStatusRenderer(value, meta, record, true);
				}
		}];
		
		return columns;
	},
	
	onSearch : function(obj){
		var me =  this;
		me.hideMask();
		me.store.removeAll();
		me.fireEvent('cleardata', me);
		//源样本单
		var SampleList = [];
		var LBSection_ID = obj.LBSection_ID;
		//目标样本单
		var DSampleList=[];		
	    var swhere = "";
		//源样本单
		me.getSampleInfo(obj.LBSection_ID,obj.GTestDate,obj.GSampleNo,swhere,function(data){
			if(data && data.value) SampleList = data.value.list;
		});
		//如果目标样本存在，状态MainStatusID=0检验中，其它状态不要合并，已经确认或者审定的检验单，不能进行合并
		var dwhere = " and listestform.MainStatusID>=0";
		//目标样本单
	    me.getSampleInfo(obj.DLBSection_ID,obj.DGTestDate,obj.DGSampleNo,dwhere,function(data){
			if(data && data.value)DSampleList = data.value.list;
		});
		//源样本不存在，没有必要合并
		if(SampleList.length==0)return;
		if(DSampleList.length==0){
			var GSampleNo=obj.DGSampleNo.replace(/，/g,",");
			DGSampleNo = GSampleNo.split(',');
			for(var i = 0;i<DGSampleNo.length;i++){
				DSampleList.push({
					LisTestForm_DSectionID: obj.DLBSection_ID,
					LisTestForm_GTestDate:obj.DGTestDate,
					LisTestForm_GSampleNo:DGSampleNo[i],
					LisTestForm_CName:'',
					LisTestForm_PatNo: '',
					LisTestForm_LisPatient_DId: '',
					LisTestForm_BarCode:'',
					LisTestForm_Id:'',
					LisTestForm_MainStatusID:'',
					IsExist: '0',
					LisTestForm_PrintCount: '',
					LisTestForm_CheckTime: '',
					LisTestForm_RedoStatus: '',
					LisTestForm_ESendStatus: '',
					LisTestForm_ReportStatusID: '',
					LisTestForm_TestAllStatus: ''
				});
			}
		}
    	for(var i=0;i<DSampleList.length;i++){
			var obj = {
				LisTestForm_DSectionID: obj.DLBSection_ID,
    			LisTestForm_DGTestDate:JcallShell.Date.toString(DSampleList[i].LisTestForm_GTestDate,true),
				LisTestForm_DGSampleNo: DSampleList[i].LisTestForm_GSampleNo,
				LisTestForm_LisPatient_DId: DSampleList[i].LisTestForm_LisPatient_Id ? DSampleList[i].LisTestForm_LisPatient_Id : '',
				LisTestForm_DCName:DSampleList[i].LisTestForm_CName,
				LisTestForm_DPatNo:DSampleList[i].LisTestForm_PatNo,
				LisTestForm_DBarCode:DSampleList[i].LisTestForm_BarCode,
				LisTestForm_DId:DSampleList[i].LisTestForm_Id,
				IsExist:DSampleList[i].IsExist ? DSampleList[i].IsExist : '1',
				LisTestForm_DMainStatusID: DSampleList[i].LisTestForm_MainStatusID,
				LisTestForm_DPrintCount: DSampleList[i].LisTestForm_PrintCount,
				LisTestForm_DCheckTime: DSampleList[i].LisTestForm_CheckTime,
				LisTestForm_DRedoStatus: DSampleList[i].LisTestForm_RedoStatus,
				LisTestForm_DESendStatus: DSampleList[i].LisTestForm_ESendStatus,
				LisTestForm_DReportStatusID: DSampleList[i].LisTestForm_ReportStatusID,
				LisTestForm_DTestAllStatus: DSampleList[i].LisTestForm_TestAllStatus
    		}
    		for(var j=0; j<SampleList.length;j++){
				SampleList[j].LisTestForm_SectionID = LBSection_ID;
				SampleList[j].LisTestForm_GTestDate = JcallShell.Date.toString(SampleList[j].LisTestForm_GTestDate, true);
    			SampleList[j].Direction="→";
    			Object.assign(obj,SampleList[j]);
    			SampleList.splice(j,1);
    			break;
    		}
    		//存在源样本才加数据
    		if(obj.LisTestForm_Id)me.store.add(obj);
    	}
    	//默认选择第一行
    	if(me.getStore().getCount()>0)me.getSelectionModel().select(0);
	},
	/**根据检验小组，检验日期，样本号得到样本单*/
	getSampleInfo: function(SectionID,GTestDate,GSampleNo,where,callback) {
		var me = this,
			url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
//      GSampleNo=GSampleNo.replace(/，/g,",");

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=LisTestForm_Id,LisTestForm_GTestDate,LisTestForm_GSampleNo,LisTestForm_CName,LisTestForm_PatNo,LisTestForm_BarCode,LisTestForm_MainStatusID' +
			',LisTestForm_PrintCount,LisTestForm_CheckTime,LisTestForm_RedoStatus,LisTestForm_ESendStatus,LisTestForm_ReportStatusID,LisTestForm_TestAllStatus,LisTestForm_LisPatient_Id';
		url += "&where=listestform.LBSection.Id=" + SectionID + " and listestform.GTestDate='" + GTestDate + "' and listestform.GSampleNo='" + GSampleNo + "'" + where;//listestform.MainStatusID=0 and 
		url +="&sort="+Ext.encode(me.defaultOrderBy);
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} 
		},false);
	},
	//状态列显示处理
	onStatusRenderer: function (value, meta, record,isD) {
		var me = this,
			isD = isD || false,
			html = [];

		//主状态->检0、初2、终3、反(0 and CheckTime<>null)、废-2
		var MainStatusID = isD ? record.get('LisTestForm_DMainStatusID') : record.get('LisTestForm_MainStatusID');

		if (MainStatusID == '0') {
			html.push(me.MainStatusTemplet.replace(/{color}/g, me.STATUS_LIST[0].color).replace(/{text}/g, me.STATUS_LIST[0].iconText));
		} else if (MainStatusID == '2') {
			html.push(me.MainStatusTemplet.replace(/{color}/g, me.STATUS_LIST[1].color).replace(/{text}/g, me.STATUS_LIST[1].iconText));
		} else if (MainStatusID == '3') {
			html.push(me.MainStatusTemplet.replace(/{color}/g, me.STATUS_LIST[2].color).replace(/{text}/g, me.STATUS_LIST[2].iconText));
		} else if (MainStatusID == '-2') {
			html.push(me.MainStatusTemplet.replace(/{color}/g, me.STATUS_LIST[4].color).replace(/{text}/g, me.STATUS_LIST[4].iconText));
		}

		//危急值->ReportStatusID,倒数第2位=1
		var ReportStatusID = isD ? record.get('LisTestForm_DReportStatusID') + '' : record.get('LisTestForm_ReportStatusID') + '';
		if (ReportStatusID && ReportStatusID.length >= 2 && ReportStatusID.charAt(ReportStatusID.length - 2) == '1') {
			html.push(me.MainStatusTemplet.replace(/{color}/g, me.ColorMap.危急值.color).replace(/{text}/g, me.ColorMap.危急值.iconText));
		}

		//检验完成->TestAllStatus,0:未开始;1:已开始;2:检验完成:绿色;
		var TestAllStatus = isD ? record.get('LisTestForm_DTestAllStatus') : record.get('LisTestForm_TestAllStatus');
		if (TestAllStatus == '2') {
			html.push(me.MainStatusTemplet.replace(/{color}/g, me.ColorMap.检验完成.color).replace(/{text}/g, me.ColorMap.检验完成.iconText));
		}

		//复检->RedoStatus,0/1
		var RedoStatus = isD ? record.get('LisTestForm_DRedoStatus') : record.get('LisTestForm_RedoStatus');
		if (RedoStatus == '1') {
			html.push(me.ColorLineTemplet.replace(/{color}/g, me.STATUS_LIST[5].color));
		}

		//仪器状态->ESendStatus,0:无;1:通过 绿色;2:警告 黄色;3:异常 红色;
		var ESendStatus = isD ? record.get('LisTestForm_DESendStatus') + '' : record.get('LisTestForm_ESendStatus') + '';
		if (ESendStatus == '1') {
			html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.仪器通过.color));
		} else if (ESendStatus == '2') {
			html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.仪器警告.color));
		} else if (ESendStatus == '3') {
			html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.仪器异常.color));
		}

		//阳性保卡->ReportStatusID,倒数第1位=1
		var ReportStatusID = isD ? record.get('LisTestForm_DReportStatusID') + '' : record.get('LisTestForm_ReportStatusID') + '';
		if (ReportStatusID && ReportStatusID.length >= 1 && ReportStatusID.charAt(ReportStatusID.length - 1) == '1') {
			html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.阳性保卡.color));
		}

		//打印->PrintCount>0
		var PrintCount = isD ? record.get('LisTestForm_DPrintCount') : record.get('LisTestForm_PrintCount');
		if (PrintCount && PrintCount > 0) {
			html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.打印.color));
		}

		//meta.tdAttr = 'data-qtip="' + me.onShowTips(record) + '"';

		return html.join('');
	}
});