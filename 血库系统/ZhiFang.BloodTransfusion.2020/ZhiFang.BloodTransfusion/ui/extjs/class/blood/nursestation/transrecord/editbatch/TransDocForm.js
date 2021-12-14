/**
 * 输血过程记录:批量修改--主单信息
 * @author longfc
 * @version 2020-03-23
 */
Ext.define('Shell.class.blood.nursestation.transrecord.editbatch.TransDocForm', {
	extend: 'Shell.class.blood.nursestation.transrecord.transdoc.DocForm',

	title: '输血过程记录信息',
	formtype: "edit",
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr:null,
	//当前选中发血血袋记录集合
	outDtlRrecords: [],
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormByOutDtlIdStr?isPlanish=true',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate);
		var hasAdverseReactions = values.BloodTransForm_HasAdverseReactions;
		if (hasAdverseReactions == true || hasAdverseReactions == 1) {
			hasAdverseReactions = 1;
		} else {
			hasAdverseReactions = 0;
		}
		var entity = {
			Id:-2,
			Visible: 1,
			BeforeCheck1: values.BloodTransForm_BeforeCheck1,
			BeforeCheck2: values.BloodTransForm_BeforeCheck2,
			TransCheck1: values.BloodTransForm_TransCheck1,
			TransCheck2: values.BloodTransForm_TransCheck2,
			HasAdverseReactions: hasAdverseReactions
		};
		if (values.BloodTransForm_BeforeCheckID1) {
			entity.BeforeCheckID1 = values.BloodTransForm_BeforeCheckID1;
		}
		if (values.BloodTransForm_BeforeCheckID2) {
			entity.BeforeCheckID2 = values.BloodTransForm_BeforeCheckID2;
		}
		if (values.BloodTransForm_TransCheckID1) {
			entity.TransCheckID1 = values.BloodTransForm_TransCheckID1;
		}
		if (values.BloodTransForm_TransCheckID2) {
			entity.TransCheckID2 = values.BloodTransForm_TransCheckID2;
		}
		var transBeginTime = values.BloodTransForm_TransBeginTime;
		if (transBeginTime && JShell.Date.toServerDate(transBeginTime)) {
			entity.TransBeginTime = JShell.Date.toServerDate(transBeginTime);
		}
		var transEndTime = values.BloodTransForm_TransEndTime;
		if (transEndTime && JShell.Date.toServerDate(transEndTime)) {
			entity.TransEndTime = JShell.Date.toServerDate(transEndTime);
		}
		var adverseReactionsTime = values.BloodTransForm_AdverseReactionsTime;
		if (adverseReactionsTime && JShell.Date.toServerDate(adverseReactionsTime)) {
			entity.AdverseReactionsTime = JShell.Date.toServerDate(adverseReactionsTime);
		}
		var adverseReactionsHP = values.BloodTransForm_AdverseReactionsHP;
		if (!adverseReactionsHP) {
			adverseReactionsHP = 0;
		}
		entity.AdverseReactionsHP = adverseReactionsHP;
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

		for (var i in fields) {
			var arr = fields[i].split('_');
			if (arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		return entity;
	},
	isEdit: function(outDtlIdStr) {
		var me = this;
		me.callParent(arguments);
	},
	/**根据输血过程记录主单ID加载数据*/
	load: function(outDtlIdStr) {
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
		if (!outDtlIdStr) return;
		me.outDtlIdStr=outDtlIdStr;
		
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
					data.value = JShell.Server.Mapping(data.value);
					//console.log(data);
					me.lastData = me.changeResult(data.value);
					me.getForm().setValues(me.lastData);
				}
			} else {
				JShell.Msg.error(data.msg);
			}
			me.fireEvent('load', me, data);
		});
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function (data) {
		var me = this;
		var data1=me.callParent(arguments);
		if (me.formtype == "edit") {
			data1["BloodTransForm_Id"] = "";
		}
		return data1;
	},
	getSaveInfo: function() {
		var me = this;
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		return params;
	}
});
