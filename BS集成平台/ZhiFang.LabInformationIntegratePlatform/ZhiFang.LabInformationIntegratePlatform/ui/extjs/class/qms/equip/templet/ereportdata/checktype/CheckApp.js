/**
 *预览仪器质量记录PDF文件
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.checktype.CheckApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '预览仪器质量记录PDF文件',
	layout:'border',
	width: 500,
	height: 400,
	hasBtn:true,
	hasBtntoolbar:true,
	PK:'',
	ETempletId:'',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_PreviewPdf',
    /**获取数据服务路径（已审）*/
	selectUrl2: '/QMSReport.svc/QMS_UDTO_PreviewCheckPdf',	
	/**数据审核*/
	CheckReportUrl : '/QMSReport.svc/QMS_UDTO_CheckReport',
	checkviewLable:'审核意见',
	btnsaveText:'审核',
	/**预览数据*/
	CurrentPageData:[],
	/**当前行数据*/
	CurrentData:[],
	/**当前行ID*/
	layout: 'border',
	startDate:null,
	endDate:null,
	rowIndex:0,
	CurrentDataLen:0,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getEl().setStyle('z-index', 1);
		me.CurrentDataLen =me.CurrentPageData.length;
		if(me.rowIndex<0 || me.rowIndex>me.CurrentDataLen) return;  
        me.CurrentData=me.CurrentPageData[me.rowIndex];
	    me.onCurrentData(me.CurrentData);
	},

	initComponent: function() {
		var me = this;
		me.title = me.title || "预览仪器质量记录PDF文件";
        me.items =[];	
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasBtntoolbar) items.push(me.createButtonbottomtoolbar());
		return items;
	},
	//查看PDF内容
    showPdf : function(url,isClear){
    	var me=this;
    	var a='%22';
    	me.showMask();
    	var html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>文件还未获取</div>';
    	if(url){
           html = '<iframe ' +
				'height="100%" width="100%" frameborder="0" ' +
				'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
				'height:100%;width:100%;position:absolute;' +
				'top:0px;left:0px;right:0px;bottom:0px;" '+ 'src=' + url+ '>' +
			'</iframe>';
    	}
    	if(isClear == true) html = '';
    	me.update(html);
    	me.hideMask();
    },
	/**创建功能按钮栏*/
	createButtonbottomtoolbar: function() {
		var me = this,
			items =  [];
		var maxwidth=me.width-290;
		items.push({
			xtype: 'textfield',labelWidth: 65,height:20,		
			margin:'0px 0px 10px 0px',
			width: maxwidth,name: 'checkview',itemId: 'checkview',
			labelAlign: 'right',fieldLabel:me.checkviewLable,magin: '5'
		},'-',{
	        xtype: 'label',
	        text: '此条记录已审核',itemId: 'lbText',
	        style: "font-weight:bold;color:red;",
	        margin: '0 0 10 10'
		},{
			type: 'button',
			iconCls: 'button-save hand',
			text:me.btnsaveText,
			itemId: 'SaveBtn',
			handler: function(grid, rowIndex, colIndex) {
				var msg = "确定要审核吗";
				JShell.Msg.confirm({
					msg:msg
				}, function(but) {
					if (but != "ok") return;
				    me.SaveClick();
				});
			}
		},'->','-',{
			type: 'button',iconCls: Ext.baseCSSPrefix + 'tbar-page-prev',
			text:'上一页',itemId: 'PreBtn',width:65,
			handler: function() {
				me.rowIndex=me.rowIndex-1;
				if(me.rowIndex<0){
					JShell.Msg.alert('已经是第一行数据', null, 1000);
					me.rowIndex=0;
					return;
				}
				if(me.rowIndex>me.CurrentDataLen){
					JShell.Msg.alert('已经是最后一行数据', null, 1000);
					me.rowIndex=me.CurrentDataLen;
					return;
				}
				me.clearCheckMemo();
				me.CurrentData=me.CurrentPageData[me.rowIndex];
				me.onCurrentData(me.CurrentData);
			}
    
		},'-',{
			type: 'button',
			iconCls: Ext.baseCSSPrefix + 'tbar-page-next',
			text:'下一页',width:65,
			itemId: 'nextBtn',
			handler: function(grid, rowIndex, colIndex) {
				me.rowIndex+=1;
				if(me.rowIndex<0){
					JShell.Msg.alert('已经是第一行数据', null, 1000);
					me.rowIndex=0;
					return;
				}
				if(me.rowIndex>me.CurrentDataLen){
					JShell.Msg.alert('已经是最后一行数据', null, 1000);
					me.rowIndex=me.CurrentDataLen;
					return;
				}
				me.clearCheckMemo();
				me.CurrentData=me.CurrentPageData[me.rowIndex];
				me.onCurrentData(me.CurrentData);
			}
		},'-',{
			xtype: 'button',itemId: 'btnColse',iconCls: 'button-del',
			text: "关闭",tooltip: '关闭',margin:'0px 10px 0px 0px',
			handler: function() {
				me.fireEvent('onCloseClick', me);
				me.close();
			}
	    });	
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottombuttonsToolbar',
			items: items
		});
	 },
	 onCurrentData :function(CurrentData){
    	 var me =this;
    	 me.IsCurrentDataCheck='0';
    	 JShell.Action.delay(function(){
		    if(CurrentData){
		    	var IsCheck = CurrentData.EReportData_IsCheck;
		    	if(IsCheck=='1' || IsCheck==1){
		            me.changeBtn(true);
		    	}else{
		    		me.changeBtn(false);
		    	}
				var url = me.getUrl(CurrentData);
	            me.showPdf(url,false);
			}
	    },null,200);
    },
    changeBtn:function(bo){
    	var me=this;
    	var buttonsToolbar=me.getComponent('bottombuttonsToolbar');
    	var checkview=buttonsToolbar.getComponent('checkview');
    	var SaveBtn=buttonsToolbar.getComponent('SaveBtn');
    	var PreBtn=buttonsToolbar.getComponent('PreBtn');
    	var nextBtn=buttonsToolbar.getComponent('nextBtn');
    	var lbText=buttonsToolbar.getComponent('lbText');
        
        if(bo){
        	lbText.setVisible(true);
        	checkview.setVisible(false);
        	SaveBtn.setVisible(false);
        	nextBtn.setVisible(true);
        	PreBtn.setVisible(true);
        }else{
            lbText.setVisible(false);
        	checkview.setVisible(true);
        	SaveBtn.setVisible(true);
        	nextBtn.setVisible(true);
        	PreBtn.setVisible(true);
        }
    },
	/**预览仪器质量记录PDF文件URL*/
	getUrl: function(list) {
		var me = this;
		var ETempletId = list.EReportData_TempletID;
		var ReportName = list.EReportData_ReportName;
		var IsCheck =list.EReportData_IsCheck;
		var TempletBatNo = list.EReportData_TempletBatNo;
		//已审
		if(IsCheck=='1'){
			var url = JShell.System.Path.ROOT + me.selectUrl2;
			var id  =  list.EReportData_ReportDataID;
			url+="?reportDataID="+id+"&operateType=1";
		}else{
			var url = JShell.System.Path.ROOT + '/QMSReport.svc/QMS_UDTO_PreviewPdf';
			var whereParams = me.getPdfWhereParams(list);
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'templetID=' + ETempletId + '&operateType=1&isCheckPreview=1';
			if(TempletBatNo)url +='&templetBatNo='+TempletBatNo;
			if(whereParams) {
				url += whereParams;
			}
		}
		if(ReportName) {
			url += "&reportName=" + ReportName;
		}
		return url;
	},
	/**清空审核说明*/
	clearCheckMemo:function(){
		var me =this;
		var checkview=me.getComponent('bottombuttonsToolbar').getComponent('checkview');
        checkview.setValue('');
	},
	/**获取Pdf带查询条件*/
	getPdfWhereParams: function(list) {
		var me = this,
			whereParams = "",
			params = [];
		var Sysdate = list.EReportData_ReportDate;
		var StrDate = JcallShell.Date.toString(Sysdate, true);
		if(Sysdate) {
			params.push("&endDate=" + StrDate);
			params.push("&beginDate=" + StrDate);
		}
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
	/**
	 * 审核功能
	 * 
	 * */
	SaveClick: function() {
		var me=this;
		if(me.CurrentData.length==0)return;
		var TempletID = me.CurrentData.EReportData_TempletID;
		var Sysdate = me.CurrentData.EReportData_ReportDate;
		var curDate = JcallShell.Date.toString(Sysdate, true);
		me.getCheckReport(TempletID, curDate, curDate);
	},
	/**审核质量记录*/
	getCheckReport: function(TempletID, beginDate, endDate) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.CheckReportUrl);
		url += '?templetID=' + TempletID;
		if(beginDate != '') {
			url += "&beginDate=" + beginDate;
		}
		if(endDate != '') {
			url += "&endDate=" + endDate;
		}
		var checkview=me.getComponent('bottombuttonsToolbar').getComponent('checkview').getValue();
		if(checkview){
			url += "&checkview=" + checkview;
		}
		JShell.Server.get(url, function(data) {
			if(data.success) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
                me.fireEvent('save', me);
                me.changeBtn(true);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	}
});