/**
 * 医生列表
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.doctor.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '医生列表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccountByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBDoctorAccountByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_DelBDoctorAccount',
	/**下载医生相片*/
	DownLoadImageUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DownLoadBDoctorAccountImageByAccountID',
	/**默认加载*/
	defaultLoad: false,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用修改按钮*/
	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用保存按钮*/
	//hasSave:true,
	/**是否启用查询框*/
	hasSearch:true,
	/**区域*/
	AreaID:null,
	/**查询栏参数设置*/
	searchToolbarConfig:{},
     /**医院*/
	HospitalID:'-1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		buttonsToolbar.insert(4,[{
			xtype:'button',
			iconCls:'button-add',
			text:'设置医生咨询费比率',
			tooltip:'设置医生咨询费比率',
			handler: function() {
				var	records = me.getSelectionModel().getSelection();
			    if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.openEditBonusPercentForm(records);
			}
		}]);
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				me.openEditForm(id);
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,itemId: 'search',
			fields:['bdoctoraccount.Name']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'医生名称',dataIndex:'BDoctorAccount_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医生工号',dataIndex:'BDoctorAccount_HWorkNumberID',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院名称',dataIndex:'BDoctorAccount_HospitalName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'科室名称',dataIndex:'BDoctorAccount_HospitalDeptName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医生类型',dataIndex:'BDoctorAccount_DoctorAccountType',width:100,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta) {
				var v = value + '';
				if (v == '1' || !v) {
//					meta.style = 'color:green';
					v = '普通医生';
				} else if (v == '2') {
//					meta.style = 'color:red';
					v = '检验技师';
				} else {
					v == '普通医生';
				}
				return v;
			}
		},{
			text:'次序',dataIndex:'BDoctorAccount_DispOrder',
			width:40,align:'center',sortable:false,menuDisabled:true
		},{
			text:'主键ID',dataIndex:'BDoctorAccount_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'微信主键ID',dataIndex:'BDoctorAccount_WeiXinUserID',width:170,hidden:true//,hideable:false
		},{
			xtype: 'actioncolumn',
			text: '绑定',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var WeiXinUserID = record.get('BDoctorAccount_WeiXinUserID');
					if(WeiXinUserID){
						meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>重新绑定医生和微信的关系</b>"';
						meta.style = 'background-color:#dd6572';
					}else{
						meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>绑定医生和微信的关系</b>"';
					}
					return 'button-config hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openWinXinCheckGrid(rec);
				}
			}]
		},{
			xtype: 'actioncolumn',
			text: '微信',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var WeiXinUserID = record.get('BDoctorAccount_WeiXinUserID');
					if(WeiXinUserID){
						meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>重新建立微信并绑定关系</b>"';
						meta.style = 'background-color:#dd6572';
					}else{
						meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>建立微信并绑定关系</b>"';
					}
					return 'button-config hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onopenAddAccount(rec);
				}
			}]
		},{
			text:'专业级别',dataIndex:'BDoctorAccount_ProfessionalAbilityName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'银行种类',dataIndex:'BDoctorAccount_BankID',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'银行帐号',dataIndex:'BDoctorAccount_BankAccount',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype: 'actioncolumn',
			text: '个人照片',
			align: 'center',
			width: 60,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id= rec.get(me.PKField);
					if(id){
						me.DownLoadImage(id,'PersonImage');
					}
					
				}
			}]
		},{
			xtype: 'actioncolumn',
			text: '职业证书',
			align: 'center',
			width: 60,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id= rec.get(me.PKField);
					if(id){
						me.DownLoadImage(id,'ProfessionalAbility');
					}
				}
			}]
		}];
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.weixin.doctor.Form', {
			SUB_WIN_NO:'1',//内部窗口编号
			resizable: false,
			formtype:'add',
			listeners: {
				save: function(p,id) {
					var PersonImageUrl=p.getComponent('BDoctorAccount_PersonImageUrl');
					if(PersonImageUrl.getValue()){
						p.onSavePersonImage(PersonImageUrl,id,'PersonImage');
					}
					var ProfessionalAbilityImageUrl=p.getComponent('BDoctorAccount_ProfessionalAbilityImageUrl');
					if(ProfessionalAbilityImageUrl.getValue()){
						p.onSavePersonImage(ProfessionalAbilityImageUrl,id,'ProfessionalAbility');
					}
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		var id = records[0].get(me.PKField);
		me.openEditForm(id);
	},
	/**打开修改页面*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.weixin.doctor.Form', {
			SUB_WIN_NO:'2',//内部窗口编号
			resizable: false,
			formtype:'edit',
			PK:id,
			listeners: {
				save: function(p,id) {
					
					var PersonImageUrl=p.getComponent('BDoctorAccount_PersonImageUrl');
					if(PersonImageUrl.getValue()){
						p.onSavePersonImage(PersonImageUrl,id,'PersonImage');
					}
					var ProfessionalAbilityImageUrl=p.getComponent('BDoctorAccount_ProfessionalAbilityImageUrl');
					if(ProfessionalAbilityImageUrl.getValue()){
						p.onSavePersonImage(ProfessionalAbilityImageUrl,id,'ProfessionalAbility');
					}
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**打开微信选择列表*/
	openWinXinCheckGrid:function(record){
		var me = this,
			DoctorAccountId = record.get(me.PKField);
		
		JShell.Win.open('Shell.class.weixin.doctor.weixin.CheckGrid',{
			SUB_WIN_NO:'3',//内部窗口编号
			resizable: false,
			listeners: {
				accept: function(p,rec) {
					var WeiXinUserID = rec ? rec.get(p.PKField) : null;
					me.onUpdateByWeiXinUserId(DoctorAccountId,WeiXinUserID,function(success){
						if(!success) return;
						record.set({BDoctorAccount_WeiXinUserID:WeiXinUserID});
						record.commit();
						p.close();
					});
				}
			}
		}).show();
	},
	/**更新微信员工ID*/
	onUpdateByWeiXinUserId:function(DoctorAccountId,WeiXinUserID,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
			
		var params = {
			entity:{
				Id:DoctorAccountId,
				WeiXinUserID:WeiXinUserID
			},
			fields:'Id,WeiXinUserID'
		};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				JShell.Msg.alert('关系绑定成功！',null,1000);
				callback(true);
			}else{
				JShell.Msg.error(data.msg);
				callback(false);
			}
		});
	},
	/**打开微信新增页面*/
	onopenAddAccount:function(record){
		var me = this,
			DoctorAccountId = record.get(me.PKField),
			WeiXinUserID = record.get('BDoctorAccount_WeiXinUserID');
		
		JShell.Win.open('Shell.class.weixin.doctor.weixin.AddAccount',{
			SUB_WIN_NO:'4',//内部窗口编号
			resizable: false,
			formtype:WeiXinUserID ? 'show' : 'add',
			PK: WeiXinUserID ? WeiXinUserID : '',
			listeners: {
				save: function(p,id) {
					me.onUpdateByWeiXinUserId(DoctorAccountId,id,function(success){
						if(success){
							record.set({BDoctorAccount_WeiXinUserID:id});
							record.commit();
						}
						p.close();
					});
				}
			}
		}).show();
	},
	/**下载*/
	DownLoadImage: function(Id,imageType) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.DownLoadImageUrl;
		url += '?accountID=' + Id + '&operateType=1&imageType='+imageType;
		window.open(url);
	},
	/**打开医生咨询费比率修改页面*/
	openEditBonusPercentForm:function(records){
		var me = this;
		JShell.Win.open('Shell.class.weixin.doctor.BonusPercentForm', {
			SUB_WIN_NO:'4',//内部窗口编号
			resizable: false,
			formtype:'edit',
			records:records,
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	  /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			params = [];
			
		me.internalWhere = '';
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		//根据医院查询
		if(me.HospitalID) {
			params.push("bdoctoraccount.HospitalID in (" + me.HospitalID+ ")");
		}
		//如果区域不为空，医院列表为空
		if(me.AreaID && !me.HospitalID){
			params.push("bdoctoraccount.HospitalID=null");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});