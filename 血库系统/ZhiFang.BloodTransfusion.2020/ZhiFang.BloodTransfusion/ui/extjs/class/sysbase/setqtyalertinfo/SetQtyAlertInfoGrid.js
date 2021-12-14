/**
 * 库存预警明细表
 * @author xiehz
 * @version 2020-08-10
 */

Ext.define('Shell.class.sysbase.setqtyalertinfo.SetQtyAlertInfoGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '预警颜色',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertInfoByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodSetQtyAlertInfo',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSetQtyAlertInfoByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodSetQtyAlertInfo',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**血型编号ID*/
	aboNo: null,
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
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询框信息*/
	searchInfo: {
		width: 220,
		emptyText: '血型ABO名称',
		isLike: true,
		itemId: 'Search',
		fields: ['bloodsetqtyalertinfo.BloodABO.CName']
	},
	
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
		//创建插件网格才能修改
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '预警编号',
			width: 120,
			dataIndex: 'BloodSetQtyAlertInfo_Id',
			isKey: true,
			hideable: false
		},{
			text: '<b style="color:blue;">下限值</b>',
			dataIndex: 'BloodSetQtyAlertInfo_StoreLower',
			width: 90,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				xtype: 'numberfield'
			}	
		},{
			text: '<b style="color:blue;">上限值</b>',
			type:'int',
			dataIndex: 'BloodSetQtyAlertInfo_StoreUpper',
			width: 90,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				xtype: 'numberfield'
			}	
		},{
			text: '<b style="color:blue;">有效期报警天数</b>',
			type:'int',
			dataIndex: 'BloodSetQtyAlertInfo_BeforeWarningDay',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				xtype: 'numberfield'
			}			
		},{
			text: '<b style="color:blue;">显示次序</b>',
			type:'int',
			dataIndex: 'BloodSetQtyAlertInfo_DispOrder',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				xtype: 'numberfield'
			}
		},{
			text: '<b style="color:blue;">是否使用</b>',
			type: 'boolean',
			//xtype: 'checkcolumn', //修改boolean列
     		dataIndex:'BloodSetQtyAlertInfo_Visible',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
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
		return columns;
	},
	
	updateOneByVisible: function(index, id, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var StoreLower = rec.get('BloodSetQtyAlertInfo_StoreLower');
		var StoreUpper = rec.get('BloodSetQtyAlertInfo_StoreUpper');
		var DispOrder = rec.get('BloodSetQtyAlertInfo_DispOrder');
		var Visible = rec.get('BloodSetQtyAlertInfo_Visible');
		var BeforeWarningDay = rec.get('BloodSetQtyAlertInfo_BeforeWarningDay');
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				StoreLower: StoreLower,
				StoreUpper: StoreUpper,
				Visible: Visible,
				DispOrder: DispOrder,
				BeforeWarningDay: BeforeWarningDay
			},
			fields: 'Id,StoreLower, StoreUpper, Visible,DispOrder, BeforeWarningDay'
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
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
		    toolbar = me.getComponent("buttonsToolbar");
		    searchBtn = toolbar.getComponent("Search"); 
			search = searchBtn.getValue(),
			params = [];

		me.internalWhere = '';

		if (me.aboNo) {
			params.push('bloodsetqtyalertinfo.BloodABO.Id=' + me.aboNo);
		}
		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if (search) {
			if (me.internalWhere) {
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
		if (!me.aboNo) {
			JShell.Msg.error('血型编号不能为空');
			return;
		}
		me.load(null, true, autoSelect);
	},
    /**@overwrite 保存按钮点击处理方法*/
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
			me.updateOneByVisible(i, id, rec);
		}
	},
	
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		var defaultWhere = " bloodstyle.IsUse=1 "; 
		var linkWhere="";//关系表查询条件
		if(me.aboNo){
			linkWhere='bloodsetqtyalertinfo.BloodABO.Id=' + me.aboNo;
		}
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		JShell.Win.open('Shell.class.sysbase.setqtyalertinfo.choose.App', {
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
	
	/**保存关系数据*/
	onSave: function(p, records) {
		var me = this;

		if (records.length == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;

		for (var i in records) {
			me.onAddOneLink(p, records[i], i);
		}
	},
	
	/**新增关系数据*/
	onAddOneLink: function(p, record, index) {
		var me = this;
		var params = {
			entity: {
				BloodStyle: {
					Id: record.get('BloodStyle_Id'),
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				},
				BloodABO: {
					Id: me.aboNo,
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				},
				Visible: 1,
				StoreLower: 0,
				StoreUpper: 0,
				BeforeWarningDay: 0
			}
		};
		params = Ext.JSON.encode(params);
		var url = JShell.System.Path.ROOT + me.addUrl;
		setTimeout(function() {
			//提交数据到后台
			JShell.Server.post(url, params, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					p.close();
					//if (me.saveErrorCount == 0) me.onSearch();
					me.onSearch();
				}
			});
		}, 100 * index);
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
			classNameSpace: 'ZhiFang.Entity.BloodTransfusion', //类域
			className: 'UpdateOperationType', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='BloodSetQtyAlertInfo'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
})