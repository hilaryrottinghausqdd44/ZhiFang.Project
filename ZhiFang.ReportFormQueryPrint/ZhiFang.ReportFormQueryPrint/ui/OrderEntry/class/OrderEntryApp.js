/**
 * 医嘱录入
 * @author Jcall
 * @version 2014-10-23
 */
Ext.define('Shell.OrderEntry.class.OrderEntryApp',{
	extend:'Shell.ux.panel.AppPanel',
	
	title:'医嘱录入',
	layout:{type:'border',regionWeights:{north:3,west:2,south:1}},
	
	/**默认数据条件*/
	listParams:'',
	/**错误信息*/
	errorInfo:[],
	/**加载字典错误信息*/
	dictionaryErrorInfo:[],
	/**字典服务地址*/
	dictionaryUrl:'/OrderService.svc/QueryData',
	/**获取病人信息服务地址*/
	patInfoUrl:'/OrderService.svc/GetPatientInfo',
	/**字典数据*/
	dictionaryData:{},
	/**病历号*/
	PatNo:null,
	/**HIS科室ID*/
	HisDeptNo:null,
	/**就诊类型*/
	SickTypeNo:null,
	/**HIS医生*/
	HisDoctorNo:null,
	
	/**获取定义参数*/
	getParamsArray:function(){
		//定义标准的参数列表,自动过滤掉非标准的参数
		//病历号、HIS科室ID、就诊类型、HIS医生
		return ['PATNO','HISDEPTNO','SICKTYPENO','HISDOCTORNO'];
	},
	
	/**渲染完后处理*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		
		me.initListWhere();//初始化列表的条件内容
		
		me.apps = [{
			className:'Shell.OrderEntry.class.OrderEntryList',
			itemId:'OrderEntryList',header:false,
			width:540,region:'west',
			split:true,collapsible:true,
			params:me.listParams
		},{
			className:'Shell.OrderEntry.class.OrderEntryInfo',
			itemId:'OrderEntryInfo',header:false,region:'center',
			HisDeptNo:me.HisDeptNo,SickTypeNo:me.SickTypeNo
		}];
		
		me.callParent(arguments);
	},
	
	/**初始化列表的条件内容*/
	initListWhere:function(){
		var me = this,
			array = me.getParamsArray(),
			len = array.length,
			params = Shell.util.Path.getRequestParams(),
			pars = {};
			
		for(var i in params){
			var bo = Shell.util.String.inArray(i,array);
			if(bo) pars[i] = params[i];
		}
		
		//校验定义的参数是否已全部传递
		me.errorInfo = [];
		for(var i=0;i<len;i++){
			var bo = pars[array[i]];
			if(!bo){
				me.errorInfo.push("缺失参数:<b style='color:red;'>" + array[i] + "</b>");
			}
		}
		
		me.listParams = pars;
		me.PatNo = pars.PATNO;
		me.HisDeptNo = pars.HISDEPTNO;
		me.SickTypeNo = pars.SICKTYPENO;
		me.HisDoctorNo = pars.HISDOCTORNO;
	},
	
	/**加载所有的字典数据*/
	loadAllDictionary:function(){
		var me = this,
			dictionaryUrl = Shell.util.Path.rootPath + me.dictionaryUrl,
			patInfoUrl = Shell.util.Path.rootPath + me.patInfoUrl;
			
		//顺序加载所有字典
		me.dictionaryErrorInfo = [];
		me.dictionaryData = {};
		//年龄单位字典
		me.getToServer(dictionaryUrl + "?tableName=AgeUnit&fields=AgeUnitNo,CName",me.loadDictionary("AgeUnitList","年龄单位字典"),false);
		//科室字典
		me.getToServer(dictionaryUrl + "?tableName=Department&fields=DeptNo,CName",me.loadDictionary("DeptList","科室字典"),false);
		//病区字典
		me.getToServer(dictionaryUrl + "?tableName=District&fields=DistrictNo,CName",me.loadDictionary("DistrictList","病区字典"),false);
		//医生字典
		me.getToServer(dictionaryUrl + "?tableName=Doctor&fields=DoctorNo,CName",me.loadDictionary("DoctorList","医生字典"),false);
		//性别字典
		me.getToServer(dictionaryUrl + "?tableName=GenderType&fields=GenderNo,CName",me.loadDictionary("GenderList","性别字典"),false);
		//就诊类型字典
		me.getToServer(dictionaryUrl + "?tableName=SickType&fields=SickTypeNo,CName",me.loadDictionary("SickTypeList","就诊类型字典"),false);
		//执行科室字典
		me.getToServer(dictionaryUrl + "?tableName=SectorType&fields=SectorTypeNo,SectorName",me.loadDictionary("SectorTypeList","执行科室"),false);
		//检验类型字典
		me.getToServer(dictionaryUrl + "?tableName=TestType&fields=TestTypeNo,CName",me.loadDictionary("TestTypeList","检验类型字典"),false);
		//收费类型字典
		me.getToServer(dictionaryUrl + "?tableName=ChargeType&fields=ChargeNo,CName",me.loadDictionary("ChargeTypeList","收费类型字典"),false);
		
		//病人信息
		me.getToServer(patInfoUrl + "?patno=" + me.PatNo + "&sickTypeNo=" + me.SickTypeNo + "&hisDeptNo=" + me.HisDeptNo + 
			"&hisDoctorNo=" + me.HisDoctorNo,me.loadPatInfo("PatInfo","病人信息"),false);
	},
	
	/**加载字典*/
	loadDictionary:function(par,name){
		var me = this,
			infoField = 'ResultDataValue';
			
		return function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				me.dictionaryData[par] = Ext.JSON.decode(result[infoField]);
			}else{
				me.dictionaryErrorInfo.push("加载<b style='color:red;'>" + name + "</b>出错!" + result.ErrorInfo);
			}
		};
	},
	/**加载病人信息*/
	loadPatInfo:function(par,name){
		var me = this,
			infoField = 'ResultDataValue';
			
		return function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				me.dictionaryData[par] = result[infoField];
			}else{
				me.dictionaryErrorInfo.push("加载<b style='color:red;'>" + name + "</b>出错!" + result.ErrorInfo);
			}
		};
	},
	
	/**页面生成后处理*/
	initListeners:function(){
		var me = this,
			OrderEntryList = me.getComponent('OrderEntryList'),
			OrderEntryInfo = me.getComponent('OrderEntryInfo'),
			OrderItemsTree = OrderEntryInfo.getComponent('OrderItemsTree');
			
		//参数接收错误
		if(me.errorInfo.length > 0){
			Shell.util.Msg.showError(me.errorInfo.join("</br>"));
			return;
		}
		
		//加载所有的字典数据
		me.loadAllDictionary();
		//字典加载错误
		if(me.dictionaryErrorInfo.length > 0){
			Shell.util.Msg.showError(me.dictionaryErrorInfo.join("</br>"));
			return;
		}
		
		//联动监听
		OrderEntryList.on({
			select:function(rowmodel,record){
				Shell.util.Action.delay(function(){
					var id = record.get(OrderEntryList.PKColumn);
					if(record.get('DoInputflag') + '' == '1'){
						OrderEntryInfo.infoShow(id);
					}else{
						OrderEntryInfo.infoEdit(id);
					}
				},null,500);
			},
			addClick:function(){
				OrderEntryInfo.infoAdd(me.dictionaryData.PatInfo);
			},
			editClick:function(grid,but,id,record){
				if(record.get('DoInputflag') + '' == '1'){
					OrderEntryInfo.infoShow(id);
				}else{
					OrderEntryInfo.infoEdit(id);
				}
			},
			afterload:function(grid,records,successful){
				if(records.length == 0){
					OrderEntryInfo.infoAdd(me.dictionaryData.PatInfo);
				}
			}
		});
		
		OrderEntryInfo.on({
			save:function(panel,id){
				if(id){
					OrderEntryList.autoSelect = id + '';
				}
				OrderEntryList.load(null,true);
			}
		});
		
		//病人信息-执行科室默认选中第一个
		if(me.dictionaryData.SectorTypeList){
			me.dictionaryData.PatInfo.ExecDeptNo = me.dictionaryData.SectorTypeList[0].SectorTypeNo;
		}
		//病人信息-检验类型默认选中第一个
		if(me.dictionaryData.TestTypeList){
			me.dictionaryData.PatInfo.TestTypeNo = me.dictionaryData.TestTypeList[0].TestTypeNo;
		}
		
		//字典数据赋值
		OrderEntryInfo.setAllDictionary(me.dictionaryData);
		//科室信息列表
		OrderEntryList.setDeptList(me.dictionaryData.DeptList);
		//加载列表数据
		OrderEntryList.load(null,true);
		//加载项目树数据
		OrderItemsTree.load(null,true);
	}
});