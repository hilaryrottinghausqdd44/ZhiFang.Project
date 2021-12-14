/**
 * 部门货品关系列表
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.deptgoods.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '部门货品列表',
	width: 800,
	height: 500,

	/**根据HRDeptID查询货品列表*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHRDeptID?isPlanish=true',
	/**获取数据服务路径*/
	selectUrl2: '/ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaDeptGoods',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaDeptGoodsByField',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaDeptGoods',
	
	defaultOrderBy: [{
		property: 'ReaDeptGoods_DeptName',
		direction: 'DESC'
	}, {
		property: 'ReaDeptGoods_DispOrder',
		direction: 'ASC'
	}],
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
	/**部门ID*/
	OrgId: null,
	/**部门名称*/
	OrgName: null,
	/**复选框*/
	multiSelect: true,
	/**查询货品包含选择部门的子孙节点*/
	isSearchChildNode: false,
	
	selType: 'checkboxmodel',
	/**用户UI配置Key*/
	userUIKey: 'deptgoods.Grid',
	/**用户UI配置Name*/
	userUIName: "部门货品列表",
	
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaDeptGoods_DeptName',
			text: '部门',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_ReaGoodsNo',
			text: '货品编码',
			flex: 1,
			minWidth: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_ProdGoodsNo',
			text: '厂商货品编码',
			flex: 1,
			minWidth: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_BarCodeMgr',
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
			dataIndex: 'ReaDeptGoods_ReaGoods_CName',
			text: '货品名称',
			flex: 1,
			minWidth: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaDeptGoods_ReaGoods_BarCodeMgr");
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
			dataIndex: 'ReaDeptGoods_ReaGoods_ProdEara',
			text: '产地',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_UnitName',
			text: '单位',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_Memo',
			text: '备注',
			flex: 1,
			minWidth: 100,
			editor: {}
		}, {
			dataIndex: 'ReaDeptGoods_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_DispOrder',
			text: '显示次序',
			width: 70,
			editor: {}
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_Id',
			text: '货品ID',
			hidden: true
		}, {
			dataIndex: 'ReaDeptGoods_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-',
			{
				text: '货品选择',
				tooltip: '新增货品选择',
				iconCls: 'button-add',
				handler: function() {
					me.onAddClick();
				}
			}, 'edit', 'del', 'save', '-',
			{
				boxLabel: '本节点',
				itemId: 'checkBDictTreeId',
				checked: true,
				value: true,
				inputValue: true,
				xtype: 'checkbox',
				height: 25,
				//				style: {marginRight: '8px'},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue == true) {
							me.isSearchChildNode = false;
						} else {
							me.isSearchChildNode = true;
						}
						me.onSearch();
					}
				}
			}
		];
		//查询框信息
		me.searchInfo = {
			width: 165,
			isLike: true,
			itemId: 'Search',
			emptyText: '拼音字头/货品编码/货品名称',
			fields: ['readeptgoods.ReaGoods.PinYinZiTou','readeptgoods.ReaGoods.ReaGoodsNo','readeptgoods.ReaGoods.CName']
		};
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		var orgId = me.OrgId + '';
		if(orgId == '0') {
			JShell.Msg.error('请先选择一个部门节点');
			return;
		}
		var defaultWhere = " reagoods.Visible=1 ";
		/* var arrIdStr = [],
			idStr = "";
		me.store.each(function(record) {
			var goodId = record.get("ReaDeptGoods_ReaGoods_Id");
			if(goodId && Ext.Array.contains(goodId) == false) arrIdStr.push(goodId);
		});
		if(arrIdStr.length > 0) idStr = arrIdStr.join(",");
		if(idStr) defaultWhere = defaultWhere + " and reagoods.Id not in (" + idStr + ")"; */
		
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		//Shell.class.rea.client.goods2.basic.CheckGrid
		var linkWhere="";//关系表查询条件
		if(me.OrgId){
			linkWhere="readeptgoods.DeptID="+ me.OrgId;
		}
		JShell.Win.open('Shell.class.rea.client.deptgoods.choose.App', {
			resizable: true,
			width: maxWidth,
			height: height,
			linkWhere:linkWhere,
			leftDefaultWhere: defaultWhere,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onSave(p, records);
				}
			}
		}).show();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.showForm(records[0].get(me.PKField));
	},

	/**显示表单*/
	showForm: function(id) {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onSearch();
					}
				}
			};

		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.client.deptgoods.Form', config).show();
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
					if(GoodsId == list[j].ReaGoods.Id) {
						hasLink = true;
						break;
					}
				}
				if(!hasLink) {
					var obj = {
						ReaGoodsId: GoodsId,
						ReaGoodsCName: CName
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
				GoodsCName: addIds.ReaGoodsCName,
				DeptName: me.OrgName,
				Visible: 1
			}
		};
		if(addIds.ReaGoodsId) {
			params.entity.ReaGoods = {
				Id: addIds.ReaGoodsId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			};
		}
		if(me.OrgId) {
			params.entity.DeptID = me.OrgId;
		}
		//创建者信息
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId) {
			params.entity.CreatorID = userId;
		}
		if(userName) {
			params.entity.CreatorName = userName;
		}
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
			url = JShell.System.Path.ROOT + me.selectUrl2.split('?')[0] +
			'?fields=ReaDeptGoods_ReaGoods_Id' +
			'&where=readeptgoods.ReaGoods.Id in(' + ids.join(',') + ')  and readeptgoods.DeptID=' + me.OrgId;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;

		var isError = false;
		var changedRecords = me.store.getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;

		if(len == 0) {
			//			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			me.updateOne(i, changedRecords[i]);
		}
	},
	/**修改信息*/
	updateOne: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: record.get('ReaDeptGoods_Id'),
				Memo: record.get('ReaDeptGoods_Memo'),
				Visible: record.get('ReaDeptGoods_Visible') ? 1 : 0,
				DispOrder: record.get('ReaDeptGoods_DispOrder')
			},
			fields: 'Id,Memo,Visible,DispOrder'
		});
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
			} else {
				me.saveErrorCount++;
				if(record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					me.onSearch();
				} else {
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		//查询货品包含选择部门的子孙节点
		var selectUrlName = me.selectUrl;
		//查询本部门
		if(!me.isSearchChildNode) {
			selectUrlName = me.selectUrl2;
		}
		var url = (selectUrlName.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + selectUrlName;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//查询货品包含选择部门的子孙节点
		if(me.isSearchChildNode && me.OrgId) {
			url += '&deptID=' + me.OrgId;
		}
		//查询本部门
		if(!me.isSearchChildNode) {
			if(me.OrgId) {
				me.defaultWhere = 'readeptgoods.DeptID=' + me.OrgId;
			}
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

		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}

		return url;
	}
});