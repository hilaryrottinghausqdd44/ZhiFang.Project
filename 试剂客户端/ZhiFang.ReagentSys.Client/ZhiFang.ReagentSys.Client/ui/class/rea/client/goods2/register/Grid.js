/**
 * 注册证信息
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods2.register.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '注册证信息',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL?isPlanish=true',
	 /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaGoodsRegister',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoodsRegister',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegisterByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: false,
	/**是否启用新增按钮*/
	hasAdd: true,
    hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	 /**货品ID*/
	GoodsID:null,
    /**货品名称*/
	GoodsCName:null,
	/**厂家*/
	FactoryName:null,
	/**货品英文名称*/
	EName:null,
	/**货品代码*/
	ShortCode:null,
	/**货品批号*/
	GoodsLotNo:null,
	/**货品编码*/
	GoodsNo:null,
	/**默认加载数据*/
	defaultLoad: false,
	/**删除的数据*/
	delArr:[],
	/**用户UI配置Key*/
	userUIKey: 'goods2.register.Grid',
	/**用户UI配置Name*/
	userUIName: "机构货品注册证维护列表",
	
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
        me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{dataIndex: 'ReaGoodsRegister_GoodsNo',text: '货品编码',width: 95,editor:{}
		},{
			dataIndex: 'ReaGoodsRegister_GoodsLotNo',text: '货品批号',width: 95,editor:{}
		},{
			dataIndex: 'ReaGoodsRegister_RegisterNo',text: '注册证编号',width: 95,editor:{}
		},{
			dataIndex: 'ReaGoodsRegister_RegisterDate',text: '注册日期',
			width:95,type:'date',isDate:true,editor:{xtype:'datefield',format:'Y-m-d'}
		}, {
			dataIndex: 'ReaGoodsRegister_RegisterInvalidDate',
			text:'<b style="color:blue;">注册证有效期</b>',
			width:95,type:'date',
            //isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'},
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					var BGColor = "";
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
					if(days < 0) {
						BGColor = "red";
					} else if(days >= 0 && days <= 30) {
						BGColor = "#e97f36";
					} else if(days > 30) {
						BGColor = "#568f36";
					}
					if(BGColor)
						meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				}
				return value;
			}
		}, 
		{
			dataIndex: 'ReaGoodsRegister_Visible',text: '启用',width: 45,align:'center',
			type:'bool',isBool:true,editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true}
		},{
			dataIndex: 'ReaGoodsRegister_EName',text: '英文名称',width: 100,editor:{}
		},{
			dataIndex: 'ReaGoodsRegister_ShortCode',text: '货品代码',width: 100,editor:{}
		},{
			dataIndex: 'ReaGoodsRegister_FactoryName',text: '厂家',width: 100,editor:{}
		},{
			dataIndex: 'ReaGoodsRegister_DispOrder',text: '显示次序',width: 70,hidden:false,align:'center',editor:{}
		},{
			dataIndex: 'ReaGoodsRegister_Id',text: '主键ID',hidden: true,hideable: false,isKey: true
		}, {
			text: '原件路径',dataIndex: 'ReaGoodsRegister_RegisterFilePath',
			width: 120,hidden: true,sortable: false,hideable: true,defaultRenderer: true
		},{
			xtype: 'actioncolumn',text: '上传',align: 'center',width: 45,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,hidden:true,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-edit hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
				    var id = rec.get(me.PKField);
				    
				}
			}]
		},{
			xtype: 'actioncolumn',text: '原件',align: 'center',width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get("ReaGoodsRegister_RegisterFilePath"))
						return 'button-search hand';
					else
						return '';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.IsSearchForm = false;
					var record = grid.getStore().getAt(rowIndex);
					me.openRegisterFile(record);
				}
			}]
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
				}
			}]
		}];
		return columns;
	},
	 onAddClick:function(){
  	    var me=this;
//	    me.createAddRec();
	    me.showForm(null);
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
      /**增加一行*/
	createAddRec: function() {
		var me = this;
		var obj = {
			ReaGoodsRegister_FactoryName:'',
			ReaGoodsRegister_GoodsNo:me.GoodsNo,
			ReaGoodsRegister_GoodsLotNo:'',
			ReaGoodsRegister_RegisterNo:'',
			ReaGoodsRegister_RegisterDate:'',
			ReaGoodsRegister_RegisterInvalidDate:'',
			ReaGoodsRegister_Visible:'1',
			ReaGoodsRegister_DispOrder:me.getStore().getCount()+1,
			ReaGoodsRegister_Id:'',
			ReaGoodsRegister_EName:me.EName,
			ReaGoodsRegister_ShortCode:''
		};
		me.store.add(obj);
	},
	/**显示表单*/
	showForm: function(id) {
		var me = this,
			config = {
				resizable: false,
				 /**货品ID*/
				GoodsID:me.GoodsID,
			    /**货品名称*/
				GoodsCName:me.GoodsCName,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onSearch();
					},
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
//		                edit.completeEdit();
		                edit.cancelEdit();
					}
				}
			};
       
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}

		JShell.Win.open('Shell.class.rea.client.goods2.register.Form', config).show();
	},
		/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//me.fireEvent('editclick',me,records[0].get(me.PKField));

		me.showForm(records[0].get(me.PKField));
	},
	
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items;
	
		var isError = false;
		var changedRecords = me.store.getModifiedRecords(),//获取修改过的行记录
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
			}
		}
		if(me.saveCount + me.saveErrorCount == me.saveLength){
			me.hideMask();//隐藏遮罩层
			if(me.saveErrorCount == 0){
				
			}
			else{
				JShell.Msg.error("注册证信息保存有误！");
			}
		}
	},
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = {
			entity:{
				FactoryName:record.get('ReaGoodsRegister_FactoryName'),
				GoodsNo:record.get('ReaGoodsRegister_GoodsNo'),
				GoodsLotNo:record.get('ReaGoodsRegister_GoodsLotNo'),
				RegisterNo:record.get('ReaGoodsRegister_RegisterNo'),			
				Id:record.get('ReaGoodsRegister_Id'),
				EName:record.get('ReaGoodsRegister_EName'),
				ShortCode:record.get('ReaGoodsRegister_ShortCode'),
				DispOrder:record.get('ReaGoodsRegister_DispOrder'),
				Visible:record.get('ReaGoodsRegister_Visible')? 1 : 0
			}
		};
	
		if(record.get('ReaGoodsRegister_RegisterInvalidDate')){
			var  isValid =JcallShell.Date.isValid(record.get('ReaGoodsRegister_RegisterInvalidDate'));
			if(isValid){
				params.entity.RegisterInvalidDate=JShell.Date.toServerDate(record.get('ReaGoodsRegister_RegisterInvalidDate'));
			}
		}
		if(record.get('ReaGoodsRegister_RegisterDate')){
			var  isValid =JcallShell.Date.isValid(record.get('ReaGoodsRegister_RegisterDate'));
			if(isValid){
				params.entity.RegisterDate=JShell.Date.toServerDate(record.get('ReaGoodsRegister_RegisterDate'));
			}
		}
		params.fields='Id,Visible,FactoryName,GoodsNo,GoodsLotNo,RegisterNo,EName,ShortCode,DispOrder,RegisterDate,RegisterInvalidDate';
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
	/*查看注册文件**/
	openRegisterFile: function(record) {
		var me = this;
		var id = "";
		if(record != null) {
			id = record.get('ReaGoodsRegister_Id');
		}	
		var url = JShell.System.Path.getRootUrl("/ReaSysManageService.svc/ST_UDTO_ReaGoodsRegisterPreviewPdf");
		url += '?operateType=1&id=' + id;
		window.open(url);
	},
	//验证
	isValid:function(records){
		var me=this;
		var isExect=true;
		for(var i=0;i<records.length;i++){
			//验证批次号不能为空
			var GoodsNo =records[i].get('ReaGoodsRegister_GoodsNo');
			var RegisterDate =records[i].get('ReaGoodsRegister_RegisterDate');
			var RegisterInvalidDate =records[i].get('ReaGoodsRegister_RegisterInvalidDate');
            var GoodsLotNo =records[i].get('ReaGoodsRegister_GoodsLotNo');
			var RegisterNo =records[i].get('ReaGoodsRegister_RegisterNo');
            var EName =records[i].get('ReaGoodsRegister_EName');
            if(!GoodsNo || !RegisterDate || !RegisterInvalidDate || !GoodsLotNo || !RegisterNo || !EName){
            	isExect=false;
            	JShell.Msg.error("英文名称,货品编码,货品批号,注册证编号,注册日期,有效期都不能为空!");
            	break;
            }
            if(RegisterInvalidDate < RegisterDate){
                isExect=false;
                JShell.Msg.error("开始时间不能大于有效期!");
            	break;
            }
		}
		return isExect;
	},
	/**根据货品id加载*/
   loadDataById:function(){
   	   var me=this;
   	   if(me.GoodsID){
	   	   me.defaultWhere='reagoodsregister.ReaGoods.Id='+me.GoodsID;
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