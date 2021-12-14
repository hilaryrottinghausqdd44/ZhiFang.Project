/**
 * 设置主库房
 * 只能有一个主库房
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.shelves.storage.MainStorageGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '设置主库房',
	width: 700,
	height: 350,
	/**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaStorageByField',
	/**默认加载数据*/
	defaultLoad: true,
	
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页按钮栏*/
	hasPagingtoolbar: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaStorage_CName',text: '库房名称',width: 150,
			sortable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_ShortCode',text: '代码',
			width: 100,sortable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_DispOrder',
			text: '次序',
			width: 50,
			align:'center',
			type:'int',	
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			 dataIndex: 'Tab',text: '标记',hidden:true,defaultRenderer: true,
			width: 50,hideable:true
			/*dataIndex: 'Tab',text: '库房标记',
			width: 90,//defaultRenderer: true,
			editor:{
				xtype:'uxSimpleComboBox',value:'0',hasStyle:true,
				data:[
					['0','一级库','color:green;'],
					['1','二级库','color:orange;'],
					
				]
			},
			renderer:function(value, meta) {
				var v = "";
				if(value == "0"){
					v = "一级库";
					meta.style = "color:green;";
				}else if (value == "1") {
					v = "二级库";
					meta.style = "color:orange;";
				}
				
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}*/
		},{
			dataIndex: 'ReaStorage_IsMainStorage',text: '一级库房',
			width: 80,align:'center',type:'bool',
			isBool:true,sortable: false,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true
				/* listeners: {
				 	change : function (com,newValue,oldValue,eOpts ){
				 		var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
				 		me.setIsMinUnit(records[0].get(me.PKField),newValue);
				 	}
				 }*/
			}
		}, {
			dataIndex: 'ReaStorage_Memo',text: '描述',
			flex: 1,sortable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_Id',text: '主键ID',hidden: true,
			hideable: false,isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh','-','save'];
		
		return items;
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items;
		var isError = false;
		//校验
		var isExect = me.isVerification();
		if(!isExect) return;
		var changedRecords = me.store.getModifiedRecords(),
		    len = changedRecords.length;
		if(len == 0 ){
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			//存在id 编辑
			if(changedRecords[i].get(me.PKField)){
			    me.updateOne(i,changedRecords[i]);
			}
		}
		if(me.saveCount + me.saveErrorCount == me.saveLength){
			me.hideMask();//隐藏遮罩层
			if(me.saveErrorCount == 0){
				me.fireEvent('save', me);
			}
		}
	},
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = {
			entity:{
				Id:record.get('ReaStorage_Id'),
				IsMainStorage:record.get('ReaStorage_IsMainStorage')? 1 : 0
			}
		};
	
		params.fields='Id,IsMainStorage';
		var entity = Ext.JSON.encode(params);
		
		JShell.Server.post(url,entity,function(data){
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
			
		},false);
	},
	//只能设置一个主库房
	setIsMinUnit:function(id,value){
		var me=this;
		me.store.each(function(record) {
			if(record.get(me.PKField) == id ){
				record.set('ReaStorage_IsMainStorage',value);
			}else{
				record.set('ReaStorage_IsMainStorage',false);
			}
        });
        me.getView().refresh();
	},
    /**保存前验证*/
	isVerification:function(){
		var me=this,
			records = me.store.data.items,
			isExect=true,
			len = records.length;
		if(len == 0){
			isExect=false;
		    return isExect;
		} 
		var arr =[],num=0;
		var msg='';
		//验证
		for(var i=0;i<len;i++){
			var rec = records[i];
			var IsMainStorage=rec.get('ReaStorage_IsMainStorage');
			if(IsMainStorage=='1'){
            	num+=1;
            }
		}
	    if(num>1){
	    	//msg+='可以设置多个主库房<br>';
	    	isExect=true;
	    }
		/*if(isExect){
			JShell.Msg.alert(msg);
		}*/
		return isExect;
	}
});