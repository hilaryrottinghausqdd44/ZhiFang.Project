/**
 * 订货方供货管理
 * @author longfc
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.client.reasale.lab.add.DocForm', {
	extend: 'Shell.class.rea.client.reasale.basic.add.DocForm',

	title: '供货信息',

	width: 680,
	height: 195,

	/**订货方的所属机构类型值:0:供货商;1:订货方;订货方不能选择*/
	LabOrgTypeValue: "1",
	/**供货商的所属机构类型值:0:供货商;1:订货方;*/
	CompOrgTypeValue: "0",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@description 新增供货单*/
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		//操作人信息
		var UserID = me.getComponent('ReaBmsCenSaleDoc_UserID');
		var UserName = me.getComponent('ReaBmsCenSaleDoc_UserName');
		UserID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		UserName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
		//供货日期
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);
		var DataAddTime = me.getComponent('ReaBmsCenSaleDoc_DataAddTime');
		DataAddTime.setValue(curDateTime);
		//数据来源:实验室
		var Source = me.getComponent('ReaBmsCenSaleDoc_Source');
		Source.setValue("2");
		//所属部门
		var DeptName = me.getComponent('ReaBmsCenSaleDoc_DeptName');
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		DeptName.setValue(deptName);
		//显示暂存按钮
		me.getComponent('buttonsToolbar').getComponent("btnTempSave").setVisible(true);

		//订货方只读
		var LabcName = me.getComponent('ReaBmsCenSaleDoc_LabcName');
		var ReaServerLabcCode = me.getComponent('ReaBmsCenSaleDoc_ReaServerLabcCode');
		LabcName.setReadOnly(true);
		//订货方取cenorg的OrgNo及CName
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if(!reaServerLabcCode) reaServerLabcCode = "";
		ReaServerLabcCode.setValue(reaServerLabcCode);
		LabcName.setValue(JcallShell.REA.System.CENORG_NAME);

		var CompanyName = me.getComponent('ReaBmsCenSaleDoc_CompanyName');
		//供货商可以选择
		CompanyName.setReadOnly(false);
		//由前台赋值LabID
		var labId = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID);
		if(labId) {
			var LabID = me.getComponent('ReaBmsCenSaleDoc_LabID');
			LabID.setValue(labId);
		}
	},
	/**@description 订货方选择*/
	onLabcAccept: function(record) {
		var me = this;
		var LabcID = me.getComponent('ReaBmsCenSaleDoc_LabcID');
		var LabcName = me.getComponent('ReaBmsCenSaleDoc_LabcName');
		var ReaServerLabcCode = me.getComponent('ReaBmsCenSaleDoc_ReaServerLabcCode');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';

		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		LabcID.setValue(id);
		LabcName.setValue(text);
		ReaServerLabcCode.setValue(platformOrgNo);
	},
	/**@description 供货商选择*/
	onCompAccept: function(record) {
		var me = this;

		var CompID = me.getComponent('ReaBmsCenSaleDoc_CompID');
		var CompanyName = me.getComponent('ReaBmsCenSaleDoc_CompanyName');
		var ReaServerCompCode = me.getComponent('ReaBmsCenSaleDoc_ReaServerCompCode');

		var ReaCompanyID = me.getComponent('ReaBmsCenSaleDoc_ReaCompID');
		var ReaCompanyName = me.getComponent('ReaBmsCenSaleDoc_ReaCompanyName');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';

		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		CompID.setValue(id);
		CompanyName.setValue(text);
		ReaServerCompCode.setValue(platformOrgNo);

		ReaCompanyID.setValue(id);
		ReaCompanyName.setValue(text);

		var objValue = {
			"CompID": id,
			"CompanyName": text,
			"PlatformOrgNo": platformOrgNo
		};
		me.fireEvent('setReaCompInfo', me, objValue);
	},
	/**@description 暂存按钮点击处理*/
	onTempSaveClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if(!me.getForm().isValid()) {
			JShell.Msg.error('请填写供货单基本必要信息');
			return;
		}
		if(!JcallShell.REA.System.CENORG_CODE) {
			JShell.Msg.error('获取订货方的平台机构码信息为空!');
			return;
		}
		me.Status = '1';
		me.onSaveClick();
	},
	/** @description 确认提交*/
	onApplyClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if(!me.getForm().isValid()) {
			JShell.Msg.error('请填写供货单基本必要信息');
			return;
		}
		if(!JcallShell.REA.System.CENORG_CODE) {
			JShell.Msg.error('获取订货方的平台机构码信息为空!');
			return;
		}
		me.Status = '2';
		me.onSaveClick();
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.getForm().isValid()) {
			JShell.Msg.error("请输入供货单基本信息后再提交!");
			return;
		}
		if(!JcallShell.REA.System.CENORG_CODE) {
			JShell.Msg.error('获取订货方的平台机构码信息为空!');
			return;
		}
		var values = me.getForm().getValues();

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("封装提交信息错!");
			return;
		}
		params.entity.Status = me.Status;
		me.fireEvent('onSave', me, params);
	}
});