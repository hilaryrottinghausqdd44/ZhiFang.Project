/**
 * 项目选择
 * @author liangyl
 * @version 2017-05-19
 */
Ext.define('Shell.class.wfm.business.pproject.basic.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '项目选择列表',
	height: 460,
	width: 350,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectByHQL?isPlanish=true',
	/**是否单选*/
	checkOne: true,
	ItemTypeEnum:{},
	IsStandard:1,
	initComponent: function() {
		var me = this;
		me.searchInfo = {
			width: 165,
			emptyText: '项目名称',
			itemId: 'search',
			isLike: true,
			fields: ['pproject.CName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '项目名称',dataIndex: 'PProject_CName',width: 150,
			sortable: false,defaultRenderer: true
		},{
			text: '项目类型',dataIndex: 'PProject_TypeID',width: 100,
			sortable: true,menuDisabled: true,
            renderer: function(value, meta) {
				var v = value;
				if(me.ItemTypeEnum != null){
					v = me.ItemTypeEnum[value];
				}
				return v;
			}
		}, {
			text: '主键ID',dataIndex: 'PProject_Id',isKey: true,hidden: true,hideable: false
		}];
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
		    search=null,
			params = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar');
        if(!buttonsToolbar) return;
		search = buttonsToolbar.getComponent('search').getValue();

	    if(me.IsStandard){
	    	params.push("pproject.IsStandard="+me.IsStandard);
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