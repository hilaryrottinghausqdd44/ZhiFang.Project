/**
 * 供货明细列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.lodop.Lodop', {
	LODOP_OB:'LODOP_OB',
	LODOP_EM:'LODOP_EM',
	/**获取LODOP*/
	getLodop:function(){
		var LODOP = getLodop(document.getElementById(this.LODOP_OB),document.getElementById(this.LODOP_EM));
		if(!LODOP){
			JShell.Msg.error('LODOP打印控件不存在!');
			return;
		}
		
		LODOP.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
		return LODOP;
	}
});