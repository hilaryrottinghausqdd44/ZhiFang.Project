/**
 * 供货总单列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.DocGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '供货总单列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true',
	/**获取数据服务路径*/
	selectDtlUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDoc',
	/**审核服务地址*/
	checkUrl: '/ReagentService.svc/RS_UDTO_CheckSaleByDocIDList',
	/**删除状态变更服务路径*/
	deleteFlagUrl:'/ReagentService.svc/RS_UDTO_SetSaleDocDeleteFlagByID',
	/**是否可调用第三方接口导入供货单数据*/
	isUseSaleDocInterfaceUrl:'/ReagentService.svc/RS_UDTO_IsUseSaleDocInterface',
	/**导出Excel数据服务路径*/
	outExcelUrl: '/ReagentService.svc/RS_UDTO_GetReportDetailExcelPath',
	/**默认加载*/
	defaultLoad: false,
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
	
	/**是否是查看列表*/
	isShow:false,
	
	/**默认打印模板类型*/
	defaultModelType:'1',
	
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenSaleDoc_OperDate',direction:'DESC'}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		if(!me.isShow){
			//是否可使用第三方接口导入供货单数据
			me.isUseSaleDocInterface();
		}
		
		if(!me.isShow){
			me.on({
				select:function(){me.onSelectListeners();},
				deselect:function(){me.onSelectListeners();}
			});
		}
	},
	initComponent: function() {
		var me = this;
		me.addEvents('addclick','editclick');
		me.PrintModel = Ext.create('Shell.class.rea.sale.print.Model');
		
		me.defaultWhere = me.defaultWhere;
		if(me.defaultWhere){
			me.defaultWhere += ' and ';
		}
		me.defaultWhere += '(bmscensaledoc.DeleteFlag=0 or bmscensaledoc.DeleteFlag is null)';
		
		//加载条码模板组件
		//me.SaleModel = me.SaleModel || Ext.create('Shell.class.rea.sale.SaleModel');
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号/订货方',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Lab.CName']
		};
		//自定义按钮功能栏
		if(me.isShow){
			//自定义按钮功能栏
			me.buttonToolbarItems = ['refresh','-',{
				fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
				itemId:'status',allowBlank:false,value:me.defaultStatusValue,
				width:140,labelWidth:55,labelAlign:'right',hasStyle:true,
				data:JcallShell.REA.Enum.getList('BmsCenSaleDoc_Status',true,true),
				listeners:{change:function(){me.onSearch();}}
			},'-',{
				text:'浏览打印',
				tooltip:'勾选一个供货单进行打印！',
				iconCls:'button-print',
				itemId:'printButton',
				handler:function(){me.onPrintClick();}
			},'-', {
				type: 'search',
				info: me.searchInfo
			},'-',{
				text:'金额统计',
				tooltip:'对勾选的多条供货单进行金额统计',
				iconCls:'button-config',
				itemId:'StatisticsButton',
				handler:function(){me.onStatisticsClick();}
			},'-', {
				text:'按勾选导出',
				tooltip:'按勾选导出成Excel文件！',
				iconCls:'file-excel',
				itemId:'outButton1',
				handler:function(){me.onOutClick(1);}
			},'-', {
				text:'按条件导出',
				tooltip:'按条件导出成Excel文件！',
				iconCls:'file-excel',
				itemId:'outButton2',
				handler:function(){me.onOutClick(2);}
			}];
			
			//临时开放逻辑删除功能：钱万耀（18708747252）账户
			var account = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME);
			if(account == '18708747252'){
				me.buttonToolbarItems.push('-',{
					text:'数据删除',iconCls:'button-cancel',tooltip:'数据删除',
					listeners:{click:function(but) {me.onChangeDeleteFlag(1);}}
				});
			}
		}else{
			//自定义按钮功能栏
			me.buttonToolbarItems = ['refresh','add', 'edit', {
				text:'删除',
				iconCls:'button-del',
				itemId:'del',
				tooltip:'<b>删除</b>',
				hidden:true,
				handler:function(){
					me.onDelClick();
				}
			}, '-',{
				text:'条码拆分并审核',
				tooltip:'供货单条码拆分并审核，审核后不可改',
				iconCls:'button-check',
				itemId:'CheckButton',
				disabled:true,
				handler:function(){me.onCheckClick();}
			},'-',{
				text:'拆分供货单',
				tooltip:'将勾选的一个供货单拆分成多个子供货单（只能勾选一个供货单进行拆分）',
				iconCls:'button-edit',
				itemId:'SplitButton',
				disabled:false,
				handler:function(){me.onSplitClick();}
			}, '-',{
				text:'供货单同步',
				tooltip:'第三方供货单同步到平台',
				iconCls:'button-import',
				itemId:'ThirdPartyButton',
				hidden:true,
				handler:function(){me.onThirdPartyClick();}
			}, '->', {
				type: 'search',
				info: me.searchInfo
			}];
		}
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**显示删除按钮*/
	showDelButton:function(isShow){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			del = buttonsToolbar.getComponent('del');
			
		if(isShow === false){
			del.hide();
		}else{
			del.show();
		}
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
			dataIndex: 'BmsCenSaleDoc_OrderDocNo',
			text: '订货单号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
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
			width: 100,
			defaultRenderer: true
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
			dataIndex: 'BmsCenSaleDoc_Status',hidden:true,
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
			dataIndex: 'BmsCenSaleDoc_PrintTimes',
			text: '打印次数',
			align:'center',
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
		}, {
			dataIndex: 'BmsCenSaleDoc_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
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
		for(var i=0;i<len;i++){
			if(!records[i].get("BmsCenSaleDoc_SaleDocNo")){
				saleDocNoIsNull = true;
				break;
			}
		}
		if(saleDocNoIsNull){
			JShell.Msg.error("勾选的单子中存在供货单号为空的单子，请填写完整后再审核");
			return;
		}
		
		JShell.Msg.confirm({
			msg:'审核通过后将不能对单子进行更改，是否确定对选中的供货单进行审核？'
		},function(but){
			if (but != "ok") return;
			var url = JShell.System.Path.getRootUrl(me.checkUrl);
			var ids = [];
			for(var i=0;i<len;i++){
				ids.push(records[i].get(me.PKField));
			}
			
			//明细数据有效性检查
			me.isDtlValid(ids,function(isValid){
				if(!isValid) return;
				
				var params = JShell.JSON.encode({listID:ids.join(",")});
				JShell.Server.post(url,params,function(data){
					if(data.success){
						JShell.Msg.alert('审核通过',null,1000);
						me.onSearch();
					}else{
						JShell.Msg.error(data.msg);
					}
				});
			});
		});
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
	/**数据选中处理*/
	onSelectListeners:function(){
		var me = this
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			canCheck = true;
			
		for(var i=0;i<len;i++){
			if(records[i].get('BmsCenSaleDoc_Status') == '1'){
				canCheck = false;
				break;
			}
		}
		
		var CheckButton = me.getComponent('buttonsToolbar').getComponent('CheckButton');
		if(canCheck){
			CheckButton.enable();
		}else{
			CheckButton.disable();
		}
	},
	/**供货单打印*/
	onSalePrint:function(type){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		if(type != 1 && len > 1){
			JShell.Msg.error('非直接打印，只能选择一个供货单进行操作！');
			return;
		}
		
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		
		var LODOP = me.Lodop.getLodop();
		if(!LODOP) return;
		
		//模板类型
		var ModelType = me.getComponent('buttonsToolbar').getComponent('ModelType').getValue();
		//Lodop打印内容字符串数组
		var LodopStr = [];
		
		var result = null;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			//LODOP = me.SaleModel.getModelContent(ModelType,LODOP);
			var strLodop = me.SaleModel.getModelContent(ModelType);
			eval(strLodop);
			if(type == 1){//直接打印
				result = LODOP.PRINT();
			}else if(type == 2){//预览打印
				result = LODOP.PREVIEW();
			}else if(type == 3){//维护打印
				result = LODOP.PRINT_SETUP();
			}else if(type == 4){//设计打印
				result = LODOP.PRINT_DESIGN();
			}
		}
	},
	/**导入供货单数据*/
	onImportClick:function(){
		var me = this;
		
		JShell.Msg.overwrite('onImportClick');
	},
	/**金额统计*/
	onStatisticsClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			saleIDList = [];

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		for(var i=0;i<len;i++){
			saleIDList.push(records[i].get(me.PKField));
		}
		
		JShell.Win.open('Shell.class.rea.sale.basic.show.TotalPriceGrid',{
			saleIDList:saleIDList
		}).show();
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
	},
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
	/**导出供货单Excel*/
	onOutClick:function(type){
		var me = this;
			
		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel',{
			items:[
				{xtype:'filefield',name:'file'},
				{xtype:'textfield',name:'reportType',value:"2"},
				{xtype:'textfield',name:'idList'},
				{xtype:'textfield',name:'where'},
				{xtype:'textfield',name:'isHeader',value:"0"}
			]
		});
		//清空数据
		me.UpdateForm.getForm().setValues({idList:'',where:''});
		
		if(type == 1){//类型为勾选导出
			var records = me.getSelectionModel().getSelection(),
				len = records.length,
				ids = [];
				
			if (len == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}
			
			for(var i=0;i<len;i++){
				ids.push(records[i].get(me.PKField));
				me.UpdateForm.getForm().setValues({idList:ids.join(",")});
			}
		}else if(type == 2){//类型为条件导出
			var gridUrl = me.getLoadUrl(),
				arr = gridUrl.split('&where='),
				where = '';
				
			if(arr.length == 2){
				where = JShell.String.decode(arr[1]);
				where = where.replace(/bmscensaledoc./g,'bmscensaledtl.BmsCenSaleDoc.');
			}
			
			me.UpdateForm.getForm().setValues({where:where});
		}
		
		me.showMask("数据请求中...");
		var url = JShell.System.Path.ROOT + me.outExcelUrl;
		me.UpdateForm.getForm().submit({
			url:url,
            //waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	me.hideMask();
        		var fileName = action.result.ResultDataValue;
        		var downloadUrl = JShell.System.Path.ROOT + '/ReagentService.svc/RS_UDTO_DownLoadExcel';
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=平台供货单数据&fileName=' + fileName.split('\/')[2];
				downloadUrl = encodeURI(downloadUrl);
				window.open(downloadUrl);
            },
            failure:function(form,action){
            	me.hideMask();
				JShell.Msg.error(action.result.ErrorInfo);
			}
		});
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
	/**拆分供货单*/
	onSplitClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		if(records[0].get('BmsCenSaleDoc_Status') != '0'){
			JShell.Msg.error('只有状态是"临时"的单子才能进行拆分！');
			return;
		}
		
		var DocId = records[0].get(me.PKField) + '';
		
		JShell.Win.open('Shell.class.rea.sale.basic.SplitDtlGrid',{
			width:'96%',
			height:'96%',
			DocId:DocId,
			listeners:{
				save:function(p,docId){
					p.close();
					me.autoSelect = DocId;
					me.onSearch();
				}
			}
		}).show();
	}
});