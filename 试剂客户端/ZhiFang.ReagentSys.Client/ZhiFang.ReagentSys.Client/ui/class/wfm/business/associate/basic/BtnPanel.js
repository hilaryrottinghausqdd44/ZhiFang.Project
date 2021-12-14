/**
 * 按钮
 * @author liangyl	
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.basic.BtnPanel', {
    extend: 'Shell.ux.panel.AppPanel',
    title: '按钮',
    width:70,defaults:{margins:'0 0 5 0'},
    style:{background:'#fff'},
	layout:{type:'vbox',padding:'5',pack:'center',align:'center'},
	bodyStyle:"background-color:#D7E4F2",
	border:false,
	hasClient:true,
	hasPayOrg:true,
	ClientMsg:'对比 客 户',
	PayOrgMsg:'对付款单位',
	BtnWidth:85,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
    },
    initComponent: function () {
        var me = this;
       	me.items = me.createItems();
        //创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
       	me.addEvents('onClientClick', 'onPayClick');
        me.callParent(arguments);
    },
    createItems: function() {
		var me = this;
		var items=[];
		if(me.hasClient){
			items.push({
				xtype:'button',text:me.ClientMsg,width:me.BtnWidth,//iconAlign:'right',
				handler:function(){
					me.fireEvent('onClientClick', me);
				}
			});
		}
		if(me.hasPayOrg){
			items.push({
				xtype:'button',text:me.PayOrgMsg,width:me.BtnWidth,//iconAlign:'right',
				handler:function(){
					me.fireEvent('onPayClick', me);
				},margins:'0'
			});
		}
		return items;
   },
   /**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || ['add'];

		if (items.length == 0) {
			if (me.hasRefresh) items.push('refresh');
			if (me.hasAdd) items.push('add');
			if (me.hasEdit) items.push('edit');
			if (me.hasDel) items.push('del');
			if (me.hasShow) items.push('show');
			if (me.hasSave) items.push('save');
			if (me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;

		var config = {
			dock: 'bottom',
			itemId:'pagingToolbar'
		};

		return Ext.create('Shell.ux.toolbar.Paging', config);
	}
});