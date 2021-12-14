/**
 * 退库入库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.out.refund.DocGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '退库入库',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocByHQL?isPlanish=true',
	/**退库入库调用退库接口失败后,物理删除退库入库的相关信息*/
	delUrl: '/ReaManageService.svc/RS_UDTO_DelDelReaBmsInDocByReturnOfInDocId',
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsInDoc_DataAddTime',
		direction: 'DESC'
	}],

	/**状态查询按钮选中值*/
	searchStatusValue: null,
	StatusList: [],
	/**状态枚举*/
	StatusEnum: {},
	/**状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**扫码模式(严格模式:strict,混合模式：mixing)*/
	CodeScanningMode: "mixing",
	/**用户UI配置Key*/
	userUIKey: 'out.refund.DocGrid',
	/**用户UI配置Name*/
	userUIName: "退库入库列表",
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**入库数据接口标志Key*/
	ReaBmsInDocIOFlag: 'ReaBmsInDocIOFlag',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//初始化
		me.initDateArea(0);
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsInDocIOFlag, false, true, null);
		me.addEvents('selectDtlGrid');
		//默认只操作入库类型为库存初始化(手工入库)的数据
		me.defaultWhere = "reabmsindoc.InType=3";
		//查询框信息
		me.searchInfo = {
			emptyText: '入库总单号/送货人',
			itemId: 'Search',
			flex: 1,
			isLike: true,
			fields: ['reabmsindoc.InDocNo', 'reabmsindoc.Carrier']
		};
		me.getStatusListData();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsInDoc_DataAddTime',
			text: '入库日期',
			align: 'center',
			width: 95,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsInDoc_Status',
			text: '单据状态',
			width: 60,
			renderer: function(value, meta) {
				var v = value;
				if (me.StatusEnum != null)
					v = me.StatusEnum[value];
				var bColor = "";
				if (me.StatusBGColorEnum != null)
					bColor = me.StatusBGColorEnum[value];
				var fColor = "";
				if (me.StatusFColorEnum != null)
					fColor = me.StatusFColorEnum[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDoc_StatusName',
			text: '单据状态',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDoc_InDocNo',
			text: '入库总单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'ReaBmsInDoc_IOFlag',
			text: '接口标志',
			width: 65,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.ReaBmsInDocIOFlag].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsInDocIOFlag].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsInDocIOFlag].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsInDocIOFlag].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsInDocIOFlag].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsInDocIOFlag].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}];

		return columns;
	},	
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createQuickSearchButtonToolbar());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonsToolbarSearch());
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '退库入库',
			tooltip: '退库入库',
			handler: function() {
				me.onAddClick();
			}
		});
		if (me.hasDel) {
			items.push('-');
			items.push('del');
		}
		items.push('->');
		return items;
	},
	/**查询输入栏*/
	createButtonsToolbarSearch: function() {
		var me = this;
		var items = [];

		var lists=JShell.REA.StatusList.Status[me.ReaBmsInDocIOFlag].List;
		if(!lists)lists=[];
		items.push({
			fieldLabel: '',
			name: 'IOFlag',
			itemId: 'IOFlag',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			labelWidth: 0,
			labelAlign: 'right',
			data: lists,
			width: 100,
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		}, '-', {
			type: 'search',
			info: me.searchInfo
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbarSearch',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'uxdatearea',
			itemId: 'date',
			labelWidth: 55,
			labelAlign: 'right',
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'dateareaToolbar',
			items: items
		});
	},
	/**获取状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if (me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsInDocStatus",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.StatusList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if (data.success) {
				if (data.value) {
					if (data.value[0].ReaBmsInDocStatus.length > 0) {
						me.StatusList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if (obj.FontColor) {
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if (obj.BGColor) {
								style.push('color:' + obj.BGColor);
								me.StatusBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.StatusEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var buttonsToolbarSearch = me.getComponent('buttonsToolbarSearch'),
			dateareaToolbar = me.getComponent('dateareaToolbar');
		var search = buttonsToolbarSearch.getComponent('Search');
		var iOFlag= buttonsToolbarSearch.getComponent('IOFlag');
		var date = dateareaToolbar.getComponent('date');
		var where = [];
		if (me.searchStatusValue != null && parseInt(me.searchStatusValue) > -1)
			where.push("reabmsindoc.Status=" + me.searchStatusValue);

		if (date) {
			var dateValue = date.getValue();
			if (dateValue) {
				if (dateValue.start) {
					where.push('reabmsindoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if (dateValue.end) {
					where.push('reabmsindoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) +
						"'");
				}
			}
		}
		if (iOFlag) {
			var iOFlagV = iOFlag.getValue();
			if (iOFlagV||iOFlagV=="0") {
				where.push('reabmsindoc.IOFlag=' + iOFlagV);
			}
		}
		
		if (search) {
			var value = search.getValue();
			if (value) {
				var searchHql = me.getSearchWhere(value);
				if (searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("ReaBmsInDoc_Id");
		var config = {
			title: '入库单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: "ReaBmsInDocStatus"
		};
		var win = JShell.Win.open('Shell.class.rea.client.reacheckinoperation.Panel', config);
		win.show();
	},
	/**@description 验证日期类型是否选择*/
	validDateType: function() {
		var me = this;
		return true;
	},
	/**@description 设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateAreaValue) date.setValue(dateAreaValue);
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);
	},
	/**按验收状态快捷查询*/
	onStatusSearch: function(status) {
		var me = this;
		me.setStatusSearchToggle(status);
		me.searchStatusValue = status;
		me.onSearch();
	},
	/**按验收状态按钮状态点击后样式设置*/
	setStatusSearchToggle: function(status) {
		var me = this;
		var buttonsToolbar = me.getComponent('statusSearchButtonToolbar');
		var allStatus = buttonsToolbar.getComponent('AllStatus');
		var apply = buttonsToolbar.getComponent('Apply');
		var accept = buttonsToolbar.getComponent('Accept');

		switch (status) {
			case 1:
				allStatus.toggle(false);
				apply.toggle(true);
				accept.toggle(false);
				break;
			case 2:
				allStatus.toggle(false);
				apply.toggle(false);
				accept.toggle(true);
				break;
			default:
				allStatus.toggle(true);
				apply.toggle(false);
				accept.toggle(false);
				break;
		}
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**@description 继续入库按钮*/
	onContinueToAcceptClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsInDoc_Status");
		if (status != "1") {
			var statusName = "";
			if (me.StatusEnum != null)
				statusName = me.StatusEnum[status];
			JShell.Msg.alert("当前状态为【" + statusName + "】,不能执行继续入库操作!", null, 2000);
			return;
		}
		me.fireEvent('onContinueToAcceptClick', me, records[0]);
	},
	/**选择出库单*/
	openAddPanel: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight * 0.78;
		var config = {
			resizable: false,
			width: maxWidth,
			height: height,
			listeners: {
				accept: function(p, records, docID) {
					me.fireEvent('selectDtlGrid', p, records, docID);
					//						p.close();
				}
			}
		};
		config.formtype = 'add';
		JShell.Win.open('Shell.class.rea.client.out.refund.check.App', config).show();
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var removeArr = [];
		//只保留全部和出库完成
		if (tempList[7]) removeArr.push(tempList[7]);
		if (tempList[6]) removeArr.push(tempList[6]);
		if (tempList[5]) removeArr.push(tempList[5]);
		if (tempList[4]) removeArr.push(tempList[4]);
		if (tempList[3]) removeArr.push(tempList[3]);
		if (tempList[2]) removeArr.push(tempList[2]);
		if (tempList[1]) removeArr.push(tempList[1]);
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		me.searchStatusValue = tempList;
		return tempList;
	}

});
