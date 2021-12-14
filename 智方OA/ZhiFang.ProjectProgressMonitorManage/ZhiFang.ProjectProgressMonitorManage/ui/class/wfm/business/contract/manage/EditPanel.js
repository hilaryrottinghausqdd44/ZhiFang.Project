/**
 * 合同编辑
 * @author liangyl
 * @version 2016-11-18
 */
Ext.define('Shell.class.wfm.business.contract.manage.EditPanel', {
	extend: 'Ext.tab.Panel',
	title: '合同编辑表单',
	
	width: 700,
	height: 480,
	
	/**合同ID*/
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Form.on({
			save: function(p, id) {
			    //保存收款计划
			    me.Preceiveplan.EditSaveInfo(id);
				me.fireEvent('save', me, id);
			}
		});
		
		me.Attachment.on({
			selectedfilerender: function(p) {
				me.Attachment.save();
			},
			save: function(p) {
				if(me.Attachment.progressMsg!="")
				JShell.Msg.alert(me.Attachment.progressMsg);
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
						me.Preceiveplan.onSaveReceivePlan(me.Form,me.PK);
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
		me.Form = Ext.create('Shell.class.wfm.business.contract.apply.Form',{
			title:'合同内容',
			formtype: 'edit',
			hasButtontoolbar:false,//带功能按钮栏
			hasLoadMask: false,//开启加载数据遮罩层
		    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',
			PK:me.PK
		});
		
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment',{
			title:'合同附件',
			PK:me.PK
		});
		me.EquipInfoListForm = Ext.create('Shell.class.wfm.business.contract.apply.EquipInfoListForm', {
			title: '仪器清单',
			header: false,
			formtype: 'edit',
			PK:me.PK,
			itemId: 'EquipInfoListForm',
			border: false,
			/**带功能按钮栏*/
        	hasButtontoolbar: false
		});
		me.PurchaseDescForm = Ext.create('Shell.class.wfm.business.contract.apply.PurchaseDescForm', {
			title: '采购说明',
			header: false,
			formtype: 'edit',
			PK:me.PK,
			itemId: 'PurchaseDescForm',
			border: false,
			/**带功能按钮栏*/
        	hasButtontoolbar: false
		});
		me.Preceiveplan = Ext.create('Shell.class.wfm.business.contract.apply.PreceiveplanTree',{
			title:'收款计划',
			itemId: 'Preceiveplan',
			/**底部工具栏*/
	        hasBottomToolbar: false,
			PContractID:me.PK
		});
		me.RcvedRecordHtml = Ext.create('Shell.class.wfm.business.contract.search.RcvedRecordHtml', {
			title: '老收款记录',
			header: false,
			layout: 'fit',
			itemId: 'RcvedRecordHtml',
			PK:me.PK
		});
		
		me.Operate = Ext.create('Shell.class.sysbase.scoperation.SCOperation',{
			title:'操作记录',
			classNameSpace:'ZhiFang.Entity.ProjectProgressMonitorManage',//类域
			className:'PContractStatus',//类名
			PK:me.PK
		});
		
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App',{
			title:'交流信息',
			FormPosition:'e',
			PK:me.PK
		});
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
		return [me.Form,me.Attachment,me.EquipInfoListForm,me.PurchaseDescForm,me.Preceiveplan,me.RcvedRecordHtml,me.Operate,me.Interaction,me.Pdf];
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this;
		var dockedItems = {
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
				text:'提交',
				iconCls:'button-save',
				tooltip:'提交',
				handler:function(){
					me.onSave(true);
				}
			},'reset']
		};
		return dockedItems;
	},
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.Form.getForm().getValues();
		if(!me.Form.getForm().isValid()){
			me.Form.fireEvent('isValid',me);
			return;
		}
		var edit = me.Preceiveplan.getPlugin('NewsGridEditing'); 
        edit.completeEdit();
        edit.cancelEdit();
	    /**校验收款计划校验   @author liangyl @version 2017-07-27*/
	   	me.Preceiveplan.Amount=values.PContract_Amount;
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
		var contentvalues = me.EquipInfoListForm.getForm().getValues();
		//采购说明
		var purchasedescvalues = me.PurchaseDescForm.getForm().getValues();
		//采购说明为空时，设置默认值
		var PurchaseDescHTML=purchasedescvalues.PContract_PurchaseDescHTML.replace(/\\/g, '&#92');
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请重新保存');
    			return;
    		}

		   var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',values.PContract_ContractStatus);
		   var Name = info ? info.Name : '';
		   
		   if(isSubmit){//提交
				me.Form.getForm().setValues({
					PContract_LinkEquipInfoListHTML:contentvalues.PContract_LinkEquipInfoListHTML.replace(/\\/g, '&#92'),
					PContract_PurchaseDescHTML:PurchaseDescHTML
				});
			}
		   if(Name == '暂存' || Name == '申请'  ){
				me.Form.onSaveClick();
			}else{
				//合同编号不为空，需要验证
	            if(values.PContract_ContractNumber){
	            	me.Form.isValidContractNumber(function(){
						me.Form.onSaveClick();
					},me.PK);
	            }
			}
    	});
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.body){me.body.unmask();}//隐藏遮罩层
	},
	/**校验合同总额和收款总额   @author liangyl @version 2017-07-27*/
	isValidAmount:function(Id,callback){
		var me = this,
			values = me.Form.getForm().getValues(),
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
    	var	values = me.Form.getForm().getValues();
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
    }
});