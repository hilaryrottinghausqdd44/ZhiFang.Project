/**
 * 商务收款记录
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.Grid',{
	extend: 'Shell.ux.grid.Panel',
    title: '商务收款记录',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPReceiveByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_AddPReceive',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPReceive',
		/**撤回商务收款*/
	backpreceiveUrl: '/SingleTableService.svc/ST_UDTO_AddBackPReceive',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PReceive_ReceiveDate',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**付款单位ID*/
	PayOrgID:null,
    PayOrgName:'',
	/**合同ID*/
	PContractID:null,
	/**合同*/
	PContractName:null,

	/**收款计划*/
	PReceivePlanId:null,

	/**收款负责人Id*/
	ReceiveManID:null,
	/**收款负责人*/
	ReceiveManName:null,
	/**未付*/
	UnReceiveAmount:0,
	//收款计划金额
	ReceivePlanReceiveAmount:0,
	//客户
	PClientName:null,
	//客户Id
	PClientID:null,
	defaultWhere:'preceive.IsUse=1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
        me.buttonToolbarItems = [{
			xtype: 'label',
			text: '商务收款记录',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 0 10'
		},'-','add'];		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'计划名称',dataIndex:'PReceive_PReceivePlan_ReceiveGradationName',
			hideable:false,sortable: false,menuDisabled: false, defaultRenderer: true
		},{
			text: '收款时间',
			dataIndex: 'PReceive_ReceiveDate',
			width: 100,
			sortable: false,
			menuDisabled: false,
			type: 'date',
			xtype: 'datecolumn',
			format: 'Y-m-d'
		},{
			text: '收款金额',
			dataIndex: 'PReceive_ReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		},  {
			xtype: 'actioncolumn',
			text: '取消',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable: false,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id=rec.get(me.PKField);
					var PFReceivenID=rec.get('PReceive_PFinanceReceive_Id');
					var PReceivePlanId=rec.get('PReceive_PReceivePlan_Id');
					var ReceiveAmount=rec.get('PReceive_ReceiveAmount');
					var PContractID=rec.get('PReceive_PContractID');
					me.onBackPReceive(id,PReceivePlanId,PFReceivenID,ReceiveAmount,PContractID);
				}
			}]
		},
		{
			text:'客户主键ID',dataIndex:'PReceive_Id',
			isKey:true,hidden:true,hideable:false
		},{
			text:'财务收款收款ID',dataIndex:'PReceive_PFinanceReceive_Id',
			hidden:true,hideable:false
		},{
			text:'收款计划ID',dataIndex:'PReceive_PReceivePlan_Id',
			hidden:true,hideable:false
		},{
			text:'合同ID',dataIndex:'PReceive_PContractID',
			hidden:true,hideable:false
		}];
		return columns;
	},
		/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if (items.length == 0) {
			if (me.hasRefresh) items.push('refresh');
			if (me.hasAdd) items.push('add');
			if (me.hasEdit) items.push('edit');
			if (me.hasDel) items.push('del');
			if (me.hasShow) items.push('show');
			if (me.hasSave) items.push('save');
			if (me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
		    height:26,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			params = [];
		//付款单位
		if(me.PReceivePlanId) {
			params.push("preceive.PFinanceReceive.Id='" + me.PReceivePlanId + "'");
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	onAddClick: function () {
		var me=this;
		me.fireEvent('onAddClick');
	},
	openEditForm: function (obj) {
        var me = this;
        JShell.Win.open('Shell.class.wfm.business.receive.preceive.Form', {
        	SUB_WIN_NO:'1',//内部窗口编号
        	//resizable:false,
            title:'商务收款新增',
            formtype:'add',
            /**付款单位ID*/
			PayOrgID:me.PayOrgID,	
			PayOrgName:me.PayOrgName,
			/**合同ID*/
			PContractID:me.PContractID,
			/**合同*/
			PContractName:me.PContractName,
			/**财务收款*/
			PFReceivenID:obj.FinanceReceiveGridId,
			/**收款计划*/
			PReceivePlanId:me.PReceivePlanId,
			/**收款负责人Id*/
			ReceiveManID:me.ReceiveManID,
			/**收款负责人*/
			ReceiveManName:me.ReceiveManName,
			/**未付*/
			UnReceiveAmount:me.UnReceiveAmount,
			//客户
			PClientName:me.PClientName,
			//客户Id
			PClientID:me.PClientID,
			PFReceiveAmount:obj.ReceiveAmount,
			PFSplitAmount:obj.SplitAmount,
			/**执行公司ID*/
			CompnameID:obj.CompnameID,
			/**执行公司*/
			ComponeName:obj.ComponeName,
			ReceiveDate:obj.ReceiveDate,
            listeners:{
                save:function(p,id){
                	me.fireEvent('save', p);
                }
            }
        }).show();
    },
    /**撤回商务收款*/
	onBackPReceive:function(id,PReceivePlanId,PFReceivenID,ReceiveAmount,PContractID){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.backpreceiveUrl);
		var PlanReceiveAmount=0,SplitAmount=0;
		if(PFReceivenID){
			SplitAmount=me.getPFReceivenById(PFReceivenID);
		}
		JShell.Msg.confirm({
			msg:'确定要撤回商务收款吗？'
		},function(but){
			if (but != "ok") return;
			var entity={
				Id:id,
				ReceiveAmount:ReceiveAmount
			};
			if(PReceivePlanId){
				entity.PReceivePlan = {
					Id:PReceivePlanId,
					ReceiveAmount:me.ReceivePlanReceiveAmount
				};
			}	
			if(PFReceivenID){
				entity.PFinanceReceive = {
					Id:PFReceivenID,
					SplitAmount:SplitAmount
				};
			}
			if(PContractID){
				entity.PContractID =PContractID;
			}
			var params = {
				entity:entity,
				fields:'Id,PReceivePlan_Id,PFinanceReceive_Id,'
			};
			me.showMask('商务收款撤回中');//显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					me.fireEvent('backsave');
				}else{
					var msg = data.msg ? data.msg : '商务收款已撤回，请刷新列表后再操作。</br>如果想撤回任务，请联系一审人员，让其主动退回。';
					JShell.Msg.error(msg);
				}
			});
		});
	},

	/**根据财务收款ID查询SplitAmount*/
	getPFReceivenById: function(Id) {
		var me = this,
			SplitAmount = 0;
		if(Id) {
			var url = '/SingleTableService.svc/ST_UDTO_SearchPFinanceReceiveByHQL?isPlanish=true&fields=PFinanceReceive_SplitAmount';
			url = JShell.System.Path.getRootUrl(url);
			url = url + '&where=pfinancereceive.Id=' + Id;
			
			JShell.Server.get(url, function(data) {
				if(data.success) {
					SplitAmount = data.value.list[0].PFinanceReceive_SplitAmount;
				} else {
					JShell.Msg.error(data.msg);
				}
			}, false, 100, false);
		} else {
			SplitAmount = 0;
		}
		return SplitAmount;
	}
});