/**
 * 批次信息
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods2.lot.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '批次信息',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotByHQL?isPlanish=true',
	 /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaGoodsLot',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoodsLot',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLotByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: false,
	/**是否启用新增按钮*/
	hasAdd: true,
    hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	
	/**默认加载数据*/
	defaultLoad: false,
	/**删除的数据*/
	delArr:[],
	 /**货品ID*/
	GoodsID:null,
    /**货品名称*/
	GoodsCName:null,
	
	afterRender: function () {
        var me = this;
        me.callParent(arguments);
		me.on({
			nodata:function(){
				me.getView().update('');
			}
		});
		me.loadDataById();
    },
	initComponent: function() {
		var me = this;
		me.addEvents('isValid');
        me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsLot_LotNo',text: '批次号',
			flex:1,maxWidth: 180,editor:{}
		},{
			dataIndex: 'ReaGoodsLot_ProdDate',text:'生产日期',
			width:100,type:'date',isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'}
		}, {
			dataIndex: 'ReaGoodsLot_InvalidDate',text:'<b style="color:blue;">有效期</b>',
			width:100,type:'date', isDate:true,editor:{xtype:'datefield',format:'Y-m-d'}
		}, {
			dataIndex: 'ReaGoodsLot_Visible',text: '启用',width: 50,
			align:'center',type:'bool',isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true}
		},{
			dataIndex: 'ReaGoodsLot_DispOrder',text: '显示次序',
			width: 70,align:'center'
		},{
			dataIndex: 'ReaGoodsLot_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true
		},{
			xtype: 'actioncolumn',text: '删除',align: 'center',
			width: 45,style:'font-weight:bold;color:white;background:orange;',hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
				    var id = rec.get(me.PKField);
				    if(id){
				    	me.delArr.push(id);
				    }
				    me.store.remove(rec);
				}
			}]
		}];
		return columns;
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
	    var id = records[0].get(me.PKField);
	    if(id){
	    	me.delArr.push(id);
	    }
	    me.store.remove(records[0]);
    },
	 onAddClick:function(){
  	    var me=this;
  	    me.createAddRec();
    },
      /**增加一行*/
	createAddRec: function() {
		var me = this;
		var obj = {
			ReaGoodsLot_LotNo:'' ,
			ReaGoodsLot_ProdDate: '',
			ReaGoodsLot_InvalidDate: '',
			ReaGoodsLot_Visible:'1',
			ReaGoodsLot_DispOrder:'0'
		};
		me.store.add(obj);
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items;
		var isError = false;
		var changedRecords = me.store.getModifiedRecords(),
		    len = changedRecords.length;
		if(len == 0 && me.delArr.length== 0 ){
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		//删除的数据
		for(var item in me.delArr) {
		   me.delOneById(item, me.delArr[item]);
		}
		me.delArr=[];
		for(var i=0;i<len;i++){
			//存在id 编辑
			if(changedRecords[i].get(me.PKField)){
			    me.updateOne(i,changedRecords[i]);
			}else{
			    //不存在id 新增
			    me.addOne(changedRecords[i]);			
			}
		}
		if(me.saveCount + me.saveErrorCount == me.saveLength){
			me.hideMask();//隐藏遮罩层
			if(me.saveErrorCount == 0){
				me.delArr=[];
			}else{
//				JShell.Msg.error("批次信息保存有误！");
			}
		}
	},
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = {
			entity:{
				Id:record.get('ReaGoodsLot_Id'),
				LotNo:record.get('ReaGoodsLot_LotNo'),
				Visible:record.get('ReaGoodsLot_Visible')? 1 : 0
			}
		};
		if(record.get('ReaGoodsLot_InvalidDate')){
			var  isValid =JcallShell.Date.isValid(record.get('ReaGoodsLot_InvalidDate'));
			if(isValid){
				params.entity.InvalidDate=JShell.Date.toServerDate(record.get('ReaGoodsLot_InvalidDate'));
			}
		}
		if(record.get('ReaGoodsLot_ProdDate')){
			var  isValid =JcallShell.Date.isValid(record.get('ReaGoodsLot_ProdDate'));
			if(isValid){
				params.entity.ProdDate=JShell.Date.toServerDate(record.get('ReaGoodsLot_ProdDate'));
			}
		}
		
		if(me.GoodsID){
			params.entity.ReaGoods = {
				Id:me.GoodsID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		
		params.fields='Id,Visible,InvalidDate,ProdDate,ReaGoods_Id';
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
	//新增数据
	addOne:function(record){
		var me=this;
			var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var params = {
			entity:{
				Visible:record.get('ReaDeptGoods_Visible')? 1 : 0,
				LotNo:record.get('ReaGoodsLot_LotNo')
			}
		};
		
		if(record.get('ReaGoodsLot_InvalidDate')){
			var  isValid =JcallShell.Date.isValid(record.get('ReaGoodsLot_InvalidDate'));
			if(isValid){
				params.entity.InvalidDate=JShell.Date.toServerDate(record.get('ReaGoodsLot_InvalidDate'));
			}
		}
		if(record.get('ReaGoodsLot_ProdDate')){
			var  isValid =JcallShell.Date.isValid(record.get('ReaGoodsLot_ProdDate'));
			if(isValid){
				params.entity.ProdDate=JShell.Date.toServerDate(record.get('ReaGoodsLot_ProdDate'));
			}
		}
		if(me.GoodsID){
			params.entity.ReaGoods = {
				Id:me.GoodsID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(me.GoodsCName){
			params.entity.GoodsCName=me.GoodsCName;
		}
		//创建者信息
		var userId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) ;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			params.entity.CreatorID = userId;
		}
		if(userName){
			params.entity.CreatorName = userName;
		}
		//提交数据到后台
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				record.commit();
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}

		},false);
	},
	//验证
	isValid:function(records){
		var me=this;
		var isExect=true;
		for(var i=0;i<records.length;i++){
			//验证批次号不能为空
			var LotNo =records[i].get('ReaGoodsLot_LotNo');
			var ProdDate =records[i].get('ReaGoodsLot_ProdDate');
			var InvalidDate =records[i].get('ReaGoodsLot_InvalidDate');

            if(!LotNo || !ProdDate   || !InvalidDate){
            	isExect=false;
            	JShell.Msg.error("批次号,生成日期,有效期都不能为空");
            	break;
            }
          
            if(InvalidDate< ProdDate){
                isExect=false;
                JShell.Msg.error("开始时间不能大于有效期");
            	break;
            }
		}
		return isExect;
	},
	/**根据货品id加载*/
   loadDataById:function(){
   	   var me=this;
   	   if(me.GoodsID){
	   	   me.defaultWhere='reagoodslot.ReaGoods.Id='+me.GoodsID;
	   	   me.onSearch();
   	   }
   },
    /**查询数据*/
	onSearch: function(autoSelect) {
		var me=this;
		me.delArr=[];
		me.load(null, true, autoSelect);
	}
});