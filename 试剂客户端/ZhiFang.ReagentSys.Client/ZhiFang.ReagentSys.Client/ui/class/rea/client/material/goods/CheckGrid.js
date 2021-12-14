/**
 * 货品选择列表
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.material.goods.CheckGrid',{
    extend:'Shell.class.rea.client.basic.CheckPanel',
    title:'货品选择列表',
    width:850,
    height:400,
    
    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:false,
	/**已存在的试剂*/
	Ids:null,
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:200,isLike:true,itemId: 'Search',
			emptyText:'货品编码/名称/物资编码',
			fields:['reagoods.ReaGoodsNo','reagoods.CName','reagoods.MatchCode']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	initButtonToolbarItems:function(){
		var me = this;
		
		if(!me.searchInfo.width) me.searchInfo.width = 145;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		me.buttonToolbarItems.push({type:'search',info:me.searchInfo});
		
//		if(me.hasClearButton){
//			me.buttonToolbarItems.unshift({
//				text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
//				handler:function(){me.fireEvent('accept',me,null);}
//			});
//		}
		if(me.hasAcceptButton){
			me.buttonToolbarItems.push({
	            xtype:'checkboxfield',margin:'0 0 0 10',
	            boxLabel: '取机构货品的物资编码', 
	            name: 'checkMatchCode',itemId:'checkMatchCode'
	        },'->','accept');
		}
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			dataIndex: 'ReaGoods_CName',text: '货品名称',width:180,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ReaGoodsNo',text: '货品编码',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',text: '单位',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',text: '规格',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_MatchCode',text: '物资编码',hidden:false,width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_GoodsClass',text: '一级分类',hidden:false,width: 100,
			defaultRenderer: true
		},{
			text: '二级分类',dataIndex: 'ReaGoods_GoodsClassType',
			width: 100,hidden:false,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_Id',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		return columns;
	},
	/**确定按钮处理*/
	onAcceptClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
            checkMatchCode=buttonsToolbar.getComponent('checkMatchCode');
		if(me.checkOne){
			if(records.length != 1){
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			me.fireEvent('accept',me,records[0],checkMatchCode.getValue());
		}else{
			if(records.length == 0){
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}
			me.fireEvent('accept',me,records,checkMatchCode.getValue());
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		me.internalWhere = "";
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		if(me.Ids) {
			params.push('reagoods.Id not in ('+me.Ids+')');
		}
		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	}
	
});