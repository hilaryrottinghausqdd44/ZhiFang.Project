/**
 * 仪器维护数据
 * @author liangyl
 * @version 2016-08-26
 */
Ext.define('Shell.class.qms.equip.templet.emaintenancedata.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '操作列表',
	layout: 'border',
	width: 500,
	height: 400,
	hasbottomtoolbar: true,
	hasButtontoolbar: true,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:true,
    /**用于职责模板,'0', '全部','1', '人员模板','2', '人员岗位模板',对外公开设置默认值*/
    TEMPTLETTYPE:'',
	 /**列表新增,新增标识*/
	isTbAdd:0,
	/**列表新增,选择行查询需要用*/
	BatchNumber:null,
	/**模板ID*/
    TempletID: null,
    hideTimes:1000,
    /**当前页签数据*/
    NowTabData:[],
     /**是否TB类型*/
    IsTbType:'',
    /**当前页签类型*/
    NowTabType:'',
    /**当前页签项目名称*/
    NowTabItemText:'',	
     /**当前页签备注模板*/
    TempletMemo:'',
    /**当天数据*/
    TodayDataList:[],
     /**是否载入上一次数据*/
    isDaily:false,
     /**载入是否自动保存，0不自动,1自动保存载入数据据*/
    IsAutoSaveLoadData:'0',    
   	/**新增服务地址*/
	addUrl2: '/QMSReport.svc/QMS_UDTO_AddEMaintenanceData',
	/**一键保存新增服务地址*/
	addUrl: '/QMSReport.svc/QMS_UDTO_AddEMaintenanceDataAndResult',
	/**查询TB数据服务地址*/
    selectTBDataUrl:'/QMSReport.svc/QMS_UDTO_SearchMaintenanceDataTB',
    /**查询数据服务地址*/
    selectUrl:'/QMSReport.svc/ST_UDTO_SearchEMaintenanceDataByHQL?isPlanish=true',
    /**查询载入上次数据服务地址（定制）*/
    selectUrl2:'/QMSReport.svc/QMS_UDTO_SearchEMaintenanceData?isPlanish=true',
	/**获取参数数据服务路径*/
	selectParaUrl: '/QMSReport.svc/ST_UDTO_SearchEParameterByHQL?isPlanish=true',
	/**获取模板审核和数据状态*/
	selectTempletStateUrl:'/QMSReport.svc/QMS_UDTO_GetTempletState?isPlanish=true',
	hasLoadMask:true,
	/**获取所有需要保存的数据(通用类型)*/
	allSaveArr:[],
	/**当前页签itemId*/
	newBtnItemId:null,
	/**保存返回的列表数据*/
	newGridData:null,
	/**改变过的时间*/
	newChangeDateTime:null,
	/**质量记录登记页面保存按钮保存数据的范围,空或0为保存当前页签数据，1为保存全部页签数据*/
	IsSaveAllData:'0',
	/**当天数据加载后是否有数据*/
	IsLoadData:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//载入按钮禁用
		me.EditTabPanel.Form.changeDailyBtn(true);
		me.disableControl(); //禁用所有的操作功能
		me.onSetPara();
		me.EditTabPanel.on({
			onAddClick:function(grid){
				me.isTbAdd=0;
				me.isDaily=false;
				var operatedate = me.getOperateDate();
				me.EditTabPanel.Form.onResetClick();
				if(me.IsTbType!='TB'){
					me.MemoTabPanel.Form.onResetClick();
				}
				me.EditTabPanel.Form.changeDate(operatedate);
			},
			selectclick:function(record,grid,fields){
				me.showMask();
				me.isTbAdd=1;
				JShell.Action.delay(function() {
					var records = grid.getSelectionModel().getSelection();
					if(records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					me.getTBData(fields, record);
				}, null, 200);
				me.hideMask();
			},
			itemclick:function(record,grid,fields){
				me.showMask();
				me.isTbAdd=1;
				JShell.Action.delay(function() {
					var records = grid.getSelectionModel().getSelection();
					if(records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					me.getTBData(fields, record);
					me.EditTabPanel.Form.hideMask();
				}, null, 200);
				me.hideMask();
			},
			blur:function(com){
		    	if(!JcallShell.Date.isValid(com.getValue())) return;
		    	me.showMask();
		    	var operatedate = JcallShell.Date.toString(com.getValue(), true);
		    	com.setValue(operatedate);
		    	me.newChangeDateTime=operatedate;
	    	   for(var i =0 ;i<me.allSaveArr.length;i++){
                    if(me.allSaveArr[i].strDate!=operatedate ){
                        //时间改变后清空保存数据项
                        me.allSaveArr=[];
                    	continue; 
                    }
                }
		    	  
		    	JShell.Action.delay(function() {
					me.loadDataByBtn();
                }, null, 200);
 		    },
 		    onDailyClick:function(com){
		    	JShell.Action.delay(function() {
		    		me.showMask("正在载入数据...");
		    		if(me.IsTbType!='TB'){
		    			me.getloadDailyData();
		    		}else{
		    		    me.getTBloadDailyData();
		    		}
		    		me.hideMask();
                }, null, 200);
              
 		    },
   		    resize : function(com,  width,  height,  oldWidth,  oldHeight,  eOpts ){
			    me.setResizeBtn();
   		    },
   		    nodata:function(form){
   		    	var num=0,arr=[];
   		    	var stratDate=me.getOperateDate();
   		    	me.EditTabPanel.Form.onResetClick();
				me.MemoTabPanel.Form.onResetClick();
				//组件表单工具栏上的时间
	            me.EditTabPanel.Form.changeDate(stratDate);
	            me.fireEvent('loadSaveDataClick', []);		
   		    },
   		    onDelClick:function(){
   		    	me.fireEvent('onDelClick');	
   		    },
   		    loadsearch:function(list){
   		    	me.IsLoadData=false;
   		        if(list>0){
   		        	me.IsLoadData=true;
   		        }
   		    }
		});
		me.MemoTabPanel.on({
			uploadClick:function(com){
				var stratDate=me.getOperateDate();
                com.showForm(stratDate);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**根据参数返回是否自动载入保存数据*/
	onSetPara:function(){
		var me =this;
		var ParaValue='0';
		me.getEParaVal(function(data){
			if(data){
				var len =data.list.length;
				for(var i=0;i<len;i++){
					var ParaNo=data.list[i].EParameter_ParaNo;
					var ParaValue=data.list[i].EParameter_ParaValue;
					switch (ParaNo){
						case 'IsAutoSaveLoadData':
						    //返回是否自动载入保存数据
							if(ParaValue=='1'){
								me.IsAutoSaveLoadData='1';
							}else{
								me.IsAutoSaveLoadData='0';
							}
							break;
						case 'IsSaveAllData':
						    //质量记录登记页面保存按钮保存数据的范围
							if(ParaValue=='1'){
								me.IsSaveAllData='1';
							}else{
								me.IsSaveAllData='0';
							}
							break;
						default:
							break;
					}
				}
			}
		});
	},
	/**加载数据（非TB）*/
	onDailyLoadData:function(DailyDateStr){
		var me =this;
		me.showMask();
		var list = me.getListData(me.getInternalWhere(DailyDateStr, me.NowTabType, me.TempletID),DailyDateStr);
		if(list.length==0){
			JShell.Msg.alert('上次数据为空,不做数据载入', null, 2000);
			me.hideMask();
			return;
		}
		me.EditTabPanel.Form.clearResultData();
		me.EditTabPanel.Form.setDailyData(list);
		me.onLoadDataSetMemo(list);
		me.onMemoTabPanel();
        me.hideMask();
        me.isDaily=false;
	},
	/**加载数据（非TB）,需自动保存*/
	onDailyLoadDataSave:function(DailyDateStr,arrList){
		var me =this;
		me.showMask();
		me.EditTabPanel.Form.clearResultData();
		var list = me.getListData(me.getInternalWhere(DailyDateStr, me.NowTabType, me.TempletID));		
		//上一次数据为空
		if(list.length==0){
			me.MemoTabPanel.Form.onSetResult('');
			me.AutoSaveData();
		}else{
			me.EditTabPanel.Form.setDailyData(list);
			me.onLoadDataSetMemo(list);
			me.onMemoTabPanel();
			me.AutoSaveData();
		}
        me.hideMask();
        me.isDaily=false;
	},
	
	/**加载数据（TB）*/
	onDailyTBLoadData:function(DailyDateStr,list){
		var me =this;
		me.TodayDataList=[];
		me.EditTabPanel.Form.clearResultData();
		me.onSeachTb(DailyDateStr);
		me.onMemoTabPanel();
	},
	onSaveClick: function() {
		var me = this;
		var me = this;
		me.isDaily=false;
		me.saveData();
//		//列表类型，按原来的保存方式
//		if(me.IsTbType=='TB' || me.IsSaveAllData=='0'){
//			me.saveData();
//		}else{
//			//一键保存
//			if(me.IsSaveAllData=='1'){
//					//获取当前页签数据
//				var NewTagDataList = me.getInitialValue();
//				//一键保存
//				var operatedate = me.getOperateDate();
//				var list = me.allSaveArr,
//				    len = list.length;
//				var entityList=[],arr=[];
//				for(var i = 0; i<len; i++){
//					var arr=list[i].objArr;
//					if(list[i].comitemId==me.newBtnItemId){
//						arr=NewTagDataList;
//					}
//	                entityList = entityList.concat(arr);
//				}
//				//如果数据为空，只取当前页签数据
//				if(entityList.length==0){
//					entityList=NewTagDataList;
//				}
//				me.onAddSaveInfo(operatedate,entityList);
//			}
//		}
	},
	onRefreshClick: function() {
		var me = this;
		if(me.IsTbType =='TB'){				
		    me.EditTabPanel.OperatePanel.load();
		}else{
//			me.EditTabPanel.Form.onResetClick();
//			me.MemoTabPanel.Form.onResetClick();
			var operatedate = me.getOperateDate();
			me.loadData(operatedate);
			var listSaveArr =[];
			me.allSaveArr= me.getAllSaveInfo(listSaveArr);
		}	
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
	    items.push(me.createDockButtontoolbar());
		return items;
	},
	/**创建功能按钮栏*/
	createDockButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			items.push('->', {
				type: 'button',
				iconCls: 'button-show hand',
				text: '预览',
				handler: function(grid, rowIndex, colIndex) {
					me.fireEvent('onShowClick', me);
				}
			}, 'refresh', 'save');
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottombuttonsToolbar',
			items: items
		});
	},
	/**创建顶部功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items =  [];
			var list=[];
		if(items.length == 0) {
			items.push('->',  'refresh');
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			margin:0,
			bodyPadding:0,
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	createItems: function() {
		var me = this;
		var items = [];
		me.Btn = Ext.create('Shell.class.qms.equip.templet.emaintenancedata.Btn', {
			border: false,
			itemId: 'buttonsToolbar2',
			region: 'north',
			height:26
		});
		me.EditTabPanel = Ext.create('Shell.class.qms.equip.templet.emaintenancedata.EditTabPanel', {
			border: false,
			title: '操作列表',
			region: 'center',
			header: false,
			TempletID: me.TempletID,
			itemId: 'EditTabPanel',
			ISEDITDATE:me.ISEDITDATE,
			TEMPTLETTYPE:me.TEMPTLETTYPE
		});
		me.MemoTabPanel = Ext.create('Shell.class.qms.equip.templet.emaintenancedata.MemoTabPanel', {
			title: '备注和附件',
			region: 'south',
			height: 140,
			header: false,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			TempletID: me.TempletID,
			itemId: 'MemoTabPanel'
		});
		return [me.Btn,me.EditTabPanel, me.MemoTabPanel];
	},
	
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(false);
	},
	
	/**根据日期，类型，模板id查询*/
	getTBWhere: function(BatchNumber, TempletID,operatedate) {
		var me = this,
			where = '',
			params = [];
		if(TempletID) {
			params.push("emaintenancedata.ETemplet.Id=" + TempletID);
		}
		if(BatchNumber) {
			params.push("emaintenancedata.BatchNumber='" + BatchNumber + "'");
		}
		if(operatedate) {
			params.push("emaintenancedata.ItemDate='" + operatedate + "'");
		}
		if(me.NowTabType) {
			params.push("emaintenancedata.TempletTypeCode='" + me.NowTabType + "'");
		}
		params.push("emaintenancedata.TempletDataType=2");
		if(params.length > 0) {
			where = params.join(' and ');
		} else {
			where = '';
		}
		return where;
	},
	/**根据日期，类型，模板id查询*/
	getTBWhere2: function(BatchNumber, TempletID,operatedate) {
		var me = this,
			where = '',
			params = [];
		if(TempletID) {
			params.push("emaintenancedata.ETemplet.Id=" + TempletID);
		}
		if(me.NowTabType) {
			params.push("emaintenancedata.TempletTypeCode='" + me.NowTabType + "'");
		}
		
		if(operatedate) {
			params.push("emaintenancedata.ItemDate='" + operatedate + "'");
		}
		if(BatchNumber) {
			params.push("emaintenancedata.BatchNumber='" + BatchNumber + "' or (emaintenancedata.TempletDataType=1 and emaintenancedata.ETemplet.Id=" + TempletID+
		    " and emaintenancedata.TempletTypeCode='"+ me.NowTabType +"' and emaintenancedata.ItemDate='" + operatedate + "')");
		}
		if(params.length > 0) {
			where = params.join(' and ');
		} else {
			where = '';
		}
		return where;
	},
	/**页备注项查询条件*/
	getTBMemoWhere: function(TempletID,operatedate) {
		var me = this,
			where = '',
			params = [];
		if(TempletID) {
			params.push("emaintenancedata.ETemplet.Id=" + TempletID);
		}
		if(operatedate) {
			params.push("emaintenancedata.ItemDate='" + operatedate + "'");
		}
		params.push("emaintenancedata.TempletTypeCode='"+me.NowTabType+"'");
		params.push("emaintenancedata.TempletDataType=1");
		if(params.length > 0) {
			where = params.join(' and ');
		} else {
			where = '';
		}
		return where;
	},
	/**根据日期，类型，模板id查询*/
	getInternalWhere: function(operatedate, TempletTypeCode, TempletID) {
		var me = this,
			where = '',
			params = [];
		var where = '&templetID='+TempletID + 
		'&itemDate='+operatedate+'&typeCode='+TempletTypeCode;
		
		return where;
	},
    /**
	 * 保存数据和备注
	 */
	saveData: function() {
		var me = this,
			entityList=[];
		if(!me.EditTabPanel.Form.getForm().isValid()){
			JShell.Msg.error('存在格式不正确的值,请检查');
			return;
		} 
		var operatedate = me.getOperateDate();
		//获取备注信息，冗余填充到各个项目
		var ItemMemoVal=me.MemoTabPanel.Form.getMemoInfo();
	    entityList=me.EditTabPanel.Form.SaveMaintenanceData(me.NowTabData, me.TempletID,me.NowTabType,operatedate,ItemMemoVal,me.isTbAdd,me.BatchNumber);
		//一个页签只能有一个备注，校验, 存在的只能修改
		if(me.IsTbType=='TB'){
			me.getIsItemMemo(function(data){
				if(data && data.value){
					if(data.value.list.length>0){
						//赋值id
						var id=data.value.list[0].EMaintenanceData_Id;
						me.MemoTabPanel.Form.onSetIdVal(id);
					}
				}
			});
		}
	    var entity =me.MemoTabPanel.Form.SaveFormData(me.TempletID, me.NowTabType,operatedate,me.isTbAdd,me.BatchNumber);
	    entityList.push(entity);
	  	me.onSaveInfo(operatedate,entityList);
	},
	/**初始化保存*/
    AutoSaveData: function() {
		var me = this,
			entityList=[];
		if(!me.EditTabPanel.Form.getForm().isValid()){
			JShell.Msg.error('存在格式不正确的值,请检查');
			return;
		} 
		var operatedate = me.getOperateDate();
		//获取备注信息，冗余填充到各个项目
		var ItemMemoVal=me.MemoTabPanel.Form.getMemoInfo();
	    entityList=	me.EditTabPanel.Form.SaveMaintenanceData(me.NowTabData, me.TempletID,me.NowTabType,operatedate,ItemMemoVal,me.isTbAdd,me.BatchNumber);
	   //一个页签只能有一个备注，校验, 存在的只能修改
		if(me.IsTbType=='TB'){
			me.getIsItemMemo(function(data){
				if(data && data.value){
					if(data.value.list.length>0){
						//赋值id
						var id=data.value.list[0].EMaintenanceData_Id;
						me.MemoTabPanel.Form.onSetIdVal(id);
					}
				}
			});
		}
	    var entity =me.MemoTabPanel.Form.SaveFormData(me.TempletID, me.NowTabType,operatedate,me.isTbAdd,me.BatchNumber);
	    entityList.push(entity);
	  	me.onSaveInfo(operatedate,entityList,'1');
	},
	 /**保存
     * isLoadSave 是否初始化保存 ,1初始化保存
     * */
	onSaveInfo:function(curDateStr,entityList,isLoadSave){
		var me = this,
		url = JShell.System.Path.getRootUrl(me.addUrl2);
		var params = {
			entityList: entityList,
			templetID:me.TempletID,
			itemDate:curDateStr,
			typeCode:me.NowTabType
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(isLoadSave && isLoadSave=='1'){
					//初始化保存不需要提示
					me.fireEvent('save', me);
				}else{
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				    me.fireEvent('save', me);
				}
				if(me.IsTbType=='TB'){
			    	me.showMask();
                    var operatedate = me.getOperateDate();
					me.loadDataByBtn();
					
				}else{
                    var operatedate = me.getOperateDate();
					me.loadData(operatedate);
				}
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},
    /**一键保存
     * isLoadSave 是否初始化保存 ,1初始化保存
     * */
	onAddSaveInfo:function(curDateStr,entityList,isLoadSave){
		var me = this,
		url = JShell.System.Path.getRootUrl(me.addUrl);
		me.newGridData=[];
		var fields ='EMaintenanceData_Id,EMaintenanceData_TempletDataType,EMaintenanceData_ItemResult,EMaintenanceData_TempletTypeCode,EMaintenanceData_TempletItemCode,EMaintenanceData_ItemDataType';
		var params = {
			entityList: entityList,
			templetID:me.TempletID,
			itemDate:curDateStr,
			fields:fields,
			isPlanish:true
		};
		
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(isLoadSave && isLoadSave=='1'){
					//初始化保存不需要提示
					me.fireEvent('save', me);
				}else{
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				    me.fireEvent('save', me);
				}
				if(me.IsTbType=='TB'){
			    	me.showMask();
                    var operatedate = me.getOperateDate();
					me.loadDataByBtn();
					
				}else{
					me.newGridData=data;
					me.onChangeResult2(me.newGridData);
				}
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},	
	
   
	/**保存
     * isLoadSave 是否初始化保存 ,1初始化保存
    * */
	onSaveTBInfo:function(curDateStr,entityList){
		var me = this,
		url = JShell.System.Path.getRootUrl(me.addUrl);
		var params = {
			entityList: entityList,
			templetID:me.TempletID,
			itemDate:curDateStr,
			typeCode:me.NowTabType
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){
					if(me.IsTbType=='TB'){
				    	me.showMask();
	                    var operatedate = me.getOperateDate();
						me.loadDataByBtn();
						
					}else{
	                    var operatedate = me.getOperateDate();
						me.loadData(operatedate);
					}
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	},
	/**
	 * 动态创建表单组件
	 */
	createMaintenanceDataForm: function() {
		var me = this,list = [];
		var DataType = Ext.typeOf(me.NowTabData);
		var ItemMemo=[];
		ItemMemo.push(me.TempletMemo);
		var ItemDataType = 'C',
			DefaultValue = '',
			MinValue = 0,
			MaxDataLength = 500,
			MaxValue = 0,
			allowDecimals = true,
			DecimalLength = 0,
			ItemValueList = null,IsSpreadItemList=null;
		if(me.IsTbType=='TB'){
			me.EditTabPanel.OperatePanel.show();
		}else{
			me.EditTabPanel.OperatePanel.hide();
		}
		if(DataType == "array") {
			me.EditTabPanel.Form.removeAll();
			Ext.Array.forEach(me.NowTabData, function(model, index, array) {
				ItemDataType = 'C';
				DefaultValue = '';
				MinValue = '';
				MaxDataLength = 500;
				MaxValue = '';
				allowDecimals = true;
				DecimalLength = 20;
				ItemValueList = null;IsSpreadItemList=null;
				
				//判断是已设置效期的组件
		        var InitItemCode = model.InitItemCode;
		        var ItemDataType = model.ItemDataType;
		        if(InitItemCode && ItemDataType=='D'){
		             var indexdatestr = InitItemCode.lastIndexOf("\|");  
					InitItemCode  = InitItemCode.substring(indexdatestr + 1, InitItemCode.length);
		        }
				var text = model.text;
				var textLength=Number(model.textLength)*14;
				if(textLength >400 && me.IsTbType!='TB'){
					textLength=400;
				}
				var maxtextWidth=300;
				if(me.IsTbType=='TB'){
					if(textLength>160){
						textLength=160;
					}
					maxtextWidth=200;
				}
				var TempletItem = me.EditTabPanel.Form.createTempletItem(text, index,textLength,maxtextWidth);
				var No = me.EditTabPanel.Form.createNo(' ', index);
				var ItemCode = me.EditTabPanel.Form.createTempletItemCode(model.ItemCode,index);
				list.push(No, TempletItem,ItemCode);
				var OperateDate= me.getOperateDate();
				list = me.EditTabPanel.Form.createFormCom(list, index, model,me.NowTabType,me.TempletID,OperateDate,InitItemCode);
			});
		}
		var TempletMemoItemCode='',TempletMemoPosition='B';
		if(me.TempletMemo.ItemCode){
			TempletMemoItemCode=me.TempletMemo.ItemCode.toUpperCase();
			TempletMemoPosition=me.TempletMemo.TempletMemoPosition.toUpperCase();
		}
		//位置上(顶部)
		if(TempletMemoItemCode=='ET' && TempletMemoPosition=='E'){
			var memoarr=me.EditTabPanel.Form.createTempletMemo(me.TempletMemo);
			me.EditTabPanel.Form.add(memoarr);
		}
		me.EditTabPanel.Form.add(list);
		
		//模板备注
		if(TempletMemoItemCode=='ET' && TempletMemoPosition!='E'){
			var memoarr=me.EditTabPanel.Form.createTempletMemo(me.TempletMemo);
			me.EditTabPanel.Form.add(memoarr);
		}
	},
	/**清空备份信息*/
	clearMemoData: function() {
		var me = this;
		var obj = {
			Id: '',
			ItemMemo: ''
		};
		me.MemoTabPanel.Form.getItemMemo(obj);
	},
	/**还原TB数据（编辑)*/
	getEditTbData:function(fields,records){
		var me=this,list=[];	
		me.TodayDataList=[];
		var operationDate = records.get('操作日期');
		var operatedate = JShell.Date.toString(operationDate,true);
		var startdate = me.getOperateDate();
		//操作时间跟当前时间不一致时，加载的数据为前一天的数据
		if(startdate!=operatedate){
			me.isDaily=true;
		}else{
			me.isDaily=false;
		}
		var BatchNumber=records.get('BatchNumber');
		me.BatchNumber=BatchNumber;
		//还原项目，不包含页备注
		list = me.getMaintenanceData(me.getTBWhere2(BatchNumber, me.TempletID,operatedate));
        if(me.isDaily){
        	me.BatchNumber=null;
		    me.EditTabPanel.Form.setDailyData(list);
		    me.onSetItemMemo(list);
			me.isDaily=false;
        }else{
        	var i = 0,Type = 'C',IsSpreadItemList=null,IsMultiSelect=null;
			Ext.Array.each(me.NowTabData, function(rec) {
				IsSpreadItemList=null;IsMultiSelect=null;
				//判断是已设置效期的组件
		        var InitItemCode = rec['InitItemCode'];
				text = rec['text'];
				if(rec['ItemDataType']) {
					Type = rec['ItemDataType'];
				}
				ItemCode = rec['ItemCode'];
				if(rec['IsSpreadItemList']){
					IsSpreadItemList= rec['IsSpreadItemList'];
				}
				if(rec['IsMultiSelect']){
					IsMultiSelect= rec['IsMultiSelect'];
				}
				if(InitItemCode && Type=='D'){
		             var indexdatestr = InitItemCode.lastIndexOf("\|");  
					InitItemCode  = InitItemCode.substring(indexdatestr + 1, InitItemCode.length);
		        }
				me.EditTabPanel.Form.SetFormData(list, ItemCode, Type, i,IsSpreadItemList,IsMultiSelect,false,InitItemCode);
				i = i + 1;
			});
			me.onSetItemMemo(list);
        }
        me.fireEvent('loadSaveDataClick', list);
	},
	/**还原表单*/
	createItemData:function(stratDate){
		var me=this;
		//动态生成表单
		me.createMaintenanceDataForm();
		//还原数据
		me.getOperateData(stratDate);
		
		me.EditTabPanel.Form.changeDailyBtn(false);
	},
	/**还原数据,页面所有数据*/
	getOperateData: function(operatedate) {
		var me = this,
			defaultWhere = null,
			list = [];
		if(me.IsTbType!='TB'){
			me.clearMemoData();
			list = me.getMaintenanceData(me.getInternalWhere(operatedate, me.NowTabType, me.TempletID));
			if(list.length>0){
				var i = 0,IsSpreadItemList=null,IsMultiSelect=null,
					Type = 'C',InitItemCode='';
				Ext.Array.each(me.NowTabData, function(rec) {
					IsSpreadItemList=null,IsMultiSelect=null;
					InitItemCode = rec['InitItemCode'];
					text = rec['text'];
					if(rec['ItemDataType']) {
						Type = rec['ItemDataType'];
					}
					ItemCode = rec['ItemCode'];
					if(rec['IsSpreadItemList']){
						IsSpreadItemList= rec['IsSpreadItemList'];
					}
					if(rec['IsMultiSelect']){
						IsMultiSelect= rec['IsMultiSelect'];
					}
	                //判断是已设置效期的组件
			        if(InitItemCode && Type=='D'){
			        	var indexdatestr = InitItemCode.lastIndexOf("\|");  
						InitItemCode  = InitItemCode.substring(indexdatestr + 1, InitItemCode.length);
			        }
					me.EditTabPanel.Form.SetFormData(list, ItemCode, Type, i,IsSpreadItemList,IsMultiSelect,false,InitItemCode);
					i = i + 1;
				});
				me.onSetItemMemo(list);
				me.IsLoadData=true;
			    me.fireEvent('loadSaveDataClick', list);
			}else{
				me.clearMemoData();
				me.fireEvent('loadSaveDataClick', []);
			}
		}
	},
	/**查询仪器维护数据表数据*/
	getMaintenanceData: function(defaultWhere) {
		var me = this,
			list = [];
		if(me.IsTbType!='TB'){
			var url = JShell.System.Path.getRootUrl(me.EditTabPanel.Form.selectUrl2);
		}else{
			var url = JShell.System.Path.getRootUrl(me.EditTabPanel.Form.selectUrl);
		}
		var fields = "EMaintenanceData_Id,EMaintenanceData_TempletItem,EMaintenanceData_TempletDataType," +
			"EMaintenanceData_ItemResult,EMaintenanceData_TempletTypeCode,EMaintenanceData_TempletItemCode,EMaintenanceData_ItemDataType,EMaintenanceData_ItemMemo,EMaintenanceData_TempletItemCode";
		url += '&fields=' + fields;
		
		if(me.IsTbType!='TB'){
			url += defaultWhere;
		}else{
			url += '&where=' + defaultWhere;
		}
		
		me.TodayDataList=[];
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var result = Ext.isArray(data.value.list); //为数组时才处理
				if(result) {
					me.TodayDataList=data.value.list;
					Ext.Array.each(data.value.list, function(model) {
						list.push(model);
						if(me.IsTbType!='TB'){
							var TempletDataType = model.EMaintenanceData_TempletDataType;
							if(TempletDataType == '1') {
								var reg = new RegExp("</br>", "g");
			                    ItemMemo = model.EMaintenanceData_ItemMemo.replace(reg, "\r\n");
								var obj = {
									ItemMemo: ItemMemo
								};
								if(!me.isDaily){
									obj.Id=model.EMaintenanceData_Id;
								}
								me.MemoTabPanel.Form.getItemMemo(obj);
							}
						}
					});
				}
				me.fireEvent('loadSaveDataClick',list);
			}
		}, false);
		return list;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(); //启用所有的操作功能
	},
	/**清空表单信息*/
	clearData:function(){
		var me=this;
		//清空表单组件
		me.EditTabPanel.Form.removeAll();
		//清空备注信息
		me.MemoTabPanel.Form.clearData();
		//TB类型  清空tb列表
		if(me.IsTbType==1 || me.IsTbType=='1' ){
			me.EditTabPanel.OperatePanel.clearData();
		}
	},
	/**TB类型查询*/
	onSeachTb:function(Operatedate){
		var me=this;
		me.clearMemoData();
		me.EditTabPanel.OperatePanel.TempletID=me.TempletID;
		me.EditTabPanel.OperatePanel.Operatedate=Operatedate;
		me.EditTabPanel.OperatePanel.typeCode=me.NowTabType;
		me.EditTabPanel.OperatePanel.onSearch(me.TempletID,Operatedate,me.NowTabType);
	},
	/**MemoTabPanel赋值*/
	onMemoTabPanel:function(){
		var me=this;
		me.MemoTabPanel.PK = me.TempletID;
		me.MemoTabPanel.TempletType = me.NowTabItemText;
		me.MemoTabPanel.TempletTypeCode =  me.NowTabType;
		me.MemoTabPanel.IsAttachmentLoad = false;
		me.MemoTabPanel.setActiveTab(me.MemoTabPanel.Form);
	},
	/**
	 * 还原数据
	 */
	ItemOperateData: function(stratDate) {
		var me = this;
        //还原表单和数据(obj,type,templetId,stratDate){
	    me.createItemData(stratDate);
	    me.EditTabPanel.Form.changeDate(stratDate);
	},
	
	//获取时间
	getOperateDate:function(){
		var me=this;
			//组件表单工具栏上的时间
		var EMaintenanceDataDate = me.EditTabPanel.Form.getComponent('buttonsToolbar').getComponent('EMaintenanceData_Date');
		
		var Sysdate = JcallShell.System.Date.getDate();
//		var Sysdate ="2018-08-16";		
		var operatedate = JcallShell.Date.toString(Sysdate, true);
        //时间组件可编辑时取组件当前时间
		if(me.ISEDITDATE =='false'){
			//组件表单工具栏上的日期
		    operatedate = JcallShell.Date.toString(EMaintenanceDataDate.getValue(), true);
		}
		return operatedate;
	},
	 /**还原TB数据*/
	getTBData:function(fields, records){
		var me=this;
		me.showMask();
		//还原数据
		me.getEditTbData(fields, records);
		me.hideMask();
	},
	/**
	 * 重新加载
	 */
	loadData: function(operatedate) {
		var me = this;
		me.getOperateData(operatedate, me.NowTabData, me.TempletID, me.NowTabType);
	},
	/**根据模板id生成页签*/
	getTempletById:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/QMSReport.svc/ST_UDTO_SearchETempletByHQL?isPlanish=true';
		url += '&fields=ETemplet_Id,ETemplet_TempletStruct&where=etemplet.Id='+id;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	changeTab:function(id,CheckType){
		var me=this;
		me.IsTbType='';
		me.TempletID=id;
        var items=[];
		var TempletStruct='',TreeData='',btnArr=[];	
		var buttonsToolbar = me.getComponent('buttonsToolbar2').getComponent('buttonsToolbar');
        if(buttonsToolbar)buttonsToolbar.removeAll();
        me.showMask();
        me.clearData();
        var numcount=0;
        me.allSaveArr=[];
        //生成按钮
		me.getTempletById(id,function(data){
			if(data && data.value){
				var list=[];
				me.newGridData=[];
				me.IsTbType='';
				me.IsLoadData=false;
				me.createTab(buttonsToolbar,data.value.list,numcount,CheckType);
			}
		});
	},
	/**创建按钮页签*/
	createTab:function(buttonsToolbar,arr,numcount,CheckType){
		var me =this;
		var memoObj={ItemCode:''},list=[];
		var templettype='MD';
		var listSaveArr=[];
		for(var j =0;j<arr.length;j++){
			list=arr[j];
		    if(list.ETemplet_TempletStruct) {
				TempletStruct = Ext.JSON.decode(list.ETemplet_TempletStruct);
				TreeData = TempletStruct.Tree;
				for(var i = 0; i < TreeData.length; i++) {
					operateData = TreeData[i].Tree;
					templettype=TreeData[i].tid;
					//备注模板
					if(templettype.toUpperCase()=='ET' && !operateData.Para){
						memoObj.TempletMemoTitle='';
						memoObj.ItemCode=templettype.toUpperCase();
						memoObj.TempletMemo=TreeData[i].text;
						memoObj.TempletMemoPosition='r';
						memoObj.InitItemCode=templettype.toUpperCase();
						memoObj.ItemName=TreeData[i].text;
					}
					if(operateData.length==0 && TreeData.length>0){
						if(TreeData[i].Para){
							memoObj.InitItemCode=TreeData[i].Para.InitItemCode;
							memoObj.ItemCode=TreeData[i].Para.ItemCode;
							memoObj.ItemName=TreeData[i].Para.ItemName;
							memoObj.TempletMemo=TreeData[i].Para.TempletMemo;
							memoObj.TempletMemoPosition=TreeData[i].Para.TempletMemoPosition;
							memoObj.TempletMemoTitle=TreeData[i].Para.TempletMemoTitle;
						}
					}
					var OperateListData = [];
					var itemid='btnTab'+me.TempletID+i;
					OperateListData = me.getData(operateData, TreeData[i].text);
					var NowTabData=OperateListData;
					/**当前页签类型*/
				    var NowTabType=TreeData[i].tid;
				    me.IsTbType='';
				    //当前是列表类型时,使用TB区分
				    if(NowTabType.length == 2 && NowTabType.substr(0, 1).toUpperCase() == "T"){
				    	me.IsTbType='TB';
				    	me.NowTabType=NowTabType;
				    }
				    /**当前页签项目名称*/
				    var NowTabItemText=TreeData[i].text;
				    var Text=NowTabItemText.replace(/[\\r\\n\\s]/g, '');
					numcount+=me.getNum(Text);
					var stratDate=me.getOperateDate();

					var btn={
						xtype:'button',
						text:Text,
						tooltip:'',
						enableToggle:false,
						itemId:itemid,
						/**当前页签数据*/
					    NowTabData:NowTabData,
					    /**当前页签类型*/
					    NowTabType:NowTabType,
					    IsTbType:me.IsTbType,
					    /**当前页签项目名称*/
					    NowTabItemText:Text,
					    TempletMemo:memoObj,
					    stratDate:stratDate
			        };
			        //TM 原有备注规则,
			        if(templettype.toUpperCase()!='ET' && templettype.toUpperCase()!='TM'){
			        	buttonsToolbar.add(btn,'-');
			        }
			    }
			}
		}
		//判断是否需要滚动条
		var width=numcount*16;
		var maxWidth = me.EditTabPanel.getWidth();
		me.Btn.TxtLen=width;
		var BtnLeft=me.Btn.getComponent('BtnLeft');
		var BtnRight=me.Btn.getComponent('BtnRight');
		
        if(width>maxWidth-50){
			BtnLeft.setVisible(true);
			BtnRight.setVisible(true);
		}else{
			BtnLeft.setVisible(false);
			BtnRight.setVisible(false);
		}
		
		for(var i = 0; i < buttonsToolbar.items.length; i++) {
		    //'-' 不处理
			if(buttonsToolbar.items.items[i].itemId){
				buttonsToolbar.items.items[i].on({
					click:function(com, e,eOpts ){
						me.IsTbType='';
						me.cleartogglebuttonsToolbar(buttonsToolbar,com);
						com.toggle(true);
					},
					toggle: function (com, pressed, opts) {
						var isLoad=false;
			            if (pressed) {
			            	me.IsTbType=com.IsTbType;
			            	/**当前页签数据*/
						    me.NowTabData=com.NowTabData;
						    /**当前页签类型*/
						    me.NowTabType=com.NowTabType;
						    /**当前页签项目名称*/
						    me.NowTabItemText=com.NowTabItemText;
						    /**当前页备注模板*/
						    me.TempletMemo=com.TempletMemo;
						    me.newBtnItemId=com.itemId;
//						    if(com.IsTbType!='TB'){
//						    	//取当前页签数据
//							    if(me.allSaveArr.length==0){
//							    	me.loadDataByBtn(CheckType);
//							    	me.allSaveArr = me.getAllSaveInfo(listSaveArr,com);
//							    }
//							    var stratDate=me.getOperateDate();
//	                            
//							    for(var i =0 ;i<me.allSaveArr.length;i++){
//		    	                    if(me.allSaveArr[i].comitemId==com.itemId && me.allSaveArr[i].strDate==stratDate ){
//		    	                    	//已加载的数据不需要重新加载
//		    	                    	me.createItemLoadData(com.itemId);
//		    	                    	me.onChangeResult2(me.newGridData);
//		    	                    	isLoad=true;
//		    	                    	break; 
//		    	                    }
//		    	                }
//							    if(!isLoad){
//							    	me.loadDataByBtn(CheckType);
//		    	                    me.allSaveArr= me.getAllSaveInfo(listSaveArr,com);
//							    }
//						    }else{
//						    	me.loadDataByBtn(CheckType);
//						    }
						    me.loadDataByBtn(CheckType);
			            	me.hideMask();
			            }
//			            if (!pressed && !com.IsTbType){//页签切换前的数据（上一页签)
//			            	me.allSaveArr= me.getAllSaveInfo(listSaveArr,com);
//			            }
			        }
				});
				//默认选中第一个
				if(i==0){
					buttonsToolbar.items.items[i].toggle(true);
				}else{
					buttonsToolbar.items.items[i].toggle(false);
				}
			}
		}
	},
	/**根据选中按钮加载*/
	loadDataByBtn:function(CheckType){
		var me=this;
		me.clearData();
		me.IsLoadData=false;
		me.MemoTabPanel.Form.isEdit();
		var stratDate=me.getOperateDate();
		me.setDateValue(CheckType,stratDate);
		//还原数据
		me.ItemOperateData(stratDate);
		me.onMemoTabPanel();
        //tb类型
		if(me.IsTbType=='TB'){
			me.onSeachTb(stratDate);
		}		
	    me.hideMask();
	},
	
	/**根据类型设置时间*/
	setDateValue:function(CheckType,Sysdate){
		var me =this;
		var startDate=null,endDate=null;
		if(CheckType=='1'){//按天审核
			var beginDate = JcallShell.Date.toString(Sysdate, true);
            startDate=beginDate;
            endDate=JShell.Date.getNextDate(beginDate,1);
		}else{
			var beginDateStr = JcallShell.Date.toString(Sysdate, true);
			var b = beginDateStr.split("-");
			startDate = b[0] + '-' + b[1] + '-01';
			var endDateStr = JcallShell.Date.getMonthLastDate(b[0], b[1], true);
			var end=JcallShell.Date.toString(endDateStr, true);
		    endDate=JShell.Date.getNextDate(end,1);
		}
		me.MemoTabPanel.startDate=startDate;
		/**结束时间*/
		me.MemoTabPanel.endDate=endDate;
	},
	 /**
     *不选中的按钮清空选中状态     */
	cleartogglebuttonsToolbar:function(buttonsToolbar,com){
		for(var i = 0; i < buttonsToolbar.items.length; i++) {
			if(buttonsToolbar.items.items[i].itemId){
				if(com.itemId != buttonsToolbar.items.items[i].itemId){    
					buttonsToolbar.items.items[i].toggle(false);
				}
			}
		}
	},
	 /**
     * 字符长度计算
     *获得字符串实际长度，中文2，英文1
     *str要获得长度的字符串
     */
	getNum:function(str){
		if(!str) return;
		str = str.replace(/\s/ig,'');
	    var realLength = 0, len = str.length, charCode = -1;
	    for (var i = 0; i < len; i++) {
	      charCode = str.charCodeAt(i);
		  if (charCode >= 0 && charCode <= 128) 
		       realLength += 1;
		    else
		       realLength += 2;
	    }
	    return realLength/2;  
	},
	getData: function(operateData, text) {
		var me = this;
		var operateobj = {},
			OperateListData = [];
		var ItemDataType = 'C',
			ItemCode = '',
			MinValue = '',
			MaxValue = '',
			DataLength = '',
			DecimalLength = '',
			DefaultValue = '',
			ItemValueList = null,
			ItemHeight=null,InitItemCode=null,
			ItemWidth=null,IsSpreadItemList=null,
			IsMultiSelect=null,IsInputItemValue=null;
		var strArr = [],
			textLength = 0;

		for(var i = 0; i < operateData.length; i++) {
			if(operateData[i].text || operateData[i].text.length>0){
				textLength=me.getNum(operateData[i].text);
				strArr.push(textLength);
			}
		}
		//项目text最大长度
		textLength = Math.max.apply(null, strArr);
		for(var j = 0; j < operateData.length; j++) {
			ItemDataType = 'C', ItemCode = '',
				MinValue = '', MaxValue = '', DataLength = '', DecimalLength = '',
				DefaultValue = '', ItemValueList = null,IsSpreadItemList=null,
				ItemHeight=null,ItemWidth=null,
				IsMultiSelect=null,IsInputItemValue=null,AddValue=null;
			//解析Para
			if(operateData[j].Para) {
				ItemDataType = operateData[j].Para.ItemDataType;
				DefaultValue = operateData[j].Para.DefaultValue;
				MaxValue = operateData[j].Para.MaxValue;
				MinValue = operateData[j].Para.MinValue;
				DataLength = operateData[j].Para.DataLength;
				DecimalLength = operateData[j].Para.DecimalLength;
				ItemValueList = operateData[j].Para.ItemValueList;
				IsSpreadItemList=operateData[j].Para.IsSpreadItemList;
				ItemHeight=operateData[j].Para.ItemHeight;
				ItemWidth=operateData[j].Para.ItemWidth;
				IsMultiSelect=operateData[j].Para.IsMultiSelect;
				IsInputItemValue=operateData[j].Para.IsInputItemValue;
				AddValue=operateData[j].Para.AddValue;
				InitItemCode=operateData[j].Para.InitItemCode;
			}
			if(ItemDataType) ItemDataType=ItemDataType.toUpperCase();
			operateobj = {
				text: operateData[j].text,
				textLength:textLength,
				ItemDataType: ItemDataType,
				DefaultValue: DefaultValue,
				MaxValue: MaxValue,
				MinValue: MinValue,
				MaxDataLength: DataLength,
				DecimalLength: DecimalLength,
				ItemValueList: ItemValueList,
				ItemCode: operateData[j].tid,
				tid: operateData[j].tid,
				pid: operateData[j].pid,
				ptext: text,
				IsSpreadItemList:IsSpreadItemList,
				IsMultiSelect:IsMultiSelect,
				IsInputItemValue:IsInputItemValue,
				ItemWidth:ItemWidth,
				ItemHeight:ItemHeight,
				AddValue:AddValue,
				InitItemCode:InitItemCode
			}
			OperateListData.push(operateobj);
		}
		return OperateListData;
	},
	/**大小改变后按钮位置*/
	setResizeBtn:function(){
		var me =this;
		var maxWidth = me.EditTabPanel.getWidth();
		 me.Btn.setWidth(maxWidth);
	},
    /**查询仪器维护数据表数据*/
	getListData: function(defaultWhere,operatedate) {
		var me = this,
			list = [],arr=[];
		var url = JShell.System.Path.getRootUrl(me.EditTabPanel.Form.selectUrl2);
		var fields = "EMaintenanceData_Id,EMaintenanceData_TempletItem,EMaintenanceData_TempletDataType," +
			"EMaintenanceData_ItemResult,EMaintenanceData_TempletTypeCode,EMaintenanceData_TempletItemCode,EMaintenanceData_ItemDataType,EMaintenanceData_ItemMemo,EMaintenanceData_TempletItemCode";
		url += '&fields=' + fields;
		url += defaultWhere+'&isLoadBeforeData=1';
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var result = Ext.isArray(data.value.list); //为数组时才处理
				if(result) {
                   list=data.value.list;
				}
			}
		}, false);
		return list;
	},
	/**查询仪器维护数据表数据*/
	getTBListData: function(TempletID,operatedate) {
		var me = this;
			me.TodayDataList=[];
		var where = '?templetID=' + TempletID + '&beginDate=' + operatedate + '&endDate=' + operatedate+'&typeCode='+me.NowTabType+
			'&isLoadBeforeData=1';
		var url = JShell.System.Path.getRootUrl(me.selectTBDataUrl) + where;
		JShell.Server.get(url, function(data) {
			if(data.success && data.value) {
				if(data.value.list.length > 0) {
					me.TodayDataList=data.value.list;
				}
			}
		}, false);
		return me.TodayDataList;
	},
	/**还原备注*/
	onSetItemMemo:function(list){
		var me =this;
		var ItemMemo='',Id='';
		if(!list)list=[];
		for(var i=0;i<list.length;i++){
			var TempletDataType = list[i].EMaintenanceData_TempletDataType;
			if(TempletDataType == '1') {
				var reg = new RegExp("</br>", "g");
				if(list[i].EMaintenanceData_ItemMemo){
					ItemMemo = list[i].EMaintenanceData_ItemMemo.replace(reg, "\r\n");
				}
				if(list[i].EMaintenanceData_Id){
					Id = list[i].EMaintenanceData_Id;
				}
			}
		}
		var obj={
			Id: Id,
	        ItemMemo: ItemMemo
		};
		me.MemoTabPanel.Form.onResetClick();
		me.MemoTabPanel.Form.setItemMemoValue(obj)
	},
	/**还原备注,不还原id ,用于载入数据*/
	onLoadDataSetMemo:function(list){
		var me =this;
		var ItemMemo='',Id='',obj={};
		if(!list)list=[];
		for(var i=0;i<list.length;i++){
			var TempletDataType = list[i].EMaintenanceData_TempletDataType;
			if(TempletDataType == '1') {
				var reg = new RegExp("</br>", "g");
				ItemMemo= list[i].EMaintenanceData_ItemMemo;
				if(ItemMemo){
					ItemMemo = ItemMemo.replace(reg, "\r\n");	          
				}
	            Id = list[i].EMaintenanceData_Id;
			}
		}
		me.MemoTabPanel.Form.onSetResult(ItemMemo);
	},
	/**TB类型载入上一次数据,已存在数据 不需要保存,否则默认保存*/
	getTBloadDailyData:function(){
		var me = this;
		me.isDaily=true;
		var stratDate=me.getOperateDate();
		//昨天的数据
		var DailyDate=JShell.Date.getNextDate(stratDate,-1);
		var DailyDateStr=JShell.Date.toString(DailyDate,true);
		var msg='已经有数据,是否继续载入?';

		if(me.IsLoadData && stratDate!=DailyDateStr && me.isDaily){
			JShell.Msg.confirm({
				msg: msg
			}, function(but) {
				if(but == "ok"){
					me.onSearchTBData(DailyDateStr,stratDate);
				}
			});
		}else{
			me.onSearchTBData(DailyDateStr,stratDate);
		}
	},
	/**非TB类型载入上一次数据,已存在数据 不需要保存,否则默认保存*/
	getloadDailyData:function(){
		var me = this;
		me.isDaily=true;
		var stratDate=me.getOperateDate();
		//昨天的数据
		var DailyDate=JShell.Date.getNextDate(stratDate,-1);
		var DailyDateStr=JShell.Date.toString(DailyDate,true);
		var msg='已经有数据 是否继续载入?';
		var list = me.getListInfo(stratDate);
		if(me.TodayDataList.length>0 && stratDate!=DailyDateStr && me.isDaily){
			JShell.Msg.confirm({
				msg: msg
			}, function(but) {
				if(but == "ok"){
					//载入数据自动保存
					if(me.IsAutoSaveLoadData=='1'){
						var stratDate1=me.getOperateDate();
						me.onDailyLoadDataSave(stratDate1,list);
					}else{
						var stratDate=me.getOperateDate();
                        me.onDailyLoadData(stratDate);
					}
				}else{
					me.isDaily=false;
				}
			});
		}else{
			//载入数据自动保存
			if(me.IsAutoSaveLoadData=='1'){
				var stratDate1=me.getOperateDate();
				me.onDailyLoadDataSave(stratDate1,list);
			}else{
				var stratDate=me.getOperateDate();
                me.onDailyLoadData(stratDate);
			}
			//me.onSearchData(DailyDateStr);
		}
	},
	onSearchTBData:function(operatedate,stratDate){
		var me =this;
		//上次数据页签备注内容信息
		var obj={};
		//(昨天的页签备注项信息)页签备注项,查询是否已存在,已存在只是修改,否则新增
		me.getItemMemoInfo(operatedate,function(value){
			if(value){
				obj=value.list[0];
			}
		});
		var where = '?templetID=' + me.TempletID + '&beginDate=' + stratDate + '&endDate=' + stratDate+'&typeCode='+me.NowTabType+
		'&isLoadBeforeData=1';
		var url = JShell.System.Path.getRootUrl(me.selectTBDataUrl) + where;
		JShell.Server.get(url, function(data) {
			if(!data.value)me.fireEvent('nodata', me.Form);
			if(data.success) {
				if(data.value && data.value.list.length > 0) {
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = data.value.list.length;
                    //数据处理
					for(var n = 0; n < data.value.list.length; n++) {
						var BatchNumber= data.value.list[n].BatchNumber;
						var operatedate2 =data.value.list[n].操作日期;
						var Date2 = JcallShell.Date.toString(operatedate2, true);
						me.getTbInfo(BatchNumber,Date2,obj);
					}
				}
//				else{
//					me.AutoSaveData();
//					me.hideMask();
//				}
			}
		},false);
	},
	
	//循环行数据
	getTbInfo:function(BatchNumber,operatedate,obj){
		var me = this;
		var where ='&where=emaintenancedata.ETemplet.Id='+me.TempletID+
		' and emaintenancedata.BatchNumber='+BatchNumber+
		" and emaintenancedata.ItemDate='"+operatedate+"'"+
		" and emaintenancedata.TempletTypeCode='"+me.NowTabType+"'";
        var fields='EMaintenanceData_Id,EMaintenanceData_TempletItem,EMaintenanceData_TempletDataType,'+
        'EMaintenanceData_ItemResult,EMaintenanceData_TempletTypeCode,EMaintenanceData_TempletItemCode,'+
        'EMaintenanceData_ItemDataType,EMaintenanceData_ItemMemo,EMaintenanceData_TempletItemCode,EMaintenanceData_TempletType';
		var url = JShell.System.Path.getRootUrl(me.selectUrl) +'&fields='+fields+ where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data&& data.value){
					me.getAddTbEntity(data.value.list,obj);
				}
			}
		},false);
	},
	getAddTbEntity:function(list,obj){
		var me = this;
		var UserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var Sysdate=me.getOperateDate();
		var operatedate = JcallShell.Date.toString(Sysdate, true);
		var OperateTime = JcallShell.Date.toString(operatedate);
		var entityList=[];
		if(me.IsTbType=='TB'){
			list.push(obj);
		}
		for(var i= 0; i<list.length;i++ ){
			var ItemDataType =list[i].EMaintenanceData_ItemDataType;
			var ItemMemo =list[i].EMaintenanceData_ItemMemo;
			var ItemResult =list[i].EMaintenanceData_ItemResult;
			var TempletDataType =list[i].EMaintenanceData_TempletDataType;
			var TempletItem =list[i].EMaintenanceData_TempletItem;
			var TempletItemCode =list[i].EMaintenanceData_TempletItemCode;
			var TempletTypeCode =list[i].EMaintenanceData_TempletTypeCode;
            var TempletType= list[i].EMaintenanceData_TempletType;
            var Id= list[i].EMaintenanceData_Id;
			if(!TempletDataType)TempletDataType='1';
			if(!me.NowTabType){
				JShell.Msg.error("上次数据模板类型为空,不能载入");
			    return false;
			}
			var entity ={
				ItemDate:JShell.Date.toServerDate(operatedate),
				ItemMemo:ItemMemo,
				ItemResult:ItemResult,
				OperateTime:JShell.Date.toServerDate(OperateTime),
				TempletDataType:parseInt(TempletDataType),
				TempletTypeCode:me.NowTabType//TempletTypeCode
			};
			if(TempletItemCode){
				entity.TempletItemCode=TempletItemCode;
			}
			if(ItemDataType){
				entity.ItemDataType=ItemDataType;
			}
			if(TempletType){
				entity.TempletType=TempletType;
			}
			if(TempletItem){
				entity.TempletItem=TempletItem;
			}
			entity.ETemplet={
				Id:me.TempletID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.HREmployee={
				Id:UserID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			//页签备注项,查询是否已存在,已存在只是修改,否则新增
			if(TempletDataType=='1' && me.IsTbType=='TB'){
				me.getIsItemMemo(function(data){
					if(data && data.value){
						if(data.value.list.length>0){
							//赋值id
							var id=data.value.list[0].EMaintenanceData_Id;
							entity.Id=id;
							if(obj){
								var ItemMemo=obj.EMaintenanceData_ItemMemo;
							    entity.ItemMemo=ItemMemo;
							}
						}
					}
				});
			}
			entityList.push(entity);
		}
		if(me.IsTbType=='TB'){
			me.onSaveTBInfo(operatedate,entityList);
		}else{
			me.onSaveInfo(operatedate,entityList,'1');
		}
	},
	
	getListInfo:function(operatedate,typeCode){
		var me =this;
		var where ='&where=emaintenancedata.ETemplet.Id='+me.TempletID+
		" and emaintenancedata.ItemDate='"+operatedate+"'"+
		" and emaintenancedata.TempletTypeCode='"+me.NowTabType+"'";
        var fields='EMaintenanceData_Id,EMaintenanceData_TempletItem,EMaintenanceData_TempletDataType,'+
        'EMaintenanceData_ItemResult,EMaintenanceData_TempletTypeCode,EMaintenanceData_TempletItemCode,'+
        'EMaintenanceData_ItemDataType,EMaintenanceData_ItemMemo,EMaintenanceData_TempletItemCode,EMaintenanceData_TempletType';
		var url = JShell.System.Path.getRootUrl(me.selectUrl) +'&fields='+fields+ where;
        me.TodayDataList=[];
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data&& data.value){
					me.TodayDataList=data.value.list;
				}
			}
		},false);
		return me.TodayDataList;
	},
	//循环行数据
	onSearchData:function(operatedate){
		var me = this;
		var where ='&where=emaintenancedata.ETemplet.Id='+me.TempletID+
		" and emaintenancedata.ItemDate='"+operatedate+"'"+
		" and emaintenancedata.TempletTypeCode='"+me.NowTabType+"'";
        var fields='EMaintenanceData_Id,EMaintenanceData_TempletItem,EMaintenanceData_TempletDataType,'+
        'EMaintenanceData_ItemResult,EMaintenanceData_TempletTypeCode,EMaintenanceData_TempletItemCode,'+
        'EMaintenanceData_ItemDataType,EMaintenanceData_ItemMemo,EMaintenanceData_TempletItemCode,EMaintenanceData_TempletType';
		var url = JShell.System.Path.getRootUrl(me.selectUrl) +'&fields='+fields+ where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data&& data.value){
					if(me.IsAutoSaveLoadData=='1'){
					    me.getAddTbEntity(data.value.list);
					}else{
						me.EditTabPanel.Form.setDailyData(data.value.list);
						me.onLoadDataSetMemo(data.value.list);
						me.onMemoTabPanel();
				        me.hideMask();
				        me.isDaily=false;
					}
				}else{
					//载入数据自动保存
					if(me.IsAutoSaveLoadData=='1'){
						me.AutoSaveData();
					}else{
					    return;
					}
				}
			}
		},false);
	},
    /**根据参数控制载入数据按钮
    * 空或0为不自动保存载入数据，1为自动保存载入数据
    */
	getEParaVal: function(callback) {
		var me = this;	
		var paraVal =0;
	    var url = JShell.System.Path.getRootUrl(me.selectParaUrl);
	    var fields='EParameter_ParaValue,EParameter_ParaNo';
		var where="&fields="+fields+"&where=ParaType='QualityRecord' and (ParaNo = 'IsAutoSaveLoadData' or ParaNo = 'IsSaveAllData'))";
		url+=where;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var obj = data.value;
				if(callback) callback(obj);
			} else {
				JShell.Msg.error('获取系统参数出错！' + data.msg);
			}
		}, false);
	},
	/**判断是否已保存有页备注*/
	getIsItemMemo:function(callback){
		var me =this;
		var Sysdate=me.getOperateDate();
		var where =me.getTBMemoWhere(me.TempletID,Sysdate);
        var fields='EMaintenanceData_Id,EMaintenanceData_ItemMemo';
		var url = JShell.System.Path.getRootUrl(me.selectUrl) +'&fields='+fields+ '&where='+where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data)
			}
		},false);
	},
	/**查找页签备注项*/
	getItemMemoInfo:function(operatedate,callback){
		var me =this;
		var where =me.getTBMemoWhere(me.TempletID,operatedate);
        var fields = "EMaintenanceData_Id,EMaintenanceData_TempletItem,EMaintenanceData_TempletDataType," +
			"EMaintenanceData_ItemResult,EMaintenanceData_TempletTypeCode,EMaintenanceData_TempletItemCode,EMaintenanceData_ItemDataType,EMaintenanceData_ItemMemo,EMaintenanceData_TempletItemCode";
		var url = JShell.System.Path.getRootUrl(me.selectUrl) +'&fields='+fields+ '&where='+where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(!data && !data.value)return;
				callback(data.value)
			}
		},false);
	},
	/**质量记录登记页面保存数据后是否直接预览
    * 空或0为不自动保存载入数据，1为自动保存载入数据
    */
	getIsSaveDataPreview: function(callback) {
		var me = this;	
		var paraVal =0;
	    var url = JShell.System.Path.getRootUrl(me.selectParaUrl);
	    var fields='EParameter_ParaValue';
		var where="&fields="+fields+"&where=ParaType='QualityRecord' and ParaNo = 'IsSaveDataPreview')";
		url+=where;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var obj = data.value;
				if(callback) callback(obj);
			} else {
				JShell.Msg.error('获取系统参数出错！' + data.msg);
			}
		}, false);
	},
	/**获取模板审核和数据状态*/
	getTempletState: function(callback) {
		var me = this;	
	    var url = JShell.System.Path.getRootUrl(me.selectTempletStateUrl);
	    var fields='ETemplet_IsFillData';
	    var Sysdate=me.getOperateDate();
		var where="&fields="+fields+"&templetID="+me.TempletID+
		"&itemDate="+Sysdate;
		url+=where;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var obj = data.value;
				if(callback) callback(obj);
			} else {
				JShell.Msg.error('获取模板审核和数据状态出错！' + data.msg);
			}
		}, false);
	},
	/**创建和还原表单,数据已加载不需要再次加载,用于一键保存
	 */
	createItemLoadData:function(comitemId){
		var me=this;
		//动态生成表单
		me.createMaintenanceDataForm();
		//还原数据
		me.setLoadData(comitemId);
		
	},
	/**还原表单,数据已加载不需要再次加载,用于一键保存*/
	setLoadData:function(comitemId){
		var me = this;
	    for(var i =0 ;i<me.allSaveArr.length;i++){
	    	if(me.allSaveArr[i].comitemId==comitemId){
	    		var list=me.allSaveArr[i].objArr;
	    	    var listarr=[];
	    		for(var n=0;n<list.length;n++ ){
	    			var obj ={
	    				EMaintenanceData_Id:list[n].Id,
	    				EMaintenanceData_ItemDataType:list[n].ItemDataType,	    	
	    				EMaintenanceData_ItemMemo:list[n].ItemMemo,
	    				EMaintenanceData_ItemResult:list[n].ItemResult==null ? '' : list[n].ItemResult,
	    				EMaintenanceData_TempletDataType:list[n].TempletDataType,
	    				EMaintenanceData_TempletItemCode:list[n].TempletItemCode,
	    				EMaintenanceData_TempletTypeCode:list[n].TempletTypeCode
	    			}
	    			listarr.push(obj);
	    		}
                var i = 0,IsSpreadItemList=null,IsMultiSelect=null,
					ItemDataType = 'C',InitItemCode='';
				Ext.Array.each(me.NowTabData, function(rec) {
					IsSpreadItemList=null,IsMultiSelect=null;
					
					text = rec['text'];
					if(rec['ItemDataType']) {
						ItemDataType = rec['ItemDataType'];
					}
					ItemCode = rec['ItemCode'];
					if(rec['IsSpreadItemList']){
						IsSpreadItemList= rec['IsSpreadItemList'];
					}
					InitItemCode= rec['InitItemCode'];
					if(rec['IsMultiSelect']){
						IsMultiSelect= rec['IsMultiSelect'];
					}
					//判断是已设置效期的组件
			        if(InitItemCode && ItemDataType){
			        	var indexdatestr = InitItemCode.lastIndexOf("\|");  
						InitItemCode  = InitItemCode.substring(indexdatestr + 1, InitItemCode.length);
			        }
					me.EditTabPanel.Form.SetFormData(listarr, ItemCode, ItemDataType, i,IsSpreadItemList,IsMultiSelect,false,InitItemCode);
					i = i + 1;
				});
				me.onSetItemMemo(listarr);
			    break;
	    	}
	    }
	},
	/**一键保存获取值,替换旧的数据*/
	getAllSaveInfo:function(list,com){
		var  me = this;
		var objArr = me.getInitialValue();
		var Sysdate=me.getOperateDate();
    	var obj={
    		comitemId: me.newBtnItemId,
    		objArr:objArr,
    		strDate:Sysdate
    	};
    	if(list.length==0){
    		list.push(obj);
    	}else{
    		for(var n=0;n<list.length;n++){
        		if(list[n].comitemId == me.newBtnItemId ){
        			list.splice(n, 1);
        		}
        	}
    		list.push(obj);
    	}
    	return list;
	},
	/**暂存页签切换数据*/
	getInitialValue:function(){
		var me = this;
		var operatedate = me.getOperateDate();
		//获取备注信息，冗余填充到各个项目
		var ItemMemoVal=me.MemoTabPanel.Form.getMemoInfo();
	    entityList=me.EditTabPanel.Form.SaveMaintenanceData(me.NowTabData, me.TempletID,me.NowTabType,operatedate,ItemMemoVal,me.isTbAdd,me.BatchNumber);
	    var entity = me.MemoTabPanel.Form.SaveFormData(me.TempletID, me.NowTabType,operatedate,me.isTbAdd,me.BatchNumber);
	    entityList.push(entity);
	    return entityList;
	},
	
	/**
	 * 保存后根据返回ID赋值
	 * 通用类型
    * */
   onChangeResult2:function(data){
  	   var me =this;
  	   if(!data || !data.value) return;
  	   if(me.IsTbType=='TB')return;
  	   var arr =[],arr2=[];
  
  	   for(var i =0 ;i<me.allSaveArr.length;i++){
    	   if(me.allSaveArr[i].comitemId==me.newBtnItemId){
		  	   arr=me.allSaveArr[i].objArr;
		  	   continue;
	       }
       }
  	   if(data.value.list){
			var list = data.value.list;
			var len =list.length;
			if(len==0)return;
			var nlen=arr.length;
			//当前保存项
			for(var n=0;n<nlen;n++){
				var idL =arr[n].Id;
				var TempletDataTypeL = arr[n].TempletDataType;
				var TempletItemCodeL = arr[n].TempletItemCode;
				var TempletTypeCodeL = arr[n].TempletTypeCode;
				var ETempletIdL = arr[n].ETempletId;
				for(var i =0 ;i<len;i++){
					var TempletDataType = list[i].EMaintenanceData_TempletDataType;
					var TempletItemCode = list[i].EMaintenanceData_TempletItemCode;
					var Id  = list[i].EMaintenanceData_Id;
					var TempletTypeCode = list[i].EMaintenanceData_TempletTypeCode;
					var ETempletId = list[i].EMaintenanceData_ETemplet_Id;
				    //页签备注
				    if(TempletDataTypeL=="1" && ETempletId==ETempletIdL && 
				    TempletTypeCode == TempletTypeCodeL ) {
				    	me.MemoTabPanel.Form.onSetIdVal(Id);
				    }
				    //其他
				    if(TempletDataTypeL=="2" && ETempletId==ETempletIdL && 
				    TempletTypeCode == TempletTypeCodeL  && TempletItemCodeL==TempletItemCode) {
			    		var obj ={
		    				EMaintenanceData_Id:list[i].EMaintenanceData_Id,
		    				EMaintenanceData_ItemDataType:list[i].EMaintenanceData_ItemDataType,	    	
		    				EMaintenanceData_TempletDataType:list[i].EMaintenanceData_TempletDataType,
		    				EMaintenanceData_TempletItemCode:list[i].EMaintenanceData_TempletItemCode,
		    				EMaintenanceData_TempletTypeCode:list[i].EMaintenanceData_TempletTypeCode
		    				
		    			}
			    		if(JSON.stringify(arr2).indexOf(JSON.stringify(list[i]))===-1){
			    			arr2.push(obj);
			    		}

				    }
			    }
            }
			  var i = 0,IsSpreadItemList=null,IsMultiSelect=null,
				ItemDataType = 'C';
			Ext.Array.each(me.NowTabData, function(rec) {
				IsSpreadItemList=null,IsMultiSelect=null;
				text = rec['text'];
				if(rec['ItemDataType']) {
					ItemDataType = rec['ItemDataType'];
				}
				var InitItemCode =rec['InitItemCode'];
				ItemCode = rec['ItemCode'];
				if(rec['IsSpreadItemList']){
					IsSpreadItemList= rec['IsSpreadItemList'];
				}
				if(rec['IsMultiSelect']){
					IsMultiSelect= rec['IsMultiSelect'];
				}
				//判断是已设置效期的组件
     	        if(InitItemCode && ItemDataType){
		        	var indexdatestr = InitItemCode.lastIndexOf("\|");  
					InitItemCode  = InitItemCode.substring(indexdatestr + 1, InitItemCode.length);
		        }
				me.EditTabPanel.Form.SetFormData2(arr2, ItemCode, ItemDataType, i,IsSpreadItemList,IsMultiSelect,false,InitItemCode);
				i = i + 1;
			});
        }
  	}
});