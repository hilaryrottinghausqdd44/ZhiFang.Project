/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.comp.add.DocForm', {
	extend: 'Shell.class.rea.client.reasale.basic.add.DocForm',

	title: '供货信息',

	width: 680,
	height: 195,

	/**订货方的所属机构类型值:0:供货商;1:订货方;*/
	LabOrgTypeValue: "1",
	/**供货商的所属机构类型值:0:供货商;1:订货方;供货方不能选择*/
	CompOrgTypeValue: "0",
	//新增订单时，如果订货方只有一家，最好默认，不要再去选择
	hasLoadLabc:false,
	//供货商是否仅有一个订货方
	hasOnlyOneLabc: false,
	//默认订货方信息
	defaultLabcInfo: {
		"ReaLabID": "",
		"ReaLabCName": "",
		"RearLabcCode": "",
		"PlatformOrgNo": ""
	},
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
		//数据来源:供应商
		var Source = me.getComponent('ReaBmsCenSaleDoc_Source');
		Source.setValue("1");
		//所属部门
		var DeptName = me.getComponent('ReaBmsCenSaleDoc_DeptName');
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		DeptName.setValue(deptName);
		//显示暂存按钮
		me.getComponent('buttonsToolbar').getComponent("btnTempSave").setVisible(true);
		//供货商录入供货单:订货方,供货商信息处理
		//订货方可以选择
		var LabcName = me.getComponent('ReaBmsCenSaleDoc_LabcName');
		LabcName.setReadOnly(false);
		var CompanyName = me.getComponent('ReaBmsCenSaleDoc_CompanyName');
		var ReaServerCompCode = me.getComponent('ReaBmsCenSaleDoc_ReaServerCompCode');
		//供货商只读
		CompanyName.setReadOnly(true);
		//供货商取cenorg的OrgNo及CName
		var reaServerCompCode = JcallShell.REA.System.CENORG_CODE;
		if (!reaServerCompCode) reaServerCompCode = "";
		ReaServerCompCode.setValue(reaServerCompCode);
		CompanyName.setValue(JcallShell.REA.System.CENORG_NAME);
		//由前台赋值LabID
		var labId = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID);
		if (labId) {
			var LabID = me.getComponent('ReaBmsCenSaleDoc_LabID');
			LabID.setValue(labId);
		}
		//新增订单时，如果订货方只有一家，最好默认，不要再去选择		
		if (me.hasOnlyOneLabc == true && me.defaultLabcInfo) {
			var labcInfo=me.defaultLabcInfo;
			me.setLabcValue(labcInfo);
		}else if(me.hasLoadLabc==false){
			me.setDefaultLabc();
		}
	},
	/**@description 新增订单时，如果订货方只有一家，最好默认，不要再去选择*/
	setDefaultLabc: function() {
		var me = this;
		var url ='/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true';
		var fields="ReaCenOrg_Id,ReaCenOrg_OrgNo,ReaCenOrg_PlatformOrgNo,ReaCenOrg_CName";
		/**OrgType:机构类型 0供货方，1订货方*/
		var where="reacenorg.OrgType=1 and reacenorg.Visible=1";
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		url=url+"&fields="+fields;
		url=url+"&where="+where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.hasLoadLabc=true;				
				if(data.value&&data.value.list&&data.value.list.length==1){
					me.hasOnlyOneLabc=true;
					var record=data.value.list[0];
					var labcInfo = {
						"ReaLabID": record["ReaCenOrg_Id"],
						"ReaLabCName":record["ReaCenOrg_CName"],
						"RearLabcCode": record["ReaCenOrg_OrgNo"],
						"PlatformOrgNo": record["ReaCenOrg_PlatformOrgNo"],
					};
					me.defaultLabcInfo=labcInfo;
					me.setLabcValue(labcInfo);
				}
			} else {
				JShell.Msg.error('获取订货方信息失败！' + data.msg);
			}
		});
	},
	setLabcValue: function(labcInfo) {
		var me = this;		
		var LabcID = me.getComponent('ReaBmsCenSaleDoc_LabcID');
		var LabcName = me.getComponent('ReaBmsCenSaleDoc_LabcName');
		var ReaServerLabcCode = me.getComponent('ReaBmsCenSaleDoc_ReaServerLabcCode');
		var RearLabcCode = me.getComponent('ReaBmsCenSaleDoc_ReaLabcCode');
		LabcID.setValue(labcInfo.ReaLabID);
		LabcName.setValue(labcInfo.ReaLabCName);
		ReaServerLabcCode.setValue(labcInfo.PlatformOrgNo);
		RearLabcCode.setValue(labcInfo.RearLabcCode);
		me.fireEvent('onReaLabCheck', me, labcInfo);
	},
	/**@description 订货方选择*/
	onLabcAccept: function(record) {
		var me = this;
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		var orgNo = record ? record.data.value.OrgNo : '';

		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		var labcInfo= {
			"ReaLabID": id,
			"ReaLabCName": text,
			"RearLabcCode": orgNo,
			"PlatformOrgNo": platformOrgNo
		};
		me.setLabcValue(labcInfo);
	},
	/**@description 供货商选择*/
	onCompAccept: function(record) {
		var me = this;

		var CompID = me.getComponent('ReaBmsCenSaleDoc_CompID');
		var CompanyName = me.getComponent('ReaBmsCenSaleDoc_CompanyName');
		var ReaServerCompCode = me.getComponent('ReaBmsCenSaleDoc_ReaServerCompCode');
		var ReaCompCode = me.getComponent('ReaBmsCenSaleDoc_ReaCompCode');

		var ReaCompanyID = me.getComponent('ReaBmsCenSaleDoc_ReaCompID');
		var ReaCompanyName = me.getComponent('ReaBmsCenSaleDoc_ReaCompanyName');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		var orgNo = record ? record.data.value.OrgNo : '';

		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		CompID.setValue(id);
		CompanyName.setValue(text);
		ReaServerCompCode.setValue(platformOrgNo);
		ReaCompCode.setValue(orgNo);

		ReaCompanyID.setValue(id);
		ReaCompanyName.setValue(text);
	},
	/**@description 暂存按钮点击处理*/
	onTempSaveClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if (!me.getForm().isValid()) {
			JShell.Msg.error('请填写供货单基本必要信息');
			return;
		}
		if (!JcallShell.REA.System.CENORG_CODE) {
			JShell.Msg.error('获取供货商的平台机构码信息为空!');
			return;
		}
		me.Status = '1';
		me.onSaveClick();
	},
	/**@description 确认提交*/
	onApplyClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if (!me.getForm().isValid()) {
			JShell.Msg.error('请填写供货单基本必要信息');
			return;
		}
		if (!JcallShell.REA.System.CENORG_CODE) {
			JShell.Msg.error('获取供货商的平台机构码信息为空!');
			return;
		}
		me.Status = '2';
		me.onSaveClick();
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if (!me.getForm().isValid()) {
			JShell.Msg.error("请输入供货单基本信息后再提交!");
			return;
		}
		if (!JcallShell.REA.System.CENORG_CODE) {
			JShell.Msg.error('获取供货商的平台机构码信息为空!');
			return;
		}
		var values = me.getForm().getValues();

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if (!params) {
			JShell.Msg.error("封装提交信息错!");
			return;
		}
		params.entity.Status = me.Status;
		me.fireEvent('onSave', me, params);
	}
});
