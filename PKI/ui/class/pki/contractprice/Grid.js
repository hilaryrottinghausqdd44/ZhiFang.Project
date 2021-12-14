/**
 * 合同价格列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.Grid',{
    extend:'Shell.ux.grid.Panel',
    title:'合同价格列表',

    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchDContractPriceByHQL?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/StatService.svc/Stat_UDTO_AddDContractPrice',
    /**修改服务地址*/
    editUrl:'/StatService.svc/Stat_UDTO_UpdateDContractPriceByField',
    /**删除数据服务路径*/
	delUrl:'/BaseService.svc/ST_UDTO_DelDContractPrice',
    /**默认加载*/
	defaultLoad:false,
	/**排序字段*/
	defaultOrderBy:[{
		property: 'DContractPrice_EndDate',
		direction: 'ASC'
	}],
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:true,
	/**默认每页数量*/
	defaultPageSize:50,
	/**是否启用序号列*/
	hasRownumberer:false,
	/**复选框*/
	multiSelect:true,
	selType:'checkboxmodel',
	hasDel:true,

	/**送检单位ID*/
	LaboratoryId:null,
	/**送检单位时间戳*/
	LaboratoryDataTimeStamp:null,

	/**送检单位默认开票方ID*/
	LaboratoryBillingUnitId:null,
	/**送检单位默认开票方名称*/
	LaboratoryBillingUnitName:null,
	/**送检单位默认开票方时间戳*/
	LaboratoryBillingUnitDataTimeStamp:null,

	/**是否带功能按钮*/
	hasButtons:true,
	/**是否带修改价格功能*/
	canEditPrice:true,

	plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),

	afterRender:function(){
		var me = this;
		me.callParent(arguments);

		if(me.hasButtons){
			me.on({
				itemdblclick:function(view,record){
					me.onEditClick();
				}
			});
		}
	},
	initComponent:function(){
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = [];
		//查询框信息
		me.searchInfo = {width:220,emptyText:'合同编号',isLike:true,fields:['dcontractprice.ContractNo']};

		if(me.hasButtons){
			me.buttonToolbarItems.push({
				text:'新增合同',iconCls:'button-add',tooltip:'<b>新增合同价格</b>',
				handler:function(){me.openAddContractPriceWin();}
			},{
				text:'复制合同价格',iconCls:'button-add',tooltip:'<b>复制合同价格</b>',
				hidden:true	,	
				handler:function(){me.openCopyContractPriceWin();}
			},'edit','del');
		}
		if(me.canEditPrice){
			me.buttonToolbarItems.push('save');
		}
		me.buttonToolbarItems.push('->',{type:'search',info:me.searchInfo});

		//数据列
		me.columns = [];

		me.columns.push({
			dataIndex:'DContractPrice_BTestItem_CName',text:'项目',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_BDept_CName',text:'科室',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_BDealer_Name',text:'经销商',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_DealerIsStepPrice',text:'经销商阶梯价',width:85,
			isBool:true,align:'center',type:'bool',
		},{
			dataIndex:'DContractPrice_CoopLevel',text:'合作级别',width:60,
			renderer:function(value,meta){
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
		        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        meta.style='background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
		        return v;
		    }
		},{
			dataIndex:'DContractPrice_BillingUnitType',text:'开票类型',width:70,
			renderer:function(value,meta){
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
		        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        meta.style='background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
		        return v;
		    }
		},{
			dataIndex:'DContractPrice_BBillingUnit_Name',text:'开票方',width:100,defaultRenderer:true
//		},{
//			dataIndex:'DContractPrice_IsStepPrice',text:'合同阶梯价',width:75,
//			isBool:true,align:'center',type:'bool',
		});

		if(me.canEditPrice){
			me.columns.push({
				dataIndex:'DContractPrice_StepPrice',text:'<b style="color:blue;">合同价格</b>',
				width:100,type:'float',editor:{xtype:'numberfield',minValue:0,allowBlank:false},sortable:false
			});
		}else{
			me.columns.push({
				dataIndex:'DContractPrice_StepPrice',text:'合同价格',
				width:100,sortable:false,type:'float'
			});
		}

		me.columns.push({
			dataIndex:'DContractPrice_ContractNo',text:'合同编号',width:100,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_BeginDate',text:'开始日期',width:80,isDate:true,sortable:false
		},{
			dataIndex:'DContractPrice_EndDate',text:'截止日期',width:80,isDate:true,sortable:false
		},{
			dataIndex:'DContractPrice_AddUser',text:'合同录入人',width:80,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_AddTime',text:'合同录入时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex:'DContractPrice_ConfirmUser',text:'合同确认人',width:80,defaultRenderer:true
		},{
			dataIndex:'DContractPrice_ConfirmTime',text:'合同确认时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex:'DContractPrice_Id',text:'主键ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'DContractPrice_BDealer_Id',text:'经销商主键ID',hidden:true,hideable:false
		},{
			dataIndex:'DContractPrice_BDealer_DataTimeStamp',text:'经销商时间戳',hidden:true,hideable:false
		},{
			dataIndex:'DContractPrice_BTestItem_Id',text:'项目主键ID',hidden:true,hideable:false
		},{
			dataIndex:'DContractPrice_BTestItem_DataTimeStamp',text:'项目时间戳',hidden:true,hideable:false
		},{
			dataIndex:'DContractPrice_BBillingUnit_Id',text:'开票方主键ID',hidden:true,hideable:false
		},{
			dataIndex:'DContractPrice_BBillingUnit_DataTimeStamp',text:'开票方时间戳',hidden:true,hideable:false
		},{
			dataIndex:'DContractPrice_BDept_Id',text:'科室主键ID',hidden:true,hideable:false
		},{
			dataIndex:'DContractPrice_BDept_DataTimeStamp',text:'科室时间戳',hidden:true,hideable:false
		});

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.openContractPriceForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		me.openContractPriceForm(id);
	},
	/**打开表单*/
	openContractPriceForm:function(id){
		var me = this;
		var config = {
			showSuccessInfo:false,//成功信息不显示
			resizable:false,
			formtype:'add',
			LaboratoryId:me.LaboratoryId,//送检单位ID
			LaboratoryDataTimeStamp:me.LaboratoryDataTimeStamp,//送检单位时间戳
			LaboratoryBillingUnitId:me.LaboratoryBillingUnitId,//送检单位默认开票方ID
			LaboratoryBillingUnitName:me.LaboratoryBillingUnitName,//送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp:me.LaboratoryBillingUnitDataTimeStamp,//送检单位默认开票方时间戳
			listeners:{
				save:function(win){
					me.onSearch();
					win.close();
				}
			}
		};
		if(id){
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.contractprice.Form',config).show();
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
			var price = rec.get('DContractPrice_StepPrice');
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
				StepPrice:price
			},
			fields:'Id,StepPrice,ConfirmUser,ConfirmTime'
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
	},
	/**新增合同价格*/
	openAddContractPriceWin:function(){
		var me = this;
		JShell.Win.open('Shell.class.pki.contractprice.AddApp1',{
			resizable:false,
			LaboratoryId:me.LaboratoryId,//送检单位ID
			LaboratoryDataTimeStamp:me.LaboratoryDataTimeStamp,//送检单位时间戳
			LaboratoryBillingUnitId:me.LaboratoryBillingUnitId,//送检单位默认开票方ID
			LaboratoryBillingUnitName:me.LaboratoryBillingUnitName,//送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp:me.LaboratoryBillingUnitDataTimeStamp,//送检单位默认开票方时间戳
			listeners:{
				save:function(p){me.onSearch();p.close();}
			}
		}).show();
	},
	/**复制合同价格*/
	openCopyContractPriceWin:function(){
		var me = this,
			records=me.getSelectionModel().getSelection();

		if(records.length == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		JShell.Win.open('Shell.class.pki.contractprice.FormCopy',{
			resizable:false,
			LaboratoryId:me.LaboratoryId,//送检单位ID
			LaboratoryDataTimeStamp:me.LaboratoryDataTimeStamp,//送检单位时间戳
			LaboratoryBillingUnitId:me.LaboratoryBillingUnitId,//送检单位默认开票方ID
			LaboratoryBillingUnitName:me.LaboratoryBillingUnitName,//送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp:me.LaboratoryBillingUnitDataTimeStamp,//送检单位默认开票方时间戳
			listeners:{
				save:function(p,entity){me.onSaveCopyInfo(p,entity);}
			}
		}).show();
	},
	/**保存信息*/
	onSaveCopyInfo:function(p,data){
		var me = this,
			records=me.getSelectionModel().getSelection(),
			len = records.length;

		me.saveCount = len;
		me.saveIndex = 0;
		me.saveError = [];

		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			var rec = records[i];
			var entity = me.getEntity(rec,data);
			me.saveOne(i,p,entity);
		}
	},
	/**获取数据对象*/
	getEntity:function(record,data){
		var me = this;
		var entity = {
			BeginDate:JShell.Date.toServerDate(record.get('DContractPrice_BeginDate')),
			EndDate:JShell.Date.toServerDate(record.get('DContractPrice_EndDate')),
			ContractNo:record.get('DContractPrice_ContractNo'),
			ContractPrice:record.get('DContractPrice_StepPrice'),
			BLaboratory:{
				Id:me.LaboratoryId,
				DataTimeStamp:me.LaboratoryDataTimeStamp.split(',')
			},
			BDealer:{
				Id:record.get('DContractPrice_BDealer_Id'),
				DataTimeStamp:record.get('DContractPrice_BDealer_DataTimeStamp').split(',')
			},
			BTestItem:{
				Id:record.get('DContractPrice_BTestItem_Id'),
				DataTimeStamp:record.get('DContractPrice_BTestItem_DataTimeStamp').split(',')
			},
			BBillingUnit:{
				Id:record.get('DContractPrice_BBillingUnit_Id'),
				DataTimeStamp:record.get('DContractPrice_BBillingUnit_DataTimeStamp').split(',')
			}
		};
		//开票类型
		if(record.get('DContractPrice_BillingUnitType')){
			entity.BillingUnitType = record.get('DContractPrice_BillingUnitType');
		}
		//科室
		if(record.get('DContractPrice_BDept_Id')){
			entity.BDept = {
				Id:record.get('DContractPrice_BDept_Id'),
				DataTimeStamp:record.get('DContractPrice_BDept_DataTimeStamp').split(',')
			};
		}

		for(var i in data){
			entity[i] = data[i];
		}

		return entity;
	},
	/**保存一条数据*/
	saveOne:function(index,p,entity){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;

		var params = Ext.JSON.encode({entity:entity});

		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				me.saveIndex++;
				if(!data.success){
					me.saveError.push(data.msg);
				}
				if(me.saveIndex == me.saveCount){
					me.hideMask();//隐藏遮罩层
					if(me.saveError.length == 0){
						JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
						me.onSearch();p.close();
					}else{
						JShell.Msg.error(me.saveError.join('</br>'));
					}
				}
			});
		},100 * index);
	},
	/**根据送检单位ID获取数据*/
	loadByLaboratoryId:function(id){
		var me = this;
		me.defaultWhere = 'dcontractprice.BLaboratory.Id=' + id;
		me.onSearch();
	}
});