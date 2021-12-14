/**
 * 出入库历史列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.stock.TempApp', {
	extend: 'Shell.class.rea.stock.TempGrid',
	
	/**默认数据条件*/
	defaultWhere:'',
	/**机构类型*/
    ORGTYPE:null,
	
	initComponent: function() {
		var me = this;
		
		if(me.ORGTYPE){
			var type = me.ORGTYPE.toLocaleUpperCase();
			if(type == 'COMP'){
				me.defaultWhere = 'cenqtydtltemp.Comp.Id=' + JShell.REA.System.CENORG_ID;
			}else if(type == 'PROD'){
				me.defaultWhere = 'cenqtydtltemp.Prod.Id=' + JShell.REA.System.CENORG_ID;
			}
		}
		
		me.callParent(arguments);
	}
});