/**
 * 【功能】
 * 
 * 【可配参数】
 * 
 * 
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.YearMonthToolbar',{
	extend:'Ext.toolbar.Toolbar',
	type:'toolbar',
	alias:'widget.zhifanguxyearmonthtoolbar',
	
	/**
	 * 渲染完后
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initListeners();
	},
	initComponent:function(){
		var me = this;
		me.bodyPadding = 2;
		me.items = me.createItems();//创建面板内容
		me.callParent(arguments);
	},
	/**
	 * 创建面板内容
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [{
				xtype:'iqcmonthmonthcombobox',itemId:'yearmonth',fieldLabel:'质控年月',
				width:160,labelWidth:60,labelAlign:'right',value:new Date(),
				editable:true,typeAhead:true,queryMode:'local',
	            listeners:{beforequery:me.comboboxFuzzyQuery}
			},{
				xtype:'radio',name:'type',inputValue:'2',itemId:'type2',margin:'0 0 0 4',listeners:{change:function(radio,v){me.showOrHideYearMonth(v);}}
			},{
				xtype:'datefield',itemId:'dateStart',fieldLabel:'质控日期',
				width:160,labelWidth:60,labelAlign:'right',disabled:true,
				format:'Y-m-d',value:new Date()
			},{
				xtype:'datefield',itemId:'dateEnd',fieldLabel:'-',disabled:true,
				width:110,labelWidth:10,labelSeparator:'',
				format:'Y-m-d',value:new Date()
			}];
		return items;
	},
	/**
	 * 初始化监听
	 * @private 
	 */
	initListeners:function(){
		var me = this;
	},
	getValue:function(){
		var me = this;
		me.getComponent('');
	}
});