/**
 * 输血过程记录:批量修改--输血过程记录项列表(不良反应症状)
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.adversereaction.BatchTransItemGrid', {
	extend: 'Shell.class.blood.nursestation.transrecord.adversereaction.TransItemGrid',

	title: '不良反应症状',

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr?isPlanish=true',
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBatchTransItemByAdverseReactions',
	/**默认查询条件:不良反应选择项*/
	defaultWhere: "bloodtransitem.ContentTypeID=4",
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr: null,
	//当前选择的不良反应分类ID
	recordTypeId:"",
	//是否包含删除列
	hasDelCol: false,
	/**是否批量修改录入*/
	isEditBatch:true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodTransItem_DispOrder',
		direction: 'ASC'
	}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if (!me.outDtlIdStr) return false;

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	loadData: function (outDtlIdStr) {
		var me = this;
		me.outDtlIdStr = outDtlIdStr;
		me.onSearch();
	},
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		url=url+"&outDtlIdStr="+me.outDtlIdStr;
		url=url+"&recordTypeId="+me.recordTypeId;
		return url;
	}
});
