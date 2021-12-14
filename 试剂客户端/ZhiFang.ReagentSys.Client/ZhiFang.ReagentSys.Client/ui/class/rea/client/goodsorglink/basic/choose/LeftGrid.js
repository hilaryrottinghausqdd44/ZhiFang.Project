/**
 * 机构货品选择
 * 适合左列表及右列表都为相同的实体对象(ReaGoodsOrgLink),并且右列表不需要默认加载及过滤原已选择的供货商货品
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2018-11-14
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.choose.LeftGrid', {
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
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByCenOrgId?isPlanish=true',
	/**当前选择的机构Id*/
	ReaCenOrgId: null,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoods_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'goodsorglink.basic.choose.LeftGrid',
	/**用户UI配置Name*/
	userUIName: "机构货品待选货品列表",
	
	initComponent: function() {
		var me = this;
		//JcallShell.REA.ReaGoods.getClassList("GoodsClass", false);
		//JcallShell.REA.ReaGoods.getClassList("GoodsClassType", false);
		me.selectUrl = me.selectUrl + "&cenOrgId=" + me.ReaCenOrgId;
		me.addEvents('onBeforeSearch');
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '部门/供应商/拼音字头/英文名/货品名称/货品编码/厂商货品编码',
			fields: ['reagoods.DeptName','reagoods.ReaCompanyName', 'reagoods.PinYinZiTou', 'reagoods.EName', 'reagoods.CName', 'reagoods.ShortCode', 'reagoods.ReaGoodsNo', 'reagoods.ProdGoodsNo']
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
			dataIndex: 'ReaGoods_GoodsClass',
			text: '一级分类',
			width: 75,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_GoodsClassType',
			text: '二级分类',
			width: 75,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			//flex:1,
			width: 210,
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
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '货品条码类型',
			sortable: false,
			menuDisabled: true,
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoods_IsPrintBarCode',
			text: '货品是否打印条码',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			hideable: false,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "否";
					meta.style = "color:orange;";
				} else if(value == "1") {
					v = "是";
					meta.style = "color:green;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ProdGoodsNo',
			text: '厂商货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_CenOrgGoodsNo',
			text: '供货商货品编码',
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Price',
			text: '单价',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '货品平台编号',
			width: 95,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ReaCompanyName',
			text: '所属供应商',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ShortCode',
			text: '同系列码',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
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
				['', '请选择', 'color:green;'],
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
		me.onSearch();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			//list[i].ReaGoods_IsRegister = list[i].ReaGoods_IsRegister == '1' ? true : false;
			list[i].ReaGoods_IsPrintBarCode = list[i].ReaGoods_IsPrintBarCode == '1' ? true : false;
			list[i].ReaGoods_CenOrgGoodsNo = list[i].ReaGoods_ReaGoodsNo;
		}
		data.list = list;
		return data;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!me.ReaCenOrgId) return false;

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
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
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			goodsClass = buttonsToolbar.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar.getComponent('GoodsClassType').getValue(),
			dictCName = buttonsToolbar.getComponent('BDict_CName').getValue(),
			search = buttonsToolbar.getComponent('Search').getValue(),
			where = [];
		if(goodsClass) {
			where.push("reagoods.GoodsClass='"+goodsClass+"'");
		}
		if(goodsClassType) {
			where.push("reagoods.GoodsClassType='"+goodsClassType+"'");
		}
		if(dictCName) {
			where.push("reagoods.ProdOrgName='"+dictCName+"'");
		}
		if(search) {
			where.push('(' + me.getSearchWhere(search) + ')');
		}
		me.internalWhere = where.join(' and ');
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