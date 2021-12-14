/**
 * 项目分类树
 * @author Jcall
 * @version 2016-12-28
 */
Ext.define('Shell.class.weixin.item.type.Tree',{
    extend:'Shell.ux.tree.Panel',
	
	title:'项目分类树',
	width:300,
	height:500,
	
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeByAreaID?fields=OSItemProductClassTree_Id,OSItemProductClassTree_CName,OSItemProductClassTree_PItemProductClassTreeID',
	/**默认加载数据*/
	defaultLoad:true,
	/**根节点*/
	root:{
		text:'所有检测项目产品分类',
		iconCls:'main-package-16',
		id:0,
		tid:0,
		leaf:false,
		expanded:false
	},
	/**区域ID*/
	AreaID:0,
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.addEvents('changeResult');
		me.topToolbar = me.topToolbar || ['-','->',{
			iconCls:'button-right',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		me.callParent(arguments);
	},
	
	createDockedItems:function(){
		var me = this;
		var dockedItems = me.callParent(arguments);
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		return dockedItems;
	},
	changeData:function(data){
    	return data;
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl+'&areaID='+me.AreaID;
		return Ext.create('Ext.data.TreeStore', {
			fields: me.getStoreFields(),
			defaultRootProperty: me.defaultRootProperty,
			root: me.root,
			proxy: {
				type: 'ajax',
				url: url,
				extractResponseData: function(response) {
					return me.changeStoreData(response);
				}
			},
			listeners: {
				beforeload: function() {
					if(!me.canLoad) return false;
					return me.onBeforeLoad();
				},
				load: function (store, node, records, successful) {
					me.onAfterLoad(records, successful);
				}
			},
			defaultLoad: false
		});
	},
		/**点击刷新按钮*/
	onRefreshClick: function() {
		var me=this;
		me.canLoad = true;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.store.load();
		me.fireEvent('changeResult', me);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
			
        if(me.AreaID){
        	url += (url.indexOf('?') == -1 ? '?' : '&') + 'areaID='+me.AreaID;
        }
		return url;
	}
});
	