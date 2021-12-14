/**
 * 列表模板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.model.ExtraGrid',{
    extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
    
    title:'',
    width: 800,
	height: 500,
	
	/**是否使用字段*/
	IsUseField:null,
	/**其他信息模板路径*/
	OtherMsgModelUrl:null,
	/**其他信息模板内容*/
	OtherMsgModelContent:null,
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:50,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	
	initComponent: function() {
		var me = this;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || ['refresh','add','del','save','->',{
			xtype:'button',
			iconCls:'button-config',
			text:'设置模板',
			tooltip:'<b>设置其他信息模板</b>',
			handler:function(){me.openOtherMsgForm();}
		}];
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get(me.IsUseField);
			me.updateOneByIsUse(i,id,IsUse);
		}
	},
	updateOneByIsUse:function(index,id,IsUse){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		
		var params = Ext.JSON.encode({
			entity:{
				Id:id,
				IsUse:IsUse
			},
			fields:'Id,IsUse'
		});
		
		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick',this);
	},
	openOtherMsgForm:function(){
		var me = this;
		var url = JShell.System.Path.getUrl(me.OtherMsgModelUrl);
		if(me.OtherMsgModelContent == null){
			JShell.Server.get(url,function(text){
				me.OtherMsgModelContent = text;
				me.doOpenOtherMsgForm();
			},null,null,true);
		}else{
			me.doOpenOtherMsgForm();
		}
	},
	doOpenOtherMsgForm:function(){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.attachment.MsgModelForm',{
			resizable:false,
			title:'其他信息模板维护',
			FilePath:me.OtherMsgModelUrl,
			Content:me.OtherMsgModelContent,
			listeners:{
				save:function(p,msg){
					me.OtherMsgModelContent = msg;
					p.close();
				}
			}
		}).show();
	}
});