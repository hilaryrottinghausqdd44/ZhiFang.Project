/**
 * 物资接口对照关系页签
 * @author liangyl
 * @version 2016-11-14
 */
Ext.define('Shell.class.rea.client.material.AddPanel', {
	extend: 'Ext.tab.Panel',
	title: '物资接口对照关系',
	width: 700,
	height: 585,
	autoScroll: false,
	//业务接口ID
	BusinessID:null,
	//业务接口名称
	BusinessCName:null,
	//物资试剂是否已加载
	isGoodsGridLoad:false,
	//物资试剂是否已加载
	isCompanyGridLoad:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'GoodsGrid':
					    me.GoodsGrid.BusinessCName=me.BusinessCName;
					    me.GoodsGrid.BusinessID=me.BusinessID;
						if(!me.isGoodsGridLoad) me.GoodsGrid.loadDataById(me.BusinessID);
						me.isGoodsGridLoad=true;
						break;
					case 'CompanyGrid':
					    me.CompanyGrid.BusinessCName=me.BusinessCName;
					    me.CompanyGrid.BusinessID=me.BusinessID;
						if(!me.isCompanyGridLoad) me.CompanyGrid.loadDataById(me.BusinessID);
						me.isCompanyGridLoad=true;
						break;
					default:
						break
				}
			}
		});
    },
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.GoodsGrid = Ext.create('Shell.class.rea.client.material.goods.Grid', {
			title: '物资试剂对照',
			BusinessID:me.BusinessID,
			//业务接口名称
			BusinessCName:me.BusinessCName,
			itemId: 'GoodsGrid'
		});
		
	   	me.CompanyGrid = Ext.create('Shell.class.rea.client.material.company.Grid', {
			title: '物资供应商对照',
			BusinessID:me.BusinessID,
			BusinessCName:me.BusinessCName,
			itemId: 'CompanyGrid'
		});
		return [me.GoodsGrid,me.CompanyGrid];
	},
	loadDataById:function(id){
		var me = this;
		me.isGoodsGridLoad=false;
		me.isCompanyGridLoad=false;
		//获取当前激活页签
	    var comtab = me.getActiveTab(me.items.items[0]);
        switch (comtab.itemId){
        	case 'GoodsGrid':
        	    me.GoodsGrid.BusinessCName=me.BusinessCName;
				me.GoodsGrid.BusinessID=me.BusinessID;
        	    me.GoodsGrid.loadDataById(id);
        	    me.isGoodsGridLoad=true;
        		break;
        	default:
        	    me.CompanyGrid.BusinessCName=me.BusinessCName;
				me.CompanyGrid.BusinessID=me.BusinessID;
        	    me.CompanyGrid.loadDataById(id);
        	    me.isCompanyGridLoad=true;
        	 	break;
        }
	},
	clearData:function(){
		var me = this;
		me.GoodsGrid.clearData();
		me.CompanyGrid.clearData();
	}
});