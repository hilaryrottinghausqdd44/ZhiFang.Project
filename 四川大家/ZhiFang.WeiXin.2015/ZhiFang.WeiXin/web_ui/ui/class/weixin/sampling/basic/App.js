/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define('Shell.class.weixin.sampling.basic.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'微信消费采样',	
	//是否加载过数据
	hasLoaded:false,
	account:'',
	afterRender:function(){
		var me = this;		
		me.callParent(arguments);
		me.form.on({
		    search: function (where) {
				me.grid.externalWhere=where;
				me.grid.onSearch();
			},
			onMaskShow: function (msg) {
				me.showMask(msg);
			},
			onMaskHide: function () {
				me.hideMask();
			},
			openPDF: function (url) {
				window.open(url, '外送清单', 'width=800, height=600 ,top=100,left=300,toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no,alwaysRaised=yes,depended=yes')
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.account = JShell.System.Cookie.get("000301");		
		//创建内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	//创建内部组件
	createItems: function () {
		var me = this;
		me.form = Ext.create('Shell.class.weixin.sampling.basic.Form', {
			region: 'north', itemId: 'Form',height:100,account:me.account,
			header: true, border: false,
			autoScroll: true, split: true,
			collapsible: true, animCollapse: false
		});
		me.grid = Ext.create('Shell.class.weixin.sampling.basic.Grid', {
			region: 'center', itemId: 'Grid', 
			header: true, border: false,
			autoScroll: true, split: true,
			collapsible: false,animCollapse: false
		});

		return [me.form,me.grid];
	},
	//查询数据
	onSearch:function(record){
		var me = this;
		
	},
	//查询按钮点击事件
	onSearchClick: function () {
		var me = this,
			where;
	}
	
	
});