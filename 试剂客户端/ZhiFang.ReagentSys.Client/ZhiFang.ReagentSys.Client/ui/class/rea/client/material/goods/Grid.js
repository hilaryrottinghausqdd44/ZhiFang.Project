/**
 * 物资试剂对照关系表
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.material.goods.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '物资试剂对照关系列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaMaterialReaGoodsMatchByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaMaterialReaGoodsMatch',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaMaterialReaGoodsMatchByField',
    /**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaMaterialReaGoodsMatch',
	/**修改试剂服务地址*/
    editGoodsUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
 	/**删除试剂数据服务路径*/
	delGoodsUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoods',

	/**默认加载数据*/
	defaultLoad: false,
	//业务接口ID
	BusinessID:null,
	//业务接口名称
	BusinessCName:null,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**后台排序*/
	remoteSort: false,
	/**默认每页数量*/
	defaultPageSize: 50000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
    },
    loadDataById:function(id){
    	var me =this;
    	me.defaultWhere="reamaterialreagoodsmatch.BusinessId="+id;
    	me.onSearch();
    },
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			xtype: 'actioncolumn',text: '删除',align: 'center',width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var GoodsID =rec.get('ReaMaterialReaGoodsMatch_GoodsID');
					JShell.Msg.del(function(but) {
						if (but != "ok") return;
						me.showMask(me.delText); //显示遮罩层
						me.delOneById(1,rec.get('ReaMaterialReaGoodsMatch_Id'));
					    me.onUpateGoodsOne(rec);
					    me.hideMask();
					    me.store.remove(rec);
					});
				}
		    }]
		},{
			dataIndex: 'ReaMaterialReaGoodsMatch_GoodsName',
			text: '货品名称',width: 220,defaultRenderer: true
		}, {
			dataIndex: 'ReaMaterialReaGoodsMatch_GoodsID',text: '货品ID',
			width: 100,hidden: true,defaultRenderer: true
		}, {
			dataIndex: 'ReaMaterialReaGoodsMatch_MatchCode',
			text: '<b style="color:blue;">物资对照码</b>',editor:{},
			width: 120,hidden: false,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaGoodsMatch_BusinessCName',text: '业务接口名称',
			width: 100,hidden: true,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaGoodsMatch_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true
		},{
			dataIndex: 'ReaMaterialReaGoodsMatch_BusinessId',text: '业务接口ID',
			width: 100,hidden: true,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaGoodsMatch_Compare',text: '是否已对照',
			width: 100,hidden: true,hideable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaGoodsMatch_Tab',text: '新增1，修改2',
			width: 100,hidden: true,hideable: false,defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh','-','add','-','save',
		'-',{
            xtype:'checkboxfield',margin:'0 0 0 10',boxLabel: '已对照', 
            name: 'IsCompare',itemId:'IsCompare',
            listeners:{
	            change : function(com,newValue,oldValue,eOpts){
				    var Compare='';
                    if(newValue) Compare ='已对照';
					me.onSearchFilter(Compare);
	            }
            }
	    },{
			name: 'txtSearch',itemId: 'txtSearch',height:22,
			emptyText: '货品名称',margin: '0 0 0 10',//labelSeparator:'',
			width: 135,hidden:true,labelAlign: 'right',
		    xtype:'textfield',fieldLabel:'',labelWidth:0,enableKeyEvents:true,
		    listeners:{
            	specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						var newValue = field.getValue();
						me.onSearchFilter(newValue);
					}
            	}
            }
	    }];
		return items;
	},
		/**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	},
	onAddClick:function(){
		var me =this;
		me.showForm();
		
	},
	/**显示表单*/
	showForm: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight * 0.68;
		var	config = {	
			resizable: false,
			width:900,
            height:height,
            formtype:'add',
            Ids:me.getGoodsID(),
			listeners: {
				save:function(p){
                    me.onSearch();
                    p.close();
                },
                accept:function(p,records,ischeck){
                	var count=me.getStore().getCount();
                    for(var i=0;i<records.length;i++){
                        var obj ={
                        	ReaMaterialReaGoodsMatch_GoodsName:records[i].get('ReaGoods_CName'),
                        	ReaMaterialReaGoodsMatch_GoodsID:records[i].get('ReaGoods_Id'),
                        	ReaMaterialReaGoodsMatch_BusinessCName:me.BusinessCName,
                        	ReaMaterialReaGoodsMatch_BusinessId:me.BusinessID,
                        	ReaMaterialReaGoodsMatch_Tab:'1'
                        };
                        if(ischeck){
                        	obj.ReaMaterialReaGoodsMatch_MatchCode=records[i].get('ReaGoods_MatchCode');
                        }
                        me.store.insert(count+1, obj);
                    }
                    p.close();
                }
              
			}
		};
		JShell.Win.open('Shell.class.rea.client.material.goods.CheckGrid', config).show();
	},
	 /**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
		for(var i=0;i<data.list.length;i++){	
			var Id =data.list[i].ReaMaterialReaGoodsMatch_Id;
			var MatchCode =data.list[i].ReaMaterialReaGoodsMatch_MatchCode;
			var GoodsID =data.list[i].ReaMaterialReaGoodsMatch_GoodsID;
            var Compare= '未对照';
			if(Id && GoodsID &&  MatchCode){
				Compare= '已对照';
			}
			var obj1={
				ReaMaterialReaGoodsMatch_Tab:'2',
				ReaMaterialReaGoodsMatch_Compare:Compare
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return result;
	},
	onSaveClick:function(){
		var me = this;
		var	records = me.store.data.items;
		var isError = false;
		var changedRecords = me.store.getModifiedRecords(),//获取修改过的行记录
			len = changedRecords.length;
			
		if(len == 0){
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			//新增
			if(changedRecords[i].get('ReaMaterialReaGoodsMatch_Tab')=='1'){
				me.oneAdd(i,changedRecords[i]);
				me.onUpateGoods(changedRecords[i]);
			}
			//修改
			if(changedRecords[i].get('ReaMaterialReaGoodsMatch_Tab')=='2'){
				me.updateOne(i,changedRecords[i]);
				me.onUpateGoods(changedRecords[i]);
			}
		}
	},
	/**行新增信息*/
	oneAdd:function(i,record){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		var entity={
			GoodsName:record.get('ReaMaterialReaGoodsMatch_GoodsName'),
			GoodsID:record.get('ReaMaterialReaGoodsMatch_GoodsID'),			
			MatchCode:record.get('ReaMaterialReaGoodsMatch_MatchCode'),
			BusinessCName:record.get('ReaMaterialReaGoodsMatch_BusinessCName'),
			BusinessId:record.get('ReaMaterialReaGoodsMatch_BusinessId'),
			Visible:1
		};
		var params = Ext.JSON.encode({
			entity:entity
		});
		JShell.Server.post(url,params,function(data){
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
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){
					JShell.Msg.alert('保存成功', null, 2000);
					me.onSearch();
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		},false);
	},
	/**修改信息*/
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var entity={
			Id:record.get('ReaMaterialReaGoodsMatch_Id'),
			GoodsName:record.get('ReaMaterialReaGoodsMatch_GoodsName'),
		    GoodsID:record.get('ReaMaterialReaGoodsMatch_GoodsID'),
		    MatchCode:record.get('ReaMaterialReaGoodsMatch_MatchCode'),
			BusinessCName:record.get('ReaMaterialReaGoodsMatch_BusinessCName'),
			BusinessId:record.get('ReaMaterialReaGoodsMatch_BusinessId')
		};
		fields='Id,GoodsName,GoodsID,MatchCode,BusinessCName,BusinessId';
		var params = Ext.JSON.encode({entity:entity,fields:fields});
		
		JShell.Server.post(url,params,function(data){
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
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){
					JShell.Msg.alert('保存成功', null, 2000);
					me.onSearch();
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		},false);
	},
	/**更改货品物资对照码*/
	onUpateGoods:function(record){
		var me = this;
		var url = (me.editGoodsUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editGoodsUrl;
		var entity={
			Id:record.get('ReaMaterialReaGoodsMatch_GoodsID'),
			MatchCode:record.get('ReaMaterialReaGoodsMatch_MatchCode') ? record.get('ReaMaterialReaGoodsMatch_MatchCode') : null
		};
		fields='Id,MatchCode';
		var params = Ext.JSON.encode({entity:entity,fields:fields});
		JShell.Server.post(url,params,function(data){
			if(!data.success){
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**更改货品物资对照码*/
	onUpateGoodsOne:function(record){
		var me = this;
		var url = (me.editGoodsUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editGoodsUrl;
		var entity={
			Id:record.get('ReaMaterialReaGoodsMatch_GoodsID'),
			MatchCode:null
		};
		fields='Id,MatchCode';
		var params = Ext.JSON.encode({entity:entity,fields:fields});
		JShell.Server.post(url,params,function(data){
			if(!data.success){
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**获取试剂id*/
	getGoodsID:function(){
		var me =this;
		var	records = me.store.data.items;
        var ids='';
		for(var i=0;i<records.length;i++){
			if(i>0){
				ids+=',';
			}
			ids+=records[i].get('ReaMaterialReaGoodsMatch_GoodsID');
		}
		return ids;
	},
	onSearchFilter:function(value) {
        var me = this;
        if (!value) {
            me.store.clearFilter();
            return;
        }
        value = String(value).trim().split(" ");
        me.store.filterBy(function(record, id) {
            var data = record.data;
            var CName = record.data.ReaMaterialReaGoodsMatch_GoodsName;
            var IsCompare = record.data.ReaMaterialReaGoodsMatch_Compare;
            var dataarr = {
                ReaMaterialReaGoodsMatch_GoodsName:CName,
                ReaMaterialReaGoodsMatch_Compare:IsCompare
            };
            for (var p in dataarr) {
                var porp = Ext.util.Format.lowercase(String(dataarr[p]));
                for (var i = 0; i < value.length; i++) {
                    var macther = value[i];
                    var macther2 = Ext.escapeRe(macther);
                    mathcer = new RegExp(macther2);
                    if (mathcer.test(porp)) {
                        return true;
                    }
                }
            }
            return false;
        });
    }
});