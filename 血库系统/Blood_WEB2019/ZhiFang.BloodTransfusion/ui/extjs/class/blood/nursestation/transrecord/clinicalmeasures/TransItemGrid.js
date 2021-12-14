/**
 * 输血过程记录:输血过程记录项列表(临床处理措施)
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.clinicalmeasures.TransItemGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '临床处理措施',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasPagingtoolbar: true,
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemByHQL?isPlanish=true',
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransItem',
	/**默认查询条件:临床处理措施*/
	defaultWhere: "bloodtransitem.ContentTypeID=3",
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodTransItem_DispOrder',
		direction: 'ASC'
	}],
	//输血过程记录主单ID
	PK: null,
	//是否包含删除列
	hasDelCol: false,
	/**后台排序*/
	remoteSort: false,
	/**是否批量修改录入*/
	isEditBatch:false,
	
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function () {
		var me = this;
		me.addEvents('onDelBatchClick');
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function () {
		var me = this;

		var columns = [];
		if (me.hasDelCol == true) {
			columns.push({
				xtype: 'actioncolumn',
				text: '删除',
				align: 'center',
				style: 'font-weight:bold;color:white;background:orange;',
				width: 40,
				hideable: false,
				sortable: false,
				items: [{
					getClass: function (v, meta, record) {
						meta.tdAttr = 'data-qtip="<b>删除</b>"';
						return 'button-del hand';
						/*
						var id = record.get("BloodTransItem_Id");
						if (id == "" || id == "-1") {
							meta.tdAttr = 'data-qtip="<b>删除</b>"';
							return 'button-del hand';
						} else {
							return '';
						}
						*/
					},
					handler: function (grid, rowIndex, colIndex) {
						JShell.Msg.confirm({
							title: '<div style="text-align:center;">删除操作确认</div>',
							msg: '请确认是否删除?',
							closable: true
						}, function (but, text) {
							if (but != "ok") return;
							var rec = grid.getStore().getAt(rowIndex);
							//me.store.remove(rec);
							if(me.isEditBatch==true){
								me.fireEvent('onDelBatchClick', me);
							}else{
								me.deleteOne(rec);
							}
						});
					}
				}]
			});
		}
		columns.push({
			text: '内容分类ID',
			dataIndex: 'BloodTransItem_ContentTypeID',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '输血记录主单ID',
			dataIndex: 'BloodTransItem_BloodTransForm_Id',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '处理措施记录项ID',
			dataIndex: 'BloodTransItem_BloodTransRecordTypeItem_Id',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '临床处理措施',
			dataIndex: 'BloodTransItem_BloodTransRecordTypeItem_CName',
			width: 90,
			flex: 1,
			defaultRenderer: true
		},{
			text: '所属记录项分类字典ID',
			dataIndex: 'BloodTransItem_BloodTransRecordType_Id',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '所属记录项分类字典名称',
			dataIndex: 'BloodTransItem_BloodTransRecordType_CName',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '次序',
			dataIndex: 'BloodTransItem_DispOrder',
			width: 70,
			hidden: true,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}, {
			text: '主键ID',
			dataIndex: 'BloodTransItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
		});
		return columns;
	},
	onBeforeLoad: function () {
		var me = this;
		me.store.removeAll();
		if (!me.PK) return false;

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	loadData: function (id) {
		var me = this;
		me.PK = id;
		var hql = "bloodtransitem.BloodTransForm.Id=" + id;
		me.externalWhere = hql;
		me.onSearch();
	},
	deleteOne: function (record) {
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		var showMask = false;
		var id = record.get(me.PKField);
		if (!id || id == "-1") {
			me.delCount++;
			me.store.remove(record);
		} else {
			if (showMask == false) {
				showMask = true;
				me.showMask(me.delText); //显示遮罩层
			}
			me.delOneById(record, 1, id);
		}
	},
	/**@description 删除一条数据*/
	delOneById: function (record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		setTimeout(function () {
			JShell.Server.get(url, function (data) {
				if (data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount != 0) {
						JShell.Msg.error('删除失败！');
					} else {
					}
				}
			});
		}, 100 * index);
	}
});
