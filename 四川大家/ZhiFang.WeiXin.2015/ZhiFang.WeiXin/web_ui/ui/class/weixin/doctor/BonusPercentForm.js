/**
 * 医生咨询费比率
 * @author liangyl	
 * @version 2017-2-28
 */
Ext.define('Shell.class.weixin.doctor.BonusPercentForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '医生咨询费比率设置',
	width: 250,
	height: 115,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccountById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_AddBDoctorAccount',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBDoctorAccountByField',
	/**图片服务地址*/
	UploadImgUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UploadBDoctorAccountImageByAccountID',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
    records:null,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 65,
		labelAlign: 'right'
	},
	/**显示成功信息*/
	showSuccessInfo: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [{
			fieldLabel: '咨询费比率',
			name: 'BDoctorAccount_BonusPercent',
			xtype: 'numberfield',
			value: 0
		}, {
			fieldLabel: '主键ID',
			name: 'BDoctorAccount_Id',
			hidden: true
		}];

		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		if(!me.records){
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = me.records.length;
		for(var i in me.records){
			var id=me.records[i].get('BDoctorAccount_Id');
			me.onEditBonusPercent(id,function(){
				me.fireEvent('save',me);
			});
		}
	},
	/**批量设置咨询费率数据*/
	onEditBonusPercent:function(id,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
		var values = me.getForm().getValues();	
		var params = {
			entity:{
				BonusPercent:values.BDoctorAccount_BonusPercent,
				Id:id
			},
			fields:'Id,BonusPercent'
		};
		//提交数据到后台
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){callback();}
			}
		});
	}
});