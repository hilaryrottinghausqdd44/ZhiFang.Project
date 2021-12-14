/**
 * 套餐项目选择列表
 * @author longfc
 * @version 2017-04-17
 */
Ext.define('Shell.class.weixin.item.check.PItemGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '套餐项目选择列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemByHQL?isPlanish=true',
	/**是否单选*/
	checkOne: true,
	/**区域代码*/
	ClientNo: null,
	ItemID:null,
	/**项目名称*/
    ItemName:'',
    	/**默认加载*/
	defaultLoad: false,
	autoSelect:true,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onSeachData();
	},
	onSeachData:function(){
		var me=this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
		    search = buttonsToolbar.getComponent('search');
		    if(me.ItemName) search.setValue(me.ItemName);
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		
		me.defaultWhere = ' blabtestitem.IsCombiItem=1 and blabtestitem.Visible=1';
		
		//查询框信息
		me.searchInfo = {
			width: '55%',
			emptyText: '名称/代码',
			isLike: true,
			itemId: 'search',
			fields: ['blabtestitem.CName', 'blabtestitem.ShortCode']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '项目编号',
			dataIndex: 'BLabTestItem_ItemNo',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'BLabTestItem_CName',
			width: 120,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '代码',
			dataIndex: 'BLabTestItem_ShortCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'BLabTestItem_ShortName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '英文名称',
			dataIndex: 'BLabTestItem_EName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BLabTestItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			params = [];

		me.internalWhere = '';
		if(buttonsToolbar) {
			search = buttonsToolbar.getComponent('search').getValue();
		}
		//根据区域Id
		if(me.ClientNo) {
			params.push("blabtestitem.LabCode=" + me.ClientNo);
		}
		if(me.ItemID && search && search==me.ItemName ){
			params.push("blabtestitem.Id='" + me.ItemID+"'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});