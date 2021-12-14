/**
 * 商务经理审核（二审)
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.twoaudit.Grid', {
	extend: 'Shell.class.wfm.business.invoice.basic.BasicGrid',
	title: '发票审核列表',
	PayOrgID: '',
	PayOrgName: '',
	PInvoiceMsg: '发票二审',
	/**状态默认为一审*/
	defaultStatusValue: '3',
	/**通过状态*/
	adoptStatus: 5,
	/**二审打回*/
	noadoptStatus: 6,
	ExportType: 2,
	OperMsg: '',
	DigSaveText: '退回',
	SaveText: '审核通过',
	/**默认员工类型*/
	defaultUserType: '',
	SUB_WIN_NO: '4',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get('PInvoice_Id');
				var Status = record.get('PInvoice_Status');
				var ContractID = record.get('PInvoice_ContractID');
				//发票状态为一审通过的为可编辑状态
				if(Status == '3' ) {
					me.openEditForm(id, ContractID);
				} else {
					me.openShowForm(id, ContractID);
				}
			},
			save: function(p) {
				p.close();
				me.onSearch();
				me.OperMsg = '';
			}
		});
	},
	onSaveClick: function(id, Status, text, p) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) || -1;

		var entity = {
			Status: Status,
			TwoReviewInfo: text,
			OperationMemo: text,
			Id: id
		};
		if(userId) {
			entity.TwoReviewManID = userId;
		}
		if(username) {
			entity.TwoReviewMan = username;
		}
		var TwoReviewDate = JShell.Date.toServerDate(ReviewDate);
		if(TwoReviewDate) {
			entity.TwoReviewDate = TwoReviewDate;
		}
		var fields = 'Id,Status';
		var params = {
			entity: entity,
			fields: fields
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', p);
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