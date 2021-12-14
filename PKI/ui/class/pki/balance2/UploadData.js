/**
 * 对账数据上传
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.UploadData', {
	extend: 'Shell.ux.grid.PostPanel',
	title: '对账数据上传 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_SearchNRequestItemByMainData',
	/**保存服务地址*/
	saveUrl: '/StatService.svc/Stat_UDTO_MigrationNRequestItemByMainData',
	/**上传状态默认值*/
	defaultMigrationFlagValue:0,
  	
  	/**消息框消失时间*/
	hideTimes: 1000,
  	
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:200,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.getComponent('filterToolbar').on({
			search:function(p,params){
				me.postParams = me.getPostParams();
				if(me.postParams){
					me.onSearch();
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
			
		if(me.reportType){
			me.buttonToolbarItems = me.buttonToolbarItems || [];
			if(me.buttonToolbarItems.length > 0){
				me.buttonToolbarItems.push('-');
			}
			me.buttonToolbarItems.push('exp_excel');
		}
		
		//创建挂靠功能栏
		var config = me.createSarchToolbarConfig();
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.balance2.SearchToolbar',Ext.apply(config,{
			itemId:'filterToolbar',
			dock:'top',
			isLocked: true,
			height:80
		}))];
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	createSarchToolbarConfig:function(){
		var panel = this;
		var config = {};
		
		/**默认选中日期方式*/
		config.isDateRadio = true;
			
		/**时间类型列表*/
		config.DateTypeList = [
			['1', '录入时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E0'] + ';'],//OperDate
			['2', '采样时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],//CollectDate
			['3', '核收时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],//ReceiveDate
			['4', '审定时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';'],//CheckDate
			['5', '二审时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E4'] + ';']//SenderTime2
		];
		
		/**创建内部组件*/
		config.createItems = function() {
			var me = this,
				items = [];
	
			//送检单位【勾选列表】
			items.push({
				x: 5,
				y: 55,
				fieldLabel: '送检单位',
				itemId: 'Laboratory_CName',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.pki.laboratory.CheckGrid'
			}, {
				fieldLabel: '送检单位主键ID',
				itemId: 'Laboratory_Id',
				hidden: true
			});
			//检验项目【勾选列表】
			items.push({
				x: 205,
				y: 55,
				fieldLabel: '检验项目',
				itemId: 'TestItem_CName',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.pki.item.CheckGrid'
			}, {
				fieldLabel: '检验项目主键ID',
				itemId: 'TestItem_Id',
				hidden: true
			});
			
			
			//预制条码
			items.push({
				x: 5,
				y: 30,
				fieldLabel: '预制条码',
				itemId: 'SerialNo'
			});
			//上机条码
			items.push({
				x: 205,
				y: 30,
				fieldLabel: '上机条码',
				itemId: 'BarCode'
			});
			//姓名（模糊查询）
			items.push({
				x: 405,
				y: 30,
				width: 145,
				labelWidth:40,
				fieldLabel: '姓名',
				emptyText: '模糊查询',
				itemId: 'NRequestForm_CName'
			});
			//上传状态（未上传，部分上传，已上传）
			items.push({
				x: 555,
				y: 30,
				width: 140,
				fieldLabel: '上传状态',
				xtype: 'uxSimpleComboBox',
				itemId: 'MigrationFlag',
				hasStyle: true,
				value: panel.defaultMigrationFlagValue,
				data: JShell.PKI.Enum.getList('MigrationFlag',true,true)
			});
			
			//时间条件
			items.push({
				x: 5,
				y: 5,
				width: 170,
				fieldLabel: '时间类型',
				xtype: 'uxSimpleComboBox',
				itemId: 'DateType',
				hasStyle: true,
				value: me.defaultDateTypeValue,
				data: me.getDateTypeList()
			}, {
				x: 185,
				y: 5,
				width: 50,
				itemId: 'radio2',
				boxLabel: '年月',
				xtype: 'radio',
				name: me.getId() + 'radioG1',
				checked: !me.isDateRadio
			}, {
				x: 235,
				y: 5,
				width: 95,
				xtype: 'uxYearComboBox',
				itemId: 'YearMonth',
				value: me.YearMonth,
				disabled: me.isDateRadio
			}, {
				x: 330,
				y: 5,
				width: 95,
				xtype: 'uxMonthComboBox',
				itemId: 'MonthMonth',
				value: me.MonthMonth,
				disabled: me.isDateRadio,
				margin: '0 2px 0 10px'
			}, {
				x: 445,
				y: 5,
				width: 50,
				itemId: 'radio1',
				boxLabel: '日期',
				xtype: 'radio',
				name: me.getId() + 'radioG1',
				checked: me.isDateRadio
			}, {
				x: 495,
				y: 5,
				width: 95,
				itemId: 'BeginDate',
				xtype: 'datefield',
				format: 'Y-m-d',
				value: me.BeginDate,
				disabled: !me.isDateRadio
			}, {
				x: 590,
				y: 5,
				width: 105,
				labelWidth: 5,
				fieldLabel: '-',
				labelSeparator: '',
				itemId: 'EndDate',
				xtype: 'datefield',
				format: 'Y-m-d',
				value: me.EndDate,
				disabled: !me.isDateRadio
			});
	
			//操作
			var buttons = me.createButtons();
			if(buttons){
				items = items.concat(buttons);
			}
	
			return items;
		};
		
		/**创建功能按钮*/
		config.createButtons = function(){
			var me = this,
				items = [];
			
			items.push({
				x: 410,
				y: 55,
				width: 60,
				iconCls: 'button-cancel',
				margin: '0 0 0 10px',
				xtype: 'button',
				text: '清空',
				tooltip: '<b>清空查询条件</b>',
				handler: function() {
					me.onClearSearch();
				}
			}, {
				x: 480,
				y: 55,
				width: 60,
				iconCls: 'button-search',
				margin: '0 0 0 10px',
				xtype: 'button',
				text: '查询',
				tooltip: '<b>查询</b>',
				handler: function() {
					me.onFilterSearch();
				}
			}, {
				x: 600,
				y: 55,
				width: 80,
				iconCls: 'button-save',
				margin: '0 0 0 10px',
				xtype: 'button',
				text: '<b>数据上传</b>',
				tooltip: '<b>上传选中的数据</b>',
				handler: function() {
					panel.onSaveClick();
				}
			});
			
			return items;
		};
		
		return config;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'NRequestItem_MigrationFlag',
			align:'center',
			text: '上传状态',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.MigrationFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_NRequestForm_BLaboratory_CName',
			text: '送检单位',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目名称',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_SerialNo',
			text: '预制条码',width:90,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BarCode',
			text: '上机条码',width:90,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '病人名',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_CollectDate',
			text: '采样时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_OperDate',
			text: '录入时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_ReceiveDate',
			text: '核收时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_SenderTime2',
			text: '报告时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_NRequestForm_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	
	/**@overwrite 条件处理*/
	getPostParams: function() {
		var me = this,
			params = me.getComponent('filterToolbar').getParams(),
			where = [];
			
		if(!params.BeginDate || !params.EndDate){
			JShell.Msg.error("时间范围：1-31天");
			return;
		}
		
		var StartDate = JShell.Date.getDate(params.BeginDate);
		var EndDate = JShell.Date.getDate(params.EndDate);
		var times = EndDate.getTime()-StartDate.getTime();
		times = Math.floor(times/(24*60*60*1000)) + 1;
		if(times > 31 || times <= 0){
			JShell.Msg.error("时间范围：1-31天");
			return;
		}
		JShell.Msg.log("时间范围：" + times + "天");
			
		//送检单位
		if (params.Laboratory_Id) {
			where.push("NRequestItem_NRequestForm_BLaboratory_Id='" + params.Laboratory_Id + "'");
		}
		//检验项目
		if (params.TestItem_Id) {
			where.push("NRequestItem_BTestItem_Id='" + params.TestItem_Id + "'");
		}
		//预制条码
		if (params.SerialNo) {
			where.push("NRequestItem_SerialNo='" + params.IsLocked + "'");
		}
		//上机条码
		if (params.BarCode) {
			where.push("NRequestItem_BarCode='" + params.BarCode + "'");
		}
		//姓名
		if (params.NRequestForm_CName) {
			where.push("NRequestItem_NRequestForm_CName like'%" + params.NRequestForm_CName + "%'");
		}
		//时间
		var dateSqlArr = [];
		if(params.BeginDate){
			dateSqlArr.push("NRequestItem_{Field}>='" + params.BeginDate + "'");
		}
		if(params.EndDate){
			dateSqlArr.push("NRequestItem_{Field}<'" + 
				JShell.Date.toString(JShell.Date.getNextDate(params.EndDate),true) + "'");
		}
		var dateSql = "";
		dateSql = dateSqlArr.join(" and ");
		if(dateSqlArr.length == 2){
			dateSql = "(" + dateSql + ")";
		}
			
		switch (params.DateType) {
			case '1':
				where.push(dateSql.replace(/{Field}/g,"OperDate"));
				break;
			case '2':
				where.push(dateSql.replace(/{Field}/g,"CollectDate"));
				break;
			case '3':
				where.push(dateSql.replace(/{Field}/g,"ReceiveDate"));
				break;
			case '4':
				where.push(dateSql.replace(/{Field}/g,"CheckDate"));
				break;
			case '5':
				where.push(dateSql.replace(/{Field}/g,"SenderTime2"));
				break;
		}
		//上传状态
		var MigrationFlag = me.getComponent('filterToolbar').getComponent('MigrationFlag').getValue();
		if(MigrationFlag !== 0){
			where.push("NRequestItem_MigrationFlag='" + MigrationFlag + "'");
		}
		
		var whereSql = (where.length >0 ? " and " : "") + where.join(" and ");
		return {
			WhereSQL:whereSql
		};
	},
	/**保存数据*/
	onSaveClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			ids = [];
		
		if(len == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		for(var i=0;i<len;i++){
			ids.push(records[i].get([me.PKField]));
		}
		
		var url = (me.saveUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;
		var params = Ext.JSON.encode({IdList:ids.join(',')});
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}
});