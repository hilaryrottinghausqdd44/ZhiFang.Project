/**
 * 稀释样本结果处理
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.dilution.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '稀释样本结果',
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestItemByHQL?isPlanish=true',
	/**样本结果稀释处理*/
    saveUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemResultDilution',
	isloadResult:false,//用于判断是否刷新检验单行数据
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,
	defaultOrderBy:[{property:'LisTestItem_DispOrder',direction:'ASC'}],
     
     /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**样本单ID*/
	TestFormID:null,
	//按钮是否可点击
    BUTTON_CAN_CLICK:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			refresh :function(store){
				me.onSelect(true);
			}
		});
	},
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
	
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'检验单ID',dataIndex:'LisTestItem_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'检验项目编号',dataIndex:'LisTestItem_LBItem_Id',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目名称',dataIndex:'LisTestItem_LBItem_CName',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目简称',dataIndex:'LisTestItem_LBItem_SName',width:120,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'报告值',dataIndex:'LisTestItem_ReportValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'原始值',dataIndex:'LisTestItem_OriglValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'状态',dataIndex:'LisTestItem_ReportStatusID',width:70,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'参考值',dataIndex:'LisTestItem_RefRange',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift({
			fieldLabel:'稀释倍数',name:'Multiple',editable:true, 
			xtype:'uxSimpleComboBox',value:'10',hasStyle:true,
			width:150,labelWidth:60,labelAlign:'right',itemId:'Multiple',
			data:[
				['10','10'],
				['100','100'],
				['1000','1000']
			]
		},{
			width:250,labelWidth:60,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'LBItemName',fieldLabel:'项目过滤',
			className:'Shell.class.lts.item.CheckGrid',emptyText: '项目过滤',
			classConfig:{checkOne:true},
			listeners : {
				check: function(p, record) {
					var CName = me.getComponent('buttonsToolbar').getComponent('LBItemName');
					var ID = me.getComponent('buttonsToolbar').getComponent('LBItemID');
					CName.setValue(record ? record.get('LBItem_CName') : '');
					ID.setValue(record ? record.get('LBItem_Id') : '');
					me.onSearch();
					p.close();
				}
			}
		},{
			xtype:'textfield',itemId:'LBItemID',fieldLabel:'项目ID',hidden:true
		});
		return buttonToolbarItems;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',
			dock:'bottom',
			itemId:'bottombuttonsToolbar',
			items:[ {
				xtype: 'button',text: '全选',
				tooltip: '全选',
				handler: function() {
					me.onSelect(true);
				}
			},{
				xtype: 'button',text: '全否',
				tooltip: '全否',
				handler: function() {
					me.onSelect(false);
				}
			},{
				xtype: 'button',iconCls: 'button-accept',text: '执行',
				tooltip: '执行',
				handler: function() {
					me.onSave();
				}
			},{
				xtype: 'label',text: '说明:执行后,选中项目的报告值=当前报告值*样本稀释倍数',
				margin: '0 0 0 10',style: "font-weight:bold;color:blue;"
			},'->',{
				xtype: 'button',iconCls: 'button-del',text: '关闭',
				tooltip: '关闭',
				handler: function() {
					if(me.BUTTON_CAN_CLICK)me.close();
				}
			}]
		};
		return items;
	},
	onSelect : function(bo){
		var me = this;
	    var recs = me.store.data.items,
	        len = recs.length;
	    for(var i=0;i<len;i++){
	    	if(bo)me.getSelectionModel().select(i,true,false);  
	    	else 
	    	    me.getSelectionModel().deselectAll(true);  
	    }
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
			//样本单IDId
		if(!me.TestFormID) {
			JShell.Msg.error("样本单ID不能为空");
			return;	
		}
		me.load(null, true, autoSelect);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
			
		var LBItemID = me.getComponent('buttonsToolbar').getComponent('LBItemID').getValue();

		//样本单IDId
		if(me.TestFormID) {
			params.push("listestitem.LisTestForm.Id=" + me.TestFormID + "");
		}
		if(LBItemID){
			params.push("listestitem.LBItem.Id=" + LBItemID + "");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},

    /**样本结果稀释处理*/
    onSave : function(){
   	    var me = this;
   	    
   	    if(!me.BUTTON_CAN_CLICK)return;
   	    
   	    //获取确认合并样本选择行
		var res = me.getSelectionModel().getSelection(),
		    len = res.length;
		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var Multiple = me.getComponent('buttonsToolbar').getComponent('Multiple').getValue();

		//校验 校验稀释倍数。稀释倍数不能小于等于0；不能等于1，等于1没有意义。当稀释系数＜1，提示稀释倍数＜1，确定要执行稀释样本结果调整吗？
		if(Multiple==0 || Multiple==1 ){
			JShell.Msg.alert("稀释倍数不能小于等于0；不能等于1");
			return;
		}
		if(Multiple<1){
			JShell.Msg.confirm({
				msg:'稀释倍数＜1，确定要执行稀释样本结果调整吗?？'
			},function(but){
				if (but != "ok") return;
				else me.onSaveUpdate(res,Multiple);
			});
		}else{
			me.onSaveUpdate(res,Multiple);
		}
    },
    onSaveUpdate : function(res,Multiple){
    	var me = this;
    	var LisTestItemID = "";
    	
   	    for(var i=0;i<res.length;i++){
   	    	var ReportValue = res[i].get('LisTestItem_ReportValue');
   	    	var isExec = me.isRealNum(ReportValue);
   	    	if(!isExec){
	  		    JShell.Msg.alert("选中行中存在不是定量结果的报告值,不能进行样本结果稀释处理!");
	  		    LisTestItemID="";
	    		return;
	    	}
   	    	if(i>0)LisTestItemID+=','
   	    	LisTestItemID += res[i].get('LisTestItem_Id');
   	    }
    	
        var url = JShell.System.Path.ROOT + me.saveUrl;
	    var entity ={
	    	testItemIDList:LisTestItemID,
	    	dilutionTimes: Multiple
	    };
	    me.showMask(me.saveText); //显示遮罩层
	    me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
			 me.BUTTON_CAN_CLICK=true;
			 me.hideMask();
			if(data.success){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
				me.isloadResult = true;
			    //清空数据
			    me.onSearch();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
    },
    /**判断报告值是否是定量结果
	 * true:数值型的，false：非数值型
	 * */
    isRealNum : function (val){
	    // isNaN()函数 把空串 空格 以及NUll 按照0来处理 所以先去除
	    if(val === "" || val ==null){
	        return false;
	    }
	    if(!isNaN(val)){
	        return true;
	    }else{
	        return false;
	    }
	}         
});