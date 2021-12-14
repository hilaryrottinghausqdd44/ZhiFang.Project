/**
 * 经销商列表
 * @author liangyl
 * @version 2017-07-19
 */
Ext.define('Shell.class.pki.dealer.dealer.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '经销商列表',

	width: 343,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBDealerByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelBDealer',
	editUrl: '/BaseService.svc/ST_UDTO_UpdateBDealerByField',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**只读*/
	readOnly:false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查看按钮*/
	hasShow: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**经销商ID*/
	BDealerId:'',
	/**经销商称*/
	BDealerName:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(!me.readOnly){
			me.on({
				itemdblclick: function(view, record) {
					me.onEditClick();
				}
			});
		}
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: '145px',
			emptyText: '经销商名称/用户代码',
			isLike: true,
			fields: ['bdealer.Name', 'bdealer.UseCode']
		};
		
		//数据列
		me.columns = me.createGridColumns();
	
		me.callParent(arguments);
	},
	createGridColumns:function(){
		var me = this;
			//数据列
		var columns = [{
			dataIndex: 'BDealer_UseCode',
			text: '用户代码',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BDealer_Name',
			text: '经销商名称',
			flex:1,
			minWidth: 150,
			maxWidth: 230,
			defaultRenderer: true
		}, {
			dataIndex: 'BDealer_BBillingUnit_Name',
			text: '默认开票方',
			flex:1,
			minWidth: 150,
			maxWidth: 230,
			defaultRenderer: true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BDealer_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		}, {
			dataIndex: 'BDealer_DealerType',
			text: '经销商类型',
			width: 100,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.DealerType['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
//				meta.style = 'background-color:' + JcallShell.PKI.Enum.IsLockedColor['E' + value] || '#FFFFFF';
				return v;
			}
			//defaultRenderer: true
		}, {
			dataIndex: 'BDealer_ContactInfo',
			text: '联系方式',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BDealer_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BDealer_BBillingUnit_Id',
			text: '开票方ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BDealer_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		}];
		return columns;

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
			var IsUse = rec.get('BDealer_IsUse');
			me.updateOneByIsUse(i,id,IsUse);
		}
	},
	updateOneByIsUse:function(index,id,IsUse){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
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
	onAddClick: function() {
		var me = this;
		var UseCode = me.getUseCode();
		me.openDealerForm(null,UseCode);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		me.openDealerForm(id,null);
	},
	/**打开表单*/
	openDealerForm: function(id,code) {
		var me = this;
		var config = {
			BDealerId:me.BDealerId,//上级经销商ID
			BDealerName:me.BDealerName,//上级经销商名称
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			UseCode:code,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.dealer.dealer.Form', config).show();
	},
	/**点击导入按钮处理*/
	onImportExcelClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.pki.excel.FileUpdatePanel', {
			formtype:'add',
			resizable: false,
			TableName: 'B_Dealer',
			ERROR_UNIQUE_KEY_INFO:'经销商代码有重复',
			listeners: {
				save: function() {
					me.onSearch();
				}
			}
		}).show();
	},
	 /**获取用户代码方法*/
	getUseCode: function() {
		var me = this;
		var UseCode = '';
		var UseCodeUrl = '/StatService.svc/Stat_UDTO_GetMaxNoByEntityName';
		var url = (UseCodeUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + UseCodeUrl;
		url += "?EntityName=BDealer&FieldName=UseCode";
//		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.get(url, function(data) {
//			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				UseCode = data.value;
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
		return UseCode;
	},
	
	loadByParentId:function(id,name){
		var me=this;
		me.BDealerId = id;
		me.BDealerName = name;
//		me.defaultWhere='';
//		if(id && id!='0' ){
			me.defaultWhere='bdealer.ParentID='+id;
//		}
		me.onSearch();
	}
});