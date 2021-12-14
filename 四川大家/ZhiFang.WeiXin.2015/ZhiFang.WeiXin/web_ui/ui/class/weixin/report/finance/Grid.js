/**
 * 财务收入日报表
 * @author liangyl
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.report.finance.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '财务收入日报表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchFinanceIncomeList?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		buttonsToolbar.on({
			search: function(params) {
				me.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**本地数据存储*/
	LocalStorage: {
		set: function(name, value) {
			localStorage.setItem(name, value);
		},
		get: function(name) {
			return localStorage.getItem(name);
		},
		remove: function(name) {
			localStorage.removeItem(name);
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return Ext.create('Shell.class.weixin.report.finance.SearchToolbar', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.defalutColumns();
		var arr=[];
		if(me.LocalStorage.get('FinanceReport')) {
			var temcolumns = Ext.JSON.decode(me.LocalStorage.get('FinanceReport'));
			arr=arr.concat(temcolumns);
			for (var i in columns) {
				if(i>0)columns[i].hidden=true;
				for (var j in arr) {
					if(columns[i].dataIndex==arr[j]){
						columns[i].hidden=false;
						break;
					}
				}
			}
		}
		return columns;
	},
	
	defalutColumns:function(){
		var me=this;
		var	columns=[{ text: '主键', dataIndex: 'Id', sortable: false,hidden:true, defaultRenderer: true },
		    { text: '订单编号', dataIndex: 'UOFCode', width: 135, sortable: false, defaultRenderer: true },
			{ text: '姓名', dataIndex: 'UserName', width: 100, sortable: false, defaultRenderer: true }, 
			{ text: '性别', dataIndex: 'SexName', width: 40, sortable: false, defaultRenderer: true,hidden:true },
			{ text: '市场价格', dataIndex: 'MarketPrice', width: 60, sortable: false, defaultRenderer: true },
			{ text: '大家价格', dataIndex: 'GreatMasterPrice', width: 60, sortable: false, defaultRenderer: true },
			{ text: '实际价格', dataIndex: 'Price', width: 60, sortable: false, defaultRenderer: true },
			{ text: '开单日期', dataIndex: 'BillingDate', width: 90, sortable: false, isDate: true },
			{ text: '采样日期', dataIndex: 'SamplingDate', width: 90, sortable: true, isDate: true,hidden:true },
			{ text: '开单医生', dataIndex: 'DoctorName', width: 100, sortable: false, defaultRenderer: true },
			{ text: '咨询费率', dataIndex: 'AdvicePriceRate', width: 60, sortable: false, defaultRenderer: true },
			{ text: '咨询费', dataIndex: 'AdvicePrice', width: 60, sortable: false, defaultRenderer: true },
			{ text: '退款单号', dataIndex: 'MRefundFormCode', width: 100, sortable: false, defaultRenderer: true },
			{ text: '退款金额', dataIndex: 'RefundPrice', width: 80, sortable: false, defaultRenderer: true }];
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			par = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&');

		var entityJson = me.getComponent('buttonsToolbar').getEntityJson();
		if(entityJson != null) {
			url += '&searchEntity=' + Ext.JSON.encode(entityJson);
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
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	}
});