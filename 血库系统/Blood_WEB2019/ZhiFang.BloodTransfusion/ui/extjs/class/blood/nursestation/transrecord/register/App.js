/**
 * 输血过程记录登记入口
 * @description 批量新增登记及支持单个血袋修改登记
 * @author longfc
 * @version 2020-02-25
 */
Ext.define('Shell.class.blood.nursestation.transrecord.register.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '输血过程记录登记',
	//输血过程记录主单ID
	PK: null,
	//当前选中发血血袋记录集合
	outDtlRrecords: [],
	//新增还是编辑
	formtype: "add",
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransFormAndDtlList',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransFormAndDtlList',

	/**显示成功信息*/
	showSuccessInfo: true,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**消息框消失时间*/
	hideTimes: 500,
	/**当前选择的发血主单行记录信息*/
	outDocRecord: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersOutPanel();
		me.loadData();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var maxWidth = me.width;
		var width2 = 280;
		if (maxWidth > 1366) {
			width2 = 460;
		}
		var width1 = maxWidth - width2;
		//输血过程记录项信息
		me.TransPanel = Ext.create('Shell.class.blood.nursestation.transrecord.register.TransPanel', {
			region: 'center',
			header: false,
			split: true,
			itemId: 'TransPanel',
			height: me.height,
			width: width1,
			formtype: me.formtype,
			outDocRecord:me.outDocRecord,
			collapsible: false
		});
		//不良反应相关信息
		me.AdverseReactionsPanel = Ext.create("Shell.class.blood.nursestation.transrecord.register.AdverseReactionsPanel", {
			region: 'east',
			width: width2,
			header: false,
			split: true,
			//border: false,
			itemId: 'AdverseReactionsPanel'
		});
		return [me.TransPanel, me.AdverseReactionsPanel];
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		//me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		//me.enableControl(); //启用所有的操作功能
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];

		for (var i = 0; i < length; i++) {
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			if (!items[i].isLocked) {
				items[i][enable ? 'enable' : 'disable']();
			}
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(false);
	},
	/*程序列表的事件监听**/
	listenersOutPanel: function() {
		var me = this;
		me.TransPanel.on({
			save: function(p, docForm, dtlForm) {
				me.onSave(p, docForm, dtlForm);
			}
		});
	},
	loadData: function() {
		var me = this;
		JShell.Action.delay(function() {
			if (me.formtype == "edit") {
				me.isEdit(me.PK);
			} else {
				me.isAdd();
			}
		}, null, 300);
	},
	isAdd: function() {
		var me = this;
		me.formtype = "add";
		me.PK = null;
		me.TransPanel.outDtlRrecords = me.outDtlRrecords;
		me.TransPanel.isAdd();

		me.AdverseReactionsPanel.outDtlRrecords = me.outDtlRrecords;
		me.AdverseReactionsPanel.isAdd();
	},
	isEdit: function(id) {
		var me = this;
		me.formtype = "edit";
		me.TransPanel.PK = id;
		me.AdverseReactionsPanel.PK = id;

		me.TransPanel.outDtlRrecords = me.outDtlRrecords;
		me.TransPanel.isEdit(id);

		me.AdverseReactionsPanel.outDtlRrecords = me.outDtlRrecords;
		me.AdverseReactionsPanel.isEdit(id);
	},
	/**保存按钮点击处理方法*/
	onSave: function(p, docForm, dtlForm) {
		var me = this;
		//验证通过后处理
		if (!docForm.getForm().isValid()) {
			JShell.Msg.alert("输血过程记录录入信息有误,请核对后再提交!");
			return;
		}

		var docInfo = docForm.getSaveInfo();
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);		
		if (!empID) empID = -1;
		if (!empName) empName = "";
		
		//输血前核对人判断处理
		if(docInfo.entity.BeforeCheck1&&docInfo.entity.BeforeCheck2){
			if(docInfo.entity.BeforeCheck1===docInfo.entity.BeforeCheck2){
				JShell.Msg.alert("输血前核对人1及输血前核对人2,不能是同一个人!");
				//docForm.getComponent('BloodTransForm_BeforeCheck2').focus(true);
				return;
			}
		}
		//输血时核对人判断处理
		if(docInfo.entity.TransCheck1&&docInfo.entity.TransCheck2){
			if(docInfo.entity.TransCheck1===docInfo.entity.TransCheck2){
				JShell.Msg.alert("输血时核对人1及输血时核对人2,不能是同一个人!");
				//docForm.getComponent('BloodTransForm_TransCheck2').focus(true);
				return;
			}
		}

		var saveInfo = {
			empID: empID,
			empName: empName,
			transForm: docInfo.entity, //输血过程记录基本信息,
			transfusionAntriesList: dtlForm.getSaveInfo(), //病人体征集合信息
			adverseReactionList: me.AdverseReactionsPanel.getAdverseReactionList(), //血袋不良反应选择项集合
			clinicalMeasuresList: me.AdverseReactionsPanel.getClinicalMeasuresList(), //血袋临床处理措施集合
			clinicalResults: me.AdverseReactionsPanel.getClinicalResults().entity, //血袋临床处理结果
			clinicalResultsDesc: me.AdverseReactionsPanel.getClinicalResultsDesc().entity //血袋临床处理结果描述
		};
		//新增或批量新增的发血血袋集合信息
		if (me.formtype == 'add') {
			saveInfo.outDtlList = me.getOutDtlList(); //发血血袋集合封装信息
		}
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		//console.log(saveInfo);
		var params = Ext.JSON.encode(saveInfo);
		me.showMask(me.saveText); //显示遮罩层
		var id = "";
		if (me.PK) id = me.PK;
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				if (me.outDtlRrecords.length == 1) {
					//id = me.formtype == 'add' ? data.value.id : id;
					id += '';
				}
				me.fireEvent('save', me, id);
				if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	//发血血袋集合封装信息
	getOutDtlList: function() {
		var me = this;
		var outDtlList = [];
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		for (var i = 0; i < me.outDtlRrecords.length; i++) {
			var outDtl = me.outDtlRrecords[i];
			var entity = {
				"Id": outDtl.get("BloodBOutItem_Id"),
				"DataTimeStamp": dataTimeStamp,
				"Pcode": outDtl.get("BloodBOutItem_Pcode"),
				"BBagCode": outDtl.get("BloodBOutItem_BBagCode")
			};
			entity.BloodBReqForm = {
				Id: outDtl.get("BloodBOutItem_BloodBReqForm_Id"),
				DataTimeStamp: dataTimeStamp
			}
			entity.BloodBOutForm = {
				Id: outDtl.get("BloodBOutItem_BloodBOutForm_Id"),
				DataTimeStamp: dataTimeStamp
			}
			entity.Bloodstyle = {
				Id: outDtl.get("BloodBOutItem_Bloodstyle_Id"),
				CName: outDtl.get("BloodBOutItem_Bloodstyle_CName"),
				DataTimeStamp: dataTimeStamp
			}
			outDtlList.push(entity);
		}
		return outDtlList;
	}
});
