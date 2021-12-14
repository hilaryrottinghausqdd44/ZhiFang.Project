/**
 * 质量数据登记
 * @author liangyl
 * @version 2016-08-26
 */
Ext.define('Shell.class.qms.equip.emaintenancedata.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '质量数据登记',
	width: 1000,
	height: 800,
	layout:'border',
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:true,
    /**用于职责模板,'0', '全部','1', '人员模板','2', '人员岗位模板',对外公开设置默认值*/
    TEMPTLETTYPE:'',
    TempletID:null,
    //审核类型,1按天审核，0按月审核
	CheckType:'0',
	/*选择行（模板）*/
	simpleGridRec:null,
	/**批号*/
	TempletBatNo:null,
	//RecordGrid列表是否显示
	ShowFillItem:false,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		 //时间改变时	
		me.AddPanel.on({
			loadSaveDataClick:function(){
				//IsAdd 状态判断，不是新增状态
				me.AddPanel.IsAdd=false;
			},
			blur:function(com){
				var stratDate = me.AddPanel.getOperateDate();
				stratDate = JShell.Date.toString(stratDate, true);
				if(me.ShowFillItem)me.RecordGrid.searchData(me.TempletID,stratDate);
			},
			onShowClick: function() {
				if(!me.simpleGridRec) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var url = me.getUrl(me.simpleGridRec);
				me.openForm(me.TempletID, url);
			},
			save:function(com,IsTbType){
				me.fireEvent('save');	
			},
			onDelClick:function(){
				me.fireEvent('del');
			}
		});
		me.RecordGrid.on({
			onAddClick:function(grid){
				var rec = me.RecordGrid.getGridSelect();
				var TempletBatNo = rec.get('TempletBatNo');
//				me.TempletBatNo =TempletBatNo;
		        me.AddPanel.changeDelBatNo();
				//新增,中间面板清空
				me.AddPanel.isAdd(null);
			},
			itemclick: function(grid, record, item, index, e, eOpts) {
                var record = me.RecordGrid.getGridSelect();
				var TempletID = record.get('TempletID');
				var TempletBatNo = record.get('TempletBatNo');
				if(!me.simpleGridRec)return;
				me.onSelect(me.simpleGridRec,TempletBatNo);
			},
			selectclick: function(RowModel, record) {
                var record = me.RecordGrid.getGridSelect();
                var TempletID = record.get('TempletID');
				var TempletBatNo = record.get('TempletBatNo');
				if(!me.simpleGridRec)return;
				me.onSelect(me.simpleGridRec,TempletBatNo);
			},
			nodata: function(p) {
				if(me.ShowFillItem)me.fireEvent('nodata');	
			}
		});
	},
	onSelect :function(record,TempletBatNo){
		var me = this;
		me.TempletBatNo =TempletBatNo;
		//IsAdd 状态判断，不是新增状态
		me.AddPanel.IsAdd=false;
		JShell.Action.delay(function() {
			var id = record.get("ETemplet_Id");
			me.TempletID = id;
			//审核类型,1按天审核，0按月审核
			var CheckType = record.get("ETemplet_CheckType");
			me.AddPanel.changeTab(id,CheckType,TempletBatNo);
		}, null, 200);
	},
	searchData:function(TempletID,record){
		var me = this;
		me.TempletID=TempletID;
		var CheckType = record.get("ETemplet_CheckType");
		me.CheckType = CheckType;
		//获取时间
		var stratDate=me.AddPanel.getOperateDate();
		me.RecordGrid.searchData(TempletID,stratDate);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save','del','nodata');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.AddPanel = Ext.create('Shell.class.qms.equip.emaintenancedata.AddPanel', {
			border: false,
			region: 'center',
			header: false,
			TempletID: me.TempletID,
			itemId: 'AddPanel',
			ISEDITDATE:me.ISEDITDATE,
			TEMPTLETTYPE:me.TEMPTLETTYPE
		});
		me.RecordGrid = Ext.create('Shell.class.qms.equip.emaintenancedata.RecordGrid', {
			border: true,
			title: '模板多次记录数据',
			region: 'east',
			width: 220,
			header: false,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			name: 'RecordGrid',
			itemId: 'RecordGrid'
		});
		return [me.AddPanel,me.RecordGrid];
	},
	clearData:function(){
		var me = this;
		me.TempletBatNo='';
	},
	/**预览仪器质量记录PDF文件URL*/
	getUrl: function(rec) {
		var me = this;
		var TempletID = rec.get('ETemplet_Id');
		var ReportName = rec.get('ETemplet_CName');
		var url = JShell.System.Path.ROOT + '/QMSReport.svc/QMS_UDTO_PreviewPdf';
		var whereParams = me.getSearchWhere(rec,me.AddPanel.getOperateDate());
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'templetID=' + TempletID + '&operateType=1&isCheckPreview=0';
        if(me.TempletBatNo)url +='&templetBatNo='+me.TempletBatNo;
		if(whereParams) {
			url += whereParams;
		}
		if(ReportName) {
			url += "&reportName=" + ReportName;
		}
		return url;
	},
	/**预览仪器质量记录PDF文件查询条件*/
	getSearchWhere: function(rec,Sysdate) {
		var me = this,
			whereParams = "",
			EndDate = "",
			params = [];
		var CheckType=rec.get('ETemplet_CheckType');
		var startDate=null,endDate=null;
		if(CheckType=='1'){//按天审核
			var beginDate = JcallShell.Date.toString(Sysdate, true);
            startDate=beginDate;
            endDate=startDate;
		}else{
			var beginDateStr = JcallShell.Date.toString(Sysdate, true);
			var b = beginDateStr.split("-");
			var startDateStr= JcallShell.Date.getMonthFirstDate(b[0], b[1], true);
			startDate=JcallShell.Date.toString(startDateStr, true);
			var endDateStr = JcallShell.Date.getMonthLastDate(b[0], b[1], true);
			endDate=JcallShell.Date.toString(endDateStr, true);
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
	},
	//ShowFillItem 不显示列表
	isShowGrid : function(bo){
		var me = this;
		me.ShowFillItem = bo;
		me.RecordGrid.setVisible(bo);
	},
	isShowDailyBtn:function(val){
		var me = this;
		me.AddPanel.isShowDailyBtn(val);
	},
	changeShowDailyBtn:function(){
		var me = this;
		me.AddPanel.changeShowDailyBtn();
	},
	getIsSaveDataPreview : function(){
		var me = this;
		return me.AddPanel.IsSaveDataPreview;
	},
	//保存成功后回调
	saveCallBack:function(grid,record){
		var me = this;
		me.AddPanel.IsAdd =false;
		//状态改变后，设置模板列表颜色 
		me.getTempletState(grid,record);
		
		if(me.ShowFillItem){//显示列表
			me.RecordGrid.load();
		}else{
			me.AddPanel.showMask();
			me.AddPanel.loadDataByBtn();
		}
		if(me.getIsSaveDataPreview()=='1'){
			var url = me.getUrl(record);
		    me.openForm(me.TempletID, url);
		}
	},
	//RecordGrid 数据被清空时
	gridNoData : function(grid,rec){
		var me =this;
		me.TempletBatNo = '';
		if(!rec)return;
		me.onSelect(rec);
		//状态改变后，设置模板列表颜色 
		me.getTempletState(grid,rec);
	},
	//状态改变后，设置模板列表颜色 
	getTempletState : function(grid,rec){
		var me =  this;
		//返回状态
		me.AddPanel.getTempletState(function(data){
			if(data){
				var IsFillData = data.ETemplet_IsFillData;
				grid.store.each(function(record) {
					if(record.data.ETemplet_Id ==rec.data.ETemplet_Id){
						record.set('ETemplet_IsFillData', IsFillData);
						grid.getView().refresh();
						return false; 
					}
		        });
			}	
		});
	}
});