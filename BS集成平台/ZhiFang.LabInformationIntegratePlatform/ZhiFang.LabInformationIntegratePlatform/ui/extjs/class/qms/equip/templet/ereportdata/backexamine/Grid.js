/**
 * 质量记录反审
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.backexamine.Grid', {
	extend: 'Shell.class.qms.equip.templet.ereportdata.Grid',
	title: '质量记录反审',
    /**是否启用序号列*/
	hasRownumberer: true,
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchEReportDataByHQL?isPlanish=true',
	/**反审质量记录路径*/
	UNCheckReportUrl: '/QMSReport.svc/QMS_UDTO_CheckReportCancel?isPlanish=true',

	/**主键列*/
	PKField: 'EReportData_Id',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'EReportData_ReportName',
		direction: 'ASC'
	}],
	hasStatus: false,
	
	defaultWhere:'ereportdata.IsUse=1', 
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			onSaveClick: function(panel) {
				var msg = "确定要反审吗";
				JShell.Msg.confirm({
					msg:msg
				}, function(but) {
					if (but != "ok") return;
				    me.onSave(panel);
				});
			},
			save: function(win) {
				win.hide();
				me.onSearch();
			}
		});
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
		},{
			text: '质量记录名称',
			dataIndex: 'EReportData_ReportName',
			minWidth: 270,flex:1,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '代码',dataIndex: 'EReportData_ETemplet_UseCode',
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		},  {
			text: '日期',dataIndex: 'EReportData_ReportDate',isDate:true,
			width: 80,sortable: true,menuDisabled: true,defaultRenderer: true
		}, {
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
			text: '小组代码',
			dataIndex: 'EReportData_ETemplet_UseCode',
			width: 100,
			hidden:true,
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
			text: '反审',
			tooltip: '反审',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-uncheck hand';
				},
				handler: function(grid, rowIndex, colIndex) {
 					me.getSelectionModel().select(rowIndex);
					var records = me.getSelectionModel().getSelection();
					if(records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					//已审
					var PreviewCheckPdfUrl = me.getPreviewCheckUrl(records[0]);
					me.openForm(false, true, PreviewCheckPdfUrl);
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
					var beginDate= JcallShell.Date.getMonthFirstDate(b[0], b[1], true);
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
		} ,{
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
	openForm: function(hasColse, hasSave, url) {
		var me = this;
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		var config = {
			width: maxWidth,
			height: height,
			hasColse: hasColse,
			hasSave: hasSave,
			checkviewLable:'反审原因',
			btnsaveText:'反审',
			URL: url,
			listeners: {
				save: function(win) {
					me.Grid.onSearch();
					win.close();
				},
				onSaveClick: function(win) {
					me.fireEvent('onSaveClick', win);
				}
			}
		};
		if(!hasColse){
			config.SUB_WIN_NO = '1';
		}
		JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.PreviewApp', config).show();
	},
	/**
	 * 审核功能
	 * 
	 * */
	onSave: function(panel) {
		var me=this;
		//审核
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var reportID = records[0].get('EReportData_Id');
		me.getCheckReport(reportID,  panel);
	},
	/**审核质量记录*/
	getCheckReport: function(reportID, panel) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.UNCheckReportUrl);
		url += '&reportID=' + reportID;
		var checkview=panel.getComponent('bottombuttonsToolbar').getComponent('checkview').getValue();
		if(checkview){
			url += "&cancelCheckView=" + checkview;
		}
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.fireEvent('save', panel);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false, 500, false);
	}
});