/**
 * 报告列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.report.search.Grid', {
	extend: 'Shell.ux.grid.Panel',
	
	width: 460,
	height: 500,
	
	/**语言包内容*/
	Shell_class_report_search_Grid:{
		title:{
			TEXT:'报告列表'
		},
		items:{
			PATNO:'病历号',
			RECEIVEDATE:'核收日期',
			CNAME:'姓名',
			SAMPLENO:'样本号',
			ITEMNAME:'检验项目',
			SECTIONNO:'检验小组编号',
			CLIENTNO:'送检单位编码',
			SECTIONTYPE:'小组类型',
			REPORTFORMID:'主键',
			CARDNUMBER:'卡号'
		}
	},

	/**获取数据服务路径*/
	selectUrl: '/ServiceWCF/ReportFormService.svc/SelectReport',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**卡号匹配字段数组*/
	cardArray:['PATNO'],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//替换语言包
		me.changeLangage('Shell.class.report.search.Grid');
		
		//标题
		me.title = me.Shell_class_report_search_Grid.title.TEXT;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	createButtonToolbarItems: function() {
		var me = this,
			cardArray = me.cardArray,
			len = cardArray.length,
			cardSearchArray = [],
			items = [];

		//查询框信息
//		me.searchInfo = {
//			width: 110,
//			isLike: true,
//			itemId: 'Search',
//			emptyText: me.Shell_class_report_search_Grid.items.CNAME + '/' + 
//				me.Shell_class_report_search_Grid.items.PATNO,
//			fields: ['CNAME', 'PATNO']
//		};
//
//		items.push('refresh', '-', {
//			type: 'search',
//			info: me.searchInfo
//		}, '->', 'COLLAPSE_RIGHT');

		for(var i=0;i<len;i++){
			var text = me.Shell_class_report_search_Grid.items[cardArray[i]];
			cardSearchArray.push(text);
		}
		
		items.push({
			xtype:'trigger',itemId:'name',
			labelAlign:'right',labelWidth:35,width:140,
			fieldLabel:me.Shell_class_report_search_Grid.items.CNAME,
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			onTriggerClick:function(){me.onSearch();},
			listeners:{keyup:{fn:function(field,e){me.onTriggerKeyup(field,e);}}}
		},{
			xtype:'trigger',itemId:'card',
			labelAlign:'right',labelWidth:45,width:150,
			fieldLabel:me.Shell_class_report_search_Grid.items.CARDNUMBER,
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			emptyText:cardSearchArray.join(','),
			onTriggerClick:function(){me.onSearch();},
			listeners:{keyup:{fn:function(field,e){me.onTriggerKeyup(field,e);}}}
		});
		
		items.push('->', 'COLLAPSE_RIGHT');
		
		return items;
	},
	/**组件监听*/
	onTriggerKeyup:function(field,e){
		var me = this;
		if(e.getKey() == Ext.EventObject.ESC){
    		field.setValue('');
    		me.onSearch();
    	}else if(e.getKey() == Ext.EventObject.ENTER){
			me.onSearch();
    	}
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			dataIndex: 'RECEIVEDATE',
			text: me.Shell_class_report_search_Grid.items.RECEIVEDATE,
			width: 100,
			sortable: false,
			isDate:true
		}, {
			dataIndex: 'CNAME',
			text: me.Shell_class_report_search_Grid.items.CNAME,
			width: 60,
			defaultRenderer:true
		}, {
			dataIndex: 'SAMPLENO',
			text: me.Shell_class_report_search_Grid.items.SAMPLENO,
			width: 100,
			defaultRenderer:true
		}, {
			dataIndex: 'ItemName',
			text: me.Shell_class_report_search_Grid.items.ITEMNAME,
			width: 100,
			defaultRenderer:true
		}, {
			dataIndex: 'SECTIONNO',
			text: me.Shell_class_report_search_Grid.items.SECTIONNO,
			hideable: false,
			hidden: true
		}, {
			dataIndex: 'CLIENTNO',
			text: me.Shell_class_report_search_Grid.items.CLIENTNO,
			hideable: false,
			hidden: true
		}, {
			dataIndex: 'SectionType',
			text: me.Shell_class_report_search_Grid.items.SECTIONTYPE,
			hideable: false,
			hidden: true
		}, {
			dataIndex: 'ReportFormID',
			text: me.Shell_class_report_search_Grid.items.REPORTFORMID,
			hideable: false,
			hidden: true,
			type: 'key'
		}];

		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult:function(data){
		data.list = data.value.rows;
		return data;
	},
	/**收缩面板*/
	onCollapseClick:function(){
		this.collapse();
	},
	/**查询数据*/
	onSearch: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			name = buttonsToolbar.getComponent('name').getValue(),
			card = buttonsToolbar.getComponent('card').getValue(),
			cardArray = me.cardArray,
			len = cardArray.length,
			where = [];
			
		if(name){
			where.push("CNAME='" + name + "'");
		}
		if(card){
			var cardWhere = [];
			for(var i=0;i<len;i++){
				var text = me.Shell_class_report_search_Grid.items[cardArray[i]];
				if(text){
					cardWhere.push(cardArray[i] + "='" + card + "'");
				}
			}
			if(cardWhere.length > 0){
				where.push("(" + cardWhere.join(" or ") + ")");
			}
		}
		
		me.internalWhere = where.join(" and ");
		
		me.load(null, true);
	}
});