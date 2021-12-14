/**
 * 财务锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.LockFGrid', {
	extend: 'Shell.class.pki.balance.LockAGrid',

	title: '财务锁定',

	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_SelectFinanceLocking',
	/**默认条件*/
	defaultWhere: '(nrequestitem.IsLocked=1 or nrequestitem.IsLocked=2)',
	/**锁定的提示文字*/
	lockTooltipText: '财务锁定',
	/**锁定的状态值*/
	lockValue: '2',
	/**显示对账状态下拉框*/
	showIsLockedCombobox: false,
	/**锁定的确认内容*/
	lockText: '您确定要财务锁定吗？',
	/**解除锁定的确认内容*/
	openText: '您确定要解除财务锁定吗？'
});