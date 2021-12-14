/**
 * 物资供应商对照关系表
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.material.company.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '物资供应商对照关系表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaMaterialReaCenOrgMatchByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaMaterialReaCenOrgMatch',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaMaterialReaCenOrgMatchByField',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaMaterialReaCenOrgMatch',
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
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
    	me.defaultWhere="reamaterialreacenorgmatch.BusinessId="+id;
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
					me.onDelClick();
				}
		    }]
		},{
			dataIndex: 'ReaMaterialReaCenOrgMatch_CompanyName',
			text: '供应商名称',width: 150,defaultRenderer: true
		}, {
			dataIndex: 'ReaMaterialReaCenOrgMatch_CompID',text: '供应商ID',
			width: 100,hidden: true,defaultRenderer: true
		}, {
			dataIndex: 'ReaMaterialReaCenOrgMatch_MatchCode',
			text: '<b style="color:blue;">物资对照码</b>',editor:{},
			width: 100,hidden: false,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaCenOrgMatch_BusinessCName',text: '业务接口名称',
			width: 100,hidden: true,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaCenOrgMatch_BusinessId',text: '业务接口ID',
			width: 100,hidden: true,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaCenOrgMatch_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true
		},{
			dataIndex: 'ReaMaterialReaCenOrgMatch_Compare',text: '是否已对照',
			width: 100,hidden: true,hideable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaMaterialReaCenOrgMatch_Tab',text: '新增1，修改2',
			width: 100,hidden: true,hideable: false,defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh','-','add','save',
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
			emptyText: '供应商名称',margin: '0 0 0 10',//labelSeparator:'',
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
			width:650,
            height:height,
            formtype:'add',
            Ids:me.getCompID(),
			listeners: {
				save:function(p){
                    me.onSearch();
                    p.close();
                },
                accept:function(p,records){
                	var count=me.getStore().getCount();
                	if(count==0){
                		me.getView().update();
                	}
                    for(var i=0;i<records.length;i++){
                        var obj ={
                        	ReaMaterialReaCenOrgMatch_CompanyName:records[i].get('ReaCenOrg_CName'),
                        	ReaMaterialReaCenOrgMatch_CompID:records[i].get('ReaCenOrg_Id'),
                        	ReaMaterialReaCenOrgMatch_BusinessCName:me.BusinessCName,
                        	ReaMaterialReaCenOrgMatch_BusinessId:me.BusinessID,
                        	ReaMaterialReaCenOrgMatch_Tab:'1'
                        };
                        me.store.insert(count+1, obj);
                    }
                    p.close();
                }
			}
		};
		JShell.Win.open('Shell.class.rea.client.material.company.CheckGrid', config).show();
	},
	 /**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
		for(var i=0;i<data.list.length;i++){	
			var Id =data.list[i].ReaMaterialReaCenOrgMatch_Id;
			var CompID =data.list[i].ReaMaterialReaCenOrgMatch_CompID;
            var MatchCode =data.list[i].ReaMaterialReaCenOrgMatch_MatchCode;

            var Compare= '未对照';
			if(Id && CompID && MatchCode){
				Compare= '已对照';
			}
			var obj1={
				ReaMaterialReaCenOrgMatch_Tab:'2',
				ReaMaterialReaCenOrgMatch_Compare:Compare
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return result;
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
            var CName = record.data.ReaMaterialReaCenOrgMatch_CompanyName;
            var IsCompare = record.data.ReaMaterialReaCenOrgMatch_Compare;
            var dataarr = {
                ReaMaterialReaCenOrgMatch_CompanyName:CName,
                ReaMaterialReaCenOrgMatch_Compare:IsCompare
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
   },
   	/**获取供应商id*/
	getCompID:function(){
		var me =this;
		var	records = me.store.data.items;
        var ids='';
		for(var i=0;i<records.length;i++){
			if(i>0){
				ids+=',';
			}
			ids+=records[i].get('ReaMaterialReaCenOrgMatch_CompID');
		}
		return ids;
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
			if(changedRecords[i].get('ReaMaterialReaCenOrgMatch_Tab')=='1'){
				me.oneAdd(i,changedRecords[i]);
			}
			//修改
			if(changedRecords[i].get('ReaMaterialReaCenOrgMatch_Tab')=='2'){
				me.updateOne(i,changedRecords[i]);
			}
		}
	},
	/**行新增信息*/
	oneAdd:function(i,record){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		var entity={
			CompanyName:record.get('ReaMaterialReaCenOrgMatch_CompanyName'),
			CompID:record.get('ReaMaterialReaCenOrgMatch_CompID'),			
			MatchCode:record.get('ReaMaterialReaCenOrgMatch_MatchCode'),
			BusinessCName:record.get('ReaMaterialReaCenOrgMatch_BusinessCName'),
			BusinessId:record.get('ReaMaterialReaCenOrgMatch_BusinessId'),
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
			Id:record.get('ReaMaterialReaCenOrgMatch_Id'),
			CompanyName:record.get('ReaMaterialReaCenOrgMatch_CompanyName'),
		    CompID:record.get('ReaMaterialReaCenOrgMatch_CompID'),
		    MatchCode:record.get('ReaMaterialReaCenOrgMatch_MatchCode'),
			BusinessCName:record.get('ReaMaterialReaCenOrgMatch_BusinessCName'),
			BusinessId:record.get('ReaMaterialReaCenOrgMatch_BusinessId')
		};
		fields='Id,CompanyName,CompID,MatchCode,BusinessCName,BusinessId';
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
	}
});