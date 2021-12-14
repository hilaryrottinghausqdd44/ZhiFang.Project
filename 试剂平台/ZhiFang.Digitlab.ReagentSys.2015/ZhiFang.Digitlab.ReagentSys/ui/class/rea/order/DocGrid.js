/**
 * 订货总单列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.order.DocGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '订货总单列表',
	requires:[
		'Shell.ux.form.field.DateArea'
    ],
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDoc',
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
	
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenOrderDoc_OperDate',direction:'DESC'}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		me.addEvents('addclick','editclick');
		
		me.defaultWhere = me.defaultWhere;
		if(me.defaultWhere){
			me.defaultWhere += ' and ';
		}
		me.defaultWhere += '(bmscenorderdoc.DeleteFlag=0 or bmscenorderdoc.DeleteFlag is null)';
		
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '订货单号/供货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscenorderdoc.OrderDocNo', 'bmscenorderdoc.Comp.CName']
		};
		
		if(me.isShow){
			//自定义按钮功能栏
			me.buttonToolbarItems = ['refresh','-',{
				xtype:'uxdatearea',itemId:'date',fieldLabel:'订购日期',
				listeners:{
					change:function(){
						setTimeout(function(){me.onSearch();},100);
					},
					enter:function(){
						me.onSearch();
					}
				}
			},'-',{
				type: 'search',
				info: me.searchInfo
			}];
		}else{
			//自定义按钮功能栏
			me.buttonToolbarItems = ['refresh','add','edit','del','-',{
				xtype:'uxdatearea',itemId:'date',fieldLabel:'订购日期',
				listeners:{
					change:function(){
						setTimeout(function(){me.onSearch();},100);
					},
					enter:function(){
						me.onSearch();
					}
				}
			},'-',{
				type: 'search',
				info: me.searchInfo
			}];
		}
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenOrderDoc_OperDate',
			text: '订购日期',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_OrderDocNo',
			text: '订货单号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Comp_CName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_DeptName',
			text: '订购部门',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_UserName',
			text: '订购人员',
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
			dataIndex: 'BmsCenOrderDoc_UrgentFlag',
			text: '紧急标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_UrgentFlag['E' + value] || {};
				var v = info.value || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') + 
					';color:' + (info.color || '#000000');
				return v;
			}
		},{
			dataIndex: 'BmsCenOrderDoc_Status',
			text: '单据状态',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_Status['E' + value] || {};
				var v = info.value || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') + 
					';color:' + (info.color || '#000000');
				return v;
			}
		},{
			dataIndex: 'BmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_CheckTime',
			text: '审核时间',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_Confirm',
			text: '确认人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_ConfirmTime',
			text: '确认时间',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_LabMemo',
			text: '订货方备注',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_CompMemo',
			text: '供货方备注',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Memo',
			text: '备注',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'BmsCenOrderDoc_Comp_Id',
			text: '供货方ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenOrderDoc_Lab_Id',
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
	/**显示交流页面*/
	onShowInteraction:function(id){
		var me = this;
		
		JShell.Win.open('Shell.class.rea.sale.interaction.App', {
			PK: id
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			date = buttonsToolbar.getComponent('date'),
			search = buttonsToolbar.getComponent('search'),
			where = [];
			
		if(date){
			var value = date.getValue();
			if(value){
				if(value.start){
					where.push("bmscenorderdoc.OperDate>='" + JShell.Date.toString(value.start,true) + "'");
				}
				if(value.end){
					where.push("bmscenorderdoc.OperDate<'" + JShell.Date.toString(JShell.Date.getNextDate(value.end),true) + "'");
				}
			}
		}
		if(search){
			var value = search.getValue();
			if(value){
				where.push(me.getSearchWhere(value));
			}
		}
		me.internalWhere = where.join(" and ");
		
		return me.callParent(arguments);
	}
});