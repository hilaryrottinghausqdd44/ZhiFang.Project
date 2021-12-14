/**
 * 机构货品选择
 * 适合左列表及右列表都为相同的实体对象(ReaGoodsOrgLink),并且右列表不需要默认加载及过滤原已选择的供货商货品
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2018-11-14
 */
Ext.define('Shell.class.rea.client.goods2.choose.LeftGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '待选货品列表',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoods_ReaGoodsNo',
		direction: 'ASC'
	}, {
		property: 'ReaGoods_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'goods2.choose.LeftGrid',
	/**用户UI配置Name*/
	userUIName: "机构货品待选货品列表",

	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品编码/名称/简称/一级分类/二级分类/部门/拼音字头/仪器',
			fields: ['reagoods.ReaGoodsNo', 'reagoods.CName', 'reagoods.SName', 'reagoods.GoodsClass', 'reagoods.GoodsClassType', 'reagoods.DeptName', 'reagoods.PinYinZiTou', 'reagoods.SuitableType']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '条码类型',
			hidden: true,
			width: 50,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoods_BarCodeMgr");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsClass',
			text: '一级分类',
			hidden: false,
			width: 70,
			defaultRenderer: true
		}, {
			text: '二级分类',
			dataIndex: 'ReaGoods_GoodsClassType',
			width: 70,
			hidden: false,
			defaultRenderer: true
		}, {
			text: '所属部门',
			dataIndex: 'ReaGoods_DeptName',
			width: 80,
			hidden: false,
			defaultRenderer: true
		}, {
			text: '仪器',
			dataIndex: 'ReaGoods_SuitableType',
			width: 70,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		me.buttonToolbarItems.unshift({
			emptyText: '条码类型',
			labelWidth: 0,
			width: 70,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'BarCodeMgr',
			hasStyle: true,
			data: [
				['', '全部', 'color:green;'],
				['0', '批条码', 'color:green;'],
				['1', '盒条码', 'color:orange;'],
				['2', '无条码', 'color:black;']
			],
			value: '',
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		}, {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 80,
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 80,
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 80,
			name: 'BDict_CName',
			itemId: 'BDict_CName',
			xtype: 'uxCheckTrigger',
			emptyText: '厂商选择',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '厂商选择',
				defaultWhere: "bdict.BDictType.DictTypeCode='ProdOrg'"
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(p, record);
				}
			}
		}, {
			fieldLabel: '厂商主键ID',
			itemId: 'BDict_Id',
			name: 'BDict_Id',
			xtype: 'textfield',
			hidden: true
		});
		me.buttonToolbarItems.push('->', {
			iconCls: 'button-check',
			text: '全部选择',
			tooltip: '将当前页货品全部选择',
			handler: function() {
				me.onAcceptClick();
			}
		});
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var Id = buttonsToolbar.getComponent('BDict_Id');
		var CName = buttonsToolbar.getComponent('BDict_CName');
		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');
		p.close();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this,
			where = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			goodsClass = buttonsToolbar.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar.getComponent('GoodsClassType').getValue(),
			dictCName = buttonsToolbar.getComponent('BDict_CName').getValue(),
			search = buttonsToolbar.getComponent('Search'),
			barCodeMgr = buttonsToolbar.getComponent('BarCodeMgr');
		if(barCodeMgr) {
			var value = barCodeMgr.getValue();
			if(value) {
				where.push("reagoods.BarCodeMgr=" + value);
			}
		}
		if(goodsClass) {
			where.push("reagoods.GoodsClass='" + goodsClass + "'");
		}
		if(goodsClassType) {
			where.push("reagoods.GoodsClassType='" + goodsClassType + "'");
		}
		if(dictCName) {
			where.push("reagoods.ProdOrgName='" + dictCName + "'");
		}
		if(search) {
			var value = search.getValue();
			var searchHql = "";
			if(value)searchHql = me.getSearchWhere(value);
			if(searchHql) {
				searchHql = "(" + searchHql + ")";
				where.push(searchHql);
			}
		}
		me.internalWhere = where.join(" and ");
	},
	/**获取外部传入的外部查询条件*/
	setExternalWhere: function(externalWhere) {
		var me = this;
		me.externalWhere = externalWhere;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.fireEvent('onBeforeSearch', me);
		//this.load(null, true, autoSelect);
		return me.callParent(arguments);
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = [];
		me.store.each(function(rec) {
			records.push(rec);
		});
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onAccept', me, records);
	}
});