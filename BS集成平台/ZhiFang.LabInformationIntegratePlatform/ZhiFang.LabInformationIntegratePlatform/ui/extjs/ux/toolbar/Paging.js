/**
 * 分页栏
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.toolbar.Paging',{
    extend:'Ext.toolbar.Paging',
    alias:'widget.uxPagingtoolbar',
    
    /**数据集*/
	store:null,
	/**默认每页数量*/
	defaultPageSize:50,
	/**分页栏下拉框数据*/
	pageSizeList:[
		[10,10],[20,20],[50,50],[100,100],
		[200,200],[300,300],[400,400],[500,500],
		[1000,1000],[2000,2000]
	],
	/**查询的方法*/
	onSearch:JShell.emptyFn,
	
	displayInfo:true,
	displayMsg:'{0}-{1}/{2}',
	
	/**自定义功能组件*/
	customItems:null,
	
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		if(me.customItems){
			if(Ext.typeOf(me.customItems) === 'object'){
				me.customItems = [me.customItems];
			}
			me.items = me.items.concat(me.customItems);
		}
		me.callParent(arguments);
	},
	/**创建内部*/
	createItems:function(){
		var me = this;
		return ['-',{
			xtype:'combo',mode:'local',editable:false,
	        displayField:'text',valueField:'value',
	        width:60,value:me.defaultPageSize,
			store:new Ext.data.SimpleStore({
				fields:['text','value'],
				data:me.pageSizeList
			}),
			listeners:{
				change:function(com,newValue){
					me.store.pageSize = newValue;
					me.ownerCt.onSearch();
				}
			}
		},'-'];
	},
	
	/**重写显示内容*/
	getPagingItems: function() {
        var me = this;

        return [{
            itemId: 'first',
            tooltip: me.firstText,
            overflowText: me.firstText,
            iconCls: Ext.baseCSSPrefix + 'tbar-page-first',
            disabled: true,
            handler: me.moveFirst,
            scope: me
        },{
            itemId: 'prev',
            tooltip: me.prevText,
            overflowText: me.prevText,
            iconCls: Ext.baseCSSPrefix + 'tbar-page-prev',
            disabled: true,
            handler: me.movePrevious,
            scope: me
        },
        //'-',
        //me.beforePageText,
        {
            xtype: 'numberfield',
            itemId: 'inputItem',
            name: 'inputItem',
            cls: Ext.baseCSSPrefix + 'tbar-page-number',
            allowDecimals: false,
            minValue: 1,
            hideTrigger: true,
            enableKeyEvents: true,
            keyNavEnabled: false,
            selectOnFocus: true,
            submitValue: false,
            // mark it as not a field so the form will not catch it when getting fields
            isFormField: false,
            width: me.inputItemWidth,
            margins: '-1 2 3 2',
            listeners: {
                scope: me,
                keydown: me.onPagingKeyDown,
                blur: me.onPagingBlur
            },
            hidden:true
        },{
            xtype: 'tbtext',
            itemId: 'afterTextItem',
            text: Ext.String.format(me.afterPageText, 1),
            hidden:true
        },
        //'-',
        {
            itemId: 'next',
            tooltip: me.nextText,
            overflowText: me.nextText,
            iconCls: Ext.baseCSSPrefix + 'tbar-page-next',
            disabled: true,
            handler: me.moveNext,
            scope: me
        },{
            itemId: 'last',
            tooltip: me.lastText,
            overflowText: me.lastText,
            iconCls: Ext.baseCSSPrefix + 'tbar-page-last',
            disabled: true,
            handler: me.moveLast,
            scope: me
        },
        '-',
        {
            itemId: 'refresh',
            tooltip: me.refreshText,
            overflowText: me.refreshText,
            iconCls: Ext.baseCSSPrefix + 'tbar-loading',
            handler: function(){me.ownerCt.onSearch();},
            scope: me
        }];
    }
});