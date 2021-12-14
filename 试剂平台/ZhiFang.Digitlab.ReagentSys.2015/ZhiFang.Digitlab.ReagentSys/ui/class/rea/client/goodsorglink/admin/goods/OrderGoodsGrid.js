/**
 * 产品采购供应维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.goods.OrderGoodsGrid', {
	extend: 'Shell.class.rea.client.goodsorglink.basic.OrderGoodsGrid',

	/**应用类型:产品:goods;订货/供货:cenorg*/
	appType: "goods",
	/**机构类型*/
	OrgType: [
		['', '全部'],
		['0', '供货方'],
		['1', '订货方']
	],
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoodsOrgLink_CenOrg_OrgType',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();

		me.on({
			itemdblclick: function(view, record) {
				me.showForm(record.get(me.PKField));
			}
		});
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '机构名称/机构编码',
			fields: ['ReaGoodsOrgLink.CenOrg.CName', 'ReaGoodsOrgLink.CenOrg.OrgNo']
		};
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns = me.callParent(arguments);
		columns.splice(1, 0, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_OrgType',
			text: '机构类型',
			width: 65,
			renderer: function(value, meta, record, rowIndex, colIndex) {
				var v = "";
				meta = me.showQtipValue(meta, record);
				if(value == "0") {
					v = "供货方";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "订货方";
					meta.style = "color:orange;";
				}
				//meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_CName',
			text: '机构名称',
			//flex: 1,
			width: 250,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_OrgNo',
			text: '机构编码',
			width: 80,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		},{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_CName',
			text: '产品名称',
			hidden:true,
			width: 230,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsNo',
			text: '对方产品编号',
			hidden:true,
			width: 80,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		});
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];

		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: 'btnCheck',
			text: '机构选择新增',
			tooltip: '供货/订货方选择',
			handler: function() {
				me.onCheckCenOrgClick();
			}
		});
		items.push('add', '-', 'edit', 'save', 'del');
		items.push('-', {
			fieldLabel: '机构类型',
			width: 140,
			labelWidth: 65,
			name: 'ReaCenOrgCenOrgType',
			itemId: 'ReaCenOrgCenOrgType',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			data: me.OrgType
		});
		items.push({
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	onCheckCenOrgClick: function() {
		var me = this;
		var config = {
			width: 620,
			height: 500,
			/**是否单选*/
			checkOne: false,
			/**是否带清除按钮*/
			hasClearButton: false,
			resizable: true,
			listeners: {
				accept: function(p, records) {
					me.onAccept(p, records);
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.goodsorglink.basic.CenOrgCheck', config).show();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		data.list = list;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			CenOrgCenOrgType = buttonsToolbar.getComponent('ReaCenOrgCenOrgType');
		CenOrgCenOrgType.on({
			change: function() {
				me.onSearch();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			OrgType = buttonsToolbar.getComponent('ReaCenOrgCenOrgType'),
			Search = buttonsToolbar.getComponent('Search').getValue();

		if(OrgType && OrgType.getValue()) {
			params.push('reagoodsorglink.CenOrg.OrgType=' + OrgType.getValue());
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}
		return me.callParent(arguments);
	},
	onAccept: function(p, records) {
		var me = this;
		var len = records.length;
		if(len == 0) {
			p.close();
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			me.addSaveOne(i, records[i], p);
		}
	},
	addSaveOne: function(index, record, p) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addUrl);
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var entity = {
			'Id': -1,
			'Visible': 1,
			'ReaGoods': {
				"Id": me.GoodsId,
				"DataTimeStamp": strDataTimeStamp.split(',')
			},
			'CenOrg': {
				"Id": record.get("ReaCenOrg_Id"),
				"DataTimeStamp": strDataTimeStamp.split(',')
			}
		};
		var params = JShell.JSON.encode({
			"entity": entity
		});
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				//var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						p.close();
						me.onSearch();
					} else {
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	}
});