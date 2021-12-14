/**
 * 合同内容
 * @author 
 * @version 2016-11-02
 */
Ext.define('Shell.class.wfm.business.contract.basic.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '合同内容',
	width: 800,
	height: 600,
	
	autoScroll:true,
	
	/**获取数据服务路径*/
	selectInfoUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractById',
	/**获取附件服务路径*/
	selectAttachmentUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCAttachmentByHQL',
	/**任务操作记录服务路径*/
	selectOperLogUrl:'/SystemCommonService.svc/SC_UDTO_SearchSCOperationByHQL',
	/**文件下载服务路径????*/
    downloadUrl:'/SystemCommonService.svc/SC_UDTO_DownLoadSCAttachment',
    
    /**加载数据提示*/
	lodingText:JShell.Server.LOADING_TEXT,
	
	/**信息数据*/
	InfoData: '',
	/**附件数据*/
	AttachmentData: '',
	/**操作记录数据*/
	OperLogData:'',
	
	/**是否显示操作记录*/
	showOperLog:true,
	
	/**信息ID*/
	PK:null,
	/**仪器清单*/
	LinkEquipInfoListHTML:'',
	/**采购说明*/
	PurchaseDescHTML:'',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.PK){
			//初始化Html页面
			me.initHtml();
		}
	},
	
	initComponent: function() {
		var me = this;
		
		if(me.PK){
			me.html = 
			'<div class="loading-div">' +
				'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
				'<div style="padding-top:10px;">页面加载中</div>' +
			'</div>';
		}
		JShell.Function.showWorklogInteraction = function(ele, e) {
			me.showInfoList(me.LinkEquipInfoListHTML,'仪器清单');
		};
		JShell.Function.showWorklogPurchaseDesc = function(ele, e) {
			me.showInfoList(me.PurchaseDescHTML,'采购说明');
		};
		me.callParent(arguments);
	},
	
	initHtml:function(){
		var me = this;
		
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请重新加载');
    			return;
    		}
			
			//显示遮罩
			me.showMask(me.lodingText);
			
			me.loadInfoData(function(data){
				me.InfoData = data;//信息数据
				me.loadAttachmentData(function(data){
					me.AttachmentData = data;//附件数据
					
					if(me.showOperLog){
						me.loadOperLogData(function(data){
							me.OperLogData = data;//操作记录数据
							me.initHtmlContent();//初始化HTML页面
						});
					}else{
						me.initHtmlContent();//初始化HTML页面
					}
				});
			});
    	});
	},
	/**初始化HTML页面*/
	initHtmlContent: function() {
		var me = this,
			html = [];
			
		//获取任务HTML
		html.push(me.getInfoHtml());
		//获取附件HTML
		html.push(me.getAttachmentHtml());
		
		if(me.showOperLog){
			//获取操作记录HTML
			html.push(me.getOperLogHtml());
		}
		
		//获取仪器清单HTML和仪器清单
		html.push(me.getEquipInfoListHtml());

		//更新HTML内容
		me.update(html.join(''));
		
		//隐藏遮罩
		me.hideMask();
		
		//初始化下载监听
		me.initDownloadListeners();
	},
	/**加载信息*/
	loadInfoData:function(callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectInfoUrl);
			
		//用户名称、付款单位、合同编号、名称
		//合同总额、已收金额、剩余款项、已开票金额、本公司名称
		//销售负责人、签署人、签署日期、有偿服务应开始时间
		//是否开具发票、发票备注、备注
		var fields = [
			'Id','PClientName','PayOrg','ContractNumber','Name',
			'Amount','PayedMoney','LeftMoney','InvoiceMoney','Compname',
			'Principal','SignMan','SignDate','PaidServiceStartTime',
			'IsInvoices','InvoiceMemo','Comment','LinkEquipInfoListHTML','PurchaseDescHTML'
		];
		
		fields = "PContract_" + fields.join(",PContract_");
		
		url += "?isPlanish=true&fields=" + fields + "&id=" + me.PK;
		
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**获取附件信息*/
	loadAttachmentData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);
			
		var fields = ['Id', 'FileName','FileSize','CreatorName','DataAddTime','NewFileName','FileExt'];
		url += "?isPlanish=true&fields=SCAttachment_" + fields.join(",SCAttachment_");
		url += '&where=scattachment.IsUse=1 and scattachment.BobjectID=' + me.PK;
		
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**获取操作记录信息*/
	loadOperLogData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectOperLogUrl);
			
		var fields = ['Id','Type','CreatorID','CreatorName','DataAddTime','Memo'];
		url += '?isPlanish=true&fields=SCOperation_' + fields.join(',SCOperation_');
		url += '&where=scoperation.BobjectID=' + me.PK;
		url += '&sort=[{"property":"SCOperation_DataAddTime","direction":"ASC"}]';
		
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	
	/**获取信息HTML*/
	getInfoHtml:function(){
		var me = this,
			data = me.InfoData,
			html = '';
			
		if(data.success){
			html = me.getInfoTemplet();
			if(data.value){
				html = html.replace(/{PClientName}/g,data.value.PContract_PClientName || '');
				html = html.replace(/{PayOrg}/g,data.value.PContract_PayOrg || '');
				html = html.replace(/{ContractNumber}/g,data.value.PContract_ContractNumber || '');
				html = html.replace(/{Name}/g,data.value.PContract_Name || '');
				
				html = html.replace(/{Amount}/g,data.value.PContract_Amount || '');
				html = html.replace(/{PayedMoney}/g,data.value.PContract_PayedMoney || '');
				html = html.replace(/{LeftMoney}/g,data.value.PContract_LeftMoney || '');
				html = html.replace(/{InvoiceMoney}/g,data.value.PContract_InvoiceMoney || '');
				html = html.replace(/{Compname}/g,data.value.PContract_Compname || '');
				
				html = html.replace(/{Principal}/g,data.value.PContract_Principal || '');
				html = html.replace(/{SignMan}/g,data.value.PContract_SignMan || '');
				html = html.replace(/{SignDate}/g,JShell.Date.toString(data.value.PContract_SignDate,true) || '');
				html = html.replace(/{PaidServiceStartTime}/g,
					JShell.Date.toString(data.value.PContract_PaidServiceStartTime,true) || '');
				
				html = html.replace(/{IsInvoices}/g,data.value.PContract_IsInvoices || '');
				html = html.replace(/{InvoiceMemo}/g,data.value.PContract_InvoiceMemo || '');
				html = html.replace(/{Comment}/g,data.value.PContract_Comment || '');
				me.LinkEquipInfoListHTML=data.value.PContract_LinkEquipInfoListHTML || '无';
				me.PurchaseDescHTML=data.value.PContract_PurchaseDescHTML || '无';
			}
		}else{
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'合同内容');
			html = html.replace(/{Error}/g,data.msg);
		}
		
		return html;
	},
	/**获取信息HTML模板*/
	getInfoTemplet: function() {
		
		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';
		
		var templet =
		//合同标题
		'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{Name}</h4>' +
		'</div>' +
		//合同编号、本公司名称、销售负责人、签署人、签署日期、有偿服务应开始时间
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '"><b>合同编号：{ContractNumber}</b></div>' +
				'<div style="' + sDivStyle + '">本公司名称：{Compname}</div>' +
				'<div style="' + sDivStyle + '">销售负责人：{Principal}</div>' +
				'<div style="' + sDivStyle + '">签署人：{SignMan}</div>' +
				'<div style="' + sDivStyle + '">签署日期：{SignDate}</div>' +
				'<div style="' + sDivStyle + '">有偿服务应开始时间：{PaidServiceStartTime}</div>' +
			'</div>' +
		'</div>' +
		//用户名称
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '"><b>用户名称：{PClientName}</b></div>' +
			'</div>' +
		'</div>' +
		//付款单位
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '"><b>付款单位：{PayOrg}</b></div>' +
			'</div>' +
		'</div>' +
		//合同总额、已收金额、剩余款项
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">合同总额：{Amount}</div>' +
				'<div style="' + sDivStyle + '">已收金额：{PayedMoney}</div>' +
				'<div style="' + sDivStyle + '">剩余款项：{LeftMoney}</div>' +
				'<div style="' + sDivStyle + '">已开票金额：{InvoiceMoney}</div>' +
			'</div>' +
		'</div>' +
		//发票备注
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">发票备注</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">是否开具发票：{IsInvoices}</p>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{InvoiceMemo}</p>' +
		'</div>' +
		//合同备注
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">合同备注</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{Comment}</p>' +
		'</div>';
		return templet;
	},
	
	/**获取附件HTML*/
	getAttachmentHtml: function() {
		var me = this,
			data = me.AttachmentData,
			html = '';
			
		if(data.success) {
			var list = (data.value || {}).list || [],
				len = list.length,
				attArr = [],
				html= '';
				
			for(var i = 0; i < len; i++) {
				attArr.push(me.getOneAttachmentHtml(list[i]));
			}
			html = me.getAttachmentTemplet();
			html = html.replace(/{AttachmentList}/g, (attArr.join("") || '无'));
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'附件');
			html = html.replace(/{Error}/g,data.msg);
		}
		return html;
	},
	/**获取一条附件信息HTML*/
	getOneAttachmentHtml:function(data){
		var me = this,
			temp = me.getOneAttachmentTemplet(),
			html = '';
		
		var Title = data.SCAttachment_CreatorName + ' 创建于 ' +
			JShell.Date.toString(data.SCAttachment_DataAddTime);
		var Name = data.SCAttachment_NewFileName + data.SCAttachment_FileExt;
			
		temp = temp.replace(/{Id}/g, data.SCAttachment_Id);
		temp = temp.replace(/{Title}/g, Title);
		temp = temp.replace(/{FileName}/g, Name);
		temp = temp.replace(/{FileSize}/g, JShell.Bytes.toSize(data.SCAttachment_FileSize));
		
		html = temp;
		
		return html;
	},
	
	/**获取附件模板*/
	getAttachmentTemplet: function() {
		return this.getContentTemplet('附件信息','{AttachmentList}');
	},
	/**获取一个附件模板*/
	getOneAttachmentTemplet: function() {
		var templet =
		'<div style="padding:5px;">' +
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" title="{Title}">{FileName}</a> ' +
			'<span style="color:green;">({FileSize})</span>' +
		'</div>';
		return templet;
	},
	
	/**获取操作记录HTML*/
	getOperLogHtml: function() {
		var me = this,
			data = me.OperLogData,
			html = '';
			
		if(data.success) {
			var list = (data.value || {}).list || [],
				len = list.length,
				attArr = [],
				html= '';
				
			for(var i = 0; i < len; i++) {
				attArr.push(me.getOneOperLogHtml(list[i]));
			}
			html = me.getOperLogTemplet();
			html = html.replace(/{OperLogList}/g, (attArr.join("") || '无'));
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'操作记录');
			html = html.replace(/{Error}/g,data.msg);
		}
		return html;
	},
	/**获取仪器清单HTML*/
	getEquipInfoListHtml: function() {
		var me = this,
			html = '';
		html='<div class="col-sm-12">' +
			"<button class='hand'" +
			" style='margin-left:5px;padding:5px 10px;background-color:#337ab7;'" +
			" onclick='JShell.Function.showWorklogInteraction(this,event)'>" +
			"<a style='color:#ffffff;'>仪器清单</a>" +
			"</button>" +
            "<button class='hand'" +
			" style='margin-left:5px;padding:5px 10px;background-color:#337ab7;'" +
			" onclick='JShell.Function.showWorklogPurchaseDesc(this,event)'>" +
			"<a style='color:#ffffff;'>采购说明</a>" +
			"</button>" +
		'</div>';	
		return html;
	},
	/**获取一条操作记录信息HTML*/
	getOneOperLogHtml:function(data){
		var me = this,
			temp = me.getOneOperLogTemplet(),
			html = '';
		
		var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',data.SCOperation_Type + '');
		
		var style = [];
		if(info && info.BGColor){style.push('color:' + info.BGColor);}
		var OperTypeName = info ? '<b style="' + style.join(';') + '">' + info.Name + '</b> ' : '';
		
		if(data.SCOperation_Memo){
			OperTypeName += '处理意见：<b>' + data.SCOperation_Memo + '</b>';
		}
			
		temp = temp.replace(/{DataAddTime}/g, JShell.Date.toString(data.SCOperation_DataAddTime));
		temp = temp.replace(/{OperaterName}/g, data.SCOperation_CreatorName);
		temp = temp.replace(/{OperTypeName}/g, OperTypeName);
		
		html = temp;
		
		return html;
	},
	
	/**获取操作记录模板*/
	getOperLogTemplet: function() {
		return this.getContentTemplet('操作记录','{OperLogList}');
	},
	/**获取一个操作记录模板*/
	getOneOperLogTemplet: function() {
		var templet =
		'<div style="padding:5px;">' +
			'{DataAddTime} {OperaterName} {OperTypeName}' +
		'</div>';
		return templet;
	},
	
	/**获取内容模板*/
	getContentTemplet:function(title,name){
		var templet =
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">' + title + '</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">' + name + '</p>' +
		'</div>';
		return templet;
	},
	/**获取错误信息HTML模板*/
	getErrorTemplet:function(){
		var templet = 
		'<div class="col-sm-12">' +
			'<h4>{Title}-错误信息</h4>' +
			'<p style="color:red;padding:5px;word-break:break-all;word-wrap:break-word;">{Error}</p>' +
		'</div>';
		return templet;
	},
	
	/**初始化下载监听*/
	initDownloadListeners:function(){
		var me = this,
			DomArray = Ext.query("[filedownload]"),
			len = DomArray.length;
			
		for(var i=0;i<len;i++){
			DomArray[i].onclick = function() {
				var id = this.getAttribute("data");
				me.onDwonload(id);
			};
		}
	},
	/**点击下载文件*/
	onDwonload:function(id){
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=1&id=' + id;
		window.open(url);
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.body){me.body.unmask();}//隐藏遮罩层
	},
	
	/**@public 加载数据*/
	load:function(id){
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
	/**仪器清单*/
	showInfoList:function(value,title){
		var me = this;
		JShell.Win.open('Ext.panel.Panel', {
			title:title,
			html:value,
			width:800,
			height:420,
			autoScroll : true,
			bodyPadding:10
		}).show();
	}
});