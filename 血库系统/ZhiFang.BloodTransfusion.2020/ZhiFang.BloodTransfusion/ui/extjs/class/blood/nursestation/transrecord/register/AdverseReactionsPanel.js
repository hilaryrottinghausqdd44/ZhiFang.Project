/**
 * 输血过程记录:血袋不良反应入口
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.register.AdverseReactionsPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '不良反应信息',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	//输血过程记录主单ID
	PK: null,
	//新增还是编辑
	formtype: "add",
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
		me.TabPanel = Ext.create("Shell.class.blood.nursestation.transrecord.adversereaction.TabPanel", {
			region: 'center',
			header: false,
			//是否包含删除列
			hasDelCol: true,
			itemId: 'TabPanel'
		});
		//临床处理措施
		me.ClinicalMeasuresGrid = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalmeasures.TransItemGrid', {
			region: 'south',
			header: false,
			height: 180,
			//border: false,
			//是否包含删除列
			hasDelCol: true,
			itemId: 'ClinicalMeasuresGrid',
			split: false,
			collapsible: false
		});
		//临床处理结果
		me.ClinicalResultsForm = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalresults.Form', {
			region: 'south',
			header: false,
			border: false,
			height: 40,
			itemId: 'ClinicalResultsForm',
			split: false,
			collapsible: false
		});
		//临床处理结果描述
		me.ClinicalResultsDescForm = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalresultsdesc.Form', {
			region: 'south',
			header: false,
			border: false,
			height: 90,
			itemId: 'ClinicalResultsDescForm',
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
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;
	},
	/*输血过程记录项弹出选择**/
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
	//输血过程记录项选择处理
	onAccept: function(p, records, opType) {
		var me = this;
		var addArr = [];
		for (var i = 0; i < records.length; i++) {
			var record = records[i];
			var addObj = {
				"BloodTransItem_Id": "",
				"BloodTransItem_BloodTransForm_Id": me.PK,
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
				addObj["BloodTransItem_BloodTransRecordType_CName"]=gridPanel.transRecordType["BloodTransRecordType_CName"];
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
	setPKVals: function(id) {
		var me = this;
		me.TabPanel.PK = id;
		var gridPanel = me.TabPanel.getActiveTab();
		gridPanel.PK = id;
		me.ClinicalMeasuresGrid.PK = id;
		me.ClinicalResultsForm.PK = id;
		me.ClinicalResultsDescForm.PK = id;
	},
	clearData: function() {
		var me = this;
	},
	loadData: function() {
		var me = this;
		if (me.formtype == "edit") {
			me.isEdit(me.PK);
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
	isEdit: function(id) {
		var me = this;
		me.formtype = "edit";
		me.setPKVals(id);

		me.TabPanel.formtype = "edit";
		//不良反应症状列表
		me.TabPanel.loadData(id);
		//临床处理措施
		me.ClinicalMeasuresGrid.loadData(id);
		
		//临床处理结果
		me.ClinicalResultsForm.formtype = "edit";
		me.ClinicalResultsForm.isEdit(id);
		
		me.ClinicalResultsDescForm.formtype = "edit";
		//临床处理结果描述
		me.ClinicalResultsDescForm.isEdit(id);
	},
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
	//获取新增的血袋不良反应选择项集合信息
	getAdverseReactionList: function() {
		var me = this;
		var addList = [];
		var items = me.TabPanel.items.items;
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
				var id = record.get("BloodTransItem_Id");
				if (!id || id == "-1") {
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
					addList.push(entity);
				}
			});
		}
		return addList;
	},
	//获取新增的血袋临床处理措施集合信息
	getClinicalMeasuresList: function() {
		var me = this;
		var addList = [];
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		me.ClinicalMeasuresGrid.store.each(function(record) {
			var id = record.get("BloodTransItem_Id");
			if (!id || id == "-1") {
				var entity = {
					"Id": -2,
					"ContentTypeID": 3,
					"DispOrder": record.get("BloodTransItem_DispOrder")
				};
				entity.BloodTransRecordTypeItem = {
					Id: record.get("BloodTransItem_BloodTransRecordTypeItem_Id"),
					CName: record.get("BloodTransItem_BloodTransRecordTypeItem_CName"),
					DataTimeStamp: dataTimeStamp
				};
				entity.BloodTransRecordType = {
					Id: record.get("BloodTransItem_BloodTransRecordType_Id"),
					CName: record.get("BloodTransItem_BloodTransRecordType_CName"),
					DataTimeStamp: dataTimeStamp
				};
				addList.push(entity);
			}
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
	}
});
