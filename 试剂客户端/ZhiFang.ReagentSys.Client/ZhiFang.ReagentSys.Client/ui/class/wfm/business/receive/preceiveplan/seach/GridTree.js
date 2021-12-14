/**
 * 收款计划查询
 * @author liangyl
 * @version 2017-08-01
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.seach.GridTree', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.SimpleGridTree',
	title: '收款计划查询列表',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_AdvSearchPReceivePlanTreeByHQL?isPlanish=true',
	/**默认加载数据*/
	defaultLoad: true,
	features: [{
		ftype: 'summary'
	}],
	defaultWhere: 'preceiveplan.IsUse=1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.paramsFilterListeners();
		me.store.on({
			load: function(store, records, successful, eOpts) {
				var roonodes = me.getRootNode().childNodes; //获取主节点
				if(roonodes.length > 0) {
					me.getSelectionModel().select(me.root.tid);
				}
				me.onPlusClick();
			}
		});
	},

	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = [];

		items.push(me.createButtontoolbar());
		items.push(me.createPagingtoolbar());

		return items;
	},
	createButtontoolbar: function() {
		var me = this;
		var items = [{
			iconCls: 'button-refresh',
			itemId: 'refresh',
			tooltip: '刷新数据',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			iconCls: 'button-arrow-in',
			itemId: 'minus',
			tooltip: '全部收缩',
			handler: function() {
				me.onMinusClick();
			}
		}, {
			iconCls: 'button-arrow-out',
			itemId: 'plus',
			tooltip: '全部展开',
			handler: function() {
				me.onPlusClick();
			}
		}, '-', {
			fieldLabel: '收款时间',
			labelWidth: 55,
			labelAlign: 'right',
			width: 150,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		}, {
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		}, {
			width: 160,
			labelWidth: 50,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			itemId: 'UserName',
			fieldLabel: '负责人',
			className: 'Shell.class.sysbase.user.CheckApp',
			classConfig: {
				title: '收款计划负责人选择'
			}
		}, {
			xtype: 'textfield',
			itemId: 'UserID',
			fieldLabel: '负责人Id',
			hidden: true
		}, {
			width: 55,
			labelAlign: 'right',
			boxLabel: '完成',
			itemId: 'IsNotFinish',
			labelWidth: 75,
			xtype: 'checkbox',
			checked: false,
			style: {
				marginLeft: '20px'
			}
		}, {
			width: 150,
			labelAlign: 'right',
			boxLabel: '是否包括变更和暂存计划',
			itemId: 'IsChange',
			xtype: 'checkbox',
			checked: true
		}];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		});
	},
	/**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;

		var config = {
			dock: 'bottom',
			itemId: 'pagingToolbar'
				//			store: me.store
		};

		if(me.defaultPageSize) config.defaultPageSize = me.defaultPageSize;
		if(me.pageSizeList) config.pageSizeList = me.pageSizeList;
		me.agingToolbarCustomItems = ['->', {
			xtype: 'label',
			itemId: 'lbReceiveAmount',
			style: "font-weight:bold;color:black;",
			text: '已收款金额总计',
			margin: '0 20 0 0'
		}, {
			xtype: 'label',
			itemId: 'lbUnReceiveAmount',
			style: "font-weight:bold;color:black;",
			text: '待收金额总计',
			margin: '0 20 0 0'
		}];
		//分页栏自定义功能组件
		if(me.agingToolbarCustomItems) config.customItems = me.agingToolbarCustomItems;

		return Ext.create('Shell.ux.toolbar.Paging', config);
	},

	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '合同名称',
			xtype: 'treecolumn',
			dataIndex: 'PContractName',
			width: 150,
			sortable: false
		}, {
			text: '用户',
			dataIndex: 'PClientName',
			minWidth: 140,
			flex: 1,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '付款单位',
			dataIndex: 'PayOrgName',
			minWidth: 140,
			flex: 1,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '签署时间',
			dataIndex: 'ContractSignDateTime',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex) {
				var v = JShell.Date.toString(value, true) || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '收款分期',
			dataIndex: 'text',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '计划收款金额',
			dataIndex: 'ReceivePlanAmount',
			width: 85,
			sortable: false,
			summaryRenderer: function(value) {
				return '<div  style="text-align:right" ><strong>本页合计:</strong></div>';
			}
		}, {
			text: '已收金额',
			dataIndex: 'ReceiveAmount',
			width: 85,
			sortable: false,
			xtype: 'numbercolumn',
			summaryType: 'sum',
			type: 'number',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				return value;
			},
			summaryRenderer: function(value) {
				var count = me.getReceiveAmount(value);
				return '<strong>' + Ext.util.Format.number(count, count > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '待收金额',
			dataIndex: 'UnReceiveAmount',
			width: 85,
			sortable: false,
			xtype: 'numbercolumn',
			summaryType: 'sum',
			type: 'number',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				return value;
			},
			summaryRenderer: function(value) {
				var count = me.getUnReceiveAmount(value);
				return '<strong>' + Ext.util.Format.number(count, count > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '待收时间',
			dataIndex: 'ExpectReceiveDate',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex) {
				var v = JShell.Date.toString(value, true) || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '负责人',
			dataIndex: 'ReceiveManName',
			width: 85,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '状态',
			dataIndex: 'Status',
			width: 85,
			sortable: false,
			renderer: function(value, meta) {
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('PReceivePlanStatus', v);
					if(info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		}, {
			text: 'leaf',
			dataIndex: 'leaf',
			width: 85,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: 'tid',
			dataIndex: 'tid',
			width: 85,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}];
		return columns;
	},
	//待收金额 
	getUnReceiveAmount: function(value) {
		var me = this;
		var count = 0;
		var calcUnReceiveAmount = function(cNo) { //遍历节点查找相应的节点
			var node = cNo.childNodes;
			cNo.eachChild(function(cnode) {
				if(cnode.data.leaf == false) {
					var childnodes = cnode.childNodes; //获取根节点的子节点    
					for(var i = 0; i < childnodes.length; i++) {
						var cdnode = childnodes[i];
						//不是已变更的都计算
						if(cdnode.data.Status != '6') {
							count += Number(cdnode.data.UnReceiveAmount);
						}
						calcUnReceiveAmount(cdnode); //递归调用
					}

				} else {
					if(cnode.data.UnReceiveAmount){
						//不是已变更的都计算
						if(cnode.data.Status != '6') {
						    count += Number(cnode.data.UnReceiveAmount);
					    }
					}
				}
			});
		}
		var rootN = me.getRootNode(); //获取树的根节点
		calcUnReceiveAmount(rootN); //调用函数
		return count;
	},
	//已收金额 
	getReceiveAmount: function(value) {
		var me = this;
		var count = 0;
		var calcReceiveAmount = function(cNo) { //遍历节点查找相应的节点
			var node = cNo.childNodes;
			cNo.eachChild(function(cnode) {
				if(cnode.data.leaf == false) {
					var childnodes = cnode.childNodes; //获取根节点的子节点    
					for(var i = 0; i < childnodes.length; i++) {
						var cdnode = childnodes[i];
						//不是已变更的都计算
						if(cdnode.data.Status != '6') {
							count += Number(cdnode.data.ReceiveAmount);
						}
						calcReceiveAmount(cdnode); //递归调用
					}

				} else {
					if(cnode.data.UnReceiveAmount){
						//不是已变更的都计算
						if(cnode.data.Status != '6') {
						    count += Number(cnode.data.ReceiveAmount);
					    }
					}
				}
			});
		}
		var rootN = me.getRootNode(); //获取树的根节点
		calcReceiveAmount(rootN); //调用函数
		return count;
	},
	/**
	 * 树字段对象
	 * @type 
	 */
	treeFields: {
		/**
		 * 基础字段数组
		 * @type 
		 */
		defaultFields: [{
				name: 'text',
				type: 'auto'
			}, //默认的现实字段
			{
				name: 'expanded',
				type: 'auto'
			}, //是否默认展开
			{
				name: 'leaf',
				type: 'auto'
			}, //是否叶子节点
			{
				name: 'icon',
				type: 'auto'
			}, //图标
			{
				name: 'url',
				type: 'auto'
			}, //地址
			{
				name: 'tid',
				type: 'auto'
			}, //默认ID号
			{
				name: 'value',
				type: 'auto'
			}
		],
		/**
		 * 模块对象字段数组
		 * @type 
		 */
		moduleFields: [{
				name: 'ReceiveGradationName',
				type: 'auto'
			}, //收款分期
			{
				name: 'ReceivePlanAmount',
				type: 'auto'
			}, //收款金额
			{
				name: 'ExpectReceiveDate',
				type: 'auto'
			}, //时间
			{
				name: 'ReceiveManName',
				type: 'auto'
			}, //责任人
			{
				name: 'UnReceiveAmount',
				type: 'number'
			}, {
				name: 'ReceiveAmount',
				type: 'number'
			}, {
				name: 'PContractName',
				type: 'auto'
			}, {
				name: 'PClientName',
				type: 'auto'
			}, //客户
			{
				name: 'PayOrgName',
				type: 'auto'
			}, //付款单位
			{
				name: 'PContractName',
				type: 'auto'
			}, //合同
			{
				name: 'ReceiveDate',
				type: 'auto'
			}, //收款日期
			{
				name: 'Status',
				type: 'auto'
			}, {
				name: 'ContractSignDateTime',
				type: 'auto'
			}
		]
	},
	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;
		var treeFields = me.treeFields;
		var defaultFields = treeFields.defaultFields;
		var moduleFields = treeFields.moduleFields;
		var fields = defaultFields.concat(moduleFields);
		return fields;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=PReceivePlan_PContractName,PReceivePlan_ReceiveGradationName,PReceivePlan_ReceivePlanAmount,PReceivePlan_ExpectReceiveDate,PReceivePlan_ReceiveManName,PReceivePlan_ReceiveAmount,PReceivePlan_UnReceiveAmount,PReceivePlan_PClientName,PReceivePlan_PayOrgName,PReceivePlan_ContractSignDateTime,PReceivePlan_Status';
		var where = me.getParms(true);
		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	getParms: function(status) {
		var me = this,
			arr = [],
			buttonsToolbar = me.getComponent('topToolbar');
		if(!buttonsToolbar) return;
		var EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			UserID = buttonsToolbar.getComponent('UserID').getValue();
		var EndDate2 = JcallShell.Date.toString(EndDate, true),
			BeginDate2 = JcallShell.Date.toString(BeginDate, true);
		var IsChange = buttonsToolbar.getComponent('IsChange').getValue(),
			IsNotFinish = buttonsToolbar.getComponent('IsNotFinish').getValue();
		if(BeginDate2) {
			arr.push("preceiveplan.ExpectReceiveDate" + ">='" + JShell.Date.toString(BeginDate2, true) + "'");
		}
		if(EndDate2) {
			arr.push("preceiveplan.ExpectReceiveDate" + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate2), true) + "'");
		}
		//是否变更  说明 && status 是因为后台计算时已经有条件了
		if(!IsChange && status) {
			arr.push("preceiveplan.Status in (3,5,7,8)");
		}
		if(IsChange && status){
			arr.push("preceiveplan.Status in (1,3,5,6,7,8)");
		}
		//未完成（待收金额>0)
		if(IsNotFinish) {
			arr.push("preceiveplan.UnReceiveAmount=0");
		} else {
			arr.push("preceiveplan.UnReceiveAmount>0");
		}
		//负责人
		if(UserID) {
			arr.push("preceiveplan.ReceiveManID='" + UserID + "'");
		}
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
		return where;

	},
	paramsFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('topToolbar');
		if(!buttonsToolbar) return;
		//负责人
		var UserName = buttonsToolbar.getComponent('UserName'),
			UserID = buttonsToolbar.getComponent('UserID');
		if(UserName) {
			UserName.on({
				check: function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					me.load();
					p.close();
				}
			});
		}
		//时间类型+时间
		var EndDate = buttonsToolbar.getComponent('EndDate');
		var BeginDate = buttonsToolbar.getComponent('BeginDate');

		if(EndDate) {
			EndDate.on({
				change: function() {
					me.load();
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function() {
					me.load();
				}
			});
		}
		var IsChange = buttonsToolbar.getComponent('IsChange'),
			IsNotFinish = buttonsToolbar.getComponent('IsNotFinish');
		if(IsChange) {
			IsChange.on({
				change: function() {
					me.load();
				}
			});
		}
		if(IsNotFinish) {
			IsNotFinish.on({
				change: function() {
					me.load();
				}
			});
		}
	},
	/**点击刷新按钮*/
	onRefreshClick: function(where, isPrivate) {
		var me = this;
		me.canLoad = true;
		collapsed = me.getCollapsed();
		me.onUnReceiveAmount();
		me.onReceiveAmount();
		me.defaultLoad = true;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.externalWhere = isPrivate ? me.externalWhere : where;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PReceivePlanStatus', function() {
			if(!JShell.System.ClassDict.PReceivePlanStatus) {
				JShell.Msg.error('未获取到收款计划状态，请刷新列表');
				return;
			}
			me.store.load();
		});
		
	},

	/**待收款金额统计*/
	onUnReceiveAmount: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('pagingToolbar');
		var lbUnReceiveAmount = buttonsToolbar.getComponent('lbUnReceiveAmount');

		var UnReceiveAmount = 0;
		me.getUnReceiveAmountCount(function(data) {
			if(data.value && data) {
				UnReceiveAmount = data.value;
			}
		});
		lbUnReceiveAmount.setText("待收金额总计:" + UnReceiveAmount);
	},
	/**已收款金额统计*/
	onReceiveAmount: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('pagingToolbar');
		var lbReceiveAmount = buttonsToolbar.getComponent('lbReceiveAmount');
		var ReceiveAmount = 0;
		me.getReceiveAmountCount(function(data) {
			if(data.value && data) {
				ReceiveAmount = data.value;
			}
		});
		lbReceiveAmount.setText("已收款金额总计:" + ReceiveAmount);
	},
	/**获取已收款金额统计*/
	getReceiveAmountCount: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_AdvSearchTotalPReceivePlanByHQL';
		url += '?fields=ReceiveAmount';
		var where = me.getParms();
		if(where) {
			url += '&where=' + where;
		}
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取待收款金额统计*/
	getUnReceiveAmountCount: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_AdvSearchTotalPReceivePlanByHQL';
		url += '?fields=UnReceiveAmount';
		var where = me.getParms();
		if(where) {
			url += '&where=' + where;
		}
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}
});