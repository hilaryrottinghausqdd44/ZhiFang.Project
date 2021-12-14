Ext.define("Shell.class.weixin.blcloentcontrol.addList", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?isPlanish=true',
	gridStore:'',
    /**默认每页数量*/
    defaultPageSize: 25,
	//hasRefresh:true,
	hasSearch:true,
    afterRender: function () {
        this.callParent(arguments);
    },

    initComponent: function () {
        var me = this,
			wherearr = [];		
		if(me.gridStore.totalCount != 0){
			me.gridStore.each(function (records) {
				wherearr.push(records.data.BusinessLogicClientControl_CLIENTELE_Id);			    
			});
			me.externalWhere = "clientele.Id not in("+wherearr.join(',')+")";
		}
        me.columns = [
            {
                dataIndex: 'CLIENTELE_Id',
                text: '实验室ID',
                width: 60,
				hidden:true,
                sortable: false
            },{
                dataIndex: 'CLIENTELE_CNAME',
                text: '实验室名称',
                width: 250,
                sortable: false
            }
        ];
        //查询框信息
        me.searchInfo = {width:145,emptyText:'名称',isLike:true,	fields:['clientele.CNAME']};
        me.callParent(arguments);
    }
});