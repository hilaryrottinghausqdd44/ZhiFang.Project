Ext.define("Shell.class.setting.ScriptingOptions.update.dbGrid",{
	extend:'Shell.ux.panel.Grid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
    selectUrl: '/ServiceWCF/ReportFormService.svc/GetClassDic?classname=DatabaseType&classnamespace=ZhiFang.ReportFormQueryPrint.Model',
    /**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
    autoSelect: true,
	copyTims:0,
	/**默认加载*/
	defaultLoad: true,
	initComponent:function(){
		var me = this;
		me.columns=me.createGcolumns();
		me.callParent(arguments);
	},
	
	afterRender:function(){
		var me =this;
		me.callParent(arguments);
	},
	
	createGcolumns:function(){
		var me =this;
		var columns=[
		{
			text:'数据库类型编码',
            dataIndex:'Id',
			width:100,
			isKey:true
		},{
			text:'数据库类型',
            dataIndex:'Describe',
			width:200,
		}];
		return columns;
	},
	

	
});
