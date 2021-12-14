/**
 * 实验室订货单审核-总单列表
 * @author Jcall
 * @version 2017-05-13
 */
Ext.define('Shell.class.rea.order.lab.audit.DocGrid', {
	extend: 'Shell.class.rea.order.basic.DocGrid',
	title: '实验室订货单审核-总单列表',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	
	/**订单审核服务地址（带推送给供应商）*/
	checkBmsCenOrderDocUrl:'/ReagentService.svc/RS_UDTO_CheckBmsCenOrderDocByID',
	
	/**是否是查看列表*/
	isShow:false,
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenOrderDoc_OperDate',direction:'DESC'}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
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
		
		me.defaultWhere = me.defaultWhere;
		if(me.defaultWhere){
			me.defaultWhere += ' and ';
		}
		me.defaultWhere += '(bmscenorderdoc.DeleteFlag=0 or bmscenorderdoc.DeleteFlag is null)';
		
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '订货单号/供货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscenorderdoc.OrderDocNo', 'bmscenorderdoc.Comp.CName']
		};
		
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || ['refresh','-','add', '-','del', '-', {
			text:'审核',
			tooltip:'订货单审核，审核后不可改',
			iconCls:'button-check',
			itemId:'CheckButton',
			disabled:true,
			handler:function(){me.onCheckClick();}
		}, '-', '->',{
			fieldLabel:'状态',xtype:'uxSimpleComboBox',
			itemId:'status',allowBlank:false,value:'0',
			width:120,labelWidth:40,labelAlign:'right',hasStyle:true,
			data:[
				[null,'全部','font-weight:bold;color:black;'],
				['0','临时','font-weight:bold;color:#ccc;'],
				['1','已提交','font-weight:bold;color:#5cb85c;'],
				['2','已确认','font-weight:bold;color:#5bc0de;'],
				['3','已出货','font-weight:bold;color:#f0ad4e;'],
				['4','已验收','font-weight:bold;color:#777;']
			],
			listeners:{change:function(){me.onSearch();}}
		}, {
			type: 'search',
			info: me.searchInfo
		}];
		
		me.callParent(arguments);
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
		
		//订货单号为空的单子不能审核
		var saleDocNoIsNull = false;
		//供货单的状态!="0(临时)"的单子不能审核
		var saleStatusError = false;
		for(var i=0;i<len;i++){
			if(!records[i].get("BmsCenOrderDoc_OrderDocNo")){
				saleDocNoIsNull = true;
				break;
			}
			if((records[i].get("BmsCenOrderDoc_Status") + "") != "0"){
				saleStatusError = true;
				break;
			}
		}
		if(saleDocNoIsNull){
			JShell.Msg.error("勾选的单子中存在订货单号为空的单子，请填写完整后再审核");
			return;
		}
		if(saleStatusError){
			JShell.Msg.error("只有订货单的状态为'临时'的单子才能审核，请确认后再审核");
			return;
		}
		
		JShell.Msg.confirm({
			msg:'审核通过后将不能对单子进行更改，是否确定对选中的订货单进行审核？'
		},function(but){
			if (but != "ok") return;
			me.onCheckUpdate(records);
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
			me.onCheckBmsCenOrderDocById(id);
		}
	},
	/**审核一个订货单*/
	onCheckBmsCenOrderDocById:function(id){
		var me = this,
			url = JShell.System.Path.ROOT + me.checkBmsCenOrderDocUrl;
		
		JShell.Server.post(url,Ext.JSON.encode({id:id}),function(data){
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
	},
	
	/**显示按钮*/
	showCheckButton:function(status){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			delButton = buttonsToolbar.getComponent('del'),
			CheckButton = buttonsToolbar.getComponent('CheckButton');
			
		delButton.disable();
		CheckButton.disable();
		
		var records = me.getSelectionModel().getSelection(),
			len = records.length,
			allStatus0 = true;//临时
			
		for(var i=0;i<len;i++){
			var status = records[i].get('BmsCenOrderDoc_Status') + '';
			if(status != '0'){
				allStatus0 = false;
			}
		}
		
		if(allStatus0){
			delButton.enable();
			CheckButton.enable();
		}
		
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		me.internalWhere = me.getInternalWhere();

		return me.callParent(arguments);;
	},
	/**获取内部条件*/
	getInternalWhere:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			status = buttonsToolbar.getComponent('status'),
			search = buttonsToolbar.getComponent('search'),
			where = [];
		
		if(status){
			var value = status.getValue();
			if(value){
				where.push('bmscenorderdoc.Status=' + value);
			}
		}
		if(search){
			var value = search.getValue();
			if(value){
				var searchWhere = me.getSearchWhere(value);
				if(searchWhere){
					where.push('(' + searchWhere + ')');
				}
				
			}
		}
		
		return where.join(" and ");
	}
});