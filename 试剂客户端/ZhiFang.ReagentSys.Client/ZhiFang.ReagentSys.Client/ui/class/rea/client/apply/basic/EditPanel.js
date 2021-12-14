/**
 * @description 部门采购申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.EditPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '采购申请',
	header: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	border: false,
	//bodyPadding: 1,
	layout: {
		type: 'border'
	},

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//部门选择改变后
		me.ApplyForm.on({
			hrdptcheck: function(form, record) {
				var text = record ? record.get('text') : '';
				if(text && text.indexOf("]") >= 0) {
					text = text.split("]")[1];
					text = Ext.String.trim(text);
				}
				me.ReqDtlGrid.CurDeptId = record ? record.get('tid') : '';
				me.ReqDtlGrid.CurDeptName = text;
				me.ReqDtlGrid.setGoodstemplateClassConfig(true);
				if(me.formtype=="add"){
					me.ReqDtlGrid.store.removeAll();
					me.setDeptNameReadOnly(false);
				}
			},
			//部门选择项的编辑状态处理
			isEditAfter: function(form) {
				var bo = true;
				if(me.ReqDtlGrid.store.getCount() <= 0) bo = false;
				me.setDeptNameReadOnly(bo);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.formtype = me.formtype || "show";
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ReqDtlGrid = Ext.create('Shell.class.rea.client.apply.basic.ReqDtlGrid', {
			header: false,
			itemId: 'ReqDtlGrid',
			region: 'center',
			collapsible: false,
			defaultLoad: false,
			collapsed: false,
			formtype: me.formtype
		});
		me.ApplyForm = Ext.create('Shell.class.rea.client.apply.basic.ApplyForm', {
			header: false,
			itemId: 'ApplyForm',
			region: 'north',
			width: me.width,
			height: 180,
			split: false,
			collapsible: false,
			collapsed: false,
			formtype: me.formtype
		});
		var appInfos = [me.ReqDtlGrid, me.ApplyForm];
		return appInfos;
	},
	/**
	 * @description 申请录入主表联动货品明细
	 * @param {Object} record
	 */
	clearData: function() {
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.ApplyForm.PK = null;
		me.ApplyForm.formtype = "show";
		me.ApplyForm.StatusName = "";
		me.ApplyForm.isShow();
		me.ApplyForm.getForm().reset();

		me.ReqDtlGrid.PK = null;
		me.ReqDtlGrid.formtype = "show";
		me.ReqDtlGrid.defaultWhere = "";
		me.ReqDtlGrid.CurDeptId = "";
		me.ReqDtlGrid.CurDeptName = "";
		me.ReqDtlGrid.Status = null;
		me.ReqDtlGrid.store.removeAll();
		me.ReqDtlGrid.disableControl();
		me.ReqDtlGrid.buttonsDisabled = true;
		me.ReqDtlGrid.setButtonsDisabled(true);
		me.ReqDtlGrid.setGoodstemplateClassConfig(true);
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";

		me.ApplyForm.PK = null;
		me.formtype = "add";
		me.ApplyForm.StatusName = "新增申请";
		me.ApplyForm.isAdd();
		me.ApplyForm.getComponent('buttonsToolbar').hide();

		me.ReqDtlGrid.PK = null;
		me.ReqDtlGrid.formtype = "add";
		me.ReqDtlGrid.defaultWhere = "";
		me.ReqDtlGrid.Status = "1";
		me.ReqDtlGrid.store.removeAll();
		//me.ReqDtlGrid.onSearch();
		me.ReqDtlGrid.enableControl();
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptCName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		me.ReqDtlGrid.CurDeptId = deptId;
		me.ReqDtlGrid.CurDeptName = deptCName;
		me.ReqDtlGrid.getReaGoodsCenOrgList();

		me.ReqDtlGrid.setBtnDisabled("btnAdd", false);
		me.ReqDtlGrid.setBtnDisabled("btnDel", false);
		me.ReqDtlGrid.setBtnDisabled("btnSave", true);
		me.ReqDtlGrid.setGoodstemplateClassConfig(true);
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsReqDoc_Id");
		me.PK = id;
		me.formtype = "edit";

		me.ApplyForm.formtype = "edit";
		me.ApplyForm.PK = id;
		me.ApplyForm.isEdit(id);
		me.ApplyForm.getComponent('buttonsToolbar').hide();

		me.ReqDtlGrid.PK = id;
		me.ReqDtlGrid.formtype = "edit";
		me.ReqDtlGrid.Status = record.get("ReaBmsReqDoc_Status");
		me.ReqDtlGrid.CurDeptId = record.get("ReaBmsReqDoc_DeptID");
		me.ReqDtlGrid.CurDeptName = record.get("ReaBmsReqDoc_DeptName");
		me.ReqDtlGrid.getReaGoodsCenOrgList();
		var defaultWhere = "";
		if(me.PK) defaultWhere = "reabmsreqdtl.ReaBmsReqDoc.Id=" + me.PK;
		me.ReqDtlGrid.defaultWhere = defaultWhere;
		me.ReqDtlGrid.onSearch();

		me.ReqDtlGrid.buttonsDisabled = false;
		me.ReqDtlGrid.setButtonsDisabled(false);
		me.ReqDtlGrid.setGoodstemplateClassConfig(true);
	},
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsReqDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.ApplyForm.PK = id;
		me.ApplyForm.formtype = "show";
		var statusName = "",
			color = "#1c8f36";
		var StatusEnum = JShell.REA.StatusList.Status[applyGrid.StatusKey].Enum;
		var StatusBGColor = JShell.REA.StatusList.Status[applyGrid.StatusKey].BGColor;
		if(StatusEnum != null) statusName = StatusEnum[record.get("ReaBmsReqDoc_Status")];
		if(StatusBGColor != null) color = StatusBGColor[record.get("ReaBmsReqDoc_Status")];
		
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.ApplyForm.StatusName = statusName;
		me.ApplyForm.isShow(id);

		me.ReqDtlGrid.PK = id;
		me.ReqDtlGrid.formtype = "show";
		me.ReqDtlGrid.Status = record.get("ReaBmsReqDoc_Status");
		me.ReqDtlGrid.CurDeptId = record.get("ReaBmsReqDoc_DeptID");
		me.ReqDtlGrid.CurDeptName = record.get("ReaBmsReqDoc_DeptName");
		var defaultWhere = "";
		if(me.PK) defaultWhere = "reabmsreqdtl.ReaBmsReqDoc.Id=" + me.PK;
		me.ReqDtlGrid.defaultWhere = defaultWhere;
		me.ReqDtlGrid.onSearch();
		me.ReqDtlGrid.buttonsDisabled = true;
		me.ReqDtlGrid.setGoodstemplateClassConfig(true);
	},
	/**@description 获取明细的保存提交数据*/
	getSaveParams: function() {
		var me = this;
		var result = me.ReqDtlGrid.getDtSaveParams();
		if(me.fromtype == "add") {
			me.PK = -1;
			me.Status = "1";
		}
		var entity = me.formtype == 'add' ? me.ApplyForm.getAddParams() : me.ApplyForm.getEditParams();
		var params = {
			entity: entity.entity,
			"dtAddList": result.dtAddList
		};
		if(me.formtype == 'edit') {
			params.fields = entity.fields;
			params.dtEditList = result.dtEditList;
		}
		return params;
	},
	/**@description 所属部门是可编辑还是只读处理*/
	setDeptNameReadOnly: function(bo) {
		var me = this;
		var com = me.ApplyForm.getComponent('ReaBmsReqDoc_DeptName');
		com.setReadOnly(bo);
	}
});