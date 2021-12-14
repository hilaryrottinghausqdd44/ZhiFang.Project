/**
 * 输血过程记录:批量修改--血袋不良反应入口
 * @author longfc
 * @version 2020-03-23
 */
Ext.define('Shell.class.blood.nursestation.transrecord.editbatch.AdverseReactionsPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '不良反应信息',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr: null,
	//新增还是编辑
	formtype: "edit",
	//当前选中发血血袋记录集合
	outDtlRrecords: [],
	/**开启加载数据遮罩层*/
	hasLoadMask: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('nodata');
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		//不良反应症状tab
		me.TabPanel = Ext.create("Shell.class.blood.nursestation.transrecord.editbatch.TabPanel", {
			region: 'center',
			header: false,
			//是否包含删除列
			hasDelCol: true,
			outDtlIdStr: me.outDtlIdStr,
			itemId: 'TabPanel'
		});
		//临床处理措施
		me.ClinicalMeasuresGrid = Ext.create(
			'Shell.class.blood.nursestation.transrecord.clinicalmeasures.BatchTransItemGrid', {
				region: 'south',
				header: false,
				height: 180,
				//是否包含删除列
				hasDelCol: true,
				itemId: 'ClinicalMeasuresGrid',
				outDtlIdStr: me.outDtlIdStr,
				split: false,
				collapsible: false
			});
		//临床处理结果
		me.ClinicalResultsForm = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalresults.BatchForm', {
			region: 'south',
			header: false,
			border: false,
			height: 40,
			itemId: 'ClinicalResultsForm',
			outDtlIdStr: me.outDtlIdStr,
			split: false,
			collapsible: false
		});
		//临床处理结果描述
		me.ClinicalResultsDescForm = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalresultsdesc.BatchForm', {
			region: 'south',
			header: false,
			border: false,
			height: 90,
			itemId: 'ClinicalResultsDescForm',
			outDtlIdStr: me.outDtlIdStr,
			split: false,
			collapsible: false
		});
		return [me.TabPanel, me.ClinicalMeasuresGrid, me.ClinicalResultsForm, me.ClinicalResultsDescForm];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if (buttontoolbar) items.push(buttontoolbar);
		}
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		items.push({
			xtype: 'button',
			iconCls: 'button-add',
			border: true,
			text: '不良反应',
			tooltip: '对当前不良反应页签选择',
			listeners: {
				click: function(but) {
					me.onShowPanel("adversereaction");
				}
			}
		});
		items.push({
			xtype: 'button',
			border: true,
			iconCls: 'button-add',
			text: '临床处理措施',
			listeners: {
				click: function(but) {
					me.onShowPanel("clinicalmeasures");
				}
			}
		});
		items.push({
			xtype: 'splitbutton',
			iconCls: 'button-del',
			text: '清空全部',
			menu: [{
				iconCls: 'button-del',
				text: '清空全部不良反应症状',
				handler: function() {
					me.onDelBatch("AdverseReactions");
				}
			}, {
				iconCls: 'button-del',
				text: '清空全部临床处理措施',
				handler: function() {
					me.onDelBatch("ClinicalMeasures");
				}
			}]
		});
		if (items.length == 0) return null;
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: "top",
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**
	 * @description 程序列表的事件监听
	 */
	onListeners: function() {
		var me = this;
		//清空全部不良反应症状
		me.TabPanel.on({
			onDelBatchClick: function(p) {
				me.onDelBatch("AdverseReactions");
			}
		});
		//清空全部临床处理措施
		me.ClinicalMeasuresGrid.on({
			onDelBatchClick: function(p) {
				me.onDelBatch("ClinicalMeasures");
			}
		});
	},
	/**
	 * @description 输血过程记录项弹出选择
	 * @param {Object} opType
	 */
	onShowPanel: function(opType) {
		var me = this;
		/*左列表默认条件*/
		var leftDefaultWhere = "";
		var typeCode = "";
		var arrIdStr = [],
			idStr = "";
		var defaultWhere = " bloodtransrecordtypeitem.IsVisible=1 ";
		if (opType == "adversereaction") { //不良反应页签
			typeCode = "AdverseReactionOptions";
			var gridPanel = me.TabPanel.getActiveTab();
			gridPanel.store.each(function(record) {
				var goodId = record.get("BloodTransItem_BloodTransRecordTypeItem_Id");
				if (goodId && Ext.Array.contains(goodId) == false) arrIdStr.push(goodId);
			});
		} else if (opType == "clinicalmeasures") { //临床处理措施
			typeCode = "ClinicalMeasures";
			me.ClinicalMeasuresGrid.store.each(function(record) {
				var goodId = record.get("BloodTransItem_BloodTransRecordTypeItem_Id");
				if (goodId && Ext.Array.contains(goodId) == false) arrIdStr.push(goodId);
			});
		}
		defaultWhere = defaultWhere + " and bloodtransrecordtypeitem.BloodTransRecordType.TypeCode='" + typeCode + "'";
		if (arrIdStr.length > 0) idStr = arrIdStr.join(",");
		if (idStr) defaultWhere = defaultWhere + " and bloodtransrecordtypeitem.Id not in (" + idStr + ")";

		var maxWidth = 620
		var height = 520;
		JShell.Win.open('Shell.class.sysbase.transrecordtypeitem.choose.App', {
			resizable: true,
			width: maxWidth,
			height: height,
			leftDefaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onAccept(p, records, opType);
				}
			}
		}).show();
	},
	/**
	 * @description 输血过程记录项选择处理
	 * @param {Object} p
	 * @param {Object} records
	 * @param {Object} opType
	 */
	onAccept: function(p, records, opType) {
		var me = this;
		var addArr = [];
		for (var i = 0; i < records.length; i++) {
			var record = records[i];
			var addObj = {
				"BloodTransItem_Id": "",
				"BloodTransItem_BloodTransForm_Id": "",
				"BloodTransItem_BloodTransRecordType_Id": record.get("BloodTransRecordTypeItem_BloodTransRecordType_Id"),
				"BloodTransItem_BloodTransRecordType_CName": record.get("BloodTransRecordTypeItem_BloodTransRecordType_CName"),
				"BloodTransItem_ContentTypeID": record.get("BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID"),
				"BloodTransItem_BloodTransRecordTypeItem_Id": record.get("BloodTransRecordTypeItem_Id"),
				"BloodTransItem_BloodTransRecordTypeItem_CName": record.get("BloodTransRecordTypeItem_CName"),
				"BloodTransItem_DispOrder": record.get("BloodTransRecordTypeItem_DispOrder")
			};
			if (opType == "adversereaction") {
				//不良反应记录的分类字典ID是对应不良反应的所属时间段分类字典ID
				var gridPanel = me.TabPanel.getActiveTab();
				addObj["BloodTransItem_BloodTransRecordType_Id"] = gridPanel.itemId;
				addObj["BloodTransItem_BloodTransRecordType_CName"] = gridPanel.transRecordType["BloodTransRecordType_CName"];
			}
			addArr.push(addObj);
		}
		if (opType == "adversereaction") {
			var gridPanel = me.TabPanel.getActiveTab();
			gridPanel.store.add(addArr);
		} else if (opType == "clinicalmeasures") {
			me.ClinicalMeasuresGrid.store.add(addArr);
		}
		p.close();
	},
	setPKVals: function(outDtlIdStr) {
		var me = this;
		me.outDtlIdStr = outDtlIdStr;
		me.TabPanel.outDtlIdStr = outDtlIdStr;
		var gridPanel = me.TabPanel.getActiveTab();
		gridPanel.outDtlIdStr = outDtlIdStr;
		me.ClinicalMeasuresGrid.outDtlIdStr = outDtlIdStr;
		me.ClinicalResultsForm.outDtlIdStr = outDtlIdStr;
		me.ClinicalResultsDescForm.outDtlIdStr = outDtlIdStr;
	},
	clearData: function() {
		var me = this;
	},
	loadData: function() {
		var me = this;
		if (me.formtype == "edit") {
			me.isEdit(me.outDtlIdStr);
		} else {
			me.isAdd();
		}
	},
	isAdd: function() {
		var me = this;
		me.formtype = "add";
		me.setPKVals(null);
		me.TabPanel.formtype = "add";
		me.TabPanel.clearData();
		me.ClinicalMeasuresGrid.clearData();

		me.ClinicalResultsForm.isAdd();
		me.ClinicalResultsDescForm.isAdd();
	},
	/**
	 * @param {Object} outDtlIdStr
	 */
	isEdit: function(outDtlIdStr) {
		var me = this;
		me.formtype = "edit";
		me.setPKVals(outDtlIdStr);

		me.TabPanel.formtype = "edit";
		//不良反应症状列表
		me.TabPanel.loadData(outDtlIdStr);

		//临床处理措施
		me.ClinicalMeasuresGrid.loadData(outDtlIdStr);
		//临床处理结果
		me.ClinicalResultsForm.formtype = "edit";
		me.ClinicalResultsForm.isEdit(outDtlIdStr);
		//临床处理结果描述
		me.ClinicalResultsDescForm.formtype = "edit";
		me.ClinicalResultsDescForm.isEdit(outDtlIdStr);
	},
	/**
	 * @description 获取封装的保存信息
	 */
	getSaveInfo: function() {
		var me = this;
		var saveInfo = {
			adverseReactionList: me.getAdverseReactionList(), //血袋不良反应选择项集合
			clinicalMeasuresList: me.getClinicalMeasuresList(), //血袋临床处理措施集合
			clinicalResults: me.getClinicalResults(), //血袋临床处理结果
			clinicalResultsDesc: me.getClinicalResultsDesc() //血袋临床处理结果描述
		};
		return saveInfo;
	},
	/**
	 * @description 获取新增的血袋不良反应选择项集合信息
	 */
	getAdverseReactionList: function() {
		var me = this;
		var addList = [];
		var items = me.TabPanel.items.items;
		//console.log(me.TabPanel.items);
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		for (var i = 0; i < items.length; i++) {
			var newCard = items[i];
			//不良反应记录的分类字典ID是对应不良反应的所属时间段分类字典ID
			var recordTypeId = recordTypeId = newCard.itemId;
			var transRecordType = {
				Id: recordTypeId,
				DataTimeStamp: dataTimeStamp
			};
			newCard.store.each(function(record) {
				var entity = {
					"Id": -2,
					"ContentTypeID": 4,
					"DispOrder": record.get("BloodTransItem_DispOrder"),
					"BloodTransRecordType": transRecordType
				};
				entity.BloodTransRecordTypeItem = {
					Id: record.get("BloodTransItem_BloodTransRecordTypeItem_Id"),
					CName: record.get("BloodTransItem_BloodTransRecordTypeItem_CName"),
					DataTimeStamp: dataTimeStamp
				};
				//console.log(entity);
				addList.push(entity);
			});
		}
		return addList;
	},
	/**
	 * @description 获取新增的血袋临床处理措施集合信息
	 */
	getClinicalMeasuresList: function() {
		var me = this;
		var addList = [];
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		me.ClinicalMeasuresGrid.store.each(function(record) {
			var entity = {
				"Id": -2,
				"ContentTypeID": 3,
				"DispOrder": record.get("BloodTransItem_DispOrder")
			};
			entity.BloodTransRecordTypeItem = {
				Id: record.get("BloodTransItem_BloodTransRecordTypeItem_Id"),
				CName: record.get("BloodTransItem_BloodTransRecordTypeItem_CName"),
				DataTimeStamp: dataTimeStamp
			}

			var recordTypeId = record.get("BloodTransItem_BloodTransRecordType_Id");
			if (recordTypeId) {
				entity.BloodTransRecordType = {
					Id: recordTypeId,
					CName: record.get("BloodTransItem_BloodTransRecordType_CName"),
					DataTimeStamp: dataTimeStamp
				}
			}
			addList.push(entity);
		});
		return addList;
	},
	//获取血袋临床处理结果信息
	getClinicalResults: function() {
		var me = this;
		var entity = me.ClinicalResultsForm.getAddParams();
		return entity;
	},
	//获取血袋临床处理结果描述信息
	getClinicalResultsDesc: function() {
		var me = this;
		var entity = me.ClinicalResultsDescForm.getAddParams();
		return entity;
	},
	getOutDtlIdStr: function() {
		var me = this;
		if (me.outDtlIdStr && me.outDtlIdStr.length > 0) return me.outDtlIdStr;

		if (!me.outDtlRrecords) me.outDtlRrecords = [];
		var arrIdStr = [];
		for (var i = 0; i < me.outDtlRrecords.length; i++) {
			var outId = me.outDtlRrecords[i].get("BloodBOutItem_Id");
			arrIdStr.push(outId);
		}
		me.outDtlIdStr = arrIdStr.join(",");
	},
	/**
	 * @description 清空全部处理
	 * @description 按选择的发血血袋明细ID,批量删除某一不良反应分类的所有不良反应症状记录信息
	 * @description 按选择的发血血袋明细ID,批量删除其所有的临床处理结果记录信息
	 * @param {Object} typeCode
	 */
	onDelBatch: function(typeCode) {
		var me = this;
		var delUrl = "";
		var outDtlIdStr = me.getOutDtlIdStr();
		var recordTypeId = ""; //某一不良反应分类ID(当前tab页面的itemId)

		switch (typeCode) {
			case "AdverseReactions":
				var gridPanel = me.TabPanel.getActiveTab();
				recordTypeId = gridPanel.itemId;
				delUrl = "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBatchTransItemByAdverseReactions";
				break;
			case "ClinicalMeasures":
				delUrl = "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBatchTransItemByClinicalMeasures";
				break;
			default:
				break;
		}
		var url = (delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'outDtlIdStr=' + outDtlIdStr;
		if (recordTypeId) url += '&recordTypeId=' + recordTypeId;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					//删除相应的列表
					me.clearStore(typeCode);
				} else {
					JShell.Msg.error('清空全部失败,' + data.msg);
				}
			});
		}, 100);
	},
	/**
	 * @description 清空列表数据 
	 * @param {Object} typeCode
	 */
	clearStore: function(typeCode) {
		var me = this;
		var gridPanel = null;
		switch (typeCode) {
			case "AdverseReactions":
				gridPanel = me.TabPanel.getActiveTab();
				break;
			case "ClinicalMeasures":
				gridPanel = me.ClinicalMeasuresGrid;
				break;
			default:
				break;
		}
		if (gridPanel) {
			gridPanel.store.each(function(record) {
				gridPanel.store.remove(record);
			});
		}
	}
});
