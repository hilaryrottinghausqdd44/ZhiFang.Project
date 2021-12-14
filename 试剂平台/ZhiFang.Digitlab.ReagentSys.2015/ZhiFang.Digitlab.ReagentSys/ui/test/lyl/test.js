Ext.onReady(function() {
	Ext.QuickTips.init();
	Ext.Loader.setConfig({
		enabled: true
	});
	Ext.Loader.setPath('Ext.zhifangux', getRootPath() + '/ui/zhifangux');
	Ext.define('itemList', {
		extend: 'Ext.zhifangux.GridPanel',
		alias: 'widget.itemList',
		title: '列表',
		width: 500,
		height: 400,
		objectName: 'BCalcFormula',
		defaultLoad: true,
		defaultWhere: '',
		sortableColumns: true,
		isTrue: false,
		selectUrl: getRootPath() + '/MEService.svc/ME_UDTO_SearchBCalcFormulaByHQL?isPlanish=true&fields=BCalcFormula_ItemAllItem_Id',
		initComponent: function() {
			var me = this;
			Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
			me.url = getRootPath() + '/MEService.svc/ME_UDTO_SearchBCalcFormulaByHQL?isPlanish=true&fields=BCalcFormula_Id,BCalcFormula_ItemAllItem_Id,BCalcFormula_ItemAllItem_DataTimeStamp,BCalcFormula_ItemAllItem_CName,BCalcFormula_ItemAllItem_Shortcode,BCalcFormula_ItemAllItem_SName,BCalcFormula_ItemAllItem_EName,BCalcFormula_ItemAllItem_DispOrder';
			me.searchArray = ['bcalcformula.ItemAllItem.CName', 'bcalcformula.ItemAllItem.Shortcode', 'bcalcformula.ItemAllItem.EName'];
			me.store = me.createStore({
				fields: ['BCalcFormula_Id', 'BCalcFormula_ItemAllItem_Id', 'BCalcFormula_ItemAllItem_DataTimeStamp', 'BCalcFormula_ItemAllItem_CName', 'BCalcFormula_ItemAllItem_Shortcode', 'BCalcFormula_ItemAllItem_SName', 'BCalcFormula_ItemAllItem_EName', 'BCalcFormula_ItemAllItem_DispOrder'],
				url: 'MEService.svc/ME_UDTO_SearchBCalcFormulaByHQL?isPlanish=true&fields=BCalcFormula_Id,BCalcFormula_ItemAllItem_Id,BCalcFormula_ItemAllItem_DataTimeStamp,BCalcFormula_ItemAllItem_CName,BCalcFormula_ItemAllItem_Shortcode,BCalcFormula_ItemAllItem_SName,BCalcFormula_ItemAllItem_EName,BCalcFormula_ItemAllItem_DispOrder',
				remoteSort: false,
				sorters: [],
				PageSize: 5e3,
				hasCountToolbar: true,
				buffered: false,
				leadingBufferZone: null
			});
			me.store.on({
				load: function(s, records, su) {
					if (records.length > 0) {
						me.store = me.getArray(s, records);
						var count = me.store.getCount();
						me.setCount(count);
					}
				}
			});
			me.getArray = function(s, arr) {
				var a = {},
					b = {};
				var len = arr.length;
				for (var i = 0; i < len; i++) {
					if (typeof a[arr[i].get('BCalcFormula_ItemAllItem_Id')] == 'undefined') {
						a[arr[i].get('BCalcFormula_ItemAllItem_Id')] = 1;
						b[arr[i]] = 1;
					} else {
						s.remove(arr[i]);
					}
				}
				return s;
			};
			me.defaultColumns = [{
				text: '主键ID',
				dataIndex: 'BCalcFormula_Id',
				width: 100,
				sortable: false,
				hidden: true,
				hideable: true,
				align: 'left'
			}, {
				text: '主键ID',
				dataIndex: 'BCalcFormula_ItemAllItem_Id',
				width: 100,
				sortable: false,
				hidden: true,
				hideable: true,
				align: 'left'
			}, {
				text: '时间戳',
				dataIndex: 'BCalcFormula_ItemAllItem_DataTimeStamp',
				width: 100,
				sortable: false,
				hidden: true,
				hideable: true,
				align: 'left'
			}, {
				text: '项目名称',
				dataIndex: 'BCalcFormula_ItemAllItem_CName',
				width: 140,
				hideable: true,
				align: 'left'
			}, {
				text: '快捷码',
				dataIndex: 'BCalcFormula_ItemAllItem_Shortcode',
				width: 100,
				hidden: true,
				hideable: true,
				align: 'left'
			}, {
				text: '简称',
				dataIndex: 'BCalcFormula_ItemAllItem_SName',
				width: 100,
				hidden: false,
				hideable: true,
				align: 'left'
			}, {
				text: '英文名称',
				dataIndex: 'BCalcFormula_ItemAllItem_EName',
				width: 100,
				hideable: true,
				align: 'left'
			}, {
				text: '显示次序',
				dataIndex: 'BCalcFormula_ItemAllItem_DispOrder',
				width: 80,
				hideable: true,
				align: 'left'
			}];
			me.columns = me.createColumns();
			me.dockedItems = [{
				itemId: 'pagingtoolbar',
				xtype: 'toolbar',
				dock: 'bottom',
				items: [{
					xtype: 'label',
					itemId: 'count',
					text: '共0条'
				}]
			}, {
				xtype: 'toolbar',
				itemId: 'buttonstoolbar',
				dock: 'top',
				items: [{
					type: 'refresh',
					itemId: 'refresh',
					text: '更新',
					iconCls: 'build-button-refresh',
					handler: function(but, e) {
						var com = but.ownerCt.ownerCt;
						com.load(true);
					}
				}, {
					type: 'add',
					itemId: 'add',
					name: 'add',
					text: '新增计算项目',
					iconCls: 'build-button-add',
					handler: function(but, e) {
						me.showFormWin('add', -1, null);
					}
				}, {
					type: 'del',
					itemId: 'del',
					text: '删除计算项目',
					iconCls: 'build-button-delete',
					handler: function(but, e) {
						var records = me.getSelectionModel().getSelection();
						if (records.length == 1) {
							Ext.Msg.confirm('提示', '确定要删除吗？', function(button) {
								var createFunction = function(id) {
									var f = function() {
										var rowIndex = me.store.find('BCalcFormula_Id', id);
										me.deleteIndex = rowIndex;
										me.load(true);
										me.fireEvent('delClick');
									};
									return f;
								};
								if (button == 'yes') {
									for (var i in records) {
										var id = records[i].get('BCalcFormula_Id');
										if (id != null && id != '' || id != 'undefined' && id != undefined) {
											var callback = createFunction(id);
											me.deleteInfo(id, callback);
										}
									}
								}
							});
						} else {
							alertInfo('请选择数据进行操作！');
						}
					}
				}, '->', {
					xtype: 'textfield',
					itemId: 'searchText',
					width: 120,
					emptyText: '项目名称/快捷码/英文名称',
					listeners: {
						render: function(input) {
							new Ext.KeyMap(input.getEl(), [{
								key: Ext.EventObject.ENTER,
								fn: function() {
									me.search();
								}
							}]);
						}
					}
				}, {
					xtype: 'button',
					text: '查询',
					iconCls: 'search-img-16 ',
					tooltip: '按照项目名称/快捷码/英文名称进行查询',
					handler: function(button) {
						me.search();
					}
				}]
			}];
			me.deleteInfo = function(id, callback) {
				var url = getRootPath() + '/MEService.svc/ME_UDTO_DelBCalcFormula?id=' + id;
				var c = function(text) {
					var result = Ext.JSON.decode(text);
					if (result.success) {
						if (Ext.typeOf(callback) == 'function') {
							callback();
						}
					}
				};
				getToServer(url, c);
			};
			me.addEvents('delClick');
			this.callParent(arguments);
		},
		showFormWin: function(type, id, record) {
			var me = this;
			Ext.Loader.setConfig({
				enabled: true
			});
			Ext.Loader.setPath('Ext.zhifangux.GridPanel', getRootPath() + '/ui/zhifangux/GridPanel.js');
			Ext.Loader.setPath('Ext.databasemanage.calcformula.allitemList', getRootPath() + '/ui/databasemanage/class/calcformula/allitemList.js');
			var maxHeight = document.body.clientHeight * .98;
			var maxWidth = document.body.clientWidth * .98;
			var panelParams = {
				type: type,
				maxWidth: maxWidth,
				width: maxHeight + 150,
				height: maxHeight - 220,
				dataId: id,
				title: '新增计算项目',
				selectionRecord: record,
				modal: true,
				floating: true,
				closable: true,
				resizable: true
					//				closeAction:'hide'
			};
			var panel = Ext.create('Ext.databasemanage.calcformula.allitemList', panelParams, null, null);
			if (panel.height > maxHeight) {
				panel.height = maxHeight;
			}
			panel.show();
			panel.on({
				addClick: function() {
					var records = panel.getSelectionModel().getSelection();
					if (records.length > 0) {
						me.getIsAdd(records);
					}
					if (me.isTrue == true) {
						alertInfo('保存成功！');
						me.load(true);
						me.isTrue = false;
					}
				}
			});
		},
		getIsAdd: function(records) {
			var me = this;
			var obj = {};
			var itemobj = {};
			for (var i in records) {
				var isAdd = false;
				var record = records[i];
				var Id = record.get('ItemAllItem_Id');
				var cname = record.get('ItemAllItem_CName');
				var hqlWhere = 'bcalcformula.ItemAllItem.Id=' + Id;
				var url = me.selectUrl + '&where=' + hqlWhere;
				var getcallback = function(text) {
					var data = Ext.JSON.decode(text);
					if (data.success) {
						if (data.ResultDataValue && data.ResultDataValue != '') {
							data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g, '');
							var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
							var count = ResultDataValue.count;
							if (count > 0) {
								isAdd = false;
							} else {
								isAdd = true;
							}
						} else {
							isAdd = true;
						}
					}
				};
				getToServer(url, getcallback, false);
				if (isAdd && isAdd == true) {
					var DataTimeStamp = record.get('ItemAllItem_DataTimeStamp');
					var DataTimeStampstr = DataTimeStamp.split(',');
					itemobj = {
						Id: Id,
						DataTimeStamp: DataTimeStampstr
					};
					obj = {
						entity: {
							Id: '-1',
							LabID: '0',
							CalcFormula: '',
							ItemAllItem: itemobj
						}
					};
					var params = Ext.JSON.encode(obj);
					var addUrl = getRootPath() + '/MEService.svc/ME_UDTO_AddBCalcFormula';
					me.saveToTable(addUrl, params);
				}
			}
		},
		saveToTable: function(url, strobj) {
			var me = this;
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				async: false,
				url: url,
				params: strobj,
				method: 'POST',
				timeout: 3000,
				success: function(response, opts) {
					var result = Ext.JSON.decode(response.responseText);
					if (result.success) {
						me.isTrue = true;
					} else {
						me.isTrue = false;
						Ext.Msg.alert('提示', '保存失败！');
					}
				},
				failure: function(response, options) {
					Ext.Msg.alert('提示', '保存失败！');
				}
			});
		}
	});
	var viewport = Ext.create('Ext.Viewport', {
		width: 400,
		height: 300,
		autoScroll: true,
		items: [{
			xtype: 'itemList',
			itemId: 'test1'
		}]
	});
});