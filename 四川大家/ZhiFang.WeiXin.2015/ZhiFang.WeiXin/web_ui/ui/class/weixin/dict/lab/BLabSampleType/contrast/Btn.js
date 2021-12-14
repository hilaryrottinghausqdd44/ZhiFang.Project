Ext.define("Shell.class.weixin.dict.lab.BLabSampleType.contrast.Btn",{
	extend:'Shell.ux.panel.AppPanel',
	split: true,
	collapsible: true,
	 layout: {
	    type: 'vbox',
	    align: 'center',
	    pack:'center'
	},
	/**新增对照服务地址*/
	addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBSampleTypeControl',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBSampleTypeControl',
  
	initComponent:function(){
		var me = this;
		me.addEvents('click','cancelClick','intelligenceClick','save');
		me.items = me.createGitems();
		me.callParent(arguments);
	},
	
	createGitems:function(){
		var me = this;
		me.btn1=Ext.create('Ext.button.Button',{
			margin:'5 5 5 5',
			width:75,
			xtype:'button',
			text:'对照',
			iconCls:'button-accept',
			iconAlign:'left',
			handler:function(){
				me.fireEvent("click",me);
			}
		});
		
		me.btn2=Ext.create('Ext.button.Button',{
			margin:'5 5 5 5',
			width:75,
			xtype:'button',
			text:'取消对照',
			iconCls:'button-del',
			iconAlign:'left',
			handler:function(){
				me.fireEvent('cancelClick', me);
			}
		});
		
		me.btn3=Ext.create("Ext.button.Button",{
			margin:'5 5 5 5',
			width:75,
			xtype:'button',
			text:'智能对照',
			iconCls:'button-filter',
			iconAlign:'left',
			handler:function(){
				me.fireEvent('intelligenceClick', me);
			}
		});
		return [me.btn1,me.btn2,me.btn3];
	},
	
	onSaveAdd:function(contorlRow,row,ClienteleId){
		var me =this;
		if(row.get('isContrast')){
			me.onClearClick(row);
		}
		
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
	    var controlSickTypeID=contorlRow.get('SampleType_Id');
	    var labSickTypeNo=row.get('BLabSampleType_LabSampleTypeNo');
	    
	    var entity={
			SampleTypeControlNo:ClienteleId+"_"+controlSickTypeID+"_"+labSickTypeNo,
			SampleTypeNo:controlSickTypeID,
			ControlLabNo:ClienteleId,		
			ControlSampleTypeNo:labSickTypeNo,
			UseFlag:1
		};
		var params = Ext.JSON.encode({
			entity:entity
		});
	    
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.fireEvent('save', me);
			}else{
				JShell.Msg.error(data.error());
			}
		},false);
	},
	
	onClearClick:function(row){
		var me = this;
		var id = row.get('BLabSampleType_isContrast');
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
                me.fireEvent('save', me);
			} else {
                JShell.Msg.error(data.msg);
			}
			
		},false);
	},
});
