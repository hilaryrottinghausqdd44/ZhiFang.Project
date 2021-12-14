/**
 * 审核基本列表
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.basic.BasicGrid', {
	extend: 'Shell.class.wfm.business.invoice.basic.Grid',
	title: '发票审核列表',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceByExportType?isPlanish=true',
	/**默认加载数据*/
	defaultLoad: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	PInvoiceMsg: '发票一审',
	defaultStatusValue: '2',
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: true,

	/**通过状态*/
	adoptStatus: 3,
	/**一审打回*/
	noadoptStatus: 4,
	/**可编辑状态*/
	Status: 2,
	DigSaveText: '退回',
	SaveText: '审核通过',
	hasSave: true,
	hasDisSave: true,
	ExportType: 1,
	SUB_WIN_NO:'2',
	/**是否是管理员,不是管理员ISADMIN==0  是管理员ISADMIN=1*/
	ISADMIN: 1,
	IsBasic:false,
	defaultWhere : 'pinvoice.IsUse=1 and pinvoice.Status!=1',
    /**付款单位*/
    PClientClassName:'Shell.class.wfm.client.CheckGrid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		//初始化检索监听
		me.initFilterListeners();
	},
	paramsFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		var StatusID = buttonsToolbar.getComponent('StatusID');

		//发票状态
		if(StatusID) {
			StatusID.on({
				change: function() {
					me.onSearch();
				}
			});
		}
        var PClientName = buttonsToolbar.getComponent('PClientName'),
			PClientID = buttonsToolbar.getComponent('PClientID');
		if(PClientName) {
			PClientName.on({
				check: function(p, record) {
					if(me.PClientClassName=='Shell.class.wfm.client.CheckGrid'){
						PClientName.setValue(record ? record.get('PClient_Name') : '');
					    PClientID.setValue(record ? record.get('PClient_Id') : '');
					}else{
						PClientName.setValue(record ? record.get('PSalesManClientLink_PClientName') : '');
					    PClientID.setValue(record ? record.get('PSalesManClientLink_PClientID') : '');
					}
					me.onGridSearch();
					p.close();
				},
				change: function() {
//					 me.onSearch();
				}
			});
		}
		
		//人员类型+人员
		var UserType = buttonsToolbar.getComponent('UserType'),
			UserName = buttonsToolbar.getComponent('UserName'),
			UserID = buttonsToolbar.getComponent('UserID');
		if(UserType) {
			UserType.on({
				change: function() {
					if(UserID.getValue()) {
						me.onGridSearch();
					}
				}
			});
		}
		if(UserName) {
			UserName.on({
				check: function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change: function() {
					if(UserType.getValue() && UserID.getValue()) {
						me.onGridSearch();
					}
				}
			});
		}
		//时间类型+时间

		var EndDate = buttonsToolbar.getComponent('EndDate');
		var BeginDate = buttonsToolbar.getComponent('BeginDate');
		var DateType = buttonsToolbar.getComponent('DateType');
		if(DateType) {
			DateType.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.onGridSearch();
					}
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.onSearch();
					}
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.onSearch();
					}
				}
			});
		}
	},
	/**发票修改*/
	openEditForm: function(id, ContractID) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.basic.ShowTabPanel', {
			resizable: true,
			PK: id,
			title:'商务经理审核',
			PInvoiceMsg: me.PInvoiceMsg,
			DigSaveText: me.DigSaveText,
			SaveText: me.SaveText,
			hasSave: me.hasSave,
			hasDisSave: me.hasDisSave,
			formtype: 'show',
			ContractID: ContractID,
			VAT: me.VAT,
			SUB_WIN_NO:me.SUB_WIN_NO,
			listeners: {
				onSaveClick: function(p) {
					me.onSave(id, me.adoptStatus, p);
				},
				onDigSaveClick: function(p) {
					me.onSave(id, me.noadoptStatus, p);
				},
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**保存按钮点击处理方法*/
	onSave: function(id, Status, p) {
		var me = this;
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">处理意见</div>',
			msg: '',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;
			me.onSaveClick(id, Status, text, p);
		});
	},
	onSaveClick: function(id, Status, text, p) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var entity = {
			Status: Status,
			OperationMemo: text,
			ReviewInfo: text,
			Id: id
		};
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		if(ReviewDateStr) {
			entity.ReviewDate = ReviewDateStr;
		}
		var fields = 'Id,Status,InvoiceDate';
		var params = {
			entity: entity,
			fields: fields
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', p);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.push({
			text: '状态',
			dataIndex: 'PInvoice_Status',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer:function(value,meta){
            	var v = value || '';
            	if(v){
            		var info = JShell.System.ClassDict.getClassInfoById('PInvoiceStatus',v);
            		if(info){
            			v = info.Name;
            			meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
            		}
            	}
            	return v;
           }
		}, {
			text: '开票金额',
			dataIndex: 'PInvoice_InvoiceAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '要求开票时间',
			dataIndex: 'PInvoice_InvoiceDate',
			width: 130,
			isDate: true,
			hasTime: true
		});
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			params = [];
		if(!buttonsToolbar) {
			return;
		}
		var EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			DateType = buttonsToolbar.getComponent('DateType').getValue(),
			StatusID = buttonsToolbar.getComponent('StatusID').getValue(),
			UserType = buttonsToolbar.getComponent('UserType').getValue(),
			UserID = buttonsToolbar.getComponent('UserID').getValue(),
			PClientID = buttonsToolbar.getComponent('PClientID').getValue();
		var EndDate2 = JcallShell.Date.toString(EndDate, true),
			BeginDate2 = JcallShell.Date.toString(BeginDate, true);
		//时间
		if(DateType) {
			if(BeginDate2 && EndDate2) {
				params.push("pinvoice." + DateType + " between '" + BeginDate2 + ' 00:00:00 ' + "' and '" + EndDate2 + " 23:59:59'");
			}
		}
		//付款单位
		if(PClientID) {
			params.push("pinvoice.PayOrgID='" + PClientID + "'");
		}
		//状态
		if(StatusID) {
			params.push("pinvoice.Status=" + StatusID + "");
		}
		//员工
		if(UserType && UserID) {
			params.push("pinvoice." + UserType + "='" + UserID + "'");
		}
			//默认员工ID
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(me.ISADMIN == 0) {
			params.push("pinvoice.ApplyManID=" + userId);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**获取状态列表*/
	getStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			if(obj.Id != 1) {
				var style = ['font-weight:bold;text-align:center'];
				if(obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
			}
		}
		return data;
	},
		/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PInvoiceStatus',function(){
			if(!JShell.System.ClassDict.PInvoiceStatus){
    			JShell.Msg.error('未获取到发票状态，请刷新列表');
    			return;
    		}
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PInvoiceStatus;
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			me.load(null, true, autoSelect);
    	});
	}
});