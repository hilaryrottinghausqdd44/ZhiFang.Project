/**
 * 基础对照列表
 * @author longfc	
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.interfacemaping.MapingVOGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '基础对照列表',

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBDictMapingVOByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCInterfaceMapingByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCInterfaceMaping',

	/**隐藏删除列*/
	hideDelColumn: false,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},
	/**查询栏默认查询值*/
	searchValue: "",
	/**对照字典的开发商代码*/
	deveCode: "",
	/**如果deveCode等于BDict,useCode为字典类型编码(作为过滤字典数据使用)*/
	useCode: "",
	/**对照字典编码Id值*/
	objectTypeId: "",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//数据列
		//me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '对照关系Id',
			dataIndex: 'BDictMapingVO_SCInterfaceMaping_Id',
			width: 165,
			isKey: true,
			hidden: true
		}, {
			text: '字典编码',
			dataIndex: 'BDictMapingVO_SCInterfaceMaping_BobjectID',
			width: 165,
			doSort: function(state) {
				var field = "SCInterfaceMaping_BobjectID";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '<b style="color:blue;">对照码</b>',
			dataIndex: 'BDictMapingVO_SCInterfaceMaping_MapingCode',
			width: 140,
			doSort: function(state) {
				var field = "SCInterfaceMaping_MapingCode";
				me.store.sort({
					property: field,
					direction: state
				});
			},
			editor: {}
		}, {
			text: '<b style="color:blue;">显示次序</b>',
			dataIndex: 'BDictMapingVO_SCInterfaceMaping_DispOrder',
			width: 65,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			doSort: function(state) {
				var field = "SCInterfaceMaping_DispOrder";
				me.store.sort({
					property: field,
					direction: state
				});
			},
			editor: {
				xtype: 'numberfield'
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}];
		columns.push(me.createDelColumn());
		return columns;
	},
	createDelColumn: function() {
		var me = this;
		return {
			dataIndex: me.DelField,
			text: '操作结果',
			width: 60,
			sortable: false,
			hidden: me.hideDelColumn,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if (value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if (value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if (msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}

				return v;
			}
		};
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		url += "&deveCode=" + me.deveCode;
		if (me.useCode) url += "&useCode=" + me.useCode;
		if (me.objectTypeId) {
			url += "&objectTypeId=" + me.objectTypeId;
			url += "&mapingWhere=scinterfacemaping.BobjectType.Id=" + me.objectTypeId;
		}
		return url;
	},
	/**保存按钮处理*/
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
			me.updateOne(i, rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var dispOrder = rec.get('BDictMapingVO_SCInterfaceMaping_DispOrder');
		var mapingCode = rec.get('BDictMapingVO_SCInterfaceMaping_MapingCode');
		if (!dispOrder) dispOrder = index;
		var entity = {
			Id: id,
			MapingCode: mapingCode,
			DispOrder: dispOrder
		};
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";

		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		var params = Ext.JSON.encode({
			empID:empID,
			empName:empName,
			entity: entity,
			fields: 'Id,MapingCode,DispOrder'
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
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.ApplyGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.WebAssist', //类域
			className: 'UpdateOperationType', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='SCInterfaceMaping'"
		};
		var win = JShell.Win.open('Shell.class.assist.scoperation.SCOperation', config);
		win.show();
	}
});
