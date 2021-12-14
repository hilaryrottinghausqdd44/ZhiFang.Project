/**
 * 输血申请综合查询:样本信息
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.recei.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '样本信息',

	/**获取数据服务路径*/
	selectUrl: JcallShell.System.Path.LOCAL+'/ilabstar/xservice/xlis/XAction',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	hasPagingtoolbar: false,
	//申请单号
	PK: null,
	//申请信息VO
	bReqVO:null,	
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodRecei_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.recei.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "样本信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddTrans');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '住院号',
			dataIndex: 'patno',
			width: 135,
			defaultRenderer: true
		}, {
			text: '条码号',
			dataIndex: 'barcode',
			width: 115,
			defaultRenderer: true
		}, {
			text: '状态',
			dataIndex: 'ywc',
			width: 95,
			defaultRenderer: true
		}];
		return columns;
	},

	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			arr = [];
			
		var url = (me.selectUrl.slice(0,4) == 'http' ? '' : 
			me.getPathRoot()) + me.selectUrl;
		
		return url;
	},
	/**查询数据*/
	onSearch:function(){
		var me = this,
			collapsed = me.getCollapsed();
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.disableControl();//禁用 所有的操作功能
		
		me.showMask(me.loadingText);//显示遮罩层
		
		var url = me.getLoadUrl();
		
		JShell.Server.post(url,Ext.JSON.encode({
			aPara:Ext.JSON.encode(me.postParams)
		}),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				var list = Ext.typeOf(data.value) == 'object' ? [] : data.value;
				me.store.loadData(list);
				
				if(list.length == 0){
					var msg = me.msgFormat.replace(/{msg}/,JShell.Server.NO_DATA);
					JShell.Action.delay(function(){me.getView().update(msg);},200);
				}else{
					if(me.autoSelect){
						me.doAutoSelect(list.length - 1);
					}
				}
			}else{
				var msg = me.errorFormat.replace(/{msg}/,data.msg);
				JShell.Action.delay(function(){me.getView().update(msg);},200);
			}
		});
	},
	/**根据病历号查询*/
	onSearchByPatno:function(patno){
		var me = this;
		if(!patno) return;
		me.postParams = {"SID":"8558B365","DBID":"6","patno":patno};
		
		me.onSearch();
	}
});
