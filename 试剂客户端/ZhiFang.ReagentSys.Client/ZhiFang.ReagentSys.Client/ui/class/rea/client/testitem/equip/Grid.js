/**
 * 仪器项目关系表
 * Rea_TestEquipItem_仪器项目关系表
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equip.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '仪器项目列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaTestEquipItemEntityListByJoinHql?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaTestEquipItem',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipItemByField',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaTestEquipItem',
	/**获取获取供应商数据服务路径*/
	selectEquipItemUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestItemByHQL?isPlanish=true',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaTestEquipItem_DispOrder',
		direction: 'ASC'
	}],
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**带分页栏*/
	hasPagingtoolbar: true,
	TestItemList: [],
	TestItemEnum: null,
	/**仪器ID*/
	EquipID: null,
	/**后台排序*/
	remoteSort: false,
	/**用户UI配置Key*/
	userUIKey: 'testitem.equip.Grid',
	/**用户UI配置Name*/
	userUIName: "仪器项目列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.override(Ext.ToolTip, {
			maxWidth: 350
		});

	},
	initComponent: function() {
		var me = this;
		me.getTestItemInfo();
		//查询框信息:因为查询服务是定制的,机构货品信息按机构货品实体查询
		me.searchInfo = {
			width: 195,
			emptyText: '项目名称/Lis编码/英文名称',
			isLike: true,
			itemId: 'Search',
			fields: ['reatestitem.CName', 'reatestitem.LisCode', 'reatestitem.EName']
		};
		me.buttonToolbarItems = ['refresh', '-', 'add', 'del',  {
			xtype: 'splitbutton',
			textAlign: 'left',
			iconCls: 'button-add',
			text: '获取LIS仪器项目',
			handler: function(btn, e) {
				btn.overMenuTrigger = true;
				btn.onClick(e);
			},
			menu: [{
				text: '按当前仪器获取',
				iconCls: 'button-import',
				tooltip: '从LIS导入当前仪器的项目信息',
				listeners: {
					click: function(but) {
						me.onExportLis(1);
					}
				}
			}, {
				text: '全部仪器项目获取',
				iconCls: 'button-exp',
				tooltip: '从LIS导入全部仪器项目信息',
				listeners: {
					click: function(but) {
						me.onExportLis(2);
					}
				}
			}]
		}, '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestEquipItem_Id',
			text: '关系ID',
			hidden: true,
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'ReaTestEquipItem_TestItemID',
			text: '项目编号',
			hidden: true,
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_LisCode',
			text: 'Lis编码',
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_CName',
			text: '项目名称',
			sortable: false,
			minWidth: 150,
			flex: 1,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_SName',
			text: '项目简称',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_EName',
			text: '英文名称',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_TestEquipID',
			text: '仪器Id',
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	showMemoText: function(value, meta) {
		var me = this;
		var val = value.replace(/(^\s*)|(\s*$)/g, "");
		val = val.replace(/\\r\\n/g, "<br />");
		val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1 = v.indexOf("</br>");
		if(index1 > 0) v = v.substring(0, index1);
		if(v.length > 0) v = (v.length > 32 ? v.substring(0, 32) : v);
		if(value.length > 32) {
			v = v + "...";
		}
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return v
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight * 0.8;
		var ids = me.getGoodsId();

		var defaultWhere = '';
		if(ids) {
			defaultWhere = 'reatestitem.Id not in(' + ids + ')';
		}
		JShell.Win.open('Shell.class.rea.client.testitem.item.CheckGrid', {
			title: '项目选择',
			width: 900,
			height: height,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onSave(p, records);
				}
			}
		}).show();
	},
	/**获取当前页的货品ID*/
	getGoodsId: function() {
		var me = this;
		var records = me.store.data.items;
		var len = records.length;
		var ids = '';

		for(var i = 0; i < len; i++) {
			var GoodsID = records[i].get('ReaTestEquipItem_TestItemID');
			if(i > 0) {
				ids += ',';
			}
			ids += GoodsID;
		}
		return ids;
	},
	/**保存关系数据*/
	onSave: function(p, records) {
		var me = this,
			ids = [],
			addIds = [];

		if(records.length == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;

		for(var i in records) {
			ids.push(records[i].get('ReaTestItem_Id'));
		}

		//获取现有关系用于验证过滤已经存在的关系
		me.getLinkByIds(ids, function(list) {
			addIds = [];
			for(var i in records) {
				var TestItemID = records[i].get('ReaTestItem_Id');
				var CName = records[i].get('ReaTestItem_CName');
				var hasLink = false;
				for(var j in list) {
					if(TestItemID == list[j].TestItemID) {
						hasLink = true;
						break;
					}
				}
				if(!hasLink) {
					var obj = {
						TestItemID: TestItemID
					};
					addIds.push(obj);
				}
				if(hasLink) {
					me.hideMask(); //隐藏遮罩层
					p.close();
				}
			}
			//循环保存数据
			for(var i in addIds) {
				me.saveLength = addIds.length;
				me.onAddOneLink(addIds[i], function() {
					JShell.Msg.alert("保存成功", null, 1000);
					p.close();
					me.onSearch();
				});
			}
		});
	},
	/**新增关系数据*/
	onAddOneLink: function(addIds, callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var params = {
			entity: {
				TestEquipID: me.EquipID,
				TestItemID: addIds.TestItemID,
				Visible: 1
			}
		};

		//提交数据到后台
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					callback();
				}
			}
		});
	},

	/**根据IDS获取关系数据，用于验证勾选的货品是否已经存在于关系中*/
	getLinkByIds: function(ids, callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl.split('?')[0] +
			'?fields=ReaTestEquipItem_TestItemID' +
			'&where=reatestequipitem.TestItemID in(' + ids.join(',') + ')  and reatestequipitem.TestEquipID=' + me.EquipID;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取项目信息*/
	getTestItemInfo: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectEquipItemUrl;
		url += '&fields=ReaTestItem_CName,ReaTestItem_Id';
		me.TestItemEnum = {}, me.TestItemList = [];
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						tempArr = [obj.ReaTestItem_Id, obj.ReaTestItem_CName];
						me.TestItemEnum[obj.ReaTestItem_Id] = obj.ReaTestItem_CName;
						me.TestItemList.push(tempArr);
					});
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = me.getComponent("buttonsToolbar").getComponent("Search").getValue(),
			params = [];

		me.internalWhere = '';

		if(me.EquipID) {
			params.push('reatestequipitem.TestEquipID=' + me.EquipID);
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
				me.internalWhere = "(" + me.getSearchWhere(search) + ")";
			}
		}
		return me.callParent(arguments);
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if(!me.EquipID) {
			JShell.Msg.error('仪器不能为空');
			return;
		}
		me.load(null, true, autoSelect);
	},
	/**
	 * 从LIS系统仪器项目
	 */
	onExportLis: function(chooseType) {
		var me = this;
		if(chooseType == 1 && !me.EquipID) {
			JShell.Msg.error('仪器不能为空');
			return;
		}
		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_EditSyncLisReaTestEquipItemInfo';
		if(chooseType == 1 &&me.EquipID) {
			url = url + "?equipId=" + me.EquipID;
		}
		me.showMask("获取LIS仪器项目中...");
		JShell.Server.get(url, function(data) {
			me.hideMask();
			if(data.success) {
				me.onSearch();			
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}
});