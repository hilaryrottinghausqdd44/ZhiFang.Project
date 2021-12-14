/**
 * 批次信息
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods2.unit.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '批次信息',
	width: 800,
	height: 500,

    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnitByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoodsUnit',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsUnitByField',
	 /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaGoodsUnit',
	
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
        me.loadDataById();
		me.on({
			nodata:function(){
				me.getView().update('');
			}
		});
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
			dataIndex: 'ReaGoodsUnit_GoodsUnit',
			text: '主单位',
			flex:1,
			maxWidth: 100,
			editor:{}
		}, {
			dataIndex: 'ReaGoodsUnit_ChangeUnit',
			text: '次单位',
			flex:1,
			maxWidth: 100,
			editor:{
				xtype:'uxCheckTrigger',
				className:'Shell.class.rea.client.goods2.unit.CheckGrid',
				classConfig:{
					title:'单位选择',
					/**是否单选*/
		            checkOne:true
				},
				listeners: {
					check: function(p, record) {
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						
						if(records[0].get('ReaGoodsUnit_Id') == record.get('ReaGoodsUnit_Id')){
							JShell.Msg.error('次单位不能是选择行的主单位!');
							return;
						}
						records[0].set('ReaGoodsUnit_ReaGoodsUnit_Id', record ? record.get('ReaGoodsUnit_Id') : '');
						records[0].set('ReaGoodsUnit_ChangeUnit', record ? record.get('ReaGoodsUnit_GoodsUnit') : '');
						p.close();
					}
				}
			}
		}, {
			dataIndex: 'ReaGoodsUnit_ReaGoodsUnit_Id',
			text: '次单位id',
			hidden:true,
			width: 50
		}, {
			dataIndex: 'ReaGoodsUnit_ChangeQty',
			text: '换算比列',
			text:'<b style="color:blue;">换算比列</b>',
			width:80,type:'float',align:'right',
			editor:{xtype:'numberfield',minValue:0,decimalPrecision:2,allowBlank:false}
		}, {
			dataIndex: 'ReaGoodsUnit_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true}
		}, {
			dataIndex: 'ReaGoodsUnit_Memo',
			text: '备注',
			flex: 1,
			editor:{}
		},{
			dataIndex: 'ReaGoodsUnit_DispOrder',
			text: '显示次序',
			width: 70,
			align:'center',editor:{}
		},{
			dataIndex: 'ReaGoodsUnit_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 45,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
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
//				    me.delOneById(2, id);
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
			ReaGoodsUnit_GoodsUnit:'',
			ReaGoodsUnit_ChangeUnit:'',
			ReaGoodsUnit_ReaGoodsUnit_Id:'',
			ReaGoodsUnit_ChangeQty:'',
			ReaGoodsUnit_Visible:'1',
			ReaGoodsUnit_Memo:'',
			ReaGoodsUnit_Id:'',
			ReaGoodsUnit_DispOrder:'0'
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
				JShell.Msg.error("单位信息保存有误！");
			}
		}
	},
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = {
			entity:{
				Id:record.get('ReaGoodsUnit_Id'),
				GoodsUnit:record.get('ReaGoodsUnit_GoodsUnit'),
				ChangeQty:record.get('ReaGoodsUnit_ChangeQty'),
				Memo:record.get('ReaGoodsUnit_Memo'),
				DispOrder:record.get('ReaGoodsUnit_DispOrder'),
				Visible:record.get('ReaGoodsUnit_Visible')? 1 : 0
			}
		};
		if(record.get('ReaGoodsUnit_ReaGoodsUnit_Id')){
			params.entity.ReaGoodsUnit = {
				Id:record.get('ReaGoodsUnit_ReaGoodsUnit_Id'),
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(record.get('ReaGoodsUnit_ChangeUnit')){
			params.entity.ChangeUnit=record.get('ReaGoodsUnit_ChangeUnit');
		}
		params.fields='Id,Visible,ChangeUnit,DispOrder,Memo,ChangeQty,GoodsUnit';

//		params.fields='Id,Visible,ChangeUnitID,ChangeUnit,DispOrder,Memo,ChangeQty,GoodsUnit,ReaGoodsUnit_Id';
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
				GoodsUnit:record.get('ReaGoodsUnit_GoodsUnit'),
				ChangeQty:record.get('ReaGoodsUnit_ChangeQty'),
				Memo:record.get('ReaGoodsUnit_Memo'),
				DispOrder:record.get('ReaGoodsUnit_DispOrder'),
				Visible:record.get('ReaGoodsUnit_Visible')? 1 : 0
			}
		};
		if(record.get('ReaGoodsUnit_ReaGoodsUnit_Id')){
			params.entity.ReaGoodsUnit = {
				Id:record.get('ReaGoodsUnit_ReaGoodsUnit_Id'),
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(record.get('ReaGoodsUnit_ChangeUnit')){
			params.entity.ChangeUnit=record.get('ReaGoodsUnit_ChangeUnit');
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
			//验证主单位不能为空
			var GoodsUnit =records[i].get('ReaGoodsUnit_GoodsUnit');
           if(!GoodsUnit){
            	isExect=false;
            	JShell.Msg.error("主单位不能为空");
            	break;
            }
		}
		return isExect;
	},
	/**根据货品id加载*/
   loadDataById:function(){
   	   var me=this;
   	   if(me.GoodsID){
   	   	   me.defaultWhere='reagoodsunit.ReaGoods.Id='+me.GoodsID;
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