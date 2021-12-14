/**
	@name：规则列表
	@author：liangyl
	@version 2020-08-13
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
		
	//分发规则
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_SearchLBTranRuleAndDicNameByHQL?isPlanish=true';
	//删除分发规则
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBTranRule';
    //新增分发规则
	var	ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBTranRule';

//	var table_ind = null;
	var SectionID = null;
	var RuleList = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			DEFAULTDRULETYPEID:null,
			cols:[[
				{field:'LBTranRule_Id',title:'ID',width:150,hide:true},
				{field:'LBTranRule_CName',title:'规则名称',width:100},
				{field:'LBTranRule_SampleNoRule',title:'样本号规则',width:100},
				{field:'LBTranRule_SampleNoMin',title:'起始样本号',width:100},
				{field:'LBTranRule_SampleNoMax',title:'终止样本号',width:100},
				{field:'LBTranRule_NextSampleNo',title:'下一样本号',width:100},
				{field:'LBTranRule_PrintCount',title:'打印份数',width:100},
				{field:'LBTranRule_TestDelayDates',title:'检验日期',width:100},
				{field:'LBTranRule_SampleTypeID',title:'样本类型ID',width:100,hide:true},
				{field:'LBTranRule_SampleTypeName',title:'样本类型',width:100},
				{field:'LBTranRule_SamplingGroupID',title:'采样组ID',width:100,hide:true},
				{field:'LBTranRule_SamplingGroupName',title:'采样组',width:100},
				{field:'LBTranRule_ResetType',title:'复位周期',width:100},
				{field:'LBTranRule_SickTypeID',title:'就诊类型id',width:100,hide:true},
				{field:'LBTranRule_SickTypeName',title:'就诊类型',width:100},
				{field:'LBTranRule_UrgentState',title:'急查标识',width:100},
				{field:'LBTranRule_IsFollow',title:'跟随',width:100,templet:function(d){
					var arr = [
						'<div style="color:#FF5722;text-align:center;">否</div>',
						'<div style="color:#009688;text-align:center;">是</div>'
					];
					var result = d.LBTranRule_IsFollow == 'true' ? arr[1] : arr[0];
					
					return result;
				}},
				{field:'LBTranRule_IsPrintProce',title:'是否打印检验流转单',width:100,templet:function(d){
					var arr = [
						'<div style="color:#FF5722;text-align:center;">否</div>',
						'<div style="color:#009688;text-align:center;">是</div>'
					];
					var result = d.LBTranRule_IsPrintProce == 'true' ? arr[1] : arr[0];
					
					return result;
				}},
				{field:'LBTranRule_IsUseNextNo',title:'启用下一样本号',width:100,templet:function(d){
					var arr = [
						'<div style="color:#FF5722;text-align:center;">否</div>',
						'<div style="color:#009688;text-align:center;">是</div>'
					];
					var result = d.LBTranRule_IsUseNextNo == 'true' ? arr[1] : arr[0];
					
					return result;
				}},
				{field:'LBTranRule_TranWeek',title:'分发星期',width:100},
				{field:'LBTranRule_TranToWeek',title:'分发到星期',width:100},
				{field:'LBTranRule_SampleTypeID',title:'样本类型ID',width:100,hide:true},
				{field:'LBTranRule_TestTypeName',title:'样本类型',width:100},
				{field:'LBTranRule_ClientID',title:'外送单位ID',width:100,hide:true},
				{field:'LBTranRule_ClientName',title:'外送单位',width:100},
				{field:'LBTranRule_DeptID',title:'送检单位ID',width:100,hide:true},
				{field:'LBTranRule_DeptName',title:'送检单位',width:100},
				{field:'LBTranRule_DateRand',title:'时间范围',width:120,templet:function(d){
					var v= "";
					var UseTimeMin =  d.LBTranRule_UseTimeMin;
					var UseTimeMax =  d.LBTranRule_UseTimeMax;
                    if(UseTimeMin && UseTimeMax ){
                    	v = timeResult(UseTimeMin)+'-'+timeResult(UseTimeMax);
                    }
					return v;
				}},
				{field:'LBTranRule_UseTimeMin',title:'起始时间',width:100,hide:true},
				{field:'LBTranRule_UseTimeMax',title:'终止时间',width:100,hide:true},
				{field:'LBTranRule_IsAutoPrint',title:'自动打印',width:100,templet:function(d){
					var arr = [
						'<div style="color:#FF5722;text-align:center;">否</div>',
						'<div style="color:#009688;text-align:center;">是</div>'
					];
					var result = d.LBTranRule_IsUseNextNo == 'true' ? arr[1] : arr[0];
					
					return result;
				}},
				{field:'LBTranRule_SampleNoType',title:'样本号类型',width:100,templet:function(d){
					var result = d.LBTranRule_SampleNoType ;
					if(result=='1')result = '使用条码号作为样本号';
					if(result=='2')result = '一个条码分发到多个小组时使用相同样本号';
					return result;
				}},
				{field:'LBTranRule_ProceModel',title:'检验流转单模板',width:100},
				{field:'LBTranRule_DispenseMemo',title:'分发备注',width:100},
				{field:'LBTranRule_DispOrder',title:'显示次序',width:100,hide:true},
				{field:'LBTranRule_LBTranRuleType_Id',title:'分发规则类型id',width:100,hide:true},
				{field:'LBTranRule_LBTranRuleType_CName',title:'分发规则类型',width:100,hide:true}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({},me.config,RuleList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(id){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		SectionID = id;
	    if(!SectionID) {
	    	me.clearData();
	    	return false;
	    }
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var whereObj = {'where':'lbtranrule.SectionID='+SectionID};
		var url = GET_LIST_URL;
		me.uxtable.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	Class.pt.clearData = function(id){
		var  me = this;
		
		me.uxtable.instance.reload({
			url:'',
			data:[]
		});
	};
	 //删除方法 
	Class.pt.onDelClick = function(id,callback){
		var me = this;
        if(!id)return;
    	var url = DEL_URL +'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
					layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
//                  me.loadData(SectionID);
                    callback();
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	};
	//打开分发规则表单窗体
    Class.pt.openForm = function(id){
    	var me = this;
		var title = '新增分发规则';
		if(id)title = '编辑分发规则';
	    var win = $(window),
		    maxWidth = win.width()-100,
			maxHeight = win.height()-80;
			maxHeight = maxHeight>500 ? 500 : maxHeight;
		layer.open({
			title:title,
			type:2,
			content:'form.html?id=' + id +'&SectionID='+SectionID+'&DEFAULTDRULETYPEID='+me.config.DEFAULTDRULETYPEID+'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:false,
			area:['620px',maxHeight+'px']
		});
	};
	 //分发规则类型数据加载
    Class.pt.rule = function(id,callback){
    	var me = this;
    	SectionID = id;
    	var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_LIST_URL,
			type:'get',
			async:false,  //使用同步的方式,true为异步方式
			data:{
				page:1,
				limit:1000,
				fields:'LBTranRule_Id'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				me.addrule(callback);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
    }
	 //新增分发规则,默认规则类型id为0,
    Class.pt.addrule = function(callback){
    	var me = this;
    	var loadIndex = layer.load();//开启加载层
    	var sysdate = new Date();//应该取集成平台的系统服务时间
		var date1 = uxutil.date.toString(sysdate,true);
		var strBeginTime =  date1+" 00:00:00";
		var strEndTime =  date1+" 23:59:59";
    	var entity ={
			IsUse:1,
			IsUseNextNo:0,//是否启用
			SectionID:SectionID,
			DispOrder:9999,//优先次序
			SampleNoMin:1,//样本区间
			SampleNoMax:9999,//样本区间
			IsAutoPrint:0,//自动打印
			IsFollow:1,//允许跟随
		    TestDelayDates:0, //检验日期	
		    UseTimeMin:uxutil.date.toServerDate(strBeginTime),
		    UseTimeMax:uxutil.date.toServerDate(strEndTime)
		};
		entity.LBTranRuleType={
    		Id:me.config.DEFAULTDRULETYPEID,
    		DataTimeStamp:[0,0,0,0,0,0,0,0]
    	};
    	uxutil.server.ajax({
            url: ADD_URL,
            type:'POST',
            async:false,  //使用同步的方式,true为异步方式
            data: JSON.stringify({entity:entity})
        }, function (data) {
        	layer.close(loadIndex);//关闭加载层
            if (data.success) {
            	callback();
            } else {
                layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    };
	//联动监听
	Class.pt.initListeners = function(){
		var me = this;
		
	};
	//时分时间还原解析(yy-mm-dd hh:mm:ss 只截取hh:mm)
	function timeResult(time){
		var value = time;
		if(uxutil.date.isValid(time)){
			var str1 = time.split(' ');
			if(str1.length>0 && str1[1]){
				var str2 = str1[1].split(':');
	            value = str2[0]+":"+str2[1]+":"+str2[2];
			}
		}
		return value;
	};
	//主入口
	RuleList.render = function(options){
		var me = new Class(options);
		me.uxtable = uxtable.render(me.config);
		me.uxtable.instance.reload({
			url: '',
			data:[]
		});
		//监听事件
		me.initListeners();
		return me;
	};

	
	//暴露接口
	exports('RuleList',RuleList);
});