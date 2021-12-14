/**
 * 最小单位设置
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods2.MinUnitGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '最小单位设置',
	width: 500,
	height: 350,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
	/**默认加载数据*/
	defaultLoad: false,
	/**产品编号*/
	GoodsNo:null,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页按钮栏*/
	hasPagingtoolbar: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.GoodsNo){
			me.defaultWhere="reagoods.GoodsNo='"+me.GoodsNo+"'";
		    me.onSearch();
		}
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
		var columns = [ {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 100,
			editor:{}
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 100,
			editor:{}
		},{
			dataIndex: 'ReaGoods_IsMinUnit',
			text: '最小单位',
			width: 80,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true,
				 listeners: {
				 	change : function (com,newValue,oldValue,eOpts ){
				 		if(newValue){
			 				var records = me.getSelectionModel().getSelection();
							if(records.length != 1) {
								JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
								return;
							}
							
							me.setIsMinUnit(records[0].get(me.PKField),newValue);
							records[0].set('ReaGoods_GonvertQty', '1');
				 		}
				 	}
				 }
			}
			
		},{
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'ReaGoods_GonvertQty',
			text: '换算系数',
			width: 100,
			editor:{xtype:'numberfield'}
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
		
//	    var isMinUnit=me.getIsMinUnit();
//	    if(!isMinUnit){
//	    	JShell.Msg.error('请设置默认最小单位');
//	    	return;
//	    }
		var changedRecords = me.store.getModifiedRecords(),
		    len = changedRecords.length;
		if(len == 0 && me.delArr.length== 0 ){
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
				Id:record.get('ReaGoods_Id'),
				UnitName:record.get('ReaGoods_UnitName'),
				UnitMemo:record.get('ReaGoods_UnitMemo'),
				GonvertQty:record.get('ReaGoods_GonvertQty'),
				IsMinUnit:record.get('ReaGoods_IsMinUnit')? 1 : 0
			}
		};
	
		params.fields='Id,UnitName,UnitMemo,GonvertQty,IsMinUnit';
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
	//同一产品编号只能设置一个最小单位
	setIsMinUnit:function(id,value){
		var me=this;
		me.store.each(function(record) {
			if(record.get(me.PKField) == id ){
				record.set('ReaGoods_IsMinUnit',value);
			}else{
				record.set('ReaGoods_IsMinUnit',false);
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
		var arr =[];
		//验证
		for(var i=0;i<len;i++){
			var rec = records[i];
			var GonvertQty=rec.get('ReaGoods_GonvertQty');
			var IsMinUnit=rec.get('ReaGoods_IsMinUnit');
            if(!GonvertQty){
            	JShell.Msg.error('换算系数不能为空');
				isExect=false;
		        return isExect;
            }
            arr.push(IsMinUnit);
		}
		var iserror = Ext.Array.contains(arr,true); //返回true 检查数组内是否包含指定元素
		if(!iserror){
		    JShell.Msg.error('请设置最小单位');
			isExect=false;
	        return isExect;
		}
		return isExect;
	}
});