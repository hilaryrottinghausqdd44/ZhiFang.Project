/**
 * 列表树
 * @author liangyl
 * @version 2016-12-23
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.basic.SimpleGridTree', {
	extend: 'Shell.ux.tree.Panel',

	title: '简单列表树',
	width: 300,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlanTreeByHQL?isPlanish=true',
	/**默认加载数据*/
	defaultLoad: false,
	hasSave:false,
	/**根节点*/
	root: {
		text: '所有模块',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: false
	},
		/**是否显示根节点*/
	rootVisible:false,
	/*收款计划内容*/
	ReceiveGradationName: 'CollectionStage',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	defaultWhere:'',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**底部工具栏*/
	hasBottomToolbar: false,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**底部工具自定义按钮栏*/
	bottomToolbarItems: null,
	/**合同ID*/
	PContractID: null,
	columnLines: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent: function() {
		var me = this;
		//列表字段
		me.columns = me.createGridColumns();
//		 me.dockedItems = me.createDockedItems();
		//获取树列表
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=PReceivePlan_ReceiveGradationName,PReceivePlan_ReceivePlanAmount,PReceivePlan_ExpectReceiveDate,PReceivePlan_ReceiveManName,PReceivePlan_ReceiveAmount,PReceivePlan_UnReceiveAmount,PReceivePlan_Status,add,PReceivePlan_ReceiveGradationID,PReceivePlan_ReceiveManID,PReceivePlan_Id';

	    if(me.PContractID){
			me.defaultWhere='(preceiveplan.IsUse=1) and (preceiveplan.PContractID='+me.PContractID+')';
		}
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}

		return url;
	},

	//=====================创建内部元素=======================
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '收款分期',
			xtype:'treecolumn',
			dataIndex: 'text',
			width: 150,
			sortable: false
		}, {
			text: '收款金额',
			dataIndex: 'ReceivePlanAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '时间',
			dataIndex: 'ExpectReceiveDate',
			width: 100,
			sortable: false,
			menuDisabled: false,
			type: 'date',
			xtype: 'datecolumn',
			format: 'Y-m-d',
			editor: {
				xtype: 'datefield',
				allowBlank: false,
				format: 'Y-m-d',
				listeners:{
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						if(newValue != null && newValue != "") {
							if(newValue != null) {
								newValue = Ext.util.Format.date(newValue.toString(), 'Y-m-d');
							}
							if(newValue != "") {
								newValue = JcallShell.Date.getDate(newValue);
							}
						}
						record.set('ExpectReceiveDate', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '责任人',
			dataIndex: 'ReceiveManName',
			width: 100,
			sortable: false
		},{
			text: '已收',
			dataIndex: 'ReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '待收',
			dataIndex: 'UnReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		},{
			text: '计划内容ID',
			dataIndex: 'ReceiveGradationID',
			hidden: true,
			width: 100,
			sortable: false
		}, {
			text: '责任人ID',
			dataIndex: 'ReceiveManID',
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			hidden: true,
			hideable: false
		}, {
			text: '父收款计划ID',
			dataIndex: 'PPReceivePlanID',
			hidden: true,
			hideable: false
		}];
		return columns;
	},
	/**
	 * 树字段对象
	 * @type 
	 */
    treeFields:{
    	/**
		 * 基础字段数组
		 * @type 
		 */
		defaultFields:[
			{name:'text',type:'auto'},//默认的现实字段
			{name:'expanded',type:'auto'},//是否默认展开
			{name:'leaf',type:'auto'},//是否叶子节点
			{name:'icon',type:'auto'},//图标
			{name:'url',type:'auto'},//地址
			{name:'tid',type:'auto'},//默认ID号
			{name:'value',type:'auto'}
		],
		/**
		 * 模块对象字段数组
		 * @type 
		 */
		moduleFields:[
			{name:'ReceiveGradationName',type:'auto'},//收款分期
			{name:'ReceivePlanAmount',type:'auto'},//收款金额
			{name:'ExpectReceiveDate',type:'auto'},//时间
			{name:'ReceiveManName',type:'auto'},//责任人
			{name:'ReceiveGradationID',type:'auto'},//计划内容ID
			{name:'ReceiveManID',type:'auto'},//责任人
			{name:'Id',type:'auto'},//收款金额
			{name:'PPReceivePlanID',type:'auto'},
			{name:'Status',type:'auto'},
			{name:'UnReceiveAmount',type:'auto'},
			{name:'ReceiveAmount',type:'auto'},
			{name:'PContractName',type:'auto'},
			{name:'PContractID',type:'auto'}
		]
    },
		/**获取数据字段*/
	getStoreFields: function() {
		var me = this;
		var treeFields = me.treeFields;
		var defaultFields = treeFields.defaultFields;
		var moduleFields = treeFields.moduleFields;
		var fields = defaultFields.concat(moduleFields);
		return fields;
	},
	  /**
	 * 数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeStoreData: function(response){
		var me = this;
    	var data = Ext.JSON.decode(response.responseText);
        if(data.ResultDataValue){
        	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    	    data[me.defaultRootProperty] = ResultDataValue.Tree;
	    	var changeNode = function(node){
	    		var value = node['value'];
	    		for(var i in value){
	    			node[i] = value[i];
	    		}
	    		var children = node[me.defaultRootProperty];
	    		if(children){
	    			changeChildren(children);
	    		}
	    	};
	    	var changeChildren = function(children){
	    		Ext.Array.each(children,changeNode);
	    	};
	    	var children = data[me.defaultRootProperty];
	    	changeChildren(children);
	    	
	    	response.responseText = Ext.JSON.encode(data);
	        //已获取到数据
	        me.hasResponseData = true;
         }
    	return response;
    },
 	/**
	 * @public
	 * 加载数据
	 */
	load: function() {
		var me = this;
		this.onRefreshClick();
	},
		/**点击刷新按钮*/
	onRefreshClick: function(where, isPrivate) {
			var me = this;
			me.canLoad = true;
			collapsed = me.getCollapsed();

		me.defaultLoad = true;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.externalWhere = isPrivate ? me.externalWhere : where;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if (collapsed) {
			me.isCollapsed = true;
			return;
		}
	    JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PReceivePlanStatus', function() {
			if(!JShell.System.ClassDict.PReceivePlanStatus) {
				JShell.Msg.error('未获取到收款计划状态，请刷新列表');
				return;
			}
			me.store.currentPage = 1;
	       	me.store.load();
		});
	}
});