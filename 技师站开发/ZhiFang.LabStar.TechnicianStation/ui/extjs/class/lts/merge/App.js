/**
 * 样本信息合并
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.merge.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'样本信息合并',
    //检验样本合并
    addUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormInfoMerge',
    //按钮是否可点击
	BUTTON_CAN_CLICK: true,
	//是否进行过合并,用于判断是否刷新检验单列表
	isMerge:false,
	SectionID:null,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.Form.on({
			accept:function(obj){
				me.Panel.onSearch(obj);
			},
			save: function (isDelFormTestItem, isDelFormTestForm, p) {
				me.onSaveClick(isDelFormTestItem, isDelFormTestForm);
			}
		});

	},
	initComponent:function(){
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		var toolitems = ['->'];
        toolitems.push({text:'关闭',tooltip:'关闭',iconCls:'button-del',
            handler:function(but,e){
            	if (me.BUTTON_CAN_CLICK)me.close();
		    	
		    }
        });
		items.push(Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			itemId:'bottomToolbar',
			items:toolitems
		}));
		return items;
	},
	createItems:function(){
		var me = this;
		me.Form = Ext.create('Shell.class.lts.merge.Form', {
			region:'west',
			width:230,
			itemId: 'Info',
			SectionID: me.SectionID,
			header:false,
			split:true,
			collapsible:false
		});
		me.Panel = Ext.create('Shell.class.lts.merge.Panel', {
			region:'center',
			itemId:'Panel',
			header:false
		});
		return [me.Form,me.Panel];
	},
	//执行合并
	onSaveClick: function (isDelFormTestItem, isDelFormTestForm){
		var me = this,
			isDelFormTestItem = isDelFormTestItem || false,
			isDelFormTestForm = isDelFormTestForm || false;
		
		if (!me.BUTTON_CAN_CLICK) return;
		
		//源项目合并后,如果没有项目,删除源样本单
    	if(isDelFormTestForm){
    		var isExec = me.Panel.isVerify();
    		if(!isExec)return;
    	}
		    	
		var values = me.Form.getForm().getValues();
	    var url = JShell.System.Path.ROOT + me.addUrl;
	    var entity ={
	    	mergeType:values.mergeType,
	    	isSampleNoMerge: values.isSampleNoMerge ? 1 : 0,
	    	isSerialNoMerge: values.isSerialNoMerge ? 1 : 0
	    };
		var parms = me.Panel.getParams();
		if (!parms) {
			JShell.Msg.alert("请先确定合并样本！");
			return;
		}
		entity.fromTestFormID = parms.FromTestFormID;
		if (parms.toTestForm) entity.toTestForm = parms.toTestForm;//目标检验单实体

	    if(parms.StrFromTestItemID && values.mergeType!="1"){
	    	entity.strFromTestItemID = parms.StrFromTestItemID;
	    }else{
	    	entity.strFromTestItemID = "";
	    }

		entity.isDelFormTestItem = isDelFormTestItem ? 1 : 0;
	    entity.isDelFormTestForm = isDelFormTestForm ? 1 : 0;
		if (values.mergeType == "2" && entity.toTestForm.Id ==0){
	    	JShell.Msg.alert("仅合并项目信息时，目标样本不能为空!");
	    	return;
	    }
	    me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
			me.BUTTON_CAN_CLICK = true;
			if(data.success){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
				me.isMerge = true;
			    //清空数据
			    me.Panel.clearData();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});