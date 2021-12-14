/**
 * 质量记录查询
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.SeachGrid', {
	extend: 'Shell.class.qms.equip.templet.ereportdata.Grid',
	title: '质量记录',
    /**是否启用序号列*/
	hasRownumberer: true,
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchEReportDataByHQL?isPlanish=true',
	/**主键列*/
	PKField: 'EReportData_Id',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'EReportData_ReportName',
		direction: 'ASC'
	}],
	hasStatus: false,
	defaultWhere:'ereportdata.IsUse=1', 
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//		me.loadData();
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			text: '仪器模板Id',
			dataIndex: 'EReportData_ETemplet_Id',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '质量记录名称',
			dataIndex: 'EReportData_ReportName',
			minWidth: 270,flex:1,
			sortable: true,
			defaultRenderer: true
		},{
			text: '代码',dataIndex: 'EReportData_ETemplet_UseCode',
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		},{
			text: '日期',dataIndex: 'EReportData_ReportDate',isDate:true,
			width: 80,sortable: true,menuDisabled: true,defaultRenderer: true
		},{
			text: '填写类型',dataIndex: 'EReportData_ETemplet_CheckType',hidden:false,
			width: 70,sortable: true,menuDisabled: true,
			align: 'center',
			renderer : function(value, meta) {
				var v = value + '';
				if (v == '1') {
					v ='按天填写';
				} else if (v == '0') {
					v = '按月填写';
				} else {
					v == '';
				}
				return v;
			}
		}, {
			text: '审核状态',
			dataIndex: 'EReportData_IsCheck',
			width: 60,
			sortable: true,
			menuDisabled: true,//align: 'center',
			renderer: function(value, meta) {
				var v = value;
				if(value == '1') {
					meta.style = 'color:green';
					v = '已审';
				} else {
					v = '未审';
					meta.style = 'color:red';
				}
				return v;
			}
		},{
			text: '仪器名称',
			dataIndex: 'EReportData_ETemplet_EEquip_CName',
			minWidth: 100,flex:1,
			sortable: true,
			defaultRenderer: true
		},  {
			text: '仪器类型',
			dataIndex: 'EReportData_ETemplet_EEquip_EquipType_CName',
			width: 120,hidden:true,
			sortable: true,
			defaultRenderer: true
		},{
			text: '小组名称',
			dataIndex: 'EReportData_ETemplet_Section_CName',
			width: 150,
			sortable: true,
			defaultRenderer: true
		},{
			text: 'Id',
			dataIndex: 'EReportData_Id',
			width: 100,
			hidden: true,
			sortable: false,
			isKey: true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '打印',
			align: 'center',
			width: 40,
			tooltip: '打印',
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-print hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					me.fireEvent('onPrintClick', grid, rowIndex);

					var rec = grid.getStore().getAt(rowIndex);
					me.openForm(rowIndex);
//					//已审
//					var PreviewCheckPdfUrl = me.getPreviewCheckUrl(records[0]);
//					me.openForm(true, false, PreviewCheckPdfUrl);

				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '浏览',
			tooltip: '浏览',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var rec = grid.getStore().getAt(rowIndex);
					me.openForm(rowIndex);
				}
			}]
		},{
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
					var TempletID = rec.get('EReportData_ETemplet_Id');
					var ReportDate = rec.get('EReportData_ReportDate');
					var b = ReportDate.split("/");
					var beginDate = b[0] + '-' + b[1] + '-01';
					var beginDate= JcallShell.Date.toString(ReportDate,true);
					var endDate = JcallShell.Date.getMonthLastDate(b[0], b[1], true);
					var endDate2=JShell.Date.getNextDate(endDate,1);
					var endDate3=JShell.Date.toString(endDate2,true);
					var id = rec.get('EReportData_Id');
					var CheckType = rec.get('EReportData_ETemplet_CheckType');
					//按天审核
					if(CheckType=='1'){
						beginDate = JShell.Date.toString(ReportDate,true);
						endDate3 = JShell.Date.toString(JShell.Date.getNextDate(beginDate,1),true);
					}
					me.showAttachmentById(id, TempletID, beginDate, endDate3);
				}
			}]
		},  {
			text: '审核人',
			dataIndex: 'EReportData_Checker',
			width: 90,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '审核时间',
			dataIndex: 'EReportData_CheckTime',
			width: 85,
			sortable: true,
			isDate: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '审核意见',
			dataIndex: 'EReportData_CheckView',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '描述',
			dataIndex: 'EReportData_Comment',
			width: 150,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '附件标志',
			dataIndex: 'EReportData_IsAttachment',width: 100,hidden: true,sortable: false,menuDisabled: false,
			defaultRenderer: true
		},{
			text: 'ReportDate',dataIndex: 'ReportDate',hidden:true,
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		},{
			text: '批号',hidden:true,
			dataIndex: 'EReportData_TempletBatNo',width: 100,sortable: false,menuDisabled: false,
			defaultRenderer: true
		},{
			text: '类型',dataIndex: 'EReportData_ETemplet_TempletType_CName',
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		}];
		return columns;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			datearea = buttonsToolbar.getComponent('datearea').getValue(),
			search = buttonsToolbar.getComponent('search').getValue(),
			TempletType = buttonsToolbar.getComponent('TempletType_Id').getValue(),
			params = [];

		var BeginDate = JShell.Date.toString(datearea.start,true),
			EndDate = JShell.Date.toString(datearea.end,true);
		if(BeginDate && EndDate) {
			params.push("ereportdata.ReportDate between '" + BeginDate + "' and '" + EndDate + "'");
		}
		if(TempletType) {
			params.push("ereportdata.ETemplet.TempletType.Id = " + TempletType);

		}
		if(search) {
			params.push('(' + "ereportdata.ReportName like '%" + search + "%'" + ')');
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}

		return me.callParent(arguments);
	},
	/**浏览已审核仪器质量记录PDF文件URL*/
	getPreviewCheckUrl: function(rec) {
		var me = this;
		var ReportDataID = rec.get('EReportData_Id');

		var url = JShell.System.Path.ROOT + '/QMSReport.svc/QMS_UDTO_PreviewCheckPdf';
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'reportDataID=' + ReportDataID + '&operateType=1';
		var reportName = rec.get('EReportData_ReportName');
		if(reportName) {
			url += '&reportName=' + reportName;
		}
		return url;
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
					me.onSearch();
				},
				onSaveClick: function(win) {
					me.fireEvent('onSaveClick', win);
				}
			}
		};
		var win =JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.CheckApp', config);
		win.show();
	},
	/**获取当前页数据
	 * 根据选择行的id 找到行位置，截取行之后的数据并返回
	 * */
	getCurrentPageData:function(){
		var me = this;
		var records = me.store.data.items,
			len = records.length;
		var list=[];
		for(var i=0;i<len;i++){
			var obj ={
				EReportData_ReportDate:records[i].data.EReportData_ReportDate,
				EReportData_TempletID:records[i].data.EReportData_ETemplet_Id,
				EReportData_ReportName:records[i].data.EReportData_ReportName,
				EReportData_IsCheck:records[i].data.EReportData_IsCheck,
				EReportData_ETemplet_CheckType:records[i].data.EReportData_ETemplet_CheckType,
				ReportDate:records[i].data.ReportDate,
				EReportData_ReportDataID:records[i].data.EReportData_Id,
				EReportData_TempletBatNo:records[i].data.EReportData_TempletBatNo
			}
	     	list.push(obj);
		}
		return list;
	},
		 /**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
		for(var i=0;i<data.list.length;i++){
			var ReportDate=data.list[i].EReportData_ReportDate;
			var obj1={
				ReportDate:ReportDate
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return result;
	}
});