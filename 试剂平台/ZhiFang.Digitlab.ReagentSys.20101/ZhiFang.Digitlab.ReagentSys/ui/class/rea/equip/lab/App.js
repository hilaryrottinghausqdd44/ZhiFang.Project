/**
 * 实验室仪器维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.lab.App', {
	extend:'Shell.ux.model.GridFormApp',
    
    title:'实验室仪器维护',
    GridClassName:'Shell.class.rea.equip.lab.Grid',
    FormClassName:'Shell.class.rea.equip.lab.Form',
    
    initComponent:function(){
		var me = this,
			accountName = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME),
			cenOrgId = JShell.REA.System.CENORG_ID;
		
		if(accountName && accountName != JShell.System.ADMINNAME && cenOrgId){
			//列表类参数
		    me.GridConfig = {
		    	defaultWhere:'testequiplab.Lab.Id=' + cenOrgId
		    };
		    //表单类参数
		    me.FormConfig = {
		    	LabId:cenOrgId
		    };
		}
		
		me.callParent(arguments);
	}
});