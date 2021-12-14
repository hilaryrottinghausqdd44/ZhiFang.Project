/**
 * 数据完整性检查
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.goods2.DataCheckGrid',{
    extend:'Shell.class.rea.client.basic.GridPanel',
    title:'数据完整性检查',
    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoods',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: false,
    /**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 50000,
	/**默认加载数据*/
	defaultLoad: true,
	width:300,
	height:360,
	/**是否启用序号列*/
	hasRownumberer: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
            load: function(s, records, su) {
                if (records.length > 0) {
                	me.store = me.getArray(s, records);
                }
            }
        });
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			dataIndex: 'ReaGoods_ReaGoodsNo',text: '货品编码',
			flex:1,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var  tab = record.get('ReaGoods_Tab');
				if(value) {
					var BGColor = "";
					if(tab=='1'){
						BGColor = "green";
					}
					if(BGColor)
						meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				}
				return value;
			}
		}, {
			xtype: 'actioncolumn',text: '设置最小单位',
			align: 'center',width: 90,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-config hand';
				},
				handler: function(grid, rowIndex, colIndex) {
                    var rec = grid.getStore().getAt(rowIndex);
			        var ReaGoodsNo=rec.get('ReaGoods_ReaGoodsNo');
			        if(!ReaGoodsNo){
			        	JShell.Msg.error('没有设置货品编码,不能设置最小单位');
			        	return;
			        }
					me.showMinUnit(ReaGoodsNo);
				}
			}]
		},{
			dataIndex: 'ReaGoods_IsMinUnit',text: '最小单位',
			hidden: true,hideable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_GonvertQty',text: '换算系数',
			hidden: true,hideable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true
		},{
			dataIndex: 'ReaGoods_Tab',text: '数据完整性标志',
			hidden: true,hideable: false,defaultRenderer: true
		}];

		return columns;
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			buffered:true,
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if (data.success) {
						var info = data.value;
						if (info) {
							var type = Ext.typeOf(info);
							if (type == 'object') {
								info = info;
							} else if (type == 'array') {
								info.list = info;
								info.count = info.list.length;
							} else {
								info = {};
							}

							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
						me.fireEvent('changeResult', me, data);
					} else {
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	getArray : function(s, arr) {
		var me =this;
        var a = {}, b = {};
        var len = arr.length;
        for (var i = 0; i < len; i++) {
            if (typeof a[arr[i].get('ReaGoods_ReaGoodsNo')] == 'undefined') {
                a[arr[i].get('ReaGoods_ReaGoodsNo')] = 1;
                b[arr[i]] = 1;
            } else {
                s.remove(arr[i]);
            }
        }
        return s;
    },
	 /**设置最小单位*/
	showMinUnit: function(ReaGoodsNo) {
		var me = this,
			config = {
				resizable: false,
                ReaGoodsNo:ReaGoodsNo,
				listeners: {
					save: function(p) {
						p.close();
						me.onSearch();
					}
				}
			};
		JShell.Win.open('Shell.class.rea.client.goods2.MinUnitGrid', config).show();
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		return items;
	},
	 /**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[],listArr=[];
		listArr=data.list;
		var arr2=[],arr3=[],arr4=[],arr5=[];
		for(var i=0;i<data.list.length;i++){
			
			arr2=[],arr3=[],arr4=[],arr5=[];
			var IsMinNum=0,isQtyNum=0;
			var ReaGoodsNo=data.list[i].ReaGoods_ReaGoodsNo;
			var GonvertQty=data.list[i].ReaGoods_GonvertQty;
			for(var j=0;j<data.list.length;j++){
				
				var ReaGoodsNo2=data.list[j].ReaGoods_ReaGoodsNo;
				var GonvertQty2=data.list[j].ReaGoods_GonvertQty;
                if(!GonvertQty2)GonvertQty2=0;
				if(ReaGoodsNo == ReaGoodsNo2){
					arr3.push(ReaGoodsNo);
					if(GonvertQty2=='1'){
						IsMinNum+=1;
					}
					if(Number(GonvertQty2)<1 || (data.list.length>1 && Number(GonvertQty2==1) ) ){
						isQtyNum+=1;
						arr4.push(ReaGoodsNo);
					}
					if(Number(GonvertQty2)<1){
						arr2.push(ReaGoodsNo);
					}
					if(Number(GonvertQty2)>1){
						arr5.push(ReaGoodsNo);
					}
				}
			}
			var tab='1';
			if(IsMinNum>1 || isQtyNum>1){
				tab='0';
			}
			//只有一行并且没有换算系数<1
			if(arr2.length==arr3.length){
				tab='0';
			}
			//只有一行并且没有换算系数>1
			if(arr5.length==arr3.length){
				tab='0';
			}
			if(IsMinNum==0){
				tab='0';
			}
			var obj1={
				ReaGoods_Tab:tab
			};
			if(ReaGoodsNo){
				var obj2 = Ext.Object.merge(data.list[i], obj1);
			    arr.push(obj2);
			}
		}
		result.list = arr;
		return result;
	}
});