/**
 * 单站点授权
 * @author longfc
 * @version 2016-12-13
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.apply.Form', {
	extend: 'Shell.class.wfm.authorization.ahsingle.basic.Form',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '单站点授权申请',
	width: 620,
	height: 445,
	bodyPadding: '10px',
	PK: null,
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function(isSubmit) {
		var me = this,
			values = me.getForm().getValues();

		var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '临时');
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyDate = JcallShell.Date.toString(Sysdate);
		var entity = {
			ApplyID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			ApplyName: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			LicenceTypeId: values.LicenceTypeId, //授权类型
			MacAddress: values.MacAddress,
			IsUse: 1,
			SQH: values.SQH
		};
		if(ApplyDate) {
			entity.ApplyDataTime = JShell.Date.toServerDate(ApplyDate);
		}
		if(values.StartDate) {
			entity.StartDate = JShell.Date.toServerDate(values.StartDate);
		}
		if(values.EndDate) {
			entity.EndDate = JShell.Date.toServerDate(values.EndDate);
		}
		if(values.PlannReceiptDate) {
			entity.PlannReceiptDate = JShell.Date.toServerDate(values.PlannReceiptDate);
		}
		if(values.PClientID) {
			entity.PClientID = values.PClientID;
			entity.PClientName = values.PClientName;
		}
		if(values.PContractID) {
			entity.PContractID = values.PContractID;
		}
		//仪器
		if(values.EquipID) {
			entity.EquipID = values.EquipID;
			entity.EquipName = values.EquipName;
		}
		//程序
		if(values.ProgramID) {
			entity.ProgramID = values.ProgramID;
			entity.ProgramName = values.ProgramName;
		}
		//未收款原因
		if(values.Comment) {
			entity.Comment = values.Comment.replace(/\\/g, '&#92');
			entity.Comment = entity.Comment.replace(/[\r\n]/g, '<br />');
		}
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {
			if(!JShell.System.ClassDict.LicenceStatus) {
				JShell.Msg.error('未获取到单站点授权状态，请重新保存');
				return;
			}
			if(isSubmit) { //提交
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '申请');
				entity.Status = info.Id;
			} else { //暂存
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '暂存');
				entity.Status = info.Id;
			}
		});
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function(isSubmit) {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams(isSubmit);

		var fields = [
			'ProgramID', 'ProgramName', 'EquipID', 'EquipName',
			'PContractID', 'PClientID', 'PClientName', 'Status',
			'MacAddress', 'LicenceTypeId', 'Id', 'SQH', 'StartDate', 'EndDate', 'Comment', 'PlannReceiptDate'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.Id;
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit) {
		var me = this;
		me.onSaveClick(isSubmit);
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this;
		var result = me.verificationSubmit();
		if(result == true) {
			var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
			url = JShell.System.Path.getRootUrl(url);
			var params = me.formtype == 'add' ? me.getAddParams(isSubmit) : me.getEditParams(isSubmit);

			if(!params) return;
			var id = params.entity.Id;
			params = Ext.JSON.encode(params);

			me.showMask(me.saveText); //显示遮罩层
			me.fireEvent('beforesave', me);
			JShell.Server.post(url, params, function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					id = me.formtype == 'add' ? data.value : id;
					if(Ext.typeOf(id) == 'object') {
						id = id.id;
					}

					if(me.isReturnData) {
						me.fireEvent('save', me, me.returnData(id));
					} else {
						me.fireEvent('save', me, id);
					}
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				} else {
					me.fireEvent('saveerror', me);
					JShell.Msg.error(data.msg);
				}
			});
		}
	},
	initFilterListeners: function() {
		var me = this;
		//程序
		var ProgramName = me.getComponent('ProgramName'),
			ProgramID = me.getComponent('ProgramID');
		//仪器
		var EquipName = me.getComponent('EquipName'),
			EquipID = me.getComponent('EquipID');
		ProgramName.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(EquipID.getValue()) {
					EquipID.setValue('');
					EquipName.setValue('');
				}
			}
		});
		EquipName.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(ProgramID.getValue()) {
					ProgramID.setValue('');
					ProgramName.setValue('');
				}
			}
		});
	}
});