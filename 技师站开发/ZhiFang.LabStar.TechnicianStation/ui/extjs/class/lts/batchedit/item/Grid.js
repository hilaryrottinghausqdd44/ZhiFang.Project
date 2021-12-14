/**
 * 已选项目
 * @author liangyl
 * @version 2019-12-20
 */
Ext.define('Shell.class.lts.batchedit.item.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '已选项目',
	width: 800,
	height: 500,
    /**获取数据服务路径*/
	selectUrl:'',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	/**是否启用查询框*/
	hasSearch: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {
			width:'100%',emptyText:'项目名称/项目简称/用户代码/拼音字头',isLike:true,
			fields:[]
		};
		
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'组合项目',dataIndex:'LBSectionItem_LBItem_GroupItemCName',width:80,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				var GroupType = record.get('LBSectionItem_LBItem_GroupType');
				if(GroupType=='1')meta.style = 'background-color:#F08080;';
				return v;
			}
		},{
			text:'组合项目代码',dataIndex:'LBSectionItem_LBItem_GroupItemUseCode',width:80,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				var GroupType = record.get('LBSectionItem_LBItem_GroupType');
				if(GroupType=='1')meta.style = 'background-color:#F08080;';
				return v;
			}
		},{
			text:'项目id',dataIndex:'LBSectionItem_LBItem_Id',width:180,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LBSectionItem_LBItem_CName',width:150,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				var IsOrderItem = record.get('LBSectionItem_LBItem_IsOrderItem');
				if(IsOrderItem=='true')meta.style = 'background-color:#3CB371;';
			
				return v;
			}
		},{
			text:'项目简称',dataIndex:'LBSectionItem_LBItem_SName',width:80,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				var IsOrderItem = record.get('LBSectionItem_LBItem_IsOrderItem');
				if(IsOrderItem=='true')meta.style = 'background-color:#3CB371;';
				return v;
			}
		},{
			text:'医嘱项目',dataIndex:'LBSectionItem_LBItem_IsOrderItem',width:60,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				if(v=='true')v='医嘱';
				   else  v='';
				if (v) {
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		},{
			text:'用户代码',dataIndex:'LBSectionItem_LBItem_UseCode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'LBSectionItem_LBItem_PinYinZiTou',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'组合项目',dataIndex:'LBSectionItem_LBItem_GroupType',width:70,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'组合项目ID',dataIndex:'LBSectionItem_LBItem_GroupItemID',width:70,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
	/**@overwrite 查询按钮点击处理方法*/
	onSearchClick: function(but, value) {
		var me = this;
		me.filterFn(value);
	},
	//删除已选择行
	removeRec : function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		for(var i=0;i<records.length;i++){
			me.store.remove(records[i]);
		}
	},
	//本地过滤查询，
	filterFn : function(value) {
        var me = this,
            valtemp = value;
        if (!valtemp) {
            me.store.clearFilter();
            return
        }
        valtemp = String(value).trim().split(" ");
        me.store.filterBy(function(record, id) {
            var dataarr = {
                LBSectionItem_LBItem_CName:record.data.LBSectionItem_LBItem_CName,
                LBSectionItem_LBItem_SName:record.data.LBSectionItem_LBItem_SName,
                LBSectionItem_LBItem_UseCode:record.data.LBSectionItem_LBItem_UseCode,
                LBSectionItem_LBItem_PinYinZiTou: record.data.LBSectionItem_LBItem_PinYinZiTou
            };
            for (var p in dataarr) {
                var porp = Ext.util.Format.lowercase(String(dataarr[p]));
                for (var i = 0; i < valtemp.length; i++) {
                    var macther = valtemp[i];
                    var macther2 = Ext.escapeRe(macther);
                    mathcer = new RegExp(macther2);
                    if (mathcer.test(porp)) {
                        return true
                    }
                }
            }
            return false
        })
    }
});