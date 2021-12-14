/**
 * 特推项目产品列表
 * @author liangyl
 * @version 2017-03-21
 */
Ext.define('Shell.class.weixin.item.product.apply.Grid', {
	extend: 'Shell.class.weixin.item.product.basic.Grid',
	title: '特推项目产品列表',
		/**下载图片*/
	DownLoadImageUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DownLoadOSRecommendationItemProductByID',
	
	/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**区域*/
	AreaID:null,
	/**区域*/
	ClientNo:null,
	AreaName:null,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
    /**是否启用删除按钮*/
	hasDel: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				me.openEditForm(id);
			}
		});
	},
	createButtonToolbarItems:function(){
    	var me = this,
			buttonToolbarItems = me.callParent(arguments);
		buttonToolbarItems.splice(1,0,'add','-',{
			width:140,boxLabel:' 显示有效特推项目',itemId:'IsValid',
		    xtype:'checkbox',checked:false,
		    listeners:{
		    	change:function(com,  newValue,  oldValue,  eOpts ){
					me.onSearch();
				}
		    }
		});
		return buttonToolbarItems;
    },
	 /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns=me.callParent(arguments);
        columns.push({
			xtype: 'actioncolumn',
			text: '图片',
			align: 'center',
			width: 35,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id= rec.get(me.PKField);
					if(id){
						me.DownLoadImage(id,'RecommendationItemProduct');
					}
				}
			}]
		});
		return columns;
    },
    /**下载*/
	DownLoadImage: function(Id,imageType) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.DownLoadImageUrl;
		url += '?recommendationitemproductID=' + Id + '&operateType=1&imageType='+imageType;
		window.open(url);
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			params = [];
			
		me.internalWhere = '';
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		//根据区域Id
		if(me.AreaID) {
			params.push("osrecommendationitemproduct.AreaID =" + me.AreaID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		IsValid = buttonsToolbar.getComponent('IsValid').getValue();
		var  url = me.callParent(arguments);
		url += '&effective=' + IsValid;
		return url;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.weixin.item.product.apply.AddPanel', {
//			SUB_WIN_NO:'1',//内部窗口编号
			resizable: false,
			formtype:'add',
			PK:null,
			AreaID:me.AreaID,
			ClientNo:me.ClientNo,
			AreaName:me.AreaName,
			listeners: {
				save: function(p,id) {
					//if(p.AreaID)me.AreaID=p.AreaID;
					//if(p.ClientNo)me.ClientNo=p.ClientNo;
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**打开修改页面*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.weixin.item.product.apply.AddPanel', {
//			SUB_WIN_NO:'2',//内部窗口编号
			resizable: false,
			formtype:'edit',
			PK:id,
			AreaID:me.AreaID,
			AreaName:me.AreaName,
			ClientNo:me.ClientNo,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	}
});