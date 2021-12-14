/**
 * 库房(货架)试剂维护
 * @author longfc	
 * @version 2019-07-09
 */
Ext.define('Shell.class.rea.client.storagegoodslink.LinkGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	
	title: '库房试剂列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaStorageGoodsLinkEntityListByAllJoinHQL?isPlanish=true',
	/**删除数据服务路径*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaStorageGoodsLink',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaStorageGoodsLink',
	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaStorageGoodsLink_DispOrder',
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
	/**仪器ID*/
	StorageID: null,
	/**后台排序*/
	remoteSort: false,
	/**用户UI配置Key*/
	userUIKey: 'storagegoodslink.LinkGrid',
	/**用户UI配置Name*/
	userUIName: "库房试剂列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息:因为查询服务是定制的,机构货品信息按机构货品实体查询
		me.searchInfo = {
			width: 225,
			emptyText: '货品编码/拼音字头/货品名称/英文名称',
			isLike: true,
			itemId: 'Search',
			fields: ['reagoods.ReaGoodsNo', 'reagoods.CName', 'reagoods.PinYinZiTou', 'reagoods.EName']
		};
		me.buttonToolbarItems = ['refresh', '-', 'add', 'del', '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaStorageGoodsLink_StorageName',
			text: '库房名称',
			hidden: true,
			width: 150,
			sortable: false,
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorageGoodsLink_PlaceName',
			text: '货架名称',
			hidden: true,
			width: 150,
			sortable: false,
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_BarCodeMgr',
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
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_CName',
			text: '货品名称',
			minWidth: 150,
			flex: 1,
			sortable: false,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaStorageGoodsLink_ReaGoods_BarCodeMgr");
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
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_SName',
			text: '货品简称',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_ProdOrgName',
			text: '品牌',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_UnitName',
			text: '单位',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_UnitMemo',
			text: '规格',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_ReaGoods_ProdEara',
			text: '产地',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_StorageID',
			text: '库房ID',
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_PlaceID',
			text: '货架ID',
			width: 150,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorageGoodsLink_GoodsID',
			text: '机构货品ID',
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorageGoodsLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}];

		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;

		var defaultWhere = " reagoods.Visible=1 ";
		var arrIdStr = [],
			idStr = "";
		me.store.each(function(record) {
			var goodId = record.get("ReaStorageGoodsLink_GoodsID");
			if(goodId && Ext.Array.contains(goodId) == false) arrIdStr.push(goodId);
		});
		if(arrIdStr.length > 0) idStr = arrIdStr.join(",");
		if(idStr) defaultWhere = defaultWhere + " and reagoods.Id not in (" + idStr + ")";
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		
		JShell.Win.open('Shell.class.rea.client.goods2.choose.App', {
			resizable: true,
			width: maxWidth,
			height: height,
			leftDefaultWhere: defaultWhere,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onSave(p, records);
				}
			}
		}).show();
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
			ids.push(records[i].get('ReaGoods_Id'));
		}

		//获取现有关系用于验证过滤已经存在的关系
		me.getLinkByIds(ids, function(list) {
			addIds = [];
			for(var i in records) {
				var GoodsId = records[i].get('ReaGoods_Id');
				var CName = records[i].get('ReaGoods_CName');
				var hasLink = false;
				for(var j in list) {
					if(GoodsId == list[j].GoodsID) {
						hasLink = true;
						break;
					}
				}
				if(!hasLink) {
					var obj = {
						GoodsID: GoodsId
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
				StorageID: me.StorageID,
				GoodsID: addIds.GoodsID,
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
			'?fields=ReaStorageGoodsLink_GoodsID' +
			'&where=reastoragegoodslink.GoodsID in(' + ids.join(',') + ')  and reastoragegoodslink.StorageID=' + me.StorageID;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = me.getComponent("buttonsToolbar").getComponent("Search").getValue(),
			params = [];

		me.internalWhere = '';

		if(me.StorageID) {
			params.push('reastoragegoodslink.StorageID=' + me.StorageID);
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
		if(!me.StorageID) {
			//JShell.Msg.error('库房不能为空');
			return;
		}
		me.load(null, true, autoSelect);
	}
});