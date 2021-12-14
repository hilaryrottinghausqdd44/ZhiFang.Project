/**
 * 知识库选择列表
 * @author liangyl
 * @version 2017-04-13
 */
Ext.define('Shell.class.wfm.business.pproject.knowledgebase.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'项目文档模板选择',
    width:370,
    height:340,
    /**我的阅读文档查询时是否查询传入节点的子孙节点*/
	isSearchChildNode: true,
	/**项目任务作业指导类型Id*/
	BDictTreeId: "5731661763405177841",//"5731661763405177841",
	IDS:'5133187353604336821',
    /**获取数据服务路径*/
	selectUrl: '/QMSService.svc/QMS_UDTO_SearchFFileByBDictTreeId?isPlanish=true',
    /**默认排序字段*/
	defaultOrderBy: [{
		property: 'FFile_IsTop',
		direction: 'DESC'
	}, {
		property: 'FFile_PublisherDateTime',
		direction: 'DESC'
	}, {
		property: 'FFile_BDictTree_Id',
		direction: 'ASC'
	}, {
		property: 'FFile_Status',
		direction: 'DESC'
	}, {
		property: 'FFile_Title',
		direction: 'ASC'
	}],
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'ffile.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:'68%',emptyText: '标题/编号/关键字',isLike: true,
			itemId: 'search',fields: ['ffile.Title', 'ffile.No', 'ffile.Keyword']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text: '类型',dataIndex: 'FFile_BDictTree_CName',hidden: true,width: 95,hideable: false
		},{
			text: '文档标题',dataIndex: 'FFile_Title',flex: 1,sortable: true,menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var IsTop = record.get("FFile_IsTop");
				if(IsTop == "true") {
					value = "<b style='color:red;'>【置顶】</b>" + value;
				}
				return value;
			}
		},{
			text: '编号',dataIndex: 'FFile_No',hidden: false,width: 120,hideable: false
		}, {
			text: '关键字',
			dataIndex: 'FFile_Keyword',
			hidden: true,
			width: 80,
			hideable: false
		},{
			text:'主键ID',dataIndex:'FFile_Id',isKey:true,hidden:true,hideable:false
		}];
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = [];	
		var where = "",
			arr = [],
			url = JShell.System.Path.ROOT + me.selectUrl + "&isSearchChildNode=" + me.isSearchChildNode;

		if(me.BDictTreeId && me.BDictTreeId.toString().length > 0) {
			where = 'id=' + me.BDictTreeId + '^';
		} else if(me.IDS && me.IDS.toString().length > 0) {
			where = 'id=' + me.IDS + '^';
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
        if(!buttonsToolbar) return;
        var	search = buttonsToolbar.getComponent('search').getValue();

		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		if(arr.length > 0) {
			where += '(' + arr.join(" and ") + ')';
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + JcallShell.String.encode(where) + '&fields=' + me.getStoreFields(true).join(',');

		return url;
	},
		/**左树节点选择改变后联动清除查询工具栏里的树信息*/
	revertSearchData: function() {
		var me = this;
	}
});