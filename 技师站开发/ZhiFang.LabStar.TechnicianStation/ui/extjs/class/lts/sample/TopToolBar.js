/**
 * 样本检验功能按钮
 * @author Jcall
 * @version 2019-11-05
 */
Ext.define('Shell.class.lts.sample.TopToolbar',{
    extend:'Shell.ux.toolbar.Button',
    title:'样本检验功能按钮',
    //每一种权限可能包含多个按钮（一起控制）
    //方案：平台：模块+按钮，角色-模块-按钮；技师站：人员+小组+角色；
    //登录后，根据人员获取小组+角色，实例化按钮；
    
    //获取角色列表服务
    getRoleListUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL',
    //获取角色操作权限列表
	getRoleOperListUrl:JShell.System.Path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL',
	//确认处理服务
	onConfirmUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormConfirm',
	//取消确认服务
	unConfirmUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormConfirmCancel',
	//审核服务
	onCheckUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormCheck',
	//取消审核服务
	unCheckUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormCheckCancel',
	//删除检验单服务
	onDelTestFormUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestForm',
	//恢复检验单服务
	unDelTestFormUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormDeleteCancel',
	//样本单智能审核判定服务
	onSystemCheckUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormIntellectCheck',
	
	//小组ID
	sectionId:null,
    //员工ID
    userId:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
    
    //操作编码列表
    OPER_CODE_LIST:null,
    //所有按钮信息
	ALL_BUTTONS_INFO:[],
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.sectionId){
			//初始化按钮
			me.initButtons();
		}else{
			me.OPER_CODE_LIST = [];
			//创建按钮
			me.creatButtons();
		}
	},
	initComponent:function(){
		var me = this;
		//操作数据
		me.OperateData = Ext.create('Shell.class.basic.data.Operate');
		
		me.items = [];
		//预定义所有按钮信息
		me.initPredefinedButtons();
		me.callParent(arguments);
	},
	//预定义所有按钮信息
	initPredefinedButtons:function(){
		var me = this;
		//所有按钮信息
	    me.ALL_BUTTONS_INFO = [{
	    	iconCls:'button-refresh',text:'刷新',tooltip:'刷新',
	    	handler:function(but){me.fireEvent('refresh');}
	    },{
	    	iconCls:'button-save',text:'保存(F2)',tooltip:'保存检验信息+检验项目',code:'save',code:'1003',itemId:'save',
	    	handler:function(but){me.fireEvent('saveclick');}
	    },{
	    	iconCls:'button-add',text:'保存并新增(F3)',tooltip:'新增前先保存检验信息+检验项目',code:'save',code:'1003',itemId:'saveAndadd',
	    	handler:function(but){me.fireEvent('saveandaddclick');}
	    },{
	    	iconCls:'button-add',text:'新增(F1)',tooltip:'新增',code:'add',code:'1003',itemId:'add',
	    	handler:function(but){me.fireEvent('addclick');}
	    },{
	    	iconCls:'button-accept',text:'检验确认',tooltip:'检验确认',code:'1001',
	    	handler:function(but){me.onConfirmClick();},
	    	xtype:'splitbutton',menu:[{
	    		iconCls:'button-uncheck',text:'取消确认',tooltip:'取消确认',
		    	handler:function(but){me.unConfirmClick();}
	    	},{
		    	iconCls:'button-config',text:'检验确认设置',tooltip:'检验确认设置',
		    	handler:function(but){
					me.onConfirmConfigClick();
		    	}
	    	}]
	    },{
	    	iconCls:'button-check',text:'审核',tooltip:'审核',code:'1002',
	    	handler:function(but){me.onCheckClick();},
	    	xtype:'splitbutton',menu:[{
	    		iconCls:'button-uncheck',text:'取消审核',tooltip:'取消审核',
		    	handler:function(but){me.unCheckClick();}
	    	},{
		    	iconCls:'button-config',text:'审核设置',tooltip:'审核设置',
		    	handler:function(but){
					me.onCheckConfigClick();
		    	}
	    	},{
		    	iconCls:'button-config',text:'智能审核设置',tooltip:'智能审核设置',
		    	handler:function(but){
					JShell.Win.open('Shell.class.lts.sample.operate.Tab',{
						activeTab:2,
						SectionID:me.sectionId
					}).show();
		    	}
	    	}]
	    },{
	    	text:'样本处理',code:'1003',
	    	iconCls:'button-edit',menu:[
	    		{iconCls:'button-edit',text:'常规转质控',tooltip:'常规转质控',handler:function(but){
	    			var Grid = me.ownerCt.Grid,
	    				records = Grid.getSelectionModel().getSelection();
			
					if (records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					var TestFormID = records[0].get(Grid.PKField);//检验单id
					var SectionID = records[0].get('LisTestForm_LBSection_Id');//小组id
					var OrderFormID = records[0].get('LisTestForm_LisOrderForm_Id');//医嘱单ID
					var MainStatusID = records[0].get('LisTestForm_MainStatusID');//检验单主状态
					//满足以下条件 :非核收样本(医嘱单ID为空)&&未检验确认样本MainStatusID=0&&检验单id和小组id不为空时弹出窗体,不满足条件不弹出
					if(!OrderFormID && MainStatusID==0 && TestFormID && SectionID){
						JShell.Win.open('Shell.class.lts.changeqc.Form',{
							//width:'95%',height:'95%',
							SectionID:SectionID,
							TestFormID: TestFormID,
							listeners: {
								close: function (p) {
									//true 的话刷新检验单选中行数据+项目结果
									if (p.isload) {
										me.fireEvent('loadRecord');
										me.fireEvent('loadResult');
									}
								}
							}
						}).show();
					}else{
						JShell.Msg.error('该检验单不满足"常规转质控"条件！');
					}
				}},
	    		{iconCls:'button-edit',text:'项目合并(糖耐量)',tooltip:'项目合并(糖耐量)',handler:function(but){
					JShell.Win.open('Shell.class.lts.itemmerge.App', {
						width: '95%', height: '95%',
						listeners: {
							close: function (p) {
								//true 的话刷新检验单选中行项目结果
								if (p.isloadResult) me.fireEvent('loadResult');

							}
						}
					}).show();
				}},
	    		{iconCls:'button-edit',text:'稀释样本结果',tooltip:'稀释样本结果',handler:function(but){
	    			var Grid = me.ownerCt.Grid,
	    				records = Grid.getSelectionModel().getSelection();
			
					if (records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					var TestFormID = records[0].get(Grid.PKField);
					if(!TestFormID){
						JShell.Msg.error('检验单的ID不存在！');
						return;
					}
					var StatusID = records[0].get("LisTestForm_MainStatusID");//状态
					if (StatusID != 0) {
						JShell.Msg.error('只有检验中的检验单可执行该操作！');
						return;
					}
					JShell.Win.open('Shell.class.lts.dilution.Grid',{
						//width:'95%',height:'95%',
						TestFormID: TestFormID,
						listeners: {
							close: function (p) {
								//true 的话刷新检验单选中行项目结果
								if (p.isloadResult) me.fireEvent('loadResult');
								
							}
						}
					}).show();
				}},
//	    		{iconCls:'button-edit',text:'<span style="color:red;">批量样本错位处理</span>',tooltip:'批量样本错位处理'},
	    		{iconCls:'button-edit',text:'合并检验单',tooltip:'合并检验单',handler:function(but){
					JShell.Win.open('Shell.class.lts.merge.App', {
						width: '95%', height: '95%', SectionID: me.sectionId,
						listeners: {
							close: function (p) {
								//true 的话刷新检验单列表
								if (p.isMerge) me.fireEvent('GridSearch');
							}
						}
					}).show();
				}},
	    		{iconCls:'button-del',text:'删除检验单',tooltip:'删除检验单',handler:function(but){me.onDelTestFormClick();}},
	    		{iconCls:'button-reset',text:'删除检验单恢复',tooltip:'删除检验单恢复',handler:function(but){me.unDelTestFormClick();}},
	    		{iconCls:'button-print',text:'<span style="color:red;">补打条码</span>',tooltip:'补打条码'},
	    		{iconCls:'button-edit',text:'打印检验清单',tooltip:'打印检验清单',handler:function(but){
	    //			var Grid = me.ownerCt.Grid,
	    //				records = Grid.getSelectionModel().getSelection();
			
					//if (records.length != 1) {
					//	JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					//	return;
					//}
					//var id = records[0].get('LisTestForm_LBSection_Id');
					//if(!id){
					//	JShell.Msg.error('检验单的小组ID不存在！');
					//	return;
					//}
					JShell.Win.open('Shell.class.lts.print.Grid', { width: '95%', height: '95%', SectionID: me.sectionId}).show();
				}},
				{iconCls:'button-list',text:'操作记录',tooltip:'操作记录',handler:function(but){
	    			var Grid = me.ownerCt.Grid,
	    				records = Grid.getSelectionModel().getSelection();
			
					if (records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					JShell.Win.open('Shell.class.lts.sample.operate.Grid',{
						testFormId:records[0].get('LisTestForm_Id')
					}).show();
				}}
//	    		{iconCls:'button-edit',text:'<span style="color:red;">核收到指定日期</span>',tooltip:'核收到指定日期'},
//	    		{iconCls:'button-edit',text:'<span style="color:red;">核收急查标本</span>',tooltip:'核收急查标本'},
//	    		{iconCls:'button-edit',text:'<span style="color:red;">追加医嘱(游离DNA)</span>',tooltip:'追加医嘱(游离DNA)'},
//	    		{iconCls:'button-edit',text:'<span style="color:red;">导出检验单</span>',tooltip:'导出检验单'},
//	    		{iconCls:'button-edit',text:'<span style="color:red;">保存修改年龄</span>',tooltip:'保存修改年龄'}
	    	]
	    },{
	    	text:'批量处理',code:'1003',
	    	iconCls:'button-edit',menu:[
				{
					iconCls: 'button-add', text: '批量新增检验单', tooltip: '批量新增检验单', handler: function (but) {
						var SectionID = me.sectionId;
						if (!SectionID) {
							var Grid = me.ownerCt.Grid,
								records = Grid.getSelectionModel().getSelection();

							if (records.length != 1) {
								JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
								return;
							}
							SectionID = records[0].get('LisTestForm_LBSection_Id');
							if (!SectionID) {
								JShell.Msg.error('检验单的小组ID不存在！');
								return;
							}
						}
						JShell.Win.open('Shell.class.lts.batchadd.Form', {
							//width:'95%',height:'95%',
							SectionID: SectionID,
							listeners: {
								close: function (p) {
									var Grid = me.ownerCt.Grid;
									Grid.onSearch();
								}
							}
						}).show();
					}
				},
				{
					iconCls: 'button-edit', text: '批量修改检验单', tooltip: '批量修改检验单', handler: function (but) {
						var SectionID = me.sectionId;
						if (!SectionID) {
							var Grid = me.ownerCt.Grid,
								records = Grid.getSelectionModel().getSelection();

							if (records.length != 1) {
								JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
								return;
							}
							var SectionID = records[0].get('LisTestForm_LBSection_Id');
							if (!SectionID) {
								JShell.Msg.error('检验单的小组ID不存在！');
								return;
							}
						}
						JShell.Win.open('Shell.class.lts.batchedit.App', {
							width: '95%', height: '95%',
							SectionID: SectionID,
							listeners: {
								close: function (p) {
									var Grid = me.ownerCt.Grid;
									Grid.onSearch();
								}
							}
						}).show();
					}
				},
	    		{iconCls:'button-accept',text:'批量检验确认(初审)-批量检验单审定',tooltip:'检验单批量检验确认', handler: function (but) {
						var SectionID = me.sectionId;
						if (!SectionID) {
							var Grid = me.ownerCt.Grid,
								records = Grid.getSelectionModel().getSelection();

							if (records.length != 1) {
								JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
								return;
							}
							SectionID = records[0].get('LisTestForm_LBSection_Id');
							if (!SectionID) {
								JShell.Msg.error('检验单的小组ID不存在！');
								return;
							}
						}
						JShell.Win.open('Shell.class.lts.batch.examine.confirm.App', {
							//width:'95%',height:'95%',
							SectionID: SectionID,
							listeners: {
								close: function (p) {
									var Grid = me.ownerCt.Grid;
									Grid.onSearch();
								}
							}
						}).show();
					}},
	    		{iconCls:'button-check',text:'检验单批量审核',tooltip:'检验单批量审核', handler: function (but) {
						var SectionID = me.sectionId;
						if (!SectionID) {
							var Grid = me.ownerCt.Grid,
								records = Grid.getSelectionModel().getSelection();

							if (records.length != 1) {
								JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
								return;
							}
							SectionID = records[0].get('LisTestForm_LBSection_Id');
							if (!SectionID) {
								JShell.Msg.error('检验单的小组ID不存在！');
								return;
							}
						}
						JShell.Win.open('Shell.class.lts.batch.examine.check.App', {
							//width:'95%',height:'95%',
							SectionID: SectionID,
							listeners: {
								close: function (p) {
									var Grid = me.ownerCt.Grid;
									Grid.onSearch();
								}
							}
						}).show();
					}},
	    		{iconCls:'file-excel',text:'<span style="color:red;">批量导出结果</span>',tooltip:'批量导出结果'},
	    		{iconCls:'button-print',text:'<span style="color:red;">批量打印报告</span>',tooltip:'批量打印报告'},
	    		{iconCls:'button-edit',text:'<span>批量样本号更改</span>',tooltip:'批量样本号更改', handler: function (but) {
					var SectionID = me.sectionId;
					if (!SectionID) {
						var Grid = me.ownerCt.Grid,
							records = Grid.getSelectionModel().getSelection();

						if (records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						var SectionID = records[0].get('LisTestForm_LBSection_Id');
						if (!SectionID) {
							JShell.Msg.error('检验单的小组ID不存在！');
							return;
						}
					}
					JShell.Win.open('Shell.class.lts.batch.editsampleno.Grid', {
						SectionID: SectionID,
						listeners: {
							close: function (p) {
								var Grid = me.ownerCt.Grid;
								Grid.onSearch();
							}
						}
					}).show();
				}},
				{iconCls:'button-edit',text:'<span>批量样本错位处理</span>',tooltip:'批量样本错位处理', handler: function (but) {
					var SectionID = me.sectionId;
					if (!SectionID) {
						var Grid = me.ownerCt.Grid,
							records = Grid.getSelectionModel().getSelection();

						if (records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						var SectionID = records[0].get('LisTestForm_LBSection_Id');
						if (!SectionID) {
							JShell.Msg.error('检验单的小组ID不存在！');
							return;
						}
					}
					JShell.Win.open('Shell.class.lts.batch.dislocation.App', {
						width: '95%', height: '95%',
						SectionID: SectionID,
						listeners: {
							close: function (p) {
								var Grid = me.ownerCt.Grid;
								Grid.onSearch();
							}
						}
					}).show();
				}}
	    	]
	    }];
	    
	},
	//初始化按钮
	initButtons:function(){
		var me = this;
		//获取角色列表
		me.getRoleList(function(roles){
			//获取角色操作权限列表
			me.getRoleOperList(roles,function(){
				//创建按钮
				me.creatButtons();
			});
		});
	},
	//创建按钮
	creatButtons:function(){
		var me = this,
			buttons = Ext.clone(me.ALL_BUTTONS_INFO),
			OperCodeListString = me.OPER_CODE_LIST.join(',') + ',';
		
		for(var i=0;i<buttons.length;i++){
			var node = buttons[i],
				type = JShell.typeOf(node);
				
			if(type == 'string') continue;
			
			if(type == 'object'){
				var code = node.code;
				
				if(code){//外层带编码就不判断内部下拉菜单编码，以外层为准
					var index = OperCodeListString.indexOf(code+',');
					//如果功能编码在角色操作权限中不存在，剔除功能按钮
					if(index == -1){
						buttons.splice(i,1);
						i--;
					}
				}else{//内部下拉菜单编码判断
					var menu = node.menu;
					if(menu){
						for(var j=0;j<menu.length;j++){
							var mCode = menu[j].code;
							if(mCode){
								var index = OperCodeListString.indexOf(mCode+',');
								//如果功能编码在角色操作权限中不存在，剔除功能按钮
								if(index == -1){
									menu.splice(j,1);
									j--;
								}
							}
						}
					}
				}
			}
		}
		
		me.add(buttons);
	},
	//获取角色列表
	getRoleList:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getRoleListUrl,
			where = 'lbright.RoleID is not null and lbright.EmpID=' + me.userId + ' and lbright.LBSection.Id=' + me.sectionId;
		url += '?fields=LBRight_RoleID&where=' + where;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//获取角色操作权限列表
	getRoleOperList:function(roles,callback){
		var me = this;
				
		if(!roles || roles.length == 0){
			me.OPER_CODE_LIST = [];
			callback();
			return;
		}
		
		var roleList = [];
		for(var i in roles){
			roleList.push(roles[i].RoleID);
		}
		var url = me.getRoleOperListUrl + '?isPlanish=true&fields=RBACRoleRight_RBACModuleOper_StandCode' + 
			'&where=rbacroleright.RBACRole.Id in (' + roleList.join(',') + ')';
		
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				me.OPER_CODE_LIST = [];
				for(var i in list){
					me.OPER_CODE_LIST.push(list[i].RBACRoleRight_RBACModuleOper_StandCode);
				}
				callback();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	
	//确认
	onConfirmClick:function(){
		var me = this;
		
		//判断操作是否可行（只允许单条数据）
		me.oprateIsValidOne(function(){
			var record = me.ownerCt.Grid.getSelectionModel().getSelection()[0],//选中行
				id = record.get('LisTestForm_Id'),//检验单id
				StatusID = record.get("LisTestForm_MainStatusID"),//状态
				FormInfoStatus = record.get('LisTestForm_FormInfoStatus'),//检验单信息基本完成状态
				url = JShell.System.Path.ROOT + me.onConfirmUrl;
			
			if (StatusID != 0) {
				JShell.Msg.warning('只有检验中数据可执行该操作！');
				return;
			}
			if (FormInfoStatus != 1) {
				JShell.Msg.warning('该检验单未完成，不允许执行该操作！');
				return;
			}
			var HandlerInfo = me.OperateData.getHandlerInfo();
			if(HandlerInfo.Id){
				me.showMask('确认中');//显示遮罩层
				JShell.Server.post(url,Ext.JSON.encode({
					testFormID:id,
					//empID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
					//empName:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
					empID:HandlerInfo.Id,
					empName:HandlerInfo.Name,
					memoInfo:''
				}),function(data){
					me.hideMask();//隐藏遮罩层
					if(data.success){
						JShell.Msg.alert('确认成功！',null,1000);
						me.ownerCt.Grid.onChangeRecodeDataById(id);
					}else{
						JShell.Msg.error(data.msg);
					}
				});
			}else{
				me.onConfirmConfigClick();
			}
		});
	},
	//取消确认
	unConfirmClick:function(){
		var me = this;
		
		//判断操作是否可行（只允许单条数据）
		me.oprateIsValidOne(function () {
			var record = me.ownerCt.Grid.getSelectionModel().getSelection()[0],//选中行
				id = record.get('LisTestForm_Id'),//检验单id
				StatusID = record.get("LisTestForm_MainStatusID"),//状态
				url= JShell.System.Path.ROOT + me.unConfirmUrl;

			if (StatusID != 2) {
				JShell.Msg.warning('只有检验确认数据可执行该操作！');
				return;
			}
			me.showMask('确认取消中');//显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode({
				testFormID:id,
				memoInfo:''
			}),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					JShell.Msg.alert('确认取消成功！',null,1000);
					me.ownerCt.Grid.onChangeRecodeDataById(id);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	//检验确认设置
	onConfirmConfigClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.lts.sample.operate.Tab',{
			SectionID:me.sectionId,
			listeners:{
				save:function(p){
					me.fireEvent('afterconfirmconfig',me);
					p.close();
				}
			}
		}).show();
	},
	//审核
	onCheckClick:function(){
		var me = this;
		
		//判断操作是否可行（只允许单条数据）
		me.oprateIsValidOne(function () {
			var record = me.ownerCt.Grid.getSelectionModel().getSelection()[0],//选中行
				id = record.get('LisTestForm_Id'),//检验单id
				StatusID = record.get("LisTestForm_MainStatusID"),//状态
				url= JShell.System.Path.ROOT + me.onCheckUrl;

			if (StatusID != 2) {
				JShell.Msg.warning('只有检验确认数据可执行该操作！');
				return;
			}
			var CheckerInfo = me.OperateData.getCheckerInfo();
			if(CheckerInfo.Id){
				me.showMask('审核中');//显示遮罩层
				JShell.Server.post(url,Ext.JSON.encode({
					testFormID:id,
					//empID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
					//empName:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
					empID:CheckerInfo.Id,
					empName:CheckerInfo.Name,
					memoInfo:''
				}),function(data){
					me.hideMask();//隐藏遮罩层
					if(data.success){
						JShell.Msg.alert('审核成功！',null,1000);
						me.ownerCt.Grid.onChangeRecodeDataById(id);
					}else{
						JShell.Msg.error(data.msg);
					}
				});
			}else{
				me.onCheckConfigClick();
			}
		});
	},
	//取消审核
	unCheckClick:function(){
		var me = this;
		
		//判断操作是否可行（只允许单条数据）
		me.oprateIsValidOne(function () {
			var record = me.ownerCt.Grid.getSelectionModel().getSelection()[0],//选中行
				id = record.get('LisTestForm_Id'),//检验单id
				StatusID = record.get("LisTestForm_MainStatusID"),//状态
				url= JShell.System.Path.ROOT + me.unCheckUrl;

			if (StatusID != 3) {
				JShell.Msg.warning('只有审核数据可执行该操作！');
				return;
			}
			me.showMask('取消审核中');//显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode({
				testFormID:id,
				memoInfo:''
			}),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					JShell.Msg.alert('取消审核成功！',null,1000);
					me.ownerCt.Grid.onChangeRecodeDataById(id);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	//检验审核设置
	onCheckConfigClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.lts.sample.operate.Tab',{
			SectionID:me.sectionId,
			activeTab:1,
			listeners:{
				save:function(p){
					me.fireEvent('aftercheckconfig',me);
					p.close();
				}
			}
		}).show();
	},
	//检验单删除
	onDelTestFormClick:function(){
		var me = this;
		
		//判断操作是否可行（只允许单条数据）
		me.oprateIsValidOne(function () {
			var record = me.ownerCt.Grid.getSelectionModel().getSelection()[0],//选中行
				id = record.get('LisTestForm_Id'),//检验单id
				StatusID = record.get("LisTestForm_MainStatusID"),//状态
				url= JShell.System.Path.ROOT + me.onDelTestFormUrl;

			if (StatusID != 0) {
				JShell.Msg.warning('只有检验中数据可执行该操作！');
				return;
			}
			me.onDelTestFormCall(url,id,0,0);
		});
	},
	//删除服务调用
	onDelTestFormCall: function (url, id, receiveDeleteFlag, resultDeleteFlag) {
		var me = this,
			url = url || "",
			id = id || null,
			receiveDeleteFlag = receiveDeleteFlag || 0,
			resultDeleteFlag = resultDeleteFlag || 0;

		if (!url || !id) return;

		me.showMask('数据删除中');//显示遮罩层
		JShell.Server.post(url, Ext.JSON.encode({
			delIDList: id, receiveDeleteFlag: receiveDeleteFlag, resultDeleteFlag: resultDeleteFlag
		}), function (data) {
			me.hideMask();//隐藏遮罩层
			if (data.success) {
				JShell.Msg.alert('检验单删除成功！', null, 1000);
				me.ownerCt.Grid.onChangeRecodeDataById(id);
			} else {
				if (data.Code == 9) {
					JShell.Msg.confirm(({ msg: data.ErrorInfo + "是否仍要执行该操作？" }), function (but) {
						if (but == "ok") {
							me.onDelTestFormCall(url, id, 1, 1);
						}
					});
				} else {
					JShell.Msg.error(data.msg);
				}
			}
		});
	},
	//检验单恢复
	unDelTestFormClick:function(){
		var me = this;
		
		//判断操作是否可行（只允许单条数据）
		me.oprateIsValidOne(function () {
			var record = me.ownerCt.Grid.getSelectionModel().getSelection()[0],//选中行
				id = record.get('LisTestForm_Id'),//检验单id
				StatusID = record.get("LisTestForm_MainStatusID"),//状态
				url= JShell.System.Path.ROOT + me.unDelTestFormUrl;

			if (StatusID != -2) {
				JShell.Msg.warning('只有已删除作废数据可执行该操作！');
				return;
			}
			me.showMask('数据恢复中');//显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode({
				testFormID:id,
				memoInfo:''
			}),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					JShell.Msg.alert('检验单恢复成功！',null,1000);
					me.ownerCt.Grid.onChangeRecodeDataById(id);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	//判断操作是否可行（只允许单条数据）
	oprateIsValidOne:function(callback,config){
		var me = this,
			Grid = me.ownerCt.Grid,
			records = Grid.getSelectionModel().getSelection();
	
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		JShell.Msg.confirm((config || {}),function(but){
			if(but == "ok"){
				callback();
			}
		});
	},
	
	//显示遮罩层
	showMask:function(value){
		this.ownerCt.body.mask(value);
	},
	//隐藏遮罩层
	hideMask:function(){
		this.ownerCt.body.unmask();
	}
});