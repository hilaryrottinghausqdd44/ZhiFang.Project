Ext.define('Shell.class.weixin.dict.lab.BLabSampleType.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'实验室选择列表',
    width:350,
    height:350,
    
    /**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:true,
    /**带分页栏*/
	hasPagingtoolbar:false,
	/**默认每页数量*/
	defaultPageSize: 50000,
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:200,isLike:true,itemId: 'Search',
			emptyText:'名称/编号',
			fields:['clientele.CNAME']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			dataIndex: 'CLIENTELE_CNAME',text: '名称',flex: 1,minWidth:100,defaultRenderer: true
		},  {
			dataIndex: 'CLIENTELE_Id',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		if(!me.searchInfo.width) me.searchInfo.width = 145;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		me.buttonToolbarItems.push({type:'search',info:me.searchInfo});
		
		if(me.hasClearButton){
			me.buttonToolbarItems.unshift({
				text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
				handler:function(){me.fireEvent('accept',me,null);}
			},'->');
		}
		if(me.hasAcceptButton){
			me.buttonToolbarItems.push('->','accept');
		}
	}
});