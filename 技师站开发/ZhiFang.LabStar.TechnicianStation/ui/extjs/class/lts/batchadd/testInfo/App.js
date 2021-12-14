/**
 * 检验评语
 * @author liangyl	
 * @version 2019-12-17
 */
Ext.define('Shell.class.lts.batchadd.testInfo.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'检验评语',
    hasLoadMask:true,
    /**小组*/
	SectionID: 1,
	//检验评语内容
	TestInfo:'',
	/**小组检验评语名称*/
	TypeName:'',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			onAcceptClick : function(rec,IsClose){
				var values = me.ContentForm.getValues();
				var strName = values.LBPhrase_CName;
				if(strName && rec)strName+="\n";
				if(rec)strName += rec.get('LBPhrase_CName');
				strName = strName.replace(/\\/g, '&#92');
				
				me.ContentForm.setValues({
					LBPhrase_CName:strName
				});
				me.fireEvent('checked',me.ContentForm.getValues());
				
				if(IsClose)me.close();
		    }
		});
		//关闭前  取消正在编辑状态
		me.on({
			beforeclose : function ( panel,eOpts ){
				me.Grid.cancelEdit();
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.addEvents('checked');
		me.title = me.TypeName;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	createItems:function(){
		var me = this;
		me.TestInfo = me.TestInfo.replace(/\\/g, '&#92');
		me.ContentForm = Ext.create('Shell.class.lts.batchadd.testInfo.ContentForm', {
			region:'north',
			height:140,
			itemId:'ContentForm',
			TestInfo:me.TestInfo,
			TypeName:me.TypeName,
			split:true,
			header:false,
			collapsible:false
		});
		me.Grid = Ext.create('Shell.class.lts.batchadd.testInfo.Grid', {
			region:'center',
			itemId:'Tab',
		    SectionID:me.SectionID,
		    TypeName:me.TypeName,
			header:false
		});
		
		return [me.ContentForm,me.Grid];
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	}
});