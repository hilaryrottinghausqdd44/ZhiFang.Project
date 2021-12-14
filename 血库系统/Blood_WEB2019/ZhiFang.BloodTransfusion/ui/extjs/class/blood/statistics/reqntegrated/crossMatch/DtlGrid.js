/**
 * 输血申请综合查询:交叉配血信息
 * @author liuyujie
 * @version 2020-10-29
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.crossMatch.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '交叉配血信息',

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
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.crossMatch.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "交叉配血信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('onAddTrans');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '配血者姓名',
			dataIndex: 'bxzcname',
			width: 95,
			defaultRenderer: true
		}, {
			text: '科室',
			dataIndex: 'deptmentcname',
			width: 95,
			defaultRenderer: true
		}, {
			text: '医生',
			dataIndex: 'doctor',
			width: 95,
			defaultRenderer: true
		}, {
			text: '',
			dataIndex: 'BloodNo',
			hidden:true,
			defaultRenderer: true
		}, {
			text: '血袋唯一号',
			dataIndex: 'xdwyh',
			width: 115,
			defaultRenderer: true
		}, {
			text: '血制品名称',
			dataIndex: 'BloodName',
			width: 95,
			defaultRenderer: true
		}, {
			text: '单位',
			dataIndex: 'BloodUnitName',
			width: 85,
			defaultRenderer: true
		}, {
			text: '数量',
			dataIndex: 'BCount',
			width: 85,
			defaultRenderer: true
		}, {
			text: '患者复检血型',
			dataIndex: 'reviewABORhdesc',
			width: 95,
			defaultRenderer: true
		}, {
			text: '血袋血型',
			dataIndex: 'BloodABOName',
			width: 95,
			defaultRenderer: true
		}, {
			text: '复检时间',
			dataIndex: 'BPreItemCheckTime',
			width: 135,
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
	/**根据申请单号查询*/
	onSearchByBReqFormID:function(BReqFormID){
		var me = this;
		if(!BReqFormID) return;
		me.postParams = {"SID":"646B0F22","DBID":"6","BReqFormID":BReqFormID};
		
		me.onSearch();
	}
});
