Ext.define('Shell.reportSetting.class.PrintSetting.class.groupGrid',{
	extend: 'Shell.ux.panel.Grid',
	/**默认选中第一行*/
    autoSelect: true,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetPGroupCNameList',
    multiSelect: true,
  
   
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'ID',dataIndex:'SectionNo',
			sortable:false,menuDisabled:true,
			hidden:true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var v = value;
				if(me.AreaEnum != null){
					v = me.AreaEnum[v];
				}
				return v;
			}
		},{
			text:'小组名',dataIndex:'CName',
			isKey:true,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	}
});