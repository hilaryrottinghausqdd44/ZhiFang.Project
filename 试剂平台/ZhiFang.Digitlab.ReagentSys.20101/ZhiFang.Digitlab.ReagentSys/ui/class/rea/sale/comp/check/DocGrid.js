/**
 * 供应商供货-供货总单列表
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.comp.check.DocGrid', {
	extend: 'Shell.class.rea.sale.basic.DocGrid',
	title: '供应商供货-供货总单列表',
    
    /**双方确认服务地址*/
	checkUrl: '/ReagentService.svc/RS_UDTO_CheckSaleByDocIDList',
	/**是否可调用第三方接口导入供货单数据*/
	isUseSaleDocInterfaceUrl:'/ReagentService.svc/RS_UDTO_IsUseSaleDocInterface',
	
    /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//是否可使用第三方接口导入供货单数据
		me.isUseSaleDocInterface();
		
		me.on({
			select:function(rowModel,record){
				me.showCheckButton();
			},
			deselect:function(rowModel,record){
				me.showCheckButton();
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		
		me.addEvents('addclick','editclick');
		
		me.defaultWhere = me.defaultWhere;
		if(me.defaultWhere){
			me.defaultWhere += ' and ';
		}
		me.defaultWhere += '(bmscensaledoc.Status=0 or bmscensaledoc.Status=2)';
		
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号/订货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Lab.CName']
		};
		me.buttonToolbarItems = ['refresh','-',{
			fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
			itemId:'status',allowBlank:false,value:0,
			width:120,labelWidth:55,labelAlign:'right',hasStyle:true,
			data:JcallShell.REA.Enum.getList('BmsCenSaleDoc_Status',true,true).slice(0,3),
			listeners:{change:function(){me.onSearch();}}
		},'-','edit','-',{
			text:'审核',
			tooltip:'供货单审核，审核后不可改',
			iconCls:'button-check',
			itemId:'CheckButton',
			disabled:true,
			handler:function(){me.onCheckClick();}
		},'-',{
			text:'供货单同步',
			tooltip:'第三方供货单同步到平台',
			iconCls:'button-import',
			itemId:'ThirdPartyButton',
			hidden:true,
			handler:function(){me.onThirdPartyClick();}
		},'->', {
			type: 'search',
			info: me.searchInfo
		}];
		
		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick',this);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		this.fireEvent('editclick',me,records[0]);
	},
	
	/**审核处理*/
	onCheckClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		//供货单号为空的单子不能审核
		var saleDocNoIsNull = false;
		//供货单的状态!="0(临时)"的单子不能审核
		var saleStatusError = false;
		for(var i=0;i<len;i++){
			if(!records[i].get("BmsCenSaleDoc_SaleDocNo")){
				saleDocNoIsNull = true;
				break;
			}
			if((records[i].get("BmsCenSaleDoc_Status") + "") != "0"){
				saleStatusError = true;
				break;
			}
		}
		if(saleDocNoIsNull){
			JShell.Msg.error("勾选的单子中存在供货单号为空的单子，请填写完整后再审核");
			return;
		}
		if(saleStatusError){
			JShell.Msg.error("只有供货单的状态为'临时'的单子才能审核，请确认后再审核");
			return;
		}
		
		JShell.Msg.confirm({
			msg:'审核通过后将不能对单子进行更改，是否确定对选中的供货单进行审核？'
		},function(but){
			if (but != "ok") return;
			
			var ids = [];
			for(var i=0;i<len;i++){
				ids.push(records[i].get(me.PKField));
			}
			//明细数据有效性检查
			me.isDtlValid(ids,function(isValid){
				if(!isValid) return;
				me.onCheckUpdate(records);
			});
		});
	},
	/**审核数据保存*/
	onCheckUpdate:function(records){
		var me = this,
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			me.updateOneByStatus(i,id,2);
		}
	},
	/**更新一个供货单状态值*/
	updateOneByStatus:function(index,id,Status){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
		
		var params = {
			entity:{
				Id:id,
				Status:Status
			},
			fields:'Id,Status'
		};
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					me.saveCount++;
					if(record){
						record.set(me.DelField,true);
						record.commit();
					}
				}else{
					me.saveErrorCount++;
					if(record){
						record.set(me.DelField,false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0){
						me.onSearch();
					}else{
						JShell.Msg.error("审核存在错误！");
					}
				}
			});
		},100 * index);
	},
	
	/**显示按钮*/
	showCheckButton:function(status){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			edit = buttonsToolbar.getComponent('edit'),
			CheckButton = buttonsToolbar.getComponent('CheckButton');
			
		edit.disable();
		CheckButton.disable();
		
		var records = me.getSelectionModel().getSelection(),
			len = records.length,
			showEdit = true,//显示编辑按钮
			allStatus0 = true;//临时
			
		if(len != 1){
			showEdit = false;
		}
			
		for(var i=0;i<len;i++){
			var status = records[i].get('BmsCenSaleDoc_Status') + '';
			if(status != '0'){
				allStatus0 = false;
				showEdit = false;
			}
		}
		
		if(showEdit){
			edit.enable();
		}
		if(allStatus0){
			CheckButton.enable();
		}
		
	},
	/**明细数据有效性检查*/
	isDtlValid:function(ids,callback){
		var me = this;
		me.getDtlData(ids,function(data){
			if(data.success){
				var list = (data.value || {}).list || [],
					len = list.length,
					docMap = {};
					isValid = true;
				
				for(var i=0;i<len;i++){
					docMap[list[i].BmsCenSaleDoc.Id] = true;
					if(!list[i].LotNo || !list[i].InvalidDate){
						isValid = false;
						break;
					}
				}
				if(!isValid){
					JShell.Msg.error('审核的供货单中存在明细批号或者效期为空的单子！');
					callback(false);
					return;
				}
				
				var isEmpty = false;
				for(var i in ids){
					if(!docMap[ids[i] + '']){
						isEmpty = true;
						break;
					}
				}
				if(isEmpty){
					JShell.Msg.error('审核的供货单中存在明细为空的单子！');
					callback(false);
					return;
				}
				
				callback(true);
			}else{
				JShell.Msg.error(data.msg);
				callback(false);
			}
		});
	},
	/**获取明细数据*/
	getDtlData:function(ids,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectDtlUrl);
			
		url += '?isPlanish=false';
		url += '&fields=BmsCenSaleDtl_LotNo,BmsCenSaleDtl_InvalidDate,BmsCenSaleDtl_BmsCenSaleDoc_Id';
		url += '&where=bmscensaledtl.BmsCenSaleDoc.Id in(' + ids.join(',') + ')';
			
		JShell.Server.get(url,function(data){
			callback(data);
		});
	},
	/**从第三方导入供货单数据*/
	onThirdPartyClick:function(){
		var me = this;
		
		JShell.Win.open('Shell.class.rea.sale.ThirdPartyWin', {
			resizable: false,
			iconCls:'button-import',
			listeners:{
				success:function(p){
					p.close();
					JShell.Msg.alert("供货单数据同步成功",null,2000);
				}
			}
		}).show();
	},
	/**是否可使用第三方接口导入供货单数据*/
	isUseSaleDocInterface:function(){
		var me = this;
		var url = JShell.System.Path.ROOT + me.isUseSaleDocInterfaceUrl;
		
		JShell.Server.get(url,function(data){
			if(data.success && data.value){
				var buttonsToolbar = me.getComponent('buttonsToolbar');
				var ThirdPartyButton = buttonsToolbar.getComponent('ThirdPartyButton');
				ThirdPartyButton.show();
			}
		});
	}
});