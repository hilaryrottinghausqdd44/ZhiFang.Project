/**
 * 供货单管理-主单列表-供应商专用
 * @author Jcall
 * @version 2017-07-25
 */
Ext.define('Shell.class.rea.sale.comp.manage.DocGrid', {
	extend: 'Shell.class.rea.sale.basic.DocGrid',
	title: '供货单管理-主单列表-供应商专用',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true',
	/**供货单拆分条码服务*/
	SplitSaleDocUrl:'/ReagentService.svc/RS_UDTO_SplitSaleDocByID',
	/**供货单取消拆分服务*/
	UnSplitSaleDocUrl:'/ReagentService.svc/RS_UDTO_SplitSaleDocCancel',
	/**供货单审核服务*/
	CheckSaleDocUrl:'/ReagentService.svc/RS_UDTO_CheckSaleDocByIDList',
    /**是否可调用第三方接口导入供货单数据*/
	isUseSaleDocInterfaceUrl:'/ReagentService.svc/RS_UDTO_IsUseSaleDocInterface',
	/**删除状态变更服务路径*/
	deleteFlagUrl:'/ReagentService.svc/RS_UDTO_SetSaleDocDeleteFlagByID',
	
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	
    /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//初始化可用按钮
		me.initShowButtons();
		
		me.on({
			select:function(rowModel,record){
				me.onRowSelect(record);
			},
			deselect:function(rowModel,record){
				me.onRowSelect(record);
			},
			nodata:function(){
				var buttonsToolbar = me.getComponent('buttonsToolbar');
					
				buttonsToolbar.getComponent('CheckBarCode').disable();
				buttonsToolbar.getComponent('CancelBarCode').disable();
				buttonsToolbar.getComponent('CheckButton').disable();
				buttonsToolbar.getComponent('printButton').disable();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('addclick');
		me.defaultWhere = me.defaultWhere;
		if(me.defaultWhere){
			me.defaultWhere += ' and ';
		}
		//供货单的供应商=本机构 
		me.defaultWhere += 'bmscensaledoc.Comp.Id=' + JShell.REA.System.CENORG_ID;
		
		//查询框信息
		me.searchInfo = {
			width: 130,
			emptyText: '供货单号/订货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Lab.CName']
		};
		me.buttonToolbarItems = ['refresh','-','add','-',{
			text:'删除',
			tooltip:'删除',
			iconCls:'button-del',
			itemId:'del',
			handler:function(){me.onChangeDeleteFlag(1);}
		},'-',{
			text:'拆分条码',
			tooltip:'按逻辑生成条码',
			iconCls:'button-accept',
			itemId:'CheckBarCode',
			disabled:true,
			isLocked:true,
			handler:function(){
			    me.onSplitSaleClick();
			}
		},'-',{
			text:'取消拆分',
			tooltip:'按逻辑取消条码，也就是将已经拆分好的明细重新变成一条数据',
			iconCls:'button-cancel',
			itemId:'CancelBarCode',
			disabled:true,
			isLocked:true,
			handler:function(){
			    me.onUnSplitSaleClick();
			}
		},'-',{
			text:'审核',
			tooltip:'供货单审核，审核后不可改',
			iconCls:'button-check',
			itemId:'CheckButton',
			disabled:true,
			isLocked:true,
			handler:function(){me.onCheckClick();}
		},'-',{
			text:'浏览打印',
			tooltip:'勾选一个供货单进行打印！',
			iconCls:'button-print',
			itemId:'printButton',
			disabled:true,
			isLocked:true,
			handler:function(){me.onPrintClick();}
		},'-',{
			text:'供货单同步',
			tooltip:'第三方供货单同步到平台',
			iconCls:'button-import',
			itemId:'ThirdPartyButton',
			disabled:true,
			isLocked:true,
			handler:function(){me.onThirdPartyClick();}
		},'-','->',{
			fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
			itemId:'status',allowBlank:false,value:0,
			width:120,labelWidth:55,labelAlign:'right',hasStyle:true,
			data:JcallShell.REA.Enum.getList('BmsCenSaleDoc_Status',true,true),
			listeners:{change:function(){me.onSearch();}}
		},'-',{
			type: 'search',
			info: me.searchInfo
		},'-',{
			itemId: 'toRightClick',
			iconCls: 'button-left',
			//text: '缩小',
			tooltip: '<b>缩小明细详情</b>',
			handler: function() {
				this.hide();
				me.fireEvent('toRightClick', me);
				this.ownerCt.getComponent('toLeftClick').show();
			}
		}, {
			itemId: 'toLeftClick',
			iconCls: 'button-right',
			//text: '放大',
			tooltip: '<b>放大明细详情</b>',
			hidden: true,
			handler: function() {
				this.hide();
				me.fireEvent('toLeftClick', me);
				this.ownerCt.getComponent('toRightClick').show();
			}
		}];
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '交流',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowInteraction(rec.get(me.PKField));
				}
			}]
		},{
			dataIndex: 'BmsCenSaleDoc_IsSplit',
			text: '拆分状态',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = '未拆分',
					color = 'green';
				if(value + '' == '1'){
					v = '已拆分';
					color = 'red';
				}
				if (v) meta.tdAttr = 'data-qtip="' + v + '"';
				meta.style = 'color:' + color;
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_UrgentFlag',
			text: '紧急标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_Status',
			text: '单据状态',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_Status['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_IOFlag',
			text: '提取标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_IOFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Comp_CName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_UserName',
			text: '操作人员',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Memo',
			text: '备注',
			width: 200,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'BmsCenSaleDoc_Comp_Id',
			text: '供货方ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDoc_Lab_Id',
			text: '订货方ID',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick',this);
	},
	/**修改删除标志*/
	onChangeDeleteFlag:function(value){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var url = JShell.System.Path.ROOT + me.deleteFlagUrl;
		var idList = [];
		for(var i=0;i<len;i++){
			idList.push(records[i].get(me.PKField));
		}
		
		var params = {
			idList:idList.join(','),
			deleteFlag:value
		};

		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.showMask('数据处理中...'); //显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					JShell.Msg.alert('数据处理成功！',null,1000);
					me.onSearch();
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	/**条码拆分*/
	onSplitSaleClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		var url = JShell.System.Path.ROOT + me.SplitSaleDocUrl;
		var params = {
			compatibleType:1,//兼容类型，0为兼容旧流程，1为新流程（主要判断条码打印标志）
			docID:records[0].get(me.PKField)
		};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.onSearch();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**取消拆分*/
	onUnSplitSaleClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		var url = JShell.System.Path.ROOT + me.UnSplitSaleDocUrl;
		var params = {docID:records[0].get(me.PKField)};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.onSearch();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**审核*/
	onCheckClick:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.CheckSaleDocUrl,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		if(len > 100){
			JShell.Msg.error("一次最多审核100个单子！");
			return;
		}
		
		var ids = []
		for(var i=0;i<len;i++){
			ids.push(records[i].get(me.PKField));
		}
		
		var params = {
			validateType:1,//验证类型，0为旧验证流程，1为先拆分后验证流程
			idList:ids.join(',')
		};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.onSearch();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**供货单打印*/
	onPrintClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		
		var LODOP = me.Lodop.getLodop();
		if(!LODOP) return;
		
		me.PrintModel = me.PrintModel || Ext.create('Shell.class.rea.sale.print.Model');
		me.PrintModel.getModelContent(records[0].get(me.PKField),function(table){
			LODOP.PRINT_INIT("实验室名称耗材、一次性物品验收记录");
			//intOrient
			//1-纵向打印，固定纸张；
			//2-横向打印，固定纸张；
			//3-纵向打印，宽度固定，高度按打印内容的高度自适应(见样例18)；
			//0-方向不定，由操作者自行选择或按打印机缺省设置。
			LODOP.SET_PRINT_PAGESIZE(2,0,0,"A4");
			LODOP.ADD_PRINT_TABLE("1%","1%","98%","98%",table);
			LODOP.ADD_PRINT_BARCODE(10,'90%',100,100,"QRCode",records[0].get("BmsCenSaleDoc_SaleDocNo"));
			LODOP.PREVIEW();
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
				ThirdPartyButton.enable();
			}
		});
	},
	/**数据选中处理*/
	onRowSelect:function(record){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CheckBarCode = buttonsToolbar.getComponent('CheckBarCode'),
			CancelBarCode = buttonsToolbar.getComponent('CancelBarCode'),
			CheckButton = buttonsToolbar.getComponent('CheckButton'),
			printButton = buttonsToolbar.getComponent('printButton'),
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			canCheckBarCode = true,
			canCancelBarCode = true,
			canCheck = true,
			canPrint = true;
			
		//没有勾选数据时，全部禁用
		if(len == 0){
			CheckBarCode.disable();
			CancelBarCode.disable();
			CheckButton.disable();
			printButton.disable();
			me.fireEvent('nodata',me);
			return;
		}
		
		for(var i=0;i<len;i++){
			if((records[i].get('BmsCenSaleDoc_IsSplit') + '') == '1'){//已拆分
				canCheckBarCode = false;//存在已拆分的数据就禁用拆分条码按钮
			}else{
				canCancelBarCode = false;//存在未拆分的数据就禁用取消拆分按钮
				canCheck = false;//未拆分的数据禁用审核按钮
			}
			if((records[i].get('BmsCenSaleDoc_Status') + '') == '0'){//临时
				canPrint = false;//临时的数据禁用打印按钮
			}
		}
		
		canCheckBarCode ? CheckBarCode.enable() : CheckBarCode.disable();
		canCancelBarCode ? CancelBarCode.enable() : CancelBarCode.disable();
		canCheck ? CheckButton.enable() : CheckButton.disable();
		canPrint ? printButton.enable() : printButton.disable();
	},
	/**初始化可用按钮*/
	initShowButtons:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			del = buttonsToolbar.getComponent('del'),
			CheckBarCode = buttonsToolbar.getComponent('CheckBarCode'),
			CancelBarCode = buttonsToolbar.getComponent('CancelBarCode'),
			CheckButton = buttonsToolbar.getComponent('CheckButton'),
			printButton = buttonsToolbar.getComponent('printButton'),
			ThirdPartyButton = buttonsToolbar.getComponent('ThirdPartyButton');
			
		del.show();
		CheckBarCode.show();
		CancelBarCode.show();
		CheckButton.show();
		printButton.show();
		
		//是否可使用第三方接口导入供货单数据
		me.isUseSaleDocInterface();
	}
});