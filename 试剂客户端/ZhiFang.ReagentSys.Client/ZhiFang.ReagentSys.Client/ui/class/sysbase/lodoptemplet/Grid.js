/**
 * lodop模板维护
 * @author longfc
 * @version 2019-09-18
 */
Ext.define('Shell.class.sysbase.lodoptemplet.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],

	title: '模板列表 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBLodopTempletByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateBLodopTempletByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelBLodopTemplet',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: false,
	//selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	//hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	/**客户端电脑上的打印机集合信息*/
	PrinterList: [],
	PagSizeList: [],
	/**一维条码模板信息*/
	BarcodeModel: null,
	/**一维条码模板集合信息*/
	BarcodeModelList: [],
	/**二维条码模板信息*/
	QRCodeModel: null,
	/**二维条码模板集合信息*/
	QRCodeModelList: [],
	/**默认选择条码类型*/
	DefaultBarCodeType: '2',
	/**默认条码模板值*/
	DefaultBarcodeModel: "",
	/**默认选择的打印机*/
	DefaultPrinter: "",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onprint','modelchange','copyclick');
		//加载一维条码模板组件
		me.BarCodeModel = me.BarCodeModel || Ext.create('Shell.class.rea.client.printbarcode.BarCodeModel');
		me.BarcodeModelList = me.BarCodeModel.getModelList();
		//加载二维条码模板组件
		me.QRCodeModel = me.QRCodeModel || Ext.create('Shell.class.rea.client.printbarcode.QRCodeModel');
		me.QRCodeModelList = me.QRCodeModel.getModelList();
		//查询框信息
		me.searchInfo = {
			width: 125,
			emptyText: '名称',
			isLike: true,
			fields: ['blodoptemplet.CName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '模板名称',
			dataIndex: 'BLodopTemplet_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '模板代码',
			dataIndex: 'BLodopTemplet_TemplateCode',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			// defaultRenderer: true
			// 由于该列中数据存在</br>导致该单元格长度会和内容一致，不好看
			renderer: function(value, meta, record) {
				var v = record.get('BLodopTemplet_TemplateCode').replace(/<\/br>/g, "");
				return v;
				
				
			}
		}, {
			text: '模板类型',
			dataIndex: 'BLodopTemplet_TypeCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = "";
				if (value == "1") {
					v = "入库一维条码";
					meta.style = "color:orange;";
				} else if (value == "2") {
					v = "入库二维条码";
					meta.style = "color:black;";
				} else if (value == "3") {
					v = "其他模板";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '纸张选择',
			dataIndex: 'BLodopTemplet_PaperType',
			width: 70,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '打印方向',
			dataIndex: 'BLodopTemplet_PrintingDirection',
			width: 100,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = "";
				if (value == "0") {
					v = "按打印机缺省设置";
					meta.style = "color:green;";
				} else if (value == "1") {
					v = "(正)向打印，固定纸张";
					meta.style = "color:orange;";
				} else if (value == "2") {
					v = "横向打印，固定纸张";
					meta.style = "color:black;";
				} else if (value == "3") {
					v = "纵(正)向打印,高度自适应";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '纸宽',
			dataIndex: 'BLodopTemplet_PaperWidth',
			width: 70,
			defaultRenderer: true,
			align: 'center'
		}, {
			text: '纸高',
			dataIndex: 'BLodopTemplet_PaperHigh',
			width: 70,
			defaultRenderer: true,
			align: 'center'
		}, {
			text: '宽高单位',
			dataIndex: 'BLodopTemplet_PaperUnit',
			width: 70,
			defaultRenderer: true,
			align: 'center'
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'BLodopTemplet_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '次序',
			dataIndex: 'BLodopTemplet_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}, {
			text: '主键ID',
			dataIndex: 'BLodopTemplet_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get('BLodopTemplet_IsUse');
			me.updateOneByIsUse(i, id, IsUse);
		}
	},
	updateOneByIsUse: function(index, id, IsUse) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsUse: IsUse
			},
			fields: 'Id,IsUse'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if (items.length == 0) {
			if (me.hasRefresh) items.push('refresh');
			if (me.hasAdd) items.push('add');
			if (me.hasEdit) items.push('edit');
			if (me.hasDel) items.push('del');
			if (me.hasShow) items.push('show');
			if (me.hasSave) items.push('save');
			items.push({
				iconCls: 'button-add',
				text: '复制新增',
				tooltip: '复制选择模板新增',
				itemId: 'btnCopy',
				handler: function(btn, e) {
					me.onCopyClick(1);
				}
			});
			items.push('-', {
				fieldLabel: '',
				emptyText: '打印机选择',
				xtype: 'uxSimpleComboBox',
				itemId: 'PrinterList',
				width: 145,
				labelWidth: 0,
				labelAlign: 'right',
				data: me.createPrinterList(),
				value: me.DefaultPrinter,
				listeners: {
					change: function(field, newValue) {
						//me.setDefaultPrinter(newValue);
					}
				}
			});
			items.push({
				fieldLabel: '',
				emptyText: '',
				xtype: 'uxSimpleComboBox',
				itemId: 'BarCodeType',
				width: 80,
				labelWidth: 0,
				xtype: 'uxSimpleComboBox',
				value: me.DefaultBarCodeType,
				hasStyle: true,
				data: [
					['1', '一维条码', 'color:orange;'],
					['2', '二维条码', 'color:green;']
				],
				listeners: {
					change: function(field, newValue) {
						//me.setDefaultBarCodeType(newValue);
						me.modelTypeLoadData(newValue);
					}
				}
			}, {
				fieldLabel: '',
				emptyText: '模板类型',
				xtype: 'uxSimpleComboBox',
				itemId: 'ModelType',
				width: 150,
				labelWidth: 0,
				data: me.DefaultBarCodeType == "2" ? me.QRCodeModelList : me.BarcodeModelList,
				//value: me.DefaultBarcodeModel,
				listeners: {
					change: function(field, newValue) {
						me.fireEvent('modelchange', newValue);
						//me.setDefaultBarcodeModel(newValue);
					}
				}
			});
			items.push({
				iconCls: 'button-print',
				text: '设计',
				tooltip: '设计模板',
				itemId: 'btnDesign',
				handler: function(btn, e) {
					me.onPrintClick(1);
				}
			}, {
				iconCls: 'button-print',
				text: '预览',
				tooltip: '预览打印',
				itemId: 'btnPreview',
				handler: function(btn, e) {
					me.onPrintClick(2);
				}
			}, {
				iconCls: 'button-print',
				text: '打印',
				hidden: true,
				tooltip: '直接打印',
				itemId: 'btnPrint',
				handler: function(btn, e) {
					me.onPrintClick(3);
				}
			});
			if (me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**
	 * 工具栏按钮是否启用/禁用设置
	 * @param {Object} bo
	 */
	setEnableControl: function(bo) {
		var me = this;
		var enable = bo === false ? false : true;
		var items = ["PrinterList", "BarCodeType", "ModelType", "ModelType","btnCopy", "btnDesign", "btnPreview", "btnPrint"];

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			me.getComponent('buttonsToolbar').getComponent(items[i])[enable ? 'enable' : 'disable']();
		}
	},
	/**
	 * 条码类型选择后,条码模板数据加载处理
	 * @param {Object} newValue
	 */
	modelTypeLoadData: function(newValue) {
		var me = this;
		var modelType = me.getComponent('buttonsToolbar').getComponent('ModelType');
		switch (newValue) {
			case "1":
				modelType.getStore().loadData(me.BarcodeModelList);
				modelType.setValue(me.BarcodeModelList[0][0]);
				break;
			default:
				modelType.getStore().loadData(me.QRCodeModelList);
				modelType.setValue(me.QRCodeModelList[0][0]);
				break;
		}
	},
	getCLodop: function(type) {
		var me = this;
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		var LODOP = me.Lodop.getLodop(true);
		if (!LODOP) {
			//JShell.Msg.error("LODOP打印控件获取出错!");
			return;
		}
		return LODOP;
	},
	/**
	 * 获取客户端电脑上的打印机集合信息
	 * */
	createPrinterList: function() {
		var me = this;
		var printerList = [];
		var LODOP = me.getCLodop();
		if (!LODOP || !CLODOP) return printerList;
		var iCount = 0;
		if (CLODOP)
			iCount = CLODOP.GET_PRINTER_COUNT();
		var iIndex = 0;
		for (var i = 0; i < iCount; i++) {
			printerList.push([iIndex, CLODOP.GET_PRINTER_NAME(i)]);
			iIndex++;
		}
		me.PrinterList = printerList;
		return printerList;
	},
	/**
	 * 获取当前选择的打印机
	 */
	getSelectedPrintIndex: function() {
		var me = this;
		var selectedPrintIndex = -1;
		var printerList = me.getComponent('buttonsToolbar').getComponent('PrinterList')
		if (printerList) {
			selectedPrintIndex = printerList.getValue();
		} else selectedPrintIndex = -1;
		return selectedPrintIndex;
	},
	/**
	 * 获取纸张集合信息
	 */
	createPagSizeList: function(selectedPrintIndex1) {
		var me = this;
		var pagSizeList = [];
		var LODOP = me.getCLodop();
		if (!LODOP || !CLODOP) return pagSizeList;
		var selectedPrintIndex = selectedPrintIndex1;
		if (!selectedPrintIndex) selectedPrintIndex = me.getSelectedPrintIndex();
		var strPageSizeList = LODOP.GET_PAGESIZES_LIST(selectedPrintIndex, "\n");
		var options = strPageSizeList.split("\n");
		for (i in options) {
			pagSizeList.push([lists[i], lists[i]]);
		}
		return pagSizeList;
	},
	/**打印事件*/
	onPrintClick: function(type) {
		var me = this;
		me.fireEvent('onprint', type);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('editclick', me, records[0]);
	},
	onCopyClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('copyclick', me, records[0]);
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.setEnableControl(false);
	},
	/**获取选择模板实体信息*/
	getSelectModel: function() {
		var me = this;
		//条码类型:一维条码:1;二维条码:2;
		var barCodeType = me.getComponent('buttonsToolbar').getComponent('BarCodeType').getValue();
		//模板类型
		var modelType = me.getComponent('buttonsToolbar').getComponent('ModelType').getValue();
		//Lodop打印内容字符串数组
		var LodopStr = [];
		var curModel = null;
		if (barCodeType == "1") {
			curModel = me.BarCodeModel.getSelectModel(modelType);
		} else {
			curModel = me.QRCodeModel.getSelectModel(modelType);
		}

		return curModel;
	},
	/**获取选择模板类型的打印内容信息*/
	getPrintData: function() {
		var me = this;
		var printData = "";
		var curModel = me.getSelectModel();
		if (curModel) {
			printData = curModel.Content;
		}
		return printData;
	},
	/**获取打印机选择*/
	getPrinter: function() {
		var me = this;
		return me.getComponent('buttonsToolbar').getComponent('PrinterList');
	}
});
