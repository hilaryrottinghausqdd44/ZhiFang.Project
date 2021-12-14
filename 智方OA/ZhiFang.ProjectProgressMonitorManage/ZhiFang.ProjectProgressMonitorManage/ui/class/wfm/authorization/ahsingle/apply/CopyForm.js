/**
 * 单站点授权复制申请（快速申请)
 * @author liangyl
 * @version 2020-03-11
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.apply.CopyForm', {
	extend: 'Shell.class.wfm.authorization.ahsingle.apply.Form',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '单站点授权快速申请',
	width: 620,
	height: 445,
	
	bodyPadding: '10px',
	PK: null,
	
	createButtonToolbarItems:function(){
		var me = this,
		    buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.push('->', {
			text: '提交',
			iconCls: 'button-save',
			tooltip: '提交',
			handler: function() {
				me.onSave(true);
			}
		}, 'reset');    
		return buttonToolbarItems;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this;
		var result = me.verificationSubmit();
		if(result == true) {
			me.formtype='add' ;
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
	}
	
});