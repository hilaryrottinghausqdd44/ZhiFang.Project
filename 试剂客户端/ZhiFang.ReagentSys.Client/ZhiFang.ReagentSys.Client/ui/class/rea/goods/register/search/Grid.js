/***
 *  注册证查看
 * 只有查询条件框的输入值不为空时才查询
 * @author longfc
 * @version 2017-05-19
 */
Ext.define('Shell.class.rea.goods.register.search.Grid', {
	extend: 'Shell.class.rea.goods.register.basic.Grid',
	title: '注册证查看',

	defaultLoad: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否隐藏产品批号列*/
	hiddenGoodsLotNo:true,
	/**后台排序*/
	remoteSort: false,
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL?isPlanish=true',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = 'goodsregister.Visible=1';
		me.callParent(arguments);
	},
	/**@overwrite 查询按钮点击处理方法*/
	onSearchClick: function(but, value) {
		var me = this;
		//查询栏为空时直接查询
		if(!value) {
			me.internalWhere = "";
			//me.onSearch();
			me.getView().update();
			return;
		}
		me.internalWhere = me.getSearchWhere(value);
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
		var searchWhere = "";
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar) {
			var search = buttonsToolbar.getComponent('search');
			searchWhere = me.getSearchWhere(search.getValue());
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		//如果查询条件值为空,不获取数据
		if(!searchWhere) {
			searchWhere = "1!=1";
		}
		if(me.internalWhere) {
			me.internalWhere += ' and (' + searchWhere + ')';
		} else {
			me.internalWhere = searchWhere;
		}
		return me.callParent(arguments);
	}
});