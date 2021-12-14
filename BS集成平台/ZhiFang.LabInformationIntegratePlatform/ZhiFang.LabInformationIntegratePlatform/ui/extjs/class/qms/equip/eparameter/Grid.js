/**
 * 质量记录参数列表
 * @author liangyl
 * @version 2018-05-04
 */
Ext.define('Shell.class.qms.equip.eparameter.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '质量记录参数列表',
	width: 480,
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 1000,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEParameterByHQL?isPlanish=true',
//	defaultOrderBy: [{
//		property: 'EParameter_DispOrder',
//		direction: 'ASC'
//	}],
	/**是否单选*/
	checkOne: false,

	initComponent: function() {
		var me = this;
			//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
//		me.defaultWhere = me.defaultWhere || '';
//		if(me.defaultWhere) {
//			me.defaultWhere = '(' + me.defaultWhere + ') and ';
//		}
		me.defaultWhere += 'eparameter.Id>0';
		//查询框信息
//		me.searchInfo = {
//			width: 145,
//			emptyText: '参数名称',
//			isLike: true,
//			fields: ['eparameter.CName']
//		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh','->',{
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,emptyText:'参数名称',
			listeners:{
	            keyup:{
	                fn:function(field,e){
	                	if(e.getKey() == Ext.EventObject.ENTER){
							me.filterFn(field.getValue());
	                	}
	                }
	            }
	        }
		}];
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '参数名称',dataIndex: 'EParameter_CName',
			flex: 1,sortable: true,
			menuDisabled: true,defaultRenderer: true
		},{
			text: '参数编码',dataIndex: 'EParameter_ParaNo',
			minWidth: 180,flex: 1,sortable: true,
			menuDisabled: true,defaultRenderer: true
		},{
			text: '系统类型',dataIndex: 'EParameter_ParaType',width: 110,
			sortable: true,menuDisabled: true,defaultRenderer: true
		}, {
			text: '参数值',dataIndex: 'EParameter_ParaValue',
			width: 150,hidden:true,sortable: true,
			menuDisabled: true,defaultRenderer: true
		}, {
			text: '次序',dataIndex: 'EParameter_DispOrder',
			width: 150,hidden:true,sortable: true,
			menuDisabled: true,defaultRenderer: true
		}, {
			text: '主键ID',dataIndex: 'EParameter_Id',isKey: true,
			hideable: false,hidden:true,defaultRenderer: true
		}]
		return columns;
	},
	 /**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
//		if(data){
//			var ParaValue=data.list[0].EParameter_ParaValue;
//			var valArr = Ext.decode(ParaValue);
//			var vallist=valArr.list;
//			var len =vallist.length;
//			for(var i =0 ;i<len;i++){
//				var obj={
//					EParameter_ParaName:vallist[i].ParaName,
//					EParameter_ParaType:vallist[i].ParaType,
//					EParameter_ParaNo:vallist[i].ParaNo,
//					EParameter_DispOrder:vallist[i].DispOrder
//				}
//				arr.push(obj);
//			}
//		}
//		result.list = arr;

		return data;
	},
	filterFn : function(value) {
        var me = this,
        valtemp = value;
        var store = me.getStore();
        if (!valtemp) {
            store.clearFilter();
            return;
        }
        valtemp = String(value).trim().split(' ');
        store.filterBy(function(record, id) {
            var data = record.data;
            var CName = record.data.EParameter_CName;
            var dataarr = {
                EParameter_CName: CName
            };
            for (var p in dataarr) {
                var porp = Ext.util.Format.lowercase(String(dataarr[p]));
                for (var i = 0; i < valtemp.length; i++) {
                    var macther = valtemp[i];
                    var macther2 = Ext.escapeRe(macther);
                    mathcer = new RegExp(macther2);
                    if (mathcer.test(porp)) {
                        return true;
                    }
                }
            }
            return false;
        });
        if (!store || store.getCount() <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
    }
});