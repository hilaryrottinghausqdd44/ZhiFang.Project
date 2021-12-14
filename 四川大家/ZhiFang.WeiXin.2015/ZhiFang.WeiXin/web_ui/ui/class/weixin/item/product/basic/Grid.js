/**
 * 特推项目产品列表
 * @author liangyl
 * @version 2017-03-21
 */
Ext.define('Shell.class.weixin.item.product.basic.Grid', {
    extend: 'Shell.ux.grid.Panel',

    title: '特推项目产品列表',
    width: 800,
    height: 500,
    /**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProductOrEffectiveByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSRecommendationItemProductByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelOSItemProductClassTree',

    /**默认加载*/
    defaultLoad: true,

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,

    defaultOrderBy: [{ property: 'OSRecommendationItemProduct_DispOrder', direction: 'ASC' }],

    afterRender: function () {
        var me = this;
        me.callParent(arguments);
    },
    initComponent: function () {
        var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
        //数据列
        me.columns = me.createGridColumns();
		
        me.callParent(arguments);
    },
    createButtonToolbarItems:function(){
    	var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		   //查询框信息
        me.searchInfo = {
            width: 145, emptyText: '名称', isLike: true,itemId:'search',
            fields: ['osrecommendationitemproduct.CName']
        };
		buttonToolbarItems.unshift('refresh');
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
       var columns = [{
			text:'名称',dataIndex:'OSRecommendationItemProduct_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
            text: '状态', dataIndex: 'OSRecommendationItemProduct_Status', width: 70,
            sortable: false,	renderer:function(value,meta){
            	var v = value || '';
            	if(v){
            		var info = JShell.System.ClassDict.getClassInfoById('OSRecommendationItemProducStatus',v);
            		if(info){
            			v = info.Name;
            			meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
            		}
            	}
            	return v;
            }
        },{
			text:'大家价格',dataIndex:'OSRecommendationItemProduct_GreatMasterPrice',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'市场价格',dataIndex:'OSRecommendationItemProduct_MarketPrice',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'折扣价格',dataIndex:'OSRecommendationItemProduct_DiscountPrice',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'折扣率',dataIndex:'OSRecommendationItemProduct_Discount',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'咨询费',dataIndex:'OSRecommendationItemProduct_BonusPercent',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text: '有效开始时间',
			dataIndex: 'OSRecommendationItemProduct_StartDateTime',
			width: 85,
			sortable: true,
			menuDisabled: false,
			isDate: true
		},{
			text: '有效结束时间',
			dataIndex: 'OSRecommendationItemProduct_EndDateTime',
			width: 85,
			sortable: true,
			menuDisabled: false,
//			isDate: true,
			renderer:function(value,meta){
            	var v =JcallShell.Date.toString(value, true) || '';
            	var Sysdate = JcallShell.System.Date.getDate();
	        	var ServiceDate = JcallShell.Date.toString(Sysdate,true);
	        	var strval=JcallShell.Date.toString(v,true);
            	if(ServiceDate>strval){
            		meta.style = 'background-color:#FF3030';
            	}
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
            	return v;
            }
		}, {
			text:'发布后是否可评论',dataIndex:'OSRecommendationItemProduct_IsDiscuss',width:100,
			sortable:false,menuDisabled:true,hidden:true,defaultRenderer:true
		},{
			text:'次序',dataIndex:'OSRecommendationItemProduct_DispOrder',
			width:40,align:'center',sortable:false,hidden:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'OSRecommendationItemProduct_Id',isKey:true,hidden:true,hideable:false
		}];

        return columns;
    },
    /**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.WeiXin.Entity','OSRecommendationItemProducStatus',function(){
			if(!JShell.System.ClassDict.OSRecommendationItemProducStatus){
    			JShell.Msg.error('未获取到特推项目产品状态，请刷新列表');
    			return;
    		}
			me.load(null, true, autoSelect);
    	});
	},
	/**改变默认条件*/
	changeDefaultWhere:function(){
		var me = this;
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('osrecommendationitemproduct.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and osrecommendationitemproduct.IsUse=1';
			}
		}else{
			me.defaultWhere = 'osrecommendationitemproduct.IsUse=1';
		}
	}
   
});