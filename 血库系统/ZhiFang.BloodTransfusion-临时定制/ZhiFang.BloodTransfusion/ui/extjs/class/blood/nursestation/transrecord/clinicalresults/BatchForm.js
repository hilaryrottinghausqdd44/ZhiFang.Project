/**
 * 输血过程记录:批量修改--临床处理结果
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.clinicalresults.BatchForm', {
	extend: 'Shell.class.blood.nursestation.transrecord.clinicalresults.Form',

	title: '临床处理结果',
	formtype:"edit",
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr:null,
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchClinicalResultsByOutDtlIdStr?isPlanish=true',
	
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function () {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**根据选择的发血血袋明细ID加载数据*/
	load: function(outDtlIdStr) {
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
		if (!outDtlIdStr) return;
		
		me.outDtlIdStr = outDtlIdStr; //面板主键
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if (collapsed) {
			me.isCollapsed = true;
			return;
		}
		me.showMask(me.loadingText); //显示遮罩层
		url = (url.slice(0, 4) == 'http' ? '' : me.getPathRoot()) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&") + "outDtlIdStr=" + me.outDtlIdStr;
		url += '&fields=' + me.getStoreFields().join(',');
	
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				if (data.value) {
					//label处理
					var batchSign = "" + data["batchSign"];
					me.setItemLabel(batchSign);
					//data.value = JShell.Server.Mapping(data.value);
					me.lastData = me.changeResult(data.value);
					me.getForm().setValues(me.lastData);
				}
			} else {
				JShell.Msg.error(data.msg);
			}
			me.fireEvent('load', me, data);
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function () {
		var me = this,
			values = me.getForm().getValues();
		var dispOrder = 0;
		if (values.BloodTransItem_DispOrder) {
			dispOrder = values.BloodTransItem_DispOrder;
		}
		var entity = {
			"ContentTypeID": 5,// values.BloodTransItem_ContentTypeID,
			"DispOrder": dispOrder,
			"Visible": 1
		};
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		var recordTypeId = values.BloodTransItem_BloodTransRecordType_Id;
		if (recordTypeId) {
			entity.BloodTransRecordType = {
				Id: recordTypeId,
				DataTimeStamp: dataTimeStamp
			}
		}
		var typeItemId = values.BloodTransItem_BloodTransRecordTypeItem_Id;
		if (typeItemId) {
			entity.BloodTransRecordTypeItem = {
				Id: typeItemId,
				CName: values.BloodTransItem_BloodTransRecordTypeItem_CName,
				DataTimeStamp: dataTimeStamp
			}
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function (data) {
		var me = this;
		data=me.callParent(arguments);
		if (me.formtype == "edit") {
			data["BloodTransItem_Id"] = "";
		}
		return data;
	},
	/**
	 * @description label处理
	 * @param {Object} batchSign 批量修改录入的数据标志
	 */
	setItemLabel: function(batchSign) {
		var me = this;
		var item1 =me.getComponent("BloodTransItem_BloodTransRecordTypeItem_CName");
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
				var fieldLabe = item1.getFieldLabel();
				item1.setFieldLabel(fieldLabe +'<span style="background-color:'+bColor+';">&nbsp;&nbsp;</span>');
			}
		}
	}
});
