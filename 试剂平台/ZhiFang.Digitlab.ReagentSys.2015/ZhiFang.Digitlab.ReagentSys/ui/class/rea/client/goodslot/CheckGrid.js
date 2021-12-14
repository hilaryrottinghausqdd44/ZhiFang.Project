/**
 * 批次信息
 * @author longfc
 * @version 2018-01-16
 */
Ext.define('Shell.class.rea.client.goodslot.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '货品批次信息',
	width: 680,
	height: 480,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaGoodsLot',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoodsLot',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLotByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**默认加载数据*/
	defaultLoad: false,
	/**是否单选*/
	checkOne: true,
	/**货品ID*/
	GoodsID: null,
	/**货品名称*/
	GoodsCName: null,
	CurLotNo: null,
	/**排序字段*/
	defaultOrderBy: [{
			property: 'ReaGoodsLot_InvalidDate',
			direction: 'ASC'
		},
		{
			property: 'ReaGoodsLot_DispOrder',
			direction: 'ASC'
		}
	],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function() {
				me.getView().update('');
			}
		});
		me.loadDataByGoodsID();
	},
	initComponent: function() {
		var me = this;
		if(!me.CurLotNo) me.CurLotNo = "";
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			value: me.CurLotNo,
			itemId: 'Search',
			emptyText: '批号',
			fields: ['reagoodslot.LotNo']
		};
		me.addEvents('isValid');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsLot_LotNo',
			text: '货品批号',
			flex: 1,
			maxWidth: 180,
			editor: {
				allowBlank: false
			}
		}, {
			dataIndex: 'ReaGoodsLot_ProdDate',
			text: '生产日期',
			width: 100,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			}
		}, {
			dataIndex: 'ReaGoodsLot_InvalidDate',
			text: '<b style="color:blue;">有效期</b>',
			width: 100,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			}
		}, {
			xtype: 'checkcolumn',
			dataIndex: 'ReaGoodsLot_Visible',
			text: '启用',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			dataIndex: 'ReaGoodsLot_DispOrder',
			text: '显示次序',
			width: 70,
			align: 'center',
			editor: {
				xtype: 'numberfield',
				minValue: 0
			}
		}, {
			dataIndex: 'ReaGoodsLot_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaGoodsLot_ReaGoods_Id',
			text: '货品主键ID',
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 45,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, rec) {
					var id = rec.get(me.PKField);
					if(id)
						return '';
					else
						return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					if(id) {
						JShell.Msg.alert("当前货品批号信息不允许删除！", null, 2000);
					} else {
						me.store.remove(rec);
						me.delCount++;
					}
				}
			}]
		}];
		columns.push({
			dataIndex: me.DelField,
			text: '',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if(value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if(value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if(msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}

				return v;
			}
		});
		return columns;
	},

	initButtonToolbarItems: function() {
		var me = this;
		//me.callParent(arguments);
		var items = [];
		if(me.hasRefresh) items.push('refresh', '-');
		if(me.hasAdd) items.push('add');
		if(me.hasEdit) items.push('edit');
		if(me.hasDel) items.push('del');
		if(me.hasSave) items.push('save', '-');
		if(me.hasAcceptButton) items.push('accept', '-');
		items.push({
			type: 'search',
			info: me.searchInfo
		});
		me.buttonToolbarItems = items;
	},
	/**删除按钮点击处理方法*/
	onDelClick: function(records) {
		var me = this;
		if(!records)
			records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		JShell.Msg.del(function(but) {
			if(but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			for(var i in records) {
				var id = records[i].get(me.PKField);
				if(id) {
					JShell.Msg.alert("批次信息保存成功！", null, 2000);
				} else {
					me.store.remove(records[i]);
					me.delCount++;
				}
			}
		});
	},
	onAddClick: function() {
		var me = this;
		me.createAddRec();
	},
	/**增加一行*/
	createAddRec: function() {
		var me = this;
		var obj = {
			"ReaGoodsLot_Id": "",
			"ReaGoodsLot_ReaGoods_Id": me.GoodsID,
			"ReaGoodsLot_LotNo": '',
			"ReaGoodsLot_ProdDate": '',
			"ReaGoodsLot_InvalidDate": '',
			"ReaGoodsLot_Visible": true,
			"ReaGoodsLot_DispOrder": me.store.getCount()
		};
		me.store.add(obj);
	},
	/**保存*/
	onSaveClick: function() {
		var me = this;
		var valid = me.saveValid();
		if(!valid) return;

		var isError = false;
		var changedRecords = me.store.getModifiedRecords(),
			len = changedRecords.length;
		if(len<=0) return;
		
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			//存在id 编辑
			if(changedRecords[i].get(me.PKField)) {
				me.updateOne(i, changedRecords[i]);
			} else {
				//不存在id 新增
				me.addOne(i, changedRecords[i]);
			}
		}

	},
	updateOne: function(index, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var Visible = record.get('ReaGoodsLot_Visible');
		if(Visible == false || Visible == "false") Visible = 0;
		if(Visible == "1" || Visible == "true" || Visible == true) Visible = 1;
		var params = {
			entity: {
				"Id": record.get('ReaGoodsLot_Id'),
				"LotNo": record.get('ReaGoodsLot_LotNo'),
				"DispOrder": record.get('ReaGoodsLot_DispOrder'),
				"Visible": Visible
			}
		};
		params.entity.ReaGoods = {
			Id: record.get('ReaGoodsLot_ReaGoods_Id')
		};
		if(record.get('ReaGoodsLot_InvalidDate')) {
			var isValid = JcallShell.Date.isValid(record.get('ReaGoodsLot_InvalidDate'));
			if(isValid) {
				params.entity.InvalidDate = JShell.Date.toServerDate(record.get('ReaGoodsLot_InvalidDate'));
			}
		}
		if(record.get('ReaGoodsLot_ProdDate')) {
			var isValid = JcallShell.Date.isValid(record.get('ReaGoodsLot_ProdDate'));
			if(isValid) {
				params.entity.ProdDate = JShell.Date.toServerDate(record.get('ReaGoodsLot_ProdDate'));
			}
		}

		params.fields = 'Id,Visible,InvalidDate,ProdDate,DispOrder';
		var entity = JcallShell.JSON.encode(params);
		setTimeout(function() {
			JShell.Server.post(url, entity, function(data) {
				if(data.success) {
					me.saveCount++;
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
				} else {
					record.set(me.DelField, false);
					record.set('ErrorInfo', data.msg);
					me.saveErrorCount++;
					record.commit();
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						me.onSearch();
					} else {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			}, false);
		}, 100 * index);

	},
	//新增数据
	addOne: function(index, record) {
		var me = this;
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var Visible = record.get('ReaGoodsLot_Visible');
		if(Visible == false || Visible == "false") Visible = 0;
		if(Visible == "1" || Visible == "true" || Visible == true) Visible = 1;
		var params = {
			entity: {
				"Visible": Visible,
				"LotNo": record.get('ReaGoodsLot_LotNo'),
				"DispOrder": record.get('ReaGoodsLot_DispOrder')
			}
		};
		if(record.get('ReaGoodsLot_InvalidDate')) {
			var isValid = JcallShell.Date.isValid(record.get('ReaGoodsLot_InvalidDate'));
			if(isValid) params.entity.InvalidDate = JShell.Date.toServerDate(record.get('ReaGoodsLot_InvalidDate'));
		}
		if(record.get('ReaGoodsLot_ProdDate')) {
			var isValid = JcallShell.Date.isValid(record.get('ReaGoodsLot_ProdDate'));
			if(isValid) params.entity.ProdDate = JShell.Date.toServerDate(record.get('ReaGoodsLot_ProdDate'));
		}
		params.entity.ReaGoods = {
			Id: record.get('ReaGoodsLot_ReaGoods_Id'),
			DataTimeStamp: [0, 0, 0, 0, 0, 0, 1, 0]
		};
		if(me.GoodsCName) {
			params.entity.GoodsCName = me.GoodsCName;
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
		setTimeout(function() {
			//提交数据到后台
			JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
				if(data.success) {
					record.set(me.DelField, true);
					record.commit();
					me.saveCount++;
				} else {
					record.set(me.DelField, false);
					record.set('ErrorInfo', data.msg);
					record.commit();
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						me.onSearch();
					} else {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			}, false);
		}, 100 * index);
	},
	//验证
	saveValid: function() {
		var me = this;
		var msgInfo = "";
		me.store.each(function(record) {
			var LotNo = record.get('ReaGoodsLot_LotNo');
			var ProdDate = record.get('ReaGoodsLot_ProdDate');
			var InvalidDate = record.get('ReaGoodsLot_InvalidDate');

			if(!LotNo || !ProdDate || !InvalidDate) {
				msgInfo = "货品批号,生产日期,有效期都不能为空!";
				return false;
			} else if(InvalidDate < ProdDate) {
				msgInfo = "货品批号为:" + LotNo + ",开始时间不能大于有效期!";
				return false;
			} else {
				me.store.findRecord('ReaGoodsLot_LotNo', LotNo);
			}
		});
		if(msgInfo) {
			JShell.Msg.alert(msgInfo, null, 2000);
			return false;
		} else {
			return true;
		}
	},
	/**根据货品id加载*/
	loadDataByGoodsID: function() {
		var me = this;
		if(me.GoodsID) {
			me.defaultWhere = 'reagoodslot.ReaGoods.Id=' + me.GoodsID;
			me.onSearch();
		}
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.load(null, true, autoSelect);
	}
});