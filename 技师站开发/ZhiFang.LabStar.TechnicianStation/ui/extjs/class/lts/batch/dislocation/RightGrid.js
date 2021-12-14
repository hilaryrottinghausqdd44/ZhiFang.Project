/**
 * 仪器项目结果列表
 * @author liangyl
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.dislocation.RightGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '仪器项目结果列表',
    width: 285,
    header:false,
    //获取数据服务路径
    selectUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true',
    //默认加载数据
    defaultLoad: false,
    //带功能按钮栏
    hasButtontoolbar: true,
    //排序字段
    defaultOrderBy: [{ property: 'LisEquipItem_IExamine', direction: 'DESC' }],
    //带分页栏
    hasPagingtoolbar: false,
    //是否启用序号列
    hasRownumberer: true,
    //是否默认选中数据
    autoSelect: true,
    /**序号列宽度*/
    rowNumbererWidth: 35,
    
    bodyPadding:'0px 0px 0px 1px',
    /**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function() {
				me.showErrorInView(JShell.Server.NO_DATA);
			}
		})
	},
    initComponent:function(){
		var me = this;
		
		me.buttonToolbarItems =[{xtype: 'label',text: '仪器项目结果列表',margin: '0 0 0 10',style: "font-weight:bold;color:blue;"}];

		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
        //创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
            text: '主键Id',dataIndex: 'LisEquipItem_Id',isKey: true, hidden: true, hideable: false
        }, {
            text: '项目iD',dataIndex: 'LisEquipItem_LBItem_Id',sortable:false, hidden: true,menuDisabled:true,width: 130
        },{
            text: '项目名称',dataIndex: 'LisEquipItem_LBItem_CName',sortable:false,menuDisabled:true,width: 130
        }, {
            text: '项目结果',dataIndex: 'LisEquipItem_EReportValue', sortable:false,menuDisabled:true,width: 100
        }];
		return columns;
	},
    clearData: function () {
        var me = this;
        me.externalWhere = "EquipFormID=-1";
        me.onSearch();
    },
    onLoadDataByID:function(EquipFormID){
    	var me = this;
    	if(EquipFormID){
    		me.externalWhere ='EquipFormID='+ EquipFormID;
    		me.onSearch();
    	}else{
    		me.store.removeAll(); //清空数据
    		me.showErrorInView(JShell.Server.NO_DATA);
    	}
    }
});