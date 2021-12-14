/**
 * 是否使用编辑列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.model.IsUseGrid',{
    extend:'Shell.ux.grid.Panel',
    requires: ['Ext.ux.CheckColumn'],
    title:'',
    width:800,
    height:500,
    
    /**是否使用字段名*/
    IsUseField:'',
    /**是否使用字段的类型，bool/int，默认bool*/
    IsUseType:'bool',
    
    /**获取数据服务路径*/
    selectUrl:'',
    /**删除数据服务路径*/
	delUrl: '',
	/**修改服务地址*/
    editUrl:'',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用修改按钮*/
	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:false,
	/**排序字段*/
	//defaultOrderBy:[{property:'TestEquipType_DispOrder',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
    
	initComponent:function(){
		var me = this;
		//查询框信息
//		me.searchInfo = {
//			width:160,emptyText:'中文名/英文名/代码',isLike:true,
//			fields:['testequiptype.CName','testequiptype.EName','testequiptype.ShortCode']
//		};
		
		//数据列
		me.columns = me.columns || me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		return [];
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		for(var i in data.list){
			var IsUse = data.list[i][me.IsUseField];
			if(IsUse == true || IsUse == 'True' || IsUse == 'true'){
				data.list[i][me.IsUseField] = true;
			}else{
				data.list[i][me.IsUseField] = false;
			}
		}
		return data;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick');
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('editclick',me,records[0].get(me.PKField));
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
		
		//是否使用的类型不同处理
		if(me.IsUseType == 'int'){
			IsUse = IsUse ? "1" : "0";
		}
		var IsUseField = me.IsUseField.split('_').slice(-1) + '';
		
		var params = {};
		params.entity = {Id:id};
		params.entity[IsUseField] = IsUse;
		params.fields = 'Id,' + IsUseField;
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
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
	}
});