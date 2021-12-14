/**
 * 当前检验单检验项目结果
 * @author liangyl
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.examine.basic.Item', {
    extend: 'Shell.ux.grid.Panel',
    title: '当前检验单检验项目结果',
    header:false,

    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
    //默认加载数据
    defaultLoad: false,
    		/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
       /**序号列宽度*/
    rowNumbererWidth: 35,
		/**带功能按钮栏*/
	hasButtontoolbar: false,
		/**默认每页数量*/
	defaultPageSize: 2000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	
	defaultWhere :'listestitem.MainStatusID in (0,-1)',
	 //排序字段
    defaultOrderBy:[{property:"LisTestItem_PLBItem_DispOrder",direction:"ASC"},{property:"LisTestItem_LBItem_DispOrder",direction:"ASC"}],

    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata:function(){
				me.showErrorInView(JShell.Server.NO_DATA);
			}
		})
	},	
	initComponent:function(){
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
    //创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
            text: '主键Id',dataIndex: 'LisTestItem_Id',isKey: true, hidden: true, hideable: false
        },{
            text: '项目Id',dataIndex: 'LisTestItem_LBItem_Id', hidden: true, hideable: false
        }, {
            text: '项目名称',dataIndex: 'LisTestItem_LBItem_CName',sortable:false,menuDisabled:true,width: 180
        }, {
            text: '项目结果',dataIndex: 'LisTestItem_ReportValue', sortable:false,menuDisabled:true,width: 100
        }];
		return columns;
	},
	onLoadDataByID:function(TestFormID){
    	var me = this;
    	if(TestFormID){
    		me.externalWhere ='listestitem.LisTestForm.Id='+ TestFormID;
    	    me.onSearch();
    	}else{
    		me.store.removeAll(); //清空数据
    		me.showErrorInView(JShell.Server.NO_DATA);
    	}
    }
});