/**
 * 用户订单明细列表
 * @author liangyl
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.seach.ItemGrid', {
    extend: 'Shell.ux.grid.Panel',

    title: '用户订单明细列表',
    width: 800,
    height: 500,

    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderItemByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderItemByField',
    /**删除数据服务路径*/
    delUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelOSUserOrderItem',
     /**获取项目*/
    SearchItemAllItemUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchItemAllItemByHQL?isPlanish=true',
    /**默认加载*/
    defaultLoad: false,
    /**订单ID*/
    OrderID: null,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);

    },
    initComponent: function () {
        var me = this;
        //数据列
        me.columns = me.createGridColumns();
        me.buttonToolbarItems = [{
			xtype: 'label',
			text: '订单明细',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 6 10'
		}];
		
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            text: '项目名称', dataIndex: 'OSUserOrderItem_ItemCName', flex:1,
            sortable: false, defaultRenderer: true
        }, {
            text: '是否特推', dataIndex: 'OSUserOrderItem_RecommendationItemProductID', width: 58,
            sortable: false,   align: 'center',
            renderer: function(value, meta, record) {
            	var v = value || '';
            	if (v) {
						meta.style = 'color:green';
						v =JShell.All.TRUE;
					}else{
						meta.style = 'color:red';
						v = JShell.All.FALSE;
					}
				return v;
			}
        }, {
            text: '市场价格', dataIndex: 'OSUserOrderItem_MarketPrice', width: 75,
            sortable: false, defaultRenderer: true
        }, {
            text: '大家价格', dataIndex: 'OSUserOrderItem_GreatMasterPrice', width: 75,
            sortable: false, defaultRenderer: true
        }, {
            text: '实际价格', dataIndex: 'OSUserOrderItem_DiscountPrice', width: 75,
            sortable: false, defaultRenderer: true
        },{
            text: '主键ID', dataIndex: 'OSUserOrderItem_Id', isKey: true, hidden: true, hideable: false
        }];

        return columns;
    },
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
		//根据订单Id
		if(me.OrderID) {
			params.push("osuserorderitem.UOFID=" + me.OrderID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**改变默认条件*/
	changeDefaultWhere:function(){
		var me = this;
		
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('osuserorderitem.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and osuserorderitem.IsUse=1';
			}
		}else{
			me.defaultWhere = 'osuserorderitem.IsUse=1';
		}
	}
});