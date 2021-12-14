/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define("Shell.class.weixin.sampling.basic.Form",{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger_NEW',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.zhifangux.DateArea'
    ],
    title:'微信消费采样',
	account:'',
	bodyPadding: 5,
	printUrl: JShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinService.svc/PrintNRequestForm_PDF",
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'add',	
	/**布局方式*/
	layout:{
        type: 'table',
        columns: 12,
        tableAttrs: {
            cellpadding: 1,
            cellspacing: 1,
            width: '100%',            
            align: 'left'            
        }
    },
	/**每个组件的默认属性*/
    defaults:{
    	//anchor:'100%',
        labelWidth:60,
        labelAlign:'left'
    },
    afterRender:function(){
    	var me =this;
    	me.callParent(arguments);
		me.getComponent('ClientName').onTriggerClick();
    },
    /**更改标题*/
	changeTitle:function(){
		var me = this;
		me.setTitle(me.title);
	},
	createItems:function(){
		var me =this;
		var items=[{
			xtype: 'button',
			text: '新增采样',
			rowspan:2,
			colspan:2,
			width:100,
			height:60,
			iconCls: 'button-add',
			listeners: {
			    click: function () {
					JShell.Win.open("Shell.class.weixin.sampling.basic.apply.App", {
					    formType:"add",
						width:1200,
						height:600,
						account:me.account,
					    listeners: {
					    }
					}).show();
				}	
			}
		}, {
			xtype: 'uxCheckTrigger', fieldLabel: '采血站点', name: 'ClientName', itemId: 'ClientName',
			className: 'Shell.class.weixin.sampling.basic.dic.ClientCheckGrid',colspan:2,labelWidth:60,width:300,
			classConfig: {	checkOne: true,	autoSelect: true,account:me.account},
			anchor: '100%', allowBlank: false, 
			emptyText: '必选项',
			listeners: {
				check: function (p, record) {
					me.getComponent('ClientName').setValue(record ? record.get('BusinessLogicClientControl_CLIENTELE_CNAME') : '');
					me.getComponent('ClientNo').setValue(record ? record.get('BusinessLogicClientControl_CLIENTELE_Id') : '');
					p.close();
				}
			}
		}, {
				xtype: 'textfield',
				fieldLabel: '站点编号',
				name: 'ClientNo',
				itemId: 'ClientNo',
				hidden: true,
				labelWidth: 80,
				anchor: '90%'
		},{
			fieldLabel:'条码',
			name:'SerialNo',colspan:1,
			itemId:'SerialNo',
			listeners:{
				specialkey:function(field,e){
					if(e.getKey()==Ext.EventObject.ENTER){
						me.onsearch();
					}
				}
			}
		},{
			fieldLabel:'消费码',
			name:'ZDY10',colspan:1,
			itemId:'ZDY10',
			listeners:{
				specialkey:function(field,e){
					if(e.getKey()==Ext.EventObject.ENTER){
						me.onsearch();
					}
				}
			}
		},{
			fieldLabel:'姓名',
			name:'CName',colspan:1,
			itemId:'CName',
			listeners:{
				specialkey:function(field,e){
					if(e.getKey()==Ext.EventObject.ENTER){
						me.onsearch();
					}
				}
			}
		}, {
			xtype: 'button',
			text: '查询',
			height:60,
			iconCls: 'button-search',
			labelAlign:'right',
			rowspan:2,colspan:3,
			handler: function () {
				me.onsearch();
			}
		}, {
			xtype: 'button',
			text: '打印外送清单',
			labelAlign:'right',
			height:60,
			rowspan:2,colspan:2,
			iconCls: 'button-print',
			handler: function () {
				me.PrintNRequestFormList();
			}
		},{
			xtype: 'uxdatearea', 
			fieldLabel:'开单时间',colspan:2,
			labelWidth:60,
			name:'OperateStartDateTime',
			itemId:'OperateStartDateTime',
			value: { start: new Date(), end: new Date() }
		},{
			xtype: 'uxdatearea', 
			fieldLabel:'采样时间',
			labelWidth:60,colspan:2,
			name:'txtCollectStartDate',
			itemId:'txtCollectStartDate'
		}
		];
		return items;
	},
	onsearch:function(){
		var me = this,
			where=" 1=1 ",	
			formval = me.getForm().getValues();
		if(formval.CName){
			where+=" and nrequestform.CName='"+formval.CName+"'";
		}
		if(formval.ClientNo){
			where+=" and nrequestform.ClientNo="+formval.ClientNo;
		}
		if(formval.SerialNo){
			where+=" and nrequestform.SerialNo="+formval.CName;
		}
		if(formval.ZDY10){
			where+=" and nrequestform.ZDY10='"+formval.ZDY10+"'";
		}		
		var OperateStartDateTime = me.getComponent("OperateStartDateTime").getValue();
		var txtCollectStartDate = me.getComponent("txtCollectStartDate").getValue();
		if(OperateStartDateTime){
			var start = JShell.Date.toString(OperateStartDateTime.start,true);
			var end = JShell.Date.toString(OperateStartDateTime.end,true);
			where+=" and (nrequestform.OperDate >= '"+start+"' and nrequestform.OperDate <= '"+end+"')";			
		}
		if(txtCollectStartDate){
			var start = JShell.Date.toString(txtCollectStartDate.start,true);
			var end = JShell.Date.toString(txtCollectStartDate.end,true);
			where+=" and (nrequestform.CollectDate >= '"+start+"' and nrequestform.CollectDate <= '"+end+"')";			
		}		
	    me.fireEvent('search',where);
	},
	//弹出遮罩层
	onMaskShow: function (msg) {
		var me = this;
		me.fireEvent('onMaskShow', msg);
		//me.body.mask(msg);	
	},
	//取消遮罩层  
	onMaskHide: function () {
		var me = this;
		me.fireEvent('onMaskHide', "");
		//me.body.unmask();
	},
	PrintNRequestFormList: function () {
		var me = this,
			where = " 1=1 ",
			formval = me.getForm().getValues();
		me.onMaskShow("加载中请稍等！");
		if (formval.CName) {
			where += " and nrequestform.CName='" + formval.CName + "'";
		}
		if (formval.ClientNo) {
			where += " and nrequestform.ClientNo=" + formval.ClientNo;
		}
		if (formval.SerialNo) {
			where += " and nrequestform.SerialNo=" + formval.CName;
		}
		if (formval.ZDY10) {
			where += " and nrequestform.ZDY10='" + formval.ZDY10 + "'";
		}
		var OperateStartDateTime = me.getComponent("OperateStartDateTime").getValue();
		var txtCollectStartDate = me.getComponent("txtCollectStartDate").getValue();
		if (OperateStartDateTime) {
			var start = JShell.Date.toString(OperateStartDateTime.start, true);
			var end = JShell.Date.toString(OperateStartDateTime.end, true);
			where += " and (nrequestform.OperDate >= '" + start + "' and nrequestform.OperDate <= '" + end + "')";
		}
		if (txtCollectStartDate) {
			var start = JShell.Date.toString(txtCollectStartDate.start, true);
			var end = JShell.Date.toString(txtCollectStartDate.end, true);
			where += " and (nrequestform.CollectDate >= '" + start + "' and nrequestform.CollectDate <= '" + end + "')";
		}
		var url = me.printUrl;
		url += "?where=" + where + "&LabId=" + formval.ClientNo + "&StartDateTime=" + JShell.Date.toString(OperateStartDateTime.start, true) + "&EndDateTime=" + JShell.Date.toString(OperateStartDateTime.end, true) ; 
		JShell.Server.get(url, function (data) {
			if (data.success) {
				me.onMaskHide();
				if (data.value) {
					for (var item in data.value) {						
						me.fireEvent("openPDF", JShell.System.Path.ROOT + "/" + data.value[item].replace("\\", "/"));						
					}
					
				} else {
					JShell.Msg.error("未查询到需要打印的清单请检查查询条件！");
				}
				//console.log(data);
			} else {
				me.onMaskHide();
				JShell.Msg.error("打印加载失败！错误信息：" + data.msg);
			}
		});


		
	}
	
	
});
