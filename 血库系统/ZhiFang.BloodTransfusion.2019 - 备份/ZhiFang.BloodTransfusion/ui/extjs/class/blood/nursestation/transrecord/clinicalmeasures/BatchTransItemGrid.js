/**
 * 输血过程记录:输血过程记录项列表(临床处理措施)
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.clinicalmeasures.BatchTransItemGrid', {
	extend: 'Shell.class.blood.nursestation.transrecord.clinicalmeasures.TransItemGrid',

	title: '临床处理措施',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr?isPlanish=true',
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelBatchTransItemByClinicalMeasures',
	/**默认查询条件:临床处理措施*/
	defaultWhere: "bloodtransitem.ContentTypeID=3",
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr:null,
	//是否包含删除列
	hasDelCol: false,
	/**是否批量修改录入*/
	isEditBatch:true,
	
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.on({
			changeResult: function(p, data) {
				me.setItemTitle(data);
			}
		});
	},
	initComponent: function () {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	onBeforeLoad: function () {
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
		return url;
	},
	/**
	 * @description label处理
	 * @param {Object} batchSign 批量修改录入的数据标志
	 */
	setItemTitle: function(data) {
		var me = this;
		var item1=me.columns[5];//临床处理措施列;
		var batchSign = "" + data["batchSign"];
		var bColor = "";
		if (item1 && batchSign) {
			switch (batchSign) {
				case "1":
					//1:表示当前选择的发血血袋对应的记录项完全未作过登记,结果值全部为空;
					bColor = "";//橙色:#FFB800;灰石色(石板灰):#708090;金色:#FFD700
					break;
				case "2":
					//2:表示当前选择的发血血袋对应的记录项的结果值部分相同,部分不相同;
					bColor = "#FF4500";// 橙红色:#FF4500; 赤色:#FF5722
					break;
				case "3":
					//3:表示当前选择的发血血袋对应的记录项的结果值完全一致;
					bColor = "#66CDAA";//墨绿:#009688;中宝石碧绿:#66CDAA
					break;
				default:
					break;
			}
			if (bColor) {
				var fieldLabel = "临床处理措施";
				item1.setText(fieldLabel +'<span style="background-color:'+bColor+';">&nbsp;&nbsp;</span>');
			}
		}
	}
});
