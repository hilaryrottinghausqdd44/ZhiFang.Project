/**
 * 合同价格设置列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.EditGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'合同价格设置列表',
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchDContractPriceByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl:'/StatService.svc/ST_UDTO_UpdateDContractPriceByField',
    /**默认加载*/
	defaultLoad:true,
	/**后台排序*/
	remoteSort:false,
	/**排序字段*/
	defaultOrderBy:[{
		property: 'DContractPrice_ContractPrice',
		direction: 'ASC'
	}],
	/**带分页栏*/
	hasPagingtoolbar:true,
	/**默认每页数量*/
	defaultPageSize:50,
	/**是否启用序号列*/
	hasRownumberer:true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	
	/**是否带修改价格功能*/
	canEditPrice:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.getComponent('filterToolbar').on({
			search:function(p,params){
				me.onSearch();
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		if(me.canEditPrice){
			me.buttonToolbarItems = ['save'];
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		}
		
		//数据列
		me.columns = me.createGridColumns();
		
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.contractprice.SearchToolbar',{
			itemId:'filterToolbar',
			dock:'top',
			isLocked: true,
			height:80
		})];
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [];
		
		columns.push({
			dataIndex:'DContractPrice_BLaboratory_CName',text:'送检单位',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_BDept_CName',text:'科室',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_BTestItem_CName',text:'项目',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_CoopLevel',text:'合作级别',width:60,
			renderer:function(value,meta){
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
		        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        meta.style='background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
		        return v;
		    }
		},{
			dataIndex:'DContractPrice_BDealer_Name',text:'经销商',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_IsStepPrice',text:'是否阶梯价',width:80,
			align:'center',isBool:true,type:'bool'
		});
		
		if(me.canEditPrice){
			columns.push({
				dataIndex:'DContractPrice_ContractPrice',
				text:'<b style="color:blue;">合同价格</b>',
				width:80,type:'float',align:'right',
				editor:{xtype:'numberfield',minValue:0,allowBlank:false},
				renderer:function(value,meta){
					var v = value == null ? '' : value;
					
			        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
			        
			        if(!value || value == 0 || value == '0'){
			        	meta.style='background-color:red;';
			        }
			        
			        return v;
			    }
			});
		}else{
			columns.push({
				dataIndex:'DContractPrice_ContractPrice',text:'合同价格',
				width:80,type:'float',align:'right',
				renderer:function(value,meta){
					var v = value == null ? '' : value;
					
			        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
			        
			        if(!value || value == 0 || value == '0'){
			        	meta.style='background-color:red;';
			        }
			        
			        return v;
			    }
			});
		}
		
		columns.push({
			dataIndex:'DContractPrice_BBillingUnit_Name',text:'开票方',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_BillingUnitType',text:'开票方类型',width:80,
			renderer:function(value,meta){
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
		        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        meta.style='background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
		        return v;
		    }
		},{
			dataIndex:'DContractPrice_ContractNo',text:'合同编号',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_BeginDate',text:'开始日期',width:80,isDate:true,sortable:false
		},{
			dataIndex:'DContractPrice_EndDate',text:'截止日期',width:80,isDate:true,sortable:false
		},{
			dataIndex:'DContractPrice_AddUser',text:'合同录入人',width:80,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_ConfirmUser',text:'合同确认人',width:80,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_Id',text:'主键ID',hidden:true,hideable:false,isKey:true
		});
		
		if(me.canEditPrice){
			columns.push({dataIndex:me.DelField,text:'',width:40,hideable:false,sortable:false,renderer:function(value){
				var v = '';
				if(value === 'true'){v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';}
				if(value === 'false'){v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';}
		        return v;
		    }});
		}
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.doFilterParams();
		return me.callParent(arguments);
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.getComponent('filterToolbar').getParams();
		
		//内部数据条件
		var where = [];
		
		if (params.ContractNo) {
			where.push("(dcontractprice.ContractNo='" + params.ContractNo + "')");
		}
		if (params.CoopLevel) {
			where.push("(dcontractprice.CoopLevel='" + params.CoopLevel + "')");
		}
		if (params.BillingUnitType) {
			where.push("(dcontractprice.BillingUnitType='" + params.BillingUnitType + "')");
		}
		if (params.IsStepPrice != null) {
			where.push("(dcontractprice.IsStepPrice='" + (params.IsStepPrice == true ? "1" : "0") + "')");
		}
		
		if (params.Laboratory_Id) {
			where.push("dcontractprice.BLaboratory.Id='" + params.Laboratory_Id + "'");
		}
		if (params.TestItem_Id) {
			where.push("dcontractprice.BTestItem.Id='" + params.TestItem_Id + "'");
		}
		if (params.Dealer_Id) {
			where.push("dcontractprice.BDealer.Id='" + params.Dealer_Id + "'");
		}
		if (params.BillingUnit_Id) {
			where.push("dcontractprice.BBillingUnit.Id='" + params.BillingUnit_Id + "'");
		}
		
//		//时间
//		if (params.StartDate) {
//			where.push("dcontractprice.BLaboratory.Id='" + params.StartDate + "'");
//		}
//		if (params.EndDate) {
//			where.push("dcontractprice.BTestItem.Id='" + params.EndDate + "'");
//		}
		
		me.internalWhere = where.join(" and ");
	},
	/**@overwrite 保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			var price = rec.get('DContractPrice_ContractPrice');
			me.updateOneByPrice(id,price);
		}
	},
	/**修改价格*/
	updateOneByPrice:function(id,price){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:id,
				ContractPrice:price
			},
			fields:'Id,ContractPrice,ConfirmUser,ConfirmTime'
		});
		JShell.Server.post(url,params,function(data){
			var record = me.store.findRecord(me.PKField,id);
			if(data.success){
				if(record){record.set(me.DelField,true);record.commit();}
				me.saveCount++;
			}else{
				me.saveErrorCount++;
				if(record){record.set(me.DelField,false);record.commit();}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0) me.onSearch();
			}
		},false);
	}
});