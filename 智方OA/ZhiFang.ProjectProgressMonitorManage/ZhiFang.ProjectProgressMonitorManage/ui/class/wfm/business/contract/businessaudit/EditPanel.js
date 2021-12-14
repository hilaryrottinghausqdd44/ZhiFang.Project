/**
 * 合同商务评审
 * @author Jcall
 * @version 2016-11-19
 */
Ext.define('Shell.class.wfm.business.contract.businessaudit.EditPanel', {
	extend: 'Shell.class.wfm.business.contract.basic.ActionTabPanel',
	title: '合同商务评审',
	
	/**通过按钮文字*/
    OverButtonName:'商务评审通过',
    /**不通过按钮文字*/
    BackButtonName:'商务评审未通过',
    
    /**通过状态文字*/
	OverName:'商务已评',
	/**不通过状态文字*/
	BackName:'评审未通过',
	
	/**处理意见字段*/
	OperMsgField:'ReviewInfo',
	
	/**合同ID*/
	PK: null,
	
	/**表单参数*/
	FormConfig:{
		/**需要保存的 信息*/
		Entity:{
			ReviewManID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			ReviewMan:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}
	},
	/**合同内容表单*/
	ClassFormPanel:'Shell.class.wfm.business.contract.businessaudit.ActionForm',
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
       	me.on({
			isValid: function(p) {
			   me.setActiveTab(me.EditForm);
			}
		});
		me.EditForm.on({
			save:function(p){
				me.fireEvent('save', me);
			}
		});
        me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'Preceiveplan':
					    //切换时赋值
						me.Preceiveplan.onSaveReceivePlan(me.EditForm,me.PK);
						break;
					default:
						break
				}
			},
			beforetabchange:function(tabPanel,  newCard, oldCard,  eOpts ){
				var edit = me.Preceiveplan.getPlugin('NewsGridEditing'); 
                edit.completeEdit();
                edit.cancelEdit();
			}
		});
     },
     initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
			//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		var items=me.callParent(arguments);
	
		me.EditForm = Ext.create('Shell.class.wfm.business.contract.businessaudit.Form',{
			title:'合同信息',
			formtype: 'edit',
			hasButtontoolbar:false,//带功能按钮栏
			hasLoadMask: false,//开启加载数据遮罩层
			PK:me.PK
		});
		items.splice(0,0,me.EditForm);
	   
	    me.Preceiveplan = Ext.create('Shell.class.wfm.business.contract.apply.PreceiveplanTree',{
			title:'收款计划',
			itemId: 'Preceiveplan',
			/**底部工具栏*/
	        hasBottomToolbar: false,
			PContractID:me.PK
		});
		items.splice(1,0,me.Preceiveplan);
		
		me.Pdf = Ext.create('Shell.class.wfm.business.contract.basic.PdfApp', {
			title: '预览PDF',
			itemId: 'Pdf',
			border: false,
			height: me.height,
			width: me.width,
			hasBtntoolbar:false,
			defaultLoad:true,
			PK: me.PK
		});
		items.push(me.Pdf);
		return items;
	},
	 createDockedItems:function(){
    	var me = this,
    		items = ['->'];
        if(me.OverButtonName){
    		items.push({
        		iconCls:'button-save',
				text:me.OverButtonName,
				tooltip:me.OverButtonName,
				handler:function(){
					if(!me.EditForm.getForm().isValid()){
						me.fireEvent('isValid',me);
						return;
					}
					me.onOver();
				}
			});
    	}
    	if(me.BackButtonName){
    		items.push({
				iconCls:'button-save',
				text:me.BackButtonName,
				tooltip:me.BackButtonName,
				handler:function(){
					if(!me.EditForm.getForm().isValid()){
						me.fireEvent('isValid',me);
						return;
					}
					me.onBack();
				}
			});
    	}
    		
    	var dockedItems = [{
        	xtype:'toolbar',
        	dock:'bottom',
        	items:items
        }];
        
        return dockedItems;
    },
	/**保存按钮点击处理方法*/
	onSave:function(StatusName){
		var me = this,
		url = JShell.System.Path.getRootUrl(me.EditForm.editUrl);
			
		var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus',StatusName);
        me.EditForm.OperMsg=me.OperMsg;
        me.EditForm.OperMsgField=me.OperMsgField;
		me.EditForm.getForm().setValues({
			PContract_ContractStatus:info.Id
		});
			
	    JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请重新保存');
    			return;
    	    }
			me.EditForm.isValidContractNumber(function(){
				var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus',StatusName);
			    var	params = me.getParams(info.Id);
				JShell.Server.post(url,Ext.JSON.encode(params),function(data){
					if(data.success){
						me.fireEvent('save',me,me.PK)
					}else{
						JShell.Msg.error(data.msg);
					}
				});
			},me.PK);
    	});
//      me.EditForm.onSaveClick();
	},
	 /**通过*/
	onOver:function(){
		var me = this;
		var	values = me.EditForm.getForm().getValues();
		if(!me.EditForm.getForm().isValid()){
			me.fireEvent('isValid',me);
			return;
		}
		var edit = me.Preceiveplan.getPlugin('NewsGridEditing'); 
        edit.completeEdit();
        edit.cancelEdit();
		me.Preceiveplan.Amount=values.PContract_Amount;
	    /**校验收款计划校验   @author liangyl @version 2017-07-27*/
        var comtab = me.getActiveTab(me.items.items[0]);
		var roonodes = me.Preceiveplan.getRootNode().childNodes; //获取主节点
        if(roonodes.length>0){
        	if(me.Preceiveplan.IsValid('1')!=true){
        		if(comtab!=me.Preceiveplan) me.setActiveTab(me.Preceiveplan);
        		return;
        	}
        }else{
			//当前页签不是在收款计划页签时，查询
			var isExect=true;
			isExect=me.checkReceivePlan();
			if(!isExect) return;
        }
		if(me.OperMsgField){
			//处理意见
			JShell.Msg.confirm({
				title:'<div style="text-align:center;">处理意见</div>',
				msg:'',
				closable:false,
				multiline:true//多行输入框
			},function(but,text){
				if(but != "ok") return;
				me.OperMsg = text;
				me.onSave(me.OverName);
			});
		}else{
			//确定进行该操作
			JShell.Msg.confirm({
				msg:'确定进行该操作？'
			},function(but,text){
				if(but != "ok") return;
				me.onSave(me.BackName);
			});
		}
	},
    /**未通过*/
	onBack:function(){
		var me = this;
		
		if(me.OperMsgField){
			//处理意见
			JShell.Msg.confirm({
				title:'<div style="text-align:center;">处理意见</div>',
				msg:'',
				closable:false,
				multiline:true//多行输入框
			},function(but,text){
				if(but != "ok") return;
				me.OperMsg = text;
				me.onSave(me.BackName);
			});
		}else{
			//确定进行该操作
			JShell.Msg.confirm({
				msg:'确定进行该操作？'
			},function(but,text){
				if(but != "ok") return;
				me.onSave(me.BackName);
			});
		}
	},
	
   /**校验合同总额和收款总额   @author liangyl @version 2017-07-27*/
	isValidAmount:function(Id,callback){
		var me = this,
			values = me.EditForm.getForm().getValues(),
			url = '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlanByHQL?isPlanish=true&fields=PReceivePlan_ReceivePlanAmount';
		
		url = JShell.System.Path.getRootUrl(url);
		var where = ' (preceiveplan.IsUse=1) and (preceiveplan.PContractID='+Id;
		where +='  and preceiveplan.Status in(3,5,7))';
		
		url += '&where=' + where;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**校验收款计划校验   @author liangyl @version 2017-07-27*/
    checkReceivePlan:function(){
    	var me=this;
    	var	values = me.EditForm.getForm().getValues();
		//收款总额 
		var ReceivePlanAmount=0;
		//合同总额
		var AmountStr=values.PContract_Amount+'';
	    var Amount =Number(AmountStr.match(/^\d+(?:\.\d{0,2})?/));
        var isExect=true;
		me.isValidAmount(values.PContract_Id,function(data){
			if(data && data.value.list){
	    		for(var i=0;i<data.value.list.length;i++){
	    			ReceivePlanAmount+=Number(data.value.list[i].PReceivePlan_ReceivePlanAmount);
	    		}
	    		var ReceivePlanAmountStr=ReceivePlanAmount+'';
	    		var AmountCount =Number(ReceivePlanAmountStr.match(/^\d+(?:\.\d{0,2})?/));
				//收款总额不等于合同总额
				if(AmountCount!=Amount){
					//切换到收款计划页
					me.setActiveTab(me.Preceiveplan);
					JShell.Msg.error('收款总额:' + AmountCount + '不等于合同金额:' +Amount + ",请校验!");
					isExect=false;
				}
			}
		});
		return isExect;
    },
    getParams:function(Status){
    	var me=this;
    	var entity = me.EditForm.getAddParams(),
    		values = me.EditForm.getForm().getValues();

    	var fields = [
			'Name','ContractNumber','PClientName','Principal',
			'PayOrg','PaidServiceStartTime','Software','Hardware',
			'SignDate','MiddleFee','Amount','SignMan',
			'Compname','IsInvoices','Comment','PClientID',
			'PayOrgID','PrincipalID','SignManID','CompnameID',
			'Id','ContractStatus','EquipOneWayCount','EquipTwoWayCount',
			'Content','ContractServiceCharge','Emphases'
		];
        entity.fields = fields.join(',');
	
       	entity.entity.Id = values.PContract_Id;
       	entity.entity.ContractStatus=Status;
		//处理意见
		if(me.OperMsgField){
		    entity.entity.ReviewInfo=me.OperMsg;
//		    entity.fields+=",ReviewInfo"; 
		}
		return entity;
   }
});