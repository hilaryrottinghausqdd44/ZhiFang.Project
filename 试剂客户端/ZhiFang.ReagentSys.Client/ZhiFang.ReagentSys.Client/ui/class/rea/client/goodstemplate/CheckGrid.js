/**
 * @description 申请明细及订单明细模板
 * @author longfc
 * @version 2018-01-16
 */
Ext.define('Shell.class.rea.client.goodstemplate.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '明细模板选择列表',
	width: 310,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplateByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaChooseGoodsTemplate',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaChooseGoodsTemplate_DispOrder',
		direction: 'ASC'
	}],
	checkOne: true,
	/**模板类型*/
	TemplateType: null,
	/**所属部门Id*/
	DeptID: null,
	/**所属机构Id*/
	OrgID: null,

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += ' reachoosegoodstemplate.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '名称/简码',
			isLike: true,
			fields: ['reachoosegoodstemplate.ShortCode', 'reachoosegoodstemplate.CName']
		};
		//数据列
		me.columns = [{
			dataIndex: 'ReaChooseGoodsTemplate_CName',
			text: '名称',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaChooseGoodsTemplate_ShortCode',
			text: '简码',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaChooseGoodsTemplate_ContextJson',
			text: '内容',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaChooseGoodsTemplate_OrgName',
			text: '所属机构',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaChooseGoodsTemplate_DeptName',
			text: '所属部门',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaChooseGoodsTemplate_Id',
			text: 'ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>删除</b>"';
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onDelClick(rec);
				}
			}]
		}];

		me.callParent(arguments);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();

		if(!me.TemplateType) return false;

		switch(me.TemplateType) {
			case "ReaReqDtl":
				if(!me.DeptID) return false;
				me.defaultWhere=" reachoosegoodstemplate.IsUse=1 and reachoosegoodstemplate.SName='ReaReqDtl' and reachoosegoodstemplate.DeptID=" + me.DeptID;
				break;
			case "ReaOrderDtl":
				if(!me.OrgID) return false;
				me.defaultWhere="reachoosegoodstemplate.IsUse=1 and reachoosegoodstemplate.SName='ReaOrderDtl' and reachoosegoodstemplate.OrgID=" + me.OrgID;
				break;
			default:
				break;
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**删除按钮点击处理方法*/
	onDelClick: function(rec) {
		var me = this;
		JShell.Msg.del(function(but) {
			if(but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = 1;

			me.showMask(me.delText); //显示遮罩层
			var id = rec.get(me.PKField);
			me.delOneById(1, id);
		});
	}
});