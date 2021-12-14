/**
 * 【功能说明】
 * 质控数据列表，对质控数据进行增删改查
 * 
 * 【公开方法】
 * load(id,type):更新质控数据，id：可以是质控物也可以是项目，type：[1:质控物；2:项目]
 * 
 */
Ext.ns('Ext.iqc');
Ext.Loader.setConfig({enabled: true});//允许动态加载
Ext.Loader.setPath('Ext.zhifangux',getRootPath()+'/ui/zhifangux');
Ext.define('Ext.iqc.date.InfoList',{
	extend:'Ext.zhifangux.BasicGridPanel',
	alias:'widget.iqcdateinfolist',
	title:'质控列表',
	/**基础标题*/
    defaultTitle:'',
	width:800,
	hieght:600,
	/**是否循环定位光标*/
	isCycle:true,
	/**默认加载数据*/
	defaultLoad:false,
	/**服务地址*/
	serverUrl:{
		/**获取预备数据*/
		searchPreliminaryData:getRootPath()+'/QCService.svc/QC_RJ_GetJudgeLoseControlBackupValue',
		/**质控失控判断*/
		judgeLoseControlValue:getRootPath()+'/QCService.svc/QC_RJ_JudgeLoseControlValue',
		/**获取列表*/
		search:[
			getRootPath()+'/QCService.svc/QC_BA_GetQCDValueByQCMatAndDate',//仪器-质控物
			getRootPath()+'/QCService.svc/QC_BA_GetQCDValueByItemAndDate'//仪器-质控项目
		],
		/**新增质控数据*/
		add:getRootPath()+'/QCService.svc/QC_BA_ADDQCDataCustom',
		/**修改质控数据*/
		update:getRootPath()+'/QCService.svc/QC_BA_UpdateQCDataCustom',
		/**根据ID删除质控数据*/
		del:getRootPath()+'/QCService.svc/QC_UDTO_DelQCDValue',
		/**逻辑删除*/
		updateIsUse:getRootPath()+'/QCService.svc/QC_UDTO_UpdateQCDValueByField'
	},
	/**条件的ID属性*/
	tid:'',
	/**仪器ID*/
	pid:'',
	/**服务类型,1:质控物;2:质控项目*/
	urlType:1,
	/**旧的条件*/
	oldParams:{},
	/**数据列信息*/
	columnFields:[
		{dataIndex:'QCItemName',text:'项目',width:120,hidden:true,des:'质控项目名称'},
		{dataIndex:'QCItemSName',text:'项目',width:120,hidden:true,des:'质控项目简称'},
		{dataIndex:'QCMatName',text:'质控物',width:120,hidden:true,des:'质控物名称'},
		
		{dataIndex:'QCDataLotNo',text:'日序号',width:50,des:'质控批次'},
		{dataIndex:'ReportValue',text:'质控结果',width:80,des:'质控结果'},
		{dataIndex:'Target',text:'靶值',width:60,des:'靶值'},
		{dataIndex:'SD',text:'标准差',width:60,des:'标准差'},
		{dataIndex:'CVValue',text:'变异系数',width:80,des:'变异系数'},
		{dataIndex:'IsControl',text:'失控',width:50,des:'是否失控'},
		{dataIndex:'QCControlInfo',text:'失控规则',width:80,des:'失控规则'},
		{dataIndex:'Operator',text:'检验人',width:80,des:'检验人'},
		{dataIndex:'ReceiveTime',text:'质控时间',width:80,des:'质控时间'},
		
		{dataIndex:'EquipName',text:'仪器',width:60,hidden:true,des:'仪器名称'},
		{dataIndex:'ConcLevel',text:'浓度',width:60,hidden:true,des:'浓度水平'},
		{dataIndex:'Manu',text:'厂家',width:60,hidden:true,des:'厂家'},
		{dataIndex:'EquipID',text:'仪器ID',width:60,hidden:true,des:'仪器ID'},
		{dataIndex:'QCItemID',text:'质控项目ID',width:60,hidden:true,des:'质控项目ID'},
		{dataIndex:'QCItemDataTimeStamp',text:'质控项目时间戳',width:60,hidden:true,des:'质控项目时间戳'},
		{dataIndex:'QCMatID',text:'质控物ID',width:60,hidden:true,des:'质控物ID'},
		
		{dataIndex:'Id',text:'主键ID',width:60,hidden:true,des:'主键ID'},
		{dataIndex:'LabID',text:'实验室ID',width:60,hidden:true,des:'实验室ID'},
		{dataIndex:'DataAddTime',text:'数据加入时间',width:60,hidden:true,des:'数据加入时间'},
		{dataIndex:'DataTimeStamp',text:'时间戳',width:60,hidden:true,des:'时间戳'},
		
		{dataIndex:'OriglValue',text:'仪器原始数值',width:60,hidden:true,des:'仪器原始数值'},
		{dataIndex:'IsEquipResult',text:'是否仪器结果',width:60,hidden:true,des:'是否仪器结果'},
		{dataIndex:'QCItemValueType',text:'质控类型',width:60,hidden:true,des:'质控类型'}
	],
	/**处理的总条数*/
	resultCount:0,
	/**当前处理的条数*/
	nowCount:0,
	/**错误信息数组*/
	ErrorInfo:[],
	/**分割线*/
	columnLines:true,
	/**枚举类*/
	Enum:Ext.create('Ext.iqc.Enum'),
	
	/**预备数据*/
	preliminaryData:null,
	/**是否已获取预备数据*/
	hasLoadPreliminaryData:false,
	/**还没有返回的质控失控判断请求数量*/
	remainingQuantity:0,
	
	/**
	 * 渲染完后
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initListeners();
		//默认加载数据
		if(me.defaultLoad){
			me.load(null,null,true);
		}else{
			me.disableControl();//禁用所有的操作功能 
		}
		//初始化质控日期
		me.initDate();
	},
	/**
	 * 初始化面板信息
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.store = me.store || me.createStore();//创建数据集
		me.columns = me.columns || me.createColumns();//创建数据列内容
		me.dockedItems = me.dockedItems || me.createDockedItems();//创建挂靠功能
		me.cellEdit = me.cellEdit || (me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}));//单元格编辑
		me.callParent(arguments);
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.Store',{
			fields:me.getStoreFields(),
			pageSize:1000,autoLoad:false,
			proxy:{
				type:'ajax',
				url:'',
				reader:{
					type:'json',
					totalProperty:'count',
	                root:'list'
				},
				//内部数据匹配方法
	            extractResponseData:function(response){
			    	return me.changeData(response);
			  	}
			}
		});
		return store;
	},
	/**
	 * 创建数据列内容
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
		var columns = [];
		var list = me.columnFields;
		for(var i in list){
			var con = list[i];
			
			if(con.dataIndex == 'ReportValue'){//质控结果
				con.editor = {};
			}else if(con.dataIndex == 'ReceiveTime'){//质控时间
				con.renderer = function(v,m){
					m.style='background:#f0f0f0;';
					return Ext.util.Format.date(v,'Y-m-d');
				};
			}else{
				con.renderer = function(v,m){m.style='background:#f0f0f0;';return v;};
			}
			con.doSort = function(state) {
		        var ds = this.up('tablepanel').store;
		        me.defaultLoad && ds.sort({
		            property: this.getSortParam(),
		            direction: state
		        });
		    };
			columns.push(con);
		}
		return columns;
	},
	/**
	 * 创建挂靠功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',dock:'top',itemId:'toptoolbar',
			items:[{
				xtype:'datefield',itemId:'date',format:'Y-m-d',
				width:200,labelWidth:65,fieldLabel:'质控日期',
				value:Ext.util.Format.date(Ext.Date.add(new Date(),Ext.Date.Day),"Y-m-d")
			},'-',{
				xtype:'checkbox',itemId:'outOfControl',boxLabel:'失控处理'
			}]
		},{
			xtype:'toolbar',dock:'bottom',itemId:'bottomtoolbar',
			items:[{
				itemId:'refresh',text:'刷新',iconCls:'build-button-refresh',
				handler:function(but){me.load(null,null,true);}
			},{
				itemId:'add',text:'新增',iconCls:'build-button-add',
				handler:function(but){me.clickAddButton();}
			},{
				itemId:'edit',text:'修改',iconCls:'build-button-edit',
				handler:function(but){me.clickEditButton();}
			},{
				itemId:'delete',text:'删除',iconCls:'build-button-delete',
				handler:function(but){me.clickDelButton();}
			},{
				itemId:'save',text:'保存',iconCls:'build-button-save',
				handler:function(but){me.clickSaveButton();}
			},'-',{
				itemId:'print',text:'打印',iconCls:'print',disabled:true,autoDisabled:true
			},{
				itemId:'export',text:'导出',iconCls:'exportFile',disabled:true,autoDisabled:true
			}]
		}];
		return dockedItems;
	},
	/**
	 * 获取服务地址
	 * @private
	 * @return {}
	 */
	getUrl:function(){
		var me = this;
		var url = me.serverUrl.search[me.urlType-1];
		
		//质控物 long QCMatID, string QCStartDate, string QCEndDate, string fields, bool isPlanish
		//质控项目 long QCItemID, string QCStartDate, string QCEndDate, string fields, bool isPlanish
		//质控项目 long EquipID, long ItemID, string QCStartDate, string QCEndDate, bool NotShowEmptyData, string fields, bool isPlanish
		var params = [];
		
		if(me.urlType == 1){
			params.push('QCMatID=' + me.tid);
		}else if(me.urlType == 2){
			params.push('EquipID=' + me.pid);
			params.push('ItemID=' + me.tid);
		}
		
		var date = me.getComponent('toptoolbar').getComponent('date').getValue();
		
		if(date && Ext.typeOf(date) == 'string'){
			date = new Dated(date);
		}
		var sDate = getDateString(date);
		var eDate = getDateString(getNextDate(date));
		
		params.push('QCStartDate=' + sDate);
		params.push('QCEndDate=' + eDate);
		//params.push('fields=' + fields);
		//params.push('isPlanish=' + isPlanish);
		
		url += "?" + params.join("&");
		return url;
	},
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		//数据集监听
		me.store.on({
		    beforeload:function(store,operation){me.beforeLoad(store,operation);},
		    load:function(store,node,records,successful,eOpts){me.afterLoad(store,node,records,successful,eOpts);}
		});
		//质控日期监听
		var toptoolbar = me.getComponent('toptoolbar'),
			date = toptoolbar.getComponent('date');
		
		date.on({
			change:function(){
				me.load(null,null,true);
			}
		});
		
		//编辑校验监听
		me.on({
			cellAvailable:function(editor,e){
				var record = e.record,
					editors = editor.editors,
					id = e.column.getItemId(),
					value = editor.editors.get(id).getValue(),
					QCItemValueType = record.get('QCItemValueType');
				
				var bo = false;	
				if(e.value != value && e.field == 'ReportValue' && 
					(QCItemValueType == me.Enum.QCValueType[0].value || 
						QCItemValueType == me.Enum.QCValueType[1].value)
				){
					//定量靶值标准差||定量靶值标准差
					//数值转换
					if(value && value != '' && !isNaN(value)){
						//变异系数处理(定值)
						if(QCItemValueType == me.Enum.QCValueType[0])
							me.changeCVValue(record,value);
						bo = true;
						//质控失控判定
						me.getJudgeLoseControlValue(record);
					}
				}else{
					bo = true;
				}
				return bo;
			}
		});
	},
  	/**
	 * 加载数据
	 * @public
	 * @param {} id
	 * @param {} type
	 * @param {} isPrivates
	 */
	load:function(obj,type,isPrivate){
		var me = this,
			date = me.getComponent('toptoolbar').getComponent('date'),
			dateStr = date.getRawValue(),
			bo = date && date != '' && date.isValid();
		
		//是否需要加载预备数据
		var hasToGetPreliminaryData = (me.urlType != type) || (me.tid != obj.tid) || (me.oldParams.date != dateStr);
			
		if(!isPrivate){//外部调用
			if(type == 1){
				me.tid = obj.tid;
			}else if(type == 2){
				me.tid = obj.tid;
				me.pid = obj.pid;
			}
			
			me.urlType = type;
			me.showOrHideColumns(type);
		}
		
		if(bo){
			if(hasToGetPreliminaryData){
				me.getPreliminaryData();
			}
			
			me.oldParams.date = dateStr;
			me.store.proxy.url = me.getUrl();
			me.store.load();
		}
	},
	/**
	 * 点击新增按钮
	 * @private
	 */
	clickAddButton:function(){
		var me = this,
			date = me.getComponent('toptoolbar').getComponent('date'),
			dateStr = date.getRawValue();
			
		if(!date.isValid() || !dateStr || dateStr == ''){
			alertError("请选择质控日期!");
			return;
		}
		
		var records = me.getSelectionModel().getSelection();
		
		if(records.length != 1){
			alertError('请选择一条质控物记录进行新增操作！');
		}else{
			var record = records[0],
				rowIndex = me.getRowIndexByRecord(record),
				config = {},
				data = record.data;
				
			for(var i in data){
				if(i == 'QCDataLotNo'){//日序号
					config[i] = isNaN(data[i]) ? 1 : parseInt(data[i]) + 1;
				}else if(i == 'Operator'){//检验人
					config[i] = getSystemInfo('EmployeeName');
				}else if(i == 'ReceiveTime'){//质控时间
					config[i] = dateStr;//getSystemTime(true);
				}else if(i == 'Id' || i == 'ReportValue' || i == 'CVValue' || i == 'IsControl' || i == 'QCControlInfo'){
					//不拷贝ID、质控结果、变异系数、失控、失控规则的数据
				}else{
					config[i] = data[i];
				}
			}
			
			var rec = ('Ext.data.Model',config);
			me.store.insert(rowIndex+1,rec);
		}
	},
	/**
	 * 点击修改按钮
	 * @private
	 */
	clickEditButton:function(){
		var me = this;
		var length = me.store.getCount();
		if(length > 0){
			//光标定位到质控结果单元格
			var record = me.store.getAt(0);
			var column = me.getColumnByDataIndex('ReportValue');
			me.cellEdit.startEdit(record,column);
		}
	},
	/**
	 * 点击删除按钮
	 * @private
	 */
	clickDelButton:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1){
			alertError('请选择一条数据进行删除操作！');
		}else{
			var record = records[0];
			var id = record.get('Id');
			
			if(id && id != '' && id+'' != '-1'){//数据库中存在的数据
				var IsEquipResult = record.get('IsEquipResult');//是否是仪器结果
				if(IsEquipResult){//仪器结果
					var num = me.getOriglValueNum(record.get('OriglValue'));
					if(num == 1){//当日只有一条原始数据
						me.disUse(records[0]);//只能设置为不可用
					}else{//当日有相同的原始数据
						Ext.Msg.confirm('提示','是否因为质控反复传输，有相同结果，所以删除？',function(button){
							if(button=='yes'){
								me.deleteByRecord(records[0]);//物理删除
							}else if(button == 'no'){
								me.disUse(records[0]);//设置为不可用
							}
						});
					}
				}else{
					Ext.Msg.confirm('提示','确定要删除吗？',function(button){
						if(button=='yes'){me.deleteByRecord(records[0]);}
					});
				}
			}else{//数据库中不存在的数据
				if(!id || id == ''){//前台新增的数据
					var rowIndex = me.getRowIndexByRecord(record);
					me.store.remove(record);//在数据集中删除本行
					me.selectRecord(rowIndex);//定位选中行
				}else{//非前台新增行
					record.set('ReportValue','');
					record.commit();
				}
			}
		}
	},
	/**
	 * 根据质控物reocrd删除记录
	 * @private
	 * @param {} record
	 */
	deleteByRecord:function(record){
		var me = this,
			url = me.serverUrl.del + '?id=' + record.get('Id');
			
		var callback = function(text){
			var result = Ext.JSON.decode(text);
	        if(result.success){
	        	var rowIndex = me.getRowIndexByRecord(record);
	        	me.store.remove(record);//在数据集中删除本行
	        	me.selectRecord(rowIndex);//定位选中行
	        }else{
	        	alertError(result.ErrorInfo);
	        }
	        me.enableControl();//启用功能栏
		};
		me.disableControl();//禁用功能栏
		getToServer(url,callback);
	},
	/**
	 * 保存数据
	 * @private
	 */
	clickSaveButton:function(){
		var me = this,
			modifiedRecords = me.store.getModifiedRecords(),//修改过的数据
			date = me.getComponent('toptoolbar').getComponent('date'),
			dateStr = date.getRawValue();
			
		if(!date.isValid() || !dateStr || dateStr == ''){
			alertError("请选择质控日期!");
			return;
		}
			
		var records = [];
		for(var i in modifiedRecords){
			if(modifiedRecords[i].get('ReportValue') && modifiedRecords[i].get('ReportValue') != ''){
				records.push(modifiedRecords[i]);
			}
		}
			
		if(records.length == 0){
			alertError("没有需要保存的数据！");
		}else{
			me.resultCount = records.length;
			me.disableControl();//禁用功能栏
			for(var i in records){
				me.saveOne(records[i]);
			}
		}
	},
	/**
	 * 保存一条数据
	 * @private
	 * @param {} record
	 */
	saveOne:function(record){
		var me = this,
			successInfo = "批量保存成功！",
			id = record.get('Id'),
			bo = (id && id != '' && id+'' != '-1');//是否是修改
			
		var info = "项目名称:[" + record.get('QCItemSName') + "]";
		if(me.urlType == 2){
			info = "质控物:[" + record.get('QCMatName') + "]";
		}
		
		//服务地址
		var url = bo ? me.serverUrl.update : me.serverUrl.add;
		
		//交互对象
		var params = {
			fields:'QCDValue_Id,QCDValue_CVValue,QCDValue_IsControl,QCDValue_QCControlInfo',
			isPlanish:true
		};
		
		if(bo){//修改
			info += " 修改出错";
			//质控数据对象
			params.entity = {
				Id:record.get('Id'),//主键ID
				DataTimeStamp:record.get('DataTimeStamp').split(','),//时间戳
				ReportValue:record.get('ReportValue'),//质控结果
				CVValue:record.get('CVValue')//变异系数
			};
			//需要更新的数据
			params.updateField = 'Id,ReportValue';
		}else{//新增
			info += " 新增出错";
			//质控数据对象
			params.entity = {
				Id:-1,//主键ID
				QCDataLotNo:record.get('QCDataLotNo'),//日序号
				ReportValue:record.get('ReportValue'),//质控结果
				CVValue:record.get('CVValue'),//变异系数
				ReceiveTime:record.get('ReceiveTime'),//质控时间
//				Operator:{
//					Id:getSystemInfo('EmployeeID')
//				},//员工
				QCItem:{
					Id:record.get('QCItemID'),
					DataTimeStamp:record.get('QCItemDataTimeStamp').split(',')//时间戳
				},//质控项目
				IsUse:true//是否使用
			};
			
			if(!params.entity.ReceiveTime || params.entity.ReceiveTime == ''){
				var dateStr = me.getComponent('toptoolbar').getComponent('date').getRawValue();
				params.entity.ReceiveTime = convertJSONDateToJSDateObject(dateStr);
				record.set('ReceiveTime',dateStr);
			}
		}
		//QC_BA_ADDQCDataCustom(QCDValue entity, string fields, bool isPlanish)
		//QC_BA_UpdateQCDataCustom(QCDValue entity, string updateField, string fields, bool isPlanish)
		//与后台交互
		postToServer(url,Ext.JSON.encode(params),function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				if(!bo){record.set('Id',result.ResultDataValue);};//新增后Id赋值
				record.commit();
			}//脏数据变为正常数据
			me.showResult(text,info,successInfo);
		},null,false);
	},
	/**
	 * 数据更新为不可用
	 * @private
	 * @param {} record
	 */
	disUse:function(record){
		var me = this;
		var url = me.serverUrl.updateIsUse;
		var params = {
			entity:{
				Id:record.get('Id'),//主键ID
				IsUse:false
			},
			fields:'Id,IsUse'
		};
		
		//与后台交互
		postToServer(url,Ext.JSON.encode(params),function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				record.set('IsUse',false);
				record.commit();
				alertInfo("数据置为不可用状态成功 ！");
			}else{
				alertError(result.ErrorInfo);
			}
		},null,false);
	},
	/**
  	 * 加载数据前
  	 * @private
  	 * @param {} store
  	 * @param {} operation
  	 */
  	beforeLoad:function(store,operation){
  		var me = this;
  		me.disableControl();//禁用 所有的操作功能
  	},
  	/**
  	 * 加载数据后
  	 * @private
  	 * @param {} store
  	 * @param {} node
  	 * @param {} records
  	 * @param {} successful
  	 * @param {} eOpts
  	 */
  	afterLoad:function(store,node,records,successful,eOpts){
  		var me = this;
  		me.enableControl();//启用所有的操作功能
  	},
	/**
	 * 初始化质控日期
	 * @private
	 */
	initDate:function(){
		var me = this;
		var toptoolbar = me.getComponent('toptoolbar');
		var date = toptoolbar.getComponent('date');
		
		var systemTime = getSystemTime();//系统时间
		systemTime && date.setValue(systemTime);
	},
	/**
	 * 启用所有的操作功能
	 * @private
	 */
	enableControl:function(){
		var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			bottomtoolbar = me.getComponent('bottomtoolbar');
		
		var items = toptoolbar.items.items.concat(bottomtoolbar.items.items);
		for(var i in items){
			if(!items[i].autoDisabled){
				items[i].enable();
			}
		}
		me.defaultLoad = true;
	},
	/**
	 * 禁用所有的操作功能
	 * @private
	 */
	disableControl:function(){
		var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			bottomtoolbar = me.getComponent('bottomtoolbar');
		
		var items = toptoolbar.items.items.concat(bottomtoolbar.items.items);
		for(var i in items){
			if(!items[i].autoDisabled){
				items[i].disable();
			}
		}
		me.defaultLoad = false;
	},
	/**
	 * 标题信息更改
	 * @public
	 * @param {} value
	 */
	changeInfo:function(value){
		var me = this;
		var title = value || "";
		me.defaultTitle = title;
		me.setTitle(title);
	},
	/**
	 * 失控处理
	 * @private
	 * @param bo
	 */
	outOfControl:function(bo){
		var me = this,
			title = me.defaultTitle;
			
		if(bo){
			var info = "<a style='color:red;'>    【质控数据失控!】</a>";
			title += info;
		}
		
		me.setTitle(title);
	},
	/**
	 * 处理失控数据
	 * @private
	 * @param {} list
	 */
	doOutOfControl:function(list){
		var me = this;
			bo = false;
		//1:在控;2:警告;3:失控;
		for(var i in list){
			if(list[i].IsControl == me.Enum.QCValueIsControl[2].value){
				bo = true;
				break;
			}
		}
		me.outOfControl(bo);
	},
	/**
	 * 显示、隐藏数据列
	 * @private
	 * @param {} type
	 */
	showOrHideColumns:function(type){
		var me = this;
		
		if(type == 1){
			me.showQCMatColumns(true);
		}else if(type == 2){
			me.showQCMatColumns(false);
		}
	},
	/**
	 * 显示质控物列
	 * @private
	 * @param {} bo
	 */
	showQCMatColumns:function(bo){
		var me = this,
			columns = me.columns;
			QCMatFun = bo ? 'show' : 'hide';
			QCItemFun = bo ? 'hide' : 'show';
		for(var i in columns){
			var dataIndex = columns[i].dataIndex;
			if(dataIndex == 'QCItemSName'){//项目
				columns[i][QCMatFun]();
			}else if(dataIndex == 'QCMatName'){//质控物
				columns[i][QCItemFun]();
			}
		}
	},
	/**
	 * 数据转化
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeData:function(response){
		var me = this;
		var data = Ext.JSON.decode(response.responseText);
		var success = (data.success + '' == 'true' ? true : false);
    	if(!success){
    		me.showError(data.ErrorInfo);
    	}
    	if(data.ResultDataValue && data.ResultDataValue != ''){
    		data.ResultDataValue =data.ResultDataValue.replace(/[\r\n]+/g,'');
    		var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	    	data.list = ResultDataValue.list;
	    	data.count = ResultDataValue.count;
    	}else{
    		data.list = [];
    		data.count = 0;
    	}
    	data.list = me.changeList(data.list);//数据处理
    	me.doOutOfControl(data.list);//失控处理
    	response.responseText = Ext.JSON.encode(data);
    	return response;
	},
	/**
	 * 数据处理
	 * @private
	 * @param {} list
	 * @return {}
	 */
	changeList:function(list){
		var arr = list || [];
		for(var i in arr){
			arr[i].ReportValue = arr[i].ReportValue || '';
		}
		return arr;
	},
	/**
	 * 获取数据集需要的字段
	 * @private
	 * @return {}
	 */
	getStoreFields:function(){
		var me = this;
		var fields = [];
		for(var i in me.columnFields){
			fields.push(me.columnFields[i].dataIndex);
		}
		return fields;
	},
	/**
	 * 显示错误信息
	 * @private
	 * @param {} value
	 */
	showError:function(value){
		var me = this;
		var html = "<center><b style='color:red;font-size:x-large'>" + value + "</b></center>";
		me.getView().update(html);
	},
	/**
	 * 定位选中行
	 * @private
	 * @param {} rowIndex
	 */
	selectRecord:function(rowIndex){
		var me = this;
		var length = me.store.getCount();
		var num = (length-1 > rowIndex) ? rowIndex : length-1;
		if(num >= 0){
			me.getSelectionModel().select(num);
		}
	},
	/**
	 * 根据record获取rowIndex值
	 * @private
	 * @param {} record
	 * @return {}
	 */
	getRowIndexByRecord:function(record){
		var me = this;
		var rowIndex = me.store.findBy(function(rec){
			if(rec == record){return true;}
		});
		return rowIndex;
	},
	/**
	 * 结果处理
	 * @private
	 * @param {} text
	 * @param {} info
	 * @param {} successInfo
	 * @param {} callback
	 */
	showResult:function(text,info,successInfo,callback){
		var me = this;
		me.nowCount++;
		
		var result = Ext.JSON.decode(text);
		if(!result.success){//错误信息暂存
			var err = info + ",错误信息："
			me.ErrorInfo.push(err + result.ErrorInfo);
		}
		
		if(me.nowCount == me.resultCount){
			me.nowCount = 0;//当期处理条数清零
			me.resultCount = 0;//处理总条数清零
			if(me.ErrorInfo.length > 0){
				alertError(me.ErrorInfo.join('</br>'));//错误信息提示
			}else{
				alertInfo(successInfo);//成功提示
				me.load(null,null,true);
			}
			me.ErrorInfo = [];//信息清空
			me.enableControl();//启用功能栏
			if(Ext.typeOf(callback) == 'function'){callback()};
		}
	},
	/**
	 * 变异系数处理
	 * @private
	 * @param {} record
	 * @param {} value
	 * @return {}
	 */
	changeCVValue:function(record,value){
		var me = this,
			ReportValue = value,//用户值
			Target = record.get('Target');//靶值
		
		//用户值必须存在,靶值必须存在,而且都是数字,靶值不能为0
		if(ReportValue && ReportValue != '' && !isNaN(ReportValue)
			&& Target && Target != '' && !isNaN(Target)){
			var r = parseFloat(ReportValue);
			var t = parseFloat(Target);
			if(t != 0){
				//计算方法=绝对值（用户值-靶值）/靶值 * 100
				var cv = 100 * Math.abs(r-t) / t;
				cv = Math.round(cv * 100) / 100;//保留两位小数
				record.set('CVValue',cv);
			}
		}
	},
	/**
	 * 根据dataIndex获取column对象
	 * @private
	 * @param {} dataIndex
	 * @return {}
	 */
	getColumnByDataIndex:function(dataIndex){
		var me = this,
			columns = me.columns;
			
		for(var i in columns){
			if(columns[i].dataIndex == dataIndex){
				return columns[i];
			}
		}
		return null;
	},
	/**
	 * 获取仪器原始数值条数
	 * @private
	 * @param {} value
	 * @return {}
	 */
	getOriglValueNum:function(value){
		var me = this,
			num = 0,
			records = me.store.data.items;
		//数据真实存在 &&	仪器原始数值==value
		for(var i in records){
			if(records[i].get('Id') && records[i].get('Id') != '' && records[i].get('Id')+'' != '-1' 
				&& records[i].get('OriglValue') == value){
				num++;
			}
		}
		return num;
	},
	/**
	 * 获取预备数据
	 * @private
	 */
	getPreliminaryData:function(){
		var me = this,
			date = me.getComponent('toptoolbar').getComponent('date').getRawValue();
			
		me.preliminaryData = null;
		me.hasLoadPreliminaryData = false;
		
		//QC_RJ_GetJudgeLoseControlBackupValue(long longID, string strReceiveTime, int type, string fields, bool isPlanish);
		var url = me.serverUrl.searchPreliminaryData;
		
		url += "?longID=" + me.tid + "&type=" + me.urlType + "&strReceiveTime=" + date;
		
		var callback = function(text){
			var result = Ext.JSON.decode(text);
	        if(result.success){
	            if(result.ResultDataValue && result.ResultDataValue != ""){
		            var data = Ext.decode(result.ResultDataValue);
		            me.preliminaryData = me.changePreliminaryData(data.list);
		            me.hasLoadPreliminaryData = true;
	            }
	        }
		};
		
		getToServer(url,callback,false);
	},
	/**
	 * 处理预备数据
	 * @private
	 * @param {} list
	 */
	changePreliminaryData:function(list){
		var me = this,
			dateArr = [];
		
		var hasDate = function(date){
			for(var i in dateArr){
				if(dateArr[i] == date){
					return true;
				}
			}
			return false;
		};
		
		for(var i=0;i<list.length;i++){
			list[i].ReceiveTime = list[i].ReceiveTime.slice(0,10).replace(/\//g,'-');
			var bo = hasDate(list[i].ReceiveTime);
			if(!bo){
				dateArr.push(list[i].ReceiveTime);
			}
		}
		
		var data = {};
		for(var i in dateArr){
			data[dateArr[i]] = [];
			for(var j in list){
				if(dateArr[i] == list[j].ReceiveTime){
					data[dateArr[i]].push(list[j]);
				}
			}
		}
		return data;
	},
	/**
	 * 质控失控判定
	 * @private
	 * @param {} record
	 */
	getJudgeLoseControlValue:function(record){
		var me = this,
			date = me.getComponent('toptoolbar').getComponent('date'),
			dateStr = date.getRawValue(),
			url = me.serverUrl.judgeLoseControlValue;
		
		//QC_RJ_JudgeLoseControlValue(QCDValue entity, IList<QCDValue> entityList, string fields, bool isPlanish)
		var params = {
			entity:{
				QCDataLotNo:record.QCDataLotNo,
				ReceiveTime:dateStr,
				ReportValue:record.ReportValue,
				QCItemID:record.QCItemID
			},
			entityList:me.getEntityList()
		};
		
		params = Ext.JSON.encode(params);
		
		var callback = function(text){
			me.remainingQuantity--;
			
			var result = Ext.JSON.decode(text);
	        if(result.success){
	            if(result.ResultDataValue && result.ResultDataValue != ""){
		            var data = Ext.decode(result.ResultDataValue);
		            record.set({
			            IsControl:data.IsControl,
			            QCControlInfo:data.QCControlInfo
		            });
	            }
	        }
		};
		
		me.remainingQuantity++;
		
		postToServer(url,params,callback);
	},
	/**
	 * 获取质控判定备用数据
	 * @private
	 * @return {}
	 */
	getEntityList:function(){
		var me = this,
			preliminaryData = me.preliminaryData,
			list = [];
			
		if(preliminaryData){
			var count = preliminaryData.length;
			
		}
		
		return list;
	}
});