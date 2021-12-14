/**
 * 还款审核
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.prepayment.oneaudit.EditPanel', {
	extend: 'Shell.class.wfm.business.prepayment.basic.EditTabPanel',
	title: '还款审核',
	FormClassName: 'Shell.class.wfm.business.prepayment.basic.ContentPanel',
	width: 750,
	height: 370,
	/**ID*/
	PK: null,
	/**一审通过状态*/
	OverStatus: 4,
	/**一审退回状态*/
	BackStatus: 3,
	/**通过文字*/
	OverName: '审核通过',
	/**退回文字*/
	BackName: '撤回',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.callParent(arguments);
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit, id) {
		var me = this;
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">处理意见</div>',
			msg: '',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;
			me.onSaveClick(isSubmit, id, text);
		});
	},
	onSaveClick: function(isSubmit, id, text, p) {
		var me = this,
			Status = me.OverStatus;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
			//一审通过
		if(isSubmit) {
			Status = me.OverStatus;
		} else {
			Status = me.BackStatus;
		}
		var entity = {
			Status: Status,
			OperationMemo: text,
			ReviewInfo: text,
			Id: id
		};
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		if(ReviewDateStr) {
			entity.ReviewDate = ReviewDateStr;
		}
		//登录员工
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//登录员工名称
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId) {
			entity.ReviewManID = userId;
		}
		if(userName) {
			entity.ReviewMan = userName;
		}
		var fields = 'Id,Status,ReviewDate,ReviewManID,ReviewMan,ReviewInfo';
		var params = {
			entity: entity,
			fields: fields
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	}
});