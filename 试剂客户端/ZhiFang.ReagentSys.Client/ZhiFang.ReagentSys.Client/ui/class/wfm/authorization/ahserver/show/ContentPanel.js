/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.show.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '服务器授权信息',
	width: 720,
	height: 600,

	autoScroll: true,
	/**获取数据服务路径*/
	selectInfoUrl: '/SingleTableService.svc/ST_UDTO_SearchAHServerLicenceById',
	/**文件下载服务路径*/
	downloadUrl: "/SingleTableService.svc/ST_UDTO_DownLoadAHServerLicenceFile",
	/**加载数据提示*/
	lodingText: JShell.Server.LOADING_TEXT,
	/**信息数据*/
	InfoData: '',
	/**信息ID*/
	PK: null,
	/**是否显示审核及审批信息*/
	IsShowAudit: false,
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
		me.initDownloadListeners();
		//隐藏遮罩
		me.hideMask();

	},
	/**加载信息*/
	loadInfoData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectInfoUrl);
		var fields = [
			'Id', 'PClientName', 'LicenceTypeId',
			'LRNo', 'LRNo1', 'LRNo2', 'OneAuditInfo', , 'Comment', 'Status', 'ApplyName', 'ApplyDataTime', 'OneAuditName', 'TwoAuditInfo', 'OneAuditDataTime', 'TwoAuditName', 'TwoAuditDataTime', 'GenDateTime', 'StatusName', 'TwoStatusName'
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

				html = me.getInfoTemplet(data.value);
				html = html.replace(/{PClientName}/g, data.value.PClientName || '');

				html = html.replace(/{StatusName}/g, data.value.StatusName || '');
				var LicenceTypeId = '';
				var info = JShell.System.ClassDict.getClassInfoById('LicenceType', data.value.LicenceTypeId);
				if(info) {
					LicenceTypeId = info.Name;
				}
				html = html.replace(/{LicenceTypeId}/g, LicenceTypeId || '');

				html = html.replace(/{LRNo}/g, data.value.SQH || '');	
				html = html.replace(/{LRNo1}/g, data.value.LRNo1 || '');
				html = html.replace(/{LRNo2}/g, data.value.LRNo2 || '');
				html = html.replace(/{Comment}/g, data.value.Comment || '');
				html = html.replace(/{Status}/g, data.value.Status || '');
				html = html.replace(/{ApplyName}/g, data.value.ApplyName || '');
				var applyDataTime = data.value.ApplyDataTime;
				if(applyDataTime != null && applyDataTime != undefined)
					applyDataTime = Ext.util.Format.date(applyDataTime, 'Y-m-d');
				html = html.replace(/{ApplyDataTime}/g, applyDataTime || '');
				var TwoStatusName = "";
				var info = JShell.System.ClassDict.getClassInfoById('LicenceStatus', data.value.Status);
				if(info)
					TwoStatusName = info.Name;
				html = html.replace(/{TwoStatusName}/g, TwoStatusName || '');
				if(me.IsShowAudit) {
					html = html.replace(/{OneAuditName}/g, data.value.OneAuditName || '');
					var OneAuditDataTime = data.value.OneAuditDataTime;
					if(OneAuditDataTime != null && OneAuditDataTime != undefined)
						OneAuditDataTime = Ext.util.Format.date(OneAuditDataTime, 'Y-m-d');
					html = html.replace(/{OneAuditDataTime}/g, OneAuditDataTime || '');
					var GenDateTime = data.value.GenDateTime;
					if(GenDateTime != null && GenDateTime != undefined)
						GenDateTime = Ext.util.Format.date(GenDateTime, 'Y-m-d');
					html = html.replace(/{GenDateTime}/g, GenDateTime || '');

					html = html.replace(/{TwoAuditName}/g, data.value.TwoAuditName || '');
					var TwoAuditDataTime = data.value.TwoAuditDataTime;
					if(TwoAuditDataTime != null && TwoAuditDataTime != undefined)
						TwoAuditDataTime = Ext.util.Format.date(TwoAuditDataTime, 'Y-m-d');
					html = html.replace(/{TwoAuditDataTime}/g, TwoAuditDataTime || '');
					html = html.replace(/{OneAuditInfo}/g, data.value.OneAuditInfo || '');
					html = html.replace(/{TwoAuditInfo}/g, data.value.TwoAuditInfo || '');

				}

			}
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{PClientName}/g, '授权内容');
			html = html.replace(/{Error}/g, data.msg);
		}

		return html;
	},
	/**获取信息HTML模板*/
	getInfoTemplet: function(values) {
		var me = this;
		var status = values.Status;
		var isSpecially = "" + values.IsSpecially;
		if(isSpecially == "false" || isSpecially == false)
			isSpecially = "0";
		else if(isSpecially == "true" || isSpecially == true)
			isSpecially = "1";
		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';

		var templet =
			'<style type="text/css">' +
			'.dl-horizontal dt{width:105px}' +
			'.dl-horizontal dd{margin-left:5px}' +
			'</style>' +
			//标题
			'<div class="col-sm-12" style="text-align:center;margin-top:2px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{PClientName}</h4>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<dl class="dl-horizontal">' +

			'<div class="col-sm-4">' +
			'<dt>申 请 人  ：</dt><dd>{ApplyName}</dd>' +
			'</div>' +
			'<div class="col-sm-4">' +
			'<dt>申请日期：</dt><dd>{ApplyDataTime}</dd>' +
			'</div>' +
			'<div class="col-sm-4">' +
			'<dt>流程状态：</dt><dd style="color:#e98f36;">{TwoStatusName}</dd>' +
			'</div>' +

			'<div class="col-sm-4">' +
			'<dt>当前授权申请号：</dt><dd>{LRNo}</dd>' +
			'</div>' +
			'<div class="col-sm-4">' +
			'<dt>主授权申请号 ：</dt><dd>{LRNo1}</dd>' +
			'</div>' +
             '<div class="col-sm-4">' +
			'<dt>备份授权申请号：</dt><dd>{LRNo2}</dd>' +
			'</div>';
		if(status == "7" || (status == "4" && isSpecially == "0")) {
			templet = templet + '<div class="col-sm-12">' +
				'<dt>授权文件：</dt><dd><a style="font-weight:bold;" filedownload="filedownload" data="' + me.PK + '">下载</a> </dd>' +
				'</div>';
		}
		templet = templet + '<div class="col-sm-12">' +
			'<dt>备注：</dt><dd>{Comment}</dd>' +
			'</div>' +

			'</div>' +
			'</div>' +
			'</div>';

		//一审人、一审时间、一审意见
		if(me.IsShowAudit) {
			templet = templet + '<div class="col-sm-12">' +
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
		}
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
	},
	initDownloadListeners: function() {
		var me = this,
			DomArray = Ext.query("[filedownload]"),
			len = DomArray.length;

		for(var i = 0; i < len; i++) {
			DomArray[i].onclick = function() {
				var id = this.getAttribute("data");
				me.onDwonload(id);
			};
		}
	},
	onDwonload: function(id) {
		var me = this;
		if(id == null || id == undefined || id == "")
			id = me.PK;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=0&id=' + id;
		window.open(url);
	}
});