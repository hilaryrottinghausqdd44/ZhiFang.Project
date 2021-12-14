/**
 * 状态列显示
 * @author liangyl
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.Grid',{
    extend:'Shell.ux.grid.Panel',
    title:'状态列显示',
    //根据指定样本号批量生成新样本号
	batchCreateSampleNoUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_BatchCreateSampleNoByCurSampleNo',
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
   //打印图标样式
    PrintTemplet:'<img  src="'+JShell.System.Path.ROOT+'/ui/extjs/css/images/buttons/print1.png"/>',

    //状态列显示处理
	onStatusRenderer: function (value, meta, record,isD) {
		var me = this,
			isD = isD || false,
			html = [];

		//主状态->检0、初2、终3、反(0 and CheckTime<>null)、废-2
		var MainStatusID = isD ? record.get('LisTestForm_MainStatusID') : record.get('LisTestForm_MainStatusID');

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
		if (TestAllStatus == '1') {
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
//		if (PrintCount && PrintCount > 0) {
//			html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.打印.color));
//		}
		if(PrintCount && PrintCount > 0){
			html.push(me.PrintTemplet);
		}

		//meta.tdAttr = 'data-qtip="' + me.onShowTips(record) + '"';

		return html.join('');
	},
	//根据指定样本号批量生成新样本号
	batchCreateSampleNo:function(curSampleNo,SampleNoCount,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.batchCreateSampleNoUrl;
			
		var entity ={
			curSampleNo:curSampleNo,
			SampleNoCount:SampleNoCount
		};
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
			var v = Ext.JSON.decode(data.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
			callback(v.ResultDataValue);
		},true,null,true);
	},
	//查询截止样本号
	endSampleNo : function(GSampleNo,Num,com){
		var me = this;
	    me.batchCreateSampleNo(GSampleNo,Num,function(data){
	    	var arr = [];
	    	if(data){
	    		arr = data.split(',');
	    		com.setValue(arr.length>0 ? arr[arr.length-1] : arr[0]);
            }
	    });
	}
});