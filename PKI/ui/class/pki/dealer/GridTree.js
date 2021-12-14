/**
 * 经销商列表树
 * @author liangyl
 * @version 2017-07-18
 */
Ext.define('Shell.class.pki.dealer.GridTree', {
	extend: 'Shell.ux.tree.Panel',
    requires: ['Shell.ux.toolbar.Button','Shell.ux.form.field.CheckTrigger'],
	title: '经销商列表',
	width: 333,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/BS_UDTO_GetBDealerFrameListTree',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelBDealer',
	/**默认加载数据*/
	defaultLoad: true,
	/**根节点*/
	root: {
		text: '所有经销商',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: false
	},
	/**是否显示根节点*/
	rootVisible:true,
    hastopBtntoolbar:true,
    hasbottomBtntoolbar:true,
	defaultWhere:'',
	sortableColumns: true,
//  columnLines: true,
    lines:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.store.on({
			load:function(com,node, records,  successful,  eOpts ){
				if(records.length>0){
					me.getSelectionModel().select(0);
				}
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		//列表字段
		me.columns = me.createGridColumns();
		me.dockedItems = me.createDockedItems();
		//获取树列表
		me.callParent(arguments);
	},
	onAfterLoad: function() {
		var me = this;
		me.onMinusClick();
//		me.selectNode(me.selectId);
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=BDealer_UseCode,BDealer_Name,BDealer_Id,BDealer_DataTimeStamp,BDealer_BBillingUnit_Id,BDealer_BBillingUnit_Name';

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
			text: '经销商名称',xtype:'treecolumn',dataIndex: 'text',width: 200,
			flex:1,sortable: false
		},{
			text: '用户代码',dataIndex: 'UseCode',width: 60,
			sortable: false,menuDisabled:false,defaultRenderer: true
		},{
			dataIndex: 'BDealer_BBillingUnit_Name',text: '默认开票方',
			hidden:true,width: 150,defaultRenderer: true
		},{
			dataIndex: 'BDealer_BBillingUnit_Id',text: '开票方ID',hidden: true,hideable: false
		},{
			dataIndex: 'leaf',text: 'leaf',hidden: true,hideable: false
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
			{name:'UseCode',type:'auto'},
			{name:'Name',type:'auto'},
			{name:'BDealer_BBillingUnit_Name',type:'auto'},
			{name:'BDealer_BBillingUnit_Id',type:'auto'}
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
                if (value.BBillingUnit){
                    var obj={
                    	BDealer_BBillingUnit_Name:value.BBillingUnit.Name,
                    	BDealer_BBillingUnit_Id:value.BBillingUnit.Id,
                    };
                    var obj2 = Ext.Object.merge(value, obj);
                }
	    		for(var i in obj2){
	    			node[i] = obj2[i];
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
  		me.store.currentPage = 1;
       	me.store.load();
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if (me.hastopBtntoolbar) items.push(me.createButtontoolbar());
		if (me.hasbottomBtntoolbar) items.push(me.createbottomButtontoolbar());

		return items;
	},
	/**顶部工具栏*/
	createButtontoolbar:function(){
		var me = this,
			items = [];
		items.push('refresh', '-', {
			iconCls: 'button-arrow-in',
			itemId: 'minus',
			tooltip: '全部收缩',
			handler: function() {
				me.onMinusClick();
			}
		}, {
			iconCls: 'button-arrow-out',
			itemId: 'plus',
			tooltip: '全部展开',
			handler: function() {
				me.onPlusClick();
			}
		});
		items.push('->', {
			xtype: 'trigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: me.searchWidth,
			triggerCls: 'x-form-clear-trigger',
			enableKeyEvents: true,
			onTriggerClick: function() {
				this.setValue('');
				me.clearFilter();
			},
			listeners: {
				keyup: {
					fn: function(field, e) {
						var bo = Ext.EventObject.ESC == e.getKey();
						bo ? field.onTriggerClick() : me.filterByText(this.getRawValue());
					}
				}
			}
		});
		
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		});
		
	},
	/**底部工具栏*/
	createbottomButtontoolbar:function(){
		var items = [];
        items.push('add','edit','del','-', 'import_excel');      
		
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items
		});
	},

	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		var UseCode = me.getUseCode();
		var records = me.getSelectionModel().getSelection();
		var id='',name='';
		if (records && records.length == 1) {
		    id=records[0].get('tid');
			name=records[0].get('text');
		}
		me.openDealerForm(null,UseCode,id,name);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get('tid');
		var name = records[0].get('text');
		me.openDealerForm(id,null,id,name);
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var isExect=true;
		var IsLeaf = records[0].get('leaf');
		if(IsLeaf == 'false' || IsLeaf == false){
			isExect = false;
		}
		if(isExect!=true){
			JShell.Msg.error('只能删除叶子节点');
			return;
		}
		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			for (var i in records) {
				var id = records[i].get('tid');
				me.delOneById(i, id);
			}
		});
	},
	/**打开表单*/
	openDealerForm: function(id,code,BDealerId,BDealerName) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			UseCode:code,
			BDealerId : BDealerId,
		    BDealerName : BDealerName,
			listeners: {
				save: function(win) {
					me.load();
					win.close();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.dealer.dealer.Form', config).show();
	},
	/**点击导入按钮处理*/
	onImportExcelClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.pki.excel.FileUpdatePanel', {
			formtype:'add',
			resizable: false,
			TableName: 'B_Dealer',
			ERROR_UNIQUE_KEY_INFO:'经销商代码有重复',
			listeners: {
				save: function() {
					me.onSearch();
				}
			}
		}).show();
	},
	 /**获取用户代码方法*/
	getUseCode: function() {
		var me = this;
		var UseCode = '';
		var UseCodeUrl = '/StatService.svc/Stat_UDTO_GetMaxNoByEntityName';
		var url = (UseCodeUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + UseCodeUrl;
		url += "?EntityName=BDealer&FieldName=UseCode";
//		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.get(url, function(data) {
//			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				UseCode = data.value;
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
		return UseCode;
	},
	/**
	 * 根据显示文字过滤
	 * @private
	 * @param {} text
	 */
	filterByText: function(text) {
		this.filterBy(text, 'text');
	},
	/**
	 * 根据值和字段过滤
	 * @private
	 * @param {} text 过滤的值
	 * @param {} by 过滤的字段
	 */
	filterBy: function(text, by) {
		this.clearFilter();
		var view = this.getView(),
			me = this,
			nodesAndParents = [];

		this.getRootNode().cascadeBy(function(tree, view) {
			var currNode = this;
			if(currNode && currNode.data[by]) {
				//节点的匹配判断逻辑-包含输入的文字，不区分大小写，可修改
				if(currNode.data[by].toString().toLowerCase().indexOf(text.toLowerCase()) > -1) {
					me.expandPath(currNode.getPath());
					while(currNode.parentNode) {
						nodesAndParents.push(currNode.id);
						currNode = currNode.parentNode;
					}
				}
			}
		}, null, [me, view]);

		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {
				Ext.get(uiNode).setDisplayed('none');
			}
		}, null, [me, view]);
	},
	/**
	 * 清空过滤
	 * @private
	 */
	clearFilter: function() {
		var view = this.getView();
		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode) {
				Ext.get(uiNode).setDisplayed('table-row');
			}
		}, null, [this, view]);
	},
		/**删除一条数据*/
	delOneById: function(index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if (data.success) {
					me.hideMask(); //隐藏遮罩层
					me.load();
				}else{
					me.hideMask(); //隐藏遮罩层
					JShell.Msg.error(data.msg);
				}
			});
		}, 100 * index);
	}
});