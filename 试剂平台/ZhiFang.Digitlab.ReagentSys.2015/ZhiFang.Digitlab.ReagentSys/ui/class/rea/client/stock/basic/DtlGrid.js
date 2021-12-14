/**
 * 入库明细列表
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.basic.DtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '入库明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsInDtl',
	/**新增数据服务路径*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsInDtl',
	/**修改数据服务路径*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDtlByField',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
			property: 'ReaBmsInDtl_DispOrder',
			direction: 'ASC'
		},
		{
			property: 'ReaBmsInDtl_DataAddTime',
			direction: 'ASC'
		},
		{
			property: 'ReaBmsInDtl_GoodsCName',
			direction: 'ASC'
		}
	],

	/**默认选中*/
	autoSelect: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,

	/**主单信息*/
	DocInfo: {},
	/**默认选中*/
	autoSelect: false,

	/**是否可编辑*/
	canEdit: true,
	/**是否多选行*/
	checkOne: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update: function(store, record) {
				me.onPriceOrGoodsQtyChanged(record);
			}
		});
		me.on({
			beforeedit: function(editor, e) {
				return me.canEdit;
			}
		});
	},
	initComponent: function() {
		var me = this;
		if(me.canEdit == true) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		//只能点击复选框才能选中
		//		me.selModel = new Ext.selection.CheckboxModel({
		//			checkOnly: true
		//		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsInDtl_ReaGoods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			renderer:function(value, meta) {
				var v = "";
				if(value == "0"){
					v = "批条码";
					meta.style = "color:green;";
				}else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				}else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				
				meta.tdAttr = 'data-qtip="<b>' + v + '"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsCName',
			text: '产品名称',
			width: 140,
			renderer: function(value, meta, record) {
				meta.tdAttr = 'data-qtip="<b>' + value + '"';
				return value;
			}
		}, {
			dataIndex: 'ReaBmsInDtl_SerialNo',
			text: '条码号',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Price',
			text: '单价',
			width: 65,
			type: 'float',
			align: 'right',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SumTotal',
			sortable: false,
			text: '总计金额',
			align: 'right',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_InCount',
			text: '已入库数',
			width: 65,
			type: 'int',
			align: 'center',
			defaultRenderer: true
		},  {
			dataIndex: 'BmsCenSaleDtlConfirm_AcceptMemo',
			sortable: false,hidden:true,
			text: '异常信息',
			width: 80,
			hidden: true,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '备注',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hidden: true,
			hideable: true,
			menuDisabled: true,
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
				}
			}]
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsQty',
			text: '本次入库数量',
			type:'int',	sortable: false,
			editor:{xtype:'numberfield'},
		},{
			dataIndex: 'ReaBmsInDtl_StorageCName',sortable: false,width: 80,
			text: '库房',
			editor:{
				fieldLabel: '',
				emptyText: '必填项',
				allowBlank: false,
				name: 'ReaStorage_CName',
				itemId: 'ReaStorage_CName',
			    xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.confirm.set.storage.CheckGrid',
				classConfig: {
					title: '库房选择'
				},
				listeners: {
					check: function(p, record) {
						var records = me.getSelectionModel().getSelection();
						if(records.length !=1){
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						records[0].set('ReaBmsInDtl_StorageID',record ? record.get('ReaStorage_Id') : '');
					    records[0].set('ReaBmsInDtl_StorageCName',record ? record.get('ReaStorage_CName') : '');
					    records[0].set('ReaBmsInDtl_PlaceID','');
					    records[0].set('ReaBmsInDtl_PlaceCName','');
					    me.getView().refresh();
						p.close();
					}
				}
			}
		},{
			dataIndex: 'ReaBmsInDtl_PlaceCName',sortable: false,width: 80,
			text: '货架',
			editor:{
				fieldLabel: '',
				emptyText: '必填项',
				allowBlank: false,
				name: 'ReaPlace_CName',
				itemId: 'ReaPlace_CName',
			    xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.shelves.place.CheckGrid',
				classConfig: {
					title: '货架选择'
				},
				listeners: {
					check: function(p, record) {
						var records = me.getSelectionModel().getSelection();
						if(records.length !=1){
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
					    records[0].set('ReaBmsInDtl_PlaceID',record ? record.get('ReaPlace_Id') : '');
					    records[0].set('ReaBmsInDtl_PlaceCName',record ? record.get('ReaPlace_CName') : '');
					    me.getView().refresh();
						p.close();
					},
					beforetriggerclick:function(field){
						var records = me.getSelectionModel().getSelection();
						if(records.length !=1){
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						var StorageID=records[0].get('StorageID');
						var defaultWhere = StorageID ? 'reaplace.ReaStorage.Id=' + StorageID : '';
						field.classConfig.defaultWhere = defaultWhere;
						field.changeClassConfig({
							defaultWhere:defaultWhere
						});
		                field.createPicker()
					}
				}
			}
		},{
			dataIndex: 'ReaBmsInDtl_GoodsUnit',
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_LotNo',
			text: '产品批号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TaxRate',
			text: '税率',
			align: 'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsInDtl_StorageID',text: '库房ID',sortable: false,
			width: 80,hidden:true,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_PlaceID',text: '货架ID',sortable: false,
			width: 80,hidden:true,defaultRenderer: true
		}];
		for(var i = 0; i < columns.length; i++) {
			if(columns[i].editor) {
				columns[i].editor.listeners = {
					beforeedit: function(editor, e) {
						return me.canEdit;
					}
				}
			}
		}
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**总金额自动计算*/
	onPriceOrGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsInDtl_Price');
		var GoodsQty = record.get('ReaBmsInDtl_GoodsQty');
		var SumTotal = parseFloat(Price) * parseInt(GoodsQty);
		record.set('ReaBmsInDtl_SumTotal', SumTotal);
	},
	/**刷新数据*/
	onSearch: function() {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = true;
		this.load(null, true);
	},
	/**只看模式*/
	onSearchOnlyRead: function() {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = false;
		this.load(null, true);
	},
	/**获取数据错误信息*/
	getDataErrorMsg: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length,
			errorMsg = null;

		for(var i = 0; i < len; i++) {
			var rec = records[i],
				GoodsQty = rec.get('ReaBmsInDtl_GoodsQty'),
				AcceptCount = rec.get('ReaBmsInDtl_AcceptCount'),
				AccepterErrorMsg = rec.get('ReaBmsInDtl_AcceptMemo');

			if(GoodsQty == AcceptCount && AccepterErrorMsg) {
				errorMsg = '验收数量与供货数量一致时，不能填写异常信息，请删除该条异常信息后再操作！';
				break;
			} else if(AcceptCount > GoodsQty) {
				errorMsg = '验收数量不能大于供货数量，请修改后再操作！';
				break;
			} else if(AcceptCount < GoodsQty && !AccepterErrorMsg) {
				errorMsg = '验收数量小于供货数量时，必须填写异常信息，请修改后再操作！';
				break;
			}
		}
		return errorMsg;
	},
	deleteOne: function(record) {
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		var showMask = false;
		var id = record.get(me.PKField);
		if(!id || id == "-1") {
			me.delCount++;
			me.store.remove(record);
			if((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
				me.fireEvent('onDelAfter', me);
			}
		} else {
			if(showMask == false) {
				showMask = true;
				me.showMask(me.delText); //显示遮罩层
			}
			me.delOneById(record, 1, id);
		}
	},
	/**删除一条数据*/
	delOneById: function(record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount != 0) {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					} else {
						me.fireEvent('onDelAfter', me);
					}
				}
			});
		}, 100 * index);
	},

	onFullScreenClick: function() {
		var me = this;
		me.fireEvent('onFullScreenClick', me);
	}
});