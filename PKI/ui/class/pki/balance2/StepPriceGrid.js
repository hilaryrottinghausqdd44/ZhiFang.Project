/**
 * 阶梯价格计算
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.StepPriceGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '阶梯价格计算 ',

	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_CalcStepPrice?isPlanish=true',
	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_ReportStepPriceToExcel',
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,

	/**默认选中送检时间*/
	isDateRadio: false,
	/***正在计算中,请稍候...*/
	loadingText:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.getComponent('filterToolbar').on({
			search: function(p, params) {
				me.params=params;
				Ext.Msg.wait("正在计算中,请稍候...", '提示');
				me.onSearch();
			}
		});
		me.store.on({
			load: function(store, records, successful) {
				me.onAfterLoad(records, successful);
				Ext.Msg.hide();
			}
		});
		me.on({
			itemdblclick: function(view, record) {
				me.showInfo(record);
			}
		});
	},

	initComponent: function() {
		var me = this;

		//数据列
		me.columns = me.createGridColumns();

		//创建挂靠功能栏
		var config = me.createSearchToolbarConfig();
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.balance2.StepPriceToolbar', Ext.apply(config, {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			height: 55
		}))];

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'DDealerCalcPrice_DealerName',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_ItemName',
			text: '检验项目',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_AllItemCount',
			text: '样本数量合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_PersonalCount',
			text: '个人样本数量合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_FreeCount',
			text: '免单样本数量合计',
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_HospitalCount',
			text: '医院样本数量合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_CalcStepPriceCount',
			text: '阶梯样本数合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_StepPriceCount',
			text: '采用阶梯价格样本数合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_StepPrice',
			text: '阶梯价格',
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerCalcPrice_StepPriceSum',
			text: '阶梯价合计',
			width: 110,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			sortable: false,
			text: '明细',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'button-show hand',
				tooltip: '查看明细',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.showInfo(rec);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			sortable: false,
			text: '下载',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'file-excel hand',
				tooltip: '导出EXCEL文件',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.doActionClick = true;
					me.onDownLoadExcel(rec);
				}
			}]
		}, {
			dataIndex: 'DDealerCalcPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'DDealerCalcPrice_DealerID',
			text: '经销商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DDealerCalcPrice_ItemID',
			text: '项目ID',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**创建查询栏参数*/
	createSearchToolbarConfig: function() {
		var config = {
			/**对账状态列表*/
			IsLockedList: [
				['1', '待对账', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E1'] + ';'],
				['4', '待对账+销售锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E4'] + ';']
			],
			defaultIsLockedValue: '1',
			/**创建内部组件*/
			createItems: function() {
				var me = this,
					items = [];

				//时间条件
				items.push({
					x: 5,
					y: 5,
					width: 170,
					fieldLabel: '时间类型',
					xtype: 'uxSimpleComboBox',
					itemId: 'DateType',
					hasStyle: true,
					value: me.defaultDateTypeValue,
					data: me.getDateTypeList()
				}, {
					x: 185,
					y: 5,
					width: 50,
					itemId: 'radio2',
					boxLabel: '年月',
					xtype: 'radio',
					name: me.getId() + 'radioG1',
					checked: !me.isDateRadio
				}, {
					x: 235,
					y: 5,
					width: 95,
					xtype: 'uxYearComboBox',
					itemId: 'YearMonth',
					value: me.YearMonth,
					disabled: me.isDateRadio
				}, {
					x: 330,
					y: 5,
					width: 95,
					xtype: 'uxMonthComboBox',
					itemId: 'MonthMonth',
					value: me.MonthMonth,
					disabled: me.isDateRadio,
					margin: '0 2px 0 10px'
				}, {
					x: 445,
					y: 5,
					width: 50,
					itemId: 'radio1',
					boxLabel: '日期',
					xtype: 'radio',
					name: me.getId() + 'radioG1',
					checked: me.isDateRadio
				}, {
					x: 495,
					y: 5,
					width: 95,
					itemId: 'BeginDate',
					xtype: 'datefield',
					format: 'Y-m-d',
					value: me.BeginDate,
					disabled: !me.isDateRadio
				}, {
					x: 590,
					y: 5,
					width: 105,
					labelWidth: 5,
					fieldLabel: '-',
					labelSeparator: '',
					itemId: 'EndDate',
					xtype: 'datefield',
					format: 'Y-m-d',
					value: me.EndDate,
					disabled: !me.isDateRadio
				});

				//检验项目【勾选列表】
				items.push({
					x: 5,
					y: 30,
					fieldLabel: '检验项目',
					itemId: 'TestItem_CName',
					xtype: 'uxCheckTrigger',
					className: 'Shell.class.pki.item.CheckGrid'
				}, {
					fieldLabel: '检验项目主键ID',
					itemId: 'TestItem_Id',
					hidden: true
				});

				//经销商【勾选列表】
				items.push({
					x: 205,
					y: 30,
					width: 190,
					labelWidth: 50,
					fieldLabel: '经销商',
					itemId: 'Dealer_Name',
					xtype: 'uxCheckTrigger',
					className: 'Shell.class.pki.balance2.CheckTree'
				}, {
					fieldLabel: '经销商主键ID',
					itemId: 'Dealer_Id',
					hidden: true
				});

                //操作
				items.push({
					x: 400,
					y: 30,
					width: 60,
				    iconCls: 'button-cancel',
					margin: '0 0 0 10px',
					xtype: 'button',
					text: '清空',
					tooltip: '<b>清空查询条件</b>',
					handler: function() {
						me.onClearSearch();
					}
				});

				//操作
				items.push({
					x: 460,
					y: 30,
					width: 60,
					iconCls: 'button-search',
					margin: '0 0 0 10px',
					xtype: 'button',
					text: '计算',
					tooltip: '<b>根据条件计算阶梯价格</b>',
					handler: function() {
						me.onFilterSearch();
					}
				});

				return items;
			}
		};

		return config;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		me.params =me.params|| me.getComponent('filterToolbar').getParams();

		var arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true);

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		if(where) {
			url += '&strWhere=' + JShell.String.encode(where);
		}
		//做处理
		if(me.params.DateType) params.push("&dateType=" + me.params.DateType);
		if(me.params.BeginDate) params.push("&startDate=" + me.params.BeginDate);
		if(me.params.EndDate) params.push("&endDate=" + me.params.EndDate);

		if(me.params.Laboratory_Id) params.push("&labID=" + me.params.Laboratory_Id);
		if(me.params.TestItem_Id) params.push("&itemID=" + me.params.TestItem_Id);
		//if (me.params.deptID) params.push("&deptID=" + me.params.deptID);
		if(me.params.Dealer_Id) params.push("&dealerID=" + me.params.Dealer_Id);
		if(me.params.BillingUnit_Id) params.push("&billingUnitID=" + me.params.BillingUnit_Id);

		url += params.join("");

		return url;
	},
	/**查看明细*/
	showInfo: function(record) {
		var me = this;

		JShell.Win.open('Shell.class.pki.balance2.StepPriceInfoGrid', {
			resizable: false,
			/**时间类型*/
			dateType: me.params.DateType,
			/**开始日期*/
			startDate: me.params.BeginDate,
			/**结束日期*/
			endDate: me.params.EndDate,
			/**送检项目ID*/
			itemID: record.get('DDealerCalcPrice_ItemID'),
			/**经销商ID*/
			dealerID: record.get('DDealerCalcPrice_DealerID'),
			/**阶梯价*/
			stepPrice: record.get('DDealerCalcPrice_StepPrice'),
		}).show();
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function(record) {
		var me = this,
			operateType = '0';

		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;

		//		dateType={DATETYPE}&startDate={STARTDATE}&endDate={ENDDATE}&
		//		itemID={ITEMID}&dealerID={DEALERID}&stepPrice={STEPPRICE}&
		//		operateType={OPERATETYPE}

		var params = [];
		params.push("dateType=" + me.params.DateType);
		params.push("startDate=" + me.params.BeginDate);
		params.push("endDate=" + me.params.EndDate);

		params.push("itemID=" + (record.get("DDealerCalcPrice_ItemID") || ""));
		params.push("dealerID=" + (record.get("DDealerCalcPrice_DealerID") || ""));

		params.push("stepPrice=" + record.get("DDealerCalcPrice_StepPrice"));
		params.push("operateType=" + operateType);

		url += "?" + params.join("&");

		window.open(url);
	}
});