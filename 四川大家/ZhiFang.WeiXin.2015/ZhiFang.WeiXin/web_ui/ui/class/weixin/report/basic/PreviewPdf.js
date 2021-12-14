/**
 *预览PDF文件
 * @author liangyl
 * @version 2017-03-08
 */
Ext.define('Shell.class.weixin.report.basic.PreviewPdf', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '预览PDF文件',
	layout:'border',
	width: 500,
	height: 400,
	hasBtn:true,
	hasBtntoolbar:true,
	PK:'',
	/**获取数据服务路径*/
	hasColse:false,
	URL:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.showPdf(me.URL);
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "预览PDF文件";
        me.items =[];	
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasBtntoolbar) items.push(me.createButtonbottomtoolbar());
		return items;
	},
	//查看PDF内容
    showPdf : function(url,isClear){
    	var me=this;
    	var a='%22';
    	var html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>文件还未获取</div>';
    	if(url){
           html = '<iframe ' +
				'height="100%" width="100%" frameborder="0" ' +
				'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
				'height:100%;width:100%;position:absolute;' +
				'top:0px;left:0px;right:0px;bottom:0px;" '+ 'src=' + url+ '>' +
			'</iframe>';
    	}
    	if(isClear == true) html = '';
    	me.update(html);
    },
	/**创建功能按钮栏*/
	createButtonbottomtoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if (items.length == 0) {
			items.push( '->');
			if(me.hasColse){
				items.push({
					xtype: 'button',
					itemId: 'btnColse',
					iconCls: 'button-del',
					text: "关闭",
					tooltip: '关闭',
					handler: function() {
						me.fireEvent('onCloseClick', me);
						me.close();
					}
				})
			}
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottombuttonsToolbar',
			items: items
		});
	}
});