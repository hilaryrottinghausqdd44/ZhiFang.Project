/**
 * 单站点授权内容
 * @author longfc
 * @version 2016-12-10
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.show.ContentPanel', {
	extend: 'Ext.panel.Panel',
	
	title: '单站点授权信息',
	width: 720,
	height: 600,
	autoScroll: true,
	
	/**获取数据服务路径*/
	selectInfoUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHSingleLicenceById',
	
	/**加载数据提示*/
	lodingText: JShell.Server.LOADING_TEXT,
	/**信息数据*/
	InfoData: '',
	/**信息ID*/
	PK: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.PK) {
			//初始化Html页面
			me.initHtml();
		}
	},
	initComponent: function() {
		var me = this;
		if(me.PK) {
			me.html =
				'<div class="loading-div">' +
				'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
				'<div style="padding-top:10px;">页面加载中</div>' +
				'</div>';
		}
		me.callParent(arguments);
	},

	initHtml: function() {
		var me = this;
		//显示遮罩
		me.showMask(me.lodingText);
		me.loadInfoData(function(data) {
			me.InfoData = data; //信息数据
			me.initHtmlContent(); //初始化HTML页面
		});
	},
	/**初始化HTML页面*/
	initHtmlContent: function() {
		var me = this,
			html = [];
		//获取任务HTML
		html.push(me.getInfoHtml());
		//更新HTML内容
		me.update(html.join(''));
		//隐藏遮罩
		me.hideMask();
	},
	/**加载信息*/
	loadInfoData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectInfoUrl);
		var fields = [
			'Id', 'PClientName', 'ProgramName', 'EquipName', 'LicenceTypeId',
			'SQH', 'LicenceKey', 'MacAddress', 'StartDate', 'EndDate', 'OneAuditInfo',
			'Status', 'ApplyName', 'ApplyDataTime', 'OneAuditName', 'TwoAuditInfo',
			'OneAuditDataTime', 'TwoAuditName', 'TwoAuditDataTime', 'GenDateTime', 'StatusName', 'TwoStatusName', 'Comment','PlannReceiptDate'
		];
		fields = fields.join(",");
		url += "?fields=" + fields + "&id=" + me.PK;
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**获取信息HTML*/
	getInfoHtml: function() {
		var me = this,
			data = me.InfoData,
			html = '';
		if(data.success) {
			if(data.value) {
				var isShowProgramName = false;
				if(data.value.ProgramName != "") {
					isShowProgramName = true;
				} else {
					isShowProgramName = false;
				}
				html = me.getInfoTemplet(isShowProgramName);
				html = html.replace(/{PClientName}/g, data.value.PClientName || '');
				html = html.replace(/{ProgramName}/g, data.value.ProgramName || '');
				html = html.replace(/{EquipName}/g, data.value.EquipName || '');
				html = html.replace(/{StatusName}/g, data.value.StatusName || '');
				var LicenceTypeId = '';
				var info = JShell.System.ClassDict.getClassInfoById('LicenceType', data.value.LicenceTypeId);
				if(info) {
					LicenceTypeId = info.Name;
				}
				html = html.replace(/{LicenceTypeId}/g, LicenceTypeId || '');

				html = html.replace(/{SQH}/g, data.value.SQH || '');
				html = html.replace(/{LicenceKey}/g, data.value.LicenceKey || '');
				html = html.replace(/{MacAddress}/g, data.value.MacAddress || '');

				html = html.replace(/{StartDate}/g, JShell.Date.toString(data.value.StartDate, true) || '');
				html = html.replace(/{EndDate}/g, JShell.Date.toString(data.value.EndDate, true) || '');
				html = html.replace(/{PlannReceiptDate}/g, JShell.Date.toString(data.value.PlannReceiptDate, true) || '');
				html = html.replace(/{Status}/g, data.value.Status || '');
				html = html.replace(/{ApplyName}/g, data.value.ApplyName || '');
				html = html.replace(/{ApplyDataTime}/g, JShell.Date.toString(data.value.ApplyDataTime) || '');
				var Comment = data.value.Comment;
				var reg = new RegExp("\r\n", "g");
				if(Comment != null && Comment != undefined)
					Comment = Comment.replace(reg, "<br />");
				html = html.replace(/{Comment}/g, Comment || '');
				html = html.replace(/{OneAuditName}/g, data.value.OneAuditName || '');
				html = html.replace(/{OneAuditDataTime}/g, JShell.Date.toString(data.value.OneAuditDataTime) || '');
				html = html.replace(/{GenDateTime}/g, JShell.Date.toString(data.value.GenDateTime) || '');

				html = html.replace(/{TwoAuditName}/g, data.value.TwoAuditName || '');
				html = html.replace(/{TwoAuditDataTime}/g, JShell.Date.toString(data.value.TwoAuditDataTime) || '');
				
				var OneAuditInfo = data.value.OneAuditInfo;
				if(OneAuditInfo != null && OneAuditInfo != undefined)
					OneAuditInfo = OneAuditInfo.replace(reg, "<br />");
				html = html.replace(/{OneAuditInfo}/g,OneAuditInfo || '');
				
				var TwoAuditInfo = data.value.TwoAuditInfo;
				if(TwoAuditInfo != null && OneAuditInfo != undefined)
					TwoAuditInfo = TwoAuditInfo.replace(reg, "<br />");
				html = html.replace(/{TwoAuditInfo}/g,TwoAuditInfo|| '');
				html = html.replace(/{TwoStatusName}/g, data.value.TwoStatusName || '');

			}
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{PClientName}/g, '授权内容');
			html = html.replace(/{Error}/g, data.msg);
		}
		return html;
	},
	/**获取信息HTML模板*/
	getInfoTemplet: function(isShowProgramName) {

		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';

		var templet =
			'<style type="text/css">' +
			'.dl-horizontal dt{width:80px}' +
			'.dl-horizontal dd{margin-left:10px}' +
			'</style>' +
			//合同标题
			'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{PClientName}</h4>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<dl class="dl-horizontal">' +

			'<div class="col-sm-12">' +
			'<dt>授权程序：</dt><dd>{ProgramName}</dd>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<dt>授权仪器：</dt><dd>{EquipName}</dd>' +
			'</div>' +

			'<div class="col-sm-6">' +
			'<dt>申 请 人  ：</dt><dd>{ApplyName}</dd>' +
			'</div>' +
			'<div class="col-sm-6">' +
			'<dt>申请日期：</dt><dd>{ApplyDataTime}</dd>' +
			'</div>' +

			'<div class="col-sm-6">' +
			'<dt>授权类型：</dt><dd>{LicenceTypeId}</dd>' +
			'</div>' +
			'<div class="col-sm-6">' +
			'<dt>授 权 号 ：</dt><dd>{SQH}</dd>' +
			'</div>' +

			'<div class="col-sm-6">' +
			'<dt>开始日期：</dt><dd>{StartDate}</dd>' +
			'</div>' +
			'<div class="col-sm-6">' +
			'<dt>结束日期：</dt><dd>{EndDate}</dd>' +
			'</div>' +

			'<div class="col-sm-6">' +
			'<dt>Mac地址：</dt><dd>{MacAddress}</dd>' +
			'</div>' +
			'<div class="col-sm-6">' +
			'<dt>流程状态：</dt><dd style="color:#e98f36;">{TwoStatusName}</dd>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<dt>授权Key：</dt><dd style="color:#1195db;">{LicenceKey}</dd>' +
			'</div>' +
			
			'<div class="col-sm-12">' +
			'<dt style="width:95px">计划收款时间：</dt><dd>{PlannReceiptDate}</dd>' +
			'</div>' +
			
			'<div class="col-sm-12">' +
			'<dt>未收款原因：</dt><dd>{Comment}</dd>' +
			'</div>' +

			'</div>' +
			'</div>' +
			'</div>' +

			//一审人、一审时间、一审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<dl class="dl-horizontal">' +

			'<div class="col-sm-3">' +
			'<dt>商务助理：</dt><dd>{OneAuditName}</dd>' +
			'</div>' +
			'<div class="col-sm-4">' +
			'<dt>审核时间：</dt><dd>{OneAuditDataTime}</dd>' +
			'</div>' +
			'<div class="col-sm-5">' +
			'<dt>审核意见：</dt><dd>{OneAuditInfo}</dd>' +
			'</div>' +

			'</div>' +
			'</div>' +
			'</div>' +

			//特殊审批、一审时间、一审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<dl class="dl-horizontal">' +

			'<div class="col-sm-3">' +
			'<dt>总 经 理  ：</dt><dd>{TwoAuditName}</dd>' +
			'</div>' +
			'<div class="col-sm-4">' +
			'<dt>审批时间：</dt><dd>{TwoAuditDataTime}</dd>' +
			'</div>' +
			'<div class="col-sm-5">' +
			'<dt>审批意见：</dt><dd>{TwoAuditInfo}</dd>' +
			'</div>' +

			'</div>' +
			'</div>' +
			'</div>';
		return templet;
	},

	/**获取错误信息HTML模板*/
	getErrorTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<h4>{Title}-错误信息</h4>' +
			'<p style="color:red;padding:5px;word-break:break-all;word-wrap:break-word;">{Error}</p>' +
			'</div>';
		return templet;
	},

	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**@public 加载数据*/
	load: function(id) {
		var me = this;
		me.PK = id;
		me.initHtml();
	},
	/**清空数据*/
	clearData: function() {
		var me = this;
		me.PK = null;
		me.update('');
	}
});