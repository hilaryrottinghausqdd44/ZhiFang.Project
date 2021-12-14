/**
 * 上级经销商选择树
 * @author liangyl
 * @version 2017-07-18
 */
Ext.define('Shell.class.pki.balance2.CheckTree', {
	extend: 'Shell.class.pki.dealer.dealer.CheckTree',
	title: '选择经销商',
	width: 333,
	height: 500,
	cratetopToolbar : function(){
		var me=this;
		me.topToolbar = me.callParent(arguments);
		me.topToolbar.splice(1,0,'-',{
			text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
			handler:function(){me.fireEvent('accept',me,null);}
		});
		return me.topToolbar;
	}
});