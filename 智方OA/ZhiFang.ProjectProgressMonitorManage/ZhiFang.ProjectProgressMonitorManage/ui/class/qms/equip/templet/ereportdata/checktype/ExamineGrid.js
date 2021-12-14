/**
 * 质量记录每日审核
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.checktype.ExamineGrid', {
	extend: 'Shell.class.qms.equip.templet.ereportdata.ExamineGrid',
	requires: ['Ext.ux.CheckColumn'],
	title: '质量记录每日审核',
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			xtype: 'rownumberer',text: '序号',width: me.rowNumbererWidth,align: 'center'
		},{
			text: 'Id',dataIndex: 'EReportData_ReportDataID',
			width: 100,hidden: true,sortable: false,
			isKey: true,menuDisabled: true,defaultRenderer: true
		}, {
			text: '仪器模板Id',dataIndex: 'EReportData_TempletID',
			width: 100,hidden: true,sortable: false,
			menuDisabled: true,defaultRenderer: true
		}, {
			text: '质量记录名称',dataIndex: 'EReportData_ReportName',
			minWidth: 270,flex:1,sortable: true,defaultRenderer: true
		},{
			text: '类型',dataIndex: 'EReportData_TempletTypeName',
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		}, {
			text: '代码',dataIndex: 'EReportData_TempletCode',
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		},{
			text: '日期',dataIndex: 'EReportData_ReportDate',
			width: 85,type: 'date',sortable: true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = JShell.Date.toString(value, true) || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text: '审核状态',dataIndex: 'EReportData_IsCheck',
			width: 60,sortable: true,menuDisabled: true,//align: 'center',
			renderer: function(value, meta) {
				var v = value;
				if(value == '1') {
					v = '已审';
					meta.style = 'color:green';
				} else {
					v = '未审';
					meta.style = 'color:red';
				}
				return v;
			}
		},{
			text: '数据状态',dataIndex: 'EReportData_IsContainData',
			width: 60,sortable: true,menuDisabled: true,
			renderer: function(value, meta) {
				var v = value;
				if(value == '1') {
					v = '已存';
					meta.style = 'color:green';
				} else {
					v = '未存';
					meta.style = 'color:red';
				}
				return v;
			}
		},{
			text: '仪器名称',dataIndex: 'EReportData_EquipName',
			minWidth: 100,flex:1,sortable: true,defaultRenderer: true
		}, {
			text: '小组名称',dataIndex: 'EReportData_SectionName',
			width: 100,sortable: true,defaultRenderer: true
		},  {
			text: '附件标志',
			dataIndex: 'EReportData_IsAttachment',width: 100,hidden: true,sortable: false,menuDisabled: false,
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',text: '预览',align: 'center',
			tooltip: '预览',width: 60,
			style: 'font-weight:bold;color:white;background:orange;',hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
                    me.myGridPanel.getSelectionModel().select(rowIndex);
					var rec = grid.getStore().getAt(rowIndex);
					me.openForm(rowIndex);
				}
			}]
		}, {
			xtype: 'actioncolumn',text: '附件',
			align: 'center',tooltip: '附件',width: 60,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
                	//IsAttachment等于0 代表没有附件 大于0 有附件 
					var IsAttachment=record.get('EReportData_IsAttachment');
					if( IsAttachment && parseInt(IsAttachment)>0){
						return 'button-show hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var TempletID = rec.get('EReportData_TempletID');
					var ReportDate = rec.get('EReportData_ReportDate');
					var beginDate= JcallShell.Date.toString(ReportDate,true);
					var endDate=JShell.Date.getNextDate(beginDate,1);
					var endDate2=JcallShell.Date.toString(endDate,true);
					var id = rec.get('EReportData_ReportDataID');
					me.showAttachmentById(id, TempletID, beginDate, endDate2);
				}
			}]
		}, {
			text: '审核意见',dataIndex: 'EReportData_CheckView',
			width: 100,hidden:true,sortable: true,
			menuDisabled: true,defaultRenderer: true
		}, {
			xtype: 'actioncolumn',text: '已审',align: 'center',
			width: 40,style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,hidden: true,tooltip: '已审',
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('EReportData_IsCheck') == '1') {
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {}
			}]
		}, {
			text: '模板路径',dataIndex: 'EReportData_TempletPath',
			width: 240,hidden: true,menuDisabled: true,defaultRenderer: true
		},{
			text: '仪器类型',dataIndex: 'EReportData_EquipTypeName',
			width: 100,sortable: true,defaultRenderer: true
		},{
			text: '小组代码',dataIndex: 'EReportData_SectionUseCode',
			width: 100,sortable: true,defaultRenderer: true
		},{
			text: '批号',hidden:true,
			dataIndex: 'EReportData_TempletBatNo',width: 100,sortable: false,menuDisabled: false,
			defaultRenderer: true
		} ];
		return columns;
	},
	
	createbuttonToolbarItems: function() {
		var me = this;
		var items = [];
	    items.push({text:'刷新',tooltip:'刷新',iconCls:'button-refresh',
		    handler:function(){
		    	me.onRefreshClick();
		    }
		}, '-', {
		   xtype: "datefield",
		   name: "startdate",itemId:'startdate', fieldLabel: "",value:me.defaultStartDate,
		   editable: false,emptyText: "开始日期",format: "Y-m-d",width: 100
		},{
		   xtype: "datefield",
		   name: "enddate",itemId:'enddate', fieldLabel: "",value:me.defaultEndDate,
		   editable: false,emptyText: "结束日期",format: "Y-m-d",width: 100
		},'-',{
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
		    name: 'fastKey', margin: '0 0 0 5',
		    emptyText: '质量记录名称',
			listeners:{
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER){
						me.onGridSearch();
						me.fireEvent('changeData', me.myGridPanel);
					}
				}
            }
        },
		'-',{text:'今天未审核', xtype: 'button', enableToggle:false, itemId:'monthBtn'},
        '-',{text:'昨天未审核', xtype: 'button', itemId:'premonthBtn' },
	    '-', {text:'批量审核', xtype: 'button',tooltip:'批量审核',iconCls:'button-save', itemId:'checksave',
	        handler:function(){
	        	me.onSaveCheckReport();
	        }},'->',{
	        xtype: 'label',forId: 'myFieldId',
	        itemId: 'myFieldId',style: "font-weight:bold;color:blue;",
	        text: '查询全部数据,包括已审核和未审核', margin: '0 0 0 10'
	    });
	    return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**获取带查询条件
     */
	getSearchWhereParams: function() {
		var me = this,
			whereParams = "",
			params = [];
	    var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
	    var startdatecom = buttonsToolbar.getComponent('startdate');
	    var enddatecom=buttonsToolbar.getComponent('enddate');
//	    var txtTempletName=buttonsToolbar.getComponent('txtTempletName').getValue();
	    var start=startdatecom.getValue();
	    var end=enddatecom.getValue();
	    if(!start)return;
	    if(!end)return;
	    var otherPara=null;
	    startdate=JShell.Date.toString(start,true );
	    endDate=JShell.Date.toString(end,true);
        if(!startdate ){
        	JShell.Msg.error('开始时间不能为空');
        	return;
        }
        if(!endDate){
        	JShell.Msg.error('结束时间不能为空');
        	return;
        }
        params.push("&endDate=" + endDate);
		params.push("&beginDate=" + startdate);
		//按每天查询 不传递此参数或0 默认按月
		params.push("&checkType=1");
		//模板类型参数 1是按日审核
        params.push("&templetType=0");
        var ParaArr=[];
        var paraobj={};
        if(me.myTag=='2' || me.myTag=='3'){
        	paraobj.IsCheck='0';
        }
//      if(txtTempletName){
//      	var ETempletStr=JcallShell.String.encode(txtTempletName); 
//	        paraobj.TempletName=ETempletStr;
//      }
        ParaArr.push(paraobj);
        var ParaStr=null;
        if(ParaArr.length>0){
        	var srtarr=Ext.encode(ParaArr);
            ParaStr = srtarr.replace(/\[/g,'');
            ParaStr = ParaStr.replace(/\]/g,'');
        }
        if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
        if(ParaArr.length>0){
        	whereParams +="&otherPara="+ParaStr;
        }
		return whereParams;
	},
	
	
	initFilterListeners:function(){
		var me=this;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
		if(!buttonsToolbar)return;
        var monthBtn = buttonsToolbar.getComponent('monthBtn');
        var premonthBtn = buttonsToolbar.getComponent('premonthBtn');
	    var myFieldId=buttonsToolbar.getComponent('myFieldId');
	    
	    var startdatecom = buttonsToolbar.getComponent('startdate');
	    var enddatecom=buttonsToolbar.getComponent('enddate');

	    var Sysdate = JcallShell.System.Date.getDate();
	    if(Sysdate == null) Sysdate = new Date();
	    //今天
	    var TDate = JcallShell.Date.toString(Sysdate, true);
        //昨天
        var YDate = JcallShell.Date.getNextDate(Sysdate, -1);
        var tab=true;
	    //选中本月未审核
		monthBtn.on({
			click:function(){
				tab=false;
				startdatecom.setValue(TDate);
				enddatecom.setValue(TDate);
				me.myTag='2';
				monthBtn.toggle(true);
				premonthBtn.toggle(false);
				myFieldId.setText('查询今天未审数据');
				me.onGridSearch();
				tab=true;
			}
		});
		premonthBtn.on({
			click:function(){
				tab=false;
				me.myTag='3';
				curDate=JShell.Date.toString(YDate,true);
				startdatecom.setValue(curDate);
				enddatecom.setValue(curDate);
				monthBtn.toggle(false);
				premonthBtn.toggle(true);
				myFieldId.setText('查询昨天未审数据');
				me.onGridSearch();
				tab=true;
			}
		});
		var strval=startdatecom.getValue();
		var endval=enddatecom.getValue();
        //只能查半年的数据,超过需提示
		startdatecom.on({
			change : function(com,newValue,oldValue,eOpts ){
				if(!tab)return;
				monthBtn.toggle(false);
				premonthBtn.toggle(false);
				if(endval && newValue){
					var IsExect = me.checkStartDate(newValue);
					if(IsExect){
						me.onGridSearch();
					}
				}
			}
		});
		enddatecom.on({
			change : function(com,newValue,oldValue,eOpts ){
				if(!tab)return;
				monthBtn.toggle(false);
				premonthBtn.toggle(false);
				if(strval && newValue ){
					var IsExect = me.checkEndDate(newValue);
					if(IsExect){
						me.onGridSearch();
					}
				}
			}
		});
	},
	/**时间比较，超过半年提示*/
	checkStartDate :function(newValue){
		var me =this;
		var isexect=true;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
	    var startdatecom = buttonsToolbar.getComponent('startdate');
	    var enddatecom=buttonsToolbar.getComponent('enddate');
		startdate=startdatecom.getValue();	 
		endDate=enddatecom.getValue();
		if(!startdate)return;
		if(!endDate)return;
	 	
		//最小开始时间
		var Start =JcallShell.Date.getNextDate(endDate,-182.5);	
		if((startdate && endDate) && (startdate > endDate)){
			JShell.Msg.alert('开始时间不能大于结束时间', null, 2000);
			isexect=false;
			return  false;
		}
		if((endDate &&startdate) && (startdate<Start) ){
			JShell.Msg.alert("只能查半年内的数据", null, 2000);
			isexect=false;
			return false;
		}
		return isexect;
	},
	/**时间比较，超过半年提示*/
	checkEndDate :function(newValue){
		var me =this;
        var isexect=true;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
	    var startdatecom = buttonsToolbar.getComponent('startdate');
	    var enddatecom=buttonsToolbar.getComponent('enddate');
		startdate=	startdatecom.getValue();	 
		endDate=enddatecom.getValue();
		if(!startdate)return;
		if(!endDate)return;
		//最大结束时间
		var nowenddata =JcallShell.Date.getNextDate(startdate,182.5);
	    if((startdate && endDate) &&(startdate>endDate)){
	    	JShell.Msg.alert('结束时间不能小于开始时间', null, 2000);
			isexect=false;
			return false;
	    }
		if((startdate && endDate) && (nowenddata<newValue)){
			JShell.Msg.alert("只能查半年内的数据", null, 2000);
			isexect=false;return false;
		}
		return isexect;
	},
	
	/**预览仪器质量记录PDF文件URL*/
	getUrl: function(rec) {
		var me = this;
		var ETempletId = rec.get('EReportData_TempletID');
		var ReportName = rec.get('EReportData_ReportName');
		var url = JShell.System.Path.ROOT + '/QMSReport.svc/QMS_UDTO_PreviewPdf';
		var whereParams = me.getPdfWhereParams(rec);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'templetID=' + ETempletId + '&operateType=1&isCheckPreview=1';
		if(whereParams) {
			url += whereParams;
		}
		if(ReportName) {
			url += "&reportName=" + ReportName;
		}
		return url;
	},
	/**获取Pdf带查询条件*/
	getPdfWhereParams: function(rec) {
		var me = this,
			whereParams = "",
			params = [];
		var Sysdate = rec.get('EReportData_ReportDate');
		var endDate=JcallShell.Date.toString(Sysdate,true)
		if(Date) {
			params.push("&endDate=" + endDate);
			params.push("&beginDate=" + endDate);
		}
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
	
	/**批量审核按钮点击处理方法*/
	onSaveCheckReport: function() {
		var me = this,
			records = me.myGridPanel.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var list=[];
		//找到未审核的行数据
		for (var i in records) {
			if(records[i].get('EReportData_IsCheck') == '0') {
				var EReportData_Id = records[i].get('EReportData_Id');
				var TempletID = records[i].get('EReportData_TempletID');
				var ReportDate = records[i].get('EReportData_ReportDate');
				var endDate = JShell.Date.toString(ReportDate,true);
				var obj={
					Id:EReportData_Id,
					TempletID:TempletID,
					stratDate:endDate,
					endDate:endDate
				};
				list.push(obj);
			}
		}
		if(list.length==0){
			JShell.Msg.alert('选中行已审核');
			return;
		} 
		
        Ext.MessageBox.show({
			title: '操作确认消息',
			msg: '批量审核选中行？',
			icon: Ext.Msg.QUESTION,
			buttons: Ext.MessageBox.OKCANCEL,
			fn: function(btn) {
				if(btn == 'ok') {
					me.delErrorCount = 0;
					me.delCount = 0;
					me.delLength = list.length;
		            
					me.showMask("正在审核..."); //显示遮罩层
				    for(var i=0;i<list.length;i++){
						var TempletID = list[i].TempletID;
						var stratDate = list[i].stratDate;
						var endDate = list[i].endDate;
						me.SaveOne(i, TempletID,stratDate,endDate);
						
					}
				}
			}
		});
		
	},
	/**初始化送检时间，默认当天*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var StrDate = JcallShell.Date.toString(Sysdate, true);
		me.defaultEndDate=StrDate;
		me.defaultStartDate=StrDate;
	},
	/**
	 * 审核功能
	 * 
	 * */
	SaveClick: function(panel) {
		var me=this;
		//审核
		var records = me.myGridPanel.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var TempletID = records[0].get('EReportData_TempletID');
		var Sysdate = records[0].get('EReportData_ReportDate');
		var Start = JcallShell.Date.toString(Sysdate, true);
		me.getCheckReport(TempletID, Start, Start, panel);
	},
	/**打开预览窗口*/
	openForm: function(rowIndex) {
		var me = this;
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		var config = {
			width: maxWidth,
			height: height,
            rowIndex:rowIndex,
			CurrentPageData:me.getCurrentPageData(),
			SUB_WIN_NO : '1',
			listeners: {
				close:function(){
					me.onGridSearch();
				},
				onSaveClick: function(win) {
					me.fireEvent('onSaveClick', win);
				}
			}
		};
		var win =JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.checktype.CheckApp', config);
		win.show();
	},
	//获取数据
	getDataAll:function(callback){
		var me=this;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
	    var startdatecom = buttonsToolbar.getComponent('startdate');
	    var enddatecom=buttonsToolbar.getComponent('enddate');
		strval=	startdatecom.getValue();	 
		endval=	enddatecom.getValue();
		//最大结束时间
		var Start =JcallShell.Date.getNextDate(strval,182.5);
	    if(endval>Start){
	    	JShell.Msg.alert('只能查半年内的数据!', null, 2000);
	    	return;
	    }
	    if(endval<strval){
	    	JShell.Msg.alert('结束时间不能小于开始时间!', null, 2000);
	    	return;
	    }
	    
		var url = JShell.System.Path.ROOT + me.selectUrl;
		var whereParams = me.getSearchWhereParams();
		url += (url.indexOf('?') == -1 ? '?' : '&') ;
		if(whereParams) {
			url += whereParams;
		}
		url += "&fields="+me.getFields();
	
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});