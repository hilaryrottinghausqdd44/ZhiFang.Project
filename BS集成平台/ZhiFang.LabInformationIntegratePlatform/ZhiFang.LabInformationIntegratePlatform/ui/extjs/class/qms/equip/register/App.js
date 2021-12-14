/**
 * 质量数据登记
 * @author liangyl
 * @version 2018-10-24
 */
Ext.define('Shell.class.qms.equip.register.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '质量数据登记',
	width: 1000,
	height: 800,
	//模板Id
	TempletID: '',
	//模板项目代码
	TempletTypeCode: '',
	selectUrl: '/QMSReport.svc/QMS_UDTO_PreviewPdf',
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:true,
    //质量记录登记页面保存数据后是否直接预览
    IsSaveDataPreview:'0',
    hideTimes:2000,
  	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//质量记录登记页面保存数据后是否直接预览
		me.IsSaveDataPreview = me.OperateGrid.IsSaveDataPreview;
		//备注
		var MemoForm = me.OperateGrid.getComponent('MemoTabPanel').getComponent('Form');
		//组件表单
		var EMaintenanceDataForm = me.OperateGrid.EditTabPanel.Form;
		//组件表单工具栏上的时间
		var EMaintenanceDataDate = EMaintenanceDataForm.getComponent('buttonsToolbar').getComponent('EMaintenanceData_Date');
		me.SimpleGrid.on({
			itemclick: function(v, record) {
				me.onSelect(record);
			},
			select: function(RowModel, record) {
				me.onSelect(record);
			},
			nodata:function(p){
				me.OperateGrid.clearData();
			}
			
		});
		me.OperateGrid.on({
			onShowClick: function() {
				var records = me.SimpleGrid.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var url = me.getUrl(records[0]);
				me.openForm(me.TempletID, url);
			},
			save:function(com){
				var records = me.SimpleGrid.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
			    me.OperateGrid.getTempletState(function(data){
					if(data){
						var IsFillData =data.ETemplet_IsFillData;
						records[0].set('ETemplet_IsFillData',IsFillData);
					}
				});
				if(me.IsSaveDataPreview=='1'){
					var url = me.getUrl(records[0]);
				    me.openForm(me.TempletID, url);
				}
			},
			onDelClick:function(){
				var records = me.SimpleGrid.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
			    me.OperateGrid.getTempletState(function(data){
					if(data){
						var IsFillData =data.ETemplet_IsFillData;
						records[0].set('ETemplet_IsFillData',IsFillData);
					}
				});
			}
		});
	},
	onSelect :function(record){
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get("ETemplet_Id");
			//填充类型
			var FillType = record.get("ETemplet_FillType");
			me.OperateGrid.changeTab(id,FillType);
			me.TempletID = id;
		}, null, 200);
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "模板日常维护";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/***/
	createItems: function() {
		var me = this;
		var TempletPanel='Shell.class.qms.equip.register.SimpleGrid';
		me.SimpleGrid = Ext.create(TempletPanel, {
			border: true,
			title: '模板列表',
			region: 'west',
			width: 420,
			header: false,
		    //从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
            ISEDITDATE:me.ISEDITDATE,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			name: 'SimpleGrid',
			itemId: 'SimpleGrid'
		});
		me.OperateGrid = Ext.create('Shell.class.qms.equip.register.AddPanel', {
			border: true,
			title: '操作列表',
			region: 'center',
			header: false,
			ISEDITDATE:me.ISEDITDATE,
			itemId: 'OperateGrid'
		});
		return [me.SimpleGrid, me.StructGrid, me.OperateGrid];
	},
     //获得本季度的开始月份 
	getQuarterStartMonth : function (moth){ 
		var quarterStartMonth = 0; 
		if(moth<3) quarterStartMonth = 0; 
		if(2<moth && moth<6)quarterStartMonth = 3; 
		if(5<moth && moth<9)quarterStartMonth = 6; 
		if(moth>8)quarterStartMonth = 9; 
		return quarterStartMonth; 
	}, 
	//获得本季度的开始日期 
	getQuarterStartDate :function (year,moth){ 
		var me = this;
		var quarterStartDate = new Date(year, me.getQuarterStartMonth(moth), 1); 
		return JShell.Date.toString(quarterStartDate,true);
	},
    //获得某月的天数 
	getMonthDays:function (year,moth){ 
		var monthStartDate = new Date(year, moth, 1); 
		var monthEndDate = new Date(year, moth + 1, 1); 
		var days = (monthEndDate - monthStartDate)/(1000 * 60 * 60 * 24); 
		return days; 
	} ,
    //获得本季度的结束日期 
    getQuarterEndDate : function (year,moth){ 
    	var me = this;
		var quarterEndMonth = me.getQuarterStartMonth(year,moth) + 2; 
		var quarterStartDate = new Date(year, quarterEndMonth, me.getMonthDays(year,quarterEndMonth));
		return JShell.Date.toString(quarterStartDate,true); 
	},
	/**预览仪器质量记录PDF文件查询条件
	 * FillType 根据填充方式改变查询条件开始时间和结束时间,无类型和按日一样
	 * */
	getSearchWhere: function(rec,Sysdate) {
		var me = this,
			whereParams = "",
			EndDate = "",
			params = [];
	    
		var FillType=rec.get('ETemplet_FillType');
		var startDate=null,endDate=null;
		//当前选择时间
		var beginDate = JcallShell.Date.toString(Sysdate, true);
		
		var beginDateStr = JcallShell.Date.toString(Sysdate, true);
		var b = beginDateStr.split("-");
		var month = b[1];
        var year = b[0]; //当前年 		
		switch (FillType){
			case '1'://按日填写
			    startDate=beginDate;
			    endDate = beginDate;
				break;
			case '2'://按月填写
				var startDateStr= JcallShell.Date.getMonthFirstDate(year, month, true);
				startDate=JcallShell.Date.toString(startDateStr, true);
				var endDateStr = JcallShell.Date.getMonthLastDate(year, month, true);
				endDate=JcallShell.Date.toString(endDateStr, true);
				break;
			case '3'://按年填写
			    var beginDateStr = JcallShell.Date.toString(Sysdate, true);
				var b = beginDateStr.split("-");
				startDate = year+'-01-01';
			    endDate = year+'-12-31';
				break;
			case '10'://按周填写
			    //本月第几周
			    var weekTims =JShell.Date.getMonthWeekByDate(beginDate); 
				//获取某年某月某一星期的日期范围
			    var  weekdates = JShell.Date.getWeekStartDateAndEndDate(year, month,weekTims);
			    startDate = weekdates.StartDate;
			    endDate = weekdates.EndDate;
				break;
			case '11'://按季填写
			    startDate = me.getQuarterStartDate(year, month);
			    endDate =  me.getQuarterEndDate(year, month);
				break;
			case '12'://按半年填写
			    var startMonth = 0;
	            var endMonth = 0;
	            if(month < 7){//上半年
	            	startDate = year+'-01-01';
			        endDate = year+'-06-30';
	            }else{//下半年
                    startDate = year+'-07-01';
			        endDate = year+'-12-31'; 
	            }
				break;	
			default://无类型
			    startDate=beginDate;
			    endDate = beginDate;
				break;
		}
		if(Date) {
			params.push("&endDate=" + endDate);
			params.push("&beginDate=" + startDate);
		}
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
	/**预览仪器质量记录PDF文件URL*/
	getUrl: function(rec) {
		var me = this;
		var TempletID = rec.get('ETemplet_Id');
		var ReportName = rec.get('ETemplet_CName');
		var url = JShell.System.Path.ROOT + '/QMSReport.svc/QMS_UDTO_PreviewPdf';
		var whereParams = me.getSearchWhere(rec,me.OperateGrid.getOperateDate());
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'templetID=' + TempletID + '&operateType=1&isCheckPreview=0';
		if(whereParams) {
			url += whereParams;
		}
		if(ReportName) {
			url += "&reportName=" + ReportName;
		}
		return url;
	},
	/**打开预览窗口*/
	openForm: function(TempletID, url) {
		var me = this;
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		var config = {
			width: maxWidth,
			height: height,
			ETempletId: TempletID,
			URL: url,
			title: '预览仪器模板PDF文件',
			hasColse: true,
			hasSave: false,
			resizable: false, //可变大小功能
			hasBtntoolbar: true,
			listeners: {
				save: function(win) {
					me.Grid.onSearch();
					win.hide();
				},
				onSaveClick: function(win) {
					me.fireEvent('onSaveClick', win);
				}
			}
		};
		JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.PreviewApp', config).show();
	}

});