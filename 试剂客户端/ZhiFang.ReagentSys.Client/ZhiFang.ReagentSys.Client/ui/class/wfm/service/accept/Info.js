/**
 * 服务信息
 * @author Jcall	
 * @version 2016-11-03
 */
Ext.define('Shell.class.wfm.service.accept.Info', {
	extend: 'Shell.ux.form.Panel',
	title: '服务信息',
	width: 810,
	height: 600,
	autoScroll: true,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPCustomerServiceById',
	/**信息数据*/
	InfoData: '',
	PK: null,
	Status:{},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PCustomerServiceStatus', function() {
			if(!JShell.System.ClassDict.PCustomerServiceStatus) {
				JShell.Msg.error('未获取到服务受理状态，请刷新列表');
				return;
			}
		});
		//初始化Html页面
		me.initHtml();
	},
	initComponent: function() {
		var me = this;
		me.html =
			'<div class="loading-div">' +
			'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
			'<div style="padding-top:10px;">页面加载中</div>' +
			'</div>';
		me.callParent(arguments);
	},

	initHtml: function() {
		var me = this;
		me.loadInfoData(function(data) {
			me.InfoData = data; //任务数据
			me.initHtmlContent(); //初始化HTML页面
		});
	},
	/**加载信息*/
	loadInfoData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);
		var fields = [
			'Id', 'IsProxy', 'ClientID', 'ClientName','ServiceRegisterDate',
			'ProvinceID', 'ProvinceName', 'ServiceRegisterMan','RequestMan',
		    'ServiceAcceptanceManID', 'ServiceAcceptanceMan', 'ServiceAcceptanceDate',
			'ProblemMemo', 'Status', 'ServiceModeID', 'ServiceModeName','ServiceOperationDate',
			'ServiceOperationCompleteManID', 'ServiceOperationCompleteMan','RequestManPhone',
			'ServiceFinishDate', 'ServiceOperationCompleteMemo', 'ServiceReturnManID', 'ServiceReturnMan',
			'ServiceReturnDate', 'SatisfactionID', 'SatisfactionName',
			'ServiceReturnToMan', 'ServiceReturnToManPhone', 'CompletionStatusName',
			'CompletionStatusID', 'ServiceReturnMemo'
		];
		fields = "PCustomerService_" + fields.join(",PCustomerService_");
		url += "?isPlanish=true&fields=" + fields + "&id=" + me.PK;
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**初始化HTML页面*/
	initHtmlContent: function() {
		var me = this,
			html = [];

		//获取任务HTML
		html.push(me.getInfoHtml());
		//更新HTML内容
		me.update(html.join(''));
	},
	/**获取信息HTML*/
	getInfoHtml: function() {
		var me = this,
			data = me.InfoData,
			html = '';
		if(data.success) {
			html = me.getInfoTemplet();
			if(data.value) {
				html = html.replace(/{ClientName}/g, data.value.PCustomerService_ClientName || '');
				html = html.replace(/{ProvinceName}/g, data.value.PCustomerService_ProvinceName || '');
				html = html.replace(/{IsProxy}/g, data.value.PCustomerService_IsProxy == true ? '是' : '否');
				html = html.replace(/{RequestMan}/g, data.value.PCustomerService_RequestMan || '');
				html = html.replace(/{RequestManPhone}/g, data.value.PCustomerService_RequestManPhone || '');
				html = html.replace(/{ServiceAcceptanceMan}/g, data.value.PCustomerService_ServiceAcceptanceMan || '');
				html = html.replace(/{ServiceAcceptanceDate}/g,JShell.Date.toString(data.value.PCustomerService_ServiceAcceptanceDate, true) || '');// data.value.PCustomerService_ServiceAcceptanceDate || '');
				html = html.replace(/{ServiceRegisterMan}/g, data.value.PCustomerService_ServiceRegisterMan || '');
				html = html.replace(/{ServiceRegisterDate}/g, JShell.Date.toString(data.value.PCustomerService_ServiceRegisterDate) || '');
				html = html.replace(/{ProblemMemo}/g, data.value.PCustomerService_ProblemMemo || '');
				var StatusBgcolor='',Status='';
				if(data.value.PCustomerService_Status){
					var obj = JShell.System.ClassDict.getClassInfoById('PCustomerServiceStatus', data.value.PCustomerService_Status);
					if(obj) {
					    StatusBgcolor= obj.BGColor;
					    Status= obj.Name;
					}
				}
                html = html.replace(/{StatusBgcolor}/g,StatusBgcolor || '');
				html = html.replace(/{Status}/g,Status || '');
				html = html.replace(/{ServiceModeName}/g, data.value.PCustomerService_ServiceModeName || '');
				html = html.replace(/{ServiceAcceptanceMan}/g, data.value.PCustomerService_ServiceAcceptanceMan || '');
				html = html.replace(/{ServiceOperationDate}/g, JShell.Date.toString(data.value.PCustomerService_ServiceOperationDate, true) || '');//data.value.PCustomerService_ServiceOperationDate || '');
				html = html.replace(/{ServiceOperationCompleteMemo}/g, data.value.PCustomerService_ServiceOperationCompleteMemo || '');
				html = html.replace(/{ServiceFinishDate}/g, JShell.Date.toString(data.value.PCustomerService_ServiceFinishDate, true) || '');//data.value.PCustomerService_ServiceFinishDate || '');
                html = html.replace(/{ServiceReturnMan}/g, data.value.PCustomerService_ServiceReturnMan || '');
				html = html.replace(/{ServiceReturnDate}/g, JShell.Date.toString(data.value.PCustomerService_ServiceReturnDate, true) || '');//data.value.PCustomerService_ServiceReturnDate || '');
				html = html.replace(/{ServiceReturnToMan}/g, data.value.PCustomerService_ServiceReturnToMan || '');
				html = html.replace(/{ServiceReturnToManPhone}/g, data.value.PCustomerService_ServiceReturnToManPhone || '');
				html = html.replace(/{SatisfactionName}/g, data.value.PCustomerService_SatisfactionName || '');
				html = html.replace(/{CompletionStatusName}/g, data.value.PCustomerService_CompletionStatusName || '');
				html = html.replace(/{ServiceReturnMemo}/g, data.value.PCustomerService_ServiceReturnMemo || '');
			}
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g, '服务受理');
			html = html.replace(/{Error}/g, data.msg);
		}
		return html;
	},
	/**获取信息HTML模板*/
	getInfoTemplet: function() {

		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';
		//服务受理
		var AcceptanceTemplet = this.getAcceptanceTemplet(rDivStyle, sDivStyle);
		//服务处理
		var OperationCompleteTemplet = this.getOperationCompleteTemplet(rDivStyle, sDivStyle);
		//分析内容
		//		var ContentanalysisTemplet=this.getContentanalysisTemplet(rDivStyle,sDivStyle);
		//服务回访
		var ServiceReturnTemplet = this.getServiceReturnTemplet(rDivStyle, sDivStyle);
        
		var templet = AcceptanceTemplet + OperationCompleteTemplet +ServiceReturnTemplet;
		return templet;
	},
	/**获取服务受理HTML模板*/
	getAcceptanceTemplet: function(rDivStyle, sDivStyle) {
		var me = this;
		var templet = '<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">服务受理</h4>' +
			'</div>' +
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>用户名称：{ClientName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>省份：{ProvinceName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>代理请求：{IsProxy}</b></div>' +
			'<div style="' + sDivStyle + '"><b>请求人：{RequestMan}</b></div>' +
			'<div style="' + sDivStyle + '"><b>联系电话：{RequestManPhone}</b></div>' +
			'<div style="' + sDivStyle + '"><b>受理人：{ServiceAcceptanceMan}</b></div>' +
			'<div style="' + sDivStyle + '"><b>受理时间：{ServiceAcceptanceDate}</b></div>' +
			'<div style="' + sDivStyle + '"><b>登记人：{ServiceRegisterMan}</b></div>' +
			'<div style="' + sDivStyle + '"><b>登记时间：{ServiceRegisterDate}</b></div>' +
			'</div>' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>问题描述：</b></div>' +
			'<div style="' + sDivStyle + '">{ProblemMemo}</div>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**获取服务处理HTML模板*/
	getOperationCompleteTemplet: function(rDivStyle, sDivStyle) {
		var me = this;
		var templet = '<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">服务处理</h4>' +
			'</div>' +
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>状态：<span style=color:{StatusBgcolor}>{Status}</span></b></div>' +
			'<div style="' + sDivStyle + '"><b>服务方式：{ServiceModeName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>完成时间：{ServiceFinishDate}</b></div>' +
//			'<div style="' + sDivStyle + '"><b>是否需要回访：{PCustomerService_ProvinceName07}</b></div>' +
//			'<div style="' + sDivStyle + '"><b>是否已回访：{PCustomerService_ProvinceName08}</b></div>' +
			'</div>' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>处理人：{ServiceAcceptanceMan}</b></div>' +
			'<div style="' + sDivStyle + '"><b>处理时间：{ServiceOperationDate}</b></div>' +
			'</div>' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>处理描述：</b></div>' +
			'<div style="' + sDivStyle + '">{ServiceOperationCompleteMemo}</div>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**获取分析内容HTML模板*/
	getContentanalysisTemplet: function(rDivStyle, sDivStyle) {
		var me = this;
		var templet = '<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">分析内容</h4>' +
			'</div>' +
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>现象分类：{PCustomerService_IsProxy01}</b></div>' +
			'<div style="' + sDivStyle + '"><b>原因分类：{PCustomerService_IsProxy02}</b></div>' +
			'<div style="' + sDivStyle + '"><b>措施分类：{PCustomerService_IsProxy03}</b></div>' +
			'<div style="' + sDivStyle + '"><b>远程处理时间：{PCustomerService_IsProxy05}</b></div>' +
			'<div style="' + sDivStyle + '"><b>上门服务时间：{PCustomerService_IsProxy06}</b></div>' +
			'<div style="' + sDivStyle + '"><b>服务成本：{PCustomerService_IsProxy07}</b></div>' +
			'</div>' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>相关功能：</b></div>' +
			'<div style="' + sDivStyle + '">{PCustomerService_IsProxy04}</div>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**获取服务回访HTML模板*/
	getServiceReturnTemplet: function(rDivStyle, sDivStyle) {
		var me = this;
		var templet = //服务回访
			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">服务回访</h4>' +
			'</div>' +
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>回访人：{ServiceReturnMan}</b></div>' +
			'<div style="' + sDivStyle + '"><b>回访时间：{ServiceReturnDate}</b></div>' +
			'<div style="' + sDivStyle + '"><b>被回访人：{ServiceReturnToMan}</b></div>' +
			'<div style="' + sDivStyle + '"><b>联系电话：{ServiceReturnToManPhone}</b></div>' +
			'<div style="' + sDivStyle + '"><b>用户满意度：{SatisfactionName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>完成情况：{CompletionStatusName}</b></div>' +
			'</div>' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>回访结果：</b></div>' +
			'<div style="' + sDivStyle + '">{ServiceReturnMemo}</div>' +
			'</div>' +
			'</div>';
		return templet;
	}
});